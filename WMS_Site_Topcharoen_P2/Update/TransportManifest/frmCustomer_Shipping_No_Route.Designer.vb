<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomer_Shipping_No_Route
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomer_Shipping_No_Route))
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.lbSearchfrom = New System.Windows.Forms.Label
        Me.cboSearchType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.grdCustomer_Shipping = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName_Thai = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColAddress = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColThaidefinition = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_cboRoute = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.col_cbosubRoute = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.col_cboTransportRegion = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ColIndex = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch.SuspendLayout()
        CType(Me.grdCustomer_Shipping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.lbSearchfrom)
        Me.grpSearch.Controls.Add(Me.cboSearchType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Location = New System.Drawing.Point(12, 12)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(501, 54)
        Me.grpSearch.TabIndex = 6
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "ค้นหา"
        '
        'lbSearchfrom
        '
        Me.lbSearchfrom.AutoSize = True
        Me.lbSearchfrom.Location = New System.Drawing.Point(6, 22)
        Me.lbSearchfrom.Name = "lbSearchfrom"
        Me.lbSearchfrom.Size = New System.Drawing.Size(43, 13)
        Me.lbSearchfrom.TabIndex = 0
        Me.lbSearchfrom.Text = "เงื่อนไข"
        '
        'cboSearchType
        '
        Me.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchType.FormattingEnabled = True
        Me.cboSearchType.Items.AddRange(New Object() {"รหัส", "ชื่อลูกค้าบริษัท (ไทย)"})
        Me.cboSearchType.Location = New System.Drawing.Point(55, 19)
        Me.cboSearchType.Name = "cboSearchType"
        Me.cboSearchType.Size = New System.Drawing.Size(113, 21)
        Me.cboSearchType.TabIndex = 1
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(174, 19)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(200, 20)
        Me.txtSearchKey.TabIndex = 2
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(380, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'grdCustomer_Shipping
        '
        Me.grdCustomer_Shipping.AllowUserToAddRows = False
        Me.grdCustomer_Shipping.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdCustomer_Shipping.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCustomer_Shipping.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCustomer_Shipping.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdCustomer_Shipping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCustomer_Shipping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCustomerID, Me.ColName_Thai, Me.ColAddress, Me.ColThaidefinition, Me.ColCity, Me.Col_cboRoute, Me.col_cbosubRoute, Me.col_cboTransportRegion, Me.ColIndex})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdCustomer_Shipping.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdCustomer_Shipping.Location = New System.Drawing.Point(12, 72)
        Me.grdCustomer_Shipping.Name = "grdCustomer_Shipping"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCustomer_Shipping.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdCustomer_Shipping.RowHeadersVisible = False
        Me.grdCustomer_Shipping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdCustomer_Shipping.Size = New System.Drawing.Size(1070, 424)
        Me.grdCustomer_Shipping.TabIndex = 7
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(975, 502)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 502)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn1.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn2.FillWeight = 134.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "ชื่อลูกค้าบริษัท (ไทย)"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Address"
        Me.DataGridViewTextBoxColumn3.FillWeight = 180.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "ที่อยู่"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "District"
        Me.DataGridViewTextBoxColumn4.HeaderText = "อำเภอ"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 75
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Province"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จังหวัด"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 75
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Customer_Shipping_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'ColCustomerID
        '
        Me.ColCustomerID.DataPropertyName = "Customer_Shipping_Location_Id"
        Me.ColCustomerID.FillWeight = 120.0!
        Me.ColCustomerID.HeaderText = "รหัส"
        Me.ColCustomerID.Name = "ColCustomerID"
        Me.ColCustomerID.Width = 80
        '
        'ColName_Thai
        '
        Me.ColName_Thai.DataPropertyName = "Shipping_Location_Name"
        Me.ColName_Thai.FillWeight = 134.0!
        Me.ColName_Thai.HeaderText = "ชื่อลูกค้าบริษัท (ไทย)"
        Me.ColName_Thai.Name = "ColName_Thai"
        Me.ColName_Thai.Width = 200
        '
        'ColAddress
        '
        Me.ColAddress.DataPropertyName = "Address"
        Me.ColAddress.FillWeight = 180.0!
        Me.ColAddress.HeaderText = "ที่อยู่"
        Me.ColAddress.Name = "ColAddress"
        Me.ColAddress.Width = 320
        '
        'ColThaidefinition
        '
        Me.ColThaidefinition.DataPropertyName = "District"
        Me.ColThaidefinition.HeaderText = "อำเภอ"
        Me.ColThaidefinition.Name = "ColThaidefinition"
        Me.ColThaidefinition.Width = 75
        '
        'ColCity
        '
        Me.ColCity.DataPropertyName = "Province"
        Me.ColCity.HeaderText = "จังหวัด"
        Me.ColCity.Name = "ColCity"
        Me.ColCity.Width = 75
        '
        'Col_cboRoute
        '
        Me.Col_cboRoute.DataPropertyName = "Route_index"
        Me.Col_cboRoute.HeaderText = "สายจัดส่ง"
        Me.Col_cboRoute.Name = "Col_cboRoute"
        Me.Col_cboRoute.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Col_cboRoute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'col_cbosubRoute
        '
        Me.col_cbosubRoute.DataPropertyName = "SubRoute_Index"
        Me.col_cbosubRoute.HeaderText = "สายจัดส่งย่อย"
        Me.col_cbosubRoute.Name = "col_cbosubRoute"
        Me.col_cbosubRoute.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_cbosubRoute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'col_cboTransportRegion
        '
        Me.col_cboTransportRegion.DataPropertyName = "TransportRegion_Index"
        Me.col_cboTransportRegion.HeaderText = "เขตพื้นที่จัดส่ง"
        Me.col_cboTransportRegion.Name = "col_cboTransportRegion"
        Me.col_cboTransportRegion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_cboTransportRegion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColIndex
        '
        Me.ColIndex.DataPropertyName = "Customer_Shipping_Location_Index"
        Me.ColIndex.HeaderText = "Index"
        Me.ColIndex.Name = "ColIndex"
        Me.ColIndex.ReadOnly = True
        Me.ColIndex.Visible = False
        '
        'frmCustomer_Shipping_No_Route
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1094, 552)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdCustomer_Shipping)
        Me.Controls.Add(Me.grpSearch)
        Me.Name = "frmCustomer_Shipping_No_Route"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รายการผู้รับสินค้าที่ยังไม่ได้กำหนดสายส่ง และเขตจัดส่ง"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdCustomer_Shipping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lbSearchfrom As System.Windows.Forms.Label
    Friend WithEvents cboSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents grdCustomer_Shipping As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents ColCustomerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName_Thai As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColThaidefinition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_cboRoute As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents col_cbosubRoute As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents col_cboTransportRegion As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColIndex As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
