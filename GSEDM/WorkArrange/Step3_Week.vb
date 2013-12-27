Public Class Step3_Week


    Private specefictime As DateTime
    Private every1 As String
    Private friendlyEvery1 As String
    Private dayOfWeek As String


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

            Return Me.Num_EveryWeek.Value

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

            Return "每" & Me.Num_EveryWeek.Value & "週"

        End Get

        Set(ByVal value)

            Me.friendlyEvery1 = value

        End Set
    End Property

    Public Property _DayOfWeek()
        Get
            Return Me.CheckedListBox1.SelectedItem
        End Get
        Set(ByVal value)
            Me.dayOfWeek = value
        End Set
    End Property


    Private Sub Step3_Month_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated




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

        'MsgBox(Me.CheckedListBox1.SelectedItem.ToString)


        ' My.Forms.AddWork.btn_Step3.BackColor = Color.Blue
        'My.Forms.AddWork.btn_Step2.BackColor = Color.Gray

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click



        My.Forms.Step2.MdiParent = My.Forms.AddWork
        My.Forms.Step2.WindowState = FormWindowState.Maximized
        My.Forms.Step2.Show()

        ' My.Forms.AddWork.btn_Step3.BackColor = Color.Gray
        'My.Forms.AddWork.btn_Step2.BackColor = Color.Blue

    End Sub


    Private Sub CheckedListBox1_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck


    End Sub

End Class