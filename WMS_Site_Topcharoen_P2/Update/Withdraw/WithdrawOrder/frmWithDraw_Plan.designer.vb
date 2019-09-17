<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWithDraw_Plan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWithDraw_Plan))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txtPlanDocument_No = New System.Windows.Forms.TextBox
        Me.cboPlanDocument = New System.Windows.Forms.ComboBox
        Me.lblPlanDocument = New System.Windows.Forms.Label
        Me.grbPlan = New System.Windows.Forms.GroupBox
        Me.lblDistributionCenter = New System.Windows.Forms.Label
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.lblRoute = New System.Windows.Forms.Label
        Me.lblSubRoute = New System.Windows.Forms.Label
        Me.cboRoute = New System.Windows.Forms.ComboBox
        Me.cboSubRoute = New System.Windows.Forms.ComboBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblPlanDocument_No = New System.Windows.Forms.Label
        Me.gbResult = New System.Windows.Forms.GroupBox
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.grdPlan = New System.Windows.Forms.DataGridView
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSelect = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chk_Plan = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TransportManifest_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Plan_Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Expected_Delivery_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Document_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnBomDetail = New System.Windows.Forms.DataGridViewButtonColumn
        Me.col_Qty_Bal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_Real = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Withdraw_Remain = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SubRoute_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Postcode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Document_Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Process_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Document_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Document_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbPlan.SuspendLayout()
        Me.gbResult.SuspendLayout()
        CType(Me.grdPlan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPlanDocument_No
        '
        Me.txtPlanDocument_No.Location = New System.Drawing.Point(305, 23)
        Me.txtPlanDocument_No.Name = "txtPlanDocument_No"
        Me.txtPlanDocument_No.Size = New System.Drawing.Size(141, 20)
        Me.txtPlanDocument_No.TabIndex = 3
        '
        'cboPlanDocument
        '
        Me.cboPlanDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlanDocument.FormattingEnabled = True
        Me.cboPlanDocument.Location = New System.Drawing.Point(90, 23)
        Me.cboPlanDocument.Name = "cboPlanDocument"
        Me.cboPlanDocument.Size = New System.Drawing.Size(134, 21)
        Me.cboPlanDocument.TabIndex = 1
        '
        'lblPlanDocument
        '
        Me.lblPlanDocument.AutoSize = True
        Me.lblPlanDocument.Location = New System.Drawing.Point(20, 26)
        Me.lblPlanDocument.Name = "lblPlanDocument"
        Me.lblPlanDocument.Size = New System.Drawing.Size(64, 13)
        Me.lblPlanDocument.TabIndex = 0
        Me.lblPlanDocument.Text = "ชนิดเอกสาร"
        '
        'grbPlan
        '
        Me.grbPlan.Controls.Add(Me.lblDistributionCenter)
        Me.grbPlan.Controls.Add(Me.cboDistributionCenter)
        Me.grbPlan.Controls.Add(Me.lblRoute)
        Me.grbPlan.Controls.Add(Me.lblSubRoute)
        Me.grbPlan.Controls.Add(Me.cboRoute)
        Me.grbPlan.Controls.Add(Me.cboSubRoute)
        Me.grbPlan.Controls.Add(Me.btnSearch)
        Me.grbPlan.Controls.Add(Me.txtPlanDocument_No)
        Me.grbPlan.Controls.Add(Me.lblPlanDocument_No)
        Me.grbPlan.Controls.Add(Me.cboPlanDocument)
        Me.grbPlan.Controls.Add(Me.lblPlanDocument)
        Me.grbPlan.Location = New System.Drawing.Point(8, 7)
        Me.grbPlan.Name = "grbPlan"
        Me.grbPlan.Size = New System.Drawing.Size(550, 98)
        Me.grbPlan.TabIndex = 0
        Me.grbPlan.TabStop = False
        Me.grbPlan.Text = "ค้นหาเอกสารอ้างอิง"
        '
        'lblDistributionCenter
        '
        Me.lblDistributionCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDistributionCenter.Location = New System.Drawing.Point(16, 71)
        Me.lblDistributionCenter.Name = "lblDistributionCenter"
        Me.lblDistributionCenter.Size = New System.Drawing.Size(71, 16)
        Me.lblDistributionCenter.TabIndex = 8
        Me.lblDistributionCenter.Text = "ศูนย์กระจาย"
        Me.lblDistributionCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDistributionCenter.Visible = False
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(90, 71)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(135, 21)
        Me.cboDistributionCenter.TabIndex = 9
        Me.cboDistributionCenter.Visible = False
        '
        'lblRoute
        '
        Me.lblRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(22, 47)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(66, 20)
        Me.lblRoute.TabIndex = 4
        Me.lblRoute.Text = "เส้นทาง"
        Me.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubRoute
        '
        Me.lblSubRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSubRoute.Location = New System.Drawing.Point(239, 47)
        Me.lblSubRoute.Name = "lblSubRoute"
        Me.lblSubRoute.Size = New System.Drawing.Size(66, 20)
        Me.lblSubRoute.TabIndex = 6
        Me.lblSubRoute.Text = "เส้นทางย่อย"
        Me.lblSubRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboRoute
        '
        Me.cboRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoute.FormattingEnabled = True
        Me.cboRoute.Location = New System.Drawing.Point(90, 47)
        Me.cboRoute.Name = "cboRoute"
        Me.cboRoute.Size = New System.Drawing.Size(134, 21)
        Me.cboRoute.TabIndex = 5
        '
        'cboSubRoute
        '
        Me.cboSubRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSubRoute.FormattingEnabled = True
        Me.cboSubRoute.Location = New System.Drawing.Point(305, 47)
        Me.cboSubRoute.Name = "cboSubRoute"
        Me.cboSubRoute.Size = New System.Drawing.Size(141, 21)
        Me.cboSubRoute.TabIndex = 7
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(452, 23)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(92, 38)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblPlanDocument_No
        '
        Me.lblPlanDocument_No.AutoSize = True
        Me.lblPlanDocument_No.Location = New System.Drawing.Point(239, 26)
        Me.lblPlanDocument_No.Name = "lblPlanDocument_No"
        Me.lblPlanDocument_No.Size = New System.Drawing.Size(66, 13)
        Me.lblPlanDocument_No.TabIndex = 2
        Me.lblPlanDocument_No.Text = "เลขที่เอกสาร"
        '
        'gbResult
        '
        Me.gbResult.Controls.Add(Me.chkAll)
        Me.gbResult.Controls.Add(Me.grdPlan)
        Me.gbResult.Location = New System.Drawing.Point(8, 110)
        Me.gbResult.Name = "gbResult"
        Me.gbResult.Size = New System.Drawing.Size(550, 370)
        Me.gbResult.TabIndex = 1
        Me.gbResult.TabStop = False
        Me.gbResult.Text = "รายการเอกสาร"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(10, 19)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(83, 17)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "เลือกทั้งหมด"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'grdPlan
        '
        Me.grdPlan.AllowUserToAddRows = False
        Me.grdPlan.AllowUserToDeleteRows = False
        Me.grdPlan.AllowUserToResizeRows = False
        Me.grdPlan.BackgroundColor = System.Drawing.Color.White
        Me.grdPlan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chk_Plan, Me.col_TransportManifest_No, Me.col_TransportManifest_Date, Me.col_Plan_Type, Me.col_Expected_Delivery_Date, Me.col_Document_No, Me.btnBomDetail, Me.col_Qty_Bal, Me.col_Qty_Real, Me.col_Withdraw_Remain, Me.Col_SubRoute_Desc, Me.Col_Postcode, Me.col_Document_Type, Me.col_Process_Id, Me.col_Document_Date, Me.col_Document_Index})
        Me.grdPlan.Location = New System.Drawing.Point(6, 42)
        Me.grdPlan.Name = "grdPlan"
        Me.grdPlan.RowHeadersVisible = False
        Me.grdPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPlan.Size = New System.Drawing.Size(538, 322)
        Me.grdPlan.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(458, 487)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 38)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Image = CType(resources.GetObject("btnSelect.Image"), System.Drawing.Image)
        Me.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelect.Location = New System.Drawing.Point(8, 487)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(100, 38)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "ตกลง"
        Me.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Plan_Type"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Document_Index"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Column3"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Document_No"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Document_Date"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Column5"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Document_Type"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Column6"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 150
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Process_Id"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Process_Id"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Document_Type"
        Me.DataGridViewTextBoxColumn7.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 150
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Process_Id"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Process_Id"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Document_Date"
        Me.DataGridViewTextBoxColumn9.HeaderText = "วันที่ออกเอกสาร"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Document_Type"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ประเภทเอกสาร"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 150
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Process_Id"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Process_Id"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Document_Date"
        Me.DataGridViewTextBoxColumn12.HeaderText = "วันที่ออกเอกสาร"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 120
        '
        'chk_Plan
        '
        Me.chk_Plan.DataPropertyName = "chkSelect"
        Me.chk_Plan.HeaderText = ""
        Me.chk_Plan.Name = "chk_Plan"
        Me.chk_Plan.Width = 25
        '
        'col_TransportManifest_No
        '
        Me.col_TransportManifest_No.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No.Name = "col_TransportManifest_No"
        Me.col_TransportManifest_No.ReadOnly = True
        Me.col_TransportManifest_No.Width = 120
        '
        'col_TransportManifest_Date
        '
        Me.col_TransportManifest_Date.DataPropertyName = "TransportManifest_Date"
        DataGridViewCellStyle1.Format = "dd/MM/yyyy"
        Me.col_TransportManifest_Date.DefaultCellStyle = DataGridViewCellStyle1
        Me.col_TransportManifest_Date.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date.Name = "col_TransportManifest_Date"
        Me.col_TransportManifest_Date.ReadOnly = True
        '
        'col_Plan_Type
        '
        Me.col_Plan_Type.DataPropertyName = "Plan_Type"
        Me.col_Plan_Type.HeaderText = "ชนิดเอกสาร"
        Me.col_Plan_Type.Name = "col_Plan_Type"
        Me.col_Plan_Type.ReadOnly = True
        Me.col_Plan_Type.Width = 80
        '
        'col_Expected_Delivery_Date
        '
        Me.col_Expected_Delivery_Date.DataPropertyName = "Expected_Delivery_Date"
        Me.col_Expected_Delivery_Date.HeaderText = "กำหนดส่ง"
        Me.col_Expected_Delivery_Date.Name = "col_Expected_Delivery_Date"
        Me.col_Expected_Delivery_Date.Visible = False
        '
        'col_Document_No
        '
        Me.col_Document_No.DataPropertyName = "Document_No"
        Me.col_Document_No.HeaderText = "เลขที่เอกสาร"
        Me.col_Document_No.Name = "col_Document_No"
        Me.col_Document_No.ReadOnly = True
        Me.col_Document_No.Width = 120
        '
        'btnBomDetail
        '
        Me.btnBomDetail.HeaderText = "รายละเอียด"
        Me.btnBomDetail.Name = "btnBomDetail"
        Me.btnBomDetail.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.btnBomDetail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.btnBomDetail.Width = 70
        '
        'col_Qty_Bal
        '
        Me.col_Qty_Bal.DataPropertyName = "Qty_Bal"
        Me.col_Qty_Bal.HeaderText = "จำนวนเบิก"
        Me.col_Qty_Bal.Name = "col_Qty_Bal"
        Me.col_Qty_Bal.Width = 80
        '
        'col_Qty_Real
        '
        Me.col_Qty_Real.DataPropertyName = "Qty_Withdraw_Real"
        Me.col_Qty_Real.HeaderText = "เบิกได้"
        Me.col_Qty_Real.Name = "col_Qty_Real"
        Me.col_Qty_Real.Width = 80
        '
        'col_Withdraw_Remain
        '
        Me.col_Withdraw_Remain.DataPropertyName = "Qty_Withdraw_Remain"
        Me.col_Withdraw_Remain.HeaderText = "ค้างเบิก"
        Me.col_Withdraw_Remain.Name = "col_Withdraw_Remain"
        Me.col_Withdraw_Remain.Width = 80
        '
        'Col_SubRoute_Desc
        '
        Me.Col_SubRoute_Desc.DataPropertyName = "SubRoute_Desc"
        Me.Col_SubRoute_Desc.HeaderText = "เส้นทางย่อย"
        Me.Col_SubRoute_Desc.Name = "Col_SubRoute_Desc"
        '
        'Col_Postcode
        '
        Me.Col_Postcode.DataPropertyName = "Postcode"
        Me.Col_Postcode.HeaderText = "รหัสไปรษณีย์"
        Me.Col_Postcode.Name = "Col_Postcode"
        '
        'col_Document_Type
        '
        Me.col_Document_Type.DataPropertyName = "Document_Type"
        Me.col_Document_Type.HeaderText = "ประเภทเอกสาร"
        Me.col_Document_Type.Name = "col_Document_Type"
        Me.col_Document_Type.ReadOnly = True
        Me.col_Document_Type.Width = 150
        '
        'col_Process_Id
        '
        Me.col_Process_Id.DataPropertyName = "Process_Id"
        Me.col_Process_Id.HeaderText = "Process_Id"
        Me.col_Process_Id.Name = "col_Process_Id"
        Me.col_Process_Id.Visible = False
        '
        'col_Document_Date
        '
        Me.col_Document_Date.DataPropertyName = "Document_Date"
        Me.col_Document_Date.HeaderText = "วันที่ออกเอกสาร"
        Me.col_Document_Date.Name = "col_Document_Date"
        Me.col_Document_Date.ReadOnly = True
        Me.col_Document_Date.Width = 120
        '
        'col_Document_Index
        '
        Me.col_Document_Index.DataPropertyName = "Document_Index"
        Me.col_Document_Index.HeaderText = "Document_Index"
        Me.col_Document_Index.Name = "col_Document_Index"
        Me.col_Document_Index.ReadOnly = True
        Me.col_Document_Index.Visible = False
        '
        'frmWithDraw_Plan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 534)
        Me.Controls.Add(Me.gbResult)
        Me.Controls.Add(Me.grbPlan)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWithDraw_Plan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหาเอกสารอ้างอิงการเบิก"
        Me.grbPlan.ResumeLayout(False)
        Me.grbPlan.PerformLayout()
        Me.gbResult.ResumeLayout(False)
        Me.gbResult.PerformLayout()
        CType(Me.grdPlan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtPlanDocument_No As System.Windows.Forms.TextBox
    Friend WithEvents cboPlanDocument As System.Windows.Forms.ComboBox
    Friend WithEvents lblPlanDocument As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grbPlan As System.Windows.Forms.GroupBox
    Friend WithEvents lblPlanDocument_No As System.Windows.Forms.Label
    Friend WithEvents gbResult As System.Windows.Forms.GroupBox
    Friend WithEvents grdPlan As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblSubRoute As System.Windows.Forms.Label
    Friend WithEvents cboSubRoute As System.Windows.Forms.ComboBox
    Friend WithEvents lblDistributionCenter As System.Windows.Forms.Label
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblRoute As System.Windows.Forms.Label
    Friend WithEvents cboRoute As System.Windows.Forms.ComboBox
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chk_Plan As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TransportManifest_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Plan_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Expected_Delivery_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Document_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBomDetail As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents col_Qty_Bal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_Real As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Withdraw_Remain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SubRoute_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Postcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Document_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Process_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Document_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Document_Index As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
