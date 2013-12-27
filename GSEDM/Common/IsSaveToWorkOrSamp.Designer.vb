<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IsSaveToWorkOrSamp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IsSaveToWorkOrSamp))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.btn_AddSamp = New System.Windows.Forms.Button
        Me.btn_AddWork = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.lbl_Message = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_AddWork, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_AddSamp, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(108, 65)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(280, 27)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btn_AddSamp
        '
        Me.btn_AddSamp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_AddSamp.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_AddSamp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_AddSamp.Location = New System.Drawing.Point(13, 3)
        Me.btn_AddSamp.Name = "btn_AddSamp"
        Me.btn_AddSamp.Size = New System.Drawing.Size(66, 21)
        Me.btn_AddSamp.TabIndex = 3
        Me.btn_AddSamp.Text = "加入範本"
        '
        'btn_AddWork
        '
        Me.btn_AddWork.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_AddWork.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_AddWork.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_AddWork.Location = New System.Drawing.Point(199, 3)
        Me.btn_AddWork.Name = "btn_AddWork"
        Me.btn_AddWork.Size = New System.Drawing.Size(67, 21)
        Me.btn_AddWork.TabIndex = 2
        Me.btn_AddWork.Text = "加入排程"
        Me.btn_AddWork.Visible = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Cancel_Button.Location = New System.Drawing.Point(106, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "取消"
        '
        'lbl_Message
        '
        Me.lbl_Message.AutoSize = True
        Me.lbl_Message.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lbl_Message.Location = New System.Drawing.Point(49, 27)
        Me.lbl_Message.Name = "lbl_Message"
        Me.lbl_Message.Size = New System.Drawing.Size(167, 12)
        Me.lbl_Message.TabIndex = 1
        Me.lbl_Message.Text = "是否加入工作排程或者範本?"
        '
        'IsSaveToWorkOrSamp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(400, 103)
        Me.Controls.Add(Me.lbl_Message)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IsSaveToWorkOrSamp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "是否加入工作排程或者範本?"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btn_AddSamp As System.Windows.Forms.Button
    Friend WithEvents btn_AddWork As System.Windows.Forms.Button
    Friend WithEvents lbl_Message As System.Windows.Forms.Label

End Class
