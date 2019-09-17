<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdjustView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdjustView))
        Me.grdAdjustView = New System.Windows.Forms.DataGridView
        Me.Col_chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.System_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Document_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DocumentType_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Location = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Ref_No1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Add_by = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UserName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_AssignJob_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuAssignJob = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AssignToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PriorityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VeryHightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NornalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HOLDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.btn_PrintAdjust_Act = New System.Windows.Forms.Button
        Me.gbPrintOut = New System.Windows.Forms.GroupBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.cbPrint = New System.Windows.Forms.ComboBox
        Me.gbCondition = New System.Windows.Forms.GroupBox
        Me.L_to = New System.Windows.Forms.Label
        Me.dateEnd = New System.Windows.Forms.DateTimePicker
        Me.rdbRef1 = New System.Windows.Forms.RadioButton
        Me.rdbCusName = New System.Windows.Forms.RadioButton
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.txtKeySearch = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.rdbAdjust_No = New System.Windows.Forms.RadioButton
        Me.rdbAdjust_Date = New System.Windows.Forms.RadioButton
        Me.gbFilter = New System.Windows.Forms.GroupBox
        Me.ChkActive = New System.Windows.Forms.CheckBox
        Me.lblFilterStatus = New System.Windows.Forms.Label
        Me.cboDocumentStatus = New System.Windows.Forms.ComboBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.lbCountRows = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnShowItemList = New System.Windows.Forms.Button
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
        Me.chkall = New System.Windows.Forms.CheckBox
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnAssignJob = New System.Windows.Forms.Button
        CType(Me.grdAdjustView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuAssignJob.SuspendLayout()
        Me.pnLeftmenu.SuspendLayout()
        Me.gbPrintOut.SuspendLayout()
        Me.gbCondition.SuspendLayout()
        Me.gbFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAdjustView
        '
        Me.grdAdjustView.AllowUserToAddRows = False
        Me.grdAdjustView.AllowUserToDeleteRows = False
        Me.grdAdjustView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdAdjustView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdAdjustView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAdjustView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdAdjustView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdAdjustView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_chkSelect, Me.System_Index, Me.Document_No, Me.Document_Date, Me.col_DocumentType_Desc, Me.col_Location, Me.Status, Me.Customer_Name, Me.Ref_No1, Me.Add_by, Me.col_UserName, Me.col_AssignJob_Index, Me.col_Status_Id})
        Me.grdAdjustView.ContextMenuStrip = Me.mnuAssignJob
        Me.grdAdjustView.Location = New System.Drawing.Point(144, 12)
        Me.grdAdjustView.MultiSelect = False
        Me.grdAdjustView.Name = "grdAdjustView"
        Me.grdAdjustView.RowHeadersVisible = False
        Me.grdAdjustView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdAdjustView.Size = New System.Drawing.Size(875, 592)
        Me.grdAdjustView.TabIndex = 1
        '
        'Col_chkSelect
        '
        Me.Col_chkSelect.DataPropertyName = "chkSelect"
        Me.Col_chkSelect.Frozen = True
        Me.Col_chkSelect.HeaderText = ""
        Me.Col_chkSelect.Name = "Col_chkSelect"
        Me.Col_chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_chkSelect.Width = 25
        '
        'System_Index
        '
        Me.System_Index.DataPropertyName = "Adjust_Index"
        Me.System_Index.HeaderText = "รหัสระบบ "
        Me.System_Index.Name = "System_Index"
        Me.System_Index.ReadOnly = True
        Me.System_Index.Visible = False
        '
        'Document_No
        '
        Me.Document_No.DataPropertyName = "Adjust_No"
        Me.Document_No.Frozen = True
        Me.Document_No.HeaderText = "เลขที่ใบตรวจนับ"
        Me.Document_No.Name = "Document_No"
        Me.Document_No.ReadOnly = True
        Me.Document_No.Width = 150
        '
        'Document_Date
        '
        Me.Document_Date.DataPropertyName = "Adjust_Date"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Document_Date.DefaultCellStyle = DataGridViewCellStyle3
        Me.Document_Date.HeaderText = "วันที่ออกเอกสาร"
        Me.Document_Date.Name = "Document_Date"
        Me.Document_Date.ReadOnly = True
        Me.Document_Date.Width = 150
        '
        'col_DocumentType_Desc
        '
        Me.col_DocumentType_Desc.DataPropertyName = "DocumentType_Desc"
        Me.col_DocumentType_Desc.HeaderText = "ประเภทเอกสาร"
        Me.col_DocumentType_Desc.Name = "col_DocumentType_Desc"
        Me.col_DocumentType_Desc.ReadOnly = True
        Me.col_DocumentType_Desc.Width = 150
        '
        'col_Location
        '
        Me.col_Location.DataPropertyName = "Location"
        Me.col_Location.HeaderText = "ตำแหน่ง"
        Me.col_Location.Name = "col_Location"
        Me.col_Location.ReadOnly = True
        Me.col_Location.Width = 150
        '
        'Status
        '
        Me.Status.DataPropertyName = "StatusDescription"
        Me.Status.HeaderText = "สถานะ"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'Customer_Name
        '
        Me.Customer_Name.DataPropertyName = "Customer_Name"
        Me.Customer_Name.HeaderText = "ชื่อลูกค้า"
        Me.Customer_Name.Name = "Customer_Name"
        Me.Customer_Name.ReadOnly = True
        Me.Customer_Name.Width = 170
        '
        'Ref_No1
        '
        Me.Ref_No1.DataPropertyName = "Ref_No1"
        Me.Ref_No1.FillWeight = 120.0!
        Me.Ref_No1.HeaderText = "เลขที่เอกสารอ้างอิง"
        Me.Ref_No1.Name = "Ref_No1"
        Me.Ref_No1.ReadOnly = True
        Me.Ref_No1.Width = 120
        '
        'Add_by
        '
        Me.Add_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Add_by.DataPropertyName = "Add_By"
        Me.Add_by.HeaderText = "ผู้ออกเอกสาร"
        Me.Add_by.Name = "Add_by"
        Me.Add_by.ReadOnly = True
        '
        'col_UserName
        '
        Me.col_UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_UserName.DataPropertyName = "userFullName"
        Me.col_UserName.HeaderText = "ผู้ปฎิบัตงาน"
        Me.col_UserName.Name = "col_UserName"
        Me.col_UserName.ReadOnly = True
        Me.col_UserName.Visible = False
        '
        'col_AssignJob_Index
        '
        Me.col_AssignJob_Index.DataPropertyName = "AssignJob_Index"
        Me.col_AssignJob_Index.HeaderText = "AssignJob_Index"
        Me.col_AssignJob_Index.Name = "col_AssignJob_Index"
        Me.col_AssignJob_Index.ReadOnly = True
        Me.col_AssignJob_Index.Visible = False
        '
        'col_Status_Id
        '
        Me.col_Status_Id.DataPropertyName = "Status"
        Me.col_Status_Id.HeaderText = "Status_Id"
        Me.col_Status_Id.Name = "col_Status_Id"
        Me.col_Status_Id.ReadOnly = True
        Me.col_Status_Id.Visible = False
        '
        'mnuAssignJob
        '
        Me.mnuAssignJob.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AssignToToolStripMenuItem, Me.PriorityToolStripMenuItem})
        Me.mnuAssignJob.Name = "ContextMenuStrip1"
        Me.mnuAssignJob.Size = New System.Drawing.Size(147, 48)
        '
        'AssignToToolStripMenuItem
        '
        Me.AssignToToolStripMenuItem.Name = "AssignToToolStripMenuItem"
        Me.AssignToToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AssignToToolStripMenuItem.Text = "มอบหมายให้"
        '
        'PriorityToolStripMenuItem
        '
        Me.PriorityToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VeryHightToolStripMenuItem, Me.HightToolStripMenuItem, Me.NornalToolStripMenuItem, Me.LowToolStripMenuItem, Me.HOLDToolStripMenuItem})
        Me.PriorityToolStripMenuItem.Name = "PriorityToolStripMenuItem"
        Me.PriorityToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.PriorityToolStripMenuItem.Text = "ระดับความสำดับ"
        '
        'VeryHightToolStripMenuItem
        '
        Me.VeryHightToolStripMenuItem.Name = "VeryHightToolStripMenuItem"
        Me.VeryHightToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.VeryHightToolStripMenuItem.Text = "เร่งด่วนมาก"
        '
        'HightToolStripMenuItem
        '
        Me.HightToolStripMenuItem.Name = "HightToolStripMenuItem"
        Me.HightToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.HightToolStripMenuItem.Text = "เร่งด่วน"
        '
        'NornalToolStripMenuItem
        '
        Me.NornalToolStripMenuItem.Name = "NornalToolStripMenuItem"
        Me.NornalToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.NornalToolStripMenuItem.Text = "ปกติ"
        '
        'LowToolStripMenuItem
        '
        Me.LowToolStripMenuItem.Name = "LowToolStripMenuItem"
        Me.LowToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.LowToolStripMenuItem.Text = "ไม่เร่งด่วน"
        '
        'HOLDToolStripMenuItem
        '
        Me.HOLDToolStripMenuItem.Name = "HOLDToolStripMenuItem"
        Me.HOLDToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.HOLDToolStripMenuItem.Text = "ระงับ"
        '
        'btnUpdate
        '
        Me.btnUpdate.Enabled = False
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnUpdate.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(252, 622)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(107, 38)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "    แก้ไขรายการ"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAdd.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(145, 622)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(107, 38)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "      เพิ่มรายการ"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'pnLeftmenu
        '
        Me.pnLeftmenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnLeftmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnLeftmenu.Controls.Add(Me.btn_PrintAdjust_Act)
        Me.pnLeftmenu.Controls.Add(Me.gbPrintOut)
        Me.pnLeftmenu.Controls.Add(Me.gbCondition)
        Me.pnLeftmenu.Controls.Add(Me.gbFilter)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 7)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(143, 592)
        Me.pnLeftmenu.TabIndex = 0
        '
        'btn_PrintAdjust_Act
        '
        Me.btn_PrintAdjust_Act.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.btn_PrintAdjust_Act.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_PrintAdjust_Act.Location = New System.Drawing.Point(6, 505)
        Me.btn_PrintAdjust_Act.Name = "btn_PrintAdjust_Act"
        Me.btn_PrintAdjust_Act.Size = New System.Drawing.Size(129, 43)
        Me.btn_PrintAdjust_Act.TabIndex = 3
        Me.btn_PrintAdjust_Act.Text = "พิมพ์รายการรวม" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "รับเข้า/เบิกออก"
        Me.btn_PrintAdjust_Act.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_PrintAdjust_Act.UseVisualStyleBackColor = True
        '
        'gbPrintOut
        '
        Me.gbPrintOut.Controls.Add(Me.btnPrint)
        Me.gbPrintOut.Controls.Add(Me.cbPrint)
        Me.gbPrintOut.Location = New System.Drawing.Point(4, 399)
        Me.gbPrintOut.Name = "gbPrintOut"
        Me.gbPrintOut.Size = New System.Drawing.Size(131, 100)
        Me.gbPrintOut.TabIndex = 2
        Me.gbPrintOut.TabStop = False
        Me.gbPrintOut.Text = "พิมพ์เอกสาร"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(14, 50)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 38)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "  พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'cbPrint
        '
        Me.cbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrint.FormattingEnabled = True
        Me.cbPrint.Location = New System.Drawing.Point(6, 23)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(119, 21)
        Me.cbPrint.TabIndex = 1
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.L_to)
        Me.gbCondition.Controls.Add(Me.dateEnd)
        Me.gbCondition.Controls.Add(Me.rdbRef1)
        Me.gbCondition.Controls.Add(Me.rdbCusName)
        Me.gbCondition.Controls.Add(Me.dtpDate)
        Me.gbCondition.Controls.Add(Me.txtKeySearch)
        Me.gbCondition.Controls.Add(Me.btnSearch)
        Me.gbCondition.Controls.Add(Me.rdbAdjust_No)
        Me.gbCondition.Controls.Add(Me.rdbAdjust_Date)
        Me.gbCondition.Location = New System.Drawing.Point(4, 3)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Size = New System.Drawing.Size(131, 267)
        Me.gbCondition.TabIndex = 0
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "เงื่อนไข"
        '
        'L_to
        '
        Me.L_to.AutoSize = True
        Me.L_to.Location = New System.Drawing.Point(50, 174)
        Me.L_to.Name = "L_to"
        Me.L_to.Size = New System.Drawing.Size(20, 13)
        Me.L_to.TabIndex = 5
        Me.L_to.Text = "To"
        '
        'dateEnd
        '
        Me.dateEnd.CustomFormat = "dd/MM/yyyy"
        Me.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateEnd.Location = New System.Drawing.Point(15, 190)
        Me.dateEnd.Name = "dateEnd"
        Me.dateEnd.Size = New System.Drawing.Size(100, 20)
        Me.dateEnd.TabIndex = 6
        '
        'rdbRef1
        '
        Me.rdbRef1.AutoSize = True
        Me.rdbRef1.Location = New System.Drawing.Point(9, 95)
        Me.rdbRef1.Name = "rdbRef1"
        Me.rdbRef1.Size = New System.Drawing.Size(111, 17)
        Me.rdbRef1.TabIndex = 3
        Me.rdbRef1.Text = "เลขที่เอกสารอ้างอิง"
        Me.rdbRef1.UseVisualStyleBackColor = True
        '
        'rdbCusName
        '
        Me.rdbCusName.AutoSize = True
        Me.rdbCusName.Location = New System.Drawing.Point(9, 72)
        Me.rdbCusName.Name = "rdbCusName"
        Me.rdbCusName.Size = New System.Drawing.Size(63, 17)
        Me.rdbCusName.TabIndex = 2
        Me.rdbCusName.Text = "ชื่อลูกค้า"
        Me.rdbCusName.UseVisualStyleBackColor = True
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(15, 150)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpDate.TabIndex = 4
        '
        'txtKeySearch
        '
        Me.txtKeySearch.Location = New System.Drawing.Point(15, 150)
        Me.txtKeySearch.Name = "txtKeySearch"
        Me.txtKeySearch.Size = New System.Drawing.Size(100, 20)
        Me.txtKeySearch.TabIndex = 6
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(15, 220)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "   ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'rdbAdjust_No
        '
        Me.rdbAdjust_No.AutoSize = True
        Me.rdbAdjust_No.Location = New System.Drawing.Point(9, 49)
        Me.rdbAdjust_No.Name = "rdbAdjust_No"
        Me.rdbAdjust_No.Size = New System.Drawing.Size(102, 17)
        Me.rdbAdjust_No.TabIndex = 1
        Me.rdbAdjust_No.Text = "เลขที่ใบตรวจนับ"
        Me.rdbAdjust_No.UseVisualStyleBackColor = True
        '
        'rdbAdjust_Date
        '
        Me.rdbAdjust_Date.AutoSize = True
        Me.rdbAdjust_Date.Checked = True
        Me.rdbAdjust_Date.Location = New System.Drawing.Point(9, 26)
        Me.rdbAdjust_Date.Name = "rdbAdjust_Date"
        Me.rdbAdjust_Date.Size = New System.Drawing.Size(100, 17)
        Me.rdbAdjust_Date.TabIndex = 0
        Me.rdbAdjust_Date.TabStop = True
        Me.rdbAdjust_Date.Text = "วันที่ออกเอกสาร"
        Me.rdbAdjust_Date.UseVisualStyleBackColor = True
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.ChkActive)
        Me.gbFilter.Controls.Add(Me.lblFilterStatus)
        Me.gbFilter.Controls.Add(Me.cboDocumentStatus)
        Me.gbFilter.Location = New System.Drawing.Point(4, 273)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(131, 120)
        Me.gbFilter.TabIndex = 1
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "กรองรายการ"
        '
        'ChkActive
        '
        Me.ChkActive.AutoSize = True
        Me.ChkActive.Checked = True
        Me.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkActive.Location = New System.Drawing.Point(9, 66)
        Me.ChkActive.Name = "ChkActive"
        Me.ChkActive.Size = New System.Drawing.Size(61, 17)
        Me.ChkActive.TabIndex = 310
        Me.ChkActive.Text = "งานค้าง"
        Me.ChkActive.UseVisualStyleBackColor = True
        '
        'lblFilterStatus
        '
        Me.lblFilterStatus.AutoSize = True
        Me.lblFilterStatus.Location = New System.Drawing.Point(8, 23)
        Me.lblFilterStatus.Name = "lblFilterStatus"
        Me.lblFilterStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblFilterStatus.TabIndex = 0
        Me.lblFilterStatus.Text = "สถานะเอกสาร"
        '
        'cboDocumentStatus
        '
        Me.cboDocumentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentStatus.FormattingEnabled = True
        Me.cboDocumentStatus.Location = New System.Drawing.Point(7, 39)
        Me.cboDocumentStatus.Name = "cboDocumentStatus"
        Me.cboDocumentStatus.Size = New System.Drawing.Size(115, 21)
        Me.cboDocumentStatus.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(901, 622)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "    ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lbCountRows
        '
        Me.lbCountRows.AutoSize = True
        Me.lbCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows.Location = New System.Drawing.Point(146, 607)
        Me.lbCountRows.Name = "lbCountRows"
        Me.lbCountRows.Size = New System.Drawing.Size(71, 13)
        Me.lbCountRows.TabIndex = 2
        Me.lbCountRows.Text = "ไม่พบรายการ"
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(579, 622)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(111, 38)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "       ยกเลิก"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnShowItemList
        '
        Me.btnShowItemList.Enabled = False
        Me.btnShowItemList.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.btnShowItemList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShowItemList.Location = New System.Drawing.Point(469, 622)
        Me.btnShowItemList.Name = "btnShowItemList"
        Me.btnShowItemList.Size = New System.Drawing.Size(110, 38)
        Me.btnShowItemList.TabIndex = 5
        Me.btnShowItemList.Text = "       ผลการตรวจนับ"
        Me.btnShowItemList.UseVisualStyleBackColor = True
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
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
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
        Me.DataGridViewTextBoxColumn5.Width = 170
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
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
        Me.DataGridViewTextBoxColumn8.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn8.HeaderText = "AssignJob_Index"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "ผู้ปฎิบัตงาน"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Status_Id"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Status_Id"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'chkall
        '
        Me.chkall.AutoSize = True
        Me.chkall.Location = New System.Drawing.Point(150, 16)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(15, 14)
        Me.chkall.TabIndex = 15
        Me.chkall.UseVisualStyleBackColor = True
        '
        'btnConfirm
        '
        Me.btnConfirm.Enabled = False
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(359, 622)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(110, 38)
        Me.btnConfirm.TabIndex = 65
        Me.btnConfirm.Text = "      ยืนยันรายการ"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAssignJob.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.btnAssignJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAssignJob.Location = New System.Drawing.Point(696, 622)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(107, 38)
        Me.btnAssignJob.TabIndex = 66
        Me.btnAssignJob.Text = "มอบหมายงาน"
        Me.btnAssignJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAssignJob.UseVisualStyleBackColor = True
        Me.btnAssignJob.Visible = False
        '
        'frmAdjustView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 708)
        Me.Controls.Add(Me.btnAssignJob)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.chkall)
        Me.Controls.Add(Me.lbCountRows)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnShowItemList)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.grdAdjustView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmAdjustView"
        Me.ShowInTaskbar = False
        Me.Text = "รายการใบตรวจนับ"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAdjustView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuAssignJob.ResumeLayout(False)
        Me.pnLeftmenu.ResumeLayout(False)
        Me.gbPrintOut.ResumeLayout(False)
        Me.gbCondition.ResumeLayout(False)
        Me.gbCondition.PerformLayout()
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdAdjustView As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtKeySearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rdbAdjust_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAdjust_Date As System.Windows.Forms.RadioButton
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents lblFilterStatus As System.Windows.Forms.Label
    Friend WithEvents cboDocumentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rdbRef1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCusName As System.Windows.Forms.RadioButton
    Friend WithEvents dateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents L_to As System.Windows.Forms.Label
    Friend WithEvents gbPrintOut As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.ComboBox
    Friend WithEvents lbCountRows As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnShowItemList As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuAssignJob As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AssignToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PriorityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VeryHightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NornalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HOLDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_PrintAdjust_Act As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChkActive As System.Windows.Forms.CheckBox
    Friend WithEvents chkall As System.Windows.Forms.CheckBox
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents Col_chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents System_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DocumentType_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Location As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref_No1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Add_by As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UserName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AssignJob_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAssignJob As System.Windows.Forms.Button
End Class
