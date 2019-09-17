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
        Me.btnExcelDrop = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnSL
        '
        Me.btnSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSL.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Copy
        Me.btnSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSL.Location = New System.Drawing.Point(28, 12)
        Me.btnSL.Name = "btnSL"
        Me.btnSL.Size = New System.Drawing.Size(248, 50)
        Me.btnSL.TabIndex = 1
        Me.btnSL.Text = "Create Order"
        Me.btnSL.UseVisualStyleBackColor = True
        '
        'btnExcelDrop
        '
        Me.btnExcelDrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExcelDrop.ForeColor = System.Drawing.Color.Black
        Me.btnExcelDrop.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ1
        Me.btnExcelDrop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcelDrop.Location = New System.Drawing.Point(28, 68)
        Me.btnExcelDrop.Name = "btnExcelDrop"
        Me.btnExcelDrop.Size = New System.Drawing.Size(248, 51)
        Me.btnExcelDrop.TabIndex = 2
        Me.btnExcelDrop.Text = "Approve Order Y"
        Me.btnExcelDrop.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ1
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(28, 125)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(248, 51)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Approve Order Z"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmExcelSelect_Import
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(298, 187)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnExcelDrop)
        Me.Controls.Add(Me.btnSL)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExcelSelect_Import"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Excel Order"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSL As System.Windows.Forms.Button
    Friend WithEvents btnExcelDrop As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
