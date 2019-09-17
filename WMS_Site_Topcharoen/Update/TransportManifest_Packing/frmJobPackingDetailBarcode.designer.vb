<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJobPackingDetailBarcode
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
        Me.dgvDataBarcode = New System.Windows.Forms.DataGridView
        Me.col_seq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtBarcodeBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.cbSizeBox = New System.Windows.Forms.ComboBox
        Me.lblResultDetial = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnRFID = New System.Windows.Forms.Button
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.btnPrintDetail = New System.Windows.Forms.Button
        Me.cmbCarrier = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDetail = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.dgvData = New System.Windows.Forms.DataGridView
        Me.txtDocNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblResult = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.col_Seq1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_BarcodePack = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Box = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_StatusPrint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_AmontPrint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_packSize_index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SalesOrderPacking_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvDataBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvDataBarcode
        '
        Me.dgvDataBarcode.AllowUserToAddRows = False
        Me.dgvDataBarcode.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dgvDataBarcode.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDataBarcode.BackgroundColor = System.Drawing.Color.White
        Me.dgvDataBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDataBarcode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_seq, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.dgvDataBarcode.Location = New System.Drawing.Point(6, 107)
        Me.dgvDataBarcode.Name = "dgvDataBarcode"
        Me.dgvDataBarcode.ReadOnly = True
        Me.dgvDataBarcode.RowHeadersVisible = False
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dgvDataBarcode.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDataBarcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDataBarcode.Size = New System.Drawing.Size(542, 328)
        Me.dgvDataBarcode.TabIndex = 1
        '
        'col_seq
        '
        Me.col_seq.DataPropertyName = "seq"
        Me.col_seq.HeaderText = "ลำดับ"
        Me.col_seq.Name = "col_seq"
        Me.col_seq.ReadOnly = True
        Me.col_seq.Width = 70
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Barcode1"
        Me.DataGridViewTextBoxColumn1.HeaderText = "บาร์โค้ดสินค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 180
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "sku_id"
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "str1"
        Me.DataGridViewTextBoxColumn3.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "qty_pack"
        Me.DataGridViewTextBoxColumn4.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'txtBarcodeBox
        '
        Me.txtBarcodeBox.Font = New System.Drawing.Font("Century Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeBox.Location = New System.Drawing.Point(158, 20)
        Me.txtBarcodeBox.Name = "txtBarcodeBox"
        Me.txtBarcodeBox.ReadOnly = True
        Me.txtBarcodeBox.Size = New System.Drawing.Size(328, 43)
        Me.txtBarcodeBox.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 45)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "บาร์โค้ด"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 29)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "ขนาดกล่อง"
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(458, 441)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 37)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "ปิด"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cbSizeBox
        '
        Me.cbSizeBox.Enabled = False
        Me.cbSizeBox.FormattingEnabled = True
        Me.cbSizeBox.Location = New System.Drawing.Point(159, 66)
        Me.cbSizeBox.Name = "cbSizeBox"
        Me.cbSizeBox.Size = New System.Drawing.Size(91, 24)
        Me.cbSizeBox.TabIndex = 13
        Me.cbSizeBox.Text = "24"
        '
        'lblResultDetial
        '
        Me.lblResultDetial.AutoSize = True
        Me.lblResultDetial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblResultDetial.ForeColor = System.Drawing.Color.Blue
        Me.lblResultDetial.Location = New System.Drawing.Point(341, 433)
        Me.lblResultDetial.Name = "lblResultDetial"
        Me.lblResultDetial.Size = New System.Drawing.Size(0, 16)
        Me.lblResultDetial.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRFID)
        Me.GroupBox2.Controls.Add(Me.chkPreview)
        Me.GroupBox2.Controls.Add(Me.btnPrintDetail)
        Me.GroupBox2.Controls.Add(Me.cmbCarrier)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnDetail)
        Me.GroupBox2.Controls.Add(Me.lblResultDetial)
        Me.GroupBox2.Controls.Add(Me.btnRemove)
        Me.GroupBox2.Controls.Add(Me.cbSizeBox)
        Me.GroupBox2.Controls.Add(Me.btnPrint)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtBarcodeBox)
        Me.GroupBox2.Controls.Add(Me.dgvDataBarcode)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(439, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(551, 525)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "รายละเอียดสินค้าในบาร์โค้ดแพ็ค"
        '
        'btnRFID
        '
        Me.btnRFID.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnRFID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRFID.Location = New System.Drawing.Point(344, 439)
        Me.btnRFID.Name = "btnRFID"
        Me.btnRFID.Size = New System.Drawing.Size(105, 40)
        Me.btnRFID.TabIndex = 21
        Me.btnRFID.Text = "RFID"
        Me.btnRFID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRFID.UseVisualStyleBackColor = True
        '
        'chkPreview
        '
        Me.chkPreview.AutoSize = True
        Me.chkPreview.Location = New System.Drawing.Point(264, 441)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(77, 20)
        Me.chkPreview.TabIndex = 20
        Me.chkPreview.Text = "ดูตัวอย่าง"
        Me.chkPreview.UseVisualStyleBackColor = True
        '
        'btnPrintDetail
        '
        Me.btnPrintDetail.Enabled = False
        Me.btnPrintDetail.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.btnPrintDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintDetail.Location = New System.Drawing.Point(238, 482)
        Me.btnPrintDetail.Name = "btnPrintDetail"
        Me.btnPrintDetail.Size = New System.Drawing.Size(140, 40)
        Me.btnPrintDetail.TabIndex = 17
        Me.btnPrintDetail.Text = "รายการแพ็คสินค้า"
        Me.btnPrintDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrintDetail.UseVisualStyleBackColor = True
        Me.btnPrintDetail.Visible = False
        '
        'cmbCarrier
        '
        Me.cmbCarrier.FormattingEnabled = True
        Me.cmbCarrier.Location = New System.Drawing.Point(51, 491)
        Me.cmbCarrier.Name = "cmbCarrier"
        Me.cmbCarrier.Size = New System.Drawing.Size(392, 24)
        Me.cmbCarrier.TabIndex = 19
        Me.cmbCarrier.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 494)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 16)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "สาย"
        Me.Label2.Visible = False
        '
        'btnDetail
        '
        Me.btnDetail.Location = New System.Drawing.Point(529, 482)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(16, 37)
        Me.btnDetail.TabIndex = 17
        Me.btnDetail.Text = "แสดงรายการ"
        Me.btnDetail.UseVisualStyleBackColor = True
        Me.btnDetail.Visible = False
        '
        'btnRemove
        '
        Me.btnRemove.Enabled = False
        Me.btnRemove.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemove.Location = New System.Drawing.Point(121, 440)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(133, 40)
        Me.btnRemove.TabIndex = 16
        Me.btnRemove.Text = "ลบบาร์โค้ดแพ็ค"
        Me.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.print
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(10, 440)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(105, 40)
        Me.btnPrint.TabIndex = 15
        Me.btnPrint.Text = "สติ๊กเกอร์"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToResizeRows = False
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dgvData.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvData.BackgroundColor = System.Drawing.Color.White
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Seq1, Me.col_BarcodePack, Me.col_Box, Me.col_StatusPrint, Me.col_AmontPrint, Me.col_packSize_index, Me.col_SalesOrderPacking_Index})
        Me.dgvData.Location = New System.Drawing.Point(5, 52)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dgvData.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.Size = New System.Drawing.Size(413, 447)
        Me.dgvData.TabIndex = 1
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.Color.White
        Me.txtDocNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtDocNo.Location = New System.Drawing.Point(106, 19)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(219, 22)
        Me.txtDocNo.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "เลขที่ใบสั่งขาย"
        '
        'lblResult
        '
        Me.lblResult.AutoSize = True
        Me.lblResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblResult.ForeColor = System.Drawing.Color.Blue
        Me.lblResult.Location = New System.Drawing.Point(170, 502)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(79, 16)
        Me.lblResult.TabIndex = 15
        Me.lblResult.Text = "ไม่พบรายการ"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblResult)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDocNo)
        Me.GroupBox1.Controls.Add(Me.dgvData)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 525)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "รายการบาร์โค้ดแพ็คสินค้า"
        '
        'col_Seq1
        '
        Me.col_Seq1.DataPropertyName = "Seq"
        Me.col_Seq1.HeaderText = "ลำดับ"
        Me.col_Seq1.Name = "col_Seq1"
        Me.col_Seq1.ReadOnly = True
        Me.col_Seq1.Width = 50
        '
        'col_BarcodePack
        '
        Me.col_BarcodePack.DataPropertyName = "BarcodePacking"
        Me.col_BarcodePack.HeaderText = "บาร์โค้ดแพ็ค"
        Me.col_BarcodePack.Name = "col_BarcodePack"
        Me.col_BarcodePack.ReadOnly = True
        Me.col_BarcodePack.Width = 130
        '
        'col_Box
        '
        Me.col_Box.DataPropertyName = "Description"
        Me.col_Box.HeaderText = "กล่อง"
        Me.col_Box.Name = "col_Box"
        Me.col_Box.ReadOnly = True
        '
        'col_StatusPrint
        '
        Me.col_StatusPrint.DataPropertyName = "strStatus"
        Me.col_StatusPrint.HeaderText = "สถานะพิมพ์"
        Me.col_StatusPrint.Name = "col_StatusPrint"
        Me.col_StatusPrint.ReadOnly = True
        '
        'col_AmontPrint
        '
        Me.col_AmontPrint.DataPropertyName = "count_Print"
        Me.col_AmontPrint.HeaderText = "ครั้งที่พิมพ์"
        Me.col_AmontPrint.Name = "col_AmontPrint"
        Me.col_AmontPrint.ReadOnly = True
        '
        'col_packSize_index
        '
        Me.col_packSize_index.DataPropertyName = "packSize_index"
        Me.col_packSize_index.HeaderText = "packSize_index"
        Me.col_packSize_index.Name = "col_packSize_index"
        Me.col_packSize_index.ReadOnly = True
        Me.col_packSize_index.Visible = False
        '
        'col_SalesOrderPacking_Index
        '
        Me.col_SalesOrderPacking_Index.DataPropertyName = "SalesOrderPacking_Index"
        Me.col_SalesOrderPacking_Index.HeaderText = "SalesOrderPacking_Index"
        Me.col_SalesOrderPacking_Index.Name = "col_SalesOrderPacking_Index"
        Me.col_SalesOrderPacking_Index.ReadOnly = True
        Me.col_SalesOrderPacking_Index.Visible = False
        '
        'frmJobPackingDetailBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 536)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmJobPackingDetailBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รายการแพ็คสินค้าในกล่อง"
        CType(Me.dgvDataBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvDataBarcode As System.Windows.Forms.DataGridView
    Friend WithEvents txtBarcodeBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cbSizeBox As System.Windows.Forms.ComboBox
    Friend WithEvents lblResultDetial As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents col_seq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnPrintDetail As System.Windows.Forms.Button
    Friend WithEvents cmbCarrier As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents btnRFID As System.Windows.Forms.Button
    Friend WithEvents col_Seq1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_BarcodePack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Box As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_StatusPrint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AmontPrint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_packSize_index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SalesOrderPacking_Index As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
