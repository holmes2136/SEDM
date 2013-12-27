Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports System.Data
Imports Microsoft.Win32.TaskScheduler
Imports EDM.Holmes.Tools


Public Class WorkArrange

    Private selectedProject As String
    Private selectedNoteId As String


    Public Property _SelectedProject()
        Get
            Return Me.selectedProject
        End Get
        Set(ByVal value)
            Me.selectedProject = value
        End Set
    End Property

    Public Property _SelectedNoteId()
        Get
            Return Me.selectedNoteId
        End Get
        Set(ByVal value)
            Me.selectedNoteId = value
        End Set
    End Property




    Private Sub WorkArrange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '�w�]���tree�ڨ���ܤ@�}�l��gridview
        'Dim dt As DataTable = GetDataTable("")

        'Me.BindingSource1.DataSource = dt

        'Me.DataGridView1.DataSource = Me.BindingSource1



        Me.RefreshData()

        DGTool.AddCheckToGridView(Me.DataGridView1)

    End Sub

    Private Sub Init()

        'dt = New DataTable







    End Sub

    ''' <summary>
    ''' ��s���
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshData()

        'Me.BindingSource1.Clear()

        Dim dt As New DataTable


        dt.Columns.Add("Serial")
        dt.Columns.Add("Name")
        dt.Columns.Add("LastRunTime")
        dt.Columns.Add("NextRunTime")
        dt.Columns.Add("BpType")



        Try
            Using ts As New TaskService
                Using tf As TaskFolder = ts.GetFolder("")

                    For Each task As Task In tf.Tasks
                        '�u���EDM���Ƶ{,����ܹq���즳���Ƶ{
                        If EDM.Holmes.VO.Work.Task.IsEDMTask(task.Definition.RegistrationInfo.Description) Then


                            Dim dr As DataRow = dt.NewRow()
                            dr.Item("Serial") = task.Name
                            dr.Item("Name") = EDM.Holmes.VO.Work.Task.DisplayEDMTaskName(task.Definition.RegistrationInfo.Description)
                            dr.Item("LastRunTime") = EDM.Holmes.VO.Work.Task.DisplayNotYetExe(task.LastRunTime)
                            dr.Item("NextRunTime") = task.NextRunTime
                            dr.Item("BpType") = EDM.Holmes.VO.Work.Task.DisplayEDMTaskType(task.Definition.RegistrationInfo.Description)
                            dt.Rows.Add(dr)

                        End If
                    Next

                    Me.BindingSource1.DataSource = dt

                    Me.BindingNavigator1.BindingSource = Me.BindingSource1

                    Me.DataGridView1.DataSource = Me.BindingSource1

                End Using

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Me.DataGridView1.Columns("Serial").Visible = False
        Me.DataGridView1.Columns("Name").Visible = True
        Me.DataGridView1.Columns("Name").HeaderText = "�Ƶ{�W��"
        Me.DataGridView1.Columns("LastRunTime").Visible = True
        Me.DataGridView1.Columns("LastRunTime").HeaderText = "�̫�@������ɶ�"
        Me.DataGridView1.Columns("NextRunTime").Visible = True
        Me.DataGridView1.Columns("NextRunTime").HeaderText = "�U������ɶ�"
        Me.DataGridView1.Columns("BpType").Visible = True
        Me.DataGridView1.Columns("BpType").HeaderText = "�H�e����"


    End Sub


    ''' <summary>
    ''' �R���Ƶ{ 2011/10/4 not use
    ''' </summary>
    ''' <param name="Serial"></param>
    ''' <remarks></remarks>
    Private Sub DeleteProject(ByVal Serial As String)




        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "delete * from �Ƶ{�� Where Serial = ? "

        cmd.Parameters.Add("@Serial", OleDbType.Integer).Value = Serial

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using






    End Sub

    '2011/10/4 not use
    Public Sub UptProject(ByVal NoteId As String, ByVal prjName As String)


        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "UPDATE �M�ת� SET projectName = ? WHERE NoteId = ?"

        cmd.Parameters.Add("@projectName", OleDbType.VarChar).Value = prjName
        cmd.Parameters.Add("@NoteId", OleDbType.Integer).Value = CInt(NoteId)


        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using



    End Sub

    ''' <summary>
    ''' �R��Window�u�@�Ƶ{�U���u�@
    ''' </summary>
    ''' <param name="Serial"></param>
    ''' <remarks></remarks>
    Private Sub DeleteWorkArrange(ByVal Serial As String)


        Dim ts As New TaskService

        Dim td As TaskDefinition = ts.NewTask


        ts.RootFolder.DeleteTask(Serial)


    End Sub




    ''' <summary>
    ''' �s�W�Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BindingNavigatorAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)




    End Sub




    ''' <summary>
    ''' �s�W�Ŀ����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCheckToGridView()

        Dim chk As New DataGridViewCheckBoxColumn
        chk.HeaderText = "�Ŀ�"
        chk.Name = "�Ŀ�"
        chk.Width = 40
        DataGridView1.Columns.Insert(0, chk)

    End Sub


    ''' <summary>
    ''' �R���Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SelectAll.Click

        For i As Integer = 0 To DataGridView1.Rows.Count - 1

            Dim chk As DataGridViewCheckBoxCell = DataGridView1.Rows(i).Cells(0)

            If (chk.Value Is Nothing) Or (chk.Value = "false") Then
                chk.Value = True
            Else
                chk.Value = False

            End If

        Next

    End Sub


    ''' <summary>
    ''' �I����U��X�{�ק����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        'My.Forms.WorkInfo._SelectedPrj = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString

        'My.Forms.WorkInfo.ShowDialog()


        ''�[�JLabel,TextBox,Button�� AddWorkArrange Form
        'My.Forms.AddWorkArrange.btn_ShowChhoseTimePrj.Visible = False
        'My.Forms.AddWorkArrange.OK_Button.Visible = False

        'Dim lbl As New Label
        'lbl.Text = "�Ƶ{�W��"
        'lbl.TextAlign = ContentAlignment.MiddleCenter
        'lbl.Left = 1
        'lbl.Top = 20



        'Dim ok As New Button
        'ok.Text = "�T�{"
        'ok.Name = "btn_Ok"

        'AddHandler ok.Click, AddressOf UptTask


        'With My.Forms.AddWorkArrange
        '    .lbl_PrjName.ReadOnly = False
        '    .lbl_PrjName.Text = Me.DataGridView1.CurrentRow.Cells("PrjName").Value.ToString
        '    '�����OSerial�Ӥ��ONoteId,�]���u�@�Ƶ{�O�H�Ƶ{��Serial�R�W,�p���~����Ƶ{�W��
        '    .lbl_PrjNoteId.Text = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString
        '    .lbl_PrjSerial.Text = Me.DataGridView1.CurrentRow.Cells("NoteId").Value.ToString
        '    .GroupBox2.Controls.Add(lbl)
        '    .TableLayoutPanel1.Controls.Add(ok)
        '    .Show()
        'End With





    End Sub

    ''' <summary>
    ''' ��s�Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub UptTask(ByVal sender As System.Object, ByVal e As System.EventArgs)


        My.Forms.AddWorkArrange.UptTask()


        Me.RefreshData()


        My.Forms.AddWorkArrange.Close()


    End Sub

    ''' <summary>
    ''' �s�W�Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click


        'AddWorkArrange.Show()
        My.Forms.AddWork.Show()


    End Sub


    ''' <summary>
    ''' �R���Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click



        'Dim msg As MsgBoxResult = MsgBox("�T�w�n�R����?", MsgBoxStyle.OkCancel)

        'If msg = MsgBoxResult.Ok Then



        '    Dim serial As String = ""

        '    If Not serial Is Nothing Then
        '        serial = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString

        '    Else
        '        Exit Sub

        '    End If


        '    If Not String.IsNullOrEmpty(serial) Then

        '        '�R���M�ת���w�u�@()
        '        DeleteProject(serial)

        '        '�R���u�@�Ƶ{���w�u�@
        '        DeleteWorkArrange(serial)
        '    Else

        '        MsgBox("�п������")

        '    End If

        'End If

        '2011/10/4
        'Dim msg As MsgBoxResult

        'If Me.DataGridView1.Rows.Count > 0 Then

        '    msg = MsgBox("�T�w�n�R��������?", MsgBoxStyle.YesNo)

        'End If


        ''�T���O�_�R��
        'If msg = MsgBoxResult.Yes Then

        '    Dim i As Integer
        '    Dim serial As String = Nothing
        '    For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
        '        If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
        '            serial = DataGridView1.Rows(i).Cells(1).Value.ToString()
        '            DeleteProject(serial)
        '            DeleteWorkArrange(serial)
        '        End If
        '    Next

        '    '�A�h�R�ثe�Ѽв��쪺�@��,�H�K�û����@���R����
        '    DeleteProject(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)
        '    DeleteWorkArrange(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)
        'End If

        '���s��z
        'Me.RefreshData()

        Dim msg As MsgBoxResult

        If Me.DataGridView1.Rows.Count > 0 Then

            msg = MsgBox("�T�w�n�R��������?", MsgBoxStyle.YesNo)

        End If


        '�T���O�_�R��
        If msg = MsgBoxResult.Yes Then

            Dim i As Integer
            Dim serial As String = Nothing
            Try
                Using ts As New TaskService
                    Using tf As TaskFolder = ts.GetFolder("")
                        For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
                            If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
                                serial = DataGridView1.Rows(i).Cells("Serial").Value.ToString
                                tf.DeleteTask(serial)
                            End If
                        Next

                        tf.DeleteTask(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)
                    End Using
                    Me.RefreshData()
                End Using
            Catch ex As Exception
                MsgBox("Error_Code:471_WorkArrange_Error" & vbCrLf & ex.Message)
            End Try
        End If



        '�T���O�_�R��
        'If msg = MsgBoxResult.Yes Then

        '    Dim i As Integer
        '    Dim serial As String = Nothing
        '    Dim prj As New Project
        '    For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
        '        If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
        '            serial = DataGridView1.Rows(i).Cells(1).Value.ToString()
        '            prj.DeletePrj(serial)
        '            prj.DeleteCustPrj(serial)
        '        End If
        '    Next

        '    '�A�h�R�ثe�Ѽв��쪺�@��,�H�K�û����@���R����
        '    prj.DeletePrj(DataGridView1.CurrentRow.Cells("NoteId").Value.ToString)
        '    prj.DeleteCustPrj(DataGridView1.CurrentRow.Cells("NoteId").Value.ToString)


        'End If

    End Sub

    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click


        Me.Close()

    End Sub


    ''' <summary>
    ''' ��s�Ƶ{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click

        If Me.DataGridView1.Rows.Count > 0 Then

            Try
                Using ts As New TaskService
                    Using tf As TaskFolder = ts.GetFolder("")

                        tf.Tasks(Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString).ShowPropertyPage()

                    End Using
                    Me.RefreshData()
                End Using

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If


    End Sub
End Class