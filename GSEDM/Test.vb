

Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Data



Public Class Test



    Private Sub Test_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim hash As New Hashtable

        'hash.Add("�K�驱", 1)

        'Dim i As Integer = hash.Item("�K�驱")

        'If hash.ContainsKey("�K�驱") Then
        '    Dim j As Integer = hash.Item("�K�驱")
        '    j = j + 1
        '    hash.Item("�K�驱") = j
        '    MsgBox(j)
        'End If



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim text As String = Me.TextBox1.Text.Trim

        Dim index As Integer = text.IndexOf("�G")

        Dim j As String = text.Substring(index + 1)

        MsgBox(j)

        MsgBox(text)

        'Dim test As String = ""

        'Dim text As String = ""

        'Dim hash As New Hashtable

        'Dim sb As New StringBuilder("")

        'Dim myRegex As New Regex("^\w{3}\/[0-9]*\/�ǰe���\", RegexOptions.Multiline)

        'Dim msg2 As String() = text.Split(vbCrLf)

        'For Each msg3 As String In msg2

        '    '�P�_���L�ŦX�ۭq�榡,�H�������|��Exception�����~�T��
        '    If myRegex.IsMatch(msg3) Then

        '        msg3 = msg3.Trim

        '        Dim index As Integer = msg3.IndexOf("/")

        '        Dim shopname As String = msg3.Substring(0, index)

        '        '��X�ۭq�榡�������W��
        '        'Dim shopname As String = msg3.Substring(0, 3)

        '        '�Y�w�g��
        '        If hash.ContainsKey(shopname) Then
        '            Dim i As Integer = CInt(hash.Item(shopname))
        '            i = i + 1
        '            hash.Item(shopname) = i
        '        Else
        '            hash.Add(shopname, 1)
        '        End If
        '    End If


        'Next


        'Dim sb2 As New StringBuilder

        'For Each a As DictionaryEntry In hash

        '    sb2.Append(a.Key & ":" & a.Value & "��" & vbCrLf)

        'Next

        'Me.TextBox2.Text = sb2.ToString


    End Sub
End Class

Class T1

End Class

Class T2

End Class