<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WebMailSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebMailSetting))
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rad_yahoo = New System.Windows.Forms.RadioButton
        Me.rad_Gmail = New System.Windows.Forms.RadioButton
        Me.rad_hotmail = New System.Windows.Forms.RadioButton
        Me.txt_MailPwd = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt_MailAccount = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.btn_EnsureMailSetting = New System.Windows.Forms.Button
        Me.btn_Close = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox4.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.SplitContainer1)
        Me.GroupBox4.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(41, 35)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(355, 147)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "寄信方式"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 18)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rad_yahoo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rad_Gmail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rad_hotmail)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt_MailPwd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt_MailAccount)
        Me.SplitContainer1.Size = New System.Drawing.Size(349, 126)
        Me.SplitContainer1.SplitterDistance = 114
        Me.SplitContainer1.TabIndex = 0
        '
        'rad_yahoo
        '
        Me.rad_yahoo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rad_yahoo.AutoSize = True
        Me.rad_yahoo.Location = New System.Drawing.Point(19, 69)
        Me.rad_yahoo.Name = "rad_yahoo"
        Me.rad_yahoo.Size = New System.Drawing.Size(75, 16)
        Me.rad_yahoo.TabIndex = 2
        Me.rad_yahoo.Tag = "yahoo"
        Me.rad_yahoo.Text = "奇摩信箱"
        Me.rad_yahoo.UseVisualStyleBackColor = True
        '
        'rad_Gmail
        '
        Me.rad_Gmail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rad_Gmail.AutoSize = True
        Me.rad_Gmail.Location = New System.Drawing.Point(19, 21)
        Me.rad_Gmail.Name = "rad_Gmail"
        Me.rad_Gmail.Size = New System.Drawing.Size(56, 16)
        Me.rad_Gmail.TabIndex = 0
        Me.rad_Gmail.Tag = "gmail"
        Me.rad_Gmail.Text = "Gmail"
        Me.rad_Gmail.UseVisualStyleBackColor = True
        '
        'rad_hotmail
        '
        Me.rad_hotmail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rad_hotmail.AutoSize = True
        Me.rad_hotmail.Location = New System.Drawing.Point(19, 43)
        Me.rad_hotmail.Name = "rad_hotmail"
        Me.rad_hotmail.Size = New System.Drawing.Size(68, 16)
        Me.rad_hotmail.TabIndex = 1
        Me.rad_hotmail.Tag = "hotmail"
        Me.rad_hotmail.Text = "HotMail"
        Me.rad_hotmail.UseVisualStyleBackColor = True
        '
        'txt_MailPwd
        '
        Me.txt_MailPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_MailPwd.Location = New System.Drawing.Point(55, 67)
        Me.txt_MailPwd.Name = "txt_MailPwd"
        Me.txt_MailPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_MailPwd.Size = New System.Drawing.Size(133, 22)
        Me.txt_MailPwd.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "帳號:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 12)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "密碼:"
        '
        'txt_MailAccount
        '
        Me.txt_MailAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_MailAccount.Location = New System.Drawing.Point(55, 21)
        Me.txt_MailAccount.Name = "txt_MailAccount"
        Me.txt_MailAccount.Size = New System.Drawing.Size(133, 22)
        Me.txt_MailAccount.TabIndex = 3
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(214, 122)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 22)
        Me.TextBox2.TabIndex = 9
        '
        'btn_EnsureMailSetting
        '
        Me.btn_EnsureMailSetting.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_EnsureMailSetting.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_EnsureMailSetting.Location = New System.Drawing.Point(202, 0)
        Me.btn_EnsureMailSetting.Name = "btn_EnsureMailSetting"
        Me.btn_EnsureMailSetting.Size = New System.Drawing.Size(75, 53)
        Me.btn_EnsureMailSetting.TabIndex = 5
        Me.btn_EnsureMailSetting.Text = "套用設定"
        Me.btn_EnsureMailSetting.UseVisualStyleBackColor = True
        '
        'btn_Close
        '
        Me.btn_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Close.Location = New System.Drawing.Point(277, 0)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(75, 53)
        Me.btn_Close.TabIndex = 6
        Me.btn_Close.Text = "關閉"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_EnsureMailSetting)
        Me.Panel1.Controls.Add(Me.btn_Close)
        Me.Panel1.Location = New System.Drawing.Point(41, 208)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(352, 53)
        Me.Panel1.TabIndex = 11
        '
        'WebMailSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(441, 273)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.TextBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WebMailSetting"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "網路信箱設定"
        Me.GroupBox4.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rad_yahoo As System.Windows.Forms.RadioButton
    Friend WithEvents rad_Gmail As System.Windows.Forms.RadioButton
    Friend WithEvents rad_hotmail As System.Windows.Forms.RadioButton
    Friend WithEvents txt_MailPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_MailAccount As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btn_EnsureMailSetting As System.Windows.Forms.Button
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
