


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
    ''' ex:�C�T��,�榡�ưT��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property _FriendlyEvery1()
        Get
            '�Y����C��,�h�b�̫᧹����ܰT����,�h���C��,�_�h�|�X�{�C�C�몺�_�Ǧr��
            Return IIf(Me.CheckedListBox2.SelectedItem = "�C��", Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "��", "�C" & Me.CheckedListBox2.SelectedItem.ToString & Me.Num_SpecificDay.Value & "��")

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
    ''' �U�@�B
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