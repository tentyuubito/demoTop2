<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTAG_Repeat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintTAG_Repeat))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDocTrans_Process = New System.Windows.Forms.TextBox
        Me.txtPlot = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSku_Id = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.cmbTag_Process = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtTAG_No = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grbTag = New System.Windows.Forms.GroupBox
        Me.ChkAllTag = New System.Windows.Forms.CheckBox
        Me.grdTag = New System.Windows.Forms.DataGridView
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TAG_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SeqTag = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_BtnAdd = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_TAG_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Location_Alias = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_sku_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_str1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Plot = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_weight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_volume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SKU_Location_AliasT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Order_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Ref_2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_OrderStatus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Tag_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_By = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_TAG_Status_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Mfg_Date = New WMS_STD_Master.CalendarColumn
        Me.Col_Exp_Date = New WMS_STD_Master.CalendarColumn
        Me.col_Document = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnPrint = New System.Windows.Forms.Button
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.grbTag.SuspendLayout()
        CType(Me.grdTag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtDocTrans_Process)
        Me.GroupBox1.Controls.Add(Me.txtPlot)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtSku_Id)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtLocation)
        Me.GroupBox1.Controls.Add(Me.cmbTag_Process)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.txtTAG_No)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(933, 82)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เงือนไขการค้นหา"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(542, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "เลขที่เอกสาร"
        '
        'txtDocTrans_Process
        '
        Me.txtDocTrans_Process.Location = New System.Drawing.Point(614, 43)
        Me.txtDocTrans_Process.Name = "txtDocTrans_Process"
        Me.txtDocTrans_Process.Size = New System.Drawing.Size(137, 20)
        Me.txtDocTrans_Process.TabIndex = 24
        '
        'txtPlot
        '
        Me.txtPlot.Location = New System.Drawing.Point(400, 43)
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.Size = New System.Drawing.Size(136, 20)
        Me.txtPlot.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(344, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Lot/Batch"
        '
        'txtSku_Id
        '
        Me.txtSku_Id.Location = New System.Drawing.Point(111, 43)
        Me.txtSku_Id.Name = "txtSku_Id"
        Me.txtSku_Id.Size = New System.Drawing.Size(171, 20)
        Me.txtSku_Id.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(55, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "รหัสสินค้า"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(400, 17)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(136, 20)
        Me.txtLocation.TabIndex = 19
        '
        'cmbTag_Process
        '
        Me.cmbTag_Process.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTag_Process.FormattingEnabled = True
        Me.cmbTag_Process.Location = New System.Drawing.Point(658, 16)
        Me.cmbTag_Process.Name = "cmbTag_Process"
        Me.cmbTag_Process.Size = New System.Drawing.Size(86, 21)
        Me.cmbTag_Process.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(604, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "โอน-รับ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(308, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "ตำแหน่งที่จัดเก็บ"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(808, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(104, 42)
        Me.btnSearch.TabIndex = 14
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtTAG_No
        '
        Me.txtTAG_No.Location = New System.Drawing.Point(111, 17)
        Me.txtTAG_No.Name = "txtTAG_No"
        Me.txtTAG_No.Size = New System.Drawing.Size(171, 20)
        Me.txtTAG_No.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "เลขกำกับ"
        '
        'grbTag
        '
        Me.grbTag.Controls.Add(Me.ChkAllTag)
        Me.grbTag.Controls.Add(Me.grdTag)
        Me.grbTag.Location = New System.Drawing.Point(12, 100)
        Me.grbTag.Name = "grbTag"
        Me.grbTag.Size = New System.Drawing.Size(929, 359)
        Me.grbTag.TabIndex = 3
        Me.grbTag.TabStop = False
        Me.grbTag.Text = "รายการ TAG"
        '
        'ChkAllTag
        '
        Me.ChkAllTag.AutoSize = True
        Me.ChkAllTag.Location = New System.Drawing.Point(9, 20)
        Me.ChkAllTag.Name = "ChkAllTag"
        Me.ChkAllTag.Size = New System.Drawing.Size(15, 14)
        Me.ChkAllTag.TabIndex = 0
        Me.ChkAllTag.UseVisualStyleBackColor = True
        '
        'grdTag
        '
        Me.grdTag.AllowUserToAddRows = False
        Me.grdTag.AllowUserToDeleteRows = False
        Me.grdTag.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTag.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTag.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect, Me.col_TAG_Index, Me.Col_SeqTag, Me.DataGridViewTextBoxColumn27, Me.Col_BtnAdd, Me.col_TAG_NO, Me.col_Location_Alias, Me.col_sku_ID, Me.col_str1, Me.Col_Plot, Me.col_QTY, Me.col_weight, Me.col_volume, Me.col_Status, Me.col_SKU_Location_AliasT, Me.col_Order_Index, Me.Col_Ref_2, Me.col_OrderStatus, Me.col_Tag_Status, Me.col_Add_By, Me.col_Add_Date, Me.Col_TAG_Status_Index, Me.Col_Mfg_Date, Me.Col_Exp_Date, Me.col_Document})
        Me.grdTag.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTag.Location = New System.Drawing.Point(3, 16)
        Me.grdTag.MultiSelect = False
        Me.grdTag.Name = "grdTag"
        Me.grdTag.RowHeadersVisible = False
        Me.grdTag.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTag.Size = New System.Drawing.Size(923, 340)
        Me.grdTag.TabIndex = 1
        '
        'chkSelect
        '
        Me.chkSelect.DataPropertyName = "chkSelect"
        Me.chkSelect.Frozen = True
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Width = 25
        '
        'col_TAG_Index
        '
        Me.col_TAG_Index.DataPropertyName = "TAG_Index"
        Me.col_TAG_Index.HeaderText = "TAG_Index"
        Me.col_TAG_Index.Name = "col_TAG_Index"
        Me.col_TAG_Index.ReadOnly = True
        Me.col_TAG_Index.Visible = False
        '
        'Col_SeqTag
        '
        Me.Col_SeqTag.DataPropertyName = "Row"
        Me.Col_SeqTag.HeaderText = "ลำดับ"
        Me.Col_SeqTag.Name = "Col_SeqTag"
        Me.Col_SeqTag.Width = 40
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Zone_Des"
        Me.DataGridViewTextBoxColumn27.HeaderText = "Zone"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Visible = False
        Me.DataGridViewTextBoxColumn27.Width = 40
        '
        'Col_BtnAdd
        '
        Me.Col_BtnAdd.HeaderText = "เพิ่ม"
        Me.Col_BtnAdd.Name = "Col_BtnAdd"
        Me.Col_BtnAdd.Visible = False
        Me.Col_BtnAdd.Width = 40
        '
        'col_TAG_NO
        '
        Me.col_TAG_NO.DataPropertyName = "TAG_No"
        Me.col_TAG_NO.HeaderText = "เลขกำกับ"
        Me.col_TAG_NO.Name = "col_TAG_NO"
        Me.col_TAG_NO.ReadOnly = True
        '
        'col_Location_Alias
        '
        Me.col_Location_Alias.DataPropertyName = "Location_Alias"
        Me.col_Location_Alias.HeaderText = "ตำแหน่งที่จัดเก็บ"
        Me.col_Location_Alias.Name = "col_Location_Alias"
        Me.col_Location_Alias.ReadOnly = True
        Me.col_Location_Alias.Width = 120
        '
        'col_sku_ID
        '
        Me.col_sku_ID.DataPropertyName = "sku_ID"
        Me.col_sku_ID.HeaderText = "รหัสสินค้า"
        Me.col_sku_ID.Name = "col_sku_ID"
        Me.col_sku_ID.ReadOnly = True
        Me.col_sku_ID.Width = 110
        '
        'col_str1
        '
        Me.col_str1.DataPropertyName = "str1"
        Me.col_str1.HeaderText = "ชื่อสินค้า"
        Me.col_str1.Name = "col_str1"
        Me.col_str1.ReadOnly = True
        Me.col_str1.Width = 140
        '
        'Col_Plot
        '
        Me.Col_Plot.DataPropertyName = "Plot"
        Me.Col_Plot.HeaderText = "Lot / Batch"
        Me.Col_Plot.Name = "Col_Plot"
        Me.Col_Plot.ReadOnly = True
        Me.Col_Plot.Width = 120
        '
        'col_QTY
        '
        Me.col_QTY.DataPropertyName = "Qty_per_TAG"
        Me.col_QTY.HeaderText = "จำนวน"
        Me.col_QTY.Name = "col_QTY"
        Me.col_QTY.ReadOnly = True
        Me.col_QTY.Visible = False
        '
        'col_weight
        '
        Me.col_weight.DataPropertyName = "Weight_per_TAG"
        Me.col_weight.HeaderText = "น้ำหนัก"
        Me.col_weight.Name = "col_weight"
        Me.col_weight.ReadOnly = True
        Me.col_weight.Visible = False
        '
        'col_volume
        '
        Me.col_volume.DataPropertyName = "Volume_per_TAG"
        Me.col_volume.HeaderText = "ปริมาตร"
        Me.col_volume.Name = "col_volume"
        Me.col_volume.ReadOnly = True
        Me.col_volume.Visible = False
        '
        'col_Status
        '
        Me.col_Status.DataPropertyName = "ProcessStatus"
        Me.col_Status.HeaderText = "สถานะ"
        Me.col_Status.Name = "col_Status"
        Me.col_Status.ReadOnly = True
        '
        'col_SKU_Location_AliasT
        '
        Me.col_SKU_Location_AliasT.DataPropertyName = "SKU_Location_Alias"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_SKU_Location_AliasT.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_SKU_Location_AliasT.HeaderText = "ตำแหน่งระบุ"
        Me.col_SKU_Location_AliasT.Name = "col_SKU_Location_AliasT"
        Me.col_SKU_Location_AliasT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_SKU_Location_AliasT.Visible = False
        '
        'col_Order_Index
        '
        Me.col_Order_Index.DataPropertyName = "Order_Index"
        Me.col_Order_Index.HeaderText = "Order_Index"
        Me.col_Order_Index.Name = "col_Order_Index"
        Me.col_Order_Index.ReadOnly = True
        Me.col_Order_Index.Visible = False
        '
        'Col_Ref_2
        '
        Me.Col_Ref_2.DataPropertyName = "Ref_No2"
        Me.Col_Ref_2.HeaderText = "เอกสารอ้างอิงที่ 2"
        Me.Col_Ref_2.Name = "Col_Ref_2"
        Me.Col_Ref_2.ReadOnly = True
        Me.Col_Ref_2.Visible = False
        Me.Col_Ref_2.Width = 120
        '
        'col_OrderStatus
        '
        Me.col_OrderStatus.DataPropertyName = "Status"
        Me.col_OrderStatus.HeaderText = "Order Status"
        Me.col_OrderStatus.Name = "col_OrderStatus"
        Me.col_OrderStatus.ReadOnly = True
        Me.col_OrderStatus.Visible = False
        '
        'col_Tag_Status
        '
        Me.col_Tag_Status.DataPropertyName = "TAG_Status"
        Me.col_Tag_Status.HeaderText = "สถานะ Tag"
        Me.col_Tag_Status.Name = "col_Tag_Status"
        Me.col_Tag_Status.Visible = False
        '
        'col_Add_By
        '
        Me.col_Add_By.DataPropertyName = "Add_By"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Add_By.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_Add_By.HeaderText = "ผู้จัดเก็บ"
        Me.col_Add_By.Name = "col_Add_By"
        Me.col_Add_By.ReadOnly = True
        '
        'col_Add_Date
        '
        Me.col_Add_Date.DataPropertyName = "Add_Date"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Add_Date.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Add_Date.HeaderText = "เวลาจัดเก็บ"
        Me.col_Add_Date.Name = "col_Add_Date"
        Me.col_Add_Date.ReadOnly = True
        '
        'Col_TAG_Status_Index
        '
        Me.Col_TAG_Status_Index.DataPropertyName = "TAG_Status"
        Me.Col_TAG_Status_Index.HeaderText = "TAG_Status"
        Me.Col_TAG_Status_Index.Name = "Col_TAG_Status_Index"
        Me.Col_TAG_Status_Index.Visible = False
        '
        'Col_Mfg_Date
        '
        Me.Col_Mfg_Date.DataPropertyName = "Mfg_Date"
        Me.Col_Mfg_Date.HeaderText = "วันผลิต"
        Me.Col_Mfg_Date.Name = "Col_Mfg_Date"
        '
        'Col_Exp_Date
        '
        Me.Col_Exp_Date.DataPropertyName = "Exp_Date"
        Me.Col_Exp_Date.HeaderText = "วันหมดอายุ"
        Me.Col_Exp_Date.Name = "Col_Exp_Date"
        '
        'col_Document
        '
        Me.col_Document.DataPropertyName = "DocTrans_Process"
        Me.col_Document.HeaderText = "Document"
        Me.col_Document.Name = "col_Document"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(202, 14)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(87, 40)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Location = New System.Drawing.Point(6, 25)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(190, 21)
        Me.cboPrint.TabIndex = 20
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboPrint)
        Me.GroupBox2.Controls.Add(Me.btnPrint)
        Me.GroupBox2.Location = New System.Drawing.Point(329, 465)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(295, 61)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "เอกสารกำกับสินค้า"
        '
        'frmPrintTAG_Repeat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 533)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.grbTag)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintTAG_Repeat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "พิมพ์ TAG"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grbTag.ResumeLayout(False)
        Me.grbTag.PerformLayout()
        CType(Me.grdTag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grbTag As System.Windows.Forms.GroupBox
    Friend WithEvents ChkAllTag As System.Windows.Forms.CheckBox
    Friend WithEvents grdTag As System.Windows.Forms.DataGridView
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents txtTAG_No As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTag_Process As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSku_Id As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDocTrans_Process As System.Windows.Forms.TextBox
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TAG_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SeqTag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_BtnAdd As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_TAG_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Location_Alias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_sku_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_str1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Plot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_volume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SKU_Location_AliasT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Order_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Ref_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_OrderStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Tag_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_By As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_TAG_Status_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Mfg_Date As WMS_STD_Master.CalendarColumn
    Friend WithEvents Col_Exp_Date As WMS_STD_Master.CalendarColumn
    Friend WithEvents col_Document As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
End Class
