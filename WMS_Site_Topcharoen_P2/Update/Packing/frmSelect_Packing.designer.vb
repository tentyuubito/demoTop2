<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelect_Packing
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
        Me.btnCL = New System.Windows.Forms.Button
        Me.btnSL = New System.Windows.Forms.Button
        Me.btnExcelDrop = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnCL
        '
        Me.btnCL.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCL.ForeColor = System.Drawing.Color.Blue
        Me.btnCL.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnCL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCL.Location = New System.Drawing.Point(22, 11)
        Me.btnCL.Name = "btnCL"
        Me.btnCL.Size = New System.Drawing.Size(248, 38)
        Me.btnCL.TabIndex = 0
        Me.btnCL.Text = "C L"
        Me.btnCL.UseVisualStyleBackColor = True
        '
        'btnSL
        '
        Me.btnSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSL.ForeColor = System.Drawing.Color.Green
        Me.btnSL.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Copy
        Me.btnSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSL.Location = New System.Drawing.Point(22, 55)
        Me.btnSL.Name = "btnSL"
        Me.btnSL.Size = New System.Drawing.Size(248, 38)
        Me.btnSL.TabIndex = 1
        Me.btnSL.Text = "S L"
        Me.btnSL.UseVisualStyleBackColor = True
        '
        'btnExcelDrop
        '
        Me.btnExcelDrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExcelDrop.ForeColor = System.Drawing.Color.Black
        Me.btnExcelDrop.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ1
        Me.btnExcelDrop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcelDrop.Location = New System.Drawing.Point(22, 99)
        Me.btnExcelDrop.Name = "btnExcelDrop"
        Me.btnExcelDrop.Size = New System.Drawing.Size(248, 49)
        Me.btnExcelDrop.TabIndex = 9
        Me.btnExcelDrop.Text = "update drop point"
        Me.btnExcelDrop.UseVisualStyleBackColor = True
        '
        'frmSelect_Packing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 160)
        Me.Controls.Add(Me.btnExcelDrop)
        Me.Controls.Add(Me.btnSL)
        Me.Controls.Add(Me.btnCL)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelect_Packing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เลือกประเภท Packing"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCL As System.Windows.Forms.Button
    Friend WithEvents btnSL As System.Windows.Forms.Button
    Friend WithEvents btnExcelDrop As System.Windows.Forms.Button
End Class
