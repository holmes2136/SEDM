

Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Data



Public Class Test



    Private Sub Test_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim hash As New Hashtable

        'hash.Add("春日店", 1)

        'Dim i As Integer = hash.Item("春日店")

        'If hash.ContainsKey("春日店") Then
        '    Dim j As Integer = hash.Item("春日店")
        '    j = j + 1
        '    hash.Item("春日店") = j
        '    MsgBox(j)
        'End If



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim text As String = Me.TextBox1.Text.Trim

        Dim index As Integer = text.IndexOf("：")

        Dim j As String = text.Substring(index + 1)

        MsgBox(j)

        MsgBox(text)

        'Dim test As String = ""

        'Dim text As String = ""

        'Dim hash As New Hashtable

        'Dim sb As New StringBuilder("")

        'Dim myRegex As New Regex("^\w{3}\/[0-9]*\/傳送成功", RegexOptions.Multiline)

        'Dim msg2 As String() = text.Split(vbCrLf)

        'For Each msg3 As String In msg2

        '    '判斷有無符合自訂格式,以防中間會有Exception等錯誤訊息
        '    If myRegex.IsMatch(msg3) Then

        '        msg3 = msg3.Trim

        '        Dim index As Integer = msg3.IndexOf("/")

        '        Dim shopname As String = msg3.Substring(0, index)

        '        '找出自訂格式的分店名稱
        '        'Dim shopname As String = msg3.Substring(0, 3)

        '        '若已經有
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

        '    sb2.Append(a.Key & ":" & a.Value & "筆" & vbCrLf)

        'Next

        'Me.TextBox2.Text = sb2.ToString


    End Sub
End Class

Class T1

End Class

Class T2

End Class