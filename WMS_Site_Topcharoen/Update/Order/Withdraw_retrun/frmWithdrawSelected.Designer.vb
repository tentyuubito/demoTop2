<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWithdrawSelected
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWithdrawSelected))
        Me.dgvWithdraw = New System.Windows.Forms.DataGridView
        Me.col_chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_Withdraw = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Withdraw_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgvWithdrawItem = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnOrderOK = New System.Windows.Forms.Button
        Me.txtWithdraw_No = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.txtTagNo = New System.Windows.Forms.TextBox
        Me.txtBookingNo = New System.Windows.Forms.TextBox
        Me.chkTagNo = New System.Windows.Forms.CheckBox
        Me.chkInvoiceNo = New System.Windows.Forms.CheckBox
        Me.chkBookingNo = New System.Windows.Forms.CheckBox
        Me.chkWithdrawNo = New System.Windows.Forms.CheckBox
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkWithdraw_Date = New System.Windows.Forms.CheckBox
        Me.Col_chkSelect_Item = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_Withdraw_No_Item = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Id_Item = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Package_Item = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Item = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvWithdraw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvWithdrawItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvWithdraw
        '
        Me.dgvWithdraw.AllowUserToAddRows = False
        Me.dgvWithdraw.AllowUserToDeleteRows = False
        Me.dgvWithdraw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWithdraw.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_chkSelect, Me.Col_Withdraw, Me.Col_Withdraw_Date})
        Me.dgvWithdraw.Location = New System.Drawing.Point(8, 19)
        Me.dgvWithdraw.Name = "dgvWithdraw"
        Me.dgvWithdraw.RowHeadersVisible = False
        Me.dgvWithdraw.Size = New System.Drawing.Size(341, 365)
        Me.dgvWithdraw.TabIndex = 0
        '
        'col_chkSelect
        '
        Me.col_chkSelect.DataPropertyName = "chk"
        Me.col_chkSelect.FalseValue = "false"
        Me.col_chkSelect.HeaderText = ""
        Me.col_chkSelect.IndeterminateValue = "false"
        Me.col_chkSelect.Name = "col_chkSelect"
        Me.col_chkSelect.TrueValue = "true"
        Me.col_chkSelect.Width = 30
        '
        'Col_Withdraw
        '
        Me.Col_Withdraw.DataPropertyName = "Withdraw_No"
        Me.Col_Withdraw.HeaderText = "เลขที่ใบเบิก"
        Me.Col_Withdraw.Name = "Col_Withdraw"
        Me.Col_Withdraw.ReadOnly = True
        Me.Col_Withdraw.Width = 150
        '
        'Col_Withdraw_Date
        '
        Me.Col_Withdraw_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Withdraw_Date.DataPropertyName = "Withdraw_Date"
        Me.Col_Withdraw_Date.HeaderText = "วันที่เบิก"
        Me.Col_Withdraw_Date.Name = "Col_Withdraw_Date"
        Me.Col_Withdraw_Date.ReadOnly = True
        '
        'dgvWithdrawItem
        '
        Me.dgvWithdrawItem.AllowUserToAddRows = False
        Me.dgvWithdrawItem.AllowUserToDeleteRows = False
        Me.dgvWithdrawItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWithdrawItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_chkSelect_Item, Me.Col_Withdraw_No_Item, Me.Col_Sku_Id_Item, Me.Col_Package_Item, Me.Col_Qty_Item})
        Me.dgvWithdrawItem.Location = New System.Drawing.Point(366, 19)
        Me.dgvWithdrawItem.Name = "dgvWithdrawItem"
        Me.dgvWithdrawItem.RowHeadersVisible = False
        Me.dgvWithdrawItem.Size = New System.Drawing.Size(496, 365)
        Me.dgvWithdrawItem.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(768, 529)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOrderOK
        '
        Me.btnOrderOK.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnOrderOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOrderOK.Location = New System.Drawing.Point(13, 529)
        Me.btnOrderOK.Name = "btnOrderOK"
        Me.btnOrderOK.Size = New System.Drawing.Size(106, 38)
        Me.btnOrderOK.TabIndex = 29
        Me.btnOrderOK.Text = "ตกลง"
        Me.btnOrderOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOrderOK.UseVisualStyleBackColor = True
        '
        'txtWithdraw_No
        '
        Me.txtWithdraw_No.Location = New System.Drawing.Point(97, 19)
        Me.txtWithdraw_No.Name = "txtWithdraw_No"
        Me.txtWithdraw_No.Size = New System.Drawing.Size(262, 20)
        Me.txtWithdraw_No.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(754, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 32
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvWithdrawItem)
        Me.GroupBox1.Controls.Add(Me.dgvWithdraw)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(866, 409)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox2.Controls.Add(Me.txtTagNo)
        Me.GroupBox2.Controls.Add(Me.txtBookingNo)
        Me.GroupBox2.Controls.Add(Me.chkTagNo)
        Me.GroupBox2.Controls.Add(Me.chkInvoiceNo)
        Me.GroupBox2.Controls.Add(Me.chkBookingNo)
        Me.GroupBox2.Controls.Add(Me.chkWithdrawNo)
        Me.GroupBox2.Controls.Add(Me.dtpEnd)
        Me.GroupBox2.Controls.Add(Me.dtpStart)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.chkWithdraw_Date)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.txtWithdraw_No)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(865, 103)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "เงื่อนไข"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(466, 45)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(263, 20)
        Me.txtInvoiceNo.TabIndex = 5
        '
        'txtTagNo
        '
        Me.txtTagNo.Location = New System.Drawing.Point(97, 71)
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.Size = New System.Drawing.Size(262, 20)
        Me.txtTagNo.TabIndex = 6
        '
        'txtBookingNo
        '
        Me.txtBookingNo.Location = New System.Drawing.Point(97, 45)
        Me.txtBookingNo.Name = "txtBookingNo"
        Me.txtBookingNo.Size = New System.Drawing.Size(262, 20)
        Me.txtBookingNo.TabIndex = 4
        '
        'chkTagNo
        '
        Me.chkTagNo.AutoSize = True
        Me.chkTagNo.Location = New System.Drawing.Point(6, 74)
        Me.chkTagNo.Name = "chkTagNo"
        Me.chkTagNo.Size = New System.Drawing.Size(65, 17)
        Me.chkTagNo.TabIndex = 44
        Me.chkTagNo.Text = "Tag No."
        Me.chkTagNo.UseVisualStyleBackColor = True
        '
        'chkInvoiceNo
        '
        Me.chkInvoiceNo.AutoSize = True
        Me.chkInvoiceNo.Location = New System.Drawing.Point(379, 47)
        Me.chkInvoiceNo.Name = "chkInvoiceNo"
        Me.chkInvoiceNo.Size = New System.Drawing.Size(81, 17)
        Me.chkInvoiceNo.TabIndex = 43
        Me.chkInvoiceNo.Text = "Invoice No."
        Me.chkInvoiceNo.UseVisualStyleBackColor = True
        '
        'chkBookingNo
        '
        Me.chkBookingNo.AutoSize = True
        Me.chkBookingNo.Location = New System.Drawing.Point(6, 48)
        Me.chkBookingNo.Name = "chkBookingNo"
        Me.chkBookingNo.Size = New System.Drawing.Size(85, 17)
        Me.chkBookingNo.TabIndex = 42
        Me.chkBookingNo.Text = "Booking No."
        Me.chkBookingNo.UseVisualStyleBackColor = True
        '
        'chkWithdrawNo
        '
        Me.chkWithdrawNo.AutoSize = True
        Me.chkWithdrawNo.Location = New System.Drawing.Point(6, 22)
        Me.chkWithdrawNo.Name = "chkWithdrawNo"
        Me.chkWithdrawNo.Size = New System.Drawing.Size(82, 17)
        Me.chkWithdrawNo.TabIndex = 41
        Me.chkWithdrawNo.Text = "เลขที่ใบเบิก"
        Me.chkWithdrawNo.UseVisualStyleBackColor = True
        '
        'dtpEnd
        '
        Me.dtpEnd.CustomFormat = "dd/MM/yyyy"
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEnd.Location = New System.Drawing.Point(613, 19)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(116, 20)
        Me.dtpEnd.TabIndex = 3
        '
        'dtpStart
        '
        Me.dtpStart.CustomFormat = "dd/MM/yyyy"
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart.Location = New System.Drawing.Point(466, 19)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(116, 20)
        Me.dtpStart.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(588, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "ถึง"
        '
        'chkWithdraw_Date
        '
        Me.chkWithdraw_Date.AutoSize = True
        Me.chkWithdraw_Date.Location = New System.Drawing.Point(379, 22)
        Me.chkWithdraw_Date.Name = "chkWithdraw_Date"
        Me.chkWithdraw_Date.Size = New System.Drawing.Size(66, 17)
        Me.chkWithdraw_Date.TabIndex = 33
        Me.chkWithdraw_Date.Text = "วันที่เบิก"
        Me.chkWithdraw_Date.UseVisualStyleBackColor = True
        '
        'Col_chkSelect_Item
        '
        Me.Col_chkSelect_Item.DataPropertyName = "chk"
        Me.Col_chkSelect_Item.FalseValue = "false"
        Me.Col_chkSelect_Item.HeaderText = ""
        Me.Col_chkSelect_Item.IndeterminateValue = "false"
        Me.Col_chkSelect_Item.Name = "Col_chkSelect_Item"
        Me.Col_chkSelect_Item.TrueValue = "true"
        Me.Col_chkSelect_Item.Width = 30
        '
        'Col_Withdraw_No_Item
        '
        Me.Col_Withdraw_No_Item.DataPropertyName = "Withdraw_No"
        Me.Col_Withdraw_No_Item.HeaderText = "เลขที่ใบเบิก"
        Me.Col_Withdraw_No_Item.Name = "Col_Withdraw_No_Item"
        Me.Col_Withdraw_No_Item.ReadOnly = True
        Me.Col_Withdraw_No_Item.Width = 120
        '
        'Col_Sku_Id_Item
        '
        Me.Col_Sku_Id_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Sku_Id_Item.DataPropertyName = "Sku_Id"
        Me.Col_Sku_Id_Item.HeaderText = "รหัสสินค้า"
        Me.Col_Sku_Id_Item.Name = "Col_Sku_Id_Item"
        Me.Col_Sku_Id_Item.ReadOnly = True
        '
        'Col_Package_Item
        '
        Me.Col_Package_Item.DataPropertyName = "Package"
        Me.Col_Package_Item.HeaderText = "หน่วย"
        Me.Col_Package_Item.Name = "Col_Package_Item"
        Me.Col_Package_Item.ReadOnly = True
        Me.Col_Package_Item.Width = 80
        '
        'Col_Qty_Item
        '
        Me.Col_Qty_Item.DataPropertyName = "Qty"
        Me.Col_Qty_Item.HeaderText = "จำนวน"
        Me.Col_Qty_Item.Name = "Col_Qty_Item"
        '
        'frmWithdrawSelected
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 579)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOrderOK)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmWithdrawSelected"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ใบเบิก"
        CType(Me.dgvWithdraw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvWithdrawItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvWithdraw As System.Windows.Forms.DataGridView
    Friend WithEvents dgvWithdrawItem As System.Windows.Forms.DataGridView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnOrderOK As System.Windows.Forms.Button
    Friend WithEvents txtWithdraw_No As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkWithdraw_Date As System.Windows.Forms.CheckBox
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkTagNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkInvoiceNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkBookingNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkWithdrawNo As System.Windows.Forms.CheckBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTagNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBookingNo As System.Windows.Forms.TextBox
    Friend WithEvents col_chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_Withdraw As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Withdraw_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_chkSelect_Item As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_Withdraw_No_Item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Id_Item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Package_Item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Item As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
