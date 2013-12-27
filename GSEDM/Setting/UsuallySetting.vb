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
    ''' 關閉按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        Me.Close()

    End Sub


    ''' <summary>
    ''' 套用設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click


        Dim mailSender As String = Me.txt_Sender.Text.Trim

        '將斷行轉變成html br,真實呈現使用者我打的簽名檔
        Dim mailSign As String = Me.txt_Sign.Text.Replace(vbCrLf, "<br>").Trim


        Dim setting As New EDMSetting
        setting._MailSender = mailSender
        setting._MailSign = mailSign
        setting._LoginId = User.LoginId
        setting._LoginPwd = User.LoginPwd

        '儲存到使用者設定檔-------------------
        User.mailSender = setting._MailSender
        User.mailSign = setting._MailSign
        '-------------------------------------

        Try
            setting.setMailSetting(setting)

        Catch ex As Exception
            MsgBox("設定失敗")
            Exit Sub
        End Try

        MsgBox("套用設定成功")

    End Sub


End Class