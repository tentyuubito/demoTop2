<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOnline_SyncData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnline_SyncData))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btnSyncData = New System.Windows.Forms.Button
        Me.gbTable = New System.Windows.Forms.GroupBox
        Me.chkSeletAll = New System.Windows.Forms.CheckBox
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chk_Select = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_Group_Data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gbTable.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSyncData
        '
        Me.btnSyncData.Image = CType(resources.GetObject("btnSyncData.Image"), System.Drawing.Image)
        Me.btnSyncData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSyncData.Location = New System.Drawing.Point(9, 316)
        Me.btnSyncData.Name = "btnSyncData"
        Me.btnSyncData.Size = New System.Drawing.Size(107, 38)
        Me.btnSyncData.TabIndex = 3
        Me.btnSyncData.Text = "เริ่มนำเข้าข้อมูล"
        Me.btnSyncData.UseVisualStyleBackColor = True
        '
        'gbTable
        '
        Me.gbTable.Controls.Add(Me.chkSeletAll)
        Me.gbTable.Controls.Add(Me.grdPreviewData)
        Me.gbTable.Location = New System.Drawing.Point(3, 4)
        Me.gbTable.Name = "gbTable"
        Me.gbTable.Size = New System.Drawing.Size(518, 307)
        Me.gbTable.TabIndex = 5
        Me.gbTable.TabStop = False
        Me.gbTable.Text = "รายการข้อมูลรับ / ส่ง"
        '
        'chkSeletAll
        '
        Me.chkSeletAll.AutoSize = True
        Me.chkSeletAll.Location = New System.Drawing.Point(9, 21)
        Me.chkSeletAll.Name = "chkSeletAll"
        Me.chkSeletAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSeletAll.TabIndex = 1
        Me.chkSeletAll.UseVisualStyleBackColor = True
        '
        'grdPreviewData
        '
        Me.grdPreviewData.AllowUserToAddRows = False
        Me.grdPreviewData.AllowUserToDeleteRows = False
        Me.grdPreviewData.AllowUserToResizeRows = False
        Me.grdPreviewData.BackgroundColor = System.Drawing.Color.White
        Me.grdPreviewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPreviewData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chk_Select, Me.col_Group_Data, Me.col_status})
        Me.grdPreviewData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPreviewData.Location = New System.Drawing.Point(3, 16)
        Me.grdPreviewData.Name = "grdPreviewData"
        Me.grdPreviewData.RowHeadersVisible = False
        Me.grdPreviewData.Size = New System.Drawing.Size(512, 288)
        Me.grdPreviewData.TabIndex = 2
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(408, 317)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Table_Source"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Table_Source"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "chkSelect"
        DataGridViewCellStyle5.NullValue = "False"
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลือก"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 35
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn3.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ข้อมูล"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 250
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn5.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 300
        '
        'chk_Select
        '
        Me.chk_Select.DataPropertyName = "chkSelect"
        Me.chk_Select.HeaderText = ""
        Me.chk_Select.Name = "chk_Select"
        Me.chk_Select.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.chk_Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chk_Select.Width = 25
        '
        'col_Group_Data
        '
        Me.col_Group_Data.DataPropertyName = "Group_Data"
        Me.col_Group_Data.HeaderText = "ส่งข้อมูล"
        Me.col_Group_Data.Name = "col_Group_Data"
        Me.col_Group_Data.Width = 350
        '
        'col_status
        '
        Me.col_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_status.DataPropertyName = "Status"
        Me.col_status.HeaderText = "สถานะ"
        Me.col_status.Name = "col_status"
        Me.col_status.ReadOnly = True
        '
        'frmOnline_SyncData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 357)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.gbTable)
        Me.Controls.Add(Me.btnSyncData)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnline_SyncData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รับ / ส่งข้อมูล"
        Me.gbTable.ResumeLayout(False)
        Me.gbTable.PerformLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSyncData As System.Windows.Forms.Button
    Friend WithEvents gbTable As System.Windows.Forms.GroupBox
    Friend WithEvents grdPreviewData As System.Windows.Forms.DataGridView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSeletAll As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_Group_Data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_status As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
