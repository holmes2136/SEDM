<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Step3_Week
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Num_SpecificMin = New System.Windows.Forms.NumericUpDown
        Me.Num_SpecificHour = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Num_EveryWeek = New System.Windows.Forms.NumericUpDown
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        CType(Me.Num_SpecificMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_SpecificHour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Num_EveryWeek, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button1.Location = New System.Drawing.Point(135, 246)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "上一步"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button2.Location = New System.Drawing.Point(229, 246)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "下一步"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "開始時間"
        '
        'Num_SpecificMin
        '
        Me.Num_SpecificMin.Location = New System.Drawing.Point(116, 44)
        Me.Num_SpecificMin.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.Num_SpecificMin.Name = "Num_SpecificMin"
        Me.Num_SpecificMin.Size = New System.Drawing.Size(67, 22)
        Me.Num_SpecificMin.TabIndex = 15
        '
        'Num_SpecificHour
        '
        Me.Num_SpecificHour.Location = New System.Drawing.Point(39, 44)
        Me.Num_SpecificHour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.Num_SpecificHour.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_SpecificHour.Name = "Num_SpecificHour"
        Me.Num_SpecificHour.Size = New System.Drawing.Size(69, 22)
        Me.Num_SpecificHour.TabIndex = 14
        Me.Num_SpecificHour.ThousandsSeparator = True
        Me.Num_SpecificHour.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(144, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(17, 12)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "周"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(37, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "每隔"
        '
        'Num_EveryWeek
        '
        Me.Num_EveryWeek.Location = New System.Drawing.Point(82, 84)
        Me.Num_EveryWeek.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Num_EveryWeek.Name = "Num_EveryWeek"
        Me.Num_EveryWeek.ReadOnly = True
        Me.Num_EveryWeek.Size = New System.Drawing.Size(56, 22)
        Me.Num_EveryWeek.TabIndex = 17
        Me.Num_EveryWeek.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(39, 124)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(265, 104)
        Me.CheckedListBox1.TabIndex = 19
        '
        'Step3_Week
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 292)
        Me.ControlBox = False
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Num_EveryWeek)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Num_SpecificMin)
        Me.Controls.Add(Me.Num_SpecificHour)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Step3_Week"
        CType(Me.Num_SpecificMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_SpecificHour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Num_EveryWeek, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Num_SpecificMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Num_SpecificHour As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Num_EveryWeek As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
End Class
