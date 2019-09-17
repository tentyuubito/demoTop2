<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTruckReturn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTruckReturn))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tTime_ReturnTruckInGate = New System.Windows.Forms.DateTimePicker
        Me.dtTime_ReturnTruckInGate = New System.Windows.Forms.DateTimePicker
        Me.lblDate_Time_ReturnTruckInGate = New System.Windows.Forms.Label
        Me.lblTim_Time_ReturnTruckInGate = New System.Windows.Forms.Label
        Me.grbTime_SourceInGate = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.grdJobInTransport = New System.Windows.Forms.DataGridView
        Me.col_Index_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_No_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_Id_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Mile_AtSource = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Mile_Return = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbTime_SourceInGate.SuspendLayout()
        CType(Me.grdJobInTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tTime_ReturnTruckInGate
        '
        Me.tTime_ReturnTruckInGate.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tTime_ReturnTruckInGate.Location = New System.Drawing.Point(246, 29)
        Me.tTime_ReturnTruckInGate.Name = "tTime_ReturnTruckInGate"
        Me.tTime_ReturnTruckInGate.ShowUpDown = True
        Me.tTime_ReturnTruckInGate.Size = New System.Drawing.Size(105, 20)
        Me.tTime_ReturnTruckInGate.TabIndex = 349
        '
        'dtTime_ReturnTruckInGate
        '
        Me.dtTime_ReturnTruckInGate.CustomFormat = "dd/MM/yyyy"
        Me.dtTime_ReturnTruckInGate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTime_ReturnTruckInGate.Location = New System.Drawing.Point(100, 28)
        Me.dtTime_ReturnTruckInGate.Name = "dtTime_ReturnTruckInGate"
        Me.dtTime_ReturnTruckInGate.Size = New System.Drawing.Size(105, 20)
        Me.dtTime_ReturnTruckInGate.TabIndex = 348
        '
        'lblDate_Time_ReturnTruckInGate
        '
        Me.lblDate_Time_ReturnTruckInGate.Location = New System.Drawing.Point(43, 29)
        Me.lblDate_Time_ReturnTruckInGate.Name = "lblDate_Time_ReturnTruckInGate"
        Me.lblDate_Time_ReturnTruckInGate.Size = New System.Drawing.Size(51, 19)
        Me.lblDate_Time_ReturnTruckInGate.TabIndex = 350
        Me.lblDate_Time_ReturnTruckInGate.Text = "วันที่"
        Me.lblDate_Time_ReturnTruckInGate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTim_Time_ReturnTruckInGate
        '
        Me.lblTim_Time_ReturnTruckInGate.Location = New System.Drawing.Point(211, 29)
        Me.lblTim_Time_ReturnTruckInGate.Name = "lblTim_Time_ReturnTruckInGate"
        Me.lblTim_Time_ReturnTruckInGate.Size = New System.Drawing.Size(29, 19)
        Me.lblTim_Time_ReturnTruckInGate.TabIndex = 351
        Me.lblTim_Time_ReturnTruckInGate.Text = "เวลา"
        Me.lblTim_Time_ReturnTruckInGate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grbTime_SourceInGate
        '
        Me.grbTime_SourceInGate.Controls.Add(Me.dtTime_ReturnTruckInGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.lblTim_Time_ReturnTruckInGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.tTime_ReturnTruckInGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.lblDate_Time_ReturnTruckInGate)
        Me.grbTime_SourceInGate.Location = New System.Drawing.Point(12, 12)
        Me.grbTime_SourceInGate.Name = "grbTime_SourceInGate"
        Me.grbTime_SourceInGate.Size = New System.Drawing.Size(492, 69)
        Me.grbTime_SourceInGate.TabIndex = 352
        Me.grbTime_SourceInGate.TabStop = False
        Me.grbTime_SourceInGate.Text = "วันที่/เวลารับรถกลับ"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 415)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 354
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(394, 414)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(110, 38)
        Me.btnExit.TabIndex = 355
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grdJobInTransport
        '
        Me.grdJobInTransport.AllowUserToAddRows = False
        Me.grdJobInTransport.AllowUserToDeleteRows = False
        Me.grdJobInTransport.AllowUserToOrderColumns = True
        Me.grdJobInTransport.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobInTransport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdJobInTransport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJobInTransport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Index_JobInTransport, Me.col_TransportManifest_No_JobInTransport, Me.col_TransportManifest_Date_JobInTransport, Me.col_Vehicle_Id_JobInTransport, Me.col_Vehicle_No_JobInTransport, Me.col_Mile_AtSource, Me.col_Mile_Return})
        Me.grdJobInTransport.Location = New System.Drawing.Point(12, 87)
        Me.grdJobInTransport.Name = "grdJobInTransport"
        Me.grdJobInTransport.RowHeadersVisible = False
        Me.grdJobInTransport.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJobInTransport.Size = New System.Drawing.Size(492, 311)
        Me.grdJobInTransport.TabIndex = 356
        '
        'col_Index_JobInTransport
        '
        Me.col_Index_JobInTransport.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobInTransport.HeaderText = "รหัสระบบ "
        Me.col_Index_JobInTransport.Name = "col_Index_JobInTransport"
        Me.col_Index_JobInTransport.Visible = False
        Me.col_Index_JobInTransport.Width = 110
        '
        'col_TransportManifest_No_JobInTransport
        '
        Me.col_TransportManifest_No_JobInTransport.DataPropertyName = "TransportManifest_No"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_TransportManifest_No_JobInTransport.DefaultCellStyle = DataGridViewCellStyle2
        Me.col_TransportManifest_No_JobInTransport.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobInTransport.Name = "col_TransportManifest_No_JobInTransport"
        Me.col_TransportManifest_No_JobInTransport.ReadOnly = True
        '
        'col_TransportManifest_Date_JobInTransport
        '
        Me.col_TransportManifest_Date_JobInTransport.DataPropertyName = "TransportManifest_Date"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_TransportManifest_Date_JobInTransport.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_TransportManifest_Date_JobInTransport.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobInTransport.Name = "col_TransportManifest_Date_JobInTransport"
        Me.col_TransportManifest_Date_JobInTransport.ReadOnly = True
        '
        'col_Vehicle_Id_JobInTransport
        '
        Me.col_Vehicle_Id_JobInTransport.DataPropertyName = "Vehicle_Id"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Vehicle_Id_JobInTransport.DefaultCellStyle = DataGridViewCellStyle4
        Me.col_Vehicle_Id_JobInTransport.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_Id_JobInTransport.Name = "col_Vehicle_Id_JobInTransport"
        Me.col_Vehicle_Id_JobInTransport.ReadOnly = True
        Me.col_Vehicle_Id_JobInTransport.Visible = False
        '
        'col_Vehicle_No_JobInTransport
        '
        Me.col_Vehicle_No_JobInTransport.DataPropertyName = "Vehicle_License_No"
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Vehicle_No_JobInTransport.DefaultCellStyle = DataGridViewCellStyle5
        Me.col_Vehicle_No_JobInTransport.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobInTransport.Name = "col_Vehicle_No_JobInTransport"
        Me.col_Vehicle_No_JobInTransport.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_Vehicle_No_JobInTransport.Width = 80
        '
        'col_Mile_AtSource
        '
        Me.col_Mile_AtSource.DataPropertyName = "Mile_AtSource"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Mile_AtSource.DefaultCellStyle = DataGridViewCellStyle6
        Me.col_Mile_AtSource.HeaderText = "ไมล์ต้นทาง"
        Me.col_Mile_AtSource.Name = "col_Mile_AtSource"
        Me.col_Mile_AtSource.ReadOnly = True
        '
        'col_Mile_Return
        '
        Me.col_Mile_Return.DataPropertyName = "Mile_Return"
        Me.col_Mile_Return.HeaderText = "ไมล์กลับต้นทาง"
        Me.col_Mile_Return.Name = "col_Mile_Return"
        Me.col_Mile_Return.Width = 150
        '
        'frmTruckReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 464)
        Me.Controls.Add(Me.grdJobInTransport)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbTime_SourceInGate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTruckReturn"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รับรถกลับ"
        Me.grbTime_SourceInGate.ResumeLayout(False)
        CType(Me.grdJobInTransport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tTime_ReturnTruckInGate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTime_ReturnTruckInGate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate_Time_ReturnTruckInGate As System.Windows.Forms.Label
    Friend WithEvents lblTim_Time_ReturnTruckInGate As System.Windows.Forms.Label
    Friend WithEvents grbTime_SourceInGate As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdJobInTransport As System.Windows.Forms.DataGridView
    Friend WithEvents col_Index_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_No_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_Id_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Mile_AtSource As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Mile_Return As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
