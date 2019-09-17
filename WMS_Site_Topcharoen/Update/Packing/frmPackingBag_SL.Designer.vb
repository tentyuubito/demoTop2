<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingBag_SL
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblRoute = New System.Windows.Forms.Label
        Me.grdShipping = New System.Windows.Forms.DataGridView
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grdSL = New System.Windows.Forms.DataGridView
        Me.lblBarcodeSL = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtBarcodeSL = New System.Windows.Forms.TextBox
        Me.txtRoute = New System.Windows.Forms.TextBox
        Me.btnRefresh = New System.Windows.Forms.Button
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
        Me.Col_Route = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Shipping_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_SL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Barcode_SL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Barcode_Tray = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SalesOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Urgent_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Droppoint_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdShipping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = True
        Me.lblRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(10, 9)
        Me.lblRoute.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(48, 17)
        Me.lblRoute.TabIndex = 0
        Me.lblRoute.Text = "สายรถ"
        Me.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdShipping
        '
        Me.grdShipping.AllowUserToAddRows = False
        Me.grdShipping.AllowUserToDeleteRows = False
        Me.grdShipping.AllowUserToResizeRows = False
        Me.grdShipping.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdShipping.BackgroundColor = System.Drawing.Color.White
        Me.grdShipping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Route, Me.Col_Shipping_Name, Me.Col_Qty_SL})
        Me.grdShipping.Location = New System.Drawing.Point(13, 58)
        Me.grdShipping.Margin = New System.Windows.Forms.Padding(4)
        Me.grdShipping.MultiSelect = False
        Me.grdShipping.Name = "grdShipping"
        Me.grdShipping.RowHeadersVisible = False
        Me.grdShipping.RowTemplate.Height = 24
        Me.grdShipping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdShipping.Size = New System.Drawing.Size(424, 530)
        Me.grdShipping.TabIndex = 3
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(445, 544)
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
        Me.btnClose.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ยกเลิกรายการ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(1089, 544)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 44)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grdSL
        '
        Me.grdSL.AllowUserToAddRows = False
        Me.grdSL.AllowUserToDeleteRows = False
        Me.grdSL.AllowUserToResizeRows = False
        Me.grdSL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSL.BackgroundColor = System.Drawing.Color.White
        Me.grdSL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode_SL, Me.Col_Barcode_Tray, Me.Col_SalesOrder_No, Me.Col_Sku_Name, Me.Col_Total_Qty, Me.Urgent_Id, Me.Droppoint_Desc})
        Me.grdSL.Location = New System.Drawing.Point(445, 58)
        Me.grdSL.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSL.MultiSelect = False
        Me.grdSL.Name = "grdSL"
        Me.grdSL.RowHeadersVisible = False
        Me.grdSL.RowTemplate.Height = 24
        Me.grdSL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSL.Size = New System.Drawing.Size(784, 482)
        Me.grdSL.TabIndex = 9
        '
        'lblBarcodeSL
        '
        Me.lblBarcodeSL.AutoSize = True
        Me.lblBarcodeSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcodeSL.Location = New System.Drawing.Point(631, 28)
        Me.lblBarcodeSL.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBarcodeSL.Name = "lblBarcodeSL"
        Me.lblBarcodeSL.Size = New System.Drawing.Size(92, 17)
        Me.lblBarcodeSL.TabIndex = 7
        Me.lblBarcodeSL.Text = "Barcode SL"
        Me.lblBarcodeSL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(692, 544)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(140, 44)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "ล้างข้อมูล Scan"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtBarcodeSL
        '
        Me.txtBarcodeSL.Location = New System.Drawing.Point(730, 26)
        Me.txtBarcodeSL.Name = "txtBarcodeSL"
        Me.txtBarcodeSL.Size = New System.Drawing.Size(280, 22)
        Me.txtBarcodeSL.TabIndex = 8
        '
        'txtRoute
        '
        Me.txtRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtRoute.Location = New System.Drawing.Point(73, 4)
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(246, 38)
        Me.txtRoute.TabIndex = 13
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.btnRefresh.Location = New System.Drawing.Point(326, 2)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(65, 41)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Route_ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "สายรถ"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Shipping_Name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Hub"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 190
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Qty_SL"
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn3.HeaderText = "จำนวน SL"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Barcode_SL"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Barcode"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "SalesOrder_No"
        Me.DataGridViewTextBoxColumn5.HeaderText = "SO No."
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn6.HeaderText = "รายละเอียดสินค้า"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 250
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Width = 250
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn8.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Urgent_Id"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Urgent_Id"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Droppoint_Desc"
        Me.DataGridViewTextBoxColumn10.HeaderText = "DropPoint"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'Col_Route
        '
        Me.Col_Route.DataPropertyName = "Route_ID"
        Me.Col_Route.HeaderText = "สายรถ"
        Me.Col_Route.Name = "Col_Route"
        Me.Col_Route.ReadOnly = True
        Me.Col_Route.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Route.Width = 50
        '
        'Col_Shipping_Name
        '
        Me.Col_Shipping_Name.DataPropertyName = "Shipping_Name"
        Me.Col_Shipping_Name.HeaderText = "Hub"
        Me.Col_Shipping_Name.Name = "Col_Shipping_Name"
        Me.Col_Shipping_Name.ReadOnly = True
        Me.Col_Shipping_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Shipping_Name.Width = 180
        '
        'Col_Qty_SL
        '
        Me.Col_Qty_SL.DataPropertyName = "Qty_SL"
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Col_Qty_SL.DefaultCellStyle = DataGridViewCellStyle1
        Me.Col_Qty_SL.HeaderText = "จำนวน SL"
        Me.Col_Qty_SL.Name = "Col_Qty_SL"
        Me.Col_Qty_SL.ReadOnly = True
        Me.Col_Qty_SL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Qty_SL.Width = 80
        '
        'Col_Barcode_SL
        '
        Me.Col_Barcode_SL.DataPropertyName = "Barcode_SL"
        Me.Col_Barcode_SL.HeaderText = "Barcode"
        Me.Col_Barcode_SL.Name = "Col_Barcode_SL"
        Me.Col_Barcode_SL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Barcode_SL.Width = 120
        '
        'Col_Barcode_Tray
        '
        Me.Col_Barcode_Tray.DataPropertyName = "Barcode_Tray"
        Me.Col_Barcode_Tray.HeaderText = "Tray"
        Me.Col_Barcode_Tray.Name = "Col_Barcode_Tray"
        Me.Col_Barcode_Tray.ReadOnly = True
        Me.Col_Barcode_Tray.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_SalesOrder_No
        '
        Me.Col_SalesOrder_No.DataPropertyName = "SalesOrder_No"
        Me.Col_SalesOrder_No.HeaderText = "SO No."
        Me.Col_SalesOrder_No.Name = "Col_SalesOrder_No"
        Me.Col_SalesOrder_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_SalesOrder_No.Width = 150
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
        'Col_Total_Qty
        '
        Me.Col_Total_Qty.DataPropertyName = "Total_Qty"
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Col_Total_Qty.DefaultCellStyle = DataGridViewCellStyle2
        Me.Col_Total_Qty.HeaderText = "จำนวน"
        Me.Col_Total_Qty.Name = "Col_Total_Qty"
        Me.Col_Total_Qty.ReadOnly = True
        Me.Col_Total_Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Urgent_Id
        '
        Me.Urgent_Id.DataPropertyName = "Urgent_Id"
        Me.Urgent_Id.HeaderText = "Urgent_Id"
        Me.Urgent_Id.Name = "Urgent_Id"
        '
        'Droppoint_Desc
        '
        Me.Droppoint_Desc.DataPropertyName = "Droppoint_Desc"
        Me.Droppoint_Desc.HeaderText = "DropPoint"
        Me.Droppoint_Desc.Name = "Droppoint_Desc"
        '
        'frmPackingBag_SL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1242, 605)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtRoute)
        Me.Controls.Add(Me.txtBarcodeSL)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblBarcodeSL)
        Me.Controls.Add(Me.grdSL)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblRoute)
        Me.Controls.Add(Me.grdShipping)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPackingBag_SL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงถุง"
        CType(Me.grdShipping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRoute As System.Windows.Forms.Label
    Friend WithEvents grdShipping As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Col_Exp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CYL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ASN_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdSL As System.Windows.Forms.DataGridView
    Friend WithEvents lblBarcodeSL As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtBarcodeSL As System.Windows.Forms.TextBox
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Route As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Shipping_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_SL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Barcode_SL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Barcode_Tray As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SalesOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Urgent_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Droppoint_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
