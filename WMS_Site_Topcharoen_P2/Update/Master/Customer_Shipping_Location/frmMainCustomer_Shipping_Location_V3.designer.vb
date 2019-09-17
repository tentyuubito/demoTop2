<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainCustomer_Shipping_Location_V3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainCustomer_Shipping_Location_V3))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
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
        Me.grdCustomer = New System.Windows.Forms.DataGridView
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
        Me.ColumnCustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnAddress = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportRegion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Route = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SubRoute = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCountry = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnThaidefinition = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnCity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnCountryName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnTel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnMobileTel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnIndex = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch.SuspendLayout()
        CType(Me.grdCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
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
        Me.BtnUpdate.Image = CType(resources.GetObject("BtnUpdate.Image"), System.Drawing.Image)
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdate.Location = New System.Drawing.Point(118, 396)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.BtnUpdate.TabIndex = 3
        Me.BtnUpdate.Text = "แก้ไข"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(224, 396)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(853, 395)
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
        Me.grpSearch.Location = New System.Drawing.Point(333, 386)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(444, 84)
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
        Me.chkINT_U.TabIndex = 33
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
        Me.cboSearchType.Items.AddRange(New Object() {"รหัสผู้รับ", "ชื่อผู้รับ(ไทย)"})
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
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(332, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'grdCustomer
        '
        Me.grdCustomer.AllowUserToAddRows = False
        Me.grdCustomer.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdCustomer.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCustomer.BackgroundColor = System.Drawing.Color.White
        Me.grdCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnCustomerID, Me.ColumnName, Me.ColumnAddress, Me.col_TransportRegion, Me.col_DistributionCenter, Me.col_Route, Me.col_SubRoute, Me.ColCountry, Me.ColumnThaidefinition, Me.ColumnCity, Me.ColumnCountryName, Me.ColumnTel, Me.ColumnMobileTel, Me.ColumnIndex})
        Me.grdCustomer.Location = New System.Drawing.Point(12, 12)
        Me.grdCustomer.Name = "grdCustomer"
        Me.grdCustomer.ReadOnly = True
        Me.grdCustomer.RowHeadersVisible = False
        Me.grdCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCustomer.Size = New System.Drawing.Size(939, 368)
        Me.grdCustomer.TabIndex = 7
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสลูกค้า"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 120
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
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
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
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
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn9.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 200
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'ColumnCustomerID
        '
        Me.ColumnCustomerID.HeaderText = "รหัสผู้รับ"
        Me.ColumnCustomerID.MinimumWidth = 120
        Me.ColumnCustomerID.Name = "ColumnCustomerID"
        Me.ColumnCustomerID.ReadOnly = True
        Me.ColumnCustomerID.Width = 120
        '
        'ColumnName
        '
        Me.ColumnName.FillWeight = 150.0!
        Me.ColumnName.HeaderText = "ชื่อผู้รับ"
        Me.ColumnName.Name = "ColumnName"
        Me.ColumnName.ReadOnly = True
        Me.ColumnName.Width = 150
        '
        'ColumnAddress
        '
        Me.ColumnAddress.HeaderText = "ที่อยู่"
        Me.ColumnAddress.Name = "ColumnAddress"
        Me.ColumnAddress.ReadOnly = True
        '
        'col_TransportRegion
        '
        Me.col_TransportRegion.HeaderText = "เขตพื้นที่"
        Me.col_TransportRegion.Name = "col_TransportRegion"
        Me.col_TransportRegion.ReadOnly = True
        Me.col_TransportRegion.Visible = False
        '
        'col_DistributionCenter
        '
        Me.col_DistributionCenter.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter.Name = "col_DistributionCenter"
        Me.col_DistributionCenter.ReadOnly = True
        Me.col_DistributionCenter.Visible = False
        '
        'col_Route
        '
        Me.col_Route.HeaderText = "เส้นทางหลัก"
        Me.col_Route.Name = "col_Route"
        Me.col_Route.ReadOnly = True
        Me.col_Route.Visible = False
        '
        'col_SubRoute
        '
        Me.col_SubRoute.HeaderText = "เส้นทางย่อย"
        Me.col_SubRoute.Name = "col_SubRoute"
        Me.col_SubRoute.ReadOnly = True
        Me.col_SubRoute.Visible = False
        '
        'ColCountry
        '
        Me.ColCountry.HeaderText = "ประเทศ"
        Me.ColCountry.Name = "ColCountry"
        Me.ColCountry.ReadOnly = True
        Me.ColCountry.Visible = False
        '
        'ColumnThaidefinition
        '
        Me.ColumnThaidefinition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColumnThaidefinition.HeaderText = "อำเภอ"
        Me.ColumnThaidefinition.Name = "ColumnThaidefinition"
        Me.ColumnThaidefinition.ReadOnly = True
        '
        'ColumnCity
        '
        Me.ColumnCity.HeaderText = "จังหวัด"
        Me.ColumnCity.Name = "ColumnCity"
        Me.ColumnCity.ReadOnly = True
        '
        'ColumnCountryName
        '
        Me.ColumnCountryName.HeaderText = "ประเทศ"
        Me.ColumnCountryName.Name = "ColumnCountryName"
        Me.ColumnCountryName.ReadOnly = True
        '
        'ColumnTel
        '
        Me.ColumnTel.HeaderText = "โทรศัพท์"
        Me.ColumnTel.Name = "ColumnTel"
        Me.ColumnTel.ReadOnly = True
        '
        'ColumnMobileTel
        '
        Me.ColumnMobileTel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColumnMobileTel.HeaderText = "โทรศัพท์มือถือ"
        Me.ColumnMobileTel.Name = "ColumnMobileTel"
        Me.ColumnMobileTel.ReadOnly = True
        '
        'ColumnIndex
        '
        Me.ColumnIndex.HeaderText = "Index"
        Me.ColumnIndex.Name = "ColumnIndex"
        Me.ColumnIndex.ReadOnly = True
        Me.ColumnIndex.Visible = False
        '
        'frmMainCustomer_Shipping_Location_V3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 482)
        Me.Controls.Add(Me.grdCustomer)
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
        Me.Name = "frmMainCustomer_Shipping_Location_V3"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "สถานที่จัดส่ง/ผู้รับ"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdCustomer, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdCustomer As System.Windows.Forms.DataGridView
    Friend WithEvents chkINT_U As System.Windows.Forms.CheckBox
    Friend WithEvents ColumnCustomerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Route As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SubRoute As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCountry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnThaidefinition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnCountryName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnMobileTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnIndex As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
