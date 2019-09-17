<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmAdjust_Stock
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
        Me.txtAdjust_Unit = New System.Windows.Forms.TextBox
        Me.lblAdjust_Unit = New System.Windows.Forms.Label
        Me.txtAdjust_Qty = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.lblName = New System.Windows.Forms.Label
        Me.txtSku_Name = New System.Windows.Forms.TextBox
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.lblBarcode = New System.Windows.Forms.Label
        Me.txtModel = New System.Windows.Forms.TextBox
        Me.lblModel = New System.Windows.Forms.Label
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.lblLocation = New System.Windows.Forms.Label
        Me.lblItemStatus = New System.Windows.Forms.Label
        Me.lblAdjust_Qty = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.dgAdjust_Detail = New System.Windows.Forms.DataGridView
        Me.col_STATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Location_Alias = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.dgAdjust_DetailLocation = New System.Windows.Forms.DataGridView
        Me.ตรวจนับ = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Location_Alias = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblAdjust_Date = New System.Windows.Forms.Label
        Me.txtAdjust_Date = New System.Windows.Forms.TextBox
        Me.txtDocumentType = New System.Windows.Forms.TextBox
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.lblAdjust_No = New System.Windows.Forms.Label
        Me.txtAdjust_No = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgAdjust_Detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgAdjust_DetailLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtAdjust_Unit
        '
        Me.txtAdjust_Unit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtAdjust_Unit.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAdjust_Unit.Location = New System.Drawing.Point(234, 161)
        Me.txtAdjust_Unit.Name = "txtAdjust_Unit"
        Me.txtAdjust_Unit.ReadOnly = True
        Me.txtAdjust_Unit.Size = New System.Drawing.Size(82, 24)
        Me.txtAdjust_Unit.TabIndex = 35
        Me.txtAdjust_Unit.TabStop = False
        '
        'lblAdjust_Unit
        '
        Me.lblAdjust_Unit.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdjust_Unit.Location = New System.Drawing.Point(191, 166)
        Me.lblAdjust_Unit.Name = "lblAdjust_Unit"
        Me.lblAdjust_Unit.Size = New System.Drawing.Size(47, 15)
        Me.lblAdjust_Unit.TabIndex = 53
        Me.lblAdjust_Unit.Text = "หน่วย"
        '
        'txtAdjust_Qty
        '
        Me.txtAdjust_Qty.BackColor = System.Drawing.Color.White
        Me.txtAdjust_Qty.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAdjust_Qty.Location = New System.Drawing.Point(82, 161)
        Me.txtAdjust_Qty.Name = "txtAdjust_Qty"
        Me.txtAdjust_Qty.Size = New System.Drawing.Size(107, 24)
        Me.txtAdjust_Qty.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(11, 245)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(114, 42)
        Me.btnSave.TabIndex = 38
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnBack.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBack.Location = New System.Drawing.Point(270, 245)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(114, 41)
        Me.btnBack.TabIndex = 39
        Me.btnBack.Text = "ปิด"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'lblName
        '
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(8, 109)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(63, 19)
        Me.lblName.TabIndex = 60
        Me.lblName.Text = "ชื่อสินค้า"
        '
        'txtSku_Name
        '
        Me.txtSku_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSku_Name.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtSku_Name.Location = New System.Drawing.Point(82, 104)
        Me.txtSku_Name.Name = "txtSku_Name"
        Me.txtSku_Name.ReadOnly = True
        Me.txtSku_Name.Size = New System.Drawing.Size(234, 24)
        Me.txtSku_Name.TabIndex = 29
        Me.txtSku_Name.TabStop = False
        '
        'txtBarcode
        '
        Me.txtBarcode.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtBarcode.Location = New System.Drawing.Point(82, 44)
        Me.txtBarcode.MaxLength = 100
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(234, 24)
        Me.txtBarcode.TabIndex = 1
        '
        'lblBarcode
        '
        Me.lblBarcode.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblBarcode.Location = New System.Drawing.Point(8, 44)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(63, 18)
        Me.lblBarcode.TabIndex = 57
        Me.lblBarcode.Text = "รหัสสินค้า"
        '
        'txtModel
        '
        Me.txtModel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtModel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtModel.Location = New System.Drawing.Point(82, 74)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.ReadOnly = True
        Me.txtModel.Size = New System.Drawing.Size(234, 24)
        Me.txtModel.TabIndex = 28
        Me.txtModel.TabStop = False
        '
        'lblModel
        '
        Me.lblModel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblModel.Location = New System.Drawing.Point(8, 77)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(67, 20)
        Me.lblModel.TabIndex = 56
        Me.lblModel.Text = "รหัสสินค้า"
        '
        'txtLocation
        '
        Me.txtLocation.BackColor = System.Drawing.Color.White
        Me.txtLocation.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtLocation.Location = New System.Drawing.Point(82, 14)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(234, 24)
        Me.txtLocation.TabIndex = 0
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblLocation.Location = New System.Drawing.Point(8, 14)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(63, 20)
        Me.lblLocation.TabIndex = 52
        Me.lblLocation.Text = "ตำแหน่ง"
        '
        'lblItemStatus
        '
        Me.lblItemStatus.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblItemStatus.Location = New System.Drawing.Point(8, 135)
        Me.lblItemStatus.Name = "lblItemStatus"
        Me.lblItemStatus.Size = New System.Drawing.Size(63, 22)
        Me.lblItemStatus.TabIndex = 54
        Me.lblItemStatus.Text = "สถานะ"
        '
        'lblAdjust_Qty
        '
        Me.lblAdjust_Qty.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdjust_Qty.Location = New System.Drawing.Point(8, 166)
        Me.lblAdjust_Qty.Name = "lblAdjust_Qty"
        Me.lblAdjust_Qty.Size = New System.Drawing.Size(78, 14)
        Me.lblAdjust_Qty.TabIndex = 63
        Me.lblAdjust_Qty.Text = "จน.ตรวจนับ"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 130)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(408, 316)
        Me.TabControl1.TabIndex = 51
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cboItemStatus)
        Me.TabPage1.Controls.Add(Me.txtAdjust_Qty)
        Me.TabPage1.Controls.Add(Me.btnSave)
        Me.TabPage1.Controls.Add(Me.lblLocation)
        Me.TabPage1.Controls.Add(Me.txtAdjust_Unit)
        Me.TabPage1.Controls.Add(Me.lblAdjust_Unit)
        Me.TabPage1.Controls.Add(Me.lblItemStatus)
        Me.TabPage1.Controls.Add(Me.txtLocation)
        Me.TabPage1.Controls.Add(Me.lblModel)
        Me.TabPage1.Controls.Add(Me.txtModel)
        Me.TabPage1.Controls.Add(Me.lblBarcode)
        Me.TabPage1.Controls.Add(Me.btnBack)
        Me.TabPage1.Controls.Add(Me.txtBarcode)
        Me.TabPage1.Controls.Add(Me.txtSku_Name)
        Me.TabPage1.Controls.Add(Me.lblName)
        Me.TabPage1.Controls.Add(Me.lblAdjust_Qty)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(400, 290)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "นับสินค้า"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.Location = New System.Drawing.Point(82, 134)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(234, 21)
        Me.cboItemStatus.TabIndex = 51
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgAdjust_Detail)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(400, 290)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "รายละเอียด/สินค้าตำแหน่ง"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgAdjust_Detail
        '
        Me.dgAdjust_Detail.AllowUserToAddRows = False
        Me.dgAdjust_Detail.AllowUserToDeleteRows = False
        Me.dgAdjust_Detail.AllowUserToResizeRows = False
        Me.dgAdjust_Detail.BackgroundColor = System.Drawing.Color.White
        Me.dgAdjust_Detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAdjust_Detail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_STATE, Me.col_Location_Alias, Me.col_Sku_Id, Me.col_ItemStatus_Id})
        Me.dgAdjust_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgAdjust_Detail.Location = New System.Drawing.Point(0, 0)
        Me.dgAdjust_Detail.Name = "dgAdjust_Detail"
        Me.dgAdjust_Detail.ReadOnly = True
        Me.dgAdjust_Detail.RowHeadersVisible = False
        Me.dgAdjust_Detail.Size = New System.Drawing.Size(400, 290)
        Me.dgAdjust_Detail.TabIndex = 40
        '
        'col_STATE
        '
        Me.col_STATE.DataPropertyName = "STATE"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Yellow
        Me.col_STATE.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_STATE.HeaderText = "ตรวจนับ"
        Me.col_STATE.Name = "col_STATE"
        Me.col_STATE.ReadOnly = True
        '
        'col_Location_Alias
        '
        Me.col_Location_Alias.DataPropertyName = "Location_Alias"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Location_Alias.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_Location_Alias.HeaderText = "ตำแหน่ง"
        Me.col_Location_Alias.Name = "col_Location_Alias"
        Me.col_Location_Alias.ReadOnly = True
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Sku_Id.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.ReadOnly = True
        '
        'col_ItemStatus_Id
        '
        Me.col_ItemStatus_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_ItemStatus_Id.DataPropertyName = "ItemStatus_Id"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_ItemStatus_Id.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_ItemStatus_Id.HeaderText = "สถานะ"
        Me.col_ItemStatus_Id.Name = "col_ItemStatus_Id"
        Me.col_ItemStatus_Id.ReadOnly = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgAdjust_DetailLocation)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(400, 290)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "รายละเอียด/ตำแหน่ง"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgAdjust_DetailLocation
        '
        Me.dgAdjust_DetailLocation.AllowUserToAddRows = False
        Me.dgAdjust_DetailLocation.AllowUserToDeleteRows = False
        Me.dgAdjust_DetailLocation.AllowUserToResizeRows = False
        Me.dgAdjust_DetailLocation.BackgroundColor = System.Drawing.Color.White
        Me.dgAdjust_DetailLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAdjust_DetailLocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ตรวจนับ, Me.Location_Alias})
        Me.dgAdjust_DetailLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgAdjust_DetailLocation.Location = New System.Drawing.Point(3, 3)
        Me.dgAdjust_DetailLocation.Name = "dgAdjust_DetailLocation"
        Me.dgAdjust_DetailLocation.ReadOnly = True
        Me.dgAdjust_DetailLocation.RowHeadersVisible = False
        Me.dgAdjust_DetailLocation.Size = New System.Drawing.Size(394, 284)
        Me.dgAdjust_DetailLocation.TabIndex = 41
        '
        'ตรวจนับ
        '
        Me.ตรวจนับ.DataPropertyName = "STATE"
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Yellow
        Me.ตรวจนับ.DefaultCellStyle = DataGridViewCellStyle5
        Me.ตรวจนับ.HeaderText = "STATE"
        Me.ตรวจนับ.Name = "ตรวจนับ"
        Me.ตรวจนับ.ReadOnly = True
        '
        'Location_Alias
        '
        Me.Location_Alias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Location_Alias.DataPropertyName = "Location_Alias"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Location_Alias.DefaultCellStyle = DataGridViewCellStyle6
        Me.Location_Alias.HeaderText = "ตำแหน่ง"
        Me.Location_Alias.Name = "Location_Alias"
        Me.Location_Alias.ReadOnly = True
        '
        'lblAdjust_Date
        '
        Me.lblAdjust_Date.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdjust_Date.Location = New System.Drawing.Point(28, 49)
        Me.lblAdjust_Date.Name = "lblAdjust_Date"
        Me.lblAdjust_Date.Size = New System.Drawing.Size(85, 17)
        Me.lblAdjust_Date.TabIndex = 60
        Me.lblAdjust_Date.Text = "วันที่เอกสาร"
        '
        'txtAdjust_Date
        '
        Me.txtAdjust_Date.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtAdjust_Date.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAdjust_Date.Location = New System.Drawing.Point(119, 46)
        Me.txtAdjust_Date.Name = "txtAdjust_Date"
        Me.txtAdjust_Date.ReadOnly = True
        Me.txtAdjust_Date.Size = New System.Drawing.Size(189, 24)
        Me.txtAdjust_Date.TabIndex = 57
        '
        'txtDocumentType
        '
        Me.txtDocumentType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDocumentType.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtDocumentType.Location = New System.Drawing.Point(119, 76)
        Me.txtDocumentType.Name = "txtDocumentType"
        Me.txtDocumentType.ReadOnly = True
        Me.txtDocumentType.Size = New System.Drawing.Size(189, 24)
        Me.txtDocumentType.TabIndex = 58
        '
        'lblDocumentType
        '
        Me.lblDocumentType.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDocumentType.Location = New System.Drawing.Point(6, 76)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(107, 20)
        Me.lblDocumentType.TabIndex = 61
        Me.lblDocumentType.Text = "ประเภทเอกสาร"
        '
        'lblAdjust_No
        '
        Me.lblAdjust_No.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdjust_No.Location = New System.Drawing.Point(22, 16)
        Me.lblAdjust_No.Name = "lblAdjust_No"
        Me.lblAdjust_No.Size = New System.Drawing.Size(91, 20)
        Me.lblAdjust_No.TabIndex = 64
        Me.lblAdjust_No.Text = "เลขที่เอกสาร"
        '
        'txtAdjust_No
        '
        Me.txtAdjust_No.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtAdjust_No.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAdjust_No.Location = New System.Drawing.Point(119, 16)
        Me.txtAdjust_No.Name = "txtAdjust_No"
        Me.txtAdjust_No.ReadOnly = True
        Me.txtAdjust_No.Size = New System.Drawing.Size(189, 24)
        Me.txtAdjust_No.TabIndex = 65
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblAdjust_No)
        Me.GroupBox1.Controls.Add(Me.txtAdjust_No)
        Me.GroupBox1.Controls.Add(Me.lblDocumentType)
        Me.GroupBox1.Controls.Add(Me.lblAdjust_Date)
        Me.GroupBox1.Controls.Add(Me.txtAdjust_Date)
        Me.GroupBox1.Controls.Add(Me.txtDocumentType)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(404, 121)
        Me.GroupBox1.TabIndex = 66
        Me.GroupBox1.TabStop = False
        '
        'frmAdjust_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(422, 451)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.KeyPreview = True
        Me.Name = "frmAdjust_Stock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "บันทึกการตรวจนับ"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgAdjust_Detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgAdjust_DetailLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtAdjust_Unit As System.Windows.Forms.TextBox
    Friend WithEvents lblAdjust_Unit As System.Windows.Forms.Label
    Friend WithEvents txtAdjust_Qty As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtSku_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents lblModel As System.Windows.Forms.Label
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblItemStatus As System.Windows.Forms.Label
    Friend WithEvents lblAdjust_Qty As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dgAdjust_Detail As System.Windows.Forms.DataGridView
    Friend WithEvents lblAdjust_Date As System.Windows.Forms.Label
    Friend WithEvents txtAdjust_Date As System.Windows.Forms.TextBox
    Friend WithEvents txtDocumentType As System.Windows.Forms.TextBox
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents lblAdjust_No As System.Windows.Forms.Label
    Friend WithEvents txtAdjust_No As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents dgAdjust_DetailLocation As System.Windows.Forms.DataGridView
    Friend WithEvents col_STATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Location_Alias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ตรวจนับ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Location_Alias As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
