Imports System.Net.Mail
Imports EDM.Holmes.Mail
Imports System.Data.OleDb
Imports System.Configuration
Imports EDM.Holmes.DB
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports EDM.Holmes.Tools
Imports EDM.Holmes.Mail.MailServerType
Imports EDM.Holmes.VO.User
Imports System.Text
Imports EDM.Holmes.VO.Work
Imports EDM.Holmes.VO.Sample
Imports System.Threading
Imports EDM.Holmes.VO.Collection




Public Class SendMail

    Dim Mmsg As New MailClass

    '�]�w�M��Id
    Private CustListId As String

    '�]�w�H���
    Private Mto As String
    '�]�w�H����D
    Private mailTitle As String
    '�]�w�H�󤺮e
    Private mailContent As String

    '����o�H��Ϥ�Id
    Private selectedSmapleId As String

    '������H��Ϥ��W��
    Private selectedSampleName As String

    '������d��Id
    Private selectedSampId As String

    '������d���W��
    Private selectedSampName As String

    '�ǳƱH�X���Ȥ�M��
    Private SendCustLst As Collection(Of MailClass)

    Public z As Integer = 1

    Public workHistory As New WorkHistory



    Private Sub SendMail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.txt_Receiver.Text = Me._Mailto

        Me.txt_Content.Text = Me.mailContent

        Me.txt_Title.Text = Me.mailTitle

        'Me.txt_Sender.Text = My.Forms.Main._Sender

        Me.txt_Sender.Text = User.mailSender


    End Sub


    ''' <summary>
    ''' �I���u�@���e
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim mail As MailClass = Me.Mmsg

        Dim ret As Collection = Me._SendCustLst

        '���~����
        Dim failCnt As Integer

        '�]�w�}�l�H�H�ɶ�
        'workHistory._StartTime = DateTime.Now


        '�]�w�ǰe�l�󪺸�T------------------------------------------

        '�̷�EMAIL �M��ƶq�ŧi�h�֭�SMTP
        'Dim smtpLst(ret.Count - 1) As MySmtp
        Dim smtpLst(ret.Count - 1) As MySmtp


        'google,hotmail,yahoo...etc...
        Dim smtplist As New SmtpList

        Dim iterator As SmtpIterator = smtplist.getIterator

        '���ʨ�U�@��
        iterator.MoveNext()

        For j As Integer = 0 To smtpLst.Length - 1

            If j Mod 2 = 0 Then
                iterator.MoveNext()
                Dim o As MySmtp = CType(iterator.Current(), MySmtp)
                smtpLst(j) = New MySmtp(o._Smtp, o._Port, o._LoginId, o._LoginPwd, o._Cat, o._IsSSL)
                'Console.Write("smtp:{0},port:{1},Id:{2},Pwd{3},Cat{4},IsSSL{5},from{6}", o._Smtp, o._Port, o._LoginId, o._LoginPwd, o._Cat, o._IsSSL, o._MailAddress & vbCrLf)
            Else
                Dim o As MySmtp = CType(iterator.Current, MySmtp)
                smtpLst(j) = New MySmtp(o._Smtp, o._Port, o._LoginId, o._LoginPwd, o._Cat, o._IsSSL)
                'Console.Write("smtp:{0},port:{1},Id:{2},Pwd{3},Cat{4},IsSSL{5},from{6}", o._Smtp, o._Port, o._LoginId, o._LoginPwd, o._Cat, o._IsSSL, o._MailAddress & vbCrLf)

            End If

            '�Y�S���U�@��,�h���s�`��
            If Not iterator.hasNext Then
                iterator.Reset()
            End If

        Next


        '�l�󤺮e�]�w-------------------------------------------------


        '�]�w���Y-----------------------------------------------------

        'bulk send mail

        Dim sendMsg As String = ""

        Dim N As Integer = 0

        For z = 1 To ret.Count - 1


            'Take a break---------------------
            If z Mod 500 = 0 Then
                Thread.Sleep(1000 * 60 * 20)
            End If
            '---------------------------------


            Dim client As New Net.Mail.SmtpClient(smtpLst(z - 1)._Smtp, smtpLst(z - 1)._Port)
            client.EnableSsl = smtpLst(z - 1)._IsSSL
            client.Credentials = New System.Net.NetworkCredential(smtpLst(z - 1)._LoginId, smtpLst(z - 1)._LoginPwd)

            Dim msg As New Net.Mail.MailMessage

            With msg
                .Priority = Net.Mail.MailPriority.High
                .Headers.Add("Precedence", "bulk")
                .Headers.Add("List-Unsubscribe", smtpLst(z - 1)._MailAddress)
                .From = New System.Net.Mail.MailAddress(smtpLst(z - 1)._MailAddress, mail.MailFrom)
                .Subject = mail.MailSubject
                .SubjectEncoding = System.Text.Encoding.UTF8
                .IsBodyHtml = True
                .Body = mail.MailBody
                .BodyEncoding = System.Text.Encoding.UTF8
            End With

            '----����------------------------------------
            If mail.MailAttachments <> Nothing Then

                Dim Att As New Net.Mail.Attachment(mail.MailAttachments)

                msg.Attachments.Add(Att)

            End If
            '--------------------------------------------

            If Not String.IsNullOrEmpty(ret.Item(z)._Mailto) Then

                Try
                    SyncLock Me

                        msg.To.Add(ret.Item(z)._Mailto)
                        client.Send(msg)
                        N += 1
                        Console.Write(Thread.CurrentThread.Name & " /")
                        Console.Write("��" & z & "��")
                        Console.Write(vbCrLf)

                    End SyncLock

                Catch ex As Exception
                    '�|�X�{datetime���ܿ��~
                    'Console.Write(String.Format("��{0}��,smtp{1},break}", i, smtpLst(i - 1)._Smtp)) 
                    'SyncLock Me

                    '    Console.Write(Thread.CurrentThread.Name & " /")
                    '    Console.Write("��" & i & "��")
                    '    Console.Write(vbCrLf)
                    '    failCnt += 1


                    'i += 1
                    'End SyncLock

                End Try

                'background.ReportProgress(N, sendMsg)

            End If

        Next

        '--------------------------------------------------------------------


        '�Ұʦ^���i�רƥ�A�åH�ǰe���ƩM�u�ǰe�����v�T�������޼ƶǰe��ƥ�{��
        'background.ReportProgress(N, "�ǰe����.....")

        '�]�w���\����
        workHistory._SuccessCnt = N

        '�]�w���ѵ���
        workHistory._FailCnt = failCnt

        'Dispose

        '2011/12/10-----------------------------------------------------------------------------

        ''���~����
        'Dim failCnt As Integer

        ''�]�w�}�l�H�H�ɶ�
        'workHistory._StartTime = DateTime.Now


        ''�]�w�ǰe�l�󪺸�T------------------------------------------
        'Dim mailServer As MailServer = MailServerFactory.createMailServer(User.mailServerType)

        'If mailServer Is Nothing Then
        '    MsgBox("�п�ܥ��@�����H�c")

        '    Exit Sub
        'End If


        'Dim mailClien As New Net.Mail.SmtpClient(mailServer.smtp, mailServer.port) '���wSMTP�D���P�ǿ��

        'Dim x As User = mailServer.getLoginData(User.LoginId, User.LoginPwd)

        'If String.IsNullOrEmpty(x._MailId) Or String.IsNullOrEmpty(x._MailPwd) Then

        '    MsgBox("�b���K�X���ର��")
        '    Exit Sub

        'End If

        'mailClien.EnableSsl = True
        'mailClien.Credentials = New System.Net.NetworkCredential(x._MailId, x._MailPwd)


        ''�l��򥻳]�w-------------------------------------------------



        ''�l�󤺮e�]�w-------------------------------------------------
        'Dim msg As New Net.Mail.MailMessage

        'msg.Priority = MailPriority.High
        ''�]�w���Y-----------------------------------------------------
        ''bulk send mail
        'msg.Headers.Add("Precedence", "bulk")
        ''Unsubscribe
        'msg.Headers.Add("List-Unsubscribe", mailServer.getMailAddress(x._MailId))

        ''�إ�msg���Ū�MailMessage��������ܼ�
        'msg.From = New System.Net.Mail.MailAddress(mailServer.getMailAddress(x._MailId), Me.txt_Sender.Text.Trim)

        ''�]�w�H��̦a�}�P�W�١A�ë��w��MailAddress��From�ݩ�
        'msg.Subject = Me.txt_Title.Text.Trim

        ''�Ѧۭq��MailClass1���O�����X�D���ݩʡA�ë��w��MailAddress��Subject�ݩ�
        'msg.SubjectEncoding = System.Text.Encoding.UTF8

        ''�]�w�D���r�ꪺ�s�X�覡��UTF8

        ''���J�I��uHTML�Ҧ��v�ﶵ
        'msg.IsBodyHtml = True

        ''�N�����˵��Ҧ��]�w��HTML�Ҧ�

        'msg.Body = Mmsg.MailBody
        ''�Ѧ۩w��MailClass1���O�����X�����ݩʡA�ë��w��MailAddress��Body�ݩ�
        'msg.BodyEncoding = System.Text.Encoding.UTF8
        ''�]�w����r�ꪺ�s�X�覡��UTF8


        'If Mmsg.MailAttachments <> Nothing Then

        '    '�p�G����������ɮ�
        '    Dim Att As New Net.Mail.Attachment(Mmsg.MailAttachments)

        '    '�إߪ����������ܼ�
        '    'Dim Att As New Net.Mail.Attachment("E:\test.txt")
        '    msg.Attachments.Add(Att)
        '    '�N����������[�JMailMessage��Attachments�ݩ�
        'End If

        ''20110926
        ''Dim ret As Collection(Of MailClass) = Me.getCustlst(Me._CustListId)

        ''���o�n�H�e���Ȥ�M��------------------------------------------------
        'Dim ret As Collection(Of MailClass)

        'If Me.chk_IsSendAllCust.Checked = True Then

        '    Dim cust As New EDM.Holmes.Cust.Cust
        '    Me._SendCustLst = cust.getCustEmail()

        '    ret = Me._SendCustLst

        'Else
        '    ret = Me._SendCustLst
        'End If


        ''�v���ǰe�l��--------------------------------------------------------
        'Dim N As Integer = 0

        'If ret Is Nothing Then
        '    MsgBox("���I��q�T����ܫȤ�")
        '    Exit Sub
        'End If

        'For Each mail As MailClass In ret
        '    Dim SendMsg As String = Nothing                     '�]�w�r���ܼ�

        '    If Not String.IsNullOrEmpty(mail._Mailto) Then

        '        Mmsg._Mailto = mail._Mailto

        '        Try
        '            msg.To.Add(Mmsg._Mailto)

        '            '�ǰe�q�l�l��    
        '            mailClien.Send(msg)
        '            '�֭p�ǰe����
        '            N += 1
        '            SendMsg = "�l��w�H�X....."
        '        Catch ex As Exception
        '            '�N���~�T�����w��SendMsg�r��
        '            SendMsg = ex.ToString
        '            failCnt += 1
        '        End Try

        '        BackgroundWorker1.ReportProgress(N, SendMsg)

        '    End If
        'Next



        ''�Ұʦ^���i�רƥ�A�åH�ǰe���ƩM�u�ǰe�����v�T�������޼ƶǰe��ƥ�{��
        'BackgroundWorker1.ReportProgress(N, "�ǰe����.....")

        ''�]�w���\����
        'workHistory._SuccessCnt = N
        ''�]�w���ѵ���
        'workHistory._FailCnt = failCnt


    End Sub


    ''' <summary>
    ''' �ǰe�H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click

        'Minimize this form-------------------------------------------------
        'Dim rs As MsgBoxResult = MsgBox("�O�_�Y�p����?", MsgBoxStyle.YesNo)

        'If rs = MsgBoxResult.Yes Then
        Me.WindowState = FormWindowState.Minimized
        'End If


        '�O�_�x�s�ܤu�@�Ƶ{�Ϊ̽d��
        IsSaveToWorkArrangeOrSamp()


        If txt_Sender.Text = "" Then
            MsgBox("�S����J�H��̦a�}    ", MsgBoxStyle.Information)
            Exit Sub
        Else
            '�N�H��̦a�}���w��MailClass1���O��MailFron�ݩ�
            Mmsg.MailFrom = txt_Sender.Text
        End If

        If txt_Title.Text = "" Then
            MsgBox("�S����J�D��    ", MsgBoxStyle.Information)
            Exit Sub
        Else
            '�N�D�����w��MailClass1���O��MailSubject�ݩ�()
            Mmsg.MailSubject = txt_Title.Text            '
        End If

        If txt_Content.Text = "" Then
            MsgBox("�S����J����    ", MsgBoxStyle.Information)
            Exit Sub
        Else

            Dim htmlContent As String = myContent(txt_Content.Text)

            '�]�wñ�W��
            Dim mailSign As String = User.mailSign

            '�N������w��MailClass1���O��MailBody�ݩ�
            '2011-09-15
            'Mmsg.MailBody = txt_Content.Text & "<br>" & Me.lbl_Img.Text
            Mmsg.MailBody = htmlContent & "<br>" & Me.lbl_Img.Text & "<br><br>" & mailSign

        End If

        '�p�G����������ɮ�
        If txt_Attach.Text <> "" Then
            '�N�����ɮת���T���w��MailClass1���O��MailAttachments�ݩ�
            Mmsg.MailAttachments = txt_Attach.Text.Trim
        End If


        '�x�s�ܱH��ƥ�
        Dim bp As New EDM.Holmes.Mail.MailBp(Me.txt_Sender.Text.Trim, Me.txt_Receiver.Text.Trim, Me.txt_Title.Text.Trim, txt_Content.Text.Trim, "", "")

        bp.SaveToMail()

        '�|�ݭn�B�z���󳡤�

        '���\�^���i��
        BackgroundWorker1.WorkerReportsProgress = True


        '���\����
        BackgroundWorker1.WorkerSupportsCancellation = True


        '�ҰʭI���@�~
        If Not Me.BackgroundWorker1.IsBusy Then
            Try

                BackgroundWorker1.RunWorkerAsync()

            Catch ex As Exception

                '2011/10/1
                Me.BackgroundWorker1 = Nothing

            End Try
        End If

        '2011/12/10-----------------------------------------------------------------------------


    End Sub

    ''' <summary>
    ''' ��ܳq�T��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_ChooseCust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If Not My.Forms.AddProject.ToolStrip2.Items.Item("SMS_AddCust") Is Nothing Then
            My.Forms.AddProject.ToolStrip2.Items("SMS_AddCust").Visible = False

        End If

        My.Forms.AddProject.TopMost = True
        My.Forms.AddProject.Show()
        My.Forms.AddProject.Left = 270
        My.Forms.AddProject.Top = 195


        'My.Forms.AddProject.btn_ImportCust.Visible = False
        '²�T���s
        'My.Forms.AddProject.ToolStripButton1.Visible = False


        'remove close button for addProject form----

        My.Forms.AddProject.ToolStrip2.Items.RemoveAt(1)

        '-------------------------------------------

        Dim control As New ControlTool

        'add new button control named �H�H-----------------------------------------

        Dim btn As ToolStripButton = control.AddToolsBtn("�H�H", ControlTool.ImgButton, "Images\Email.png")

        My.Forms.AddProject.ToolStrip2.Items.Add(btn)


        AddHandler btn.Click, AddressOf AddPrj_Click

        '--------------------------------------------------------------------------

        'readd close button for addProjet form----------



        Dim btn2 As ToolStripButton = control.AddToolsBtn("����", ControlTool.ImgButton, "Images\Delete.png")


        My.Forms.AddProject.ToolStrip2.Items.Add(btn2)


        AddHandler btn2.Click, AddressOf CloseAddPrject_Click

        '-----------------------------------------------


    End Sub

    ''' <summary>
    ''' �[�J�q�T����ثe���H�H�W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddPrj_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim ret As Collection(Of MailClass) = My.Forms.AddProject.getCheckedLst(My.Forms.AddProject.DataGridView1)

        Dim receiver As New StringBuilder

        For Each x As MailClass In ret
            receiver.Append(IIf(x._Mailto <> "", x._Mailto & ",", ""))
        Next

        Dim test As String = receiver.ToString

        Me._SendCustLst = ret
        Me.txt_Receiver.Text = test

        My.Forms.AddProject.Close()

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


    Private Sub txt_Sender_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '���ܨS����J�H��̦a�}
        If txt_Sender.Text = Nothing Then
            ErrorProvider1.SetError(txt_Sender, "��J�H��̦a�}")
        Else
            ErrorProvider1.SetError(txt_Sender, "")
        End If

    End Sub


    Private Sub txt_Title_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Me._MailTitle = Me.txt_Title.Text.Trim

    End Sub

    Private Sub txt_Title_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '���ܨS����J�D��
        If txt_Title.Text = Nothing Then
            ErrorProvider1.SetError(txt_Title, "��J�D��")
        Else
            ErrorProvider1.SetError(txt_Title, "")
        End If
    End Sub

    Private Sub txt_Content_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        Me._MailContent = Me.txt_Content.Text.Trim

    End Sub



    Private Sub txt_Content_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '���ܨS����J����
        If txt_Content.Text = Nothing Then
            ErrorProvider1.SetError(txt_Content, "��J����")
        Else
            ErrorProvider1.SetError(txt_Content, "")
        End If
    End Sub




    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        '�I���@�~�i�צ^���{��()
        'txt_Receiver.Text = Mmsg.Mailto                             '�e�{����̦�}                                        
        txt_Message.Text = e.UserState
        '�e�{�ǰe�T��   
        Label4.Text = "�w�ǰe " & e.ProgressPercentage & "��"          '�e�{�ǰe����   




    End Sub



    Private Function myContent(ByVal orginContent As String) As String

        orginContent = orginContent.Trim

        '�N�������ܦ�html br
        orginContent = orginContent.Replace(vbCrLf, "<br/>")

        Dim htmlContent As String = " <div style=display:block>" & _
                   "<font size=""9"" style=font-family:Arial;color:Gray>" & orginContent & "</font>" & _
                   "</div>" & _
                   "<br/>"

        Return htmlContent


    End Function




    ''' <summary>
    ''' �����I���{�ǫ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        '�]�w�H�H�����ɶ�
        workHistory._EndTime = DateTime.Now

        '�]�w���{����
        workHistory._BpType = SendType.SendTypeFactory.SendMail

        Try
            workHistory.Save()
        Catch ex As Exception
            MsgBox("Error_Code:709_SendMail_Error")
            Exit Sub
        End Try


        Me.txt_Message.Visible = True

        MsgBox("�u�@����")

        My.Forms.AddProject.Close()

        Me.Close()



    End Sub


    ''' <summary>
    ''' �������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        '���_�@�~
        '�Y���b���椤�~��ϥΤ��_�@�~,or�|���Ϳ��~
        If Me.BackgroundWorker1.IsBusy Then
            Me.BackgroundWorker1.CancelAsync()
            Me.Close()

        Else
            Me.Close()


        End If

    End Sub


    ''' <summary>
    ''' ��ܽd��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



        My.Forms.SampleMaintain.Show()
        'My.Forms.SampleMaintain.btn_OK.Visible = True
        ' My.Forms.SampleMaintain.btn_Cancel.Visible = True



    End Sub

    ''' <summary>
    ''' ���J�Ϥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        'My.Forms.Samp.Show()

    End Sub


    ''' <summary>
    '''�s���ɮ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "��r�� (*.txt)|*.txt|�Ҧ��ɮ� (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: �b���[�J�}���ɮת��{���X�C

            Me.txt_Attach.Text = FileName

            Dim i As Integer = FileName.LastIndexOf("\")
            Me.lbl_SelectedFile.Text &= FileName.Substring(i + 1)
            Me.lbl_SelectedFile.Visible = True

        End If
    End Sub

    ''' <summary>
    ''' ��ܬO�_�u�@�O�_�s�J�Ƶ{�Ϊ̽d��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IsSaveToWorkArrangeOrSamp()

        Dim btn As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddSamp

        Dim btn1 As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddWork

        AddHandler btn.Click, AddressOf AddSample

        AddHandler btn1.Click, AddressOf AddWork

        My.Forms.IsSaveToWorkOrSamp.Show()


    End Sub

    '�s�W�d��
    Private Sub AddSample(ByVal sender As Object, ByVal e As EventArgs)

        Dim samp As New Saple
        samp._SampName = Me.txt_Title.Text.Trim
        samp._SampContent = Me.txt_Content.Text.Trim
        samp._SampCat = Saple.MailSamp

        samp.AddSamp(samp)

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub

    '�s�W�d��
    Private Sub AddWork(ByVal sender As Object, ByVal e As EventArgs)

        My.Forms.AddWork.Show()

    End Sub


    Private Sub lbl_CustPrjId_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Me._CustListId = Me.lbl_CustPrjId.Text.Trim

    End Sub

    Private Sub txt_Receiver_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Me._Mailto = Me.txt_Receiver.Text.Trim

    End Sub


    ''' <summary>
    ''' �Ŀ�ɨ��o�Ҧ��Ȥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_IsSendAllCust_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_IsSendAllCust.CheckedChanged

        Dim ret As Collection = Nothing

        Dim sb As New StringBuilder

        '�Ŀ�
        If Me.chk_IsSendAllCust.Checked = True Then

            Dim cust As New EDM.Holmes.Cust.Cust

            Try
                ret = cust.getCustEmail
            Catch ex As Exception
                MsgBox("Error_Code:815_SendMail_Error")
            End Try

            For Each email As String In ret
                sb.Append(IIf(email <> "", email & ",", ""))
            Next

            Me.txt_Receiver.Text = sb.ToString

        End If

        '���Ŀ�
        If Me.chk_IsSendAllCust.Checked = False Then
            Me.txt_Receiver.Text = ""
        End If


    End Sub



    Private Sub SendMail_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.StyleChanged

        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
            Me.Hide()


        End If

    End Sub

    ''' <summary>
    ''' ���U�t�ιϥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick

        Me.ShowInTaskbar = True

        Me.Show()

        Me.WindowState = FormWindowState.Normal


    End Sub


    Private Sub NotifyIcon1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseMove

        'Me.NotifyIcon1.BalloonTipText = Me.txt_Message.Text



    End Sub

    ''' <summary>
    ''' ���o�Ȥ�W�� or ��¨ϥ�Email�a�} 2011/10/10 not use
    ''' </summary>
    ''' <param name="custPrjId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function getCustlst(ByVal custPrjId As String) As Collection(Of MailClass)

        Dim ret As New Collection(Of MailClass)


        '�P�_�O�_�O�M�שΪ̳�«H�c�a�}
        If String.IsNullOrEmpty(custPrjId) Then



            Dim mail As New MailClass
            mail._Mailto = Me.txt_Receiver.Text.Trim


            ret.Add(mail)

        Else

            Dim conn As OleDbConnection = HolmesConn.getConnection

            Dim cmd As New OleDbCommand

            cmd.Connection = conn

            cmd.CommandText = "select CustEmail  from (�Ȥ�� a left join �Ȥ��ƪ� b on a.CustId = b.CustId) where CustPrjid = ?   and  CustEmail is not null"

            cmd.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = custPrjId

            cmd.Connection = conn

            conn.Open()


            Dim reader As OleDbDataReader = cmd.ExecuteReader

            While reader.Read
                '�O�_�����TEmail�榡
                If Tool.IsValidEmail(reader.Item("CustEmail")) Then
                    Dim mail As New MailClass
                    mail._Mailto = CStr(reader.Item("CustEmail"))
                    ret.Add(mail)
                End If
            End While
        End If



        Return ret


    End Function


    Private Overloads Function getCustlst(ByVal CustRet As Collection(Of EDM.Holmes.Cust.Cust)) As Collection(Of MailClass)



        ''�P�_�O�_�O�M�שΪ̳�«H�c�a�}
        'If String.IsNullOrEmpty(custPrjId) Then



        '    Dim mail As New MailClass
        '    mail.Mailto = Me.txt_Receiver.Text.Trim


        '    ret.Add(mail)

        'Else

        '    Dim conn As OleDbConnection = HolmesConn.getConnection

        '    Dim cmd As New OleDbCommand

        '    cmd.Connection = conn

        '    cmd.CommandText = "select CustEmail  from (�Ȥ�� a left join �Ȥ��ƪ� b on a.CustId = b.CustId) where CustPrjid = ?   and  CustEmail is not null"

        '    cmd.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = custPrjId

        '    cmd.Connection = conn

        '    conn.Open()


        '    Dim reader As OleDbDataReader = cmd.ExecuteReader

        '    While reader.Read
        '        '�O�_�����TEmail�榡
        '        If Tool.IsValidEmail(reader.Item("CustEmail")) Then
        '            Dim mail As New MailClass
        '            mail.Mailto = CStr(reader.Item("CustEmail"))
        '            ret.Add(mail)
        '        End If


        '    End While
        'End If



        'Return ret


    End Function



    Public Property _SelectedSmapleId()
        Get
            Return Me.selectedSmapleId
        End Get
        Set(ByVal value)
            Me.selectedSmapleId = value
        End Set
    End Property

    Public Property _SelectedSampleName()
        Get
            Return Me.selectedSampleName
        End Get
        Set(ByVal value)
            Me.selectedSampleName = value
        End Set
    End Property

    Public Property _MailTitle()
        Get
            Return Me.mailTitle
        End Get
        Set(ByVal value)
            Me.mailTitle = value
        End Set
    End Property

    Public Property _MailContent()
        Get
            Return Me.mailContent
        End Get
        Set(ByVal value)
            Me.mailContent = value
        End Set
    End Property

    '�M�׹������Ȥ�W��
    Public Property _CustListId()
        Get
            Return Me.CustListId
        End Get
        Set(ByVal value)
            Me.CustListId = value
        End Set
    End Property

    '�����(�M�צW�٩Ϊ̳�ª��H�c)
    Public Property _Mailto()
        Get
            Return Me.Mto
        End Get
        Set(ByVal value)
            Me.Mto = value
        End Set
    End Property

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

    Public Property _SendCustLst()
        Get
            Return Me.SendCustLst
        End Get
        Set(ByVal value)
            Me.SendCustLst = value
        End Set
    End Property


End Class