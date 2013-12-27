
Imports System.Configuration
Imports System.Data
Imports System.Net.NetworkInformation
Imports Crypt
Imports System.Net
Imports KOTSMSAPI.KOT_SMSAPI
Imports EDM.Holmes.VO.User
Imports EDM.Holmes.Tools


Public NotInheritable Class About


    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '設定序號----------------------------------
        '序號分店ID產生問題?
        Dim shopId As String = ConfigurationManager.AppSettings("ShopId").ToString

        getShopId2(shopId)

        Dim decode As String = GenerateNumber()


        Me.lbl_Serial.Text = decode
        '設定序號----------------------------



        ' 設定表單的標題。
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("關於 {0}", ApplicationTitle)
        ' 初始化 [關於] 對話方塊中顯示的所有文字。
        ' TODO: 在專案屬性對話方塊 (位於 [專案] 功能表下) 的 [應用程式] 窗格中，
        '    自訂應用程式的組件資訊。
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("版本 {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description


    End Sub



    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub


    ''' <summary>
    ''' 取得使用中的IP
    ''' </summary>
    ''' <param name="ShopId"></param>
    ''' <remarks></remarks>
    Private Function getShopId2(ByVal ShopId As String) As String

        Dim ip As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(Dns.GetHostName)
        Dim i() As System.Net.IPAddress

        i = ip.AddressList()

        Return i(0).ToString


    End Function

    ''' <summary>
    ''' 將MAC位址加密
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateNumber() As String

        '抓取分店IP
        'Dim ip As String = getShopIp("a012")

        '取得正在使用的IP位址
        'Dim ip As String = getShopId2("a012")

        '抓取分店網卡
        Dim tools As New Tool
        Dim macAddres As String = tools.getMacAddress

        '組合字串
        '加密
        Dim Encode As String = Crypt.Crypt.Encrypt(macAddres, "holmesss")

        Return Encode


    End Function

    ''' <summary>
    ''' 取得MAC位址
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMacAddress() As String

        Dim macAddress As String = ""

        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()


        Dim adapter As NetworkInterface

        For Each adapter In adapters
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()

            macAddress = adapter.GetPhysicalAddress.ToString
            'MsgBox(adapter.Description)
            'MsgBox(adapter.GetPhysicalAddress.ToString)

            Return macAddress

        Next adapter

        Return macAddress

    End Function


End Class
