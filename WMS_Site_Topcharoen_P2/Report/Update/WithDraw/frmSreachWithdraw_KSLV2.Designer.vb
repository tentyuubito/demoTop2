<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachWithdraw_KSLV2
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
        Me.CanCel = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnProductType_Popup = New System.Windows.Forms.Button
        Me.txtProductType = New System.Windows.Forms.TextBox
        Me.chkSku = New System.Windows.Forms.CheckBox
        Me.chkProductType = New System.Windows.Forms.CheckBox
        Me.cboDocType = New System.Windows.Forms.ComboBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.txtSku_id = New System.Windows.Forms.TextBox
        Me.txtWareHouserId = New System.Windows.Forms.TextBox
        Me.txtSkuDes = New System.Windows.Forms.TextBox
        Me.btnSku = New System.Windows.Forms.Button
        Me.txtWareHouserDes = New System.Windows.Forms.TextBox
        Me.btnWareHouser = New System.Windows.Forms.Button
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.chkDocType = New System.Windows.Forms.CheckBox
        Me.chkRemark = New System.Windows.Forms.CheckBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.txtCustomerId = New System.Windows.Forms.TextBox
        Me.txtCustomerDes = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CanCel
        '
        Me.CanCel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.CanCel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CanCel.Location = New System.Drawing.Point(355, 240)
        Me.CanCel.Name = "CanCel"
        Me.CanCel.Size = New System.Drawing.Size(100, 38)
        Me.CanCel.TabIndex = 16
        Me.CanCel.Text = "ยกเลิก"
        Me.CanCel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CanCel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnProductType_Popup)
        Me.GroupBox1.Controls.Add(Me.txtProductType)
        Me.GroupBox1.Controls.Add(Me.chkSku)
        Me.GroupBox1.Controls.Add(Me.chkProductType)
        Me.GroupBox1.Controls.Add(Me.cboDocType)
        Me.GroupBox1.Controls.Add(Me.chkWarehouse)
        Me.GroupBox1.Controls.Add(Me.txtSku_id)
        Me.GroupBox1.Controls.Add(Me.txtWareHouserId)
        Me.GroupBox1.Controls.Add(Me.txtSkuDes)
        Me.GroupBox1.Controls.Add(Me.btnSku)
        Me.GroupBox1.Controls.Add(Me.txtWareHouserDes)
        Me.GroupBox1.Controls.Add(Me.btnWareHouser)
        Me.GroupBox1.Controls.Add(Me.txtRemark)
        Me.GroupBox1.Controls.Add(Me.chkDocType)
        Me.GroupBox1.Controls.Add(Me.chkRemark)
        Me.GroupBox1.Controls.Add(Me.chkdate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkCustomer)
        Me.GroupBox1.Controls.Add(Me.dtEndDate)
        Me.GroupBox1.Controls.Add(Me.dtStartDate)
        Me.GroupBox1.Controls.Add(Me.txtCustomerId)
        Me.GroupBox1.Controls.Add(Me.txtCustomerDes)
        Me.GroupBox1.Controls.Add(Me.btnCustomer)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(443, 222)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เงื่อนไขกรองรายการ"
        '
        'btnProductType_Popup
        '
        Me.btnProductType_Popup.Enabled = False
        Me.btnProductType_Popup.Location = New System.Drawing.Point(217, 132)
        Me.btnProductType_Popup.Name = "btnProductType_Popup"
        Me.btnProductType_Popup.Size = New System.Drawing.Size(24, 23)
        Me.btnProductType_Popup.TabIndex = 43
        Me.btnProductType_Popup.Text = "..."
        Me.btnProductType_Popup.UseVisualStyleBackColor = True
        '
        'txtProductType
        '
        Me.txtProductType.BackColor = System.Drawing.Color.Gainsboro
        Me.txtProductType.Location = New System.Drawing.Point(109, 133)
        Me.txtProductType.MaxLength = 200
        Me.txtProductType.Name = "txtProductType"
        Me.txtProductType.ReadOnly = True
        Me.txtProductType.Size = New System.Drawing.Size(106, 20)
        Me.txtProductType.TabIndex = 42
        '
        'chkSku
        '
        Me.chkSku.AutoSize = True
        Me.chkSku.Location = New System.Drawing.Point(16, 166)
        Me.chkSku.Name = "chkSku"
        Me.chkSku.Size = New System.Drawing.Size(71, 17)
        Me.chkSku.TabIndex = 40
        Me.chkSku.Text = "รหัสสินค้า"
        Me.chkSku.UseVisualStyleBackColor = True
        '
        'chkProductType
        '
        Me.chkProductType.AutoSize = True
        Me.chkProductType.Location = New System.Drawing.Point(16, 136)
        Me.chkProductType.Name = "chkProductType"
        Me.chkProductType.Size = New System.Drawing.Size(89, 17)
        Me.chkProductType.TabIndex = 40
        Me.chkProductType.Text = "ประเภทสินค้า"
        Me.chkProductType.UseVisualStyleBackColor = True
        '
        'cboDocType
        '
        Me.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocType.Enabled = False
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(109, 105)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(121, 21)
        Me.cboDocType.TabIndex = 39
        '
        'chkWarehouse
        '
        Me.chkWarehouse.AutoSize = True
        Me.chkWarehouse.Location = New System.Drawing.Point(16, 51)
        Me.chkWarehouse.Name = "chkWarehouse"
        Me.chkWarehouse.Size = New System.Drawing.Size(44, 17)
        Me.chkWarehouse.TabIndex = 35
        Me.chkWarehouse.Text = "คลัง"
        Me.chkWarehouse.UseVisualStyleBackColor = True
        '
        'txtSku_id
        '
        Me.txtSku_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSku_id.Location = New System.Drawing.Point(109, 163)
        Me.txtSku_id.MaxLength = 50
        Me.txtSku_id.Name = "txtSku_id"
        Me.txtSku_id.ReadOnly = True
        Me.txtSku_id.Size = New System.Drawing.Size(106, 20)
        Me.txtSku_id.TabIndex = 38
        '
        'txtWareHouserId
        '
        Me.txtWareHouserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWareHouserId.Location = New System.Drawing.Point(109, 49)
        Me.txtWareHouserId.MaxLength = 50
        Me.txtWareHouserId.Name = "txtWareHouserId"
        Me.txtWareHouserId.ReadOnly = True
        Me.txtWareHouserId.Size = New System.Drawing.Size(106, 20)
        Me.txtWareHouserId.TabIndex = 38
        '
        'txtSkuDes
        '
        Me.txtSkuDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSkuDes.Location = New System.Drawing.Point(242, 163)
        Me.txtSkuDes.MaxLength = 500
        Me.txtSkuDes.Name = "txtSkuDes"
        Me.txtSkuDes.ReadOnly = True
        Me.txtSkuDes.Size = New System.Drawing.Size(191, 20)
        Me.txtSkuDes.TabIndex = 37
        '
        'btnSku
        '
        Me.btnSku.Enabled = False
        Me.btnSku.Location = New System.Drawing.Point(217, 162)
        Me.btnSku.Name = "btnSku"
        Me.btnSku.Size = New System.Drawing.Size(24, 23)
        Me.btnSku.TabIndex = 36
        Me.btnSku.Text = "..."
        Me.btnSku.UseVisualStyleBackColor = True
        '
        'txtWareHouserDes
        '
        Me.txtWareHouserDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWareHouserDes.Location = New System.Drawing.Point(242, 49)
        Me.txtWareHouserDes.MaxLength = 500
        Me.txtWareHouserDes.Name = "txtWareHouserDes"
        Me.txtWareHouserDes.ReadOnly = True
        Me.txtWareHouserDes.Size = New System.Drawing.Size(191, 20)
        Me.txtWareHouserDes.TabIndex = 37
        '
        'btnWareHouser
        '
        Me.btnWareHouser.Enabled = False
        Me.btnWareHouser.Location = New System.Drawing.Point(217, 48)
        Me.btnWareHouser.Name = "btnWareHouser"
        Me.btnWareHouser.Size = New System.Drawing.Size(24, 23)
        Me.btnWareHouser.TabIndex = 36
        Me.btnWareHouser.Text = "..."
        Me.btnWareHouser.UseVisualStyleBackColor = True
        '
        'txtRemark
        '
        Me.txtRemark.Enabled = False
        Me.txtRemark.Location = New System.Drawing.Point(109, 191)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(121, 20)
        Me.txtRemark.TabIndex = 12
        '
        'chkDocType
        '
        Me.chkDocType.AutoSize = True
        Me.chkDocType.Location = New System.Drawing.Point(16, 107)
        Me.chkDocType.Name = "chkDocType"
        Me.chkDocType.Size = New System.Drawing.Size(101, 17)
        Me.chkDocType.TabIndex = 7
        Me.chkDocType.Text = "วัตถุประสงค์เบิก"
        Me.chkDocType.UseVisualStyleBackColor = True
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(16, 193)
        Me.chkRemark.Name = "chkRemark"
        Me.chkRemark.Size = New System.Drawing.Size(71, 17)
        Me.chkRemark.TabIndex = 11
        Me.chkRemark.Text = "หมายเหตุ"
        Me.chkRemark.UseVisualStyleBackColor = True
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(16, 78)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(74, 17)
        Me.chkdate.TabIndex = 4
        Me.chkdate.Text = "ตั้งแต่วันที่"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(241, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "ถึง"
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(16, 22)
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
        Me.dtEndDate.Location = New System.Drawing.Point(269, 76)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(119, 20)
        Me.dtEndDate.TabIndex = 6
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = ""
        Me.dtStartDate.Enabled = False
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(109, 76)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(119, 20)
        Me.dtStartDate.TabIndex = 5
        '
        'txtCustomerId
        '
        Me.txtCustomerId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerId.Location = New System.Drawing.Point(109, 20)
        Me.txtCustomerId.MaxLength = 50
        Me.txtCustomerId.Name = "txtCustomerId"
        Me.txtCustomerId.ReadOnly = True
        Me.txtCustomerId.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomerId.TabIndex = 27
        '
        'txtCustomerDes
        '
        Me.txtCustomerDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerDes.Location = New System.Drawing.Point(242, 20)
        Me.txtCustomerDes.MaxLength = 500
        Me.txtCustomerDes.Name = "txtCustomerDes"
        Me.txtCustomerDes.ReadOnly = True
        Me.txtCustomerDes.Size = New System.Drawing.Size(191, 20)
        Me.txtCustomerDes.TabIndex = 24
        '
        'btnCustomer
        '
        Me.btnCustomer.Enabled = False
        Me.btnCustomer.Location = New System.Drawing.Point(217, 19)
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
        Me.OK.Location = New System.Drawing.Point(249, 240)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 15
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'frmSreachWithdraw_KSLV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 285)
        Me.Controls.Add(Me.CanCel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachWithdraw_KSLV2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : WithDraw"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CanCel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSku As System.Windows.Forms.CheckBox
    Friend WithEvents chkProductType As System.Windows.Forms.CheckBox
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents chkWarehouse As System.Windows.Forms.CheckBox
    Friend WithEvents txtSku_id As System.Windows.Forms.TextBox
    Friend WithEvents txtWareHouserId As System.Windows.Forms.TextBox
    Friend WithEvents txtSkuDes As System.Windows.Forms.TextBox
    Friend WithEvents btnSku As System.Windows.Forms.Button
    Friend WithEvents txtWareHouserDes As System.Windows.Forms.TextBox
    Friend WithEvents btnWareHouser As System.Windows.Forms.Button
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents chkDocType As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemark As System.Windows.Forms.CheckBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomerId As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDes As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents btnProductType_Popup As System.Windows.Forms.Button
    Friend WithEvents txtProductType As System.Windows.Forms.TextBox
End Class
