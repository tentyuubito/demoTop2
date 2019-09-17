<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMappingWareHouse
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
        Me.grdMappingWareHouse = New System.Windows.Forms.DataGridView
        Me.Col_index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_WareHouseST_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_WareHouse_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Distri_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.chkMapped = New System.Windows.Forms.CheckBox
        Me.btnSreach = New System.Windows.Forms.Button
        Me.cboConition = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtKey = New System.Windows.Forms.TextBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        CType(Me.grdMappingWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdMappingWareHouse
        '
        Me.grdMappingWareHouse.AllowUserToAddRows = False
        Me.grdMappingWareHouse.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdMappingWareHouse.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdMappingWareHouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMappingWareHouse.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_index, Me.Col_WareHouseST_ID, Me.Col_WareHouse_Name, Me.Col_Distri_id})
        Me.grdMappingWareHouse.Location = New System.Drawing.Point(12, 75)
        Me.grdMappingWareHouse.Name = "grdMappingWareHouse"
        Me.grdMappingWareHouse.RowHeadersVisible = False
        Me.grdMappingWareHouse.Size = New System.Drawing.Size(605, 253)
        Me.grdMappingWareHouse.TabIndex = 1
        '
        'Col_index
        '
        Me.Col_index.DataPropertyName = "Map_index"
        Me.Col_index.HeaderText = "_index"
        Me.Col_index.Name = "Col_index"
        Me.Col_index.ReadOnly = True
        Me.Col_index.Visible = False
        '
        'Col_WareHouseST_ID
        '
        Me.Col_WareHouseST_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_WareHouseST_ID.DataPropertyName = "WAREHOUSEID"
        Me.Col_WareHouseST_ID.FillWeight = 10.0!
        Me.Col_WareHouseST_ID.HeaderText = "คลังของ ST"
        Me.Col_WareHouseST_ID.MinimumWidth = 100
        Me.Col_WareHouseST_ID.Name = "Col_WareHouseST_ID"
        Me.Col_WareHouseST_ID.ReadOnly = True
        '
        'Col_WareHouse_Name
        '
        Me.Col_WareHouse_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Col_WareHouse_Name.DataPropertyName = "WAREHOUSENAME"
        Me.Col_WareHouse_Name.FillWeight = 350.0!
        Me.Col_WareHouse_Name.HeaderText = "ชื่อคลัง ST"
        Me.Col_WareHouse_Name.MinimumWidth = 350
        Me.Col_WareHouse_Name.Name = "Col_WareHouse_Name"
        Me.Col_WareHouse_Name.ReadOnly = True
        Me.Col_WareHouse_Name.Width = 350
        '
        'Col_Distri_id
        '
        Me.Col_Distri_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Distri_id.DataPropertyName = "DistributionCenter_Id"
        Me.Col_Distri_id.FillWeight = 10.15228!
        Me.Col_Distri_id.HeaderText = "ศุนย์กระจาย"
        Me.Col_Distri_id.Name = "Col_Distri_id"
        Me.Col_Distri_id.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ยกเลิกรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(234, 357)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(101, 48)
        Me.btnDelete.TabIndex = 0
        Me.btnDelete.Text = "     ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(516, 357)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 48)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "     ปิด"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkMapped
        '
        Me.chkMapped.AutoSize = True
        Me.chkMapped.Location = New System.Drawing.Point(289, 47)
        Me.chkMapped.Name = "chkMapped"
        Me.chkMapped.Size = New System.Drawing.Size(100, 17)
        Me.chkMapped.TabIndex = 3
        Me.chkMapped.Text = "ที่ Mapping แล้ว"
        Me.chkMapped.UseVisualStyleBackColor = True
        '
        'btnSreach
        '
        Me.btnSreach.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSreach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSreach.Location = New System.Drawing.Point(518, 10)
        Me.btnSreach.Name = "btnSreach"
        Me.btnSreach.Size = New System.Drawing.Size(93, 32)
        Me.btnSreach.TabIndex = 4
        Me.btnSreach.Text = "  ค้นหา"
        Me.btnSreach.UseVisualStyleBackColor = True
        '
        'cboConition
        '
        Me.cboConition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConition.FormattingEnabled = True
        Me.cboConition.Items.AddRange(New Object() {"WareHouse ST", "WareHouseName"})
        Me.cboConition.Location = New System.Drawing.Point(55, 16)
        Me.cboConition.Name = "cboConition"
        Me.cboConition.Size = New System.Drawing.Size(101, 21)
        Me.cboConition.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "เงื่อนไข"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtKey)
        Me.GroupBox1.Controls.Add(Me.chkMapped)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboConition)
        Me.GroupBox1.Location = New System.Drawing.Point(222, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(395, 68)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ค้นหา"
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(162, 16)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(128, 20)
        Me.txtKey.TabIndex = 7
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Map_index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "_index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.FillWeight = 25.0!
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "WAREHOUSEID"
        Me.DataGridViewTextBoxColumn2.FillWeight = 10.15228!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Chacked Data"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "WAREHOUSEID"
        Me.DataGridViewTextBoxColumn3.FillWeight = 10.15228!
        Me.DataGridViewTextBoxColumn3.HeaderText = "WareHouse ST"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 200
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "WAREHOUSENAME"
        Me.DataGridViewTextBoxColumn4.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "WareHouseName"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 200
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewComboBoxColumn1.DataPropertyName = "DistributionCenter_Id"
        Me.DataGridViewComboBoxColumn1.FillWeight = 10.15228!
        Me.DataGridViewComboBoxColumn1.HeaderText = "DistributionCenter_Id"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.ReadOnly = True
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "DistributionCenter_index"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "DistributionCenter_Id"
        Me.DataGridViewTextBoxColumn6.FillWeight = 10.15228!
        Me.DataGridViewTextBoxColumn6.HeaderText = "DistributionCenter_Id"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'btnAdd
        '
        Me.btnAdd.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(12, 357)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(101, 48)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "     เพิ่ม"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdit.Location = New System.Drawing.Point(119, 357)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(101, 48)
        Me.btnEdit.TabIndex = 9
        Me.btnEdit.Text = "     แก้ไข"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'frmMappingWareHouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 417)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnSreach)
        Me.Controls.Add(Me.grdMappingWareHouse)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmMappingWareHouse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MappingWareHouse"
        CType(Me.grdMappingWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdMappingWareHouse As System.Windows.Forms.DataGridView
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkMapped As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents btnSreach As System.Windows.Forms.Button
    Friend WithEvents cboConition As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Col_index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_WareHouseST_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_WareHouse_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Distri_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnEdit As System.Windows.Forms.Button
End Class
