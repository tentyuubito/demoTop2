<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHandlingChargeByDocTypeSetup
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbHeader = New System.Windows.Forms.GroupBox
        Me.lblCurrency = New System.Windows.Forms.Label
        Me.cboCurrency = New System.Windows.Forms.ComboBox
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.grbLoad = New System.Windows.Forms.GroupBox
        Me.dgvLoadFee = New System.Windows.Forms.DataGridView
        Me.btnClearLoad = New System.Windows.Forms.Button
        Me.btnAddLoad = New System.Windows.Forms.Button
        Me.lblTypload = New System.Windows.Forms.Label
        Me.lblCrMinimumrateload = New System.Windows.Forms.Label
        Me.cboTypeLoad = New System.Windows.Forms.ComboBox
        Me.lblMinimumrateLoad = New System.Windows.Forms.Label
        Me.txtMinimumrateLoad = New System.Windows.Forms.TextBox
        Me.lblCrloadFee = New System.Windows.Forms.Label
        Me.lblPerloadFee = New System.Windows.Forms.Label
        Me.cboUnitloadFee = New System.Windows.Forms.ComboBox
        Me.lblRateloadFee = New System.Windows.Forms.Label
        Me.txtRateloadFee = New System.Windows.Forms.TextBox
        Me.grbUnLoadFee = New System.Windows.Forms.GroupBox
        Me.btnClearUnload = New System.Windows.Forms.Button
        Me.btnAddUnloadFee = New System.Windows.Forms.Button
        Me.dgvUnLoadFee = New System.Windows.Forms.DataGridView
        Me.lblTypUnload = New System.Windows.Forms.Label
        Me.cboTypeUnLoad = New System.Windows.Forms.ComboBox
        Me.lblCrMinimumrateUnload = New System.Windows.Forms.Label
        Me.lblMinimumrateUnload = New System.Windows.Forms.Label
        Me.txtMinimumrateUnload = New System.Windows.Forms.TextBox
        Me.lblCrUnLoadFee = New System.Windows.Forms.Label
        Me.lblPerUnloadFee = New System.Windows.Forms.Label
        Me.cboUnitUnloadFee = New System.Windows.Forms.ComboBox
        Me.lblRateUnLoadFee = New System.Windows.Forms.Label
        Me.txtRateUnLoadFee = New System.Windows.Forms.TextBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColIndexLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTypeLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRateLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColminimumrateLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColUnitLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColIndexUnLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTypeUnload = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRateUnload = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMinimumrateUnload = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColUnitUnload = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbHeader.SuspendLayout()
        Me.grbLoad.SuspendLayout()
        CType(Me.dgvLoadFee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbUnLoadFee.SuspendLayout()
        CType(Me.dgvUnLoadFee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbHeader
        '
        Me.grbHeader.Controls.Add(Me.lblCurrency)
        Me.grbHeader.Controls.Add(Me.cboCurrency)
        Me.grbHeader.Controls.Add(Me.txtCustomer_Id)
        Me.grbHeader.Controls.Add(Me.txtCustomer_Name)
        Me.grbHeader.Controls.Add(Me.lblDocumentType)
        Me.grbHeader.Controls.Add(Me.lblCustomer)
        Me.grbHeader.Controls.Add(Me.cboDocumentType)
        Me.grbHeader.Location = New System.Drawing.Point(12, 12)
        Me.grbHeader.Name = "grbHeader"
        Me.grbHeader.Size = New System.Drawing.Size(733, 82)
        Me.grbHeader.TabIndex = 12
        Me.grbHeader.TabStop = False
        Me.grbHeader.Text = "ข้อกำหนดการคิดเงิน"
        '
        'lblCurrency
        '
        Me.lblCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCurrency.Location = New System.Drawing.Point(358, 44)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 23)
        Me.lblCurrency.TabIndex = 15
        Me.lblCurrency.Text = "สกุลเงิน"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCurrency
        '
        Me.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCurrency.FormattingEnabled = True
        Me.cboCurrency.Location = New System.Drawing.Point(416, 45)
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.Size = New System.Drawing.Size(114, 21)
        Me.cboCurrency.TabIndex = 16
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Id.Location = New System.Drawing.Point(153, 19)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(186, 20)
        Me.txtCustomer_Id.TabIndex = 1
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Name.Location = New System.Drawing.Point(345, 20)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(265, 20)
        Me.txtCustomer_Name.TabIndex = 3
        '
        'lblDocumentType
        '
        Me.lblDocumentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDocumentType.Location = New System.Drawing.Point(24, 44)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(118, 23)
        Me.lblDocumentType.TabIndex = 4
        Me.lblDocumentType.Text = "ประเภทงานขนส่ง"
        Me.lblDocumentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(38, 18)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(105, 23)
        Me.lblCustomer.TabIndex = 0
        Me.lblCustomer.Text = "ลูกค้า/เจ้าของงาน"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.Enabled = False
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(153, 45)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(187, 21)
        Me.cboDocumentType.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(18, 559)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(627, 559)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 16
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grbLoad
        '
        Me.grbLoad.Controls.Add(Me.dgvLoadFee)
        Me.grbLoad.Controls.Add(Me.btnClearLoad)
        Me.grbLoad.Controls.Add(Me.btnAddLoad)
        Me.grbLoad.Controls.Add(Me.lblTypload)
        Me.grbLoad.Controls.Add(Me.lblCrMinimumrateload)
        Me.grbLoad.Controls.Add(Me.cboTypeLoad)
        Me.grbLoad.Controls.Add(Me.lblMinimumrateLoad)
        Me.grbLoad.Controls.Add(Me.txtMinimumrateLoad)
        Me.grbLoad.Controls.Add(Me.lblCrloadFee)
        Me.grbLoad.Controls.Add(Me.lblPerloadFee)
        Me.grbLoad.Controls.Add(Me.cboUnitloadFee)
        Me.grbLoad.Controls.Add(Me.lblRateloadFee)
        Me.grbLoad.Controls.Add(Me.txtRateloadFee)
        Me.grbLoad.Location = New System.Drawing.Point(12, 337)
        Me.grbLoad.Name = "grbLoad"
        Me.grbLoad.Size = New System.Drawing.Size(733, 216)
        Me.grbLoad.TabIndex = 18
        Me.grbLoad.TabStop = False
        Me.grbLoad.Text = "ค่าจัดสินค้าขึ้นรถ-จ่ายออก (Handling Out)"
        '
        'dgvLoadFee
        '
        Me.dgvLoadFee.AllowUserToAddRows = False
        Me.dgvLoadFee.AllowUserToDeleteRows = False
        Me.dgvLoadFee.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvLoadFee.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLoadFee.BackgroundColor = System.Drawing.Color.White
        Me.dgvLoadFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLoadFee.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColIndexLoad, Me.ColTypeLoad, Me.ColRateLoad, Me.ColminimumrateLoad, Me.ColUnitLoad})
        Me.dgvLoadFee.Location = New System.Drawing.Point(6, 77)
        Me.dgvLoadFee.Name = "dgvLoadFee"
        Me.dgvLoadFee.ReadOnly = True
        Me.dgvLoadFee.RowHeadersVisible = False
        Me.dgvLoadFee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLoadFee.Size = New System.Drawing.Size(721, 133)
        Me.dgvLoadFee.TabIndex = 12
        '
        'btnClearLoad
        '
        Me.btnClearLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearLoad.Location = New System.Drawing.Point(639, 48)
        Me.btnClearLoad.Name = "btnClearLoad"
        Me.btnClearLoad.Size = New System.Drawing.Size(87, 25)
        Me.btnClearLoad.TabIndex = 11
        Me.btnClearLoad.Text = "ล้างหน้าจอ"
        Me.btnClearLoad.UseVisualStyleBackColor = True
        '
        'btnAddLoad
        '
        Me.btnAddLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddLoad.Location = New System.Drawing.Point(546, 48)
        Me.btnAddLoad.Name = "btnAddLoad"
        Me.btnAddLoad.Size = New System.Drawing.Size(87, 25)
        Me.btnAddLoad.TabIndex = 10
        Me.btnAddLoad.Text = "เพิ่ม"
        Me.btnAddLoad.UseVisualStyleBackColor = True
        '
        'lblTypload
        '
        Me.lblTypload.AutoSize = True
        Me.lblTypload.Location = New System.Drawing.Point(39, 21)
        Me.lblTypload.Name = "lblTypload"
        Me.lblTypload.Size = New System.Drawing.Size(44, 13)
        Me.lblTypload.TabIndex = 0
        Me.lblTypload.Text = "ประเภท"
        '
        'lblCrMinimumrateload
        '
        Me.lblCrMinimumrateload.AutoSize = True
        Me.lblCrMinimumrateload.Location = New System.Drawing.Point(346, 48)
        Me.lblCrMinimumrateload.Name = "lblCrMinimumrateload"
        Me.lblCrMinimumrateload.Size = New System.Drawing.Size(26, 13)
        Me.lblCrMinimumrateload.TabIndex = 9
        Me.lblCrMinimumrateload.Text = "บาท"
        '
        'cboTypeLoad
        '
        Me.cboTypeLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTypeLoad.FormattingEnabled = True
        Me.cboTypeLoad.Items.AddRange(New Object() {"On-Pallet", "Non-Pallet"})
        Me.cboTypeLoad.Location = New System.Drawing.Point(89, 18)
        Me.cboTypeLoad.Name = "cboTypeLoad"
        Me.cboTypeLoad.Size = New System.Drawing.Size(99, 21)
        Me.cboTypeLoad.TabIndex = 1
        '
        'lblMinimumrateLoad
        '
        Me.lblMinimumrateLoad.AutoSize = True
        Me.lblMinimumrateLoad.Location = New System.Drawing.Point(170, 48)
        Me.lblMinimumrateLoad.Name = "lblMinimumrateLoad"
        Me.lblMinimumrateLoad.Size = New System.Drawing.Size(76, 13)
        Me.lblMinimumrateLoad.TabIndex = 7
        Me.lblMinimumrateLoad.Text = "ค่าบริการขั้นต่ำ"
        '
        'txtMinimumrateLoad
        '
        Me.txtMinimumrateLoad.Location = New System.Drawing.Point(252, 45)
        Me.txtMinimumrateLoad.Name = "txtMinimumrateLoad"
        Me.txtMinimumrateLoad.Size = New System.Drawing.Size(88, 20)
        Me.txtMinimumrateLoad.TabIndex = 8
        Me.txtMinimumrateLoad.Text = "0"
        '
        'lblCrloadFee
        '
        Me.lblCrloadFee.AutoSize = True
        Me.lblCrloadFee.Location = New System.Drawing.Point(346, 23)
        Me.lblCrloadFee.Name = "lblCrloadFee"
        Me.lblCrloadFee.Size = New System.Drawing.Size(26, 13)
        Me.lblCrloadFee.TabIndex = 4
        Me.lblCrloadFee.Text = "บาท"
        '
        'lblPerloadFee
        '
        Me.lblPerloadFee.AutoSize = True
        Me.lblPerloadFee.Location = New System.Drawing.Point(379, 23)
        Me.lblPerloadFee.Name = "lblPerloadFee"
        Me.lblPerloadFee.Size = New System.Drawing.Size(20, 13)
        Me.lblPerloadFee.TabIndex = 5
        Me.lblPerloadFee.Text = "ต่อ"
        '
        'cboUnitloadFee
        '
        Me.cboUnitloadFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnitloadFee.FormattingEnabled = True
        Me.cboUnitloadFee.Location = New System.Drawing.Point(405, 18)
        Me.cboUnitloadFee.Name = "cboUnitloadFee"
        Me.cboUnitloadFee.Size = New System.Drawing.Size(99, 21)
        Me.cboUnitloadFee.TabIndex = 6
        '
        'lblRateloadFee
        '
        Me.lblRateloadFee.AutoSize = True
        Me.lblRateloadFee.Location = New System.Drawing.Point(196, 23)
        Me.lblRateloadFee.Name = "lblRateloadFee"
        Me.lblRateloadFee.Size = New System.Drawing.Size(50, 13)
        Me.lblRateloadFee.TabIndex = 2
        Me.lblRateloadFee.Text = "ค่าบริการ"
        '
        'txtRateloadFee
        '
        Me.txtRateloadFee.Location = New System.Drawing.Point(252, 18)
        Me.txtRateloadFee.Name = "txtRateloadFee"
        Me.txtRateloadFee.Size = New System.Drawing.Size(88, 20)
        Me.txtRateloadFee.TabIndex = 3
        Me.txtRateloadFee.Text = "0"
        '
        'grbUnLoadFee
        '
        Me.grbUnLoadFee.Controls.Add(Me.btnClearUnload)
        Me.grbUnLoadFee.Controls.Add(Me.btnAddUnloadFee)
        Me.grbUnLoadFee.Controls.Add(Me.dgvUnLoadFee)
        Me.grbUnLoadFee.Controls.Add(Me.lblTypUnload)
        Me.grbUnLoadFee.Controls.Add(Me.cboTypeUnLoad)
        Me.grbUnLoadFee.Controls.Add(Me.lblCrMinimumrateUnload)
        Me.grbUnLoadFee.Controls.Add(Me.lblMinimumrateUnload)
        Me.grbUnLoadFee.Controls.Add(Me.txtMinimumrateUnload)
        Me.grbUnLoadFee.Controls.Add(Me.lblCrUnLoadFee)
        Me.grbUnLoadFee.Controls.Add(Me.lblPerUnloadFee)
        Me.grbUnLoadFee.Controls.Add(Me.cboUnitUnloadFee)
        Me.grbUnLoadFee.Controls.Add(Me.lblRateUnLoadFee)
        Me.grbUnLoadFee.Controls.Add(Me.txtRateUnLoadFee)
        Me.grbUnLoadFee.Location = New System.Drawing.Point(12, 100)
        Me.grbUnLoadFee.Name = "grbUnLoadFee"
        Me.grbUnLoadFee.Size = New System.Drawing.Size(733, 231)
        Me.grbUnLoadFee.TabIndex = 17
        Me.grbUnLoadFee.TabStop = False
        Me.grbUnLoadFee.Text = "ค่าขนสินค้าลงรถ-เข้าคลัง (Handling In)"
        '
        'btnClearUnload
        '
        Me.btnClearUnload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearUnload.Location = New System.Drawing.Point(639, 46)
        Me.btnClearUnload.Name = "btnClearUnload"
        Me.btnClearUnload.Size = New System.Drawing.Size(87, 25)
        Me.btnClearUnload.TabIndex = 11
        Me.btnClearUnload.Text = "ล้างหน้าจอ"
        Me.btnClearUnload.UseVisualStyleBackColor = True
        '
        'btnAddUnloadFee
        '
        Me.btnAddUnloadFee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddUnloadFee.Location = New System.Drawing.Point(546, 46)
        Me.btnAddUnloadFee.Name = "btnAddUnloadFee"
        Me.btnAddUnloadFee.Size = New System.Drawing.Size(87, 25)
        Me.btnAddUnloadFee.TabIndex = 10
        Me.btnAddUnloadFee.Text = "เพิ่ม"
        Me.btnAddUnloadFee.UseVisualStyleBackColor = True
        '
        'dgvUnLoadFee
        '
        Me.dgvUnLoadFee.AllowUserToAddRows = False
        Me.dgvUnLoadFee.AllowUserToDeleteRows = False
        Me.dgvUnLoadFee.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvUnLoadFee.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvUnLoadFee.BackgroundColor = System.Drawing.Color.White
        Me.dgvUnLoadFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUnLoadFee.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColIndexUnLoad, Me.ColTypeUnload, Me.ColRateUnload, Me.ColMinimumrateUnload, Me.ColUnitUnload})
        Me.dgvUnLoadFee.Location = New System.Drawing.Point(6, 77)
        Me.dgvUnLoadFee.Name = "dgvUnLoadFee"
        Me.dgvUnLoadFee.ReadOnly = True
        Me.dgvUnLoadFee.RowHeadersVisible = False
        Me.dgvUnLoadFee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUnLoadFee.Size = New System.Drawing.Size(721, 148)
        Me.dgvUnLoadFee.TabIndex = 12
        '
        'lblTypUnload
        '
        Me.lblTypUnload.AutoSize = True
        Me.lblTypUnload.Location = New System.Drawing.Point(39, 22)
        Me.lblTypUnload.Name = "lblTypUnload"
        Me.lblTypUnload.Size = New System.Drawing.Size(44, 13)
        Me.lblTypUnload.TabIndex = 0
        Me.lblTypUnload.Text = "ประเภท"
        '
        'cboTypeUnLoad
        '
        Me.cboTypeUnLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTypeUnLoad.FormattingEnabled = True
        Me.cboTypeUnLoad.Items.AddRange(New Object() {"On-Pallet", "Non-Pallet"})
        Me.cboTypeUnLoad.Location = New System.Drawing.Point(89, 19)
        Me.cboTypeUnLoad.Name = "cboTypeUnLoad"
        Me.cboTypeUnLoad.Size = New System.Drawing.Size(99, 21)
        Me.cboTypeUnLoad.TabIndex = 1
        '
        'lblCrMinimumrateUnload
        '
        Me.lblCrMinimumrateUnload.AutoSize = True
        Me.lblCrMinimumrateUnload.Location = New System.Drawing.Point(346, 48)
        Me.lblCrMinimumrateUnload.Name = "lblCrMinimumrateUnload"
        Me.lblCrMinimumrateUnload.Size = New System.Drawing.Size(26, 13)
        Me.lblCrMinimumrateUnload.TabIndex = 9
        Me.lblCrMinimumrateUnload.Text = "บาท"
        '
        'lblMinimumrateUnload
        '
        Me.lblMinimumrateUnload.AutoSize = True
        Me.lblMinimumrateUnload.Location = New System.Drawing.Point(176, 48)
        Me.lblMinimumrateUnload.Name = "lblMinimumrateUnload"
        Me.lblMinimumrateUnload.Size = New System.Drawing.Size(76, 13)
        Me.lblMinimumrateUnload.TabIndex = 7
        Me.lblMinimumrateUnload.Text = "ค่าบริการขั้นต่ำ"
        '
        'txtMinimumrateUnload
        '
        Me.txtMinimumrateUnload.Location = New System.Drawing.Point(252, 45)
        Me.txtMinimumrateUnload.Name = "txtMinimumrateUnload"
        Me.txtMinimumrateUnload.Size = New System.Drawing.Size(88, 20)
        Me.txtMinimumrateUnload.TabIndex = 8
        Me.txtMinimumrateUnload.Text = "0"
        '
        'lblCrUnLoadFee
        '
        Me.lblCrUnLoadFee.AutoSize = True
        Me.lblCrUnLoadFee.Location = New System.Drawing.Point(346, 22)
        Me.lblCrUnLoadFee.Name = "lblCrUnLoadFee"
        Me.lblCrUnLoadFee.Size = New System.Drawing.Size(26, 13)
        Me.lblCrUnLoadFee.TabIndex = 4
        Me.lblCrUnLoadFee.Text = "บาท"
        '
        'lblPerUnloadFee
        '
        Me.lblPerUnloadFee.AutoSize = True
        Me.lblPerUnloadFee.Location = New System.Drawing.Point(379, 22)
        Me.lblPerUnloadFee.Name = "lblPerUnloadFee"
        Me.lblPerUnloadFee.Size = New System.Drawing.Size(20, 13)
        Me.lblPerUnloadFee.TabIndex = 5
        Me.lblPerUnloadFee.Text = "ต่อ"
        '
        'cboUnitUnloadFee
        '
        Me.cboUnitUnloadFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnitUnloadFee.FormattingEnabled = True
        Me.cboUnitUnloadFee.Location = New System.Drawing.Point(405, 18)
        Me.cboUnitUnloadFee.Name = "cboUnitUnloadFee"
        Me.cboUnitUnloadFee.Size = New System.Drawing.Size(99, 21)
        Me.cboUnitUnloadFee.TabIndex = 6
        '
        'lblRateUnLoadFee
        '
        Me.lblRateUnLoadFee.AutoSize = True
        Me.lblRateUnLoadFee.Location = New System.Drawing.Point(197, 22)
        Me.lblRateUnLoadFee.Name = "lblRateUnLoadFee"
        Me.lblRateUnLoadFee.Size = New System.Drawing.Size(50, 13)
        Me.lblRateUnLoadFee.TabIndex = 2
        Me.lblRateUnLoadFee.Text = "ค่าบริการ"
        '
        'txtRateUnLoadFee
        '
        Me.txtRateUnLoadFee.Location = New System.Drawing.Point(252, 19)
        Me.txtRateUnLoadFee.Name = "txtRateUnLoadFee"
        Me.txtRateUnLoadFee.Size = New System.Drawing.Size(88, 20)
        Me.txtRateUnLoadFee.TabIndex = 3
        Me.txtRateUnLoadFee.Text = "0"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ชื่อลูกค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "TransportJobType_desc"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ประเภทงานขนส่ง"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TransportRegion_desc"
        Me.DataGridViewTextBoxColumn3.HeaderText = "เขตพื้นที่บริการ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Currency_desc"
        Me.DataGridViewTextBoxColumn4.HeaderText = "สกุลเงิน"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 122
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn5.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TransportJobType_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "TransportRegion_Index"
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวน Drop (เริ่ม)"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Customer_Index"
        Me.DataGridViewTextBoxColumn8.HeaderText = "จำนวน Drop (ถึง)"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn9.HeaderText = "อัตรา Drop ละ"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 130
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Currency_Index"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ปริมาณเกิน"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'ColIndexLoad
        '
        Me.ColIndexLoad.HeaderText = "ColIndex"
        Me.ColIndexLoad.Name = "ColIndexLoad"
        Me.ColIndexLoad.ReadOnly = True
        Me.ColIndexLoad.Visible = False
        '
        'ColTypeLoad
        '
        Me.ColTypeLoad.HeaderText = "ประเภท"
        Me.ColTypeLoad.Name = "ColTypeLoad"
        Me.ColTypeLoad.ReadOnly = True
        Me.ColTypeLoad.Width = 200
        '
        'ColRateLoad
        '
        Me.ColRateLoad.HeaderText = "ค่าบริการ"
        Me.ColRateLoad.Name = "ColRateLoad"
        Me.ColRateLoad.ReadOnly = True
        Me.ColRateLoad.Width = 200
        '
        'ColminimumrateLoad
        '
        Me.ColminimumrateLoad.HeaderText = "ค่าบริการขั้นต่ำ"
        Me.ColminimumrateLoad.Name = "ColminimumrateLoad"
        Me.ColminimumrateLoad.ReadOnly = True
        '
        'ColUnitLoad
        '
        Me.ColUnitLoad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColUnitLoad.HeaderText = "หน่วยคิดค่าบริการ"
        Me.ColUnitLoad.Name = "ColUnitLoad"
        Me.ColUnitLoad.ReadOnly = True
        '
        'ColIndexUnLoad
        '
        Me.ColIndexUnLoad.HeaderText = "ColIndex"
        Me.ColIndexUnLoad.Name = "ColIndexUnLoad"
        Me.ColIndexUnLoad.ReadOnly = True
        Me.ColIndexUnLoad.Visible = False
        '
        'ColTypeUnload
        '
        Me.ColTypeUnload.HeaderText = "ประเภท"
        Me.ColTypeUnload.Name = "ColTypeUnload"
        Me.ColTypeUnload.ReadOnly = True
        Me.ColTypeUnload.Width = 200
        '
        'ColRateUnload
        '
        Me.ColRateUnload.HeaderText = "ค่าบริการ"
        Me.ColRateUnload.Name = "ColRateUnload"
        Me.ColRateUnload.ReadOnly = True
        Me.ColRateUnload.Width = 200
        '
        'ColMinimumrateUnload
        '
        Me.ColMinimumrateUnload.HeaderText = "ค่าบริการขั้นต่ำ"
        Me.ColMinimumrateUnload.Name = "ColMinimumrateUnload"
        Me.ColMinimumrateUnload.ReadOnly = True
        '
        'ColUnitUnload
        '
        Me.ColUnitUnload.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColUnitUnload.HeaderText = "หน่วยคิดค่าบริการ"
        Me.ColUnitUnload.Name = "ColUnitUnload"
        Me.ColUnitUnload.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn11.HeaderText = "หน่วยส่วนเกิน"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 120
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "QtyDropStart"
        Me.DataGridViewTextBoxColumn12.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "QtyDropEnd"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "RateTransportPerDrop"
        Me.DataGridViewTextBoxColumn14.HeaderText = "หน่วยส่วนเกิน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn14.Width = 130
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "OverflowPerDrop"
        Me.DataGridViewTextBoxColumn15.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "OverflowPerDropUnit_desc"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "OverflowPerDropUnit"
        Me.DataGridViewTextBoxColumn17.HeaderText = "OverflowPerDropUnit"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "OverflowRate"
        Me.DataGridViewTextBoxColumn18.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        '
        'frmHandlingChargeByDocTypeSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 611)
        Me.Controls.Add(Me.grbLoad)
        Me.Controls.Add(Me.grbUnLoadFee)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grbHeader)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHandlingChargeByDocTypeSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดค่า Handling"
        Me.grbHeader.ResumeLayout(False)
        Me.grbHeader.PerformLayout()
        Me.grbLoad.ResumeLayout(False)
        Me.grbLoad.PerformLayout()
        CType(Me.dgvLoadFee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbUnLoadFee.ResumeLayout(False)
        Me.grbUnLoadFee.PerformLayout()
        CType(Me.dgvUnLoadFee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbHeader As System.Windows.Forms.GroupBox
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents cboCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grbLoad As System.Windows.Forms.GroupBox
    Friend WithEvents dgvLoadFee As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearLoad As System.Windows.Forms.Button
    Friend WithEvents btnAddLoad As System.Windows.Forms.Button
    Friend WithEvents lblTypload As System.Windows.Forms.Label
    Friend WithEvents lblCrMinimumrateload As System.Windows.Forms.Label
    Friend WithEvents cboTypeLoad As System.Windows.Forms.ComboBox
    Friend WithEvents lblMinimumrateLoad As System.Windows.Forms.Label
    Friend WithEvents txtMinimumrateLoad As System.Windows.Forms.TextBox
    Friend WithEvents lblCrloadFee As System.Windows.Forms.Label
    Friend WithEvents lblPerloadFee As System.Windows.Forms.Label
    Friend WithEvents cboUnitloadFee As System.Windows.Forms.ComboBox
    Friend WithEvents lblRateloadFee As System.Windows.Forms.Label
    Friend WithEvents txtRateloadFee As System.Windows.Forms.TextBox
    Friend WithEvents grbUnLoadFee As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearUnload As System.Windows.Forms.Button
    Friend WithEvents btnAddUnloadFee As System.Windows.Forms.Button
    Friend WithEvents dgvUnLoadFee As System.Windows.Forms.DataGridView
    Friend WithEvents lblTypUnload As System.Windows.Forms.Label
    Friend WithEvents lblCrMinimumrateUnload As System.Windows.Forms.Label
    Friend WithEvents lblMinimumrateUnload As System.Windows.Forms.Label
    Friend WithEvents txtMinimumrateUnload As System.Windows.Forms.TextBox
    Friend WithEvents lblCrUnLoadFee As System.Windows.Forms.Label
    Friend WithEvents lblPerUnloadFee As System.Windows.Forms.Label
    Friend WithEvents cboUnitUnloadFee As System.Windows.Forms.ComboBox
    Friend WithEvents lblRateUnLoadFee As System.Windows.Forms.Label
    Friend WithEvents txtRateUnLoadFee As System.Windows.Forms.TextBox
    Friend WithEvents ColIndexLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTypeLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRateLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColminimumrateLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnitLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColIndexUnLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTypeUnload As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRateUnload As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMinimumrateUnload As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnitUnload As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cboTypeUnLoad As System.Windows.Forms.ComboBox
End Class
