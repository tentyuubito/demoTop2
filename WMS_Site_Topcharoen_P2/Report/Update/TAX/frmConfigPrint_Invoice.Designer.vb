<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigPrint_Invoice
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigPrint_Invoice))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdPrintInvoice = New System.Windows.Forms.DataGridView
        Me.btn_Print = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.ChkPrintFrom = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewButtonColumn1 = New System.Windows.Forms.DataGridViewButtonColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Select = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_Customer_Shipping = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Report_Des = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Report_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Unit_Print = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Print_Total = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_btn_View = New System.Windows.Forms.DataGridViewButtonColumn
        Me.Col_Customer_Shipping_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdPrintInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPrintInvoice
        '
        Me.grdPrintInvoice.AllowUserToAddRows = False
        Me.grdPrintInvoice.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPrintInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPrintInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPrintInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Select, Me.Col_Customer_Shipping, Me.Col_Report_Des, Me.Col_Report_Name, Me.Col_Unit_Print, Me.Col_Print_Total, Me.Col_btn_View, Me.Col_Customer_Shipping_Index})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPrintInvoice.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdPrintInvoice.Location = New System.Drawing.Point(17, 21)
        Me.grdPrintInvoice.Name = "grdPrintInvoice"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPrintInvoice.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdPrintInvoice.RowHeadersVisible = False
        Me.grdPrintInvoice.Size = New System.Drawing.Size(536, 192)
        Me.grdPrintInvoice.TabIndex = 0
        '
        'btn_Print
        '
        Me.btn_Print.Image = CType(resources.GetObject("btn_Print.Image"), System.Drawing.Image)
        Me.btn_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Print.Location = New System.Drawing.Point(12, 246)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(100, 38)
        Me.btn_Print.TabIndex = 3
        Me.btn_Print.Text = "พิมพ์"
        Me.btn_Print.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(486, 246)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 38)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(24, 24)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 9
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'ChkPrintFrom
        '
        Me.ChkPrintFrom.AutoSize = True
        Me.ChkPrintFrom.Location = New System.Drawing.Point(118, 257)
        Me.ChkPrintFrom.Name = "ChkPrintFrom"
        Me.ChkPrintFrom.Size = New System.Drawing.Size(103, 17)
        Me.ChkPrintFrom.TabIndex = 10
        Me.ChkPrintFrom.Text = "ปริ้นฟอร์มขาวดำ"
        Me.ChkPrintFrom.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSelectAll)
        Me.GroupBox1.Controls.Add(Me.grdPrintInvoice)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(570, 227)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ปริ้นInvoice"
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "Col_Select"
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Customer_Shipping_Id"
        Me.DataGridViewTextBoxColumn1.FillWeight = 110.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสผู้รับ Bill to"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 155
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ชื่อรายงาน"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Report_Name"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Report Name"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Unit_Print"
        DataGridViewCellStyle7.NullValue = "0"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn4.FillWeight = 90.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "จำนวนปริ้น"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 90
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Customer_Shipping_Index"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn5.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "View"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 82
        '
        'DataGridViewButtonColumn1
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.NullValue = "..."
        Me.DataGridViewButtonColumn1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewButtonColumn1.FillWeight = 45.0!
        Me.DataGridViewButtonColumn1.HeaderText = ""
        Me.DataGridViewButtonColumn1.Name = "DataGridViewButtonColumn1"
        Me.DataGridViewButtonColumn1.ReadOnly = True
        Me.DataGridViewButtonColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Customer_Shipping_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Customer_Shipping_Index"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'Col_Select
        '
        Me.Col_Select.DataPropertyName = "Col_Select"
        Me.Col_Select.HeaderText = ""
        Me.Col_Select.Name = "Col_Select"
        Me.Col_Select.Width = 25
        '
        'Col_Customer_Shipping
        '
        Me.Col_Customer_Shipping.DataPropertyName = "Customer_Shipping_Id"
        Me.Col_Customer_Shipping.FillWeight = 110.0!
        Me.Col_Customer_Shipping.HeaderText = "รหัสผู้รับ Bill to"
        Me.Col_Customer_Shipping.Name = "Col_Customer_Shipping"
        Me.Col_Customer_Shipping.ReadOnly = True
        Me.Col_Customer_Shipping.Width = 110
        '
        'Col_Report_Des
        '
        Me.Col_Report_Des.DataPropertyName = "Description"
        Me.Col_Report_Des.HeaderText = "ชื่อรายงาน"
        Me.Col_Report_Des.Name = "Col_Report_Des"
        Me.Col_Report_Des.ReadOnly = True
        Me.Col_Report_Des.Width = 180
        '
        'Col_Report_Name
        '
        Me.Col_Report_Name.DataPropertyName = "Report_Name"
        Me.Col_Report_Name.HeaderText = "Report Name"
        Me.Col_Report_Name.Name = "Col_Report_Name"
        Me.Col_Report_Name.ReadOnly = True
        Me.Col_Report_Name.Visible = False
        '
        'Col_Unit_Print
        '
        Me.Col_Unit_Print.DataPropertyName = "Unit_Print"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = "0"
        Me.Col_Unit_Print.DefaultCellStyle = DataGridViewCellStyle2
        Me.Col_Unit_Print.FillWeight = 90.0!
        Me.Col_Unit_Print.HeaderText = "จำนวนปริ้น"
        Me.Col_Unit_Print.Name = "Col_Unit_Print"
        Me.Col_Unit_Print.Width = 90
        '
        'Col_Print_Total
        '
        Me.Col_Print_Total.DataPropertyName = "Total_Print"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ActiveBorder
        DataGridViewCellStyle3.NullValue = "0"
        Me.Col_Print_Total.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_Print_Total.FillWeight = 80.0!
        Me.Col_Print_Total.HeaderText = "ปริ้นแล้ว"
        Me.Col_Print_Total.Name = "Col_Print_Total"
        Me.Col_Print_Total.Width = 80
        '
        'Col_btn_View
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.NullValue = "..."
        Me.Col_btn_View.DefaultCellStyle = DataGridViewCellStyle4
        Me.Col_btn_View.FillWeight = 45.0!
        Me.Col_btn_View.HeaderText = "View"
        Me.Col_btn_View.Name = "Col_btn_View"
        Me.Col_btn_View.ReadOnly = True
        Me.Col_btn_View.Width = 45
        '
        'Col_Customer_Shipping_Index
        '
        Me.Col_Customer_Shipping_Index.DataPropertyName = "Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.HeaderText = "Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.Name = "Col_Customer_Shipping_Index"
        Me.Col_Customer_Shipping_Index.ReadOnly = True
        Me.Col_Customer_Shipping_Index.Visible = False
        '
        'frmConfigPrint_Invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 297)
        Me.Controls.Add(Me.ChkPrintFrom)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btn_Print)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmConfigPrint_Invoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตั้งค่าการปริ้นรายงาน"
        CType(Me.grdPrintInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdPrintInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Print As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewButtonColumn1 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents Col_Master As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChkPrintFrom As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Col_Select As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_Customer_Shipping As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Report_Des As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Report_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Unit_Print As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Print_Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_btn_View As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Col_Customer_Shipping_Index As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
