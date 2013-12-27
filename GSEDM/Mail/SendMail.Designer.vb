<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendMail
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
        Dim Label1 As System.Windows.Forms.Label
        Dim btn_ChooseCust As System.Windows.Forms.Button
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim SplitContainer1 As System.Windows.Forms.SplitContainer
        Dim Panel1 As System.Windows.Forms.Panel
        Dim SplitContainer2 As System.Windows.Forms.SplitContainer
        Dim ToolStrip2 As System.Windows.Forms.ToolStrip
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim ToolStripButton1 As System.Windows.Forms.ToolStripButton
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SendMail))
        Dim ToolStripButton2 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton3 As System.Windows.Forms.ToolStripButton
        Dim ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Dim lbl_SelectedSample As System.Windows.Forms.ToolStripLabel
        Me.chk_IsSendAllCust = New System.Windows.Forms.CheckBox
        Me.lbl_Img = New System.Windows.Forms.Label
        Me.lbl_CustPrjId = New System.Windows.Forms.Label
        Me.txt_Sender = New System.Windows.Forms.TextBox
        Me.txt_Receiver = New System.Windows.Forms.TextBox
        Me.txt_Title = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt_Attach = New System.Windows.Forms.TextBox
        Me.txt_Message = New System.Windows.Forms.TextBox
        Me.txt_Content = New System.Windows.Forms.TextBox
        Me.lbl_SelectedSampleName = New System.Windows.Forms.ToolStripLabel
        Me.lbl_SelectedImage = New System.Windows.Forms.ToolStripLabel
        Me.lbl_SelectedFile = New System.Windows.Forms.ToolStripLabel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btn_Send = New System.Windows.Forms.Button
        Me.btn_Close = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Label1 = New System.Windows.Forms.Label
        btn_ChooseCust = New System.Windows.Forms.Button
        Label2 = New System.Windows.Forms.Label
        Label3 = New System.Windows.Forms.Label
        Label9 = New System.Windows.Forms.Label
        SplitContainer1 = New System.Windows.Forms.SplitContainer
        Panel1 = New System.Windows.Forms.Panel
        SplitContainer2 = New System.Windows.Forms.SplitContainer
        ToolStrip2 = New System.Windows.Forms.ToolStrip
        ToolStrip1 = New System.Windows.Forms.ToolStrip
        ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        lbl_SelectedSample = New System.Windows.Forms.ToolStripLabel
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        Panel1.SuspendLayout()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        ToolStrip2.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Label1.Location = New System.Drawing.Point(23, 119)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(35, 12)
        Label1.TabIndex = 53
        Label1.Text = "標題:"
        '
        'btn_ChooseCust
        '
        btn_ChooseCust.Anchor = System.Windows.Forms.AnchorStyles.Right
        btn_ChooseCust.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        btn_ChooseCust.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        btn_ChooseCust.Location = New System.Drawing.Point(415, 79)
        btn_ChooseCust.Name = "btn_ChooseCust"
        btn_ChooseCust.Size = New System.Drawing.Size(97, 22)
        btn_ChooseCust.TabIndex = 52
        btn_ChooseCust.Text = "選擇通訊錄"
        btn_ChooseCust.UseVisualStyleBackColor = True
        AddHandler btn_ChooseCust.Click, AddressOf Me.btn_ChooseCust_Click
        '
        'Label2
        '
        Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Label2.Location = New System.Drawing.Point(11, 42)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(48, 12)
        Label2.TabIndex = 54
        Label2.Text = "寄件人:"
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Label3.Location = New System.Drawing.Point(10, 81)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(48, 12)
        Label3.TabIndex = 55
        Label3.Text = "收件者:"
        '
        'Label9
        '
        Label9.Location = New System.Drawing.Point(440, 119)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(13, 22)
        Label9.TabIndex = 57
        '
        'SplitContainer1
        '
        SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer1.Location = New System.Drawing.Point(0, 0)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        SplitContainer1.Panel1.Controls.Add(Panel1)
        '
        'SplitContainer1.Panel2
        '
        SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        SplitContainer1.Size = New System.Drawing.Size(576, 479)
        SplitContainer1.SplitterDistance = 423
        SplitContainer1.TabIndex = 2
        '
        'Panel1
        '
        Panel1.Controls.Add(SplitContainer2)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(574, 421)
        Panel1.TabIndex = 0
        '
        'SplitContainer2
        '
        SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer2.Location = New System.Drawing.Point(0, 0)
        SplitContainer2.Name = "SplitContainer2"
        SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        SplitContainer2.Panel1.Controls.Add(Me.chk_IsSendAllCust)
        SplitContainer2.Panel1.Controls.Add(Me.lbl_Img)
        SplitContainer2.Panel1.Controls.Add(Me.lbl_CustPrjId)
        SplitContainer2.Panel1.Controls.Add(Me.txt_Sender)
        SplitContainer2.Panel1.Controls.Add(Me.txt_Receiver)
        SplitContainer2.Panel1.Controls.Add(Me.txt_Title)
        SplitContainer2.Panel1.Controls.Add(Label9)
        SplitContainer2.Panel1.Controls.Add(Label3)
        SplitContainer2.Panel1.Controls.Add(Label2)
        SplitContainer2.Panel1.Controls.Add(Label1)
        SplitContainer2.Panel1.Controls.Add(btn_ChooseCust)
        '
        'SplitContainer2.Panel2
        '
        SplitContainer2.Panel2.Controls.Add(Me.Label5)
        SplitContainer2.Panel2.Controls.Add(Me.Label4)
        SplitContainer2.Panel2.Controls.Add(Me.Label6)
        SplitContainer2.Panel2.Controls.Add(Me.txt_Attach)
        SplitContainer2.Panel2.Controls.Add(Me.txt_Message)
        SplitContainer2.Panel2.Controls.Add(Me.txt_Content)
        SplitContainer2.Panel2.Controls.Add(ToolStrip2)
        SplitContainer2.Panel2.Controls.Add(ToolStrip1)
        SplitContainer2.Size = New System.Drawing.Size(574, 421)
        SplitContainer2.SplitterDistance = 173
        SplitContainer2.TabIndex = 41
        '
        'chk_IsSendAllCust
        '
        Me.chk_IsSendAllCust.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.chk_IsSendAllCust.AutoSize = True
        Me.chk_IsSendAllCust.Location = New System.Drawing.Point(415, 115)
        Me.chk_IsSendAllCust.Name = "chk_IsSendAllCust"
        Me.chk_IsSendAllCust.Size = New System.Drawing.Size(72, 16)
        Me.chk_IsSendAllCust.TabIndex = 65
        Me.chk_IsSendAllCust.Text = "所有客戶"
        Me.chk_IsSendAllCust.UseVisualStyleBackColor = True
        '
        'lbl_Img
        '
        Me.lbl_Img.AutoSize = True
        Me.lbl_Img.Location = New System.Drawing.Point(314, 146)
        Me.lbl_Img.Name = "lbl_Img"
        Me.lbl_Img.Size = New System.Drawing.Size(0, 12)
        Me.lbl_Img.TabIndex = 64
        Me.lbl_Img.Tag = "隱藏圖片HTML代碼"
        Me.lbl_Img.Visible = False
        '
        'lbl_CustPrjId
        '
        Me.lbl_CustPrjId.AutoSize = True
        Me.lbl_CustPrjId.Location = New System.Drawing.Point(335, 125)
        Me.lbl_CustPrjId.Name = "lbl_CustPrjId"
        Me.lbl_CustPrjId.Size = New System.Drawing.Size(0, 12)
        Me.lbl_CustPrjId.TabIndex = 63
        Me.lbl_CustPrjId.Visible = False
        '
        'txt_Sender
        '
        Me.txt_Sender.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Sender.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_Sender.Location = New System.Drawing.Point(85, 40)
        Me.txt_Sender.Name = "txt_Sender"
        Me.txt_Sender.Size = New System.Drawing.Size(307, 22)
        Me.txt_Sender.TabIndex = 1
        '
        'txt_Receiver
        '
        Me.txt_Receiver.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Receiver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Receiver.Enabled = False
        Me.txt_Receiver.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_Receiver.Location = New System.Drawing.Point(85, 79)
        Me.txt_Receiver.Name = "txt_Receiver"
        Me.txt_Receiver.Size = New System.Drawing.Size(307, 22)
        Me.txt_Receiver.TabIndex = 2
        '
        'txt_Title
        '
        Me.txt_Title.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Title.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_Title.Location = New System.Drawing.Point(85, 116)
        Me.txt_Title.Name = "txt_Title"
        Me.txt_Title.Size = New System.Drawing.Size(307, 22)
        Me.txt_Title.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "內文:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(141, 212)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 12)
        Me.Label4.TabIndex = 66
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(85, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "訊息"
        '
        'txt_Attach
        '
        Me.txt_Attach.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Attach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Attach.Location = New System.Drawing.Point(398, 187)
        Me.txt_Attach.Name = "txt_Attach"
        Me.txt_Attach.Size = New System.Drawing.Size(71, 22)
        Me.txt_Attach.TabIndex = 64
        Me.txt_Attach.Visible = False
        '
        'txt_Message
        '
        Me.txt_Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Message.Location = New System.Drawing.Point(87, 187)
        Me.txt_Message.Name = "txt_Message"
        Me.txt_Message.Size = New System.Drawing.Size(201, 22)
        Me.txt_Message.TabIndex = 63
        Me.txt_Message.Visible = False
        '
        'txt_Content
        '
        Me.txt_Content.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Content.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_Content.Location = New System.Drawing.Point(85, 62)
        Me.txt_Content.Multiline = True
        Me.txt_Content.Name = "txt_Content"
        Me.txt_Content.Size = New System.Drawing.Size(326, 108)
        Me.txt_Content.TabIndex = 4
        '
        'ToolStrip2
        '
        ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_SelectedSampleName, Me.lbl_SelectedImage, Me.lbl_SelectedFile})
        ToolStrip2.Location = New System.Drawing.Point(0, 25)
        ToolStrip2.Name = "ToolStrip2"
        ToolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        ToolStrip2.Size = New System.Drawing.Size(574, 25)
        ToolStrip2.TabIndex = 55
        ToolStrip2.Text = "ToolStrip2"
        '
        'lbl_SelectedSampleName
        '
        Me.lbl_SelectedSampleName.Name = "lbl_SelectedSampleName"
        Me.lbl_SelectedSampleName.Size = New System.Drawing.Size(71, 22)
        Me.lbl_SelectedSampleName.Text = "選擇的範本:"
        Me.lbl_SelectedSampleName.Visible = False
        '
        'lbl_SelectedImage
        '
        Me.lbl_SelectedImage.Name = "lbl_SelectedImage"
        Me.lbl_SelectedImage.Size = New System.Drawing.Size(71, 22)
        Me.lbl_SelectedImage.Text = "選擇的圖片:"
        Me.lbl_SelectedImage.Visible = False
        '
        'lbl_SelectedFile
        '
        Me.lbl_SelectedFile.Name = "lbl_SelectedFile"
        Me.lbl_SelectedFile.Size = New System.Drawing.Size(71, 22)
        Me.lbl_SelectedFile.Text = "選擇的檔案:"
        Me.lbl_SelectedFile.Visible = False
        '
        'ToolStrip1
        '
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripButton1, ToolStripButton2, ToolStripButton3, ToolStripSeparator1, lbl_SelectedSample})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        ToolStrip1.Size = New System.Drawing.Size(574, 25)
        ToolStrip1.TabIndex = 54
        ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New System.Drawing.Size(75, 22)
        ToolStripButton1.Text = "選擇範本"
        AddHandler ToolStripButton1.Click, AddressOf Me.ToolStripButton1_Click
        '
        'ToolStripButton2
        '
        ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New System.Drawing.Size(75, 22)
        ToolStripButton2.Text = "插入圖片"
        ToolStripButton2.Visible = False
        AddHandler ToolStripButton2.Click, AddressOf Me.ToolStripButton2_Click
        '
        'ToolStripButton3
        '
        ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton3.Name = "ToolStripButton3"
        ToolStripButton3.Size = New System.Drawing.Size(75, 22)
        ToolStripButton3.Text = "附加檔案"
        AddHandler ToolStripButton3.Click, AddressOf Me.ToolStripButton3_Click
        '
        'ToolStripSeparator1
        '
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lbl_SelectedSample
        '
        lbl_SelectedSample.ForeColor = System.Drawing.Color.Red
        lbl_SelectedSample.Name = "lbl_SelectedSample"
        lbl_SelectedSample.Size = New System.Drawing.Size(0, 22)
        lbl_SelectedSample.Tag = "選取的範本"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btn_Send)
        Me.Panel2.Controls.Add(Me.btn_Close)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(574, 50)
        Me.Panel2.TabIndex = 8
        '
        'btn_Send
        '
        Me.btn_Send.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Send.Location = New System.Drawing.Point(386, 0)
        Me.btn_Send.Name = "btn_Send"
        Me.btn_Send.Size = New System.Drawing.Size(94, 50)
        Me.btn_Send.TabIndex = 8
        Me.btn_Send.Text = "送出"
        Me.btn_Send.UseVisualStyleBackColor = True
        '
        'btn_Close
        '
        Me.btn_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Close.Location = New System.Drawing.Point(480, 0)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(94, 50)
        Me.btn_Close.TabIndex = 6
        Me.btn_Close.Text = "關閉"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'SendMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 479)
        Me.Controls.Add(SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SendMail"
        Me.Text = "寄信"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        SplitContainer1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel1.PerformLayout()
        SplitContainer2.Panel2.ResumeLayout(False)
        SplitContainer2.Panel2.PerformLayout()
        SplitContainer2.ResumeLayout(False)
        ToolStrip2.ResumeLayout(False)
        ToolStrip2.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txt_Title As System.Windows.Forms.TextBox
    Friend WithEvents txt_Sender As System.Windows.Forms.TextBox
    Friend WithEvents txt_Receiver As System.Windows.Forms.TextBox
    Friend WithEvents txt_Content As System.Windows.Forms.TextBox
    Friend WithEvents txt_Attach As System.Windows.Forms.TextBox
    Friend WithEvents txt_Message As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbl_CustPrjId As System.Windows.Forms.Label
    Friend WithEvents lbl_SelectedFile As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lbl_SelectedSampleName As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lbl_SelectedImage As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbl_Img As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chk_IsSendAllCust As System.Windows.Forms.CheckBox
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btn_Send As System.Windows.Forms.Button
End Class
