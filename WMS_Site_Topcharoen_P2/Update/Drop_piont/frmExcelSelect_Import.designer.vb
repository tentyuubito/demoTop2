<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExcelSelect_Import
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
        Me.btnSL = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnSL
        '
        Me.btnSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSL.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Copy
        Me.btnSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSL.Location = New System.Drawing.Point(36, 74)
        Me.btnSL.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSL.Name = "btnSL"
        Me.btnSL.Size = New System.Drawing.Size(331, 62)
        Me.btnSL.TabIndex = 1
        Me.btnSL.Text = "Create Order"
        Me.btnSL.UseVisualStyleBackColor = True
        '
        'frmExcelSelect_Import
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 230)
        Me.Controls.Add(Me.btnSL)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExcelSelect_Import"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Excel Order"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSL As System.Windows.Forms.Button
End Class
