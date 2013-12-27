
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.Project

Public Class AddProject2


    Private AddPrj2_projectName As String

    '是否新增專案
    'Public AddPrj2_flag As Boolean

    Public Property _AddPrj2_projectName()
        Get
            'Return Me.AddPrj2_projectName
            Return Me.txt_PrjName.Text.Trim
        End Get
        Set(ByVal value)
            Me.AddPrj2_projectName = value
        End Set
    End Property

    'Public Property _AddPrj2_flag()
    '    Get
    '        Return Me.AddPrj2_flag
    '    End Get
    '    Set(ByVal value)
    '        Me.AddPrj2_flag = value
    '    End Set
    'End Property



    Private Sub AddProject2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load




    End Sub

    ''' <summary>
    ''' 取消按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click


        Me.Close()

    End Sub


    ''' <summary>
    ''' 新增專案
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click

        'Dim prjName As String = Me.txt_PrjName.Text.Trim

        'If prjName = "" Then
        '    MsgBox("請輸入專案名稱", MsgBoxStyle.Information)
        '    Exit Sub
        'End If


        'Dim prj As New Project

        'Try
        '    prj.addPrj(prjName)
        'Catch ex As Exception
        '    Throw New ArgumentException("Error_Code:63_AddProject2")
        'End Try

        'My.Forms.CustList.BuildTree()



        'Me.Close()



    End Sub

   
End Class