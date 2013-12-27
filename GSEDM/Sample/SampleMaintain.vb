Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.VO.Sample
Imports EDM.Holmes.DB




Public Class SampleMaintain


    Private Sub SampleMaintain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        '預設出現常用設定視窗-------------------

        My.Forms.MailSamp.MdiParent = Me

        'My.Forms.MailSamp.Text = ""

        'My.Forms.MailSamp.ShowIcon = False

        'My.Forms.MailSamp.ControlBox = False


        My.Forms.MailSamp.Show()

        My.Forms.MailSamp.WindowState = FormWindowState.Maximized

        '---------------------------------------


    End Sub

    'Show mail sample
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click


        If My.Forms.MailSamp Is Nothing Then

            My.Forms.MailSamp.MdiParent = Me

            'My.Forms.MailSamp.Text = ""

            'My.Forms.MailSamp.ShowIcon = False

            'My.Forms.MailSamp.ControlBox = False

            My.Forms.MailSamp.Show()

            My.Forms.MailSamp.WindowState = FormWindowState.Maximized
        Else

            My.Forms.MailSamp.MdiParent = Me

            'My.Forms.MailSamp.Text = ""

            'My.Forms.MailSamp.ControlBox = False

            'My.Forms.MailSamp.ShowIcon = False

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show

            My.Forms.MailSamp.Show()

            My.Forms.MailSamp.WindowState = FormWindowState.Maximized

        End If


    End Sub

    'show mail sample 
    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        If My.Forms.SMSSamp Is Nothing Then

            My.Forms.SMSSamp.MdiParent = Me

            'My.Forms.SMSSamp.Text = ""

            'My.Forms.SMSSamp.ShowIcon = False

            'My.Forms.SMSSamp.ControlBox = False


            My.Forms.SMSSamp.Show()

            My.Forms.SMSSamp.WindowState = FormWindowState.Maximized

        Else

            My.Forms.SMSSamp.MdiParent = Me

            'My.Forms.SMSSamp.Text = ""

            'My.Forms.SMSSamp.ShowIcon = False

            'My.Forms.SMSSamp.ControlBox = False

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.SMSSamp.Show()

            My.Forms.SMSSamp.WindowState = FormWindowState.Maximized

        End If

    End Sub



  

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Close()

    End Sub

   
End Class
