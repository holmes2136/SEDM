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

    '設定專案Id
    Private CustListId As String

    '設定寄件者
    Private Mto As String
    '設定信件標題
    Private mailTitle As String
    '設定信件內容
    Private mailContent As String

    '選取得信件圖片Id
    Private selectedSmapleId As String

    '選取的信件圖片名稱
    Private selectedSampleName As String

    '選取的範本Id
    Private selectedSampId As String

    '選取的範本名稱
    Private selectedSampName As String

    '準備寄出的客戶清單
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
    ''' 背景工作內容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim mail As MailClass = Me.Mmsg

        Dim ret As Collection = Me._SendCustLst

        '錯誤筆數
        Dim failCnt As Integer

        '設定開始寄信時間
        'workHistory._StartTime = DateTime.Now


        '設定傳送郵件的資訊------------------------------------------

        '依照EMAIL 清單數量宣告多少個SMTP
        'Dim smtpLst(ret.Count - 1) As MySmtp
        Dim smtpLst(ret.Count - 1) As MySmtp


        'google,hotmail,yahoo...etc...
        Dim smtplist As New SmtpList

        Dim iterator As SmtpIterator = smtplist.getIterator

        '移動到下一筆
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

            '若沒有下一筆,則重新循環
            If Not iterator.hasNext Then
                iterator.Reset()
            End If

        Next


        '郵件內容設定-------------------------------------------------


        '設定標頭-----------------------------------------------------

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

            '----附件------------------------------------
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
                        Console.Write("第" & z & "筆")
                        Console.Write(vbCrLf)

                    End SyncLock

                Catch ex As Exception
                    '會出現datetime轉變錯誤
                    'Console.Write(String.Format("第{0}筆,smtp{1},break}", i, smtpLst(i - 1)._Smtp)) 
                    'SyncLock Me

                    '    Console.Write(Thread.CurrentThread.Name & " /")
                    '    Console.Write("第" & i & "筆")
                    '    Console.Write(vbCrLf)
                    '    failCnt += 1


                    'i += 1
                    'End SyncLock

                End Try

                'background.ReportProgress(N, sendMsg)

            End If

        Next

        '--------------------------------------------------------------------


        '啟動回報進度事件，並以傳送筆數和「傳送完閉」訊息做為引數傳送到事件程序
        'background.ReportProgress(N, "傳送完畢.....")

        '設定成功筆數
        workHistory._SuccessCnt = N

        '設定失敗筆數
        workHistory._FailCnt = failCnt

        'Dispose

        '2011/12/10-----------------------------------------------------------------------------

        ''錯誤筆數
        'Dim failCnt As Integer

        ''設定開始寄信時間
        'workHistory._StartTime = DateTime.Now


        ''設定傳送郵件的資訊------------------------------------------
        'Dim mailServer As MailServer = MailServerFactory.createMailServer(User.mailServerType)

        'If mailServer Is Nothing Then
        '    MsgBox("請選擇任一網路信箱")

        '    Exit Sub
        'End If


        'Dim mailClien As New Net.Mail.SmtpClient(mailServer.smtp, mailServer.port) '指定SMTP主機與傳輸埠

        'Dim x As User = mailServer.getLoginData(User.LoginId, User.LoginPwd)

        'If String.IsNullOrEmpty(x._MailId) Or String.IsNullOrEmpty(x._MailPwd) Then

        '    MsgBox("帳號密碼不能為空")
        '    Exit Sub

        'End If

        'mailClien.EnableSsl = True
        'mailClien.Credentials = New System.Net.NetworkCredential(x._MailId, x._MailPwd)


        ''郵件基本設定-------------------------------------------------



        ''郵件內容設定-------------------------------------------------
        'Dim msg As New Net.Mail.MailMessage

        'msg.Priority = MailPriority.High
        ''設定標頭-----------------------------------------------------
        ''bulk send mail
        'msg.Headers.Add("Precedence", "bulk")
        ''Unsubscribe
        'msg.Headers.Add("List-Unsubscribe", mailServer.getMailAddress(x._MailId))

        ''建立msg為空的MailMessage執行個體變數
        'msg.From = New System.Net.Mail.MailAddress(mailServer.getMailAddress(x._MailId), Me.txt_Sender.Text.Trim)

        ''設定寄件者地址與名稱，並指定給MailAddress的From屬性
        'msg.Subject = Me.txt_Title.Text.Trim

        ''由自訂的MailClass1類別中取出主旨屬性，並指定給MailAddress的Subject屬性
        'msg.SubjectEncoding = System.Text.Encoding.UTF8

        ''設定主旨字串的編碼方式為UTF8

        ''假入點選「HTML模式」選項
        'msg.IsBodyHtml = True

        ''將內文檢視模式設定為HTML模式

        'msg.Body = Mmsg.MailBody
        ''由自定的MailClass1類別中取出內文屬性，並指定給MailAddress的Body屬性
        'msg.BodyEncoding = System.Text.Encoding.UTF8
        ''設定內文字串的編碼方式為UTF8


        'If Mmsg.MailAttachments <> Nothing Then

        '    '如果有選取附件檔案
        '    Dim Att As New Net.Mail.Attachment(Mmsg.MailAttachments)

        '    '建立附件執行個體變數
        '    'Dim Att As New Net.Mail.Attachment("E:\test.txt")
        '    msg.Attachments.Add(Att)
        '    '將附件執行個體加入MailMessage的Attachments屬性
        'End If

        ''20110926
        ''Dim ret As Collection(Of MailClass) = Me.getCustlst(Me._CustListId)

        ''取得要寄送的客戶清單------------------------------------------------
        'Dim ret As Collection(Of MailClass)

        'If Me.chk_IsSendAllCust.Checked = True Then

        '    Dim cust As New EDM.Holmes.Cust.Cust
        '    Me._SendCustLst = cust.getCustEmail()

        '    ret = Me._SendCustLst

        'Else
        '    ret = Me._SendCustLst
        'End If


        ''逐筆傳送郵件--------------------------------------------------------
        'Dim N As Integer = 0

        'If ret Is Nothing Then
        '    MsgBox("請點選通訊錄選擇客戶")
        '    Exit Sub
        'End If

        'For Each mail As MailClass In ret
        '    Dim SendMsg As String = Nothing                     '設定字串變數

        '    If Not String.IsNullOrEmpty(mail._Mailto) Then

        '        Mmsg._Mailto = mail._Mailto

        '        Try
        '            msg.To.Add(Mmsg._Mailto)

        '            '傳送電子郵件    
        '            mailClien.Send(msg)
        '            '累計傳送筆數
        '            N += 1
        '            SendMsg = "郵件已寄出....."
        '        Catch ex As Exception
        '            '將錯誤訊息指定給SendMsg字串
        '            SendMsg = ex.ToString
        '            failCnt += 1
        '        End Try

        '        BackgroundWorker1.ReportProgress(N, SendMsg)

        '    End If
        'Next



        ''啟動回報進度事件，並以傳送筆數和「傳送完閉」訊息做為引數傳送到事件程序
        'BackgroundWorker1.ReportProgress(N, "傳送完畢.....")

        ''設定成功筆數
        'workHistory._SuccessCnt = N
        ''設定失敗筆數
        'workHistory._FailCnt = failCnt


    End Sub


    ''' <summary>
    ''' 傳送信件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click

        'Minimize this form-------------------------------------------------
        'Dim rs As MsgBoxResult = MsgBox("是否縮小視窗?", MsgBoxStyle.YesNo)

        'If rs = MsgBoxResult.Yes Then
        Me.WindowState = FormWindowState.Minimized
        'End If


        '是否儲存至工作排程或者範本
        IsSaveToWorkArrangeOrSamp()


        If txt_Sender.Text = "" Then
            MsgBox("沒有輸入寄件者地址    ", MsgBoxStyle.Information)
            Exit Sub
        Else
            '將寄件者地址指定給MailClass1類別的MailFron屬性
            Mmsg.MailFrom = txt_Sender.Text
        End If

        If txt_Title.Text = "" Then
            MsgBox("沒有輸入主旨    ", MsgBoxStyle.Information)
            Exit Sub
        Else
            '將主旨指定給MailClass1類別的MailSubject屬性()
            Mmsg.MailSubject = txt_Title.Text            '
        End If

        If txt_Content.Text = "" Then
            MsgBox("沒有輸入內文    ", MsgBoxStyle.Information)
            Exit Sub
        Else

            Dim htmlContent As String = myContent(txt_Content.Text)

            '設定簽名檔
            Dim mailSign As String = User.mailSign

            '將內文指定給MailClass1類別的MailBody屬性
            '2011-09-15
            'Mmsg.MailBody = txt_Content.Text & "<br>" & Me.lbl_Img.Text
            Mmsg.MailBody = htmlContent & "<br>" & Me.lbl_Img.Text & "<br><br>" & mailSign

        End If

        '如果有選取附件檔案
        If txt_Attach.Text <> "" Then
            '將附件檔案的資訊指定給MailClass1類別的MailAttachments屬性
            Mmsg.MailAttachments = txt_Attach.Text.Trim
        End If


        '儲存至寄件備份
        Dim bp As New EDM.Holmes.Mail.MailBp(Me.txt_Sender.Text.Trim, Me.txt_Receiver.Text.Trim, Me.txt_Title.Text.Trim, txt_Content.Text.Trim, "", "")

        bp.SaveToMail()

        '尚需要處理附件部分

        '允許回報進度
        BackgroundWorker1.WorkerReportsProgress = True


        '允許取消
        BackgroundWorker1.WorkerSupportsCancellation = True


        '啟動背景作業
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
    ''' 選擇通訊錄
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
        '簡訊按鈕
        'My.Forms.AddProject.ToolStripButton1.Visible = False


        'remove close button for addProject form----

        My.Forms.AddProject.ToolStrip2.Items.RemoveAt(1)

        '-------------------------------------------

        Dim control As New ControlTool

        'add new button control named 寄信-----------------------------------------

        Dim btn As ToolStripButton = control.AddToolsBtn("寄信", ControlTool.ImgButton, "Images\Email.png")

        My.Forms.AddProject.ToolStrip2.Items.Add(btn)


        AddHandler btn.Click, AddressOf AddPrj_Click

        '--------------------------------------------------------------------------

        'readd close button for addProjet form----------



        Dim btn2 As ToolStripButton = control.AddToolsBtn("關閉", ControlTool.ImgButton, "Images\Delete.png")


        My.Forms.AddProject.ToolStrip2.Items.Add(btn2)


        AddHandler btn2.Click, AddressOf CloseAddPrject_Click

        '-----------------------------------------------


    End Sub

    ''' <summary>
    ''' 加入通訊錄到目前的寄信名單
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

        '提示沒有輸入寄件者地址
        If txt_Sender.Text = Nothing Then
            ErrorProvider1.SetError(txt_Sender, "輸入寄件者地址")
        Else
            ErrorProvider1.SetError(txt_Sender, "")
        End If

    End Sub


    Private Sub txt_Title_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Me._MailTitle = Me.txt_Title.Text.Trim

    End Sub

    Private Sub txt_Title_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '提示沒有輸入主旨
        If txt_Title.Text = Nothing Then
            ErrorProvider1.SetError(txt_Title, "輸入主旨")
        Else
            ErrorProvider1.SetError(txt_Title, "")
        End If
    End Sub

    Private Sub txt_Content_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        Me._MailContent = Me.txt_Content.Text.Trim

    End Sub



    Private Sub txt_Content_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '提示沒有輸入內文
        If txt_Content.Text = Nothing Then
            ErrorProvider1.SetError(txt_Content, "輸入內文")
        Else
            ErrorProvider1.SetError(txt_Content, "")
        End If
    End Sub




    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        '背景作業進度回報程序()
        'txt_Receiver.Text = Mmsg.Mailto                             '呈現收件者住址                                        
        txt_Message.Text = e.UserState
        '呈現傳送訊息   
        Label4.Text = "已傳送 " & e.ProgressPercentage & "筆"          '呈現傳送筆數   




    End Sub



    Private Function myContent(ByVal orginContent As String) As String

        orginContent = orginContent.Trim

        '將換行轉變成html br
        orginContent = orginContent.Replace(vbCrLf, "<br/>")

        Dim htmlContent As String = " <div style=display:block>" & _
                   "<font size=""9"" style=font-family:Arial;color:Gray>" & orginContent & "</font>" & _
                   "</div>" & _
                   "<br/>"

        Return htmlContent


    End Function




    ''' <summary>
    ''' 完成背景程序後
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted


        '設定寄信結束時間
        workHistory._EndTime = DateTime.Now

        '設定歷程類型
        workHistory._BpType = SendType.SendTypeFactory.SendMail

        Try
            workHistory.Save()
        Catch ex As Exception
            MsgBox("Error_Code:709_SendMail_Error")
            Exit Sub
        End Try


        Me.txt_Message.Visible = True

        MsgBox("工作完成")

        My.Forms.AddProject.Close()

        Me.Close()



    End Sub


    ''' <summary>
    ''' 關閉按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        '中斷作業
        '若正在執行中才能使用中斷作業,or會產生錯誤
        If Me.BackgroundWorker1.IsBusy Then
            Me.BackgroundWorker1.CancelAsync()
            Me.Close()

        Else
            Me.Close()


        End If

    End Sub


    ''' <summary>
    ''' 選擇範本
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
    ''' 插入圖片
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        'My.Forms.Samp.Show()

    End Sub


    ''' <summary>
    '''瀏覽檔案
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "文字檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: 在此加入開啟檔案的程式碼。

            Me.txt_Attach.Text = FileName

            Dim i As Integer = FileName.LastIndexOf("\")
            Me.lbl_SelectedFile.Text &= FileName.Substring(i + 1)
            Me.lbl_SelectedFile.Visible = True

        End If
    End Sub

    ''' <summary>
    ''' 顯示是否工作是否存入排程或者範本
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IsSaveToWorkArrangeOrSamp()

        Dim btn As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddSamp

        Dim btn1 As Button = My.Forms.IsSaveToWorkOrSamp.btn_AddWork

        AddHandler btn.Click, AddressOf AddSample

        AddHandler btn1.Click, AddressOf AddWork

        My.Forms.IsSaveToWorkOrSamp.Show()


    End Sub

    '新增範本
    Private Sub AddSample(ByVal sender As Object, ByVal e As EventArgs)

        Dim samp As New Saple
        samp._SampName = Me.txt_Title.Text.Trim
        samp._SampContent = Me.txt_Content.Text.Trim
        samp._SampCat = Saple.MailSamp

        samp.AddSamp(samp)

        My.Forms.IsSaveToWorkOrSamp.Close()


    End Sub

    '新增範本
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
    ''' 勾選時取得所有客戶
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_IsSendAllCust_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_IsSendAllCust.CheckedChanged

        Dim ret As Collection = Nothing

        Dim sb As New StringBuilder

        '勾選
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

        '未勾選
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
    ''' 按下系統圖示
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
    ''' 取得客戶名單 or 單純使用Email地址 2011/10/10 not use
    ''' </summary>
    ''' <param name="custPrjId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function getCustlst(ByVal custPrjId As String) As Collection(Of MailClass)

        Dim ret As New Collection(Of MailClass)


        '判斷是否是專案或者單純信箱地址
        If String.IsNullOrEmpty(custPrjId) Then



            Dim mail As New MailClass
            mail._Mailto = Me.txt_Receiver.Text.Trim


            ret.Add(mail)

        Else

            Dim conn As OleDbConnection = HolmesConn.getConnection

            Dim cmd As New OleDbCommand

            cmd.Connection = conn

            cmd.CommandText = "select CustEmail  from (客戶表 a left join 客戶資料表 b on a.CustId = b.CustId) where CustPrjid = ?   and  CustEmail is not null"

            cmd.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = custPrjId

            cmd.Connection = conn

            conn.Open()


            Dim reader As OleDbDataReader = cmd.ExecuteReader

            While reader.Read
                '是否為正確Email格式
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



        ''判斷是否是專案或者單純信箱地址
        'If String.IsNullOrEmpty(custPrjId) Then



        '    Dim mail As New MailClass
        '    mail.Mailto = Me.txt_Receiver.Text.Trim


        '    ret.Add(mail)

        'Else

        '    Dim conn As OleDbConnection = HolmesConn.getConnection

        '    Dim cmd As New OleDbCommand

        '    cmd.Connection = conn

        '    cmd.CommandText = "select CustEmail  from (客戶表 a left join 客戶資料表 b on a.CustId = b.CustId) where CustPrjid = ?   and  CustEmail is not null"

        '    cmd.Parameters.Add("@CustPrjId", OleDbType.Integer).Value = custPrjId

        '    cmd.Connection = conn

        '    conn.Open()


        '    Dim reader As OleDbDataReader = cmd.ExecuteReader

        '    While reader.Read
        '        '是否為正確Email格式
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

    '專案對應的客戶名單
    Public Property _CustListId()
        Get
            Return Me.CustListId
        End Get
        Set(ByVal value)
            Me.CustListId = value
        End Set
    End Property

    '收件者(專案名稱或者單純的信箱)
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