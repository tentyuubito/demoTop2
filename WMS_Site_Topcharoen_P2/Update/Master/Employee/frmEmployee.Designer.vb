<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployee
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployee))
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.radwoman = New System.Windows.Forms.RadioButton
        Me.radman = New System.Windows.Forms.RadioButton
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtImage_Path = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.grbContact = New System.Windows.Forms.GroupBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.lblEmail = New System.Windows.Forms.Label
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.lblFax = New System.Windows.Forms.Label
        Me.lblMobile = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.txtTel = New System.Windows.Forms.TextBox
        Me.lblTel = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPostcode = New System.Windows.Forms.TextBox
        Me.lblAddress = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.lblCountr = New System.Windows.Forms.Label
        Me.lblProvince = New System.Windows.Forms.Label
        Me.cboProvince = New System.Windows.Forms.ComboBox
        Me.cboDistrict = New System.Windows.Forms.ComboBox
        Me.grbEmployeeDetail = New System.Windows.Forms.GroupBox
        Me.chkBlackList = New System.Windows.Forms.CheckBox
        Me.txtRecruit_By = New System.Windows.Forms.TextBox
        Me.lblBlackList = New System.Windows.Forms.Label
        Me.dtpRecruit_Date = New System.Windows.Forms.DateTimePicker
        Me.lblRecruit_By = New System.Windows.Forms.Label
        Me.txtAttachment2 = New System.Windows.Forms.TextBox
        Me.lblRecruit_Date = New System.Windows.Forms.Label
        Me.lblAttachment2 = New System.Windows.Forms.Label
        Me.txtAttachment3 = New System.Windows.Forms.TextBox
        Me.lblAttachment1 = New System.Windows.Forms.Label
        Me.txtAttachment1 = New System.Windows.Forms.TextBox
        Me.lbllblAttachment3 = New System.Windows.Forms.Label
        Me.txtDriver_License_No = New System.Windows.Forms.TextBox
        Me.txtNational_Id = New System.Windows.Forms.TextBox
        Me.lblDriver_License_No = New System.Windows.Forms.Label
        Me.lblNational_Id = New System.Windows.Forms.Label
        Me.dtpBirth_Date = New System.Windows.Forms.DateTimePicker
        Me.grbEmployee = New System.Windows.Forms.GroupBox
        Me.btnGroupEmployee = New System.Windows.Forms.Button
        Me.lblBirth_Date = New System.Windows.Forms.Label
        Me.lblGender = New System.Windows.Forms.Label
        Me.rdbGender_W = New System.Windows.Forms.RadioButton
        Me.lblPosition = New System.Windows.Forms.Label
        Me.rdbGender_M = New System.Windows.Forms.RadioButton
        Me.lblEmpLastName = New System.Windows.Forms.Label
        Me.txtEmpLastName = New System.Windows.Forms.TextBox
        Me.lblEmpName = New System.Windows.Forms.Label
        Me.txtPosition = New System.Windows.Forms.TextBox
        Me.lblEmpID = New System.Windows.Forms.Label
        Me.cboEmployeeGroup = New System.Windows.Forms.ComboBox
        Me.lblEmployeeGroup = New System.Windows.Forms.Label
        Me.txtEmpID = New System.Windows.Forms.TextBox
        Me.txtEmpName = New System.Windows.Forms.TextBox
        Me.lblDepartment = New System.Windows.Forms.Label
        Me.cboDepartment = New System.Windows.Forms.ComboBox
        Me.btnRemovePic = New System.Windows.Forms.Button
        Me.btnAddPic = New System.Windows.Forms.Button
        Me.picEmployee = New System.Windows.Forms.PictureBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox2.SuspendLayout()
        Me.grbContact.SuspendLayout()
        Me.grbEmployeeDetail.SuspendLayout()
        Me.grbEmployee.SuspendLayout()
        CType(Me.picEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(464, 558)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(107, 38)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(12, 558)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(354, 151)
        Me.TextBox1.MaxLength = 100
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(156, 20)
        Me.TextBox1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(274, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "E-mail"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(108, 151)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(151, 20)
        Me.TextBox2.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(39, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "โทรสาร"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(272, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "เบอร์โทรส่วนตัว"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(354, 128)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(156, 20)
        Me.TextBox3.TabIndex = 5
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(108, 128)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(151, 20)
        Me.TextBox4.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "เบอร์โทรติดต่อ"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(272, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "รหัสไปรษณีย์"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(354, 104)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(156, 20)
        Me.TextBox5.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(22, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "ที่อยู่"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(108, 16)
        Me.TextBox6.Multiline = True
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(388, 61)
        Me.TextBox6.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(39, 109)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "เขต"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(31, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "จังหวัด"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(108, 80)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(151, 21)
        Me.ComboBox1.TabIndex = 2
        '
        'ComboBox2
        '
        Me.ComboBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(108, 104)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(151, 21)
        Me.ComboBox2.TabIndex = 1
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(112, 13)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(67, 17)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "เพศหญิง"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(32, 13)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(65, 17)
        Me.RadioButton2.TabIndex = 0
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "เพศชาย"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radwoman)
        Me.GroupBox2.Controls.Add(Me.radman)
        Me.GroupBox2.Location = New System.Drawing.Point(285, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(240, 47)
        Me.GroupBox2.TabIndex = 54
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "เพศ"
        '
        'radwoman
        '
        Me.radwoman.AutoSize = True
        Me.radwoman.Location = New System.Drawing.Point(112, 13)
        Me.radwoman.Name = "radwoman"
        Me.radwoman.Size = New System.Drawing.Size(67, 17)
        Me.radwoman.TabIndex = 1
        Me.radwoman.TabStop = True
        Me.radwoman.Text = "เพศหญิง"
        Me.radwoman.UseVisualStyleBackColor = True
        '
        'radman
        '
        Me.radman.AutoSize = True
        Me.radman.Checked = True
        Me.radman.Location = New System.Drawing.Point(32, 13)
        Me.radman.Name = "radman"
        Me.radman.Size = New System.Drawing.Size(65, 17)
        Me.radman.TabIndex = 0
        Me.radman.TabStop = True
        Me.radman.Text = "เพศชาย"
        Me.radman.UseVisualStyleBackColor = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(292, 109)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(73, 13)
        Me.Label30.TabIndex = 37
        Me.Label30.Text = "ผู้รับเข้าทำงาน"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(13, 109)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 13)
        Me.Label29.TabIndex = 36
        Me.Label29.Text = "วันที่เริ่มงาน"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(292, 136)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(75, 13)
        Me.Label28.TabIndex = 35
        Me.Label28.Text = "เอกสารอ้างอิง3"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(13, 159)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(75, 13)
        Me.Label27.TabIndex = 34
        Me.Label27.Text = "เอกสารอ้างอิง2"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(16, 136)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(75, 13)
        Me.Label26.TabIndex = 33
        Me.Label26.Text = "เอกสารอ้างอิง1"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(13, 91)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(49, 13)
        Me.Label24.TabIndex = 31
        Me.Label24.Text = "Black list"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 41)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(40, 13)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "วันเกิด"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(13, 65)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(63, 13)
        Me.Label22.TabIndex = 29
        Me.Label22.Text = "เลขที่ใบขับขี่"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 13)
        Me.Label21.TabIndex = 28
        Me.Label21.Text = "เลขที่บัตรประชาชน"
        '
        'txtImage_Path
        '
        Me.txtImage_Path.Location = New System.Drawing.Point(116, 217)
        Me.txtImage_Path.Name = "txtImage_Path"
        Me.txtImage_Path.Size = New System.Drawing.Size(100, 20)
        Me.txtImage_Path.TabIndex = 27
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(16, 220)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(40, 13)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "รูปภาพ"
        '
        'grbContact
        '
        Me.grbContact.Controls.Add(Me.txtEmail)
        Me.grbContact.Controls.Add(Me.lblEmail)
        Me.grbContact.Controls.Add(Me.txtFax)
        Me.grbContact.Controls.Add(Me.lblFax)
        Me.grbContact.Controls.Add(Me.lblMobile)
        Me.grbContact.Controls.Add(Me.txtMobile)
        Me.grbContact.Controls.Add(Me.txtTel)
        Me.grbContact.Controls.Add(Me.lblTel)
        Me.grbContact.Controls.Add(Me.Label6)
        Me.grbContact.Controls.Add(Me.txtPostcode)
        Me.grbContact.Controls.Add(Me.lblAddress)
        Me.grbContact.Controls.Add(Me.txtAddress)
        Me.grbContact.Controls.Add(Me.lblCountr)
        Me.grbContact.Controls.Add(Me.lblProvince)
        Me.grbContact.Controls.Add(Me.cboProvince)
        Me.grbContact.Controls.Add(Me.cboDistrict)
        Me.grbContact.Location = New System.Drawing.Point(10, 264)
        Me.grbContact.Name = "grbContact"
        Me.grbContact.Size = New System.Drawing.Size(559, 163)
        Me.grbContact.TabIndex = 3
        Me.grbContact.TabStop = False
        Me.grbContact.Text = "ที่อยู่ / ข้อมูลการสื่อสาร"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(108, 137)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(177, 20)
        Me.txtEmail.TabIndex = 4
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(64, 137)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 41
        Me.lblEmail.Text = "E-mail"
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(376, 113)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(173, 20)
        Me.txtFax.TabIndex = 7
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.Location = New System.Drawing.Point(325, 113)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(42, 13)
        Me.lblFax.TabIndex = 39
        Me.lblFax.Text = "โทรสาร"
        '
        'lblMobile
        '
        Me.lblMobile.AutoSize = True
        Me.lblMobile.Location = New System.Drawing.Point(17, 113)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.Size = New System.Drawing.Size(82, 13)
        Me.lblMobile.TabIndex = 38
        Me.lblMobile.Text = "เบอร์โทรส่วนตัว"
        '
        'txtMobile
        '
        Me.txtMobile.Location = New System.Drawing.Point(108, 113)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(177, 20)
        Me.txtMobile.TabIndex = 3
        '
        'txtTel
        '
        Me.txtTel.Location = New System.Drawing.Point(376, 89)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(173, 20)
        Me.txtTel.TabIndex = 6
        '
        'lblTel
        '
        Me.lblTel.AutoSize = True
        Me.lblTel.Location = New System.Drawing.Point(291, 89)
        Me.lblTel.Name = "lblTel"
        Me.lblTel.Size = New System.Drawing.Size(76, 13)
        Me.lblTel.TabIndex = 35
        Me.lblTel.Text = "เบอร์โทรติดต่อ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(30, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "รหัสไปรษณีย์"
        '
        'txtPostcode
        '
        Me.txtPostcode.Location = New System.Drawing.Point(108, 89)
        Me.txtPostcode.Name = "txtPostcode"
        Me.txtPostcode.Size = New System.Drawing.Size(177, 20)
        Me.txtPostcode.TabIndex = 2
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAddress.Location = New System.Drawing.Point(72, 28)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(27, 13)
        Me.lblAddress.TabIndex = 32
        Me.lblAddress.Text = "ที่อยู่"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(108, 16)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(441, 44)
        Me.txtAddress.TabIndex = 0
        '
        'lblCountr
        '
        Me.lblCountr.AutoSize = True
        Me.lblCountr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCountr.Location = New System.Drawing.Point(342, 64)
        Me.lblCountr.Name = "lblCountr"
        Me.lblCountr.Size = New System.Drawing.Size(25, 13)
        Me.lblCountr.TabIndex = 30
        Me.lblCountr.Text = "เขต"
        '
        'lblProvince
        '
        Me.lblProvince.AutoSize = True
        Me.lblProvince.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvince.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProvince.Location = New System.Drawing.Point(61, 64)
        Me.lblProvince.Name = "lblProvince"
        Me.lblProvince.Size = New System.Drawing.Size(38, 13)
        Me.lblProvince.TabIndex = 26
        Me.lblProvince.Text = "จังหวัด"
        '
        'cboProvince
        '
        Me.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProvince.FormattingEnabled = True
        Me.cboProvince.Location = New System.Drawing.Point(108, 64)
        Me.cboProvince.Name = "cboProvince"
        Me.cboProvince.Size = New System.Drawing.Size(180, 21)
        Me.cboProvince.TabIndex = 1
        '
        'cboDistrict
        '
        Me.cboDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrict.FormattingEnabled = True
        Me.cboDistrict.Location = New System.Drawing.Point(376, 64)
        Me.cboDistrict.Name = "cboDistrict"
        Me.cboDistrict.Size = New System.Drawing.Size(173, 21)
        Me.cboDistrict.TabIndex = 5
        '
        'grbEmployeeDetail
        '
        Me.grbEmployeeDetail.Controls.Add(Me.chkBlackList)
        Me.grbEmployeeDetail.Controls.Add(Me.txtRecruit_By)
        Me.grbEmployeeDetail.Controls.Add(Me.lblBlackList)
        Me.grbEmployeeDetail.Controls.Add(Me.dtpRecruit_Date)
        Me.grbEmployeeDetail.Controls.Add(Me.lblRecruit_By)
        Me.grbEmployeeDetail.Controls.Add(Me.txtAttachment2)
        Me.grbEmployeeDetail.Controls.Add(Me.lblRecruit_Date)
        Me.grbEmployeeDetail.Controls.Add(Me.lblAttachment2)
        Me.grbEmployeeDetail.Controls.Add(Me.txtAttachment3)
        Me.grbEmployeeDetail.Controls.Add(Me.lblAttachment1)
        Me.grbEmployeeDetail.Controls.Add(Me.txtAttachment1)
        Me.grbEmployeeDetail.Controls.Add(Me.lbllblAttachment3)
        Me.grbEmployeeDetail.Location = New System.Drawing.Point(11, 433)
        Me.grbEmployeeDetail.Name = "grbEmployeeDetail"
        Me.grbEmployeeDetail.Size = New System.Drawing.Size(557, 119)
        Me.grbEmployeeDetail.TabIndex = 4
        Me.grbEmployeeDetail.TabStop = False
        Me.grbEmployeeDetail.Text = "รายละเอียดอื่นๆ"
        '
        'chkBlackList
        '
        Me.chkBlackList.AutoSize = True
        Me.chkBlackList.Location = New System.Drawing.Point(108, 18)
        Me.chkBlackList.Name = "chkBlackList"
        Me.chkBlackList.Size = New System.Drawing.Size(15, 14)
        Me.chkBlackList.TabIndex = 53
        Me.chkBlackList.UseVisualStyleBackColor = True
        '
        'txtRecruit_By
        '
        Me.txtRecruit_By.Location = New System.Drawing.Point(376, 38)
        Me.txtRecruit_By.Name = "txtRecruit_By"
        Me.txtRecruit_By.Size = New System.Drawing.Size(168, 20)
        Me.txtRecruit_By.TabIndex = 3
        '
        'lblBlackList
        '
        Me.lblBlackList.AutoSize = True
        Me.lblBlackList.Location = New System.Drawing.Point(46, 18)
        Me.lblBlackList.Name = "lblBlackList"
        Me.lblBlackList.Size = New System.Drawing.Size(53, 13)
        Me.lblBlackList.TabIndex = 54
        Me.lblBlackList.Text = "Black List"
        '
        'dtpRecruit_Date
        '
        Me.dtpRecruit_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRecruit_Date.Location = New System.Drawing.Point(108, 38)
        Me.dtpRecruit_Date.Name = "dtpRecruit_Date"
        Me.dtpRecruit_Date.Size = New System.Drawing.Size(176, 20)
        Me.dtpRecruit_Date.TabIndex = 0
        '
        'lblRecruit_By
        '
        Me.lblRecruit_By.AutoSize = True
        Me.lblRecruit_By.Location = New System.Drawing.Point(294, 38)
        Me.lblRecruit_By.Name = "lblRecruit_By"
        Me.lblRecruit_By.Size = New System.Drawing.Size(73, 13)
        Me.lblRecruit_By.TabIndex = 59
        Me.lblRecruit_By.Text = "ผู้รับเข้าทำงาน"
        '
        'txtAttachment2
        '
        Me.txtAttachment2.Location = New System.Drawing.Point(376, 62)
        Me.txtAttachment2.Name = "txtAttachment2"
        Me.txtAttachment2.Size = New System.Drawing.Size(168, 20)
        Me.txtAttachment2.TabIndex = 4
        '
        'lblRecruit_Date
        '
        Me.lblRecruit_Date.AutoSize = True
        Me.lblRecruit_Date.Location = New System.Drawing.Point(35, 38)
        Me.lblRecruit_Date.Name = "lblRecruit_Date"
        Me.lblRecruit_Date.Size = New System.Drawing.Size(64, 13)
        Me.lblRecruit_Date.TabIndex = 58
        Me.lblRecruit_Date.Text = "วันที่เริ่มงาน"
        '
        'lblAttachment2
        '
        Me.lblAttachment2.AutoSize = True
        Me.lblAttachment2.Location = New System.Drawing.Point(292, 62)
        Me.lblAttachment2.Name = "lblAttachment2"
        Me.lblAttachment2.Size = New System.Drawing.Size(75, 13)
        Me.lblAttachment2.TabIndex = 56
        Me.lblAttachment2.Text = "เอกสารอ้างอิง2"
        '
        'txtAttachment3
        '
        Me.txtAttachment3.Location = New System.Drawing.Point(108, 85)
        Me.txtAttachment3.Name = "txtAttachment3"
        Me.txtAttachment3.Size = New System.Drawing.Size(176, 20)
        Me.txtAttachment3.TabIndex = 2
        '
        'lblAttachment1
        '
        Me.lblAttachment1.AutoSize = True
        Me.lblAttachment1.Location = New System.Drawing.Point(24, 62)
        Me.lblAttachment1.Name = "lblAttachment1"
        Me.lblAttachment1.Size = New System.Drawing.Size(75, 13)
        Me.lblAttachment1.TabIndex = 55
        Me.lblAttachment1.Text = "เอกสารอ้างอิง1"
        '
        'txtAttachment1
        '
        Me.txtAttachment1.Location = New System.Drawing.Point(108, 62)
        Me.txtAttachment1.Name = "txtAttachment1"
        Me.txtAttachment1.Size = New System.Drawing.Size(176, 20)
        Me.txtAttachment1.TabIndex = 1
        '
        'lbllblAttachment3
        '
        Me.lbllblAttachment3.AutoSize = True
        Me.lbllblAttachment3.Location = New System.Drawing.Point(24, 85)
        Me.lbllblAttachment3.Name = "lbllblAttachment3"
        Me.lbllblAttachment3.Size = New System.Drawing.Size(75, 13)
        Me.lbllblAttachment3.TabIndex = 57
        Me.lbllblAttachment3.Text = "เอกสารอ้างอิง3"
        '
        'txtDriver_License_No
        '
        Me.txtDriver_License_No.Location = New System.Drawing.Point(108, 112)
        Me.txtDriver_License_No.Name = "txtDriver_License_No"
        Me.txtDriver_License_No.Size = New System.Drawing.Size(214, 20)
        Me.txtDriver_License_No.TabIndex = 4
        '
        'txtNational_Id
        '
        Me.txtNational_Id.Location = New System.Drawing.Point(108, 88)
        Me.txtNational_Id.Name = "txtNational_Id"
        Me.txtNational_Id.Size = New System.Drawing.Size(214, 20)
        Me.txtNational_Id.TabIndex = 3
        '
        'lblDriver_License_No
        '
        Me.lblDriver_License_No.AutoSize = True
        Me.lblDriver_License_No.Location = New System.Drawing.Point(36, 115)
        Me.lblDriver_License_No.Name = "lblDriver_License_No"
        Me.lblDriver_License_No.Size = New System.Drawing.Size(63, 13)
        Me.lblDriver_License_No.TabIndex = 29
        Me.lblDriver_License_No.Text = "เลขที่ใบขับขี่"
        '
        'lblNational_Id
        '
        Me.lblNational_Id.AutoSize = True
        Me.lblNational_Id.Location = New System.Drawing.Point(3, 89)
        Me.lblNational_Id.Name = "lblNational_Id"
        Me.lblNational_Id.Size = New System.Drawing.Size(96, 13)
        Me.lblNational_Id.TabIndex = 28
        Me.lblNational_Id.Text = "เลขที่บัตรประชาชน"
        '
        'dtpBirth_Date
        '
        Me.dtpBirth_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBirth_Date.Location = New System.Drawing.Point(108, 156)
        Me.dtpBirth_Date.Name = "dtpBirth_Date"
        Me.dtpBirth_Date.Size = New System.Drawing.Size(214, 20)
        Me.dtpBirth_Date.TabIndex = 7
        '
        'grbEmployee
        '
        Me.grbEmployee.Controls.Add(Me.btnGroupEmployee)
        Me.grbEmployee.Controls.Add(Me.lblBirth_Date)
        Me.grbEmployee.Controls.Add(Me.lblGender)
        Me.grbEmployee.Controls.Add(Me.dtpBirth_Date)
        Me.grbEmployee.Controls.Add(Me.rdbGender_W)
        Me.grbEmployee.Controls.Add(Me.lblPosition)
        Me.grbEmployee.Controls.Add(Me.rdbGender_M)
        Me.grbEmployee.Controls.Add(Me.lblEmpLastName)
        Me.grbEmployee.Controls.Add(Me.txtDriver_License_No)
        Me.grbEmployee.Controls.Add(Me.txtEmpLastName)
        Me.grbEmployee.Controls.Add(Me.lblEmpName)
        Me.grbEmployee.Controls.Add(Me.txtNational_Id)
        Me.grbEmployee.Controls.Add(Me.txtPosition)
        Me.grbEmployee.Controls.Add(Me.lblEmpID)
        Me.grbEmployee.Controls.Add(Me.cboEmployeeGroup)
        Me.grbEmployee.Controls.Add(Me.lblEmployeeGroup)
        Me.grbEmployee.Controls.Add(Me.txtEmpID)
        Me.grbEmployee.Controls.Add(Me.lblDriver_License_No)
        Me.grbEmployee.Controls.Add(Me.txtEmpName)
        Me.grbEmployee.Controls.Add(Me.lblDepartment)
        Me.grbEmployee.Controls.Add(Me.cboDepartment)
        Me.grbEmployee.Controls.Add(Me.lblNational_Id)
        Me.grbEmployee.Location = New System.Drawing.Point(11, 2)
        Me.grbEmployee.Name = "grbEmployee"
        Me.grbEmployee.Size = New System.Drawing.Size(338, 256)
        Me.grbEmployee.TabIndex = 0
        Me.grbEmployee.TabStop = False
        Me.grbEmployee.Text = "ชื่อพนักงาน"
        '
        'btnGroupEmployee
        '
        Me.btnGroupEmployee.Location = New System.Drawing.Point(294, 230)
        Me.btnGroupEmployee.Name = "btnGroupEmployee"
        Me.btnGroupEmployee.Size = New System.Drawing.Size(28, 20)
        Me.btnGroupEmployee.TabIndex = 54
        Me.btnGroupEmployee.Text = "..."
        Me.btnGroupEmployee.UseVisualStyleBackColor = True
        '
        'lblBirth_Date
        '
        Me.lblBirth_Date.AutoSize = True
        Me.lblBirth_Date.Location = New System.Drawing.Point(59, 156)
        Me.lblBirth_Date.Name = "lblBirth_Date"
        Me.lblBirth_Date.Size = New System.Drawing.Size(40, 13)
        Me.lblBirth_Date.TabIndex = 53
        Me.lblBirth_Date.Text = "วันเกิด"
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGender.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblGender.Location = New System.Drawing.Point(71, 136)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(28, 13)
        Me.lblGender.TabIndex = 51
        Me.lblGender.Text = "เพศ"
        '
        'rdbGender_W
        '
        Me.rdbGender_W.AutoSize = True
        Me.rdbGender_W.Location = New System.Drawing.Point(200, 136)
        Me.rdbGender_W.Name = "rdbGender_W"
        Me.rdbGender_W.Size = New System.Drawing.Size(67, 17)
        Me.rdbGender_W.TabIndex = 6
        Me.rdbGender_W.TabStop = True
        Me.rdbGender_W.Text = "เพศหญิง"
        Me.rdbGender_W.UseVisualStyleBackColor = True
        '
        'lblPosition
        '
        Me.lblPosition.AutoSize = True
        Me.lblPosition.Location = New System.Drawing.Point(52, 180)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(47, 13)
        Me.lblPosition.TabIndex = 50
        Me.lblPosition.Text = "ตำแหน่ง"
        '
        'rdbGender_M
        '
        Me.rdbGender_M.AutoSize = True
        Me.rdbGender_M.Checked = True
        Me.rdbGender_M.Location = New System.Drawing.Point(108, 136)
        Me.rdbGender_M.Name = "rdbGender_M"
        Me.rdbGender_M.Size = New System.Drawing.Size(65, 17)
        Me.rdbGender_M.TabIndex = 5
        Me.rdbGender_M.TabStop = True
        Me.rdbGender_M.Text = "เพศชาย"
        Me.rdbGender_M.UseVisualStyleBackColor = True
        '
        'lblEmpLastName
        '
        Me.lblEmpLastName.AutoSize = True
        Me.lblEmpLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpLastName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmpLastName.Location = New System.Drawing.Point(53, 64)
        Me.lblEmpLastName.Name = "lblEmpLastName"
        Me.lblEmpLastName.Size = New System.Drawing.Size(46, 13)
        Me.lblEmpLastName.TabIndex = 22
        Me.lblEmpLastName.Text = "นามสกุล"
        '
        'txtEmpLastName
        '
        Me.txtEmpLastName.Location = New System.Drawing.Point(108, 64)
        Me.txtEmpLastName.Name = "txtEmpLastName"
        Me.txtEmpLastName.Size = New System.Drawing.Size(214, 20)
        Me.txtEmpLastName.TabIndex = 2
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = True
        Me.lblEmpName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblEmpName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmpName.Location = New System.Drawing.Point(30, 40)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(69, 13)
        Me.lblEmpName.TabIndex = 19
        Me.lblEmpName.Text = "ชื่อพนักงาน"
        '
        'txtPosition
        '
        Me.txtPosition.Location = New System.Drawing.Point(108, 180)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(214, 20)
        Me.txtPosition.TabIndex = 8
        '
        'lblEmpID
        '
        Me.lblEmpID.AutoSize = True
        Me.lblEmpID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblEmpID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmpID.Location = New System.Drawing.Point(70, 16)
        Me.lblEmpID.Name = "lblEmpID"
        Me.lblEmpID.Size = New System.Drawing.Size(29, 13)
        Me.lblEmpID.TabIndex = 20
        Me.lblEmpID.Text = "รหัส"
        '
        'cboEmployeeGroup
        '
        Me.cboEmployeeGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmployeeGroup.FormattingEnabled = True
        Me.cboEmployeeGroup.Location = New System.Drawing.Point(108, 229)
        Me.cboEmployeeGroup.Name = "cboEmployeeGroup"
        Me.cboEmployeeGroup.Size = New System.Drawing.Size(186, 21)
        Me.cboEmployeeGroup.TabIndex = 10
        '
        'lblEmployeeGroup
        '
        Me.lblEmployeeGroup.AutoSize = True
        Me.lblEmployeeGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblEmployeeGroup.Location = New System.Drawing.Point(31, 229)
        Me.lblEmployeeGroup.Name = "lblEmployeeGroup"
        Me.lblEmployeeGroup.Size = New System.Drawing.Size(68, 13)
        Me.lblEmployeeGroup.TabIndex = 28
        Me.lblEmployeeGroup.Text = "กลุ่มพนักงาน"
        '
        'txtEmpID
        '
        Me.txtEmpID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtEmpID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpID.Location = New System.Drawing.Point(108, 16)
        Me.txtEmpID.Name = "txtEmpID"
        Me.txtEmpID.Size = New System.Drawing.Size(214, 20)
        Me.txtEmpID.TabIndex = 0
        '
        'txtEmpName
        '
        Me.txtEmpName.Location = New System.Drawing.Point(108, 40)
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.Size = New System.Drawing.Size(214, 20)
        Me.txtEmpName.TabIndex = 1
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDepartment.Location = New System.Drawing.Point(62, 204)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(37, 13)
        Me.lblDepartment.TabIndex = 24
        Me.lblDepartment.Text = "แผนก"
        '
        'cboDepartment
        '
        Me.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDepartment.FormattingEnabled = True
        Me.cboDepartment.Location = New System.Drawing.Point(108, 204)
        Me.cboDepartment.Name = "cboDepartment"
        Me.cboDepartment.Size = New System.Drawing.Size(214, 21)
        Me.cboDepartment.TabIndex = 9
        '
        'btnRemovePic
        '
        Me.btnRemovePic.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnRemovePic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemovePic.Location = New System.Drawing.Point(457, 223)
        Me.btnRemovePic.Name = "btnRemovePic"
        Me.btnRemovePic.Size = New System.Drawing.Size(95, 33)
        Me.btnRemovePic.TabIndex = 2
        Me.btnRemovePic.Text = "ลบรูป"
        Me.btnRemovePic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemovePic.UseVisualStyleBackColor = True
        '
        'btnAddPic
        '
        Me.btnAddPic.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddPic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddPic.Location = New System.Drawing.Point(358, 223)
        Me.btnAddPic.Name = "btnAddPic"
        Me.btnAddPic.Size = New System.Drawing.Size(95, 33)
        Me.btnAddPic.TabIndex = 1
        Me.btnAddPic.Text = "เพิ่มรูป"
        Me.btnAddPic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddPic.UseVisualStyleBackColor = True
        '
        'picEmployee
        '
        Me.picEmployee.BackColor = System.Drawing.Color.White
        Me.picEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picEmployee.Location = New System.Drawing.Point(358, 9)
        Me.picEmployee.Name = "picEmployee"
        Me.picEmployee.Size = New System.Drawing.Size(211, 210)
        Me.picEmployee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEmployee.TabIndex = 56
        Me.picEmployee.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = resources.GetString("OpenFileDialog1.Filter")
        '
        'frmEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 601)
        Me.Controls.Add(Me.grbEmployeeDetail)
        Me.Controls.Add(Me.btnRemovePic)
        Me.Controls.Add(Me.btnAddPic)
        Me.Controls.Add(Me.picEmployee)
        Me.Controls.Add(Me.grbEmployee)
        Me.Controls.Add(Me.grbContact)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmployee"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลพนักงาน"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grbContact.ResumeLayout(False)
        Me.grbContact.PerformLayout()
        Me.grbEmployeeDetail.ResumeLayout(False)
        Me.grbEmployeeDetail.PerformLayout()
        Me.grbEmployee.ResumeLayout(False)
        Me.grbEmployee.PerformLayout()
        CType(Me.picEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radwoman As System.Windows.Forms.RadioButton
    Friend WithEvents radman As System.Windows.Forms.RadioButton




    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtImage_Path As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label

    Friend WithEvents grbEmployee As System.Windows.Forms.GroupBox
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents rdbGender_W As System.Windows.Forms.RadioButton
    Friend WithEvents lblPosition As System.Windows.Forms.Label
    Friend WithEvents rdbGender_M As System.Windows.Forms.RadioButton
    Friend WithEvents lblEmpLastName As System.Windows.Forms.Label
    Friend WithEvents txtEmpLastName As System.Windows.Forms.TextBox
    Friend WithEvents lblEmpName As System.Windows.Forms.Label
    Friend WithEvents txtPosition As System.Windows.Forms.TextBox
    Friend WithEvents lblEmpID As System.Windows.Forms.Label
    Friend WithEvents cboEmployeeGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblEmployeeGroup As System.Windows.Forms.Label
    Friend WithEvents txtEmpID As System.Windows.Forms.TextBox
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents cboDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents grbContact As System.Windows.Forms.GroupBox
    Friend WithEvents chkBlackList As System.Windows.Forms.CheckBox
    Friend WithEvents dtpBirth_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDriver_License_No As System.Windows.Forms.TextBox
    Friend WithEvents txtNational_Id As System.Windows.Forms.TextBox
    Friend WithEvents lblDriver_License_No As System.Windows.Forms.Label
    Friend WithEvents lblNational_Id As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblMobile As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents lblTel As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPostcode As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblCountr As System.Windows.Forms.Label
    Friend WithEvents lblProvince As System.Windows.Forms.Label
    Friend WithEvents cboProvince As System.Windows.Forms.ComboBox
    Friend WithEvents cboDistrict As System.Windows.Forms.ComboBox

    Friend WithEvents lblBirth_Date As System.Windows.Forms.Label
    Friend WithEvents btnRemovePic As System.Windows.Forms.Button
    Friend WithEvents btnAddPic As System.Windows.Forms.Button
    Friend WithEvents picEmployee As System.Windows.Forms.PictureBox
    Friend WithEvents lblBlackList As System.Windows.Forms.Label
    Friend WithEvents dtpRecruit_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRecruit_By As System.Windows.Forms.Label
    Friend WithEvents lblRecruit_Date As System.Windows.Forms.Label
    Friend WithEvents lbllblAttachment3 As System.Windows.Forms.Label
    Friend WithEvents lblAttachment2 As System.Windows.Forms.Label
    Friend WithEvents lblAttachment1 As System.Windows.Forms.Label
    Friend WithEvents grbEmployeeDetail As System.Windows.Forms.GroupBox
    Friend WithEvents txtRecruit_By As System.Windows.Forms.TextBox
    Friend WithEvents txtAttachment2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAttachment3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAttachment1 As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnGroupEmployee As System.Windows.Forms.Button
End Class
