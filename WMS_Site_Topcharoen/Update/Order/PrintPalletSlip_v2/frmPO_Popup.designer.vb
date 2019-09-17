<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPO_Popup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPO_Popup))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbSearch = New System.Windows.Forms.GroupBox
        Me.lblPurchaseOrder_Date = New System.Windows.Forms.Label
        Me.lblExpected_Delivery_Date = New System.Windows.Forms.Label
        Me.dtpPurchaseOrder_Date_E = New System.Windows.Forms.DateTimePicker
        Me.dtpExpected_Delivery_Date_E = New System.Windows.Forms.DateTimePicker
        Me.chkPurchaseOrder_Date = New System.Windows.Forms.CheckBox
        Me.chkExpected_Delivery_Date = New System.Windows.Forms.CheckBox
        Me.dtpPurchaseOrder_Date_S = New System.Windows.Forms.DateTimePicker
        Me.dtpExpected_Delivery_Date_S = New System.Windows.Forms.DateTimePicker
        Me.lblSearchfrom = New System.Windows.Forms.Label
        Me.cboCondition = New System.Windows.Forms.ComboBox
        Me.txtCondition = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSelect = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgvPopupList = New System.Windows.Forms.DataGridView
        Me.col_PurchaseOrder_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrderItem_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Item_Seq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Expected_Delivery_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ratio = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Received_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Padding_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PackageSku_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkShowAll = New System.Windows.Forms.CheckBox
        Me.grbSearch.SuspendLayout()
        CType(Me.dgvPopupList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbSearch
        '
        Me.grbSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbSearch.Controls.Add(Me.chkShowAll)
        Me.grbSearch.Controls.Add(Me.lblPurchaseOrder_Date)
        Me.grbSearch.Controls.Add(Me.lblExpected_Delivery_Date)
        Me.grbSearch.Controls.Add(Me.dtpPurchaseOrder_Date_E)
        Me.grbSearch.Controls.Add(Me.dtpExpected_Delivery_Date_E)
        Me.grbSearch.Controls.Add(Me.chkPurchaseOrder_Date)
        Me.grbSearch.Controls.Add(Me.chkExpected_Delivery_Date)
        Me.grbSearch.Controls.Add(Me.dtpPurchaseOrder_Date_S)
        Me.grbSearch.Controls.Add(Me.dtpExpected_Delivery_Date_S)
        Me.grbSearch.Controls.Add(Me.lblSearchfrom)
        Me.grbSearch.Controls.Add(Me.cboCondition)
        Me.grbSearch.Controls.Add(Me.txtCondition)
        Me.grbSearch.Controls.Add(Me.btnSearch)
        Me.grbSearch.Location = New System.Drawing.Point(12, 12)
        Me.grbSearch.Name = "grbSearch"
        Me.grbSearch.Size = New System.Drawing.Size(772, 96)
        Me.grbSearch.TabIndex = 1
        Me.grbSearch.TabStop = False
        Me.grbSearch.Text = "ค้นหา"
        '
        'lblPurchaseOrder_Date
        '
        Me.lblPurchaseOrder_Date.Location = New System.Drawing.Point(312, 70)
        Me.lblPurchaseOrder_Date.Name = "lblPurchaseOrder_Date"
        Me.lblPurchaseOrder_Date.Size = New System.Drawing.Size(14, 20)
        Me.lblPurchaseOrder_Date.TabIndex = 12
        Me.lblPurchaseOrder_Date.Text = "-"
        Me.lblPurchaseOrder_Date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblExpected_Delivery_Date
        '
        Me.lblExpected_Delivery_Date.Location = New System.Drawing.Point(312, 44)
        Me.lblExpected_Delivery_Date.Name = "lblExpected_Delivery_Date"
        Me.lblExpected_Delivery_Date.Size = New System.Drawing.Size(14, 20)
        Me.lblExpected_Delivery_Date.TabIndex = 0
        Me.lblExpected_Delivery_Date.Text = "-"
        Me.lblExpected_Delivery_Date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpPurchaseOrder_Date_E
        '
        Me.dtpPurchaseOrder_Date_E.Checked = False
        Me.dtpPurchaseOrder_Date_E.CustomFormat = "dd/MM/yyy"
        Me.dtpPurchaseOrder_Date_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPurchaseOrder_Date_E.Location = New System.Drawing.Point(332, 70)
        Me.dtpPurchaseOrder_Date_E.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpPurchaseOrder_Date_E.Name = "dtpPurchaseOrder_Date_E"
        Me.dtpPurchaseOrder_Date_E.Size = New System.Drawing.Size(105, 20)
        Me.dtpPurchaseOrder_Date_E.TabIndex = 11
        '
        'dtpExpected_Delivery_Date_E
        '
        Me.dtpExpected_Delivery_Date_E.Checked = False
        Me.dtpExpected_Delivery_Date_E.CustomFormat = "dd/MM/yyy"
        Me.dtpExpected_Delivery_Date_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpected_Delivery_Date_E.Location = New System.Drawing.Point(332, 44)
        Me.dtpExpected_Delivery_Date_E.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpExpected_Delivery_Date_E.Name = "dtpExpected_Delivery_Date_E"
        Me.dtpExpected_Delivery_Date_E.Size = New System.Drawing.Size(105, 20)
        Me.dtpExpected_Delivery_Date_E.TabIndex = 10
        '
        'chkPurchaseOrder_Date
        '
        Me.chkPurchaseOrder_Date.Location = New System.Drawing.Point(69, 70)
        Me.chkPurchaseOrder_Date.Name = "chkPurchaseOrder_Date"
        Me.chkPurchaseOrder_Date.Size = New System.Drawing.Size(126, 20)
        Me.chkPurchaseOrder_Date.TabIndex = 6
        Me.chkPurchaseOrder_Date.Text = "วันที่ใบสั่งซื้อ"
        Me.chkPurchaseOrder_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPurchaseOrder_Date.UseVisualStyleBackColor = True
        '
        'chkExpected_Delivery_Date
        '
        Me.chkExpected_Delivery_Date.Location = New System.Drawing.Point(69, 44)
        Me.chkExpected_Delivery_Date.Name = "chkExpected_Delivery_Date"
        Me.chkExpected_Delivery_Date.Size = New System.Drawing.Size(126, 20)
        Me.chkExpected_Delivery_Date.TabIndex = 4
        Me.chkExpected_Delivery_Date.Text = "วันที่กำหนดรับ"
        Me.chkExpected_Delivery_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkExpected_Delivery_Date.UseVisualStyleBackColor = True
        '
        'dtpPurchaseOrder_Date_S
        '
        Me.dtpPurchaseOrder_Date_S.Checked = False
        Me.dtpPurchaseOrder_Date_S.CustomFormat = "dd/MM/yyy"
        Me.dtpPurchaseOrder_Date_S.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPurchaseOrder_Date_S.Location = New System.Drawing.Point(201, 70)
        Me.dtpPurchaseOrder_Date_S.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpPurchaseOrder_Date_S.Name = "dtpPurchaseOrder_Date_S"
        Me.dtpPurchaseOrder_Date_S.Size = New System.Drawing.Size(105, 20)
        Me.dtpPurchaseOrder_Date_S.TabIndex = 7
        '
        'dtpExpected_Delivery_Date_S
        '
        Me.dtpExpected_Delivery_Date_S.Checked = False
        Me.dtpExpected_Delivery_Date_S.CustomFormat = "dd/MM/yyy"
        Me.dtpExpected_Delivery_Date_S.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpected_Delivery_Date_S.Location = New System.Drawing.Point(201, 44)
        Me.dtpExpected_Delivery_Date_S.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpExpected_Delivery_Date_S.Name = "dtpExpected_Delivery_Date_S"
        Me.dtpExpected_Delivery_Date_S.Size = New System.Drawing.Size(105, 20)
        Me.dtpExpected_Delivery_Date_S.TabIndex = 5
        '
        'lblSearchfrom
        '
        Me.lblSearchfrom.AutoSize = True
        Me.lblSearchfrom.Location = New System.Drawing.Point(20, 21)
        Me.lblSearchfrom.Name = "lblSearchfrom"
        Me.lblSearchfrom.Size = New System.Drawing.Size(43, 13)
        Me.lblSearchfrom.TabIndex = 1
        Me.lblSearchfrom.Text = "เงื่อนไข"
        '
        'cboCondition
        '
        Me.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition.FormattingEnabled = True
        Me.cboCondition.Items.AddRange(New Object() {"เลขที่ใบสั่งซื้อ", "รหัสสินค้า"})
        Me.cboCondition.Location = New System.Drawing.Point(69, 17)
        Me.cboCondition.Name = "cboCondition"
        Me.cboCondition.Size = New System.Drawing.Size(126, 21)
        Me.cboCondition.TabIndex = 2
        '
        'txtCondition
        '
        Me.txtCondition.Location = New System.Drawing.Point(201, 18)
        Me.txtCondition.MaxLength = 200
        Me.txtCondition.Name = "txtCondition"
        Me.txtCondition.Size = New System.Drawing.Size(236, 20)
        Me.txtCondition.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(443, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(80, 38)
        Me.btnSearch.TabIndex = 9
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(704, 507)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 38)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Image = CType(resources.GetObject("btnSelect.Image"), System.Drawing.Image)
        Me.btnSelect.Location = New System.Drawing.Point(12, 507)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(80, 38)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "เลือก"
        Me.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Vehicle_Id"
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Vehicle_No"
        Me.DataGridViewTextBoxColumn2.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "description"
        Me.DataGridViewTextBoxColumn3.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "ประเภทรถ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Driver_name"
        Me.DataGridViewTextBoxColumn4.FillWeight = 170.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "คนขับ"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 170
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Weight_Vehicle"
        Me.DataGridViewTextBoxColumn5.FillWeight = 130.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "น้ำหนักบรรทุกสูงสุด"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 130
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Volume_Vehicle"
        Me.DataGridViewTextBoxColumn6.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "ปริมาตรบรรทุกสูงสุด"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "pallet_Vehicle"
        Me.DataGridViewTextBoxColumn7.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn7.HeaderText = "พาเลทสูงสุด"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 120
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Vehicle_Date"
        Me.DataGridViewTextBoxColumn8.HeaderText = "วันที่"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 140
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Vehicle_Index"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'dgvPopupList
        '
        Me.dgvPopupList.AllowUserToAddRows = False
        Me.dgvPopupList.AllowUserToDeleteRows = False
        Me.dgvPopupList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvPopupList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPopupList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPopupList.BackgroundColor = System.Drawing.Color.White
        Me.dgvPopupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPopupList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_PurchaseOrder_Index, Me.col_PurchaseOrderItem_Index, Me.col_Sku_Index, Me.col_Package_Index, Me.col_Customer_Index, Me.col_Item_Seq, Me.col_PurchaseOrder_No, Me.col_Expected_Delivery_Date, Me.col_Sku_Id, Me.col_Sku_Name, Me.col_Qty, Me.col_Package_Desc, Me.col_Ratio, Me.col_Total_Qty, Me.col_Total_Received_Qty, Me.col_Total_Padding_Qty, Me.col_PackageSku_Desc, Me.col_PurchaseOrder_Date})
        Me.dgvPopupList.Location = New System.Drawing.Point(12, 114)
        Me.dgvPopupList.Name = "dgvPopupList"
        Me.dgvPopupList.ReadOnly = True
        Me.dgvPopupList.RowHeadersVisible = False
        Me.dgvPopupList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPopupList.Size = New System.Drawing.Size(772, 387)
        Me.dgvPopupList.TabIndex = 2
        '
        'col_PurchaseOrder_Index
        '
        Me.col_PurchaseOrder_Index.DataPropertyName = "PurchaseOrder_Index"
        Me.col_PurchaseOrder_Index.HeaderText = "PurchaseOrder_Index"
        Me.col_PurchaseOrder_Index.Name = "col_PurchaseOrder_Index"
        Me.col_PurchaseOrder_Index.ReadOnly = True
        Me.col_PurchaseOrder_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_PurchaseOrder_Index.Visible = False
        '
        'col_PurchaseOrderItem_Index
        '
        Me.col_PurchaseOrderItem_Index.DataPropertyName = "PurchaseOrderItem_Index"
        Me.col_PurchaseOrderItem_Index.HeaderText = "PurchaseOrderItem_Index"
        Me.col_PurchaseOrderItem_Index.Name = "col_PurchaseOrderItem_Index"
        Me.col_PurchaseOrderItem_Index.ReadOnly = True
        Me.col_PurchaseOrderItem_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_PurchaseOrderItem_Index.Visible = False
        '
        'col_Sku_Index
        '
        Me.col_Sku_Index.DataPropertyName = "Sku_Index"
        Me.col_Sku_Index.HeaderText = "Sku_Index"
        Me.col_Sku_Index.Name = "col_Sku_Index"
        Me.col_Sku_Index.ReadOnly = True
        Me.col_Sku_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Sku_Index.Visible = False
        '
        'col_Package_Index
        '
        Me.col_Package_Index.DataPropertyName = "Package_Index"
        Me.col_Package_Index.HeaderText = "Package_Index"
        Me.col_Package_Index.Name = "col_Package_Index"
        Me.col_Package_Index.ReadOnly = True
        Me.col_Package_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Package_Index.Visible = False
        '
        'col_Customer_Index
        '
        Me.col_Customer_Index.DataPropertyName = "Customer_Index"
        Me.col_Customer_Index.HeaderText = "Customer_Index"
        Me.col_Customer_Index.Name = "col_Customer_Index"
        Me.col_Customer_Index.ReadOnly = True
        Me.col_Customer_Index.Visible = False
        '
        'col_Item_Seq
        '
        Me.col_Item_Seq.DataPropertyName = "Item_Seq"
        Me.col_Item_Seq.HeaderText = "Item_Seq"
        Me.col_Item_Seq.Name = "col_Item_Seq"
        Me.col_Item_Seq.ReadOnly = True
        Me.col_Item_Seq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Item_Seq.Visible = False
        '
        'col_PurchaseOrder_No
        '
        Me.col_PurchaseOrder_No.DataPropertyName = "PurchaseOrder_No"
        Me.col_PurchaseOrder_No.HeaderText = "PO No"
        Me.col_PurchaseOrder_No.Name = "col_PurchaseOrder_No"
        Me.col_PurchaseOrder_No.ReadOnly = True
        Me.col_PurchaseOrder_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Expected_Delivery_Date
        '
        Me.col_Expected_Delivery_Date.DataPropertyName = "Expected_Delivery_Date"
        DataGridViewCellStyle2.Format = "dd/MM/yyyy"
        Me.col_Expected_Delivery_Date.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_Expected_Delivery_Date.HeaderText = "วันที่กำหนดรับ"
        Me.col_Expected_Delivery_Date.Name = "col_Expected_Delivery_Date"
        Me.col_Expected_Delivery_Date.ReadOnly = True
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.ReadOnly = True
        Me.col_Sku_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Sku_Name
        '
        Me.col_Sku_Name.DataPropertyName = "Sku_Name"
        Me.col_Sku_Name.HeaderText = "ชื่อสินค้า"
        Me.col_Sku_Name.Name = "col_Sku_Name"
        Me.col_Sku_Name.ReadOnly = True
        Me.col_Sku_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Sku_Name.Width = 150
        '
        'col_Qty
        '
        Me.col_Qty.DataPropertyName = "Qty"
        DataGridViewCellStyle3.Format = "N2"
        Me.col_Qty.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_Qty.HeaderText = "จำนวนสั่ง"
        Me.col_Qty.Name = "col_Qty"
        Me.col_Qty.ReadOnly = True
        Me.col_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Package_Desc
        '
        Me.col_Package_Desc.DataPropertyName = "Package_Desc"
        Me.col_Package_Desc.HeaderText = "หน่วยสั่ง"
        Me.col_Package_Desc.Name = "col_Package_Desc"
        Me.col_Package_Desc.ReadOnly = True
        Me.col_Package_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Ratio
        '
        Me.col_Ratio.DataPropertyName = "Ratio"
        DataGridViewCellStyle4.Format = "N2"
        Me.col_Ratio.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Ratio.HeaderText = "จำนวนชิ้น/หน่วย"
        Me.col_Ratio.Name = "col_Ratio"
        Me.col_Ratio.ReadOnly = True
        Me.col_Ratio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Ratio.Visible = False
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle5.Format = "N2"
        Me.col_Total_Qty.DefaultCellStyle = DataGridViewCellStyle5
        Me.col_Total_Qty.HeaderText = "จำนวนสั่ง(ย่อย)"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        Me.col_Total_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Total_Received_Qty
        '
        Me.col_Total_Received_Qty.DataPropertyName = "Total_Received_Qty"
        DataGridViewCellStyle6.Format = "N2"
        Me.col_Total_Received_Qty.DefaultCellStyle = DataGridViewCellStyle6
        Me.col_Total_Received_Qty.HeaderText = "จำนวนรับแล้ว(ย่อย)"
        Me.col_Total_Received_Qty.Name = "col_Total_Received_Qty"
        Me.col_Total_Received_Qty.ReadOnly = True
        Me.col_Total_Received_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Total_Received_Qty.Width = 110
        '
        'col_Total_Padding_Qty
        '
        Me.col_Total_Padding_Qty.DataPropertyName = "Total_Padding_Qty"
        DataGridViewCellStyle7.Format = "N2"
        Me.col_Total_Padding_Qty.DefaultCellStyle = DataGridViewCellStyle7
        Me.col_Total_Padding_Qty.HeaderText = "จำนวนค้างรับ(ย่อย)"
        Me.col_Total_Padding_Qty.Name = "col_Total_Padding_Qty"
        Me.col_Total_Padding_Qty.ReadOnly = True
        Me.col_Total_Padding_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Total_Padding_Qty.Width = 110
        '
        'col_PackageSku_Desc
        '
        Me.col_PackageSku_Desc.DataPropertyName = "PackageSku_Desc"
        Me.col_PackageSku_Desc.HeaderText = "หน่วยย่อย"
        Me.col_PackageSku_Desc.Name = "col_PackageSku_Desc"
        Me.col_PackageSku_Desc.ReadOnly = True
        '
        'col_PurchaseOrder_Date
        '
        Me.col_PurchaseOrder_Date.DataPropertyName = "PurchaseOrder_Date"
        DataGridViewCellStyle8.Format = "dd/MM/yyyy"
        Me.col_PurchaseOrder_Date.DefaultCellStyle = DataGridViewCellStyle8
        Me.col_PurchaseOrder_Date.HeaderText = "วันที่ใบสั่งซื้อ"
        Me.col_PurchaseOrder_Date.Name = "col_PurchaseOrder_Date"
        Me.col_PurchaseOrder_Date.ReadOnly = True
        '
        'chkShowAll
        '
        Me.chkShowAll.AutoSize = True
        Me.chkShowAll.Location = New System.Drawing.Point(444, 72)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(148, 17)
        Me.chkShowAll.TabIndex = 13
        Me.chkShowAll.Text = "แสดงรายการที่รับครบแล้ว"
        Me.chkShowAll.UseVisualStyleBackColor = True
        '
        'frmPO_Popup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 557)
        Me.Controls.Add(Me.dgvPopupList)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.grbSearch)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPO_Popup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รายการใบสั่งซื้อ"
        Me.grbSearch.ResumeLayout(False)
        Me.grbSearch.PerformLayout()
        CType(Me.dgvPopupList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearchfrom As System.Windows.Forms.Label
    Friend WithEvents cboCondition As System.Windows.Forms.ComboBox
    Friend WithEvents txtCondition As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvPopupList As System.Windows.Forms.DataGridView
    Friend WithEvents dtpExpected_Delivery_Date_S As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkPurchaseOrder_Date As System.Windows.Forms.CheckBox
    Friend WithEvents chkExpected_Delivery_Date As System.Windows.Forms.CheckBox
    Friend WithEvents dtpPurchaseOrder_Date_S As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblPurchaseOrder_Date As System.Windows.Forms.Label
    Friend WithEvents lblExpected_Delivery_Date As System.Windows.Forms.Label
    Friend WithEvents dtpPurchaseOrder_Date_E As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpExpected_Delivery_Date_E As System.Windows.Forms.DateTimePicker
    Friend WithEvents col_PurchaseOrder_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrderItem_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Item_Seq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Expected_Delivery_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ratio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Received_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Padding_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PackageSku_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkShowAll As System.Windows.Forms.CheckBox
End Class
