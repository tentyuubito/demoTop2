<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingBox_Backup
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblReservedLocation = New System.Windows.Forms.Label
        Me.grdSOView = New System.Windows.Forms.DataGridView
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_SalesOrder_index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.route_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Company_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping_Location_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.total_qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Barcode_BAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_ConfirmBAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnClosedPacking = New System.Windows.Forms.Button
        Me.btnFrmClose = New System.Windows.Forms.Button
        Me.btnConsignee = New System.Windows.Forms.Button
        Me.txtConsignee_Id = New System.Windows.Forms.TextBox
        Me.txtBarcodeBAG = New System.Windows.Forms.TextBox
        Me.lblBarcodeBag = New System.Windows.Forms.Label
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
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdSOView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblReservedLocation
        '
        Me.lblReservedLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReservedLocation.Location = New System.Drawing.Point(6, 19)
        Me.lblReservedLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReservedLocation.Name = "lblReservedLocation"
        Me.lblReservedLocation.Size = New System.Drawing.Size(116, 16)
        Me.lblReservedLocation.TabIndex = 11
        Me.lblReservedLocation.Text = "สาขาแม่"
        Me.lblReservedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdSOView
        '
        Me.grdSOView.AllowUserToAddRows = False
        Me.grdSOView.AllowUserToDeleteRows = False
        Me.grdSOView.AllowUserToResizeRows = False
        Me.grdSOView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSOView.BackgroundColor = System.Drawing.Color.White
        Me.grdSOView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect, Me.Col_SalesOrder_index, Me.Col_SalesOrder_No, Me.route_Desc, Me.Company_Name, Me.Col_Shipping_Location_Name, Me.total_qty, Me.Barcode_BAG, Me.Col_ConfirmBAG})
        Me.grdSOView.Location = New System.Drawing.Point(9, 117)
        Me.grdSOView.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSOView.MultiSelect = False
        Me.grdSOView.Name = "grdSOView"
        Me.grdSOView.RowHeadersVisible = False
        Me.grdSOView.RowTemplate.Height = 24
        Me.grdSOView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdSOView.Size = New System.Drawing.Size(1269, 434)
        Me.grdSOView.TabIndex = 15
        '
        'chkSelect
        '
        Me.chkSelect.DataPropertyName = "Checked"
        Me.chkSelect.FalseValue = "0"
        Me.chkSelect.Frozen = True
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.ReadOnly = True
        Me.chkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkSelect.TrueValue = "1"
        Me.chkSelect.Width = 50
        '
        'Col_SalesOrder_index
        '
        Me.Col_SalesOrder_index.DataPropertyName = "SalesOrder_index"
        Me.Col_SalesOrder_index.HeaderText = "index"
        Me.Col_SalesOrder_index.Name = "Col_SalesOrder_index"
        Me.Col_SalesOrder_index.Visible = False
        '
        'Col_SalesOrder_No
        '
        Me.Col_SalesOrder_No.DataPropertyName = "SalesOrder_No"
        Me.Col_SalesOrder_No.HeaderText = "SalesOrder_No"
        Me.Col_SalesOrder_No.Name = "Col_SalesOrder_No"
        Me.Col_SalesOrder_No.Width = 150
        '
        'route_Desc
        '
        Me.route_Desc.DataPropertyName = "route_Desc"
        Me.route_Desc.HeaderText = "Route"
        Me.route_Desc.Name = "route_Desc"
        '
        'Company_Name
        '
        Me.Company_Name.DataPropertyName = "Company_Name"
        Me.Company_Name.HeaderText = "สาขาแม่"
        Me.Company_Name.Name = "Company_Name"
        '
        'Col_Shipping_Location_Name
        '
        Me.Col_Shipping_Location_Name.DataPropertyName = "Shipping_Location_Name"
        Me.Col_Shipping_Location_Name.HeaderText = "สาขาย่อย"
        Me.Col_Shipping_Location_Name.Name = "Col_Shipping_Location_Name"
        Me.Col_Shipping_Location_Name.Width = 200
        '
        'total_qty
        '
        Me.total_qty.DataPropertyName = "total_qty"
        Me.total_qty.HeaderText = "จำนวน"
        Me.total_qty.Name = "total_qty"
        '
        'Barcode_BAG
        '
        Me.Barcode_BAG.DataPropertyName = "Barcode_BAG"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Barcode_BAG.DefaultCellStyle = DataGridViewCellStyle1
        Me.Barcode_BAG.HeaderText = "Barcode BAG"
        Me.Barcode_BAG.Name = "Barcode_BAG"
        Me.Barcode_BAG.ReadOnly = True
        Me.Barcode_BAG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Barcode_BAG.Width = 160
        '
        'Col_ConfirmBAG
        '
        Me.Col_ConfirmBAG.DataPropertyName = "ConfirmBAG"
        Me.Col_ConfirmBAG.HeaderText = "Confirm BAG"
        Me.Col_ConfirmBAG.Name = "Col_ConfirmBAG"
        Me.Col_ConfirmBAG.ReadOnly = True
        Me.Col_ConfirmBAG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_ConfirmBAG.Width = 160
        '
        'btnClosedPacking
        '
        Me.btnClosedPacking.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClosedPacking.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnClosedPacking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClosedPacking.Location = New System.Drawing.Point(13, 578)
        Me.btnClosedPacking.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClosedPacking.Name = "btnClosedPacking"
        Me.btnClosedPacking.Size = New System.Drawing.Size(109, 42)
        Me.btnClosedPacking.TabIndex = 16
        Me.btnClosedPacking.Text = "ปิดถุง"
        Me.btnClosedPacking.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClosedPacking.UseVisualStyleBackColor = True
        '
        'btnFrmClose
        '
        Me.btnFrmClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFrmClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ยกเลิกรายการ
        Me.btnFrmClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFrmClose.Location = New System.Drawing.Point(1145, 576)
        Me.btnFrmClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFrmClose.Name = "btnFrmClose"
        Me.btnFrmClose.Size = New System.Drawing.Size(133, 44)
        Me.btnFrmClose.TabIndex = 17
        Me.btnFrmClose.Text = "Close"
        Me.btnFrmClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFrmClose.UseVisualStyleBackColor = True
        '
        'btnConsignee
        '
        Me.btnConsignee.Location = New System.Drawing.Point(338, 9)
        Me.btnConsignee.Margin = New System.Windows.Forms.Padding(4)
        Me.btnConsignee.Name = "btnConsignee"
        Me.btnConsignee.Size = New System.Drawing.Size(32, 28)
        Me.btnConsignee.TabIndex = 19
        Me.btnConsignee.Text = "..."
        Me.btnConsignee.UseVisualStyleBackColor = True
        '
        'txtConsignee_Id
        '
        Me.txtConsignee_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignee_Id.Location = New System.Drawing.Point(130, 15)
        Me.txtConsignee_Id.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConsignee_Id.Name = "txtConsignee_Id"
        Me.txtConsignee_Id.ReadOnly = True
        Me.txtConsignee_Id.Size = New System.Drawing.Size(193, 22)
        Me.txtConsignee_Id.TabIndex = 18
        '
        'txtBarcodeBAG
        '
        Me.txtBarcodeBAG.BackColor = System.Drawing.Color.White
        Me.txtBarcodeBAG.Location = New System.Drawing.Point(130, 59)
        Me.txtBarcodeBAG.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBarcodeBAG.Name = "txtBarcodeBAG"
        Me.txtBarcodeBAG.Size = New System.Drawing.Size(193, 22)
        Me.txtBarcodeBAG.TabIndex = 20
        '
        'lblBarcodeBag
        '
        Me.lblBarcodeBag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcodeBag.Location = New System.Drawing.Point(6, 59)
        Me.lblBarcodeBag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBarcodeBag.Name = "lblBarcodeBag"
        Me.lblBarcodeBag.Size = New System.Drawing.Size(116, 16)
        Me.lblBarcodeBag.TabIndex = 21
        Me.lblBarcodeBag.Text = "Barcode BAG"
        Me.lblBarcodeBag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "chkSelect"
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Confirm Bar"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Invoice_No"
        Me.DataGridViewTextBoxColumn1.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "เลขที่อินวอยส์"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SalesOrder_No"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "ใบสั่งขาย"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Str1"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Qty_Pack"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "แพ็คแล้ว"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Description"
        DataGridViewCellStyle6.Format = "d"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn7.Frozen = True
        Me.DataGridViewTextBoxColumn7.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 60
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "SalesOrder_Index"
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn8.Frozen = True
        Me.DataGridViewTextBoxColumn8.HeaderText = "SalesOrder_Index"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 50
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Sku_Index"
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn9.Frozen = True
        Me.DataGridViewTextBoxColumn9.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 50
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "TransportManifestItem_Index"
        Me.DataGridViewTextBoxColumn10.Frozen = True
        Me.DataGridViewTextBoxColumn10.HeaderText = "TransportManifestItem_Index"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "ibarcode"
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn11.Frozen = True
        Me.DataGridViewTextBoxColumn11.HeaderText = "บาร์โค้ดสินค้า"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 120
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Sku_Index"
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn12.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn12.Frozen = True
        Me.DataGridViewTextBoxColumn12.HeaderText = "Sku_Index_Bx"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "QTY"
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn13.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn13.Frozen = True
        Me.DataGridViewTextBoxColumn13.HeaderText = "SalesOrderPackingItem_Index"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        Me.DataGridViewTextBoxColumn13.Width = 120
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "qty_pack"
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn14.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn14.Frozen = True
        Me.DataGridViewTextBoxColumn14.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 80
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "sku_id"
        Me.DataGridViewTextBoxColumn15.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn15.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 120
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "str1"
        Me.DataGridViewTextBoxColumn16.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Width = 150
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn17.HeaderText = "TotalQty"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Qty_Barcode"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Qty_Barcode"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Visible = False
        '
        'frmPackingBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1291, 660)
        Me.Controls.Add(Me.lblBarcodeBag)
        Me.Controls.Add(Me.txtBarcodeBAG)
        Me.Controls.Add(Me.btnConsignee)
        Me.Controls.Add(Me.txtConsignee_Id)
        Me.Controls.Add(Me.btnFrmClose)
        Me.Controls.Add(Me.btnClosedPacking)
        Me.Controls.Add(Me.grdSOView)
        Me.Controls.Add(Me.lblReservedLocation)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPackingBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงกล่อง"
        CType(Me.grdSOView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents lblReservedLocation As System.Windows.Forms.Label
    Friend WithEvents grdSOView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClosedPacking As System.Windows.Forms.Button
    Friend WithEvents btnFrmClose As System.Windows.Forms.Button
    Friend WithEvents Col_Exp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CYL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ASN_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnConsignee As System.Windows.Forms.Button
    Friend WithEvents txtConsignee_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcodeBAG As System.Windows.Forms.TextBox
    Friend WithEvents lblBarcodeBag As System.Windows.Forms.Label
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_SalesOrder_index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents route_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping_Location_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_BAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ConfirmBAG As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
