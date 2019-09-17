<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTruckRelease_Update
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
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTruckRelease_Update))
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tTime_SourceOutGate = New System.Windows.Forms.DateTimePicker
        Me.dtTime_SourceOutGate = New System.Windows.Forms.DateTimePicker
        Me.lblDate_Time_SourceOutGate = New System.Windows.Forms.Label
        Me.lblTim_Time_SourceOutGate = New System.Windows.Forms.Label
        Me.grbTime_SourceInGate = New System.Windows.Forms.GroupBox
        Me.grdJobLoading = New System.Windows.Forms.DataGridView
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewMaskedEditColumn1 = New WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewMaskedEditColumn2 = New WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Index_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_No_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_ID_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_Truck_NoLoad = New WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
        Me.col_Weight_Truck_NoLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_Truck_FullLoad = New WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
        Me.col_Weight_Truck_FullLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Mile_AtSource = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbTime_SourceInGate.SuspendLayout()
        CType(Me.grdJobLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tTime_SourceOutGate
        '
        Me.tTime_SourceOutGate.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tTime_SourceOutGate.Location = New System.Drawing.Point(353, 22)
        Me.tTime_SourceOutGate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tTime_SourceOutGate.Name = "tTime_SourceOutGate"
        Me.tTime_SourceOutGate.ShowUpDown = True
        Me.tTime_SourceOutGate.Size = New System.Drawing.Size(139, 22)
        Me.tTime_SourceOutGate.TabIndex = 349
        Me.tTime_SourceOutGate.Value = New Date(2010, 9, 16, 0, 0, 0, 0)
        '
        'dtTime_SourceOutGate
        '
        Me.dtTime_SourceOutGate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTime_SourceOutGate.Location = New System.Drawing.Point(158, 22)
        Me.dtTime_SourceOutGate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtTime_SourceOutGate.Name = "dtTime_SourceOutGate"
        Me.dtTime_SourceOutGate.Size = New System.Drawing.Size(139, 22)
        Me.dtTime_SourceOutGate.TabIndex = 348
        Me.dtTime_SourceOutGate.Value = New Date(2010, 9, 16, 0, 0, 0, 0)
        '
        'lblDate_Time_SourceOutGate
        '
        Me.lblDate_Time_SourceOutGate.Location = New System.Drawing.Point(82, 21)
        Me.lblDate_Time_SourceOutGate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDate_Time_SourceOutGate.Name = "lblDate_Time_SourceOutGate"
        Me.lblDate_Time_SourceOutGate.Size = New System.Drawing.Size(68, 23)
        Me.lblDate_Time_SourceOutGate.TabIndex = 350
        Me.lblDate_Time_SourceOutGate.Text = "วันที่"
        Me.lblDate_Time_SourceOutGate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTim_Time_SourceOutGate
        '
        Me.lblTim_Time_SourceOutGate.Location = New System.Drawing.Point(307, 21)
        Me.lblTim_Time_SourceOutGate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTim_Time_SourceOutGate.Name = "lblTim_Time_SourceOutGate"
        Me.lblTim_Time_SourceOutGate.Size = New System.Drawing.Size(39, 23)
        Me.lblTim_Time_SourceOutGate.TabIndex = 351
        Me.lblTim_Time_SourceOutGate.Text = "เวลา"
        Me.lblTim_Time_SourceOutGate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grbTime_SourceInGate
        '
        Me.grbTime_SourceInGate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbTime_SourceInGate.Controls.Add(Me.dtTime_SourceOutGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.lblTim_Time_SourceOutGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.tTime_SourceOutGate)
        Me.grbTime_SourceInGate.Controls.Add(Me.lblDate_Time_SourceOutGate)
        Me.grbTime_SourceInGate.Location = New System.Drawing.Point(13, 15)
        Me.grbTime_SourceInGate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grbTime_SourceInGate.Name = "grbTime_SourceInGate"
        Me.grbTime_SourceInGate.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grbTime_SourceInGate.Size = New System.Drawing.Size(619, 60)
        Me.grbTime_SourceInGate.TabIndex = 352
        Me.grbTime_SourceInGate.TabStop = False
        Me.grbTime_SourceInGate.Text = "บันทึกวันที่ / เวลาปล่อยรถ"
        '
        'grdJobLoading
        '
        Me.grdJobLoading.AllowUserToAddRows = False
        Me.grdJobLoading.AllowUserToDeleteRows = False
        Me.grdJobLoading.AllowUserToOrderColumns = True
        Me.grdJobLoading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdJobLoading.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobLoading.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle28
        Me.grdJobLoading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJobLoading.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Index_JobLoading, Me.col_TransportManifest_No_JobLoading, Me.col_TransportManifest_Date_JobLoading, Me.col_Vehicle_ID_JobLoading, Me.col_Vehicle_No_JobLoading, Me.col_Time_Truck_NoLoad, Me.col_Weight_Truck_NoLoad, Me.col_Time_Truck_FullLoad, Me.col_Weight_Truck_FullLoad, Me.col_Mile_AtSource})
        Me.grdJobLoading.Location = New System.Drawing.Point(13, 82)
        Me.grdJobLoading.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdJobLoading.Name = "grdJobLoading"
        Me.grdJobLoading.RowHeadersVisible = False
        Me.grdJobLoading.RowTemplate.Height = 24
        Me.grdJobLoading.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJobLoading.Size = New System.Drawing.Size(619, 420)
        Me.grdJobLoading.TabIndex = 353
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(16, 511)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 47)
        Me.btnSave.TabIndex = 354
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(485, 511)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(147, 47)
        Me.btnExit.TabIndex = 355
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "TransportManifest_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 110
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "TransportManifest_No"
        DataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle33
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TransportManifest_Date"
        DataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle34
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Vehicle_Id"
        DataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle35
        Me.DataGridViewTextBoxColumn4.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Vehicle_No"
        DataGridViewCellStyle36.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle36
        Me.DataGridViewTextBoxColumn5.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewMaskedEditColumn1
        '
        Me.DataGridViewMaskedEditColumn1.DataPropertyName = "Time_Truck_NoLoad"
        Me.DataGridViewMaskedEditColumn1.HeaderText = "เวลาชั่งเบา"
        Me.DataGridViewMaskedEditColumn1.Mask = ""
        Me.DataGridViewMaskedEditColumn1.Name = "DataGridViewMaskedEditColumn1"
        Me.DataGridViewMaskedEditColumn1.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.DataGridViewMaskedEditColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewMaskedEditColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewMaskedEditColumn1.ValidatingType = GetType(String)
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Mile_AtSource"
        Me.DataGridViewTextBoxColumn6.HeaderText = "ไมล์ต้นทาง"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewMaskedEditColumn2
        '
        Me.DataGridViewMaskedEditColumn2.DataPropertyName = "Time_Truck_FullLoad"
        Me.DataGridViewMaskedEditColumn2.HeaderText = "เวลาชั่งหนัก"
        Me.DataGridViewMaskedEditColumn2.Mask = ""
        Me.DataGridViewMaskedEditColumn2.Name = "DataGridViewMaskedEditColumn2"
        Me.DataGridViewMaskedEditColumn2.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.DataGridViewMaskedEditColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewMaskedEditColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewMaskedEditColumn2.ValidatingType = GetType(String)
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Time_Truck_NoLoad"
        Me.DataGridViewTextBoxColumn7.HeaderText = "เวลาชั่งเบา"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 125
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Weight_Truck_FullLoad"
        Me.DataGridViewTextBoxColumn8.HeaderText = "น้ำหนักชั่งหนัก"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 125
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Time_Truck_FullLoad"
        Me.DataGridViewTextBoxColumn9.HeaderText = "เวลาชั่งหนัก"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Mile_AtSource"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ไมล์ต้นทาง"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'col_Index_JobLoading
        '
        Me.col_Index_JobLoading.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobLoading.HeaderText = "รหัสระบบ "
        Me.col_Index_JobLoading.Name = "col_Index_JobLoading"
        Me.col_Index_JobLoading.ReadOnly = True
        Me.col_Index_JobLoading.Visible = False
        Me.col_Index_JobLoading.Width = 110
        '
        'col_TransportManifest_No_JobLoading
        '
        Me.col_TransportManifest_No_JobLoading.DataPropertyName = "TransportManifest_No"
        DataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_TransportManifest_No_JobLoading.DefaultCellStyle = DataGridViewCellStyle29
        Me.col_TransportManifest_No_JobLoading.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobLoading.Name = "col_TransportManifest_No_JobLoading"
        Me.col_TransportManifest_No_JobLoading.ReadOnly = True
        '
        'col_TransportManifest_Date_JobLoading
        '
        Me.col_TransportManifest_Date_JobLoading.DataPropertyName = "TransportManifest_Date"
        DataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_TransportManifest_Date_JobLoading.DefaultCellStyle = DataGridViewCellStyle30
        Me.col_TransportManifest_Date_JobLoading.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobLoading.Name = "col_TransportManifest_Date_JobLoading"
        Me.col_TransportManifest_Date_JobLoading.ReadOnly = True
        '
        'col_Vehicle_ID_JobLoading
        '
        Me.col_Vehicle_ID_JobLoading.DataPropertyName = "Vehicle_Id"
        DataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Vehicle_ID_JobLoading.DefaultCellStyle = DataGridViewCellStyle31
        Me.col_Vehicle_ID_JobLoading.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_ID_JobLoading.Name = "col_Vehicle_ID_JobLoading"
        Me.col_Vehicle_ID_JobLoading.ReadOnly = True
        Me.col_Vehicle_ID_JobLoading.Visible = False
        '
        'col_Vehicle_No_JobLoading
        '
        Me.col_Vehicle_No_JobLoading.DataPropertyName = "Vehicle_License_No"
        DataGridViewCellStyle32.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.col_Vehicle_No_JobLoading.DefaultCellStyle = DataGridViewCellStyle32
        Me.col_Vehicle_No_JobLoading.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobLoading.Name = "col_Vehicle_No_JobLoading"
        Me.col_Vehicle_No_JobLoading.ReadOnly = True
        Me.col_Vehicle_No_JobLoading.Width = 80
        '
        'col_Time_Truck_NoLoad
        '
        Me.col_Time_Truck_NoLoad.DataPropertyName = "Time_Truck_NoLoad"
        Me.col_Time_Truck_NoLoad.HeaderText = "เวลาชั่งเบา"
        Me.col_Time_Truck_NoLoad.Mask = ""
        Me.col_Time_Truck_NoLoad.Name = "col_Time_Truck_NoLoad"
        Me.col_Time_Truck_NoLoad.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.col_Time_Truck_NoLoad.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_Time_Truck_NoLoad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.col_Time_Truck_NoLoad.ValidatingType = GetType(String)
        Me.col_Time_Truck_NoLoad.Visible = False
        '
        'col_Weight_Truck_NoLoad
        '
        Me.col_Weight_Truck_NoLoad.DataPropertyName = "Weight_Truck_NoLoad"
        Me.col_Weight_Truck_NoLoad.HeaderText = "น้ำหนักชั่งเบา (ตัน)"
        Me.col_Weight_Truck_NoLoad.Name = "col_Weight_Truck_NoLoad"
        Me.col_Weight_Truck_NoLoad.Visible = False
        Me.col_Weight_Truck_NoLoad.Width = 120
        '
        'col_Time_Truck_FullLoad
        '
        Me.col_Time_Truck_FullLoad.DataPropertyName = "Time_Truck_FullLoad"
        Me.col_Time_Truck_FullLoad.HeaderText = "เวลาชั่งหนัก"
        Me.col_Time_Truck_FullLoad.Mask = ""
        Me.col_Time_Truck_FullLoad.Name = "col_Time_Truck_FullLoad"
        Me.col_Time_Truck_FullLoad.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.col_Time_Truck_FullLoad.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_Time_Truck_FullLoad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.col_Time_Truck_FullLoad.ValidatingType = GetType(String)
        Me.col_Time_Truck_FullLoad.Visible = False
        '
        'col_Weight_Truck_FullLoad
        '
        Me.col_Weight_Truck_FullLoad.DataPropertyName = "Weight_Truck_FullLoad"
        Me.col_Weight_Truck_FullLoad.HeaderText = "น้ำหนักชั่งหนัก (ตัน)"
        Me.col_Weight_Truck_FullLoad.Name = "col_Weight_Truck_FullLoad"
        Me.col_Weight_Truck_FullLoad.Visible = False
        Me.col_Weight_Truck_FullLoad.Width = 125
        '
        'col_Mile_AtSource
        '
        Me.col_Mile_AtSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Mile_AtSource.DataPropertyName = "Mile_AtSource"
        Me.col_Mile_AtSource.HeaderText = "ไมล์ต้นทาง"
        Me.col_Mile_AtSource.Name = "col_Mile_AtSource"
        '
        'frmTruckRelease_Update
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 571)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grdJobLoading)
        Me.Controls.Add(Me.grbTime_SourceInGate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTruckRelease_Update"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ปล่อยรถ"
        Me.grbTime_SourceInGate.ResumeLayout(False)
        CType(Me.grdJobLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tTime_SourceOutGate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTime_SourceOutGate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate_Time_SourceOutGate As System.Windows.Forms.Label
    Friend WithEvents lblTim_Time_SourceOutGate As System.Windows.Forms.Label
    Friend WithEvents grbTime_SourceInGate As System.Windows.Forms.GroupBox
    Friend WithEvents grdJobLoading As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewMaskedEditColumn1 As WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
    Friend WithEvents DataGridViewMaskedEditColumn2 As WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
    Friend WithEvents col_Index_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_No_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_ID_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_Truck_NoLoad As WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
    Friend WithEvents col_Weight_Truck_NoLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_Truck_FullLoad As WMS_STD_OUTB_Transport.DataGridViewMaskedEditColumn
    Friend WithEvents col_Weight_Truck_FullLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Mile_AtSource As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
