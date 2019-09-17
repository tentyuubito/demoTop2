<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGroupingConditionPicking
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
        Me.dgvItem = New System.Windows.Forms.DataGridView
        Me.btnCreateWithdraw = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.dgvItemGroup = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnPrinter = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtWithdraw_No = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtWV_User = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.lblAlert = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.btnWithdrawView = New System.Windows.Forms.Button
        Me.lblRecord_so = New System.Windows.Forms.Label
        Me.lblRecord_sogroup = New System.Windows.Forms.Label
        Me.grbFullFill = New System.Windows.Forms.GroupBox
        Me.chkFullFill = New System.Windows.Forms.CheckBox
        Me.pnlProgressBar = New System.Windows.Forms.Panel
        Me.lblProgress = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.BwgImport = New System.ComponentModel.BackgroundWorker
        Me.col_Pre_Manifest_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Type_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Shipping_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Company_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Shelf_Life = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SalesOrder_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Branch_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PLot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SO_Exp_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_QtyCT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_QtyPC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty_Plan = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty_Withdraw = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_btnCheckStock = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_btnEditSO = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_btnManual = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_MinAgeRemain = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Day_picking = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_FlagDont_Reverse_LOT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_LastExp_date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_FlagMix_Lot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_Per_Pallet = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SalesOrder_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SalesOrderItem_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_MFG_Day_Count = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItemGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbFullFill.SuspendLayout()
        Me.pnlProgressBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvItem
        '
        Me.dgvItem.AllowUserToAddRows = False
        Me.dgvItem.AllowUserToDeleteRows = False
        Me.dgvItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItem.BackgroundColor = System.Drawing.Color.White
        Me.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Pre_Manifest_No, Me.col_Customer_Type_Desc, Me.col_Customer_Shipping_Id, Me.col_Company_Name, Me.col_DistributionCenter, Me.col_Shelf_Life, Me.col_SalesOrder_No, Me.col_Branch_Id, Me.col_Sku_Id, Me.col_Sku_Name, Me.col_PLot, Me.col_SO_Exp_Date, Me.col_ItemStatus_Id, Me.col_QtyCT, Me.col_QtyPC, Me.col_Total_Qty_Plan, Me.col_Total_Qty_Withdraw, Me.col_btnCheckStock, Me.col_btnEditSO, Me.col_btnManual, Me.col_MinAgeRemain, Me.col_Day_picking, Me.col_FlagDont_Reverse_LOT, Me.col_LastExp_date, Me.col_FlagMix_Lot, Me.col_Qty_Per_Pallet, Me.col_Total_Qty, Me.col_SalesOrder_Index, Me.col_SalesOrderItem_Index, Me.col_ItemStatus_Index, Me.col_Sku_Index, Me.col_Package_Index, Me.col_Customer_Index, Me.col_Customer_Id, Me.col_Customer_Name})
        Me.dgvItem.Location = New System.Drawing.Point(6, 19)
        Me.dgvItem.Name = "dgvItem"
        Me.dgvItem.ReadOnly = True
        Me.dgvItem.RowHeadersVisible = False
        Me.dgvItem.Size = New System.Drawing.Size(1230, 190)
        Me.dgvItem.TabIndex = 0
        '
        'btnCreateWithdraw
        '
        Me.btnCreateWithdraw.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แนะนำตำแหน่งจัดเก็บ
        Me.btnCreateWithdraw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCreateWithdraw.Location = New System.Drawing.Point(184, 555)
        Me.btnCreateWithdraw.Name = "btnCreateWithdraw"
        Me.btnCreateWithdraw.Size = New System.Drawing.Size(102, 41)
        Me.btnCreateWithdraw.TabIndex = 2
        Me.btnCreateWithdraw.Text = "สร้างใบเบิก+" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FIFO (FG)"
        Me.btnCreateWithdraw.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCreateWithdraw.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(1146, 555)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(102, 41)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dgvItemGroup
        '
        Me.dgvItemGroup.AllowUserToAddRows = False
        Me.dgvItemGroup.AllowUserToDeleteRows = False
        Me.dgvItemGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemGroup.BackgroundColor = System.Drawing.Color.White
        Me.dgvItemGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.col_DistributionCenter2, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.col_MFG_Day_Count, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13, Me.Column4, Me.Column3, Me.DataGridViewTextBoxColumn14, Me.Column2, Me.DataGridViewTextBoxColumn15})
        Me.dgvItemGroup.Location = New System.Drawing.Point(6, 19)
        Me.dgvItemGroup.Name = "dgvItemGroup"
        Me.dgvItemGroup.ReadOnly = True
        Me.dgvItemGroup.RowHeadersVisible = False
        Me.dgvItemGroup.Size = New System.Drawing.Size(1230, 255)
        Me.dgvItemGroup.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvItem)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1242, 215)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sales Order List"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvItemGroup)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 269)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1242, 280)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Group Pick"
        '
        'btnPrinter
        '
        Me.btnPrinter.Enabled = False
        Me.btnPrinter.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.btnPrinter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrinter.Location = New System.Drawing.Point(400, 555)
        Me.btnPrinter.Name = "btnPrinter"
        Me.btnPrinter.Size = New System.Drawing.Size(109, 41)
        Me.btnPrinter.TabIndex = 7
        Me.btnPrinter.Text = "พิมพ์เอกสาร"
        Me.btnPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrinter.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "เลขที่ใบเบิก"
        '
        'txtWithdraw_No
        '
        Me.txtWithdraw_No.Location = New System.Drawing.Point(84, 11)
        Me.txtWithdraw_No.Name = "txtWithdraw_No"
        Me.txtWithdraw_No.ReadOnly = True
        Me.txtWithdraw_No.Size = New System.Drawing.Size(138, 20)
        Me.txtWithdraw_No.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(281, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "ประเภทเอกสาร"
        '
        'txtWV_User
        '
        Me.txtWV_User.Location = New System.Drawing.Point(990, 14)
        Me.txtWV_User.Name = "txtWV_User"
        Me.txtWV_User.ReadOnly = True
        Me.txtWV_User.Size = New System.Drawing.Size(264, 20)
        Me.txtWV_User.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(923, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "ผู้ออกเอกสาร"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(292, 555)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(102, 41)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "บันทึกใบเบิก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(366, 10)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(216, 21)
        Me.cboDocumentType.TabIndex = 16
        '
        'lblAlert
        '
        Me.lblAlert.AutoSize = True
        Me.lblAlert.ForeColor = System.Drawing.Color.Red
        Me.lblAlert.Location = New System.Drawing.Point(515, 569)
        Me.lblAlert.Name = "lblAlert"
        Me.lblAlert.Size = New System.Drawing.Size(158, 13)
        Me.lblAlert.TabIndex = 17
        Me.lblAlert.Text = "*มีการเติมสินค้าในรายการเบิกนี้"
        Me.lblAlert.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(588, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Picking For"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(654, 10)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(263, 37)
        Me.txtComment.TabIndex = 51
        '
        'btnWithdrawView
        '
        Me.btnWithdrawView.Location = New System.Drawing.Point(228, 9)
        Me.btnWithdrawView.Name = "btnWithdrawView"
        Me.btnWithdrawView.Size = New System.Drawing.Size(24, 23)
        Me.btnWithdrawView.TabIndex = 52
        Me.btnWithdrawView.Text = "..."
        Me.btnWithdrawView.UseVisualStyleBackColor = True
        '
        'lblRecord_so
        '
        Me.lblRecord_so.AutoSize = True
        Me.lblRecord_so.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRecord_so.Location = New System.Drawing.Point(1048, 260)
        Me.lblRecord_so.Name = "lblRecord_so"
        Me.lblRecord_so.Size = New System.Drawing.Size(14, 13)
        Me.lblRecord_so.TabIndex = 53
        Me.lblRecord_so.Tag = ": Rows"
        Me.lblRecord_so.Text = "0"
        '
        'lblRecord_sogroup
        '
        Me.lblRecord_sogroup.AutoSize = True
        Me.lblRecord_sogroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRecord_sogroup.Location = New System.Drawing.Point(1048, 555)
        Me.lblRecord_sogroup.Name = "lblRecord_sogroup"
        Me.lblRecord_sogroup.Size = New System.Drawing.Size(14, 13)
        Me.lblRecord_sogroup.TabIndex = 54
        Me.lblRecord_sogroup.Tag = ": Rows"
        Me.lblRecord_sogroup.Text = "0"
        '
        'grbFullFill
        '
        Me.grbFullFill.Controls.Add(Me.chkFullFill)
        Me.grbFullFill.Location = New System.Drawing.Point(12, 549)
        Me.grbFullFill.Name = "grbFullFill"
        Me.grbFullFill.Size = New System.Drawing.Size(166, 57)
        Me.grbFullFill.TabIndex = 55
        Me.grbFullFill.TabStop = False
        Me.grbFullFill.Text = "เติมเต็มสินค้า"
        '
        'chkFullFill
        '
        Me.chkFullFill.AutoSize = True
        Me.chkFullFill.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkFullFill.Location = New System.Drawing.Point(23, 25)
        Me.chkFullFill.Name = "chkFullFill"
        Me.chkFullFill.Size = New System.Drawing.Size(107, 17)
        Me.chkFullFill.TabIndex = 0
        Me.chkFullFill.Text = "มีการเติมสินค้า"
        Me.chkFullFill.UseVisualStyleBackColor = True
        '
        'pnlProgressBar
        '
        Me.pnlProgressBar.BackColor = System.Drawing.Color.White
        Me.pnlProgressBar.Controls.Add(Me.lblProgress)
        Me.pnlProgressBar.Controls.Add(Me.ProgressBar1)
        Me.pnlProgressBar.Location = New System.Drawing.Point(422, 222)
        Me.pnlProgressBar.Name = "pnlProgressBar"
        Me.pnlProgressBar.Size = New System.Drawing.Size(422, 100)
        Me.pnlProgressBar.TabIndex = 81
        Me.pnlProgressBar.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(11, 13)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(136, 16)
        Me.lblProgress.TabIndex = 9
        Me.lblProgress.Tag = "ระบบกำลังประมวลผล..."
        Me.lblProgress.Text = "ระบบกำลังประมวลผล..."
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(14, 32)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(393, 48)
        Me.ProgressBar1.TabIndex = 8
        '
        'BwgImport
        '
        Me.BwgImport.WorkerReportsProgress = True
        Me.BwgImport.WorkerSupportsCancellation = True
        '
        'col_Pre_Manifest_No
        '
        Me.col_Pre_Manifest_No.DataPropertyName = "Pre_Manifest_No"
        Me.col_Pre_Manifest_No.HeaderText = "Pre-Manifest No"
        Me.col_Pre_Manifest_No.Name = "col_Pre_Manifest_No"
        Me.col_Pre_Manifest_No.ReadOnly = True
        Me.col_Pre_Manifest_No.Width = 120
        '
        'col_Customer_Type_Desc
        '
        Me.col_Customer_Type_Desc.DataPropertyName = "Customer_Type_Desc"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.col_Customer_Type_Desc.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_Customer_Type_Desc.HeaderText = "ประเภทลูกค้า"
        Me.col_Customer_Type_Desc.Name = "col_Customer_Type_Desc"
        Me.col_Customer_Type_Desc.ReadOnly = True
        '
        'col_Customer_Shipping_Id
        '
        Me.col_Customer_Shipping_Id.DataPropertyName = "Customer_Shipping_Id"
        Me.col_Customer_Shipping_Id.HeaderText = "รหัสลูกค้า"
        Me.col_Customer_Shipping_Id.Name = "col_Customer_Shipping_Id"
        Me.col_Customer_Shipping_Id.ReadOnly = True
        '
        'col_Company_Name
        '
        Me.col_Company_Name.DataPropertyName = "Company_Name"
        Me.col_Company_Name.HeaderText = "ชื่อลูกค้า"
        Me.col_Company_Name.Name = "col_Company_Name"
        Me.col_Company_Name.ReadOnly = True
        '
        'col_DistributionCenter
        '
        Me.col_DistributionCenter.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter.Name = "col_DistributionCenter"
        Me.col_DistributionCenter.ReadOnly = True
        '
        'col_Shelf_Life
        '
        Me.col_Shelf_Life.DataPropertyName = "Shelf_Life"
        Me.col_Shelf_Life.HeaderText = "Req Aging (C)"
        Me.col_Shelf_Life.Name = "col_Shelf_Life"
        Me.col_Shelf_Life.ReadOnly = True
        '
        'col_SalesOrder_No
        '
        Me.col_SalesOrder_No.DataPropertyName = "SalesOrder_No"
        Me.col_SalesOrder_No.HeaderText = "SO No"
        Me.col_SalesOrder_No.Name = "col_SalesOrder_No"
        Me.col_SalesOrder_No.ReadOnly = True
        '
        'col_Branch_Id
        '
        Me.col_Branch_Id.DataPropertyName = "Branch_Id"
        Me.col_Branch_Id.HeaderText = "สาขา"
        Me.col_Branch_Id.Name = "col_Branch_Id"
        Me.col_Branch_Id.ReadOnly = True
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.ReadOnly = True
        '
        'col_Sku_Name
        '
        Me.col_Sku_Name.DataPropertyName = "Sku_Name"
        Me.col_Sku_Name.HeaderText = "ชื่อสินค้า"
        Me.col_Sku_Name.Name = "col_Sku_Name"
        Me.col_Sku_Name.ReadOnly = True
        '
        'col_PLot
        '
        Me.col_PLot.DataPropertyName = "PLot"
        Me.col_PLot.HeaderText = "Lot/Batch"
        Me.col_PLot.Name = "col_PLot"
        Me.col_PLot.ReadOnly = True
        '
        'col_SO_Exp_Date
        '
        Me.col_SO_Exp_Date.DataPropertyName = "SO_Exp_Date"
        Me.col_SO_Exp_Date.HeaderText = "Exp Date"
        Me.col_SO_Exp_Date.Name = "col_SO_Exp_Date"
        Me.col_SO_Exp_Date.ReadOnly = True
        '
        'col_ItemStatus_Id
        '
        Me.col_ItemStatus_Id.DataPropertyName = "ItemStatus_Id"
        Me.col_ItemStatus_Id.HeaderText = "WH Code"
        Me.col_ItemStatus_Id.Name = "col_ItemStatus_Id"
        Me.col_ItemStatus_Id.ReadOnly = True
        '
        'col_QtyCT
        '
        Me.col_QtyCT.DataPropertyName = "QtyCARTON"
        Me.col_QtyCT.HeaderText = "Qty CT"
        Me.col_QtyCT.Name = "col_QtyCT"
        Me.col_QtyCT.ReadOnly = True
        '
        'col_QtyPC
        '
        Me.col_QtyPC.DataPropertyName = "QtyPC"
        Me.col_QtyPC.HeaderText = "Qty PC"
        Me.col_QtyPC.Name = "col_QtyPC"
        Me.col_QtyPC.ReadOnly = True
        '
        'col_Total_Qty_Plan
        '
        Me.col_Total_Qty_Plan.DataPropertyName = "Total_Qty_Plan"
        Me.col_Total_Qty_Plan.HeaderText = "จำนวน"
        Me.col_Total_Qty_Plan.Name = "col_Total_Qty_Plan"
        Me.col_Total_Qty_Plan.ReadOnly = True
        '
        'col_Total_Qty_Withdraw
        '
        Me.col_Total_Qty_Withdraw.DataPropertyName = "Total_Qty_Withdraw"
        Me.col_Total_Qty_Withdraw.HeaderText = "เบิกแล้ว"
        Me.col_Total_Qty_Withdraw.Name = "col_Total_Qty_Withdraw"
        Me.col_Total_Qty_Withdraw.ReadOnly = True
        '
        'col_btnCheckStock
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.NullValue = "CheckStock"
        Me.col_btnCheckStock.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_btnCheckStock.HeaderText = ""
        Me.col_btnCheckStock.Name = "col_btnCheckStock"
        Me.col_btnCheckStock.ReadOnly = True
        Me.col_btnCheckStock.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_btnCheckStock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.col_btnCheckStock.Width = 80
        '
        'col_btnEditSO
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.NullValue = "แก้ SO"
        Me.col_btnEditSO.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_btnEditSO.HeaderText = ""
        Me.col_btnEditSO.Name = "col_btnEditSO"
        Me.col_btnEditSO.ReadOnly = True
        '
        'col_btnManual
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.NullValue = "หยิบ"
        Me.col_btnManual.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_btnManual.HeaderText = ""
        Me.col_btnManual.Name = "col_btnManual"
        Me.col_btnManual.ReadOnly = True
        '
        'col_MinAgeRemain
        '
        Me.col_MinAgeRemain.DataPropertyName = "MinAgeRemain"
        Me.col_MinAgeRemain.HeaderText = "Aging (STD)"
        Me.col_MinAgeRemain.Name = "col_MinAgeRemain"
        Me.col_MinAgeRemain.ReadOnly = True
        '
        'col_Day_picking
        '
        Me.col_Day_picking.DataPropertyName = "Day_picking"
        Me.col_Day_picking.HeaderText = "อายุสินค้า(C)"
        Me.col_Day_picking.Name = "col_Day_picking"
        Me.col_Day_picking.ReadOnly = True
        '
        'col_FlagDont_Reverse_LOT
        '
        Me.col_FlagDont_Reverse_LOT.DataPropertyName = "FlagDont_Reverse_LOT"
        Me.col_FlagDont_Reverse_LOT.HeaderText = "ห้ามย้อน Lot(C)"
        Me.col_FlagDont_Reverse_LOT.Name = "col_FlagDont_Reverse_LOT"
        Me.col_FlagDont_Reverse_LOT.ReadOnly = True
        Me.col_FlagDont_Reverse_LOT.Width = 120
        '
        'col_LastExp_date
        '
        Me.col_LastExp_date.DataPropertyName = "LastExp_date"
        Me.col_LastExp_date.HeaderText = "Last Lot"
        Me.col_LastExp_date.Name = "col_LastExp_date"
        Me.col_LastExp_date.ReadOnly = True
        '
        'col_FlagMix_Lot
        '
        Me.col_FlagMix_Lot.DataPropertyName = "FlagMix_Lot"
        Me.col_FlagMix_Lot.HeaderText = "ห้ามเกิน 2 Lot"
        Me.col_FlagMix_Lot.Name = "col_FlagMix_Lot"
        Me.col_FlagMix_Lot.ReadOnly = True
        Me.col_FlagMix_Lot.Width = 120
        '
        'col_Qty_Per_Pallet
        '
        Me.col_Qty_Per_Pallet.DataPropertyName = "Qty_Per_Pallet"
        Me.col_Qty_Per_Pallet.HeaderText = "Pallet"
        Me.col_Qty_Per_Pallet.Name = "col_Qty_Per_Pallet"
        Me.col_Qty_Per_Pallet.ReadOnly = True
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        Me.col_Total_Qty.HeaderText = "Total_Qty"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        Me.col_Total_Qty.Visible = False
        '
        'col_SalesOrder_Index
        '
        Me.col_SalesOrder_Index.DataPropertyName = "SalesOrder_Index"
        Me.col_SalesOrder_Index.HeaderText = "SalesOrder_Index"
        Me.col_SalesOrder_Index.Name = "col_SalesOrder_Index"
        Me.col_SalesOrder_Index.ReadOnly = True
        Me.col_SalesOrder_Index.Visible = False
        '
        'col_SalesOrderItem_Index
        '
        Me.col_SalesOrderItem_Index.DataPropertyName = "SalesOrderItem_Index"
        Me.col_SalesOrderItem_Index.HeaderText = "SalesOrderItem_Index"
        Me.col_SalesOrderItem_Index.Name = "col_SalesOrderItem_Index"
        Me.col_SalesOrderItem_Index.ReadOnly = True
        Me.col_SalesOrderItem_Index.Visible = False
        '
        'col_ItemStatus_Index
        '
        Me.col_ItemStatus_Index.DataPropertyName = "ItemStatus_Index"
        Me.col_ItemStatus_Index.HeaderText = "ItemStatus_Index"
        Me.col_ItemStatus_Index.Name = "col_ItemStatus_Index"
        Me.col_ItemStatus_Index.ReadOnly = True
        Me.col_ItemStatus_Index.Visible = False
        '
        'col_Sku_Index
        '
        Me.col_Sku_Index.DataPropertyName = "Sku_Index"
        Me.col_Sku_Index.HeaderText = "Sku_Index"
        Me.col_Sku_Index.Name = "col_Sku_Index"
        Me.col_Sku_Index.ReadOnly = True
        Me.col_Sku_Index.Visible = False
        '
        'col_Package_Index
        '
        Me.col_Package_Index.DataPropertyName = "Package_Index"
        Me.col_Package_Index.HeaderText = "Package_Index"
        Me.col_Package_Index.Name = "col_Package_Index"
        Me.col_Package_Index.ReadOnly = True
        Me.col_Package_Index.Visible = False
        '
        'col_Customer_Index
        '
        Me.col_Customer_Index.DataPropertyName = "Customer_Index"
        Me.col_Customer_Index.HeaderText = "Customer_Index"
        Me.col_Customer_Index.Name = "col_Customer_Index"
        Me.col_Customer_Index.ReadOnly = True
        Me.col_Customer_Index.Visible = False
        '
        'col_Customer_Id
        '
        Me.col_Customer_Id.DataPropertyName = "Customer_Id"
        Me.col_Customer_Id.HeaderText = "Customer_Id"
        Me.col_Customer_Id.Name = "col_Customer_Id"
        Me.col_Customer_Id.ReadOnly = True
        Me.col_Customer_Id.Visible = False
        '
        'col_Customer_Name
        '
        Me.col_Customer_Name.DataPropertyName = "Customer_Name"
        Me.col_Customer_Name.HeaderText = "Customer_Name"
        Me.col_Customer_Name.Name = "col_Customer_Name"
        Me.col_Customer_Name.ReadOnly = True
        Me.col_Customer_Name.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Customer_Type_Desc"
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn3.HeaderText = "ประเภทลูกค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'col_DistributionCenter2
        '
        Me.col_DistributionCenter2.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter2.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter2.Name = "col_DistributionCenter2"
        Me.col_DistributionCenter2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "SalesOrder_No"
        Me.DataGridViewTextBoxColumn5.HeaderText = "SO No"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn6.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn7.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "PLot"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Lot/Batch"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "ItemStatus_Id"
        Me.DataGridViewTextBoxColumn9.HeaderText = "WH Code"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn10.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'col_MFG_Day_Count
        '
        Me.col_MFG_Day_Count.DataPropertyName = "MFG_Day_Count"
        Me.col_MFG_Day_Count.HeaderText = "ผลิตไม่เกิน(วัน)"
        Me.col_MFG_Day_Count.Name = "col_MFG_Day_Count"
        Me.col_MFG_Day_Count.ReadOnly = True
        Me.col_MFG_Day_Count.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Shelf_Life"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Req Aging (C)"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "MinAgeRemain"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Aging (STD)"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Day_picking"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Stock Aging"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "FlagDont_Reverse_LOT"
        Me.DataGridViewTextBoxColumn13.HeaderText = "ห้ามย้อน Lot(C)"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 120
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "LastMfg_date"
        Me.Column4.HeaderText = "LastMfg"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "LastExp_date"
        Me.Column3.HeaderText = "LastExp"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "FlagMix_Lot"
        Me.DataGridViewTextBoxColumn14.HeaderText = "ห้ามเกิน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 120
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "NumFlagMix_Lot"
        Me.Column2.HeaderText = "ล็อต(x)"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Qty_Per_Pallet"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Pallet"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'frmGroupingConditionPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1266, 679)
        Me.Controls.Add(Me.pnlProgressBar)
        Me.Controls.Add(Me.grbFullFill)
        Me.Controls.Add(Me.lblRecord_sogroup)
        Me.Controls.Add(Me.lblRecord_so)
        Me.Controls.Add(Me.btnWithdrawView)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblAlert)
        Me.Controls.Add(Me.cboDocumentType)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtWV_User)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtWithdraw_No)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnPrinter)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCreateWithdraw)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1282, 645)
        Me.Name = "frmGroupingConditionPicking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulate Picking"
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItemGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.grbFullFill.ResumeLayout(False)
        Me.grbFullFill.PerformLayout()
        Me.pnlProgressBar.ResumeLayout(False)
        Me.pnlProgressBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvItem As System.Windows.Forms.DataGridView
    Friend WithEvents btnCreateWithdraw As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dgvItemGroup As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrinter As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWithdraw_No As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWV_User As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblAlert As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents btnWithdrawView As System.Windows.Forms.Button
    Friend WithEvents lblRecord_so As System.Windows.Forms.Label
    Friend WithEvents lblRecord_sogroup As System.Windows.Forms.Label
    Friend WithEvents grbFullFill As System.Windows.Forms.GroupBox
    Friend WithEvents chkFullFill As System.Windows.Forms.CheckBox
    Friend WithEvents pnlProgressBar As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BwgImport As System.ComponentModel.BackgroundWorker
    Friend WithEvents col_Pre_Manifest_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Type_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Shipping_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Company_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Shelf_Life As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SalesOrder_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Branch_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PLot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SO_Exp_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_QtyCT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_QtyPC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty_Plan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty_Withdraw As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_btnCheckStock As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_btnEditSO As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_btnManual As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_MinAgeRemain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Day_picking As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_FlagDont_Reverse_LOT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_LastExp_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_FlagMix_Lot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_Per_Pallet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SalesOrder_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SalesOrderItem_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_MFG_Day_Count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
