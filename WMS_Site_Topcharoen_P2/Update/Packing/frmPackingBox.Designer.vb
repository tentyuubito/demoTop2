<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingBox
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
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grdData = New System.Windows.Forms.DataGridView
        Me.Col_Barcode_Bag = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_ProductType_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SO_Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Urgent_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Route = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.droppoint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping_Location = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblBarcodeBag = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtBarcodeBag = New System.Windows.Forms.TextBox
        Me.grpShipping = New System.Windows.Forms.GroupBox
        Me.txtBranch = New System.Windows.Forms.TextBox
        Me.cboCustomerShipping = New System.Windows.Forms.ComboBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtRoute = New System.Windows.Forms.TextBox
        Me.lblReoute = New System.Windows.Forms.Label
        Me.lblShipping = New System.Windows.Forms.Label
        Me.lblSumScan = New System.Windows.Forms.Label
        Me.lblSum = New System.Windows.Forms.Label
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnExportExcel = New System.Windows.Forms.Button
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpShipping.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(16, 548)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 44)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "ปิดกล่อง"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ยกเลิกรายการ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(1089, 548)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 44)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        Me.grdData.AllowUserToDeleteRows = False
        Me.grdData.AllowUserToResizeRows = False
        Me.grdData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdData.BackgroundColor = System.Drawing.Color.White
        Me.grdData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode_Bag, Me.Col_ProductType_Desc, Me.col_SO_Type, Me.Col_Urgent_Id, Me.Col_Route, Me.droppoint, Me.Col_Shipping, Me.Col_Shipping_Location, Me.Col_Shipping_Desc, Me.Col_Total_Qty})
        Me.grdData.Location = New System.Drawing.Point(53, 166)
        Me.grdData.Margin = New System.Windows.Forms.Padding(4)
        Me.grdData.MultiSelect = False
        Me.grdData.Name = "grdData"
        Me.grdData.RowHeadersVisible = False
        Me.grdData.RowTemplate.Height = 24
        Me.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdData.Size = New System.Drawing.Size(1170, 359)
        Me.grdData.TabIndex = 3
        '
        'Col_Barcode_Bag
        '
        Me.Col_Barcode_Bag.DataPropertyName = "Barcode_Bag"
        Me.Col_Barcode_Bag.HeaderText = "Barcode Bag"
        Me.Col_Barcode_Bag.Name = "Col_Barcode_Bag"
        Me.Col_Barcode_Bag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Barcode_Bag.Width = 150
        '
        'Col_ProductType_Desc
        '
        Me.Col_ProductType_Desc.DataPropertyName = "ProductType_Desc"
        Me.Col_ProductType_Desc.HeaderText = "ประเภทสินค้า"
        Me.Col_ProductType_Desc.Name = "Col_ProductType_Desc"
        Me.Col_ProductType_Desc.ReadOnly = True
        Me.Col_ProductType_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_ProductType_Desc.Width = 150
        '
        'col_SO_Type
        '
        Me.col_SO_Type.DataPropertyName = "SO_Type"
        Me.col_SO_Type.HeaderText = "ประเภท"
        Me.col_SO_Type.Name = "col_SO_Type"
        '
        'Col_Urgent_Id
        '
        Me.Col_Urgent_Id.DataPropertyName = "Urgent_Id"
        Me.Col_Urgent_Id.HeaderText = "ด่วน"
        Me.Col_Urgent_Id.Name = "Col_Urgent_Id"
        Me.Col_Urgent_Id.ReadOnly = True
        '
        'Col_Route
        '
        Me.Col_Route.DataPropertyName = "Route"
        Me.Col_Route.HeaderText = "สายรถ"
        Me.Col_Route.Name = "Col_Route"
        Me.Col_Route.ReadOnly = True
        Me.Col_Route.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Route.Width = 120
        '
        'droppoint
        '
        Me.droppoint.DataPropertyName = "droppoint"
        Me.droppoint.HeaderText = "droppoint"
        Me.droppoint.Name = "droppoint"
        Me.droppoint.ReadOnly = True
        Me.droppoint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_Shipping
        '
        Me.Col_Shipping.DataPropertyName = "Shipping"
        Me.Col_Shipping.HeaderText = "สาขาแม่"
        Me.Col_Shipping.Name = "Col_Shipping"
        Me.Col_Shipping.ReadOnly = True
        Me.Col_Shipping.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Shipping.Width = 120
        '
        'Col_Shipping_Location
        '
        Me.Col_Shipping_Location.DataPropertyName = "Shipping_Location"
        Me.Col_Shipping_Location.HeaderText = "สาขาย่อย"
        Me.Col_Shipping_Location.Name = "Col_Shipping_Location"
        Me.Col_Shipping_Location.ReadOnly = True
        Me.Col_Shipping_Location.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Shipping_Location.Width = 120
        '
        'Col_Shipping_Desc
        '
        Me.Col_Shipping_Desc.DataPropertyName = "Shipping_Desc"
        Me.Col_Shipping_Desc.HeaderText = "รายละเอียดสาขา"
        Me.Col_Shipping_Desc.Name = "Col_Shipping_Desc"
        Me.Col_Shipping_Desc.ReadOnly = True
        Me.Col_Shipping_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Shipping_Desc.Width = 350
        '
        'Col_Total_Qty
        '
        Me.Col_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Col_Total_Qty.DefaultCellStyle = DataGridViewCellStyle1
        Me.Col_Total_Qty.HeaderText = "จำนวน"
        Me.Col_Total_Qty.Name = "Col_Total_Qty"
        Me.Col_Total_Qty.ReadOnly = True
        Me.Col_Total_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Total_Qty.Width = 150
        '
        'lblBarcodeBag
        '
        Me.lblBarcodeBag.AutoSize = True
        Me.lblBarcodeBag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcodeBag.Location = New System.Drawing.Point(19, 94)
        Me.lblBarcodeBag.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBarcodeBag.Name = "lblBarcodeBag"
        Me.lblBarcodeBag.Size = New System.Drawing.Size(101, 17)
        Me.lblBarcodeBag.TabIndex = 1
        Me.lblBarcodeBag.Text = "Barcode Bag"
        Me.lblBarcodeBag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(164, 548)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(140, 44)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "ล้างข้อมูล Scan"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtBarcodeBag
        '
        Me.txtBarcodeBag.BackColor = System.Drawing.SystemColors.Info
        Me.txtBarcodeBag.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtBarcodeBag.Location = New System.Drawing.Point(127, 91)
        Me.txtBarcodeBag.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBarcodeBag.Name = "txtBarcodeBag"
        Me.txtBarcodeBag.Size = New System.Drawing.Size(575, 41)
        Me.txtBarcodeBag.TabIndex = 2
        '
        'grpShipping
        '
        Me.grpShipping.Controls.Add(Me.txtBranch)
        Me.grpShipping.Controls.Add(Me.cboCustomerShipping)
        Me.grpShipping.Controls.Add(Me.btnSearch)
        Me.grpShipping.Controls.Add(Me.txtRoute)
        Me.grpShipping.Controls.Add(Me.lblReoute)
        Me.grpShipping.Controls.Add(Me.lblShipping)
        Me.grpShipping.Location = New System.Drawing.Point(16, -1)
        Me.grpShipping.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grpShipping.Name = "grpShipping"
        Me.grpShipping.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grpShipping.Size = New System.Drawing.Size(1213, 89)
        Me.grpShipping.TabIndex = 0
        Me.grpShipping.TabStop = False
        '
        'txtBranch
        '
        Me.txtBranch.Location = New System.Drawing.Point(692, 46)
        Me.txtBranch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(207, 22)
        Me.txtBranch.TabIndex = 10
        '
        'cboCustomerShipping
        '
        Me.cboCustomerShipping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomerShipping.FormattingEnabled = True
        Me.cboCustomerShipping.Location = New System.Drawing.Point(111, 47)
        Me.cboCustomerShipping.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboCustomerShipping.Name = "cboCustomerShipping"
        Me.cboCustomerShipping.Size = New System.Drawing.Size(575, 24)
        Me.cboCustomerShipping.TabIndex = 9
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(1067, 14)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(140, 44)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtRoute
        '
        Me.txtRoute.Location = New System.Drawing.Point(111, 20)
        Me.txtRoute.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(257, 22)
        Me.txtRoute.TabIndex = 1
        '
        'lblReoute
        '
        Me.lblReoute.AutoSize = True
        Me.lblReoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReoute.Location = New System.Drawing.Point(56, 21)
        Me.lblReoute.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReoute.Name = "lblReoute"
        Me.lblReoute.Size = New System.Drawing.Size(48, 17)
        Me.lblReoute.TabIndex = 0
        Me.lblReoute.Text = "สายรถ"
        Me.lblReoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShipping
        '
        Me.lblShipping.AutoSize = True
        Me.lblShipping.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblShipping.Location = New System.Drawing.Point(65, 49)
        Me.lblShipping.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShipping.Name = "lblShipping"
        Me.lblShipping.Size = New System.Drawing.Size(39, 17)
        Me.lblShipping.TabIndex = 2
        Me.lblShipping.Text = "สาขา"
        Me.lblShipping.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSumScan
        '
        Me.lblSumScan.AutoSize = True
        Me.lblSumScan.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSumScan.ForeColor = System.Drawing.Color.Blue
        Me.lblSumScan.Location = New System.Drawing.Point(725, 94)
        Me.lblSumScan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumScan.Name = "lblSumScan"
        Me.lblSumScan.Size = New System.Drawing.Size(243, 36)
        Me.lblSumScan.TabIndex = 7
        Me.lblSumScan.Text = "ไม่มีรายการ Scan"
        Me.lblSumScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSum
        '
        Me.lblSum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSum.AutoSize = True
        Me.lblSum.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSum.ForeColor = System.Drawing.Color.Blue
        Me.lblSum.Location = New System.Drawing.Point(17, 508)
        Me.lblSum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(175, 29)
        Me.lblSum.TabIndex = 8
        Me.lblSum.Text = "ไม่พบรายการถุง"
        Me.lblSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Barcode_Bag"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Barcode Bag"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ProductType_Desc"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ประเภทสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "SO_Type"
        Me.DataGridViewTextBoxColumn3.HeaderText = "ประเภท"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Urgent_Id"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ด่วน"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Route"
        Me.DataGridViewTextBoxColumn5.HeaderText = "สายรถ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Shipping"
        Me.DataGridViewTextBoxColumn6.HeaderText = "สาขาแม่"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Shipping_Location"
        Me.DataGridViewTextBoxColumn7.HeaderText = "สาขาย่อย"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 120
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Shipping_Desc"
        Me.DataGridViewTextBoxColumn8.HeaderText = "รายละเอียดสาขา"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Width = 350
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn9.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportExcel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ดึงข้อมูล
        Me.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportExcel.Location = New System.Drawing.Point(324, 548)
        Me.btnExportExcel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(140, 44)
        Me.btnExportExcel.TabIndex = 9
        Me.btnExportExcel.Text = "Export Excel"
        Me.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'frmPackingBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 606)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.lblSum)
        Me.Controls.Add(Me.lblSumScan)
        Me.Controls.Add(Me.grpShipping)
        Me.Controls.Add(Me.txtBarcodeBag)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblBarcodeBag)
        Me.Controls.Add(Me.grdData)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPackingBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงกล่อง"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpShipping.ResumeLayout(False)
        Me.grpShipping.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Col_Exp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CYL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ASN_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdData As System.Windows.Forms.DataGridView
    Friend WithEvents lblBarcodeBag As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtBarcodeBag As System.Windows.Forms.TextBox
    Friend WithEvents grpShipping As System.Windows.Forms.GroupBox
    Friend WithEvents lblShipping As System.Windows.Forms.Label
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents lblReoute As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblSumScan As System.Windows.Forms.Label
    Friend WithEvents cboCustomerShipping As System.Windows.Forms.ComboBox
    Friend WithEvents lblSum As System.Windows.Forms.Label
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Barcode_Bag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ProductType_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SO_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Urgent_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Route As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents droppoint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping_Location As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
End Class
