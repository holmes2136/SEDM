Imports System.Windows.Forms
Imports System.Net.Mail
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Data
Imports EDM.Holmes.DB
Imports System.IO
Imports System.Text
Imports EDM.Holmes.EDMSetting
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports EDM.Holmes.Tools
Imports EDM.Holmes.Project
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Cust
Imports EDM.Holmes.Mail.MailClass

Public Class Main


    Private SMSAccount As String
    Private SMSPwd As String

    Private Sender As String

    'updated rpt data or not
    Private RptUptTime As String


    'updated cust data or not
    Private CustUptTime As String


    '時間戳記不應該放在Config裡面,否則在大量散撥時會產生問題
    Private LastOpenEDMtime As String

    '今日生日客戶人數
    Private todayBirthdayCust As String



    


    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '-------------------Test User-------------------------
        Dim User As New User

        Dim myUser As EDM.Holmes.VO.User.User = User.getUserByLogin("test", "test")

        If Not myUser Is Nothing Then

            User._LoginId = "test"
            User._LoginPwd = "test"
            User._MailServerType = myUser._MailServerType
            User._MailSender = myUser._MailSender
            User._MailSign = myUser._MailSign
            User._SMSLoginPwd = myUser._SMSLoginPwd
            User._SMSLoginId = myUser._SMSLoginId



        End If

        '-------------------Test User-------------------------

        '載入網頁
        'Me.WebBrowser1.Navigate("http://www.3552363.tw/site/EDMPlatfom/Default.aspx")

        Me._Sender = User.mailSender

        Me._SMSAccount = User.SMSLoginid

        Me._SMSPwd = User.SMSLoginPwd

        '1.顯示軟體上次啟動時間 
        Me.lbl_LastOpenEDMtime.Text = "上次登入 " & Me._LastOpenEDMtime

        '2 set the config LastOpenEDMtime property
        Me._LastOpenEDMtime = DateTime.Now.ToString




        '--------------Test ID--------
        'User.LoginId = "test"
        'User.LoginPwd = "test"

        '2011/10/31  暫時close
        'TodayBirthCustListInit()


    End Sub


    ''' <summary>
    ''' 今天生日客戶清單初始化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TodayBirthCustListInit()

        Dim cust As New EDM.Holmes.Cust.Cust

        Me.BindingSource1.DataSource = cust.getTodayIsCustBirthtoDt(DateTime.Now.ToString("yyyy-MM-dd"))

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.DataSource = Me.BindingSource1


        Me.DataGridView1.Columns("CustId").Visible = True
        Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
        Me.DataGridView1.Columns("CustName").Visible = True
        Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
        Me.DataGridView1.Columns("CustMobile").Visible = True
        Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("CustTelPhone").Visible = True
        Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
        Me.DataGridView1.Columns("CustAddress").Visible = True
        Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
        Me.DataGridView1.Columns("CustEmail").Visible = True
        Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"



        '增加checkbox欄位
        addCheckToGridView()

    End Sub

  

    ''' <summary>
    ''' 更新日報
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_UptRpt1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UptRpt1.Click

        Dim msg As MsgBoxResult

        msg = MsgBox("是否需要更新日報資料", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then

            Dim isEqualNow As Boolean = Me._RptUptTime.Equals(DateTime.Now.ToString("yyyy-MM-dd"))


            Try


                If isEqualNow Then

                    MsgBox("您今日已更新過")
                Else

                    Me.getNewestDayRpt1(Me._RptUptTime, EDMSetting.getShopId)

                    Me._RptUptTime = DateTime.Now.ToString("yyyy-MM-dd")


                    MsgBox("更新完畢")


                End If

            Catch ex As Exception

                MsgBox("Error_Code:564_Main_Error" & vbCrLf & ex.Message)

            End Try

        End If


    End Sub


    ''' <summary>
    ''' 更新客戶資料
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_UptCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UptCust.Click

        Dim msg As MsgBoxResult

        msg = MsgBox("是否需要更新客戶資料", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then


            Dim isEqualNow As Boolean = Me._CustUptTime.Equals(DateTime.Now.ToString("yyyy-MM-dd"))

            Try


                If isEqualNow Then

                    MsgBox("您今日已更新過")
                Else



                    Me.getNewestCust(Me._CustUptTime, EDMSetting.getShopId)

                    Me._CustUptTime = DateTime.Now.ToString("yyyy-MM-dd")



                    MsgBox("更新完畢")



                End If

            Catch ex As Exception

                MsgBox("Error_Code:589_Main_Error" & vbCrLf & ex.Message)

            End Try


        End If

    End Sub

 

    ''' <summary>
    ''' EX:(判斷依據為SERVER的日報一資料表的PublishDay)
    ''' 1.若上次開啟軟體時間是9/15.則會傳回9/15號以後的日報記錄
    ''' 2.新增至EDM裡面的日報1資料庫
    ''' </summary>
    ''' <param name="lastOpenEDMtime">上次開啟EDM時間</param>
    ''' <param name="shopid">分店ID</param>
    ''' <remarks></remarks>
    Private Sub getNewestDayRpt1(ByVal lastOpenEDMtime As String, ByVal shopid As String)

        If My.Computer.Network.IsAvailable Then


            lastOpenEDMtime = FilterTime(lastOpenEDMtime)

            '自訂
            'lastOpenEDMtime = "2011-09-13"

            Dim rpts As New EDM.Rpt1.Rpt1

            Dim rpt As New EDM.Holmes.Rpt.Rpt1

            rpt.InsertNewestRpt1(rpts.getNewestRpt1(shopid, lastOpenEDMtime))
        Else

            MsgBox("您目前並無連上網路,無法進行工作")
            Exit Sub

        End If


        'Return Nothing

    End Sub

    ''' <summary>
    ''' EX:(判斷Server客戶資料表的SystemTime ps客戶建立時間)
    ''' 1.若上次開啟軟體時間是9/15.則會傳回9/15號以後的新增的客戶
    ''' 2.新增至EDM裡面的日報1資料庫
    ''' </summary>
    ''' <param name="lastOpenEDMtime">上次開啟EDM軟體時間</param>
    ''' <param name="shopid">分店ID</param>
    ''' <remarks></remarks>
    Private Sub getNewestCust(ByVal lastOpenEDMtime As String, ByVal shopid As String)


        lastOpenEDMtime = FilterTime(lastOpenEDMtime)

        'MsgBox(lastOpenEDMtime)

        If lastOpenEDMtime = Date.Now.ToString("yyyy-mm-dd") Then
            MsgBox("您今日已更新過")
            Exit Sub
        End If

        '自訂
        'lastOpenEDMtime = "2011-06-25"

        Dim cs As New EDM.Cust.Cust

        Dim cust As New EDM.Holmes.Cust.Cust

        If My.Computer.Network.IsAvailable Then

            cust.InsertNewestCust(cs.getNewestCust(shopid, lastOpenEDMtime))
        Else
            MsgBox("您目前並無連上網路,無法進行工作")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    '''  提醒今日生日的客戶有誰
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CustTodayIsBirth()

        Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")

        Dim cust As New EDM.Holmes.Cust.Cust


        '取得選取的客戶CustId
        Dim ret As Collection = cust.getCheckedEmailLst(Me.DataGridView1, 0)

        '若今日生日客戶大於一人
        If ret.Count > 0 Then

            Dim msg As MsgBoxResult = MsgBox("是否寄發生日禮卡", MsgBoxStyle.YesNo)

            '取得專案表第一個專案NodeId,把新增的專案都新增至此

            'Dim prj As New Project
            'Dim firstNode As String = prj.getFirstPrj


            If msg = MsgBoxResult.Yes Then

           

                My.Forms.SendMail._SendCustLst = ret

                My.Forms.SendMail._Mailto = EDM.Holmes.Mail.MailClass.FriendlyReceiver(ret)

                My.Forms.SendMail.Show()

                SendMail.Left = 270

                SendMail.Top = 195

            End If

        End If



    End Sub

    ''' <summary>
    ''' 取得專案資料表的第一個NodeId
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getFirstPrj() As String

        Dim returnVal As String = ""

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "select Top 1 NoteId from 專案表 order by NoteId ASC"

        cmd.Connection = conn

        conn.Open()

        Using conn
            returnVal = cmd.ExecuteScalar

        End Using

        Return returnVal



    End Function


    Private Function FilterTime(ByVal lastOpenEDMtime As String) As String

        Dim i As Integer = lastOpenEDMtime.IndexOf(" ")

        Dim lastOpenEDMtime2 = lastOpenEDMtime

        lastOpenEDMtime2 = lastOpenEDMtime2.Substring(0, 10)

        lastOpenEDMtime2 = lastOpenEDMtime2.Replace("/", "-")

        Return lastOpenEDMtime2

    End Function




    ''' <summary>
    ''' 結束程式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_EndSys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EndSys.Click

        'My.Forms.Login.Close()


        'Me.Close()

        Application.Exit()

  
    End Sub

    ''' <summary>
    ''' 寄信給今日壽星
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SendM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendM.Click


        CustTodayIsBirth()

    End Sub

    ''' <summary>
    ''' 寄發簡訊給今日壽星
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendSMS.Click

        Dim cust As New EDM.Holmes.Cust.Cust

        Dim sb As New StringBuilder

        Dim ret As Collection = Nothing

        '取得選取的名單
        Try
            ret = cust.getCheckedList(Me.DataGridView1, 0, "CustMobile")
        Catch ex As Exception
            MsgBox("Error_Code:558_Main_Error")
            Exit Sub
        End Try

        For Each mobile As String In ret

            sb.Append(IIf(mobile <> "", mobile & ",", ""))

        Next

        '設定客戶手機名單
        My.Forms.SMS._CustMobileLst = ret

        '顯示所取得的客戶手機名稱
        My.Forms.SMS._SelectedPrjName = sb.ToString

        '顯示專案名稱
        My.Forms.SMS.lbl_PrjName.Visible = True

        My.Forms.SMS.Show()



    End Sub

  
  
    ''' <summary>
    ''' 顯示範本維護功能
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        My.Forms.SampleMaintain.Left = 270
        My.Forms.SampleMaintain.Top = 195
        My.Forms.SampleMaintain.Show()

    End Sub


    ''' <summary>
    '''  新增CheckBox到GridView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addCheckToGridView()

        Dim ch As New DataGridViewCheckBoxColumn
        ch.HeaderText = "勾選"
        ch.Name = "勾選"
        ch.Width = 40
        DataGridView1.Columns.Insert(0, ch)

    End Sub


    ''' <summary>
    ''' 全選
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SelectAll.Click


        For i As Integer = 0 To DataGridView1.Rows.Count - 1

            Dim chk As DataGridViewCheckBoxCell = DataGridView1.Rows(i).Cells(0)

            If (chk.Value Is Nothing) Or (chk.Value = "false") Then
                chk.Value = True
            Else
                chk.Value = False

            End If

        Next


    End Sub

    ''' <summary>
    ''' 最小化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.StyleChanged

        If Me.WindowState = FormWindowState.Minimized Then
            'Me.ShowInTaskbar = True
            Me.Hide()


        End If

    End Sub

    ''' <summary>
    ''' 按下系統圖示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick

        Me.ShowInTaskbar = True
        Me.Show()
        Me.WindowState = FormWindowState.Maximized

    End Sub


  

    Private Sub 簡訊設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 簡訊設定ToolStripMenuItem.Click


        My.Forms.SMSSetting.Show()
        My.Forms.SMSSetting.StartPosition = FormStartPosition.CenterScreen

    End Sub

    Private Sub 網路信箱設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 網路信箱設定ToolStripMenuItem.Click

        My.Forms.WebMailSetting.Show()
        My.Forms.WebMailSetting.StartPosition = FormStartPosition.CenterScreen

    End Sub

    Private Sub 常用設定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 常用設定ToolStripMenuItem.Click




        My.Forms.UsuallySetting.Show()
        My.Forms.UsuallySetting.StartPosition = FormStartPosition.CenterScreen


    End Sub


    ''' <summary>
    ''' 功能列的 關於 按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click


        My.Forms.About.Show()
        My.Forms.About.StartPosition = FormStartPosition.CenterScreen

    End Sub


    Private Sub 簡訊範本維護ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 簡訊範本維護ToolStripMenuItem.Click


        
        My.Forms.SMSSamp.Show()

        My.Forms.SMSSamp.StartPosition = FormStartPosition.CenterScreen




    End Sub

    Private Sub 信件範本維護ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 信件範本維護ToolStripMenuItem.Click


        My.Forms.MailSamp.Show()

        My.Forms.MailSamp.StartPosition = FormStartPosition.CenterScreen

    End Sub

    Private Sub 排程歷史記錄ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 排程歷史記錄ToolStripMenuItem.Click


       
        My.Forms.WorkInfo.Show()
        My.Forms.WorkInfo.StartPosition = FormStartPosition.CenterScreen


    End Sub

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' 建立子表單的新執行個體。
        Dim ChildForm As New System.Windows.Forms.Form
        ' 將它變成這個 MDI 表單的子表單，然後才顯示。
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "視窗" & m_ChildFormNumber

        ChildForm.Show()
    End Sub



    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 使用 My.Computer.Clipboard 將選取的文字或影像插入剪貼簿
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 使用 My.Computer.Clipboard 將選取的文字或影像插入剪貼簿
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        '使用 My.Computer.Clipboard.GetText() 或 My.Computer.Clipboard.GetData 從剪貼簿擷取資訊。
    End Sub





    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' 關閉父表單的所有子表單。
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0




    Private Sub btn_Desc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Desc.Click


        My.Forms.Desc.Show()
        My.Forms.Desc.StartPosition = FormStartPosition.CenterScreen


    End Sub





    Private Sub btn_SendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendMail.Click

        My.Forms.SendMail.Show()
        My.Forms.SendMail.StartPosition = FormStartPosition.CenterScreen

    End Sub


    Private Sub btn_CustList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CustList.Click

     

        My.Forms.AddProject.ShowDialog()
        My.Forms.AddProject.StartPosition = FormStartPosition.CenterScreen


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click


        My.Forms.Seting.Show()
        My.Forms.Seting.StartPosition = FormStartPosition.CenterScreen


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click



        My.Forms.WorkArrange.Show()
        My.Forms.WorkArrange.StartPosition = FormStartPosition.CenterScreen



    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        My.Forms.SMS.Show()
        My.Forms.SMS.StartPosition = FormStartPosition.CenterScreen


    End Sub

    Public Property _SMSAccount()
        Get
            Return Me.SMSAccount
        End Get
        Set(ByVal value)
            Me.SMSAccount = value
        End Set
    End Property

    Public Property _SMSPwd()
        Get
            Return Me.SMSPwd
        End Get
        Set(ByVal value)
            Me.SMSPwd = value
        End Set
    End Property

    Public Property _Sender()
        Get
            Return Me.Sender
        End Get
        Set(ByVal value)
            Me.Sender = value
        End Set
    End Property


    Public Property _RptUptTime()
        Get
            'if RptUptTime is null,then get the last rpt data from server
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("RptUptTime")) Then
                Return ConfigurationManager.AppSettings("RptUptTime")
            Else
                Dim rpt As New EDM.Holmes.Rpt.Rpt1
                Dim str As DateTime = rpt.getLastRptTime()

                Return str.ToString("yyyy-MM-dd")
            End If

        End Get
        Set(ByVal value)

            '需要過濾特殊符號.否則在Config恐怕會失效

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            Dim app As AppSettingsSection = config.AppSettings
            app.Settings("RptUptTime").Value = value

            Try
                config.Save(ConfigurationSaveMode.Modified)
            Catch ex As Exception
                MsgBox("RptUptTime套用失敗")
                Exit Property
            End Try

            Me.RptUptTime = value

        End Set
    End Property

    Public Property _CustUptTime()
        Get
            'if CustUptTime is null,then get the last Cust data from server
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("CustUptTime")) Then
                Return ConfigurationManager.AppSettings("CustUptTime")
            Else
                Dim cust As New EDM.Holmes.Cust.Cust
                Dim str As DateTime = cust.getLastCustTime()
                Return str.ToString("yyyy-MM-dd")
            End If
        End Get
        Set(ByVal value)

            '需要過濾特殊符號.否則在Config恐怕會失效

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            Dim app As AppSettingsSection = config.AppSettings
            app.Settings("CustUptTime").Value = value

            Try
                config.Save(ConfigurationSaveMode.Modified)
            Catch ex As Exception
                MsgBox("CustUptTime套用失敗")
                Exit Property
            End Try

            Me.CustUptTime = value

        End Set
    End Property

    Public Property _LastOpenEDMtime()
        Get
            If String.IsNullOrEmpty(ConfigurationManager.AppSettings("LastOpenEDMtime")) Then
                Return DateTime.Now.ToString
            Else
                Return ConfigurationManager.AppSettings("LastOpenEDMtime")

            End If
        End Get
        Set(ByVal value)

            '需要過濾特殊符號.否則在Config恐怕會失效

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            Dim app As AppSettingsSection = config.AppSettings
            app.Settings("LastOpenEDMtime").Value = value

            Try
                config.Save(ConfigurationSaveMode.Modified)
            Catch ex As Exception
                MsgBox("LastOpenEDMtime套用失敗")
                Exit Property
            End Try

            Me.LastOpenEDMtime = value

        End Set
    End Property

    '記錄今日生日客戶
    Public ReadOnly Property _TodayBirthdayCust()
        Get
            Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")

            Dim cust As New EDM.Holmes.Cust.Cust

            '取得今日生日客戶
            Dim ret As Collection(Of String) = cust.getTodayIsCustBirth(today)

            Dim custCnt As Integer = ret.Count

            Return custCnt
        End Get

    End Property
End Class
