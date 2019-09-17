<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachSO_KSL
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
        Me.txtSO_NO = New System.Windows.Forms.TextBox
        Me.chkSo_No = New System.Windows.Forms.CheckBox
        Me.chkSku_id = New System.Windows.Forms.CheckBox
        Me.txtSku_id = New System.Windows.Forms.TextBox
        Me.txtSkuDes = New System.Windows.Forms.TextBox
        Me.btnSku = New System.Windows.Forms.Button
        Me.cboDistri = New System.Windows.Forms.ComboBox
        Me.cboPayment = New System.Windows.Forms.ComboBox
        Me.chkPayment = New System.Windows.Forms.CheckBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.cboDocType = New System.Windows.Forms.ComboBox
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.chkDocType = New System.Windows.Forms.CheckBox
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
        Me.gbCriteria.Controls.Add(Me.txtSO_NO)
        Me.gbCriteria.Controls.Add(Me.chkSo_No)
        Me.gbCriteria.Controls.Add(Me.chkSku_id)
        Me.gbCriteria.Controls.Add(Me.txtSku_id)
        Me.gbCriteria.Controls.Add(Me.txtSkuDes)
        Me.gbCriteria.Controls.Add(Me.btnSku)
        Me.gbCriteria.Controls.Add(Me.cboDistri)
        Me.gbCriteria.Controls.Add(Me.cboPayment)
        Me.gbCriteria.Controls.Add(Me.chkPayment)
        Me.gbCriteria.Controls.Add(Me.chkWarehouse)
        Me.gbCriteria.Controls.Add(Me.cboDocType)
        Me.gbCriteria.Controls.Add(Me.txtRemark)
        Me.gbCriteria.Controls.Add(Me.chkDocType)
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
        Me.gbCriteria.Size = New System.Drawing.Size(443, 261)
        Me.gbCriteria.TabIndex = 11
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'txtSO_NO
        '
        Me.txtSO_NO.Enabled = False
        Me.txtSO_NO.Location = New System.Drawing.Point(104, 28)
        Me.txtSO_NO.Name = "txtSO_NO"
        Me.txtSO_NO.Size = New System.Drawing.Size(121, 20)
        Me.txtSO_NO.TabIndex = 48
        '
        'chkSo_No
        '
        Me.chkSo_No.AutoSize = True
        Me.chkSo_No.Location = New System.Drawing.Point(11, 30)
        Me.chkSo_No.Name = "chkSo_No"
        Me.chkSo_No.Size = New System.Drawing.Size(87, 17)
        Me.chkSo_No.TabIndex = 47
        Me.chkSo_No.Text = "เลขที่ใบสั่งซื้อ"
        Me.chkSo_No.UseVisualStyleBackColor = True
        '
        'chkSku_id
        '
        Me.chkSku_id.AutoSize = True
        Me.chkSku_id.Location = New System.Drawing.Point(11, 169)
        Me.chkSku_id.Name = "chkSku_id"
        Me.chkSku_id.Size = New System.Drawing.Size(71, 17)
        Me.chkSku_id.TabIndex = 46
        Me.chkSku_id.Text = "รหัสสินค้า"
        Me.chkSku_id.UseVisualStyleBackColor = True
        '
        'txtSku_id
        '
        Me.txtSku_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSku_id.Location = New System.Drawing.Point(104, 167)
        Me.txtSku_id.MaxLength = 50
        Me.txtSku_id.Name = "txtSku_id"
        Me.txtSku_id.ReadOnly = True
        Me.txtSku_id.Size = New System.Drawing.Size(106, 20)
        Me.txtSku_id.TabIndex = 45
        '
        'txtSkuDes
        '
        Me.txtSkuDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSkuDes.Location = New System.Drawing.Point(237, 167)
        Me.txtSkuDes.MaxLength = 500
        Me.txtSkuDes.Name = "txtSkuDes"
        Me.txtSkuDes.ReadOnly = True
        Me.txtSkuDes.Size = New System.Drawing.Size(191, 20)
        Me.txtSkuDes.TabIndex = 44
        '
        'btnSku
        '
        Me.btnSku.Enabled = False
        Me.btnSku.Location = New System.Drawing.Point(212, 166)
        Me.btnSku.Name = "btnSku"
        Me.btnSku.Size = New System.Drawing.Size(24, 23)
        Me.btnSku.TabIndex = 43
        Me.btnSku.Text = "..."
        Me.btnSku.UseVisualStyleBackColor = True
        '
        'cboDistri
        '
        Me.cboDistri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistri.Enabled = False
        Me.cboDistri.FormattingEnabled = True
        Me.cboDistri.Location = New System.Drawing.Point(104, 83)
        Me.cboDistri.Name = "cboDistri"
        Me.cboDistri.Size = New System.Drawing.Size(153, 21)
        Me.cboDistri.TabIndex = 42
        '
        'cboPayment
        '
        Me.cboPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPayment.Enabled = False
        Me.cboPayment.FormattingEnabled = True
        Me.cboPayment.Items.AddRange(New Object() {"กำลังจ่ายสินค้า", "จ่ายสินค้าเสร็จแล้ว", "ไม่สามารถจ่ายสินค้าได้"})
        Me.cboPayment.Location = New System.Drawing.Point(104, 197)
        Me.cboPayment.Name = "cboPayment"
        Me.cboPayment.Size = New System.Drawing.Size(121, 21)
        Me.cboPayment.TabIndex = 41
        '
        'chkPayment
        '
        Me.chkPayment.AutoSize = True
        Me.chkPayment.Location = New System.Drawing.Point(11, 201)
        Me.chkPayment.Name = "chkPayment"
        Me.chkPayment.Size = New System.Drawing.Size(93, 17)
        Me.chkPayment.TabIndex = 40
        Me.chkPayment.Text = "สถานะการจ่าย"
        Me.chkPayment.UseVisualStyleBackColor = True
        '
        'chkWarehouse
        '
        Me.chkWarehouse.AutoSize = True
        Me.chkWarehouse.Location = New System.Drawing.Point(11, 85)
        Me.chkWarehouse.Name = "chkWarehouse"
        Me.chkWarehouse.Size = New System.Drawing.Size(85, 17)
        Me.chkWarehouse.TabIndex = 2
        Me.chkWarehouse.Text = "ศูนย์กระจาย"
        Me.chkWarehouse.UseVisualStyleBackColor = True
        '
        'cboDocType
        '
        Me.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocType.Enabled = False
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Location = New System.Drawing.Point(104, 140)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(121, 21)
        Me.cboDocType.TabIndex = 39
        '
        'txtRemark
        '
        Me.txtRemark.Enabled = False
        Me.txtRemark.Location = New System.Drawing.Point(104, 226)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(121, 20)
        Me.txtRemark.TabIndex = 12
        '
        'chkDocType
        '
        Me.chkDocType.AutoSize = True
        Me.chkDocType.Location = New System.Drawing.Point(11, 142)
        Me.chkDocType.Name = "chkDocType"
        Me.chkDocType.Size = New System.Drawing.Size(99, 17)
        Me.chkDocType.TabIndex = 7
        Me.chkDocType.Text = "ประเภทการจ่าย"
        Me.chkDocType.UseVisualStyleBackColor = True
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(11, 228)
        Me.chkRemark.Name = "chkRemark"
        Me.chkRemark.Size = New System.Drawing.Size(71, 17)
        Me.chkRemark.TabIndex = 11
        Me.chkRemark.Text = "หมายเหตุ"
        Me.chkRemark.UseVisualStyleBackColor = True
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(11, 113)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(74, 17)
        Me.chkdate.TabIndex = 4
        Me.chkdate.Text = "ตั้งแต่วันที่"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(236, 115)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(19, 13)
        Me.lblToDate.TabIndex = 19
        Me.lblToDate.Text = "ถึง"
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(11, 57)
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
        Me.dtEndDate.Location = New System.Drawing.Point(264, 111)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(119, 20)
        Me.dtEndDate.TabIndex = 6
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = ""
        Me.dtStartDate.Enabled = False
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(104, 112)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(119, 20)
        Me.dtStartDate.TabIndex = 5
        '
        'txtCustomerId
        '
        Me.txtCustomerId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerId.Location = New System.Drawing.Point(104, 55)
        Me.txtCustomerId.MaxLength = 50
        Me.txtCustomerId.Name = "txtCustomerId"
        Me.txtCustomerId.ReadOnly = True
        Me.txtCustomerId.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomerId.TabIndex = 27
        '
        'txtCustomerDes
        '
        Me.txtCustomerDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerDes.Location = New System.Drawing.Point(237, 55)
        Me.txtCustomerDes.MaxLength = 500
        Me.txtCustomerDes.Name = "txtCustomerDes"
        Me.txtCustomerDes.ReadOnly = True
        Me.txtCustomerDes.Size = New System.Drawing.Size(191, 20)
        Me.txtCustomerDes.TabIndex = 24
        '
        'btnCustomer
        '
        Me.btnCustomer.Enabled = False
        Me.btnCustomer.Location = New System.Drawing.Point(212, 54)
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
        Me.Cancel.Location = New System.Drawing.Point(355, 279)
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
        Me.OK.Location = New System.Drawing.Point(249, 279)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 12
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'frmSreachSO_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 329)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachSO_KSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : SO"
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
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents chkDocType As System.Windows.Forms.CheckBox
    Friend WithEvents cboPayment As System.Windows.Forms.ComboBox
    Friend WithEvents chkPayment As System.Windows.Forms.CheckBox
    Friend WithEvents cboDistri As System.Windows.Forms.ComboBox
    Friend WithEvents chkWarehouse As System.Windows.Forms.CheckBox
    Friend WithEvents chkSku_id As System.Windows.Forms.CheckBox
    Friend WithEvents txtSku_id As System.Windows.Forms.TextBox
    Friend WithEvents txtSkuDes As System.Windows.Forms.TextBox
    Friend WithEvents btnSku As System.Windows.Forms.Button
    Friend WithEvents txtSO_NO As System.Windows.Forms.TextBox
    Friend WithEvents chkSo_No As System.Windows.Forms.CheckBox
End Class
