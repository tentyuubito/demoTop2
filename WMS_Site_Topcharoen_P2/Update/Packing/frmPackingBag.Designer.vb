<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingBag
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblShipping = New System.Windows.Forms.Label
        Me.grdTray = New System.Windows.Forms.DataGridView
        Me.grdShipping = New System.Windows.Forms.DataGridView
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.lblBarcodeTray = New System.Windows.Forms.Label
        Me.grdCL = New System.Windows.Forms.DataGridView
        Me.lblBarcodeCL = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtBarcodeTray = New System.Windows.Forms.TextBox
        Me.txtBarcodeCL = New System.Windows.Forms.TextBox
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.txtRoute = New System.Windows.Forms.TextBox
        Me.lblRoute = New System.Windows.Forms.Label
        Me.cboShipping = New System.Windows.Forms.ComboBox
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
        Me.Col_Barcode_Tray = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_CL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Route = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Hub = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping_Location_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_SO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DropPoint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Urgent_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdTray, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdShipping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblShipping
        '
        Me.lblShipping.AutoSize = True
        Me.lblShipping.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblShipping.Location = New System.Drawing.Point(43, 34)
        Me.lblShipping.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShipping.Name = "lblShipping"
        Me.lblShipping.Size = New System.Drawing.Size(40, 17)
        Me.lblShipping.TabIndex = 0
        Me.lblShipping.Text = "HUB"
        Me.lblShipping.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdTray
        '
        Me.grdTray.AllowUserToAddRows = False
        Me.grdTray.AllowUserToDeleteRows = False
        Me.grdTray.AllowUserToResizeRows = False
        Me.grdTray.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTray.BackgroundColor = System.Drawing.Color.White
        Me.grdTray.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode_Tray, Me.Col_Qty_Barcode_CL, Me.Col_Qty_CL})
        Me.grdTray.Location = New System.Drawing.Point(649, 63)
        Me.grdTray.Margin = New System.Windows.Forms.Padding(4)
        Me.grdTray.MultiSelect = False
        Me.grdTray.Name = "grdTray"
        Me.grdTray.RowHeadersVisible = False
        Me.grdTray.RowTemplate.Height = 24
        Me.grdTray.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTray.Size = New System.Drawing.Size(599, 133)
        Me.grdTray.TabIndex = 6
        '
        'grdShipping
        '
        Me.grdShipping.AllowUserToAddRows = False
        Me.grdShipping.AllowUserToDeleteRows = False
        Me.grdShipping.AllowUserToResizeRows = False
        Me.grdShipping.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdShipping.BackgroundColor = System.Drawing.Color.White
        Me.grdShipping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Route, Me.col_Hub, Me.Col_Shipping_Location_Name, Me.Col_Qty_SO})
        Me.grdShipping.Location = New System.Drawing.Point(13, 63)
        Me.grdShipping.Margin = New System.Windows.Forms.Padding(4)
        Me.grdShipping.MultiSelect = False
        Me.grdShipping.Name = "grdShipping"
        Me.grdShipping.RowHeadersVisible = False
        Me.grdShipping.RowTemplate.Height = 24
        Me.grdShipping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdShipping.Size = New System.Drawing.Size(628, 598)
        Me.grdShipping.TabIndex = 3
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(649, 617)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 44)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "ปิดถุง"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ยกเลิกรายการ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(1109, 617)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 44)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblBarcodeTray
        '
        Me.lblBarcodeTray.AutoSize = True
        Me.lblBarcodeTray.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcodeTray.Location = New System.Drawing.Point(667, 34)
        Me.lblBarcodeTray.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBarcodeTray.Name = "lblBarcodeTray"
        Me.lblBarcodeTray.Size = New System.Drawing.Size(71, 17)
        Me.lblBarcodeTray.TabIndex = 4
        Me.lblBarcodeTray.Text = "Tray No."
        Me.lblBarcodeTray.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdCL
        '
        Me.grdCL.AllowUserToAddRows = False
        Me.grdCL.AllowUserToDeleteRows = False
        Me.grdCL.AllowUserToResizeRows = False
        Me.grdCL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCL.BackgroundColor = System.Drawing.Color.White
        Me.grdCL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode_CL, Me.Col_Total_Qty, Me.Col_SalesOrder_No, Me.Col_DropPoint, Me.Col_Urgent_Id, Me.Col_Sku_Name})
        Me.grdCL.Location = New System.Drawing.Point(649, 239)
        Me.grdCL.Margin = New System.Windows.Forms.Padding(4)
        Me.grdCL.MultiSelect = False
        Me.grdCL.Name = "grdCL"
        Me.grdCL.RowHeadersVisible = False
        Me.grdCL.RowTemplate.Height = 24
        Me.grdCL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCL.Size = New System.Drawing.Size(599, 370)
        Me.grdCL.TabIndex = 9
        '
        'lblBarcodeCL
        '
        Me.lblBarcodeCL.AutoSize = True
        Me.lblBarcodeCL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcodeCL.Location = New System.Drawing.Point(645, 213)
        Me.lblBarcodeCL.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBarcodeCL.Name = "lblBarcodeCL"
        Me.lblBarcodeCL.Size = New System.Drawing.Size(92, 17)
        Me.lblBarcodeCL.TabIndex = 7
        Me.lblBarcodeCL.Text = "Barcode CL"
        Me.lblBarcodeCL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(797, 617)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(140, 44)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "ล้างข้อมูล Scan"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtBarcodeTray
        '
        Me.txtBarcodeTray.Location = New System.Drawing.Point(745, 32)
        Me.txtBarcodeTray.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBarcodeTray.Name = "txtBarcodeTray"
        Me.txtBarcodeTray.Size = New System.Drawing.Size(280, 22)
        Me.txtBarcodeTray.TabIndex = 5
        '
        'txtBarcodeCL
        '
        Me.txtBarcodeCL.Location = New System.Drawing.Point(745, 210)
        Me.txtBarcodeCL.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBarcodeCL.Name = "txtBarcodeCL"
        Me.txtBarcodeCL.Size = New System.Drawing.Size(280, 22)
        Me.txtBarcodeCL.TabIndex = 8
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ
        Me.btnRefresh.Location = New System.Drawing.Point(387, 7)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(81, 49)
        Me.btnRefresh.TabIndex = 18
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtRoute
        '
        Me.txtRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtRoute.Location = New System.Drawing.Point(89, 7)
        Me.txtRoute.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(247, 22)
        Me.txtRoute.TabIndex = 17
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = True
        Me.lblRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(35, 7)
        Me.lblRoute.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(48, 17)
        Me.lblRoute.TabIndex = 16
        Me.lblRoute.Text = "สายรถ"
        Me.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboShipping
        '
        Me.cboShipping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShipping.FormattingEnabled = True
        Me.cboShipping.Location = New System.Drawing.Point(89, 32)
        Me.cboShipping.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboShipping.Name = "cboShipping"
        Me.cboShipping.Size = New System.Drawing.Size(247, 24)
        Me.cboShipping.TabIndex = 19
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "สาขาย่อย"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 190
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Qty_SO"
        Me.DataGridViewTextBoxColumn2.HeaderText = "จำนวน SO"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Barcode_Tray"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Tray No."
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 400
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Qty_Barcode_CL"
        Me.DataGridViewTextBoxColumn4.HeaderText = "จำนวน Barcode"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Qty_CL"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวน CL"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Barcode_CL"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Barcode"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 120
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "SalesOrder_No"
        Me.DataGridViewTextBoxColumn7.HeaderText = "SO No."
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn8.HeaderText = "รายละเอียดสินค้า"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Width = 250
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
        Me.DataGridViewTextBoxColumn9.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "SalesOrder_No"
        Me.DataGridViewTextBoxColumn10.HeaderText = "SO No."
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn11.HeaderText = "รายละเอียดสินค้า"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Width = 250
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn12.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn13.HeaderText = "รายละเอียดสินค้า"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn13.Width = 250
        '
        'Col_Barcode_Tray
        '
        Me.Col_Barcode_Tray.DataPropertyName = "Barcode_Tray"
        Me.Col_Barcode_Tray.HeaderText = "Tray No."
        Me.Col_Barcode_Tray.Name = "Col_Barcode_Tray"
        Me.Col_Barcode_Tray.ReadOnly = True
        Me.Col_Barcode_Tray.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Barcode_Tray.Width = 370
        '
        'Col_Qty_Barcode_CL
        '
        Me.Col_Qty_Barcode_CL.DataPropertyName = "Qty_Barcode_CL"
        Me.Col_Qty_Barcode_CL.HeaderText = "จำนวน Barcode"
        Me.Col_Qty_Barcode_CL.Name = "Col_Qty_Barcode_CL"
        Me.Col_Qty_Barcode_CL.ReadOnly = True
        Me.Col_Qty_Barcode_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Qty_Barcode_CL.Width = 150
        '
        'Col_Qty_CL
        '
        Me.Col_Qty_CL.DataPropertyName = "Qty_CL"
        Me.Col_Qty_CL.HeaderText = "จำนวน CL"
        Me.Col_Qty_CL.Name = "Col_Qty_CL"
        Me.Col_Qty_CL.ReadOnly = True
        Me.Col_Qty_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_Route
        '
        Me.Col_Route.DataPropertyName = "Route_Id"
        Me.Col_Route.HeaderText = "สายรถ"
        Me.Col_Route.Name = "Col_Route"
        Me.Col_Route.ReadOnly = True
        Me.Col_Route.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Route.Width = 60
        '
        'col_Hub
        '
        Me.col_Hub.DataPropertyName = "Customer_Shipping"
        Me.col_Hub.HeaderText = "HUB"
        Me.col_Hub.Name = "col_Hub"
        Me.col_Hub.ReadOnly = True
        Me.col_Hub.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Hub.Width = 150
        '
        'Col_Shipping_Location_Name
        '
        Me.Col_Shipping_Location_Name.DataPropertyName = "Shipping_Location_Name"
        Me.Col_Shipping_Location_Name.HeaderText = "สาขาย่อย"
        Me.Col_Shipping_Location_Name.Name = "Col_Shipping_Location_Name"
        Me.Col_Shipping_Location_Name.ReadOnly = True
        Me.Col_Shipping_Location_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Shipping_Location_Name.Width = 150
        '
        'Col_Qty_SO
        '
        Me.Col_Qty_SO.DataPropertyName = "Qty_SO"
        Me.Col_Qty_SO.HeaderText = "จำนวน SO"
        Me.Col_Qty_SO.Name = "Col_Qty_SO"
        Me.Col_Qty_SO.ReadOnly = True
        Me.Col_Qty_SO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Qty_SO.Width = 80
        '
        'Col_Barcode_CL
        '
        Me.Col_Barcode_CL.DataPropertyName = "Barcode_CL"
        Me.Col_Barcode_CL.HeaderText = "Barcode"
        Me.Col_Barcode_CL.Name = "Col_Barcode_CL"
        Me.Col_Barcode_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Barcode_CL.Width = 120
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
        Me.Col_Total_Qty.Width = 60
        '
        'Col_SalesOrder_No
        '
        Me.Col_SalesOrder_No.DataPropertyName = "SalesOrder_No"
        Me.Col_SalesOrder_No.HeaderText = "SO No."
        Me.Col_SalesOrder_No.Name = "Col_SalesOrder_No"
        Me.Col_SalesOrder_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_SalesOrder_No.Width = 150
        '
        'Col_DropPoint
        '
        Me.Col_DropPoint.DataPropertyName = "OMS_DropPoint_Desc"
        Me.Col_DropPoint.HeaderText = "Drop Point"
        Me.Col_DropPoint.Name = "Col_DropPoint"
        Me.Col_DropPoint.ReadOnly = True
        Me.Col_DropPoint.Width = 180
        '
        'Col_Urgent_Id
        '
        Me.Col_Urgent_Id.DataPropertyName = "Urgent_Id"
        Me.Col_Urgent_Id.HeaderText = "รหัสความด่วน"
        Me.Col_Urgent_Id.Name = "Col_Urgent_Id"
        Me.Col_Urgent_Id.ReadOnly = True
        '
        'Col_Sku_Name
        '
        Me.Col_Sku_Name.DataPropertyName = "Sku_Name"
        Me.Col_Sku_Name.HeaderText = "รายละเอียดสินค้า"
        Me.Col_Sku_Name.Name = "Col_Sku_Name"
        Me.Col_Sku_Name.ReadOnly = True
        Me.Col_Sku_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Sku_Name.Width = 250
        '
        'frmPackingBag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 673)
        Me.Controls.Add(Me.cboShipping)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtRoute)
        Me.Controls.Add(Me.lblRoute)
        Me.Controls.Add(Me.txtBarcodeCL)
        Me.Controls.Add(Me.txtBarcodeTray)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblBarcodeCL)
        Me.Controls.Add(Me.grdCL)
        Me.Controls.Add(Me.lblBarcodeTray)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdTray)
        Me.Controls.Add(Me.lblShipping)
        Me.Controls.Add(Me.grdShipping)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPackingBag"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงถุง"
        CType(Me.grdTray, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdShipping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblShipping As System.Windows.Forms.Label
    Friend WithEvents grdTray As System.Windows.Forms.DataGridView
    Friend WithEvents grdShipping As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Col_Exp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CYL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ASN_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblBarcodeTray As System.Windows.Forms.Label
    Friend WithEvents grdCL As System.Windows.Forms.DataGridView
    Friend WithEvents lblBarcodeCL As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtBarcodeTray As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcodeCL As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents lblRoute As System.Windows.Forms.Label
    Friend WithEvents cboShipping As System.Windows.Forms.ComboBox
    Friend WithEvents Col_Barcode_Tray As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Route As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Hub As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping_Location_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_SO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DropPoint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Urgent_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
