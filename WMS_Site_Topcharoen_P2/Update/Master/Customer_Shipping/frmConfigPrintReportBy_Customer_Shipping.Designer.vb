<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigPrintReportBy_Customer_Shipping
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
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblRPT_KSL_Invoice = New System.Windows.Forms.Label
        Me.lblRPT_KSL_Invoice_Copy = New System.Windows.Forms.Label
        Me.lblRPT_KSL_Invoice_Dup = New System.Windows.Forms.Label
        Me.lblRPT_KSL_Invoice_PrintFrom = New System.Windows.Forms.Label
        Me.lblRPT_KSL_Recept = New System.Windows.Forms.Label
        Me.txtRPT_KSL_Invoice = New System.Windows.Forms.TextBox
        Me.txtRPT_KSL_Invoice_Copy = New System.Windows.Forms.TextBox
        Me.txtRPT_KSL_Invoice_Dup = New System.Windows.Forms.TextBox
        Me.txtRPT_KSL_Invoice_PrintFrom = New System.Windows.Forms.TextBox
        Me.txtRPT_KSL_Recept = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(31, 156)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(115, 38)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(171, 156)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(111, 38)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblRPT_KSL_Invoice
        '
        Me.lblRPT_KSL_Invoice.Location = New System.Drawing.Point(12, 17)
        Me.lblRPT_KSL_Invoice.Name = "lblRPT_KSL_Invoice"
        Me.lblRPT_KSL_Invoice.Size = New System.Drawing.Size(200, 16)
        Me.lblRPT_KSL_Invoice.TabIndex = 4
        Me.lblRPT_KSL_Invoice.Tag = "RPT_KSL_Invoice"
        Me.lblRPT_KSL_Invoice.Text = "ใบกำกับภาษี / ใบส่งของ"
        Me.lblRPT_KSL_Invoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRPT_KSL_Invoice_Copy
        '
        Me.lblRPT_KSL_Invoice_Copy.Location = New System.Drawing.Point(12, 44)
        Me.lblRPT_KSL_Invoice_Copy.Name = "lblRPT_KSL_Invoice_Copy"
        Me.lblRPT_KSL_Invoice_Copy.Size = New System.Drawing.Size(200, 16)
        Me.lblRPT_KSL_Invoice_Copy.TabIndex = 4
        Me.lblRPT_KSL_Invoice_Copy.Tag = "RPT_KSL_Invoice_Copy"
        Me.lblRPT_KSL_Invoice_Copy.Text = "(สำเนา)ใบกำกับภาษี / ใบส่งของ"
        Me.lblRPT_KSL_Invoice_Copy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRPT_KSL_Invoice_Dup
        '
        Me.lblRPT_KSL_Invoice_Dup.Location = New System.Drawing.Point(12, 71)
        Me.lblRPT_KSL_Invoice_Dup.Name = "lblRPT_KSL_Invoice_Dup"
        Me.lblRPT_KSL_Invoice_Dup.Size = New System.Drawing.Size(200, 16)
        Me.lblRPT_KSL_Invoice_Dup.TabIndex = 4
        Me.lblRPT_KSL_Invoice_Dup.Tag = "RPT_KSL_Invoice_Dup"
        Me.lblRPT_KSL_Invoice_Dup.Text = "ใบส่งของ / ใบแจ้งหนี้"
        Me.lblRPT_KSL_Invoice_Dup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRPT_KSL_Invoice_PrintFrom
        '
        Me.lblRPT_KSL_Invoice_PrintFrom.Location = New System.Drawing.Point(12, 98)
        Me.lblRPT_KSL_Invoice_PrintFrom.Name = "lblRPT_KSL_Invoice_PrintFrom"
        Me.lblRPT_KSL_Invoice_PrintFrom.Size = New System.Drawing.Size(200, 16)
        Me.lblRPT_KSL_Invoice_PrintFrom.TabIndex = 4
        Me.lblRPT_KSL_Invoice_PrintFrom.Tag = "RPT_KSL_Invoice_PrintFrom"
        Me.lblRPT_KSL_Invoice_PrintFrom.Text = "ใบเสร็จรับเงิน"
        Me.lblRPT_KSL_Invoice_PrintFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRPT_KSL_Recept
        '
        Me.lblRPT_KSL_Recept.Location = New System.Drawing.Point(12, 125)
        Me.lblRPT_KSL_Recept.Name = "lblRPT_KSL_Recept"
        Me.lblRPT_KSL_Recept.Size = New System.Drawing.Size(200, 16)
        Me.lblRPT_KSL_Recept.TabIndex = 4
        Me.lblRPT_KSL_Recept.Tag = "RPT_KSL_Recept"
        Me.lblRPT_KSL_Recept.Text = "ต้นฉบับ  ใบเสร็จรับเงิน"
        Me.lblRPT_KSL_Recept.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRPT_KSL_Invoice
        '
        Me.txtRPT_KSL_Invoice.Location = New System.Drawing.Point(218, 17)
        Me.txtRPT_KSL_Invoice.Name = "txtRPT_KSL_Invoice"
        Me.txtRPT_KSL_Invoice.Size = New System.Drawing.Size(64, 20)
        Me.txtRPT_KSL_Invoice.TabIndex = 5
        '
        'txtRPT_KSL_Invoice_Copy
        '
        Me.txtRPT_KSL_Invoice_Copy.Location = New System.Drawing.Point(218, 44)
        Me.txtRPT_KSL_Invoice_Copy.Name = "txtRPT_KSL_Invoice_Copy"
        Me.txtRPT_KSL_Invoice_Copy.Size = New System.Drawing.Size(64, 20)
        Me.txtRPT_KSL_Invoice_Copy.TabIndex = 5
        '
        'txtRPT_KSL_Invoice_Dup
        '
        Me.txtRPT_KSL_Invoice_Dup.Location = New System.Drawing.Point(218, 71)
        Me.txtRPT_KSL_Invoice_Dup.Name = "txtRPT_KSL_Invoice_Dup"
        Me.txtRPT_KSL_Invoice_Dup.Size = New System.Drawing.Size(64, 20)
        Me.txtRPT_KSL_Invoice_Dup.TabIndex = 5
        '
        'txtRPT_KSL_Invoice_PrintFrom
        '
        Me.txtRPT_KSL_Invoice_PrintFrom.Location = New System.Drawing.Point(218, 98)
        Me.txtRPT_KSL_Invoice_PrintFrom.Name = "txtRPT_KSL_Invoice_PrintFrom"
        Me.txtRPT_KSL_Invoice_PrintFrom.Size = New System.Drawing.Size(64, 20)
        Me.txtRPT_KSL_Invoice_PrintFrom.TabIndex = 5
        '
        'txtRPT_KSL_Recept
        '
        Me.txtRPT_KSL_Recept.Location = New System.Drawing.Point(218, 125)
        Me.txtRPT_KSL_Recept.Name = "txtRPT_KSL_Recept"
        Me.txtRPT_KSL_Recept.Size = New System.Drawing.Size(64, 20)
        Me.txtRPT_KSL_Recept.TabIndex = 5
        '
        'frmConfigPrintReportBy_Customer_Shipping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 206)
        Me.Controls.Add(Me.txtRPT_KSL_Recept)
        Me.Controls.Add(Me.txtRPT_KSL_Invoice_PrintFrom)
        Me.Controls.Add(Me.txtRPT_KSL_Invoice_Dup)
        Me.Controls.Add(Me.txtRPT_KSL_Invoice_Copy)
        Me.Controls.Add(Me.txtRPT_KSL_Invoice)
        Me.Controls.Add(Me.lblRPT_KSL_Recept)
        Me.Controls.Add(Me.lblRPT_KSL_Invoice_PrintFrom)
        Me.Controls.Add(Me.lblRPT_KSL_Invoice_Dup)
        Me.Controls.Add(Me.lblRPT_KSL_Invoice_Copy)
        Me.Controls.Add(Me.lblRPT_KSL_Invoice)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmConfigPrintReportBy_Customer_Shipping"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตั้งค่าจำนวนการปริ้น Invoice"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblRPT_KSL_Invoice As System.Windows.Forms.Label
    Friend WithEvents lblRPT_KSL_Invoice_Copy As System.Windows.Forms.Label
    Friend WithEvents lblRPT_KSL_Invoice_Dup As System.Windows.Forms.Label
    Friend WithEvents lblRPT_KSL_Invoice_PrintFrom As System.Windows.Forms.Label
    Friend WithEvents lblRPT_KSL_Recept As System.Windows.Forms.Label
    Friend WithEvents txtRPT_KSL_Invoice As System.Windows.Forms.TextBox
    Friend WithEvents txtRPT_KSL_Invoice_Copy As System.Windows.Forms.TextBox
    Friend WithEvents txtRPT_KSL_Invoice_Dup As System.Windows.Forms.TextBox
    Friend WithEvents txtRPT_KSL_Invoice_PrintFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtRPT_KSL_Recept As System.Windows.Forms.TextBox
End Class
