<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTag_Add_One_to_M
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdTag = New System.Windows.Forms.DataGridView
        Me.btnGenTAG = New System.Windows.Forms.Button
        Me.txtCountTag = New System.Windows.Forms.TextBox
        Me.gbTAGRunning = New System.Windows.Forms.GroupBox
        Me.lblVolumeUnit = New System.Windows.Forms.Label
        Me.lblWeightUnit2 = New System.Windows.Forms.Label
        Me.txtPerTag = New System.Windows.Forms.TextBox
        Me.txtVolume = New System.Windows.Forms.TextBox
        Me.rdPerTag = New System.Windows.Forms.RadioButton
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.rdTag = New System.Windows.Forms.RadioButton
        Me.lblReceivedVolume = New System.Windows.Forms.Label
        Me.lblReceivedWeight = New System.Windows.Forms.Label
        Me.lblReceivedQty = New System.Windows.Forms.Label
        Me.txtQTY = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lbCountRowsOrder = New System.Windows.Forms.Label
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_per_TAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Weight_per_TAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Volume_per_TAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LPN_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdTag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTAGRunning.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdTag
        '
        Me.grdTag.AllowUserToAddRows = False
        Me.grdTag.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTag.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTag.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Qty_per_TAG, Me.col_Weight_per_TAG, Me.col_Volume_per_TAG, Me.LPN_NO})
        Me.grdTag.Location = New System.Drawing.Point(8, 144)
        Me.grdTag.Margin = New System.Windows.Forms.Padding(4)
        Me.grdTag.MultiSelect = False
        Me.grdTag.Name = "grdTag"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTag.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdTag.RowHeadersVisible = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.grdTag.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.grdTag.RowTemplate.Height = 24
        Me.grdTag.Size = New System.Drawing.Size(996, 230)
        Me.grdTag.TabIndex = 12
        '
        'btnGenTAG
        '
        Me.btnGenTAG.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ดึงข้อมูล
        Me.btnGenTAG.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenTAG.Location = New System.Drawing.Point(848, 79)
        Me.btnGenTAG.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenTAG.Name = "btnGenTAG"
        Me.btnGenTAG.Size = New System.Drawing.Size(143, 47)
        Me.btnGenTAG.TabIndex = 2
        Me.btnGenTAG.Text = "สร้างชุด TAG"
        Me.btnGenTAG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenTAG.UseVisualStyleBackColor = True
        '
        'txtCountTag
        '
        Me.txtCountTag.Enabled = False
        Me.txtCountTag.Location = New System.Drawing.Point(858, 47)
        Me.txtCountTag.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCountTag.Name = "txtCountTag"
        Me.txtCountTag.Size = New System.Drawing.Size(132, 22)
        Me.txtCountTag.TabIndex = 1
        '
        'gbTAGRunning
        '
        Me.gbTAGRunning.Controls.Add(Me.grdTag)
        Me.gbTAGRunning.Controls.Add(Me.lblVolumeUnit)
        Me.gbTAGRunning.Controls.Add(Me.lblWeightUnit2)
        Me.gbTAGRunning.Controls.Add(Me.txtPerTag)
        Me.gbTAGRunning.Controls.Add(Me.txtVolume)
        Me.gbTAGRunning.Controls.Add(Me.rdPerTag)
        Me.gbTAGRunning.Controls.Add(Me.txtWeight)
        Me.gbTAGRunning.Controls.Add(Me.rdTag)
        Me.gbTAGRunning.Controls.Add(Me.lblReceivedVolume)
        Me.gbTAGRunning.Controls.Add(Me.btnGenTAG)
        Me.gbTAGRunning.Controls.Add(Me.lblReceivedWeight)
        Me.gbTAGRunning.Controls.Add(Me.txtCountTag)
        Me.gbTAGRunning.Controls.Add(Me.lblReceivedQty)
        Me.gbTAGRunning.Controls.Add(Me.txtQTY)
        Me.gbTAGRunning.Location = New System.Drawing.Point(4, 4)
        Me.gbTAGRunning.Margin = New System.Windows.Forms.Padding(4)
        Me.gbTAGRunning.Name = "gbTAGRunning"
        Me.gbTAGRunning.Padding = New System.Windows.Forms.Padding(4)
        Me.gbTAGRunning.Size = New System.Drawing.Size(1012, 383)
        Me.gbTAGRunning.TabIndex = 5
        Me.gbTAGRunning.TabStop = False
        Me.gbTAGRunning.Text = "สร้างเลขกำกับสินค้า 1 : M"
        '
        'lblVolumeUnit
        '
        Me.lblVolumeUnit.Location = New System.Drawing.Point(297, 65)
        Me.lblVolumeUnit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVolumeUnit.Name = "lblVolumeUnit"
        Me.lblVolumeUnit.Size = New System.Drawing.Size(33, 22)
        Me.lblVolumeUnit.TabIndex = 29
        Me.lblVolumeUnit.Text = "M3"
        Me.lblVolumeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblWeightUnit2
        '
        Me.lblWeightUnit2.Location = New System.Drawing.Point(297, 92)
        Me.lblWeightUnit2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWeightUnit2.Name = "lblWeightUnit2"
        Me.lblWeightUnit2.Size = New System.Drawing.Size(49, 20)
        Me.lblWeightUnit2.TabIndex = 26
        Me.lblWeightUnit2.Text = "KG"
        Me.lblWeightUnit2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPerTag
        '
        Me.txtPerTag.Location = New System.Drawing.Point(858, 17)
        Me.txtPerTag.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPerTag.Name = "txtPerTag"
        Me.txtPerTag.Size = New System.Drawing.Size(132, 22)
        Me.txtPerTag.TabIndex = 15
        '
        'txtVolume
        '
        Me.txtVolume.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVolume.Location = New System.Drawing.Point(157, 63)
        Me.txtVolume.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVolume.MaxLength = 8
        Me.txtVolume.Name = "txtVolume"
        Me.txtVolume.ReadOnly = True
        Me.txtVolume.Size = New System.Drawing.Size(125, 22)
        Me.txtVolume.TabIndex = 28
        '
        'rdPerTag
        '
        Me.rdPerTag.AutoSize = True
        Me.rdPerTag.Checked = True
        Me.rdPerTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.rdPerTag.Location = New System.Drawing.Point(706, 20)
        Me.rdPerTag.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPerTag.Name = "rdPerTag"
        Me.rdPerTag.Size = New System.Drawing.Size(109, 21)
        Me.rdPerTag.TabIndex = 14
        Me.rdPerTag.TabStop = True
        Me.rdPerTag.Text = "จำนวน/TAG"
        Me.rdPerTag.UseVisualStyleBackColor = True
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWeight.Location = New System.Drawing.Point(157, 91)
        Me.txtWeight.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWeight.MaxLength = 8
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(125, 22)
        Me.txtWeight.TabIndex = 25
        '
        'rdTag
        '
        Me.rdTag.AutoSize = True
        Me.rdTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.rdTag.Location = New System.Drawing.Point(706, 48)
        Me.rdTag.Margin = New System.Windows.Forms.Padding(4)
        Me.rdTag.Name = "rdTag"
        Me.rdTag.Size = New System.Drawing.Size(109, 21)
        Me.rdTag.TabIndex = 13
        Me.rdTag.Text = "จำนวน TAG"
        Me.rdTag.UseVisualStyleBackColor = True
        '
        'lblReceivedVolume
        '
        Me.lblReceivedVolume.Location = New System.Drawing.Point(17, 65)
        Me.lblReceivedVolume.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReceivedVolume.Name = "lblReceivedVolume"
        Me.lblReceivedVolume.Size = New System.Drawing.Size(128, 16)
        Me.lblReceivedVolume.TabIndex = 27
        Me.lblReceivedVolume.Text = "ปริมาตรรับเข้า"
        Me.lblReceivedVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReceivedWeight
        '
        Me.lblReceivedWeight.Location = New System.Drawing.Point(13, 95)
        Me.lblReceivedWeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReceivedWeight.Name = "lblReceivedWeight"
        Me.lblReceivedWeight.Size = New System.Drawing.Size(132, 16)
        Me.lblReceivedWeight.TabIndex = 30
        Me.lblReceivedWeight.Text = "น้ำหนักรับเข้า"
        Me.lblReceivedWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReceivedQty
        '
        Me.lblReceivedQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReceivedQty.Location = New System.Drawing.Point(21, 33)
        Me.lblReceivedQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReceivedQty.Name = "lblReceivedQty"
        Me.lblReceivedQty.Size = New System.Drawing.Size(124, 20)
        Me.lblReceivedQty.TabIndex = 23
        Me.lblReceivedQty.Text = "จำนวนรับเข้า"
        Me.lblReceivedQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtQTY
        '
        Me.txtQTY.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtQTY.Location = New System.Drawing.Point(157, 33)
        Me.txtQTY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQTY.MaxLength = 8
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.ReadOnly = True
        Me.txtQTY.Size = New System.Drawing.Size(125, 22)
        Me.txtQTY.TabIndex = 24
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(865, 395)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(143, 47)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "      ออก"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 394)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 47)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "       ตกลง"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lbCountRowsOrder
        '
        Me.lbCountRowsOrder.AutoSize = True
        Me.lbCountRowsOrder.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRowsOrder.Location = New System.Drawing.Point(163, 394)
        Me.lbCountRowsOrder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRowsOrder.Name = "lbCountRowsOrder"
        Me.lbCountRowsOrder.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRowsOrder.TabIndex = 9
        Me.lbCountRowsOrder.Text = "ไม่พบรายการ"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "TAG_No"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle8.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn1.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "เลขที่ TAG"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Qty_per_TAG"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle9.Format = "N4"
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn2.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 120
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Weight_per_TAG"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle10.Format = "N4"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn3.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "น้ำหนัก"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Volume_per_TAG"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn4.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "ปริมาตร"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 300
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'col_Qty_per_TAG
        '
        Me.col_Qty_per_TAG.DataPropertyName = "Qty_per_TAG"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle2.NullValue = Nothing
        Me.col_Qty_per_TAG.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_Qty_per_TAG.FillWeight = 80.0!
        Me.col_Qty_per_TAG.HeaderText = "จำนวน"
        Me.col_Qty_per_TAG.MinimumWidth = 100
        Me.col_Qty_per_TAG.Name = "col_Qty_per_TAG"
        '
        'col_Weight_per_TAG
        '
        Me.col_Weight_per_TAG.DataPropertyName = "Weight_per_TAG"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle3.Format = "N4"
        Me.col_Weight_per_TAG.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_Weight_per_TAG.FillWeight = 80.0!
        Me.col_Weight_per_TAG.HeaderText = "น้ำหนัก (KG)"
        Me.col_Weight_per_TAG.MinimumWidth = 120
        Me.col_Weight_per_TAG.Name = "col_Weight_per_TAG"
        Me.col_Weight_per_TAG.ReadOnly = True
        Me.col_Weight_per_TAG.Width = 120
        '
        'col_Volume_per_TAG
        '
        Me.col_Volume_per_TAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Volume_per_TAG.DataPropertyName = "Volume_per_TAG"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle4.Format = "N4"
        Me.col_Volume_per_TAG.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Volume_per_TAG.FillWeight = 50.0!
        Me.col_Volume_per_TAG.HeaderText = "ปริมาตร(M3)"
        Me.col_Volume_per_TAG.MinimumWidth = 100
        Me.col_Volume_per_TAG.Name = "col_Volume_per_TAG"
        Me.col_Volume_per_TAG.ReadOnly = True
        '
        'LPN_NO
        '
        Me.LPN_NO.DataPropertyName = "LPN_NO"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.LPN_NO.DefaultCellStyle = DataGridViewCellStyle5
        Me.LPN_NO.HeaderText = "LPN No."
        Me.LPN_NO.MinimumWidth = 300
        Me.LPN_NO.Name = "LPN_NO"
        Me.LPN_NO.Width = 300
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Volume_per_TAG"
        Me.DataGridViewTextBoxColumn5.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn5.HeaderText = "ปริมาตร"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'frmTag_Add_One_to_M
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 470)
        Me.Controls.Add(Me.lbCountRowsOrder)
        Me.Controls.Add(Me.gbTAGRunning)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTag_Add_One_to_M"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grdTag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTAGRunning.ResumeLayout(False)
        Me.gbTAGRunning.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdTag As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenTAG As System.Windows.Forms.Button
    Friend WithEvents txtCountTag As System.Windows.Forms.TextBox
    Friend WithEvents gbTAGRunning As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtPerTag As System.Windows.Forms.TextBox
    Friend WithEvents rdPerTag As System.Windows.Forms.RadioButton
    Friend WithEvents rdTag As System.Windows.Forms.RadioButton
    Public WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblVolumeUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeightUnit2 As System.Windows.Forms.Label
    Friend WithEvents txtVolume As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblReceivedVolume As System.Windows.Forms.Label
    Friend WithEvents lblReceivedWeight As System.Windows.Forms.Label
    Friend WithEvents lblReceivedQty As System.Windows.Forms.Label
    Friend WithEvents txtQTY As System.Windows.Forms.TextBox
    Friend WithEvents lbCountRowsOrder As System.Windows.Forms.Label
    Friend WithEvents col_Qty_per_TAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Weight_per_TAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Volume_per_TAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LPN_NO As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
