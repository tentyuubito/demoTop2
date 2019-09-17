<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopupScanBarcodePacking
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
        Me.lblReservedLocation = New System.Windows.Forms.Label
        Me.txtShipping_Location_ID = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Col_Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_ProductType_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnFrmClose = New System.Windows.Forms.Button
        Me.txtSKU_ID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Invoice_No"
        Me.DataGridViewTextBoxColumn1.HeaderText = "เลขที่อินวอยส์"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SalesOrder_No"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ใบสั่งขาย"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Qty_Pack"
        Me.DataGridViewTextBoxColumn6.HeaderText = "แพ็คแล้ว"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn7.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 60
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "SalesOrder_Index"
        Me.DataGridViewTextBoxColumn8.HeaderText = "SalesOrder_Index"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Sku_Index"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "TransportManifestItem_Index"
        Me.DataGridViewTextBoxColumn10.HeaderText = "TransportManifestItem_Index"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "ibarcode"
        Me.DataGridViewTextBoxColumn11.HeaderText = "บาร์โค้ดสินค้า"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Width = 120
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Sku_Index"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Sku_Index_Bx"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "SalesOrderPackingItem_Index"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "qty_pack"
        Me.DataGridViewTextBoxColumn14.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 80
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "sku_id"
        Me.DataGridViewTextBoxColumn15.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "str1"
        Me.DataGridViewTextBoxColumn16.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Width = 150
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn17.HeaderText = "TotalQty"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Qty_Barcode"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Qty_Barcode"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Visible = False
        '
        'lblReservedLocation
        '
        Me.lblReservedLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReservedLocation.Location = New System.Drawing.Point(3, 25)
        Me.lblReservedLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReservedLocation.Name = "lblReservedLocation"
        Me.lblReservedLocation.Size = New System.Drawing.Size(74, 16)
        Me.lblReservedLocation.TabIndex = 11
        Me.lblReservedLocation.Text = "Barcode"
        Me.lblReservedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShipping_Location_ID
        '
        Me.txtShipping_Location_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtShipping_Location_ID.Location = New System.Drawing.Point(85, 25)
        Me.txtShipping_Location_ID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtShipping_Location_ID.Name = "txtShipping_Location_ID"
        Me.txtShipping_Location_ID.ReadOnly = True
        Me.txtShipping_Location_ID.Size = New System.Drawing.Size(294, 22)
        Me.txtShipping_Location_ID.TabIndex = 12
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode, Me.Col_ProductType_Id})
        Me.DataGridView1.Location = New System.Drawing.Point(13, 108)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(219, 127)
        Me.DataGridView1.TabIndex = 15
        '
        'Col_Barcode
        '
        Me.Col_Barcode.DataPropertyName = "Barcode"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Col_Barcode.DefaultCellStyle = DataGridViewCellStyle1
        Me.Col_Barcode.Frozen = True
        Me.Col_Barcode.HeaderText = "Barcode"
        Me.Col_Barcode.Name = "Col_Barcode"
        Me.Col_Barcode.ReadOnly = True
        Me.Col_Barcode.Width = 180
        '
        'Col_ProductType_Id
        '
        Me.Col_ProductType_Id.DataPropertyName = "ProductType_Id"
        Me.Col_ProductType_Id.Frozen = True
        Me.Col_ProductType_Id.HeaderText = "---"
        Me.Col_ProductType_Id.Name = "Col_ProductType_Id"
        Me.Col_ProductType_Id.ReadOnly = True
        Me.Col_ProductType_Id.Width = 120
        '
        'btnFrmClose
        '
        Me.btnFrmClose.Location = New System.Drawing.Point(255, 191)
        Me.btnFrmClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFrmClose.Name = "btnFrmClose"
        Me.btnFrmClose.Size = New System.Drawing.Size(124, 44)
        Me.btnFrmClose.TabIndex = 17
        Me.btnFrmClose.Text = "Close"
        Me.btnFrmClose.UseVisualStyleBackColor = True
        '
        'txtSKU_ID
        '
        Me.txtSKU_ID.Location = New System.Drawing.Point(85, 65)
        Me.txtSKU_ID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSKU_ID.MaxLength = 50
        Me.txtSKU_ID.Name = "txtSKU_ID"
        Me.txtSKU_ID.Size = New System.Drawing.Size(151, 22)
        Me.txtSKU_ID.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 64)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 22)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Qty"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(244, 65)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(135, 22)
        Me.TextBox1.TabIndex = 20
        '
        'frmPopupScanBarcodePacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 250)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSKU_ID)
        Me.Controls.Add(Me.btnFrmClose)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lblReservedLocation)
        Me.Controls.Add(Me.txtShipping_Location_ID)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPopupScanBarcodePacking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงถุง"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblReservedLocation As System.Windows.Forms.Label
    Friend WithEvents txtShipping_Location_ID As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnFrmClose As System.Windows.Forms.Button
    Friend WithEvents txtSKU_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Col_Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ProductType_Id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
