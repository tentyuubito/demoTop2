<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportQueryToolsSF
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportQueryToolsSF))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnRpt = New System.Windows.Forms.Button
        Me.grdSearchCondition = New System.Windows.Forms.DataGridView
        Me.col_select = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_Add = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_and_or = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.col_name_where = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_colname = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_condition = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.col_Data1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colData2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SP_Report_View_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReportname = New System.Windows.Forms.TextBox
        Me.txtReportid = New System.Windows.Forms.TextBox
        Me.btnPopupSelectReport = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdData = New System.Windows.Forms.DataGridView
        Me.btnExcel = New System.Windows.Forms.Button
        Me.btnOut = New System.Windows.Forms.Button
        Me.lblCountRows = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdSearchCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnRpt)
        Me.GroupBox1.Controls.Add(Me.grdSearchCondition)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtReportname)
        Me.GroupBox1.Controls.Add(Me.txtReportid)
        Me.GroupBox1.Controls.Add(Me.btnPopupSelectReport)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 9)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(986, 227)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Special Report"
        '
        'btnRpt
        '
        Me.btnRpt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRpt.Image = CType(resources.GetObject("btnRpt.Image"), System.Drawing.Image)
        Me.btnRpt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRpt.Location = New System.Drawing.Point(867, 14)
        Me.btnRpt.Name = "btnRpt"
        Me.btnRpt.Size = New System.Drawing.Size(113, 43)
        Me.btnRpt.TabIndex = 32
        Me.btnRpt.Text = "Search"
        Me.btnRpt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRpt.UseVisualStyleBackColor = True
        '
        'grdSearchCondition
        '
        Me.grdSearchCondition.AllowUserToAddRows = False
        Me.grdSearchCondition.AllowUserToDeleteRows = False
        Me.grdSearchCondition.AllowUserToResizeRows = False
        Me.grdSearchCondition.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSearchCondition.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSearchCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSearchCondition.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_select, Me.Col_Add, Me.col_and_or, Me.col_name_where, Me.col_colname, Me.col_condition, Me.col_Data1, Me.colData2, Me.col_SP_Report_View_Name})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSearchCondition.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdSearchCondition.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grdSearchCondition.Location = New System.Drawing.Point(3, 69)
        Me.grdSearchCondition.Name = "grdSearchCondition"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSearchCondition.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdSearchCondition.RowHeadersVisible = False
        Me.grdSearchCondition.Size = New System.Drawing.Size(980, 155)
        Me.grdSearchCondition.TabIndex = 5
        '
        'col_select
        '
        Me.col_select.DataPropertyName = "Select"
        Me.col_select.HeaderText = "เลือก"
        Me.col_select.Name = "col_select"
        Me.col_select.Width = 40
        '
        'Col_Add
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.NullValue = "เพิ่ม"
        Me.Col_Add.DefaultCellStyle = DataGridViewCellStyle2
        Me.Col_Add.HeaderText = ""
        Me.Col_Add.Name = "Col_Add"
        Me.Col_Add.Width = 50
        '
        'col_and_or
        '
        Me.col_and_or.DataPropertyName = "Condition_1"
        Me.col_and_or.HeaderText = "และ/หรือ"
        Me.col_and_or.Name = "col_and_or"
        '
        'col_name_where
        '
        Me.col_name_where.DataPropertyName = "Col_NameWhere"
        Me.col_name_where.HeaderText = "name_where"
        Me.col_name_where.Name = "col_name_where"
        Me.col_name_where.Visible = False
        '
        'col_colname
        '
        Me.col_colname.DataPropertyName = "Column_Name"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_colname.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_colname.HeaderText = "ชื่อ Column"
        Me.col_colname.Name = "col_colname"
        Me.col_colname.ReadOnly = True
        Me.col_colname.Width = 250
        '
        'col_condition
        '
        Me.col_condition.DataPropertyName = "Condition_2"
        Me.col_condition.HeaderText = "เงื่อนไข"
        Me.col_condition.Name = "col_condition"
        '
        'col_Data1
        '
        Me.col_Data1.DataPropertyName = "Value_1"
        Me.col_Data1.HeaderText = "ข้อมูล 1"
        Me.col_Data1.Name = "col_Data1"
        Me.col_Data1.Width = 150
        '
        'colData2
        '
        Me.colData2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colData2.DataPropertyName = "Value_2"
        Me.colData2.HeaderText = "ถึง ข้อมูล2"
        Me.colData2.Name = "colData2"
        '
        'col_SP_Report_View_Name
        '
        Me.col_SP_Report_View_Name.DataPropertyName = "View_Name"
        Me.col_SP_Report_View_Name.HeaderText = "SP_Report_View_Name"
        Me.col_SP_Report_View_Name.Name = "col_SP_Report_View_Name"
        Me.col_SP_Report_View_Name.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "เงื่อนไขในการค้นหา"
        '
        'txtReportname
        '
        Me.txtReportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReportname.BackColor = System.Drawing.SystemColors.Control
        Me.txtReportname.Location = New System.Drawing.Point(203, 20)
        Me.txtReportname.Name = "txtReportname"
        Me.txtReportname.ReadOnly = True
        Me.txtReportname.Size = New System.Drawing.Size(658, 20)
        Me.txtReportname.TabIndex = 3
        '
        'txtReportid
        '
        Me.txtReportid.BackColor = System.Drawing.SystemColors.Control
        Me.txtReportid.Location = New System.Drawing.Point(55, 20)
        Me.txtReportid.Name = "txtReportid"
        Me.txtReportid.ReadOnly = True
        Me.txtReportid.Size = New System.Drawing.Size(108, 20)
        Me.txtReportid.TabIndex = 2
        '
        'btnPopupSelectReport
        '
        Me.btnPopupSelectReport.Location = New System.Drawing.Point(169, 18)
        Me.btnPopupSelectReport.Name = "btnPopupSelectReport"
        Me.btnPopupSelectReport.Size = New System.Drawing.Size(28, 23)
        Me.btnPopupSelectReport.TabIndex = 1
        Me.btnPopupSelectReport.Text = "..."
        Me.btnPopupSelectReport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "รายงาน"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.grdData)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 239)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(986, 408)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ผลการค้นหา"
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        Me.grdData.AllowUserToDeleteRows = False
        Me.grdData.AllowUserToResizeRows = False
        Me.grdData.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdData.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdData.Location = New System.Drawing.Point(3, 16)
        Me.grdData.Name = "grdData"
        Me.grdData.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdData.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdData.RowHeadersVisible = False
        Me.grdData.Size = New System.Drawing.Size(980, 389)
        Me.grdData.TabIndex = 0
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.Location = New System.Drawing.Point(12, 653)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(104, 36)
        Me.btnExcel.TabIndex = 3
        Me.btnExcel.Text = "Export Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnOut
        '
        Me.btnOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOut.Image = CType(resources.GetObject("btnOut.Image"), System.Drawing.Image)
        Me.btnOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOut.Location = New System.Drawing.Point(888, 653)
        Me.btnOut.Name = "btnOut"
        Me.btnOut.Size = New System.Drawing.Size(104, 36)
        Me.btnOut.TabIndex = 3
        Me.btnOut.Text = "Close"
        Me.btnOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOut.UseVisualStyleBackColor = True
        '
        'lblCountRows
        '
        Me.lblCountRows.AutoSize = True
        Me.lblCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lblCountRows.Location = New System.Drawing.Point(260, 665)
        Me.lblCountRows.Name = "lblCountRows"
        Me.lblCountRows.Size = New System.Drawing.Size(71, 13)
        Me.lblCountRows.TabIndex = 30
        Me.lblCountRows.Text = "ไม่พบรายการ"
        '
        'frmReportQueryToolsSF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 701)
        Me.Controls.Add(Me.lblCountRows)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnOut)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmReportQueryToolsSF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Query Tools"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdSearchCondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReportname As System.Windows.Forms.TextBox
    Friend WithEvents txtReportid As System.Windows.Forms.TextBox
    Friend WithEvents btnPopupSelectReport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdSearchCondition As System.Windows.Forms.DataGridView
    Friend WithEvents btnRpt As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdData As System.Windows.Forms.DataGridView
    Friend WithEvents btnOut As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents col_select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_Add As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_and_or As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents col_name_where As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_colname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_condition As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents col_Data1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colData2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SP_Report_View_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblCountRows As System.Windows.Forms.Label
End Class
