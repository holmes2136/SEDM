Imports Microsoft.Win32.TaskScheduler
Imports System.Data
Imports System.Data.OleDb
Imports EDM.Holmes.DB



Public Class WorkInfo



    Private Sub WorkInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        refreshData()

    End Sub

    ''' <summary>
    ''' ��s�M��
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Overloads Sub refreshData()



        Dim dt As DataTable

        Dim conn As OleDbConnection = HolmesConn.getConnection

        Dim cmd As New OleDbCommand

        cmd.CommandText = "select * from �Ƶ{���v�O�� order by StartTime Desc "

        cmd.Connection = conn

        conn.Open()

        Using conn

            dt = New DataTable
            dt.Load(cmd.ExecuteReader)


        End Using

        Me.BindingSource1.DataSource = dt

        Me.BindingNavigator1.BindingSource = Me.BindingSource1

        Me.DataGridView1.DataSource = Me.BindingSource1


        Me.DataGridView1.Columns("Serial").Visible = False
        Me.DataGridView1.Columns("PrjNoteId").Visible = False
        Me.DataGridView1.Columns("PrjName").Visible = False
        Me.DataGridView1.Columns("StartTime").Visible = True
        Me.DataGridView1.Columns("StartTime").HeaderText = "�}�l����ɶ�"
        Me.DataGridView1.Columns("EndTime").Visible = True
        Me.DataGridView1.Columns("EndTime").HeaderText = "�����ɶ�"

        Me.DataGridView1.Columns("SuccessCnt").Visible = True
        Me.DataGridView1.Columns("SuccessCnt").HeaderText = "���\����"
        Me.DataGridView1.Columns("FailCnt").Visible = True
        Me.DataGridView1.Columns("FailCnt").HeaderText = "���ѵ���"
        Me.DataGridView1.Columns("BpType").Visible = True
        Me.DataGridView1.Columns("BpType").HeaderText = "�H������"


    End Sub


    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.Close()

    End Sub

  
End Class