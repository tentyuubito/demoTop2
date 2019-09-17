<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetTransferView_V2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetTransferView_V2))
        Me.grdTranferStatusView = New System.Windows.Forms.DataGridView
        Me.mnuAssignJob = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AssignToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PriorityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VeryHightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NornalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HOLDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.Group_print = New System.Windows.Forms.GroupBox
        Me.But_print = New System.Windows.Forms.Button
        Me.cbPrint = New System.Windows.Forms.ComboBox
        Me.L_DocumentType = New System.Windows.Forms.Label
        Me.gbCondition = New System.Windows.Forms.GroupBox
        Me.dateEnd = New System.Windows.Forms.DateTimePicker
        Me.L_to = New System.Windows.Forms.Label
        Me.Rad_Ref1 = New System.Windows.Forms.RadioButton
        Me.Rad_CusName = New System.Windows.Forms.RadioButton
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.txtKeySearch = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.rdbTransferStatus_No = New System.Windows.Forms.RadioButton
        Me.rdbTransferStatus_Date = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboDocument_Type = New System.Windows.Forms.ComboBox
        Me.L_tran = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboDocumentStatus = New System.Windows.Forms.ComboBox
        Me.lbCountRows = New System.Windows.Forms.Label
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
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnAssignJob = New System.Windows.Forms.Button
        Me.But_Cancel = New System.Windows.Forms.Button
        Me.But_Exit = New System.Windows.Forms.Button
        Me.But_Add = New System.Windows.Forms.Button
        Me.But_Edit = New System.Windows.Forms.Button
        Me.But_Summit = New System.Windows.Forms.Button
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
        Me.btnTransfer = New System.Windows.Forms.Button
        Me.StatusValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.System_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_Fullfill = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Activity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Ref1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Ref2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Add_by = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.status_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_AssignJob_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UserName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Comment = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdTranferStatusView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuAssignJob.SuspendLayout()
        Me.pnLeftmenu.SuspendLayout()
        Me.Group_print.SuspendLayout()
        Me.gbCondition.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbPageEng.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdTranferStatusView
        '
        Me.grdTranferStatusView.AllowUserToAddRows = False
        Me.grdTranferStatusView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdTranferStatusView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTranferStatusView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTranferStatusView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTranferStatusView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdTranferStatusView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.StatusValue, Me.System_Index, Me.Document_No, Me.Document_type, Me.Document_Date, Me.Customer_Name, Me.Status, Me.col_Status_Fullfill, Me.Activity, Me.Col_Ref1, Me.Col_Ref2, Me.Add_by, Me.col_DistributionCenter, Me.status_ID, Me.col_AssignJob_Index, Me.col_UserName, Me.Col_Comment})
        Me.grdTranferStatusView.Location = New System.Drawing.Point(192, 60)
        Me.grdTranferStatusView.Margin = New System.Windows.Forms.Padding(4)
        Me.grdTranferStatusView.Name = "grdTranferStatusView"
        Me.grdTranferStatusView.RowHeadersVisible = False
        Me.grdTranferStatusView.RowTemplate.Height = 24
        Me.grdTranferStatusView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTranferStatusView.Size = New System.Drawing.Size(1140, 677)
        Me.grdTranferStatusView.TabIndex = 1
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
        Me.pnLeftmenu.Controls.Add(Me.Group_print)
        Me.pnLeftmenu.Controls.Add(Me.gbCondition)
        Me.pnLeftmenu.Controls.Add(Me.GroupBox2)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 9)
        Me.pnLeftmenu.Margin = New System.Windows.Forms.Padding(4)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(191, 729)
        Me.pnLeftmenu.TabIndex = 0
        '
        'Group_print
        '
        Me.Group_print.Controls.Add(Me.But_print)
        Me.Group_print.Controls.Add(Me.cbPrint)
        Me.Group_print.Controls.Add(Me.L_DocumentType)
        Me.Group_print.Location = New System.Drawing.Point(9, 497)
        Me.Group_print.Margin = New System.Windows.Forms.Padding(4)
        Me.Group_print.Name = "Group_print"
        Me.Group_print.Padding = New System.Windows.Forms.Padding(4)
        Me.Group_print.Size = New System.Drawing.Size(175, 155)
        Me.Group_print.TabIndex = 2
        Me.Group_print.TabStop = False
        Me.Group_print.Text = "พิมพ์เอกสาร"
        '
        'But_print
        '
        Me.But_print.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.But_print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.But_print.Location = New System.Drawing.Point(20, 85)
        Me.But_print.Margin = New System.Windows.Forms.Padding(4)
        Me.But_print.Name = "But_print"
        Me.But_print.Size = New System.Drawing.Size(133, 47)
        Me.But_print.TabIndex = 2
        Me.But_print.Text = "พิมพ์"
        Me.But_print.UseVisualStyleBackColor = True
        '
        'cbPrint
        '
        Me.cbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrint.FormattingEnabled = True
        Me.cbPrint.Location = New System.Drawing.Point(7, 52)
        Me.cbPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(159, 24)
        Me.cbPrint.TabIndex = 1
        '
        'L_DocumentType
        '
        Me.L_DocumentType.AutoSize = True
        Me.L_DocumentType.Location = New System.Drawing.Point(7, 32)
        Me.L_DocumentType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.L_DocumentType.Name = "L_DocumentType"
        Me.L_DocumentType.Size = New System.Drawing.Size(88, 17)
        Me.L_DocumentType.TabIndex = 0
        Me.L_DocumentType.Text = "ประเภทเอกสาร"
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.dateEnd)
        Me.gbCondition.Controls.Add(Me.L_to)
        Me.gbCondition.Controls.Add(Me.Rad_Ref1)
        Me.gbCondition.Controls.Add(Me.Rad_CusName)
        Me.gbCondition.Controls.Add(Me.dtpDate)
        Me.gbCondition.Controls.Add(Me.txtKeySearch)
        Me.gbCondition.Controls.Add(Me.btnSearch)
        Me.gbCondition.Controls.Add(Me.rdbTransferStatus_No)
        Me.gbCondition.Controls.Add(Me.rdbTransferStatus_Date)
        Me.gbCondition.Location = New System.Drawing.Point(9, 4)
        Me.gbCondition.Margin = New System.Windows.Forms.Padding(4)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCondition.Size = New System.Drawing.Size(175, 332)
        Me.gbCondition.TabIndex = 0
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "เงื่อนไข"
        '
        'dateEnd
        '
        Me.dateEnd.CustomFormat = "dd/MM/yyyy"
        Me.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateEnd.Location = New System.Drawing.Point(7, 217)
        Me.dateEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.dateEnd.Name = "dateEnd"
        Me.dateEnd.Size = New System.Drawing.Size(159, 22)
        Me.dateEnd.TabIndex = 6
        '
        'L_to
        '
        Me.L_to.AutoSize = True
        Me.L_to.Location = New System.Drawing.Point(72, 196)
        Me.L_to.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.L_to.Name = "L_to"
        Me.L_to.Size = New System.Drawing.Size(22, 17)
        Me.L_to.TabIndex = 5
        Me.L_to.Text = "ถึง"
        '
        'Rad_Ref1
        '
        Me.Rad_Ref1.AutoSize = True
        Me.Rad_Ref1.Location = New System.Drawing.Point(11, 121)
        Me.Rad_Ref1.Margin = New System.Windows.Forms.Padding(4)
        Me.Rad_Ref1.Name = "Rad_Ref1"
        Me.Rad_Ref1.Size = New System.Drawing.Size(127, 21)
        Me.Rad_Ref1.TabIndex = 3
        Me.Rad_Ref1.TabStop = True
        Me.Rad_Ref1.Text = "เลขที่เอกสารอ้างอิง"
        Me.Rad_Ref1.UseVisualStyleBackColor = True
        '
        'Rad_CusName
        '
        Me.Rad_CusName.AutoSize = True
        Me.Rad_CusName.Location = New System.Drawing.Point(11, 91)
        Me.Rad_CusName.Margin = New System.Windows.Forms.Padding(4)
        Me.Rad_CusName.Name = "Rad_CusName"
        Me.Rad_CusName.Size = New System.Drawing.Size(73, 21)
        Me.Rad_CusName.TabIndex = 2
        Me.Rad_CusName.TabStop = True
        Me.Rad_CusName.Text = "ชื่อลูกค้า"
        Me.Rad_CusName.UseVisualStyleBackColor = True
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(7, 166)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(159, 22)
        Me.dtpDate.TabIndex = 4
        '
        'txtKeySearch
        '
        Me.txtKeySearch.Location = New System.Drawing.Point(7, 166)
        Me.txtKeySearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKeySearch.Name = "txtKeySearch"
        Me.txtKeySearch.Size = New System.Drawing.Size(159, 22)
        Me.txtKeySearch.TabIndex = 6
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(20, 251)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(133, 47)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'rdbTransferStatus_No
        '
        Me.rdbTransferStatus_No.AutoSize = True
        Me.rdbTransferStatus_No.Location = New System.Drawing.Point(11, 62)
        Me.rdbTransferStatus_No.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbTransferStatus_No.Name = "rdbTransferStatus_No"
        Me.rdbTransferStatus_No.Size = New System.Drawing.Size(122, 21)
        Me.rdbTransferStatus_No.TabIndex = 1
        Me.rdbTransferStatus_No.Text = "เลขที่ใบโอนสินค้า"
        Me.rdbTransferStatus_No.UseVisualStyleBackColor = True
        '
        'rdbTransferStatus_Date
        '
        Me.rdbTransferStatus_Date.AutoSize = True
        Me.rdbTransferStatus_Date.Checked = True
        Me.rdbTransferStatus_Date.Location = New System.Drawing.Point(11, 33)
        Me.rdbTransferStatus_Date.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbTransferStatus_Date.Name = "rdbTransferStatus_Date"
        Me.rdbTransferStatus_Date.Size = New System.Drawing.Size(114, 21)
        Me.rdbTransferStatus_Date.TabIndex = 0
        Me.rdbTransferStatus_Date.TabStop = True
        Me.rdbTransferStatus_Date.Text = "วันที่ออกเอกสาร"
        Me.rdbTransferStatus_Date.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboDocument_Type)
        Me.GroupBox2.Controls.Add(Me.L_tran)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cboDocumentStatus)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 343)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(175, 146)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "กรองรายการ"
        '
        'cboDocument_Type
        '
        Me.cboDocument_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocument_Type.FormattingEnabled = True
        Me.cboDocument_Type.Location = New System.Drawing.Point(7, 98)
        Me.cboDocument_Type.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocument_Type.Name = "cboDocument_Type"
        Me.cboDocument_Type.Size = New System.Drawing.Size(160, 24)
        Me.cboDocument_Type.TabIndex = 3
        '
        'L_tran
        '
        Me.L_tran.AutoSize = True
        Me.L_tran.Location = New System.Drawing.Point(7, 79)
        Me.L_tran.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.L_tran.Name = "L_tran"
        Me.L_tran.Size = New System.Drawing.Size(92, 17)
        Me.L_tran.TabIndex = 2
        Me.L_tran.Text = "ประเภทการโอน"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "สถานะเอกสาร"
        '
        'cboDocumentStatus
        '
        Me.cboDocumentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentStatus.FormattingEnabled = True
        Me.cboDocumentStatus.Location = New System.Drawing.Point(7, 47)
        Me.cboDocumentStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocumentStatus.Name = "cboDocumentStatus"
        Me.cboDocumentStatus.Size = New System.Drawing.Size(159, 24)
        Me.cboDocumentStatus.TabIndex = 1
        '
        'lbCountRows
        '
        Me.lbCountRows.AutoSize = True
        Me.lbCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows.Location = New System.Drawing.Point(188, 743)
        Me.lbCountRows.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows.Name = "lbCountRows"
        Me.lbCountRows.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows.TabIndex = 2
        Me.lbCountRows.Text = "ไม่พบรายการ"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่ออกเอกสาร "
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
        Me.DataGridViewTextBoxColumn5.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Ref1"
        Me.DataGridViewTextBoxColumn8.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 150
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Ref1"
        Me.DataGridViewTextBoxColumn9.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 80
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Ref2"
        Me.DataGridViewTextBoxColumn10.HeaderText = "อ้างอิง 2"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn11.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "status_ID"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "col_AssignJob_Index"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "ผู้ปฎิบัตงาน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Ref2"
        Me.DataGridViewTextBoxColumn17.HeaderText = "ชื่อร้านค้า"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn18.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Visible = False
        Me.DataGridViewTextBoxColumn18.Width = 150
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = "MID"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 80
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn20.HeaderText = "ชื่อร้านค้า"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 150
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAssignJob.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnAssignJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAssignJob.Location = New System.Drawing.Point(795, 768)
        Me.btnAssignJob.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(143, 47)
        Me.btnAssignJob.TabIndex = 33
        Me.btnAssignJob.Text = "มอบหมายงาน"
        Me.btnAssignJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAssignJob.UseVisualStyleBackColor = True
        '
        'But_Cancel
        '
        Me.But_Cancel.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ยกเลิกรายการ
        Me.But_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.But_Cancel.Location = New System.Drawing.Point(644, 768)
        Me.But_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.But_Cancel.Name = "But_Cancel"
        Me.But_Cancel.Size = New System.Drawing.Size(143, 47)
        Me.But_Cancel.TabIndex = 6
        Me.But_Cancel.Text = "ยกเลิก"
        Me.But_Cancel.UseVisualStyleBackColor = True
        '
        'But_Exit
        '
        Me.But_Exit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.But_Exit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.But_Exit.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.But_Exit.Location = New System.Drawing.Point(1189, 768)
        Me.But_Exit.Margin = New System.Windows.Forms.Padding(4)
        Me.But_Exit.Name = "But_Exit"
        Me.But_Exit.Size = New System.Drawing.Size(143, 47)
        Me.But_Exit.TabIndex = 8
        Me.But_Exit.Text = "ออก"
        Me.But_Exit.UseVisualStyleBackColor = True
        '
        'But_Add
        '
        Me.But_Add.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.But_Add.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.But_Add.Location = New System.Drawing.Point(192, 768)
        Me.But_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.But_Add.Name = "But_Add"
        Me.But_Add.Size = New System.Drawing.Size(143, 47)
        Me.But_Add.TabIndex = 3
        Me.But_Add.Text = "เพิ่มรายการ"
        Me.But_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.But_Add.UseVisualStyleBackColor = True
        '
        'But_Edit
        '
        Me.But_Edit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.But_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.But_Edit.Location = New System.Drawing.Point(343, 768)
        Me.But_Edit.Margin = New System.Windows.Forms.Padding(4)
        Me.But_Edit.Name = "But_Edit"
        Me.But_Edit.Size = New System.Drawing.Size(143, 47)
        Me.But_Edit.TabIndex = 4
        Me.But_Edit.Text = "แก้ไขรายการ"
        Me.But_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.But_Edit.UseVisualStyleBackColor = True
        '
        'But_Summit
        '
        Me.But_Summit.Image = CType(resources.GetObject("But_Summit.Image"), System.Drawing.Image)
        Me.But_Summit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.But_Summit.Location = New System.Drawing.Point(493, 768)
        Me.But_Summit.Margin = New System.Windows.Forms.Padding(4)
        Me.But_Summit.Name = "But_Summit"
        Me.But_Summit.Size = New System.Drawing.Size(143, 47)
        Me.But_Summit.TabIndex = 5
        Me.But_Summit.Text = "ยืนยันรายการ"
        Me.But_Summit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.But_Summit.UseVisualStyleBackColor = True
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
        Me.grbPageEng.Margin = New System.Windows.Forms.Padding(4)
        Me.grbPageEng.Name = "grbPageEng"
        Me.grbPageEng.Padding = New System.Windows.Forms.Padding(4)
        Me.grbPageEng.Size = New System.Drawing.Size(1152, 49)
        Me.grbPageEng.TabIndex = 225
        Me.grbPageEng.TabStop = False
        Me.grbPageEng.Text = "การแสดงผล"
        '
        'rdAll
        '
        Me.rdAll.AutoSize = True
        Me.rdAll.Location = New System.Drawing.Point(7, 21)
        Me.rdAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(97, 21)
        Me.rdAll.TabIndex = 22
        Me.rdAll.Text = "แสดงทั้งหมด"
        Me.rdAll.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(223, 20)
        Me.txtTop.Margin = New System.Windows.Forms.Padding(4)
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
        Me.rdRowPage.Margin = New System.Windows.Forms.Padding(4)
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
        Me.rdTop.Margin = New System.Windows.Forms.Padding(4)
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
        Me.txtTotalPage.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalPage.Name = "txtTotalPage"
        Me.txtTotalPage.ReadOnly = True
        Me.txtTotalPage.Size = New System.Drawing.Size(57, 22)
        Me.txtTotalPage.TabIndex = 4
        Me.txtTotalPage.TabStop = False
        '
        'txtPageIndex
        '
        Me.txtPageIndex.Location = New System.Drawing.Point(589, 16)
        Me.txtPageIndex.Margin = New System.Windows.Forms.Padding(4)
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
        Me.btnPageLast.Margin = New System.Windows.Forms.Padding(4)
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
        Me.btnPageNext.Margin = New System.Windows.Forms.Padding(4)
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
        Me.btnPagePrev.Margin = New System.Windows.Forms.Padding(4)
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
        Me.btnPageFirst.Margin = New System.Windows.Forms.Padding(4)
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
        Me.cboRowPerPage.Margin = New System.Windows.Forms.Padding(4)
        Me.cboRowPerPage.Name = "cboRowPerPage"
        Me.cboRowPerPage.Size = New System.Drawing.Size(56, 24)
        Me.cboRowPerPage.TabIndex = 0
        '
        'txtRowCount
        '
        Me.txtRowCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtRowCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRowCount.Location = New System.Drawing.Point(920, 15)
        Me.txtRowCount.Margin = New System.Windows.Forms.Padding(4)
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
        'btnTransfer
        '
        Me.btnTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnTransfer.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTransfer.Location = New System.Drawing.Point(946, 768)
        Me.btnTransfer.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(235, 47)
        Me.btnTransfer.TabIndex = 226
        Me.btnTransfer.Text = "เติมสินค้า pickface อัตโนมัติ"
        Me.btnTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'StatusValue
        '
        Me.StatusValue.HeaderText = "Column1"
        Me.StatusValue.Name = "StatusValue"
        Me.StatusValue.Visible = False
        '
        'System_Index
        '
        Me.System_Index.HeaderText = "รหัสระบบ "
        Me.System_Index.Name = "System_Index"
        Me.System_Index.ReadOnly = True
        Me.System_Index.Visible = False
        Me.System_Index.Width = 80
        '
        'Document_No
        '
        Me.Document_No.HeaderText = "เลขที่ใบโอนสินค้า"
        Me.Document_No.Name = "Document_No"
        Me.Document_No.ReadOnly = True
        Me.Document_No.Width = 140
        '
        'Document_type
        '
        Me.Document_type.HeaderText = "ประเภทการโอน"
        Me.Document_type.Name = "Document_type"
        Me.Document_type.ReadOnly = True
        Me.Document_type.Width = 120
        '
        'Document_Date
        '
        Me.Document_Date.HeaderText = "วันที่โอนสินค้า"
        Me.Document_Date.Name = "Document_Date"
        Me.Document_Date.ReadOnly = True
        Me.Document_Date.Width = 130
        '
        'Customer_Name
        '
        Me.Customer_Name.HeaderText = "ชื่อลูกค้า "
        Me.Customer_Name.Name = "Customer_Name"
        Me.Customer_Name.ReadOnly = True
        Me.Customer_Name.Width = 150
        '
        'Status
        '
        Me.Status.HeaderText = "สถานะ"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'col_Status_Fullfill
        '
        Me.col_Status_Fullfill.HeaderText = "เติมเต็ม"
        Me.col_Status_Fullfill.Name = "col_Status_Fullfill"
        '
        'Activity
        '
        Me.Activity.DataPropertyName = "Activity"
        Me.Activity.HeaderText = "Activity"
        Me.Activity.Name = "Activity"
        '
        'Col_Ref1
        '
        Me.Col_Ref1.DataPropertyName = "Ref1"
        Me.Col_Ref1.HeaderText = "อ้างอิง 1"
        Me.Col_Ref1.Name = "Col_Ref1"
        Me.Col_Ref1.ReadOnly = True
        Me.Col_Ref1.Width = 50
        '
        'Col_Ref2
        '
        Me.Col_Ref2.DataPropertyName = "Ref2"
        Me.Col_Ref2.HeaderText = "อ้างอิง 2"
        Me.Col_Ref2.Name = "Col_Ref2"
        Me.Col_Ref2.ReadOnly = True
        Me.Col_Ref2.Width = 50
        '
        'Add_by
        '
        Me.Add_by.HeaderText = "ผู้ออกเอกสาร"
        Me.Add_by.Name = "Add_by"
        Me.Add_by.ReadOnly = True
        '
        'col_DistributionCenter
        '
        Me.col_DistributionCenter.HeaderText = "สูนย์กระจาย(สร้าง)"
        Me.col_DistributionCenter.Name = "col_DistributionCenter"
        '
        'status_ID
        '
        Me.status_ID.HeaderText = "status_ID"
        Me.status_ID.Name = "status_ID"
        Me.status_ID.Visible = False
        '
        'col_AssignJob_Index
        '
        Me.col_AssignJob_Index.HeaderText = "col_AssignJob_Index"
        Me.col_AssignJob_Index.Name = "col_AssignJob_Index"
        Me.col_AssignJob_Index.Visible = False
        '
        'col_UserName
        '
        Me.col_UserName.HeaderText = "ผู้ปฎิบัตงาน"
        Me.col_UserName.Name = "col_UserName"
        '
        'Col_Comment
        '
        Me.Col_Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Comment.DataPropertyName = "Comment"
        Me.Col_Comment.HeaderText = "หมายเหตุ"
        Me.Col_Comment.Name = "Col_Comment"
        Me.Col_Comment.ReadOnly = True
        '
        'frmAssetTransferView_V2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1355, 916)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.grbPageEng)
        Me.Controls.Add(Me.btnAssignJob)
        Me.Controls.Add(Me.lbCountRows)
        Me.Controls.Add(Me.grdTranferStatusView)
        Me.Controls.Add(Me.But_Cancel)
        Me.Controls.Add(Me.But_Exit)
        Me.Controls.Add(Me.But_Add)
        Me.Controls.Add(Me.But_Edit)
        Me.Controls.Add(Me.But_Summit)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAssetTransferView_V2"
        Me.ShowInTaskbar = False
        Me.Text = "รายการใบโอน/ย้ายสินค้า"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdTranferStatusView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuAssignJob.ResumeLayout(False)
        Me.pnLeftmenu.ResumeLayout(False)
        Me.Group_print.ResumeLayout(False)
        Me.Group_print.PerformLayout()
        Me.gbCondition.ResumeLayout(False)
        Me.gbCondition.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grbPageEng.ResumeLayout(False)
        Me.grbPageEng.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdTranferStatusView As System.Windows.Forms.DataGridView
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtKeySearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rdbTransferStatus_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTransferStatus_Date As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDocumentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents But_Summit As System.Windows.Forms.Button
    Friend WithEvents But_Edit As System.Windows.Forms.Button
    Friend WithEvents But_Add As System.Windows.Forms.Button
    Friend WithEvents But_Exit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rad_Ref1 As System.Windows.Forms.RadioButton
    Friend WithEvents Rad_CusName As System.Windows.Forms.RadioButton
    Friend WithEvents Group_print As System.Windows.Forms.GroupBox
    Friend WithEvents L_to As System.Windows.Forms.Label
    Friend WithEvents dateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents L_DocumentType As System.Windows.Forms.Label
    Friend WithEvents cboDocument_Type As System.Windows.Forms.ComboBox
    Friend WithEvents L_tran As System.Windows.Forms.Label
    Friend WithEvents But_print As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.ComboBox
    Friend WithEvents But_Cancel As System.Windows.Forms.Button
    Friend WithEvents lbCountRows As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuAssignJob As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AssignToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PriorityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VeryHightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NornalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HOLDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAssignJob As System.Windows.Forms.Button
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
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents StatusValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents System_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_Fullfill As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Activity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Ref1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Ref2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Add_by As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AssignJob_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Comment As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
