Imports System.Windows.Forms
Imports EDM.Holmes.VO.Sample



Public Class IsSaveToWorkOrSamp

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    ''' <summary>
    ''' 取消按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 加入範本
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AddSamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddSamp.Click



      

    End Sub

    ''' <summary>
    ''' 加入排程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_AddWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddWork.Click


        'Dim custPrjId As String = My.Forms.SendMail._CustListId
        'My.Forms.AddWorkArrange._SelectedNoteId = custPrjId
        'My.Forms.AddWorkArrange._SelectedProject = My.Forms.SendMail._Mailto

        'My.Forms.AddWorkArrange.Show()


        'Me.Close()

    End Sub
End Class
