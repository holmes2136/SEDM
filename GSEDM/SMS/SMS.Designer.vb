<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SMS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SMS))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.panel_Tools = New System.Windows.Forms.Panel
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.progress = New System.Windows.Forms.ToolStripProgressBar
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btn_Send = New System.Windows.Forms.Button
        Me.btn_Cancel = New System.Windows.Forms.Button
        Me.lbl_PrjId = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_SampName = New System.Windows.Forms.Label
        Me.lbl_PrjName = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btn_ShowPrj = New System.Windows.Forms.ToolStripButton
        Me.lbl_CustCnt = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btn_AllCust = New System.Windows.Forms.ToolStripButton
        Me.btn_ChooseSamp = New System.Windows.Forms.ToolStripButton
        Me.btn_ClearContact = New System.Windows.Forms.ToolStripButton
        Me.lbl_Strlength = New System.Windows.Forms.Label
        Me.txt_Content = New System.Windows.Forms.TextBox
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Panel1.SuspendLayout()
        Me.panel_Tools.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.panel_Tools)
        Me.Panel1.Controls.Add(Me.StatusStrip1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.lbl_PrjId)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(692, 573)
        Me.Panel1.TabIndex = 1
        '
        'panel_Tools
        '
        Me.panel_Tools.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel_Tools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel_Tools.Controls.Add(Me.ToolStrip2)
        Me.panel_Tools.Location = New System.Drawing.Point(37, 22)
        Me.panel_Tools.Name = "panel_Tools"
        Me.panel_Tools.Size = New System.Drawing.Size(629, 58)
        Me.panel_Tools.TabIndex = 10
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip2.Size = New System.Drawing.Size(627, 56)
        Me.ToolStrip2.TabIndex = 9
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(129, 53)
        Me.ToolStripButton1.Text = "查詢歷史記錄"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.progress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 549)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(690, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(29, 17)
        Me.ToolStripStatusLabel1.Text = "進度"
        '
        'progress
        '
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(100, 16)
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.btn_Send)
        Me.Panel2.Controls.Add(Me.btn_Cancel)
        Me.Panel2.Location = New System.Drawing.Point(50, 499)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(594, 37)
        Me.Panel2.TabIndex = 7
        '
        'btn_Send
        '
        Me.btn_Send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_Send.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Send.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Send.Location = New System.Drawing.Point(326, 0)
        Me.btn_Send.Name = "btn_Send"
        Me.btn_Send.Size = New System.Drawing.Size(134, 37)
        Me.btn_Send.TabIndex = 1
        Me.btn_Send.Text = "步驟3:簡訊發送"
        Me.btn_Send.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        Me.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Cancel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.Location = New System.Drawing.Point(460, 0)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(134, 37)
        Me.btn_Cancel.TabIndex = 2
        Me.btn_Cancel.Text = "取消"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'lbl_PrjId
        '
        Me.lbl_PrjId.AutoSize = True
        Me.lbl_PrjId.Location = New System.Drawing.Point(312, 30)
        Me.lbl_PrjId.Name = "lbl_PrjId"
        Me.lbl_PrjId.Size = New System.Drawing.Size(0, 12)
        Me.lbl_PrjId.TabIndex = 6
        Me.lbl_PrjId.Tag = "專案ID"
        Me.lbl_PrjId.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lbl_SampName)
        Me.GroupBox1.Controls.Add(Me.lbl_PrjName)
        Me.GroupBox1.Controls.Add(Me.ToolStrip1)
        Me.GroupBox1.Controls.Add(Me.lbl_Strlength)
        Me.GroupBox1.Controls.Add(Me.txt_Content)
        Me.GroupBox1.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(34, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(635, 405)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "發送內容設定"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("PMingLiU", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "步驟2:輸入以下簡訊內容或選擇範本"
        '
        'lbl_SampName
        '
        Me.lbl_SampName.AutoEllipsis = True
        Me.lbl_SampName.AutoSize = True
        Me.lbl_SampName.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl_SampName.Location = New System.Drawing.Point(24, 80)
        Me.lbl_SampName.Name = "lbl_SampName"
        Me.lbl_SampName.Size = New System.Drawing.Size(122, 12)
        Me.lbl_SampName.TabIndex = 6
        Me.lbl_SampName.Text = "顯示選擇的範本名稱"
        '
        'lbl_PrjName
        '
        Me.lbl_PrjName.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl_PrjName.Location = New System.Drawing.Point(24, 64)
        Me.lbl_PrjName.Name = "lbl_PrjName"
        Me.lbl_PrjName.Size = New System.Drawing.Size(537, 10)
        Me.lbl_PrjName.TabIndex = 5
        Me.lbl_PrjName.Text = "顯示選擇的專案名稱"
        Me.lbl_PrjName.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_ShowPrj, Me.ToolStripButton2, Me.lbl_CustCnt, Me.ToolStripSeparator2, Me.btn_AllCust, Me.btn_ChooseSamp, Me.btn_ClearContact})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 18)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(629, 46)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btn_ShowPrj
        '
        Me.btn_ShowPrj.Image = CType(resources.GetObject("btn_ShowPrj.Image"), System.Drawing.Image)
        Me.btn_ShowPrj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_ShowPrj.Name = "btn_ShowPrj"
        Me.btn_ShowPrj.Size = New System.Drawing.Size(85, 43)
        Me.btn_ShowPrj.Tag = ""
        Me.btn_ShowPrj.Text = "選擇通訊錄"
        Me.btn_ShowPrj.Visible = False
        '
        'lbl_CustCnt
        '
        Me.lbl_CustCnt.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_CustCnt.Name = "lbl_CustCnt"
        Me.lbl_CustCnt.Size = New System.Drawing.Size(109, 43)
        Me.lbl_CustCnt.Text = "顯示選取客戶數目"
        Me.lbl_CustCnt.Visible = False
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("PMingLiU", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(184, 43)
        Me.ToolStripButton2.Text = "步驟1:加入客戶清單"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 46)
        '
        'btn_AllCust
        '
        Me.btn_AllCust.CheckOnClick = True
        Me.btn_AllCust.Image = CType(resources.GetObject("btn_AllCust.Image"), System.Drawing.Image)
        Me.btn_AllCust.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_AllCust.Name = "btn_AllCust"
        Me.btn_AllCust.Size = New System.Drawing.Size(133, 43)
        Me.btn_AllCust.Text = "點選為選擇所有客戶"
        Me.btn_AllCust.Visible = False
        '
        'btn_ChooseSamp
        '
        Me.btn_ChooseSamp.Font = New System.Drawing.Font("PMingLiU", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_ChooseSamp.Image = CType(resources.GetObject("btn_ChooseSamp.Image"), System.Drawing.Image)
        Me.btn_ChooseSamp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btn_ChooseSamp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_ChooseSamp.Name = "btn_ChooseSamp"
        Me.btn_ChooseSamp.Size = New System.Drawing.Size(93, 43)
        Me.btn_ChooseSamp.Text = "選擇範本"
        '
        'btn_ClearContact
        '
        Me.btn_ClearContact.Image = CType(resources.GetObject("btn_ClearContact.Image"), System.Drawing.Image)
        Me.btn_ClearContact.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_ClearContact.Name = "btn_ClearContact"
        Me.btn_ClearContact.Size = New System.Drawing.Size(97, 43)
        Me.btn_ClearContact.Text = "清除連絡清單"
        Me.btn_ClearContact.Visible = False
        '
        'lbl_Strlength
        '
        Me.lbl_Strlength.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_Strlength.AutoSize = True
        Me.lbl_Strlength.Location = New System.Drawing.Point(39, 382)
        Me.lbl_Strlength.Name = "lbl_Strlength"
        Me.lbl_Strlength.Size = New System.Drawing.Size(0, 12)
        Me.lbl_Strlength.TabIndex = 4
        Me.lbl_Strlength.Tag = "字數長度"
        '
        'txt_Content
        '
        Me.txt_Content.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Content.Location = New System.Drawing.Point(21, 107)
        Me.txt_Content.Multiline = True
        Me.txt_Content.Name = "txt_Content"
        Me.txt_Content.Size = New System.Drawing.Size(584, 272)
        Me.txt_Content.TabIndex = 0
        '
        'BackgroundWorker1
        '
        '
        'SMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 573)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SMS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "簡訊發送"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panel_Tools.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_Send As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Content As System.Windows.Forms.TextBox
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbl_Strlength As System.Windows.Forms.Label
    Friend WithEvents lbl_PrjId As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btn_ShowPrj As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_ChooseSamp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btn_AllCust As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbl_SampName As System.Windows.Forms.Label
    Friend WithEvents lbl_PrjName As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btn_ClearContact As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents progress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lbl_CustCnt As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents panel_Tools As System.Windows.Forms.Panel
End Class
