
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.VO.Sample
Imports EDM.Holmes.DB





Public Class SMSSamp


    Private selectedSamp As String
    Private selecteNoteId As String

    Public Event NoteChg(ByVal selectedNoteId As String)

    Public Property _SelectedSamp()
        Get
            Return Me.selectedSamp
        End Get
        Set(ByVal value)
            Me.selectedSamp = value
        End Set
    End Property

    Public Property _SelectedNoteId()
        Get
            Return Me.selecteNoteId
        End Get
        Set(ByVal value)
            Me.selecteNoteId = value
            RaiseEvent NoteChg(Me.selecteNoteId)
        End Set
    End Property



    Private Sub SampleMaintain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.RefreshData("")

        AddCheckToGridView()

        CboInit()


    End Sub

    ''' <summary>
    ''' ��s���
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshData(ByVal CatSerial As String)

        BuildTree()


        Dim samp As New Saple

        Dim dt As DataTable = samp.getAllSamp(Saple.SmsSamp, CatSerial)

        Me.BindingSource1.DataSource = dt

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.DataSource = Me.BindingSource1

        Me.DataGridView1.Columns("Serial").Visible = False
        Me.DataGridView1.Columns("SampContect").Visible = False
        Me.DataGridView1.Columns("ParentId").Visible = False
        Me.DataGridView1.Columns("SampCat").Visible = False
        Me.DataGridView1.Columns("SampName").Visible = False
        'Me.DataGridView1.Columns("SampName").HeaderText = "�d���W��"
        Me.DataGridView1.Columns("SampContent").Visible = True
        Me.DataGridView1.Columns("SampContent").HeaderText = "�d�����e"
        Me.DataGridView1.Columns("LblName").HeaderText = "����"
        Me.DataGridView1.Columns("LblName").Visible = True
        Me.DataGridView1.Columns("a.PublishDay").Visible = False
        'Me.DataGridView1.Columns("a.PublishDay").HeaderText = "�إ߮ɶ�"
        Me.DataGridView1.Columns("a.CatSerial").Visible = False
        Me.DataGridView1.Columns("b.PublishDay").Visible = False
        Me.DataGridView1.Columns("b.CatSerial").Visible = False
        Me.DataGridView1.Columns("LblCat").Visible = False


    End Sub

    'init label control
    Private Sub CboInit()


        Dim samp As New Saple

        Dim dt As DataTable = samp.getAllLbl(Saple.SmsSamp)


        'add ��ܼ��� rows-------------------------

        Dim row As DataRow
        row = dt.NewRow()
        row.Item("CatSerial") = 0
        row.Item("LblName") = "��ܼ���"
        dt.Rows.InsertAt(row, 0)

        '-------------------------------------

        Me.cbo_LabelName.DataSource = dt

        Me.cbo_LabelName.DisplayMember = "LblName"

        Me.cbo_LabelName.ValueMember = "CatSerial"

        'Me.cbo_LabelName.SelectedIndex = 0

    End Sub


    ''' <summary>
    ''' ������Ү�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbo_LabelName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_LabelName.SelectedIndexChanged

        '-------------------���o�������-----------------------
        '���o�ҿ������ñ
        Dim selectedLblSerial As String = Me.cbo_LabelName.SelectedValue.ToString
        Dim samp As New Saple
        Dim i As Integer
        Dim serial As String = ""
        Dim sampName As String = ""
        Dim sampContent As String = ""
        For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
            If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
                '���o����d����Serial
                serial = DataGridView1.Rows(i).Cells(1).Value.ToString()
                'update the samp label
                Try
                    samp.uptSampLbl(selectedLblSerial, serial)

                Catch ex As Exception
                    MsgBox("Error_Code:143_SMSSamp_Error" & vbCrLf & ex.Message)
                    Exit Sub
                End Try
            End If
        Next
        '-------------------���o�������-----------------------
        Me.RefreshData("")

    End Sub



    Public Sub BuildTree()

        Me.TreeView1.Nodes.Clear()

        AddNodes(Me.TreeView1, Nothing)


    End Sub

    ''' <summary>
    ''' ���j�Ы�Tree�`�I
    ''' </summary>
    ''' <param name="myTree"></param>
    ''' <param name="Pid"></param>
    ''' <remarks></remarks>
    Private Sub AddNodes(ByRef myTree As TreeView, ByVal Pid As String)


        Dim dv As DataView = Me.GetDataTable

        Dim filterExpr As String = String.Empty

        filterExpr = "LblCat = '²�T�d��' "

        dv.RowFilter = filterExpr

        Dim rows() As DataRow = dv.ToTable().Select()

        If rows.GetUpperBound(0) >= 0 Then

            Dim i As Integer

            For i = 0 To rows.GetUpperBound(0) Step i + 1
                Dim row As DataRow = rows(i)
                Dim tmpNodeID As String
                Dim tmpsText As String


                tmpNodeID = row("CatSerial").ToString()
                tmpsText = row("LblName").ToString()

                Dim t As New TreeNode()
                t.Text = tmpsText
                t.ToolTipText = tmpNodeID

                myTree.Nodes.Add(t)
            Next


        End If
    End Sub

    Private Function GetDataTable() As DataView

        Dim samp As New Saple

        Dim dt As DataTable = samp.getAllLbl()

       
        'add �ڸ`�I----------------------------
        Dim row As DataRow = dt.NewRow
        row = dt.NewRow()
        row.Item("CatSerial") = 0
        row.Item("LblName") = "�Ҧ��d��"
        row.Item("LblCat") = "²�T�d��"
        dt.Rows.InsertAt(row, 0)

        '--------------------------------------

        Dim dv As DataView = dt.DefaultView

        Return dv



    End Function


    ''' <summary>
    ''' ���treeview node��޵o
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect


        Dim selectedNode As String = CType(sender, TreeView).SelectedNode.ToolTipText

        Me._SelectedNoteId = selectedNode

        If String.IsNullOrEmpty(selectedNode) Then
            Me._SelectedSamp = ""
        Else
            Me._SelectedSamp = selectedNode
            Me._SelectedNoteId = selectedNode
        End If

        'MsgBox(Me._SelectedNoteId)

        '�Y�O0��ܬ��ڸ`�I
        If Me._SelectedNoteId <> 0 Then
            Me.RefreshData(Me._SelectedNoteId)
        Else
            Me.RefreshData("")

        End If


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
    ''' �T�{���s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click


        '-------------------���o�������-----------------------
        Dim i As Integer
        Dim serial As String = ""
        Dim sampName As String = ""
        Dim sampContent As String = ""
        For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
            If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
                serial = DataGridView1.Rows(i).Cells(1).Value.ToString()
                sampName = DataGridView1.Rows(i).Cells("SampName").Value.ToString
                sampContent = DataGridView1.Rows(i).Cells("SampContent").Value.ToString
                My.Forms.SendMail._SelectedSampName = sampName
                My.Forms.SendMail._SelectedSampId = serial
            End If
        Next
        '-------------------���o�������-----------------------

        '�ǭȵ�SendMail Form
        'My.Forms.SendMail.lbl_SelectedSample.Text = "�d���W��:" & sampName
        My.Forms.SendMail.lbl_SelectedSampleName.Text &= sampName
        My.Forms.SendMail.lbl_SelectedSampleName.Visible = True
        'My.Forms.SendMail.lbl_sele()
        My.Forms.SendMail.txt_Content.Text = sampContent

        '�ǭȵ�SMS Form
        'My.Forms.SMS._SelectedSampName = sampName
        My.Forms.SMS._SelectedSampId = serial
        'My.Forms.SMS._SelectedSampContent = sampContent
        'My.Forms.SMS.btn_ChooseSamp.Text = "�w��ܽd��" & sampName
        My.Forms.SMS.txt_Content.Text = sampContent
        My.Forms.SMS._SelectedSampName = sampName
        My.Forms.SMS.lbl_SampName.Visible = True

        '�Y�LMdi Parent,�h�����ۤv,�_�h�N��������Mdi Parent
        If Not Me.ParentForm Is Nothing Then

            Me.ParentForm.Close()

        Else
            Me.Close()
        End If




    End Sub

    ''' <summary>
    ''' �������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click


        Me.Close()


    End Sub


    ''' <summary>
    ''' �s�W�d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        '���ýd���W��LABEL�M�d���W��TEXTBOX,�]��²�T�u�ݭn���e--
        My.Forms.AddSample.lbl_SampName.Visible = False

        My.Forms.AddSample.txt_SampName.Visible = False

        Dim point As New Point(105, 50)

        Dim point2 As New Point(23, 50)

        My.Forms.AddSample.lbl_SampContent.Location = point2

        My.Forms.AddSample.txt_SampContent.Location = point

        My.Forms.AddSample.lbl_InputLength.Visible = True
        '-------------------------------------------------------


        My.Forms.AddSample.Show()

        Dim btn As Button = My.Forms.AddSample.btn_Add

        AddHandler btn.Click, AddressOf AddSample

    End Sub


    ''' <summary>
    ''' �s�W�d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddSample(ByVal sender As Object, ByVal e As EventArgs)


        Dim sampContent As String = My.Forms.AddSample.txt_SampContent.Text

        Dim samp As New Saple

        samp._SampContent = sampContent
        samp._SampCat = Saple.SmsSamp

        samp.AddSamp(samp)

        Me.RefreshData("")

        My.Forms.AddSample.Close()


    End Sub





    ''' <summary>
    ''' �R���d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        Dim msg As MsgBoxResult

        If Me.DataGridView1.Rows.Count > 0 Then


            msg = MsgBox("�T�w�n�R��������?", MsgBoxStyle.YesNo)

        Else
            Exit Sub

        End If

        '�T���O�_�R��
        If msg = MsgBoxResult.Yes Then

            Dim samp As New Saple
            Dim i As Integer
            Dim serial As String = Nothing
            For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
                If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
                    serial = DataGridView1.Rows(i).Cells(1).Value.ToString()
                    samp.deleteSamp(serial)
                End If
            Next

            '�A�h�R�ثe�Ѽв��쪺�@��,�H�K�û����@���R����
            samp.deleteSamp(DataGridView1.CurrentRow.Cells("Serial").Value.ToString)

        End If

        '���s��z
        Me.RefreshData("")


    End Sub



    ''' <summary>
    ''' �W�[����`�I
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click

        My.Forms.AddCat.Show()
        My.Forms.AddCat.Left = 270
        My.Forms.AddCat.Top = 195

        Dim btn As Button = My.Forms.AddCat.btn_AddCat

        AddHandler btn.Click, AddressOf AddCat


    End Sub

    ''' <summary>
    ''' �s�W����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddCat(ByVal sender As Object, ByVal e As EventArgs)

        Dim catName As String = My.Forms.AddCat._CatName

        If Not String.IsNullOrEmpty(catName) Then

            Dim samp As New EDM.Holmes.VO.Sample.Saple
            samp.AddLbl(catName, Saple.SmsSamp)

        End If

        My.Forms.AddCat.Close()

        Me.BuildTree()

        CboInit()


    End Sub

    ''' <summary>
    ''' �R���`�I
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click



        Dim NoteId As String = Me._SelectedNoteId

        If String.IsNullOrEmpty(NoteId) Then

            MsgBox("�п�ܶ���")

            Exit Sub

        End If

        Dim samp As New Saple

        samp.DeleteLbl(NoteId)


        Me.BuildTree()

        CboInit()

    End Sub



End Class