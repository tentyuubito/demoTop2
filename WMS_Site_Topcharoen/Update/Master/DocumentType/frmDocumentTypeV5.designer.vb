<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocumentTypeV5
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblDes = New System.Windows.Forms.Label
        Me.lbID = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.cboProcess = New System.Windows.Forms.ComboBox
        Me.lblProcess = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtRef_No3 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtRef_No2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRef_No1 = New System.Windows.Forms.TextBox
        Me.btnSeachLoc = New System.Windows.Forms.Button
        Me.txtItemStatus_Location = New System.Windows.Forms.TextBox
        Me.lblItemStatus_Location = New System.Windows.Forms.Label
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
        Me.lblItemStatsus = New System.Windows.Forms.Label
        Me.grdItemStatus = New System.Windows.Forms.DataGridView
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_DocumentType_ItemStatus_index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_priority = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbItemStatus = New System.Windows.Forms.GroupBox
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtSysValue = New System.Windows.Forms.TextBox
        Me.cboSysKeyName = New System.Windows.Forms.ComboBox
        Me.lblSys = New System.Windows.Forms.Label
        Me.cboFormat_Running = New System.Windows.Forms.ComboBox
        Me.cboFormat_Date = New System.Windows.Forms.ComboBox
        Me.lblFormat_Document = New System.Windows.Forms.Label
        Me.lblFormat_Running = New System.Windows.Forms.Label
        Me.lblFormat_Date = New System.Windows.Forms.Label
        Me.lblReset_Running_by = New System.Windows.Forms.Label
        Me.cboFormat2 = New System.Windows.Forms.ComboBox
        Me.cboFormat1 = New System.Windows.Forms.ComboBox
        Me.txtFormat_Document = New System.Windows.Forms.TextBox
        Me.cboReset_Running_By = New System.Windows.Forms.ComboBox
        Me.chkUseMaxDocument = New System.Windows.Forms.CheckBox
        Me.grbDocumentType.SuspendLayout()
        CType(Me.grdItemStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbItemStatus.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(84, 42)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(70, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "รายละเอียด"
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(125, 20)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(159, 17)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(246, 20)
        Me.txtID.TabIndex = 0
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(159, 39)
        Me.txtDes.MaxLength = 100
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(246, 20)
        Me.txtDes.TabIndex = 1
        '
        'cboProcess
        '
        Me.cboProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProcess.FormattingEnabled = True
        Me.cboProcess.Location = New System.Drawing.Point(159, 62)
        Me.cboProcess.Name = "cboProcess"
        Me.cboProcess.Size = New System.Drawing.Size(246, 21)
        Me.cboProcess.TabIndex = 2
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblProcess.Location = New System.Drawing.Point(75, 65)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(79, 13)
        Me.lblProcess.TabIndex = 11
        Me.lblProcess.Text = "ประเภทของงาน"
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(735, 425)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 44)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(12, 428)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 41)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Controls.Add(Me.Label4)
        Me.grbDocumentType.Controls.Add(Me.txtRef_No3)
        Me.grbDocumentType.Controls.Add(Me.Label3)
        Me.grbDocumentType.Controls.Add(Me.txtRef_No2)
        Me.grbDocumentType.Controls.Add(Me.Label2)
        Me.grbDocumentType.Controls.Add(Me.txtRef_No1)
        Me.grbDocumentType.Controls.Add(Me.btnSeachLoc)
        Me.grbDocumentType.Controls.Add(Me.txtItemStatus_Location)
        Me.grbDocumentType.Controls.Add(Me.lblItemStatus_Location)
        Me.grbDocumentType.Controls.Add(Me.cboItemStatus)
        Me.grbDocumentType.Controls.Add(Me.txtID)
        Me.grbDocumentType.Controls.Add(Me.lblItemStatsus)
        Me.grbDocumentType.Controls.Add(Me.txtDes)
        Me.grbDocumentType.Controls.Add(Me.lbID)
        Me.grbDocumentType.Controls.Add(Me.cboProcess)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Controls.Add(Me.lblProcess)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 4)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(415, 215)
        Me.grbDocumentType.TabIndex = 0
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "ประเภทเอกสาร"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(114, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "อ้างอิง3"
        '
        'txtRef_No3
        '
        Me.txtRef_No3.Location = New System.Drawing.Point(159, 177)
        Me.txtRef_No3.MaxLength = 100
        Me.txtRef_No3.Name = "txtRef_No3"
        Me.txtRef_No3.Size = New System.Drawing.Size(246, 20)
        Me.txtRef_No3.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(114, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "อ้างอิง2"
        '
        'txtRef_No2
        '
        Me.txtRef_No2.Location = New System.Drawing.Point(159, 154)
        Me.txtRef_No2.MaxLength = 100
        Me.txtRef_No2.Name = "txtRef_No2"
        Me.txtRef_No2.Size = New System.Drawing.Size(246, 20)
        Me.txtRef_No2.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(114, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "อ้างอิง1"
        '
        'txtRef_No1
        '
        Me.txtRef_No1.Location = New System.Drawing.Point(159, 131)
        Me.txtRef_No1.MaxLength = 100
        Me.txtRef_No1.Name = "txtRef_No1"
        Me.txtRef_No1.Size = New System.Drawing.Size(246, 20)
        Me.txtRef_No1.TabIndex = 24
        '
        'btnSeachLoc
        '
        Me.btnSeachLoc.Location = New System.Drawing.Point(370, 108)
        Me.btnSeachLoc.Name = "btnSeachLoc"
        Me.btnSeachLoc.Size = New System.Drawing.Size(35, 22)
        Me.btnSeachLoc.TabIndex = 23
        Me.btnSeachLoc.Text = "..."
        Me.btnSeachLoc.UseVisualStyleBackColor = True
        '
        'txtItemStatus_Location
        '
        Me.txtItemStatus_Location.Location = New System.Drawing.Point(159, 109)
        Me.txtItemStatus_Location.MaxLength = 100
        Me.txtItemStatus_Location.Name = "txtItemStatus_Location"
        Me.txtItemStatus_Location.ReadOnly = True
        Me.txtItemStatus_Location.Size = New System.Drawing.Size(205, 20)
        Me.txtItemStatus_Location.TabIndex = 21
        '
        'lblItemStatus_Location
        '
        Me.lblItemStatus_Location.AutoSize = True
        Me.lblItemStatus_Location.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblItemStatus_Location.Location = New System.Drawing.Point(25, 112)
        Me.lblItemStatus_Location.Name = "lblItemStatus_Location"
        Me.lblItemStatus_Location.Size = New System.Drawing.Size(129, 13)
        Me.lblItemStatus_Location.TabIndex = 22
        Me.lblItemStatus_Location.Text = "ตำแหน่งปลายทาง(Default)"
        '
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.FormattingEnabled = True
        Me.cboItemStatus.Location = New System.Drawing.Point(159, 86)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(246, 21)
        Me.cboItemStatus.TabIndex = 19
        '
        'lblItemStatsus
        '
        Me.lblItemStatsus.AutoSize = True
        Me.lblItemStatsus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblItemStatsus.Location = New System.Drawing.Point(8, 89)
        Me.lblItemStatsus.Name = "lblItemStatsus"
        Me.lblItemStatsus.Size = New System.Drawing.Size(146, 13)
        Me.lblItemStatsus.TabIndex = 20
        Me.lblItemStatsus.Text = "สถานะสินค้าปลายทาง(Default)"
        '
        'grdItemStatus
        '
        Me.grdItemStatus.AllowUserToAddRows = False
        Me.grdItemStatus.AllowUserToDeleteRows = False
        Me.grdItemStatus.AllowUserToResizeRows = False
        Me.grdItemStatus.BackgroundColor = System.Drawing.Color.White
        Me.grdItemStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemStatus.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect, Me.col_DocumentType_ItemStatus_index, Me.col_ItemStatus_Index, Me.Col_priority, Me.col_ItemStatus_Id, Me.col_Description})
        Me.grdItemStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItemStatus.Location = New System.Drawing.Point(3, 16)
        Me.grdItemStatus.Name = "grdItemStatus"
        Me.grdItemStatus.RowHeadersVisible = False
        Me.grdItemStatus.Size = New System.Drawing.Size(399, 390)
        Me.grdItemStatus.TabIndex = 3
        '
        'chkSelect
        '
        Me.chkSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.chkSelect.DataPropertyName = "chkSelect"
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkSelect.Width = 19
        '
        'col_DocumentType_ItemStatus_index
        '
        Me.col_DocumentType_ItemStatus_index.DataPropertyName = "DocumentType_ItemStatus_index"
        Me.col_DocumentType_ItemStatus_index.HeaderText = "DocumentType_ItemStatus_index"
        Me.col_DocumentType_ItemStatus_index.Name = "col_DocumentType_ItemStatus_index"
        Me.col_DocumentType_ItemStatus_index.Visible = False
        '
        'col_ItemStatus_Index
        '
        Me.col_ItemStatus_Index.DataPropertyName = "ItemStatus_Index"
        Me.col_ItemStatus_Index.HeaderText = "ItemStatus_Index"
        Me.col_ItemStatus_Index.Name = "col_ItemStatus_Index"
        Me.col_ItemStatus_Index.Visible = False
        '
        'Col_priority
        '
        Me.Col_priority.DataPropertyName = "priority_index"
        Me.Col_priority.HeaderText = "ลำดับความสำคัญ"
        Me.Col_priority.Name = "Col_priority"
        Me.Col_priority.Width = 120
        '
        'col_ItemStatus_Id
        '
        Me.col_ItemStatus_Id.DataPropertyName = "ItemStatus_Id"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_ItemStatus_Id.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_ItemStatus_Id.HeaderText = "รหัสสถานะสินค้า"
        Me.col_ItemStatus_Id.Name = "col_ItemStatus_Id"
        Me.col_ItemStatus_Id.ReadOnly = True
        Me.col_ItemStatus_Id.Width = 150
        '
        'col_Description
        '
        Me.col_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Description.DataPropertyName = "Description"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Description.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_Description.HeaderText = "สถานะสินค้า"
        Me.col_Description.Name = "col_Description"
        Me.col_Description.ReadOnly = True
        '
        'grbItemStatus
        '
        Me.grbItemStatus.Controls.Add(Me.chkAll)
        Me.grbItemStatus.Controls.Add(Me.grdItemStatus)
        Me.grbItemStatus.Location = New System.Drawing.Point(433, 10)
        Me.grbItemStatus.Name = "grbItemStatus"
        Me.grbItemStatus.Size = New System.Drawing.Size(405, 409)
        Me.grbItemStatus.TabIndex = 4
        Me.grbItemStatus.TabStop = False
        Me.grbItemStatus.Text = "สถานะสินค้าที่ใช้งาน"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(8, 20)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(15, 14)
        Me.chkAll.TabIndex = 5
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ItemStatus_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ItemStatus_Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ItemStatus_Id"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัสสถานะสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn3.HeaderText = "สถานะสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Description"
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn4.HeaderText = "สถานะสินค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkUseMaxDocument)
        Me.GroupBox1.Controls.Add(Me.txtSysValue)
        Me.GroupBox1.Controls.Add(Me.cboSysKeyName)
        Me.GroupBox1.Controls.Add(Me.lblSys)
        Me.GroupBox1.Controls.Add(Me.cboFormat_Running)
        Me.GroupBox1.Controls.Add(Me.cboFormat_Date)
        Me.GroupBox1.Controls.Add(Me.lblFormat_Document)
        Me.GroupBox1.Controls.Add(Me.lblFormat_Running)
        Me.GroupBox1.Controls.Add(Me.lblFormat_Date)
        Me.GroupBox1.Controls.Add(Me.lblReset_Running_by)
        Me.GroupBox1.Controls.Add(Me.cboFormat2)
        Me.GroupBox1.Controls.Add(Me.cboFormat1)
        Me.GroupBox1.Controls.Add(Me.txtFormat_Document)
        Me.GroupBox1.Controls.Add(Me.cboReset_Running_By)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 225)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(415, 194)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "รูปแบบเลขที่เอกสาร"
        '
        'txtSysValue
        '
        Me.txtSysValue.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtSysValue.Location = New System.Drawing.Point(268, 116)
        Me.txtSysValue.MaxLength = 100
        Me.txtSysValue.Name = "txtSysValue"
        Me.txtSysValue.ReadOnly = True
        Me.txtSysValue.Size = New System.Drawing.Size(104, 20)
        Me.txtSysValue.TabIndex = 24
        '
        'cboSysKeyName
        '
        Me.cboSysKeyName.FormattingEnabled = True
        Me.cboSysKeyName.Items.AddRange(New Object() {"M", "Y", "RUNNING"})
        Me.cboSysKeyName.Location = New System.Drawing.Point(120, 116)
        Me.cboSysKeyName.Name = "cboSysKeyName"
        Me.cboSysKeyName.Size = New System.Drawing.Size(142, 21)
        Me.cboSysKeyName.TabIndex = 16
        '
        'lblSys
        '
        Me.lblSys.AutoSize = True
        Me.lblSys.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblSys.Location = New System.Drawing.Point(38, 119)
        Me.lblSys.Name = "lblSys"
        Me.lblSys.Size = New System.Drawing.Size(76, 13)
        Me.lblSys.TabIndex = 15
        Me.lblSys.Text = "Sys Key Name"
        '
        'cboFormat_Running
        '
        Me.cboFormat_Running.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormat_Running.FormattingEnabled = True
        Me.cboFormat_Running.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cboFormat_Running.Location = New System.Drawing.Point(120, 66)
        Me.cboFormat_Running.Name = "cboFormat_Running"
        Me.cboFormat_Running.Size = New System.Drawing.Size(142, 21)
        Me.cboFormat_Running.TabIndex = 14
        '
        'cboFormat_Date
        '
        Me.cboFormat_Date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormat_Date.FormattingEnabled = True
        Me.cboFormat_Date.Items.AddRange(New Object() {"yyMM", "MMyy", "ddMMyy", "yyyyMMdd", "ddMMyyyy"})
        Me.cboFormat_Date.Location = New System.Drawing.Point(120, 43)
        Me.cboFormat_Date.Name = "cboFormat_Date"
        Me.cboFormat_Date.Size = New System.Drawing.Size(142, 21)
        Me.cboFormat_Date.TabIndex = 13
        '
        'lblFormat_Document
        '
        Me.lblFormat_Document.AutoSize = True
        Me.lblFormat_Document.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormat_Document.Location = New System.Drawing.Point(13, 20)
        Me.lblFormat_Document.Name = "lblFormat_Document"
        Me.lblFormat_Document.Size = New System.Drawing.Size(101, 13)
        Me.lblFormat_Document.TabIndex = 12
        Me.lblFormat_Document.Text = "รูปแบบเลขที่เอกสาร"
        '
        'lblFormat_Running
        '
        Me.lblFormat_Running.AutoSize = True
        Me.lblFormat_Running.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormat_Running.Location = New System.Drawing.Point(29, 69)
        Me.lblFormat_Running.Name = "lblFormat_Running"
        Me.lblFormat_Running.Size = New System.Drawing.Size(85, 13)
        Me.lblFormat_Running.TabIndex = 12
        Me.lblFormat_Running.Text = "Format_Running"
        '
        'lblFormat_Date
        '
        Me.lblFormat_Date.AutoSize = True
        Me.lblFormat_Date.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblFormat_Date.Location = New System.Drawing.Point(46, 46)
        Me.lblFormat_Date.Name = "lblFormat_Date"
        Me.lblFormat_Date.Size = New System.Drawing.Size(68, 13)
        Me.lblFormat_Date.TabIndex = 12
        Me.lblFormat_Date.Text = "Format_Date"
        '
        'lblReset_Running_by
        '
        Me.lblReset_Running_by.AutoSize = True
        Me.lblReset_Running_by.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblReset_Running_by.Location = New System.Drawing.Point(43, 95)
        Me.lblReset_Running_by.Name = "lblReset_Running_by"
        Me.lblReset_Running_by.Size = New System.Drawing.Size(71, 13)
        Me.lblReset_Running_by.TabIndex = 12
        Me.lblReset_Running_by.Text = "เปลี่ยนค่าตาม"
        '
        'cboFormat2
        '
        Me.cboFormat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormat2.FormattingEnabled = True
        Me.cboFormat2.Items.AddRange(New Object() {"Format_Running", "Format_Date"})
        Me.cboFormat2.Location = New System.Drawing.Point(264, 16)
        Me.cboFormat2.Name = "cboFormat2"
        Me.cboFormat2.Size = New System.Drawing.Size(112, 21)
        Me.cboFormat2.TabIndex = 8
        '
        'cboFormat1
        '
        Me.cboFormat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormat1.FormattingEnabled = True
        Me.cboFormat1.Items.AddRange(New Object() {"Format_Date", "Format_Running"})
        Me.cboFormat1.Location = New System.Drawing.Point(150, 16)
        Me.cboFormat1.Name = "cboFormat1"
        Me.cboFormat1.Size = New System.Drawing.Size(112, 21)
        Me.cboFormat1.TabIndex = 7
        '
        'txtFormat_Document
        '
        Me.txtFormat_Document.Location = New System.Drawing.Point(120, 17)
        Me.txtFormat_Document.MaxLength = 100
        Me.txtFormat_Document.Name = "txtFormat_Document"
        Me.txtFormat_Document.Size = New System.Drawing.Size(28, 20)
        Me.txtFormat_Document.TabIndex = 6
        Me.txtFormat_Document.Text = "O"
        '
        'cboReset_Running_By
        '
        Me.cboReset_Running_By.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReset_Running_By.FormattingEnabled = True
        Me.cboReset_Running_By.Items.AddRange(New Object() {"M", "Y", "RUNNING"})
        Me.cboReset_Running_By.Location = New System.Drawing.Point(120, 90)
        Me.cboReset_Running_By.Name = "cboReset_Running_By"
        Me.cboReset_Running_By.Size = New System.Drawing.Size(142, 21)
        Me.cboReset_Running_By.TabIndex = 3
        '
        'chkUseMaxDocument
        '
        Me.chkUseMaxDocument.AutoSize = True
        Me.chkUseMaxDocument.Location = New System.Drawing.Point(120, 143)
        Me.chkUseMaxDocument.Name = "chkUseMaxDocument"
        Me.chkUseMaxDocument.Size = New System.Drawing.Size(98, 17)
        Me.chkUseMaxDocument.TabIndex = 25
        Me.chkUseMaxDocument.Text = "Max Document"
        Me.chkUseMaxDocument.UseVisualStyleBackColor = True
        '
        'frmDocumentTypeV5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 478)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grbItemStatus)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDocumentTypeV5"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ประเภทเอกสาร"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        CType(Me.grdItemStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbItemStatus.ResumeLayout(False)
        Me.grbItemStatus.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents cboProcess As System.Windows.Forms.ComboBox
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
    Friend WithEvents grdItemStatus As System.Windows.Forms.DataGridView
    Friend WithEvents grbItemStatus As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_DocumentType_ItemStatus_index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_priority As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboReset_Running_By As System.Windows.Forms.ComboBox
    Friend WithEvents cboFormat1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtFormat_Document As System.Windows.Forms.TextBox
    Friend WithEvents cboFormat2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblReset_Running_by As System.Windows.Forms.Label
    Friend WithEvents lblFormat_Document As System.Windows.Forms.Label
    Friend WithEvents lblFormat_Running As System.Windows.Forms.Label
    Friend WithEvents lblFormat_Date As System.Windows.Forms.Label
    Friend WithEvents cboFormat_Running As System.Windows.Forms.ComboBox
    Friend WithEvents cboFormat_Date As System.Windows.Forms.ComboBox
    Friend WithEvents btnSeachLoc As System.Windows.Forms.Button
    Friend WithEvents txtItemStatus_Location As System.Windows.Forms.TextBox
    Friend WithEvents lblItemStatus_Location As System.Windows.Forms.Label
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblItemStatsus As System.Windows.Forms.Label
    Friend WithEvents txtSysValue As System.Windows.Forms.TextBox
    Friend WithEvents cboSysKeyName As System.Windows.Forms.ComboBox
    Friend WithEvents lblSys As System.Windows.Forms.Label
    Friend WithEvents txtRef_No1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRef_No3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRef_No2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkUseMaxDocument As System.Windows.Forms.CheckBox
End Class
