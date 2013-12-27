Imports System.Configuration
Imports EDM.Holmes.Tools
Imports EDM.Holmes.EDMSetting
Imports EDM.Holmes.VO.User
Imports System.Net
Imports System.IO


Public Class SMSSetting

    

    Private Sub SMSSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        init()

        '���o�Ѿl�I��
        Dim restPoint As String = getRestPoint(Me.txt_SMSId.Text.Trim, Me.txt_SMSPwd.Text.Trim)
        If restPoint = "-2" Then
            Me.lbl_Point.Text = "�b���Ϊ̱K�X���~"
        Else
            Me.lbl_Point.Text = restPoint

        End If


    End Sub

    ''' <summary>
    ''' ��l��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init()


        Dim x As New User

        x = x.getUserSMS(User.LoginId, User.LoginPwd)

        Me.txt_SMSId.Text = x._SMSLoginId

        Me.txt_SMSPwd.Text = x._SMSLoginPwd



    End Sub


 

    ''' <summary>
    ''' �M�γ]�w
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Send.Click



        Dim SMSAccount As String = Me.txt_SMSId.Text.Trim

        Dim SMSpwd As String = Me.txt_SMSPwd.Text.Trim

        Dim x As New User
        x._SMSLoginId = SMSAccount
        x._SMSLoginPwd = SMSpwd
        x._LoginId = User.LoginId
        x._LoginPwd = User.LoginPwd

        User.SMSLoginid = x._SMSLoginId
        User.SMSLoginPwd = x._SMSLoginPwd



        Try
            x.UpdUserSMS(x)
        Catch ex As Exception
            MsgBox("�M�γ]�w����")
            Exit Sub

        End Try

        MsgBox("�M�Φ��\")


    End Sub

    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        Me.Close()

    End Sub

    Private Function getRestPoint(ByVal username As String, ByVal pwd As String) As String

        'Dim proxyObject As WebProxy

        'Dim proxyStrin As String

        'Dim myRequest As HttpWebRequest = New System.Net.HttpWebRequest.create("")

        Dim url As String = String.Format("http://mail2sms.com.tw/memberpoint.php?username={0}&password={1}", username, pwd)

        Dim myRequest As System.Net.HttpWebRequest = System.Net.WebRequest.Create(url)

        Dim myRs As WebResponse = myRequest.GetResponse

        Dim myStream As System.IO.Stream = myRs.GetResponseStream

        Dim sr As New StreamReader(mystream, System.Text.Encoding.Default)

        Dim sb As String = ""

        Try
            sb = sr.ReadToEnd
        Catch ex As Exception
            Throw New ArgumentException("Error_Code:119_SMSSetting_Error")

        End Try



        'Dispose------------
        myRequest = Nothing
        myRs = Nothing
        myStream = Nothing
        sr = Nothing


        Return sb



    End Function


    
End Class