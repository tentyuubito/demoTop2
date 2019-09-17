<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSO_CheckStock
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSO_CheckStock))
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_Bal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Total_Qty_Request = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Sku_Id, Me.col_Package, Me.col_Total_Qty, Me.col_Total_Qty_Pending, Me.col_Qty_Bal, Me.col_Total_Qty_Request})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 16)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(663, 232)
        Me.DataGridView1.TabIndex = 0
        '
        'col_Sku_Id
        '
        Me.col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Id.Name = "col_Sku_Id"
        Me.col_Sku_Id.ReadOnly = True
        '
        'col_Package
        '
        Me.col_Package.DataPropertyName = "Package"
        Me.col_Package.HeaderText = "หน่วย"
        Me.col_Package.Name = "col_Package"
        Me.col_Package.ReadOnly = True
        '
        'col_Total_Qty
        '
        Me.col_Total_Qty.DataPropertyName = "Total_Qty"
        Me.col_Total_Qty.HeaderText = "จำนวนที่สั่ง"
        Me.col_Total_Qty.Name = "col_Total_Qty"
        Me.col_Total_Qty.ReadOnly = True
        '
        'col_Total_Qty_Pending
        '
        Me.col_Total_Qty_Pending.DataPropertyName = "Total_Qty_Pending"
        Me.col_Total_Qty_Pending.HeaderText = "จำนวนสั่งขายค้าง"
        Me.col_Total_Qty_Pending.Name = "col_Total_Qty_Pending"
        Me.col_Total_Qty_Pending.ReadOnly = True
        Me.col_Total_Qty_Pending.Width = 120
        '
        'col_Qty_Bal
        '
        Me.col_Qty_Bal.DataPropertyName = "Qty_Bal"
        Me.col_Qty_Bal.HeaderText = "สินค้าคงคลัง"
        Me.col_Qty_Bal.Name = "col_Qty_Bal"
        Me.col_Qty_Bal.ReadOnly = True
        '
        'col_Total_Qty_Request
        '
        Me.col_Total_Qty_Request.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Total_Qty_Request.DataPropertyName = "Total_Qty_Request"
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        Me.col_Total_Qty_Request.DefaultCellStyle = DataGridViewCellStyle7
        Me.col_Total_Qty_Request.HeaderText = "จำนวนคงเหลือ"
        Me.col_Total_Qty_Request.Name = "col_Total_Qty_Request"
        Me.col_Total_Qty_Request.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(669, 251)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "รายการ"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(12, 259)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(100, 37)
        Me.btnExport.TabIndex = 23
        Me.btnExport.Text = "Export"
        Me.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(569, 259)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 38)
        Me.btnExit.TabIndex = 24
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Package"
        Me.DataGridViewTextBoxColumn2.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn3.HeaderText = "จำนวนที่สั่ง"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Total_Qty_Pending"
        Me.DataGridViewTextBoxColumn4.HeaderText = "จำนวนสั่งขายค้าง"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Qty_Bal"
        Me.DataGridViewTextBoxColumn5.HeaderText = "สินค้าคงคลัง"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 120
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Total_Qty_Request"
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn6.HeaderText = "จำนวนที่ขาด"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Total_Qty_Request"
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวนที่ขาด"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'btnConfirm
        '
        Me.btnConfirm.Enabled = False
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(118, 259)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(96, 38)
        Me.btnConfirm.TabIndex = 25
        Me.btnConfirm.Text = "ยืนยันใบสั่งขายนี้"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(220, 260)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 38)
        Me.btnClear.TabIndex = 232
        Me.btnClear.Text = "คืนสถานะ" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "เพื่อแก้ไข"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmSO_CheckStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 308)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSO_CheckStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "สินค้าคงคลังของใบสั่งขาย"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_Bal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Total_Qty_Request As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
