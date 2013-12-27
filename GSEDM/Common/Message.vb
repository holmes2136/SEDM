Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Text
Imports EDM.Holmes.Tools
Imports System.Data.OleDb
Imports EDM.Holmes.DB
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports EDM.Holmes.VO.User
Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.Configuration
Imports EDM.Holmes.EDMSetting

Public Class Message



    Private message As String

    Private hash As Hashtable

    'save shopname and shopid to supply match 
    Private shopHash As Hashtable = Nothing

    '��T���o�ͧ��ܮ�
    Public Event MessageChg(ByVal content As String)

    '�ǰe���������
    Public Event SendBill_End(ByVal result As String)




    Public Delegate Sub Thread_InitListView_Do(ByVal dt As Object)
    Public Delegate Sub Trd_Dg1Access()

    Private Sub Message_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


        '�w�]�����ѩҦ�������,�����bLoad,�_�h�u�|�X�{�@��
        initSelect(Me.DataGridView1, 0)

    End Sub


    Private Sub Message_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'Form.CheckForIllegalCrossThreadCalls = False

        '��l�Ƥ���ID�M��
        getShopIdLst()

        '���o²�T�O���� 
        Dim dt As DataTable = Me.getAllRecord


        Dim td As New Thread(New ParameterizedThreadStart(AddressOf Init))
        td.IsBackground = True
        td.Start(dt)


  
        DGTool.AddCheckToGridView(Me.DataGridView1)



        '��ܵo�e�O��
        'InitListView(dt)

        '��ܦU�����o�e�ƥزM��
        'Me.InitView()



    End Sub

    ''' <summary>
    ''' ��l��
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub Init(ByVal dt As Object)

        Invoke(New Thread_InitListView_Do(AddressOf InitListView), dt)

        'If Me.DataGridView2.InvokeRequired Then
        '    InitListView(dt)

        'Else
        '    Invoke(New Thread_InitListView_Do(AddressOf InitListView), dt)
        'End If


        'Invoke(New Thread_InitListView_Do(AddressOf InitListView), dt)




    End Sub


    ''' <summary>
    ''' ��l�Ʀ�����M��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitView()

        Dim olddt As DataTable = CType(Me.BindingSource2.DataSource, DataTable)
        'Dim olddt As DataTable = FindMobile(Me.txt_Mobile.Text.Trim)

        '�������ƦC,�HShopId,ShopName distinct
        Dim newDT As DataTable = olddt.DefaultView.ToTable(True, "ShopName", "PublishDay")
        'add columns to second datatable ---------------
        newDT.Columns.Add("ShopId")
        'newDT.Columns.Add("Item")
        'newDT.Columns.Add("price")
        newDT.Columns.Add("Amount")
        'newDT.Columns.Add("UnderTaker")
        'newDT.Columns.Add("Class")
        'newDT.Columns.Add("Contect")
        newDT.Columns.Add("allCnt")
        newDT.Columns.Add("price2")
        newDT.Columns.Add("failCnt")
        '----------------------------
        If newDT.Rows.Count > 0 Then


            'init columns value--------------------------
            For i As Integer = 0 To newDT.Rows.Count - 1
                '�p��C��ShopName�X�{�X���ӭp��²�T�ƶq  '& " and PublishDay" = newDT.Rows(i).Item("PublishDay") & "
                '���󬰤����סH�M�H�o�ɶ��סH�M�o�e���A�����\�o�e�M�w�o�e
                Dim cnt As String = olddt.Compute("Count(ShopName)", String.Format("ShopName='{0}' and PublishDay = '{1}'  and (State = '���\�o�e' or State = '�w�o�e') ", newDT.Rows(i).Item("ShopName"), newDT.Rows(i).Item("PublishDay")))
                newDT.Rows(i).Item("Amount") = cnt
                Dim hash As Hashtable = Me._ShopHash
                newDT.Rows(i).Item("ShopId") = hash.Item(newDT.Rows(i).Item("ShopName"))
                'newDT.Rows(i).Item("UnderTaker") = "�`��"
                'newDT.Rows(i).Item("Class") = "���O"
                'newDT.Rows(i).Item("Item") = "²�T"
                'newDT.Rows(i).Item("price") = 1
                newDT.Rows(i).Item("price2") = 1 * IIf(newDT.Rows(i).Item("Amount") Is DBNull.Value, 0, newDT.Rows(i).Item("Amount"))
                newDT.Rows(i).Item("allCnt") = olddt.Compute("Count(ShopName)", String.Format("ShopName = '{0}' and PublishDay = '{1}' ", newDT.Rows(i).Item("ShopName"), newDT.Rows(i).Item("PublishDay")))
                newDT.Rows(i).Item("failCnt") = newDT.Rows(i).Item("allCnt") - cnt
            Next


            Me.BindingSource1.DataSource = newDT

            Me.BindingNavigator1.BindingSource = Me.BindingSource1

            Me.DataGridView1.DataSource = Me.BindingSource1

            Me.DataGridView1.Columns("check").Width = "5"
            Me.DataGridView1.Columns("ShopId").Visible = True
            Me.DataGridView1.Columns("ShopId").HeaderText = "�����N��"
            Me.DataGridView1.Columns("ShopId").Width = "5"
            Me.DataGridView1.Columns("ShopName").Visible = True
            Me.DataGridView1.Columns("ShopName").HeaderText = "�o�e���"
            Me.DataGridView1.Columns("ShopName").Width = "8"
            'Me.DataGridView1.Columns("Item").Visible = True
            'Me.DataGridView1.Columns("Item").HeaderText = "����"
            'Me.DataGridView1.Columns("price").Visible = True
            'Me.DataGridView1.Columns("price").HeaderText = "����"
            Me.DataGridView1.Columns("Amount").Visible = False
            'Me.DataGridView1.Columns("Amount").HeaderText = "�ƶq"
            'Me.DataGridView1.Columns("price2").Visible = True
            Me.DataGridView1.Columns("price2").HeaderText = "���\����(�w�w�s�W������ƶq)"
            Me.DataGridView1.Columns("price2").Width = "14"
            'Me.DataGridView1.Columns("UnderTaker").Visible = True
            'Me.DataGridView1.Columns("UnderTaker").HeaderText = "�g��H"
            'Me.DataGridView1.Columns("Class").Visible = True
            'Me.DataGridView1.Columns("Class").HeaderText = "���O"
            'Me.DataGridView1.Columns("Contect").Visible = True
            'Me.DataGridView1.Columns("Contect").HeaderText = "�Ƶ�"
            Me.DataGridView1.Columns("PublishDay").Visible = True
            Me.DataGridView1.Columns("PublishDay").HeaderText = "���ͮɶ�"
            Me.DataGridView1.Columns("PublishDay").Width = "10"
            Me.DataGridView1.Columns("allCnt").Visible = True
            Me.DataGridView1.Columns("allCnt").HeaderText = "�o�e����"
            Me.DataGridView1.Columns("allCnt").Width = "10"
            Me.DataGridView1.Columns("failCnt").Visible = True
            Me.DataGridView1.Columns("failCnt").HeaderText = "���ѵ���"
            Me.DataGridView1.Columns("failCnt").Width = "40"


        End If

    End Sub


    ''' <summary>
    ''' ��l�Ƥ���²�T���G�M��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitListView(ByVal source As Object)

        Dim dt As DataTable = CType(source, DataTable)
        'dt.Columns.Add("LineNumber")
        dt.Columns.Add("State")

        For i As Integer = 0 To dt.Rows.Count - 1
            'dt.Rows(i).Item("LineNumber") = i + 1

            '���o²�T���A
            dt.Rows(i).Item("State") = Me.getSmsState(User.SMSLoginid, _
                            User.SMSLoginPwd, _
                            dt.Rows(i).Item("SmsId"))
        Next


        Me.BindingSource2.DataSource = dt

        Me.BindingNavigator2.BindingSource = Me.BindingSource2




        Me.DataGridView2.DataSource = Me.BindingSource2
       


        'Me.DataGridView2.Columns("LineNumber").DisplayIndex = 0
        'Me.DataGridView2.Columns("LineNumber").Visible = False
        'Me.DataGridView2.Columns("LineNumber").HeaderText = "��"
        'Me.DataGridView2.Columns("Serial").Visible = False
        Me.DataGridView2.Columns("ShopName").Visible = True
        Me.DataGridView2.Columns("ShopName").HeaderText = "�����W��"
        Me.DataGridView2.Columns("Mobile").Visible = True
        Me.DataGridView2.Columns("Mobile").HeaderText = "���"
        Me.DataGridView2.Columns("State").Visible = True
        Me.DataGridView2.Columns("State").HeaderText = "�ǰe���G"
        Me.DataGridView2.Columns("SmsId").Visible = False
        'Me.DataGridView2.Columns("PublishDay").HeaderText = "�o�e�ɶ�"
        Me.DataGridView2.Columns("PublishDay").Visible = False

        '---------------��ܦ��\�M���ѵ���------------------------------

        '�o�e�`����  ���󬰤���
        Dim totalCnt As Integer = dt.Compute("Count(ShopName)", String.Format("PublishDay='{0}' ", DateTime.Now.ToString("yyyy/M/d")))

        '���\�o�e���ơ@�@���󬰤��ѨåB���A�����\�o�e�M�w�o�e
        Dim successCnt As Integer = dt.Compute("Count(ShopName)", String.Format("PublishDay='{0}' and State = '���\�o�e' or State = '�w�o�e' ", DateTime.Now.ToString("yyyy/M/d")))

        Me.lbl_SuccessCnt.Text = String.Format("{0} ���\����{1}", DateTime.Now.ToString("yyyy-MM-dd"), successCnt)
        Me.lbl_SuccessCnt.Visible = True


        Me.lbl_FailCnt.Text = String.Format("���ѵ���{0}", totalCnt - successCnt)
        Me.lbl_FailCnt.Visible = True

        '---------------��ܦ��\�M���ѵ���------------------------------

        '��l�Ʀ�����M��
        InitView()


    End Sub

    ''' <summary>
    ''' �j�M�q��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Search_Click_(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Search.Click

        Dim dt As DataTable = FindMobile(Me.txt_Mobile.Text.Trim)

        Dim td As New Thread(New ParameterizedThreadStart(AddressOf InitListView))

        td.Start(dt)


        'Me.InitListView(dt)

    End Sub

    ''' <summary>
    ''' get all mobile list from ²�T�O����
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getAllRecord() As DataTable

        Dim whatMode As String = ConfigurationManager.AppSettings("Mode")

        Dim dt As DataTable = Nothing

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        If whatMode = "test" Then
            cmd.CommandText = "select  top 10 PublishDay,ShopName,Mobile,SmsId  from  ²�T�O����   order by Serial desc"
        Else
            cmd.CommandText = "select   PublishDay,ShopName,Mobile,SmsId from  ²�T�O����   order by Serial desc"
        End If
        cmd.Connection = conn

        conn.Open()

        Using conn

            dt = New DataTable
            dt.Load(cmd.ExecuteReader)

        End Using

        Return dt

    End Function



    ''' <summary>
    ''' Find Specific Mobile from ²�T�O����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindMobile(ByVal mobile As String) As DataTable

        Dim dt As DataTable = Nothing

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "select * from  ²�T�O���� where Mobile like ?"

        cmd.Parameters.Add("@Mobile", OleDbType.VarChar).Value = mobile & "%"

        cmd.Connection = conn

        conn.Open()

        Using conn

            dt = New DataTable
            dt.Load(cmd.ExecuteReader)

        End Using

        Return dt

    End Function


    ''' <summary>
    ''' �ǥ�SMSid���o²�T���w�H�X²�T�����A
    ''' </summary>
    ''' <param name="username">SMS Login id</param>
    ''' <param name="pwd">SMS Login Pwd</param>
    ''' <param name="smsId">�H�X²�T��ҶǦ^�� kmsgid</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getSmsState(ByVal username As String, ByVal pwd As String, ByVal smsId As String) As String

        'test--------------------
        username = "pkyou77286"
        pwd = "l7220836"
        '------------------------

        Dim url As String = String.Format("http://mail2sms.com.tw/msgstatus.php?username={0}&password={1}&kmsgid={2}", username, pwd, smsId)

        Dim myRequest As System.Net.HttpWebRequest = System.Net.WebRequest.Create(url)

        Dim myRs As WebResponse = myRequest.GetResponse

        Dim myStream As System.IO.Stream = myRs.GetResponseStream

        Dim sr As New StreamReader(myStream, System.Text.Encoding.Default)

        Dim sb As String = ""

        Try
            sb = sr.ReadToEnd
        Catch ex As Exception
            Throw New ArgumentException("Error_Code:339_Message_Error")

        End Try

        'Dispose------------
        myRequest = Nothing
        myRs = Nothing
        myStream = Nothing
        sr = Nothing


        Return FriendlySmsState(sb)

    End Function

    ''' <summary>
    ''' friendly the orgin status message
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FriendlySmsState(ByVal state As String) As String

        Dim i As Integer = state.IndexOf("=")

        Dim result As String = state.Substring(i + 1)

        Select Case result
            Case "DELIVERED"
                result = "���\�o�e"
            Case "SUCCESSED"
                result = "���\�o�e"
            Case "EXPIRED"
                result = "�O�ɥ��F"
            Case "DELETED"
                result = "�R��²�T"
            Case "UNDELIVERABLE"
                result = "�L�k�뻼"
            Case "ACCEPTED"
                result = "�o�e����"
            Case "UNKNOWN"
                result = "��������"
            Case "REJECTD"
                result = "�ڦ�²�T"
            Case "SYNTAXERROR"
                result = "�y�k���~"
        End Select

        Return result

    End Function


    ''' <summary>
    ''' Send Estimate Bill
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



    End Sub

    ''' <summary>
    ''' �ǰe���������
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub Message_SendBill_End(ByVal result As String) Handles Me.SendBill_End

        If Not String.IsNullOrEmpty(result) Then

            MsgBox(result)
        Else

            Dim yesOrNo As MsgBoxResult = MsgBox("������H�e����,�z�n�R��²�T�O����?", MsgBoxStyle.YesNo)

            '�i�צC�]��0
            Me.progress.Value = 0


            If yesOrNo = MsgBoxResult.Yes Then
                IsDeleteRecord()
            End If

        End If

    End Sub

    ''' <summary>
    ''' �M��²�T�O����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IsDeleteRecord()

        Dim dt As DataTable = Nothing

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "delete from  ²�T�O����"

        cmd.Connection = conn

        conn.Open()

        Using conn
            cmd.ExecuteNonQuery()
        End Using


    End Sub

   


    ''' <summary>
    ''' 1.���o������D��Serial
    ''' 2.�N�D��Serail �s�P�䥦�s�W�� ��ڪ�
    ''' </summary>
    ''' <param name="ShopId"></param>
    ''' <param name="MonthClassify"></param>
    ''' <param name="publishday"></param>
    ''' <param name="shopname"></param>
    ''' <param name="undertaker"></param>
    ''' <param name="category"></param>
    ''' <remarks></remarks>
    Private Sub AddBill(ByVal ShopId As String, _
    ByVal MonthClassify As String, _
    ByVal publishday As String, _
    ByVal shopname As String, _
    ByVal undertaker As String, _
    ByVal category As String, _
    ByVal item As String, _
    ByVal price As Integer, _
    ByVal price2 As Integer, _
    ByVal amount As Integer, _
    ByVal contect As String)

        Dim bill As New EDM.Web.Bill.Bill

        Dim returnSerial As Integer = bill.AddBill(ShopId, MonthClassify, publishday, shopname, undertaker)

        bill.AddBill2(returnSerial, category, MonthClassify, item, price, price2, amount, contect)

    End Sub





    ''' <summary>
    ''' get all the shopname and shopid list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getShopIdLst()

        Dim hash As Hashtable = Nothing

        Dim ShopInfoWs As New EDM.Web.ShopInfo.ShopInfo

        Dim dt As DataTable = Nothing

        Try
            dt = ShopInfoWs.getAllShopInfo
        Catch ex As Exception
            MsgBox("Error_Code:447_Message_Error_���A�����~")
            Exit Sub
        End Try



        hash = New Hashtable

        For i As Integer = 0 To dt.Rows.Count - 1
            hash.Add(dt.Rows(i).Item("ShopName"), dt.Rows(i).Item("ShopId"))
        Next


        Me._ShopHash = hash


    End Sub


    ''' <summary>
    ''' find specific shopid by use shopname
    ''' </summary>
    ''' <param name="shopname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRelativeShopid(ByVal shopname As String) As String

        Dim hash As Hashtable = Me._ShopHash

        Return hash.Item(shopname)

    End Function




    ''' <summary>
    ''' not use
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Analytics_Do() As DataTable

        Dim dt As DataTable = Nothing

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "select ShopName,Count(ShopName)as Cnt from  ²�T�O����   Group By ShopName"

        cmd.Connection = conn

        conn.Open()

        Using conn

            dt = New DataTable
            dt.Load(cmd.ExecuteReader)

        End Using

        Return dt

    End Function

    ''' <summary>
    ''' select all item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SelectAll.Click

        DGTool.SelectAll(Me.DataGridView1, 0)

    End Sub

    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_UnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UnSelectAll.Click

        DGTool.NonSelectAll(Me.DataGridView1, 0)

    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="dg"></param>
    ''' <param name="chkIndex"></param>
    ''' <remarks></remarks>
    Private Sub initSelect(ByRef dg As DataGridView, ByVal chkIndex As Integer)


        For i As Integer = 0 To dg.Rows.Count - 1
            If dg.Rows(i).Cells("PublishDay").Value = DateTime.Now.ToString("yyyy-MM-dd") Then

                Dim chk As DataGridViewCheckBoxCell = dg.Rows(i).Cells(chkIndex)

                If (chk.Value Is Nothing) Or (chk.Value = "false") Then
                    chk.Value = True
                Else
                    chk.Value = False

                End If
            End If

        Next


    End Sub

    ''' <summary>
    ''' save message
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_SaveMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Dim text As String = Me.txt_Message.Text

        ''���w�ɦW���ɶ�
        'Me.SaveFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") & ".txt"

        'Me.SaveFileDialog1.Filter = "��r�� (*.txt)|*.txt|�Ҧ��ɮ� (*.*)|*.*"

        'Me.SaveFileDialog1.ShowDialog()

        'Dim path As String = Me.SaveFileDialog1.FileName

        'Try
        '    Using sw As New System.IO.StreamWriter(path)

        '        sw.Write(text)

        '    End Using

        'Catch ex As Exception
        '    MsgBox("Error_Code:59_Message_Error" & vbCrLf & ex.Message)
        'End Try

    End Sub


    Private Sub btn_SendBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendBill.Click


    End Sub



    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        'Me.txt_Message.Text = ""

        'Dispose------------------
        message = Nothing

        hash = Nothing

        shopHash = Nothing

        '-------------------------

        Me.Close()

    End Sub



    ''' <summary>
    ''' when message has change
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Private Sub Message_MessageChg(ByVal content As String) Handles Me.MessageChg

        Me._Message = content.Trim

    End Sub





    ''' <summary>
    ''' init controls not use 2011/11/10
    ''' </summary>
    ''' <param name="hash"></param>
    ''' <remarks></remarks>
    Private Sub InitCount(ByVal hash As Hashtable)

        ''add Columns to first datatable -------------------------
        'Dim oldDT As New DataTable()
        'oldDT.Columns.Add("ShopId")
        'oldDT.Columns.Add("ShopName")

        ''add Columns-------------------------

        ''init value----------------------------------

        'If Not hash Is Nothing Then

        '    For Each shop As DictionaryEntry In hash

        '        Dim row As DataRow
        '        row = oldDT.NewRow()
        '        row.Item("ShopId") = Me.getRelativeShopid(shop.Value)
        '        row.Item("ShopName") = shop.Value
        '        oldDT.Rows.InsertAt(row, 0)

        '    Next

        '    '�������ƦC,�HShopId,ShopName distinct
        '    Dim newDT As DataTable = oldDT.DefaultView.ToTable(True, "ShopId", "ShopName")

        '    'add columns to second datatable ---------------
        '    newDT.Columns.Add("Item")
        '    newDT.Columns.Add("price")
        '    newDT.Columns.Add("Amount")
        '    newDT.Columns.Add("price2")
        '    newDT.Columns.Add("UnderTaker")
        '    newDT.Columns.Add("Class")
        '    newDT.Columns.Add("Contect")
        '    '----------------------------

        '    'init columns value--------------------------
        '    For i As Integer = 0 To newDT.Rows.Count - 1
        '        '�p��C��ShopName�X�{�X���ӭp��²�T�ƶq
        '        Dim cnt As String = oldDT.Compute("Count(ShopName)", "ShopName='" & newDT.Rows(i).Item("ShopName") & "'")
        '        newDT.Rows(i).Item("Amount") = cnt
        '        newDT.Rows(i).Item("UnderTaker") = "�`��"
        '        newDT.Rows(i).Item("Class") = "���O"
        '        newDT.Rows(i).Item("Item") = "²�T"
        '        newDT.Rows(i).Item("price") = 1
        '        newDT.Rows(i).Item("price2") = 1 * IIf(newDT.Rows(i).Item("Amount") Is DBNull.Value, 0, newDT.Rows(i).Item("Amount"))
        '    Next
        'init columns value--------------------------


        'display---------------------------------------------------

        Dim newdt As DataTable = Me.Analytics_Do()

        Me.BindingSource1.DataSource = newdt

        Me.DataGridView1.DataSource = Me.BindingSource1

        'Me.DataGridView1.Columns("ShopId").Visible = True
        Me.DataGridView1.Columns("ShopName").Visible = True
        Me.DataGridView1.Columns("ShopName").HeaderText = "�����W��"
        'Me.DataGridView1.Columns("item").Visible = True
        'Me.DataGridView1.Columns("item").HeaderText = "�~��"
        'Me.DataGridView1.Columns("price").Visible = True
        'Me.DataGridView1.Columns("price").HeaderText = "����"
        'Me.DataGridView1.Columns("Amount").Visible = True
        'Me.DataGridView1.Columns("Amount").HeaderText = "�ƶq"
        'Me.DataGridView1.Columns("price2").HeaderText = "�`�M"
        'Me.DataGridView1.Columns("price2").Visible = True
        'Me.DataGridView1.Columns("Class").HeaderText = "���O"
        'Me.DataGridView1.Columns("Class").Visible = True
        'Me.DataGridView1.Columns("UnderTaker").HeaderText = "�g��H"
        'Me.DataGridView1.Columns("UnderTaker").Visible = True
        'Me.DataGridView1.Columns("Contect").Visible = True
        'Me.DataGridView1.Columns("Contect").HeaderText = "�Ƶ�"
        'Me.DataGridView1.Columns("Cnt").Visible = True
        'Me.DataGridView1.Columns("Cnt").HeaderText = "�ƶq"
        'display---------------------------------------------------

        'End If

    End Sub


    ''' <summary>
    ''' �����u�@�Უ�ͪ��T���榡 not use 2011/11/9
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CustomMsg(ByVal msg As String) As Hashtable

        '�T����/097056432/�ǰe���\�A�Ǹ����G33865190
        '�T����/097056432/-5:B Number�H�ϳW�h ������ �������~

        Dim hash As New Hashtable

        Dim shopname As String = ""

        Dim key As String = ""

        Dim myRegex As New Regex("^\w{3}\/[0-9]*\/�ǰe���\", RegexOptions.Multiline)

        'Dim msg2 As String() = msg.Split(vbCrLf)

        Dim msg2 As String() = Split(msg, vbCrLf)

        For Each msg3 As String In msg2

            Try
                If Not String.IsNullOrEmpty(msg3) Then

                    '�P�_���L�ŦX�ۭq�榡,�H�������|��Exception�����~�T��
                    '�u�p�J���\����
                    If myRegex.IsMatch(msg3) Then

                        msg3 = msg3.Trim

                        Dim index2 As Integer = msg3.IndexOf("/")

                        '��X�ۭq�榡�������W��
                        If Not index2 = -1 Then
                            shopname = msg3.Substring(0, index2)
                        End If

                        'find sms id
                        Dim index As Integer = msg3.IndexOf("�G")

                        If Not index = -1 Then

                            key = msg3.Substring(index + 1)

                        End If

                        If hash.ContainsKey(key) Then

                        Else
                            hash.Add(key, shopname)
                        End If

                    End If
                End If

            Catch ex As Exception

                MsgBox("Error_Code:420_SMS_Error" & vbCrLf & ex.Message)

            End Try
        Next

        '2011/11/7
        'Dim hash As New Hashtable

        'Dim myRegex As New Regex("^\w{3}\/[0-9]*\/�ǰe���\", RegexOptions.Multiline)

        'Dim msg2 As String() = msg.Split(vbCrLf)

        'For Each msg3 As String In msg2

        '    '�P�_���L�ŦX�ǰe���\���榡,�H�������|��Exception�����~�T��
        '    '�u�p�J���\����
        '    If myRegex.IsMatch(msg3) Then
        '        '����Ĥ@�C���|���ͦh�l�Ů�
        '        msg3 = msg3.Trim

        '        Dim index As Integer = msg3.IndexOf("/")

        '        '��X�T���̪������W��
        '        Dim shopname As String = msg3.Substring(0, index)

        '        '�Y�w�g���o�����٦s�b,�h���X�Ȩå[1,�Y�L,�h�s�W�����W�٪� key
        '        If hash.ContainsKey(shopname) Then
        '            Dim i As Integer = hash.Item(shopname)
        '            i = i + 1
        '            hash.Item(shopname) = i
        '        Else
        '            hash.Add(shopname, 1)

        '        End If

        '    End If
        'Next

        Return hash

    End Function



    ''' <summary>
    ''' �N�T����z��,�N�����W�٥H�ΦU�������I�ƥ[�Jhashtable not use 2011/11/10
    ''' </summary>
    ''' <param name="msg">Sms�ǰe���Უ�ͪ��T��</param>
    ''' <remarks></remarks>
    Private Sub Refreshdata(ByVal msg As String)

        'If String.IsNullOrEmpty(msg) Then
        '    MsgBox("�S��������!")
        '    Exit Sub
        'End If

        'If hash Is Nothing Then
        '    MsgBox("�S��������!")
        '    Exit Sub
        'End If


        'Test Version---------------------------------------------------

        Dim hash2 As New Hashtable

        Dim myRegex As New Regex("^\w{3}\/[0-9]*\/�ǰe���\", RegexOptions.Multiline)

        Dim shopname As String = ""

        Dim key As String = ""

        Dim msg2 As String() = msg.Split(vbCrLf)

        'Dim msg2 As String() = Split(msg4.ToString, vbCrLf)


        For Each msg3 As String In msg2

            '�P�_���L�ŦX�ۭq�榡,�H�������|��Exception�����~�T��
            '�u�p�J���\����
            If myRegex.IsMatch(msg3) Then

                msg3 = msg3.Trim

                Dim index2 As Integer = msg3.IndexOf("/")

                '��X�ۭq�榡�������W��
                shopname = msg3.Substring(0, index2)

                If Not index2 = -1 Then
                    shopname = msg3.Substring(0, index2)
                End If

                'find sms id
                Dim index As Integer = msg3.IndexOf("�G")

                If Not index = -1 Then

                    key = msg3.Substring(index + 1)

                End If


                If hash2.ContainsKey(key) Then

                Else
                    hash2.Add(key, shopname)
                End If

            End If
        Next

        'Test Version---------------------------------------------------

        InitCount(hash)


    End Sub




    ''' <summary>
    ''' export excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ExportExl.Click

        Dim dt As DataTable = BindingSource1.DataSource

        Dim savepath As String = ""

        SaveFileDialog1.Filter = "EXCEL�� (*.xl)|*.xls"

        SaveFileDialog1.FilterIndex = 1

        SaveFileDialog1.RestoreDirectory = True

        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            savepath = SaveFileDialog1.FileName

            ExportExcel(dt, savepath, "����")

        End If

        'DT: Data Table 
        'FullFileName: �ץXExcel ��������| Ex: C:\123.xls 
        'TableName: �ץXExcel ��Sheet Name 
    End Sub


    ''' <summary>
    ''' �ץX�Ҧ�²�T�ǿ鵲�G
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_EptAllState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EptAllState.Click

        Dim dt As DataTable = BindingSource2.DataSource

        Dim savepath As String = ""

        SaveFileDialog1.Filter = "EXCEL�� (*.xl)|*.xls"

        SaveFileDialog1.FilterIndex = 1

        SaveFileDialog1.RestoreDirectory = True

        'SaveFileDialog1.FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm")

        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            savepath = SaveFileDialog1.FileName

            ExportExcel(dt, savepath, "����")

        End If

    End Sub


    ''' <summary>
    ''' �ץXExcel
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="FullFileName"></param>
    ''' <param name="TableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExportExcel(ByVal DT As DataTable, ByVal FullFileName As String, ByVal TableName As String) As Boolean
        Dim connection As New Data.OleDb.OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0""", FullFileName))
        Try
            '�إ�Excel Connection 
            connection.Open()
            '����Table 
            Dim strCreate As String = ""

            For i As Int32 = 0 To DT.Columns.Count - 1
                strCreate += String.Format(",{0} Text(255)", DT.Columns(i).ColumnName)
            Next
            strCreate = String.Format("Create Table [{0}] ({1})", TableName, strCreate.Substring(1))
            Dim command As New Data.OleDb.OleDbCommand(strCreate, connection)
            command.ExecuteNonQuery()

            'Ū��Excel Table (Sheet) 
            Dim queryString As String = String.Format("Select * From [{0}]", TableName)
            '�إ�Excel �t���� 
            Dim adapter As New Data.OleDb.OleDbDataAdapter()
            '�ŧi�@��Dataset  
            Dim ds As New DataSet
            adapter.SelectCommand = New Data.OleDb.OleDbCommand(queryString, connection)
            Dim builder As Data.OleDb.OleDbCommandBuilder = New Data.OleDb.OleDbCommandBuilder(adapter)
            '���ͷs�W�y�k 
            adapter.Fill(ds, TableName)
            For i As Int32 = 0 To DT.Rows.Count - 1
                Dim dr As DataRow = ds.Tables(TableName).NewRow
                For j As Int32 = 0 To DT.Columns.Count - 1
                    dr.Item(DT.Columns(j).ColumnName) = DT.Rows(i).Item(j)
                Next
                ds.Tables(TableName).Rows.Add(dr)
            Next
            builder.GetInsertCommand(True)
            '����s�W�y�k 
            adapter.Update(ds, TableName)
            Return True
        Catch ex As Exception
            '�Y�X���~!! 
            Throw ex
        Finally
            '�����s�u 
            connection.Close()
        End Try
    End Function


    ''' <summary>
    ''' clear all  data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        Dim msg As MsgBoxResult = MsgBox("�T�w�n�M���Ҧ���ƶ�", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then

            IsDeleteRecord()

        End If



    End Sub



    Public Property _Message()
        Get
            Return Me.message
        End Get
        Set(ByVal value)
            Me.message = value
            'RaiseEvent MessageChg(Me.message)
        End Set
    End Property


    Public Property _Hash()
        Get
            Return Me.hash
        End Get
        Set(ByVal value)
            Me.hash = value

        End Set
    End Property


    Public Property _ShopHash()
        Get
            Return Me.shopHash
        End Get
        Set(ByVal value)
            Me.shopHash = value
        End Set
    End Property


    ''' <summary>
    ''' send bill
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendBill.Click


        '�i�ת�Hdatagridview �����Ƭ��̤j��
        Me.progress.Maximum = Me.DataGridView1.Rows.Count

        '-------------------���o�������-----------------------

        '2011/12/15  �N�J�I����Ĥ@�C,�o�˴N���|���û��ֳ̫�@�C����
        DataGridView1.CurrentCell = Nothing

        '���o�ҿ������ñ
        Dim result As New StringBuilder
        Dim i As Integer
        For i = DataGridView1.Rows.Count - 1 To 0 Step i - 1
            If DataGridView1.Rows(i).Cells(0).Value <> Nothing And CType(DataGridView1.Rows(i).Cells(0).Value, Boolean) Then


                'when add bill has error,its can not be disturb,it would be continue to add bill
                Try
                    AddBill(DataGridView1.Rows(i).Cells("ShopId").Value.ToString(), _
                          Tool.getMonthClassify, _
                          Tool.getPublishday, _
                          DataGridView1.Rows(i).Cells("shopname").Value.ToString(), _
                          "�`��", _
                          "���O", _
                          "²�T", _
                          "1", _
                          DataGridView1.Rows(i).Cells("Price2").Value.ToString(), _
                          DataGridView1.Rows(i).Cells("Amount").Value.ToString(), _
                          "")
                    Me.progress.Increment(1)
                Catch ex As Exception
                    result.Append("��" & i & "���o�Ϳ��~," & "�����W��:" & Me.DataGridView1.Rows(i).Cells("shopname").ToString)
                    result.Append("Error_Code:239_Message_Error" & vbCrLf & ex.Message)

                End Try
            End If
        Next


        'when finish Send Estimate Bill
        RaiseEvent SendBill_End(result.ToString)
    End Sub


  


End Class
