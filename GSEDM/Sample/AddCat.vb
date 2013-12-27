Public Class AddCat

    Private catName As String

    Public Property _CatName()
        Get
            Return Me.catName
        End Get
        Set(ByVal value)
            Me.catName = value
        End Set
    End Property
    ''' <summary>
    ''' 關閉視窗
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        Me.Close()

    End Sub


    ''' <summary>
    ''' 新增分類
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AddCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddCat.Click

        If Me.txt_CatName.Text = "" Then

            MsgBox("請輸入名稱")
            Exit Sub

        End If

    End Sub

    Private Sub txt_CatName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_CatName.TextChanged


        Me._CatName = Me.txt_CatName.Text.Trim

    End Sub
End Class