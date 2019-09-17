<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPR))
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tcoPRItem = New System.Windows.Forms.TabControl
        Me.tpaPRItem = New System.Windows.Forms.TabPage
        Me.dgvPRItem = New System.Windows.Forms.DataGridView
        Me.col_Item_Seq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_btnPopupSku = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_Sku_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ProductType_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Due_Date = New WMS_STD_Master.CalendarColumn
        Me.col_Weight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Volume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Received_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Pending_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Remark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_RequestItem_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pnlPRItemButton = New System.Windows.Forms.Panel
        Me.btnRemovePRItem = New System.Windows.Forms.Button
        Me.btnInsertPRItem = New System.Windows.Forms.Button
        Me.tpaReceived = New System.Windows.Forms.TabPage
        Me.dgvPRItem_PO = New System.Windows.Forms.DataGridView
        Me.col_PO_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_Sku_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_PurchaseOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_add_date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_Package_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PO_PackageSku_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gboPrintReport = New System.Windows.Forms.GroupBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.tcoHeader = New System.Windows.Forms.TabControl
        Me.tpaHeader = New System.Windows.Forms.TabPage
        Me.txtStatus_Desc = New System.Windows.Forms.TextBox
        Me.lblStatus_Desc = New System.Windows.Forms.Label
        Me.dtpDue_Date = New System.Windows.Forms.DateTimePicker
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.btnPopupCustomer = New System.Windows.Forms.Button
        Me.lblDue_Date = New System.Windows.Forms.Label
        Me.lblRef_No2 = New System.Windows.Forms.Label
        Me.txtRef_No2 = New System.Windows.Forms.TextBox
        Me.lblRef_No1 = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtRef_No1 = New System.Windows.Forms.TextBox
        Me.lblUser = New System.Windows.Forms.Label
        Me.lblPurchaseOrder_Request_Date = New System.Windows.Forms.Label
        Me.dtpPurchaseOrder_Request_Date = New System.Windows.Forms.DateTimePicker
        Me.txtPurchaseOrder_Request_No = New System.Windows.Forms.TextBox
        Me.lblRemark = New System.Windows.Forms.Label
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.lblPurchaseOrder_Request_No = New System.Windows.Forms.Label
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.tpaHide = New System.Windows.Forms.TabPage
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnClose_PR = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.pnlMainButton = New System.Windows.Forms.Panel
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn
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
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn37 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tcoPRItem.SuspendLayout()
        Me.tpaPRItem.SuspendLayout()
        CType(Me.dgvPRItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPRItemButton.SuspendLayout()
        Me.tpaReceived.SuspendLayout()
        CType(Me.dgvPRItem_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboPrintReport.SuspendLayout()
        Me.tcoHeader.SuspendLayout()
        Me.tpaHeader.SuspendLayout()
        Me.pnlMainButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcoPRItem
        '
        Me.tcoPRItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcoPRItem.Controls.Add(Me.tpaPRItem)
        Me.tcoPRItem.Controls.Add(Me.tpaReceived)
        Me.tcoPRItem.Location = New System.Drawing.Point(0, 125)
        Me.tcoPRItem.Name = "tcoPRItem"
        Me.tcoPRItem.SelectedIndex = 0
        Me.tcoPRItem.Size = New System.Drawing.Size(1001, 520)
        Me.tcoPRItem.TabIndex = 2
        '
        'tpaPRItem
        '
        Me.tpaPRItem.Controls.Add(Me.dgvPRItem)
        Me.tpaPRItem.Controls.Add(Me.pnlPRItemButton)
        Me.tpaPRItem.Location = New System.Drawing.Point(4, 22)
        Me.tpaPRItem.Name = "tpaPRItem"
        Me.tpaPRItem.Padding = New System.Windows.Forms.Padding(3)
        Me.tpaPRItem.Size = New System.Drawing.Size(993, 494)
        Me.tpaPRItem.TabIndex = 0
        Me.tpaPRItem.Text = "รายการคำขอสั่งซื้อ"
        Me.tpaPRItem.UseVisualStyleBackColor = True
        '
        'dgvPRItem
        '
        Me.dgvPRItem.AllowUserToDeleteRows = False
        Me.dgvPRItem.AllowUserToResizeRows = False
        Me.dgvPRItem.BackgroundColor = System.Drawing.Color.White
        Me.dgvPRItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPRItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Item_Seq, Me.col_Sku_Id, Me.col_btnPopupSku, Me.col_Sku_Desc, Me.col_ProductType_Desc, Me.col_Qty, Me.col_Package, Me.col_Due_Date, Me.col_Weight, Me.col_Volume, Me.col_Total_Qty, Me.col_Total_Received_Qty, Me.col_Total_Pending_Qty, Me.col_Remark, Me.col_PurchaseOrder_RequestItem_Index})
        Me.dgvPRItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPRItem.Location = New System.Drawing.Point(3, 3)
        Me.dgvPRItem.Name = "dgvPRItem"
        Me.dgvPRItem.RowHeadersVisible = False
        Me.dgvPRItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvPRItem.Size = New System.Drawing.Size(987, 447)
        Me.dgvPRItem.TabIndex = 0
        '
        'col_Item_Seq
        '
        Me.col_Item_Seq.DataPropertyName = "Item_Seq"
        DataGridViewCellStyle1.Format = "N0"
        Me.col_Item_Seq.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_Item_Seq.Frozen = True
        Me.col_Item_Seq.HeaderText = "ลำดับ"
        Me.col_Item_Seq.Name = "col_Item_Seq"
        Me.col_Item_Seq.ReadOnly = True
        Me.col_Item_Seq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Item_Seq.Width = 50
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_Sku_Id.Frozen = True
        Me.col_Sku_Id.HeaderText = "รหัส SKU"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_btnPopupSku
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.NullValue = "..."
        Me.col_btnPopupSku.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_btnPopupSku.Frozen = True
        Me.col_btnPopupSku.HeaderText = ""
        Me.col_btnPopupSku.Name = "col_btnPopupSku"
        Me.col_btnPopupSku.Text = ""
        Me.col_btnPopupSku.Width = 25
        '
        'col_Sku_Desc
        '
        Me.col_Sku_Desc.DataPropertyName = "Sku_Desc"
        Me.col_Sku_Desc.FillWeight = 150.0!
        Me.col_Sku_Desc.HeaderText = "รายละเอียด"
        Me.col_Sku_Desc.Name = "col_Sku_Desc"
        Me.col_Sku_Desc.ReadOnly = True
        Me.col_Sku_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Sku_Desc.Width = 150
        '
        'col_ProductType_Desc
        '
        Me.col_ProductType_Desc.DataPropertyName = "ProductType_Desc"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_ProductType_Desc.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_ProductType_Desc.FillWeight = 150.0!
        Me.col_ProductType_Desc.HeaderText = "ประเภทสินค้า"
        Me.col_ProductType_Desc.Name = "col_ProductType_Desc"
        Me.col_ProductType_Desc.ReadOnly = True
        Me.col_ProductType_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_ProductType_Desc.Width = 150
        '
        'col_Qty
        '
        Me.col_Qty.DataPropertyName = "Qty"
        DataGridViewCellStyle4.Format = "#,##0.######"
        DataGridViewCellStyle4.NullValue = "0"
        Me.col_Qty.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Qty.HeaderText = "จำนวนสั่ง"
        Me.col_Qty.Name = "col_Qty"
        Me.col_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Package
        '
        Me.col_Package.HeaderText = "หน่วย"
        Me.col_Package.Name = "col_Package"
        Me.col_Package.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Due_Date
        '
        Me.col_Due_Date.DataPropertyName = "Due_Date"
        Me.col_Due_Date.HeaderText = "กำหนดรับ"
        Me.col_Due_Date.Name = "col_Due_Date"
        Me.col_Due_Date.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_Due_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'col_Weight
        '
        Me.col_Weight.DataPropertyName = "Weight"
        DataGridViewCellStyle5.Format = "N4"
        DataGridViewCellStyle5.NullValue = "0"
        Me.col_Weight.DefaultCellStyle = DataGridViewCellStyle5
        Me.col_Weight.HeaderText = "น้ำหนัก"
        Me.col_Weight.Name = "col_Weight"
        Me.col_Weight.ReadOnly = True
        Me.col_Weight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Weight.Visible = False
        Me.col_Weight.Width = 105
        '
        'col_Volume
        '
        Me.col_Volume.DataPropertyName = "Volume"
        DataGridViewCellStyle6.Format = "N4"
        DataGridViewCellStyle6.NullValue = "0"
        Me.col_Volume.DefaultCellStyle = DataGridViewCellStyle6
        Me.col_Volume.HeaderText = "ปริมาตร"
        Me.col_Volume.Name = "col_Volume"
        Me.col_Volume.ReadOnly = True
        Me.col_Volume.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Volume.Visible = False
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle7.Format = "#,##0.######"
        DataGridViewCellStyle7.NullValue = "0"
        Me.col_Total_Qty.DefaultCellStyle = DataGridViewCellStyle7
        Me.col_Total_Qty.HeaderText = "สั่ง(ย่อย)"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        Me.col_Total_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Total_Received_Qty
        '
        Me.col_Total_Received_Qty.DataPropertyName = "Total_Received_Qty"
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle8.Format = "#,##0.######"
        DataGridViewCellStyle8.NullValue = "0"
        Me.col_Total_Received_Qty.DefaultCellStyle = DataGridViewCellStyle8
        Me.col_Total_Received_Qty.HeaderText = "รับแล้ว(ย่อย)"
        Me.col_Total_Received_Qty.Name = "col_Total_Received_Qty"
        Me.col_Total_Received_Qty.ReadOnly = True
        Me.col_Total_Received_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Total_Pending_Qty
        '
        Me.col_Total_Pending_Qty.DataPropertyName = "Total_Pending_Qty"
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle9.Format = "#,##0.######"
        DataGridViewCellStyle9.NullValue = "0"
        Me.col_Total_Pending_Qty.DefaultCellStyle = DataGridViewCellStyle9
        Me.col_Total_Pending_Qty.HeaderText = "ค้างรับ(ย่อย)"
        Me.col_Total_Pending_Qty.Name = "col_Total_Pending_Qty"
        Me.col_Total_Pending_Qty.ReadOnly = True
        Me.col_Total_Pending_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Remark
        '
        Me.col_Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.col_Remark.DataPropertyName = "Remark"
        Me.col_Remark.HeaderText = "หมายเหตุ"
        Me.col_Remark.MinimumWidth = 100
        Me.col_Remark.Name = "col_Remark"
        Me.col_Remark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_PurchaseOrder_RequestItem_Index
        '
        Me.col_PurchaseOrder_RequestItem_Index.DataPropertyName = "PurchaseOrder_RequestItem_Index"
        Me.col_PurchaseOrder_RequestItem_Index.HeaderText = "PurchaseOrder_RequestItem_Index"
        Me.col_PurchaseOrder_RequestItem_Index.Name = "col_PurchaseOrder_RequestItem_Index"
        Me.col_PurchaseOrder_RequestItem_Index.ReadOnly = True
        Me.col_PurchaseOrder_RequestItem_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_PurchaseOrder_RequestItem_Index.Visible = False
        '
        'pnlPRItemButton
        '
        Me.pnlPRItemButton.Controls.Add(Me.btnRemovePRItem)
        Me.pnlPRItemButton.Controls.Add(Me.btnInsertPRItem)
        Me.pnlPRItemButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPRItemButton.Location = New System.Drawing.Point(3, 450)
        Me.pnlPRItemButton.Name = "pnlPRItemButton"
        Me.pnlPRItemButton.Size = New System.Drawing.Size(987, 41)
        Me.pnlPRItemButton.TabIndex = 357
        '
        'btnRemovePRItem
        '
        Me.btnRemovePRItem.Image = CType(resources.GetObject("btnRemovePRItem.Image"), System.Drawing.Image)
        Me.btnRemovePRItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemovePRItem.Location = New System.Drawing.Point(0, 3)
        Me.btnRemovePRItem.Name = "btnRemovePRItem"
        Me.btnRemovePRItem.Size = New System.Drawing.Size(105, 38)
        Me.btnRemovePRItem.TabIndex = 1
        Me.btnRemovePRItem.Text = "       ลบรายการ"
        Me.btnRemovePRItem.UseVisualStyleBackColor = True
        '
        'btnInsertPRItem
        '
        Me.btnInsertPRItem.Image = CType(resources.GetObject("btnInsertPRItem.Image"), System.Drawing.Image)
        Me.btnInsertPRItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInsertPRItem.Location = New System.Drawing.Point(111, 3)
        Me.btnInsertPRItem.Name = "btnInsertPRItem"
        Me.btnInsertPRItem.Size = New System.Drawing.Size(108, 38)
        Me.btnInsertPRItem.TabIndex = 2
        Me.btnInsertPRItem.Text = "    แทรกรายการ"
        Me.btnInsertPRItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnInsertPRItem.UseVisualStyleBackColor = True
        '
        'tpaReceived
        '
        Me.tpaReceived.Controls.Add(Me.dgvPRItem_PO)
        Me.tpaReceived.Location = New System.Drawing.Point(4, 22)
        Me.tpaReceived.Name = "tpaReceived"
        Me.tpaReceived.Size = New System.Drawing.Size(993, 494)
        Me.tpaReceived.TabIndex = 2
        Me.tpaReceived.Text = "สินค้าที่รับแล้ว"
        Me.tpaReceived.UseVisualStyleBackColor = True
        '
        'dgvPRItem_PO
        '
        Me.dgvPRItem_PO.AllowUserToAddRows = False
        Me.dgvPRItem_PO.AllowUserToDeleteRows = False
        Me.dgvPRItem_PO.AllowUserToResizeRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvPRItem_PO.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvPRItem_PO.BackgroundColor = System.Drawing.Color.White
        Me.dgvPRItem_PO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPRItem_PO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_PO_Sku_Id, Me.col_PO_Sku_Desc, Me.col_PO_PurchaseOrder_No, Me.col_PO_add_date, Me.col_PO_Qty, Me.col_PO_Package_Desc, Me.col_PO_Total_Qty, Me.col_PO_PackageSku_Desc})
        Me.dgvPRItem_PO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPRItem_PO.Location = New System.Drawing.Point(0, 0)
        Me.dgvPRItem_PO.Name = "dgvPRItem_PO"
        Me.dgvPRItem_PO.ReadOnly = True
        Me.dgvPRItem_PO.RowHeadersVisible = False
        Me.dgvPRItem_PO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPRItem_PO.Size = New System.Drawing.Size(993, 494)
        Me.dgvPRItem_PO.TabIndex = 0
        '
        'col_PO_Sku_Id
        '
        Me.col_PO_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_PO_Sku_Id.HeaderText = "รหัส SKU"
        Me.col_PO_Sku_Id.Name = "col_PO_Sku_Id"
        Me.col_PO_Sku_Id.ReadOnly = True
        '
        'col_PO_Sku_Desc
        '
        Me.col_PO_Sku_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_PO_Sku_Desc.DataPropertyName = "Sku_Desc"
        Me.col_PO_Sku_Desc.FillWeight = 180.0!
        Me.col_PO_Sku_Desc.HeaderText = "รายละเอียดสินค้า"
        Me.col_PO_Sku_Desc.MinimumWidth = 180
        Me.col_PO_Sku_Desc.Name = "col_PO_Sku_Desc"
        Me.col_PO_Sku_Desc.ReadOnly = True
        '
        'col_PO_PurchaseOrder_No
        '
        Me.col_PO_PurchaseOrder_No.DataPropertyName = "PurchaseOrder_No"
        Me.col_PO_PurchaseOrder_No.FillWeight = 140.0!
        Me.col_PO_PurchaseOrder_No.HeaderText = "เลขที่ใบสั่งซื้อ"
        Me.col_PO_PurchaseOrder_No.Name = "col_PO_PurchaseOrder_No"
        Me.col_PO_PurchaseOrder_No.ReadOnly = True
        Me.col_PO_PurchaseOrder_No.Width = 140
        '
        'col_PO_add_date
        '
        Me.col_PO_add_date.DataPropertyName = "add_date"
        Me.col_PO_add_date.HeaderText = "วันที่รับ"
        Me.col_PO_add_date.Name = "col_PO_add_date"
        Me.col_PO_add_date.ReadOnly = True
        '
        'col_PO_Qty
        '
        Me.col_PO_Qty.DataPropertyName = "Qty"
        DataGridViewCellStyle11.Format = "#,##0.######"
        DataGridViewCellStyle11.NullValue = "0"
        Me.col_PO_Qty.DefaultCellStyle = DataGridViewCellStyle11
        Me.col_PO_Qty.HeaderText = "จำนวน"
        Me.col_PO_Qty.Name = "col_PO_Qty"
        Me.col_PO_Qty.ReadOnly = True
        '
        'col_PO_Package_Desc
        '
        Me.col_PO_Package_Desc.DataPropertyName = "Package_Desc"
        Me.col_PO_Package_Desc.HeaderText = "หน่วย"
        Me.col_PO_Package_Desc.Name = "col_PO_Package_Desc"
        Me.col_PO_Package_Desc.ReadOnly = True
        '
        'col_PO_Total_Qty
        '
        Me.col_PO_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle12.Format = "#,##0.######"
        DataGridViewCellStyle12.NullValue = "0"
        Me.col_PO_Total_Qty.DefaultCellStyle = DataGridViewCellStyle12
        Me.col_PO_Total_Qty.HeaderText = "จำนวน(ย่อย)"
        Me.col_PO_Total_Qty.Name = "col_PO_Total_Qty"
        Me.col_PO_Total_Qty.ReadOnly = True
        '
        'col_PO_PackageSku_Desc
        '
        Me.col_PO_PackageSku_Desc.DataPropertyName = "PackageSku_Desc"
        Me.col_PO_PackageSku_Desc.HeaderText = "หน่วย(ย่อย)"
        Me.col_PO_PackageSku_Desc.Name = "col_PO_PackageSku_Desc"
        Me.col_PO_PackageSku_Desc.ReadOnly = True
        '
        'gboPrintReport
        '
        Me.gboPrintReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboPrintReport.Controls.Add(Me.btnPrint)
        Me.gboPrintReport.Controls.Add(Me.cboPrint)
        Me.gboPrintReport.Location = New System.Drawing.Point(622, 647)
        Me.gboPrintReport.Name = "gboPrintReport"
        Me.gboPrintReport.Size = New System.Drawing.Size(293, 51)
        Me.gboPrintReport.TabIndex = 3
        Me.gboPrintReport.TabStop = False
        Me.gboPrintReport.Text = "พิมพ์เอกสาร"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(182, 10)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(105, 38)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "พิมพ์รายงาน"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'cboPrint
        '
        Me.cboPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Items.AddRange(New Object() {"ใบสั่งงานนำสินค้าออก"})
        Me.cboPrint.Location = New System.Drawing.Point(6, 20)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(170, 21)
        Me.cboPrint.TabIndex = 2
        '
        'tcoHeader
        '
        Me.tcoHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcoHeader.Controls.Add(Me.tpaHeader)
        Me.tcoHeader.Controls.Add(Me.tpaHide)
        Me.tcoHeader.Location = New System.Drawing.Point(0, 0)
        Me.tcoHeader.Name = "tcoHeader"
        Me.tcoHeader.SelectedIndex = 0
        Me.tcoHeader.Size = New System.Drawing.Size(1001, 123)
        Me.tcoHeader.TabIndex = 1
        Me.tcoHeader.TabStop = False
        '
        'tpaHeader
        '
        Me.tpaHeader.Controls.Add(Me.txtStatus_Desc)
        Me.tpaHeader.Controls.Add(Me.lblStatus_Desc)
        Me.tpaHeader.Controls.Add(Me.dtpDue_Date)
        Me.tpaHeader.Controls.Add(Me.txtCustomer_Id)
        Me.tpaHeader.Controls.Add(Me.txtCustomer_Name)
        Me.tpaHeader.Controls.Add(Me.btnPopupCustomer)
        Me.tpaHeader.Controls.Add(Me.lblDue_Date)
        Me.tpaHeader.Controls.Add(Me.lblRef_No2)
        Me.tpaHeader.Controls.Add(Me.txtRef_No2)
        Me.tpaHeader.Controls.Add(Me.lblRef_No1)
        Me.tpaHeader.Controls.Add(Me.txtUser)
        Me.tpaHeader.Controls.Add(Me.txtRef_No1)
        Me.tpaHeader.Controls.Add(Me.lblUser)
        Me.tpaHeader.Controls.Add(Me.lblPurchaseOrder_Request_Date)
        Me.tpaHeader.Controls.Add(Me.dtpPurchaseOrder_Request_Date)
        Me.tpaHeader.Controls.Add(Me.txtPurchaseOrder_Request_No)
        Me.tpaHeader.Controls.Add(Me.lblRemark)
        Me.tpaHeader.Controls.Add(Me.txtRemark)
        Me.tpaHeader.Controls.Add(Me.cboDocumentType)
        Me.tpaHeader.Controls.Add(Me.lblCustomer)
        Me.tpaHeader.Controls.Add(Me.lblPurchaseOrder_Request_No)
        Me.tpaHeader.Controls.Add(Me.lblDocumentType)
        Me.tpaHeader.Location = New System.Drawing.Point(4, 22)
        Me.tpaHeader.Name = "tpaHeader"
        Me.tpaHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpaHeader.Size = New System.Drawing.Size(993, 97)
        Me.tpaHeader.TabIndex = 2
        Me.tpaHeader.Text = "ข้อมูลเอกสาร"
        Me.tpaHeader.UseVisualStyleBackColor = True
        '
        'txtStatus_Desc
        '
        Me.txtStatus_Desc.Location = New System.Drawing.Point(335, 29)
        Me.txtStatus_Desc.MaxLength = 100
        Me.txtStatus_Desc.Name = "txtStatus_Desc"
        Me.txtStatus_Desc.ReadOnly = True
        Me.txtStatus_Desc.Size = New System.Drawing.Size(135, 20)
        Me.txtStatus_Desc.TabIndex = 8
        Me.txtStatus_Desc.TabStop = False
        Me.txtStatus_Desc.Visible = False
        '
        'lblStatus_Desc
        '
        Me.lblStatus_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblStatus_Desc.Location = New System.Drawing.Point(235, 33)
        Me.lblStatus_Desc.Name = "lblStatus_Desc"
        Me.lblStatus_Desc.Size = New System.Drawing.Size(94, 13)
        Me.lblStatus_Desc.TabIndex = 7
        Me.lblStatus_Desc.Text = "สถานะ"
        Me.lblStatus_Desc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus_Desc.Visible = False
        '
        'dtpDue_Date
        '
        Me.dtpDue_Date.Checked = False
        Me.dtpDue_Date.CustomFormat = "dd/MM/yyyy"
        Me.dtpDue_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDue_Date.Location = New System.Drawing.Point(835, 29)
        Me.dtpDue_Date.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.dtpDue_Date.Name = "dtpDue_Date"
        Me.dtpDue_Date.ShowCheckBox = True
        Me.dtpDue_Date.Size = New System.Drawing.Size(135, 20)
        Me.dtpDue_Date.TabIndex = 20
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(100, 52)
        Me.txtCustomer_Id.MaxLength = 50
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomer_Id.TabIndex = 10
        Me.txtCustomer_Id.TabStop = False
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Name.Location = New System.Drawing.Point(236, 52)
        Me.txtCustomer_Name.MaxLength = 500
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(234, 20)
        Me.txtCustomer_Name.TabIndex = 12
        Me.txtCustomer_Name.TabStop = False
        '
        'btnPopupCustomer
        '
        Me.btnPopupCustomer.Location = New System.Drawing.Point(209, 53)
        Me.btnPopupCustomer.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.btnPopupCustomer.Name = "btnPopupCustomer"
        Me.btnPopupCustomer.Size = New System.Drawing.Size(24, 20)
        Me.btnPopupCustomer.TabIndex = 11
        Me.btnPopupCustomer.Text = "..."
        Me.btnPopupCustomer.UseVisualStyleBackColor = True
        '
        'lblDue_Date
        '
        Me.lblDue_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDue_Date.Location = New System.Drawing.Point(735, 33)
        Me.lblDue_Date.Name = "lblDue_Date"
        Me.lblDue_Date.Size = New System.Drawing.Size(94, 13)
        Me.lblDue_Date.TabIndex = 19
        Me.lblDue_Date.Text = "วันที่กำหนดรับ"
        Me.lblDue_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRef_No2
        '
        Me.lblRef_No2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRef_No2.Location = New System.Drawing.Point(500, 33)
        Me.lblRef_No2.Name = "lblRef_No2"
        Me.lblRef_No2.Size = New System.Drawing.Size(94, 13)
        Me.lblRef_No2.TabIndex = 15
        Me.lblRef_No2.Text = "เอกสารอ้างอิง 2"
        Me.lblRef_No2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRef_No2
        '
        Me.txtRef_No2.Location = New System.Drawing.Point(600, 29)
        Me.txtRef_No2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtRef_No2.MaxLength = 100
        Me.txtRef_No2.Name = "txtRef_No2"
        Me.txtRef_No2.Size = New System.Drawing.Size(135, 20)
        Me.txtRef_No2.TabIndex = 16
        '
        'lblRef_No1
        '
        Me.lblRef_No1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRef_No1.Location = New System.Drawing.Point(500, 10)
        Me.lblRef_No1.Name = "lblRef_No1"
        Me.lblRef_No1.Size = New System.Drawing.Size(94, 13)
        Me.lblRef_No1.TabIndex = 13
        Me.lblRef_No1.Text = "เอกสารอ้างอิง 1"
        Me.lblRef_No1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUser.Location = New System.Drawing.Point(835, 6)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtUser.MaxLength = 50
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.Size = New System.Drawing.Size(135, 20)
        Me.txtUser.TabIndex = 18
        Me.txtUser.TabStop = False
        '
        'txtRef_No1
        '
        Me.txtRef_No1.Location = New System.Drawing.Point(600, 6)
        Me.txtRef_No1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtRef_No1.MaxLength = 100
        Me.txtRef_No1.Name = "txtRef_No1"
        Me.txtRef_No1.Size = New System.Drawing.Size(135, 20)
        Me.txtRef_No1.TabIndex = 14
        '
        'lblUser
        '
        Me.lblUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUser.Location = New System.Drawing.Point(735, 10)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(94, 13)
        Me.lblUser.TabIndex = 17
        Me.lblUser.Text = "ผู้ทำรายการ"
        Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPurchaseOrder_Request_Date
        '
        Me.lblPurchaseOrder_Request_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPurchaseOrder_Request_Date.Location = New System.Drawing.Point(235, 10)
        Me.lblPurchaseOrder_Request_Date.Name = "lblPurchaseOrder_Request_Date"
        Me.lblPurchaseOrder_Request_Date.Size = New System.Drawing.Size(94, 13)
        Me.lblPurchaseOrder_Request_Date.TabIndex = 3
        Me.lblPurchaseOrder_Request_Date.Text = "วันที่เอกสาร"
        Me.lblPurchaseOrder_Request_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpPurchaseOrder_Request_Date
        '
        Me.dtpPurchaseOrder_Request_Date.CustomFormat = "dd/MM/yyyy"
        Me.dtpPurchaseOrder_Request_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPurchaseOrder_Request_Date.Location = New System.Drawing.Point(335, 6)
        Me.dtpPurchaseOrder_Request_Date.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.dtpPurchaseOrder_Request_Date.Name = "dtpPurchaseOrder_Request_Date"
        Me.dtpPurchaseOrder_Request_Date.Size = New System.Drawing.Size(135, 20)
        Me.dtpPurchaseOrder_Request_Date.TabIndex = 4
        Me.dtpPurchaseOrder_Request_Date.TabStop = False
        '
        'txtPurchaseOrder_Request_No
        '
        Me.txtPurchaseOrder_Request_No.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtPurchaseOrder_Request_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtPurchaseOrder_Request_No.ForeColor = System.Drawing.Color.Black
        Me.txtPurchaseOrder_Request_No.Location = New System.Drawing.Point(100, 6)
        Me.txtPurchaseOrder_Request_No.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtPurchaseOrder_Request_No.MaxLength = 50
        Me.txtPurchaseOrder_Request_No.Name = "txtPurchaseOrder_Request_No"
        Me.txtPurchaseOrder_Request_No.Size = New System.Drawing.Size(135, 20)
        Me.txtPurchaseOrder_Request_No.TabIndex = 2
        Me.txtPurchaseOrder_Request_No.TabStop = False
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(500, 56)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(94, 13)
        Me.lblRemark.TabIndex = 21
        Me.lblRemark.Text = "หมายเหตุ"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Location = New System.Drawing.Point(600, 52)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(370, 42)
        Me.txtRemark.TabIndex = 22
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(100, 29)
        Me.cboDocumentType.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(135, 21)
        Me.cboDocumentType.TabIndex = 6
        Me.cboDocumentType.TabStop = False
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(0, 55)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(94, 13)
        Me.lblCustomer.TabIndex = 9
        Me.lblCustomer.Text = "ลูกค้า/เจ้าของ"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPurchaseOrder_Request_No
        '
        Me.lblPurchaseOrder_Request_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPurchaseOrder_Request_No.Location = New System.Drawing.Point(0, 10)
        Me.lblPurchaseOrder_Request_No.Name = "lblPurchaseOrder_Request_No"
        Me.lblPurchaseOrder_Request_No.Size = New System.Drawing.Size(94, 13)
        Me.lblPurchaseOrder_Request_No.TabIndex = 1
        Me.lblPurchaseOrder_Request_No.Text = "เลขที่เอกสาร"
        Me.lblPurchaseOrder_Request_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentType
        '
        Me.lblDocumentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDocumentType.ForeColor = System.Drawing.Color.Black
        Me.lblDocumentType.Location = New System.Drawing.Point(0, 33)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(94, 13)
        Me.lblDocumentType.TabIndex = 5
        Me.lblDocumentType.Text = "ประเภทเอกสาร"
        Me.lblDocumentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tpaHide
        '
        Me.tpaHide.Location = New System.Drawing.Point(4, 22)
        Me.tpaHide.Name = "tpaHide"
        Me.tpaHide.Padding = New System.Windows.Forms.Padding(3)
        Me.tpaHide.Size = New System.Drawing.Size(993, 97)
        Me.tpaHide.TabIndex = 3
        Me.tpaHide.Text = "ซ่อน"
        Me.tpaHide.UseVisualStyleBackColor = True
        '
        'btnConfirm
        '
        Me.btnConfirm.Enabled = False
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(222, 0)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(105, 38)
        Me.btnConfirm.TabIndex = 3
        Me.btnConfirm.Text = "    ยืนยัน"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnClose_PR
        '
        Me.btnClose_PR.Enabled = False
        Me.btnClose_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClose_PR.Image = CType(resources.GetObject("btnClose_PR.Image"), System.Drawing.Image)
        Me.btnClose_PR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose_PR.Location = New System.Drawing.Point(111, 0)
        Me.btnClose_PR.Name = "btnClose_PR"
        Me.btnClose_PR.Size = New System.Drawing.Size(105, 38)
        Me.btnClose_PR.TabIndex = 2
        Me.btnClose_PR.Text = "ปิดเอกสาร"
        Me.btnClose_PR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose_PR.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(917, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 38)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "ออก"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'pnlMainButton
        '
        Me.pnlMainButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMainButton.Controls.Add(Me.btnSave)
        Me.pnlMainButton.Controls.Add(Me.btnClose)
        Me.pnlMainButton.Controls.Add(Me.btnConfirm)
        Me.pnlMainButton.Controls.Add(Me.btnClose_PR)
        Me.pnlMainButton.Location = New System.Drawing.Point(4, 651)
        Me.pnlMainButton.Name = "pnlMainButton"
        Me.pnlMainButton.Size = New System.Drawing.Size(997, 38)
        Me.pnlMainButton.TabIndex = 4
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Item_Seq"
        DataGridViewCellStyle13.Format = "N0"
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "OrderItem_Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewButtonColumn1
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.NullValue = "..."
        Me.DataGridViewButtonColumn1.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewButtonColumn1.Frozen = True
        Me.DataGridViewButtonColumn1.HeaderText = ""
        Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
        Me.DataGridViewButtonColumn1.Text = ""
        Me.DataGridViewButtonColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Desc"
        Me.DataGridViewTextBoxColumn3.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "ProductType_Desc"
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewTextBoxColumn4.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "Product_Name_th"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 70
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Qty"
        DataGridViewCellStyle16.NullValue = "0"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle16
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 70
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle17.NullValue = "0"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle17
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "Plan_Qty"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 300
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Weight"
        DataGridViewCellStyle18.NullValue = "0"
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle18
        Me.DataGridViewTextBoxColumn7.Frozen = True
        Me.DataGridViewTextBoxColumn7.HeaderText = "Pallet_Quantity"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 300
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Volume"
        DataGridViewCellStyle19.NullValue = "0"
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle19
        Me.DataGridViewTextBoxColumn8.Frozen = True
        Me.DataGridViewTextBoxColumn8.HeaderText = "Weight"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 150
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle20.NullValue = "0"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle20
        Me.DataGridViewTextBoxColumn9.Frozen = True
        Me.DataGridViewTextBoxColumn9.HeaderText = "Volume"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Total_Received_Qty"
        DataGridViewCellStyle21.NullValue = "0"
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle21
        Me.DataGridViewTextBoxColumn10.Frozen = True
        Me.DataGridViewTextBoxColumn10.HeaderText = "Serial_No"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 105
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Total_Pending_Qty"
        DataGridViewCellStyle22.NullValue = "0"
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle22
        Me.DataGridViewTextBoxColumn11.Frozen = True
        Me.DataGridViewTextBoxColumn11.HeaderText = "Mfg_Date"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 150
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Remark"
        Me.DataGridViewTextBoxColumn12.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn12.Frozen = True
        Me.DataGridViewTextBoxColumn12.HeaderText = "Exp_date"
        Me.DataGridViewTextBoxColumn12.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "PurchaseOrder_RequestItem_Index"
        DataGridViewCellStyle23.NullValue = "0"
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle23
        Me.DataGridViewTextBoxColumn13.Frozen = True
        Me.DataGridViewTextBoxColumn13.HeaderText = "PLot"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn13.Visible = False
        Me.DataGridViewTextBoxColumn13.Width = 105
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn14.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn14.Frozen = True
        Me.DataGridViewTextBoxColumn14.HeaderText = "Item_Status_Index"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Total_Received_Qty"
        Me.DataGridViewTextBoxColumn15.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn15.HeaderText = "Package_Index"
        Me.DataGridViewTextBoxColumn15.MinimumWidth = 180
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "PurchaseOrder_Index"
        Me.DataGridViewTextBoxColumn16.FillWeight = 140.0!
        Me.DataGridViewTextBoxColumn16.HeaderText = "Item_Status_Index"
        Me.DataGridViewTextBoxColumn16.MinimumWidth = 180
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn17.FillWeight = 140.0!
        Me.DataGridViewTextBoxColumn17.HeaderText = "ประเภทสินค้า"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 140
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn18.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Visible = False
        Me.DataGridViewTextBoxColumn18.Width = 180
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn19.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Visible = False
        Me.DataGridViewTextBoxColumn19.Width = 80
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "DesPack"
        Me.DataGridViewTextBoxColumn20.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Visible = False
        Me.DataGridViewTextBoxColumn20.Width = 80
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "PurQty"
        Me.DataGridViewTextBoxColumn21.HeaderText = "ค้างร้บ"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 180
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "Last_Received_Date"
        Me.DataGridViewTextBoxColumn22.HeaderText = "วันที่รับ"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "PurchaseOrderItem_Index"
        Me.DataGridViewTextBoxColumn23.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.Visible = False
        Me.DataGridViewTextBoxColumn23.Width = 80
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn24.HeaderText = "รหัส SKU"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Width = 180
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "Order_No"
        Me.DataGridViewTextBoxColumn25.HeaderText = "เอกสารรับเลขที่"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn27.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.Visible = False
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn28.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Visible = False
        Me.DataGridViewTextBoxColumn28.Width = 80
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn29.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Visible = False
        Me.DataGridViewTextBoxColumn29.Width = 80
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "UnitPrice"
        Me.DataGridViewTextBoxColumn30.HeaderText = "ราคา/หน่วย"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Width = 90
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn31.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Visible = False
        Me.DataGridViewTextBoxColumn31.Width = 80
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn32.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn33.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        Me.DataGridViewTextBoxColumn33.Width = 80
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn34.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        Me.DataGridViewTextBoxColumn34.Visible = False
        Me.DataGridViewTextBoxColumn34.Width = 90
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn35.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn36.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Visible = False
        '
        'DataGridViewTextBoxColumn37
        '
        Me.DataGridViewTextBoxColumn37.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn37.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn37.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn37.Name = "DataGridViewTextBoxColumn37"
        Me.DataGridViewTextBoxColumn37.ReadOnly = True
        '
        'frmPR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 701)
        Me.Controls.Add(Me.gboPrintReport)
        Me.Controls.Add(Me.pnlMainButton)
        Me.Controls.Add(Me.tcoHeader)
        Me.Controls.Add(Me.tcoPRItem)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(990, 440)
        Me.Name = "frmPR"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ใบคำขอสั่งซื้อ"
        Me.tcoPRItem.ResumeLayout(False)
        Me.tpaPRItem.ResumeLayout(False)
        CType(Me.dgvPRItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPRItemButton.ResumeLayout(False)
        Me.tpaReceived.ResumeLayout(False)
        CType(Me.dgvPRItem_PO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboPrintReport.ResumeLayout(False)
        Me.tcoHeader.ResumeLayout(False)
        Me.tpaHeader.ResumeLayout(False)
        Me.tpaHeader.PerformLayout()
        Me.pnlMainButton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tcoPRItem As System.Windows.Forms.TabControl
    Friend WithEvents tpaPRItem As System.Windows.Forms.TabPage
    Friend WithEvents dgvPRItem As System.Windows.Forms.DataGridView
    Friend WithEvents btnRemovePRItem As System.Windows.Forms.Button
    Friend WithEvents tpaReceived As System.Windows.Forms.TabPage
    Friend WithEvents dgvPRItem_PO As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gboPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose_PR As System.Windows.Forms.Button
    Friend WithEvents btnInsertPRItem As System.Windows.Forms.Button
    Friend WithEvents tcoHeader As System.Windows.Forms.TabControl
    Friend WithEvents tpaHide As System.Windows.Forms.TabPage
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents tpaHeader As System.Windows.Forms.TabPage
    Friend WithEvents txtStatus_Desc As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus_Desc As System.Windows.Forms.Label
    Friend WithEvents dtpDue_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnPopupCustomer As System.Windows.Forms.Button
    Friend WithEvents lblDue_Date As System.Windows.Forms.Label
    Friend WithEvents lblRef_No2 As System.Windows.Forms.Label
    Friend WithEvents txtRef_No2 As System.Windows.Forms.TextBox
    Friend WithEvents lblRef_No1 As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtRef_No1 As System.Windows.Forms.TextBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseOrder_Request_Date As System.Windows.Forms.Label
    Friend WithEvents dtpPurchaseOrder_Request_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPurchaseOrder_Request_No As System.Windows.Forms.TextBox
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseOrder_Request_No As System.Windows.Forms.Label
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents pnlPRItemButton As System.Windows.Forms.Panel
    Friend WithEvents pnlMainButton As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_PO_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_Sku_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_PurchaseOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_add_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_Package_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PO_PackageSku_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Item_Seq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_btnPopupSku As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_Sku_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ProductType_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Due_Date As WMS_STD_Master.CalendarColumn
    Friend WithEvents col_Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Volume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Received_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Pending_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_RequestItem_Index As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
