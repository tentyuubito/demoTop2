<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectReport))
        Me.grdReport = New System.Windows.Forms.DataGridView
        Me.col_report_index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_report_view = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_report_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_report_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnOut = New System.Windows.Forms.Button
        Me.btnSelect = New System.Windows.Forms.Button
        CType(Me.grdReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdReport
        '
        Me.grdReport.AllowUserToAddRows = False
        Me.grdReport.AllowUserToDeleteRows = False
        Me.grdReport.AllowUserToResizeRows = False
        Me.grdReport.BackgroundColor = System.Drawing.SystemColors.Window
        Me.grdReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_report_index, Me.col_report_view, Me.col_report_id, Me.col_report_name})
        Me.grdReport.Location = New System.Drawing.Point(12, 12)
        Me.grdReport.Name = "grdReport"
        Me.grdReport.RowHeadersVisible = False
        Me.grdReport.RowTemplate.ReadOnly = True
        Me.grdReport.Size = New System.Drawing.Size(372, 319)
        Me.grdReport.TabIndex = 0
        '
        'col_report_index
        '
        Me.col_report_index.DataPropertyName = "SP_Report_Index"
        Me.col_report_index.HeaderText = "report_index"
        Me.col_report_index.Name = "col_report_index"
        Me.col_report_index.Visible = False
        '
        'col_report_view
        '
        Me.col_report_view.DataPropertyName = "SP_Report_View_Name"
        Me.col_report_view.HeaderText = "report_view"
        Me.col_report_view.Name = "col_report_view"
        Me.col_report_view.Visible = False
        '
        'col_report_id
        '
        Me.col_report_id.DataPropertyName = "SP_Report_ID"
        Me.col_report_id.HeaderText = "รหัส"
        Me.col_report_id.Name = "col_report_id"
        '
        'col_report_name
        '
        Me.col_report_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_report_name.DataPropertyName = "SP_Report_Name"
        Me.col_report_name.HeaderText = "ชื่อรายงาน"
        Me.col_report_name.Name = "col_report_name"
        '
        'btnOut
        '
        Me.btnOut.Image = CType(resources.GetObject("btnOut.Image"), System.Drawing.Image)
        Me.btnOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOut.Location = New System.Drawing.Point(280, 337)
        Me.btnOut.Name = "btnOut"
        Me.btnOut.Size = New System.Drawing.Size(104, 36)
        Me.btnOut.TabIndex = 4
        Me.btnOut.Text = "ออก"
        Me.btnOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOut.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Image = CType(resources.GetObject("btnSelect.Image"), System.Drawing.Image)
        Me.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelect.Location = New System.Drawing.Point(12, 337)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(104, 36)
        Me.btnSelect.TabIndex = 5
        Me.btnSelect.Text = "เลือก"
        Me.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'frmSelectReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 385)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnOut)
        Me.Controls.Add(Me.grdReport)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelectReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เลือกรายงาน"
        CType(Me.grdReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnOut As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents col_report_index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_report_view As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_report_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_report_name As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
