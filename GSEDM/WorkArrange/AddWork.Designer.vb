<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddWork
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddWork))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btn_Step1 = New System.Windows.Forms.ToolStripButton
        Me.btn_Step2 = New System.Windows.Forms.ToolStripButton
        Me.btn_Step3 = New System.Windows.Forms.ToolStripButton
        Me.btn_Step4 = New System.Windows.Forms.ToolStripButton
        Me.btn_Step5 = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 551)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(31, 17)
        Me.ToolStripStatusLabel.Text = "狀態"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_Step1, Me.btn_Step2, Me.btn_Step3, Me.btn_Step4, Me.btn_Step5})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(99, 551)
        Me.ToolStrip1.TabIndex = 9
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btn_Step1
        '
        Me.btn_Step1.AutoSize = False
        Me.btn_Step1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_Step1.Enabled = False
        Me.btn_Step1.Image = CType(resources.GetObject("btn_Step1.Image"), System.Drawing.Image)
        Me.btn_Step1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Step1.Name = "btn_Step1"
        Me.btn_Step1.Size = New System.Drawing.Size(97, 30)
        Me.btn_Step1.Text = "建立基本工作"
        '
        'btn_Step2
        '
        Me.btn_Step2.AutoSize = False
        Me.btn_Step2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_Step2.Enabled = False
        Me.btn_Step2.Image = CType(resources.GetObject("btn_Step2.Image"), System.Drawing.Image)
        Me.btn_Step2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Step2.Name = "btn_Step2"
        Me.btn_Step2.Size = New System.Drawing.Size(97, 30)
        Me.btn_Step2.Text = "何時觸發"
        '
        'btn_Step3
        '
        Me.btn_Step3.AutoSize = False
        Me.btn_Step3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_Step3.Enabled = False
        Me.btn_Step3.Image = CType(resources.GetObject("btn_Step3.Image"), System.Drawing.Image)
        Me.btn_Step3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Step3.Name = "btn_Step3"
        Me.btn_Step3.Size = New System.Drawing.Size(97, 30)
        Me.btn_Step3.Text = "選擇排程類型"
        '
        'btn_Step4
        '
        Me.btn_Step4.AutoSize = False
        Me.btn_Step4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_Step4.Enabled = False
        Me.btn_Step4.Image = CType(resources.GetObject("btn_Step4.Image"), System.Drawing.Image)
        Me.btn_Step4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Step4.Name = "btn_Step4"
        Me.btn_Step4.Size = New System.Drawing.Size(97, 30)
        Me.btn_Step4.Text = "工作內容"
        '
        'btn_Step5
        '
        Me.btn_Step5.AutoSize = False
        Me.btn_Step5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_Step5.Enabled = False
        Me.btn_Step5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btn_Step5.Image = CType(resources.GetObject("btn_Step5.Image"), System.Drawing.Image)
        Me.btn_Step5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Step5.Name = "btn_Step5"
        Me.btn_Step5.Size = New System.Drawing.Size(97, 30)
        Me.btn_Step5.Text = "執行"
        '
        'AddWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "AddWork"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "新增排程"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btn_Step1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_Step2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_Step5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_Step3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_Step4 As System.Windows.Forms.ToolStripButton

End Class
