<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachNonMoving_KSL
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
        Me.Cancel = New System.Windows.Forms.Button
        Me.gbCriteria = New System.Windows.Forms.GroupBox
        Me.chkProductType = New System.Windows.Forms.CheckBox
        Me.txtProductType_id = New System.Windows.Forms.TextBox
        Me.txtProductTypeDes = New System.Windows.Forms.TextBox
        Me.btnProduct = New System.Windows.Forms.Button
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.chkStatus = New System.Windows.Forms.CheckBox
        Me.txtStart = New System.Windows.Forms.TextBox
        Me.chkNonMoving = New System.Windows.Forms.CheckBox
        Me.dtWH = New System.Windows.Forms.DateTimePicker
        Me.dtRS = New System.Windows.Forms.DateTimePicker
        Me.chkMaxWH = New System.Windows.Forms.CheckBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.txtWareHouserId = New System.Windows.Forms.TextBox
        Me.txtWareHouserDes = New System.Windows.Forms.TextBox
        Me.btnWareHouser = New System.Windows.Forms.Button
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.chkMaxRS = New System.Windows.Forms.CheckBox
        Me.chkRemark = New System.Windows.Forms.CheckBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.lblToDate = New System.Windows.Forms.Label
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.txtCustomerId = New System.Windows.Forms.TextBox
        Me.txtCustomerDes = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.txtEND = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSku = New System.Windows.Forms.CheckBox
        Me.txtSku_id = New System.Windows.Forms.TextBox
        Me.txtSkuDes = New System.Windows.Forms.TextBox
        Me.btnSku = New System.Windows.Forms.Button
        Me.gbCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel.Location = New System.Drawing.Point(354, 359)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 38)
        Me.Cancel.TabIndex = 16
        Me.Cancel.Text = "ยกเลิก"
        Me.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.chkSku)
        Me.gbCriteria.Controls.Add(Me.txtSku_id)
        Me.gbCriteria.Controls.Add(Me.chkProductType)
        Me.gbCriteria.Controls.Add(Me.txtSkuDes)
        Me.gbCriteria.Controls.Add(Me.txtProductType_id)
        Me.gbCriteria.Controls.Add(Me.btnSku)
        Me.gbCriteria.Controls.Add(Me.txtProductTypeDes)
        Me.gbCriteria.Controls.Add(Me.btnProduct)
        Me.gbCriteria.Controls.Add(Me.cboStatus)
        Me.gbCriteria.Controls.Add(Me.chkStatus)
        Me.gbCriteria.Controls.Add(Me.txtEND)
        Me.gbCriteria.Controls.Add(Me.txtStart)
        Me.gbCriteria.Controls.Add(Me.chkNonMoving)
        Me.gbCriteria.Controls.Add(Me.dtWH)
        Me.gbCriteria.Controls.Add(Me.dtRS)
        Me.gbCriteria.Controls.Add(Me.chkMaxWH)
        Me.gbCriteria.Controls.Add(Me.chkWarehouse)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserId)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserDes)
        Me.gbCriteria.Controls.Add(Me.btnWareHouser)
        Me.gbCriteria.Controls.Add(Me.txtRemark)
        Me.gbCriteria.Controls.Add(Me.chkMaxRS)
        Me.gbCriteria.Controls.Add(Me.chkRemark)
        Me.gbCriteria.Controls.Add(Me.chkdate)
        Me.gbCriteria.Controls.Add(Me.Label1)
        Me.gbCriteria.Controls.Add(Me.lblToDate)
        Me.gbCriteria.Controls.Add(Me.chkCustomer)
        Me.gbCriteria.Controls.Add(Me.dtEndDate)
        Me.gbCriteria.Controls.Add(Me.dtStartDate)
        Me.gbCriteria.Controls.Add(Me.txtCustomerId)
        Me.gbCriteria.Controls.Add(Me.txtCustomerDes)
        Me.gbCriteria.Controls.Add(Me.btnCustomer)
        Me.gbCriteria.Location = New System.Drawing.Point(11, 12)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(443, 309)
        Me.gbCriteria.TabIndex = 14
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'chkProductType
        '
        Me.chkProductType.AutoSize = True
        Me.chkProductType.Location = New System.Drawing.Point(10, 75)
        Me.chkProductType.Name = "chkProductType"
        Me.chkProductType.Size = New System.Drawing.Size(63, 17)
        Me.chkProductType.TabIndex = 51
        Me.chkProductType.Text = "ประเภท"
        Me.chkProductType.UseVisualStyleBackColor = True
        '
        'txtProductType_id
        '
        Me.txtProductType_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtProductType_id.Location = New System.Drawing.Point(110, 75)
        Me.txtProductType_id.MaxLength = 50
        Me.txtProductType_id.Name = "txtProductType_id"
        Me.txtProductType_id.ReadOnly = True
        Me.txtProductType_id.Size = New System.Drawing.Size(106, 20)
        Me.txtProductType_id.TabIndex = 50
        '
        'txtProductTypeDes
        '
        Me.txtProductTypeDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtProductTypeDes.Location = New System.Drawing.Point(256, 75)
        Me.txtProductTypeDes.MaxLength = 500
        Me.txtProductTypeDes.Name = "txtProductTypeDes"
        Me.txtProductTypeDes.ReadOnly = True
        Me.txtProductTypeDes.Size = New System.Drawing.Size(171, 20)
        Me.txtProductTypeDes.TabIndex = 49
        '
        'btnProduct
        '
        Me.btnProduct.Enabled = False
        Me.btnProduct.Location = New System.Drawing.Point(224, 74)
        Me.btnProduct.Name = "btnProduct"
        Me.btnProduct.Size = New System.Drawing.Size(24, 23)
        Me.btnProduct.TabIndex = 48
        Me.btnProduct.Text = "..."
        Me.btnProduct.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"ไม่พอจ่าย", "พอจ่าย"})
        Me.cboStatus.Location = New System.Drawing.Point(110, 128)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(121, 21)
        Me.cboStatus.TabIndex = 47
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Location = New System.Drawing.Point(10, 130)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(60, 17)
        Me.chkStatus.TabIndex = 46
        Me.chkStatus.Text = "สถานะ "
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'txtStart
        '
        Me.txtStart.Enabled = False
        Me.txtStart.Location = New System.Drawing.Point(149, 238)
        Me.txtStart.Name = "txtStart"
        Me.txtStart.Size = New System.Drawing.Size(31, 20)
        Me.txtStart.TabIndex = 43
        '
        'chkNonMoving
        '
        Me.chkNonMoving.AutoSize = True
        Me.chkNonMoving.Location = New System.Drawing.Point(10, 240)
        Me.chkNonMoving.Name = "chkNonMoving"
        Me.chkNonMoving.Size = New System.Drawing.Size(144, 17)
        Me.chkNonMoving.TabIndex = 42
        Me.chkNonMoving.Text = "จำนวนวันที่ไม่เคลื่อนไหว"
        Me.chkNonMoving.UseVisualStyleBackColor = True
        '
        'dtWH
        '
        Me.dtWH.CustomFormat = ""
        Me.dtWH.Enabled = False
        Me.dtWH.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtWH.Location = New System.Drawing.Point(110, 212)
        Me.dtWH.Name = "dtWH"
        Me.dtWH.Size = New System.Drawing.Size(121, 20)
        Me.dtWH.TabIndex = 41
        '
        'dtRS
        '
        Me.dtRS.CustomFormat = ""
        Me.dtRS.Enabled = False
        Me.dtRS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRS.Location = New System.Drawing.Point(110, 186)
        Me.dtRS.Name = "dtRS"
        Me.dtRS.Size = New System.Drawing.Size(121, 20)
        Me.dtRS.TabIndex = 5
        '
        'chkMaxWH
        '
        Me.chkMaxWH.AutoSize = True
        Me.chkMaxWH.Location = New System.Drawing.Point(10, 214)
        Me.chkMaxWH.Name = "chkMaxWH"
        Me.chkMaxWH.Size = New System.Drawing.Size(94, 17)
        Me.chkMaxWH.TabIndex = 40
        Me.chkMaxWH.Text = "จ่ายครั้งสุดท้าย"
        Me.chkMaxWH.UseVisualStyleBackColor = True
        '
        'chkWarehouse
        '
        Me.chkWarehouse.AutoSize = True
        Me.chkWarehouse.Location = New System.Drawing.Point(10, 51)
        Me.chkWarehouse.Name = "chkWarehouse"
        Me.chkWarehouse.Size = New System.Drawing.Size(44, 17)
        Me.chkWarehouse.TabIndex = 35
        Me.chkWarehouse.Text = "คลัง"
        Me.chkWarehouse.UseVisualStyleBackColor = True
        '
        'txtWareHouserId
        '
        Me.txtWareHouserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWareHouserId.Location = New System.Drawing.Point(110, 49)
        Me.txtWareHouserId.MaxLength = 50
        Me.txtWareHouserId.Name = "txtWareHouserId"
        Me.txtWareHouserId.ReadOnly = True
        Me.txtWareHouserId.Size = New System.Drawing.Size(106, 20)
        Me.txtWareHouserId.TabIndex = 38
        '
        'txtWareHouserDes
        '
        Me.txtWareHouserDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWareHouserDes.Location = New System.Drawing.Point(256, 49)
        Me.txtWareHouserDes.MaxLength = 500
        Me.txtWareHouserDes.Name = "txtWareHouserDes"
        Me.txtWareHouserDes.ReadOnly = True
        Me.txtWareHouserDes.Size = New System.Drawing.Size(171, 20)
        Me.txtWareHouserDes.TabIndex = 37
        '
        'btnWareHouser
        '
        Me.btnWareHouser.Enabled = False
        Me.btnWareHouser.Location = New System.Drawing.Point(224, 48)
        Me.btnWareHouser.Name = "btnWareHouser"
        Me.btnWareHouser.Size = New System.Drawing.Size(24, 23)
        Me.btnWareHouser.TabIndex = 36
        Me.btnWareHouser.Text = "..."
        Me.btnWareHouser.UseVisualStyleBackColor = True
        '
        'txtRemark
        '
        Me.txtRemark.Enabled = False
        Me.txtRemark.Location = New System.Drawing.Point(109, 267)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(121, 20)
        Me.txtRemark.TabIndex = 12
        Me.txtRemark.Visible = False
        '
        'chkMaxRS
        '
        Me.chkMaxRS.AutoSize = True
        Me.chkMaxRS.Location = New System.Drawing.Point(10, 188)
        Me.chkMaxRS.Name = "chkMaxRS"
        Me.chkMaxRS.Size = New System.Drawing.Size(89, 17)
        Me.chkMaxRS.TabIndex = 7
        Me.chkMaxRS.Text = "รับครั้งสุดท้าย"
        Me.chkMaxRS.UseVisualStyleBackColor = True
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(10, 269)
        Me.chkRemark.Name = "chkRemark"
        Me.chkRemark.Size = New System.Drawing.Size(71, 17)
        Me.chkRemark.TabIndex = 11
        Me.chkRemark.Text = "หมายเหตุ"
        Me.chkRemark.UseVisualStyleBackColor = True
        Me.chkRemark.Visible = False
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(10, 160)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(74, 17)
        Me.chkdate.TabIndex = 4
        Me.chkdate.Text = "ตั้งแต่วันที่"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(240, 162)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(19, 13)
        Me.lblToDate.TabIndex = 19
        Me.lblToDate.Text = "ถึง"
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(10, 22)
        Me.chkCustomer.Name = "chkCustomer"
        Me.chkCustomer.Size = New System.Drawing.Size(54, 17)
        Me.chkCustomer.TabIndex = 0
        Me.chkCustomer.Text = "บริษัท"
        Me.chkCustomer.UseVisualStyleBackColor = True
        '
        'dtEndDate
        '
        Me.dtEndDate.CustomFormat = ""
        Me.dtEndDate.Enabled = False
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEndDate.Location = New System.Drawing.Point(266, 158)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(119, 20)
        Me.dtEndDate.TabIndex = 6
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = ""
        Me.dtStartDate.Enabled = False
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(110, 158)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(121, 20)
        Me.dtStartDate.TabIndex = 5
        '
        'txtCustomerId
        '
        Me.txtCustomerId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerId.Location = New System.Drawing.Point(110, 20)
        Me.txtCustomerId.MaxLength = 50
        Me.txtCustomerId.Name = "txtCustomerId"
        Me.txtCustomerId.ReadOnly = True
        Me.txtCustomerId.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomerId.TabIndex = 27
        '
        'txtCustomerDes
        '
        Me.txtCustomerDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerDes.Location = New System.Drawing.Point(256, 20)
        Me.txtCustomerDes.MaxLength = 500
        Me.txtCustomerDes.Name = "txtCustomerDes"
        Me.txtCustomerDes.ReadOnly = True
        Me.txtCustomerDes.Size = New System.Drawing.Size(171, 20)
        Me.txtCustomerDes.TabIndex = 24
        '
        'btnCustomer
        '
        Me.btnCustomer.Enabled = False
        Me.btnCustomer.Location = New System.Drawing.Point(224, 19)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer.TabIndex = 1
        Me.btnCustomer.Text = "..."
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OK.Location = New System.Drawing.Point(248, 359)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 15
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'txtEND
        '
        Me.txtEND.Enabled = False
        Me.txtEND.Location = New System.Drawing.Point(211, 238)
        Me.txtEND.Name = "txtEND"
        Me.txtEND.Size = New System.Drawing.Size(31, 20)
        Me.txtEND.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(186, 242)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "ถึง"
        '
        'chkSku
        '
        Me.chkSku.AutoSize = True
        Me.chkSku.Location = New System.Drawing.Point(10, 103)
        Me.chkSku.Name = "chkSku"
        Me.chkSku.Size = New System.Drawing.Size(71, 17)
        Me.chkSku.TabIndex = 44
        Me.chkSku.Text = "รหัสสินค้า"
        Me.chkSku.UseVisualStyleBackColor = True
        '
        'txtSku_id
        '
        Me.txtSku_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSku_id.Location = New System.Drawing.Point(110, 100)
        Me.txtSku_id.MaxLength = 50
        Me.txtSku_id.Name = "txtSku_id"
        Me.txtSku_id.ReadOnly = True
        Me.txtSku_id.Size = New System.Drawing.Size(106, 20)
        Me.txtSku_id.TabIndex = 43
        '
        'txtSkuDes
        '
        Me.txtSkuDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSkuDes.Location = New System.Drawing.Point(256, 100)
        Me.txtSkuDes.MaxLength = 500
        Me.txtSkuDes.Name = "txtSkuDes"
        Me.txtSkuDes.ReadOnly = True
        Me.txtSkuDes.Size = New System.Drawing.Size(171, 20)
        Me.txtSkuDes.TabIndex = 42
        '
        'btnSku
        '
        Me.btnSku.Enabled = False
        Me.btnSku.Location = New System.Drawing.Point(224, 99)
        Me.btnSku.Name = "btnSku"
        Me.btnSku.Size = New System.Drawing.Size(24, 23)
        Me.btnSku.TabIndex = 41
        Me.btnSku.Text = "..."
        Me.btnSku.UseVisualStyleBackColor = True
        '
        'frmSreachNonMoving_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 409)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachNonMoving_KSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : DeadStock"
        Me.gbCriteria.ResumeLayout(False)
        Me.gbCriteria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents txtStart As System.Windows.Forms.TextBox
    Friend WithEvents chkNonMoving As System.Windows.Forms.CheckBox
    Friend WithEvents dtWH As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtRS As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkMaxWH As System.Windows.Forms.CheckBox
    Friend WithEvents chkWarehouse As System.Windows.Forms.CheckBox
    Friend WithEvents txtWareHouserId As System.Windows.Forms.TextBox
    Friend WithEvents txtWareHouserDes As System.Windows.Forms.TextBox
    Friend WithEvents btnWareHouser As System.Windows.Forms.Button
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents chkMaxRS As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemark As System.Windows.Forms.CheckBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomerId As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDes As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chkProductType As System.Windows.Forms.CheckBox
    Friend WithEvents txtProductType_id As System.Windows.Forms.TextBox
    Friend WithEvents txtProductTypeDes As System.Windows.Forms.TextBox
    Friend WithEvents btnProduct As System.Windows.Forms.Button
    Friend WithEvents txtEND As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSku As System.Windows.Forms.CheckBox
    Friend WithEvents txtSku_id As System.Windows.Forms.TextBox
    Friend WithEvents txtSkuDes As System.Windows.Forms.TextBox
    Friend WithEvents btnSku As System.Windows.Forms.Button
End Class
