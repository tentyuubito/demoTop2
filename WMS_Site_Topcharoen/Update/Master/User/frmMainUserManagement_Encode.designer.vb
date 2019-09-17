<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainUserManagement_Encode
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.lblSearchfrom = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboSearchDocumentType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdUserManagement = New System.Windows.Forms.DataGridView
        Me.btnImportlicense = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UserIndex = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_UserID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_userFullName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Username = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Passwd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_GroupDes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DepartmentDes = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch.SuspendLayout()
        CType(Me.grdUserManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(592, 375)
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
        Me.btnDelete.Location = New System.Drawing.Point(228, 375)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'grpSearch
        '
        Me.grpSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSearch.Controls.Add(Me.lblSearchfrom)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Controls.Add(Me.cboSearchDocumentType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Location = New System.Drawing.Point(7, 316)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(685, 53)
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
        Me.lblSearchfrom.TabIndex = 0
        Me.lblSearchfrom.Text = "เงื่อนไข"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(579, 11)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboSearchDocumentType
        '
        Me.cboSearchDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchDocumentType.FormattingEnabled = True
        Me.cboSearchDocumentType.Items.AddRange(New Object() {"รหัสผู้ใช้", "ชื่อผู้ใช้", "Username"})
        Me.cboSearchDocumentType.Location = New System.Drawing.Point(55, 20)
        Me.cboSearchDocumentType.Name = "cboSearchDocumentType"
        Me.cboSearchDocumentType.Size = New System.Drawing.Size(135, 21)
        Me.cboSearchDocumentType.TabIndex = 1
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(196, 20)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(183, 20)
        Me.txtSearchKey.TabIndex = 2
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.แก้ไขรายการ
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(122, 375)
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
        Me.btnSave.Location = New System.Drawing.Point(12, 375)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "เพิ่ม"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdUserManagement
        '
        Me.grdUserManagement.AllowUserToAddRows = False
        Me.grdUserManagement.AllowUserToDeleteRows = False
        Me.grdUserManagement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdUserManagement.BackgroundColor = System.Drawing.Color.White
        Me.grdUserManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdUserManagement.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_UserIndex, Me.col_UserID, Me.col_userFullName, Me.col_Username, Me.col_Passwd, Me.Col_GroupDes, Me.Col_DepartmentDes})
        Me.grdUserManagement.Location = New System.Drawing.Point(12, 12)
        Me.grdUserManagement.MultiSelect = False
        Me.grdUserManagement.Name = "grdUserManagement"
        Me.grdUserManagement.ReadOnly = True
        Me.grdUserManagement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUserManagement.Size = New System.Drawing.Size(680, 295)
        Me.grdUserManagement.TabIndex = 0
        '
        'btnImportlicense
        '
        Me.btnImportlicense.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btnImportlicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImportlicense.Location = New System.Drawing.Point(334, 375)
        Me.btnImportlicense.Name = "btnImportlicense"
        Me.btnImportlicense.Size = New System.Drawing.Size(114, 38)
        Me.btnImportlicense.TabIndex = 6
        Me.btnImportlicense.Text = "Import License"
        Me.btnImportlicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImportlicense.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "user_index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "UserIndex"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "UserName"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ผู้ใช้งาน"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "User_id"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัสผู้ใช้งาน"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "userpasswd"
        Me.DataGridViewTextBoxColumn4.HeaderText = "รหัสผ่านผู้ใช้"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "userpasswd"
        Me.DataGridViewTextBoxColumn5.HeaderText = "รหัสผ่าน (Password)"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "GroupDes"
        Me.DataGridViewTextBoxColumn6.HeaderText = "กลุ่ม"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "DepartmentDes"
        Me.DataGridViewTextBoxColumn7.HeaderText = "แผนก"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'col_UserIndex
        '
        Me.col_UserIndex.DataPropertyName = "user_index"
        Me.col_UserIndex.HeaderText = "UserIndex"
        Me.col_UserIndex.Name = "col_UserIndex"
        Me.col_UserIndex.ReadOnly = True
        Me.col_UserIndex.Visible = False
        '
        'col_UserID
        '
        Me.col_UserID.DataPropertyName = "User_id"
        Me.col_UserID.HeaderText = "รหัสผู้ใช้"
        Me.col_UserID.Name = "col_UserID"
        Me.col_UserID.ReadOnly = True
        '
        'col_userFullName
        '
        Me.col_userFullName.DataPropertyName = "userFullName"
        Me.col_userFullName.HeaderText = "ชื่อผู้ใช้"
        Me.col_userFullName.Name = "col_userFullName"
        Me.col_userFullName.ReadOnly = True
        Me.col_userFullName.Width = 150
        '
        'col_Username
        '
        Me.col_Username.DataPropertyName = "userName"
        Me.col_Username.HeaderText = "Username"
        Me.col_Username.Name = "col_Username"
        Me.col_Username.ReadOnly = True
        '
        'col_Passwd
        '
        Me.col_Passwd.DataPropertyName = "userpasswd"
        Me.col_Passwd.HeaderText = "รหัสผ่าน (Password)"
        Me.col_Passwd.Name = "col_Passwd"
        Me.col_Passwd.ReadOnly = True
        Me.col_Passwd.Visible = False
        Me.col_Passwd.Width = 150
        '
        'Col_GroupDes
        '
        Me.Col_GroupDes.DataPropertyName = "GroupDes"
        Me.Col_GroupDes.HeaderText = "กลุ่ม"
        Me.Col_GroupDes.Name = "Col_GroupDes"
        Me.Col_GroupDes.ReadOnly = True
        Me.Col_GroupDes.Width = 150
        '
        'Col_DepartmentDes
        '
        Me.Col_DepartmentDes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_DepartmentDes.DataPropertyName = "DepartmentDes"
        Me.Col_DepartmentDes.HeaderText = "แผนก"
        Me.Col_DepartmentDes.Name = "Col_DepartmentDes"
        Me.Col_DepartmentDes.ReadOnly = True
        '
        'frmMainUserManagement_Encode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 419)
        Me.Controls.Add(Me.btnImportlicense)
        Me.Controls.Add(Me.grdUserManagement)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainUserManagement_Encode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "จัดการผู้ใช้และรหัสผ่าน"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdUserManagement, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents grdUserManagement As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnImportlicense As System.Windows.Forms.Button
    Friend WithEvents col_UserIndex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_UserID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_userFullName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Username As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Passwd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_GroupDes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DepartmentDes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
