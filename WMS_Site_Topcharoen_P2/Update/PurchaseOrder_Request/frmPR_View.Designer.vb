<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPR_View
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPR_View))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.gboPrint = New System.Windows.Forms.GroupBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.lblSelectReport = New System.Windows.Forms.Label
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.gboCondition = New System.Windows.Forms.GroupBox
        Me.pnlCondition1 = New System.Windows.Forms.Panel
        Me.dtpDate_S = New System.Windows.Forms.DateTimePicker
        Me.lblDate_To = New System.Windows.Forms.Label
        Me.dtpDate_E = New System.Windows.Forms.DateTimePicker
        Me.pnlCondition2 = New System.Windows.Forms.Panel
        Me.txtText_Condition = New System.Windows.Forms.TextBox
        Me.radCustomer_Id = New System.Windows.Forms.RadioButton
        Me.radDue_Date = New System.Windows.Forms.RadioButton
        Me.btnSearch = New System.Windows.Forms.Button
        Me.radPurchaseOrder_Request_No = New System.Windows.Forms.RadioButton
        Me.radPurchaseOrder_Request_Date = New System.Windows.Forms.RadioButton
        Me.gboFilter = New System.Windows.Forms.GroupBox
        Me.cboProductType = New System.Windows.Forms.ComboBox
        Me.lblProductType = New System.Windows.Forms.Label
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.cboStatus = New System.Windows.Forms.ComboBox
        Me.dgvPR = New System.Windows.Forms.DataGridView
        Me.col_IsSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_PurchaseOrder_Request_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_Request_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DocumentType_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Weight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Addby = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Remark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Received_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Pending_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_Request_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_IsClosed = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_IsConfirm = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_IsCanCancel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.gboPager = New System.Windows.Forms.GroupBox
        Me.radAll = New System.Windows.Forms.RadioButton
        Me.txtTop = New System.Windows.Forms.TextBox
        Me.radRowPage = New System.Windows.Forms.RadioButton
        Me.radTop = New System.Windows.Forms.RadioButton
        Me.lblTopRow = New System.Windows.Forms.Label
        Me.txtTotalPage = New System.Windows.Forms.TextBox
        Me.txtPage = New System.Windows.Forms.TextBox
        Me.lblSplit = New System.Windows.Forms.Label
        Me.btnPageLast = New System.Windows.Forms.Button
        Me.btnPageNext = New System.Windows.Forms.Button
        Me.btnPagePrev = New System.Windows.Forms.Button
        Me.btnPageFirst = New System.Windows.Forms.Button
        Me.cboRowPerPage = New System.Windows.Forms.ComboBox
        Me.txtRowsCount = New System.Windows.Forms.TextBox
        Me.lblRowsCount = New System.Windows.Forms.Label
        Me.lblTotalRows = New System.Windows.Forms.Label
        Me.pnlMainButton = New System.Windows.Forms.Panel
        Me.btnAutoPR = New System.Windows.Forms.Button
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
        Me.pnLeftmenu.SuspendLayout()
        Me.gboPrint.SuspendLayout()
        Me.gboCondition.SuspendLayout()
        Me.pnlCondition1.SuspendLayout()
        Me.pnlCondition2.SuspendLayout()
        Me.gboFilter.SuspendLayout()
        CType(Me.dgvPR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboPager.SuspendLayout()
        Me.pnlMainButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnLeftmenu
        '
        Me.pnLeftmenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnLeftmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnLeftmenu.Controls.Add(Me.gboPrint)
        Me.pnLeftmenu.Controls.Add(Me.gboCondition)
        Me.pnLeftmenu.Controls.Add(Me.gboFilter)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 3)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(143, 516)
        Me.pnLeftmenu.TabIndex = 1
        '
        'gboPrint
        '
        Me.gboPrint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboPrint.Controls.Add(Me.btnPrint)
        Me.gboPrint.Controls.Add(Me.lblSelectReport)
        Me.gboPrint.Controls.Add(Me.cboPrint)
        Me.gboPrint.Location = New System.Drawing.Point(3, 368)
        Me.gboPrint.Name = "gboPrint"
        Me.gboPrint.Size = New System.Drawing.Size(135, 103)
        Me.gboPrint.TabIndex = 3
        Me.gboPrint.TabStop = False
        Me.gboPrint.Text = "พิมพ์เอกสาร"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(27, 59)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(80, 38)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'lblSelectReport
        '
        Me.lblSelectReport.AutoSize = True
        Me.lblSelectReport.Location = New System.Drawing.Point(6, 16)
        Me.lblSelectReport.Name = "lblSelectReport"
        Me.lblSelectReport.Size = New System.Drawing.Size(79, 13)
        Me.lblSelectReport.TabIndex = 0
        Me.lblSelectReport.Text = "ประเภทเอกสาร"
        '
        'cboPrint
        '
        Me.cboPrint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Location = New System.Drawing.Point(3, 32)
        Me.cboPrint.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(129, 21)
        Me.cboPrint.TabIndex = 9
        '
        'gboCondition
        '
        Me.gboCondition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboCondition.Controls.Add(Me.pnlCondition1)
        Me.gboCondition.Controls.Add(Me.pnlCondition2)
        Me.gboCondition.Controls.Add(Me.radCustomer_Id)
        Me.gboCondition.Controls.Add(Me.radDue_Date)
        Me.gboCondition.Controls.Add(Me.btnSearch)
        Me.gboCondition.Controls.Add(Me.radPurchaseOrder_Request_No)
        Me.gboCondition.Controls.Add(Me.radPurchaseOrder_Request_Date)
        Me.gboCondition.Location = New System.Drawing.Point(3, 3)
        Me.gboCondition.Name = "gboCondition"
        Me.gboCondition.Size = New System.Drawing.Size(135, 214)
        Me.gboCondition.TabIndex = 1
        Me.gboCondition.TabStop = False
        Me.gboCondition.Text = "เงื่อนไข"
        '
        'pnlCondition1
        '
        Me.pnlCondition1.Controls.Add(Me.dtpDate_S)
        Me.pnlCondition1.Controls.Add(Me.lblDate_To)
        Me.pnlCondition1.Controls.Add(Me.dtpDate_E)
        Me.pnlCondition1.Location = New System.Drawing.Point(3, 99)
        Me.pnlCondition1.Name = "pnlCondition1"
        Me.pnlCondition1.Size = New System.Drawing.Size(129, 65)
        Me.pnlCondition1.TabIndex = 5
        '
        'dtpDate_S
        '
        Me.dtpDate_S.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpDate_S.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate_S.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate_S.Location = New System.Drawing.Point(14, 3)
        Me.dtpDate_S.Name = "dtpDate_S"
        Me.dtpDate_S.Size = New System.Drawing.Size(100, 20)
        Me.dtpDate_S.TabIndex = 1
        '
        'lblDate_To
        '
        Me.lblDate_To.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblDate_To.AutoSize = True
        Me.lblDate_To.Location = New System.Drawing.Point(55, 26)
        Me.lblDate_To.Name = "lblDate_To"
        Me.lblDate_To.Size = New System.Drawing.Size(19, 13)
        Me.lblDate_To.TabIndex = 2
        Me.lblDate_To.Text = "ถึง"
        '
        'dtpDate_E
        '
        Me.dtpDate_E.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpDate_E.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate_E.Location = New System.Drawing.Point(14, 42)
        Me.dtpDate_E.Name = "dtpDate_E"
        Me.dtpDate_E.Size = New System.Drawing.Size(100, 20)
        Me.dtpDate_E.TabIndex = 3
        '
        'pnlCondition2
        '
        Me.pnlCondition2.Controls.Add(Me.txtText_Condition)
        Me.pnlCondition2.Location = New System.Drawing.Point(3, 99)
        Me.pnlCondition2.Name = "pnlCondition2"
        Me.pnlCondition2.Size = New System.Drawing.Size(129, 65)
        Me.pnlCondition2.TabIndex = 6
        Me.pnlCondition2.Visible = False
        '
        'txtText_Condition
        '
        Me.txtText_Condition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtText_Condition.Location = New System.Drawing.Point(0, 22)
        Me.txtText_Condition.Name = "txtText_Condition"
        Me.txtText_Condition.Size = New System.Drawing.Size(129, 20)
        Me.txtText_Condition.TabIndex = 1
        '
        'radCustomer_Id
        '
        Me.radCustomer_Id.AutoSize = True
        Me.radCustomer_Id.Location = New System.Drawing.Point(6, 79)
        Me.radCustomer_Id.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.radCustomer_Id.Name = "radCustomer_Id"
        Me.radCustomer_Id.Size = New System.Drawing.Size(69, 17)
        Me.radCustomer_Id.TabIndex = 4
        Me.radCustomer_Id.Text = "รหัสลูกค้า"
        Me.radCustomer_Id.UseVisualStyleBackColor = True
        '
        'radDue_Date
        '
        Me.radDue_Date.AutoSize = True
        Me.radDue_Date.Location = New System.Drawing.Point(6, 59)
        Me.radDue_Date.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.radDue_Date.Name = "radDue_Date"
        Me.radDue_Date.Size = New System.Drawing.Size(93, 17)
        Me.radDue_Date.TabIndex = 3
        Me.radDue_Date.Text = "วันที่กำหนดรับ"
        Me.radDue_Date.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(27, 170)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(80, 38)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'radPurchaseOrder_Request_No
        '
        Me.radPurchaseOrder_Request_No.AutoSize = True
        Me.radPurchaseOrder_Request_No.Location = New System.Drawing.Point(6, 39)
        Me.radPurchaseOrder_Request_No.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.radPurchaseOrder_Request_No.Name = "radPurchaseOrder_Request_No"
        Me.radPurchaseOrder_Request_No.Size = New System.Drawing.Size(110, 17)
        Me.radPurchaseOrder_Request_No.TabIndex = 2
        Me.radPurchaseOrder_Request_No.Text = "เลขที่ใบคำขอสั่งชื้อ"
        Me.radPurchaseOrder_Request_No.UseVisualStyleBackColor = True
        '
        'radPurchaseOrder_Request_Date
        '
        Me.radPurchaseOrder_Request_Date.AutoSize = True
        Me.radPurchaseOrder_Request_Date.Checked = True
        Me.radPurchaseOrder_Request_Date.Location = New System.Drawing.Point(6, 19)
        Me.radPurchaseOrder_Request_Date.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.radPurchaseOrder_Request_Date.Name = "radPurchaseOrder_Request_Date"
        Me.radPurchaseOrder_Request_Date.Size = New System.Drawing.Size(107, 17)
        Me.radPurchaseOrder_Request_Date.TabIndex = 1
        Me.radPurchaseOrder_Request_Date.TabStop = True
        Me.radPurchaseOrder_Request_Date.Text = "วันที่ใบคำขอสั่งชื้อ"
        Me.radPurchaseOrder_Request_Date.UseVisualStyleBackColor = True
        '
        'gboFilter
        '
        Me.gboFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboFilter.Controls.Add(Me.cboProductType)
        Me.gboFilter.Controls.Add(Me.lblProductType)
        Me.gboFilter.Controls.Add(Me.lblDocumentType)
        Me.gboFilter.Controls.Add(Me.cboDocumentType)
        Me.gboFilter.Controls.Add(Me.lblStatus)
        Me.gboFilter.Controls.Add(Me.cboStatus)
        Me.gboFilter.Location = New System.Drawing.Point(3, 223)
        Me.gboFilter.Name = "gboFilter"
        Me.gboFilter.Size = New System.Drawing.Size(135, 139)
        Me.gboFilter.TabIndex = 2
        Me.gboFilter.TabStop = False
        Me.gboFilter.Text = "กรองรายการ"
        '
        'cboProductType
        '
        Me.cboProductType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProductType.FormattingEnabled = True
        Me.cboProductType.Location = New System.Drawing.Point(3, 112)
        Me.cboProductType.Name = "cboProductType"
        Me.cboProductType.Size = New System.Drawing.Size(129, 21)
        Me.cboProductType.TabIndex = 19
        '
        'lblProductType
        '
        Me.lblProductType.AutoSize = True
        Me.lblProductType.Location = New System.Drawing.Point(6, 96)
        Me.lblProductType.Name = "lblProductType"
        Me.lblProductType.Size = New System.Drawing.Size(70, 13)
        Me.lblProductType.TabIndex = 18
        Me.lblProductType.Text = "ประเภทสินค้า"
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.Location = New System.Drawing.Point(6, 56)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(105, 13)
        Me.lblDocumentType.TabIndex = 16
        Me.lblDocumentType.Text = "ประเภทใบคำขอสั่งซื้อ"
        '
        'cboDocumentType
        '
        Me.cboDocumentType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(3, 72)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(129, 21)
        Me.cboDocumentType.TabIndex = 17
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(6, 16)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "สถานะเอกสาร"
        '
        'cboStatus
        '
        Me.cboStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(3, 32)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(129, 21)
        Me.cboStatus.TabIndex = 8
        '
        'dgvPR
        '
        Me.dgvPR.AllowUserToAddRows = False
        Me.dgvPR.AllowUserToDeleteRows = False
        Me.dgvPR.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvPR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPR.BackgroundColor = System.Drawing.Color.White
        Me.dgvPR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_IsSelected, Me.col_PurchaseOrder_Request_No, Me.col_PurchaseOrder_Request_Date, Me.col_DocumentType_Desc, Me.col_Status_Desc, Me.col_Total_Qty, Me.Col_Weight, Me.Col_Addby, Me.Col_Remark, Me.Col_Total_Received_Qty, Me.Col_Total_Pending_Qty, Me.col_PurchaseOrder_Request_Index, Me.col_Status, Me.col_IsClosed, Me.col_IsConfirm, Me.col_IsCanCancel})
        Me.dgvPR.Location = New System.Drawing.Point(144, 51)
        Me.dgvPR.Name = "dgvPR"
        Me.dgvPR.RowHeadersVisible = False
        Me.dgvPR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPR.Size = New System.Drawing.Size(866, 639)
        Me.dgvPR.TabIndex = 3
        '
        'col_IsSelected
        '
        Me.col_IsSelected.DataPropertyName = "IsSelected"
        Me.col_IsSelected.FalseValue = "False"
        Me.col_IsSelected.FillWeight = 25.0!
        Me.col_IsSelected.Frozen = True
        Me.col_IsSelected.HeaderText = ""
        Me.col_IsSelected.MinimumWidth = 25
        Me.col_IsSelected.Name = "col_IsSelected"
        Me.col_IsSelected.TrueValue = "True"
        Me.col_IsSelected.Width = 25
        '
        'col_PurchaseOrder_Request_No
        '
        Me.col_PurchaseOrder_Request_No.DataPropertyName = "PurchaseOrder_Request_No"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_PurchaseOrder_Request_No.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_PurchaseOrder_Request_No.Frozen = True
        Me.col_PurchaseOrder_Request_No.HeaderText = "เลขที่เอกสาร"
        Me.col_PurchaseOrder_Request_No.Name = "col_PurchaseOrder_Request_No"
        Me.col_PurchaseOrder_Request_No.ReadOnly = True
        Me.col_PurchaseOrder_Request_No.Width = 120
        '
        'col_PurchaseOrder_Request_Date
        '
        Me.col_PurchaseOrder_Request_Date.DataPropertyName = "PurchaseOrder_Request_Date"
        DataGridViewCellStyle3.Format = "dd/MM/yyyyy"
        Me.col_PurchaseOrder_Request_Date.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_PurchaseOrder_Request_Date.HeaderText = "วันที่เอกสาร"
        Me.col_PurchaseOrder_Request_Date.Name = "col_PurchaseOrder_Request_Date"
        Me.col_PurchaseOrder_Request_Date.ReadOnly = True
        '
        'col_DocumentType_Desc
        '
        Me.col_DocumentType_Desc.DataPropertyName = "DocumentType_Desc"
        Me.col_DocumentType_Desc.HeaderText = "ประเภทเอกสาร"
        Me.col_DocumentType_Desc.Name = "col_DocumentType_Desc"
        Me.col_DocumentType_Desc.ReadOnly = True
        Me.col_DocumentType_Desc.Width = 120
        '
        'col_Status_Desc
        '
        Me.col_Status_Desc.DataPropertyName = "Status_Desc"
        Me.col_Status_Desc.HeaderText = "สถานะ"
        Me.col_Status_Desc.Name = "col_Status_Desc"
        Me.col_Status_Desc.ReadOnly = True
        Me.col_Status_Desc.Width = 85
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle4.Format = "#,##0.######"
        DataGridViewCellStyle4.NullValue = "0"
        Me.col_Total_Qty.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Total_Qty.HeaderText = "จำนวน(ย่อย)"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        Me.col_Total_Qty.Width = 110
        '
        'Col_Weight
        '
        Me.Col_Weight.DataPropertyName = "Weight"
        DataGridViewCellStyle5.Format = "N4"
        Me.Col_Weight.DefaultCellStyle = DataGridViewCellStyle5
        Me.Col_Weight.HeaderText = "น้ำหนัก"
        Me.Col_Weight.Name = "Col_Weight"
        Me.Col_Weight.ReadOnly = True
        '
        'Col_Addby
        '
        Me.Col_Addby.DataPropertyName = "add_by"
        Me.Col_Addby.HeaderText = "ผู้ทำรายการ"
        Me.Col_Addby.Name = "Col_Addby"
        Me.Col_Addby.ReadOnly = True
        '
        'Col_Remark
        '
        Me.Col_Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Remark.DataPropertyName = "Remark"
        Me.Col_Remark.HeaderText = "หมายเหตุ"
        Me.Col_Remark.MinimumWidth = 100
        Me.Col_Remark.Name = "Col_Remark"
        Me.Col_Remark.ReadOnly = True
        '
        'Col_Total_Received_Qty
        '
        Me.Col_Total_Received_Qty.DataPropertyName = "Total_Received_Qty"
        DataGridViewCellStyle6.Format = "#,##0.######"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Col_Total_Received_Qty.DefaultCellStyle = DataGridViewCellStyle6
        Me.Col_Total_Received_Qty.HeaderText = "รับแล้ว(ย่อย)"
        Me.Col_Total_Received_Qty.Name = "Col_Total_Received_Qty"
        Me.Col_Total_Received_Qty.ReadOnly = True
        Me.Col_Total_Received_Qty.Width = 120
        '
        'Col_Total_Pending_Qty
        '
        Me.Col_Total_Pending_Qty.DataPropertyName = "Total_Pending_Qty"
        DataGridViewCellStyle7.Format = "#,##0.######"
        DataGridViewCellStyle7.NullValue = "0"
        Me.Col_Total_Pending_Qty.DefaultCellStyle = DataGridViewCellStyle7
        Me.Col_Total_Pending_Qty.HeaderText = "ค้างรับ(ย่อย)"
        Me.Col_Total_Pending_Qty.Name = "Col_Total_Pending_Qty"
        Me.Col_Total_Pending_Qty.ReadOnly = True
        Me.Col_Total_Pending_Qty.Width = 120
        '
        'col_PurchaseOrder_Request_Index
        '
        Me.col_PurchaseOrder_Request_Index.DataPropertyName = "PurchaseOrder_Request_Index"
        Me.col_PurchaseOrder_Request_Index.HeaderText = "PurchaseOrder_Request_Index"
        Me.col_PurchaseOrder_Request_Index.Name = "col_PurchaseOrder_Request_Index"
        Me.col_PurchaseOrder_Request_Index.ReadOnly = True
        Me.col_PurchaseOrder_Request_Index.Visible = False
        '
        'col_Status
        '
        Me.col_Status.DataPropertyName = "Status"
        Me.col_Status.HeaderText = "Status"
        Me.col_Status.Name = "col_Status"
        Me.col_Status.ReadOnly = True
        Me.col_Status.Visible = False
        '
        'col_IsClosed
        '
        Me.col_IsClosed.DataPropertyName = "IsClosed"
        Me.col_IsClosed.HeaderText = "IsClosed"
        Me.col_IsClosed.Name = "col_IsClosed"
        Me.col_IsClosed.ReadOnly = True
        Me.col_IsClosed.Visible = False
        '
        'col_IsConfirm
        '
        Me.col_IsConfirm.DataPropertyName = "IsConfirm"
        Me.col_IsConfirm.HeaderText = "IsConfirm"
        Me.col_IsConfirm.Name = "col_IsConfirm"
        Me.col_IsConfirm.ReadOnly = True
        Me.col_IsConfirm.Visible = False
        '
        'col_IsCanCancel
        '
        Me.col_IsCanCancel.DataPropertyName = "IsCanCancel"
        Me.col_IsCanCancel.HeaderText = "IsCanCancel"
        Me.col_IsCanCancel.Name = "col_IsCanCancel"
        Me.col_IsCanCancel.ReadOnly = True
        Me.col_IsCanCancel.Visible = False
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(333, 0)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(105, 38)
        Me.btnConfirm.TabIndex = 4
        Me.btnConfirm.Text = "ยืนยันรายการ"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(222, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 38)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "ยกเลิกรายการ"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(786, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 38)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "ออก"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(111, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(105, 38)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "แก้ไขรายการ"
        Me.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(0, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(105, 38)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "เพิ่มรายการ"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'gboPager
        '
        Me.gboPager.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboPager.Controls.Add(Me.radAll)
        Me.gboPager.Controls.Add(Me.txtTop)
        Me.gboPager.Controls.Add(Me.radRowPage)
        Me.gboPager.Controls.Add(Me.radTop)
        Me.gboPager.Controls.Add(Me.lblTopRow)
        Me.gboPager.Controls.Add(Me.txtTotalPage)
        Me.gboPager.Controls.Add(Me.txtPage)
        Me.gboPager.Controls.Add(Me.lblSplit)
        Me.gboPager.Controls.Add(Me.btnPageLast)
        Me.gboPager.Controls.Add(Me.btnPageNext)
        Me.gboPager.Controls.Add(Me.btnPagePrev)
        Me.gboPager.Controls.Add(Me.btnPageFirst)
        Me.gboPager.Controls.Add(Me.cboRowPerPage)
        Me.gboPager.Controls.Add(Me.txtRowsCount)
        Me.gboPager.Controls.Add(Me.lblRowsCount)
        Me.gboPager.Controls.Add(Me.lblTotalRows)
        Me.gboPager.Location = New System.Drawing.Point(144, 3)
        Me.gboPager.Name = "gboPager"
        Me.gboPager.Size = New System.Drawing.Size(866, 41)
        Me.gboPager.TabIndex = 2
        Me.gboPager.TabStop = False
        Me.gboPager.Text = "การแสดงผล"
        '
        'radAll
        '
        Me.radAll.AutoSize = True
        Me.radAll.Location = New System.Drawing.Point(19, 16)
        Me.radAll.Name = "radAll"
        Me.radAll.Size = New System.Drawing.Size(84, 17)
        Me.radAll.TabIndex = 1
        Me.radAll.Text = "แสดงทั้งหมด"
        Me.radAll.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(184, 14)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(50, 20)
        Me.txtTop.TabIndex = 4
        Me.txtTop.Text = "100"
        Me.txtTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'radRowPage
        '
        Me.radRowPage.AutoSize = True
        Me.radRowPage.Location = New System.Drawing.Point(262, 16)
        Me.radRowPage.Name = "radRowPage"
        Me.radRowPage.Size = New System.Drawing.Size(86, 17)
        Me.radRowPage.TabIndex = 3
        Me.radRowPage.Text = "รายการ/หน้า"
        Me.radRowPage.UseVisualStyleBackColor = True
        '
        'radTop
        '
        Me.radTop.AutoSize = True
        Me.radTop.Checked = True
        Me.radTop.Location = New System.Drawing.Point(112, 16)
        Me.radTop.Name = "radTop"
        Me.radTop.Size = New System.Drawing.Size(75, 17)
        Me.radTop.TabIndex = 2
        Me.radTop.TabStop = True
        Me.radTop.Text = "แสดงสูงสุด"
        Me.radTop.UseVisualStyleBackColor = True
        '
        'lblTopRow
        '
        Me.lblTopRow.AutoSize = True
        Me.lblTopRow.Location = New System.Drawing.Point(234, 18)
        Me.lblTopRow.Name = "lblTopRow"
        Me.lblTopRow.Size = New System.Drawing.Size(28, 13)
        Me.lblTopRow.TabIndex = 5
        Me.lblTopRow.Text = "แถว"
        '
        'txtTotalPage
        '
        Me.txtTotalPage.Location = New System.Drawing.Point(526, 14)
        Me.txtTotalPage.Name = "txtTotalPage"
        Me.txtTotalPage.ReadOnly = True
        Me.txtTotalPage.Size = New System.Drawing.Size(44, 20)
        Me.txtTotalPage.TabIndex = 11
        Me.txtTotalPage.TabStop = False
        Me.txtTotalPage.Text = "0"
        '
        'txtPage
        '
        Me.txtPage.Location = New System.Drawing.Point(459, 14)
        Me.txtPage.Name = "txtPage"
        Me.txtPage.Size = New System.Drawing.Size(43, 20)
        Me.txtPage.TabIndex = 9
        Me.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSplit
        '
        Me.lblSplit.AutoSize = True
        Me.lblSplit.Location = New System.Drawing.Point(508, 18)
        Me.lblSplit.Name = "lblSplit"
        Me.lblSplit.Size = New System.Drawing.Size(12, 13)
        Me.lblSplit.TabIndex = 10
        Me.lblSplit.Text = "/"
        '
        'btnPageLast
        '
        Me.btnPageLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageLast.Location = New System.Drawing.Point(602, 14)
        Me.btnPageLast.Name = "btnPageLast"
        Me.btnPageLast.Size = New System.Drawing.Size(30, 21)
        Me.btnPageLast.TabIndex = 13
        Me.btnPageLast.Text = ">|"
        Me.btnPageLast.UseVisualStyleBackColor = True
        '
        'btnPageNext
        '
        Me.btnPageNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(571, 14)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(30, 21)
        Me.btnPageNext.TabIndex = 12
        Me.btnPageNext.Text = ">"
        Me.btnPageNext.UseVisualStyleBackColor = True
        '
        'btnPagePrev
        '
        Me.btnPagePrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPagePrev.Location = New System.Drawing.Point(427, 14)
        Me.btnPagePrev.Name = "btnPagePrev"
        Me.btnPagePrev.Size = New System.Drawing.Size(30, 21)
        Me.btnPagePrev.TabIndex = 8
        Me.btnPagePrev.Text = "<"
        Me.btnPagePrev.UseVisualStyleBackColor = True
        '
        'btnPageFirst
        '
        Me.btnPageFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageFirst.Location = New System.Drawing.Point(396, 14)
        Me.btnPageFirst.Name = "btnPageFirst"
        Me.btnPageFirst.Size = New System.Drawing.Size(30, 21)
        Me.btnPageFirst.TabIndex = 7
        Me.btnPageFirst.Text = "|<"
        Me.btnPageFirst.UseVisualStyleBackColor = True
        '
        'cboRowPerPage
        '
        Me.cboRowPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRowPerPage.FormattingEnabled = True
        Me.cboRowPerPage.Items.AddRange(New Object() {"50", "100", "200"})
        Me.cboRowPerPage.Location = New System.Drawing.Point(351, 14)
        Me.cboRowPerPage.Name = "cboRowPerPage"
        Me.cboRowPerPage.Size = New System.Drawing.Size(43, 21)
        Me.cboRowPerPage.TabIndex = 6
        '
        'txtRowsCount
        '
        Me.txtRowsCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtRowsCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRowsCount.Location = New System.Drawing.Point(707, 14)
        Me.txtRowsCount.Name = "txtRowsCount"
        Me.txtRowsCount.ReadOnly = True
        Me.txtRowsCount.Size = New System.Drawing.Size(41, 20)
        Me.txtRowsCount.TabIndex = 15
        Me.txtRowsCount.TabStop = False
        Me.txtRowsCount.Text = "0"
        Me.txtRowsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRowsCount
        '
        Me.lblRowsCount.AutoSize = True
        Me.lblRowsCount.Location = New System.Drawing.Point(748, 18)
        Me.lblRowsCount.Name = "lblRowsCount"
        Me.lblRowsCount.Size = New System.Drawing.Size(43, 13)
        Me.lblRowsCount.TabIndex = 16
        Me.lblRowsCount.Text = "รายการ"
        '
        'lblTotalRows
        '
        Me.lblTotalRows.AutoSize = True
        Me.lblTotalRows.Location = New System.Drawing.Point(636, 18)
        Me.lblTotalRows.Name = "lblTotalRows"
        Me.lblTotalRows.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalRows.TabIndex = 14
        Me.lblTotalRows.Text = "ผลลัพธ์ทั้งหมด"
        '
        'pnlMainButton
        '
        Me.pnlMainButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMainButton.Controls.Add(Me.btnAutoPR)
        Me.pnlMainButton.Controls.Add(Me.btnClose)
        Me.pnlMainButton.Controls.Add(Me.btnAdd)
        Me.pnlMainButton.Controls.Add(Me.btnEdit)
        Me.pnlMainButton.Controls.Add(Me.btnConfirm)
        Me.pnlMainButton.Controls.Add(Me.btnCancel)
        Me.pnlMainButton.Location = New System.Drawing.Point(144, 696)
        Me.pnlMainButton.Name = "pnlMainButton"
        Me.pnlMainButton.Size = New System.Drawing.Size(866, 38)
        Me.pnlMainButton.TabIndex = 5
        '
        'btnAutoPR
        '
        Me.btnAutoPR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAutoPR.Image = CType(resources.GetObject("btnAutoPR.Image"), System.Drawing.Image)
        Me.btnAutoPR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAutoPR.Location = New System.Drawing.Point(444, 0)
        Me.btnAutoPR.Name = "btnAutoPR"
        Me.btnAutoPR.Size = New System.Drawing.Size(105, 38)
        Me.btnAutoPR.TabIndex = 5
        Me.btnAutoPR.Text = "Auto PR"
        Me.btnAutoPR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAutoPR.UseVisualStyleBackColor = True
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "IsSelected"
        Me.DataGridViewCheckBoxColumn1.FalseValue = ""
        Me.DataGridViewCheckBoxColumn1.FillWeight = 25.0!
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.MinimumWidth = 25
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.TrueValue = ""
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "PurchaseOrder_Index"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "เลขที่ใบประกอบสินค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 140
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "PurchaseOrder_No"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn2.HeaderText = "วันที่"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "PurchaseOrder_Date"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn3.HeaderText = "ประเภทการประกอบ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Supplier_Name"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn4.HeaderText = "ลูกค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 220
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "add_by"
        DataGridViewCellStyle12.Format = "N2"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn5.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "add_by"
        DataGridViewCellStyle13.Format = "N4"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn6.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Net_Amt"
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวนเงิน"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Net_Amt"
        Me.DataGridViewTextBoxColumn8.HeaderText = "การชำระเงิน"
        Me.DataGridViewTextBoxColumn8.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Remark"
        DataGridViewCellStyle14.Format = "N2"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn9.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Remark"
        DataGridViewCellStyle15.Format = "N2"
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewTextBoxColumn10.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Remark"
        Me.DataGridViewTextBoxColumn11.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Remark"
        Me.DataGridViewTextBoxColumn12.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        Me.DataGridViewTextBoxColumn12.Width = 120
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "add_by"
        Me.DataGridViewTextBoxColumn13.HeaderText = "ผู้ทำรายการ"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "IsConfirm"
        Me.DataGridViewTextBoxColumn14.HeaderText = "IsConfirm"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "IsCanCancel"
        Me.DataGridViewTextBoxColumn15.HeaderText = "IsCanCancel"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'frmPR_View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 746)
        Me.Controls.Add(Me.gboPager)
        Me.Controls.Add(Me.pnlMainButton)
        Me.Controls.Add(Me.dgvPR)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(960, 560)
        Me.Name = "frmPR_View"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รายการใบคำขอสั่งซื้อ"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnLeftmenu.ResumeLayout(False)
        Me.gboPrint.ResumeLayout(False)
        Me.gboPrint.PerformLayout()
        Me.gboCondition.ResumeLayout(False)
        Me.gboCondition.PerformLayout()
        Me.pnlCondition1.ResumeLayout(False)
        Me.pnlCondition1.PerformLayout()
        Me.pnlCondition2.ResumeLayout(False)
        Me.pnlCondition2.PerformLayout()
        Me.gboFilter.ResumeLayout(False)
        Me.gboFilter.PerformLayout()
        CType(Me.dgvPR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboPager.ResumeLayout(False)
        Me.gboPager.PerformLayout()
        Me.pnlMainButton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents gboCondition As System.Windows.Forms.GroupBox
    Friend WithEvents lblDate_To As System.Windows.Forms.Label
    Friend WithEvents txtText_Condition As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents radPurchaseOrder_Request_No As System.Windows.Forms.RadioButton
    Friend WithEvents radPurchaseOrder_Request_Date As System.Windows.Forms.RadioButton
    Friend WithEvents gboPrint As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents lblSelectReport As System.Windows.Forms.Label
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents dgvPR As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gboFilter As System.Windows.Forms.GroupBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents dtpDate_S As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate_E As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents gboPager As System.Windows.Forms.GroupBox
    Friend WithEvents txtTop As System.Windows.Forms.TextBox
    Friend WithEvents radRowPage As System.Windows.Forms.RadioButton
    Friend WithEvents radTop As System.Windows.Forms.RadioButton
    Friend WithEvents lblTopRow As System.Windows.Forms.Label
    Friend WithEvents txtTotalPage As System.Windows.Forms.TextBox
    Friend WithEvents txtPage As System.Windows.Forms.TextBox
    Friend WithEvents lblSplit As System.Windows.Forms.Label
    Friend WithEvents btnPageLast As System.Windows.Forms.Button
    Friend WithEvents btnPageNext As System.Windows.Forms.Button
    Friend WithEvents btnPagePrev As System.Windows.Forms.Button
    Friend WithEvents btnPageFirst As System.Windows.Forms.Button
    Friend WithEvents cboRowPerPage As System.Windows.Forms.ComboBox
    Friend WithEvents txtRowsCount As System.Windows.Forms.TextBox
    Friend WithEvents lblRowsCount As System.Windows.Forms.Label
    Friend WithEvents lblTotalRows As System.Windows.Forms.Label
    Friend WithEvents radAll As System.Windows.Forms.RadioButton
    Friend WithEvents radDue_Date As System.Windows.Forms.RadioButton
    Friend WithEvents radCustomer_Id As System.Windows.Forms.RadioButton
    Friend WithEvents lblProductType As System.Windows.Forms.Label
    Friend WithEvents cboProductType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMainButton As System.Windows.Forms.Panel
    Friend WithEvents pnlCondition1 As System.Windows.Forms.Panel
    Friend WithEvents pnlCondition2 As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAutoPR As System.Windows.Forms.Button
    Friend WithEvents col_IsSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_PurchaseOrder_Request_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_Request_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DocumentType_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Addby As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Received_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Pending_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_Request_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_IsClosed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_IsConfirm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_IsCanCancel As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
