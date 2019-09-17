<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackSize_Popup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.cboSizeBox = New System.Windows.Forms.ComboBox
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(12, 107)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(260, 50)
        Me.btnConfirm.TabIndex = 3
        Me.btnConfirm.Text = "ยืนยัน"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'cboSizeBox
        '
        Me.cboSizeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSizeBox.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cboSizeBox.FormattingEnabled = True
        Me.cboSizeBox.Location = New System.Drawing.Point(12, 57)
        Me.cboSizeBox.Name = "cboSizeBox"
        Me.cboSizeBox.Size = New System.Drawing.Size(260, 47)
        Me.cboSizeBox.TabIndex = 11
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.SystemColors.Info
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(11, 9)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(261, 44)
        Me.txtBarcode.TabIndex = 12
        '
        'frmPackSize_Popup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 164)
        Me.Controls.Add(Me.txtBarcode)
        Me.Controls.Add(Me.cboSizeBox)
        Me.Controls.Add(Me.btnConfirm)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPackSize_Popup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ขนาดกล่อง"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents cboSizeBox As System.Windows.Forms.ComboBox
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
End Class
