<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddCat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddCat))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_CatName = New System.Windows.Forms.TextBox
        Me.btn_AddCat = New System.Windows.Forms.Button
        Me.btn_Close = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "新增標籤"
        '
        'txt_CatName
        '
        Me.txt_CatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_CatName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_CatName.Location = New System.Drawing.Point(29, 24)
        Me.txt_CatName.Name = "txt_CatName"
        Me.txt_CatName.Size = New System.Drawing.Size(235, 22)
        Me.txt_CatName.TabIndex = 1
        '
        'btn_AddCat
        '
        Me.btn_AddCat.Location = New System.Drawing.Point(100, 71)
        Me.btn_AddCat.Name = "btn_AddCat"
        Me.btn_AddCat.Size = New System.Drawing.Size(75, 23)
        Me.btn_AddCat.TabIndex = 2
        Me.btn_AddCat.Text = "新增"
        Me.btn_AddCat.UseVisualStyleBackColor = True
        '
        'btn_Close
        '
        Me.btn_Close.Location = New System.Drawing.Point(191, 71)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(75, 23)
        Me.btn_Close.TabIndex = 3
        Me.btn_Close.Text = "取消"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'AddCat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 106)
        Me.Controls.Add(Me.btn_Close)
        Me.Controls.Add(Me.btn_AddCat)
        Me.Controls.Add(Me.txt_CatName)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AddCat"
        Me.Text = "新增分類"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_CatName As System.Windows.Forms.TextBox
    Friend WithEvents btn_AddCat As System.Windows.Forms.Button
    Friend WithEvents btn_Close As System.Windows.Forms.Button
End Class
