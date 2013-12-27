

Imports EDM.Holmes.VO.Work
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports EDM.Holmes.VO.Work.Task
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Mail

Public Class Step4

    Private Sub Step4_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


        'MsgBox(My.Forms.Step2._Every)

        '----------------------------------
        '若此表單出現,表示btn step1,step2都是灰色,step3是藍色
        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step3.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step4.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step5.BackColor = Color.Blue

        '----------------------------------


        '設定描述文字---------------------------------------------------------
        Dim message As String = ""

        Dim selectedPrj As String = My.Forms.ChooseMailBp._SelectedMailBpName

        Select Case My.Forms.Step2._Every
            Case Work.EveryDay
                message = "已建立" & selectedPrj & "排程," & My.Forms.Step3_Day._FriendlyEvery1 & "執行,從" & My.Forms.Step3_Day._Specefictime & "開始"
            Case Work.EveryWeek
                message = "已建立" & selectedPrj & "排程," & My.Forms.Step3_Week._FriendlyEvery1 & "執行,從" & My.Forms.Step3_Week._Specefictime & "開始"
            Case Work.EveryMonth
                message = "已建立" & selectedPrj & "排程," & My.Forms.Step3_Month._FriendlyEvery1 & "執行,從" & My.Forms.Step3_Month._Specefictime & "開始"

        End Select

        Me.lbl_Message.Text = message

        '---------------------------------------------------------------------

    End Sub


    ''' <summary>
    ''' 完成按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        '選取的專案ID
        '2011/10/14
        'Dim projectNoteId As String = My.Forms.ChooseMailBp._SelectedMailBpSerial
        Dim projectNoteId As String = ""

        '選取的專案名稱
        '2011/10/14
        'Dim projectName As String = My.Forms.ChooseMailBp._SelectedMailBpName
        Dim projectname As String = My.Forms.Step1._WorkName

        '寄件備份類型
        '2011/10/14
        'Dim bpType As String = My.Forms.ChooseMailBp._Bptype

        Dim bpType As String = My.Forms.SmsOrMail._BpType

        Dim everyWhat As String = My.Forms.Step2._Every

        Dim every1 As String = ""

        Dim specificTime As DateTime = Nothing

        '------------------------------------------------------------------------
        '1.detect bptype is Sms or Mail
        '2.return prjNoteid to next step use
        Try
            Select Case My.Forms.SmsOrMail._BpType
                Case MailBp.mailBp

                    Dim mailSender As String = My.Forms.SendMail.txt_Sender.Text.Trim
                    Dim Receiver As String = My.Forms.SendMail.txt_Receiver.Text.Trim
                    Dim content As String = My.Forms.SendMail.txt_Content.Text.Trim
                    Dim attachment As String = My.Forms.SendMail.txt_Attach.Text.Trim
                    Dim mailTitle As String = My.Forms.SendMail.txt_Title.Text.Trim

                    Dim bp As New MailBp(mailSender, Receiver, mailTitle, content, attachment, "")


                    projectNoteId = bp.Save(MailBp.mailBp)

                Case MailBp.smsBp


                    Dim Receiver As String = My.Forms.SMS.lbl_PrjName.Text
                    Dim content As String = My.Forms.SMS.txt_Content.Text.Trim

                    Dim bp As New MailBp(Receiver, content, "")

                    projectNoteId = bp.Save(MailBp.smsBp)

            End Select

        Catch ex As Exception
            MsgBox("Error_Code:114_Step4_Error" & vbCrLf & ex.Message)
            Exit Sub

        End Try

        '------------------------------------------------------------------------

        '------------------------------------------------------------
        'param projectnoteId:the serial which last step add new 寄件範本
        Dim conn As OleDbConnection = HolmesConn.getConnection

        Try
            Select Case My.Forms.Step2._Every
                Case Work.EveryDay
                    every1 = My.Forms.Step3_Day._FriendlyEvery1
                    specificTime = My.Forms.Step3_Day._Specefictime
                Case Work.EveryWeek
                    every1 = My.Forms.Step3_Week._FriendlyEvery1
                    specificTime = My.Forms.Step3_Week._Specefictime
                Case Work.EveryMonth
                    every1 = My.Forms.Step3_Month._FriendlyEvery1
                    specificTime = My.Forms.Step3_Month._Specefictime

            End Select

            '新增到DB
            AddTaskToDB(projectNoteId, projectName, everyWhat, specificTime.ToString("tt h:mm"), every1, specificTime.ToString & "_開始", User.LoginId, User.LoginPwd, bpType)

        Catch ex As Exception

            MsgBox("Error_Code:144_Step4_Error" & vbCrLf & ex.Message)
            Exit Sub

        End Try

        



        '------------------------------------------------------------

        Dim myWork As New Work

        '取得最新的Serial以為工作排程命名
        'Dim projectName2 As String = myWork.getMaxSerial()
        Dim prjNoteId As String = myWork.getMaxSerial()

        Dim projectName2 As String = My.Forms.Step1._WorkName


        '新增到windows工作排程-----------------------------------------------------------
        Dim myTask As New Task


        Select Case My.Forms.Step2._Every
            Case Work.EveryDay
                myTask.AddDayTask(prjNoteId, projectName2, My.Forms.Step3_Day._Specefictime, My.Forms.Step3_Day._Every1, bpType)

            Case Work.EveryWeek
                myTask.AddWeekTask(prjNoteId, projectName2, My.Forms.Step3_Week._DayOfWeek, My.Forms.Step3_Week._Specefictime, My.Forms.Step3_Week._Every1, bpType)

            Case Work.EveryMonth
                myTask.AddMonthTask(prjNoteId, projectName2, My.Forms.Step3_Month._Specefictime, Step3_Month._SpeceficDay, My.Forms.Step3_Month._WhichMonth, bpType)

        End Select



        '--------------------------------------------------------------

        '更新工作排程視窗 
        My.Forms.WorkArrange.RefreshData()

        MsgBox("新增成功")

        My.Forms.AddWork.Close()


    End Sub


    ''' <summary>
    ''' 上一步
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '2011/10/14
        'Dim selectedWorkType As String = ""

        'If Not My.Forms.Step2 Is Nothing Then
        '    If My.Forms.Step2.rad_EveryDay.Checked = True Then
        '        selectedWorkType = Work.EveryDay
        '    ElseIf My.Forms.Step2.rad_EveryWeek.Checked = True Then
        '        selectedWorkType = Work.EveryWeek
        '    ElseIf My.Forms.Step2.rad_EveryMonth.Checked = True Then
        '        selectedWorkType = Work.EveryMonth
        '    End If

        'End If

        'Select Case selectedWorkType
        '    Case Work.EveryDay
        '        My.Forms.Step3_Day.WindowState = FormWindowState.Maximized
        '        My.Forms.Step3_Day.Show()
        '    Case Work.EveryWeek
        '        My.Forms.Step3_Week.WindowState = FormWindowState.Maximized
        '        My.Forms.Step3_Week.Show()
        '    Case Work.EveryMonth
        '        My.Forms.Step3_Month.WindowState = FormWindowState.Maximized
        '        My.Forms.Step3_Month.Show()
        'End Select

        Dim selectedWorkType As String = ""

        If Not My.Forms.SmsOrMail Is Nothing Then
            If My.Forms.SmsOrMail.rad_Mail.Checked = True Then
                selectedWorkType = "mail"
            ElseIf My.Forms.SmsOrMail.rad_Sms.Checked = True Then
                selectedWorkType = "sms"
            End If

        End If

        Select Case selectedWorkType
            Case "mail"
                My.Forms.SendMail.WindowState = FormWindowState.Maximized
                My.Forms.SendMail.Show()
            Case "sms"
                My.Forms.SMS.WindowState = FormWindowState.Maximized
                My.Forms.SMS.Show()
        End Select

    End Sub


    ''' <summary>
    ''' 新增工作排程
    ''' </summary>
    ''' <param name="every">每天or每周or每月</param>
    ''' <param name="specificTime">開始時間,需為DateTime型態</param>
    ''' <param name="every1">每隔幾天/幾周/幾月</param>
    ''' <remarks></remarks>
    Private Sub AddTaskToDB(ByVal projectNoteId As String, ByVal projectName As String, ByVal every As String, ByVal specificTime As String, ByVal every1 As String, ByVal contect As String, ByVal LoginId As String, ByVal LoginPwd As String, ByVal BpType As String)

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        Dim sql As New System.Text.StringBuilder("Insert into 排程表(PrjName,NoteId,Every,SpecificTime,Every1,Contect,LoginId,LoginPwd,BpType)")
        sql.Append("Values(?,?,?,?,?,?,?,?,?)")

        cmd.CommandText = sql.ToString

        'cmd.CommandText = "INSERT INTO 排程表(NoteId,Every,SpecificTime,Every1)Values(?,?,?,?)"
        cmd.Parameters.Add("@projectNoteId", OleDbType.VarChar).Value = projectName
        cmd.Parameters.Add("@projectNoteId", OleDbType.Integer).Value = CInt(projectNoteId)
        cmd.Parameters.Add("@every", OleDbType.VarChar).Value = every
        cmd.Parameters.Add("@specificTime", OleDbType.VarChar).Value = specificTime
        cmd.Parameters.Add("@every1", OleDbType.VarChar).Value = every1
        cmd.Parameters.Add("@contect", OleDbType.VarChar).Value = contect
        cmd.Parameters.Add("@LoginId", OleDbType.VarChar).Value = LoginId
        cmd.Parameters.Add("@LoginPwd", OleDbType.VarChar).Value = LoginPwd
        cmd.Parameters.Add("@BpType", OleDbType.VarChar).Value = BpType

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using


    End Sub



End Class