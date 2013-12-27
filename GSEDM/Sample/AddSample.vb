Imports EDM.Holmes.VO.Sample


Public Class AddSample


    Private sampName As String
    Private sampContent As String

    Public Property _SampName()
        Get
            Return Me.sampName
        End Get
        Set(ByVal value)
            Me.sampName = value
        End Set
    End Property

    Public Property _SampContent()
        Get
            Return Me.sampContent
        End Get
        Set(ByVal value)
            Me.sampContent = value
        End Set
    End Property


    Private Sub AddSample_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load




        Me.txt_SampName.Text = Me._SampName

        Me.txt_SampContent.Text = Me._SampContent

    End Sub


    ''' <summary>
    ''' 取消新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click

        Me.Close()

    End Sub


    ''' <summary>
    ''' 新增範本
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add.Click

        '2011/10/02
        'Dim sampName As String = Me.txt_SampName.Text.Trim

        'Dim sampContent As String = Me.txt_SampContent.Text.Trim

        'Dim samp As New Saple

        'samp._SampName = sampName
        'samp._SampContent = sampContent

        'samp.AddSamp(samp)



        'Me.Close()


    End Sub


    Private Sub txt_SampContent_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SampContent.TextChanged

        Me._SampContent = Me.txt_SampContent.Text.Trim

        'count input length
        Me.lbl_InputLength.Text = "字數" & Me.txt_SampContent.Text.Length


    End Sub


    Private Sub btn_Add_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Me._SampName = Me.txt_SampName.Text.Trim

    End Sub


  

End Class