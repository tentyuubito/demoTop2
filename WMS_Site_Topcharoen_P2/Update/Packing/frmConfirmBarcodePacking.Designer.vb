<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmBarcodePacking
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
        Me.grdData = New System.Windows.Forms.DataGridView
        Me.Col_Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.lblSum = New System.Windows.Forms.Label
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.lblBarcode = New System.Windows.Forms.Label
        Me.lblSku = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtQty = New System.Windows.Forms.TextBox
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        Me.grdData.AllowUserToDeleteRows = False
        Me.grdData.AllowUserToResizeRows = False
        Me.grdData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdData.BackgroundColor = System.Drawing.Color.White
        Me.grdData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Barcode})
        Me.grdData.Location = New System.Drawing.Point(10, 110)
        Me.grdData.MultiSelect = False
        Me.grdData.Name = "grdData"
        Me.grdData.RowHeadersVisible = False
        Me.grdData.RowTemplate.Height = 24
        Me.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdData.Size = New System.Drawing.Size(447, 262)
        Me.grdData.TabIndex = 15
        '
        'Col_Barcode
        '
        Me.Col_Barcode.DataPropertyName = "Barcode"
        Me.Col_Barcode.Frozen = True
        Me.Col_Barcode.HeaderText = "Barcode"
        Me.Col_Barcode.Name = "Col_Barcode"
        Me.Col_Barcode.ReadOnly = True
        Me.Col_Barcode.Width = 550
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ยกเลิกรายการ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(352, 403)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(105, 36)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(12, 403)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(105, 36)
        Me.btnDelete.TabIndex = 22
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'lblSum
        '
        Me.lblSum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSum.AutoSize = True
        Me.lblSum.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSum.ForeColor = System.Drawing.Color.Blue
        Me.lblSum.Location = New System.Drawing.Point(10, 379)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(91, 17)
        Me.lblSum.TabIndex = 23
        Me.lblSum.Text = "ไม่พบรายการ"
        Me.lblSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBarcode
        '
        Me.txtBarcode.Location = New System.Drawing.Point(72, 85)
        Me.txtBarcode.MaxLength = 50
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(278, 20)
        Me.txtBarcode.TabIndex = 24
        '
        'lblBarcode
        '
        Me.lblBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblBarcode.Location = New System.Drawing.Point(10, 87)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(56, 13)
        Me.lblBarcode.TabIndex = 25
        Me.lblBarcode.Text = "Barcode"
        Me.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSku
        '
        Me.lblSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku.ForeColor = System.Drawing.Color.Red
        Me.lblSku.Location = New System.Drawing.Point(13, 7)
        Me.lblSku.Name = "lblSku"
        Me.lblSku.Size = New System.Drawing.Size(444, 75)
        Me.lblSku.TabIndex = 27
        Me.lblSku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(349, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Qty"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(387, 85)
        Me.txtQty.MaxLength = 50
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(67, 20)
        Me.txtQty.TabIndex = 29
        '
        'frmConfirmBarcodePacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 449)
        Me.Controls.Add(Me.txtBarcode)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSku)
        Me.Controls.Add(Me.lblBarcode)
        Me.Controls.Add(Me.lblSum)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grdData)
        Me.Name = "frmConfirmBarcodePacking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "แพ็คสินค้าลงถุง"
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdData As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lblSum As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents lblSku As System.Windows.Forms.Label
    Friend WithEvents Col_Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
End Class
