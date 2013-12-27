Imports Microsoft.Win32.TaskScheduler


Public Class Step1


    Private workName As String

    Public Property _WorkName()
        Get
            Return Me.workName
        End Get
        Set(ByVal value)
            Me.workName = value
        End Set
    End Property


    Private Sub Step1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------


    End Sub

    ''' <summary>
    ''' next step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '2011/10/4
        '循環所有工作排程,防止有相同名稱之工作排程名稱輸入---------
        'Using ts As New TaskService
        '    Using tf As TaskFolder = ts.GetFolder("")
        '        For i As Integer = 0 To tf.Tasks.Count - 1
        '            If Me._WorkName = tf.Tasks(i).Name Then
        '                MsgBox("已有相同名稱的工作排程,請另選他名")
        '                Exit Sub
        '            End If
        '        Next
        '    End Using

        'End Using

        '-----------------------------------------------------------


        Me._WorkName = Me.txt_WorkName.Text.Trim

        If Me.txt_WorkName.Text.Trim = "" Then
            MsgBox("請輸入名稱")
            Exit Sub
        End If


        If My.Forms.Step2 Is Nothing Then
            My.Forms.Step2.MdiParent = My.Forms.AddWork
            My.Forms.Step2.Show()
            My.Forms.Step2.WindowState = FormWindowState.Maximized
        Else


            My.Forms.Step2.MdiParent = My.Forms.AddWork
            My.Forms.Step2.WindowState = FormWindowState.Maximized
            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.Step2.Show()

        End If


        '---------------------------------------------------------




    End Sub


    Private Sub txt_WorkName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_WorkName.TextChanged



    End Sub


    ''' <summary>
    ''' last step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click


        My.Forms.ChooseMailBp.MdiParent = My.Forms.AddWork

        My.Forms.ChooseMailBp.WindowState = FormWindowState.Maximized

        '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
        My.Forms.ChooseMailBp.Show()


    End Sub
End Class