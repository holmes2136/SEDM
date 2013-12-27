Imports System.Windows.Forms
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports Microsoft.Win32.TaskScheduler

Public Class AddWorkArrange

    Private selectedProject As String
    Private selectedNoteId As String
    Private selectedSerial As String

    Private every As String
    Private specificTime As DateTime
    Private every1 As String
    Private contect As String
    Private everyWhat As String

    Public Property _SelectedSerial()
        Get
            Return Me.lbl_PrjSerial.Text
        End Get
        Set(ByVal value)
            Me.selectedSerial = value
        End Set
    End Property

    Public Property _SelectedProject()
        Get

            Return Me.lbl_PrjName.Text.Trim
        End Get
        Set(ByVal value)
            Me.selectedProject = value
        End Set
    End Property

    Public Property _SelectedNoteId()
        Get

            Return Me.lbl_PrjNoteId.Text
        End Get
        Set(ByVal value)
            Me.selectedNoteId = value
        End Set
    End Property

    Public Property _Every()
        Get
            Dim everyWhat As String = Me.combo_Every.SelectedItem.ToString

            Return everyWhat
        End Get
        Set(ByVal value)
            Me.every = value
        End Set
    End Property


    Public Property _SpecificTime()
        Get

            Dim now As DateTime = DateTime.Now
            Dim specificHour As Integer = Me.Num_SpecificHour.Value
            Dim specificMin As Integer = Me.Num_SpecificMin.Value
            Dim specificTime As DateTime = New DateTime(now.Year, now.Month, now.Day, specificHour, specificMin, 0)

            Return specificTime

        End Get
        Set(ByVal value)
            Me.specificTime = value
        End Set
    End Property


    Public Property _EveryWhat()
        Get
            Return Me.combo_Every.SelectedItem.ToString
        End Get
        Set(ByVal value)
            Me.everyWhat = value
        End Set
    End Property
    Public Property _Every1()
        Get

            Select Case Me.combo_Every.SelectedItem.ToString
                Case "每天"
                    Me.every1 = "每" & Me.Num_Every1.Value & "天"
                Case "每週"
                    Me.every1 = "每" & Me.Num_EveryWeek.Value & "週" & Me.CheckedListBox1.SelectedItem.ToString
                Case "每月"
                    Me.every1 = "每" & Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "號"

            End Select

            Return Me.every1

        End Get
        Set(ByVal value)
            Me.every = value
        End Set
    End Property


    Public Property _Contect()
        Get
            Return Me._SpecificTime.ToString & "_開始"
        End Get
        Set(ByVal value)
            Me.contect = value
        End Set
    End Property



    Private Sub AddWorkArrange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.lbl_PrjName.Text = Me._SelectedProject

        Me.lbl_PrjNoteId.Text = Me._SelectedNoteId


        '預設選項為每天
        Me.combo_Every.SelectedIndex = 0


     



    End Sub



    Public Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        'combo所選取的項目:每天or每周or每月
        Dim everyWhat As String = Me.combo_Every.SelectedItem.ToString

        '每隔幾天/幾周/幾月
        Dim every1 As String = ""

        '選取的專案ID
        Dim projectNoteId As String = Me.lbl_PrjNoteId.Text

        '選取的專案名稱
        Dim projectName As String = Me.lbl_PrjName.Text.Trim


        '--設定特定時間/////////////////////////////////////////////////

        Dim now As DateTime = DateTime.Now

        Dim specificHour As Integer = Me.Num_SpecificHour.Value

        Dim specificMin As Integer = Me.Num_SpecificMin.Value

        Dim specificTime As DateTime = New DateTime(now.Year, now.Month, now.Day, specificHour, specificMin, 0)


        '--/////////////////////////////////////////////////////

        '-----------------
        Dim dayOfWeek As String = ""

        Dim specificDay As Int32 = 0

        Dim whichMonth As String = ""
        '-----------------

        '----------------------------------------


        '判斷every不能為0

        '判斷有無選擇專案
        If String.IsNullOrEmpty(projectNoteId) Or projectNoteId Is Nothing Then

            MsgBox("請選擇專案")

            Exit Sub

        End If



        '新增至資料庫,若新增至資料庫失敗,則不進行之後的新增工作排程---


        Dim conn As OleDbConnection = HolmesConn.getConnection
        Try
            Select Case everyWhat
                Case "每天"
                    every1 = "每" & Me.Num_Every1.Value & "天"
                Case "每週"
                    every1 = "每" & Me.Num_EveryWeek.Value & "週" & Me.CheckedListBox1.SelectedItem.ToString
                Case "每月"
                    every1 = "每" & Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "號"

            End Select

            AddTaskToDB(projectNoteId, projectName, everyWhat, specificTime.ToString("tt h:mm"), every1, specificTime.ToString & "_開始")

        Catch ex As Exception

            MsgBox("新增工作發生錯誤")
            Exit Sub

        End Try

        '--------------------------------------------------------------


        Dim projectName2 As String = getMaxSerial()

        Try
            '新增windows工作排程
            Select Case everyWhat
                Case "每天"
                    every1 = Me.Num_Every1.Value
                    AddTask(projectName2, specificTime, every1)
                Case "每週"
                    every1 = Me.Num_EveryWeek.Value
                    dayOfWeek = Me.CheckedListBox1.SelectedItem.ToString
                    AddTask(projectName2, dayOfWeek, specificTime, every1)
                Case "每月"
                    whichMonth = Me.CheckedListBox2.SelectedItem.ToString
                    specificDay = Me.Num_SpecificDay.Value
                    AddTask(projectName2, specificTime, specificDay, whichMonth)
            End Select
        Catch ex As Exception

            MsgBox("新增工作排程錯誤")
            Exit Sub

        End Try


        '更新工作排程視窗 
        'My.Forms.WorkArrange.RefreshData()

        Me.Close()






    End Sub

    ''' <summary>
    ''' 取得最新的Serial以為工作排程命名
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMaxSerial() As String

        Dim returnVal As Integer

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        Dim sql As New System.Text.StringBuilder("select Max(Serial) from 排程表")

        cmd.CommandText = sql.ToString


        cmd.Connection = conn

        conn.Open()

        Using conn
            returnVal = cmd.ExecuteScalar

        End Using

        Return CStr(returnVal)

    End Function

    ''' <summary>
    ''' 新增工作排程
    ''' </summary>
    ''' <param name="every">每天or每周or每月</param>
    ''' <param name="specificTime">開始時間,需為DateTime型態</param>
    ''' <param name="every1">每隔幾天/幾周/幾月</param>
    ''' <remarks></remarks>
    Private Sub AddTaskToDB(ByVal projectNoteId As String, ByVal projectName As String, ByVal every As String, ByVal specificTime As String, ByVal every1 As String, ByVal contect As String)

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        Dim sql As New System.Text.StringBuilder("Insert into 排程表(PrjName,NoteId,Every,SpecificTime,Every1,Contect)")
        sql.Append("Values(?,?,?,?,?,?)")

        cmd.CommandText = sql.ToString

        'cmd.CommandText = "INSERT INTO 排程表(NoteId,Every,SpecificTime,Every1)Values(?,?,?,?)"
        cmd.Parameters.Add("@projectNoteId", OleDbType.VarChar).Value = projectName
        cmd.Parameters.Add("@projectNoteId", OleDbType.Integer).Value = CInt(projectNoteId)
        cmd.Parameters.Add("@every", OleDbType.VarChar).Value = every
        cmd.Parameters.Add("@specificTime", OleDbType.VarChar).Value = specificTime
        cmd.Parameters.Add("@every1", OleDbType.VarChar).Value = every1
        cmd.Parameters.Add("@contect", OleDbType.VarChar).Value = contect

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using


    End Sub


    ''' <summary>
    ''' 新增天的工作排程
    ''' </summary>
    ''' <param name="projectNoteId">專案ID</param>
    ''' <param name="specificTime">開始時間</param>
    ''' <param name="every1">每隔幾天/幾周/幾月</param>
    ''' <remarks></remarks>

    Private Overloads Sub AddTask(ByVal projectNoteId As String, ByVal specificTime As DateTime, ByVal every1 As String)

        Dim ts As New TaskService

        'Create a new task definition and assign properties
        Dim td As TaskDefinition = ts.NewTask

        'td.RegistrationInfo.Description = projectName

        ' Create a trigger that will fire the task at this time every other day

        '隔天執行



        Dim daily As New DailyTrigger(every1)

        daily.StartBoundary = specificTime

        td.Triggers.Add(daily)


        'daily.StartBoundary = New DateTime(2011, 8, 17, 12, 5, 0)

        'td.Triggers.Add(New DailyTrigger(every1))


        'Dim weekly As New WeeklyTrigger(every1, every)
        'weekly.StartBoundary = specificTime
        'td.Triggers.Add(weekly)

        'td.Triggers.Add(New MonthlyTrigger(every1))


        'td.Triggers.Add(New DailyTrigger(1))

        '距離現在時間30秒後執行
        'td.Triggers.Add(New TimeTrigger(DateTime.Now + TimeSpan.FromSeconds(30)))

        '在2011/8/14 晚上9點24分0秒發生
        'Dim test As New System.DateTime(2011, 8, 14, 21, 24, 0)
        'td.Triggers.Add(New TimeTrigger(test))

        td.RegistrationInfo.Description = "上光EDM排程"
        ' Create an action that will launch Notepad whenever the trigger fires
        td.Actions.Add(New ExecAction("C:\Documents and Settings\Administrator.HOLMES-FZCUUO8V\My Documents\Visual Studio 2005\Projects\EDMforExe\EDMforExe\bin\Debug\EDMforExe.exe", Nothing, Nothing))

        ' Register the task in the root folder
        ts.RootFolder.RegisterTaskDefinition(projectNoteId, td)


        ts = Nothing

        td = Nothing

    End Sub

    ''' <summary>
    ''' 新增週的工作排程
    ''' </summary>
    ''' <param name="projectNoteId"></param>
    ''' <param name="dayOfWeek"></param>
    ''' <param name="specificTime"></param>
    ''' <param name="every1"></param>
    ''' <remarks></remarks>
    Private Overloads Sub AddTask(ByVal projectNoteId As String, ByVal dayOfWeek As String, ByVal specificTime As DateTime, ByVal every1 As String)

        Dim dayOfWeek2 As Integer = 0

        Select Case dayOfWeek
            Case "星期一"
                dayOfWeek2 = 2
            Case "星期二"
                dayOfWeek2 = 4
            Case "星期三"
                dayOfWeek2 = 8
            Case "星期四"
                dayOfWeek2 = 16
            Case "星期五"
                dayOfWeek2 = 32
            Case "星期六"
                dayOfWeek2 = 64
            Case "星期日"
                dayOfWeek2 = 1
        End Select

        Dim ts As New TaskService

        Dim td As TaskDefinition = ts.NewTask

        Dim weekly As New WeeklyTrigger(dayOfWeek2, every1)

        weekly.StartBoundary = specificTime

        td.RegistrationInfo.Description = "上光EDM排程"

        td.Triggers.Add(weekly)

        td.Actions.Add(New ExecAction("C:\Documents and Settings\Administrator.HOLMES-FZCUUO8V\My Documents\Visual Studio 2005\Projects\EDMforExe\EDMforExe\bin\Debug\EDMforExe.exe", Nothing, Nothing))

        ts.RootFolder.RegisterTaskDefinition(projectNoteId, td)


        ts = Nothing

        td = Nothing

    End Sub

    ''' <summary>
    ''' 新增月的工作排程
    ''' </summary>
    ''' <param name="projectNoteId"></param>
    ''' <param name="specificTime"></param>
    ''' <param name="specifiDay"></param>
    ''' <param name="whichMonth"></param>
    ''' <remarks></remarks>
    Private Overloads Sub AddTask(ByVal projectNoteId As String, ByVal specificTime As DateTime, ByVal specifiDay As Integer, ByVal whichMonth As String)

        Select Case whichMonth
            Case "每月"
                whichMonth = 4095
            Case "一月"
                whichMonth = 1
            Case "二月"
                whichMonth = 2
            Case "三月"
                whichMonth = 4
            Case "四月"
                whichMonth = 8
            Case "五月"
                whichMonth = 16
            Case "六月"
                whichMonth = 32
            Case "七月"
                whichMonth = 64
            Case "八月"
                whichMonth = 128
            Case "九月"
                whichMonth = 256
            Case "十月"
                whichMonth = 512
            Case "十一月"
                whichMonth = 1024
            Case "十二月"
                whichMonth = 2048
        End Select


        Dim ts As New TaskService

        Dim td As TaskDefinition = ts.NewTask

        Dim monthly As New MonthlyTrigger(specifiDay, whichMonth)

        td.RegistrationInfo.Description = "上光EDM排程"

        monthly.StartBoundary = specificTime

        td.Triggers.Add(monthly)

        td.Actions.Add(New ExecAction("C:\Documents and Settings\Administrator.HOLMES-FZCUUO8V\My Documents\Visual Studio 2005\Projects\EDMforExe\EDMforExe\bin\Debug\EDMforExe.exe", Nothing, Nothing))

        ts.RootFolder.RegisterTaskDefinition(projectNoteId, td)

        ts = Nothing

        td = Nothing



    End Sub

    ''' <summary>
    ''' 更新工作排程
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UptTask()


        Me.UpTaskToDB()

        Me.UpTaskToWindows()

    End Sub

    ''' <summary>
    ''' 更新資料庫裡的排程資料
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpTaskToDB()

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        Dim sql As New System.Text.StringBuilder("UPDATE 排程表 SET PrjName = ?,Every=?,SpecificTime=?,Every1=?,Contect=? Where Serial = ?")

        cmd.CommandText = sql.ToString

        cmd.Parameters.Add("@PrjName", OleDbType.VarChar).Value = Me._SelectedProject
        cmd.Parameters.Add("@every", OleDbType.VarChar).Value = Me._Every
        cmd.Parameters.Add("@specificTime", OleDbType.VarChar).Value = Me._SpecificTime
        cmd.Parameters.Add("@every1", OleDbType.VarChar).Value = Me._Every1
        cmd.Parameters.Add("@contect", OleDbType.VarChar).Value = Me._Contect
        cmd.Parameters.Add("@NoteId", OleDbType.VarChar).Value = Me._SelectedNoteId

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using

    End Sub

    ''' <summary>
    ''' 更新Windows排程
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpTaskToWindows()

      

        'Try
        '新增windows工作排程

        Select Case Me._EveryWhat

            Case "每天"
                UptTaskByEveryDay(Me._SelectedNoteId, Me._SpecificTime, 1)
            Case "每週"
                Dim every1 As String = Me.Num_EveryWeek.Value
                Dim DayOfWeek As String = Me.CheckedListBox1.SelectedItem.ToString
                UptTaskByEveryWeek(Me._SelectedNoteId, DayOfWeek, Me._SpecificTime, every1)
            Case "每月"
                Dim whichMonth As String = Me.CheckedListBox2.SelectedItem.ToString
                Dim specificDay As String = Me.Num_SpecificDay.Value
                UptTaskByEveryMonth(Me._SelectedNoteId, Me._SpecificTime, specificDay, whichMonth)

        End Select
        'Catch ex As Exception

        'MsgBox("新增工作排程錯誤")
        'Exit Sub

        'End Try


    End Sub

    ''' <summary>
    ''' 更新每天工作排程
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="specificTime"></param>
    ''' <param name="every1"></param>
    ''' <remarks></remarks>
    Private Sub UptTaskByEveryDay(ByVal projectName As String, ByVal specificTime As DateTime, ByVal every1 As String)

        Dim ts As New TaskService

        Dim noteId As String = Me._SelectedNoteId

        Dim td As TaskDefinition = ts.GetTask(noteId).Definition

        'td.RegistrationInfo.Description = "tt"

        Dim daily As New DailyTrigger(every1)

        daily.StartBoundary = specificTime

        td.Triggers.Add(daily)

        td.Triggers.RemoveAt(0)

        ts.RootFolder.RegisterTaskDefinition(Me.selectedNoteId, td)

        ts = Nothing

        td = Nothing

    End Sub

    ''' <summary>
    ''' 更新每週工作排程
    ''' </summary>
    ''' <param name="projectNoteId"></param>
    ''' <param name="DayOfWeek"></param>
    ''' <param name="specificTime"></param>
    ''' <remarks></remarks>
    Private Sub UptTaskByEveryWeek(ByVal projectNoteId As String, ByVal DayOfWeek As String, ByVal specificTime As String, ByVal every1 As String)

        Dim dayOfWeek2 As Integer = 0

        Select Case DayOfWeek
            Case "星期一"
                dayOfWeek2 = 2
            Case "星期二"
                dayOfWeek2 = 4
            Case "星期三"
                dayOfWeek2 = 8
            Case "星期四"
                dayOfWeek2 = 16
            Case "星期五"
                dayOfWeek2 = 32
            Case "星期六"
                dayOfWeek2 = 64
            Case "星期日"
                dayOfWeek2 = 1
        End Select

        Dim ts As New TaskService

        Dim noteId As String = Me._SelectedNoteId

        Dim td As TaskDefinition = ts.GetTask(noteId).Definition


        Dim weekly As New WeeklyTrigger(dayOfWeek2, every1)

        weekly.StartBoundary = specificTime

        td.Triggers.Add(weekly)

        td.Triggers.RemoveAt(0)

        ts.RootFolder.RegisterTaskDefinition(projectNoteId, td)


        ts = Nothing

        td = Nothing

    End Sub


    ''' <summary>
    ''' 更新每月工作排程
    ''' </summary>
    ''' <param name="projectNoteId"></param>
    ''' <param name="specificTime"></param>
    ''' <param name="specificDay"></param>
    ''' <param name="whichMonth"></param>
    ''' <remarks></remarks>
    Private Sub UptTaskByEveryMonth(ByVal projectNoteId As String, ByVal specificTime As String, ByVal specificDay As String, ByVal whichMonth As String)


        Select Case whichMonth
            Case "每月"
                whichMonth = 4095
            Case "一月"
                whichMonth = 1
            Case "二月"
                whichMonth = 2
            Case "三月"
                whichMonth = 4
            Case "四月"
                whichMonth = 8
            Case "五月"
                whichMonth = 16
            Case "六月"
                whichMonth = 32
            Case "七月"
                whichMonth = 64
            Case "八月"
                whichMonth = 128
            Case "九月"
                whichMonth = 256
            Case "十月"
                whichMonth = 512
            Case "十一月"
                whichMonth = 1024
            Case "十二月"
                whichMonth = 2048
        End Select

        Dim ts As New TaskService

        Dim td As TaskDefinition = ts.GetTask(projectNoteId).Definition

        Dim monthly As New MonthlyTrigger(specificDay, whichMonth)

        monthly.StartBoundary = specificTime

        td.Triggers.Add(monthly)

        td.Triggers.RemoveAt(0)

        td.Actions.Add(New ExecAction("C:\Documents and Settings\Administrator.HOLMES-FZCUUO8V\My Documents\Visual Studio 2005\Projects\EDMforExe\EDMforExe\bin\Debug\EDMforExe.exe", Nothing, Nothing))

        ts.RootFolder.RegisterTaskDefinition(projectNoteId, td)

        ts = Nothing

        td = Nothing

    End Sub


    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 顯示選取專案視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btn_ShowChhoseTimePrj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ShowChhoseTimePrj.Click


        'ChooseTimePrj.Show()


    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_Every.SelectedIndexChanged


        'Me._Every = Me.combo_Every.SelectedItem.ToString


    End Sub

    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_Every.SelectedValueChanged

    End Sub

    ''' <summary>
    ''' 切換相對應的"每"視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_Every.TextChanged

        Dim combo As ComboBox = CType(sender, ComboBox)

        'Me.lbl_PrjName.Text = combo.SelectedItem.ToString

        Select Case combo.SelectedItem
            Case "每天"
                Me.TabControl1.SelectedIndex = 0
                Me.TabControl1.TabPages(0).Enabled = True
            Case "每週"
                Me.TabControl1.SelectedIndex = 1
                Me.TabControl1.TabPages(1).Enabled = True
            Case "每月"
                Me.TabControl1.SelectedIndex = 2
                Me.TabControl1.TabPages(2).Enabled = True
        End Select

    End Sub


    ''' <summary>
    ''' 若是使用選擇標籤,則標簽內容則被封鎖,讓使用者只能使用combobox移動內容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        Me.TabControl1.TabPages(0).Enabled = False
        Me.TabControl1.TabPages(1).Enabled = False
        Me.TabControl1.TabPages(2).Enabled = False


    End Sub


    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub TabControl1_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting


        'If (e.TabPageIndex = 1) Then

        '    e.Cancel = True

        'ElseIf (e.TabPageIndex = 2) Then

        '    e.Cancel = True

        'ElseIf (e.TabPageIndex = 3) Then

        'End If


    End Sub


   

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click






    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

     

  

    End Sub
End Class
