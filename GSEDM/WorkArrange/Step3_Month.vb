


Public Class Step3_Month


    Private specefictime As DateTime
    Private every1 As String
    Private friendlyEvery1 As String
    Private whichMonth As String
    Private speceficDay As Integer

    Public Property _Specefictime()
        Get

            Dim now As DateTime = DateTime.Now
            Dim hour As Integer = Me.Num_SpecificHour.Value
            Dim min As Integer = Me.Num_SpecificMin.Value
            Dim specificTime As DateTime = New DateTime(now.Year, now.Month, now.Day, hour, min, 0)
            Return specificTime

        End Get
        Set(ByVal value)
            Me.specefictime = value
        End Set
    End Property


    Public Property _Every1()
        Get

            Return Me.Num_SpecificDay.Value

        End Get

        Set(ByVal value)

            Me.every1 = value

        End Set
    End Property

    ''' <summary>
    ''' ex:每三天,格式化訊息
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _FriendlyEvery1()
        Get
            '若選取每月,則在最後完成顯示訊息時,去掉每月,否則會出現每每月的奇怪字樣
            Return IIf(Me.CheckedListBox2.SelectedItem = "每月", Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "號", "每" & Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "號")

        End Get

        Set(ByVal value)

            Me.friendlyEvery1 = value

        End Set
    End Property


    Public Property _WhichMonth()
        Get
            Return Me.CheckedListBox2.SelectedItem.ToString
        End Get
        Set(ByVal value)
            Me.whichMonth = value
        End Set
    End Property


    Public Property _SpeceficDay()
        Get
            Return Me.Num_SpecificDay.Value
        End Get
        Set(ByVal value)
            Me.speceficDay = value
        End Set
    End Property


    Private Sub Step3_Week_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '----------------------------------
        My.Forms.AddWork.btn_Step1.BackColor = Color.LightGray
        My.Forms.AddWork.btn_Step2.BackColor = Color.Blue
        My.Forms.AddWork.btn_Step5.BackColor = Color.LightGray

        '----------------------------------

    End Sub

    ''' <summary>
    ''' 下一步
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click


        If My.Forms.SmsOrMail Is Nothing Then
            My.Forms.SmsOrMail.MdiParent = My.Forms.AddWork
            My.Forms.SmsOrMail.Show()
            My.Forms.SmsOrMail.WindowState = FormWindowState.Maximized
        Else
            My.Forms.SmsOrMail.MdiParent = My.Forms.AddWork
            My.Forms.SmsOrMail.WindowState = FormWindowState.Maximized
            My.Forms.SmsOrMail.Show()
        End If






    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        My.Forms.Step2.MdiParent = My.Forms.AddWork
        My.Forms.Step2.WindowState = FormWindowState.Maximized


    End Sub


End Class