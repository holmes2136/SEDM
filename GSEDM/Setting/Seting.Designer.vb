<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Seting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Seting))
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.btn_UsuallySetting = New System.Windows.Forms.ToolStripButton
        Me.btn_WebMailSetting = New System.Windows.Forms.ToolStripButton
        Me.btn_SMSSetting = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_UsuallySetting, Me.btn_WebMailSetting, Me.btn_SMSSetting})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(692, 25)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip"
        '
        'btn_UsuallySetting
        '
        Me.btn_UsuallySetting.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btn_UsuallySetting.Image = CType(resources.GetObject("btn_UsuallySetting.Image"), System.Drawing.Image)
        Me.btn_UsuallySetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_UsuallySetting.Name = "btn_UsuallySetting"
        Me.btn_UsuallySetting.Size = New System.Drawing.Size(79, 22)
        Me.btn_UsuallySetting.Text = "常用設定"
        '
        'btn_WebMailSetting
        '
        Me.btn_WebMailSetting.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btn_WebMailSetting.Image = CType(resources.GetObject("btn_WebMailSetting.Image"), System.Drawing.Image)
        Me.btn_WebMailSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_WebMailSetting.Name = "btn_WebMailSetting"
        Me.btn_WebMailSetting.Size = New System.Drawing.Size(105, 22)
        Me.btn_WebMailSetting.Text = "網路信箱設定"
        '
        'btn_SMSSetting
        '
        Me.btn_SMSSetting.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btn_SMSSetting.Image = CType(resources.GetObject("btn_SMSSetting.Image"), System.Drawing.Image)
        Me.btn_SMSSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_SMSSetting.Name = "btn_SMSSetting"
        Me.btn_SMSSetting.Size = New System.Drawing.Size(79, 22)
        Me.btn_SMSSetting.Text = "簡訊設定"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 451)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(692, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(29, 17)
        Me.ToolStripStatusLabel.Text = "狀態"
        '
        'Seting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 473)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "Seting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "設定"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btn_UsuallySetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_WebMailSetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_SMSSetting As System.Windows.Forms.ToolStripButton

End Class
