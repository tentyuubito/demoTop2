<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderView))
        Me.grdOrderView = New System.Windows.Forms.DataGridView
        Me.gbCondition = New System.Windows.Forms.GroupBox
        Me.rdbDeclar = New System.Windows.Forms.RadioButton
        Me.btnPop_Search = New System.Windows.Forms.Button
        Me.lb_to = New System.Windows.Forms.Label
        Me.dateEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.txtKeySearch = New System.Windows.Forms.TextBox
        Me.rdbSupplier = New System.Windows.Forms.RadioButton
        Me.btnSearch = New System.Windows.Forms.Button
        Me.rdbReferent = New System.Windows.Forms.RadioButton
        Me.rdb_Sku = New System.Windows.Forms.RadioButton
        Me.rdbPo = New System.Windows.Forms.RadioButton
        Me.rdbDepartment = New System.Windows.Forms.RadioButton
        Me.rdbCustomer = New System.Windows.Forms.RadioButton
        Me.rdbOrder_No = New System.Windows.Forms.RadioButton
        Me.rdbOrder_Date = New System.Windows.Forms.RadioButton
        Me.gbFilter = New System.Windows.Forms.GroupBox
        Me.lblFilterDocType = New System.Windows.Forms.Label
        Me.lblFilterStatus = New System.Windows.Forms.Label
        Me.cbReciveType = New System.Windows.Forms.ComboBox
        Me.cboDocumentStatus = New System.Windows.Forms.ComboBox
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.gbPrintReport = New System.Windows.Forms.GroupBox
        Me.btn_Print = New System.Windows.Forms.Button
        Me.lblSelectReport = New System.Windows.Forms.Label
        Me.cbPrint = New System.Windows.Forms.ComboBox
        Me.lbCountRows = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnEditOrder = New System.Windows.Forms.Button
        Me.btnStoreIn = New System.Windows.Forms.Button
        Me.btnNewOrder = New System.Windows.Forms.Button
        Me.btnTag = New System.Windows.Forms.Button
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
        Me.btnConfrim_PutAway = New System.Windows.Forms.Button
        Me.btnAssignJob = New System.Windows.Forms.Button
        Me.grbPageEng = New System.Windows.Forms.GroupBox
        Me.rdAll = New System.Windows.Forms.RadioButton
        Me.txtTop = New System.Windows.Forms.TextBox
        Me.rdRowPage = New System.Windows.Forms.RadioButton
        Me.rdTop = New System.Windows.Forms.RadioButton
        Me.lblTopRow = New System.Windows.Forms.Label
        Me.txtTotalPage = New System.Windows.Forms.TextBox
        Me.txtPageIndex = New System.Windows.Forms.TextBox
        Me.lblSplit = New System.Windows.Forms.Label
        Me.btnPageLast = New System.Windows.Forms.Button
        Me.btnPageNext = New System.Windows.Forms.Button
        Me.btnPagePrev = New System.Windows.Forms.Button
        Me.btnPageFirst = New System.Windows.Forms.Button
        Me.cboRowPerPage = New System.Windows.Forms.ComboBox
        Me.txtRowCount = New System.Windows.Forms.TextBox
        Me.lblrow = New System.Windows.Forms.Label
        Me.lbltotal = New System.Windows.Forms.Label
        Me.System_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Declaration_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Order_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cl_OrderType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cl_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cl_Referent = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Add_by = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.StatusValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Receive_Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdOrderView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCondition.SuspendLayout()
        Me.gbFilter.SuspendLayout()
        Me.pnLeftmenu.SuspendLayout()
        Me.gbPrintReport.SuspendLayout()
        Me.grbPageEng.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdOrderView
        '
        Me.grdOrderView.AllowUserToAddRows = False
        Me.grdOrderView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdOrderView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdOrderView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrderView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOrderView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdOrderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOrderView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.System_Index, Me.Document_No, Me.col_Declaration_No, Me.Order_Date, Me.cl_OrderType, Me.Status, Me.Customer_Name, Me.cl_Supplier, Me.cl_Referent, Me.Add_by, Me.col_DistributionCenter, Me.StatusValue, Me.col_Receive_Type})
        Me.grdOrderView.Location = New System.Drawing.Point(144, 49)
        Me.grdOrderView.MultiSelect = False
        Me.grdOrderView.Name = "grdOrderView"
        Me.grdOrderView.ReadOnly = True
        Me.grdOrderView.RowHeadersVisible = False
        Me.grdOrderView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdOrderView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdOrderView.Size = New System.Drawing.Size(864, 548)
        Me.grdOrderView.TabIndex = 1
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.rdbDeclar)
        Me.gbCondition.Controls.Add(Me.btnPop_Search)
        Me.gbCondition.Controls.Add(Me.lb_to)
        Me.gbCondition.Controls.Add(Me.dateEnd)
        Me.gbCondition.Controls.Add(Me.dtpDate)
        Me.gbCondition.Controls.Add(Me.txtKeySearch)
        Me.gbCondition.Controls.Add(Me.rdbSupplier)
        Me.gbCondition.Controls.Add(Me.btnSearch)
        Me.gbCondition.Controls.Add(Me.rdbReferent)
        Me.gbCondition.Controls.Add(Me.rdb_Sku)
        Me.gbCondition.Controls.Add(Me.rdbPo)
        Me.gbCondition.Controls.Add(Me.rdbDepartment)
        Me.gbCondition.Controls.Add(Me.rdbCustomer)
        Me.gbCondition.Controls.Add(Me.rdbOrder_No)
        Me.gbCondition.Controls.Add(Me.rdbOrder_Date)
        Me.gbCondition.Location = New System.Drawing.Point(7, 2)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Size = New System.Drawing.Size(131, 372)
        Me.gbCondition.TabIndex = 0
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "เงื่อนไข"
        '
        'rdbDeclar
        '
        Me.rdbDeclar.AutoSize = True
        Me.rdbDeclar.BackColor = System.Drawing.SystemColors.Control
        Me.rdbDeclar.Location = New System.Drawing.Point(14, 180)
        Me.rdbDeclar.Name = "rdbDeclar"
        Me.rdbDeclar.Size = New System.Drawing.Size(102, 17)
        Me.rdbDeclar.TabIndex = 7
        Me.rdbDeclar.Text = "เลขที่ใบขนสินค้า"
        Me.rdbDeclar.UseVisualStyleBackColor = True
        '
        'btnPop_Search
        '
        Me.btnPop_Search.Location = New System.Drawing.Point(13, 236)
        Me.btnPop_Search.Name = "btnPop_Search"
        Me.btnPop_Search.Size = New System.Drawing.Size(24, 23)
        Me.btnPop_Search.TabIndex = 9
        Me.btnPop_Search.Text = "..."
        Me.btnPop_Search.UseVisualStyleBackColor = True
        Me.btnPop_Search.Visible = False
        '
        'lb_to
        '
        Me.lb_to.AutoSize = True
        Me.lb_to.Location = New System.Drawing.Point(50, 287)
        Me.lb_to.Name = "lb_to"
        Me.lb_to.Size = New System.Drawing.Size(19, 13)
        Me.lb_to.TabIndex = 11
        Me.lb_to.Text = "ถึง"
        '
        'dateEnd
        '
        Me.dateEnd.CustomFormat = "dd/MM/yyy"
        Me.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateEnd.Location = New System.Drawing.Point(6, 303)
        Me.dateEnd.Name = "dateEnd"
        Me.dateEnd.Size = New System.Drawing.Size(119, 20)
        Me.dateEnd.TabIndex = 12
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(6, 265)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpDate.TabIndex = 10
        '
        'txtKeySearch
        '
        Me.txtKeySearch.Location = New System.Drawing.Point(13, 264)
        Me.txtKeySearch.Name = "txtKeySearch"
        Me.txtKeySearch.Size = New System.Drawing.Size(100, 20)
        Me.txtKeySearch.TabIndex = 6
        '
        'rdbSupplier
        '
        Me.rdbSupplier.AutoSize = True
        Me.rdbSupplier.Location = New System.Drawing.Point(14, 88)
        Me.rdbSupplier.Name = "rdbSupplier"
        Me.rdbSupplier.Size = New System.Drawing.Size(63, 17)
        Me.rdbSupplier.TabIndex = 3
        Me.rdbSupplier.Text = "ชื่อผู้ขาย"
        Me.rdbSupplier.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(15, 328)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 13
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'rdbReferent
        '
        Me.rdbReferent.AutoSize = True
        Me.rdbReferent.Location = New System.Drawing.Point(14, 134)
        Me.rdbReferent.Name = "rdbReferent"
        Me.rdbReferent.Size = New System.Drawing.Size(111, 17)
        Me.rdbReferent.TabIndex = 5
        Me.rdbReferent.Text = "เลขที่เอกสารอ้างอิง"
        Me.rdbReferent.UseVisualStyleBackColor = True
        '
        'rdb_Sku
        '
        Me.rdb_Sku.AutoSize = True
        Me.rdb_Sku.Location = New System.Drawing.Point(15, 203)
        Me.rdb_Sku.Name = "rdb_Sku"
        Me.rdb_Sku.Size = New System.Drawing.Size(69, 17)
        Me.rdb_Sku.TabIndex = 8
        Me.rdb_Sku.Text = "รหัส SKU"
        Me.rdb_Sku.UseVisualStyleBackColor = True
        '
        'rdbPo
        '
        Me.rdbPo.AutoSize = True
        Me.rdbPo.Location = New System.Drawing.Point(14, 157)
        Me.rdbPo.Name = "rdbPo"
        Me.rdbPo.Size = New System.Drawing.Size(86, 17)
        Me.rdbPo.TabIndex = 6
        Me.rdbPo.Text = "เลขที่ใบสั่งซื้อ"
        Me.rdbPo.UseVisualStyleBackColor = True
        '
        'rdbDepartment
        '
        Me.rdbDepartment.AutoSize = True
        Me.rdbDepartment.Location = New System.Drawing.Point(14, 111)
        Me.rdbDepartment.Name = "rdbDepartment"
        Me.rdbDepartment.Size = New System.Drawing.Size(55, 17)
        Me.rdbDepartment.TabIndex = 4
        Me.rdbDepartment.Text = "แผนก"
        Me.rdbDepartment.UseVisualStyleBackColor = True
        '
        'rdbCustomer
        '
        Me.rdbCustomer.AutoSize = True
        Me.rdbCustomer.Location = New System.Drawing.Point(14, 65)
        Me.rdbCustomer.Name = "rdbCustomer"
        Me.rdbCustomer.Size = New System.Drawing.Size(63, 17)
        Me.rdbCustomer.TabIndex = 2
        Me.rdbCustomer.Text = "ชื่อลูกค้า"
        Me.rdbCustomer.UseVisualStyleBackColor = True
        '
        'rdbOrder_No
        '
        Me.rdbOrder_No.AutoSize = True
        Me.rdbOrder_No.Location = New System.Drawing.Point(14, 42)
        Me.rdbOrder_No.Name = "rdbOrder_No"
        Me.rdbOrder_No.Size = New System.Drawing.Size(101, 17)
        Me.rdbOrder_No.TabIndex = 1
        Me.rdbOrder_No.Text = "เลขที่ใบรับสินค้า"
        Me.rdbOrder_No.UseVisualStyleBackColor = True
        '
        'rdbOrder_Date
        '
        Me.rdbOrder_Date.AutoSize = True
        Me.rdbOrder_Date.Checked = True
        Me.rdbOrder_Date.Location = New System.Drawing.Point(14, 21)
        Me.rdbOrder_Date.Name = "rdbOrder_Date"
        Me.rdbOrder_Date.Size = New System.Drawing.Size(85, 17)
        Me.rdbOrder_Date.TabIndex = 0
        Me.rdbOrder_Date.TabStop = True
        Me.rdbOrder_Date.Text = "วันที่รับสินค้า"
        Me.rdbOrder_Date.UseVisualStyleBackColor = True
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.lblFilterDocType)
        Me.gbFilter.Controls.Add(Me.lblFilterStatus)
        Me.gbFilter.Controls.Add(Me.cbReciveType)
        Me.gbFilter.Controls.Add(Me.cboDocumentStatus)
        Me.gbFilter.Location = New System.Drawing.Point(7, 380)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(131, 111)
        Me.gbFilter.TabIndex = 1
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "กรองรายการ"
        '
        'lblFilterDocType
        '
        Me.lblFilterDocType.AutoSize = True
        Me.lblFilterDocType.Location = New System.Drawing.Point(9, 65)
        Me.lblFilterDocType.Name = "lblFilterDocType"
        Me.lblFilterDocType.Size = New System.Drawing.Size(75, 13)
        Me.lblFilterDocType.TabIndex = 2
        Me.lblFilterDocType.Text = "ประเภทการรับ"
        '
        'lblFilterStatus
        '
        Me.lblFilterStatus.AutoSize = True
        Me.lblFilterStatus.Location = New System.Drawing.Point(9, 19)
        Me.lblFilterStatus.Name = "lblFilterStatus"
        Me.lblFilterStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblFilterStatus.TabIndex = 0
        Me.lblFilterStatus.Text = "สถานะเอกสาร"
        '
        'cbReciveType
        '
        Me.cbReciveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReciveType.FormattingEnabled = True
        Me.cbReciveType.Location = New System.Drawing.Point(6, 81)
        Me.cbReciveType.Name = "cbReciveType"
        Me.cbReciveType.Size = New System.Drawing.Size(119, 21)
        Me.cbReciveType.TabIndex = 15
        '
        'cboDocumentStatus
        '
        Me.cboDocumentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentStatus.FormattingEnabled = True
        Me.cboDocumentStatus.Location = New System.Drawing.Point(5, 35)
        Me.cboDocumentStatus.Name = "cboDocumentStatus"
        Me.cboDocumentStatus.Size = New System.Drawing.Size(120, 21)
        Me.cboDocumentStatus.TabIndex = 14
        '
        'pnLeftmenu
        '
        Me.pnLeftmenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnLeftmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnLeftmenu.Controls.Add(Me.gbCondition)
        Me.pnLeftmenu.Controls.Add(Me.gbPrintReport)
        Me.pnLeftmenu.Controls.Add(Me.gbFilter)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 7)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(143, 608)
        Me.pnLeftmenu.TabIndex = 0
        '
        'gbPrintReport
        '
        Me.gbPrintReport.Controls.Add(Me.btn_Print)
        Me.gbPrintReport.Controls.Add(Me.lblSelectReport)
        Me.gbPrintReport.Controls.Add(Me.cbPrint)
        Me.gbPrintReport.Location = New System.Drawing.Point(7, 497)
        Me.gbPrintReport.Name = "gbPrintReport"
        Me.gbPrintReport.Size = New System.Drawing.Size(131, 108)
        Me.gbPrintReport.TabIndex = 2
        Me.gbPrintReport.TabStop = False
        Me.gbPrintReport.Text = "พิมพ์เอกสาร"
        '
        'btn_Print
        '
        Me.btn_Print.Image = CType(resources.GetObject("btn_Print.Image"), System.Drawing.Image)
        Me.btn_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Print.Location = New System.Drawing.Point(15, 60)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(100, 38)
        Me.btn_Print.TabIndex = 17
        Me.btn_Print.Text = "พิมพ์"
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'lblSelectReport
        '
        Me.lblSelectReport.AutoSize = True
        Me.lblSelectReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSelectReport.Location = New System.Drawing.Point(11, 17)
        Me.lblSelectReport.Name = "lblSelectReport"
        Me.lblSelectReport.Size = New System.Drawing.Size(79, 13)
        Me.lblSelectReport.TabIndex = 0
        Me.lblSelectReport.Text = "ประเภทเอกสาร"
        '
        'cbPrint
        '
        Me.cbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrint.FormattingEnabled = True
        Me.cbPrint.Location = New System.Drawing.Point(5, 33)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(120, 21)
        Me.cbPrint.TabIndex = 16
        '
        'lbCountRows
        '
        Me.lbCountRows.AutoSize = True
        Me.lbCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows.Location = New System.Drawing.Point(141, 602)
        Me.lbCountRows.Name = "lbCountRows"
        Me.lbCountRows.Size = New System.Drawing.Size(71, 13)
        Me.lbCountRows.TabIndex = 2
        Me.lbCountRows.Text = "ไม่พบรายการ"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(901, 622)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 24
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(603, 622)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(107, 38)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "ยกเลิก       "
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEditOrder
        '
        Me.btnEditOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEditOrder.Image = CType(resources.GetObject("btnEditOrder.Image"), System.Drawing.Image)
        Me.btnEditOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditOrder.Location = New System.Drawing.Point(258, 622)
        Me.btnEditOrder.Name = "btnEditOrder"
        Me.btnEditOrder.Size = New System.Drawing.Size(107, 38)
        Me.btnEditOrder.TabIndex = 19
        Me.btnEditOrder.Text = "แก้ไขรายการ"
        Me.btnEditOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditOrder.UseVisualStyleBackColor = True
        '
        'btnStoreIn
        '
        Me.btnStoreIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnStoreIn.Image = CType(resources.GetObject("btnStoreIn.Image"), System.Drawing.Image)
        Me.btnStoreIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStoreIn.Location = New System.Drawing.Point(371, 622)
        Me.btnStoreIn.Name = "btnStoreIn"
        Me.btnStoreIn.Size = New System.Drawing.Size(110, 38)
        Me.btnStoreIn.TabIndex = 20
        Me.btnStoreIn.Text = "จัดเก็บ"
        Me.btnStoreIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnStoreIn.UseVisualStyleBackColor = True
        '
        'btnNewOrder
        '
        Me.btnNewOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnNewOrder.Image = CType(resources.GetObject("btnNewOrder.Image"), System.Drawing.Image)
        Me.btnNewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewOrder.Location = New System.Drawing.Point(145, 622)
        Me.btnNewOrder.Name = "btnNewOrder"
        Me.btnNewOrder.Size = New System.Drawing.Size(107, 38)
        Me.btnNewOrder.TabIndex = 18
        Me.btnNewOrder.Text = "เพิ่มรายการ"
        Me.btnNewOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewOrder.UseVisualStyleBackColor = True
        '
        'btnTag
        '
        Me.btnTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnTag.Image = CType(resources.GetObject("btnTag.Image"), System.Drawing.Image)
        Me.btnTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTag.Location = New System.Drawing.Point(487, 622)
        Me.btnTag.Name = "btnTag"
        Me.btnTag.Size = New System.Drawing.Size(110, 38)
        Me.btnTag.TabIndex = 21
        Me.btnTag.Text = "จัดการ TAG"
        Me.btnTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTag.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่ใบรับสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่รับสินค้า "
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อลูกค้า "
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "ผู้ออกใบรับรับสินค้า"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "ผู้ออกใบรับรับสินค้า"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "เอกสารอ้างอิง"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "ผู้ออกใบรับ"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 220
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = ""
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 10
        '
        'btnConfrim_PutAway
        '
        Me.btnConfrim_PutAway.Image = CType(resources.GetObject("btnConfrim_PutAway.Image"), System.Drawing.Image)
        Me.btnConfrim_PutAway.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfrim_PutAway.Location = New System.Drawing.Point(487, 622)
        Me.btnConfrim_PutAway.Name = "btnConfrim_PutAway"
        Me.btnConfrim_PutAway.Size = New System.Drawing.Size(110, 38)
        Me.btnConfrim_PutAway.TabIndex = 23
        Me.btnConfrim_PutAway.Text = "     ยืนยัน "
        Me.btnConfrim_PutAway.UseVisualStyleBackColor = True
        Me.btnConfrim_PutAway.Visible = False
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAssignJob.Image = CType(resources.GetObject("btnAssignJob.Image"), System.Drawing.Image)
        Me.btnAssignJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAssignJob.Location = New System.Drawing.Point(716, 622)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(107, 38)
        Me.btnAssignJob.TabIndex = 34
        Me.btnAssignJob.Text = "มอบหมายงาน"
        Me.btnAssignJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAssignJob.UseVisualStyleBackColor = True
        '
        'grbPageEng
        '
        Me.grbPageEng.Controls.Add(Me.rdAll)
        Me.grbPageEng.Controls.Add(Me.txtTop)
        Me.grbPageEng.Controls.Add(Me.rdRowPage)
        Me.grbPageEng.Controls.Add(Me.rdTop)
        Me.grbPageEng.Controls.Add(Me.lblTopRow)
        Me.grbPageEng.Controls.Add(Me.txtTotalPage)
        Me.grbPageEng.Controls.Add(Me.txtPageIndex)
        Me.grbPageEng.Controls.Add(Me.lblSplit)
        Me.grbPageEng.Controls.Add(Me.btnPageLast)
        Me.grbPageEng.Controls.Add(Me.btnPageNext)
        Me.grbPageEng.Controls.Add(Me.btnPagePrev)
        Me.grbPageEng.Controls.Add(Me.btnPageFirst)
        Me.grbPageEng.Controls.Add(Me.cboRowPerPage)
        Me.grbPageEng.Controls.Add(Me.txtRowCount)
        Me.grbPageEng.Controls.Add(Me.lblrow)
        Me.grbPageEng.Controls.Add(Me.lbltotal)
        Me.grbPageEng.Location = New System.Drawing.Point(144, 7)
        Me.grbPageEng.Name = "grbPageEng"
        Me.grbPageEng.Size = New System.Drawing.Size(1215, 41)
        Me.grbPageEng.TabIndex = 216
        Me.grbPageEng.TabStop = False
        Me.grbPageEng.Text = "การแสดงผล"
        '
        'rdAll
        '
        Me.rdAll.AutoSize = True
        Me.rdAll.Location = New System.Drawing.Point(12, 17)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(84, 17)
        Me.rdAll.TabIndex = 22
        Me.rdAll.Text = "แสดงทั้งหมด"
        Me.rdAll.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(169, 16)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(50, 20)
        Me.txtTop.TabIndex = 18
        Me.txtTop.Text = "100"
        Me.txtTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdRowPage
        '
        Me.rdRowPage.AutoSize = True
        Me.rdRowPage.Location = New System.Drawing.Point(247, 16)
        Me.rdRowPage.Name = "rdRowPage"
        Me.rdRowPage.Size = New System.Drawing.Size(86, 17)
        Me.rdRowPage.TabIndex = 21
        Me.rdRowPage.Text = "รายการ/หน้า"
        Me.rdRowPage.UseVisualStyleBackColor = True
        '
        'rdTop
        '
        Me.rdTop.AutoSize = True
        Me.rdTop.Checked = True
        Me.rdTop.Location = New System.Drawing.Point(97, 17)
        Me.rdTop.Name = "rdTop"
        Me.rdTop.Size = New System.Drawing.Size(75, 17)
        Me.rdTop.TabIndex = 20
        Me.rdTop.TabStop = True
        Me.rdTop.Text = "แสดงสูงสุด"
        Me.rdTop.UseVisualStyleBackColor = True
        '
        'lblTopRow
        '
        Me.lblTopRow.AutoSize = True
        Me.lblTopRow.Location = New System.Drawing.Point(219, 19)
        Me.lblTopRow.Name = "lblTopRow"
        Me.lblTopRow.Size = New System.Drawing.Size(28, 13)
        Me.lblTopRow.TabIndex = 19
        Me.lblTopRow.Text = "แถว"
        '
        'txtTotalPage
        '
        Me.txtTotalPage.Location = New System.Drawing.Point(507, 13)
        Me.txtTotalPage.Name = "txtTotalPage"
        Me.txtTotalPage.ReadOnly = True
        Me.txtTotalPage.Size = New System.Drawing.Size(44, 20)
        Me.txtTotalPage.TabIndex = 4
        Me.txtTotalPage.TabStop = False
        '
        'txtPageIndex
        '
        Me.txtPageIndex.Location = New System.Drawing.Point(445, 13)
        Me.txtPageIndex.Name = "txtPageIndex"
        Me.txtPageIndex.Size = New System.Drawing.Size(43, 20)
        Me.txtPageIndex.TabIndex = 3
        Me.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSplit
        '
        Me.lblSplit.AutoSize = True
        Me.lblSplit.Location = New System.Drawing.Point(492, 17)
        Me.lblSplit.Name = "lblSplit"
        Me.lblSplit.Size = New System.Drawing.Size(12, 13)
        Me.lblSplit.TabIndex = 5
        Me.lblSplit.Text = "/"
        '
        'btnPageLast
        '
        Me.btnPageLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageLast.Location = New System.Drawing.Point(583, 13)
        Me.btnPageLast.Name = "btnPageLast"
        Me.btnPageLast.Size = New System.Drawing.Size(30, 21)
        Me.btnPageLast.TabIndex = 6
        Me.btnPageLast.Text = ">|"
        Me.btnPageLast.UseVisualStyleBackColor = True
        '
        'btnPageNext
        '
        Me.btnPageNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(552, 13)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(30, 21)
        Me.btnPageNext.TabIndex = 5
        Me.btnPageNext.Text = ">"
        Me.btnPageNext.UseVisualStyleBackColor = True
        '
        'btnPagePrev
        '
        Me.btnPagePrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPagePrev.Location = New System.Drawing.Point(413, 13)
        Me.btnPagePrev.Name = "btnPagePrev"
        Me.btnPagePrev.Size = New System.Drawing.Size(30, 21)
        Me.btnPagePrev.TabIndex = 2
        Me.btnPagePrev.Text = "<"
        Me.btnPagePrev.UseVisualStyleBackColor = True
        '
        'btnPageFirst
        '
        Me.btnPageFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageFirst.Location = New System.Drawing.Point(382, 13)
        Me.btnPageFirst.Name = "btnPageFirst"
        Me.btnPageFirst.Size = New System.Drawing.Size(30, 21)
        Me.btnPageFirst.TabIndex = 1
        Me.btnPageFirst.Text = "|<"
        Me.btnPageFirst.UseVisualStyleBackColor = True
        '
        'cboRowPerPage
        '
        Me.cboRowPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRowPerPage.FormattingEnabled = True
        Me.cboRowPerPage.Items.AddRange(New Object() {"50", "100", "200"})
        Me.cboRowPerPage.Location = New System.Drawing.Point(337, 13)
        Me.cboRowPerPage.Name = "cboRowPerPage"
        Me.cboRowPerPage.Size = New System.Drawing.Size(43, 21)
        Me.cboRowPerPage.TabIndex = 0
        '
        'txtRowCount
        '
        Me.txtRowCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtRowCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRowCount.Location = New System.Drawing.Point(688, 12)
        Me.txtRowCount.Name = "txtRowCount"
        Me.txtRowCount.ReadOnly = True
        Me.txtRowCount.Size = New System.Drawing.Size(41, 20)
        Me.txtRowCount.TabIndex = 7
        Me.txtRowCount.TabStop = False
        Me.txtRowCount.Text = "0"
        Me.txtRowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblrow
        '
        Me.lblrow.AutoSize = True
        Me.lblrow.Location = New System.Drawing.Point(729, 15)
        Me.lblrow.Name = "lblrow"
        Me.lblrow.Size = New System.Drawing.Size(43, 13)
        Me.lblrow.TabIndex = 12
        Me.lblrow.Text = "รายการ"
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.Location = New System.Drawing.Point(617, 15)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(73, 13)
        Me.lbltotal.TabIndex = 10
        Me.lbltotal.Text = "ผลลัพธ์ทั้งหมด"
        '
        'System_Index
        '
        Me.System_Index.HeaderText = "รหัสระบบ "
        Me.System_Index.Name = "System_Index"
        Me.System_Index.ReadOnly = True
        Me.System_Index.Visible = False
        Me.System_Index.Width = 120
        '
        'Document_No
        '
        Me.Document_No.HeaderText = "เลขที่ใบรับสินค้า"
        Me.Document_No.Name = "Document_No"
        Me.Document_No.ReadOnly = True
        Me.Document_No.Width = 120
        '
        'col_Declaration_No
        '
        Me.col_Declaration_No.HeaderText = "เลขที่ใบขนสินค้า"
        Me.col_Declaration_No.Name = "col_Declaration_No"
        Me.col_Declaration_No.ReadOnly = True
        Me.col_Declaration_No.Visible = False
        Me.col_Declaration_No.Width = 120
        '
        'Order_Date
        '
        Me.Order_Date.HeaderText = "วันที่รับสินค้า"
        Me.Order_Date.Name = "Order_Date"
        Me.Order_Date.ReadOnly = True
        Me.Order_Date.Width = 120
        '
        'cl_OrderType
        '
        Me.cl_OrderType.HeaderText = "ประเภทการรับ"
        Me.cl_OrderType.Name = "cl_OrderType"
        Me.cl_OrderType.ReadOnly = True
        '
        'Status
        '
        Me.Status.HeaderText = "สถานะ"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 80
        '
        'Customer_Name
        '
        Me.Customer_Name.HeaderText = "ชื่อลูกค้า"
        Me.Customer_Name.Name = "Customer_Name"
        Me.Customer_Name.ReadOnly = True
        Me.Customer_Name.Width = 150
        '
        'cl_Supplier
        '
        Me.cl_Supplier.HeaderText = "ชื่อผู้ขาย"
        Me.cl_Supplier.Name = "cl_Supplier"
        Me.cl_Supplier.ReadOnly = True
        '
        'cl_Referent
        '
        Me.cl_Referent.HeaderText = "เอกสารอ้างอิง"
        Me.cl_Referent.Name = "cl_Referent"
        Me.cl_Referent.ReadOnly = True
        '
        'Add_by
        '
        Me.Add_by.HeaderText = "ผู้ออกใบรับ"
        Me.Add_by.Name = "Add_by"
        Me.Add_by.ReadOnly = True
        '
        'col_DistributionCenter
        '
        Me.col_DistributionCenter.HeaderText = "ศูนย์กระจาย(สร้าง)"
        Me.col_DistributionCenter.Name = "col_DistributionCenter"
        Me.col_DistributionCenter.ReadOnly = True
        Me.col_DistributionCenter.Width = 180
        '
        'StatusValue
        '
        Me.StatusValue.HeaderText = ""
        Me.StatusValue.Name = "StatusValue"
        Me.StatusValue.ReadOnly = True
        Me.StatusValue.Visible = False
        Me.StatusValue.Width = 10
        '
        'col_Receive_Type
        '
        Me.col_Receive_Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Receive_Type.HeaderText = "ReceiveType"
        Me.col_Receive_Type.Name = "col_Receive_Type"
        Me.col_Receive_Type.ReadOnly = True
        Me.col_Receive_Type.Visible = False
        '
        'frmOrderView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 746)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.grbPageEng)
        Me.Controls.Add(Me.btnAssignJob)
        Me.Controls.Add(Me.btnConfrim_PutAway)
        Me.Controls.Add(Me.btnTag)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnEditOrder)
        Me.Controls.Add(Me.btnStoreIn)
        Me.Controls.Add(Me.btnNewOrder)
        Me.Controls.Add(Me.lbCountRows)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.Controls.Add(Me.grdOrderView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmOrderView"
        Me.ShowIcon = False
        Me.Text = "รายการใบรับสินค้า"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdOrderView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCondition.ResumeLayout(False)
        Me.gbCondition.PerformLayout()
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.pnLeftmenu.ResumeLayout(False)
        Me.gbPrintReport.ResumeLayout(False)
        Me.gbPrintReport.PerformLayout()
        Me.grbPageEng.ResumeLayout(False)
        Me.grbPageEng.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdOrderView As System.Windows.Forms.DataGridView
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOrder_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOrder_Date As System.Windows.Forms.RadioButton
    Friend WithEvents lblFilterStatus As System.Windows.Forms.Label
    Friend WithEvents cboDocumentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rdbSupplier As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDepartment As System.Windows.Forms.RadioButton
    Friend WithEvents txtKeySearch As System.Windows.Forms.TextBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lbCountRows As System.Windows.Forms.Label
    Friend WithEvents btnNewOrder As System.Windows.Forms.Button
    Friend WithEvents btnEditOrder As System.Windows.Forms.Button
    Friend WithEvents btnStoreIn As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rdbReferent As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_Sku As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPo As System.Windows.Forms.RadioButton
    Friend WithEvents lblFilterDocType As System.Windows.Forms.Label
    Friend WithEvents cbReciveType As System.Windows.Forms.ComboBox
    Friend WithEvents gbPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents lblSelectReport As System.Windows.Forms.Label
    Friend WithEvents cbPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btn_Print As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lb_to As System.Windows.Forms.Label
    Friend WithEvents dateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnTag As System.Windows.Forms.Button
    Friend WithEvents btnPop_Search As System.Windows.Forms.Button
    Friend WithEvents rdbDeclar As System.Windows.Forms.RadioButton
    Public WithEvents btnConfrim_PutAway As System.Windows.Forms.Button
    Friend WithEvents btnAssignJob As System.Windows.Forms.Button
    Friend WithEvents grbPageEng As System.Windows.Forms.GroupBox
    Friend WithEvents txtTop As System.Windows.Forms.TextBox
    Friend WithEvents rdRowPage As System.Windows.Forms.RadioButton
    Friend WithEvents rdTop As System.Windows.Forms.RadioButton
    Friend WithEvents lblTopRow As System.Windows.Forms.Label
    Friend WithEvents txtTotalPage As System.Windows.Forms.TextBox
    Friend WithEvents txtPageIndex As System.Windows.Forms.TextBox
    Friend WithEvents lblSplit As System.Windows.Forms.Label
    Friend WithEvents btnPageLast As System.Windows.Forms.Button
    Friend WithEvents btnPageNext As System.Windows.Forms.Button
    Friend WithEvents btnPagePrev As System.Windows.Forms.Button
    Friend WithEvents btnPageFirst As System.Windows.Forms.Button
    Friend WithEvents cboRowPerPage As System.Windows.Forms.ComboBox
    Friend WithEvents txtRowCount As System.Windows.Forms.TextBox
    Friend WithEvents lblrow As System.Windows.Forms.Label
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents rdAll As System.Windows.Forms.RadioButton
    Friend WithEvents System_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Declaration_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Order_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cl_OrderType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cl_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cl_Referent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Add_by As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Receive_Type As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
