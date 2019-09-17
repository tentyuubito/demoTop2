<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWithdrawAsset_TAG
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWithdrawAsset_TAG))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdNonTAG = New System.Windows.Forms.DataGridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdTAG = New System.Windows.Forms.DataGridView
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnCreate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblWithDraw_no = New System.Windows.Forms.Label
        Me.txtWithdraw_No = New System.Windows.Forms.TextBox
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.btnPrint_TAG = New System.Windows.Forms.Button
        Me.btnAuto = New System.Windows.Forms.Button
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PRQ = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Plot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TagOut_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdNonTAG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdTAG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.grdNonTAG)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 247)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "รายการที่ยังไม่สร้าง"
        '
        'grdNonTAG
        '
        Me.grdNonTAG.AllowUserToAddRows = False
        Me.grdNonTAG.AllowUserToDeleteRows = False
        Me.grdNonTAG.AllowUserToResizeRows = False
        Me.grdNonTAG.BackgroundColor = System.Drawing.Color.White
        Me.grdNonTAG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdNonTAG.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_PRQ, Me.col_Plot, Me.col_Sku_Id, Me.col_Sku_Name, Me.col_Total_Qty, Me.col_UOM})
        Me.grdNonTAG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdNonTAG.Location = New System.Drawing.Point(3, 16)
        Me.grdNonTAG.Name = "grdNonTAG"
        Me.grdNonTAG.ReadOnly = True
        Me.grdNonTAG.Size = New System.Drawing.Size(754, 228)
        Me.grdNonTAG.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkSelectAll)
        Me.GroupBox2.Controls.Add(Me.grdTAG)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 341)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(760, 260)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "รายการที่ยังสร้างแล้ว"
        '
        'grdTAG
        '
        Me.grdTAG.AllowUserToAddRows = False
        Me.grdTAG.AllowUserToDeleteRows = False
        Me.grdTAG.AllowUserToResizeRows = False
        Me.grdTAG.BackgroundColor = System.Drawing.Color.White
        Me.grdTAG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTAG.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect, Me.col_TagOut_No, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12})
        Me.grdTAG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTAG.Location = New System.Drawing.Point(3, 16)
        Me.grdTAG.Name = "grdTAG"
        Me.grdTAG.RowHeadersWidth = 30
        Me.grdTAG.Size = New System.Drawing.Size(754, 241)
        Me.grdTAG.TabIndex = 1
        '
        'btnRemove
        '
        Me.btnRemove.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemove.Location = New System.Drawing.Point(183, 301)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(91, 34)
        Me.btnRemove.TabIndex = 7
        Me.btnRemove.Text = "ลบ  M"
        Me.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCreate.Location = New System.Drawing.Point(88, 301)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(91, 34)
        Me.btnCreate.TabIndex = 6
        Me.btnCreate.Text = "สร้าง 1 : 1"
        Me.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(665, 608)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblWithDraw_no
        '
        Me.lblWithDraw_no.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblWithDraw_no.Location = New System.Drawing.Point(15, 16)
        Me.lblWithDraw_no.Name = "lblWithDraw_no"
        Me.lblWithDraw_no.Size = New System.Drawing.Size(79, 13)
        Me.lblWithDraw_no.TabIndex = 9
        Me.lblWithDraw_no.Text = "เลขที่เอกสาร"
        Me.lblWithDraw_no.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWithdraw_No
        '
        Me.txtWithdraw_No.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtWithdraw_No.ForeColor = System.Drawing.Color.White
        Me.txtWithdraw_No.Location = New System.Drawing.Point(100, 12)
        Me.txtWithdraw_No.Name = "txtWithdraw_No"
        Me.txtWithdraw_No.ReadOnly = True
        Me.txtWithdraw_No.Size = New System.Drawing.Size(133, 20)
        Me.txtWithdraw_No.TabIndex = 10
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(211, 16)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(87, 13)
        Me.lblCustomer.TabIndex = 12
        Me.lblCustomer.Text = "ลูกค้า"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Id.Location = New System.Drawing.Point(304, 12)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(81, 20)
        Me.txtCustomer_Id.TabIndex = 13
        Me.txtCustomer_Id.TabStop = False
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Name.Location = New System.Drawing.Point(391, 12)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(378, 20)
        Me.txtCustomer_Name.TabIndex = 14
        Me.txtCustomer_Name.TabStop = False
        '
        'btnPrint_TAG
        '
        Me.btnPrint_TAG.Image = CType(resources.GetObject("btnPrint_TAG.Image"), System.Drawing.Image)
        Me.btnPrint_TAG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint_TAG.Location = New System.Drawing.Point(22, 608)
        Me.btnPrint_TAG.Name = "btnPrint_TAG"
        Me.btnPrint_TAG.Size = New System.Drawing.Size(100, 38)
        Me.btnPrint_TAG.TabIndex = 17
        Me.btnPrint_TAG.Text = "พิมพ์ TAG"
        Me.btnPrint_TAG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint_TAG.UseVisualStyleBackColor = True
        '
        'btnAuto
        '
        Me.btnAuto.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แนะนำตำแหน่งจัดเก็บ
        Me.btnAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAuto.Location = New System.Drawing.Point(15, 301)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(67, 34)
        Me.btnAuto.TabIndex = 18
        Me.btnAuto.Text = "Auto"
        Me.btnAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(39, 21)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 9
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "PRQ"
        Me.DataGridViewTextBoxColumn1.HeaderText = "เอกสารตั้งต้น"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Lot"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ล็อต"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "UOM"
        Me.DataGridViewTextBoxColumn6.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "chkSelect"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "0"
        DataGridViewCellStyle3.NullValue = False
        Me.DataGridViewCheckBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.IndeterminateValue = "0"
        Me.DataGridViewCheckBoxColumn1.MinimumWidth = 25
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "UOM"
        Me.DataGridViewTextBoxColumn13.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 200
        '
        'col_PRQ
        '
        Me.col_PRQ.DataPropertyName = "PRQ"
        Me.col_PRQ.HeaderText = "เอกสารตั้งต้น"
        Me.col_PRQ.Name = "col_PRQ"
        Me.col_PRQ.ReadOnly = True
        '
        'col_Plot
        '
        Me.col_Plot.DataPropertyName = "Lot"
        Me.col_Plot.HeaderText = "ล็อต"
        Me.col_Plot.Name = "col_Plot"
        Me.col_Plot.ReadOnly = True
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.ReadOnly = True
        Me.col_Sku_Id.Width = 120
        '
        'col_Sku_Name
        '
        Me.col_Sku_Name.DataPropertyName = "Name"
        Me.col_Sku_Name.HeaderText = "ชื่อสินค้า"
        Me.col_Sku_Name.Name = "col_Sku_Name"
        Me.col_Sku_Name.ReadOnly = True
        Me.col_Sku_Name.Width = 200
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        Me.col_Total_Qty.HeaderText = "จำนวน"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        '
        'col_UOM
        '
        Me.col_UOM.DataPropertyName = "UOM"
        Me.col_UOM.HeaderText = "หน่วย"
        Me.col_UOM.Name = "col_UOM"
        Me.col_UOM.ReadOnly = True
        '
        'chkSelect
        '
        Me.chkSelect.DataPropertyName = "chkSelect"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Format = "0"
        DataGridViewCellStyle1.NullValue = False
        Me.chkSelect.DefaultCellStyle = DataGridViewCellStyle1
        Me.chkSelect.FalseValue = "0"
        Me.chkSelect.Frozen = True
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.IndeterminateValue = ""
        Me.chkSelect.MinimumWidth = 25
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.TrueValue = "1"
        Me.chkSelect.Width = 25
        '
        'col_TagOut_No
        '
        Me.col_TagOut_No.DataPropertyName = "TagOut_No"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Yellow
        Me.col_TagOut_No.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_TagOut_No.Frozen = True
        Me.col_TagOut_No.HeaderText = "TAG No"
        Me.col_TagOut_No.Name = "col_TagOut_No"
        Me.col_TagOut_No.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "PRQ"
        Me.DataGridViewTextBoxColumn7.HeaderText = "เอกสารตั้งต้น"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Lot"
        Me.DataGridViewTextBoxColumn8.HeaderText = "ล็อต"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn9.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 200
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn11.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 200
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "UOM"
        Me.DataGridViewTextBoxColumn12.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 200
        '
        'frmWithdrawAsset_TAG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 657)
        Me.Controls.Add(Me.btnAuto)
        Me.Controls.Add(Me.btnPrint_TAG)
        Me.Controls.Add(Me.txtCustomer_Id)
        Me.Controls.Add(Me.txtCustomer_Name)
        Me.Controls.Add(Me.lblWithDraw_no)
        Me.Controls.Add(Me.txtWithdraw_No)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblCustomer)
        Me.Name = "frmWithdrawAsset_TAG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "จัดการ TAG ขาจ่ายสินค้า"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdNonTAG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdTAG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblWithDraw_no As System.Windows.Forms.Label
    Friend WithEvents txtWithdraw_No As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents grdNonTAG As System.Windows.Forms.DataGridView
    Friend WithEvents col_PRQ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Plot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdTAG As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnPrint_TAG As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAuto As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TagOut_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
