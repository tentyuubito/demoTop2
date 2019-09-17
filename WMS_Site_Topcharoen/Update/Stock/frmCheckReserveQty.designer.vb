<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckReserveQty
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckReserveQty))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.Tab_ReserveReturn = New System.Windows.Forms.TabPage
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdReserveReturn = New System.Windows.Forms.DataGridView
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TAG_NOr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Reserve = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ReserveQtyAll = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Idr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Location_Aliasr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_LocationBalance_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabControl1.SuspendLayout()
        Me.Tab_ReserveReturn.SuspendLayout()
        CType(Me.grdReserveReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tab_ReserveReturn)
        Me.TabControl1.Location = New System.Drawing.Point(16, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(686, 413)
        Me.TabControl1.TabIndex = 9
        '
        'Tab_ReserveReturn
        '
        Me.Tab_ReserveReturn.Controls.Add(Me.chkSelectAll)
        Me.Tab_ReserveReturn.Controls.Add(Me.btnSave)
        Me.Tab_ReserveReturn.Controls.Add(Me.grdReserveReturn)
        Me.Tab_ReserveReturn.Location = New System.Drawing.Point(4, 22)
        Me.Tab_ReserveReturn.Name = "Tab_ReserveReturn"
        Me.Tab_ReserveReturn.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_ReserveReturn.Size = New System.Drawing.Size(678, 387)
        Me.Tab_ReserveReturn.TabIndex = 6
        Me.Tab_ReserveReturn.Text = "คืนค่าจองจากการจองจากระบบ"
        Me.Tab_ReserveReturn.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(14, 11)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 4
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(9, 343)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdReserveReturn
        '
        Me.grdReserveReturn.AllowUserToAddRows = False
        Me.grdReserveReturn.AllowUserToDeleteRows = False
        Me.grdReserveReturn.AllowUserToResizeRows = False
        Me.grdReserveReturn.BackgroundColor = System.Drawing.Color.White
        Me.grdReserveReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReserveReturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect, Me.col_TAG_NOr, Me.col_Reserve, Me.col_ReserveQtyAll, Me.col_Sku_Idr, Me.col_Location_Aliasr, Me.col_LocationBalance_Index})
        Me.grdReserveReturn.Location = New System.Drawing.Point(6, 6)
        Me.grdReserveReturn.Name = "grdReserveReturn"
        Me.grdReserveReturn.RowHeadersVisible = False
        Me.grdReserveReturn.Size = New System.Drawing.Size(666, 331)
        Me.grdReserveReturn.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.FalseValue = "0"
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkSelect.TrueValue = "1"
        Me.chkSelect.Width = 30
        '
        'col_TAG_NOr
        '
        Me.col_TAG_NOr.DataPropertyName = "TAG_NO"
        Me.col_TAG_NOr.HeaderText = "TAG NO"
        Me.col_TAG_NOr.Name = "col_TAG_NOr"
        Me.col_TAG_NOr.ReadOnly = True
        '
        'col_Reserve
        '
        Me.col_Reserve.DataPropertyName = "ReserveQty"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Red
        Me.col_Reserve.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_Reserve.HeaderText = "จำนวนจอง"
        Me.col_Reserve.Name = "col_Reserve"
        Me.col_Reserve.ReadOnly = True
        '
        'col_ReserveQtyAll
        '
        Me.col_ReserveQtyAll.DataPropertyName = "ReserveQtyAll"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Yellow
        Me.col_ReserveQtyAll.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_ReserveQtyAll.HeaderText = "จำนวนคืนจอง"
        Me.col_ReserveQtyAll.Name = "col_ReserveQtyAll"
        Me.col_ReserveQtyAll.ReadOnly = True
        '
        'col_Sku_Idr
        '
        Me.col_Sku_Idr.DataPropertyName = "Sku_Id"
        Me.col_Sku_Idr.HeaderText = "รหัสสินค้า"
        Me.col_Sku_Idr.Name = "col_Sku_Idr"
        Me.col_Sku_Idr.ReadOnly = True
        '
        'col_Location_Aliasr
        '
        Me.col_Location_Aliasr.DataPropertyName = "Location_Alias"
        Me.col_Location_Aliasr.HeaderText = "ตำแหน่ง"
        Me.col_Location_Aliasr.Name = "col_Location_Aliasr"
        Me.col_Location_Aliasr.ReadOnly = True
        '
        'col_LocationBalance_Index
        '
        Me.col_LocationBalance_Index.DataPropertyName = "LocationBalance_Index"
        Me.col_LocationBalance_Index.HeaderText = "LocationBalance_Index"
        Me.col_LocationBalance_Index.Name = "col_LocationBalance_Index"
        Me.col_LocationBalance_Index.ReadOnly = True
        Me.col_LocationBalance_Index.Width = 150
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Withdraw_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Col_Tag_No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Withdraw_No"
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Withdraw_Date"
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "DocumentType_WithDraw"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TransferStatus_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "TransferStatus_Index"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "TransferStatus_No"
        Me.DataGridViewTextBoxColumn7.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "TransferStatus_Date"
        Me.DataGridViewTextBoxColumn8.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 150
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "DocumentType_TranferStatus"
        Me.DataGridViewTextBoxColumn9.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn10.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "TransferOwner_Index"
        Me.DataGridViewTextBoxColumn11.HeaderText = "TransferOwner_Index"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "TransferOwner_No"
        Me.DataGridViewTextBoxColumn12.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 150
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "TransferOwner_Date"
        Me.DataGridViewTextBoxColumn13.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 150
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "DocumentType_TranferOwner"
        Me.DataGridViewTextBoxColumn14.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 150
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn15.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 150
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "TransferCode_Index"
        Me.DataGridViewTextBoxColumn16.HeaderText = "TransferCode_Index"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "TransferCode_No"
        Me.DataGridViewTextBoxColumn17.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 150
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "TransferCode_Date"
        Me.DataGridViewTextBoxColumn18.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Width = 150
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "DocumentType_TranferCode"
        Me.DataGridViewTextBoxColumn19.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 150
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn20.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 150
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "Borrow_Index"
        Me.DataGridViewTextBoxColumn21.HeaderText = "Borrow_Index"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.Visible = False
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "Borrow_No"
        Me.DataGridViewTextBoxColumn22.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.Width = 150
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "Borrow_Date"
        Me.DataGridViewTextBoxColumn23.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Width = 150
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "DocumentType_Borrow"
        Me.DataGridViewTextBoxColumn24.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.Width = 150
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn25.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.Width = 150
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "BorrowReturn_Index"
        Me.DataGridViewTextBoxColumn26.HeaderText = "BorrowReturn_Index"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.Visible = False
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "BorrowReturn_No"
        Me.DataGridViewTextBoxColumn27.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Width = 150
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "BorrowReturn_Date"
        Me.DataGridViewTextBoxColumn28.HeaderText = "วันที่เอกสาร"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.Width = 150
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "DocumentType_BorrowReturn"
        Me.DataGridViewTextBoxColumn29.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.Width = 150
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "Total_Qty"
        Me.DataGridViewTextBoxColumn30.HeaderText = "จำนวนจอง"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.Width = 150
        '
        'frmCheckReserveQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 434)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmCheckReserveQty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "คืนจองนอกระบบ"
        Me.TabControl1.ResumeLayout(False)
        Me.Tab_ReserveReturn.ResumeLayout(False)
        Me.Tab_ReserveReturn.PerformLayout()
        CType(Me.grdReserveReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
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
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tab_ReserveReturn As System.Windows.Forms.TabPage
    Friend WithEvents grdReserveReturn As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TAG_NOr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Reserve As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ReserveQtyAll As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Idr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Location_Aliasr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_LocationBalance_Index As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
