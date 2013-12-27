Public Class Desc


    Private Sub Desc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If My.Forms.MailDesc Is Nothing Then

            My.Forms.MailDesc.MdiParent = Me

            My.Forms.MailDesc.Text = ""

            My.Forms.MailDesc.ShowIcon = False

            My.Forms.MailDesc.ControlBox = False

            My.Forms.MailDesc.WindowState = FormWindowState.Maximized

            My.Forms.MailDesc.Show()

        Else

            My.Forms.MailDesc.MdiParent = Me
            My.Forms.MailDesc.Text = ""

            My.Forms.MailDesc.ShowIcon = False

            My.Forms.MailDesc.ControlBox = False

            My.Forms.MailDesc.WindowState = FormWindowState.Maximized
            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.MailDesc.Show()

        End If
    End Sub

    'show mail desc window
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click


        If My.Forms.MailDesc Is Nothing Then

            My.Forms.MailDesc.MdiParent = Me

            My.Forms.MailDesc.Text = ""

            My.Forms.MailDesc.ShowIcon = False

            My.Forms.MailDesc.ControlBox = False

            My.Forms.MailDesc.WindowState = FormWindowState.Maximized

            My.Forms.MailDesc.Show()

        Else

            My.Forms.MailDesc.MdiParent = Me
            My.Forms.MailDesc.WindowState = FormWindowState.Maximized
            My.Forms.MailDesc.Text = ""

            My.Forms.MailDesc.ShowIcon = False

            My.Forms.MailDesc.ControlBox = False

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.MailDesc.Show()

        End If


    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

      

        If My.Forms.SMSDesc Is Nothing Then

            My.Forms.SMSDesc.MdiParent = Me

            My.Forms.SMSDesc.Text = ""

            My.Forms.SMSDesc.ShowIcon = False

            My.Forms.SMSDesc.ControlBox = False

            My.Forms.SMSDesc.WindowState = FormWindowState.Maximized

            My.Forms.SMSDesc.Show()

        Else

            My.Forms.SMSDesc.MdiParent = Me
            My.Forms.MailDesc.Text = ""

            My.Forms.MailDesc.ShowIcon = False

            My.Forms.MailDesc.ControlBox = False

            My.Forms.SMSDesc.WindowState = FormWindowState.Maximized
            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.SMSDesc.Show()

        End If

    End Sub

  
End Class