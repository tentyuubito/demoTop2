<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoice_Print
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoice_Print))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dtStart = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtEnd = New System.Windows.Forms.DateTimePicker
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.btnSearchCustomer = New System.Windows.Forms.Button
        Me.txtSalesOrderNo = New System.Windows.Forms.TextBox
        Me.btnConsignee = New System.Windows.Forms.Button
        Me.txtConsignee_Name = New System.Windows.Forms.TextBox
        Me.txtConsignee_Id = New System.Windows.Forms.TextBox
        Me.txtInvoice = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkExpWinNo = New System.Windows.Forms.CheckBox
        Me.chkExpWin = New System.Windows.Forms.CheckBox
        Me.chkInvoiceNull = New System.Windows.Forms.CheckBox
        Me.cbkSalerId = New System.Windows.Forms.CheckBox
        Me.txtSalerId = New System.Windows.Forms.TextBox
        Me.cbkSalesArea = New System.Windows.Forms.CheckBox
        Me.txtSalesArea = New System.Windows.Forms.TextBox
        Me.chkNotRed = New System.Windows.Forms.CheckBox
        Me.chkInvcoice_Date = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtInvcoice_Date_B = New System.Windows.Forms.DateTimePicker
        Me.dtInvcoice_Date_E = New System.Windows.Forms.DateTimePicker
        Me.chkDistributionCenter = New System.Windows.Forms.CheckBox
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.chkInvoiceNotNull = New System.Windows.Forms.CheckBox
        Me.chkInvoiceNo = New System.Windows.Forms.CheckBox
        Me.chkConsignee = New System.Windows.Forms.CheckBox
        Me.chkSalesOrderNo = New System.Windows.Forms.CheckBox
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.chkSalesOrderDate = New System.Windows.Forms.CheckBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.grdSOView = New System.Windows.Forms.DataGridView
        Me.chkselect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_WinspeedExport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_CustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Invoice_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Invoice_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Consignee = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Expected_Delivery_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_CustomerShippingLocation = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_comment = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Credit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Amount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Amount_Vat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Shipping_Location_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Status_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_Manifest_Des = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Carrier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DayDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SumQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SumWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SumVolume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Addby = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Confirm_By = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Confirm_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DocumentType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_RGB = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesUnit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Customer_Shipping_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Customer_Shipping_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btn_Print = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnGenInvoice = New System.Windows.Forms.Button
        Me.btnExportExcel = New System.Windows.Forms.Button
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.btnPrintPickingList = New System.Windows.Forms.Button
        Me.btnExport = New System.Windows.Forms.Button
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
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
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnExportText = New System.Windows.Forms.Button
        Me.btnCancelInvoice = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdSOView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtStart
        '
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStart.Location = New System.Drawing.Point(113, 23)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(106, 20)
        Me.dtStart.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(222, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "ถึง"
        '
        'dtEnd
        '
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEnd.Location = New System.Drawing.Point(247, 23)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(106, 20)
        Me.dtEnd.TabIndex = 3
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(112, 100)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomer_Id.TabIndex = 5
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Name.Location = New System.Drawing.Point(246, 100)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(223, 20)
        Me.txtCustomer_Name.TabIndex = 7
        '
        'btnSearchCustomer
        '
        Me.btnSearchCustomer.Enabled = False
        Me.btnSearchCustomer.Location = New System.Drawing.Point(220, 99)
        Me.btnSearchCustomer.Name = "btnSearchCustomer"
        Me.btnSearchCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnSearchCustomer.TabIndex = 6
        Me.btnSearchCustomer.Text = "..."
        Me.btnSearchCustomer.UseVisualStyleBackColor = True
        '
        'txtSalesOrderNo
        '
        Me.txtSalesOrderNo.Enabled = False
        Me.txtSalesOrderNo.Location = New System.Drawing.Point(112, 152)
        Me.txtSalesOrderNo.Name = "txtSalesOrderNo"
        Me.txtSalesOrderNo.Size = New System.Drawing.Size(172, 20)
        Me.txtSalesOrderNo.TabIndex = 9
        '
        'btnConsignee
        '
        Me.btnConsignee.Enabled = False
        Me.btnConsignee.Location = New System.Drawing.Point(220, 125)
        Me.btnConsignee.Name = "btnConsignee"
        Me.btnConsignee.Size = New System.Drawing.Size(24, 23)
        Me.btnConsignee.TabIndex = 11
        Me.btnConsignee.Text = "..."
        Me.btnConsignee.UseVisualStyleBackColor = True
        '
        'txtConsignee_Name
        '
        Me.txtConsignee_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignee_Name.Location = New System.Drawing.Point(246, 126)
        Me.txtConsignee_Name.Name = "txtConsignee_Name"
        Me.txtConsignee_Name.ReadOnly = True
        Me.txtConsignee_Name.Size = New System.Drawing.Size(223, 20)
        Me.txtConsignee_Name.TabIndex = 16
        '
        'txtConsignee_Id
        '
        Me.txtConsignee_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignee_Id.Location = New System.Drawing.Point(112, 126)
        Me.txtConsignee_Id.Name = "txtConsignee_Id"
        Me.txtConsignee_Id.ReadOnly = True
        Me.txtConsignee_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtConsignee_Id.TabIndex = 14
        '
        'txtInvoice
        '
        Me.txtInvoice.Enabled = False
        Me.txtInvoice.Location = New System.Drawing.Point(434, 152)
        Me.txtInvoice.Name = "txtInvoice"
        Me.txtInvoice.Size = New System.Drawing.Size(172, 20)
        Me.txtInvoice.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkExpWinNo)
        Me.GroupBox1.Controls.Add(Me.chkExpWin)
        Me.GroupBox1.Controls.Add(Me.chkInvoiceNull)
        Me.GroupBox1.Controls.Add(Me.cbkSalerId)
        Me.GroupBox1.Controls.Add(Me.txtSalerId)
        Me.GroupBox1.Controls.Add(Me.cbkSalesArea)
        Me.GroupBox1.Controls.Add(Me.txtSalesArea)
        Me.GroupBox1.Controls.Add(Me.chkNotRed)
        Me.GroupBox1.Controls.Add(Me.chkInvcoice_Date)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtInvcoice_Date_B)
        Me.GroupBox1.Controls.Add(Me.dtInvcoice_Date_E)
        Me.GroupBox1.Controls.Add(Me.chkDistributionCenter)
        Me.GroupBox1.Controls.Add(Me.cboDistributionCenter)
        Me.GroupBox1.Controls.Add(Me.chkInvoiceNotNull)
        Me.GroupBox1.Controls.Add(Me.chkInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.chkConsignee)
        Me.GroupBox1.Controls.Add(Me.chkSalesOrderNo)
        Me.GroupBox1.Controls.Add(Me.chkCustomer)
        Me.GroupBox1.Controls.Add(Me.chkSalesOrderDate)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.btnConsignee)
        Me.GroupBox1.Controls.Add(Me.txtCustomer_Name)
        Me.GroupBox1.Controls.Add(Me.txtConsignee_Name)
        Me.GroupBox1.Controls.Add(Me.txtConsignee_Id)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtInvoice)
        Me.GroupBox1.Controls.Add(Me.txtSalesOrderNo)
        Me.GroupBox1.Controls.Add(Me.txtCustomer_Id)
        Me.GroupBox1.Controls.Add(Me.btnSearchCustomer)
        Me.GroupBox1.Controls.Add(Me.dtStart)
        Me.GroupBox1.Controls.Add(Me.dtEnd)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(777, 211)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เงื่อนไข"
        '
        'chkExpWinNo
        '
        Me.chkExpWinNo.AutoSize = True
        Me.chkExpWinNo.Location = New System.Drawing.Point(628, 126)
        Me.chkExpWinNo.Name = "chkExpWinNo"
        Me.chkExpWinNo.Size = New System.Drawing.Size(132, 17)
        Me.chkExpWinNo.TabIndex = 34
        Me.chkExpWinNo.Text = "ยังไม่ Export winspeed"
        Me.chkExpWinNo.UseVisualStyleBackColor = True
        '
        'chkExpWin
        '
        Me.chkExpWin.AutoSize = True
        Me.chkExpWin.Location = New System.Drawing.Point(496, 125)
        Me.chkExpWin.Name = "chkExpWin"
        Me.chkExpWin.Size = New System.Drawing.Size(127, 17)
        Me.chkExpWin.TabIndex = 33
        Me.chkExpWin.Text = "Export winspeed แล้ว"
        Me.chkExpWin.UseVisualStyleBackColor = True
        '
        'chkInvoiceNull
        '
        Me.chkInvoiceNull.AutoSize = True
        Me.chkInvoiceNull.Location = New System.Drawing.Point(496, 77)
        Me.chkInvoiceNull.Name = "chkInvoiceNull"
        Me.chkInvoiceNull.Size = New System.Drawing.Size(162, 17)
        Me.chkInvoiceNull.TabIndex = 32
        Me.chkInvoiceNull.Text = "แสดงเฉพาะ SO ไม่มี Invoice"
        Me.chkInvoiceNull.UseVisualStyleBackColor = True
        '
        'cbkSalerId
        '
        Me.cbkSalerId.AutoSize = True
        Me.cbkSalerId.Location = New System.Drawing.Point(552, 177)
        Me.cbkSalerId.Name = "cbkSalerId"
        Me.cbkSalerId.Size = New System.Drawing.Size(70, 17)
        Me.cbkSalerId.TabIndex = 30
        Me.cbkSalerId.Text = "รหัสผู้ขาย"
        Me.cbkSalerId.UseVisualStyleBackColor = True
        '
        'txtSalerId
        '
        Me.txtSalerId.Enabled = False
        Me.txtSalerId.Location = New System.Drawing.Point(628, 177)
        Me.txtSalerId.Name = "txtSalerId"
        Me.txtSalerId.Size = New System.Drawing.Size(93, 20)
        Me.txtSalerId.TabIndex = 31
        '
        'cbkSalesArea
        '
        Me.cbkSalesArea.AutoSize = True
        Me.cbkSalesArea.Location = New System.Drawing.Point(342, 177)
        Me.cbkSalesArea.Name = "cbkSalesArea"
        Me.cbkSalesArea.Size = New System.Drawing.Size(80, 17)
        Me.cbkSalesArea.TabIndex = 28
        Me.cbkSalesArea.Text = "เขตการขาย"
        Me.cbkSalesArea.UseVisualStyleBackColor = True
        '
        'txtSalesArea
        '
        Me.txtSalesArea.Enabled = False
        Me.txtSalesArea.Location = New System.Drawing.Point(434, 176)
        Me.txtSalesArea.Name = "txtSalesArea"
        Me.txtSalesArea.Size = New System.Drawing.Size(101, 20)
        Me.txtSalesArea.TabIndex = 29
        '
        'chkNotRed
        '
        Me.chkNotRed.AutoSize = True
        Me.chkNotRed.Checked = True
        Me.chkNotRed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNotRed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkNotRed.Location = New System.Drawing.Point(20, 179)
        Me.chkNotRed.Name = "chkNotRed"
        Me.chkNotRed.Size = New System.Drawing.Size(142, 17)
        Me.chkNotRed.TabIndex = 27
        Me.chkNotRed.Text = "ไม่แสดงเอกสารสีแดง"
        Me.chkNotRed.UseVisualStyleBackColor = True
        '
        'chkInvcoice_Date
        '
        Me.chkInvcoice_Date.AutoSize = True
        Me.chkInvcoice_Date.Location = New System.Drawing.Point(20, 51)
        Me.chkInvcoice_Date.Name = "chkInvcoice_Date"
        Me.chkInvcoice_Date.Size = New System.Drawing.Size(91, 17)
        Me.chkInvcoice_Date.TabIndex = 24
        Me.chkInvcoice_Date.Text = "วันที่ Invcoice"
        Me.chkInvcoice_Date.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(221, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "ถึง"
        '
        'dtInvcoice_Date_B
        '
        Me.dtInvcoice_Date_B.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInvcoice_Date_B.Location = New System.Drawing.Point(112, 49)
        Me.dtInvcoice_Date_B.Name = "dtInvcoice_Date_B"
        Me.dtInvcoice_Date_B.Size = New System.Drawing.Size(106, 20)
        Me.dtInvcoice_Date_B.TabIndex = 25
        '
        'dtInvcoice_Date_E
        '
        Me.dtInvcoice_Date_E.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInvcoice_Date_E.Location = New System.Drawing.Point(246, 49)
        Me.dtInvcoice_Date_E.Name = "dtInvcoice_Date_E"
        Me.dtInvcoice_Date_E.Size = New System.Drawing.Size(106, 20)
        Me.dtInvcoice_Date_E.TabIndex = 26
        '
        'chkDistributionCenter
        '
        Me.chkDistributionCenter.AutoSize = True
        Me.chkDistributionCenter.Location = New System.Drawing.Point(20, 77)
        Me.chkDistributionCenter.Name = "chkDistributionCenter"
        Me.chkDistributionCenter.Size = New System.Drawing.Size(85, 17)
        Me.chkDistributionCenter.TabIndex = 22
        Me.chkDistributionCenter.Text = "ศูนย์กระจาย"
        Me.chkDistributionCenter.UseVisualStyleBackColor = True
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(113, 75)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(356, 21)
        Me.cboDistributionCenter.TabIndex = 21
        '
        'chkInvoiceNotNull
        '
        Me.chkInvoiceNotNull.AutoSize = True
        Me.chkInvoiceNotNull.Location = New System.Drawing.Point(496, 102)
        Me.chkInvoiceNotNull.Name = "chkInvoiceNotNull"
        Me.chkInvoiceNotNull.Size = New System.Drawing.Size(156, 17)
        Me.chkInvoiceNotNull.TabIndex = 14
        Me.chkInvoiceNotNull.Text = "แสดงเฉพาะ SO ที่มี Invoice"
        Me.chkInvoiceNotNull.UseVisualStyleBackColor = True
        '
        'chkInvoiceNo
        '
        Me.chkInvoiceNo.AutoSize = True
        Me.chkInvoiceNo.Location = New System.Drawing.Point(342, 154)
        Me.chkInvoiceNo.Name = "chkInvoiceNo"
        Me.chkInvoiceNo.Size = New System.Drawing.Size(88, 17)
        Me.chkInvoiceNo.TabIndex = 12
        Me.chkInvoiceNo.Text = "เลขที่ Invoice"
        Me.chkInvoiceNo.UseVisualStyleBackColor = True
        '
        'chkConsignee
        '
        Me.chkConsignee.AutoSize = True
        Me.chkConsignee.Location = New System.Drawing.Point(20, 128)
        Me.chkConsignee.Name = "chkConsignee"
        Me.chkConsignee.Size = New System.Drawing.Size(46, 17)
        Me.chkConsignee.TabIndex = 10
        Me.chkConsignee.Text = "ผู้รับ"
        Me.chkConsignee.UseVisualStyleBackColor = True
        '
        'chkSalesOrderNo
        '
        Me.chkSalesOrderNo.AutoSize = True
        Me.chkSalesOrderNo.Location = New System.Drawing.Point(20, 154)
        Me.chkSalesOrderNo.Name = "chkSalesOrderNo"
        Me.chkSalesOrderNo.Size = New System.Drawing.Size(79, 17)
        Me.chkSalesOrderNo.TabIndex = 8
        Me.chkSalesOrderNo.Text = "เลขที่สั่งขาย"
        Me.chkSalesOrderNo.UseVisualStyleBackColor = True
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(20, 102)
        Me.chkCustomer.Name = "chkCustomer"
        Me.chkCustomer.Size = New System.Drawing.Size(51, 17)
        Me.chkCustomer.TabIndex = 4
        Me.chkCustomer.Text = "ลูกค้า"
        Me.chkCustomer.UseVisualStyleBackColor = True
        '
        'chkSalesOrderDate
        '
        Me.chkSalesOrderDate.AutoSize = True
        Me.chkSalesOrderDate.Checked = True
        Me.chkSalesOrderDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSalesOrderDate.Location = New System.Drawing.Point(21, 25)
        Me.chkSalesOrderDate.Name = "chkSalesOrderDate"
        Me.chkSalesOrderDate.Size = New System.Drawing.Size(65, 17)
        Me.chkSalesOrderDate.TabIndex = 1
        Me.chkSalesOrderDate.Text = "วันที่ขาย"
        Me.chkSalesOrderDate.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(636, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 15
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'grdSOView
        '
        Me.grdSOView.AllowUserToAddRows = False
        Me.grdSOView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdSOView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSOView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSOView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSOView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkselect, Me.col_WinspeedExport, Me.col_DistributionCenter, Me.Col_CustomerName, Me.Col_SalesOrder_No, Me.Col_SalesOrder_Date, Me.col_TransportManifest_No, Me.col_Invoice_No, Me.col_Invoice_Date, Me.Col_Consignee, Me.col_Expected_Delivery_Date, Me.Col_CustomerShippingLocation, Me.col_comment, Me.col_Credit, Me.col_Amount, Me.col_Vat, Me.col_Amount_Vat, Me.Customer_Shipping_Location_ID, Me.Col_Status_Desc, Me.col_Status_Manifest_Des, Me.Col_Carrier, Me.Col_DayDesc, Me.Col_SumQty, Me.Col_SumWeight, Me.Col_SumVolume, Me.Col_Addby, Me.col_Confirm_By, Me.col_Confirm_Date, Me.Col_DocumentType, Me.Col_SalesOrder_Index, Me.Col_Status, Me.Col_RGB, Me.Col_SalesUnit, Me.Col_Customer_Index, Me.Col_Customer_Shipping_Id, Me.Col_Customer_Shipping_Index})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSOView.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdSOView.Location = New System.Drawing.Point(12, 227)
        Me.grdSOView.Name = "grdSOView"
        Me.grdSOView.RightToLeft = System.Windows.Forms.RightToLeft.No
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSOView.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdSOView.RowHeadersVisible = False
        Me.grdSOView.RowTemplate.Height = 24
        Me.grdSOView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSOView.Size = New System.Drawing.Size(777, 298)
        Me.grdSOView.TabIndex = 18
        '
        'chkselect
        '
        Me.chkselect.FalseValue = "0"
        Me.chkselect.Frozen = True
        Me.chkselect.HeaderText = ""
        Me.chkselect.Name = "chkselect"
        Me.chkselect.TrueValue = "1"
        Me.chkselect.Width = 25
        '
        'col_WinspeedExport
        '
        Me.col_WinspeedExport.DataPropertyName = "TXT_EXP_NUM"
        Me.col_WinspeedExport.HeaderText = "Win"
        Me.col_WinspeedExport.Name = "col_WinspeedExport"
        Me.col_WinspeedExport.Width = 30
        '
        'col_DistributionCenter
        '
        Me.col_DistributionCenter.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter.Name = "col_DistributionCenter"
        Me.col_DistributionCenter.ReadOnly = True
        '
        'Col_CustomerName
        '
        Me.Col_CustomerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Col_CustomerName.DataPropertyName = "Customer_Name"
        Me.Col_CustomerName.FillWeight = 150.0!
        Me.Col_CustomerName.HeaderText = "ลูกค้า"
        Me.Col_CustomerName.MinimumWidth = 150
        Me.Col_CustomerName.Name = "Col_CustomerName"
        Me.Col_CustomerName.ReadOnly = True
        Me.Col_CustomerName.Width = 150
        '
        'Col_SalesOrder_No
        '
        Me.Col_SalesOrder_No.DataPropertyName = "SalesOrder_No"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Col_SalesOrder_No.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_SalesOrder_No.HeaderText = "เลขที่ใบสั่งขาย"
        Me.Col_SalesOrder_No.Name = "Col_SalesOrder_No"
        Me.Col_SalesOrder_No.ReadOnly = True
        Me.Col_SalesOrder_No.Width = 120
        '
        'Col_SalesOrder_Date
        '
        Me.Col_SalesOrder_Date.DataPropertyName = "SalesOrder_Date"
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Col_SalesOrder_Date.DefaultCellStyle = DataGridViewCellStyle4
        Me.Col_SalesOrder_Date.HeaderText = "วันที่ใบสั่งขาย"
        Me.Col_SalesOrder_Date.Name = "Col_SalesOrder_Date"
        Me.Col_SalesOrder_Date.ReadOnly = True
        Me.Col_SalesOrder_Date.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'col_TransportManifest_No
        '
        Me.col_TransportManifest_No.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No.HeaderText = "ใบคุมรถ"
        Me.col_TransportManifest_No.Name = "col_TransportManifest_No"
        Me.col_TransportManifest_No.ReadOnly = True
        '
        'col_Invoice_No
        '
        Me.col_Invoice_No.DataPropertyName = "Invoice_No"
        Me.col_Invoice_No.HeaderText = "อินวอยน์"
        Me.col_Invoice_No.Name = "col_Invoice_No"
        Me.col_Invoice_No.ReadOnly = True
        '
        'col_Invoice_Date
        '
        Me.col_Invoice_Date.DataPropertyName = "Invoice_Date"
        Me.col_Invoice_Date.HeaderText = "วันที่อินวอยน์"
        Me.col_Invoice_Date.Name = "col_Invoice_Date"
        Me.col_Invoice_Date.ReadOnly = True
        '
        'Col_Consignee
        '
        Me.Col_Consignee.DataPropertyName = "Company_Name"
        Me.Col_Consignee.FillWeight = 120.0!
        Me.Col_Consignee.HeaderText = "ผู้รับสินค้า"
        Me.Col_Consignee.Name = "Col_Consignee"
        Me.Col_Consignee.ReadOnly = True
        Me.Col_Consignee.Width = 120
        '
        'col_Expected_Delivery_Date
        '
        Me.col_Expected_Delivery_Date.DataPropertyName = "Expected_Delivery_Date"
        DataGridViewCellStyle5.Format = "dd/MM/yyyy"
        Me.col_Expected_Delivery_Date.DefaultCellStyle = DataGridViewCellStyle5
        Me.col_Expected_Delivery_Date.HeaderText = "วันที่กำหนดส่ง"
        Me.col_Expected_Delivery_Date.Name = "col_Expected_Delivery_Date"
        Me.col_Expected_Delivery_Date.ReadOnly = True
        '
        'Col_CustomerShippingLocation
        '
        Me.Col_CustomerShippingLocation.DataPropertyName = "Shipping_Location_Name"
        Me.Col_CustomerShippingLocation.FillWeight = 120.0!
        Me.Col_CustomerShippingLocation.HeaderText = "ปลายทางจัดส่ง"
        Me.Col_CustomerShippingLocation.Name = "Col_CustomerShippingLocation"
        Me.Col_CustomerShippingLocation.ReadOnly = True
        Me.Col_CustomerShippingLocation.Visible = False
        Me.Col_CustomerShippingLocation.Width = 120
        '
        'col_comment
        '
        Me.col_comment.DataPropertyName = "Remark"
        Me.col_comment.HeaderText = "หมายเหตุ"
        Me.col_comment.Name = "col_comment"
        Me.col_comment.ReadOnly = True
        Me.col_comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Credit
        '
        Me.col_Credit.DataPropertyName = "Credit_date"
        Me.col_Credit.HeaderText = "เครดิต"
        Me.col_Credit.Name = "col_Credit"
        Me.col_Credit.ReadOnly = True
        '
        'col_Amount
        '
        Me.col_Amount.DataPropertyName = "Amount_All"
        Me.col_Amount.HeaderText = "เงินก่อน Vat."
        Me.col_Amount.Name = "col_Amount"
        Me.col_Amount.ReadOnly = True
        '
        'col_Vat
        '
        Me.col_Vat.DataPropertyName = "VAT"
        Me.col_Vat.HeaderText = "Vat."
        Me.col_Vat.Name = "col_Vat"
        Me.col_Vat.ReadOnly = True
        '
        'col_Amount_Vat
        '
        Me.col_Amount_Vat.DataPropertyName = "Amount_Vat"
        Me.col_Amount_Vat.HeaderText = "เงินรวม Vat."
        Me.col_Amount_Vat.Name = "col_Amount_Vat"
        Me.col_Amount_Vat.ReadOnly = True
        '
        'Customer_Shipping_Location_ID
        '
        Me.Customer_Shipping_Location_ID.DataPropertyName = "Customer_Shipping_Location_ID"
        Me.Customer_Shipping_Location_ID.HeaderText = "รหัสปลายทาง"
        Me.Customer_Shipping_Location_ID.Name = "Customer_Shipping_Location_ID"
        Me.Customer_Shipping_Location_ID.ReadOnly = True
        Me.Customer_Shipping_Location_ID.Visible = False
        '
        'Col_Status_Desc
        '
        Me.Col_Status_Desc.DataPropertyName = "Description"
        Me.Col_Status_Desc.HeaderText = "สถานะการเบิก"
        Me.Col_Status_Desc.Name = "Col_Status_Desc"
        Me.Col_Status_Desc.ReadOnly = True
        Me.Col_Status_Desc.Visible = False
        '
        'col_Status_Manifest_Des
        '
        Me.col_Status_Manifest_Des.DataPropertyName = "Status_Manifest_Des"
        Me.col_Status_Manifest_Des.HeaderText = "สถานะจัดส่ง"
        Me.col_Status_Manifest_Des.Name = "col_Status_Manifest_Des"
        Me.col_Status_Manifest_Des.ReadOnly = True
        Me.col_Status_Manifest_Des.Visible = False
        '
        'Col_Carrier
        '
        Me.Col_Carrier.DataPropertyName = "CarrierDES"
        Me.Col_Carrier.HeaderText = "ผู้จัดส่ง"
        Me.Col_Carrier.Name = "Col_Carrier"
        Me.Col_Carrier.ReadOnly = True
        Me.Col_Carrier.Visible = False
        '
        'Col_DayDesc
        '
        Me.Col_DayDesc.DataPropertyName = "DayDesc"
        Me.Col_DayDesc.HeaderText = "วันจัดส่ง"
        Me.Col_DayDesc.Name = "Col_DayDesc"
        Me.Col_DayDesc.ReadOnly = True
        Me.Col_DayDesc.Visible = False
        '
        'Col_SumQty
        '
        Me.Col_SumQty.DataPropertyName = "QTY"
        Me.Col_SumQty.HeaderText = "จำนวน"
        Me.Col_SumQty.Name = "Col_SumQty"
        Me.Col_SumQty.ReadOnly = True
        Me.Col_SumQty.Visible = False
        '
        'Col_SumWeight
        '
        Me.Col_SumWeight.DataPropertyName = "Weight"
        Me.Col_SumWeight.HeaderText = "น้ำหนัก"
        Me.Col_SumWeight.Name = "Col_SumWeight"
        Me.Col_SumWeight.ReadOnly = True
        Me.Col_SumWeight.Visible = False
        '
        'Col_SumVolume
        '
        Me.Col_SumVolume.DataPropertyName = "Volume"
        Me.Col_SumVolume.HeaderText = "ปริมาตร"
        Me.Col_SumVolume.Name = "Col_SumVolume"
        Me.Col_SumVolume.ReadOnly = True
        Me.Col_SumVolume.Visible = False
        '
        'Col_Addby
        '
        Me.Col_Addby.DataPropertyName = "add_by"
        Me.Col_Addby.HeaderText = "ผู้ทำรายการ"
        Me.Col_Addby.Name = "Col_Addby"
        Me.Col_Addby.ReadOnly = True
        Me.Col_Addby.Visible = False
        '
        'col_Confirm_By
        '
        Me.col_Confirm_By.DataPropertyName = "Confirm_By"
        Me.col_Confirm_By.HeaderText = "ผู้ยืนยัน"
        Me.col_Confirm_By.Name = "col_Confirm_By"
        Me.col_Confirm_By.ReadOnly = True
        Me.col_Confirm_By.Visible = False
        '
        'col_Confirm_Date
        '
        Me.col_Confirm_Date.DataPropertyName = "Confirm_Date"
        DataGridViewCellStyle6.Format = "G"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.col_Confirm_Date.DefaultCellStyle = DataGridViewCellStyle6
        Me.col_Confirm_Date.HeaderText = "วันยืนยัน"
        Me.col_Confirm_Date.Name = "col_Confirm_Date"
        Me.col_Confirm_Date.ReadOnly = True
        Me.col_Confirm_Date.Visible = False
        Me.col_Confirm_Date.Width = 150
        '
        'Col_DocumentType
        '
        Me.Col_DocumentType.DataPropertyName = "DocumentType"
        Me.Col_DocumentType.HeaderText = "ประเภทเอกสาร"
        Me.Col_DocumentType.Name = "Col_DocumentType"
        Me.Col_DocumentType.ReadOnly = True
        Me.Col_DocumentType.Visible = False
        Me.Col_DocumentType.Width = 120
        '
        'Col_SalesOrder_Index
        '
        Me.Col_SalesOrder_Index.DataPropertyName = "SalesOrder_Index"
        Me.Col_SalesOrder_Index.HeaderText = "Col_SalesOrder_Index"
        Me.Col_SalesOrder_Index.Name = "Col_SalesOrder_Index"
        Me.Col_SalesOrder_Index.ReadOnly = True
        Me.Col_SalesOrder_Index.Visible = False
        '
        'Col_Status
        '
        Me.Col_Status.DataPropertyName = "Status"
        Me.Col_Status.HeaderText = "Col_Status"
        Me.Col_Status.Name = "Col_Status"
        Me.Col_Status.ReadOnly = True
        Me.Col_Status.Visible = False
        '
        'Col_RGB
        '
        Me.Col_RGB.DataPropertyName = "RGB_Check"
        Me.Col_RGB.HeaderText = "Col_RGB"
        Me.Col_RGB.Name = "Col_RGB"
        Me.Col_RGB.ReadOnly = True
        Me.Col_RGB.Visible = False
        '
        'Col_SalesUnit
        '
        Me.Col_SalesUnit.DataPropertyName = "SalesUnit"
        Me.Col_SalesUnit.HeaderText = "Col_SalesUnit"
        Me.Col_SalesUnit.Name = "Col_SalesUnit"
        Me.Col_SalesUnit.ReadOnly = True
        Me.Col_SalesUnit.Visible = False
        '
        'Col_Customer_Index
        '
        Me.Col_Customer_Index.DataPropertyName = "Customer_Index"
        Me.Col_Customer_Index.HeaderText = "Col_Customer_Index"
        Me.Col_Customer_Index.Name = "Col_Customer_Index"
        Me.Col_Customer_Index.Visible = False
        '
        'Col_Customer_Shipping_Id
        '
        Me.Col_Customer_Shipping_Id.DataPropertyName = "Customer_Shipping_Id"
        Me.Col_Customer_Shipping_Id.HeaderText = "Customer_Shipping_Id"
        Me.Col_Customer_Shipping_Id.Name = "Col_Customer_Shipping_Id"
        Me.Col_Customer_Shipping_Id.ReadOnly = True
        Me.Col_Customer_Shipping_Id.Visible = False
        '
        'Col_Customer_Shipping_Index
        '
        Me.Col_Customer_Shipping_Index.DataPropertyName = "Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.HeaderText = "Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.Name = "Col_Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.ReadOnly = True
        Me.Col_Customer_Shipping_Index.Visible = False
        '
        'btn_Print
        '
        Me.btn_Print.Image = CType(resources.GetObject("btn_Print.Image"), System.Drawing.Image)
        Me.btn_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Print.Location = New System.Drawing.Point(483, 530)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(100, 38)
        Me.btn_Print.TabIndex = 16
        Me.btn_Print.Text = "พิมพ์"
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(693, 530)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 38)
        Me.btnExit.TabIndex = 18
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnGenInvoice
        '
        Me.btnGenInvoice.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnGenInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenInvoice.Location = New System.Drawing.Point(18, 530)
        Me.btnGenInvoice.Name = "btnGenInvoice"
        Me.btnGenInvoice.Size = New System.Drawing.Size(100, 38)
        Me.btnGenInvoice.TabIndex = 17
        Me.btnGenInvoice.Text = "     Create Invoice"
        Me.btnGenInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenInvoice.UseVisualStyleBackColor = True
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportExcel.Location = New System.Drawing.Point(271, 569)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(104, 37)
        Me.btnExportExcel.TabIndex = 19
        Me.btnExportExcel.Text = "       Export Excel" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "       WinSpeed"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        Me.btnExportExcel.Visible = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(18, 232)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 20
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'btnPrintPickingList
        '
        Me.btnPrintPickingList.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.btnPrintPickingList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintPickingList.Location = New System.Drawing.Point(589, 530)
        Me.btnPrintPickingList.Name = "btnPrintPickingList"
        Me.btnPrintPickingList.Size = New System.Drawing.Size(100, 37)
        Me.btnPrintPickingList.TabIndex = 21
        Me.btnPrintPickingList.Text = "       Packing List"
        Me.btnPrintPickingList.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(381, 530)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(100, 37)
        Me.btnExport.TabIndex = 22
        Me.btnExport.Text = "Export"
        Me.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "SalesOrder_No"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "เลขที่ใบสั่งขาย"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SalesOrder_Date"
        DataGridViewCellStyle10.Format = "d"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn2.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "วันที่ใบสั่งขาย"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 150
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TransportManifest_No"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "ใบคุมรถ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Invoice_No"
        DataGridViewCellStyle12.Format = "d"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "อินวอยน์"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn5.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "ผู้รับสินค้า"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn6.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "ปลายทางจัดส่ง"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Customer_Shipping_Location_ID"
        Me.DataGridViewTextBoxColumn7.Frozen = True
        Me.DataGridViewTextBoxColumn7.HeaderText = "รหัสปลายทาง"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn8.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn8.Frozen = True
        Me.DataGridViewTextBoxColumn8.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Description"
        DataGridViewCellStyle13.Format = "dd/MM/yyyy"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn9.Frozen = True
        Me.DataGridViewTextBoxColumn9.HeaderText = "สถานะการเบิก"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Status_Manifest_Des"
        Me.DataGridViewTextBoxColumn10.Frozen = True
        Me.DataGridViewTextBoxColumn10.HeaderText = "สถานะจัดส่ง"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "CarrierDES"
        Me.DataGridViewTextBoxColumn11.Frozen = True
        Me.DataGridViewTextBoxColumn11.HeaderText = "ผู้จัดส่ง"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn12.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn12.Frozen = True
        Me.DataGridViewTextBoxColumn12.HeaderText = "ลูกค้า"
        Me.DataGridViewTextBoxColumn12.MinimumWidth = 150
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 150
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "DayDesc"
        Me.DataGridViewTextBoxColumn13.Frozen = True
        Me.DataGridViewTextBoxColumn13.HeaderText = "วันจัดส่ง"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Expected_Delivery_Date"
        DataGridViewCellStyle14.Format = "dd/MM/yyyy"
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn14.Frozen = True
        Me.DataGridViewTextBoxColumn14.HeaderText = "วันที่กำหนดส่ง"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "QTY"
        Me.DataGridViewTextBoxColumn15.Frozen = True
        Me.DataGridViewTextBoxColumn15.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Weight"
        Me.DataGridViewTextBoxColumn16.Frozen = True
        Me.DataGridViewTextBoxColumn16.HeaderText = "น้ำหนัก"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Volume"
        Me.DataGridViewTextBoxColumn17.Frozen = True
        Me.DataGridViewTextBoxColumn17.HeaderText = "ปริมาตร"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Remark"
        Me.DataGridViewTextBoxColumn18.Frozen = True
        Me.DataGridViewTextBoxColumn18.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn18.Visible = False
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "add_by"
        Me.DataGridViewTextBoxColumn19.Frozen = True
        Me.DataGridViewTextBoxColumn19.HeaderText = "ผู้ทำรายการ"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Visible = False
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "Confirm_By"
        Me.DataGridViewTextBoxColumn20.Frozen = True
        Me.DataGridViewTextBoxColumn20.HeaderText = "ผู้ยืนยัน"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Visible = False
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "Confirm_Date"
        DataGridViewCellStyle15.Format = "G"
        DataGridViewCellStyle15.NullValue = Nothing
        Me.DataGridViewTextBoxColumn21.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewTextBoxColumn21.Frozen = True
        Me.DataGridViewTextBoxColumn21.HeaderText = "วันยืนยัน"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 150
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "DocumentType"
        Me.DataGridViewTextBoxColumn22.Frozen = True
        Me.DataGridViewTextBoxColumn22.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        Me.DataGridViewTextBoxColumn22.Width = 120
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "SalesOrder_Index"
        Me.DataGridViewTextBoxColumn23.Frozen = True
        Me.DataGridViewTextBoxColumn23.HeaderText = "Col_SalesOrder_Index"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.Visible = False
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn24.Frozen = True
        Me.DataGridViewTextBoxColumn24.HeaderText = "Col_Status"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Visible = False
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "RGB_Check"
        Me.DataGridViewTextBoxColumn25.Frozen = True
        Me.DataGridViewTextBoxColumn25.HeaderText = "Col_RGB"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        Me.DataGridViewTextBoxColumn25.Visible = False
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "SalesUnit"
        Me.DataGridViewTextBoxColumn26.Frozen = True
        Me.DataGridViewTextBoxColumn26.HeaderText = "Col_SalesUnit"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        Me.DataGridViewTextBoxColumn26.Visible = False
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Customer_Index"
        Me.DataGridViewTextBoxColumn27.Frozen = True
        Me.DataGridViewTextBoxColumn27.HeaderText = "Col_Customer_Index"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Visible = False
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "Customer_Shipping_Id"
        Me.DataGridViewTextBoxColumn28.Frozen = True
        Me.DataGridViewTextBoxColumn28.HeaderText = "Customer_Shipping_Id"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Visible = False
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "Customer_Shipping_Index"
        Me.DataGridViewTextBoxColumn29.Frozen = True
        Me.DataGridViewTextBoxColumn29.HeaderText = "Customer_Shipping_Index"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Visible = False
        '
        'btnExportText
        '
        Me.btnExportText.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btnExportText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportText.Location = New System.Drawing.Point(271, 530)
        Me.btnExportText.Name = "btnExportText"
        Me.btnExportText.Size = New System.Drawing.Size(104, 37)
        Me.btnExportText.TabIndex = 23
        Me.btnExportText.Text = "       Export Text" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "       WinSpeed"
        Me.btnExportText.UseVisualStyleBackColor = True
        '
        'btnCancelInvoice
        '
        Me.btnCancelInvoice.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ยกเลิกรายการ
        Me.btnCancelInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelInvoice.Location = New System.Drawing.Point(124, 530)
        Me.btnCancelInvoice.Name = "btnCancelInvoice"
        Me.btnCancelInvoice.Size = New System.Drawing.Size(100, 38)
        Me.btnCancelInvoice.TabIndex = 24
        Me.btnCancelInvoice.Text = "     Cancel Invoice"
        Me.btnCancelInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelInvoice.UseVisualStyleBackColor = True
        '
        'frmInvoice_Print
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(801, 612)
        Me.Controls.Add(Me.btnCancelInvoice)
        Me.Controls.Add(Me.btnExportText)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnPrintPickingList)
        Me.Controls.Add(Me.chkSelectAll)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnGenInvoice)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btn_Print)
        Me.Controls.Add(Me.grdSOView)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmInvoice_Print"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ใบกำกับภาษี"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdSOView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchCustomer As System.Windows.Forms.Button
    Friend WithEvents txtSalesOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents btnConsignee As System.Windows.Forms.Button
    Friend WithEvents txtConsignee_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtConsignee_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btn_Print As System.Windows.Forms.Button
    Friend WithEvents chkSalesOrderNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesOrderDate As System.Windows.Forms.CheckBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents chkInvoiceNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkConsignee As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdSOView As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnPrintPickingList As System.Windows.Forms.Button
    Friend WithEvents chkInvcoice_Date As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtInvcoice_Date_B As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtInvcoice_Date_E As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDistributionCenter As System.Windows.Forms.CheckBox
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents chkNotRed As System.Windows.Forms.CheckBox
    Public WithEvents btnGenInvoice As System.Windows.Forms.Button
    Friend WithEvents cbkSalesArea As System.Windows.Forms.CheckBox
    Friend WithEvents txtSalesArea As System.Windows.Forms.TextBox
    Friend WithEvents cbkSalerId As System.Windows.Forms.CheckBox
    Friend WithEvents txtSalerId As System.Windows.Forms.TextBox
    Friend WithEvents btnExportText As System.Windows.Forms.Button
    Friend WithEvents chkselect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_WinspeedExport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CustomerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Invoice_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Invoice_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Consignee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Expected_Delivery_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CustomerShippingLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_comment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Credit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Amount_Vat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Shipping_Location_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Status_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_Manifest_Des As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Carrier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DayDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SumQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SumWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SumVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Addby As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Confirm_By As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Confirm_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DocumentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_RGB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Customer_Shipping_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Customer_Shipping_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents btnCancelInvoice As System.Windows.Forms.Button
    Friend WithEvents chkInvoiceNull As System.Windows.Forms.CheckBox
    Friend WithEvents chkInvoiceNotNull As System.Windows.Forms.CheckBox
    Friend WithEvents chkExpWinNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkExpWin As System.Windows.Forms.CheckBox
End Class
