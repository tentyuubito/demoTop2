<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckWithdraw
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txtWithdraw_No = New System.Windows.Forms.TextBox
        Me.txtCustomerShipping_Location = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblSumQty = New System.Windows.Forms.Label
        Me.grdData = New System.Windows.Forms.DataGridView
        Me.colSeq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSku_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sku_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ItemStatus_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtWithdraw_No
        '
        Me.txtWithdraw_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtWithdraw_No.Location = New System.Drawing.Point(152, 20)
        Me.txtWithdraw_No.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWithdraw_No.Name = "txtWithdraw_No"
        Me.txtWithdraw_No.Size = New System.Drawing.Size(248, 30)
        Me.txtWithdraw_No.TabIndex = 0
        '
        'txtCustomerShipping_Location
        '
        Me.txtCustomerShipping_Location.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtCustomerShipping_Location.Location = New System.Drawing.Point(152, 74)
        Me.txtCustomerShipping_Location.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerShipping_Location.Name = "txtCustomerShipping_Location"
        Me.txtCustomerShipping_Location.Size = New System.Drawing.Size(248, 30)
        Me.txtCustomerShipping_Location.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "เลขที่ใบเบิก"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(35, 78)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "สาขา"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(16, 474)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 47)
        Me.btnSave.TabIndex = 17
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnStart.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ส่งข้อมูล
        Me.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStart.Location = New System.Drawing.Point(317, 474)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(143, 47)
        Me.btnStart.TabIndex = 18
        Me.btnStart.Text = "เริ่มสแกน"
        Me.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClear.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(167, 474)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(143, 47)
        Me.btnClear.TabIndex = 19
        Me.btnClear.Text = "ล้างข้อมูล"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(732, 474)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(143, 47)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblSumQty
        '
        Me.lblSumQty.AutoSize = True
        Me.lblSumQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSumQty.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSumQty.Location = New System.Drawing.Point(16, 117)
        Me.lblSumQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumQty.Name = "lblSumQty"
        Me.lblSumQty.Size = New System.Drawing.Size(36, 39)
        Me.lblSumQty.TabIndex = 22
        Me.lblSumQty.Text = "0"
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdData.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdData.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSeq, Me.colTAG, Me.colSku_Id, Me.col_Sku_Name, Me.col_ItemStatus_Description, Me.col_Qty})
        Me.grdData.Location = New System.Drawing.Point(16, 159)
        Me.grdData.Margin = New System.Windows.Forms.Padding(4)
        Me.grdData.MultiSelect = False
        Me.grdData.Name = "grdData"
        Me.grdData.RowHeadersVisible = False
        Me.grdData.RowTemplate.Height = 24
        Me.grdData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdData.Size = New System.Drawing.Size(859, 308)
        Me.grdData.TabIndex = 23
        '
        'colSeq
        '
        Me.colSeq.DataPropertyName = "seq"
        Me.colSeq.HeaderText = "ลำดับ"
        Me.colSeq.Name = "colSeq"
        Me.colSeq.Width = 50
        '
        'colTAG
        '
        Me.colTAG.DataPropertyName = "Tag_no"
        Me.colTAG.HeaderText = "เลขที่ TAG เข้า"
        Me.colTAG.Name = "colTAG"
        '
        'colSku_Id
        '
        Me.colSku_Id.DataPropertyName = "Sku_Id"
        Me.colSku_Id.HeaderText = "รหัส Sku"
        Me.colSku_Id.Name = "colSku_Id"
        '
        'col_Sku_Name
        '
        Me.col_Sku_Name.DataPropertyName = "Sku_Description"
        Me.col_Sku_Name.HeaderText = "ชื่อรายการ"
        Me.col_Sku_Name.Name = "col_Sku_Name"
        Me.col_Sku_Name.Width = 200
        '
        'col_ItemStatus_Description
        '
        Me.col_ItemStatus_Description.DataPropertyName = "ItemStatus_Desc"
        Me.col_ItemStatus_Description.HeaderText = "สถานะสินค้า"
        Me.col_ItemStatus_Description.Name = "col_ItemStatus_Description"
        '
        'col_Qty
        '
        Me.col_Qty.DataPropertyName = "Qty"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.col_Qty.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_Qty.HeaderText = "จำนวน"
        Me.col_Qty.Name = "col_Qty"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "seq"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ลำดับ"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Tag_no"
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่ TAG เข้า"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รหัส Sku"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Sku_Description"
        Me.DataGridViewTextBoxColumn4.HeaderText = "ชื่อรายการ"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 300
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "ItemStatus_Desc"
        Me.DataGridViewTextBoxColumn5.HeaderText = "ประเภทสินค้า"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Column6"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Column7"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Column8"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Column9"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'frmCheckWithdraw
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(891, 538)
        Me.Controls.Add(Me.grdData)
        Me.Controls.Add(Me.lblSumQty)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustomerShipping_Location)
        Me.Controls.Add(Me.txtWithdraw_No)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCheckWithdraw"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCheckWithdraw"
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtWithdraw_No As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerShipping_Location As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblSumQty As System.Windows.Forms.Label
    Friend WithEvents grdData As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSeq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSku_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sku_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ItemStatus_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
