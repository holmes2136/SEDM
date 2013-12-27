
Imports EDM.Holmes.VO.Work
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports EDM.Holmes.VO.Work.Task
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Mail


Public Class SmsOrMail

    Private bpType As String

    Public Property _BpType()
        Get
            Return Me.bpType
        End Get
        Set(ByVal value)
            Me.bpType = value
        End Set
    End Property

    Private Sub SmsOrMail_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step3.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step4.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------

    End Sub



    ''' <summary>
    ''' Last Step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim selectedWorkType As String = ""

        If Not My.Forms.Step2 Is Nothing Then
            If My.Forms.Step2.rad_EveryDay.Checked = True Then
                selectedWorkType = Work.EveryDay
            ElseIf My.Forms.Step2.rad_EveryWeek.Checked = True Then
                selectedWorkType = Work.EveryWeek
            ElseIf My.Forms.Step2.rad_EveryMonth.Checked = True Then
                selectedWorkType = Work.EveryMonth
            End If

        End If

        Select Case selectedWorkType
            Case Work.EveryDay
                My.Forms.Step3_Day.WindowState = FormWindowState.Maximized
                My.Forms.Step3_Day.Show()
            Case Work.EveryWeek
                My.Forms.Step3_Week.WindowState = FormWindowState.Maximized
                My.Forms.Step3_Week.Show()
            Case Work.EveryMonth
                My.Forms.Step3_Month.WindowState = FormWindowState.Maximized
                My.Forms.Step3_Month.Show()
        End Select

    End Sub


    ''' <summary>
    ''' Next Step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click


        If Me.rad_Mail.Checked = True Then


            SendMail_do()

            If My.Forms.SendMail Is Nothing Then
                My.Forms.SendMail.MdiParent = My.Forms.AddWork
                My.Forms.SendMail.Show()
                My.Forms.SendMail.WindowState = FormWindowState.Maximized
            End If



            My.Forms.SendMail.MdiParent = My.Forms.AddWork
            My.Forms.SendMail.WindowState = FormWindowState.Maximized
            My.Forms.SendMail.Show()

        ElseIf Me.rad_Sms.Checked = True Then

            SMS_do()

            If My.Forms.SMS Is Nothing Then

                My.Forms.SMS.MdiParent = My.Forms.AddWork
                My.Forms.SMS.Show()
                My.Forms.SMS.WindowState = FormWindowState.Maximized
            End If

            My.Forms.SMS.MdiParent = My.Forms.AddWork
            My.Forms.SMS.WindowState = FormWindowState.Maximized
            My.Forms.SMS.Show()

        End If


    End Sub


    Private Sub SendMail_do()

        'change addWork form btn color
        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step3.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step4.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------

        My.Forms.SendMail.btn_Send.Visible = False
        My.Forms.SendMail.btn_Close.visible = False

        'prevent to readd control
        My.Forms.SendMail.Panel2.Controls.Clear()

        'add last step button
        Dim btn As New Button
        btn.Name = "btn_LastStep"
        btn.Text = "上一步"
        btn.Dock = DockStyle.Right
        Dim point As New Point(386, 0)
        btn.Location = point

        AddHandler btn.Click, AddressOf BackSmsOrMail

        'add next step button
        Dim btn2 As New Button
        btn.Name = "btn_NextStep"
        btn2.Text = "下一步"
        btn2.Dock = DockStyle.Right

        My.Forms.SendMail.Panel2.Controls.Add(btn2)
        My.Forms.SendMail.Panel2.Controls.Add(btn)

        AddHandler btn2.Click, AddressOf ForwardStep4


    End Sub

    ''' <summary>
    ''' back to SmsOrMail form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackSmsOrMail(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.SendMail.Panel2.Controls.Clear()

        Me.MdiParent = My.Forms.AddWork

        Me.WindowState = FormWindowState.Maximized

        '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
        Me.Show()

    End Sub

    ''' <summary>
    ''' Forward Step4 Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ForwardStep4(ByVal sender As Object, ByVal e As EventArgs)


        If My.Forms.Step4 Is Nothing Then
            My.Forms.Step4.MdiParent = My.Forms.AddWork
            My.Forms.Step4.Show()
            My.Forms.Step4.WindowState = FormWindowState.Maximized
        Else


            My.Forms.Step4.MdiParent = My.Forms.AddWork
            My.Forms.Step4.WindowState = FormWindowState.Maximized
            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.Step4.Show()

        End If

    End Sub

    Private Sub SMS_do()

        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step3.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step4.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------

        My.Forms.SMS.btn_Send.Visible = False
        My.Forms.SMS.btn_Cancel.Visible = False

        'prevent to readd control
        My.Forms.SMS.Panel2.Controls.Clear()

        'add last step button
        Dim btn As New Button
        btn.Name = "btn_LastStep"
        btn.Text = "上一步"
        btn.Dock = DockStyle.Right
        Dim point As New Point(386, 0)
        btn.Location = point

        AddHandler btn.Click, AddressOf BackSmsOrMail

        'add next step button
        Dim btn2 As New Button
        btn.Name = "btn_NextStep"
        btn2.Text = "下一步"
        btn2.Dock = DockStyle.Right

        My.Forms.SMS.Panel2.Controls.Add(btn2)
        My.Forms.SMS.Panel2.Controls.Add(btn)

        AddHandler btn2.Click, AddressOf ForwardStep4


    End Sub


    

    Private Sub rad_Mail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_Mail.CheckedChanged


        If Me.rad_Mail.Checked = True Then
            Me._BpType = MailBp.mailBp
        End If


    End Sub


    Private Sub rad_Sms_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_Sms.CheckedChanged

        If Me.rad_Sms.Checked = True Then
            Me._BpType = MailBp.smsBp
        End If

    End Sub

End Class