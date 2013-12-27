Imports System.Windows.Forms

Public Class AddWork


    Private Sub AddWork_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '載入Step1視窗
        LoadStep1()

    End Sub

    ''' <summary>
    ''' 載入Step1視窗
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStep1()

        '一開始就載入Step1視窗
        My.Forms.Step1.MdiParent = Me
        My.Forms.Step1.WindowState = FormWindowState.Maximized
        My.Forms.Step1.Show()
        'My.Forms.ChooseMailBp.MdiParent = Me
        'My.Forms.ChooseMailBp.WindowState = FormWindowState.Maximized
        'My.Forms.ChooseMailBp.Show()


        Me.btn_Step1.BackColor = Color.Blue

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


    ''' <summary>
    ''' Step1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Step1.Click


       

    End Sub

    ''' <summary>
    ''' Step2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Step2.Click

       

    End Sub

   
End Class
