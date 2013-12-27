Imports System.Windows.Forms

Public Class Seting


    Private Sub Seting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '預設出現常用設定視窗-------------------
        If My.Forms.UsuallySetting Is Nothing Then

            My.Forms.UsuallySetting.MdiParent = Me

            'My.Forms.UsuallySetting.Text = ""

            'My.Forms.UsuallySetting.ShowIcon = False

            'My.Forms.UsuallySetting.ControlBox = False

            My.Forms.UsuallySetting.Show()

            My.Forms.UsuallySetting.WindowState = FormWindowState.Maximized
        Else

            My.Forms.UsuallySetting.MdiParent = Me

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.UsuallySetting.Show()

            My.Forms.UsuallySetting.WindowState = FormWindowState.Maximized

        End If
        '---------------------------------------


    End Sub


    Private Sub btn_UsuallySetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UsuallySetting.Click

        If My.Forms.UsuallySetting Is Nothing Then

            My.Forms.UsuallySetting.MdiParent = Me

            'My.Forms.UsuallySetting.Text = ""

            'My.Forms.UsuallySetting.ShowIcon = False

            'My.Forms.UsuallySetting.ControlBox = False


            My.Forms.UsuallySetting.Show()

            My.Forms.UsuallySetting.WindowState = FormWindowState.Maximized

        Else

            My.Forms.UsuallySetting.MdiParent = Me

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.UsuallySetting.Show()

            My.Forms.UsuallySetting.WindowState = FormWindowState.Maximized

        End If

      

    End Sub

    Private Sub btn_WebMailSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_WebMailSetting.Click




        If My.Forms.WebMailSetting Is Nothing Then

            My.Forms.WebMailSetting.MdiParent = Me

            'My.Forms.WebMailSetting.Text = ""

            'My.Forms.WebMailSetting.ShowIcon = False

            'My.Forms.WebMailSetting.ControlBox = False


            My.Forms.WebMailSetting.Show()

            My.Forms.WebMailSetting.WindowState = FormWindowState.Maximized

        Else

            My.Forms.WebMailSetting.MdiParent = Me

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.WebMailSetting.Show()

            My.Forms.WebMailSetting.WindowState = FormWindowState.Maximized

        End If





    End Sub

    Private Sub btn_SMSSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SMSSetting.Click

        If My.Forms.SMSSetting Is Nothing Then

            My.Forms.SMSSetting.MdiParent = Me

            'My.Forms.SMSSetting.Text = ""

            'My.Forms.SMSSetting.ShowIcon = False

            'My.Forms.SMSSetting.ControlBox = False


            My.Forms.SMSSetting.Show()

            My.Forms.SMSSetting.WindowState = FormWindowState.Maximized

        Else

            My.Forms.SMSSetting.MdiParent = Me

            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.SMSSetting.Show()

            My.Forms.SMSSetting.WindowState = FormWindowState.Maximized

        End If





    End Sub


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Close()

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

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "文字檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: 在此加入開啟檔案的程式碼。
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "文字檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: 在此加入程式碼，將表單目前的內容儲存成檔案。
        End If
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

    'Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    'End Sub

    'Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    'End Sub

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

  

End Class
