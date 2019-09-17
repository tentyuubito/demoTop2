<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachPicking_KSL
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
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.chkStatus = New System.Windows.Forms.CheckBox
        Me.cboDocType = New System.Windows.Forms.ComboBox
        Me.chkWarehouse = New System.Windows.Forms.CheckBox
        Me.txtWareHouserId = New System.Windows.Forms.TextBox
        Me.txtWareHouserDes = New System.Windows.Forms.TextBox
        Me.btnWareHouser = New System.Windows.Forms.Button
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
        Me.gbCriteria.Controls.Add(Me.cboStatus)
        Me.gbCriteria.Controls.Add(Me.chkStatus)
        Me.gbCriteria.Controls.Add(Me.cboDocType)
        Me.gbCriteria.Controls.Add(Me.chkWarehouse)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserId)
        Me.gbCriteria.Controls.Add(Me.txtWareHouserDes)
        Me.gbCriteria.Controls.Add(Me.btnWareHouser)
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
        Me.gbCriteria.Size = New System.Drawing.Size(443, 222)
        Me.gbCriteria.TabIndex = 11
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"ไม่พอจ่าย", "พอจ่าย"})
        Me.cboStatus.Location = New System.Drawing.Point(109, 134)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(121, 21)
        Me.cboStatus.TabIndex = 41
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Location = New System.Drawing.Point(16, 136)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(57, 17)
        Me.chkStatus.TabIndex = 40
        Me.chkStatus.Text = "สถานะ"
        Me.chkStatus.UseVisualStyleBackColor = True
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
        Me.txtRemark.Location = New System.Drawing.Point(109, 169)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(121, 20)
        Me.txtRemark.TabIndex = 12
        '
        'chkDocType
        '
        Me.chkDocType.AutoSize = True
        Me.chkDocType.Location = New System.Drawing.Point(16, 107)
        Me.chkDocType.Name = "chkDocType"
        Me.chkDocType.Size = New System.Drawing.Size(63, 17)
        Me.chkDocType.TabIndex = 7
        Me.chkDocType.Text = "ประเภท"
        Me.chkDocType.UseVisualStyleBackColor = True
        '
        'chkRemark
        '
        Me.chkRemark.AutoSize = True
        Me.chkRemark.Location = New System.Drawing.Point(16, 171)
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
        Me.Cancel.Location = New System.Drawing.Point(355, 240)
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
        Me.OK.Location = New System.Drawing.Point(249, 240)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 12
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'frmSreachPicking_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 289)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachPicking_KSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : Picking"
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
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents chkDocType As System.Windows.Forms.CheckBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
End Class
