<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_Order_StockBalance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_Order_StockBalance))
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbFile = New System.Windows.Forms.GroupBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtWorkSheet = New System.Windows.Forms.TextBox
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.btnCheckData = New System.Windows.Forms.Button
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.grbDetail = New System.Windows.Forms.GroupBox
        Me.pnlProgressBar = New System.Windows.Forms.Panel
        Me.lblProgress = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.grbDefaultValue = New System.Windows.Forms.GroupBox
        Me.lblSupplier_Index = New System.Windows.Forms.Label
        Me.txtSupplier_Id = New System.Windows.Forms.TextBox
        Me.txtSupplier_Name = New System.Windows.Forms.TextBox
        Me.btnSeachSupplier = New System.Windows.Forms.Button
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.lblCountRows = New System.Windows.Forms.Label
        Me.btnDeleteFailAll = New System.Windows.Forms.Button
        Me.BwgImport = New System.ComponentModel.BackgroundWorker
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
        Me.col_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Check_Data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RECEIVE_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RECEIVE_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LOT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SKU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EAN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PALLET_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LOCATION_ALIAS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MFG_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EXP_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.QTY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UOM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ITEMSTATUS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.REF_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.WGT_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.WGT_KG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.VOL_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.VOL_M3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ERP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PRICEUNIT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PRICE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_INVOICE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Sku_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Package_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ItemStatus_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocumentType_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbFile.SuspendLayout()
        Me.grbDetail.SuspendLayout()
        Me.pnlProgressBar.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbDefaultValue.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbFile
        '
        Me.grbFile.Controls.Add(Me.btnBrowse)
        Me.grbFile.Controls.Add(Me.txtWorkSheet)
        Me.grbFile.Controls.Add(Me.txtFilePath)
        Me.grbFile.Controls.Add(Me.Label1)
        Me.grbFile.Controls.Add(Me.lblFilePath)
        Me.grbFile.Location = New System.Drawing.Point(415, 12)
        Me.grbFile.Name = "grbFile"
        Me.grbFile.Size = New System.Drawing.Size(451, 128)
        Me.grbFile.TabIndex = 0
        Me.grbFile.TabStop = False
        Me.grbFile.Text = "เลือก File "
        '
        'btnBrowse
        '
        Me.btnBrowse.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ดึงข้อมูล
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowse.Location = New System.Drawing.Point(368, 20)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(77, 38)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "เลือก..."
        Me.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtWorkSheet
        '
        Me.txtWorkSheet.Location = New System.Drawing.Point(100, 46)
        Me.txtWorkSheet.Name = "txtWorkSheet"
        Me.txtWorkSheet.Size = New System.Drawing.Size(262, 20)
        Me.txtWorkSheet.TabIndex = 1
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(100, 20)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(262, 20)
        Me.txtFilePath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ชื่อ Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(6, 23)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(87, 13)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "ระบุที่อยู่ของ File :"
        '
        'btnCheckData
        '
        Me.btnCheckData.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCheckData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCheckData.Location = New System.Drawing.Point(872, 12)
        Me.btnCheckData.Name = "btnCheckData"
        Me.btnCheckData.Size = New System.Drawing.Size(142, 113)
        Me.btnCheckData.TabIndex = 5
        Me.btnCheckData.Text = "ตรวจสอบ" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ข้อมูล"
        Me.btnCheckData.UseVisualStyleBackColor = True
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(123, 69)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(262, 21)
        Me.cboDocumentType.TabIndex = 1
        '
        'grbDetail
        '
        Me.grbDetail.Controls.Add(Me.pnlProgressBar)
        Me.grbDetail.Controls.Add(Me.grdPreviewData)
        Me.grbDetail.Location = New System.Drawing.Point(12, 149)
        Me.grbDetail.Name = "grbDetail"
        Me.grbDetail.Size = New System.Drawing.Size(1002, 488)
        Me.grbDetail.TabIndex = 2
        Me.grbDetail.TabStop = False
        Me.grbDetail.Text = "Preview Data"
        '
        'pnlProgressBar
        '
        Me.pnlProgressBar.BackColor = System.Drawing.Color.White
        Me.pnlProgressBar.Controls.Add(Me.lblProgress)
        Me.pnlProgressBar.Controls.Add(Me.ProgressBar1)
        Me.pnlProgressBar.Location = New System.Drawing.Point(305, 202)
        Me.pnlProgressBar.Name = "pnlProgressBar"
        Me.pnlProgressBar.Size = New System.Drawing.Size(422, 100)
        Me.pnlProgressBar.TabIndex = 80
        Me.pnlProgressBar.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(11, 13)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(136, 16)
        Me.lblProgress.TabIndex = 9
        Me.lblProgress.Tag = "ระบบกำลังประมวลผล..."
        Me.lblProgress.Text = "ระบบกำลังประมวลผล..."
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(14, 32)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(393, 48)
        Me.ProgressBar1.TabIndex = 8
        '
        'grdPreviewData
        '
        Me.grdPreviewData.AllowUserToAddRows = False
        Me.grdPreviewData.BackgroundColor = System.Drawing.Color.White
        Me.grdPreviewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPreviewData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_No, Me.Check_Data, Me.RECEIVE_DATE, Me.RECEIVE_NO, Me.LOT, Me.SKU, Me.EAN, Me.PALLET_ID, Me.LOCATION_ALIAS, Me.MFG_DATE, Me.EXP_DATE, Me.QTY, Me.UOM, Me.ITEMSTATUS, Me.REF_NO, Me.WGT_UOM, Me.WGT_KG, Me.VOL_UOM, Me.VOL_M3, Me.col_ERP, Me.col_PRICEUNIT, Me.col_PRICE, Me.col_INVOICE, Me.Sku_Index, Me.Package_Index, Me.Customer_Index, Me.Supplier_Index, Me.ItemStatus_Index, Me.DocumentType_Index})
        Me.grdPreviewData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPreviewData.Location = New System.Drawing.Point(3, 16)
        Me.grdPreviewData.Name = "grdPreviewData"
        Me.grdPreviewData.Size = New System.Drawing.Size(996, 469)
        Me.grdPreviewData.TabIndex = 0
        '
        'btnImport
        '
        Me.btnImport.Enabled = False
        Me.btnImport.Image = CType(resources.GetObject("btnImport.Image"), System.Drawing.Image)
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(21, 661)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(107, 38)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "เริ่มนำเข้าข้อมูล"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(904, 661)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls"
        Me.OpenFileDialog1.Filter = "Excel File|*.xls"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'grbDefaultValue
        '
        Me.grbDefaultValue.Controls.Add(Me.lblSupplier_Index)
        Me.grbDefaultValue.Controls.Add(Me.txtSupplier_Id)
        Me.grbDefaultValue.Controls.Add(Me.txtSupplier_Name)
        Me.grbDefaultValue.Controls.Add(Me.btnSeachSupplier)
        Me.grbDefaultValue.Controls.Add(Me.txtCustomer_Id)
        Me.grbDefaultValue.Controls.Add(Me.txtCustomer_Name)
        Me.grbDefaultValue.Controls.Add(Me.btnCustomer)
        Me.grbDefaultValue.Controls.Add(Me.Label6)
        Me.grbDefaultValue.Controls.Add(Me.cboDocumentType)
        Me.grbDefaultValue.Controls.Add(Me.Label7)
        Me.grbDefaultValue.Location = New System.Drawing.Point(12, 12)
        Me.grbDefaultValue.Name = "grbDefaultValue"
        Me.grbDefaultValue.Size = New System.Drawing.Size(397, 128)
        Me.grbDefaultValue.TabIndex = 1
        Me.grbDefaultValue.TabStop = False
        Me.grbDefaultValue.Text = "กำหนดค่าเริ่มต้น"
        '
        'lblSupplier_Index
        '
        Me.lblSupplier_Index.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSupplier_Index.Location = New System.Drawing.Point(7, 47)
        Me.lblSupplier_Index.Name = "lblSupplier_Index"
        Me.lblSupplier_Index.Size = New System.Drawing.Size(109, 13)
        Me.lblSupplier_Index.TabIndex = 20
        Me.lblSupplier_Index.Text = "Source :"
        Me.lblSupplier_Index.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSupplier_Id
        '
        Me.txtSupplier_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplier_Id.Location = New System.Drawing.Point(124, 43)
        Me.txtSupplier_Id.MaxLength = 100
        Me.txtSupplier_Id.Name = "txtSupplier_Id"
        Me.txtSupplier_Id.ReadOnly = True
        Me.txtSupplier_Id.Size = New System.Drawing.Size(111, 20)
        Me.txtSupplier_Id.TabIndex = 19
        '
        'txtSupplier_Name
        '
        Me.txtSupplier_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplier_Name.Location = New System.Drawing.Point(264, 43)
        Me.txtSupplier_Name.Name = "txtSupplier_Name"
        Me.txtSupplier_Name.ReadOnly = True
        Me.txtSupplier_Name.Size = New System.Drawing.Size(120, 20)
        Me.txtSupplier_Name.TabIndex = 21
        Me.txtSupplier_Name.TabStop = False
        '
        'btnSeachSupplier
        '
        Me.btnSeachSupplier.Location = New System.Drawing.Point(238, 42)
        Me.btnSeachSupplier.Name = "btnSeachSupplier"
        Me.btnSeachSupplier.Size = New System.Drawing.Size(24, 23)
        Me.btnSeachSupplier.TabIndex = 22
        Me.btnSeachSupplier.TabStop = False
        Me.btnSeachSupplier.Text = "..."
        Me.btnSeachSupplier.UseVisualStyleBackColor = True
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(124, 19)
        Me.txtCustomer_Id.MaxLength = 100
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(111, 20)
        Me.txtCustomer_Id.TabIndex = 17
        Me.txtCustomer_Id.TabStop = False
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Name.Location = New System.Drawing.Point(265, 19)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomer_Name.TabIndex = 18
        Me.txtCustomer_Name.TabStop = False
        '
        'btnCustomer
        '
        Me.btnCustomer.Location = New System.Drawing.Point(237, 18)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomer.TabIndex = 16
        Me.btnCustomer.Text = "..."
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label6.Location = New System.Drawing.Point(72, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "ลูกค้า :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "ประเภทเอกสาร :"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(134, 661)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(113, 38)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "ลบ(Key Delete)"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'lblCountRows
        '
        Me.lblCountRows.AutoSize = True
        Me.lblCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lblCountRows.Location = New System.Drawing.Point(18, 641)
        Me.lblCountRows.Name = "lblCountRows"
        Me.lblCountRows.Size = New System.Drawing.Size(71, 13)
        Me.lblCountRows.TabIndex = 6
        Me.lblCountRows.Text = "ไม่พบรายการ"
        '
        'btnDeleteFailAll
        '
        Me.btnDeleteFailAll.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDeleteFailAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDeleteFailAll.Location = New System.Drawing.Point(253, 661)
        Me.btnDeleteFailAll.Name = "btnDeleteFailAll"
        Me.btnDeleteFailAll.Size = New System.Drawing.Size(144, 38)
        Me.btnDeleteFailAll.TabIndex = 7
        Me.btnDeleteFailAll.Text = "ลบรายการที่ผิดพลาด"
        Me.btnDeleteFailAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDeleteFailAll.UseVisualStyleBackColor = True
        '
        'BwgImport
        '
        Me.BwgImport.WorkerReportsProgress = True
        Me.BwgImport.WorkerSupportsCancellation = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Line_No"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Check_Data"
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "ตรวจสอบ"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "RECEIVE_DATE"
        Me.DataGridViewTextBoxColumn3.HeaderText = "RECEIVE_DATE"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "RECEIVE_NO"
        Me.DataGridViewTextBoxColumn4.HeaderText = "RECEIVE_NO"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "LOT"
        Me.DataGridViewTextBoxColumn5.HeaderText = "LOT"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "SKU"
        Me.DataGridViewTextBoxColumn6.HeaderText = "SKU"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "EAN"
        Me.DataGridViewTextBoxColumn7.HeaderText = "EAN"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "PALLET_ID"
        Me.DataGridViewTextBoxColumn8.HeaderText = "PALLET_ID"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "LOCATION_ALIAS"
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn9.HeaderText = "LOCATION_ALIAS"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "EXP_DATE"
        Me.DataGridViewTextBoxColumn10.HeaderText = "EXP_DATE"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "QTY"
        Me.DataGridViewTextBoxColumn11.HeaderText = "QTY"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "UOM"
        DataGridViewCellStyle8.Format = "N4"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn12.HeaderText = "UOM"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "ITEMSTATUS"
        DataGridViewCellStyle9.Format = "N4"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn13.HeaderText = "ITEMSTATUS"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "REF_NO"
        DataGridViewCellStyle10.Format = "N4"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn14.HeaderText = "REF_NO"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "WGT_UOM"
        Me.DataGridViewTextBoxColumn15.HeaderText = "WGT_UOM"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "WGT_KG"
        Me.DataGridViewTextBoxColumn16.HeaderText = "WGT_KG"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "VOL_UOM"
        Me.DataGridViewTextBoxColumn17.HeaderText = "VOL_UOM"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "VOL_M3"
        Me.DataGridViewTextBoxColumn18.HeaderText = "VOL_M3"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "MFG_DATE"
        Me.DataGridViewTextBoxColumn19.HeaderText = "MFG_DATE"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "ERP"
        Me.DataGridViewTextBoxColumn20.HeaderText = "ERP"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.HeaderText = "PRICEUNIT"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "PRICE"
        Me.DataGridViewTextBoxColumn22.HeaderText = "PRICE"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "INVOICE"
        Me.DataGridViewTextBoxColumn23.HeaderText = "INVOICE"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "Sku_Index"
        Me.DataGridViewTextBoxColumn24.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn24.Visible = False
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "Package_Index"
        Me.DataGridViewTextBoxColumn25.HeaderText = "Package_Index"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn25.Visible = False
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "Customer_Index"
        Me.DataGridViewTextBoxColumn26.HeaderText = "Customer_Index"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn26.Visible = False
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Supplier_Index"
        Me.DataGridViewTextBoxColumn27.HeaderText = "Supplier_Index"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn27.Visible = False
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "ItemStatus_Index"
        Me.DataGridViewTextBoxColumn28.HeaderText = "ItemStatus_Index"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.Visible = False
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "DocumentType_Index"
        Me.DataGridViewTextBoxColumn29.HeaderText = "DocumentType_Index"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.Visible = False
        '
        'col_No
        '
        Me.col_No.DataPropertyName = "Line_No"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.col_No.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_No.Frozen = True
        Me.col_No.HeaderText = "No."
        Me.col_No.Name = "col_No"
        Me.col_No.Width = 40
        '
        'Check_Data
        '
        Me.Check_Data.DataPropertyName = "Check_Data"
        Me.Check_Data.Frozen = True
        Me.Check_Data.HeaderText = "ตรวจสอบ"
        Me.Check_Data.Name = "Check_Data"
        Me.Check_Data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Check_Data.Width = 150
        '
        'RECEIVE_DATE
        '
        Me.RECEIVE_DATE.DataPropertyName = "RECEIVE_DATE"
        Me.RECEIVE_DATE.HeaderText = "RECEIVE_DATE"
        Me.RECEIVE_DATE.Name = "RECEIVE_DATE"
        Me.RECEIVE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RECEIVE_NO
        '
        Me.RECEIVE_NO.DataPropertyName = "RECEIVE_NO"
        Me.RECEIVE_NO.HeaderText = "RECEIVE_NO"
        Me.RECEIVE_NO.Name = "RECEIVE_NO"
        Me.RECEIVE_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'LOT
        '
        Me.LOT.DataPropertyName = "LOT"
        Me.LOT.HeaderText = "LOT"
        Me.LOT.Name = "LOT"
        Me.LOT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SKU
        '
        Me.SKU.DataPropertyName = "SKU"
        Me.SKU.HeaderText = "SKU"
        Me.SKU.Name = "SKU"
        Me.SKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'EAN
        '
        Me.EAN.DataPropertyName = "EAN"
        Me.EAN.HeaderText = "EAN"
        Me.EAN.Name = "EAN"
        Me.EAN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PALLET_ID
        '
        Me.PALLET_ID.DataPropertyName = "PALLET_ID"
        Me.PALLET_ID.HeaderText = "PALLET_ID"
        Me.PALLET_ID.Name = "PALLET_ID"
        Me.PALLET_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'LOCATION_ALIAS
        '
        Me.LOCATION_ALIAS.DataPropertyName = "LOCATION_ALIAS"
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.LOCATION_ALIAS.DefaultCellStyle = DataGridViewCellStyle2
        Me.LOCATION_ALIAS.HeaderText = "LOCATION_ALIAS"
        Me.LOCATION_ALIAS.Name = "LOCATION_ALIAS"
        Me.LOCATION_ALIAS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MFG_DATE
        '
        Me.MFG_DATE.DataPropertyName = "MFG_DATE"
        Me.MFG_DATE.HeaderText = "MFG_DATE"
        Me.MFG_DATE.Name = "MFG_DATE"
        '
        'EXP_DATE
        '
        Me.EXP_DATE.DataPropertyName = "EXP_DATE"
        Me.EXP_DATE.HeaderText = "EXP_DATE"
        Me.EXP_DATE.Name = "EXP_DATE"
        '
        'QTY
        '
        Me.QTY.DataPropertyName = "QTY"
        Me.QTY.HeaderText = "QTY"
        Me.QTY.Name = "QTY"
        Me.QTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UOM
        '
        Me.UOM.DataPropertyName = "UOM"
        DataGridViewCellStyle3.Format = "N4"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.UOM.DefaultCellStyle = DataGridViewCellStyle3
        Me.UOM.HeaderText = "UOM"
        Me.UOM.Name = "UOM"
        '
        'ITEMSTATUS
        '
        Me.ITEMSTATUS.DataPropertyName = "ITEMSTATUS"
        DataGridViewCellStyle4.Format = "N4"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ITEMSTATUS.DefaultCellStyle = DataGridViewCellStyle4
        Me.ITEMSTATUS.HeaderText = "ITEMSTATUS"
        Me.ITEMSTATUS.Name = "ITEMSTATUS"
        Me.ITEMSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'REF_NO
        '
        Me.REF_NO.DataPropertyName = "REF_NO"
        DataGridViewCellStyle5.Format = "N4"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.REF_NO.DefaultCellStyle = DataGridViewCellStyle5
        Me.REF_NO.HeaderText = "REF_NO"
        Me.REF_NO.Name = "REF_NO"
        '
        'WGT_UOM
        '
        Me.WGT_UOM.DataPropertyName = "WGT_UOM"
        Me.WGT_UOM.HeaderText = "WGT_UOM"
        Me.WGT_UOM.Name = "WGT_UOM"
        Me.WGT_UOM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'WGT_KG
        '
        Me.WGT_KG.DataPropertyName = "WGT_KG"
        Me.WGT_KG.HeaderText = "WGT_KG"
        Me.WGT_KG.Name = "WGT_KG"
        Me.WGT_KG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'VOL_UOM
        '
        Me.VOL_UOM.DataPropertyName = "VOL_UOM"
        Me.VOL_UOM.HeaderText = "VOL_UOM"
        Me.VOL_UOM.Name = "VOL_UOM"
        '
        'VOL_M3
        '
        Me.VOL_M3.DataPropertyName = "VOL_M3"
        Me.VOL_M3.HeaderText = "VOL_M3"
        Me.VOL_M3.Name = "VOL_M3"
        '
        'col_ERP
        '
        Me.col_ERP.DataPropertyName = "ERP"
        Me.col_ERP.HeaderText = "ERP"
        Me.col_ERP.Name = "col_ERP"
        '
        'col_PRICEUNIT
        '
        Me.col_PRICEUNIT.HeaderText = "PRICEUNIT"
        Me.col_PRICEUNIT.Name = "col_PRICEUNIT"
        '
        'col_PRICE
        '
        Me.col_PRICE.DataPropertyName = "PRICE"
        Me.col_PRICE.HeaderText = "PRICE"
        Me.col_PRICE.Name = "col_PRICE"
        '
        'col_INVOICE
        '
        Me.col_INVOICE.DataPropertyName = "INVOICE"
        Me.col_INVOICE.HeaderText = "INVOICE"
        Me.col_INVOICE.Name = "col_INVOICE"
        '
        'Sku_Index
        '
        Me.Sku_Index.DataPropertyName = "Sku_Index"
        Me.Sku_Index.HeaderText = "Sku_Index"
        Me.Sku_Index.Name = "Sku_Index"
        Me.Sku_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sku_Index.Visible = False
        '
        'Package_Index
        '
        Me.Package_Index.DataPropertyName = "Package_Index"
        Me.Package_Index.HeaderText = "Package_Index"
        Me.Package_Index.Name = "Package_Index"
        Me.Package_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Package_Index.Visible = False
        '
        'Customer_Index
        '
        Me.Customer_Index.DataPropertyName = "Customer_Index"
        Me.Customer_Index.HeaderText = "Customer_Index"
        Me.Customer_Index.Name = "Customer_Index"
        Me.Customer_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Customer_Index.Visible = False
        '
        'Supplier_Index
        '
        Me.Supplier_Index.DataPropertyName = "Supplier_Index"
        Me.Supplier_Index.HeaderText = "Supplier_Index"
        Me.Supplier_Index.Name = "Supplier_Index"
        Me.Supplier_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Supplier_Index.Visible = False
        '
        'ItemStatus_Index
        '
        Me.ItemStatus_Index.DataPropertyName = "ItemStatus_Index"
        Me.ItemStatus_Index.HeaderText = "ItemStatus_Index"
        Me.ItemStatus_Index.Name = "ItemStatus_Index"
        Me.ItemStatus_Index.Visible = False
        '
        'DocumentType_Index
        '
        Me.DocumentType_Index.DataPropertyName = "DocumentType_Index"
        Me.DocumentType_Index.HeaderText = "DocumentType_Index"
        Me.DocumentType_Index.Name = "DocumentType_Index"
        Me.DocumentType_Index.Visible = False
        '
        'frmImport_Order_StockBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 714)
        Me.Controls.Add(Me.btnCheckData)
        Me.Controls.Add(Me.btnDeleteFailAll)
        Me.Controls.Add(Me.lblCountRows)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grbDefaultValue)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.grbDetail)
        Me.Controls.Add(Me.grbFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmImport_Order_StockBalance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "นำเข้าข้อมูลรับสินค้า"
        Me.grbFile.ResumeLayout(False)
        Me.grbFile.PerformLayout()
        Me.grbDetail.ResumeLayout(False)
        Me.pnlProgressBar.ResumeLayout(False)
        Me.pnlProgressBar.PerformLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbDefaultValue.ResumeLayout(False)
        Me.grbDefaultValue.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grbFile As System.Windows.Forms.GroupBox
    Friend WithEvents grbDetail As System.Windows.Forms.GroupBox
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdPreviewData As System.Windows.Forms.DataGridView
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWorkSheet As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents grbDefaultValue As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnCheckData As System.Windows.Forms.Button
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lblCountRows As System.Windows.Forms.Label
    Friend WithEvents btnDeleteFailAll As System.Windows.Forms.Button
    Friend WithEvents pnlProgressBar As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BwgImport As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblSupplier_Index As System.Windows.Forms.Label
    Friend WithEvents txtSupplier_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplier_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSeachSupplier As System.Windows.Forms.Button
    Friend WithEvents col_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Check_Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RECEIVE_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RECEIVE_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SKU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EAN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PALLET_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOCATION_ALIAS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MFG_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EXP_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ITEMSTATUS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents REF_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WGT_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WGT_KG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VOL_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VOL_M3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ERP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PRICEUNIT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PRICE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_INVOICE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sku_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Package_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemStatus_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocumentType_Index As System.Windows.Forms.DataGridViewTextBoxColumn
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
End Class
