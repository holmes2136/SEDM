Imports System.Configuration
Imports EDM.Holmes.VO.User

Public Class WebMailSetting

    Private mailServerType As String

    Public Property _MailServerType()
        Get

            Dim user As New User

            Dim serverType As String = user.getMailServerType(user._LoginId, user._LoginPwd)

            Return serverType

        End Get
        Set(ByVal value)
            Me.mailServerType = value
        End Set
    End Property



    Private Sub WebMailSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'Me.MdiParent = My.Forms.Seting

      

        '設定初始化()

        'User.LoginId = "sangua011"
        'User.LoginPwd = "168168"

        WebMailInit()

    End Sub


    ''' <summary>
    ''' 設定初始化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WebMailInit()

        'Dim mailServerType As String = ConfigurationManager.AppSettings("mailServerType")

        'If Not String.IsNullOrEmpty(mailServerType) Then

        '    Select Case mailServerType
        '        Case "gmail"
        '            Me.rad_Gmail.Checked = True
        '        Case "hotmail"
        '            Me.rad_hotmail.Checked = True
        '        Case "yahoo"
        '            Me.rad_yahoo.Checked = True
        '    End Select
        'End If

        ' If Not String.IsNullOrEmpty(Me._MailServerType) Then

        Select Case Me._MailServerType
            Case "gmail"
                Me.rad_Gmail.Checked = True
            Case "hotmail"
                Me.rad_hotmail.Checked = True
            Case "yahoo"
                Me.rad_yahoo.Checked = True
        End Select
        ' End If

    End Sub

    ''' <summary>
    ''' 套用設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_EnsureMailSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EnsureMailSetting.Click


        Dim id As String = Me.txt_MailAccount.Text.Trim
        Dim pwd As String = Me.txt_MailPwd.Text.Trim

        Dim user As New User


        Try


            If Me.rad_Gmail.Checked = True Then
                user.UptGmailSetting(user._LoginId, user._LoginPwd, id, pwd)
            ElseIf Me.rad_hotmail.Checked = True Then
                user.UptHotMailSetting(user._LoginId, user._LoginPwd, id, pwd)
            ElseIf Me.rad_yahoo.Checked = True Then
                user.UptYahooSetting(user._LoginId, user._LoginPwd, id, pwd)

            End If

        Catch ex As Exception
            MsgBox("套用設定失敗")
            Exit Sub
        End Try

        MsgBox("套用設定成功")


        '需要過濾特殊符號.否則在Config恐怕會失效

        'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        'Dim app As AppSettingsSection = config.AppSettings

        'If Me.rad_yahoo.Checked = True Then
        '    Me._MailServerType = "yahoo"
        'ElseIf Me.rad_hotmail.Checked = True Then
        '    Me._MailServerType = "hotmail"
        'ElseIf Me.rad_Gmail.Checked = True Then
        '    Me._MailServerType = "gmail"
        'End If

        'app.Settings("mailServerType").Value = Me._MailServerType



        'Try
        '    config.Save(ConfigurationSaveMode.Modified)
        'Catch ex As Exception
        '    MsgBox("套用失敗")
        '    Exit Sub
        'End Try

        'MsgBox("已經套用")



    End Sub

  

    ''' <summary>
    ''' 當點選Gmail radio時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rad_Gmail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_Gmail.CheckedChanged

        Dim gmail As RadioButton = CType(sender, RadioButton)

        If gmail.Checked Then
            Dim user As New User
            Dim user1 As User = user.getUserGmail(user._LoginId, user._LoginPwd)

            Me.txt_MailAccount.Text = user1._GmailLoginId
            Me.txt_MailPwd.Text = user1._GmailPwd
            user._MailServerType = "gmail"

        End If

    End Sub


    ''' <summary>
    ''' 當點選Hotmail radio時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rad_hotmail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_hotmail.CheckedChanged


        Dim hotmail As RadioButton = CType(sender, RadioButton)

        If hotmail.Checked Then
            Dim user As New User
            Dim user1 As User = user.getUserHotmail(user._LoginId, user._LoginPwd)

            Me.txt_MailAccount.Text = user1._HotMailLoginId
            Me.txt_MailPwd.Text = user1._HotMailLoginPwd
            user._MailServerType = "hotmail"

        End If


    End Sub

    ''' <summary>
    ''' 當點選yahoo radio時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rad_yahoo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_yahoo.CheckedChanged


        Dim yahoo As RadioButton = CType(sender, RadioButton)

        If yahoo.Checked Then
            Dim user As New User
            Dim user1 As User = user.getUserYmail(user._LoginId, user._LoginPwd)

            Me.txt_MailAccount.Text = user1._YahooLoginId
            Me.txt_MailPwd.Text = user1._YahooLoginPwd
            user._MailServerType = "yahoo"

        End If


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

   
End Class