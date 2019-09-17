Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_OUTB_SO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.CurrencyTextBox
Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_OUTB_WithDraw


Public Class frmTransportManifestView_Update


    Private _IsSubManifest As Integer = 0

    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property




    Private Sub btnEditOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditManifest.Click
        Try
            If grdJobLoading.RowCount = 0 Then Exit Sub
            Dim strTransportManifest_Index As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString
            Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.EDIT, strTransportManifest_Index)

            Me.LoadDataGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditJobInTransport.Click
        Try
            If grdJobInTransport.RowCount = 0 Then Exit Sub

            If grdJobInTransport.DataSource Is Nothing Then Exit Sub


            Dim strTransportManifest_Index As String = Me.grdJobInTransport.CurrentRow.Cells("col_Index_JobInTransport").Value.ToString
            Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.EDIT, strTransportManifest_Index)




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditJobReturn.Click
        Try
            If grdJobReturn.RowCount = 0 Then Exit Sub
            Dim strTransportManifest_Index As String = Me.grdJobReturn.CurrentRow.Cells("col_Index_JobReturn").Value.ToString
            Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.EDIT, strTransportManifest_Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub TransportLoadingView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.Insert(Me)
            oFunction.SwitchLanguage(Me, 24)
            oFunction.SW_Language_Column(Me, Me.grdJob_Complete, 24)
            oFunction.SW_Language_Column(Me, Me.grdJob_Complete, 24)
            oFunction.SW_Language_Column(Me, Me.grdJobInTransport, 24)
            oFunction.SW_Language_Column(Me, Me.grdJobLoading, 24)
            oFunction.SW_Language_Column(Me, Me.grdJobReturn, 24)

            oFunction = Nothing

            'Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")
            grdJobInTransport.AutoGenerateColumns = False
            grdJobLoading.AutoGenerateColumns = False
            grdJobReturn.AutoGenerateColumns = False

            'Remove Tab
            'Me.tabTransportManifest.TabPages.Remove(tbpJobLoading)
            'Me.tabTransportManifest.TabPages.Remove(tbpJobInTransport)
            'Me.tabTransportManifest.TabPages.Remove(tbpJobReturn)
            'Me.tabTransportManifest.TabPages.Remove(tabManifestComplete)

            LoadComboBox()
            'LoadDataGrid()

            config_Transport()

            Me.getReportName(23)
            Select Case _IsSubManifest
                Case 0
                    'btnReturn.Visible = False
                    'btnConfirm.Visible = False
                Case 1
                    btnReceiveDestination.Visible = False
                    col_Time_DestinationInGate_JobInTransport.Visible = False
            End Select

            Me.LoadDataGrid()

            Me.pnlHide.Visible = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            'Dim dr As DataRow = objDT.NewRow
            'dr("Description") = "รายงานคิดเงิน/กล่อง"
            'dr("Report_Name") = "rptSumBox"
            'objDT.Rows.Add(dr)
            ' ***** Using add comboboxcolumn *****
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With

            ' *************************************
            'Me.cboPrint.Items.Add("รายงานคิดเงิน/กล่อง")
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub config_Transport()
        Dim objClassDB As New tb_TransportManifest
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "USE_TRANSPORT_DATETIME"
                            Me.col_TransportManifest_Date_JobLoading.DefaultCellStyle.Format = "d"
                            Me.col_TransportManifest_Date_JobReturn.DefaultCellStyle.Format = "d"
                            Me.col_Time_SourceOutGate_JobReturn.DefaultCellStyle.Format = "d"
                            Me.col_Time_DestinationInGate_JobReturn.DefaultCellStyle.Format = "d"
                            Me.col_Time_ReturnTruckInGate_JobReturn.DefaultCellStyle.Format = "d"
                            Me.col_TransportManifest_Date_JobInTransport.DefaultCellStyle.Format = "d"
                            Me.col_Time_SourceOutGate_JobInTransport.DefaultCellStyle.Format = "d"
                            Me.col_Time_DestinationInGate_JobInTransport.DefaultCellStyle.Format = "d"
                            Me.col_Time_ReturnTruckInGate_JobInTransport.DefaultCellStyle.Format = "d"

                            'Me.col_TransportManifest_Date_JobLoading.Width = 95
                            'Me.col_TransportManifest_Date_JobReturn.Width = 95
                            'Me.col_Time_SourceOutGate_JobReturn.Width = 95
                            'Me.col_Time_DestinationInGate_JobReturn.Width = 95
                            'Me.col_Time_ReturnTruckInGate_JobReturn.Width = 95
                            'Me.col_TransportManifest_Date_JobInTransport.Width = 95
                            'Me.col_Time_SourceOutGate_JobInTransport.Width = 95
                            'Me.col_Time_DestinationInGate_JobInTransport.Width = 95
                            'Me.col_Time_ReturnTruckInGate_JobInTransport.Width = 95

                        Case "Receive_Destination"
                            btnReceiveDestination.Visible = False
                            col_Time_DestinationInGate_JobInTransport.Visible = False

                        Case "USE_CUSTOMER_SHIPPING_LOCATION"
                            col_CustomerShippingLocation_JobLoading.Visible = False
                            col_ShippingLocation_JobInTransport.Visible = False
                            col_ShippingLocation_JobReturn.Visible = False
                            col_ShippingLocation_JobComplete.Visible = False

                        Case "USE_VOLUME_TRANSPORT"
                            lblSumVolume_JobLoading.Visible = False
                            txtSumVolume_JobLoading.Visible = False
                            lblSumVolume_JobInTransport.Visible = False
                            txtSumVolume_JobInTransport.Visible = False
                            lbltxtSumVolume_JobReturn.Visible = False
                            txtSumVolume_JobReturn.Visible = False
                            lblSumVolume_JobComplete.Visible = False
                            txtSumVolume_JobComplete.Visible = False

                            col_Volume_JobComplete.Visible = False
                            col_Volume_JobInTransport.Visible = False
                            col_Volume_JobReturn.Visible = False
                            col_Sum_Volume_JobLoading.Visible = False


                        Case "USE_WEIGHT_TRANSPORT"
                            lblSumWeight_JobLoading.Visible = False
                            txtSumWeight_JobLoading.Visible = False
                            lblSumWeight_JobInTransport.Visible = False
                            txtSumWeight_JobInTransport.Visible = False
                            lblSumWeight_JobReturn.Visible = False
                            txtSumWeight_JobReturn.Visible = False
                            lblSumWeight_JobComplete.Visible = False
                            txtSumWeight_JobComplete.Visible = False

                            col_Weight_JobComplete.Visible = False
                            col_weight_JobInTransport.Visible = False
                            col_Weight_JobReturn.Visible = False
                            col_Sum_Weight_JobLoading.Visible = False



                    End Select

                    i = i + 1
                Next

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub

    Private Sub getProcessStatus()
        Dim objClassDB As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus(24)
            objDT = objClassDB.DataTable


            Dim drRow As DataRow
            drRow = objDT.NewRow
            drRow("Description") = "ไม่ระบุ"
            drRow("Status") = "-11"
            objDT.Rows.Add(drRow)

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With
            cboDocumentStatus.SelectedValue = "-11"
            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub


    Private Sub LoadComboBox()
        Try
            Me.getDocumentType(24)
            Me.getProcessStatus()
            Me.getComboRoute()
            Me.getComboDistributionCenter()
            Me.getComboTransportJobType()
            Me.getComboVehicleType()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            Dim drRow As DataRow
            drRow = objDT.NewRow
            drRow("Description") = "ไม่ระบุ"
            drRow("DocumentType_Index") = "-11"
            objDT.Rows.Add(drRow)
            objDT.DefaultView.Sort = "DocumentType_Index DESC"

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With
            cboDocumentType.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getComboVehicleType()
        Dim objClassDB As New ms_VehicleType(ms_VehicleType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboVehicleType.BeginUpdate()
            With cboVehicleType
                .DisplayMember = "Description"
                .ValueMember = "VehicleType_Index"
                .DataSource = objDT
            End With
            cboVehicleType.SelectedIndex = cboVehicleType.Items.Count - 1
            cboVehicleType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboTransportJobType()
        Dim objClassDB As New ms_TransportJobType(ms_TransportJobType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim cbItem(2) As String
            cbItem(0) = "-11"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboTransportJobType.BeginUpdate()
            With cboTransportJobType
                .DisplayMember = "Description"
                .ValueMember = "TransportJobType_Index"
                .DataSource = objDT
            End With
            cboTransportJobType.SelectedIndex = cboTransportJobType.Items.Count - 1
            cboTransportJobType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboDistributionCenter()
        Dim objClassDB As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable
            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboDistributionCenter.BeginUpdate()
            With cboDistributionCenter
                .DisplayMember = "Description"
                .ValueMember = "DistributionCenter_Index"
                .DataSource = objDT
            End With
            cboDistributionCenter.SelectedIndex = cboDistributionCenter.Items.Count - 1
            cboDistributionCenter.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboRoute()
        Dim objClassDB As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboRoute.BeginUpdate()
            With cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = objDT
            End With

            cboRoute.EndUpdate()
            cboRoute.SelectedIndex = 0
            cboRoute.SelectedIndex = cboRoute.Items.Count - 1
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboSubRoute(ByVal vRoute_Index As String)
        Dim objClassDB As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            'If (cboRoute.SelectedIndex = (cboRoute.Items.Count - 1)) Then
            '    cboSubRoute.SelectedIndex = cboSubRoute.Items.Count - 1
            '    Exit Sub
            'End If


            objClassDB.GetAllAsDataTable(vRoute_Index)
            objDT = objClassDB.DataTable
            Dim cbItem(5) As String
            cbItem(0) = "-11"
            cbItem(3) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)

            cboSubRoute.BeginUpdate()
            With cboSubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With
            If objDT.Rows.Count > 0 Then
                cboSubRoute.SelectedValue = "-11"
            End If
            cboSubRoute.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub LoadDataGrid()
        Try
            'tbpJobLoading
            'tbpJobInTransport
            'tbpJobReturn
            'tabManifestComplete

            Select Case Me.tabTransportManifest.SelectedTab.Name
                Case tbpJobLoading.Name
                    Me.LoadgrdManifest_Summary()
                Case tbpJobInTransport.Name
                    Me.LoadgrdJobInTransport()
                Case tbpJobReturn.Name
                    Me.LoadgrdJobReturn()
                Case tabManifestComplete.Name
                    Me.LoadgrdJobComplete()
            End Select

            SetnumRows()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub LoadgrdJobComplete()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_Summary(sqlCondition(grdJob_Complete.Name), 2, _IsSubManifest)
    '        Me.grdJob_Complete.DataSource = objtbManifest.DataTable
    '        If grdJob_Complete.RowCount = 0 Then
    '            txtSumDoc_JobComplete.Text = "0"
    '            txtSumQty_Jobcomplete.Text = "0"
    '            txtSumWeight_JobComplete.Text = "0.0000"
    '            txtSumVolume_JobComplete.Text = "0.0000"
    '        Else
    '            txtSumDoc_JobComplete.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
    '            txtSumQty_Jobcomplete.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
    '            txtSumWeight_JobComplete.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
    '            txtSumVolume_JobComplete.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    Private Sub LoadgrdJobComplete()
        'update best data reader 20-08-2012
        Dim objtbManifest As New tb_TransportManifest
        Try
            objtbManifest.getTransportManifest_Summary(sqlCondition(grdJob_Complete.Name), 5, _IsSubManifest)
            Me.grdJob_Complete.DataSource = objtbManifest.DataTable
            If grdJob_Complete.RowCount = 0 Then
                txtSumDoc_JobComplete.Text = "0"
                txtSumQty_Jobcomplete.Text = "0"
                txtSumWeight_JobComplete.Text = "0.0000"
                txtSumVolume_JobComplete.Text = "0.0000"
            Else
                txtSumDoc_JobComplete.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
                txtSumQty_Jobcomplete.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
                txtSumWeight_JobComplete.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtSumVolume_JobComplete.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

            End If

        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    'Private Sub LoadgrdJobReturn()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobReturn.Name), 5, _IsSubManifest)
    '        Me.grdJobReturn.DataSource = objtbManifest.DataTable
    '        If grdJobReturn.RowCount = 0 Then
    '            txtSumDoc_JobReturn.Text = "0"
    '            txtSumQty_JobReturn.Text = "0"
    '            txtSumWeight_JobReturn.Text = "0.0000"
    '            txtSumVolume_JobReturn.Text = "0.0000"
    '        Else
    '            txtSumDoc_JobReturn.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
    '            txtSumQty_JobReturn.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
    '            txtSumWeight_JobReturn.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
    '            txtSumVolume_JobReturn.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    Private Sub LoadgrdJobReturn()
        'update best 20-08-2012
        'data table เป็น data reader
        Dim objtbManifest As New tb_TransportManifest
        Try
            'objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobReturn.Name), 5, _IsSubManifest)
            objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobReturn.Name), "13,14,15", _IsSubManifest)
            Me.grdJobReturn.DataSource = objtbManifest.DataTable
            If grdJobReturn.RowCount = 0 Then
                txtSumDoc_JobReturn.Text = "0"
                txtSumQty_JobReturn.Text = "0"
                txtSumWeight_JobReturn.Text = "0.0000"
                txtSumVolume_JobReturn.Text = "0.0000"
            Else
                txtSumDoc_JobReturn.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
                txtSumQty_JobReturn.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
                txtSumWeight_JobReturn.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtSumVolume_JobReturn.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

            End If

        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    'Private Sub LoadgrdJobInTransport()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_InTranport(sqlCondition(grdJobLoading.Name), _IsSubManifest)
    '        Me.grdJobInTransport.DataSource = objtbManifest.DataTable
    '        If grdJobInTransport.RowCount = 0 Then
    '            txtSumDoc_JobInTransport.Text = "0"
    '            txtSumQty_JobInTransport.Text = "0"
    '            txtSumWeight_JobReturn.Text = "0.0000"
    '            txtSumVolume_JobInTransport.Text = "0.0000"
    '        Else
    '            txtSumDoc_JobInTransport.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
    '            txtSumQty_JobInTransport.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
    '            txtSumWeight_JobInTransport.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
    '            txtSumVolume_JobInTransport.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    Private Sub LoadgrdJobInTransport()
        'best update 
        '20-08-2012
        'datatable เป็น reader
        Dim objtbManifest As New tb_TransportManifest
        Try
            objtbManifest.getTransportManifest_InTranport(sqlCondition(grdJobLoading.Name), _IsSubManifest)
            Me.grdJobInTransport.DataSource = objtbManifest.DataTable
            If grdJobInTransport.RowCount = 0 Then
                txtSumDoc_JobInTransport.Text = "0"
                txtSumQty_JobInTransport.Text = "0"
                txtSumWeight_JobReturn.Text = "0.0000"
                txtSumVolume_JobInTransport.Text = "0.0000"
            Else
                txtSumDoc_JobInTransport.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
                txtSumQty_JobInTransport.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
                txtSumWeight_JobInTransport.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtSumVolume_JobInTransport.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

            End If

        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    'Private Sub LoadgrdManifest_Summary()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobLoading.Name), "-1,1,3,2,6,7", _IsSubManifest)
    '        Me.grdJobLoading.DataSource = objtbManifest.DataTable
    '        If grdJobLoading.RowCount = 0 Then
    '            txtSumDoc_JobLoading.Text = "0"
    '            txtSumQty_JobLoading.Text = "0"
    '            txtSumWeight_JobLoading.Text = "0.0000"
    '            txtSumVolume_JobLoading.Text = "0.0000"
    '        Else
    '            txtSumDoc_JobLoading.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
    '            txtSumQty_JobLoading.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
    '            txtSumWeight_JobLoading.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
    '            txtSumVolume_JobLoading.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    
    Private Sub LoadgrdManifest_Summary()
        'best update 
        '20-08-2012
        'datatable เป็น reader
        Dim objtbManifest As New tb_TransportManifest
        Try
            'objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobLoading.Name), "-1,1,3,2,6,7", _IsSubManifest)
            objtbManifest.getTransportManifest_Summary(sqlCondition(grdJobLoading.Name), "-1,1,3,2,6,7,16", _IsSubManifest)
            Me.grdJobLoading.DataSource = objtbManifest.DataTable
            If grdJobLoading.RowCount = 0 Then
                txtSumDoc_JobLoading.Text = "0"
                txtSumQty_JobLoading.Text = "0"
                txtSumWeight_JobLoading.Text = "0.0000"
                txtSumVolume_JobLoading.Text = "0.0000"
            Else
                txtSumDoc_JobLoading.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
                txtSumQty_JobLoading.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
                txtSumWeight_JobLoading.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtSumVolume_JobLoading.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub
    Private Function sqlCondition(ByVal dataGridName As String)
        Try
            Dim strWhere As String = ""
            Dim intDay As Integer = Me.dtpDate.Value.Day
            Dim intMonth As Integer = Me.dtpDate.Value.Month
            Dim intYear As Integer = Me.dtpDate.Value.Year
            Dim StartDate As String = Format(dtpDate.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
            Dim EndtDate As String = Format(dateEnd.Value, "yyyy/MM/dd").ToString() + "  23:59:00"


            If rdbTransportManifest_Date.Checked = True Then
                strWhere &= " and TransportManifest_Date Between '" & StartDate & "' and '" & EndtDate & "'"
            End If

            If rdoTransportManifest_No.Checked = True Then
                strWhere &= " and TransportManifest_No like '%" & txtKeySearch.Text & "'"
            End If

            'If rdbSO_No.Checked = True Then
            '    strWhere &= " and SalesOrder_No like '" & txtKeySearch.Text & "%'"
            'End If

            'If rdbCustomer.Checked = True Then
            '    strWhere &= " and Customer_Name like '" & txtKeySearch.Text & "%'"
            'End If

            'If rdoCustomerShipping.Checked = True Then
            '    strWhere &= " and Company_Name like '" & txtKeySearch.Text & "%'"
            'End If
            If rdbCustomer.Checked = True Then
                If txtKeySearch.Text.Trim <> "" Then
                    If Len(txtKeySearch.Text) < 3 Then
                        W_MSG_Information("กรุณากรอกสามตัวอักษรขึ้นไป")
                        Return ""
                    End If
                    strWhere &= " AND TransportManifest_Index IN ( "
                    strWhere &= " 	 SELECT TransportManifest_Index "
                    strWhere &= " 	 FROM tb_TransportManifestItem "
                    strWhere &= " 	 WHERE Status <> -1 "
                    strWhere &= " 	 AND SalesOrder_Index IN ( "
                    strWhere &= " 		 SELECT SalesOrder_Index "
                    strWhere &= " 		 FROM tb_SalesOrderPackingItem "
                    strWhere &= " 		 WHERE Barcode_BOX LIKE '%" & txtKeySearch.Text.ToString & "%'"
                    strWhere &= " 		 GROUP BY SalesOrder_Index "
                    strWhere &= " 	 ) "
                    strWhere &= " )"

                    'strWhere &= " and Customer_Index like '" & txtKeySearch.Tag.ToString & "%'"
                End If

            End If
            If rdoCustomerShipping.Checked = True Then
                If txtKeySearch.Text.Trim <> "" Then
                    If Len(txtKeySearch.Text) < 3 Then
                        W_MSG_Information("กรุณากรอกสามตัวอักษรขึ้นไป")
                        Return ""
                    End If
                    strWhere &= " and Customer_Shipping_Index like '" & txtKeySearch.Tag.ToString & "%'"
                End If
            End If
            If rdoDriver.Checked = True Then
                If txtKeySearch.Text.Trim <> "" Then
                    If Len(txtKeySearch.Text) < 3 Then
                        W_MSG_Information("กรุณากรอกสามตัวอักษรขึ้นไป")
                        Return ""
                    End If
                    strWhere &= " and Driver_Index like '" & txtKeySearch.Tag.ToString & "%'"
                End If

            End If

            If rdoVehicleNo.Checked = True Then
                If Len(txtKeySearch.Text) < 3 Then
                    W_MSG_Information("กรุณากรอกสามตัวอักษรขึ้นไป")
                    Return ""
                End If
                strWhere &= " and Vehicle_No like '" & txtKeySearch.Text & "%'"
            End If

            If rdoContainerNo.Checked = True Then
                strWhere &= " and (Container_No1 like '" & txtKeySearch.Text & "%'"
                strWhere &= " or Container_No2 like '" & txtKeySearch.Text & "%')"
            End If

            If cboTransportJobType.SelectedValue <> "-11" Then
                strWhere &= " and TransportJobType_Index = '" & cboTransportJobType.SelectedValue.ToString & "'"
            End If
            If cboDistributionCenter.SelectedValue <> "-11" Then
                strWhere &= " and DistributionCenter_Index = '" & cboDistributionCenter.SelectedValue.ToString & "'"
            End If
            If cboRoute.SelectedValue <> "-11" Then
                strWhere &= " and Route_Index = '" & cboRoute.SelectedValue.ToString & "'"
            End If
            If cboSubRoute.SelectedValue <> "-11" Then
                strWhere &= " and SubRoute_Index = '" & cboSubRoute.SelectedValue.ToString & "'"
            End If
            If cboVehicleType.SelectedValue <> "-11" Then
                strWhere &= " and VehicleType_Index = '" & cboVehicleType.SelectedValue.ToString & "'"
            End If
            If cboDocumentType.SelectedValue <> "-11" Then
                strWhere &= " and DocumentType_Index = '" & cboDocumentType.SelectedValue.ToString & "'"
            End If
            Return strWhere

        Catch ex As Exception
            Throw ex
        End Try
    End Function





    Private Sub btnReleaseTruck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReleaseTruck.Click
        Try
            '1:          รอจัดสินค้าขึ้นรถ()
            '2:          จัดสินค้าขึ้นรถแล้ว()
            '3:          กำลังจัดสินค้า()
            '4:          กำลังจัดส่ง()
            '5:          เสร็จสิ้น()
            '6:          กำลังจัดสินค้า()
            '7	        รอส่งใหม่/ค้างส่ง
            '11     	ถึงผู้รับ/ถึงศูนย์กระจาย
            '12:         กระจายสินค้า()
            '13:         รอคืนบิล()
            '14:         คืนบิล(DC)
            '15:         คืนบิลต้นทาง()

            Dim chkSave As Boolean = False
            If Me.grdJobLoading.RowCount = 0 Then Exit Sub
            CType(grdJobLoading.DataSource, DataTable).AcceptChanges()
            Dim dtpValue As New DataTable
            Dim odrValue As DataRow
            Dim oGetconfig As New config_CustomSetting
            Dim strUse_Status_ReleaseTruck As String = ""

            Dim TransportManifestIndex As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value


            dtpValue = CType(grdJobLoading.DataSource, DataTable).Clone

            Dim drJobLoading() As DataRow
            Dim chkLOADING_CONFIRM As Boolean = False
            Dim chkUSE_STATUS_RELEASETRUCK_2 As Boolean = False
            strUse_Status_ReleaseTruck = oGetconfig.getConfig_Key_DEFUALT("STATUS_RELEASETRUCK")


            chkLOADING_CONFIRM = oGetconfig.getConfig_Key_USE("USE_TRANSPORT_LOADING_CONFIRM")
            chkUSE_STATUS_RELEASETRUCK_2 = oGetconfig.getConfig_Key_USE("USE_STATUS_RELEASETRUCK_2")
            If chkLOADING_CONFIRM Then
                drJobLoading = CType(grdJobLoading.DataSource, DataTable).Select(" TransportManifest_Index = '" & TransportManifestIndex & "'  and Status_Id  IN (" & strUse_Status_ReleaseTruck & ")", "")
            Else
                drJobLoading = CType(grdJobLoading.DataSource, DataTable).Select(" TransportManifest_Index = '" & TransportManifestIndex & "'  and Status_Id <> 2", "") 'รอปล่อยรถ
                If drJobLoading.Length > 0 Then
                    If W_MSG_Confirm("มีรายการที่ยังไม่โหลดสินค้า คุณต้องการปล่อยรถใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                drJobLoading = CType(grdJobLoading.DataSource, DataTable).Select(" TransportManifest_Index = '" & TransportManifestIndex & "' ", "")
            End If



            For Each odr As DataRow In drJobLoading
                If odr("Vehicle_License_No").ToString = "" Then
                    W_MSG_Information("ใบคุมรถเลขที่ " & odr("TransportManifest_No").ToString & " ยังไม่ได้ระบุทะเบียนรถ.")
                    Exit Sub
                End If

                'Update By Art
                'Update Date 30/04/2012
                'ตรวจสอบการปล่อยรถแบบที่จัดสิ้นค้าไม่เรียบร้อยได้
                If chkUSE_STATUS_RELEASETRUCK_2 Then
                    If chkLOADING_CONFIRM Then
                        'กรองข้อมูลสถานะใบคุม ยังไม่เรียบร้อย
                        If odr("Status_Id").ToString = "2" Then
                            W_MSG_Information("ใบคุมรถเลขที่ " & odr("TransportManifest_No").ToString & "จัดสิ้นค้าไม่เรียบร้อย")
                            Exit Sub
                        End If
                    End If
                End If




                'ขาดทุนธรรมดา
                If CDbl(odr("TotalTransportCharged")) < CDbl(odr("TotalTransportPaid")) Then 'ขาดทุน
                    'ขาดทุน 10%
                    Dim dblTRANSPORTCHARGE_PERCENTLOSS As Double = 0
                    Dim dblChargePercent As Double = 0
                    Dim oConfig As New config_CustomSetting
                    dblTRANSPORTCHARGE_PERCENTLOSS = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(oConfig.getConfig_Key_DEFUALT("DEFAULT_TRANSPORTCHARGE_PERCENTLOSS"), GetType(Double))

                    dblChargePercent = ((CDbl(odr("TotalTransportCharged")) - CDbl(odr("TotalTransportPaid"))) / CDbl(odr("TotalTransportPaid"))) * 100
                    If dblTRANSPORTCHARGE_PERCENTLOSS > 0 Then
                        If (Math.Abs(dblChargePercent) >= dblTRANSPORTCHARGE_PERCENTLOSS) Then 'SupperAdmin Apporve
                            If W_MSG_Confirm("ใบคุมรถเลขที่ " & odr("TransportManifest_No").ToString & Chr(13) & " เที่ยวนี้ ขาดทุนมากกว่า " & dblTRANSPORTCHARGE_PERCENTLOSS & " % (ติดต่อ SuperAdmin)" & Chr(13) & "  ท่านต้องการปล่อยรถใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then Exit Sub
                            Dim frmpassword As New WMS_STD_Master.PopupEnterPassword

                            Dim strGROUP_USER As String = ""

                            strGROUP_USER = oGetconfig.getConfig_Key_DEFUALT("GROUP_USER_SUPERVISOR")
                            'If chkGROUP_USER = True Then

                            'End If

                            'frmpassword.Group = strGROUP_USER '"0010000000007" '
                            'frmpassword.isSupper_Admin = True
                            frmpassword.ShowDialog()
                            If frmpassword.passwordistrue = False Then
                                Exit Sub
                            End If
                        Else
                            If W_MSG_Confirm("ใบคุมรถเลขที่ " & odr("TransportManifest_No").ToString & Chr(13) & " เที่ยวนี้ขาดทุน (ติดต่อ Admin)" & Chr(13) & "  ท่านต้องการปล่อยรถใช่หรือไม่  ?") = Windows.Forms.DialogResult.No Then Exit Sub
                            Dim frmpassword As New WMS_STD_Master.PopupEnterPassword
                            'frmpassword.Group = "0010000000001"
                            frmpassword.ShowDialog()
                            If frmpassword.passwordistrue = False Then
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    'ปล่อยรถธรรมดา()
                    If W_MSG_Confirm_ByIndex(100032) = Windows.Forms.DialogResult.No = True Then
                        Exit Sub
                    End If
                End If


                odrValue = dtpValue.NewRow
                odrValue.ItemArray = odr.ItemArray.Clone
                dtpValue.Rows.Add(odrValue)

            Next

            If dtpValue.Rows.Count = 0 Then
                W_MSG_Information("กรุณาเลือกรายการที่จัดรถแล้ว")
                Exit Sub
            End If

            Dim frm As New frmTruckRelease_Update
            frm.IsSubManifest = _IsSubManifest
            frm.dtJobLoading = dtpValue
            frm.ShowDialog()

            LoadDataGrid()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnReceiveDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceiveDestination.Click
        Try

            Dim chkSave As Boolean = False
            If Me.grdJobInTransport.RowCount = 0 Then Exit Sub

            Dim dtpValue As New DataTable
            Dim odrValue As DataRow

            dtpValue = CType(grdJobInTransport.DataSource, DataTable).Clone
            For Each odr As DataRow In CType(grdJobInTransport.DataSource, DataTable).Select("chkSelect=1 and Status_Id in(4)", "chkSelect")
                odrValue = dtpValue.NewRow
                odrValue.ItemArray = odr.ItemArray.Clone
                dtpValue.Rows.Add(odrValue)
            Next

            If dtpValue.Rows.Count = 0 Then Exit Sub

            If W_MSG_Confirm_ByIndex(100033) = Windows.Forms.DialogResult.Yes = True Then
                Dim frm As New WMS_STD_OUTB_Transport.frmTruckToDC
                frm.IsSubManifest = _IsSubManifest

                frm.dtJobLoading = dtpValue
                frm.ShowDialog()

                'LoadDataGrid()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Try
            Dim chkSave As Boolean = False
            If Me.grdJobInTransport.RowCount = 0 Then Exit Sub

            Dim dtpValue As New DataTable
            Dim odrValue As DataRow

            dtpValue = CType(grdJobInTransport.DataSource, DataTable).Clone
            For Each odr As DataRow In CType(grdJobInTransport.DataSource, DataTable).Select("chkSelect=1 and Status_Id in(11,4)", "chkSelect")
                odrValue = dtpValue.NewRow
                odrValue.ItemArray = odr.ItemArray.Clone
                dtpValue.Rows.Add(odrValue)
            Next

            If dtpValue.Rows.Count = 0 Then Exit Sub

            If W_MSG_Confirm_ByIndex(100037) = Windows.Forms.DialogResult.Yes = True Then
                'Dim dtTemp As New DataTable
                'dtTemp = grdJobLoading.DataSource
                'Dim drItem() As DataRow = dtTemp.Select("chkSelect=1", "chkSelect")
                Dim frm As New frmTruckReturn
                frm.IsSubManifest = _IsSubManifest

                frm.dtJobLoading = dtpValue
                frm.ShowDialog()

                'LoadDataGrid()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If rdoTransportManifest_No.Checked = True Then
                If Len(txtKeySearch.Text) < 3 Then
                    W_MSG_Information("กรุณากรอกสามตัวอักษรขึ้นไป")
                    Exit Sub
                End If
            End If
            LoadDataGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If Me.grdJobReturn.RowCount = 0 Then Exit Sub
            Dim strTransportManifest_No As String = Me.grdJobReturn.CurrentRow.Cells("col_TransportManifest_No_JobReturn").Value.ToString
            'Dim odrValue() As DataRow
            CType(grdJobReturn.DataSource, DataTable).AcceptChanges()

            'odrValue = CType(grdJobReturn.DataSource, DataTable).Select("chkSelect=1")
            'If odrValue.Length = 0 Then Exit Sub
            'Dim frm As New frmTransportSO_POD_Update_LP
            'frm.drArrTransportManifest = odrValue

            '''''''best up date 01-09-2012''''''''
            'odrValue = CType(grdJobReturn.DataSource, DataTable).Select("chkSelect=1")
            'If odrValue.Length = 0 Then Exit Sub
            If strTransportManifest_No = "" Then Exit Sub
            Dim frm As New frmTransportSO_POD_Update_LP
            'frm.drArrTransportManifest = odrValue
            '''''''end best up date 01-09-2012''''

            frm.IsSubManifest = _IsSubManifest
            frm.IsSubManifest_No = strTransportManifest_No
            frm.ShowDialog()
            '   frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop_Search.Click
        Try
            If rdbCustomer.Checked = True Then
                Dim frm As New frmCustmer_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomer_Index As String = ""
                tmpCustomer_Index = frm.Customer_Index 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                If Not tmpCustomer_Index = "" Then
                    Me.txtKeySearch.Text = frm.customerName
                    Me.txtKeySearch.Tag = frm.Customer_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If
            If rdoCustomerShipping.Checked = True Then
                Dim frm As New frmConsignee_Popup
                frm.Customer_Index = ""
                frm.ShowDialog()

                Dim tmp_Index As String = ""
                tmp_Index = frm.Consignee_Index

                If Not tmp_Index = "" Then

                    Me.txtKeySearch.Text = frm.Consignee_Name
                    Me.txtKeySearch.Tag = frm.Consignee_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If

            If rdoDriver.Checked = True Then
                Dim frm As New frmDriver_Popup
                frm.isDriverManifest = False
                frm.TransportManifest_Index = "" 'Me._TransportManifest_Index
                frm.ShowDialog()

                Dim tmp_Index As String = ""
                tmp_Index = frm.Driver_Index

                If Not tmp_Index = "" Then

                    Me.txtKeySearch.Text = frm.Driver_Name
                    Me.txtKeySearch.Tag = frm.Driver_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbTransportManifest_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTransportManifest_Date.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoTransportManifest_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTransportManifest_No.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoCustomerShipping_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCustomerShipping.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoVehicleNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVehicleNo.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoDriver_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDriver.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoContainerNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoContainerNo.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub closeAllCheckbox(ByVal rdoEnble As RadioButton)
        Try
            Dim radName As String = rdoEnble.Name.ToString

            Select Case radName
                Case "rdbTransportManifest_Date"
                    dtpDate.Visible = True
                    lb_to.Visible = True
                    dateEnd.Visible = True
                    txtKeySearch.Visible = False
                Case ""
                    dtpDate.Visible = True
                    lb_to.Visible = True
                    dateEnd.Visible = True
                    txtKeySearch.Visible = False

                Case Else
                    dtpDate.Visible = False
                    lb_to.Visible = False
                    dateEnd.Visible = False
                    txtKeySearch.Visible = True
            End Select
            Me.txtKeySearch.Text = ""
            Me.txtKeySearch.Tag = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = grdJobLoading.Rows.Count
        If numRows > 0 Then
            lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows.Text = "ไม่พบรายการ"
        End If

        numRows = grdJobInTransport.Rows.Count
        If numRows > 0 Then
            lbCountRows2.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows2.Text = "ไม่พบรายการ"
        End If
        numRows = grdJobReturn.Rows.Count
        If numRows > 0 Then
            lbCountRows3.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows3.Text = "ไม่พบรายการ"
        End If
        numRows = grdJob_Complete.Rows.Count
        If numRows > 0 Then
            lbCountRows4.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows4.Text = "ไม่พบรายการ"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub




    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            If grdJobLoading.RowCount = 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(100036) = Windows.Forms.DialogResult.Yes = True Then
                Dim strTransportManifest_Index As String = Me.grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString
                Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.COPY, strTransportManifest_Index)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnConfirm_Dc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If Me.grdJobInTransport.RowCount = 0 Then Exit Sub

            Dim odrValue() As DataRow
            CType(grdJobInTransport.DataSource, DataTable).AcceptChanges()
            odrValue = CType(grdJobInTransport.DataSource, DataTable).Select("chkSelect=1")

            If odrValue.Length = 0 Then Exit Sub

            Dim frm As New WMS_STD_OUTB_Transport.frmTransportSO_POD
            frm.drArrTransportManifest = odrValue
            frm.IsSubManifest = _IsSubManifest
            frm.ShowDialog()

            ''Dim frm As New frmTransportDeliveryResult
            'Dim frm As New frmTransportSO_POD
            'frm.TransportManifest_Index = grdJobInTransport.CurrentRow.Cells("col_Index_JobInTransport").Value.ToString
            'frm.IsSubManifest = _IsSubManifest
            'frm.ShowDialog()
            '   frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddManifest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddManifest.Click
        Try
            Dim strTransportManifest_Index As String = ""
            Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.ADD, strTransportManifest_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdJobLoading_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdJobLoading.CellDoubleClick
        Try
            If grdJobLoading.RowCount = 0 Then Exit Sub
            Dim strTransportManifest_Index As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString
            Me.AddEditTransportManifest(frmTransportManifest_Update.Manifest_Mode.EDIT, strTransportManifest_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub AddEditTransportManifest(ByVal pMode As frmTransportManifest_Update.Manifest_Mode, ByVal pstrTransportManifest_Index As String)
        Try
            Select Case pMode
                Case frmTransportManifest_Update.Manifest_Mode.ADD
                    Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.ADD)
                    frm.IsSubManifest = _IsSubManifest
                    frm.ShowDialog()
                Case frmTransportManifest_Update.Manifest_Mode.EDIT
                    Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.EDIT)
                    frm.TransportManifest_Index = pstrTransportManifest_Index
                    frm.IsSubManifest = _IsSubManifest

                    frm.ShowDialog()

                Case frmTransportManifest_Update.Manifest_Mode.COPY
                    Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.COPY)
                    frm.TransportManifest_Index = pstrTransportManifest_Index
                    frm.IsSubManifest = _IsSubManifest
                    frm.ShowDialog()
                    Me.LoadgrdManifest_Summary()
            End Select

            'Me.LoadgrdManifest_Summary()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdJobLoading.RowCount > 0 Then
                For iRow As Integer = 0 To grdJobLoading.RowCount - 1
                    grdJobLoading.Rows(iRow).Cells("chkItem_JobLoading").Value = Me.chkSelectAll.Checked
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkSelectAll1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll1.CheckedChanged
        Try
            If grdJobInTransport.RowCount > 0 Then
                For iRow As Integer = 0 To grdJobInTransport.RowCount - 1
                    grdJobInTransport.Rows(iRow).Cells("chkSelect_JobInTransport").Value = Me.chkSelectAll1.Checked
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub chkSelectAll2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If grdJobReturn.RowCount > 0 Then
    '            For iRow As Integer = 0 To grdJobReturn.RowCount - 1
    '                grdJobReturn.Rows(iRow).Cells("chkSelect_JobReturn").Value = Me.chkSelectAll2.Checked
    '            Next
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
    Private Sub frmTransportManifestView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransport
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 24)
                    oFunction.SW_Language_Column(Me, Me.grdJob_Complete, 24)
                    oFunction.SW_Language_Column(Me, Me.grdJob_Complete, 24)
                    oFunction.SW_Language_Column(Me, Me.grdJobInTransport, 24)
                    oFunction.SW_Language_Column(Me, Me.grdJobLoading, 24)
                    oFunction.SW_Language_Column(Me, Me.grdJobReturn, 24)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If grdJobLoading.RowCount = 0 Then Exit Sub
            Dim strTransportManifest_Index As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString

            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And TransportManifest_Index ='" & strTransportManifest_Index & "'")
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()


            'If grdJobLoading.RowCount = 0 Then Exit Sub
            'Dim oconfig_Report As New config_Report
            'Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            'Try

            '    Dim strTransportManifest_Index As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString
            '    Dim strTransportManifest_No As String = grdJobLoading.CurrentRow.Cells("col_TransportManifest_No_JobLoading").Value.ToString

            '    If Report_Name = "rptSumBox" Then
            '        Dim obj As New frmReportViewerMain
            '        obj.TransportManifest_No = grdJobLoading.CurrentRow.Cells("col_TransportManifest_No_JobLoading").Value.ToString
            '        obj.Report_Name = Report_Name
            '        obj.ShowDialog()
            '    Else
            '        Dim frm As New frmReportMainTransportManifest_Update
            '        frm.Report_Name = Report_Name
            '        frm.Document_No = strTransportManifest_No
            '        frm.Document_Index = strTransportManifest_Index

            '        'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TransportManifest_Index ='" & _TransportManifest_Index & "'")
            '        frm.ShowDialog()
            '    End If




            '    '###################################
            'Catch ex As Exception
            '    W_MSG_Error(ex.Message)
            'Finally
            'End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            Dim BarcodeGroup As String = "" 'Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Group").Value

            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            oCrystal = clsPacking.GetReportInfoST_G("rptWarranty_TOR", " AND Barcode_GROUP = '" & BarcodeGroup & "' ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnInterface_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterface.Click
        Try
            If grdJobLoading.CurrentRow Is Nothing OrElse grdJobLoading.CurrentRow.Index < 0 Then
                Exit Sub
            End If

            Dim TransportManifestIndex As String = grdJobLoading.CurrentRow.Cells("col_Index_JobLoading").Value.ToString

            Dim Service As New WMS_Site_Topcharoen_Interface.WMS_Interface
            If Service.IsConfirmGI(TransportManifestIndex) Then
                If W_MSG_Confirm("ใบคุมรถนี้ ส่ง Interface OMS ไปแล้ว" & vbCrLf & "ต้องการส่ง Interface อีกครั้ง ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            Else
                If W_MSG_Confirm("ยืนยันการส่ง Interface ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If

INTERFACE_OMS:
            Dim Result As String = Service.ConfirmGI(TransportManifestIndex, W_Module.WV_UserName)
            If Not String.IsNullOrEmpty(Result) Then
                If W_MSG_Confirm("Interface OMS ผิดพลาด" & vbCrLf & Result & vbCrLf & "ต้องการส่ง Interface OMS อีกครั้งใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                    GoTo INTERFACE_OMS
                End If
            End If

            W_MSG_Information("ส่ง Interface OMS เสร็จสิ้น")

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

End Class