<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSupplier_Popup
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
        Me.grdSupplier_PopupList = New System.Windows.Forms.DataGridView
        Me.System_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Title = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Name_eng = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cboCondition = New System.Windows.Forms.ComboBox
        Me.txtCondition = New System.Windows.Forms.TextBox
        Me.btn_select = New System.Windows.Forms.Button
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdSupplier_PopupList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdSupplier_PopupList
        '
        Me.grdSupplier_PopupList.AllowUserToAddRows = False
        Me.grdSupplier_PopupList.AllowUserToResizeRows = False
        Me.grdSupplier_PopupList.BackgroundColor = System.Drawing.Color.White
        Me.grdSupplier_PopupList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdSupplier_PopupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSupplier_PopupList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.System_Index, Me.Supplier_Id, Me.col_Title, Me.Supplier_Name, Me.Supplier_Name_eng})
        Me.grdSupplier_PopupList.Location = New System.Drawing.Point(12, 64)
        Me.grdSupplier_PopupList.Name = "grdSupplier_PopupList"
        Me.grdSupplier_PopupList.RowHeadersVisible = False
        Me.grdSupplier_PopupList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdSupplier_PopupList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSupplier_PopupList.Size = New System.Drawing.Size(389, 322)
        Me.grdSupplier_PopupList.TabIndex = 2
        '
        'System_Index
        '
        Me.System_Index.HeaderText = "รหัสระบบ"
        Me.System_Index.Name = "System_Index"
        Me.System_Index.ReadOnly = True
        Me.System_Index.Visible = False
        Me.System_Index.Width = 57
        '
        'Supplier_Id
        '
        Me.Supplier_Id.HeaderText = "รหัสผู้จำหน่าย"
        Me.Supplier_Id.Name = "Supplier_Id"
        Me.Supplier_Id.ReadOnly = True
        Me.Supplier_Id.Width = 96
        '
        'col_Title
        '
        Me.col_Title.DataPropertyName = "Title"
        Me.col_Title.HeaderText = "คำนำหน้า"
        Me.col_Title.Name = "col_Title"
        Me.col_Title.Visible = False
        '
        'Supplier_Name
        '
        Me.Supplier_Name.HeaderText = "ชื่อจำหน่าย(ไทย)"
        Me.Supplier_Name.Name = "Supplier_Name"
        Me.Supplier_Name.ReadOnly = True
        Me.Supplier_Name.Width = 190
        '
        'Supplier_Name_eng
        '
        Me.Supplier_Name_eng.HeaderText = "ชื่อจำหน่าย(อังกฤษ)"
        Me.Supplier_Name_eng.Name = "Supplier_Name_eng"
        Me.Supplier_Name_eng.Width = 150
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.cboCondition)
        Me.GroupBox1.Controls.Add(Me.txtCondition)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(389, 53)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ค้นหา"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(303, 11)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(80, 38)
        Me.btnSearch.TabIndex = 137
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboCondition
        '
        Me.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCondition.FormattingEnabled = True
        Me.cboCondition.Items.AddRange(New Object() {"รหัสจำหน่าย", "ชื่อจำหน่าย"})
        Me.cboCondition.Location = New System.Drawing.Point(7, 19)
        Me.cboCondition.Name = "cboCondition"
        Me.cboCondition.Size = New System.Drawing.Size(144, 21)
        Me.cboCondition.TabIndex = 136
        '
        'txtCondition
        '
        Me.txtCondition.Location = New System.Drawing.Point(157, 19)
        Me.txtCondition.Name = "txtCondition"
        Me.txtCondition.Size = New System.Drawing.Size(133, 20)
        Me.txtCondition.TabIndex = 135
        '
        'btn_select
        '
        Me.btn_select.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btn_select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_select.Location = New System.Drawing.Point(12, 390)
        Me.btn_select.Name = "btn_select"
        Me.btn_select.Size = New System.Drawing.Size(100, 38)
        Me.btn_select.TabIndex = 4
        Me.btn_select.Text = "เลือก"
        Me.btn_select.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_select.UseVisualStyleBackColor = True
        '
        'btn_cancel
        '
        Me.btn_cancel.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_cancel.Location = New System.Drawing.Point(301, 390)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(100, 38)
        Me.btn_cancel.TabIndex = 5
        Me.btn_cancel.Text = "ยกเลิก"
        Me.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(118, 390)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 38)
        Me.btnAdd.TabIndex = 25
        Me.btnAdd.Text = "เพิ่ม"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัสผู้จำหน่าย"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.HeaderText = "ชื่อจำหน่าย"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'frmSupplier_Popup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 435)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_select)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdSupplier_PopupList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSupplier_Popup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ผู้จำหน่าย"
        CType(Me.grdSupplier_PopupList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdSupplier_PopupList As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cboCondition As System.Windows.Forms.ComboBox
    Friend WithEvents txtCondition As System.Windows.Forms.TextBox
    Friend WithEvents btn_select As System.Windows.Forms.Button
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents System_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Title As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Name_eng As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
