<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopUpTagDuplicate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPopUpTagDuplicate))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdOrderItem = New System.Windows.Forms.DataGridView
        Me.lblTag_No = New System.Windows.Forms.Label
        Me.txtTag_No = New System.Windows.Forms.TextBox
        Me.btn_OKManualTag = New System.Windows.Forms.Button
        Me.btnCancelManualTag = New System.Windows.Forms.Button
        Me.chkAllOrder = New System.Windows.Forms.CheckBox
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_OrderItem_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Seq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_QtyOrder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_QtyTagComplet = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_WeightOrder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_VolumeOrder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_ERP_Location = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdOrderItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdOrderItem
        '
        Me.grdOrderItem.AllowDrop = True
        Me.grdOrderItem.AllowUserToAddRows = False
        Me.grdOrderItem.AllowUserToDeleteRows = False
        Me.grdOrderItem.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOrderItem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdOrderItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOrderItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.Col_OrderItem_Index, Me.Col_Seq, Me.DataGridViewTextBoxColumn30, Me.DataGridViewTextBoxColumn31, Me.DataGridViewTextBoxColumn32, Me.Col_QtyOrder, Me.Col_QtyTagComplet, Me.Col_WeightOrder, Me.Col_VolumeOrder, Me.Col_ERP_Location})
        Me.grdOrderItem.Location = New System.Drawing.Point(12, 12)
        Me.grdOrderItem.MultiSelect = False
        Me.grdOrderItem.Name = "grdOrderItem"
        Me.grdOrderItem.RowHeadersVisible = False
        Me.grdOrderItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdOrderItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdOrderItem.Size = New System.Drawing.Size(783, 280)
        Me.grdOrderItem.TabIndex = 2
        '
        'lblTag_No
        '
        Me.lblTag_No.AutoSize = True
        Me.lblTag_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTag_No.Location = New System.Drawing.Point(119, 299)
        Me.lblTag_No.Name = "lblTag_No"
        Me.lblTag_No.Size = New System.Drawing.Size(55, 16)
        Me.lblTag_No.TabIndex = 3
        Me.lblTag_No.Text = "เลขกำกับ"
        '
        'txtTag_No
        '
        Me.txtTag_No.Enabled = False
        Me.txtTag_No.Location = New System.Drawing.Point(180, 298)
        Me.txtTag_No.Name = "txtTag_No"
        Me.txtTag_No.Size = New System.Drawing.Size(165, 20)
        Me.txtTag_No.TabIndex = 4
        '
        'btn_OKManualTag
        '
        Me.btn_OKManualTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btn_OKManualTag.Image = CType(resources.GetObject("btn_OKManualTag.Image"), System.Drawing.Image)
        Me.btn_OKManualTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_OKManualTag.Location = New System.Drawing.Point(12, 298)
        Me.btn_OKManualTag.Name = "btn_OKManualTag"
        Me.btn_OKManualTag.Size = New System.Drawing.Size(104, 38)
        Me.btn_OKManualTag.TabIndex = 11
        Me.btn_OKManualTag.Text = "ตกลง"
        Me.btn_OKManualTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_OKManualTag.UseVisualStyleBackColor = True
        '
        'btnCancelManualTag
        '
        Me.btnCancelManualTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnCancelManualTag.Image = CType(resources.GetObject("btnCancelManualTag.Image"), System.Drawing.Image)
        Me.btnCancelManualTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelManualTag.Location = New System.Drawing.Point(691, 298)
        Me.btnCancelManualTag.Name = "btnCancelManualTag"
        Me.btnCancelManualTag.Size = New System.Drawing.Size(104, 38)
        Me.btnCancelManualTag.TabIndex = 15
        Me.btnCancelManualTag.Text = "ออก"
        Me.btnCancelManualTag.UseVisualStyleBackColor = True
        '
        'chkAllOrder
        '
        Me.chkAllOrder.AutoSize = True
        Me.chkAllOrder.Location = New System.Drawing.Point(18, 16)
        Me.chkAllOrder.Name = "chkAllOrder"
        Me.chkAllOrder.Size = New System.Drawing.Size(15, 14)
        Me.chkAllOrder.TabIndex = 16
        Me.chkAllOrder.UseVisualStyleBackColor = True
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "chkSelect"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.NullValue = False
        Me.DataGridViewCheckBoxColumn1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'Col_OrderItem_Index
        '
        Me.Col_OrderItem_Index.DataPropertyName = "OrderItem_Index"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.Col_OrderItem_Index.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_OrderItem_Index.HeaderText = "OrderItem_Index"
        Me.Col_OrderItem_Index.Name = "Col_OrderItem_Index"
        Me.Col_OrderItem_Index.Visible = False
        '
        'Col_Seq
        '
        Me.Col_Seq.DataPropertyName = "Seq"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Col_Seq.DefaultCellStyle = DataGridViewCellStyle4
        Me.Col_Seq.HeaderText = "ลำดับ"
        Me.Col_Seq.Name = "Col_Seq"
        Me.Col_Seq.ReadOnly = True
        Me.Col_Seq.Width = 40
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "Sku_Id"
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn30.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn30.HeaderText = "รหัส SKU"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Width = 110
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "SKU_Name"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn31.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn31.HeaderText = "ชื่อรายการ"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Width = 140
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "PLot"
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn32.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn32.HeaderText = "Lot / Batch"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        Me.DataGridViewTextBoxColumn32.Width = 120
        '
        'Col_QtyOrder
        '
        Me.Col_QtyOrder.DataPropertyName = "Qty_Order"
        Me.Col_QtyOrder.HeaderText = "จำนวน"
        Me.Col_QtyOrder.Name = "Col_QtyOrder"
        Me.Col_QtyOrder.Width = 80
        '
        'Col_QtyTagComplet
        '
        Me.Col_QtyTagComplet.DataPropertyName = "Qty"
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Col_QtyTagComplet.DefaultCellStyle = DataGridViewCellStyle8
        Me.Col_QtyTagComplet.HeaderText = "จำนวนสร้างได้"
        Me.Col_QtyTagComplet.Name = "Col_QtyTagComplet"
        Me.Col_QtyTagComplet.ReadOnly = True
        '
        'Col_WeightOrder
        '
        Me.Col_WeightOrder.DataPropertyName = "Weight"
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Col_WeightOrder.DefaultCellStyle = DataGridViewCellStyle9
        Me.Col_WeightOrder.HeaderText = "น้ำหนัก"
        Me.Col_WeightOrder.Name = "Col_WeightOrder"
        Me.Col_WeightOrder.ReadOnly = True
        Me.Col_WeightOrder.Width = 80
        '
        'Col_VolumeOrder
        '
        Me.Col_VolumeOrder.DataPropertyName = "Volume"
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Col_VolumeOrder.DefaultCellStyle = DataGridViewCellStyle10
        Me.Col_VolumeOrder.HeaderText = "ปริมาตร"
        Me.Col_VolumeOrder.Name = "Col_VolumeOrder"
        Me.Col_VolumeOrder.ReadOnly = True
        Me.Col_VolumeOrder.Width = 80
        '
        'Col_ERP_Location
        '
        Me.Col_ERP_Location.DataPropertyName = "ERP_Location"
        Me.Col_ERP_Location.HeaderText = "ERP_Location"
        Me.Col_ERP_Location.Name = "Col_ERP_Location"
        '
        'frmPopUpTagDuplicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 342)
        Me.Controls.Add(Me.chkAllOrder)
        Me.Controls.Add(Me.btnCancelManualTag)
        Me.Controls.Add(Me.btn_OKManualTag)
        Me.Controls.Add(Me.txtTag_No)
        Me.Controls.Add(Me.lblTag_No)
        Me.Controls.Add(Me.grdOrderItem)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPopUpTagDuplicate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เพิ่มรายการสินค้าในเลขกำกับ"
        CType(Me.grdOrderItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdOrderItem As System.Windows.Forms.DataGridView
    Friend WithEvents lblTag_No As System.Windows.Forms.Label
    Friend WithEvents txtTag_No As System.Windows.Forms.TextBox
    Friend WithEvents btn_OKManualTag As System.Windows.Forms.Button
    Friend WithEvents btnCancelManualTag As System.Windows.Forms.Button
    Friend WithEvents chkAllOrder As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_OrderItem_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Seq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_QtyOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_QtyTagComplet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_WeightOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_VolumeOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_ERP_Location As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
