
Imports EDM.Holmes.VO.Work


Public Class Step2


    Private every As String

    '每天。每週。每月
    Public Property _Every()
        Get
            Dim everyWhat As String = ""

            If Me.rad_EveryDay.Checked = True Then
                everyWhat = Work.EveryDay
            ElseIf Me.rad_EveryWeek.Checked = True Then
                everyWhat = Work.EveryWeek
            ElseIf Me.rad_EveryMonth.Checked = True Then
                everyWhat = Work.EveryMonth

            End If

            Return everyWhat

        End Get

        Set(ByVal value)

            Me.every = value

        End Set
    End Property



    Private Sub Step2_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated


        'MsgBox(My.Forms.ChooseMailBp._SelectedMailBpName)


        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------


    End Sub


    ''' <summary>
    ''' 上一步
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click



        My.Forms.Step1.MdiParent = My.Forms.AddWork

        My.Forms.Step1.WindowState = FormWindowState.Maximized

        '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
        My.Forms.Step1.Show()



    End Sub

    ''' <summary>
    ''' 根據點選型態選擇出現相對的Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If Me.rad_EveryDay.Checked = True Then
            If My.Forms.Step3_Day Is Nothing Then
                My.Forms.Step3_Day.MdiParent = My.Forms.AddWork
                My.Forms.Step3_Day.Show()
                My.Forms.Step3_Day.WindowState = FormWindowState.Maximized
            End If

            My.Forms.Step3_Day.MdiParent = My.Forms.AddWork
            My.Forms.Step3_Day.WindowState = FormWindowState.Maximized
            My.Forms.Step3_Day.Show()

        ElseIf Me.rad_EveryWeek.Checked = True Then

         
            If My.Forms.Step3_Week Is Nothing Then
                My.Forms.Step3_Week.MdiParent = My.Forms.AddWork
                My.Forms.Step3_Week.Show()
                My.Forms.Step3_Week.WindowState = FormWindowState.Maximized
            End If
            My.Forms.Step3_Week.MdiParent = My.Forms.AddWork
            My.Forms.Step3_Week.WindowState = FormWindowState.Maximized
            My.Forms.Step3_Week.Show()

        ElseIf Me.rad_EveryMonth.Checked = True Then

            If My.Forms.Step3_Month Is Nothing Then
                My.Forms.Step3_Month.MdiParent = My.Forms.AddWork
                My.Forms.Step3_Month.Show()
                My.Forms.Step3_Month.WindowState = FormWindowState.Maximized
            End If
            My.Forms.Step3_Month.MdiParent = My.Forms.AddWork
            My.Forms.Step3_Month.WindowState = FormWindowState.Maximized
            My.Forms.Step3_Month.Show()
        End If


    End Sub




End Class