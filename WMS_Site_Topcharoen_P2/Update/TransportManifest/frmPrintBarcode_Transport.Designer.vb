<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintBarcode_Transport
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
        Me.grdDataDoc_no = New System.Windows.Forms.DataGridView
        Me.col_Select = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_Seq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Doc_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Doc_No2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdDataDoc_no, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdDataDoc_no
        '
        Me.grdDataDoc_no.AllowUserToAddRows = False
        Me.grdDataDoc_no.BackgroundColor = System.Drawing.Color.White
        Me.grdDataDoc_no.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDataDoc_no.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Select, Me.col_Seq, Me.col_Doc_No, Me.col_Doc_No2})
        Me.grdDataDoc_no.Location = New System.Drawing.Point(12, 52)
        Me.grdDataDoc_no.Name = "grdDataDoc_no"
        Me.grdDataDoc_no.RowHeadersVisible = False
        Me.grdDataDoc_no.Size = New System.Drawing.Size(425, 343)
        Me.grdDataDoc_no.TabIndex = 0
        '
        'col_Select
        '
        Me.col_Select.DataPropertyName = "chkSelect"
        Me.col_Select.FalseValue = "0"
        Me.col_Select.HeaderText = "เลือก"
        Me.col_Select.Name = "col_Select"
        Me.col_Select.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.col_Select.TrueValue = "1"
        Me.col_Select.Width = 50
        '
        'col_Seq
        '
        Me.col_Seq.HeaderText = "ลำดับ"
        Me.col_Seq.Name = "col_Seq"
        Me.col_Seq.Width = 50
        '
        'col_Doc_No
        '
        Me.col_Doc_No.DataPropertyName = "SalesOrder_No"
        Me.col_Doc_No.HeaderText = "เลขที่เอกสาร"
        Me.col_Doc_No.Name = "col_Doc_No"
        Me.col_Doc_No.Width = 150
        '
        'col_Doc_No2
        '
        Me.col_Doc_No2.DataPropertyName = "str1"
        Me.col_Doc_No2.HeaderText = "เลขที่เอกสาร2"
        Me.col_Doc_No2.Name = "col_Doc_No2"
        Me.col_Doc_No2.Width = 150
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "เลขที่ใบคุม : ..."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 413)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "จำนวนบิล  : 0 ใบ"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Barcode
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(194, 413)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(116, 43)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "พิมพ์บาร์โค้ด"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(322, 413)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(116, 43)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "ออก"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "ลำดับ"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่เอกสาร"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 320
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "เลขที่เอกสาร2"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'frmPrintBarcode_Transport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 480)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdDataDoc_no)
        Me.Name = "frmPrintBarcode_Transport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Barcode"
        CType(Me.grdDataDoc_no, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDataDoc_no As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_Seq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Doc_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Doc_No2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
