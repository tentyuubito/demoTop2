<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_Order_StockBalance_std
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_Order_StockBalance_std))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbFile = New System.Windows.Forms.GroupBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtWorkSheet = New System.Windows.Forms.TextBox
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.btnCheckData = New System.Windows.Forms.Button
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
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
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.lblCountRows = New System.Windows.Forms.Label
        Me.btnDeleteFailAll = New System.Windows.Forms.Button
        Me.BwgImport = New System.ComponentModel.BackgroundWorker
        Me.col_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Check_Data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Order_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Ref_No3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SKU_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Plot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Str4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Exp_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ItemStatus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Ref_No2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Flo1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.M3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Epson_ProductGroup = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Sku_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Package_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ItemStatus_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocumentType_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Epson_ProductGroup_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Weight_PerPackage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.M3_PerPackage = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.btnBrowse.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
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
        Me.txtWorkSheet.Text = "Sheet1"
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
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.FormattingEnabled = True
        Me.cboItemStatus.Location = New System.Drawing.Point(123, 96)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(262, 21)
        Me.cboItemStatus.TabIndex = 2
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
        Me.grdPreviewData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_No, Me.Check_Data, Me.Order_Date, Me.Ref_No3, Me.SKU_Id, Me.Plot, Me.Str4, Me.Exp_Date, Me.Qty, Me.ItemStatus, Me.Ref_No2, Me.Flo1, Me.Weight, Me.M3, Me.Epson_ProductGroup, Me.Sku_Index, Me.Package_Index, Me.Customer_Index, Me.Supplier_Index, Me.ItemStatus_Index, Me.DocumentType_Index, Me.Epson_ProductGroup_Index, Me.Weight_PerPackage, Me.M3_PerPackage})
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
        Me.grbDefaultValue.Controls.Add(Me.cboItemStatus)
        Me.grbDefaultValue.Controls.Add(Me.Label5)
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
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "สถานะสินค้า :"
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
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
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
        Me.btnDeleteFailAll.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
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
        'Order_Date
        '
        Me.Order_Date.DataPropertyName = "Order_Date"
        Me.Order_Date.HeaderText = "วันที่รับ"
        Me.Order_Date.Name = "Order_Date"
        Me.Order_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Ref_No3
        '
        Me.Ref_No3.DataPropertyName = "Ref_No3"
        Me.Ref_No3.HeaderText = "Container No."
        Me.Ref_No3.Name = "Ref_No3"
        Me.Ref_No3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SKU_Id
        '
        Me.SKU_Id.DataPropertyName = "SKU_Id"
        Me.SKU_Id.HeaderText = "รหัสสินค้า"
        Me.SKU_Id.Name = "SKU_Id"
        Me.SKU_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Plot
        '
        Me.Plot.DataPropertyName = "Plot"
        Me.Plot.HeaderText = "Lot/Batch"
        Me.Plot.Name = "Plot"
        Me.Plot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Str4
        '
        Me.Str4.DataPropertyName = "Str4"
        Me.Str4.HeaderText = "ตำแหน่ง"
        Me.Str4.Name = "Str4"
        Me.Str4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Exp_Date
        '
        Me.Exp_Date.DataPropertyName = "Exp_Date"
        Me.Exp_Date.HeaderText = "วันหมดอายุ"
        Me.Exp_Date.Name = "Exp_Date"
        Me.Exp_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Qty
        '
        Me.Qty.DataPropertyName = "Qty"
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Qty.DefaultCellStyle = DataGridViewCellStyle2
        Me.Qty.HeaderText = "จำนวน"
        Me.Qty.Name = "Qty"
        Me.Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ItemStatus
        '
        Me.ItemStatus.DataPropertyName = "ItemStatus"
        Me.ItemStatus.HeaderText = "สถานะสินค้า"
        Me.ItemStatus.Name = "ItemStatus"
        '
        'Ref_No2
        '
        Me.Ref_No2.DataPropertyName = "Ref_No2"
        Me.Ref_No2.HeaderText = "Receiving INV.NO."
        Me.Ref_No2.Name = "Ref_No2"
        Me.Ref_No2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Flo1
        '
        Me.Flo1.DataPropertyName = "Flo1"
        DataGridViewCellStyle3.Format = "N4"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Flo1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Flo1.HeaderText = "In-Net Weight"
        Me.Flo1.Name = "Flo1"
        '
        'Weight
        '
        Me.Weight.DataPropertyName = "Weight"
        DataGridViewCellStyle4.Format = "N4"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Weight.DefaultCellStyle = DataGridViewCellStyle4
        Me.Weight.HeaderText = "IN-Gross Weight/KGS"
        Me.Weight.Name = "Weight"
        Me.Weight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'M3
        '
        Me.M3.DataPropertyName = "M3"
        DataGridViewCellStyle5.Format = "N4"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.M3.DefaultCellStyle = DataGridViewCellStyle5
        Me.M3.HeaderText = "In-M3"
        Me.M3.Name = "M3"
        '
        'Epson_ProductGroup
        '
        Me.Epson_ProductGroup.DataPropertyName = "Epson_ProductGroup"
        Me.Epson_ProductGroup.HeaderText = "EPSON LOCATION"
        Me.Epson_ProductGroup.Name = "Epson_ProductGroup"
        Me.Epson_ProductGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        'Epson_ProductGroup_Index
        '
        Me.Epson_ProductGroup_Index.DataPropertyName = "Epson_ProductGroup_Index"
        Me.Epson_ProductGroup_Index.HeaderText = "Epson_ProductGroup_Index"
        Me.Epson_ProductGroup_Index.Name = "Epson_ProductGroup_Index"
        Me.Epson_ProductGroup_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Epson_ProductGroup_Index.Visible = False
        '
        'Weight_PerPackage
        '
        Me.Weight_PerPackage.DataPropertyName = "Weight_PerPackage"
        DataGridViewCellStyle6.Format = "N4"
        Me.Weight_PerPackage.DefaultCellStyle = DataGridViewCellStyle6
        Me.Weight_PerPackage.HeaderText = "Weight_PerPackage"
        Me.Weight_PerPackage.Name = "Weight_PerPackage"
        '
        'M3_PerPackage
        '
        Me.M3_PerPackage.DataPropertyName = "M3_PerPackage"
        DataGridViewCellStyle7.Format = "N4"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.M3_PerPackage.DefaultCellStyle = DataGridViewCellStyle7
        Me.M3_PerPackage.HeaderText = "M3_PerPackage"
        Me.M3_PerPackage.Name = "M3_PerPackage"
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
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Friend WithEvents grbDefaultValue As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
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
    Friend WithEvents Order_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref_No3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SKU_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Str4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Exp_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref_No2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Epson_ProductGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sku_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Package_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemStatus_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocumentType_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Epson_ProductGroup_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight_PerPackage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M3_PerPackage As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
