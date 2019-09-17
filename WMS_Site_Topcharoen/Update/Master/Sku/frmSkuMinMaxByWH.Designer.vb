<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSkuMinMaxByWH
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSkuMinMaxByWH))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.gboSummary = New System.Windows.Forms.GroupBox
        Me.txtMaxSku_Package = New System.Windows.Forms.TextBox
        Me.txtMaxSku_Qty = New System.Windows.Forms.TextBox
        Me.lblMaxSku = New System.Windows.Forms.Label
        Me.txtMinSku_Package = New System.Windows.Forms.TextBox
        Me.txtMinSku_Qty = New System.Windows.Forms.TextBox
        Me.lblMinSku = New System.Windows.Forms.Label
        Me.txtSku_Desc = New System.Windows.Forms.TextBox
        Me.lblSku_Desc = New System.Windows.Forms.Label
        Me.txtSku_Id = New System.Windows.Forms.TextBox
        Me.lblSku_Id = New System.Windows.Forms.Label
        Me.gboData = New System.Windows.Forms.GroupBox
        Me.dgvData = New System.Windows.Forms.DataGridView
        Me.pnlData_Bottom = New System.Windows.Forms.Panel
        Me.lblData_Count = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
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
        Me.col_Running_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Warehouse_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Min_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Max_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gboSummary.SuspendLayout()
        Me.gboData.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlData_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboSummary
        '
        Me.gboSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboSummary.Controls.Add(Me.txtMaxSku_Package)
        Me.gboSummary.Controls.Add(Me.txtMaxSku_Qty)
        Me.gboSummary.Controls.Add(Me.lblMaxSku)
        Me.gboSummary.Controls.Add(Me.txtMinSku_Package)
        Me.gboSummary.Controls.Add(Me.txtMinSku_Qty)
        Me.gboSummary.Controls.Add(Me.lblMinSku)
        Me.gboSummary.Controls.Add(Me.txtSku_Desc)
        Me.gboSummary.Controls.Add(Me.lblSku_Desc)
        Me.gboSummary.Controls.Add(Me.txtSku_Id)
        Me.gboSummary.Controls.Add(Me.lblSku_Id)
        Me.gboSummary.Location = New System.Drawing.Point(12, 12)
        Me.gboSummary.Name = "gboSummary"
        Me.gboSummary.Size = New System.Drawing.Size(460, 137)
        Me.gboSummary.TabIndex = 0
        Me.gboSummary.TabStop = False
        Me.gboSummary.Text = "สรุปข้อมูล"
        '
        'txtMaxSku_Package
        '
        Me.txtMaxSku_Package.Location = New System.Drawing.Point(233, 111)
        Me.txtMaxSku_Package.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtMaxSku_Package.Name = "txtMaxSku_Package"
        Me.txtMaxSku_Package.ReadOnly = True
        Me.txtMaxSku_Package.Size = New System.Drawing.Size(117, 20)
        Me.txtMaxSku_Package.TabIndex = 10
        Me.txtMaxSku_Package.TabStop = False
        '
        'txtMaxSku_Qty
        '
        Me.txtMaxSku_Qty.Location = New System.Drawing.Point(100, 111)
        Me.txtMaxSku_Qty.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtMaxSku_Qty.Name = "txtMaxSku_Qty"
        Me.txtMaxSku_Qty.ReadOnly = True
        Me.txtMaxSku_Qty.Size = New System.Drawing.Size(130, 20)
        Me.txtMaxSku_Qty.TabIndex = 9
        Me.txtMaxSku_Qty.TabStop = False
        '
        'lblMaxSku
        '
        Me.lblMaxSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMaxSku.Location = New System.Drawing.Point(6, 111)
        Me.lblMaxSku.Name = "lblMaxSku"
        Me.lblMaxSku.Size = New System.Drawing.Size(88, 20)
        Me.lblMaxSku.TabIndex = 8
        Me.lblMaxSku.Text = "Max (รวม)"
        Me.lblMaxSku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMinSku_Package
        '
        Me.txtMinSku_Package.Location = New System.Drawing.Point(233, 88)
        Me.txtMinSku_Package.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtMinSku_Package.Name = "txtMinSku_Package"
        Me.txtMinSku_Package.ReadOnly = True
        Me.txtMinSku_Package.Size = New System.Drawing.Size(117, 20)
        Me.txtMinSku_Package.TabIndex = 7
        Me.txtMinSku_Package.TabStop = False
        '
        'txtMinSku_Qty
        '
        Me.txtMinSku_Qty.Location = New System.Drawing.Point(100, 88)
        Me.txtMinSku_Qty.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtMinSku_Qty.Name = "txtMinSku_Qty"
        Me.txtMinSku_Qty.ReadOnly = True
        Me.txtMinSku_Qty.Size = New System.Drawing.Size(130, 20)
        Me.txtMinSku_Qty.TabIndex = 6
        Me.txtMinSku_Qty.TabStop = False
        '
        'lblMinSku
        '
        Me.lblMinSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMinSku.Location = New System.Drawing.Point(6, 88)
        Me.lblMinSku.Name = "lblMinSku"
        Me.lblMinSku.Size = New System.Drawing.Size(88, 20)
        Me.lblMinSku.TabIndex = 5
        Me.lblMinSku.Text = "Min (รวม)"
        Me.lblMinSku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSku_Desc
        '
        Me.txtSku_Desc.Location = New System.Drawing.Point(100, 42)
        Me.txtSku_Desc.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtSku_Desc.Multiline = True
        Me.txtSku_Desc.Name = "txtSku_Desc"
        Me.txtSku_Desc.ReadOnly = True
        Me.txtSku_Desc.Size = New System.Drawing.Size(250, 43)
        Me.txtSku_Desc.TabIndex = 4
        Me.txtSku_Desc.TabStop = False
        '
        'lblSku_Desc
        '
        Me.lblSku_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku_Desc.Location = New System.Drawing.Point(6, 42)
        Me.lblSku_Desc.Name = "lblSku_Desc"
        Me.lblSku_Desc.Size = New System.Drawing.Size(88, 20)
        Me.lblSku_Desc.TabIndex = 3
        Me.lblSku_Desc.Text = "ชื่อสินค้า"
        Me.lblSku_Desc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSku_Id
        '
        Me.txtSku_Id.Location = New System.Drawing.Point(100, 19)
        Me.txtSku_Id.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.txtSku_Id.Name = "txtSku_Id"
        Me.txtSku_Id.ReadOnly = True
        Me.txtSku_Id.Size = New System.Drawing.Size(130, 20)
        Me.txtSku_Id.TabIndex = 2
        Me.txtSku_Id.TabStop = False
        '
        'lblSku_Id
        '
        Me.lblSku_Id.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku_Id.Location = New System.Drawing.Point(6, 19)
        Me.lblSku_Id.Name = "lblSku_Id"
        Me.lblSku_Id.Size = New System.Drawing.Size(88, 20)
        Me.lblSku_Id.TabIndex = 1
        Me.lblSku_Id.Text = "รหัสสินค้า"
        Me.lblSku_Id.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gboData
        '
        Me.gboData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboData.Controls.Add(Me.dgvData)
        Me.gboData.Controls.Add(Me.pnlData_Bottom)
        Me.gboData.Location = New System.Drawing.Point(12, 155)
        Me.gboData.Name = "gboData"
        Me.gboData.Size = New System.Drawing.Size(460, 350)
        Me.gboData.TabIndex = 1
        Me.gboData.TabStop = False
        Me.gboData.Text = "ข้อมูล"
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.AllowUserToResizeRows = False
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Running_Index, Me.col_Warehouse_Desc, Me.col_Min_Qty, Me.col_Max_Qty})
        Me.dgvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvData.Location = New System.Drawing.Point(3, 16)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvData.Size = New System.Drawing.Size(454, 311)
        Me.dgvData.TabIndex = 0
        '
        'pnlData_Bottom
        '
        Me.pnlData_Bottom.Controls.Add(Me.lblData_Count)
        Me.pnlData_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlData_Bottom.Location = New System.Drawing.Point(3, 327)
        Me.pnlData_Bottom.Name = "pnlData_Bottom"
        Me.pnlData_Bottom.Size = New System.Drawing.Size(454, 20)
        Me.pnlData_Bottom.TabIndex = 1
        '
        'lblData_Count
        '
        Me.lblData_Count.AutoSize = True
        Me.lblData_Count.ForeColor = System.Drawing.Color.Blue
        Me.lblData_Count.Location = New System.Drawing.Point(3, 3)
        Me.lblData_Count.Name = "lblData_Count"
        Me.lblData_Count.Size = New System.Drawing.Size(0, 13)
        Me.lblData_Count.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(372, 511)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 38)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "ออก"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(15, 511)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "PO_Date"
        DataGridViewCellStyle3.Format = "dd/MM/yyyy"
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.HeaderText = "วันที่ออกเอกสาร"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 120
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 150
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Expected_Delivery_Date"
        DataGridViewCellStyle4.Format = "dd/MM/yyyy"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่กำหนดส่ง"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Sku_Id"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "#,##0.######"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn4.HeaderText = "รหัสสินค้า WMS"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Sku_Cust"
        Me.DataGridViewTextBoxColumn5.HeaderText = "รหัสสินค้า WINSpeet/ERP"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn6.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Qty"
        DataGridViewCellStyle6.Format = "N2"
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวนที่สั่งซื้อ"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Package_Desc"
        DataGridViewCellStyle7.Format = "dd/MM/yyyy"
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn8.HeaderText = "หน่วยนับ"
        Me.DataGridViewTextBoxColumn8.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "UnitPrice"
        DataGridViewCellStyle8.Format = "N2"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn9.HeaderText = "ราคา/หน่วย"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "IsStatus1"
        Me.DataGridViewTextBoxColumn10.HeaderText = "รอยืนยัน"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "IsStatus2"
        Me.DataGridViewTextBoxColumn11.HeaderText = "ค้างส่ง"
        Me.DataGridViewTextBoxColumn11.MinimumWidth = 120
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "IsStatus3"
        Me.DataGridViewTextBoxColumn12.HeaderText = "เสร็จสิ้น"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn12.Width = 80
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Remark"
        Me.DataGridViewTextBoxColumn13.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn13.Width = 150
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Total_Received_Qty"
        DataGridViewCellStyle9.Format = "N2"
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn14.HeaderText = "สินค้ารับแล้ว"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Total_Padding_Qty"
        DataGridViewCellStyle10.Format = "N2"
        Me.DataGridViewTextBoxColumn15.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn15.HeaderText = "ค้างรับ"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Status_Desc"
        Me.DataGridViewTextBoxColumn16.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_Running_Index
        '
        Me.col_Running_Index.DataPropertyName = "Running_Index"
        Me.col_Running_Index.HeaderText = "Running_Index"
        Me.col_Running_Index.Name = "col_Running_Index"
        Me.col_Running_Index.ReadOnly = True
        Me.col_Running_Index.Visible = False
        '
        'col_Warehouse_Desc
        '
        Me.col_Warehouse_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Warehouse_Desc.DataPropertyName = "Warehouse_Desc"
        Me.col_Warehouse_Desc.HeaderText = "คลัง"
        Me.col_Warehouse_Desc.MinimumWidth = 150
        Me.col_Warehouse_Desc.Name = "col_Warehouse_Desc"
        Me.col_Warehouse_Desc.ReadOnly = True
        '
        'col_Min_Qty
        '
        Me.col_Min_Qty.DataPropertyName = "Min_Qty"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "#,##0.######"
        Me.col_Min_Qty.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_Min_Qty.HeaderText = "Min"
        Me.col_Min_Qty.Name = "col_Min_Qty"
        Me.col_Min_Qty.ReadOnly = True
        '
        'col_Max_Qty
        '
        Me.col_Max_Qty.DataPropertyName = "Max_Qty"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "#,##0.######"
        Me.col_Max_Qty.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_Max_Qty.HeaderText = "Max"
        Me.col_Max_Qty.Name = "col_Max_Qty"
        Me.col_Max_Qty.ReadOnly = True
        '
        'frmSkuMinMaxByWH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 561)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gboData)
        Me.Controls.Add(Me.gboSummary)
        Me.MinimumSize = New System.Drawing.Size(500, 365)
        Me.Name = "frmSkuMinMaxByWH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดค่า Min-Max สินค้า"
        Me.gboSummary.ResumeLayout(False)
        Me.gboSummary.PerformLayout()
        Me.gboData.ResumeLayout(False)
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlData_Bottom.ResumeLayout(False)
        Me.pnlData_Bottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gboSummary As System.Windows.Forms.GroupBox
    Friend WithEvents gboData As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
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
    Friend WithEvents pnlData_Bottom As System.Windows.Forms.Panel
    Friend WithEvents lblData_Count As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblSku_Id As System.Windows.Forms.Label
    Friend WithEvents txtSku_Desc As System.Windows.Forms.TextBox
    Friend WithEvents lblSku_Desc As System.Windows.Forms.Label
    Friend WithEvents txtMaxSku_Qty As System.Windows.Forms.TextBox
    Friend WithEvents lblMaxSku As System.Windows.Forms.Label
    Friend WithEvents txtMinSku_Qty As System.Windows.Forms.TextBox
    Friend WithEvents lblMinSku As System.Windows.Forms.Label
    Friend WithEvents txtSku_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxSku_Package As System.Windows.Forms.TextBox
    Friend WithEvents txtMinSku_Package As System.Windows.Forms.TextBox
    Friend WithEvents col_Running_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Warehouse_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Min_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Max_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
