<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddSample
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddSample))
        Me.lbl_SampName = New System.Windows.Forms.Label
        Me.lbl_SampContent = New System.Windows.Forms.Label
        Me.txt_SampName = New System.Windows.Forms.TextBox
        Me.txt_SampContent = New System.Windows.Forms.TextBox
        Me.btn_Cancel = New System.Windows.Forms.Button
        Me.lbl_InputLength = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btn_Add = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_SampName
        '
        Me.lbl_SampName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_SampName.AutoSize = True
        Me.lbl_SampName.Location = New System.Drawing.Point(23, 42)
        Me.lbl_SampName.Name = "lbl_SampName"
        Me.lbl_SampName.Size = New System.Drawing.Size(56, 12)
        Me.lbl_SampName.TabIndex = 0
        Me.lbl_SampName.Text = "範本名稱:"
        '
        'lbl_SampContent
        '
        Me.lbl_SampContent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_SampContent.AutoSize = True
        Me.lbl_SampContent.Location = New System.Drawing.Point(23, 87)
        Me.lbl_SampContent.Name = "lbl_SampContent"
        Me.lbl_SampContent.Size = New System.Drawing.Size(56, 12)
        Me.lbl_SampContent.TabIndex = 1
        Me.lbl_SampContent.Text = "範本內容:"
        '
        'txt_SampName
        '
        Me.txt_SampName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SampName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_SampName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_SampName.Location = New System.Drawing.Point(105, 39)
        Me.txt_SampName.Name = "txt_SampName"
        Me.txt_SampName.Size = New System.Drawing.Size(226, 22)
        Me.txt_SampName.TabIndex = 2
        '
        'txt_SampContent
        '
        Me.txt_SampContent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SampContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_SampContent.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txt_SampContent.Location = New System.Drawing.Point(105, 85)
        Me.txt_SampContent.Multiline = True
        Me.txt_SampContent.Name = "txt_SampContent"
        Me.txt_SampContent.Size = New System.Drawing.Size(226, 165)
        Me.txt_SampContent.TabIndex = 3
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Cancel.Location = New System.Drawing.Point(151, 0)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(75, 41)
        Me.btn_Cancel.TabIndex = 5
        Me.btn_Cancel.Text = "取消"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'lbl_InputLength
        '
        Me.lbl_InputLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_InputLength.AutoSize = True
        Me.lbl_InputLength.Location = New System.Drawing.Point(105, 268)
        Me.lbl_InputLength.Name = "lbl_InputLength"
        Me.lbl_InputLength.Size = New System.Drawing.Size(53, 12)
        Me.lbl_InputLength.TabIndex = 6
        Me.lbl_InputLength.Text = "顯示字數"
        Me.lbl_InputLength.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_Add)
        Me.Panel1.Controls.Add(Me.btn_Cancel)
        Me.Panel1.Location = New System.Drawing.Point(105, 283)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(226, 41)
        Me.Panel1.TabIndex = 7
        '
        'btn_Add
        '
        Me.btn_Add.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_Add.Location = New System.Drawing.Point(76, 0)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(75, 41)
        Me.btn_Add.TabIndex = 6
        Me.btn_Add.Text = "新增"
        Me.btn_Add.UseVisualStyleBackColor = True
        '
        'AddSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 329)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lbl_InputLength)
        Me.Controls.Add(Me.txt_SampContent)
        Me.Controls.Add(Me.txt_SampName)
        Me.Controls.Add(Me.lbl_SampContent)
        Me.Controls.Add(Me.lbl_SampName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AddSample"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "新增範本"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_SampName As System.Windows.Forms.Label
    Friend WithEvents lbl_SampContent As System.Windows.Forms.Label
    Friend WithEvents txt_SampName As System.Windows.Forms.TextBox
    Friend WithEvents txt_SampContent As System.Windows.Forms.TextBox
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbl_InputLength As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_Add As System.Windows.Forms.Button
End Class
