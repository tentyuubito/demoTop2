<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWarehouse
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
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.lblSearchfrom = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboSearchDocumentType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.grdWarehouse = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch.SuspendLayout()
        CType(Me.grdWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.lblSearchfrom)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Controls.Add(Me.cboSearchDocumentType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Location = New System.Drawing.Point(7, 395)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(440, 53)
        Me.grpSearch.TabIndex = 1
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "ค้นหา"
        '
        'lblSearchfrom
        '
        Me.lblSearchfrom.AutoSize = True
        Me.lblSearchfrom.Location = New System.Drawing.Point(6, 24)
        Me.lblSearchfrom.Name = "lblSearchfrom"
        Me.lblSearchfrom.Size = New System.Drawing.Size(43, 13)
        Me.lblSearchfrom.TabIndex = 3
        Me.lblSearchfrom.Text = "เงื่อนไข"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(330, 11)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboSearchDocumentType
        '
        Me.cboSearchDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchDocumentType.FormattingEnabled = True
        Me.cboSearchDocumentType.Items.AddRange(New Object() {"รหัส", "WareHouse"})
        Me.cboSearchDocumentType.Location = New System.Drawing.Point(55, 20)
        Me.cboSearchDocumentType.Name = "cboSearchDocumentType"
        Me.cboSearchDocumentType.Size = New System.Drawing.Size(113, 21)
        Me.cboSearchDocumentType.TabIndex = 0
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(174, 20)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(150, 20)
        Me.txtSearchKey.TabIndex = 1
        '
        'grdWarehouse
        '
        Me.grdWarehouse.AllowUserToAddRows = False
        Me.grdWarehouse.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdWarehouse.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdWarehouse.BackgroundColor = System.Drawing.Color.White
        Me.grdWarehouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdWarehouse.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Index, Me.Col_Id, Me.Col_Description, Me.col_DistributionCenter_Desc})
        Me.grdWarehouse.Location = New System.Drawing.Point(12, 12)
        Me.grdWarehouse.Name = "grdWarehouse"
        Me.grdWarehouse.ReadOnly = True
        Me.grdWarehouse.RowHeadersVisible = False
        Me.grdWarehouse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdWarehouse.Size = New System.Drawing.Size(435, 377)
        Me.grdWarehouse.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(347, 454)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 38)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(220, 454)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(114, 454)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "แก้ไข"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(8, 454)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "เพิ่ม"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Warehouse_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Warehouse_No"
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Warehouse"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'Col_Index
        '
        Me.Col_Index.DataPropertyName = "Warehouse_Index"
        Me.Col_Index.HeaderText = "Index"
        Me.Col_Index.Name = "Col_Index"
        Me.Col_Index.ReadOnly = True
        Me.Col_Index.Visible = False
        '
        'Col_Id
        '
        Me.Col_Id.DataPropertyName = "Warehouse_No"
        Me.Col_Id.HeaderText = "รหัส"
        Me.Col_Id.Name = "Col_Id"
        Me.Col_Id.ReadOnly = True
        '
        'Col_Description
        '
        Me.Col_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Description.DataPropertyName = "Description"
        Me.Col_Description.HeaderText = "Warehouse"
        Me.Col_Description.Name = "Col_Description"
        Me.Col_Description.ReadOnly = True
        '
        'col_DistributionCenter_Desc
        '
        Me.col_DistributionCenter_Desc.DataPropertyName = "DistributionCenter_Desc"
        Me.col_DistributionCenter_Desc.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_Desc.Name = "col_DistributionCenter_Desc"
        Me.col_DistributionCenter_Desc.ReadOnly = True
        Me.col_DistributionCenter_Desc.Width = 120
        '
        'frmMainWarehouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 503)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdWarehouse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainWarehouse"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลWarehouse"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearchfrom As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cboSearchDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdWarehouse As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
