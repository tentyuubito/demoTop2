<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRoute
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
        Me.grbRoute = New System.Windows.Forms.GroupBox
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.lbID = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblDes = New System.Windows.Forms.Label
        Me.grdSubRoute = New System.Windows.Forms.DataGridView
        Me.Col_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbSubRoute = New System.Windows.Forms.GroupBox
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnAddSub = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbRoute.SuspendLayout()
        CType(Me.grdSubRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbSubRoute.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(287, 414)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 37)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grbRoute
        '
        Me.grbRoute.Controls.Add(Me.txtID)
        Me.grbRoute.Controls.Add(Me.txtDes)
        Me.grbRoute.Controls.Add(Me.lbID)
        Me.grbRoute.Controls.Add(Me.btnSave)
        Me.grbRoute.Controls.Add(Me.lblDes)
        Me.grbRoute.Location = New System.Drawing.Point(12, 12)
        Me.grbRoute.Name = "grbRoute"
        Me.grbRoute.Size = New System.Drawing.Size(376, 111)
        Me.grbRoute.TabIndex = 0
        Me.grbRoute.TabStop = False
        Me.grbRoute.Text = "เส้นทาง"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(100, 19)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(151, 20)
        Me.txtID.TabIndex = 0
        '
        'txtDes
        '
        Me.txtDes.Location = New System.Drawing.Point(100, 43)
        Me.txtDes.MaxLength = 100
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(270, 20)
        Me.txtDes.TabIndex = 1
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(65, 23)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(269, 69)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(24, 46)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(70, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "รายละเอียด"
        '
        'grdSubRoute
        '
        Me.grdSubRoute.AllowUserToAddRows = False
        Me.grdSubRoute.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdSubRoute.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSubRoute.BackgroundColor = System.Drawing.Color.White
        Me.grdSubRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSubRoute.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Index, Me.Col_Id, Me.Col_Description})
        Me.grdSubRoute.Location = New System.Drawing.Point(6, 19)
        Me.grdSubRoute.Name = "grdSubRoute"
        Me.grdSubRoute.ReadOnly = True
        Me.grdSubRoute.RowHeadersVisible = False
        Me.grdSubRoute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSubRoute.Size = New System.Drawing.Size(361, 210)
        Me.grdSubRoute.TabIndex = 3
        '
        'Col_Index
        '
        Me.Col_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Col_Index.DataPropertyName = "SubRoute_index"
        Me.Col_Index.HeaderText = "Index"
        Me.Col_Index.Name = "Col_Index"
        Me.Col_Index.ReadOnly = True
        Me.Col_Index.Visible = False
        '
        'Col_Id
        '
        Me.Col_Id.DataPropertyName = "SubRoute_No"
        Me.Col_Id.HeaderText = "รหัส"
        Me.Col_Id.Name = "Col_Id"
        Me.Col_Id.ReadOnly = True
        Me.Col_Id.Width = 200
        '
        'Col_Description
        '
        Me.Col_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Description.DataPropertyName = "Description"
        Me.Col_Description.HeaderText = "รายละเอียด"
        Me.Col_Description.Name = "Col_Description"
        Me.Col_Description.ReadOnly = True
        '
        'grbSubRoute
        '
        Me.grbSubRoute.Controls.Add(Me.btnUpdate)
        Me.grbSubRoute.Controls.Add(Me.btnAddSub)
        Me.grbSubRoute.Controls.Add(Me.grdSubRoute)
        Me.grbSubRoute.Controls.Add(Me.btnDelete)
        Me.grbSubRoute.Location = New System.Drawing.Point(14, 129)
        Me.grbSubRoute.Name = "grbSubRoute"
        Me.grbSubRoute.Size = New System.Drawing.Size(373, 279)
        Me.grbSubRoute.TabIndex = 4
        Me.grbSubRoute.TabStop = False
        Me.grbSubRoute.Text = "เส้นทางย่อย"
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.แก้ไขรายการ
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(114, 235)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 38)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "แก้ไข"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAddSub
        '
        Me.btnAddSub.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddSub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddSub.Location = New System.Drawing.Point(8, 235)
        Me.btnAddSub.Name = "btnAddSub"
        Me.btnAddSub.Size = New System.Drawing.Size(100, 38)
        Me.btnAddSub.TabIndex = 5
        Me.btnAddSub.Text = "เพิ่ม"
        Me.btnAddSub.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(267, 235)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 38)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "ลบ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "SubRoute_index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SubRoute_No"
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'frmRoute
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 463)
        Me.Controls.Add(Me.grbSubRoute)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbRoute)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRoute"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เส้นทาง"
        Me.grbRoute.ResumeLayout(False)
        Me.grbRoute.PerformLayout()
        CType(Me.grdSubRoute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbSubRoute.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grbRoute As System.Windows.Forms.GroupBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdSubRoute As System.Windows.Forms.DataGridView
    Friend WithEvents grbSubRoute As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAddSub As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Col_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
