<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomer_ShippingV2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomer_ShippingV2))
        Me.btnSave = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.chkINT_U = New System.Windows.Forms.CheckBox
        Me.lbSearchfrom = New System.Windows.Forms.Label
        Me.cboSearchType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.grdCustomer_Shipping = New System.Windows.Forms.DataGridView
        Me.ColCustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTitle = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName_Thai = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName_Eng = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportRegion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Route = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SubRoute = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColAddress = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColThaidefinition = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCountry = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMobileTel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColIndex = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_UserSalesTool = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnCusRefId = New System.Windows.Forms.Button
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.grpSearch.SuspendLayout()
        CType(Me.grdCustomer_Shipping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 396)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "เพิ่ม"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdate.Location = New System.Drawing.Point(114, 396)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.BtnUpdate.TabIndex = 3
        Me.BtnUpdate.Text = "แก้ไข"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(216, 396)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(877, 396)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 38)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.chkINT_U)
        Me.grpSearch.Controls.Add(Me.lbSearchfrom)
        Me.grpSearch.Controls.Add(Me.cboSearchType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Location = New System.Drawing.Point(433, 387)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(438, 75)
        Me.grpSearch.TabIndex = 5
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "ค้นหา"
        '
        'chkINT_U
        '
        Me.chkINT_U.AutoSize = True
        Me.chkINT_U.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkINT_U.Location = New System.Drawing.Point(55, 46)
        Me.chkINT_U.Name = "chkINT_U"
        Me.chkINT_U.Size = New System.Drawing.Size(106, 17)
        Me.chkINT_U.TabIndex = 34
        Me.chkINT_U.Text = "New Interface"
        Me.chkINT_U.UseVisualStyleBackColor = True
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
        Me.cboSearchType.Items.AddRange(New Object() {"ชื่อลูกค้าบริษัท (ไทย)", "ชื่อลูกค้าบริษัท (อังกฤษ)", "รหัสลูกค้าบริษัท", "โทรศัพท์", "โทรศัพท์มือถือ"})
        Me.cboSearchType.Location = New System.Drawing.Point(55, 19)
        Me.cboSearchType.Name = "cboSearchType"
        Me.cboSearchType.Size = New System.Drawing.Size(113, 21)
        Me.cboSearchType.TabIndex = 1
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(174, 20)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(150, 20)
        Me.txtSearchKey.TabIndex = 2
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(332, 10)
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
        Me.grdCustomer_Shipping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCustomer_Shipping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCustomerID, Me.colTitle, Me.ColName_Thai, Me.ColName_Eng, Me.col_TransportRegion, Me.Col_Route, Me.col_SubRoute, Me.ColAddress, Me.ColThaidefinition, Me.ColCity, Me.ColCountry, Me.ColTel, Me.ColMobileTel, Me.ColIndex, Me.Col_UserSalesTool})
        Me.grdCustomer_Shipping.Location = New System.Drawing.Point(12, 7)
        Me.grdCustomer_Shipping.Name = "grdCustomer_Shipping"
        Me.grdCustomer_Shipping.ReadOnly = True
        Me.grdCustomer_Shipping.RowHeadersVisible = False
        Me.grdCustomer_Shipping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCustomer_Shipping.Size = New System.Drawing.Size(965, 374)
        Me.grdCustomer_Shipping.TabIndex = 0
        '
        'ColCustomerID
        '
        Me.ColCustomerID.FillWeight = 120.0!
        Me.ColCustomerID.HeaderText = "รหัสลูกค้าบริษัท"
        Me.ColCustomerID.Name = "ColCustomerID"
        Me.ColCustomerID.ReadOnly = True
        Me.ColCustomerID.Width = 120
        '
        'colTitle
        '
        Me.colTitle.HeaderText = "คำนำหน้าชื่อ"
        Me.colTitle.Name = "colTitle"
        Me.colTitle.ReadOnly = True
        '
        'ColName_Thai
        '
        Me.ColName_Thai.FillWeight = 134.0!
        Me.ColName_Thai.HeaderText = "ชื่อลูกค้าบริษัท (ไทย)"
        Me.ColName_Thai.Name = "ColName_Thai"
        Me.ColName_Thai.ReadOnly = True
        Me.ColName_Thai.Width = 200
        '
        'ColName_Eng
        '
        Me.ColName_Eng.HeaderText = "ชื่อลูกค้าบริษัท (อังกฤษ)"
        Me.ColName_Eng.Name = "ColName_Eng"
        Me.ColName_Eng.ReadOnly = True
        Me.ColName_Eng.Width = 200
        '
        'col_TransportRegion
        '
        Me.col_TransportRegion.HeaderText = "เขตพื้นที่จัดส่ง"
        Me.col_TransportRegion.Name = "col_TransportRegion"
        Me.col_TransportRegion.ReadOnly = True
        '
        'Col_Route
        '
        Me.Col_Route.HeaderText = "เส้นทางหลัก"
        Me.Col_Route.Name = "Col_Route"
        Me.Col_Route.ReadOnly = True
        '
        'col_SubRoute
        '
        Me.col_SubRoute.HeaderText = "เส้นทางย่อย"
        Me.col_SubRoute.Name = "col_SubRoute"
        Me.col_SubRoute.ReadOnly = True
        '
        'ColAddress
        '
        Me.ColAddress.FillWeight = 180.0!
        Me.ColAddress.HeaderText = "ที่อยู่"
        Me.ColAddress.Name = "ColAddress"
        Me.ColAddress.ReadOnly = True
        Me.ColAddress.Width = 200
        '
        'ColThaidefinition
        '
        Me.ColThaidefinition.HeaderText = "อำเภอ"
        Me.ColThaidefinition.Name = "ColThaidefinition"
        Me.ColThaidefinition.ReadOnly = True
        '
        'ColCity
        '
        Me.ColCity.HeaderText = "จังหวัด"
        Me.ColCity.Name = "ColCity"
        Me.ColCity.ReadOnly = True
        '
        'ColCountry
        '
        Me.ColCountry.HeaderText = "ประเทศ"
        Me.ColCountry.Name = "ColCountry"
        Me.ColCountry.ReadOnly = True
        '
        'ColTel
        '
        Me.ColTel.HeaderText = "โทรศัพท์"
        Me.ColTel.Name = "ColTel"
        Me.ColTel.ReadOnly = True
        '
        'ColMobileTel
        '
        Me.ColMobileTel.HeaderText = "โทรศัพท์มือถือ"
        Me.ColMobileTel.Name = "ColMobileTel"
        Me.ColMobileTel.ReadOnly = True
        Me.ColMobileTel.Width = 120
        '
        'ColIndex
        '
        Me.ColIndex.HeaderText = "Index"
        Me.ColIndex.Name = "ColIndex"
        Me.ColIndex.ReadOnly = True
        Me.ColIndex.Visible = False
        '
        'Col_UserSalesTool
        '
        Me.Col_UserSalesTool.DataPropertyName = "SalesTool_User"
        Me.Col_UserSalesTool.HeaderText = "ชื่อผู้ใช้ST"
        Me.Col_UserSalesTool.Name = "Col_UserSalesTool"
        Me.Col_UserSalesTool.ReadOnly = True
        '
        'btnCusRefId
        '
        Me.btnCusRefId.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnCusRefId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCusRefId.Location = New System.Drawing.Point(318, 396)
        Me.btnCusRefId.Name = "btnCusRefId"
        Me.btnCusRefId.Size = New System.Drawing.Size(113, 38)
        Me.btnCusRefId.TabIndex = 7
        Me.btnCusRefId.Text = "       รหัสลูกค้าอ้างอิง"
        Me.btnCusRefId.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสลูกค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "คำนำ"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 60
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "ชื่อ-สกุล"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.FillWeight = 180.0!
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "ที่อยู่"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.FillWeight = 110.0!
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "อำเภอ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 110
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "จังหวัด"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn7.Frozen = True
        Me.DataGridViewTextBoxColumn7.HeaderText = "โทรศัพท์"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.Frozen = True
        Me.DataGridViewTextBoxColumn8.HeaderText = "โทรศัพท์มือถือ"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 200
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'Button1
        '
        Me.Button1.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.รายงาน_แสดงรายงาน
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(12, 440)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(106, 38)
        Me.Button1.TabIndex = 36
        Me.Button1.Text = "Config Report"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmCustomer_ShippingV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 485)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCusRefId)
        Me.Controls.Add(Me.grdCustomer_Shipping)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomer_ShippingV2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ลูกค้าบริษัท/ผู้รับสินค้า"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdCustomer_Shipping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lbSearchfrom As System.Windows.Forms.Label
    Friend WithEvents cboSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
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
    Friend WithEvents grdCustomer_Shipping As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCusRefId As System.Windows.Forms.Button
    Friend WithEvents ColCustomerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName_Thai As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName_Eng As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Route As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SubRoute As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColThaidefinition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCountry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMobileTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_UserSalesTool As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkINT_U As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
