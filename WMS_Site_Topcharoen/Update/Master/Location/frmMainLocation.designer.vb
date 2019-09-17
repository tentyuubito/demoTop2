<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainLocation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainLocation))
        Me.grdLocationType = New System.Windows.Forms.DataGridView
        Me.col_select = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColAlias = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColWareHouse = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRoom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColZone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColLock = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColLocationType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxVolume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.btn_clear = New System.Windows.Forms.Button
        Me.lbSearchfrom = New System.Windows.Forms.Label
        Me.cboSearchType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblCount_location = New System.Windows.Forms.Label
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
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.grbPrintReport = New System.Windows.Forms.GroupBox
        Me.btnPrintReport = New System.Windows.Forms.Button
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.btnAddM = New System.Windows.Forms.Button
        CType(Me.grdLocationType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearch.SuspendLayout()
        Me.grbPrintReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdLocationType
        '
        Me.grdLocationType.AllowUserToAddRows = False
        Me.grdLocationType.AllowUserToOrderColumns = True
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdLocationType.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdLocationType.BackgroundColor = System.Drawing.Color.White
        Me.grdLocationType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_select, Me.Column6, Me.ColAlias, Me.ColWareHouse, Me.ColRoom, Me.ColZone, Me.ColLock, Me.Column7, Me.Column8, Me.Column9, Me.ColLocationType, Me.ColMaxQty, Me.ColMaxWeight, Me.ColMaxVolume, Me.Column1, Me.Column3, Me.Column2, Me.Column4, Me.Column5})
        Me.grdLocationType.Location = New System.Drawing.Point(7, 64)
        Me.grdLocationType.Name = "grdLocationType"
        Me.grdLocationType.Size = New System.Drawing.Size(823, 421)
        Me.grdLocationType.TabIndex = 0
        '
        'col_select
        '
        Me.col_select.DataPropertyName = "chk_select"
        Me.col_select.Frozen = True
        Me.col_select.HeaderText = ""
        Me.col_select.Name = "col_select"
        Me.col_select.Width = 35
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "Location_Index"
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        Me.Column6.Visible = False
        Me.Column6.Width = 5
        '
        'ColAlias
        '
        Me.ColAlias.DataPropertyName = "Location_Alias"
        Me.ColAlias.Frozen = True
        Me.ColAlias.HeaderText = "ตำแหน่ง"
        Me.ColAlias.Name = "ColAlias"
        '
        'ColWareHouse
        '
        Me.ColWareHouse.DataPropertyName = "warehose"
        Me.ColWareHouse.HeaderText = "คลังสินค้า"
        Me.ColWareHouse.Name = "ColWareHouse"
        '
        'ColRoom
        '
        Me.ColRoom.DataPropertyName = "Room"
        Me.ColRoom.HeaderText = "ห้อง"
        Me.ColRoom.Name = "ColRoom"
        '
        'ColZone
        '
        Me.ColZone.DataPropertyName = "Zone"
        Me.ColZone.HeaderText = "โซน"
        Me.ColZone.Name = "ColZone"
        '
        'ColLock
        '
        Me.ColLock.DataPropertyName = "Lock"
        Me.ColLock.HeaderText = "ล็อค"
        Me.ColLock.Name = "ColLock"
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "Row"
        Me.Column7.HeaderText = "แถว"
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 70
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "Level"
        Me.Column8.HeaderText = "ชั้น"
        Me.Column8.Name = "Column8"
        Me.Column8.Width = 70
        '
        'Column9
        '
        Me.Column9.DataPropertyName = "Depth"
        Me.Column9.HeaderText = "ลึก"
        Me.Column9.Name = "Column9"
        Me.Column9.Width = 70
        '
        'ColLocationType
        '
        Me.ColLocationType.DataPropertyName = "LocationType"
        Me.ColLocationType.HeaderText = "ประเภทตำแหน่ง"
        Me.ColLocationType.Name = "ColLocationType"
        Me.ColLocationType.Width = 150
        '
        'ColMaxQty
        '
        Me.ColMaxQty.DataPropertyName = "Max_Qty"
        Me.ColMaxQty.HeaderText = "ปริมาณสูงสุด"
        Me.ColMaxQty.Name = "ColMaxQty"
        '
        'ColMaxWeight
        '
        Me.ColMaxWeight.DataPropertyName = "Max_Weight"
        Me.ColMaxWeight.HeaderText = "น้ำหนักสูงสุด"
        Me.ColMaxWeight.Name = "ColMaxWeight"
        '
        'ColMaxVolume
        '
        Me.ColMaxVolume.DataPropertyName = "Max_Volume"
        Me.ColMaxVolume.HeaderText = "ปริมาตรสูงสุด"
        Me.ColMaxVolume.Name = "ColMaxVolume"
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "Room_Id"
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.Visible = False
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "Row"
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.Visible = False
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "Location_Id"
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.Visible = False
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "LocationType_Index"
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        Me.Column4.Visible = False
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "Warehouse_No"
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        Me.Column5.Visible = False
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.btn_clear)
        Me.grpSearch.Controls.Add(Me.lbSearchfrom)
        Me.grpSearch.Controls.Add(Me.cboSearchType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Location = New System.Drawing.Point(7, 5)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(557, 53)
        Me.grpSearch.TabIndex = 4
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "ค้นหา"
        '
        'btn_clear
        '
        Me.btn_clear.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.btn_clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_clear.Location = New System.Drawing.Point(445, 9)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(100, 38)
        Me.btn_clear.TabIndex = 4
        Me.btn_clear.Text = "ล้าง"
        Me.btn_clear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_clear.UseVisualStyleBackColor = True
        '
        'lbSearchfrom
        '
        Me.lbSearchfrom.AutoSize = True
        Me.lbSearchfrom.Location = New System.Drawing.Point(6, 22)
        Me.lbSearchfrom.Name = "lbSearchfrom"
        Me.lbSearchfrom.Size = New System.Drawing.Size(43, 13)
        Me.lbSearchfrom.TabIndex = 3
        Me.lbSearchfrom.Text = "เงื่อนไข"
        '
        'cboSearchType
        '
        Me.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchType.FormattingEnabled = True
        Me.cboSearchType.Items.AddRange(New Object() {"ทั้งหมด", "คลังสินค้า", "ประเภทตำแหน่ง", "ตำแหน่ง", "ห้อง", "โซน"})
        Me.cboSearchType.Location = New System.Drawing.Point(55, 19)
        Me.cboSearchType.Name = "cboSearchType"
        Me.cboSearchType.Size = New System.Drawing.Size(113, 21)
        Me.cboSearchType.TabIndex = 0
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(174, 20)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(150, 20)
        Me.txtSearchKey.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(332, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(730, 491)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 38)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(321, 491)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdate.Location = New System.Drawing.Point(219, 491)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.BtnUpdate.TabIndex = 2
        Me.BtnUpdate.Text = "แก้ไข"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(7, 491)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "เพิ่ม"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblCount_location
        '
        Me.lblCount_location.AutoSize = True
        Me.lblCount_location.ForeColor = System.Drawing.Color.Blue
        Me.lblCount_location.Location = New System.Drawing.Point(4, 545)
        Me.lblCount_location.Name = "lblCount_location"
        Me.lblCount_location.Size = New System.Drawing.Size(125, 13)
        Me.lblCount_location.TabIndex = 5
        Me.lblCount_location.Text = "จำนวนทั้งหมด 0 ตำแหน่ง"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Location_Alias"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ตำแหน่ง"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "warehose"
        Me.DataGridViewTextBoxColumn2.HeaderText = "คลังสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Room"
        Me.DataGridViewTextBoxColumn3.HeaderText = "ห้อง"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Lock"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ล็อค"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "LocationType"
        Me.DataGridViewTextBoxColumn5.HeaderText = "ประเภทตำแหน่ง"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Max_Qty"
        Me.DataGridViewTextBoxColumn6.HeaderText = "จำนวนสูงสุด"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Max_Weight"
        Me.DataGridViewTextBoxColumn7.HeaderText = "น้ำหนักสูงสุด"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 70
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Max_Volume"
        Me.DataGridViewTextBoxColumn8.HeaderText = "ปริมาตรสูงสุด"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 70
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Room_Id"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 70
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Row"
        Me.DataGridViewTextBoxColumn10.HeaderText = "Column3"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Location_Id"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Column2"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "LocationType_Index"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Warehouse_No"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Column5"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Location_Index"
        Me.DataGridViewTextBoxColumn14.HeaderText = "Column6"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Row"
        Me.DataGridViewTextBoxColumn15.HeaderText = "โซน"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Location_Id"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Column2"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "LocationType_Index"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Warehouse_No"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Column5"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Visible = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(58, 69)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 17
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'grbPrintReport
        '
        Me.grbPrintReport.Controls.Add(Me.btnPrintReport)
        Me.grbPrintReport.Controls.Add(Me.cboPrint)
        Me.grbPrintReport.Location = New System.Drawing.Point(427, 491)
        Me.grbPrintReport.Name = "grbPrintReport"
        Me.grbPrintReport.Size = New System.Drawing.Size(295, 61)
        Me.grbPrintReport.TabIndex = 53
        Me.grbPrintReport.TabStop = False
        Me.grbPrintReport.Text = "เอกสารกำกับสินค้า"
        '
        'btnPrintReport
        '
        Me.btnPrintReport.Image = CType(resources.GetObject("btnPrintReport.Image"), System.Drawing.Image)
        Me.btnPrintReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintReport.Location = New System.Drawing.Point(6, 16)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(107, 38)
        Me.btnPrintReport.TabIndex = 2
        Me.btnPrintReport.Text = "พิมพ์"
        Me.btnPrintReport.UseVisualStyleBackColor = True
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Location = New System.Drawing.Point(119, 25)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(165, 21)
        Me.cboPrint.TabIndex = 1
        '
        'btnAddM
        '
        Me.btnAddM.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddM.Location = New System.Drawing.Point(113, 491)
        Me.btnAddM.Name = "btnAddM"
        Me.btnAddM.Size = New System.Drawing.Size(100, 38)
        Me.btnAddM.TabIndex = 54
        Me.btnAddM.Text = "เพิ่ม Multi"
        Me.btnAddM.UseVisualStyleBackColor = True
        '
        'frmMainLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 567)
        Me.Controls.Add(Me.btnAddM)
        Me.Controls.Add(Me.grbPrintReport)
        Me.Controls.Add(Me.chkSelectAll)
        Me.Controls.Add(Me.lblCount_location)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdLocationType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainLocation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลตำแหน่งจัดเก็บ"
        CType(Me.grdLocationType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.grbPrintReport.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdLocationType As System.Windows.Forms.DataGridView
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lbSearchfrom As System.Windows.Forms.Label
    Friend WithEvents cboSearchType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
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
    Friend WithEvents btn_clear As System.Windows.Forms.Button
    Friend WithEvents lblCount_location As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents col_select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAlias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColWareHouse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRoom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColZone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLocationType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grbPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddM As System.Windows.Forms.Button
End Class
