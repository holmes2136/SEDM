Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports EDM.Holmes.Tools


Public Class ChooseMailBp


    Private selectedMailBpSerial As String
    Private selectedMailBpName As String
    Private BpType As String

    Public Property _SelectedMailBpSerial()
        Get
            Return Me.selectedMailBpSerial
        End Get
        Set(ByVal value)
            Me.selectedMailBpSerial = value
        End Set
    End Property

    Public Property _SelectedMailBpName()
        Get
            Return Me.selectedMailBpName
        End Get
        Set(ByVal value)
            Me.selectedMailBpName = value
        End Set
    End Property

    Public Property _Bptype()
        Get
            Return Me.BpType
        End Get
        Set(ByVal value)
            Me.BpType = value
        End Set
    End Property

    Private Sub ChooseMailBp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '初始化控制項--------------
        Dim tool As New Tool
        tool.YearInit(Me.cbo_Year)
        tool.MonthInit(Me.cbo_Month)
        '-------------------------


        '初始化控制項--------------
        DGTool.AddCheckToGridView(Me.DataGridView1)
        '-------------------------



        '初始化搜尋--------------------------------------------
        Dim year As String = Me.cbo_Year.SelectedItem.ToString

        Dim month As String = Me.cbo_Month.SelectedItem.ToString

        Dim d1 As DateTime = New DateTime(year, month, 1)

        Dim time As String = d1.ToString("yyyy-MM")
        '-------------------------------------------------------


        Me.refreshData("", time)


    End Sub


    Private Sub ChooseMailBp_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


        '----------------------------------

        My.Forms.AddWork.btn_Step1.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step2.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------
    End Sub


    ''' <summary>
    ''' 更新專案
    ''' </summary>
    ''' <param name="mailTitle"></param>
    ''' <param name="time"></param>
    ''' <remarks></remarks>
    Public Overloads Sub refreshData(ByVal mailTitle As String, ByVal time As String)

        Dim bp As New EDM.Holmes.Mail.MailBp

        Dim dt As DataTable = bp.getMailBpByTitleAndTime(mailTitle, time)
        Dim i As Integer = dt.Rows.Count


        Me.BindingSource1.DataSource = dt

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.DataSource = Me.BindingSource1

        Me.DataGridView1.AllowUserToAddRows = False

        Me.DataGridView1.Columns("Serial").Visible = False
        Me.DataGridView1.Columns("Contect").Visible = False

        Me.DataGridView1.Columns("Publishday").Visible = False
        Me.DataGridView1.Columns("MailAttachment").Visible = False

        Me.DataGridView1.Columns("MailSender").Visible = True
        Me.DataGridView1.Columns("MailSender").HeaderText = "寄件人"
        Me.DataGridView1.Columns("MailReceiver").Visible = True
        Me.DataGridView1.Columns("MailReceiver").HeaderText = "收件人"
        Me.DataGridView1.Columns("MailTitle").Visible = True
        Me.DataGridView1.Columns("MailTitle").HeaderText = "信件標題"
        Me.DataGridView1.Columns("MailContent").Visible = True
        Me.DataGridView1.Columns("MailContent").HeaderText = "信件內容"
        Me.DataGridView1.Columns("BpType").Visible = True
        Me.DataGridView1.Columns("BpType").HeaderText = "信件類型"

    End Sub



    ''' <summary>
    ''' Next Step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim selectedItem As EDM.Holmes.Mail.MailBp = getCheckedBp(Me.DataGridView1, 0)
        If selectedItem Is Nothing Then
            MsgBox("請選取寄件備份")
            Exit Sub
        End If

        If String.IsNullOrEmpty(selectedItem._MailSerial) Then
            MsgBox("請選擇項目")
            Exit Sub
        End If

        Me._SelectedMailBpSerial = selectedItem._MailSerial
        Me._SelectedMailBpName = selectedItem._mailTitle
        Me._Bptype = selectedItem._BpType
        '---------------------------------------------------------

        If My.Forms.Step1 Is Nothing Then
            My.Forms.Step1.MdiParent = My.Forms.AddWork
            My.Forms.Step1.Show()
            My.Forms.Step1.WindowState = FormWindowState.Maximized
        Else


            My.Forms.Step1.MdiParent = My.Forms.AddWork
            My.Forms.Step1.WindowState = FormWindowState.Maximized
            '仍需要show,否則一但Step Load過,第二次在執行一樣不會Show
            My.Forms.Step1.Show()

        End If



        '---------------------------------------------------------


    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub


    ''' <summary>
    ''' 搜尋
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click


        Dim mailTitle As String = Me.txt_Title.Text.Trim

        Dim year As String = Me.cbo_Year.SelectedItem.ToString

        Dim month As String = Me.cbo_Month.SelectedItem.ToString

        Dim d1 As Date = New Date(year, month, 1)

        Dim time As String = d1.ToString("yyyy-MM")

        Me.refreshData(mailTitle, time)


    End Sub

    ''' <summary>
    ''' 取得勾選的客戶CustEmail清單
    ''' </summary>
    ''' <param name="dg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCheckedBp(ByRef dg As DataGridView, ByVal chkIndex As Integer) As EDM.Holmes.Mail.MailBp

        Dim i As Integer
        'Dim serial As String = ""
        Dim bp As EDM.Holmes.Mail.MailBp = Nothing

        For i = dg.Rows.Count - 1 To 0 Step i - 1
            If dg.Rows(i).Cells(chkIndex).Value <> Nothing And CType(dg.Rows(i).Cells(0).Value, Boolean) Then

                bp = New EDM.Holmes.Mail.MailBp
                bp._MailSerial = dg.Rows(i).Cells("Serial").Value.ToString
                bp._mailTitle = dg.Rows(i).Cells("mailTitle").Value.ToString
                bp._BpType = dg.Rows(i).Cells("BpType").Value.ToString

            End If
        Next

        Return bp

    End Function




End Class