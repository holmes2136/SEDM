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


        '預設選取tree根來顯示一開始的gridview
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
    ''' 更新資料
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
                        '只顯示EDM的排程,不顯示電腦原有的排程
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
        Me.DataGridView1.Columns("Name").HeaderText = "排程名稱"
        Me.DataGridView1.Columns("LastRunTime").Visible = True
        Me.DataGridView1.Columns("LastRunTime").HeaderText = "最後一次執行時間"
        Me.DataGridView1.Columns("NextRunTime").Visible = True
        Me.DataGridView1.Columns("NextRunTime").HeaderText = "下次執行時間"
        Me.DataGridView1.Columns("BpType").Visible = True
        Me.DataGridView1.Columns("BpType").HeaderText = "寄送類型"


    End Sub


    ''' <summary>
    ''' 刪除排程 2011/10/4 not use
    ''' </summary>
    ''' <param name="Serial"></param>
    ''' <remarks></remarks>
    Private Sub DeleteProject(ByVal Serial As String)




        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "delete * from 排程表 Where Serial = ? "

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

        cmd.CommandText = "UPDATE 專案表 SET projectName = ? WHERE NoteId = ?"

        cmd.Parameters.Add("@projectName", OleDbType.VarChar).Value = prjName
        cmd.Parameters.Add("@NoteId", OleDbType.Integer).Value = CInt(NoteId)


        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using



    End Sub

    ''' <summary>
    ''' 刪除Window工作排程下的工作
    ''' </summary>
    ''' <param name="Serial"></param>
    ''' <remarks></remarks>
    Private Sub DeleteWorkArrange(ByVal Serial As String)


        Dim ts As New TaskService

        Dim td As TaskDefinition = ts.NewTask


        ts.RootFolder.DeleteTask(Serial)


    End Sub




    ''' <summary>
    ''' 新增排程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BindingNavigatorAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)




    End Sub




    ''' <summary>
    ''' 新增勾選欄位
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCheckToGridView()

        Dim chk As New DataGridViewCheckBoxColumn
        chk.HeaderText = "勾選"
        chk.Name = "勾選"
        chk.Width = 40
        DataGridView1.Columns.Insert(0, chk)

    End Sub


    ''' <summary>
    ''' 刪除排程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    ''' <summary>
    ''' 全選
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
    ''' 點擊兩下後出現修改視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        'My.Forms.WorkInfo._SelectedPrj = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString

        'My.Forms.WorkInfo.ShowDialog()


        ''加入Label,TextBox,Button到 AddWorkArrange Form
        'My.Forms.AddWorkArrange.btn_ShowChhoseTimePrj.Visible = False
        'My.Forms.AddWorkArrange.OK_Button.Visible = False

        'Dim lbl As New Label
        'lbl.Text = "排程名稱"
        'lbl.TextAlign = ContentAlignment.MiddleCenter
        'lbl.Left = 1
        'lbl.Top = 20



        'Dim ok As New Button
        'ok.Text = "確認"
        'ok.Name = "btn_Ok"

        'AddHandler ok.Click, AddressOf UptTask


        'With My.Forms.AddWorkArrange
        '    .lbl_PrjName.ReadOnly = False
        '    .lbl_PrjName.Text = Me.DataGridView1.CurrentRow.Cells("PrjName").Value.ToString
        '    '必須是Serial而不是NoteId,因為工作排程是以排程表的Serial命名,如此才能抓到排程名稱
        '    .lbl_PrjNoteId.Text = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString
        '    .lbl_PrjSerial.Text = Me.DataGridView1.CurrentRow.Cells("NoteId").Value.ToString
        '    .GroupBox2.Controls.Add(lbl)
        '    .TableLayoutPanel1.Controls.Add(ok)
        '    .Show()
        'End With





    End Sub

    ''' <summary>
    ''' 更新排程
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
    ''' 新增排程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click


        'AddWorkArrange.Show()
        My.Forms.AddWork.Show()


    End Sub


    ''' <summary>
    ''' 刪除排程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click



        'Dim msg As MsgBoxResult = MsgBox("確定要刪除嗎?", MsgBoxStyle.OkCancel)

        'If msg = MsgBoxResult.Ok Then



        '    Dim serial As String = ""

        '    If Not serial Is Nothing Then
        '        serial = Me.DataGridView1.CurrentRow.Cells("Serial").Value.ToString

        '    Else
        '        Exit Sub

        '    End If


        '    If Not String.IsNullOrEmpty(serial) Then

        '        '刪除專案表指定工作()
        '        DeleteProject(serial)

        '        '刪除工作排程指定工作
        '        DeleteWorkArrange(serial)
        '    Else

        '        MsgBox("請選取項目")

        '    End If

        'End If

        '2011/10/4
        'Dim msg As MsgBoxResult

        'If Me.DataGridView1.Rows.Count > 0 Then

        '    msg = MsgBox("確定要刪除此項目?", MsgBoxStyle.YesNo)

        'End If


        ''確任是否刪除
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

        '    '再多刪目前由標移到的一筆,以免永遠有一筆刪不掉
        '    DeleteProject(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)
        '    DeleteWorkArrange(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)
        'End If

        '重新整理
        'Me.RefreshData()

        Dim msg As MsgBoxResult

        If Me.DataGridView1.Rows.Count > 0 Then

            msg = MsgBox("確定要刪除此項目?", MsgBoxStyle.YesNo)

        End If


        '確任是否刪除
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



        '確任是否刪除
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

        '    '再多刪目前由標移到的一筆,以免永遠有一筆刪不掉
        '    prj.DeletePrj(DataGridView1.CurrentRow.Cells("NoteId").Value.ToString)
        '    prj.DeleteCustPrj(DataGridView1.CurrentRow.Cells("NoteId").Value.ToString)


        'End If

    End Sub

    ''' <summary>
    ''' 關閉視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click


        Me.Close()

    End Sub


    ''' <summary>
    ''' 更新排程
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