<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTown
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
        Me.cboProvince = New System.Windows.Forms.ComboBox
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.lblProvine = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.lbID = New System.Windows.Forms.Label
        Me.lblDes = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboDistrict = New System.Windows.Forms.ComboBox
        Me.grbDocumentType.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboProvince
        '
        Me.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProvince.FormattingEnabled = True
        Me.cboProvince.Location = New System.Drawing.Point(74, 68)
        Me.cboProvince.Name = "cboProvince"
        Me.cboProvince.Size = New System.Drawing.Size(121, 21)
        Me.cboProvince.TabIndex = 2
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Controls.Add(Me.Label1)
        Me.grbDocumentType.Controls.Add(Me.cboDistrict)
        Me.grbDocumentType.Controls.Add(Me.lblProvine)
        Me.grbDocumentType.Controls.Add(Me.txtID)
        Me.grbDocumentType.Controls.Add(Me.txtDes)
        Me.grbDocumentType.Controls.Add(Me.lbID)
        Me.grbDocumentType.Controls.Add(Me.cboProvince)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 12)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(372, 136)
        Me.grbDocumentType.TabIndex = 0
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "ตำบล"
        '
        'lblProvine
        '
        Me.lblProvine.AutoSize = True
        Me.lblProvine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvine.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProvine.Location = New System.Drawing.Point(32, 72)
        Me.lblProvine.Name = "lblProvine"
        Me.lblProvine.Size = New System.Drawing.Size(38, 13)
        Me.lblProvine.TabIndex = 8
        Me.lblProvine.Text = "จังหวัด"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(74, 19)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(151, 20)
        Me.txtID.TabIndex = 0
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(74, 43)
        Me.txtDes.MaxLength = 255
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(288, 20)
        Me.txtDes.TabIndex = 1
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(39, 23)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(32, 46)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(36, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "ตำบล"
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(283, 154)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 33)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(12, 154)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 33)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(32, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "อำเภอ"
        '
        'cboDistrict
        '
        Me.cboDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrict.FormattingEnabled = True
        Me.cboDistrict.Location = New System.Drawing.Point(74, 95)
        Me.cboDistrict.Name = "cboDistrict"
        Me.cboDistrict.Size = New System.Drawing.Size(121, 21)
        Me.cboDistrict.TabIndex = 9
        '
        'frmTown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 199)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTown"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตำบล"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboProvince As System.Windows.Forms.ComboBox
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
    Friend WithEvents lblProvine As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDistrict As System.Windows.Forms.ComboBox
End Class
