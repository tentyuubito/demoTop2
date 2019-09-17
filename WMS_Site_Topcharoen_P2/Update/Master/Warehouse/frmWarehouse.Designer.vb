<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWarehouse
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
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.txtID = New System.Windows.Forms.TextBox
        Me.lbID = New System.Windows.Forms.Label
        Me.lblDes = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.lblDistributionCenter = New System.Windows.Forms.Label
        Me.grbDocumentType.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbDocumentType.Controls.Add(Me.lblDistributionCenter)
        Me.grbDocumentType.Controls.Add(Me.cboDistributionCenter)
        Me.grbDocumentType.Controls.Add(Me.txtDes)
        Me.grbDocumentType.Controls.Add(Me.txtID)
        Me.grbDocumentType.Controls.Add(Me.lbID)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 12)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(372, 99)
        Me.grbDocumentType.TabIndex = 0
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "WareHouse"
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(87, 46)
        Me.txtDes.MaxLength = 50
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(232, 20)
        Me.txtDes.TabIndex = 8
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(87, 19)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(151, 20)
        Me.txtID.TabIndex = 0
        Me.txtID.Text = " "
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(51, 23)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(8, 46)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(73, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "WareHouse"
        '
        'btnExit
        '
        Me.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(202, 117)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 33)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(92, 117)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 33)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(87, 72)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(232, 21)
        Me.cboDistributionCenter.TabIndex = 18
        '
        'lblDistributionCenter
        '
        Me.lblDistributionCenter.AutoSize = True
        Me.lblDistributionCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDistributionCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDistributionCenter.Location = New System.Drawing.Point(5, 75)
        Me.lblDistributionCenter.Name = "lblDistributionCenter"
        Me.lblDistributionCenter.Size = New System.Drawing.Size(75, 13)
        Me.lblDistributionCenter.TabIndex = 19
        Me.lblDistributionCenter.Text = "ศูนย์กระจาย"
        '
        'frmWarehouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 167)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWarehouse"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WareHouse"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents lblDistributionCenter As System.Windows.Forms.Label
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
End Class
