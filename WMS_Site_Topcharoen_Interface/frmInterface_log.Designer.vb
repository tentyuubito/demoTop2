<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterface_log
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterface_log))
        Me.grbTransactionDate = New System.Windows.Forms.GroupBox
        Me.grdInterval = New System.Windows.Forms.DataGridView
        Me.col_Num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Type_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Co_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSreach = New System.Windows.Forms.Button
        Me.txtSreach = New System.Windows.Forms.TextBox
        Me.grpSreach = New System.Windows.Forms.GroupBox
        Me.lblMaxlog = New System.Windows.Forms.Label
        Me.txtMaxLog = New System.Windows.Forms.TextBox
        Me.ChkE = New System.Windows.Forms.CheckBox
        Me.ChkS = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbDescrip = New System.Windows.Forms.Label
        Me.txtDocNo = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbTransactionDate.SuspendLayout()
        CType(Me.grdInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSreach.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbTransactionDate
        '
        Me.grbTransactionDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbTransactionDate.Controls.Add(Me.grdInterval)
        Me.grbTransactionDate.Controls.Add(Me.Button2)
        Me.grbTransactionDate.Location = New System.Drawing.Point(8, 84)
        Me.grbTransactionDate.Name = "grbTransactionDate"
        Me.grbTransactionDate.Size = New System.Drawing.Size(681, 321)
        Me.grbTransactionDate.TabIndex = 0
        Me.grbTransactionDate.TabStop = False
        Me.grbTransactionDate.Text = "Interface Log"
        '
        'grdInterval
        '
        Me.grdInterval.AllowUserToAddRows = False
        Me.grdInterval.AllowUserToDeleteRows = False
        Me.grdInterval.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdInterval.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInterval.BackgroundColor = System.Drawing.Color.White
        Me.grdInterval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInterval.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Num, Me.Col_Status, Me.Col_Type_Id, Me.Co_Description, Me.Col_DateTime})
        Me.grdInterval.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInterval.Location = New System.Drawing.Point(3, 16)
        Me.grdInterval.MultiSelect = False
        Me.grdInterval.Name = "grdInterval"
        Me.grdInterval.RowHeadersVisible = False
        Me.grdInterval.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInterval.Size = New System.Drawing.Size(675, 302)
        Me.grdInterval.TabIndex = 15
        '
        'col_Num
        '
        Me.col_Num.DataPropertyName = "_Index"
        Me.col_Num.HeaderText = "No"
        Me.col_Num.Name = "col_Num"
        Me.col_Num.Width = 50
        '
        'Col_Status
        '
        Me.Col_Status.DataPropertyName = "Status"
        Me.Col_Status.HeaderText = "Status"
        Me.Col_Status.Name = "Col_Status"
        Me.Col_Status.ReadOnly = True
        Me.Col_Status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Col_Status.Width = 50
        '
        'Col_Type_Id
        '
        Me.Col_Type_Id.DataPropertyName = "Type_Id"
        Me.Col_Type_Id.HeaderText = "Document No"
        Me.Col_Type_Id.Name = "Col_Type_Id"
        '
        'Co_Description
        '
        Me.Co_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Co_Description.DataPropertyName = "Description"
        Me.Co_Description.HeaderText = "Description"
        Me.Co_Description.Name = "Co_Description"
        '
        'Col_DateTime
        '
        Me.Col_DateTime.DataPropertyName = "add_date"
        Me.Col_DateTime.HeaderText = "DateADD"
        Me.Col_DateTime.Name = "Col_DateTime"
        Me.Col_DateTime.ReadOnly = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(3, 260)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 85
        Me.Button2.Text = "TestDate"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(604, 408)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(85, 38)
        Me.btnClose.TabIndex = 72
        Me.btnClose.Text = "   Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSreach
        '
        Me.btnSreach.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSreach.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ค้นหา
        Me.btnSreach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSreach.Location = New System.Drawing.Point(560, 12)
        Me.btnSreach.Name = "btnSreach"
        Me.btnSreach.Size = New System.Drawing.Size(130, 50)
        Me.btnSreach.TabIndex = 73
        Me.btnSreach.Text = "  SEARCH"
        Me.btnSreach.UseVisualStyleBackColor = True
        '
        'txtSreach
        '
        Me.txtSreach.Location = New System.Drawing.Point(252, 40)
        Me.txtSreach.Name = "txtSreach"
        Me.txtSreach.Size = New System.Drawing.Size(100, 20)
        Me.txtSreach.TabIndex = 74
        '
        'grpSreach
        '
        Me.grpSreach.Controls.Add(Me.lblMaxlog)
        Me.grpSreach.Controls.Add(Me.txtMaxLog)
        Me.grpSreach.Controls.Add(Me.ChkE)
        Me.grpSreach.Controls.Add(Me.ChkS)
        Me.grpSreach.Controls.Add(Me.Label6)
        Me.grpSreach.Controls.Add(Me.dtDate)
        Me.grpSreach.Controls.Add(Me.Label2)
        Me.grpSreach.Controls.Add(Me.lbDescrip)
        Me.grpSreach.Controls.Add(Me.txtDocNo)
        Me.grpSreach.Controls.Add(Me.txtSreach)
        Me.grpSreach.Location = New System.Drawing.Point(11, 8)
        Me.grpSreach.Name = "grpSreach"
        Me.grpSreach.Size = New System.Drawing.Size(543, 72)
        Me.grpSreach.TabIndex = 75
        Me.grpSreach.TabStop = False
        Me.grpSreach.Text = "Sreach"
        '
        'lblMaxlog
        '
        Me.lblMaxlog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMaxlog.Location = New System.Drawing.Point(6, 44)
        Me.lblMaxlog.Name = "lblMaxlog"
        Me.lblMaxlog.Size = New System.Drawing.Size(69, 17)
        Me.lblMaxlog.TabIndex = 81
        Me.lblMaxlog.Text = "Dispaly"
        Me.lblMaxlog.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMaxLog
        '
        Me.txtMaxLog.BackColor = System.Drawing.Color.White
        Me.txtMaxLog.Location = New System.Drawing.Point(81, 44)
        Me.txtMaxLog.MaxLength = 1000
        Me.txtMaxLog.Name = "txtMaxLog"
        Me.txtMaxLog.Size = New System.Drawing.Size(77, 20)
        Me.txtMaxLog.TabIndex = 80
        Me.txtMaxLog.Text = "5000"
        '
        'ChkE
        '
        Me.ChkE.AutoSize = True
        Me.ChkE.Location = New System.Drawing.Point(396, 16)
        Me.ChkE.Name = "ChkE"
        Me.ChkE.Size = New System.Drawing.Size(81, 17)
        Me.ChkE.TabIndex = 79
        Me.ChkE.Text = "Status Error"
        Me.ChkE.UseVisualStyleBackColor = True
        '
        'ChkS
        '
        Me.ChkS.AutoSize = True
        Me.ChkS.Location = New System.Drawing.Point(396, 42)
        Me.ChkS.Name = "ChkS"
        Me.ChkS.Size = New System.Drawing.Size(100, 17)
        Me.ChkS.TabIndex = 78
        Me.ChkS.Text = "Status Success"
        Me.ChkS.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 77
        Me.Label6.Text = "Date"
        '
        'dtDate
        '
        Me.dtDate.Checked = False
        Me.dtDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDate.Location = New System.Drawing.Point(43, 19)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.ShowCheckBox = True
        Me.dtDate.Size = New System.Drawing.Size(115, 20)
        Me.dtDate.TabIndex = 76
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "DocumentNo"
        '
        'lbDescrip
        '
        Me.lbDescrip.AutoSize = True
        Me.lbDescrip.Location = New System.Drawing.Point(186, 44)
        Me.lbDescrip.Name = "lbDescrip"
        Me.lbDescrip.Size = New System.Drawing.Size(60, 13)
        Me.lbDescrip.TabIndex = 75
        Me.lbDescrip.Text = "Description"
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.Location = New System.Drawing.Point(252, 14)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(100, 20)
        Me.txtDocNo.TabIndex = 74
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ส่งข้อมูล
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(11, 411)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 38)
        Me.Button1.TabIndex = 76
        Me.Button1.Text = "   Export" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Excel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "add_date"
        Me.DataGridViewTextBoxColumn4.HeaderText = "DateADD"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'frmInterface_log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 458)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSreach)
        Me.Controls.Add(Me.grbTransactionDate)
        Me.Controls.Add(Me.grpSreach)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmInterface_log"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daily Processing Management"
        Me.grbTransactionDate.ResumeLayout(False)
        CType(Me.grdInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSreach.ResumeLayout(False)
        Me.grpSreach.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbTransactionDate As System.Windows.Forms.GroupBox
    Friend WithEvents grdInterval As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSreach As System.Windows.Forms.Button
    Friend WithEvents txtSreach As System.Windows.Forms.TextBox
    Friend WithEvents grpSreach As System.Windows.Forms.GroupBox
    Friend WithEvents lbDescrip As System.Windows.Forms.Label
    Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ChkE As System.Windows.Forms.CheckBox
    Friend WithEvents ChkS As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents col_Num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Type_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Co_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblMaxlog As System.Windows.Forms.Label
    Friend WithEvents txtMaxLog As System.Windows.Forms.TextBox
End Class
