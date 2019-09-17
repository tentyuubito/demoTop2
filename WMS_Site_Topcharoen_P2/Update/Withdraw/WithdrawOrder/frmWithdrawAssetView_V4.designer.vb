<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWithdrawAssetView_V4
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWithdrawAssetView_V4))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdWithdrawView = New System.Windows.Forms.DataGridView
        Me.chkselect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.System_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_So = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clWithdrawType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Activity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Customer_Shipping = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clReferrent = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Add_by = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_AssignJob_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UserName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_statusConfirm = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_statusCancel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_FullFill = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuAssignJob = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AssignToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PriorityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VeryHightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NornalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HOLDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.gbPrintReport = New System.Windows.Forms.GroupBox
        Me.btn_Print = New System.Windows.Forms.Button
        Me.lblSelectReport = New System.Windows.Forms.Label
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.gbFilter = New System.Windows.Forms.GroupBox
        Me.lblFilterDocType = New System.Windows.Forms.Label
        Me.cboDocumentStatus = New System.Windows.Forms.ComboBox
        Me.lblFilterStatus = New System.Windows.Forms.Label
        Me.cbWithDrawType = New System.Windows.Forms.ComboBox
        Me.gbCondition = New System.Windows.Forms.GroupBox
        Me.rdbCustomerShipping = New System.Windows.Forms.RadioButton
        Me.btnPop_Search = New System.Windows.Forms.Button
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.lb_to = New System.Windows.Forms.Label
        Me.dateEnd = New System.Windows.Forms.DateTimePicker
        Me.txtKeySearch = New System.Windows.Forms.TextBox
        Me.rdbReferent = New System.Windows.Forms.RadioButton
        Me.rdb_Sku = New System.Windows.Forms.RadioButton
        Me.rdbSO = New System.Windows.Forms.RadioButton
        Me.rdbDepartment = New System.Windows.Forms.RadioButton
        Me.btnSearch = New System.Windows.Forms.Button
        Me.rdbCustomer = New System.Windows.Forms.RadioButton
        Me.rdbWithdraw_No = New System.Windows.Forms.RadioButton
        Me.rdbWithdraw_Date = New System.Windows.Forms.RadioButton
        Me.lbCountRows = New System.Windows.Forms.Label
        Me.lblQCRequest_Index = New System.Windows.Forms.Label
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnEdit_Admin = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnAssignJob = New System.Windows.Forms.Button
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnConfirm_BarCode = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnEditOrder = New System.Windows.Forms.Button
        Me.btnNewWithdraw = New System.Windows.Forms.Button
        Me.btnUnConfirm = New System.Windows.Forms.Button
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
        Me.btnExportExcel = New System.Windows.Forms.Button
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
        Me.btnBarcodeB1 = New System.Windows.Forms.Button
        CType(Me.grdWithdrawView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuAssignJob.SuspendLayout()
        Me.pnLeftmenu.SuspendLayout()
        Me.gbPrintReport.SuspendLayout()
        Me.gbFilter.SuspendLayout()
        Me.gbCondition.SuspendLayout()
        Me.grbPageEng.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdWithdrawView
        '
        Me.grdWithdrawView.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdWithdrawView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdWithdrawView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdWithdrawView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdWithdrawView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdWithdrawView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkselect, Me.System_Index, Me.Document_No, Me.Col_So, Me.Document_Date, Me.clWithdrawType, Me.Status, Me.col_Activity, Me.Customer_Name, Me.Col_Customer_Shipping, Me.clReferrent, Me.Add_by, Me.col_AssignJob_Index, Me.col_UserName, Me.col_statusConfirm, Me.col_statusCancel, Me.col_Status_FullFill, Me.col_DistributionCenter_Desc})
        Me.grdWithdrawView.ContextMenuStrip = Me.mnuAssignJob
        Me.grdWithdrawView.Location = New System.Drawing.Point(192, 58)
        Me.grdWithdrawView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdWithdrawView.MultiSelect = False
        Me.grdWithdrawView.Name = "grdWithdrawView"
        Me.grdWithdrawView.RowHeadersVisible = False
        Me.grdWithdrawView.RowTemplate.Height = 24
        Me.grdWithdrawView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdWithdrawView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdWithdrawView.Size = New System.Drawing.Size(1153, 677)
        Me.grdWithdrawView.TabIndex = 1
        '
        'chkselect
        '
        Me.chkselect.FalseValue = "False"
        Me.chkselect.Frozen = True
        Me.chkselect.HeaderText = "เลือก"
        Me.chkselect.Name = "chkselect"
        Me.chkselect.TrueValue = "True"
        Me.chkselect.Width = 40
        '
        'System_Index
        '
        Me.System_Index.HeaderText = "รหัสระบบ "
        Me.System_Index.Name = "System_Index"
        Me.System_Index.ReadOnly = True
        Me.System_Index.Visible = False
        '
        'Document_No
        '
        Me.Document_No.Frozen = True
        Me.Document_No.HeaderText = "เลขที่ใบเบิกสินค้า"
        Me.Document_No.Name = "Document_No"
        Me.Document_No.ReadOnly = True
        Me.Document_No.Width = 130
        '
        'Col_So
        '
        Me.Col_So.Frozen = True
        Me.Col_So.HeaderText = "เลขที่ใบสั่งขาย"
        Me.Col_So.Name = "Col_So"
        Me.Col_So.ReadOnly = True
        '
        'Document_Date
        '
        Me.Document_Date.Frozen = True
        Me.Document_Date.HeaderText = "วันที่เบิกสินค้า"
        Me.Document_Date.Name = "Document_Date"
        Me.Document_Date.ReadOnly = True
        Me.Document_Date.Width = 120
        '
        'clWithdrawType
        '
        Me.clWithdrawType.Frozen = True
        Me.clWithdrawType.HeaderText = "ประเภทการเบิก"
        Me.clWithdrawType.Name = "clWithdrawType"
        Me.clWithdrawType.ReadOnly = True
        Me.clWithdrawType.Width = 110
        '
        'Status
        '
        Me.Status.Frozen = True
        Me.Status.HeaderText = "สถานะ"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'col_Activity
        '
        Me.col_Activity.DataPropertyName = "Activity"
        Me.col_Activity.HeaderText = "Activity"
        Me.col_Activity.Name = "col_Activity"
        Me.col_Activity.ReadOnly = True
        '
        'Customer_Name
        '
        Me.Customer_Name.HeaderText = "ชื่อลูกค้า "
        Me.Customer_Name.Name = "Customer_Name"
        Me.Customer_Name.ReadOnly = True
        Me.Customer_Name.Width = 150
        '
        'Col_Customer_Shipping
        '
        Me.Col_Customer_Shipping.HeaderText = "ผู้รับสินค้า"
        Me.Col_Customer_Shipping.Name = "Col_Customer_Shipping"
        Me.Col_Customer_Shipping.ReadOnly = True
        '
        'clReferrent
        '
        Me.clReferrent.HeaderText = "เอกสารอ้างอิง"
        Me.clReferrent.Name = "clReferrent"
        Me.clReferrent.ReadOnly = True
        '
        'Add_by
        '
        Me.Add_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Add_by.HeaderText = "ผู้ออกใบเบิก"
        Me.Add_by.Name = "Add_by"
        Me.Add_by.ReadOnly = True
        '
        'col_AssignJob_Index
        '
        Me.col_AssignJob_Index.HeaderText = "AssignJob_Index"
        Me.col_AssignJob_Index.Name = "col_AssignJob_Index"
        Me.col_AssignJob_Index.ReadOnly = True
        Me.col_AssignJob_Index.Visible = False
        '
        'col_UserName
        '
        Me.col_UserName.HeaderText = "ผู้ปฎิบัตงาน"
        Me.col_UserName.Name = "col_UserName"
        Me.col_UserName.ReadOnly = True
        '
        'col_statusConfirm
        '
        DataGridViewCellStyle3.NullValue = """-99"""
        Me.col_statusConfirm.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_statusConfirm.HeaderText = "statusConfirm"
        Me.col_statusConfirm.Name = "col_statusConfirm"
        Me.col_statusConfirm.ReadOnly = True
        Me.col_statusConfirm.Visible = False
        '
        'col_statusCancel
        '
        DataGridViewCellStyle4.NullValue = """-99"""
        Me.col_statusCancel.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_statusCancel.HeaderText = "statusCancel"
        Me.col_statusCancel.Name = "col_statusCancel"
        Me.col_statusCancel.ReadOnly = True
        Me.col_statusCancel.Visible = False
        '
        'col_Status_FullFill
        '
        Me.col_Status_FullFill.HeaderText = "เติมสินค้า"
        Me.col_Status_FullFill.Name = "col_Status_FullFill"
        Me.col_Status_FullFill.ReadOnly = True
        '
        'col_DistributionCenter_Desc
        '
        Me.col_DistributionCenter_Desc.DataPropertyName = "DistributionCenter_Desc"
        Me.col_DistributionCenter_Desc.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_Desc.Name = "col_DistributionCenter_Desc"
        Me.col_DistributionCenter_Desc.ReadOnly = True
        '
        'mnuAssignJob
        '
        Me.mnuAssignJob.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AssignToToolStripMenuItem, Me.PriorityToolStripMenuItem})
        Me.mnuAssignJob.Name = "ContextMenuStrip1"
        Me.mnuAssignJob.Size = New System.Drawing.Size(172, 52)
        '
        'AssignToToolStripMenuItem
        '
        Me.AssignToToolStripMenuItem.Name = "AssignToToolStripMenuItem"
        Me.AssignToToolStripMenuItem.Size = New System.Drawing.Size(171, 24)
        Me.AssignToToolStripMenuItem.Text = "มอบหมายให้"
        '
        'PriorityToolStripMenuItem
        '
        Me.PriorityToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VeryHightToolStripMenuItem, Me.HightToolStripMenuItem, Me.NornalToolStripMenuItem, Me.LowToolStripMenuItem, Me.HOLDToolStripMenuItem})
        Me.PriorityToolStripMenuItem.Name = "PriorityToolStripMenuItem"
        Me.PriorityToolStripMenuItem.Size = New System.Drawing.Size(171, 24)
        Me.PriorityToolStripMenuItem.Text = "ระดับความสำดับ"
        '
        'VeryHightToolStripMenuItem
        '
        Me.VeryHightToolStripMenuItem.Name = "VeryHightToolStripMenuItem"
        Me.VeryHightToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.VeryHightToolStripMenuItem.Text = "เร่งด่วนมาก"
        '
        'HightToolStripMenuItem
        '
        Me.HightToolStripMenuItem.Name = "HightToolStripMenuItem"
        Me.HightToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.HightToolStripMenuItem.Text = "เร่งด่วน"
        '
        'NornalToolStripMenuItem
        '
        Me.NornalToolStripMenuItem.Name = "NornalToolStripMenuItem"
        Me.NornalToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.NornalToolStripMenuItem.Text = "ปกติ"
        '
        'LowToolStripMenuItem
        '
        Me.LowToolStripMenuItem.Name = "LowToolStripMenuItem"
        Me.LowToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.LowToolStripMenuItem.Text = "ไม่เร่งด่วน"
        '
        'HOLDToolStripMenuItem
        '
        Me.HOLDToolStripMenuItem.Name = "HOLDToolStripMenuItem"
        Me.HOLDToolStripMenuItem.Size = New System.Drawing.Size(143, 24)
        Me.HOLDToolStripMenuItem.Text = "ระงับ"
        '
        'pnLeftmenu
        '
        Me.pnLeftmenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnLeftmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnLeftmenu.Controls.Add(Me.gbPrintReport)
        Me.pnLeftmenu.Controls.Add(Me.gbFilter)
        Me.pnLeftmenu.Controls.Add(Me.gbCondition)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 9)
        Me.pnLeftmenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(191, 729)
        Me.pnLeftmenu.TabIndex = 0
        '
        'gbPrintReport
        '
        Me.gbPrintReport.Controls.Add(Me.btn_Print)
        Me.gbPrintReport.Controls.Add(Me.lblSelectReport)
        Me.gbPrintReport.Controls.Add(Me.cboPrint)
        Me.gbPrintReport.Location = New System.Drawing.Point(9, 574)
        Me.gbPrintReport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbPrintReport.Name = "gbPrintReport"
        Me.gbPrintReport.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbPrintReport.Size = New System.Drawing.Size(175, 151)
        Me.gbPrintReport.TabIndex = 2
        Me.gbPrintReport.TabStop = False
        Me.gbPrintReport.Text = "พิมพ์เอกสาร"
        '
        'btn_Print
        '
        Me.btn_Print.Image = CType(resources.GetObject("btn_Print.Image"), System.Drawing.Image)
        Me.btn_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Print.Location = New System.Drawing.Point(19, 82)
        Me.btn_Print.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(133, 47)
        Me.btn_Print.TabIndex = 15
        Me.btn_Print.Text = "พิมพ์"
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'lblSelectReport
        '
        Me.lblSelectReport.AutoSize = True
        Me.lblSelectReport.Location = New System.Drawing.Point(16, 23)
        Me.lblSelectReport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSelectReport.Name = "lblSelectReport"
        Me.lblSelectReport.Size = New System.Drawing.Size(88, 17)
        Me.lblSelectReport.TabIndex = 0
        Me.lblSelectReport.Text = "ประเภทเอกสาร"
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Location = New System.Drawing.Point(8, 43)
        Me.cboPrint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(157, 24)
        Me.cboPrint.TabIndex = 14
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.lblFilterDocType)
        Me.gbFilter.Controls.Add(Me.cboDocumentStatus)
        Me.gbFilter.Controls.Add(Me.lblFilterStatus)
        Me.gbFilter.Controls.Add(Me.cbWithDrawType)
        Me.gbFilter.Location = New System.Drawing.Point(9, 420)
        Me.gbFilter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbFilter.Size = New System.Drawing.Size(175, 146)
        Me.gbFilter.TabIndex = 1
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "กรองรายการ"
        '
        'lblFilterDocType
        '
        Me.lblFilterDocType.AutoSize = True
        Me.lblFilterDocType.Location = New System.Drawing.Point(16, 73)
        Me.lblFilterDocType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterDocType.Name = "lblFilterDocType"
        Me.lblFilterDocType.Size = New System.Drawing.Size(90, 17)
        Me.lblFilterDocType.TabIndex = 2
        Me.lblFilterDocType.Text = "ประเภทการเบิก"
        '
        'cboDocumentStatus
        '
        Me.cboDocumentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentStatus.FormattingEnabled = True
        Me.cboDocumentStatus.Location = New System.Drawing.Point(8, 43)
        Me.cboDocumentStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboDocumentStatus.Name = "cboDocumentStatus"
        Me.cboDocumentStatus.Size = New System.Drawing.Size(157, 24)
        Me.cboDocumentStatus.TabIndex = 12
        '
        'lblFilterStatus
        '
        Me.lblFilterStatus.AutoSize = True
        Me.lblFilterStatus.Location = New System.Drawing.Point(16, 23)
        Me.lblFilterStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilterStatus.Name = "lblFilterStatus"
        Me.lblFilterStatus.Size = New System.Drawing.Size(83, 17)
        Me.lblFilterStatus.TabIndex = 0
        Me.lblFilterStatus.Text = "สถานะเอกสาร"
        '
        'cbWithDrawType
        '
        Me.cbWithDrawType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWithDrawType.FormattingEnabled = True
        Me.cbWithDrawType.Location = New System.Drawing.Point(8, 92)
        Me.cbWithDrawType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbWithDrawType.Name = "cbWithDrawType"
        Me.cbWithDrawType.Size = New System.Drawing.Size(157, 24)
        Me.cbWithDrawType.TabIndex = 13
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.rdbCustomerShipping)
        Me.gbCondition.Controls.Add(Me.btnPop_Search)
        Me.gbCondition.Controls.Add(Me.dtpDate)
        Me.gbCondition.Controls.Add(Me.lb_to)
        Me.gbCondition.Controls.Add(Me.dateEnd)
        Me.gbCondition.Controls.Add(Me.txtKeySearch)
        Me.gbCondition.Controls.Add(Me.rdbReferent)
        Me.gbCondition.Controls.Add(Me.rdb_Sku)
        Me.gbCondition.Controls.Add(Me.rdbSO)
        Me.gbCondition.Controls.Add(Me.rdbDepartment)
        Me.gbCondition.Controls.Add(Me.btnSearch)
        Me.gbCondition.Controls.Add(Me.rdbCustomer)
        Me.gbCondition.Controls.Add(Me.rdbWithdraw_No)
        Me.gbCondition.Controls.Add(Me.rdbWithdraw_Date)
        Me.gbCondition.Location = New System.Drawing.Point(9, 2)
        Me.gbCondition.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbCondition.Size = New System.Drawing.Size(175, 410)
        Me.gbCondition.TabIndex = 0
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "เงื่อนไข"
        '
        'rdbCustomerShipping
        '
        Me.rdbCustomerShipping.AutoSize = True
        Me.rdbCustomerShipping.Location = New System.Drawing.Point(16, 106)
        Me.rdbCustomerShipping.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbCustomerShipping.Name = "rdbCustomerShipping"
        Me.rdbCustomerShipping.Size = New System.Drawing.Size(67, 21)
        Me.rdbCustomerShipping.TabIndex = 3
        Me.rdbCustomerShipping.Text = "ชื่อผู้รับ"
        Me.rdbCustomerShipping.UseVisualStyleBackColor = True
        '
        'btnPop_Search
        '
        Me.btnPop_Search.Location = New System.Drawing.Point(13, 235)
        Me.btnPop_Search.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPop_Search.Name = "btnPop_Search"
        Me.btnPop_Search.Size = New System.Drawing.Size(32, 28)
        Me.btnPop_Search.TabIndex = 8
        Me.btnPop_Search.Text = "..."
        Me.btnPop_Search.UseVisualStyleBackColor = True
        Me.btnPop_Search.Visible = False
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(13, 270)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(157, 22)
        Me.dtpDate.TabIndex = 9
        '
        'lb_to
        '
        Me.lb_to.AutoSize = True
        Me.lb_to.Location = New System.Drawing.Point(61, 297)
        Me.lb_to.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_to.Name = "lb_to"
        Me.lb_to.Size = New System.Drawing.Size(22, 17)
        Me.lb_to.TabIndex = 9
        Me.lb_to.Text = "ถึง"
        '
        'dateEnd
        '
        Me.dateEnd.CustomFormat = "dd/MM/yyyy"
        Me.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateEnd.Location = New System.Drawing.Point(13, 316)
        Me.dateEnd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dateEnd.Name = "dateEnd"
        Me.dateEnd.Size = New System.Drawing.Size(157, 22)
        Me.dateEnd.TabIndex = 10
        '
        'txtKeySearch
        '
        Me.txtKeySearch.Location = New System.Drawing.Point(13, 270)
        Me.txtKeySearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtKeySearch.Name = "txtKeySearch"
        Me.txtKeySearch.Size = New System.Drawing.Size(132, 22)
        Me.txtKeySearch.TabIndex = 10
        '
        'rdbReferent
        '
        Me.rdbReferent.AutoSize = True
        Me.rdbReferent.Location = New System.Drawing.Point(16, 155)
        Me.rdbReferent.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbReferent.Name = "rdbReferent"
        Me.rdbReferent.Size = New System.Drawing.Size(127, 21)
        Me.rdbReferent.TabIndex = 5
        Me.rdbReferent.Text = "เลขที่เอกสารอ้างอิง"
        Me.rdbReferent.UseVisualStyleBackColor = True
        '
        'rdb_Sku
        '
        Me.rdb_Sku.AutoSize = True
        Me.rdb_Sku.Location = New System.Drawing.Point(16, 212)
        Me.rdb_Sku.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdb_Sku.Name = "rdb_Sku"
        Me.rdb_Sku.Size = New System.Drawing.Size(84, 21)
        Me.rdb_Sku.TabIndex = 7
        Me.rdb_Sku.Text = "รหัส SKU"
        Me.rdb_Sku.UseVisualStyleBackColor = True
        '
        'rdbSO
        '
        Me.rdbSO.AutoSize = True
        Me.rdbSO.BackColor = System.Drawing.SystemColors.Control
        Me.rdbSO.Location = New System.Drawing.Point(16, 183)
        Me.rdbSO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbSO.Name = "rdbSO"
        Me.rdbSO.Size = New System.Drawing.Size(129, 21)
        Me.rdbSO.TabIndex = 6
        Me.rdbSO.Text = "เลขที่เอกสารตั้งเบิก"
        Me.rdbSO.UseVisualStyleBackColor = False
        '
        'rdbDepartment
        '
        Me.rdbDepartment.AutoSize = True
        Me.rdbDepartment.Location = New System.Drawing.Point(16, 129)
        Me.rdbDepartment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbDepartment.Name = "rdbDepartment"
        Me.rdbDepartment.Size = New System.Drawing.Size(62, 21)
        Me.rdbDepartment.TabIndex = 4
        Me.rdbDepartment.Text = "แผนก"
        Me.rdbDepartment.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(16, 354)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(133, 47)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'rdbCustomer
        '
        Me.rdbCustomer.AutoSize = True
        Me.rdbCustomer.Location = New System.Drawing.Point(16, 81)
        Me.rdbCustomer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbCustomer.Name = "rdbCustomer"
        Me.rdbCustomer.Size = New System.Drawing.Size(73, 21)
        Me.rdbCustomer.TabIndex = 2
        Me.rdbCustomer.Text = "ชื่อลูกค้า"
        Me.rdbCustomer.UseVisualStyleBackColor = True
        '
        'rdbWithdraw_No
        '
        Me.rdbWithdraw_No.AutoSize = True
        Me.rdbWithdraw_No.Location = New System.Drawing.Point(16, 53)
        Me.rdbWithdraw_No.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbWithdraw_No.Name = "rdbWithdraw_No"
        Me.rdbWithdraw_No.Size = New System.Drawing.Size(90, 21)
        Me.rdbWithdraw_No.TabIndex = 1
        Me.rdbWithdraw_No.Text = "เลขที่ใบเบิก"
        Me.rdbWithdraw_No.UseVisualStyleBackColor = True
        '
        'rdbWithdraw_Date
        '
        Me.rdbWithdraw_Date.AutoSize = True
        Me.rdbWithdraw_Date.Checked = True
        Me.rdbWithdraw_Date.Location = New System.Drawing.Point(16, 25)
        Me.rdbWithdraw_Date.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbWithdraw_Date.Name = "rdbWithdraw_Date"
        Me.rdbWithdraw_Date.Size = New System.Drawing.Size(117, 21)
        Me.rdbWithdraw_Date.TabIndex = 0
        Me.rdbWithdraw_Date.TabStop = True
        Me.rdbWithdraw_Date.Text = "วันที่ใบเบิกสินค้า"
        Me.rdbWithdraw_Date.UseVisualStyleBackColor = True
        '
        'lbCountRows
        '
        Me.lbCountRows.AutoSize = True
        Me.lbCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows.Location = New System.Drawing.Point(189, 743)
        Me.lbCountRows.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows.Name = "lbCountRows"
        Me.lbCountRows.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows.TabIndex = 2
        Me.lbCountRows.Text = "ไม่พบรายการ"
        '
        'lblQCRequest_Index
        '
        Me.lblQCRequest_Index.AutoSize = True
        Me.lblQCRequest_Index.Location = New System.Drawing.Point(267, 635)
        Me.lblQCRequest_Index.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQCRequest_Index.Name = "lblQCRequest_Index"
        Me.lblQCRequest_Index.Size = New System.Drawing.Size(136, 17)
        Me.lblQCRequest_Index.TabIndex = 9
        Me.lblQCRequest_Index.Text = "lblQCRequest_Index"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'btnEdit_Admin
        '
        Me.btnEdit_Admin.Enabled = False
        Me.btnEdit_Admin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEdit_Admin.Image = CType(resources.GetObject("btnEdit_Admin.Image"), System.Drawing.Image)
        Me.btnEdit_Admin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit_Admin.Location = New System.Drawing.Point(944, 766)
        Me.btnEdit_Admin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEdit_Admin.Name = "btnEdit_Admin"
        Me.btnEdit_Admin.Size = New System.Drawing.Size(143, 47)
        Me.btnEdit_Admin.TabIndex = 33
        Me.btnEdit_Admin.Text = "แก้ไข(Admin)"
        Me.btnEdit_Admin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEdit_Admin.UseVisualStyleBackColor = True
        Me.btnEdit_Admin.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(1201, 766)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(143, 47)
        Me.btnExit.TabIndex = 21
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAssignJob.Image = CType(resources.GetObject("btnAssignJob.Image"), System.Drawing.Image)
        Me.btnAssignJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAssignJob.Location = New System.Drawing.Point(793, 766)
        Me.btnAssignJob.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(143, 47)
        Me.btnAssignJob.TabIndex = 32
        Me.btnAssignJob.Text = "มอบหมายงาน"
        Me.btnAssignJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAssignJob.UseVisualStyleBackColor = True
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(488, 766)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(147, 47)
        Me.btnConfirm.TabIndex = 18
        Me.btnConfirm.Text = "ยืนยันรายการ"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnConfirm_BarCode
        '
        Me.btnConfirm_BarCode.Enabled = False
        Me.btnConfirm_BarCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm_BarCode.Location = New System.Drawing.Point(944, 766)
        Me.btnConfirm_BarCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnConfirm_BarCode.Name = "btnConfirm_BarCode"
        Me.btnConfirm_BarCode.Size = New System.Drawing.Size(143, 47)
        Me.btnConfirm_BarCode.TabIndex = 20
        Me.btnConfirm_BarCode.Text = "ยืนยัน บาร์โค้ด"
        Me.btnConfirm_BarCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm_BarCode.UseVisualStyleBackColor = True
        Me.btnConfirm_BarCode.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(643, 766)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(143, 47)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "ยกเลิกรายการ"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEditOrder
        '
        Me.btnEditOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEditOrder.Image = CType(resources.GetObject("btnEditOrder.Image"), System.Drawing.Image)
        Me.btnEditOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditOrder.Location = New System.Drawing.Point(337, 766)
        Me.btnEditOrder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEditOrder.Name = "btnEditOrder"
        Me.btnEditOrder.Size = New System.Drawing.Size(143, 47)
        Me.btnEditOrder.TabIndex = 17
        Me.btnEditOrder.Text = "แก้ไขรายการ"
        Me.btnEditOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditOrder.UseVisualStyleBackColor = True
        '
        'btnNewWithdraw
        '
        Me.btnNewWithdraw.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnNewWithdraw.Image = CType(resources.GetObject("btnNewWithdraw.Image"), System.Drawing.Image)
        Me.btnNewWithdraw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewWithdraw.Location = New System.Drawing.Point(187, 766)
        Me.btnNewWithdraw.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnNewWithdraw.Name = "btnNewWithdraw"
        Me.btnNewWithdraw.Size = New System.Drawing.Size(143, 47)
        Me.btnNewWithdraw.TabIndex = 16
        Me.btnNewWithdraw.Text = "เพิ่มรายการ"
        Me.btnNewWithdraw.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewWithdraw.UseVisualStyleBackColor = True
        '
        'btnUnConfirm
        '
        Me.btnUnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnUnConfirm.Image = CType(resources.GetObject("btnUnConfirm.Image"), System.Drawing.Image)
        Me.btnUnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUnConfirm.Location = New System.Drawing.Point(643, 766)
        Me.btnUnConfirm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnUnConfirm.Name = "btnUnConfirm"
        Me.btnUnConfirm.Size = New System.Drawing.Size(143, 47)
        Me.btnUnConfirm.TabIndex = 34
        Me.btnUnConfirm.Text = "ยกเลิกการยืนยัน"
        Me.btnUnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUnConfirm.UseVisualStyleBackColor = True
        '
        'grbPageEng
        '
        Me.grbPageEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.grbPageEng.Location = New System.Drawing.Point(192, 9)
        Me.grbPageEng.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grbPageEng.Name = "grbPageEng"
        Me.grbPageEng.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grbPageEng.Size = New System.Drawing.Size(1153, 49)
        Me.grbPageEng.TabIndex = 226
        Me.grbPageEng.TabStop = False
        Me.grbPageEng.Text = "การแสดงผล"
        '
        'rdAll
        '
        Me.rdAll.AutoSize = True
        Me.rdAll.Location = New System.Drawing.Point(7, 21)
        Me.rdAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(97, 21)
        Me.rdAll.TabIndex = 22
        Me.rdAll.Text = "แสดงทั้งหมด"
        Me.rdAll.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(223, 20)
        Me.txtTop.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(65, 22)
        Me.txtTop.TabIndex = 18
        Me.txtTop.Text = "100"
        Me.txtTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdRowPage
        '
        Me.rdRowPage.AutoSize = True
        Me.rdRowPage.Location = New System.Drawing.Point(327, 20)
        Me.rdRowPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdRowPage.Name = "rdRowPage"
        Me.rdRowPage.Size = New System.Drawing.Size(98, 21)
        Me.rdRowPage.TabIndex = 21
        Me.rdRowPage.Text = "รายการ/หน้า"
        Me.rdRowPage.UseVisualStyleBackColor = True
        '
        'rdTop
        '
        Me.rdTop.AutoSize = True
        Me.rdTop.Checked = True
        Me.rdTop.Location = New System.Drawing.Point(127, 21)
        Me.rdTop.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdTop.Name = "rdTop"
        Me.rdTop.Size = New System.Drawing.Size(86, 21)
        Me.rdTop.TabIndex = 20
        Me.rdTop.TabStop = True
        Me.rdTop.Text = "แสดงสูงสุด"
        Me.rdTop.UseVisualStyleBackColor = True
        '
        'lblTopRow
        '
        Me.lblTopRow.AutoSize = True
        Me.lblTopRow.Location = New System.Drawing.Point(289, 23)
        Me.lblTopRow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTopRow.Name = "lblTopRow"
        Me.lblTopRow.Size = New System.Drawing.Size(31, 17)
        Me.lblTopRow.TabIndex = 19
        Me.lblTopRow.Text = "แถว"
        '
        'txtTotalPage
        '
        Me.txtTotalPage.Location = New System.Drawing.Point(672, 16)
        Me.txtTotalPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTotalPage.Name = "txtTotalPage"
        Me.txtTotalPage.ReadOnly = True
        Me.txtTotalPage.Size = New System.Drawing.Size(57, 22)
        Me.txtTotalPage.TabIndex = 4
        Me.txtTotalPage.TabStop = False
        '
        'txtPageIndex
        '
        Me.txtPageIndex.Location = New System.Drawing.Point(589, 16)
        Me.txtPageIndex.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPageIndex.Name = "txtPageIndex"
        Me.txtPageIndex.Size = New System.Drawing.Size(56, 22)
        Me.txtPageIndex.TabIndex = 3
        Me.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSplit
        '
        Me.lblSplit.AutoSize = True
        Me.lblSplit.Location = New System.Drawing.Point(652, 21)
        Me.lblSplit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSplit.Name = "lblSplit"
        Me.lblSplit.Size = New System.Drawing.Size(12, 17)
        Me.lblSplit.TabIndex = 5
        Me.lblSplit.Text = "/"
        '
        'btnPageLast
        '
        Me.btnPageLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageLast.Location = New System.Drawing.Point(764, 16)
        Me.btnPageLast.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPageLast.Name = "btnPageLast"
        Me.btnPageLast.Size = New System.Drawing.Size(33, 26)
        Me.btnPageLast.TabIndex = 6
        Me.btnPageLast.Text = ">|"
        Me.btnPageLast.UseVisualStyleBackColor = True
        '
        'btnPageNext
        '
        Me.btnPageNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(732, 16)
        Me.btnPageNext.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(33, 26)
        Me.btnPageNext.TabIndex = 5
        Me.btnPageNext.Text = ">"
        Me.btnPageNext.UseVisualStyleBackColor = True
        '
        'btnPagePrev
        '
        Me.btnPagePrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPagePrev.Location = New System.Drawing.Point(553, 16)
        Me.btnPagePrev.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPagePrev.Name = "btnPagePrev"
        Me.btnPagePrev.Size = New System.Drawing.Size(33, 26)
        Me.btnPagePrev.TabIndex = 2
        Me.btnPagePrev.Text = "<"
        Me.btnPagePrev.UseVisualStyleBackColor = True
        '
        'btnPageFirst
        '
        Me.btnPageFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageFirst.Location = New System.Drawing.Point(521, 16)
        Me.btnPageFirst.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPageFirst.Name = "btnPageFirst"
        Me.btnPageFirst.Size = New System.Drawing.Size(33, 26)
        Me.btnPageFirst.TabIndex = 1
        Me.btnPageFirst.Text = "|<"
        Me.btnPageFirst.UseVisualStyleBackColor = True
        '
        'cboRowPerPage
        '
        Me.cboRowPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRowPerPage.FormattingEnabled = True
        Me.cboRowPerPage.Items.AddRange(New Object() {"1", "50", "100", "200"})
        Me.cboRowPerPage.Location = New System.Drawing.Point(445, 16)
        Me.cboRowPerPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboRowPerPage.Name = "cboRowPerPage"
        Me.cboRowPerPage.Size = New System.Drawing.Size(56, 24)
        Me.cboRowPerPage.TabIndex = 0
        '
        'txtRowCount
        '
        Me.txtRowCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtRowCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRowCount.Location = New System.Drawing.Point(920, 15)
        Me.txtRowCount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRowCount.Name = "txtRowCount"
        Me.txtRowCount.ReadOnly = True
        Me.txtRowCount.Size = New System.Drawing.Size(53, 22)
        Me.txtRowCount.TabIndex = 7
        Me.txtRowCount.TabStop = False
        Me.txtRowCount.Text = "0"
        Me.txtRowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblrow
        '
        Me.lblrow.AutoSize = True
        Me.lblrow.Location = New System.Drawing.Point(975, 18)
        Me.lblrow.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblrow.Name = "lblrow"
        Me.lblrow.Size = New System.Drawing.Size(49, 17)
        Me.lblrow.TabIndex = 12
        Me.lblrow.Text = "รายการ"
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.Location = New System.Drawing.Point(823, 18)
        Me.lbltotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(85, 17)
        Me.lbltotal.TabIndex = 10
        Me.lbltotal.Text = "ผลลัพธ์ทั้งหมด"
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Enabled = False
        Me.btnExportExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExportExcel.Image = CType(resources.GetObject("btnExportExcel.Image"), System.Drawing.Image)
        Me.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportExcel.Location = New System.Drawing.Point(944, 766)
        Me.btnExportExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(143, 47)
        Me.btnExportExcel.TabIndex = 227
        Me.btnExportExcel.Text = "Export"
        Me.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportExcel.UseVisualStyleBackColor = True
        Me.btnExportExcel.Visible = False
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.FalseValue = "False"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = "เลือก"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.TrueValue = "True"
        Me.DataGridViewCheckBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่ออกเอกสาร "
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อลูกค้า "
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 110
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Activity"
        Me.DataGridViewTextBoxColumn7.HeaderText = "ชื่อผู้ขาย"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "เอกสารอ้างอิง"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 150
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "ผู้ออกใบเบิก"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "ผู้ออกใบเบิก"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.HeaderText = "AssignJob_Index"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "ชื่อผู้ใช้"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle5.NullValue = """-99"""
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn13.HeaderText = "statusConfirm"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        DataGridViewCellStyle6.NullValue = """-99"""
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn14.HeaderText = "statusCancel"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        DataGridViewCellStyle7.NullValue = """-99"""
        Me.DataGridViewTextBoxColumn15.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn15.HeaderText = "statusCancel"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "เติมสินค้า"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "DistributionCenter_Desc"
        Me.DataGridViewTextBoxColumn17.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'btnBarcodeB1
        '
        Me.btnBarcodeB1.Enabled = False
        Me.btnBarcodeB1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnBarcodeB1.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.btnBarcodeB1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBarcodeB1.Location = New System.Drawing.Point(1095, 766)
        Me.btnBarcodeB1.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBarcodeB1.Name = "btnBarcodeB1"
        Me.btnBarcodeB1.Size = New System.Drawing.Size(143, 47)
        Me.btnBarcodeB1.TabIndex = 228
        Me.btnBarcodeB1.Text = "Barcode B1"
        Me.btnBarcodeB1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBarcodeB1.UseVisualStyleBackColor = True
        Me.btnBarcodeB1.Visible = False
        '
        'frmWithdrawAssetView_V4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1357, 848)
        Me.Controls.Add(Me.btnBarcodeB1)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbPageEng)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnEditOrder)
        Me.Controls.Add(Me.btnEdit_Admin)
        Me.Controls.Add(Me.btnAssignJob)
        Me.Controls.Add(Me.btnNewWithdraw)
        Me.Controls.Add(Me.btnConfirm_BarCode)
        Me.Controls.Add(Me.lbCountRows)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.Controls.Add(Me.grdWithdrawView)
        Me.Controls.Add(Me.lblQCRequest_Index)
        Me.Controls.Add(Me.btnUnConfirm)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmWithdrawAssetView_V4"
        Me.ShowIcon = False
        Me.Text = "รายการใบเบิกสินค้า_V4"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdWithdrawView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuAssignJob.ResumeLayout(False)
        Me.pnLeftmenu.ResumeLayout(False)
        Me.gbPrintReport.ResumeLayout(False)
        Me.gbPrintReport.PerformLayout()
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.gbCondition.ResumeLayout(False)
        Me.gbCondition.PerformLayout()
        Me.grbPageEng.ResumeLayout(False)
        Me.grbPageEng.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdWithdrawView As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rdbCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbWithdraw_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdbWithdraw_Date As System.Windows.Forms.RadioButton
    Friend WithEvents cboDocumentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lbCountRows As System.Windows.Forms.Label
    Friend WithEvents btnNewWithdraw As System.Windows.Forms.Button
    Friend WithEvents btnEditOrder As System.Windows.Forms.Button
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Print As System.Windows.Forms.Button
    Friend WithEvents lblSelectReport As System.Windows.Forms.Label
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents lblFilterDocType As System.Windows.Forms.Label
    Friend WithEvents lblFilterStatus As System.Windows.Forms.Label
    Friend WithEvents cbWithDrawType As System.Windows.Forms.ComboBox
    Friend WithEvents rdbReferent As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_Sku As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSO As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDepartment As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblQCRequest_Index As System.Windows.Forms.Label
    Friend WithEvents btnPop_Search As System.Windows.Forms.Button
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lb_to As System.Windows.Forms.Label
    Friend WithEvents dateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtKeySearch As System.Windows.Forms.TextBox
    Friend WithEvents btnConfirm_BarCode As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuAssignJob As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AssignToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PriorityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VeryHightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NornalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HOLDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rdbCustomerShipping As System.Windows.Forms.RadioButton
    Friend WithEvents btnAssignJob As System.Windows.Forms.Button
    Friend WithEvents btnEdit_Admin As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnUnConfirm As System.Windows.Forms.Button
    Friend WithEvents grbPageEng As System.Windows.Forms.GroupBox
    Friend WithEvents rdAll As System.Windows.Forms.RadioButton
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
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents chkselect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents System_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_So As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clWithdrawType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Activity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Customer_Shipping As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clReferrent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Add_by As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AssignJob_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_statusConfirm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_statusCancel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_FullFill As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBarcodeB1 As System.Windows.Forms.Button
End Class
