<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJobProblem
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
        Me.lblDes = New System.Windows.Forms.Label
        Me.lbID = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.cboProcess = New System.Windows.Forms.ComboBox
        Me.lblProcess = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.grbDocumentType.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(33, 46)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(70, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "รายละเอียด"
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(74, 23)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(109, 20)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(172, 20)
        Me.txtID.TabIndex = 0
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(109, 43)
        Me.txtDes.MaxLength = 100
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(172, 20)
        Me.txtDes.TabIndex = 1
        '
        'cboProcess
        '
        Me.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProcess.FormattingEnabled = True
        Me.cboProcess.Location = New System.Drawing.Point(109, 69)
        Me.cboProcess.Name = "cboProcess"
        Me.cboProcess.Size = New System.Drawing.Size(172, 21)
        Me.cboProcess.TabIndex = 2
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProcess.Location = New System.Drawing.Point(24, 72)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(79, 13)
        Me.lblProcess.TabIndex = 11
        Me.lblProcess.Text = "ประเภทของงาน"
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(203, 120)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 33)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(93, 120)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 33)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Controls.Add(Me.txtID)
        Me.grbDocumentType.Controls.Add(Me.txtDes)
        Me.grbDocumentType.Controls.Add(Me.lbID)
        Me.grbDocumentType.Controls.Add(Me.cboProcess)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Controls.Add(Me.lblProcess)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 12)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(372, 101)
        Me.grbDocumentType.TabIndex = 0
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "ประเภทปัญหา"
        '
        'frmDocumentType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 164)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDocumentType"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ประเภทปัญหา"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents cboProcess As System.Windows.Forms.ComboBox
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
End Class
