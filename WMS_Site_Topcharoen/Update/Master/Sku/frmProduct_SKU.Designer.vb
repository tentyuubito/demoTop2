<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProduct_SKU
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProduct_SKU))
        Me.grdSKU = New System.Windows.Forms.DataGridView
        Me.lbSkuIndex = New System.Windows.Forms.Label
        Me.gbSearch = New System.Windows.Forms.GroupBox
        Me.chkINT_U = New System.Windows.Forms.CheckBox
        Me.btnProductType_Popup = New System.Windows.Forms.Button
        Me.txtProductType = New System.Windows.Forms.TextBox
        Me.ProductSubClass = New System.Windows.Forms.Label
        Me.lblProductClass = New System.Windows.Forms.Label
        Me.cboProductClass = New System.Windows.Forms.ComboBox
        Me.cboProductSubClass = New System.Windows.Forms.ComboBox
        Me.cbProductType = New System.Windows.Forms.ComboBox
        Me.lblProductType = New System.Windows.Forms.Label
        Me.btnSearchSupplier = New System.Windows.Forms.Button
        Me.txtSupplierName = New System.Windows.Forms.TextBox
        Me.btnSeachCustomer = New System.Windows.Forms.Button
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.chkSupplier = New System.Windows.Forms.CheckBox
        Me.chkCustomer = New System.Windows.Forms.CheckBox
        Me.chkBOMOnly = New System.Windows.Forms.CheckBox
        Me.cbSearchSelectLang = New System.Windows.Forms.ComboBox
        Me.cbOperator = New System.Windows.Forms.ComboBox
        Me.txtSearchPackage = New System.Windows.Forms.TextBox
        Me.txtSearchSKUName = New System.Windows.Forms.TextBox
        Me.txtSearchProductID = New System.Windows.Forms.TextBox
        Me.lbSearchPackage = New System.Windows.Forms.Label
        Me.lbSearchProduct = New System.Windows.Forms.Label
        Me.lbSearchProductID = New System.Windows.Forms.Label
        Me.btnClear_Search = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtSearchUnitWeight = New System.Windows.Forms.TextBox
        Me.lbSearchUnitWeight = New System.Windows.Forms.Label
        Me.txtSearchSize = New System.Windows.Forms.TextBox
        Me.lblSearchSize = New System.Windows.Forms.Label
        Me.txtSearchSKUID = New System.Windows.Forms.TextBox
        Me.lblSerchSKUID = New System.Windows.Forms.Label
        Me.picSKU = New System.Windows.Forms.PictureBox
        Me.btnAddBom = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.grbPageEng = New System.Windows.Forms.GroupBox
        Me.txtTotalPage = New System.Windows.Forms.TextBox
        Me.txtPageIndex = New System.Windows.Forms.TextBox
        Me.lblSplit = New System.Windows.Forms.Label
        Me.btnPageLast = New System.Windows.Forms.Button
        Me.btnPageNext = New System.Windows.Forms.Button
        Me.btnPagePrev = New System.Windows.Forms.Button
        Me.btnPageFirst = New System.Windows.Forms.Button
        Me.lblRowPage = New System.Windows.Forms.Label
        Me.cboRowPerPage = New System.Windows.Forms.ComboBox
        Me.txtRowCount = New System.Windows.Forms.TextBox
        Me.lblrow = New System.Windows.Forms.Label
        Me.lbltotal = New System.Windows.Forms.Label
        Me.btnCusRefId = New System.Windows.Forms.Button
        Me.btnSkuMinMaxByWH = New System.Windows.Forms.Button
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn
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
        Me.ColBom_Pic = New System.Windows.Forms.DataGridViewImageColumn
        Me.ColumnSKU_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnSKU_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnProduct_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnProduct_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnDes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnProduct = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Product_Name_en = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnPackage_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnPackage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnSize = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnMin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnMax = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBom_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Unit_Width = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Unit_Length = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Unit_Height = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Unit_Volume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_UserSalseTool = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdSKU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSearch.SuspendLayout()
        CType(Me.picSKU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbPageEng.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdSKU
        '
        Me.grdSKU.AllowUserToAddRows = False
        Me.grdSKU.AllowUserToDeleteRows = False
        Me.grdSKU.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdSKU.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSKU.BackgroundColor = System.Drawing.Color.White
        Me.grdSKU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSKU.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColBom_Pic, Me.ColumnSKU_Index, Me.ColumnSKU_ID, Me.ColumnProduct_Index, Me.ColumnProduct_Id, Me.ColumnDes, Me.ColumnProduct, Me.col_Product_Name_en, Me.ColumnPackage_Index, Me.ColumnPackage, Me.ColumnSize, Me.ColumnWeight, Me.ColumnMin, Me.ColumnMax, Me.ColBom_Index, Me.col_Unit_Width, Me.col_Unit_Length, Me.col_Unit_Height, Me.col_Unit_Volume, Me.Col_UserSalseTool})
        Me.grdSKU.Location = New System.Drawing.Point(12, 232)
        Me.grdSKU.Name = "grdSKU"
        Me.grdSKU.ReadOnly = True
        Me.grdSKU.RowHeadersVisible = False
        Me.grdSKU.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSKU.Size = New System.Drawing.Size(887, 309)
        Me.grdSKU.TabIndex = 2
        Me.grdSKU.TabStop = False
        '
        'lbSkuIndex
        '
        Me.lbSkuIndex.AutoSize = True
        Me.lbSkuIndex.Location = New System.Drawing.Point(576, 92)
        Me.lbSkuIndex.Name = "lbSkuIndex"
        Me.lbSkuIndex.Size = New System.Drawing.Size(61, 13)
        Me.lbSkuIndex.TabIndex = 23
        Me.lbSkuIndex.Text = "SKU_Index"
        Me.lbSkuIndex.Visible = False
        '
        'gbSearch
        '
        Me.gbSearch.Controls.Add(Me.chkINT_U)
        Me.gbSearch.Controls.Add(Me.btnProductType_Popup)
        Me.gbSearch.Controls.Add(Me.txtProductType)
        Me.gbSearch.Controls.Add(Me.ProductSubClass)
        Me.gbSearch.Controls.Add(Me.lblProductClass)
        Me.gbSearch.Controls.Add(Me.cboProductClass)
        Me.gbSearch.Controls.Add(Me.cboProductSubClass)
        Me.gbSearch.Controls.Add(Me.cbProductType)
        Me.gbSearch.Controls.Add(Me.lblProductType)
        Me.gbSearch.Controls.Add(Me.btnSearchSupplier)
        Me.gbSearch.Controls.Add(Me.txtSupplierName)
        Me.gbSearch.Controls.Add(Me.btnSeachCustomer)
        Me.gbSearch.Controls.Add(Me.txtCustomer_Name)
        Me.gbSearch.Controls.Add(Me.chkSupplier)
        Me.gbSearch.Controls.Add(Me.chkCustomer)
        Me.gbSearch.Controls.Add(Me.chkBOMOnly)
        Me.gbSearch.Controls.Add(Me.cbSearchSelectLang)
        Me.gbSearch.Controls.Add(Me.lbSkuIndex)
        Me.gbSearch.Controls.Add(Me.cbOperator)
        Me.gbSearch.Controls.Add(Me.txtSearchPackage)
        Me.gbSearch.Controls.Add(Me.txtSearchSKUName)
        Me.gbSearch.Controls.Add(Me.txtSearchProductID)
        Me.gbSearch.Controls.Add(Me.lbSearchPackage)
        Me.gbSearch.Controls.Add(Me.lbSearchProduct)
        Me.gbSearch.Controls.Add(Me.lbSearchProductID)
        Me.gbSearch.Controls.Add(Me.btnClear_Search)
        Me.gbSearch.Controls.Add(Me.btnSearch)
        Me.gbSearch.Controls.Add(Me.txtSearchUnitWeight)
        Me.gbSearch.Controls.Add(Me.lbSearchUnitWeight)
        Me.gbSearch.Controls.Add(Me.txtSearchSize)
        Me.gbSearch.Controls.Add(Me.lblSearchSize)
        Me.gbSearch.Controls.Add(Me.txtSearchSKUID)
        Me.gbSearch.Controls.Add(Me.lblSerchSKUID)
        Me.gbSearch.Location = New System.Drawing.Point(12, 2)
        Me.gbSearch.Name = "gbSearch"
        Me.gbSearch.Size = New System.Drawing.Size(656, 182)
        Me.gbSearch.TabIndex = 0
        Me.gbSearch.TabStop = False
        Me.gbSearch.Text = "เงื่อนไขการค้นหา"
        '
        'chkINT_U
        '
        Me.chkINT_U.AutoSize = True
        Me.chkINT_U.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkINT_U.Location = New System.Drawing.Point(223, 160)
        Me.chkINT_U.Name = "chkINT_U"
        Me.chkINT_U.Size = New System.Drawing.Size(106, 17)
        Me.chkINT_U.TabIndex = 32
        Me.chkINT_U.Text = "New Interface"
        Me.chkINT_U.UseVisualStyleBackColor = True
        '
        'btnProductType_Popup
        '
        Me.btnProductType_Popup.Location = New System.Drawing.Point(248, 62)
        Me.btnProductType_Popup.Name = "btnProductType_Popup"
        Me.btnProductType_Popup.Size = New System.Drawing.Size(24, 23)
        Me.btnProductType_Popup.TabIndex = 31
        Me.btnProductType_Popup.Text = "..."
        Me.btnProductType_Popup.UseVisualStyleBackColor = True
        '
        'txtProductType
        '
        Me.txtProductType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProductType.Location = New System.Drawing.Point(85, 63)
        Me.txtProductType.MaxLength = 200
        Me.txtProductType.Name = "txtProductType"
        Me.txtProductType.ReadOnly = True
        Me.txtProductType.Size = New System.Drawing.Size(161, 20)
        Me.txtProductType.TabIndex = 30
        '
        'ProductSubClass
        '
        Me.ProductSubClass.Location = New System.Drawing.Point(278, 113)
        Me.ProductSubClass.Name = "ProductSubClass"
        Me.ProductSubClass.Size = New System.Drawing.Size(89, 17)
        Me.ProductSubClass.TabIndex = 26
        Me.ProductSubClass.Text = "ProductSubClass"
        Me.ProductSubClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProductClass
        '
        Me.lblProductClass.Location = New System.Drawing.Point(294, 90)
        Me.lblProductClass.Name = "lblProductClass"
        Me.lblProductClass.Size = New System.Drawing.Size(73, 17)
        Me.lblProductClass.TabIndex = 24
        Me.lblProductClass.Text = "ProductClass"
        Me.lblProductClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboProductClass
        '
        Me.cboProductClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProductClass.FormattingEnabled = True
        Me.cboProductClass.Location = New System.Drawing.Point(373, 87)
        Me.cboProductClass.Name = "cboProductClass"
        Me.cboProductClass.Size = New System.Drawing.Size(176, 21)
        Me.cboProductClass.TabIndex = 25
        '
        'cboProductSubClass
        '
        Me.cboProductSubClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProductSubClass.FormattingEnabled = True
        Me.cboProductSubClass.Location = New System.Drawing.Point(373, 112)
        Me.cboProductSubClass.Name = "cboProductSubClass"
        Me.cboProductSubClass.Size = New System.Drawing.Size(176, 21)
        Me.cboProductSubClass.TabIndex = 27
        '
        'cbProductType
        '
        Me.cbProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProductType.FormattingEnabled = True
        Me.cbProductType.Location = New System.Drawing.Point(85, 63)
        Me.cbProductType.Name = "cbProductType"
        Me.cbProductType.Size = New System.Drawing.Size(161, 21)
        Me.cbProductType.TabIndex = 11
        '
        'lblProductType
        '
        Me.lblProductType.Location = New System.Drawing.Point(6, 66)
        Me.lblProductType.Name = "lblProductType"
        Me.lblProductType.Size = New System.Drawing.Size(73, 13)
        Me.lblProductType.TabIndex = 10
        Me.lblProductType.Text = "ประเภทสินค้า"
        Me.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSearchSupplier
        '
        Me.btnSearchSupplier.Location = New System.Drawing.Point(248, 135)
        Me.btnSearchSupplier.Name = "btnSearchSupplier"
        Me.btnSearchSupplier.Size = New System.Drawing.Size(24, 23)
        Me.btnSearchSupplier.TabIndex = 21
        Me.btnSearchSupplier.Text = "..."
        Me.btnSearchSupplier.UseVisualStyleBackColor = True
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSupplierName.Location = New System.Drawing.Point(85, 136)
        Me.txtSupplierName.MaxLength = 200
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.ReadOnly = True
        Me.txtSupplierName.Size = New System.Drawing.Size(161, 20)
        Me.txtSupplierName.TabIndex = 20
        '
        'btnSeachCustomer
        '
        Me.btnSeachCustomer.Location = New System.Drawing.Point(248, 110)
        Me.btnSeachCustomer.Name = "btnSeachCustomer"
        Me.btnSeachCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnSeachCustomer.TabIndex = 18
        Me.btnSeachCustomer.Text = "..."
        Me.btnSeachCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Name.Location = New System.Drawing.Point(85, 112)
        Me.txtCustomer_Name.MaxLength = 200
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(161, 20)
        Me.txtCustomer_Name.TabIndex = 17
        '
        'chkSupplier
        '
        Me.chkSupplier.AutoSize = True
        Me.chkSupplier.Location = New System.Drawing.Point(27, 138)
        Me.chkSupplier.Name = "chkSupplier"
        Me.chkSupplier.Size = New System.Drawing.Size(51, 17)
        Me.chkSupplier.TabIndex = 19
        Me.chkSupplier.Text = "ผู้ขาย"
        Me.chkSupplier.UseVisualStyleBackColor = True
        '
        'chkCustomer
        '
        Me.chkCustomer.AutoSize = True
        Me.chkCustomer.Location = New System.Drawing.Point(27, 115)
        Me.chkCustomer.Name = "chkCustomer"
        Me.chkCustomer.Size = New System.Drawing.Size(51, 17)
        Me.chkCustomer.TabIndex = 16
        Me.chkCustomer.Text = "ลูกค้า"
        Me.chkCustomer.UseVisualStyleBackColor = True
        '
        'chkBOMOnly
        '
        Me.chkBOMOnly.AutoSize = True
        Me.chkBOMOnly.Location = New System.Drawing.Point(27, 160)
        Me.chkBOMOnly.Name = "chkBOMOnly"
        Me.chkBOMOnly.Size = New System.Drawing.Size(185, 17)
        Me.chkBOMOnly.TabIndex = 22
        Me.chkBOMOnly.Text = "เฉพาะรายการที่มีสูตรสินค้า (BOM)"
        Me.chkBOMOnly.UseVisualStyleBackColor = True
        '
        'cbSearchSelectLang
        '
        Me.cbSearchSelectLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchSelectLang.FormattingEnabled = True
        Me.cbSearchSelectLang.Items.AddRange(New Object() {"", "ไทย (Thai)", "อังกฤษ (English)"})
        Me.cbSearchSelectLang.Location = New System.Drawing.Point(373, 14)
        Me.cbSearchSelectLang.Name = "cbSearchSelectLang"
        Me.cbSearchSelectLang.Size = New System.Drawing.Size(83, 21)
        Me.cbSearchSelectLang.TabIndex = 3
        '
        'cbOperator
        '
        Me.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOperator.FormattingEnabled = True
        Me.cbOperator.Items.AddRange(New Object() {"", ">", "<", "="})
        Me.cbOperator.Location = New System.Drawing.Point(373, 38)
        Me.cbOperator.Name = "cbOperator"
        Me.cbOperator.Size = New System.Drawing.Size(83, 21)
        Me.cbOperator.TabIndex = 8
        '
        'txtSearchPackage
        '
        Me.txtSearchPackage.Location = New System.Drawing.Point(373, 63)
        Me.txtSearchPackage.MaxLength = 100
        Me.txtSearchPackage.Name = "txtSearchPackage"
        Me.txtSearchPackage.Size = New System.Drawing.Size(264, 20)
        Me.txtSearchPackage.TabIndex = 13
        '
        'txtSearchSKUName
        '
        Me.txtSearchSKUName.Location = New System.Drawing.Point(462, 15)
        Me.txtSearchSKUName.MaxLength = 200
        Me.txtSearchSKUName.Name = "txtSearchSKUName"
        Me.txtSearchSKUName.Size = New System.Drawing.Size(175, 20)
        Me.txtSearchSKUName.TabIndex = 4
        '
        'txtSearchProductID
        '
        Me.txtSearchProductID.Location = New System.Drawing.Point(85, 40)
        Me.txtSearchProductID.MaxLength = 100
        Me.txtSearchProductID.Name = "txtSearchProductID"
        Me.txtSearchProductID.Size = New System.Drawing.Size(160, 20)
        Me.txtSearchProductID.TabIndex = 6
        '
        'lbSearchPackage
        '
        Me.lbSearchPackage.Location = New System.Drawing.Point(294, 64)
        Me.lbSearchPackage.Name = "lbSearchPackage"
        Me.lbSearchPackage.Size = New System.Drawing.Size(73, 17)
        Me.lbSearchPackage.TabIndex = 12
        Me.lbSearchPackage.Text = "หน่วย"
        Me.lbSearchPackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbSearchProduct
        '
        Me.lbSearchProduct.Location = New System.Drawing.Point(310, 17)
        Me.lbSearchProduct.Name = "lbSearchProduct"
        Me.lbSearchProduct.Size = New System.Drawing.Size(57, 18)
        Me.lbSearchProduct.TabIndex = 2
        Me.lbSearchProduct.Text = "ชื่อรายการ"
        Me.lbSearchProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbSearchProductID
        '
        Me.lbSearchProductID.Location = New System.Drawing.Point(6, 42)
        Me.lbSearchProductID.Name = "lbSearchProductID"
        Me.lbSearchProductID.Size = New System.Drawing.Size(73, 17)
        Me.lbSearchProductID.TabIndex = 5
        Me.lbSearchProductID.Text = "รหัสสินค้า"
        Me.lbSearchProductID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear_Search
        '
        Me.btnClear_Search.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear_Search.Location = New System.Drawing.Point(488, 136)
        Me.btnClear_Search.Name = "btnClear_Search"
        Me.btnClear_Search.Size = New System.Drawing.Size(115, 38)
        Me.btnClear_Search.TabIndex = 29
        Me.btnClear_Search.Text = "ล้าง"
        Me.btnClear_Search.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(373, 136)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(109, 38)
        Me.btnSearch.TabIndex = 28
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearchUnitWeight
        '
        Me.txtSearchUnitWeight.Location = New System.Drawing.Point(462, 38)
        Me.txtSearchUnitWeight.MaxLength = 10
        Me.txtSearchUnitWeight.Name = "txtSearchUnitWeight"
        Me.txtSearchUnitWeight.Size = New System.Drawing.Size(175, 20)
        Me.txtSearchUnitWeight.TabIndex = 9
        '
        'lbSearchUnitWeight
        '
        Me.lbSearchUnitWeight.Location = New System.Drawing.Point(284, 39)
        Me.lbSearchUnitWeight.Name = "lbSearchUnitWeight"
        Me.lbSearchUnitWeight.Size = New System.Drawing.Size(83, 18)
        Me.lbSearchUnitWeight.TabIndex = 7
        Me.lbSearchUnitWeight.Text = "น้ำหนัก/หน่วย"
        Me.lbSearchUnitWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearchSize
        '
        Me.txtSearchSize.Location = New System.Drawing.Point(85, 88)
        Me.txtSearchSize.MaxLength = 100
        Me.txtSearchSize.Name = "txtSearchSize"
        Me.txtSearchSize.Size = New System.Drawing.Size(160, 20)
        Me.txtSearchSize.TabIndex = 15
        '
        'lblSearchSize
        '
        Me.lblSearchSize.Location = New System.Drawing.Point(19, 89)
        Me.lblSearchSize.Name = "lblSearchSize"
        Me.lblSearchSize.Size = New System.Drawing.Size(60, 17)
        Me.lblSearchSize.TabIndex = 14
        Me.lblSearchSize.Text = "ขนาด"
        Me.lblSearchSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearchSKUID
        '
        Me.txtSearchSKUID.Location = New System.Drawing.Point(85, 17)
        Me.txtSearchSKUID.MaxLength = 100
        Me.txtSearchSKUID.Name = "txtSearchSKUID"
        Me.txtSearchSKUID.Size = New System.Drawing.Size(160, 20)
        Me.txtSearchSKUID.TabIndex = 1
        '
        'lblSerchSKUID
        '
        Me.lblSerchSKUID.Location = New System.Drawing.Point(6, 18)
        Me.lblSerchSKUID.Name = "lblSerchSKUID"
        Me.lblSerchSKUID.Size = New System.Drawing.Size(73, 17)
        Me.lblSerchSKUID.TabIndex = 0
        Me.lblSerchSKUID.Text = "รหัส SKU"
        Me.lblSerchSKUID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picSKU
        '
        Me.picSKU.BackColor = System.Drawing.Color.White
        Me.picSKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSKU.Location = New System.Drawing.Point(674, 6)
        Me.picSKU.Name = "picSKU"
        Me.picSKU.Size = New System.Drawing.Size(223, 220)
        Me.picSKU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSKU.TabIndex = 26
        Me.picSKU.TabStop = False
        '
        'btnAddBom
        '
        Me.btnAddBom.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddBom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddBom.Location = New System.Drawing.Point(330, 547)
        Me.btnAddBom.Name = "btnAddBom"
        Me.btnAddBom.Size = New System.Drawing.Size(100, 38)
        Me.btnAddBom.TabIndex = 6
        Me.btnAddBom.Text = "สูตรสินค้า"
        Me.btnAddBom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddBom.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(797, 547)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 38)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(224, 547)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(118, 547)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "แก้ไข"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(10, 547)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 38)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "เพิ่ม"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grbPageEng
        '
        Me.grbPageEng.Controls.Add(Me.txtTotalPage)
        Me.grbPageEng.Controls.Add(Me.txtPageIndex)
        Me.grbPageEng.Controls.Add(Me.lblSplit)
        Me.grbPageEng.Controls.Add(Me.btnPageLast)
        Me.grbPageEng.Controls.Add(Me.btnPageNext)
        Me.grbPageEng.Controls.Add(Me.btnPagePrev)
        Me.grbPageEng.Controls.Add(Me.btnPageFirst)
        Me.grbPageEng.Controls.Add(Me.lblRowPage)
        Me.grbPageEng.Controls.Add(Me.cboRowPerPage)
        Me.grbPageEng.Controls.Add(Me.txtRowCount)
        Me.grbPageEng.Controls.Add(Me.lblrow)
        Me.grbPageEng.Controls.Add(Me.lbltotal)
        Me.grbPageEng.Location = New System.Drawing.Point(12, 185)
        Me.grbPageEng.Name = "grbPageEng"
        Me.grbPageEng.Size = New System.Drawing.Size(656, 41)
        Me.grbPageEng.TabIndex = 1
        Me.grbPageEng.TabStop = False
        Me.grbPageEng.Text = "การแสดงผล"
        '
        'txtTotalPage
        '
        Me.txtTotalPage.Location = New System.Drawing.Point(328, 14)
        Me.txtTotalPage.Name = "txtTotalPage"
        Me.txtTotalPage.ReadOnly = True
        Me.txtTotalPage.Size = New System.Drawing.Size(44, 20)
        Me.txtTotalPage.TabIndex = 4
        Me.txtTotalPage.TabStop = False
        '
        'txtPageIndex
        '
        Me.txtPageIndex.Location = New System.Drawing.Point(267, 14)
        Me.txtPageIndex.Name = "txtPageIndex"
        Me.txtPageIndex.Size = New System.Drawing.Size(43, 20)
        Me.txtPageIndex.TabIndex = 3
        Me.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSplit
        '
        Me.lblSplit.AutoSize = True
        Me.lblSplit.Location = New System.Drawing.Point(313, 18)
        Me.lblSplit.Name = "lblSplit"
        Me.lblSplit.Size = New System.Drawing.Size(12, 13)
        Me.lblSplit.TabIndex = 5
        Me.lblSplit.Text = "/"
        '
        'btnPageLast
        '
        Me.btnPageLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageLast.Location = New System.Drawing.Point(414, 14)
        Me.btnPageLast.Name = "btnPageLast"
        Me.btnPageLast.Size = New System.Drawing.Size(30, 21)
        Me.btnPageLast.TabIndex = 6
        Me.btnPageLast.Text = ">|"
        Me.btnPageLast.UseVisualStyleBackColor = True
        '
        'btnPageNext
        '
        Me.btnPageNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(378, 14)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(30, 21)
        Me.btnPageNext.TabIndex = 5
        Me.btnPageNext.Text = ">"
        Me.btnPageNext.UseVisualStyleBackColor = True
        '
        'btnPagePrev
        '
        Me.btnPagePrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPagePrev.Location = New System.Drawing.Point(231, 14)
        Me.btnPagePrev.Name = "btnPagePrev"
        Me.btnPagePrev.Size = New System.Drawing.Size(30, 21)
        Me.btnPagePrev.TabIndex = 2
        Me.btnPagePrev.Text = "<"
        Me.btnPagePrev.UseVisualStyleBackColor = True
        '
        'btnPageFirst
        '
        Me.btnPageFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPageFirst.Location = New System.Drawing.Point(195, 14)
        Me.btnPageFirst.Name = "btnPageFirst"
        Me.btnPageFirst.Size = New System.Drawing.Size(30, 21)
        Me.btnPageFirst.TabIndex = 1
        Me.btnPageFirst.Text = "|<"
        Me.btnPageFirst.UseVisualStyleBackColor = True
        '
        'lblRowPage
        '
        Me.lblRowPage.AutoSize = True
        Me.lblRowPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRowPage.Location = New System.Drawing.Point(13, 18)
        Me.lblRowPage.Name = "lblRowPage"
        Me.lblRowPage.Size = New System.Drawing.Size(68, 13)
        Me.lblRowPage.TabIndex = 0
        Me.lblRowPage.Text = "รายการ/หน้า"
        '
        'cboRowPerPage
        '
        Me.cboRowPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRowPerPage.FormattingEnabled = True
        Me.cboRowPerPage.Items.AddRange(New Object() {"50", "100", "200"})
        Me.cboRowPerPage.Location = New System.Drawing.Point(85, 14)
        Me.cboRowPerPage.Name = "cboRowPerPage"
        Me.cboRowPerPage.Size = New System.Drawing.Size(71, 21)
        Me.cboRowPerPage.TabIndex = 0
        '
        'txtRowCount
        '
        Me.txtRowCount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtRowCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRowCount.Location = New System.Drawing.Point(525, 13)
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
        Me.lblrow.Location = New System.Drawing.Point(572, 16)
        Me.lblrow.Name = "lblrow"
        Me.lblrow.Size = New System.Drawing.Size(43, 13)
        Me.lblrow.TabIndex = 12
        Me.lblrow.Text = "รายการ"
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.Location = New System.Drawing.Point(450, 16)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(73, 13)
        Me.lbltotal.TabIndex = 10
        Me.lbltotal.Text = "ผลลัพธ์ทั้งหมด"
        '
        'btnCusRefId
        '
        Me.btnCusRefId.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnCusRefId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCusRefId.Location = New System.Drawing.Point(436, 547)
        Me.btnCusRefId.Name = "btnCusRefId"
        Me.btnCusRefId.Size = New System.Drawing.Size(109, 38)
        Me.btnCusRefId.TabIndex = 27
        Me.btnCusRefId.Text = "       รหัสลูกค้าอ้างอิง"
        Me.btnCusRefId.UseVisualStyleBackColor = True
        '
        'btnSkuMinMaxByWH
        '
        Me.btnSkuMinMaxByWH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSkuMinMaxByWH.Image = CType(resources.GetObject("btnSkuMinMaxByWH.Image"), System.Drawing.Image)
        Me.btnSkuMinMaxByWH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSkuMinMaxByWH.Location = New System.Drawing.Point(551, 547)
        Me.btnSkuMinMaxByWH.Name = "btnSkuMinMaxByWH"
        Me.btnSkuMinMaxByWH.Size = New System.Drawing.Size(105, 38)
        Me.btnSkuMinMaxByWH.TabIndex = 28
        Me.btnSkuMinMaxByWH.Text = "WH Min-Max"
        Me.btnSkuMinMaxByWH.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSkuMinMaxByWH.UseVisualStyleBackColor = True
        '
        'DataGridViewImageColumn1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.NullValue = CType(resources.GetObject("DataGridViewCellStyle3.NullValue"), Object)
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
        Me.DataGridViewImageColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewImageColumn1.HeaderText = "Bom"
        Me.DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewImageColumn1.Width = 30
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Sku_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn2.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "ratio"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Product_Index"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Product_Index"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Product_Index"
        Me.DataGridViewTextBoxColumn4.FillWeight = 110.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Product"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Product_Name_th"
        Me.DataGridViewTextBoxColumn5.FillWeight = 203.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "Package_Index"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Package_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Package"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Expr5"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Des"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 200
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Size"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Expr3"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Weight"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 80
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Max_Weight"
        Me.DataGridViewTextBoxColumn10.FillWeight = 130.0!
        Me.DataGridViewTextBoxColumn10.HeaderText = "Min"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 130
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Min_Qty"
        Me.DataGridViewTextBoxColumn11.FillWeight = 130.0!
        Me.DataGridViewTextBoxColumn11.HeaderText = "Max"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 50
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Max_Qty"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Max"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        Me.DataGridViewTextBoxColumn12.Width = 50
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Max_Qty"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Bom_Index"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "PackingBom_Index"
        Me.DataGridViewTextBoxColumn14.HeaderText = "Bom_Index"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Unit_Width"
        Me.DataGridViewTextBoxColumn15.HeaderText = "กว้าง"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 50
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Unit_Length"
        Me.DataGridViewTextBoxColumn16.HeaderText = "ยาว"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 50
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Unit_Height"
        Me.DataGridViewTextBoxColumn17.HeaderText = "สูง"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 50
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Unit_Volume"
        Me.DataGridViewTextBoxColumn18.HeaderText = "ปริมาตร"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "SalesTool_User"
        Me.DataGridViewTextBoxColumn19.HeaderText = "ชื่อผู่ใช้ST"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        '
        'ColBom_Pic
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.NullValue = CType(resources.GetObject("DataGridViewCellStyle2.NullValue"), Object)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        Me.ColBom_Pic.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColBom_Pic.HeaderText = "BOM"
        Me.ColBom_Pic.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Emptys
        Me.ColBom_Pic.Name = "ColBom_Pic"
        Me.ColBom_Pic.ReadOnly = True
        Me.ColBom_Pic.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColBom_Pic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColBom_Pic.Width = 45
        '
        'ColumnSKU_Index
        '
        Me.ColumnSKU_Index.DataPropertyName = "Sku_Index"
        Me.ColumnSKU_Index.HeaderText = "SKU_Index"
        Me.ColumnSKU_Index.Name = "ColumnSKU_Index"
        Me.ColumnSKU_Index.ReadOnly = True
        Me.ColumnSKU_Index.Visible = False
        '
        'ColumnSKU_ID
        '
        Me.ColumnSKU_ID.DataPropertyName = "Sku_Id"
        Me.ColumnSKU_ID.FillWeight = 120.0!
        Me.ColumnSKU_ID.HeaderText = "รหัส SKU"
        Me.ColumnSKU_ID.Name = "ColumnSKU_ID"
        Me.ColumnSKU_ID.ReadOnly = True
        Me.ColumnSKU_ID.Width = 120
        '
        'ColumnProduct_Index
        '
        Me.ColumnProduct_Index.DataPropertyName = "Product_Index"
        Me.ColumnProduct_Index.HeaderText = "Product_Index"
        Me.ColumnProduct_Index.Name = "ColumnProduct_Index"
        Me.ColumnProduct_Index.ReadOnly = True
        Me.ColumnProduct_Index.Visible = False
        '
        'ColumnProduct_Id
        '
        Me.ColumnProduct_Id.DataPropertyName = "Str4"
        Me.ColumnProduct_Id.FillWeight = 110.0!
        Me.ColumnProduct_Id.HeaderText = "รหัสสินค้า"
        Me.ColumnProduct_Id.Name = "ColumnProduct_Id"
        Me.ColumnProduct_Id.ReadOnly = True
        Me.ColumnProduct_Id.Visible = False
        Me.ColumnProduct_Id.Width = 110
        '
        'ColumnDes
        '
        Me.ColumnDes.DataPropertyName = "Str1"
        Me.ColumnDes.FillWeight = 203.0!
        Me.ColumnDes.HeaderText = "ชื่อรายการ (ไทย)"
        Me.ColumnDes.Name = "ColumnDes"
        Me.ColumnDes.ReadOnly = True
        Me.ColumnDes.Width = 200
        '
        'ColumnProduct
        '
        Me.ColumnProduct.DataPropertyName = "Product_Name_th"
        Me.ColumnProduct.HeaderText = "ชื่อสินค้า (ไทย)"
        Me.ColumnProduct.Name = "ColumnProduct"
        Me.ColumnProduct.ReadOnly = True
        Me.ColumnProduct.Visible = False
        Me.ColumnProduct.Width = 120
        '
        'col_Product_Name_en
        '
        Me.col_Product_Name_en.DataPropertyName = "Str2"
        Me.col_Product_Name_en.HeaderText = "ชื่อรายการ(อังกฤษ)"
        Me.col_Product_Name_en.Name = "col_Product_Name_en"
        Me.col_Product_Name_en.ReadOnly = True
        Me.col_Product_Name_en.Width = 200
        '
        'ColumnPackage_Index
        '
        Me.ColumnPackage_Index.DataPropertyName = "Package_Index"
        Me.ColumnPackage_Index.HeaderText = "Package_Index"
        Me.ColumnPackage_Index.Name = "ColumnPackage_Index"
        Me.ColumnPackage_Index.ReadOnly = True
        Me.ColumnPackage_Index.Visible = False
        '
        'ColumnPackage
        '
        Me.ColumnPackage.DataPropertyName = "Expr5"
        Me.ColumnPackage.HeaderText = "หน่วยหลัก"
        Me.ColumnPackage.Name = "ColumnPackage"
        Me.ColumnPackage.ReadOnly = True
        Me.ColumnPackage.Width = 80
        '
        'ColumnSize
        '
        Me.ColumnSize.DataPropertyName = "Expr3"
        Me.ColumnSize.HeaderText = "ขนาด"
        Me.ColumnSize.Name = "ColumnSize"
        Me.ColumnSize.ReadOnly = True
        Me.ColumnSize.Width = 50
        '
        'ColumnWeight
        '
        Me.ColumnWeight.DataPropertyName = "UnitWeight_Index"
        Me.ColumnWeight.FillWeight = 130.0!
        Me.ColumnWeight.HeaderText = "น้ำหนัก/หน่วย (kg.)"
        Me.ColumnWeight.Name = "ColumnWeight"
        Me.ColumnWeight.ReadOnly = True
        Me.ColumnWeight.Width = 125
        '
        'ColumnMin
        '
        Me.ColumnMin.DataPropertyName = "Min_Qty"
        Me.ColumnMin.HeaderText = "จำนวนต่ำสุด"
        Me.ColumnMin.Name = "ColumnMin"
        Me.ColumnMin.ReadOnly = True
        Me.ColumnMin.Visible = False
        '
        'ColumnMax
        '
        Me.ColumnMax.DataPropertyName = "Max_Qty"
        Me.ColumnMax.HeaderText = "จำนวนสูงสุด"
        Me.ColumnMax.Name = "ColumnMax"
        Me.ColumnMax.ReadOnly = True
        Me.ColumnMax.Visible = False
        '
        'ColBom_Index
        '
        Me.ColBom_Index.DataPropertyName = "PackingBom_Index"
        Me.ColBom_Index.HeaderText = "Bom_Index"
        Me.ColBom_Index.Name = "ColBom_Index"
        Me.ColBom_Index.ReadOnly = True
        Me.ColBom_Index.Visible = False
        '
        'col_Unit_Width
        '
        Me.col_Unit_Width.DataPropertyName = "Unit_Width"
        Me.col_Unit_Width.HeaderText = "กว้าง"
        Me.col_Unit_Width.Name = "col_Unit_Width"
        Me.col_Unit_Width.ReadOnly = True
        Me.col_Unit_Width.Width = 50
        '
        'col_Unit_Length
        '
        Me.col_Unit_Length.DataPropertyName = "Unit_Length"
        Me.col_Unit_Length.HeaderText = "ยาว"
        Me.col_Unit_Length.Name = "col_Unit_Length"
        Me.col_Unit_Length.ReadOnly = True
        Me.col_Unit_Length.Width = 50
        '
        'col_Unit_Height
        '
        Me.col_Unit_Height.DataPropertyName = "Unit_Height"
        Me.col_Unit_Height.HeaderText = "สูง"
        Me.col_Unit_Height.Name = "col_Unit_Height"
        Me.col_Unit_Height.ReadOnly = True
        Me.col_Unit_Height.Width = 50
        '
        'col_Unit_Volume
        '
        Me.col_Unit_Volume.DataPropertyName = "Unit_Volume"
        Me.col_Unit_Volume.HeaderText = "ปริมาตร"
        Me.col_Unit_Volume.Name = "col_Unit_Volume"
        Me.col_Unit_Volume.ReadOnly = True
        '
        'Col_UserSalseTool
        '
        Me.Col_UserSalseTool.DataPropertyName = "SalesTool_User"
        Me.Col_UserSalseTool.HeaderText = "ชื่อผู้ใช้ST"
        Me.Col_UserSalseTool.Name = "Col_UserSalseTool"
        Me.Col_UserSalseTool.ReadOnly = True
        '
        'frmProduct_SKU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 597)
        Me.Controls.Add(Me.btnSkuMinMaxByWH)
        Me.Controls.Add(Me.btnCusRefId)
        Me.Controls.Add(Me.picSKU)
        Me.Controls.Add(Me.grbPageEng)
        Me.Controls.Add(Me.btnAddBom)
        Me.Controls.Add(Me.gbSearch)
        Me.Controls.Add(Me.grdSKU)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnAdd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProduct_SKU"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ทะเบียนลักษณะเฉพาะสินค้่า (SKU)"
        CType(Me.grdSKU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSearch.ResumeLayout(False)
        Me.gbSearch.PerformLayout()
        CType(Me.picSKU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbPageEng.ResumeLayout(False)
        Me.grbPageEng.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdSKU As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lbSkuIndex As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gbSearch As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearchSKUName As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchProductID As System.Windows.Forms.TextBox
    Friend WithEvents lbSearchProduct As System.Windows.Forms.Label
    Friend WithEvents lbSearchProductID As System.Windows.Forms.Label
    Friend WithEvents txtSearchSKUID As System.Windows.Forms.TextBox
    Friend WithEvents lblSerchSKUID As System.Windows.Forms.Label
    Friend WithEvents txtSearchPackage As System.Windows.Forms.TextBox
    Friend WithEvents lbSearchPackage As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearchUnitWeight As System.Windows.Forms.TextBox
    Friend WithEvents lbSearchUnitWeight As System.Windows.Forms.Label
    Friend WithEvents txtSearchSize As System.Windows.Forms.TextBox
    Friend WithEvents lblSearchSize As System.Windows.Forms.Label
    Friend WithEvents cbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents cbSearchSelectLang As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear_Search As System.Windows.Forms.Button
    Friend WithEvents btnAddBom As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents grbPageEng As System.Windows.Forms.GroupBox
    Friend WithEvents txtRowCount As System.Windows.Forms.TextBox
    Friend WithEvents lblrow As System.Windows.Forms.Label
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents cboRowPerPage As System.Windows.Forms.ComboBox
    Friend WithEvents lblRowPage As System.Windows.Forms.Label
    Friend WithEvents btnPageLast As System.Windows.Forms.Button
    Friend WithEvents btnPageNext As System.Windows.Forms.Button
    Friend WithEvents btnPagePrev As System.Windows.Forms.Button
    Friend WithEvents btnPageFirst As System.Windows.Forms.Button
    Friend WithEvents lblSplit As System.Windows.Forms.Label
    Friend WithEvents txtPageIndex As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPage As System.Windows.Forms.TextBox
    Friend WithEvents chkBOMOnly As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSupplier As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents btnSeachCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchSupplier As System.Windows.Forms.Button
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents picSKU As System.Windows.Forms.PictureBox
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblProductType As System.Windows.Forms.Label
    Friend WithEvents cbProductType As System.Windows.Forms.ComboBox
    Friend WithEvents btnCusRefId As System.Windows.Forms.Button
    Friend WithEvents ProductSubClass As System.Windows.Forms.Label
    Friend WithEvents lblProductClass As System.Windows.Forms.Label
    Friend WithEvents cboProductClass As System.Windows.Forms.ComboBox
    Friend WithEvents cboProductSubClass As System.Windows.Forms.ComboBox
    Friend WithEvents btnProductType_Popup As System.Windows.Forms.Button
    Friend WithEvents txtProductType As System.Windows.Forms.TextBox
    Friend WithEvents btnSkuMinMaxByWH As System.Windows.Forms.Button
    Friend WithEvents chkINT_U As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBom_Pic As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents ColumnSKU_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnSKU_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnProduct_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnProduct_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnDes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnProduct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Product_Name_en As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnPackage_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnPackage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnMin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnMax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBom_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Unit_Width As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Unit_Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Unit_Height As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Unit_Volume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_UserSalseTool As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
