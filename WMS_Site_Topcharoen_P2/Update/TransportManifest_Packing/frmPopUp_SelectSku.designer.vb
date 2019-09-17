<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopUp_SelectSku
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
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.btn_select = New System.Windows.Forms.Button
        Me.grdPopupList = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ratio = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Package2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdPopupList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_cancel
        '
        Me.btn_cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_cancel.Location = New System.Drawing.Point(472, 391)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(100, 38)
        Me.btn_cancel.TabIndex = 3
        Me.btn_cancel.Text = "ยกเลิก"
        Me.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_select
        '
        Me.btn_select.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btn_select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_select.Location = New System.Drawing.Point(12, 391)
        Me.btn_select.Name = "btn_select"
        Me.btn_select.Size = New System.Drawing.Size(100, 38)
        Me.btn_select.TabIndex = 2
        Me.btn_select.Text = "เลือก"
        Me.btn_select.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_select.UseVisualStyleBackColor = True
        '
        'grdPopupList
        '
        Me.grdPopupList.AllowUserToAddRows = False
        Me.grdPopupList.AllowUserToDeleteRows = False
        Me.grdPopupList.AllowUserToResizeRows = False
        Me.grdPopupList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.grdPopupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPopupList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Sku_Index, Me.col_Package_Index, Me.Col_Sku_Id, Me.Col_Sku_Name, Me.col_Package, Me.col_Ratio, Me.col_Package2})
        Me.grdPopupList.Location = New System.Drawing.Point(12, 12)
        Me.grdPopupList.Name = "grdPopupList"
        Me.grdPopupList.ReadOnly = True
        Me.grdPopupList.RowHeadersVisible = False
        Me.grdPopupList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPopupList.Size = New System.Drawing.Size(560, 373)
        Me.grdPopupList.TabIndex = 4
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Sku_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัสสินค้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Sku_Name"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อสินค้า"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Package_Des"
        Me.DataGridViewTextBoxColumn5.HeaderText = "หน่วยสินค้า"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'Col_Sku_Index
        '
        Me.Col_Sku_Index.DataPropertyName = "Sku_Index"
        Me.Col_Sku_Index.HeaderText = "Sku_Index"
        Me.Col_Sku_Index.Name = "Col_Sku_Index"
        Me.Col_Sku_Index.ReadOnly = True
        Me.Col_Sku_Index.Visible = False
        '
        'col_Package_Index
        '
        Me.col_Package_Index.DataPropertyName = "Package_Index"
        Me.col_Package_Index.HeaderText = "Package_Index"
        Me.col_Package_Index.Name = "col_Package_Index"
        Me.col_Package_Index.ReadOnly = True
        Me.col_Package_Index.Visible = False
        '
        'Col_Sku_Id
        '
        Me.Col_Sku_Id.DataPropertyName = "Sku_Id"
        Me.Col_Sku_Id.HeaderText = "รหัสสินค้า"
        Me.Col_Sku_Id.Name = "Col_Sku_Id"
        Me.Col_Sku_Id.ReadOnly = True
        '
        'Col_Sku_Name
        '
        Me.Col_Sku_Name.DataPropertyName = "Sku_Name"
        Me.Col_Sku_Name.HeaderText = "ชื่อสินค้า"
        Me.Col_Sku_Name.Name = "Col_Sku_Name"
        Me.Col_Sku_Name.ReadOnly = True
        Me.Col_Sku_Name.Width = 200
        '
        'col_Package
        '
        Me.col_Package.DataPropertyName = "Package_Des"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.col_Package.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_Package.HeaderText = "หน่วย"
        Me.col_Package.Name = "col_Package"
        Me.col_Package.ReadOnly = True
        Me.col_Package.Width = 50
        '
        'col_Ratio
        '
        Me.col_Ratio.DataPropertyName = "Ratio"
        Me.col_Ratio.HeaderText = "จำนวน"
        Me.col_Ratio.Name = "col_Ratio"
        Me.col_Ratio.ReadOnly = True
        Me.col_Ratio.Width = 80
        '
        'col_Package2
        '
        Me.col_Package2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Package2.DataPropertyName = "Package"
        Me.col_Package2.HeaderText = "หน่วยหลัก"
        Me.col_Package2.Name = "col_Package2"
        Me.col_Package2.ReadOnly = True
        '
        'frmPopUp_SelectSku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 435)
        Me.Controls.Add(Me.grdPopupList)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_select)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPopUp_SelectSku"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เลือกสินค้า"
        CType(Me.grdPopupList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents btn_select As System.Windows.Forms.Button
    Friend WithEvents grdPopupList As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ratio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Package2 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
