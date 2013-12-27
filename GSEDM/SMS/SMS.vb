Imports KOTSMSAPI
Imports System.Text.RegularExpressions
Imports EDM.Holmes.Cust
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.Text
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Tools
Imports EDM.Holmes.VO.Work
Imports EDM.Holmes.Mail
Imports EDM.Holmes.Mail.SendType
Imports EDM.Holmes.VO.Sample
Imports System.Configuration
Imports EDM.Holmes.Sms

''' <summary>
''' Goverment SMS
''' </summary>
''' <remarks></remarks>
Public Class SMS

    '選擇的範本名稱
    Private selectedSampName As String
    '選擇的範本ID
    Private selectedSampId As String
    '選擇的範本內容
    Private selectedSampContent As String
    '選擇的專案名稱
    Private selectedPrjName As String
    '選擇的客戶數目
    Private CustCnt As Integer

    '儲存手機清單
    'Private custMobileLst As Collection(Of String)
    Private custMobileLst As Collection

    '儲存歷程紀錄
    Public workHistory As New WorkHistory

    '儲存SQL SERVER的Serial record
    Private ReturnSerial As Integer

    'if lbl_PrjName value changed
    Public Event PrjName_Chg(ByVal prjName As String)

    Public Event CustCnt_Chg(ByVal cnt As Integer)

    '儲存總結果
    Dim sb As StringBuilder

   

    Private Sub SMS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.lbl_PrjName.Text = Me._SelectedPrjName

        Me.lbl_SampName.Text = Me._SelectedSampName


    End Sub

    Private Sub SMS_CustCnt_Chg(ByVal cnt As Integer) Handles Me.CustCnt_Chg

        'Me.lbl_CustCnt.Visible = True

        'Me.lbl_CustCnt.Text = String.Format("({0})名客戶", cnt)

        Dim msg As MsgBoxResult = MsgBox(String.Format("發送簡訊清單共有{0}名客戶,請繼續輸入簡訊內容", cnt))

        

    End Sub


    ''' <summary>
    ''' 啟動發動簡訊
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


        If Me._CustMobileLst.Count > 0 Then

            Me.SendSMS()



            '儲存至寄件備份--------------------------------------
            Dim bp As New EDM.Holmes.Mail.MailBp(Me.lbl_PrjName.Text.Trim, Me.txt_Content.Text.Trim, "")

            bp.SaveToSms()
            '----------------------------------------------------


        Else
            MsgBox("沒有任何通訊名單!")
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' Send
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click

        Dim msg As MsgBoxResult = MsgBox("確定要寄發簡訊嗎?", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then

            '是否存入排程或者範本
            IsSaveToWorkArrangeOrSamp()

            '允許回報進度
            BackgroundWorker1.WorkerReportsProgress = True
            '允許取消
            BackgroundWorker1.WorkerSupportsCancellation = True

            '啟動背景作業
            If Me.BackgroundWorker1.IsBusy Then
                MsgBox("工作已在進行中.....")
                Exit Sub
            Else


                Dim ret As Collection = Me._CustMobileLst

               
                If ret.Count > 0 Then

                    Me.progress.Maximum = ret.Count - 1

                End If

                BackgroundWorker1.RunWorkerAsync()

            End If

        End If




    End Sub


    ''' <summary>
    ''' 寄出簡訊 2011/11/06 
    ''' </summary>

    Public Sub SendSMS()

       
        'add evt handler if application happend to close
        'AddHandler Application.ApplicationExit, AddressOf AppExit


        '開始時間-----------------------------
        workHistory._StartTime = DateTime.Now
        workHistory._PrjName = DateTime.Now & "寄發SMS"
        workHistory._BpType = SendTypeFactory.SendSms
        '-------------------------------------


        '儲存結果
        sb = New StringBuilder



        '計算寄出總數 not use
        Dim cnt As Integer


        '---------------取得SMS帳密-------------------
        Dim y As New User

        y = y.getUserSMS(User.LoginId, User.LoginPwd)

        Dim x As New KOT_SMSAPI
        x.username = y._SMSLoginId
        x.password = y._SMSLoginPwd

        If String.IsNullOrEmpty(x.username) Or String.IsNullOrEmpty(x.password) Then
            MsgBox("請設定簡訊帳號密碼")
            Exit Sub
        End If

        '---------------取得SMS帳密-------------------

        'WorkHistory-----------------------------
        '預定傳送筆數()
        workHistory._PlanTotalCnt = Me._CustMobileLst.count

        '---------------傳送資料到SQL Server-------------------------------------
        Dim smsWs As New EDM.SmsWs.SMS
        ReturnSerial = smsWs.InsertSMS("sp", _
        "總部", _
        workHistory._PrjName, _
        workHistory._StartTime, _
        workHistory._PlanTotalCnt)

        '------------------------------------------------------------------------
        ' Judge which instance in the collection , govermentsms or shopsms?
        Dim ret As Collection = Me._CustMobileLst

        Dim sms As MySMS = Nothing

        If ret.Count > 0 Then
            sms = MySMS.SmsFactory(ret.Item(1))
        End If



        'Try
        sms.Send(ret, Me.txt_Content.Text, sb, x, workHistory, cnt, Me.BackgroundWorker1)
        'Catch ex As Exception
        '不可以做釋放資源動作,否則會影響到接下來的寄發動作
        'sb = Nothing
        'x = Nothing
        'workHistory = Nothing
        'smsWs = Nothing
        'End Try


    End Sub


    ''' <summary>
    ''' 發送簡訊完成後
    ''' 為避免中斷,使用try catch 來判斷,並且寫入訊息,而不用msgbox打斷
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        '儲存結束時間
        workHistory._EndTime = DateTime.Now


        '儲存工作歷程---------------------------------------------------
        Try
            workHistory.Save()
            'workHistory = Nothing  ' fuck you
        Catch ex As Exception
            sb.Append("Error_Code:281_SMS_Error" & vbCrLf & ex.Message)

        End Try
        '儲存工作歷程---------------------------------------------------



        'Send data to Sql server---------------------------------------
        Try
            Dim smsWs As New EDM.SmsWs.SMS

            smsWs.UptSMS(Me.ReturnSerial, workHistory._EndTime, workHistory._SuccessCnt, workHistory._FailCnt)
            smsWs = Nothing
        Catch ex As Exception
            sb.Append("Error_Code:253_SMS_Error" & vbCrLf & ex.Message)

        End Try
        'Send data to Sql server---------------------------------------


        'test 
        'Dim test As String = sb.ToString

        '-------客製訊息-----------------------------------------------------------
        'ex:
        '0919xxxxxx  成功
        '0919xxxxxx  失敗
        '總筆數:
        '成功筆數:
        '失敗筆數:
        '---------------------------------------------------------------------------
        '預設 Message Form 的 message and hash field
        'My.Forms.Message.txt_Message.Text = sb.ToString & vbCrLf & "總筆數:" & _
        'workHistory._SuccessCnt + workHistory._FailCnt & vbCrLf & _
        '"成功筆數" & workHistory._SuccessCnt & _
        'vbCrLf & "失敗筆數:" & workHistory._FailCnt & _
        'vbCrLf
        '2011/11/10
        'My.Forms.Message.txt_Message.Text = "總筆數:" & _
        'workHistory._SuccessCnt + workHistory._FailCnt & vbCrLf & _
        '"成功筆數" & workHistory._SuccessCnt & _
        'vbCrLf & "失敗筆數:" & workHistory._FailCnt & _
        'vbCrLf
        '---------------------------------------------------------------------------
        '傳值給Message Form準備做估價單統計
        'My.Forms.Message._Hash = CustomMsg(sb.ToString)

        'My.Forms.Message._Message = sb.ToString
        'My.Forms.Message.txt_Message.Text = sb.ToString

        MsgBox("工作已完成")

        Me.progress.Value = 0


        My.Forms.Message.Show()


        'Me._CustMobileLst = Nothing

        'workHistory = Nothing



    End Sub

    ''' <summary>
    ''' 結束工作後產生的訊息格式
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CustomMsg(ByVal msg As String) As Hashtable

        '三民店/097056432/傳送成功，序號為：33865190
        '三民店/097056432/-5:B Number違反規則 接收端 門號錯誤

        Dim hash As New Hashtable

        Dim shopname As String = ""

        Dim key As String = ""

        Dim myRegex As New Regex("^\w{3}\/[0-9]*\/傳送成功", RegexOptions.Multiline)

        'Dim msg2 As String() = msg.Split(vbCrLf)

        Dim msg2 As String() = Split(msg, vbCrLf)

        For Each msg3 As String In msg2

            Try
                If Not String.IsNullOrEmpty(msg3) Then

                    '判斷有無符合自訂格式,以防中間會有Exception等錯誤訊息
                    '只計入成功次數
                    If myRegex.IsMatch(msg3) Then

                        msg3 = msg3.Trim

                        Dim index2 As Integer = msg3.IndexOf("/")

                        '找出自訂格式的分店名稱
                        If Not index2 = -1 Then
                            shopname = msg3.Substring(0, index2)
                        End If

                        'find sms id
                        Dim index As Integer = msg3.IndexOf("：")

                        If Not index = -1 Then

                            key = msg3.Substring(index + 1)

                        End If

                        If hash.ContainsKey(key) Then

                        Else
                            hash.Add(key, shopname)
                        End If

                    End If
                End If

            Catch ex As Exception

                MsgBox("Error_Code:420_SMS_Error" & vbCrLf & ex.Message)

            End Try
        Next

        '2011/11/7
        'Dim hash As New Hashtable

        'Dim myRegex As New Regex("^\w{3}\/[0-9]*\/傳送成功", RegexOptions.Multiline)

        'Dim msg2 As String() = msg.Split(vbCrLf)

        'For Each msg3 As String In msg2

        '    '判斷有無符合傳送成功的格式,以防中間會有Exception等錯誤訊息
        '    '只計入成功次數
        '    If myRegex.IsMatch(msg3) Then
        '        '防止第一列都會產生多餘空格
        '        msg3 = msg3.Trim

        '        Dim index As Integer = msg3.IndexOf("/")

        '        '找出訊息裡的分店名稱
        '        Dim shopname As String = msg3.Substring(0, index)

        '        '若已經有這分店稱存在,則取出值並加1,若無,則新增分店名稱的 key
        '        If hash.ContainsKey(shopname) Then
        '            Dim i As Integer = hash.Item(shopname)
        '            i = i + 1
        '            hash.Item(shopname) = i
        '        Else
        '            hash.Add(shopname, 1)

        '        End If

        '    End If
        'Next

        Return hash

    End Function


    ''' <summary>
    ''' when form closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SMS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Dim msg As MsgBoxResult = Nothing

        '啟動背景作業
        If Me.BackgroundWorker1.IsBusy Then
            msg = MsgBox("工作進行中,您確定要取消嗎?", MsgBoxStyle.YesNoCancel)

            If msg = MsgBoxResult.Yes Then

                Me.BackgroundWorker1.CancelAsync()

                Me.Close()

            End If

        End If

    End Sub


    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged


        ''背景作業進度回報程序()

        'txt_Message.Text = e.UserState                             '呈現傳送訊息   
        'Label9.Text = "已傳送 " & e.ProgressPercentage & "筆"          '呈現傳送筆數   

        '取得寄出總數
        'workHistory._TotalCnt = e.ProgressPercentage

        Me.progress.Increment(e.ProgressPercentage)

    End Sub



    '中止作業
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click


        If Me.BackgroundWorker1.CancellationPending Then
            Me.BackgroundWorker1.CancelAsync()
            Me.Close()
        Else

            Me.Close()

        End If


    End Sub


    '計算數字多寡
    Private Sub txt_Content_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Content.TextChanged


        Me.lbl_Strlength.Text = "字數" & txt_Content.Text.Length.ToString


    End Sub


    ''' <summary>
    ''' 顯示是否工作是否存入排程或者範本
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IsSaveToWorkArrangeOrSamp()

        'Dim btn As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddSamp

        'Dim btn1 As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddWork

        'AddHandler btn.Click, AddressOf AddSample

        'AddHandler btn1.Click, AddressOf AddWork

        'My.Forms.IsSaveToWorkOrSamp.Show()


    End Sub

    '新增範本
    Private Sub AddSample(ByVal sender As Object, ByVal e As EventArgs)

        Dim samp As New Saple
        samp._SampName = DateTime.Now.ToString
        samp._SampContent = Me.txt_Content.Text.Trim
        samp._SampCat = Saple.SmsSamp

        Try
            samp.AddSamp(samp)
        Catch ex As Exception
            MsgBox("Error_Code:460_SMS_Error" & vbCrLf & ex.Message)
            Exit Sub
        End Try

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub


    '新增排程
    Private Sub AddWork(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.AddWork.Show()

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub


    ''' <summary>
    '''選擇通訊錄
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ShowPrj.Click

        My.Forms.AddProject.TopMost = True
        My.Forms.AddProject.Show()
        My.Forms.AddProject.StartPosition = FormStartPosition.CenterScreen

        'My.Forms.AddProject.btn_ImportCust.Visible = False
        '簡訊按鈕
        'My.Forms.AddProject.ToolStripButton1.Visible = False

        'remove button named close
        My.Forms.AddProject.ToolStrip2.Items.RemoveAt(2)

        'add Button named 寄出簡訊-----------------------------------------------------
        Dim control As New ControlTool

        Dim btn As ToolStripButton = control.AddToolsBtn("寄出簡訊", ControlTool.ImgButton, "Images\Email.png")


        My.Forms.AddProject.ToolStrip2.Items.Add(btn)

        AddHandler btn.Click, AddressOf AddPrj_Click
        '-----------------------------------------------------

        'add Button named close ------------------------------
        Dim btn2 As ToolStripButton = control.AddToolsBtn("關閉", ControlTool.ImgButton, "Images\Delete.png")

        My.Forms.AddProject.ToolStrip2.Items.Add(btn2)

        AddHandler btn2.Click, AddressOf CloseAddPrject_Click
        '-----------------------------------------------------

    End Sub

    ''' <summary>
    ''' close AddProject Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseAddPrject_Click(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.AddProject.Close()

    End Sub

 
    ''' <summary>
    ''' 加入通訊錄到目前的寄發簡訊名單 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddPrj_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Reset-------------------------
        'Me._CustMobileLst = Nothing

        'Me._CustCnt = 0
        'Reset-------------------------



        '取得何種類型客戶
        Dim cust As OrginCust = CustFactory.getCustTpe(CustFactory.goverment)

        '根據客戶類型取得何種簡訊類型
        Dim sms As MySMS = MySMS.SmsFactory(cust)

        'Dim ret As Collection(Of GovermentCust) = cust.getCheckedMobileLst(My.Forms.AddProject.DataGridView1)

        'Dim sms As MySMS = MySMS.SmsFactory(cust)

        Dim ret As Collection = sms.getMobileLst(My.Forms.AddProject.DataGridView1)


        Dim receiver As String = sms.mobileLstToString


        '累加通訊清單,2012/2/6 改成不累加
        Me._SelectedPrjName = receiver

        Me._CustMobileLst = ret

        Me._CustCnt = ret.Count

        'RaiseEvent CustCnt_Chg(ret.Count)

        My.Forms.AddProject.Close()

    End Sub

  
    ''' <summary>
    ''' 按下選擇專案表單後的動作,設定Cust Mobile List
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub getCustLst(ByVal sender As Object, ByVal e As EventArgs)

        Dim cust As EDM.Holmes.Cust.Cust

        Dim ret As Collection(Of String) = Nothing

        If Not String.IsNullOrEmpty(Me.lbl_PrjId.Text) Then

            cust = New EDM.Holmes.Cust.Cust
            ret = cust.getMobilelst(Me.lbl_PrjId.Text)


            Me._CustMobileLst = ret


        End If

    End Sub

    ''' <summary>
    ''' 選擇範本
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ChooseSamp.Click

        'My.Forms.SampleMaintain.Show()
        My.Forms.SMSSamp.Show()

    End Sub


    ''' <summary>
    ''' 選擇所有客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AllCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AllCust.Click

        Dim ret As Collection(Of String) = Nothing


        Dim sb As New StringBuilder

        Dim cust As OrginCust = CustFactory.getCustTpe(CustFactory.goverment)

        Try
            ret = cust.getCustMobile

        Catch ex As Exception

            MsgBox("Error_Code:463_SMS_Error")
            Exit Sub

        End Try

        For Each mobile As String In ret

            sb.Append(IIf(mobile <> "", mobile & ",", ""))
        Next

        Me._CustCnt = ret.Count

        'RaiseEvent CustCnt_Chg(ret.Count)

        'Me.lbl_PrjName.Text = String.Format("({0}):{1}", ret.Count, sb.ToString)

        Me.lbl_PrjName.Text = sb.ToString

        'Me._SelectedPrjName = sb.ToString

    End Sub

    ''' <summary>
    ''' when contact list has changed
    ''' </summary>
    ''' <param name="prjName"></param>
    ''' <remarks></remarks>
    Private Sub SMS_PrjName_Chg(ByVal prjName As String) Handles Me.PrjName_Chg

        'Me.lbl_PrjName.Text = String.Format("({0}):{1}", custCnt, prjName)

        Me.lbl_PrjName.Text = prjName

    End Sub


    ''' <summary>
    ''' clear contact list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_ClearContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearContact.Click

        Me.lbl_PrjName.Text = ""


        'Me.custMobileLst.Clear()

        Me._SelectedPrjName = ""

        RaiseEvent CustCnt_Chg(0)

    End Sub


    Public Property _SelectedSampName()
        Get
            Return Me.selectedSampName
        End Get
        Set(ByVal value)
            Me.selectedSampName = value
        End Set
    End Property

    Public Property _SelectedSampId()
        Get
            Return Me.selectedSampId
        End Get
        Set(ByVal value)
            Me.selectedSampId = value
        End Set
    End Property

    Public Property _SelectedSampContent()
        Get
            Return Me.selectedSampContent
        End Get
        Set(ByVal value)
            Me.selectedSampContent = value
        End Set
    End Property

    Public Property _SelectedPrjName()
        Get
            Return Me.selectedPrjName
        End Get
        Set(ByVal value)
            Me.selectedPrjName = value
            RaiseEvent PrjName_Chg(Me.selectedPrjName)
        End Set
    End Property

    Public Property _CustMobileLst()
        Get
            Return Me.custMobileLst
        End Get
        Set(ByVal value)
            Me.custMobileLst = value
        End Set
    End Property

    Public Property _CustCnt()
        Get
            Return Me.CustCnt
        End Get
        Set(ByVal value)
            'Dim oldvalue As Integer = 0
            'oldvalue = Me.CustCnt
            Me.CustCnt = value
            RaiseEvent CustCnt_Chg(value)
        End Set
    End Property


  

    ''' <summary>
    ''' 加入客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        Try

            Me.AddPrj_Click(sender, e)

        Catch ex As Exception

            '避免發生無讀取到客戶檔案的錯誤
            MsgBox(ex.Message)

            Exit Sub

        End Try

    End Sub


    ''' <summary>
    ''' 查詢歷史記錄
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        My.Forms.Message.StartPosition = FormStartPosition.CenterScreen

        My.Forms.Message.ShowDialog()

    End Sub
End Class