<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Step2
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
        Me.rad_EveryDay = New System.Windows.Forms.RadioButton
        Me.rad_EveryMonth = New System.Windows.Forms.RadioButton
        Me.rad_EveryWeek = New System.Windows.Forms.RadioButton
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'rad_EveryDay
        '
        Me.rad_EveryDay.AutoSize = True
        Me.rad_EveryDay.Location = New System.Drawing.Point(35, 26)
        Me.rad_EveryDay.Margin = New System.Windows.Forms.Padding(10)
        Me.rad_EveryDay.Name = "rad_EveryDay"
        Me.rad_EveryDay.Size = New System.Drawing.Size(47, 16)
        Me.rad_EveryDay.TabIndex = 0
        Me.rad_EveryDay.TabStop = True
        Me.rad_EveryDay.Text = "每天"
        Me.rad_EveryDay.UseVisualStyleBackColor = True
        '
        'rad_EveryMonth
        '
        Me.rad_EveryMonth.AutoSize = True
        Me.rad_EveryMonth.Location = New System.Drawing.Point(35, 95)
        Me.rad_EveryMonth.Margin = New System.Windows.Forms.Padding(10)
        Me.rad_EveryMonth.Name = "rad_EveryMonth"
        Me.rad_EveryMonth.Size = New System.Drawing.Size(47, 16)
        Me.rad_EveryMonth.TabIndex = 1
        Me.rad_EveryMonth.TabStop = True
        Me.rad_EveryMonth.Text = "每月"
        Me.rad_EveryMonth.UseVisualStyleBackColor = True
        '
        'rad_EveryWeek
        '
        Me.rad_EveryWeek.AutoSize = True
        Me.rad_EveryWeek.Location = New System.Drawing.Point(35, 59)
        Me.rad_EveryWeek.Margin = New System.Windows.Forms.Padding(10)
        Me.rad_EveryWeek.Name = "rad_EveryWeek"
        Me.rad_EveryWeek.Size = New System.Drawing.Size(47, 16)
        Me.rad_EveryWeek.TabIndex = 2
        Me.rad_EveryWeek.TabStop = True
        Me.rad_EveryWeek.Text = "每週"
        Me.rad_EveryWeek.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(184, 215)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "下一步"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(88, 215)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "上一步"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 142)
        Me.Label1.Margin = New System.Windows.Forms.Padding(10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(191, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "P.S.若需要排程特定日期請設定每月"
        '
        'Step2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.rad_EveryWeek)
        Me.Controls.Add(Me.rad_EveryMonth)
        Me.Controls.Add(Me.rad_EveryDay)
        Me.Name = "Step2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rad_EveryDay As System.Windows.Forms.RadioButton
    Friend WithEvents rad_EveryMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rad_EveryWeek As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
