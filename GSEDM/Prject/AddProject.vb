Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports System.Text
Imports System.EventArgs
Imports EDM.Holmes.Cust
Imports System.Text.RegularExpressions
Imports EDM.Holmes.Tools
Imports EDM.Holmes.Project
Imports EDM.Holmes.Mail


Public Class AddProject


    '匯入客戶時引發
    Event Cust_Cat_Change(ByVal cat As String, ByVal val As String)

    '勾選客戶時發生
    Event Checked_Cust_Checked(ByVal rows As Integer, ByVal columns As Integer)

    Event Checked_Cust_UnChecked(ByVal rows As Integer, ByVal columns As Integer)

    '若無選取任何客戶種類,則字串的長度會小於10
    Public Const DoNotChooseAnyCustType As Integer = 10

    Public addProject2 As AddProject2

    Private parentId As String

    Private addedPrjName As String

    Private custMobileLst As New Collection

    Public Property _ParentId()
        Get
            Return Me.parentId
        End Get
        Set(ByVal value)
            Me.parentId = value
        End Set
    End Property


    Public Property _AddedPrjName()
        Get
            Return Me.addedPrjName
        End Get
        Set(ByVal value)
            Me.addedPrjName = value
        End Set
    End Property


    Public Property _CustMobileLst()
        Get
            Return Me.custMobileLst
        End Get
        Set(ByVal value)
            Me.custMobileLst = value
        End Set
    End Property


    Private Sub AddProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'turn of the search-----------------

        Me.FlowLayoutPanel1.Enabled = False

        Me.FlowLayoutPanel3.Enabled = False
        '-----------------------------------

        'custom datagirdview control---------------

        DGTool.AddCheckToGridView(Me.DataGridView1)

        '------------------------------------------

        '2011/10/31 temp close-------------------------

        'init year
        'getAllYearInit()

        'init month 
        'MonthInit()

        'init year 
        'Me.cbo_Years2.SelectedIndex = 0

        'init upordown control  
        'Me.cbo_UpOrDown.SelectedIndex = 0


        'StartMonthInit(cbo_StartMonth)

        'init upordown control  
        'Me.cbo_UpOrDown2.SelectedIndex = 0

        '2011/10/31 temp close-------------------------


        Me.RefreshData("", "", "", "", "", 300)

    End Sub


    ''' <summary>
    ''' 初始化年
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getAllYearInit()

        Dim rpt As New EDM.Holmes.Rpt.Rpt1

        Dim year As String = DateTime.Now.Year

        Dim ret As Collection(Of String) = rpt.getAllYear

        For i As Integer = 0 To ret.Count - 1

            Me.cbo_Years.Items.Add(ret.Item(i))

            If ret.Item(i) = year Then

                Me.cbo_Years.SelectedIndex = i

            End If


        Next



    End Sub


    ''' <summary>
    ''' 初始化月
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MonthInit()

        Dim month As String = DateTime.Now.Month

        For i As Integer = 0 To Me.cbo_Month.Items.Count - 1

            If cbo_Month.Items.Item(i) = month Then
                Me.cbo_Month.SelectedIndex = i
            End If
        Next
    End Sub


    ''' <summary>
    ''' 初始化月
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartMonthInit(ByRef cbo_StartMonth As ComboBox)

        Dim month As String = DateTime.Now.AddMonths(-3).Month.ToString

        For i As Integer = 0 To Me.cbo_StartMonth.Items.Count - 1

            If Me.cbo_StartMonth.Items.Item(i) = month Then
                Me.cbo_StartMonth.SelectedIndex = i
            End If
        Next
    End Sub


    ''' <summary>
    ''' basic search 
    ''' </summary>
    ''' <param name="custid"></param>
    ''' <param name="custName"></param>
    ''' <param name="custMobile"></param>
    ''' <param name="custtelphone"></param>
    ''' <param name="custaddress"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RefreshData(ByVal custid As String, ByVal custName As String, ByVal custMobile As String, ByVal custtelphone As String, ByVal custaddress As String)

        
        Dim cust As EDM.Holmes.Cust.OrginCust = CustFactory.getCustTpe(CustFactory.goverment)

        cust._CustId = custid
        cust._CustName = custName
        cust._CustMobile = custMobile
        cust._CustTelPhone = custtelphone
        cust._CustAddress = custaddress


        Dim dt As DataTable = cust.getCustByCondition(cust)

      
        Me.BindingSource1.DataSource = dt

        Me.DataGridView1.DataSource = Me.BindingSource1

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        'Me.DataGridView1.Columns("CustId").Visible = True
        'Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
        Me.DataGridView1.Columns("ShopName").Visible = True
        Me.DataGridView1.Columns("ShopName").HeaderText = "分店名稱"
        Me.DataGridView1.Columns("CustName").Visible = True
        Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
        'Me.DataGridView1.Columns("CustTelPhone").Visible = False
        'Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
        Me.DataGridView1.Columns("CustMobile").Visible = True
        Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
        'Me.DataGridView1.Columns("CustAddress").Visible = False
        'Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
        'Me.DataGridView1.Columns("CustEmail").Visible = False
        'Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"
        Me.DataGridView1.Columns("MyMobile").Visible = False
        'Me.DataGridView1.Columns("MyMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("MyEmail").Visible = False
        'Me.DataGridView1.Columns("MyEmail").HeaderText = "電子郵件"


    End Sub

    ''' <summary>
    ''' 設定取得筆數
    ''' </summary>
    ''' <param name="custid"></param>
    ''' <param name="custName"></param>
    ''' <param name="custMobile"></param>
    ''' <param name="custtelphone"></param>
    ''' <param name="custaddress"></param>
    ''' <param name="count"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RefreshData(ByVal custid As String, ByVal custName As String, ByVal custMobile As String, ByVal custtelphone As String, ByVal custaddress As String, ByVal count As Integer)


        Dim cust As EDM.Holmes.Cust.OrginCust = CustFactory.getCustTpe(CustFactory.goverment)


        cust._CustId = custid
        cust._CustName = custName
        cust._CustMobile = custMobile
        cust._CustTelPhone = custtelphone
        cust._CustAddress = custaddress


        Dim dt As DataTable = cust.getCustByCondition(cust, count)

        

        If Not dt Is Nothing Then

            Me.BindingSource1.DataSource = dt

            Me.DataGridView1.DataSource = Me.BindingSource1

            Me.BindingNavigator1.BindingSource = Me.BindingSource1

            'Me.DataGridView1.VirtualMode = True
            'Me.DataGridView1.Columns("CustId").Visible = True
            ' Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
            Me.DataGridView1.Columns("ShopName").Visible = True
            Me.DataGridView1.Columns("ShopName").HeaderText = "分店名稱"
            Me.DataGridView1.Columns("CustName").Visible = True
            Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
            'Me.DataGridView1.Columns("CustTelPhone").Visible = False
            'Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
            Me.DataGridView1.Columns("CustMobile").Visible = True
            Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
            'Me.DataGridView1.Columns("CustAddress").Visible = False
            'Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
            'Me.DataGridView1.Columns("CustEmail").Visible = True
            'Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"
            Me.DataGridView1.Columns("MyMobile").Visible = False
            'Me.DataGridView1.Columns("MyMobile").HeaderText = "手機"
            Me.DataGridView1.Columns("MyEmail").Visible = False
            'Me.DataGridView1.Columns("MyEmail").HeaderText = "電子郵件"
        End If


    End Sub

    ''' <summary>
    ''' 進階搜尋 2011/10/17 not use
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="cat"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RefreshData(ByVal year As String, ByVal month As String, ByVal cat As String)


        'Dim dt As DataTable = Nothing

        'Dim cust As New EDM.Holmes.Cust.Cust

        'Dim time As String = year & "-" & FilterMonth(month)

        'Dim message As String = ""

        'Try
        '    Select Case cat
        '        Case SearchCust.ByDealAmount
        '            Dim dealAmount As String = Me.txt_DealAmount.Text
        '            dt = cust.getCustByDealAmount(dealAmount, time)
        '            message = "交易金額" & dealAmount & "多少以上"

        'Case SearchCust.ByDealCnt
        '    Dim dealCnt As String = Me.txt_DealCnt.Text
        '    message = "交易次數" & dealCnt & "次"
        '    dt = cust.getCustByDealCnt(dealCnt, time)

        'Case SearchCust.ByMonth
        '    Dim mon As String = Me.txt_DealRecord.Text
        '    message = mon & "月內有交易記錄"
        '    dt = cust.getCustByMonth(mon)

        '    End Select

        'Catch ex As Exception
        '    MsgBox("Error_Code:574_AddProject_Error:參數輸入錯誤")
        '    Exit Sub
        'End Try

        'Me.lbl_SearchMessage.Text = Message

        'Me.BindingSource1.DataSource = dt

        'Me.DataGridView1.DataSource = Me.BindingSource1

        'Me.BindingNavigator1.BindingSource = Me.BindingSource1

        'Me.DataGridView1.Columns("CustId").Visible = True
        'Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
        'Me.DataGridView1.Columns("CustName").Visible = True
        'Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
        'Me.DataGridView1.Columns("CustTelPhone").Visible = True
        'Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
        'Me.DataGridView1.Columns("CustMobile").Visible = True
        'Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
        'Me.DataGridView1.Columns("CustAddress").Visible = True
        'Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
        'Me.DataGridView1.Columns("CustEmail").Visible = True
        'Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"



    End Sub




   

    ''' <summary>
    ''' 搜尋按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Search.Click




        Dim custid As String = Me.txt_CustId.Text.Trim
        Dim custname As String = Me.txt_CustName.Text.Trim
        Dim custmobile As String = Me.txt_CustMobile.Text.Trim
        'Dim custtelphone As String = Me.txt_CustTelPhone.Text.Trim
        'Dim custaddress As String = Me.txt_CustAddress.Text.Trim

        '曾經有消費之客群
        If Me.cbo_HasOrder.Checked = True Then
            Dim year As String = Me.cbo_Years.SelectedItem
            Dim endMonth As String = Me.cbo_Month.SelectedItem
            Dim startMonth As String = Me.cbo_StartMonth.SelectedItem
            Dim spend As Integer = IIf(String.IsNullOrEmpty(Me.txt_DealAmount.Text), 0, Me.txt_DealAmount.Text)

            '累積消費
            If Me.cbo_HasOrder.Checked = True Then

                If Me.rad_PlusAmt.Checked = True Then
                    Select Case Me.cbo_UpOrDown.SelectedItem
                        '以上
                        Case "以上"
                            Me.RefreshData(year, startMonth, endMonth, spend, "up", "plus")
                            '以下
                        Case "以下"
                            Me.RefreshData(year, startMonth, endMonth, spend, "down", "plus")
                    End Select

                    '單筆消費
                ElseIf Me.rad_SingleAmt.Checked = True Then

                    Select Case Me.cbo_UpOrDown.SelectedItem
                        '以上
                        Case "以上"
                            Me.RefreshData(year, startMonth, endMonth, spend, "up", "single")
                            '以下
                        Case "以下"
                            Me.RefreshData(year, startMonth, endMonth, spend, "down", "single")
                    End Select
                End If
           
          

            End If


            '無消費之客群
        ElseIf Me.cbo_NoOrder.Checked = True Then


            Dim year As String = Me.cbo_Years2.SelectedItem

            Dim spend As Integer = IIf(String.IsNullOrEmpty(Me.txt_DealAmt2.Text), 0, Me.txt_DealAmt2.Text)

            '累積消費
            If Me.rad_PlusAmt2.Checked = True Then

                Select Case Me.cbo_UpOrDown2.SelectedItem
                    '以上
                    Case "以上"
                        Me.RefreshData(year, spend, "up", "plus")
                        '以下
                    Case "以下"
                        Me.RefreshData(year, spend, "down", "plus")
                End Select

                '單筆消費
            ElseIf Me.rad_SingleAmt2.Checked = True Then

                Select Case Me.cbo_UpOrDown2.SelectedItem
                    '以上
                    Case "以上"
                        Me.RefreshData(year, spend, "up", "single")
                        '以下
                    Case "以下"
                        Me.RefreshData(year, spend, "down", "single")
                End Select

            End If

        Else

            '基本搜尋
            Me.RefreshData(custid, custname, custmobile, "", "")

        End If


    End Sub



    ''' <summary>
    ''' 取得某段時間有消費客群
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="startMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="spend"></param>
    ''' <param name="UpOrDown"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RefreshData(ByVal year As String, ByVal startMonth As String, ByVal EndMonth As String, ByVal spend As Integer, ByVal UpOrDown As String, ByVal spendType As String)


        Dim dt As DataTable = Nothing

        Dim m1 As String = year & "-" & FilterMonth(startMonth)

        Dim m2 As String = year & "-" & FilterMonth(EndMonth)

        Dim cust As New EDM.Holmes.Cust.Cust

        Dim message As String = ""

        Try

            Select Case spendType
                Case "plus"
                    Select Case UpOrDown

                        Case "up"

                            dt = cust.getCustByHasOrderPlus(m1, m2, spend, "up")

                        Case "down"
                            dt = cust.getCustByHasOrderPlus(m1, m2, spend, "down")

                    End Select
                Case "single"

                    Select Case UpOrDown

                        Case "up"

                            dt = cust.getCustByHasOrderSingle(m1, m2, spend, "up")

                        Case "down"
                            dt = cust.getCustByHasOrderSingle(m1, m2, spend, "down")

                    End Select

            End Select

        Catch ex As Exception
            dt = Nothing
            MsgBox("Error_Code:574_AddProject_Error")
            Exit Sub
        End Try

        Me.lbl_SearchMessage.Text = message

        Me.BindingSource1.DataSource = dt

        Me.DataGridView1.DataSource = Me.BindingSource1

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.Columns("CustId").Visible = True
        Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
        Me.DataGridView1.Columns("CustName").Visible = True
        Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
        Me.DataGridView1.Columns("CustTelPhone").Visible = False
        Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
        Me.DataGridView1.Columns("CustMobile").Visible = False
        Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("CustAddress").Visible = False
        Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
        Me.DataGridView1.Columns("CustEmail").Visible = False
        Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"
        Me.DataGridView1.Columns("MyMobile").Visible = True
        Me.DataGridView1.Columns("MyMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("MyEmail").Visible = True
        Me.DataGridView1.Columns("MyEmail").HeaderText = "電子郵件"


    End Sub

    ''' <summary>
    ''' 取得某段時間無消費客群
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="spend"></param>
    ''' <param name="UpOrDown"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RefreshData(ByVal year As String, ByVal spend As Integer, ByVal UpOrDown As String, ByVal spendType As String)


        Dim dt As DataTable = Nothing

        Dim cust As New EDM.Holmes.Cust.Cust

        Dim message As String = ""

        Try

            Select Case spendType

                Case "plus"
                    Select Case UpOrDown
                        Case "up"

                            dt = cust.getCustByNoOrderPlus(year, spend, "up")

                        Case "down"
                            dt = cust.getCustByNoOrderPlus(year, spend, "down")

                    End Select

                Case "single"
                    Select Case UpOrDown
                        Case "up"

                            dt = cust.getCustByNoOrderSingle(year, spend, "up")

                        Case "down"
                            dt = cust.getCustByNoOrderSingle(year, spend, "down")

                    End Select
            End Select


        Catch ex As Exception
            dt = Nothing
            MsgBox("Error_Code:574_AddProject_Error")
            Exit Sub
        End Try

        Me.lbl_SearchMessage.Text = message

        Me.BindingSource1.DataSource = dt

        Me.DataGridView1.DataSource = Me.BindingSource1

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.Columns("CustId").Visible = True
        Me.DataGridView1.Columns("CustId").HeaderText = "客戶編號"
        Me.DataGridView1.Columns("CustName").Visible = True
        Me.DataGridView1.Columns("CustName").HeaderText = "姓名"
        Me.DataGridView1.Columns("CustTelPhone").Visible = False
        Me.DataGridView1.Columns("CustTelPhone").HeaderText = "電話"
        Me.DataGridView1.Columns("CustMobile").Visible = False
        Me.DataGridView1.Columns("CustMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("CustAddress").Visible = False
        Me.DataGridView1.Columns("CustAddress").HeaderText = "地址"
        Me.DataGridView1.Columns("CustEmail").Visible = False
        Me.DataGridView1.Columns("CustEmail").HeaderText = "信箱"
        Me.DataGridView1.Columns("MyMobile").Visible = True
        Me.DataGridView1.Columns("MyMobile").HeaderText = "手機"
        Me.DataGridView1.Columns("MyEmail").Visible = True
        Me.DataGridView1.Columns("MyEmail").HeaderText = "電子郵件"


    End Sub

    '格式化月份
    Private Function FilterMonth(ByVal month As String) As String

        If month.Length = 1 Then
            month = String.Format("0{0}", month)

        End If

        Return month

    End Function

    ''' <summary>
    ''' 選擇客戶後匯入客戶名單
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub addProject_do(ByVal sender As Object, ByVal e As EventArgs)

        '2012/1/7
        Dim ret As Collection(Of MailClass) = Me.getCheckedLst(Me.DataGridView1)

        'Dim ret As Collection(Of MailClass) = Me.ReadSmsSource

        Dim receiver As New StringBuilder

        For Each x As MailClass In ret
            receiver.Append(IIf(x._Mailto <> "", x._Mailto & ",", ""))
        Next

        Dim test As String = receiver.ToString

        My.Forms.SendMail._SendCustLst = ret
        My.Forms.SendMail._Mailto = test
        My.Forms.SendMail.Show()


    End Sub

  
    ''' <summary>
    ''' 全選 搜尋視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SelectAll1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SelectAll.Click

        DGTool.SelectAll(Me.DataGridView1, 0)


    End Sub


    ''' <summary>
    ''' 加入準備匯出的客戶清單 not use 20110926
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AddCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '-------------------取得選取的值-----------------------
        'Dim i As Integer
        'Dim custid As String = ""
        'Dim custname As String = ""


        'For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
        '    If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then
        '        custid = DataGridView1.Rows(i).Cells("CustId").Value.ToString()
        '        custname = DataGridView1.Rows(i).Cells("CustName").Value.ToString

        '        Dim row() As String = {False, custid, custname}

        '        Me.DataGridView3.Rows.Add(row)
        '    End If
        'Next

        'custid = Me.DataGridView1.CurrentRow.Cells("CustId").Value.ToString
        'custname = Me.DataGridView1.CurrentRow.Cells("CustName").Value.ToString

        'Dim appendRow As String() = {False, custid, custname}

        'Me.DataGridView3.Rows.Add(appendRow)



        '-------------------取得選取的值-----------------------

    End Sub




    ''' <summary>
    ''' not use
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="columns"></param>
    ''' <remarks></remarks>
    Private Sub CheckCount(ByVal row As Integer, ByVal columns As Integer) Handles Me.Checked_Cust_Checked

        'MsgBox(row & "/" & columns)
        'MsgBox(CType(Me.DataGridView1.Rows(row).Cells(columns).Value, Boolean).ToString)
        'MsgBox(Me.DataGridView1.Rows(row).Cells("CustMobile").Value.ToString)

        'If Me.lbl_SelectCnt.Text = "顯示選取的數目" Then
        '    Me.lbl_SelectCnt.Text = "0"
        'End If

        'Dim i As Integer

        'Integer.TryParse(Me.lbl_SelectCnt.Text, i)

        'i += 1

        'Me.lbl_SelectCnt.Text = i

    End Sub

    ''' <summary>
    ''' not use 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="columns"></param>
    ''' <remarks></remarks>
    Private Sub UnCheckCount(ByVal row As Integer, ByVal columns As Integer) Handles Me.Checked_Cust_UnChecked

        'MsgBox(row & "/" & columns)
        'MsgBox(CType(Me.DataGridView1.CurrentRow.Cells(0).Value, Boolean).ToString)    
        'MsgBox(Me.DataGridView1.CurrentRow.Cells("CustMobile").Value.ToString)

        'Dim i As Integer

        'Integer.TryParse(Me.lbl_SelectCnt.Text, i)

        'i = i - 1

        'Me.lbl_SelectCnt.Text = i


    End Sub


    ''' <summary>
    ''' 已經選取的清單 not use 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCheckedLst(ByRef obj As DataGridView) As Collection(Of MailClass)


        '20110926
        Dim i As Integer

        Dim ret As New Collection(Of MailClass)

        For i = obj.Rows.Count - 1 To 0 Step i - 1

            If obj.Rows(i).Cells(0).Value <> Nothing And CType(obj.Rows(i).Cells(0).Value, Boolean) Then

                Dim cust As New MailClass
                cust._Mailto = obj.Rows(i).Cells("CustEmail").Value.ToString()


                ret.Add(cust)

            End If
        Next

        Dim appendCust As New MailClass

        appendCust._Mailto = obj.CurrentRow.Cells("CustEmail").Value.ToString

        ret.Add(appendCust)

        Return ret

    End Function

    ''' <summary>
    ''' 已經選取的清單
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCheckedMobileLst(ByRef obj As DataGridView) As Collection(Of String)


        Dim i As Integer

        Dim ret As New Collection(Of String)

        For i = obj.Rows.Count - 1 To 0 Step i - 1

            If obj.Rows(i).Cells(0).Value <> Nothing And CType(obj.Rows(i).Cells(0).Value, Boolean) Then

                If Tool.IsValidMobile(obj.Rows(i).Cells("CustMobile").Value.ToString()) Then
                    ret.Add(obj.Rows(i).Cells("CustMobile").Value.ToString())

                End If

            End If
        Next

        If Tool.IsValidMobile(obj.CurrentRow.Cells("CustMobile").Value.ToString) Then
            ret.Add(obj.CurrentRow.Cells("CustMobile").Value.ToString)

        End If

        Return ret

    End Function

    ''' <summary>
    ''' 刪除準備匯出的客戶名單 not use 20110926
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_DeleteCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        '-------------------取得選取的值-----------------------
        'Dim i As Integer


        'For i = DataGridView3.Rows.Count - 1 To 0 Step i - 1
        '    If DataGridView3.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView3.Rows(i).Cells(0).Value, Boolean) Then

        '        Me.DataGridView3.Rows.RemoveAt(i)

        '    End If
        'Next

        'Me.DataGridView3.Rows.RemoveAt(Me.DataGridView3.CurrentRow.Index)


        '-------------------取得選取的值----------------------

    End Sub



    ''' <summary>
    ''' 寄簡訊 2011/10/12 not use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub ToolStripButton1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim cust As New EDM.Holmes.Cust.Cust

    '    Dim ret As Collection(Of String) = cust.getCheckedList(Me.DataGridView1, 0, "CustMobile")

    '    Dim sb As New StringBuilder

    '    For Each mobile As String In ret
    '        sb.Append(IIf(mobile <> "", mobile & ",", ""))
    '    Next


    '    My.Forms.SMS._CustMobileLst = ret
    '    My.Forms.SMS._SelectedPrjName = sb.ToString()
    '    My.Forms.SMS.Show()


    'End Sub

    ''' <summary>
    ''' 關閉視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Me.Close()


    End Sub

  


    ''' <summary>
    ''' 使移動所選取的區塊的checkbox都呈現勾選
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged

        '防止LOAD時立即載入引發錯誤
        If Me.DataGridView1.SelectedRows.Count > 1 Then

            '取得所選取的列區塊中特定列的索引
            Dim i As Integer = Me.DataGridView1.SelectedRows.Item(1).Index
            'set the checkbox from false to true
            Me.DataGridView1.Rows(i).Cells(0).Value = True

            '防止最後一列都呈現未點選狀態
            Me.DataGridView1.Rows(i + 1).Cells(0).Value = True


        End If


    End Sub



    ''' <summary>
    ''' 勾選曾有消費紀錄之客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbo_HasOrder_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_HasOrder.CheckedChanged

        If CType(sender, CheckBox).Checked = True Then
            Me.FlowLayoutPanel1.Enabled = True
            Me.cbo_NoOrder.Checked = False
            'Me.FlowLayoutPanel3.Enabled = False
        Else
            Me.FlowLayoutPanel1.Enabled = False

            'Me.FlowLayoutPanel1.Enabled = False
        End If


    End Sub

    ''' <summary>
    ''' 勾選無消費紀錄之客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbo_NoOrder_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_NoOrder.CheckedChanged


        If CType(sender, CheckBox).Checked = True Then
            Me.FlowLayoutPanel3.Enabled = True
            Me.cbo_HasOrder.Checked = False
            ' Me.FlowLayoutPanel1.Enabled = False
        Else
            Me.FlowLayoutPanel3.Enabled = False
            ' Me.FlowLayoutPanel3.Enabled = False
        End If

    End Sub


    ''' <summary>
    ''' 進階設定->新增專案
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



        'Me.DialogResult = System.Windows.Forms.DialogResult.OK

        '2011-09-09-----------------------------------------

        'Dim projectName As String = Me.txt_PrjName.Text.Trim

        'Dim parentId As String = Me._ParentId



        'Dim cust As New EDM.Holmes.Cust.Cust



        'Dim ret As Collection(Of String) = Nothing


        'If projectName = "" Then
        '    MsgBox("請輸入專案名稱")
        '    Exit Sub
        'End If




        ''處理沒有選擇任何客戶種類的情況
        'If Me.lbl_Message.Text.Length < DoNotChooseAnyCustType Then
        '    MsgBox("您未選擇任一方式的客戶")
        '    Exit Sub
        'End If

        'Dim cat As String = Me.lbl_Message.Text.Substring(5, 4)



        'Select Case cat
        '    Case "交易金額"
        '        Dim dealAmount As String = Me.num_DealAmount.Text
        '        ret = cust.getCustByDealAmount(dealAmount)
        '        Dim message As String = Me.lbl_DealAmount1.Text & dealAmount & Me.lbl_DealAmount2.Text
        '        If ret.Count > 0 Then
        '            InsertDB(projectName, parentId, ret.Count, message, ret)
        '        Else : MsgBox("0個客戶符合條件")
        '        End If
        '    Case "交易次數"
        '        Dim dealCnt As String = Me.num_DealCnt.Value
        '        Dim message As String = Me.lbl_DealCnt1.Text & dealCnt & Me.lbl_DealCnt2.Text
        '        ret = cust.getCustByDealCnt(dealCnt)
        '        If ret.Count > 0 Then
        '            InsertDB(projectName, parentId, ret.Count, message, ret)
        '        Else : MsgBox("0個客戶符合條件")
        '        End If

        '    Case "交易月份"
        '        Dim month As String = Me.num_Month.Value
        '        Dim message As String = Me.lbl_DealMonth1.Text & month & Me.lbl_DealMonth2.Text
        '        ret = cust.getCustByMonth(month)
        '        If ret.Count > 0 Then
        '            InsertDB(projectName, parentId, ret.Count, message, ret)
        '        Else : MsgBox("0個客戶符合條件")
        '        End If



        'End Select







        'Me.Close()


    End Sub


    ''' <summary>
    ''' 基本設定->關閉按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Me.Close()


    End Sub


    ''' <summary>
    ''' 關閉視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Me.Close()

    End Sub



    ''' <summary>
    ''' 寄信按鈕 2011/10/12 not use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        addProject_do(sender, e)

    End Sub

   

    ''' <summary>
    ''' close AddProject Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseAddPrject_Click(ByVal sender As Object, ByVal e As EventArgs)

        Me.Close()

    End Sub



    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    ''' <summary>
    ''' 新增專案 2011/10/12 not use
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="parentId"></param>
    ''' <param name="ret"></param>
    ''' <remarks></remarks>
    Public Overloads Sub InsertDB(ByVal projectName As String, ByVal parentId As String, ByVal contect As String, ByVal ret As Collection(Of String))


        '新增專案
        Dim NoteId As String = Me.InsertProject(projectName, parentId, contect)


        '新增客戶表
        For Each custId As String In ret

            InsertCustList(NoteId, custId)

        Next


    End Sub


    ''' <summary>
    ''' 2011/10/12 not use
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="parentId"></param>
    ''' <param name="custCnt"></param>
    ''' <param name="contect"></param>
    ''' <param name="ret"></param>
    ''' <remarks></remarks>
    Public Overloads Sub InsertDB(ByVal projectName As String, ByVal parentId As String, ByVal custCnt As Integer, ByVal contect As String, ByVal ret As Collection(Of String))


        '新增專案
        Dim NoteId As String = Me.InsertProject(projectName, parentId, custCnt, contect)


        '新增客戶表
        For Each custId As String In ret

            InsertCustList(NoteId, custId)

        Next

    End Sub

    ''' <summary>
    ''' 新增通訊錄
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="parentId"></param>
    ''' <param name="custCnt"></param>
    ''' <param name="ret"></param>
    ''' <returns>回傳專案NoteId</returns>
    ''' <remarks></remarks>
    Public Overloads Function InsertDB(ByVal projectName As String, ByVal parentId As String, ByVal custCnt As Integer, ByVal ret As Collection(Of String)) As String


        '新增專案
        Dim NoteId As String = Me.InsertProject(projectName, parentId, custCnt)


        '新增客戶表
        For Each custId As String In ret

            InsertCustList(NoteId, custId)

        Next

        Return NoteId

    End Function

    ''' <summary>
    ''' 新增專案,not use
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="parentId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Overloads Function InsertProject(ByVal projectName As String, ByVal parentId As String, ByVal contect As String) As String

        Dim NoteId As String = ""
        Dim cmd As New OleDbCommand
        Dim cmd2 As New OleDbCommand
        Dim cmd3 As New OleDbCommand
        Dim conn As OleDbConnection = HolmesConn.getConnection

        cmd.CommandText = "insert into 專案表(ParentId,ProjectName,Contect)Values(?,?,?)"

        If Not String.IsNullOrEmpty(parentId) Then
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = parentId
        Else
            '最上層的節點都為Null而不是0
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = DBNull.Value
        End If

        cmd.Parameters.Add("@ProjectName", OleDbType.WChar).Value = projectName

        cmd.Parameters.Add("@Contect", OleDbType.WChar).Value = contect

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()
            cmd2.CommandText = "SELECT @@Identity AS NoteId"
            cmd2.Connection = conn
            NoteId = cmd2.ExecuteScalar
            cmd3.CommandText = "UPDATE 專案表 SET CustPrjId = ? WHERE NoteId = ?"
            cmd3.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = CInt(NoteId)
            cmd3.Parameters.Add("@NoteID", OleDbType.Integer).Value = CInt(NoteId)
            cmd3.Connection = conn
            cmd3.ExecuteReader()

        End Using

        Return NoteId


    End Function

    ''' <summary>
    ''' 新增專案
    ''' </summary>
    ''' <param name="projectName"></param>
    ''' <param name="parentId"></param>
    ''' <param name="custCnt"></param>
    ''' <param name="contect"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function InsertProject(ByVal projectName As String, ByVal parentId As String, ByVal custCnt As Integer, ByVal contect As String) As String

        Dim NoteId As String = ""
        Dim cmd As New OleDbCommand
        Dim cmd2 As New OleDbCommand
        Dim cmd3 As New OleDbCommand
        Dim conn As OleDbConnection = HolmesConn.getConnection

        cmd.CommandText = "insert into 專案表(ParentId,ProjectName,Cnt,Contect)Values(?,?,?,?)"

        If Not String.IsNullOrEmpty(parentId) Then
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = parentId
        Else
            '最上層的節點都為Null而不是0
            cmd.Parameters.Add("@ParentId", OleDbType.Integer).Value = DBNull.Value
        End If

        cmd.Parameters.Add("@ProjectName", OleDbType.WChar).Value = projectName
        cmd.Parameters.Add("@Cnt", OleDbType.Integer).Value = custCnt
        cmd.Parameters.Add("@Contect", OleDbType.WChar).Value = contect

        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()
            cmd2.CommandText = "SELECT @@Identity AS NoteId"
            cmd2.Connection = conn
            NoteId = cmd2.ExecuteScalar
            cmd3.CommandText = "UPDATE 專案表 SET CustPrjId = ? WHERE NoteId = ?"
            cmd3.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = CInt(NoteId)
            cmd3.Parameters.Add("@NoteID", OleDbType.Integer).Value = CInt(NoteId)
            cmd3.Connection = conn
            cmd3.ExecuteReader()

        End Using

        Return NoteId


    End Function


    Public Sub InsertCustList(ByVal custPrjId As String, ByVal custId As String)

        Dim cmd As New OleDbCommand

        Dim conn As OleDbConnection = HolmesConn.getConnection

        cmd.CommandText = "insert into 客戶表(CustPrjId,CustId)Values(?,?)"

        cmd.Parameters.Add("@CustPrjId", OleDbType.WChar).Value = custPrjId
        cmd.Parameters.Add("@CustId", OleDbType.WChar).Value = custId
        cmd.Connection = conn

        conn.Open()

        Using conn

            cmd.ExecuteNonQuery()

        End Using



    End Sub




    ''' <summary>
    ''' not use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_CustIdStart_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'MsgBox("客戶編號")

        'Dim obj As TextBox = CType(sender, TextBox)

        'Dim cat As String = Me.FilterCat(obj.Name)


        'Dim val As String = obj.Text.Trim


        'RaiseEvent Cust_Cat_Change(cat, val)



    End Sub

    ''' <summary>
    ''' not use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_CustIdEnd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim obj As TextBox = CType(sender, TextBox)

        'Dim cat As String = Me.FilterCat(obj.Name)


        'Dim val As String = obj.Text.Trim


        'RaiseEvent Cust_Cat_Change(cat, val)

    End Sub

    ''' <summary>
    ''' 匯入的客戶種類變動時引發
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddProject_Cust_Cat_Change(ByVal cat As String, ByVal val As String) Handles Me.Cust_Cat_Change

        'Me.lbl_Message.Text = "您選擇產生" & cat & "/" & val


    End Sub

    ''' <summary>
    ''' 將控制項Id轉變成可識別之中文
    '''
    ''' </summary>
    ''' <param name="cat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FilterCat(ByVal cat As String) As String

        Dim returnVal As String = ""

        Select Case cat
            Case "num_DealAmount"
                returnVal = "交易金額"
            Case "num_DealCnt"
                returnVal = "交易次數"
            Case "num_Month"
                returnVal = "交易月份"
            Case "txt_CustStart"
                returnVal = "客戶編號"
            Case "txt_CustEnd"
                returnVal = "客戶編號"

        End Select

        Return returnVal

    End Function


    ''' <summary>
    ''' close AddProject form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        'Me.DataGridView1.Columns.Remove("check")
        Me.Close()


    End Sub

    ''' <summary>
    ''' clear server data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_ClearTbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearTbl.Click


        Dim msg As MsgBoxResult = MsgBox("確定要刪除資料嗎?", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then

            Dim cust As New EDM.Web.Top50Cust.Top50Cust

            cust.ClearTbl()

        End If

    End Sub

    ''' <summary>
    ''' 匯入客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub ToolStripButton2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click


        '打開特定資料夾,讓使用者直接把文字檔丟進去
        Dim destination As String = String.Format("{0}\{1}", Application.StartupPath, "Holmes\Ds")

        Try
            System.Diagnostics.Process.Start(destination)
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try

    End Sub




End Class

