<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopupConfirmQty
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.lblQty = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.txtConfirmQty = New System.Windows.Forms.TextBox
        Me.lblSku = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(325, 120)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(124, 35)
        Me.btnClose.TabIndex = 17
        Me.btnClose.Text = "ปิด"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(88, 90)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQty.MaxLength = 50
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(135, 22)
        Me.txtQty.TabIndex = 18
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQty.Location = New System.Drawing.Point(29, 92)
        Me.lblQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(51, 17)
        Me.lblQty.TabIndex = 19
        Me.lblQty.Text = "จำนวน"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnOK
        '
        Me.btnOK.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.Location = New System.Drawing.Point(13, 120)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(124, 35)
        Me.btnOK.TabIndex = 20
        Me.btnOK.Text = "ตกลง"
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtConfirmQty
        '
        Me.txtConfirmQty.Location = New System.Drawing.Point(225, 90)
        Me.txtConfirmQty.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConfirmQty.MaxLength = 50
        Me.txtConfirmQty.Name = "txtConfirmQty"
        Me.txtConfirmQty.ReadOnly = True
        Me.txtConfirmQty.Size = New System.Drawing.Size(226, 22)
        Me.txtConfirmQty.TabIndex = 21
        '
        'lblSku
        '
        Me.lblSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku.ForeColor = System.Drawing.Color.Red
        Me.lblSku.Location = New System.Drawing.Point(13, 6)
        Me.lblSku.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSku.Name = "lblSku"
        Me.lblSku.Size = New System.Drawing.Size(436, 77)
        Me.lblSku.TabIndex = 28
        Me.lblSku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmPopupConfirmQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 168)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblSku)
        Me.Controls.Add(Me.txtConfirmQty)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.btnClose)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPopupConfirmQty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirm จำนวนสินค้า"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtConfirmQty As System.Windows.Forms.TextBox
    Friend WithEvents lblSku As System.Windows.Forms.Label
End Class
