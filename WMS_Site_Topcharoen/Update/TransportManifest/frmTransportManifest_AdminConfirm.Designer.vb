<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransportManifest_AdminConfirm
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
        Me.btnHandOver = New System.Windows.Forms.Button
        Me.btnLoading = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnHandOver
        '
        Me.btnHandOver.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnHandOver.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnHandOver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHandOver.Location = New System.Drawing.Point(12, 12)
        Me.btnHandOver.Name = "btnHandOver"
        Me.btnHandOver.Size = New System.Drawing.Size(116, 49)
        Me.btnHandOver.TabIndex = 309
        Me.btnHandOver.Text = "HandOver"
        Me.btnHandOver.UseVisualStyleBackColor = True
        '
        'btnLoading
        '
        Me.btnLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnLoading.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnLoading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoading.Location = New System.Drawing.Point(134, 12)
        Me.btnLoading.Name = "btnLoading"
        Me.btnLoading.Size = New System.Drawing.Size(116, 49)
        Me.btnLoading.TabIndex = 310
        Me.btnLoading.Text = "Loading"
        Me.btnLoading.UseVisualStyleBackColor = True
        '
        'frmTransportManifest_AdminConfirm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 81)
        Me.Controls.Add(Me.btnLoading)
        Me.Controls.Add(Me.btnHandOver)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTransportManifest_AdminConfirm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ยืนยัน Admin"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnHandOver As System.Windows.Forms.Button
    Friend WithEvents btnLoading As System.Windows.Forms.Button
End Class
