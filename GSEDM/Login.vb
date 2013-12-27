Imports EDM.Holmes.VO.User


Public Class Login

    ' TODO: 插入程式碼，利用提供的使用者名稱和密碼執行自訂驗證
    ' (請參閱 http://go.microsoft.com/fwlink/?LinkId=35339)。
    ' 如此便可將自訂主體附加到目前執行緒的主體，如下所示:
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' 其中 CustomPrincipal 是用來執行驗證的 IPrincipal 實作。
    ' 接著，My.User 便會傳回封裝在 CustomPrincipal 物件中的識別資訊，
    ' 例如使用者名稱、顯示名稱等。

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        Dim id As String = Me.txt_LoginId.Text.Trim

        Dim pwd As String = Me.txt_LoingPwd.Text.Trim

        Dim User As New User

        Dim myUser As EDM.Holmes.VO.User.User = User.getUserByLogin(id, pwd)


        If Not myUser Is Nothing Then

            User._LoginId = id
            User._LoginPwd = pwd
            User._MailServerType = myUser._MailServerType
            User._MailSender = myUser._MailSender
            User._MailSign = myUser._MailSign
            User._SMSLoginPwd = myUser._SMSLoginPwd
            User._SMSLoginId = myUser._SMSLoginId

            MsgBox("已登入")

            My.Forms.Main.Show()

            Me.Visible = False

        Else

            MsgBox("帳號或密碼錯誤")
            Exit Sub

        End If


        'MsgBox(User._MailServerType)



    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

End Class
