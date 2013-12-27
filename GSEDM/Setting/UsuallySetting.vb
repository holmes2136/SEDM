Imports EDM.Holmes.EDMSetting
Imports EDM.Holmes.VO.User


Public Class UsuallySetting

    Private Sub UsuallySetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        

        Init()


    End Sub

    Private Sub Init()

        Dim setting As New EDMSetting

        Dim mysetting As EDMSetting = setting.getMailSetting(User.LoginId, User.LoginPwd)

        Me.txt_Sender.Text = mysetting._MailSender

        Me.txt_Sign.Text = mysetting._MailSign

        Me.txt_Sign.Text = Me.txt_Sign.Text.Replace("<br>", vbCrLf)


    End Sub

    ''' <summary>
    ''' �������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        Me.Close()

    End Sub


    ''' <summary>
    ''' �M�γ]�w
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click


        Dim mailSender As String = Me.txt_Sender.Text.Trim

        '�N�_�����ܦ�html br,�u��e�{�ϥΪ̧ڥ���ñ�W��
        Dim mailSign As String = Me.txt_Sign.Text.Replace(vbCrLf, "<br>").Trim


        Dim setting As New EDMSetting
        setting._MailSender = mailSender
        setting._MailSign = mailSign
        setting._LoginId = User.LoginId
        setting._LoginPwd = User.LoginPwd

        '�x�s��ϥΪ̳]�w��-------------------
        User.mailSender = setting._MailSender
        User.mailSign = setting._MailSign
        '-------------------------------------

        Try
            setting.setMailSetting(setting)

        Catch ex As Exception
            MsgBox("�]�w����")
            Exit Sub
        End Try

        MsgBox("�M�γ]�w���\")

    End Sub


End Class