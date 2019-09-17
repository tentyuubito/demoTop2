<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin1
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
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX
        Me.btnOK = New DevComponents.DotNetBar.ButtonX
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.lblUpdate = New System.Windows.Forms.LinkLabel
        Me.lblImportLicense = New System.Windows.Forms.LinkLabel
        Me.btnEditPass = New System.Windows.Forms.LinkLabel
        Me.grbChange = New System.Windows.Forms.GroupBox
        Me.txtOldUserName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdSave = New System.Windows.Forms.Button
        Me.txtNewPasswordAgain = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtNewPassword = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtOldPassword = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label_lan = New DevComponents.DotNetBar.LabelX
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX
        Me.txtUserName = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.txtPassword = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.cboDepartment = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.rdbEng = New System.Windows.Forms.RadioButton
        Me.rdbthai = New System.Windows.Forms.RadioButton
        Me.lblVersation = New System.Windows.Forms.Label
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel
        Me.GroupPanel1.SuspendLayout()
        Me.grbChange.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(233, 123)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 24)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&ยกเลิก"
        '
        'btnOK
        '
        Me.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOK.Location = New System.Drawing.Point(33, 123)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 24)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "&ตกลง"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.lblUpdate)
        Me.GroupPanel1.Controls.Add(Me.lblImportLicense)
        Me.GroupPanel1.Controls.Add(Me.btnEditPass)
        Me.GroupPanel1.Controls.Add(Me.grbChange)
        Me.GroupPanel1.Controls.Add(Me.Label_lan)
        Me.GroupPanel1.Controls.Add(Me.LabelX3)
        Me.GroupPanel1.Controls.Add(Me.LabelX2)
        Me.GroupPanel1.Controls.Add(Me.LabelX1)
        Me.GroupPanel1.Controls.Add(Me.txtUserName)
        Me.GroupPanel1.Controls.Add(Me.txtPassword)
        Me.GroupPanel1.Controls.Add(Me.cboDepartment)
        Me.GroupPanel1.Controls.Add(Me.rdbEng)
        Me.GroupPanel1.Controls.Add(Me.rdbthai)
        Me.GroupPanel1.Controls.Add(Me.btnCancel)
        Me.GroupPanel1.Controls.Add(Me.btnOK)
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 46)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(336, 185)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.GroupPanel1.TabIndex = 4
        '
        'lblUpdate
        '
        Me.lblUpdate.AutoSize = True
        Me.lblUpdate.BackColor = System.Drawing.Color.Transparent
        Me.lblUpdate.Location = New System.Drawing.Point(122, 156)
        Me.lblUpdate.Name = "lblUpdate"
        Me.lblUpdate.Size = New System.Drawing.Size(98, 15)
        Me.lblUpdate.TabIndex = 315
        Me.lblUpdate.TabStop = True
        Me.lblUpdate.Text = "&Update Program"
        '
        'lblImportLicense
        '
        Me.lblImportLicense.AutoSize = True
        Me.lblImportLicense.BackColor = System.Drawing.Color.Transparent
        Me.lblImportLicense.Location = New System.Drawing.Point(4, 156)
        Me.lblImportLicense.Name = "lblImportLicense"
        Me.lblImportLicense.Size = New System.Drawing.Size(88, 15)
        Me.lblImportLicense.TabIndex = 314
        Me.lblImportLicense.TabStop = True
        Me.lblImportLicense.Text = "&Import License"
        '
        'btnEditPass
        '
        Me.btnEditPass.AutoSize = True
        Me.btnEditPass.BackColor = System.Drawing.Color.Transparent
        Me.btnEditPass.Location = New System.Drawing.Point(247, 156)
        Me.btnEditPass.Name = "btnEditPass"
        Me.btnEditPass.Size = New System.Drawing.Size(79, 15)
        Me.btnEditPass.TabIndex = 313
        Me.btnEditPass.TabStop = True
        Me.btnEditPass.Text = "&เปลี่ยนรหัสผ่าน"
        '
        'grbChange
        '
        Me.grbChange.Controls.Add(Me.txtOldUserName)
        Me.grbChange.Controls.Add(Me.Label4)
        Me.grbChange.Controls.Add(Me.cmdSave)
        Me.grbChange.Controls.Add(Me.txtNewPasswordAgain)
        Me.grbChange.Controls.Add(Me.Label3)
        Me.grbChange.Controls.Add(Me.txtNewPassword)
        Me.grbChange.Controls.Add(Me.Label2)
        Me.grbChange.Controls.Add(Me.txtOldPassword)
        Me.grbChange.Controls.Add(Me.Label1)
        Me.grbChange.Location = New System.Drawing.Point(332, 14)
        Me.grbChange.Name = "grbChange"
        Me.grbChange.Size = New System.Drawing.Size(279, 157)
        Me.grbChange.TabIndex = 312
        Me.grbChange.TabStop = False
        Me.grbChange.Text = "เปลี่ยน Password"
        '
        'txtOldUserName
        '
        Me.txtOldUserName.Location = New System.Drawing.Point(143, 27)
        Me.txtOldUserName.MaxLength = 16
        Me.txtOldUserName.Name = "txtOldUserName"
        Me.txtOldUserName.Size = New System.Drawing.Size(108, 21)
        Me.txtOldUserName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "UserName* :"
        '
        'cmdSave
        '
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdSave.Location = New System.Drawing.Point(177, 119)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(74, 27)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "บันทึก"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'txtNewPasswordAgain
        '
        Me.txtNewPasswordAgain.Location = New System.Drawing.Point(143, 93)
        Me.txtNewPasswordAgain.MaxLength = 16
        Me.txtNewPasswordAgain.Name = "txtNewPasswordAgain"
        Me.txtNewPasswordAgain.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPasswordAgain.Size = New System.Drawing.Size(108, 21)
        Me.txtNewPasswordAgain.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Password ใหม่อีกครั้ง* :"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(143, 71)
        Me.txtNewPassword.MaxLength = 16
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(108, 21)
        Me.txtNewPassword.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password ใหม่* :"
        '
        'txtOldPassword
        '
        Me.txtOldPassword.Location = New System.Drawing.Point(143, 49)
        Me.txtOldPassword.MaxLength = 16
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOldPassword.Size = New System.Drawing.Size(108, 21)
        Me.txtOldPassword.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Password เดิม* :"
        '
        'Label_lan
        '
        Me.Label_lan.BackColor = System.Drawing.Color.Transparent
        Me.Label_lan.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label_lan.Location = New System.Drawing.Point(27, 88)
        Me.Label_lan.Name = "Label_lan"
        Me.Label_lan.Size = New System.Drawing.Size(69, 18)
        Me.Label_lan.TabIndex = 310
        Me.Label_lan.Text = "&เลือกภาษา"
        Me.Label_lan.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        Me.LabelX3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(32, 63)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(64, 18)
        Me.LabelX3.TabIndex = 309
        Me.LabelX3.Text = "&ฐานข้อมูล"
        Me.LabelX3.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        Me.LabelX2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(37, 38)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(59, 21)
        Me.LabelX2.TabIndex = 308
        Me.LabelX2.Text = "&รหัสผ่าน"
        Me.LabelX2.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        Me.LabelX1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(51, 15)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(45, 18)
        Me.LabelX1.TabIndex = 307
        Me.LabelX1.Text = "&ผู้ใช้งาน"
        Me.LabelX1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.LightBlue
        '
        '
        '
        Me.txtUserName.Border.Class = "TextBoxBorder"
        Me.txtUserName.Location = New System.Drawing.Point(114, 14)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(177, 21)
        Me.txtUserName.TabIndex = 0
        Me.txtUserName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtUserName.WatermarkText = "USERNAME"
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.LightBlue
        '
        '
        '
        Me.txtPassword.Border.Class = "TextBoxBorder"
        Me.txtPassword.Location = New System.Drawing.Point(114, 39)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(177, 21)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtPassword.WatermarkText = "PASSWORD"
        '
        'cboDepartment
        '
        Me.cboDepartment.DisplayMember = "Text"
        Me.cboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDepartment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cboDepartment.FormattingEnabled = True
        Me.cboDepartment.ItemHeight = 15
        Me.cboDepartment.Location = New System.Drawing.Point(114, 64)
        Me.cboDepartment.Name = "cboDepartment"
        Me.cboDepartment.Size = New System.Drawing.Size(177, 21)
        Me.cboDepartment.TabIndex = 2
        '
        'rdbEng
        '
        Me.rdbEng.AutoSize = True
        Me.rdbEng.BackColor = System.Drawing.Color.Transparent
        Me.rdbEng.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.rdbEng.Location = New System.Drawing.Point(114, 89)
        Me.rdbEng.Name = "rdbEng"
        Me.rdbEng.Size = New System.Drawing.Size(66, 20)
        Me.rdbEng.TabIndex = 297
        Me.rdbEng.Text = "English"
        Me.rdbEng.UseVisualStyleBackColor = False
        '
        'rdbthai
        '
        Me.rdbthai.AutoSize = True
        Me.rdbthai.BackColor = System.Drawing.Color.Transparent
        Me.rdbthai.Checked = True
        Me.rdbthai.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.rdbthai.Location = New System.Drawing.Point(241, 88)
        Me.rdbthai.Name = "rdbthai"
        Me.rdbthai.Size = New System.Drawing.Size(50, 20)
        Me.rdbthai.TabIndex = 296
        Me.rdbthai.TabStop = True
        Me.rdbthai.Text = "ไทย"
        Me.rdbthai.UseVisualStyleBackColor = False
        '
        'lblVersation
        '
        Me.lblVersation.BackColor = System.Drawing.Color.SteelBlue
        Me.lblVersation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblVersation.ForeColor = System.Drawing.Color.White
        Me.lblVersation.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVersation.Location = New System.Drawing.Point(193, 9)
        Me.lblVersation.Name = "lblVersation"
        Me.lblVersation.Size = New System.Drawing.Size(130, 18)
        Me.lblVersation.TabIndex = 312
        Me.lblVersation.Text = "V."
        Me.lblVersation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ReflectionLabel1
        '
        Me.ReflectionLabel1.BackColor = System.Drawing.Color.SteelBlue
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile
        Me.ReflectionLabel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ReflectionLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ReflectionLabel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ReflectionLabel1.Location = New System.Drawing.Point(0, 0)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(336, 46)
        Me.ReflectionLabel1.TabIndex = 311
        Me.ReflectionLabel1.Text = "<b><font color=""WHITE"" size=""12""> KASCO  WMS </font></b>"
        '
        'frmLogin1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(336, 231)
        Me.Controls.Add(Me.lblVersation)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Controls.Add(Me.ReflectionLabel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmLogin1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLogin1"
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.grbChange.ResumeLayout(False)
        Me.grbChange.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOK As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents rdbEng As System.Windows.Forms.RadioButton
    Friend WithEvents rdbthai As System.Windows.Forms.RadioButton
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPassword As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cboDepartment As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Label_lan As DevComponents.DotNetBar.LabelX
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents lblVersation As System.Windows.Forms.Label
    Friend WithEvents grbChange As System.Windows.Forms.GroupBox
    Friend WithEvents txtOldUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtNewPasswordAgain As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOldPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEditPass As System.Windows.Forms.LinkLabel
    Friend WithEvents lblImportLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents lblUpdate As System.Windows.Forms.LinkLabel
    Friend WithEvents txtUserName As DevComponents.DotNetBar.Controls.TextBoxX
End Class
