<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainTown
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.grpSearch = New System.Windows.Forms.GroupBox
        Me.lblSearchfrom = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboSearchDocumentType = New System.Windows.Forms.ComboBox
        Me.txtSearchKey = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdDocumentType = New System.Windows.Forms.DataGridView
        Me.Col_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Province = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Town_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpSearch.SuspendLayout()
        CType(Me.grdDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(433, 454)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 38)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(114, 454)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.lblSearchfrom)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Controls.Add(Me.cboSearchDocumentType)
        Me.grpSearch.Controls.Add(Me.txtSearchKey)
        Me.grpSearch.Location = New System.Drawing.Point(7, 395)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(526, 53)
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
        Me.btnSearch.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(420, 11)
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
        Me.cboSearchDocumentType.Items.AddRange(New Object() {"รหัส", "ตำบล", "อำเภอ", "จังหวัด"})
        Me.cboSearchDocumentType.Location = New System.Drawing.Point(55, 20)
        Me.cboSearchDocumentType.Name = "cboSearchDocumentType"
        Me.cboSearchDocumentType.Size = New System.Drawing.Size(113, 21)
        Me.cboSearchDocumentType.TabIndex = 0
        '
        'txtSearchKey
        '
        Me.txtSearchKey.Location = New System.Drawing.Point(174, 20)
        Me.txtSearchKey.Name = "txtSearchKey"
        Me.txtSearchKey.Size = New System.Drawing.Size(240, 20)
        Me.txtSearchKey.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(8, 454)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "เพิ่ม"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdDocumentType
        '
        Me.grdDocumentType.AllowUserToAddRows = False
        Me.grdDocumentType.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdDocumentType.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdDocumentType.BackgroundColor = System.Drawing.Color.White
        Me.grdDocumentType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDocumentType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Index, Me.col_Province, Me.Col_Description, Me.Col_Id, Me.Col_Town_Name})
        Me.grdDocumentType.Location = New System.Drawing.Point(12, 12)
        Me.grdDocumentType.Name = "grdDocumentType"
        Me.grdDocumentType.ReadOnly = True
        Me.grdDocumentType.RowHeadersVisible = False
        Me.grdDocumentType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDocumentType.Size = New System.Drawing.Size(521, 377)
        Me.grdDocumentType.TabIndex = 0
        '
        'Col_Index
        '
        Me.Col_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Index.DataPropertyName = "Town_Index"
        Me.Col_Index.HeaderText = "Index"
        Me.Col_Index.Name = "Col_Index"
        Me.Col_Index.ReadOnly = True
        Me.Col_Index.Visible = False
        '
        'col_Province
        '
        Me.col_Province.DataPropertyName = "Province"
        Me.col_Province.HeaderText = "จังหวัด"
        Me.col_Province.Name = "col_Province"
        Me.col_Province.ReadOnly = True
        '
        'Col_Description
        '
        Me.Col_Description.DataPropertyName = "District"
        Me.Col_Description.HeaderText = "อำเภอ"
        Me.Col_Description.Name = "Col_Description"
        Me.Col_Description.ReadOnly = True
        '
        'Col_Id
        '
        Me.Col_Id.DataPropertyName = "Town_ID"
        Me.Col_Id.HeaderText = "รหัส"
        Me.Col_Id.Name = "Col_Id"
        Me.Col_Id.ReadOnly = True
        '
        'Col_Town_Name
        '
        Me.Col_Town_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Town_Name.DataPropertyName = "Town_Name"
        Me.Col_Town_Name.HeaderText = "ตำบล"
        Me.Col_Town_Name.Name = "Col_Town_Name"
        Me.Col_Town_Name.ReadOnly = True
        '
        'frmMainTown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 503)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdDocumentType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainTown"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลตำบล"
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearchfrom As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cboSearchDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdDocumentType As System.Windows.Forms.DataGridView
    Friend WithEvents Col_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Province As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Town_Name As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
