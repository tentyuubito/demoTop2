<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachPO_KSL
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
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.chkSupplier = New System.Windows.Forms.CheckBox
        Me.chkRemark = New System.Windows.Forms.CheckBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.txtSupplierId = New System.Windows.Forms.TextBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.chkSend = New System.Windows.Forms.CheckBox
        Me.txtSupplierDes = New System.Windows.Forms.TextBox
        Me.lblToDate = New System.Windows.Forms.Label
        Me.btnSupplier = New System.Windows.Forms.Button
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtExpire = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.txtCustomerId = New System.Windows.Forms.TextBox
        Me.txtCustomerDes = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.cboDistri = New System.Windows.Forms.ComboBox
        Me.gbCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.cboDistri)
        Me.gbCriteria.Controls.Add(Me.txtRemark)
        Me.gbCriteria.Controls.Add(Me.chkSupplier)
        Me.gbCriteria.Controls.Add(Me.chkRemark)
        Me.gbCriteria.Controls.Add(Me.chkWarehouse)
        Me.gbCriteria.Controls.Add(Me.txtSupplierId)
        Me.gbCriteria.Controls.Add(Me.chkdate)
        Me.gbCriteria.Controls.Add(Me.chkSend)
        Me.gbCriteria.Controls.Add(Me.txtSupplierDes)
        Me.gbCriteria.Controls.Add(Me.lblToDate)
        Me.gbCriteria.Controls.Add(Me.btnSupplier)
        Me.gbCriteria.Controls.Add(Me.chkCustomer)
        Me.gbCriteria.Controls.Add(Me.dtEndDate)
        Me.gbCriteria.Controls.Add(Me.dtExpire)
        Me.gbCriteria.Controls.Add(Me.dtStartDate)
        Me.gbCriteria.Controls.Add(Me.txtCustomerId)
        Me.gbCriteria.Controls.Add(Me.txtCustomerDes)
        Me.gbCriteria.Controls.Add(Me.btnCustomer)
        Me.gbCriteria.Location = New System.Drawing.Point(12, 12)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(443, 212)
        Me.gbCriteria.TabIndex = 11
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'txtRemark
        '
        Me.txtRemark.Enabled = False
        Me.txtRemark.Location = New System.Drawing.Point(108, 172)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(119, 20)
        Me.txtRemark.TabIndex = 12
        '
        'chkSupplier
        '
        Me.chkSupplier.AutoSize = True
        Me.chkSupplier.Location = New System.Drawing.Point(15, 109)
        Me.chkSupplier.Name = "chkSupplier"
        Me.chkSupplier.Size = New System.Drawing.Size(90, 17)
        Me.chkSupplier.TabIndex = 7
        Me.chkSupplier.Text = "ซัพพลายเออร์"
        Me.chkSupplier.UseVisualStyleBackColor = True
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(15, 174)
        Me.chkRemark.Name = "chkRemark"
        Me.chkRemark.Size = New System.Drawing.Size(71, 17)
        Me.chkRemark.TabIndex = 11
        Me.chkRemark.Text = "หมายเหตุ"
        Me.chkRemark.UseVisualStyleBackColor = True
        '
        'chkWarehouse
        '
        Me.chkWarehouse.AutoSize = True
        Me.chkWarehouse.Location = New System.Drawing.Point(15, 50)
        Me.chkWarehouse.Name = "chkWarehouse"
        Me.chkWarehouse.Size = New System.Drawing.Size(85, 17)
        Me.chkWarehouse.TabIndex = 2
        Me.chkWarehouse.Text = "ศูนย์กระจาย"
        Me.chkWarehouse.UseVisualStyleBackColor = True
        '
        'txtSupplierId
        '
        Me.txtSupplierId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplierId.Location = New System.Drawing.Point(108, 107)
        Me.txtSupplierId.MaxLength = 50
        Me.txtSupplierId.Name = "txtSupplierId"
        Me.txtSupplierId.ReadOnly = True
        Me.txtSupplierId.Size = New System.Drawing.Size(106, 20)
        Me.txtSupplierId.TabIndex = 34
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(15, 78)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(74, 17)
        Me.chkdate.TabIndex = 4
        Me.chkdate.Text = "ตั้งแต่วันที่"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'chkSend
        '
        Me.chkSend.AutoSize = True
        Me.chkSend.Location = New System.Drawing.Point(15, 141)
        Me.chkSend.Name = "chkSend"
        Me.chkSend.Size = New System.Drawing.Size(78, 17)
        Me.chkSend.TabIndex = 9
        Me.chkSend.Text = "วันที่ส่งมอบ"
        Me.chkSend.UseVisualStyleBackColor = True
        '
        'txtSupplierDes
        '
        Me.txtSupplierDes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplierDes.Location = New System.Drawing.Point(242, 107)
        Me.txtSupplierDes.MaxLength = 500
        Me.txtSupplierDes.Name = "txtSupplierDes"
        Me.txtSupplierDes.ReadOnly = True
        Me.txtSupplierDes.Size = New System.Drawing.Size(191, 20)
        Me.txtSupplierDes.TabIndex = 33
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(240, 80)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(19, 13)
        Me.lblToDate.TabIndex = 19
        Me.lblToDate.Text = "ถึง"
        '
        'btnSupplier
        '
        Me.btnSupplier.Enabled = False
        Me.btnSupplier.Location = New System.Drawing.Point(217, 106)
        Me.btnSupplier.Name = "btnSupplier"
        Me.btnSupplier.Size = New System.Drawing.Size(24, 23)
        Me.btnSupplier.TabIndex = 8
        Me.btnSupplier.Text = "..."
        Me.btnSupplier.UseVisualStyleBackColor = True
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(15, 22)
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
        Me.dtEndDate.Location = New System.Drawing.Point(268, 76)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(119, 20)
        Me.dtEndDate.TabIndex = 6
        '
        'dtExpire
        '
        Me.dtExpire.CustomFormat = ""
        Me.dtExpire.Enabled = False
        Me.dtExpire.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtExpire.Location = New System.Drawing.Point(108, 139)
        Me.dtExpire.Name = "dtExpire"
        Me.dtExpire.Size = New System.Drawing.Size(119, 20)
        Me.dtExpire.TabIndex = 10
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = ""
        Me.dtStartDate.Enabled = False
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(108, 76)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(119, 20)
        Me.dtStartDate.TabIndex = 5
        '
        'txtCustomerId
        '
        Me.txtCustomerId.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerId.Location = New System.Drawing.Point(108, 20)
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
        Me.Cancel.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel.Location = New System.Drawing.Point(355, 230)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 38)
        Me.Cancel.TabIndex = 13
        Me.Cancel.Text = "ยกเลิก"
        Me.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OK.Location = New System.Drawing.Point(249, 230)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 12
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'cboDistri
        '
        Me.cboDistri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistri.Enabled = False
        Me.cboDistri.FormattingEnabled = True
        Me.cboDistri.Location = New System.Drawing.Point(106, 48)
        Me.cboDistri.Name = "cboDistri"
        Me.cboDistri.Size = New System.Drawing.Size(153, 21)
        Me.cboDistri.TabIndex = 35
        '
        'frmSreachPO_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 280)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachPO_KSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : PO"
        Me.gbCriteria.ResumeLayout(False)
        Me.gbCriteria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents chkSupplier As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemark As System.Windows.Forms.CheckBox
    Friend WithEvents chkWarehouse As System.Windows.Forms.CheckBox
    Friend WithEvents txtSupplierId As System.Windows.Forms.TextBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkSend As System.Windows.Forms.CheckBox
    Friend WithEvents txtSupplierDes As System.Windows.Forms.TextBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents btnSupplier As System.Windows.Forms.Button
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtExpire As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomerId As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerDes As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents cboDistri As System.Windows.Forms.ComboBox
End Class
