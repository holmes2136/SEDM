Imports KOTSMSAPI
Imports System.Text.RegularExpressions
Imports EDM.Holmes.Cust
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.Text
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Tools
Imports EDM.Holmes.VO.Work
Imports EDM.Holmes.Mail
Imports EDM.Holmes.Mail.SendType
Imports EDM.Holmes.VO.Sample
Imports System.Configuration
Imports EDM.Holmes.Sms

''' <summary>
''' Goverment SMS
''' </summary>
''' <remarks></remarks>
Public Class SMS

    '��ܪ��d���W��
    Private selectedSampName As String
    '��ܪ��d��ID
    Private selectedSampId As String
    '��ܪ��d�����e
    Private selectedSampContent As String
    '��ܪ��M�צW��
    Private selectedPrjName As String
    '��ܪ��Ȥ�ƥ�
    Private CustCnt As Integer

    '�x�s����M��
    'Private custMobileLst As Collection(Of String)
    Private custMobileLst As Collection

    '�x�s���{����
    Public workHistory As New WorkHistory

    '�x�sSQL SERVER��Serial record
    Private ReturnSerial As Integer

    'if lbl_PrjName value changed
    Public Event PrjName_Chg(ByVal prjName As String)

    Public Event CustCnt_Chg(ByVal cnt As Integer)

    '�x�s�`���G
    Dim sb As StringBuilder

   

    Private Sub SMS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.lbl_PrjName.Text = Me._SelectedPrjName

        Me.lbl_SampName.Text = Me._SelectedSampName


    End Sub

    Private Sub SMS_CustCnt_Chg(ByVal cnt As Integer) Handles Me.CustCnt_Chg

        'Me.lbl_CustCnt.Visible = True

        'Me.lbl_CustCnt.Text = String.Format("({0})�W�Ȥ�", cnt)

        Dim msg As MsgBoxResult = MsgBox(String.Format("�o�e²�T�M��@��{0}�W�Ȥ�,���~���J²�T���e", cnt))

        

    End Sub


    ''' <summary>
    ''' �Ұʵo��²�T
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


        If Me._CustMobileLst.Count > 0 Then

            Me.SendSMS()



            '�x�s�ܱH��ƥ�--------------------------------------
            Dim bp As New EDM.Holmes.Mail.MailBp(Me.lbl_PrjName.Text.Trim, Me.txt_Content.Text.Trim, "")

            bp.SaveToSms()
            '----------------------------------------------------


        Else
            MsgBox("�S������q�T�W��!")
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' Send
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click

        Dim msg As MsgBoxResult = MsgBox("�T�w�n�H�o²�T��?", MsgBoxStyle.YesNo)

        If msg = MsgBoxResult.Yes Then

            '�O�_�s�J�Ƶ{�Ϊ̽d��
            IsSaveToWorkArrangeOrSamp()

            '���\�^���i��
            BackgroundWorker1.WorkerReportsProgress = True
            '���\����
            BackgroundWorker1.WorkerSupportsCancellation = True

            '�ҰʭI���@�~
            If Me.BackgroundWorker1.IsBusy Then
                MsgBox("�u�@�w�b�i�椤.....")
                Exit Sub
            Else


                Dim ret As Collection = Me._CustMobileLst

               
                If ret.Count > 0 Then

                    Me.progress.Maximum = ret.Count - 1

                End If

                BackgroundWorker1.RunWorkerAsync()

            End If

        End If




    End Sub


    ''' <summary>
    ''' �H�X²�T 2011/11/06 
    ''' </summary>

    Public Sub SendSMS()

       
        'add evt handler if application happend to close
        'AddHandler Application.ApplicationExit, AddressOf AppExit


        '�}�l�ɶ�-----------------------------
        workHistory._StartTime = DateTime.Now
        workHistory._PrjName = DateTime.Now & "�H�oSMS"
        workHistory._BpType = SendTypeFactory.SendSms
        '-------------------------------------


        '�x�s���G
        sb = New StringBuilder



        '�p��H�X�`�� not use
        Dim cnt As Integer


        '---------------���oSMS�b�K-------------------
        Dim y As New User

        y = y.getUserSMS(User.LoginId, User.LoginPwd)

        Dim x As New KOT_SMSAPI
        x.username = y._SMSLoginId
        x.password = y._SMSLoginPwd

        If String.IsNullOrEmpty(x.username) Or String.IsNullOrEmpty(x.password) Then
            MsgBox("�г]�w²�T�b���K�X")
            Exit Sub
        End If

        '---------------���oSMS�b�K-------------------

        'WorkHistory-----------------------------
        '�w�w�ǰe����()
        workHistory._PlanTotalCnt = Me._CustMobileLst.count

        '---------------�ǰe��ƨ�SQL Server-------------------------------------
        Dim smsWs As New EDM.SmsWs.SMS
        ReturnSerial = smsWs.InsertSMS("sp", _
        "�`��", _
        workHistory._PrjName, _
        workHistory._StartTime, _
        workHistory._PlanTotalCnt)

        '------------------------------------------------------------------------
        ' Judge which instance in the collection , govermentsms or shopsms?
        Dim ret As Collection = Me._CustMobileLst

        Dim sms As MySMS = Nothing

        If ret.Count > 0 Then
            sms = MySMS.SmsFactory(ret.Item(1))
        End If



        'Try
        sms.Send(ret, Me.txt_Content.Text, sb, x, workHistory, cnt, Me.BackgroundWorker1)
        'Catch ex As Exception
        '���i�H������귽�ʧ@,�_�h�|�v�T�챵�U�Ӫ��H�o�ʧ@
        'sb = Nothing
        'x = Nothing
        'workHistory = Nothing
        'smsWs = Nothing
        'End Try


    End Sub


    ''' <summary>
    ''' �o�e²�T������
    ''' ���קK���_,�ϥ�try catch �ӧP�_,�åB�g�J�T��,�Ӥ���msgbox���_
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        '�x�s�����ɶ�
        workHistory._EndTime = DateTime.Now


        '�x�s�u�@���{---------------------------------------------------
        Try
            workHistory.Save()
            'workHistory = Nothing  ' fuck you
        Catch ex As Exception
            sb.Append("Error_Code:281_SMS_Error" & vbCrLf & ex.Message)

        End Try
        '�x�s�u�@���{---------------------------------------------------



        'Send data to Sql server---------------------------------------
        Try
            Dim smsWs As New EDM.SmsWs.SMS

            smsWs.UptSMS(Me.ReturnSerial, workHistory._EndTime, workHistory._SuccessCnt, workHistory._FailCnt)
            smsWs = Nothing
        Catch ex As Exception
            sb.Append("Error_Code:253_SMS_Error" & vbCrLf & ex.Message)

        End Try
        'Send data to Sql server---------------------------------------


        'test 
        'Dim test As String = sb.ToString

        '-------�Ȼs�T��-----------------------------------------------------------
        'ex:
        '0919xxxxxx  ���\
        '0919xxxxxx  ����
        '�`����:
        '���\����:
        '���ѵ���:
        '---------------------------------------------------------------------------
        '�w�] Message Form �� message and hash field
        'My.Forms.Message.txt_Message.Text = sb.ToString & vbCrLf & "�`����:" & _
        'workHistory._SuccessCnt + workHistory._FailCnt & vbCrLf & _
        '"���\����" & workHistory._SuccessCnt & _
        'vbCrLf & "���ѵ���:" & workHistory._FailCnt & _
        'vbCrLf
        '2011/11/10
        'My.Forms.Message.txt_Message.Text = "�`����:" & _
        'workHistory._SuccessCnt + workHistory._FailCnt & vbCrLf & _
        '"���\����" & workHistory._SuccessCnt & _
        'vbCrLf & "���ѵ���:" & workHistory._FailCnt & _
        'vbCrLf
        '---------------------------------------------------------------------------
        '�ǭȵ�Message Form�ǳư�������έp
        'My.Forms.Message._Hash = CustomMsg(sb.ToString)

        'My.Forms.Message._Message = sb.ToString
        'My.Forms.Message.txt_Message.Text = sb.ToString

        MsgBox("�u�@�w����")

        Me.progress.Value = 0


        My.Forms.Message.Show()


        'Me._CustMobileLst = Nothing

        'workHistory = Nothing



    End Sub

    ''' <summary>
    ''' �����u�@�Უ�ͪ��T���榡
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
    ''' when form closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SMS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Dim msg As MsgBoxResult = Nothing

        '�ҰʭI���@�~
        If Me.BackgroundWorker1.IsBusy Then
            msg = MsgBox("�u�@�i�椤,�z�T�w�n������?", MsgBoxStyle.YesNoCancel)

            If msg = MsgBoxResult.Yes Then

                Me.BackgroundWorker1.CancelAsync()

                Me.Close()

            End If

        End If

    End Sub


    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged


        ''�I���@�~�i�צ^���{��()

        'txt_Message.Text = e.UserState                             '�e�{�ǰe�T��   
        'Label9.Text = "�w�ǰe " & e.ProgressPercentage & "��"          '�e�{�ǰe����   

        '���o�H�X�`��
        'workHistory._TotalCnt = e.ProgressPercentage

        Me.progress.Increment(e.ProgressPercentage)

    End Sub



    '����@�~
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click


        If Me.BackgroundWorker1.CancellationPending Then
            Me.BackgroundWorker1.CancelAsync()
            Me.Close()
        Else

            Me.Close()

        End If


    End Sub


    '�p��Ʀr�h��
    Private Sub txt_Content_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Content.TextChanged


        Me.lbl_Strlength.Text = "�r��" & txt_Content.Text.Length.ToString


    End Sub


    ''' <summary>
    ''' ��ܬO�_�u�@�O�_�s�J�Ƶ{�Ϊ̽d��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IsSaveToWorkArrangeOrSamp()

        'Dim btn As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddSamp

        'Dim btn1 As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddWork

        'AddHandler btn.Click, AddressOf AddSample

        'AddHandler btn1.Click, AddressOf AddWork

        'My.Forms.IsSaveToWorkOrSamp.Show()


    End Sub

    '�s�W�d��
    Private Sub AddSample(ByVal sender As Object, ByVal e As EventArgs)

        Dim samp As New Saple
        samp._SampName = DateTime.Now.ToString
        samp._SampContent = Me.txt_Content.Text.Trim
        samp._SampCat = Saple.SmsSamp

        Try
            samp.AddSamp(samp)
        Catch ex As Exception
            MsgBox("Error_Code:460_SMS_Error" & vbCrLf & ex.Message)
            Exit Sub
        End Try

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub


    '�s�W�Ƶ{
    Private Sub AddWork(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.AddWork.Show()

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub


    ''' <summary>
    '''��ܳq�T��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ShowPrj.Click

        My.Forms.AddProject.TopMost = True
        My.Forms.AddProject.Show()
        My.Forms.AddProject.StartPosition = FormStartPosition.CenterScreen

        'My.Forms.AddProject.btn_ImportCust.Visible = False
        '²�T���s
        'My.Forms.AddProject.ToolStripButton1.Visible = False

        'remove button named close
        My.Forms.AddProject.ToolStrip2.Items.RemoveAt(2)

        'add Button named �H�X²�T-----------------------------------------------------
        Dim control As New ControlTool

        Dim btn As ToolStripButton = control.AddToolsBtn("�H�X²�T", ControlTool.ImgButton, "Images\Email.png")


        My.Forms.AddProject.ToolStrip2.Items.Add(btn)

        AddHandler btn.Click, AddressOf AddPrj_Click
        '-----------------------------------------------------

        'add Button named close ------------------------------
        Dim btn2 As ToolStripButton = control.AddToolsBtn("����", ControlTool.ImgButton, "Images\Delete.png")

        My.Forms.AddProject.ToolStrip2.Items.Add(btn2)

        AddHandler btn2.Click, AddressOf CloseAddPrject_Click
        '-----------------------------------------------------

    End Sub

    ''' <summary>
    ''' close AddProject Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseAddPrject_Click(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.AddProject.Close()

    End Sub

 
    ''' <summary>
    ''' �[�J�q�T����ثe���H�o²�T�W�� 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddPrj_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Reset-------------------------
        'Me._CustMobileLst = Nothing

        'Me._CustCnt = 0
        'Reset-------------------------



        '���o��������Ȥ�
        Dim cust As OrginCust = CustFactory.getCustTpe(CustFactory.goverment)

        '�ھګȤ��������o���²�T����
        Dim sms As MySMS = MySMS.SmsFactory(cust)

        'Dim ret As Collection(Of GovermentCust) = cust.getCheckedMobileLst(My.Forms.AddProject.DataGridView1)

        'Dim sms As MySMS = MySMS.SmsFactory(cust)

        Dim ret As Collection = sms.getMobileLst(My.Forms.AddProject.DataGridView1)


        Dim receiver As String = sms.mobileLstToString


        '�֥[�q�T�M��,2012/2/6 �令���֥[
        Me._SelectedPrjName = receiver

        Me._CustMobileLst = ret

        Me._CustCnt = ret.Count

        'RaiseEvent CustCnt_Chg(ret.Count)

        My.Forms.AddProject.Close()

    End Sub

  
    ''' <summary>
    ''' ���U��ܱM�ת��᪺�ʧ@,�]�wCust Mobile List
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub getCustLst(ByVal sender As Object, ByVal e As EventArgs)

        Dim cust As EDM.Holmes.Cust.Cust

        Dim ret As Collection(Of String) = Nothing

        If Not String.IsNullOrEmpty(Me.lbl_PrjId.Text) Then

            cust = New EDM.Holmes.Cust.Cust
            ret = cust.getMobilelst(Me.lbl_PrjId.Text)


            Me._CustMobileLst = ret


        End If

    End Sub

    ''' <summary>
    ''' ��ܽd��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ChooseSamp.Click

        'My.Forms.SampleMaintain.Show()
        My.Forms.SMSSamp.Show()

    End Sub


    ''' <summary>
    ''' ��ܩҦ��Ȥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AllCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AllCust.Click

        Dim ret As Collection(Of String) = Nothing


        Dim sb As New StringBuilder

        Dim cust As OrginCust = CustFactory.getCustTpe(CustFactory.goverment)

        Try
            ret = cust.getCustMobile

        Catch ex As Exception

            MsgBox("Error_Code:463_SMS_Error")
            Exit Sub

        End Try

        For Each mobile As String In ret

            sb.Append(IIf(mobile <> "", mobile & ",", ""))
        Next

        Me._CustCnt = ret.Count

        'RaiseEvent CustCnt_Chg(ret.Count)

        'Me.lbl_PrjName.Text = String.Format("({0}):{1}", ret.Count, sb.ToString)

        Me.lbl_PrjName.Text = sb.ToString

        'Me._SelectedPrjName = sb.ToString

    End Sub

    ''' <summary>
    ''' when contact list has changed
    ''' </summary>
    ''' <param name="prjName"></param>
    ''' <remarks></remarks>
    Private Sub SMS_PrjName_Chg(ByVal prjName As String) Handles Me.PrjName_Chg

        'Me.lbl_PrjName.Text = String.Format("({0}):{1}", custCnt, prjName)

        Me.lbl_PrjName.Text = prjName

    End Sub


    ''' <summary>
    ''' clear contact list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_ClearContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearContact.Click

        Me.lbl_PrjName.Text = ""


        'Me.custMobileLst.Clear()

        Me._SelectedPrjName = ""

        RaiseEvent CustCnt_Chg(0)

    End Sub


    Public Property _SelectedSampName()
        Get
            Return Me.selectedSampName
        End Get
        Set(ByVal value)
            Me.selectedSampName = value
        End Set
    End Property

    Public Property _SelectedSampId()
        Get
            Return Me.selectedSampId
        End Get
        Set(ByVal value)
            Me.selectedSampId = value
        End Set
    End Property

    Public Property _SelectedSampContent()
        Get
            Return Me.selectedSampContent
        End Get
        Set(ByVal value)
            Me.selectedSampContent = value
        End Set
    End Property

    Public Property _SelectedPrjName()
        Get
            Return Me.selectedPrjName
        End Get
        Set(ByVal value)
            Me.selectedPrjName = value
            RaiseEvent PrjName_Chg(Me.selectedPrjName)
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

    Public Property _CustCnt()
        Get
            Return Me.CustCnt
        End Get
        Set(ByVal value)
            'Dim oldvalue As Integer = 0
            'oldvalue = Me.CustCnt
            Me.CustCnt = value
            RaiseEvent CustCnt_Chg(value)
        End Set
    End Property


  

    ''' <summary>
    ''' �[�J�Ȥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        Try

            Me.AddPrj_Click(sender, e)

        Catch ex As Exception

            '�קK�o�͵LŪ����Ȥ��ɮת����~
            MsgBox(ex.Message)

            Exit Sub

        End Try

    End Sub


    ''' <summary>
    ''' �d�߾��v�O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        My.Forms.Message.StartPosition = FormStartPosition.CenterScreen

        My.Forms.Message.ShowDialog()

    End Sub
End Class