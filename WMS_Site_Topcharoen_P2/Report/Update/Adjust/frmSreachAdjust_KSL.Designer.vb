<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachAdjust_KSL
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
        Me.gbCriteria = New System.Windows.Forms.GroupBox
        Me.chkProductType = New System.Windows.Forms.CheckBox
        Me.chkSku = New System.Windows.Forms.CheckBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.txtProductType_id = New System.Windows.Forms.TextBox
        Me.txtSku_id = New System.Windows.Forms.TextBox
        Me.txtWareHouserId = New System.Windows.Forms.TextBox
        Me.txtProductTypeDes = New System.Windows.Forms.TextBox
        Me.btnProduct = New System.Windows.Forms.Button
        Me.txtSkuDes = New System.Windows.Forms.TextBox
        Me.btnSku = New System.Windows.Forms.Button
        Me.txtWareHouserDes = New System.Windows.Forms.TextBox
        Me.btnWareHouser = New System.Windows.Forms.Button
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.chkRemark = New System.Windows.Forms.CheckBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.lblToDate = New System.Windows.Forms.Label
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.txtCustomerId = New System.Windows.Forms.TextBox
        Me.txtCustomerDes = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.gbCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.chkProductType)
        Me.gbCriteria.Controls.Add(Me.chkSku)
        Me.gbCriteria.Controls.Add(Me.chkWarehouse)
        Me.gbCriteria.Controls.Add(Me.txtProductType_id)
        Me.gbCriteria.Controls.Add(Me.txtSku_id)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserId)
        Me.gbCriteria.Controls.Add(Me.txtProductTypeDes)
        Me.gbCriteria.Controls.Add(Me.btnProduct)
        Me.gbCriteria.Controls.Add(Me.txtSkuDes)
        Me.gbCriteria.Controls.Add(Me.btnSku)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserDes)
        Me.gbCriteria.Controls.Add(Me.btnWareHouser)
        Me.gbCriteria.Controls.Add(Me.txtRemark)
        Me.gbCriteria.Controls.Add(Me.chkRemark)
        Me.gbCriteria.Controls.Add(Me.chkdate)
        Me.gbCriteria.Controls.Add(Me.lblToDate)
        Me.gbCriteria.Controls.Add(Me.chkCustomer)
        Me.gbCriteria.Controls.Add(Me.dtEndDate)
        Me.gbCriteria.Controls.Add(Me.dtStartDate)
        Me.gbCriteria.Controls.Add(Me.txtCustomerId)
        Me.gbCriteria.Controls.Add(Me.txtCustomerDes)
        Me.gbCriteria.Controls.Add(Me.btnCustomer)
        Me.gbCriteria.Location = New System.Drawing.Point(12, 12)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(443, 205)
        Me.gbCriteria.TabIndex = 11
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'chkProductType
        '
        Me.chkProductType.AutoSize = True
        Me.chkProductType.Location = New System.Drawing.Point(16, 112)
        Me.chkProductType.Name = "chkProductType"
        Me.chkProductType.Size = New System.Drawing.Size(72, 17)
        Me.chkProductType.TabIndex = 42
        Me.chkProductType.Text = "กลุ่มสินค้า"
        Me.chkProductType.UseVisualStyleBackColor = True
        '
        'chkSku
        '
        Me.chkSku.AutoSize = True
        Me.chkSku.Location = New System.Drawing.Point(16, 140)
        Me.chkSku.Name = "chkSku"
        Me.chkSku.Size = New System.Drawing.Size(71, 17)
        Me.chkSku.TabIndex = 40
        Me.chkSku.Text = "รหัสสินค้า"
        Me.chkSku.UseVisualStyleBackColor = True
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
        'txtProductType_id
        '
        Me.txtProductType_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtProductType_id.Location = New System.Drawing.Point(109, 111)
        Me.txtProductType_id.MaxLength = 50
        Me.txtProductType_id.Name = "txtProductType_id"
        Me.txtProductType_id.ReadOnly = True
        Me.txtProductType_id.Size = New System.Drawing.Size(106, 20)
        Me.txtProductType_id.TabIndex = 38
        '
        'txtSku_id
        '
        Me.txtSku_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSku_id.Location = New System.Drawing.Point(109, 137)
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
        'txtProductTypeDes
        '
        Me.txtProductTypeDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtProductTypeDes.Location = New System.Drawing.Point(242, 111)
        Me.txtProductTypeDes.MaxLength = 500
        Me.txtProductTypeDes.Name = "txtProductTypeDes"
        Me.txtProductTypeDes.ReadOnly = True
        Me.txtProductTypeDes.Size = New System.Drawing.Size(191, 20)
        Me.txtProductTypeDes.TabIndex = 37
        '
        'btnProduct
        '
        Me.btnProduct.Enabled = False
        Me.btnProduct.Location = New System.Drawing.Point(217, 110)
        Me.btnProduct.Name = "btnProduct"
        Me.btnProduct.Size = New System.Drawing.Size(24, 23)
        Me.btnProduct.TabIndex = 36
        Me.btnProduct.Text = "..."
        Me.btnProduct.UseVisualStyleBackColor = True
        '
        'txtSkuDes
        '
        Me.txtSkuDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSkuDes.Location = New System.Drawing.Point(242, 137)
        Me.txtSkuDes.MaxLength = 500
        Me.txtSkuDes.Name = "txtSkuDes"
        Me.txtSkuDes.ReadOnly = True
        Me.txtSkuDes.Size = New System.Drawing.Size(191, 20)
        Me.txtSkuDes.TabIndex = 37
        '
        'btnSku
        '
        Me.btnSku.Enabled = False
        Me.btnSku.Location = New System.Drawing.Point(217, 136)
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
        Me.txtRemark.Location = New System.Drawing.Point(109, 170)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(121, 20)
        Me.txtRemark.TabIndex = 12
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(16, 172)
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
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(241, 80)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(19, 13)
        Me.lblToDate.TabIndex = 19
        Me.lblToDate.Text = "ถึง"
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
        'Cancel
        '
        Me.Cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel.Location = New System.Drawing.Point(355, 223)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 38)
        Me.Cancel.TabIndex = 13
        Me.Cancel.Text = "ยกเลิก"
        Me.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OK.Location = New System.Drawing.Point(249, 223)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 12
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'frmSreachAdjust_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 269)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachAdjust_KSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : Adjust"
        Me.gbCriteria.ResumeLayout(False)
        Me.gbCriteria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents chkRemark As System.Windows.Forms.CheckBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomerId As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDes As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents chkWarehouse As System.Windows.Forms.CheckBox
    Friend WithEvents txtWareHouserId As System.Windows.Forms.TextBox
    Friend WithEvents txtWareHouserDes As System.Windows.Forms.TextBox
    Friend WithEvents btnWareHouser As System.Windows.Forms.Button
    Friend WithEvents chkSku As System.Windows.Forms.CheckBox
    Friend WithEvents txtSku_id As System.Windows.Forms.TextBox
    Friend WithEvents txtSkuDes As System.Windows.Forms.TextBox
    Friend WithEvents btnSku As System.Windows.Forms.Button
    Friend WithEvents chkProductType As System.Windows.Forms.CheckBox
    Friend WithEvents txtProductType_id As System.Windows.Forms.TextBox
    Friend WithEvents txtProductTypeDes As System.Windows.Forms.TextBox
    Friend WithEvents btnProduct As System.Windows.Forms.Button
End Class
