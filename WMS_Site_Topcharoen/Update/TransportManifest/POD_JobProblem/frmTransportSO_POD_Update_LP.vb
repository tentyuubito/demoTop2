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
Imports WMS_STD_OUTB_Transport
Imports System.Threading
Imports System.Globalization

Public Class frmTransportSO_POD_Update_LP

    Private _odtTransportManifest_OnTruck As New DataTable
    Dim tmpRow_Index As Integer = 0
    Dim tmpColumn_Name As String = ""
    Private _IsUSE_TRANSPORT_DATETIME As Boolean = True
    Private _CustomFormat_Date As String = "dd/MM/yyyy"
    Private _TransportManifest_Index As String = ""
    Dim GetSystemDatePattern As String
    'Public Property TransportManifest_Index() As String
    '    Get
    '        Return _TransportManifest_Index
    '    End Get
    '    Set(ByVal Value As String)
    '        _TransportManifest_Index = Value
    '    End Set
    'End Property

    Private _TransportManifestItem_Index As String = ""
    'Public Property TransportManifestItem_Index() As String
    '    Get
    '        Return _TransportManifestItem_Index
    '    End Get
    '    Set(ByVal Value As String)
    '        _TransportManifestItem_Index = Value
    '    End Set
    'End Property


    Private _drArrTransportManifest() As DataRow
    Public Property drArrTransportManifest() As DataRow()
        Get
            Return _drArrTransportManifest
        End Get
        Set(ByVal value As DataRow())
            _drArrTransportManifest = value
        End Set
    End Property

    Private _IsSubManifest As Integer = 0

    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property

    Private _IsSubManifest_No As String = ""

    Public Property IsSubManifest_No() As String
        Get
            Return _IsSubManifest_No
        End Get
        Set(ByVal value As String)
            _IsSubManifest_No = value
        End Set
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    'Private _odtManifestItem As New DataTable

    Private Sub LoadgrdItem_OnTruck(ByVal Where_VIEW_TransportManifest_OnTruck As String)
        Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
        Try
            objtbManifest.getTransportManifest_DeliveryRrsult(Where_VIEW_TransportManifest_OnTruck, "5", "")
            Dim odtManifestItem As New DataTable
            odtManifestItem = objtbManifest.DataTable
            'Set Primary key
            odtManifestItem.PrimaryKey = New DataColumn() {odtManifestItem.Columns("TransportManifestItem_Index")}

            If odtManifestItem.Rows.Count = 0 Then
                W_MSG_Information_ByIndex(13)
                If grdSoView_Check.RowCount = 1 Then Exit Sub
                grdSoView_Check.Rows.RemoveAt(grdSoView_Check.CurrentRow.Index)
                Exit Sub
            End If

            For Each drDefualtStatus As DataRow In odtManifestItem.Rows
                drDefualtStatus("Status_Manifest") = 5
            Next

            If Me.grdSoView_Check.DataSource Is Nothing Then
                Me.grdSoView_Check.DataSource = odtManifestItem
            Else
                Dim odtTemp As New DataTable
                odtTemp = Me.grdSoView_Check.DataSource
                If odtTemp Is Nothing Then odtTemp = odtManifestItem.Clone
                odtManifestItem.Merge(odtTemp)
                Me.grdSoView_Check.DataSource = odtManifestItem
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    Private Sub frmTransportDeliveryResult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")

            grdSoView_Check.AutoGenerateColumns = False
            getDataGridSalesOrder_Status()
            getDataUpdateAllSalesOrder_Status()
            Me.txtTransportManifest_no.Text = _IsSubManifest_No
            If _IsSubManifest_No <> "" Then
                SearchData_By_TransportManifest_No()
                Enablesomerows()
            End If
            Me.grdSoView_Check.AutoGenerateColumns = False
            'Me.grdItem_ToLoad.AutoGenerateColumns = False
            Me.txtTransportManifest_no.Focus()
            'Me.cboConditionSort.SelectedIndex = 0

            GetSystemDatePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern

            'GetSystemDatePattern = AppSettings("GetFormatDate")

            config_Transport()
            Me.AddCurrencyTextBox()
            'get Status
            SetMaskText()
            Me.SetCalenda_Format()
            'getComboSalesOrder_Status()

            config_Transport()


            'If Me._drArrTransportManifest IsNot Nothing Then
            '    If Me._drArrTransportManifest.Length > 0 Then
            '        For Each drSelect As DataRow In Me._drArrTransportManifest
            '            LoadgrdItem_OnTruck(" and TransportManifest_Index='" & drSelect("TransportManifest_Index").ToString & "' ")
            '        Next
            '    Else
            '        Me.pnlTransportManifest.Visible = True
            '    End If
            'Else
            '    Me.ClearPnlPod()
            '    Me.pnlTransportManifest.Visible = True
            'End If




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub Enablesomerows()
        Try
            For iRow As Integer = 0 To grdSoView_Check.Rows.Count - 1

                If grdSoView_Check.Rows(iRow).Cells("cboSalesOrder_Status").Value.ToString = "5" Then
                    grdSoView_Check.Rows(iRow).ReadOnly = True
                End If


            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub AddCurrencyTextBox()
        Try
            'AddHandler Me.txtSumPrice_In.KeyPress, AddressOf Me.keypressed
            'AddHandler Me.txtSumPrice_Out.KeyPress, AddressOf Me.keypressed
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub

    Private Sub SetCalenda_Format()
        Try
            Dim strMaskText As String = "00/00/0000"
            If Not _IsUSE_TRANSPORT_DATETIME Then
                _CustomFormat_Date = "dd/MM/yyyy"
                Me.col_Time_DeliveryToDestination.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToDC.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToSource.DefaultCellStyle.Format = "d"
                Me.col_Time_DocConfirmedByOwner.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToOwner.DefaultCellStyle.Format = "d"
                'Me.col_Expected_Delivery_Date.DefaultCellStyle.Format = "d"
                'Me.col_Time_ExpectedDocPickup.DefaultCellStyle.Format = "d"
            Else
                strMaskText = "00/00/0000 90:00"
                _CustomFormat_Date = "dd/MM/yyyy HH:mm"
            End If

            'MaskText(DataGridView)
            Me.col_Time_DeliveryToDestination.Mask = strMaskText
            Me.col_Time_DocReturnedToDC.Mask = strMaskText
            Me.col_Time_DocReturnedToSource.Mask = strMaskText
            Me.col_Time_DocConfirmedByOwner.Mask = strMaskText

            'Me.dtpTime_DeliveryToDestination.CustomFormat = _CustomFormat_Date
            'Me.dtpTime_DocReturnedToDC.CustomFormat = _CustomFormat_Date
            Me.dtpTime_DocReturnedToOwner.CustomFormat = _CustomFormat_Date
            'Me.dtpTime_DocConfirmedByOwner.CustomFormat = _CustomFormat_Date
            'Me.dtpTime_DocReturnedToSource.CustomFormat = _CustomFormat_Date




        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub SetMaskText()
        Try
            Dim strMaskText As String = "00/00/0000"
            If Not _IsUSE_TRANSPORT_DATETIME Then
                strMaskText = "00/00/0000"
                Me.col_Time_DeliveryToDestination.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToDC.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToSource.DefaultCellStyle.Format = "d"
                Me.col_Time_DocConfirmedByOwner.DefaultCellStyle.Format = "d"
                Me.col_Time_DocReturnedToOwner.DefaultCellStyle.Format = "d"

                'Me.col_Expected_Delivery_Date.DefaultCellStyle.Format = "d"
                'Me.col_Time_ExpectedDocPickup.DefaultCellStyle.Format = "d"
            Else
                strMaskText = "00/00/0000 90:00"
            End If

            'MaskText TextBox
            'Me.dtpTime_DeliveryToDestination.Format = strMaskText
            'Me.dtpTime_DocReturnedToDC.Format = strMaskText
            'Me.dtpTime_DocReturnedToOwner.Format = strMaskText
            ''Me.dtpTime_DocConfirmedByOwner.Value = strMaskText
            'Me.dtpTime_DocReturnedToSource.Format = strMaskText


            'AddHandler Me.dtpTime_DeliveryToDestination.Leave, AddressOf subchkMskTextTime
            'AddHandler Me.dtpTime_DocReturnedToDC.Leave, AddressOf subchkMskTextTime
            'AddHandler Me.dtpTime_DocReturnedToOwner.Leave, AddressOf subchkMskTextTime
            ''AddHandler Me.dtpTime_DocConfirmedByOwner.Leave, AddressOf subchkMskTextTime
            'AddHandler Me.dtpTime_DocReturnedToSource.Leave, AddressOf subchkMskTextTime

            'MaskText DataGridView

            'Me.col_Time_DeliveryToDestination.Mask = strMaskText
            'Me.col_Time_DocReturnedToDC.Mask = strMaskText
            Me.col_Time_DocReturnedToSource.Mask = strMaskText
            Me.col_Time_DocConfirmedByOwner.Mask = strMaskText
            Me.col_Time_DocReturnedToOwner.Mask = strMaskText



        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LoadgrdItem_NoResult(ByVal pstrWhere As String)
        Try
            'Dim objtb_SalesOrder As New SODeliveryResult_Update(SODeliveryResult_Update.enuOperation_Type.SEARCH)
            'Dim odtSoItem As New DataTable
            'objtb_SalesOrder.getSO_TODeliveryResult(pstrWhere)
            'odtSoItem = objtb_SalesOrder.DataTable
            'grdItem_ToLoad.DataSource = odtSoItem 
            grdTransport.AutoGenerateColumns = False
            Dim objTransport As New clsTransportSO_POD
            Dim odtTransport As New DataTable
            objTransport.getTransportManifest_Noregister("")
            odtTransport = objTransport.DataTable
            grdTransport.DataSource = odtTransport


            Me.lblCountRows.Text = FormatNumber(Me.grdTransport.RowCount, 0).ToString + Me.lblCountRows.Tag.ToString

        Catch ex As Exception
            Throw ex
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
                            _IsUSE_TRANSPORT_DATETIME = False
                            ' Me.col_Expected_Delivery_Date.DefaultCellStyle.Format = "d"

                            'Case "Receive_Destination"
                            '    col_Time_DocReturnedToDC.Visible = False
                            '    col_Time_DocReturnedToSource.Visible = False

                        Case "USE_CUSTOMER_SHIPPING_LOCATION"
                            col_CustomerShippingLocation.Visible = False
                        Case "USE_VOLUME_TRANSPORT"
                            'col_Volume.Visible = False
                        Case "USE_WEIGHT_TRANSPORT"
                            'col_Weight.Visible = False


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

    'Private Sub getComboSalesOrder_Status()

    '    Dim objDBType As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Dim objDTType As DataTable = New DataTable

    '    Try

    '        objDBType.getDeliveryResult_Status()
    '        objDTType = objDBType.DataTable
    '        With cboStatusManifest
    '            .DataSource = objDTType
    '            .DisplayMember = "Description"
    '            .ValueMember = "Status"

    '        End With
    '        '   cboSalesOrder_Status.ValueMember = objDTType.Rows(3).Item("SalesOrder_Status").ToString
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDBType = Nothing
    '        objDTType = Nothing

    '    End Try
    'End Sub
    Private Sub getDataUpdateAllSalesOrder_Status()

        Dim objDBType As New clsTransportSO_POD '(tb_TransportManifest.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try

            objDBType.getDeliveryResult_Status()
            objDTType = objDBType.DataTable
            With Me.cboUpdateAllSOStatus
                .DataSource = objDTType
                .DisplayMember = "Description"
                .ValueMember = "Status"
            End With

            Me.cboUpdateAllSOStatus.SelectedValue = 5 'objDTType.Rows(3).Item("SalesOrder_Status").ToString


        Catch ex As Exception
            Throw ex
        Finally
            'objDBType = Nothing
            'objDTType = Nothing

        End Try
    End Sub
    Private Sub getDataGridSalesOrder_Status()

        'Dim objDBType As New tb_TransportManifest_Update '(tb_TransportManifest.enuOperation_Type.SEARCH)
        Dim objDBType As New clsTransportSO_POD
        Dim objDTType As DataTable = New DataTable

        Try

            objDBType.getDeliveryResult_Status()
            objDTType = objDBType.DataTable
            With cboSalesOrder_Status
                .DataSource = objDTType
                .DisplayMember = "Description"
                .ValueMember = "Status"

            End With

      


        Catch ex As Exception
            Throw ex
        Finally
            'objDBType = Nothing
            'objDTType = Nothing

        End Try
    End Sub

    Private Sub grdSoView_Check_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSoView_Check.CellClick
        Try
            If grdSoView_Check.CurrentRow.Cells("col_TransportManifestItem_Index").Value Is Nothing Then Exit Sub
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
                Case "BTNDETAIL"
                    '      If CBool(grdSoView_Check.CurrentRow.Cells("col_chkResult").Value) Then
                    'Dim frm As New frmTransportProblem_Update(frmTransportProblem.WithDraw_Mode.ADD)
                    Dim frm As New frmTransportProblem(frmTransportProblem.WithDraw_Mode.ADD)
                    frm.TransportManifest_Index = Me._TransportManifest_Index
                    frm.TransportManifestItem_Index = grdSoView_Check.CurrentRow.Cells("col_TransportManifestItem_Index").Value.ToString
                    frm.IsSubManifest = Me._IsSubManifest
                    frm.Status_Manifest = grdSoView_Check.CurrentRow.Cells("cboSalesOrder_Status").Value
                    frm.ShowDialog()
                    grdSoView_Check.CurrentRow.Cells("cboSalesOrder_Status").Value = frm.Status_Manifest
                    grdSoView_Check.CurrentRow.Cells("col_chkResult").Value = frm.Problem_Status
                    '  SearchData_By_TransportManifest_No()
                    'If W_MSG_Confirm_ByIndex(100035) = Windows.Forms.DialogResult.Yes Then
                    '    grdSoView_Check.CurrentRow.Cells("col_chkResult").Value = frm.Problem_Status
                    'Else
                    '    grdSoView_Check.CurrentRow.Cells("col_chkResult").Value = False

                    'End If



                    '    End If

            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim dtJobProblem As New DataTable
            Dim dtObjCollectionJobProblem As New List(Of tb_TransportManifestItem)
            Dim datetime_col_Time_DeliveryToDestination As Date = Date.Today.ToString("yyyy/MM/dd hh:mm:ss")

            For i As Integer = 0 To grdSoView_Check.Rows.Count - 1
                Dim objJobProblem As New tb_TransportManifestItem
                With grdSoView_Check
                    objJobProblem.TransportManifestItem_Index = .Rows(i).Cells("col_TransportManifestItem_Index").Value.ToString
                    objJobProblem.TransportManifest_Index = .Rows(i).Cells("col_TransportManifest_Index").Value.ToString
                    'Time_DocReturnedToDC
                    'Time_DocReturnedToSource
                    'Time_DocReturnedToOwner
                    'Time_DocConfirmedByOwner
                    If .Rows(i).Cells("col_Time_DocReturnedToDC").Value.ToString = "" Then
                        objJobProblem.Time_DocReturnedToDC = Nothing
                    Else
                        objJobProblem.Time_DocReturnedToDC = .Rows(i).Cells("col_Time_DocReturnedToDC").Value.ToString
                    End If
                    If .Rows(i).Cells("col_Time_DocReturnedToSource").Value.ToString = "" Then
                        objJobProblem.Time_DocReturnedToSource = Nothing
                    Else
                        objJobProblem.Time_DocReturnedToSource = .Rows(i).Cells("col_Time_DocReturnedToSource").Value.ToString
                    End If
                    If .Rows(i).Cells("col_Time_DocReturnedToOwner").Value.ToString = "" Then
                        objJobProblem.Time_DocReturnedToOwner = Nothing
                    Else
                        objJobProblem.Time_DocReturnedToOwner = .Rows(i).Cells("col_Time_DocReturnedToOwner").Value.ToString
                    End If
                    If .Rows(i).Cells("col_Time_DocConfirmedByOwner").Value.ToString = "" Then
                        objJobProblem.Time_DocConfirmedByOwner = Nothing
                    Else
                        objJobProblem.Time_DocConfirmedByOwner = .Rows(i).Cells("col_Time_DocConfirmedByOwner").Value.ToString
                    End If
                    If .Rows(i).Cells("col_Time_DeliveryToDestination").Value.ToString = "" Then
                        objJobProblem.Time_DeliveryToDestination = Nothing
                    Else

                        If boolchkTimeFormat(i) = False Then
                            Me.grdSoView_Check.Rows(i).Cells("col_Time").Selected = True
                            Exit Sub
                        End If
                        objJobProblem.Time_DeliveryToDestination = CDate(CStr(CDate(grdSoView_Check.Rows(i).Cells("col_Time_DeliveryToDestination").Value).ToString("yyyy/MM/dd")) + " " + CDate(CStr(grdSoView_Check.Rows(i).Cells("col_Time").Value).Replace(".", ":").ToString).ToString("HH:mm")) '.ToString("yyyy/MM/dd hh:mm:ss")
                    End If
                    objJobProblem.Mile_AtDestination = .Rows(i).Cells("col_Mile_AtDestination").Value.ToString
                    objJobProblem.chk_Problem = CBool(.Rows(i).Cells("col_chkResult").Value.ToString)
                    objJobProblem.Comment = .Rows(i).Cells("col_Remark").Value.ToString
                    objJobProblem.SalesOrder_Index = .Rows(i).Cells("col_SalesOrder_Index").Value.ToString
                    If .Rows(i).Cells("cboSalesOrder_Status").Value IsNot Nothing Then
                        If .Rows(i).Cells("cboSalesOrder_Status").Value.ToString <> "" Then
                            objJobProblem.Status = .Rows(i).Cells("cboSalesOrder_Status").Value.ToString
                        End If
                    Else
                        'Set complete
                        objJobProblem.Status = 5
                    End If
                    objJobProblem.Invoice_No = ""
                    If .Rows(i).Cells("col_Invoice").Value IsNot Nothing Then
                        objJobProblem.Invoice_No = .Rows(i).Cells("col_Invoice").Value
                    End If

                    'objJobProblem.Flo1 = .Rows(i).Cells("col_TotalTransportCharged").Value
                    'objJobProblem.Flo2 = .Rows(i).Cells("col_TotalTransportPaid").Value
                    'objJobProblem.Flo3 = .Rows(i).Cells("col_DriverPaidAmount").Value

                    'If objJobProblem.chk_Problem Then
                    '    'So Satus Use: Manifest Status save temp
                    '    objJobProblem.Status = 7
                    'Else
                    '    objJobProblem.Status = 5
                    'End If
                    dtObjCollectionJobProblem.Add(objJobProblem)
                End With

            Next

            'dtJobProblem = grdSoView_Check.DataSource
            'For Each drJobProblem As DataRow In dtJobProblem.Rows
            '    Dim objJobProblem As New tb_TransportManifestItem
            '    objJobProblem.TransportManifestItem_Index = drJobProblem("TransportManifestItem_Index").ToString

            '    'Time_DocReturnedToDC
            '    'Time_DocReturnedToSource
            '    'Time_DocReturnedToOwner
            '    'Time_DocConfirmedByOwner

            '    If drJobProblem("Time_DocReturnedToDC").ToString = "" Then
            '        objJobProblem.Time_DocReturnedToDC = Nothing
            '    Else
            '        objJobProblem.Time_DocReturnedToDC = drJobProblem("Time_DocReturnedToDC").ToString
            '    End If
            '    If drJobProblem("Time_DocReturnedToSource").ToString = "" Then
            '        objJobProblem.Time_DocReturnedToSource = Nothing
            '    Else
            '        objJobProblem.Time_DocReturnedToSource = drJobProblem("Time_DocReturnedToSource").ToString
            '    End If
            '    If drJobProblem("Time_DocReturnedToOwner").ToString = "" Then
            '        objJobProblem.Time_DocReturnedToOwner = Nothing
            '    Else
            '        objJobProblem.Time_DocReturnedToOwner = drJobProblem("Time_DocReturnedToOwner").ToString
            '    End If
            '    If drJobProblem("Time_DocConfirmedByOwner").ToString = "" Then
            '        objJobProblem.Time_DocConfirmedByOwner = Nothing
            '    Else
            '        objJobProblem.Time_DocConfirmedByOwner = drJobProblem("Time_DocConfirmedByOwner").ToString
            '    End If

            '    objJobProblem.Mile_AtDestination = drJobProblem("Mile_AtDestination").ToString
            '    objJobProblem.chk_Problem = CBool(drJobProblem("chk_Problem").ToString)
            '    objJobProblem.Comment = drJobProblem("Comment_ManifestItem").ToString
            '    objJobProblem.SalesOrder_Index = drJobProblem("SalesOrder_Index").ToString
            '    If drJobProblem("SalesOrder_Status").ToString <> "" Then
            '        objJobProblem.Status = drJobProblem("SaleOrders_Status").ToString
            '    End If

            'If objJobProblem.chk_Problem Then
            '    'So Satus Use: Manifest Status save temp
            '    objJobProblem.Status = 7
            'Else
            '    objJobProblem.Status = 5
            'End If
            'dtObjCollectionJobProblem.Add(objJobProblem)
            'Next

            Dim objUpdateJobPrblam As New clsTransportSO_POD
            'Dim objUpdateJobPrblam As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.UPDATE, dtObjCollectionJobProblem)
            If objUpdateJobPrblam.Update(dtObjCollectionJobProblem) <> "" Then
                W_MSG_Information_ByIndex(1)
                grdSoView_Check.DataSource = Nothing
                'LoadgrdItem_NoResult("")
            Else
                W_MSG_Information_ByIndex(2)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdSoView_Check.Rows.Count = 0 Then Exit Sub
            grdSoView_Check.Rows.RemoveAt(grdSoView_Check.CurrentRow.Index)

            SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "DataGridViewCalendarColumn"

    Private Sub grdSoView_Check_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdSoView_Check.CellBeginEdit
        Try
            If e.RowIndex <= -1 Then Exit Sub
            Select Case Me.grdSoView_Check.Columns(e.ColumnIndex).Name.ToUpper
                Case "CBOSALESORDER_STATUS"
                    If Me.grdSoView_Check.Rows(e.RowIndex).Cells("col_SalesOrder_Index").Value IsNot Nothing Then
                        Dim strSalesOrder_No As String = Me.grdSoView_Check.Rows(e.RowIndex).Cells("col_SalesOreder_No").Value.ToString

                        Dim strSalesOrder_Status As String = Me.grdSoView_Check.Rows(e.RowIndex).Cells("cboSalesOrder_Status").Value.ToString

                        Dim objDBType As New clsTransportSO_POD 'tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
                        Dim objDTType As DataTable = New DataTable

                        objDBType.getDeliveryResult_Status()
                        objDTType = objDBType.DataTable
                        Dim objGrdCombox As New DataGridViewComboBoxCell
                        With objGrdCombox
                            .DisplayMember = "Description"
                            .ValueMember = "Status"
                            .DataSource = objDTType
                        End With
                        grdSoView_Check.Rows(e.RowIndex).Cells("cboSalesOrder_Status") = objGrdCombox
                        objGrdCombox.Value = strSalesOrder_Status
                    End If


                    'Case "COL_TIME_DELIVERYTODESTINATION", "COL_TIME_DOCRETURNEDTODC", "COL_TIME_DOCRETURNEDTOSOURCE", "COL_TIME_DOCRETURNEDTOOWNER", "COL_TIME_DOCCONFIRMEDBYOWNER"
                    '    If _IsUSE_TRANSPORT_DATETIME = True Then
                    '        Dim dgvcob As New DataGridViewCalendarColumn
                    '        CType(grdSoView_Check.Rows(e.RowIndex).Cells(e.ColumnIndex), CalendarCell).My_Format = "dd/MM/yyyy HH:mm:ss"
                    '    End If
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdSoView_Check_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSoView_Check.CellEndEdit
        Try
            With Me.grdSoView_Check
                Select Case .Columns(e.ColumnIndex).Name.ToUpper
                    Case "COL_TIME_DELIVERYTODESTINATION", "COL_TIME_DOCRETURNEDTODC", "COL_TIME_DOCRETURNEDTOSOURCE", "COL_TIME_DOCRETURNEDTOOWNER", "COL_TIME_DOCCONFIRMEDBYOWNER"
                        ''If Me.dtpTime_DeliveryToDestination.Text.Replace("/", "").Trim <> "" Then
                        Dim strDate As String = Me.grdSoView_Check.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Replace("/", "").Trim
                        If (strDate <> "") And _
                            (strDate <> ":") And _
                            (strDate <> "::") Then
                            If Not IsDate(.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString) Then
                                W_MSG_Information("กรุณาตรวจสอบ : " & .Columns(e.ColumnIndex).HeaderText)
                                '.Item(e.ColumnIndex, e.RowIndex).Value = ""
                                .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)
                                .BeginEdit(True)
                            End If
                        End If
                End Select
            End With



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub grdSoView_Check_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSoView_Check.CellValueChanged
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)
            If dgv.Columns(e.ColumnIndex).Name = "col_SalesOreder_No" Then
                Dim vSalesOreder_No As String = grdSoView_Check.Rows(e.RowIndex).Cells("col_SalesOreder_No").Value.ToString()

                If grdSoView_Check.DataSource IsNot Nothing Then
                    Dim drArr() As DataRow = CType(Me.grdSoView_Check.DataSource, DataTable).Select("SalesOrder_No = '" & vSalesOreder_No.Trim & "'")
                    If drArr.Length > 0 Then
                        W_MSG_Information_ByIndex("300029")
                        grdSoView_Check.Rows.RemoveAt(grdSoView_Check.CurrentRow.Index)
                        Exit Sub
                    End If
                End If

                '   LoadgrdItem_OnTruck(" AND SalesOrder_No = '" & vSalesOreder_No.Trim & "'")
                LoadgrdItem_OnTruck(" AND SalesOrder_No = '" & vSalesOreder_No.Trim & "'")

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub txtSaleOrder_NoSeach_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSalesOrder_No.KeyDown
    '    Try
    '        If (Not (e.KeyCode = Keys.Enter)) Or (txtSalesOrder_No.Text.Trim = "") Then Exit Sub
    '        LoadDataSalsOrder()

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
    'Sub LoadDataSalsOrder()
    '    Try
    '        '_odtManifestItem = Nothing
    '        'Dim vSalesOreder_No As String = Me.txtSaleOrder_No.Text
    '        'If grdSoView_Check.DataSource IsNot Nothing Then
    '        '    Dim drArr() As DataRow = CType(Me.grdSoView_Check.DataSource, DataTable).Select("SalesOrder_No = '" & vSalesOreder_No.Trim & "'")
    '        '    If drArr.Length > 0 Then
    '        '        W_MSG_Information_ByIndex("300029")
    '        '        Me.txtSaleOrder_No.Text = ""
    '        '        Me.txtSaleOrder_No.Focus()
    '        '        Exit Sub
    '        '    End If
    '        'End If
    '        ' Load Default Data From SalesOrder
    '        Dim odtManifestItem As New DataTable
    '        Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '        objtbManifest.getTransportManifest_DeliveryRrsult(" AND SalesOrder_No = '" & Me.txtSalesOrder_No.Text.Trim & "'", "5", "")
    '        odtManifestItem = objtbManifest.DataTable
    '        If odtManifestItem.Rows.Count = 0 Then
    '            W_MSG_Information_ByIndex(13)
    '            Me._TransportManifestItem_Index = ""
    '            Me._TransportManifest_Index = ""
    '            Me._IsSubManifest = 1
    '            Me.txtSalesOrder_No.Text = ""
    '            Me.txtSalesOrder_No.Focus()
    '            Exit Sub
    '        Else
    '            With odtManifestItem
    '                Me.setPnlPod(odtManifestItem.Rows(0))
    '            End With
    '            Me.dtpTime_DeliveryToDestination.Focus()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub btnConfrim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfrim.Click
    '    Try
    '        If Me.txtSalesOrder_No.Text.Trim = "" Then
    '            W_MSG_Information_ByIndex(13)
    '            Exit Sub
    '        End If
    '        Dim odtManifestItem As New DataTable
    '        Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '        objtbManifest.getTransportManifest_DeliveryRrsult(" AND SalesOrder_No = '" & Me.txtSalesOrder_No.Text.Trim & "'", "5", "")
    '        odtManifestItem = objtbManifest.DataTable

    '        For Each drPODItem As DataRow In odtManifestItem.Rows
    '            If Me.dtpTime_DeliveryToDestination.Checked Then drPODItem("Time_DeliveryToDestination") = Me.dtpTime_DeliveryToDestination.Value
    '            If Me.dtpTime_DocReturnedToSource.Checked Then drPODItem("Time_DocReturnedToSource") = Me.dtpTime_DocReturnedToSource.Value
    '            If Me.dtpTime_DocReturnedToDC.Checked Then drPODItem("Time_DocReturnedToDC") = Me.dtpTime_DocReturnedToDC.Value
    '            If Me.dtpTime_DocReturnedToOwner.Checked Then drPODItem("Time_DocReturnedToOwner") = Me.dtpTime_DocReturnedToOwner.Value
    '            If Me.dtpTime_DocConfirmedByOwner.Checked Then drPODItem("Time_DocConfirmedByOwner") = Me.dtpTime_DocConfirmedByOwner.Value

    '            'drPODItem("TotalTransportCharged") = txtSumPrice_In.Text
    '            'drPODItem("TotalTransportPaid") = txtSumPrice_Out.Text
    '            'drPODItem("DriverPaidAmount") = txtDriverPaidAmount.Text

    '            drPODItem("chk_Problem") = chkProblam.Checked
    '            drPODItem("Status_Manifest") = cboStatusManifest.SelectedValue
    '        Next

    '        If grdSoView_Check.RowCount = 0 Then
    '            Me.grdSoView_Check.DataSource = odtManifestItem
    '        Else
    '            Dim odtDatatSource As New DataTable
    '            CType(Me.grdSoView_Check.DataSource, DataTable).AcceptChanges()
    '            odtDatatSource = CType(Me.grdSoView_Check.DataSource, DataTable)
    '            'Delete Duplicate SalesOrder
    '            Dim drArrSelect() As DataRow = odtDatatSource.Select("SalesOrder_No = '" & Me.txtSalesOrder_No.Text.Trim & "'")
    '            If drArrSelect.Length > 0 Then
    '                odtDatatSource.Rows.Remove(drArrSelect(0))
    '            End If
    '            'Merge New Row
    '            odtManifestItem.Merge(odtDatatSource)
    '            Me.grdSoView_Check.DataSource = odtManifestItem
    '            CType(Me.grdSoView_Check.DataSource, DataTable).AcceptChanges()


    '        End If



    '        ClearPnlPod()
    '        Me.txtSalesOrder_No.Focus()

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Function getMskTime(ByVal mskTextBox As MaskedTextBox) As String
        Try
            'Seach Label
            Dim lblText As New Label
            Dim oControl() As Control
            Dim mskTextBox_Name As String = ""
            mskTextBox_Name = CStr(mskTextBox.Name.ToString.Replace("msk", ""))
            oControl = Me.Controls.Find("lbl" & mskTextBox_Name, True)
            If oControl.Length <> 0 Then
                lblText = CType(oControl(0), Label)
            End If

            Dim txtMskText As String = mskTextBox.Text.Replace("/", "").Trim
            Dim lblMskText As String = lblText.Text

            If (txtMskText <> "") And _
               (txtMskText <> ":") And _
               (txtMskText <> "::") Then
                If Not IsDate(mskTextBox.Text) Then
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

            Return mskTextBox.Text


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function DataGridFormateDate(ByVal strDate As String) As String
        Try
            Dim strDateReturn As String = ""
            If Not _IsUSE_TRANSPORT_DATETIME Then
                strDateReturn = CDate(strDate).ToString("dd/MM/yyyy")
            Else
                strDateReturn = CDate(strDate).ToString("dd/MM/yyyy HH:mm")
            End If
            Return strDateReturn
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Private Sub chkProblam_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProblam.CheckedChanged
    '    Try
    '        btnProblam.Enabled = chkProblam.Checked
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnProblam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProblam.Click
    '    Try
    '        Dim frm As New frmTransportProblem(frmTransportProblem.WithDraw_Mode.ADD)
    '        frm.TransportManifest_Index = Me._TransportManifest_Index
    '        frm.TransportManifestItem_Index = Me._TransportManifestItem_Index
    '        frm.IsSubManifest = Me._IsSubManifest
    '        frm.Status_Manifest = Me.cboStatusManifest.SelectedValue
    '        frm.ShowDialog()
    '        Me.chkProblam.Checked = frm.Problem_Status
    '        If Me.chkProblam.Checked Then
    '            Me.cboStatusManifest.SelectedValue = frm.cboStatusManifest.SelectedValue
    '        End If
    '        'Me.txtDriverPaidAmount.Text = frm.txtDriverPaidAmountAmount.Text
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisible.Click
    '    Try
    '        Me.pnlTransportManifest.Visible = False
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnAddPod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearPnlPod()
            'Me.pnlTransportManifest.Visible = True
            'Me.txtSalesOrder_No.Focus()

            SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If grdSoView_Check.RowCount = 0 Then Exit Sub
            Me._TransportManifestItem_Index = Me.grdSoView_Check.CurrentRow.Cells("col_TransportManifest_Index").Value
            Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.EDIT)
            frm.TransportManifest_Index = Me._TransportManifestItem_Index
            frm.IsSubManifest = _IsSubManifest
            frm.ShowDialog()
         
            'CType(Me.grdSoView_Check.DataSource, DataTable).AcceptChanges()
            'Dim drArrSales() As DataRow = CType(Me.grdSoView_Check.DataSource, DataTable).Select("TransportManifestItem_Index='" & Me._TransportManifestItem_Index & "'")
            'If drArrSales.Length = 0 Then Exit Sub

            'Me.setPnlPod(drArrSales(0))
            ''Me.pnlTransportManifest.Visible = True

            'SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub ClearPnlPod()

        'Me.dtpTime_DeliveryToDestination.Checked = False
        'Me.dtpTime_DocReturnedToDC.Checked = False
        Me.dtpTime_DocReturnedToOwner.Checked = False
        'Me.dtpTime_DocReturnedToSource.Checked = False
        'Me.dtpTime_DocConfirmedByOwner.Checked = False
        'Me.chkProblam.Checked = False
        'Me.cboStatusManifest.SelectedValue = "5"

        Me._TransportManifest_Index = ""
        Me._TransportManifestItem_Index = ""
        'Me.txtTransportManifest_No.Text = ""
        'Me.txtSalesOrder_No.Text = ""

        'Me.txtSalesOrder_No.Focus()
    End Sub

    Sub setPnlPod(ByVal drRow As DataRow)
        Try
            'If drRow("Time_DeliveryToDestination").ToString <> "" Then Me.dtpTime_DeliveryToDestination.Text = DataGridFormateDate(drRow("Time_DeliveryToDestination").ToString)
            'If drRow("Time_DocReturnedToDC").ToString <> "" Then Me.dtpTime_DocReturnedToDC.Text = DataGridFormateDate(drRow("Time_DocReturnedToDC").ToString)
            If drRow("Time_DocReturnedToOwner").ToString <> "" Then Me.dtpTime_DocReturnedToOwner.Text = DataGridFormateDate(drRow("Time_DocReturnedToOwner").ToString)
            'If drRow("Time_DocReturnedToSource").ToString <> "" Then Me.dtpTime_DocReturnedToSource.Text = DataGridFormateDate(drRow("Time_DocReturnedToSource").ToString)
            'Me.dtpTime_DocReturnedToSource.Checked = True
            'If drRow("Time_DocConfirmedByOwner").ToString <> "" Then Me.dtpTime_DocConfirmedByOwner.Text = DataGridFormateDate(drRow("Time_DocConfirmedByOwner").ToString)


            Me._TransportManifestItem_Index = drRow("TransportManifestItem_Index").ToString
            Me._TransportManifest_Index = drRow("TransportManifest_Index").ToString
            'Me.txtTransportManifest_No.Text = drRow("TransportManifest_No").ToString
            'Me.txtSalesOrder_No.Text = drRow("SalesOrder_No").ToString
            'Me.chkProblam.Checked = drRow("chk_Problem")

            Me._IsSubManifest = drRow("IsSubManifest").ToString
            'Me.txtSumPrice_In.Text = drRow("TotalTransportCharged").ToString
            'Me.txtSumPrice_Out.Text = drRow("TotalTransportPaid").ToString
            'Me.cboStatusManifest.SelectedValue = drRow("Status_Manifest").ToString
            'If Me.cboStatusManifest.SelectedValue Is Nothing Then
            'Me.txtDriverPaidAmount.Text = drRow("DriverPaidAmount").ToString
            'Me.cboStatusManifest.SelectedValue = 5
            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdSoView_Check_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSoView_Check.EditingControlShowing
        'Dim strColumnName As String = grdSoView_Check.Columns(grdSoView_Check.CurrentCell.ColumnIndex).Name.ToUpper
        ''Reset Format Calendar
        'Select Case strColumnName
        '    Case "COL_TIME_DELIVERYTODESTINATION", _
        '        "COL_TIME_DOCRETURNEDTODC", _
        '        "COL_TIME_DOCRETURNEDTOSOURCE", _
        '        "COL_TIME_DOCCONFIRMEDBYOWNER", _
        '        "COL_TIME_DOCRETURNEDTOOWNER", _
        '        "COL_EXPECTED_DELIVERY_DATE", _
        '        "COL_TIME_EXPECTEDDOCPICKUP"

        '        Dim ctl As CalendarEditingControl = CType(grdSoView_Check.EditingControl, CalendarEditingControl)
        '        ctl.Format = DateTimePickerFormat.Custom
        '        ctl.CustomFormat = _CustomFormat_Date

        'End Select


    End Sub

    'Private Sub btnEditManifest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditManifest.Click
    '    Try
    '        Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.EDIT)
    '        frm.TransportManifest_Index = Me._TransportManifest_Index
    '        frm.IsSubManifest = _IsSubManifest
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


    ''' <summary>
    ''' Update By Art
    ''' Update Date 25-04-2012
    ''' Update Detail : เพราะแบบเดิมจะต้องทำการคีย์ SO ทุกใบยากต่อการทำงาน
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
#Region "  CONTROL TEXTBOX BUTTON  "
    Private Sub txtInvoice_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInvoice_No.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                SearchData_By_TransportManifest_No()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub txtTransportManifest_no_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTransportManifest_no.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                SearchData_By_TransportManifest_No()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchData_By_TransportManifest_No()

            Dim bSearch As Boolean = True
            If rdoInvoice_No.Checked = True Then
                If Me.txtInvoice_No.Text.Trim = "" Then
                    bSearch = False
                End If
            Else
                If Me.txtTransportManifest_no.Text.Trim = "" Then
                    bSearch = False
                End If
            End If
            'If bSearch = True Then
            '    LoadgrdItem_NoResult("")
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'Private Sub btnShowData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        LoadgrdItem_NoResult("")
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub rdoInvoice_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoInvoice_No.CheckedChanged
        If Me.rdoInvoice_No.Checked = True Then
            Me.rdoTransportManfest_No.Checked = False
            Me.txtTransportManifest_no.Enabled = False

            Me.txtInvoice_No.Enabled = True
        End If
    End Sub

    Private Sub rdoTransportManfest_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTransportManfest_No.CheckedChanged
        If Me.rdoTransportManfest_No.Checked = True Then
            Me.rdoInvoice_No.Checked = False
            Me.txtInvoice_No.Enabled = False

            Me.txtTransportManifest_no.Enabled = True
        End If
    End Sub
    'Private Sub dtpTime_DocReturnedToOwner_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTime_DocReturnedToOwner.ValueChanged
    '    Try

    '        If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub

    '        Dim odtDateToReturnDoc As New DataTable

    '        odtDateToReturnDoc = Me.grdSoView_Check.DataSource

    '        odtDateToReturnDoc.BeginLoadData()
    '        If odtDateToReturnDoc.Rows.Count > 0 Then
    '            For i As Integer = 0 To odtDateToReturnDoc.Rows.Count - 1
    '                odtDateToReturnDoc.Rows(i)("Time_DocReturnedToSource") = dtpTime_DocReturnedToOwner.Value
    '            Next

    '        End If
    '        odtDateToReturnDoc.EndLoadData()
    '        Me.grdSoView_Check.DataSource = odtDateToReturnDoc
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
#End Region


#Region "  FUNCTION AND SUB  "

    Private Function boolchkTimeFormat(ByVal pintRowGrd As Integer) As Boolean
        Try
            Dim intHour As Integer = 0
            Dim intMinute As Integer = 0
            Dim strTemp As String = ""
            Dim strSplit() As String


            'For i As Integer = pintRowGrd To pintRowGrd
            If Me.grdSoView_Check.Rows(pintRowGrd).Cells("col_Time").Value Is Nothing Then
                'strTemp = ""
                W_MSG_Information("กรุณาระบุเวลาถึงที่หมาย")
                Return False
            Else
                strTemp = Me.grdSoView_Check.Rows(pintRowGrd).Cells("col_Time").Value.ToString
            End If


            If strTemp = "" Then
                W_MSG_Information("กรุณาระบุเวลาถึงที่หมาย")
                Return False
            End If

            If Not strTemp.Contains(":") Then
                If IsNumeric(strTemp) = False Then
                    W_MSG_Information("รูปแบบเวลาต้องเป็นตัวเลขเท่านั้น กรุณาตรวจสอบอีกครั้ง")
                    Return False
                End If
            End If


            With strTemp
                If .Contains(".") Or .Contains("/") Or .Contains("\") Or .Contains("*") Then

                    strTemp = .Replace(".", ":")
                End If
            End With

            'If strTemp.Contains(".") Then
            '    strTemp = strTemp.Replace(".", ":")
            'End If
            If IsDate(strTemp) Then
                strTemp = CDate(strTemp).ToString("HH:mm")
            End If

            strSplit = strTemp.Split(":")
            intHour = CInt(strSplit(0))
            intMinute = CInt(strSplit(1))

            If intHour > 23 Then
                W_MSG_Information("รูปแบบชั่วโมงไม่ถูก กรุณาตรวจสอบอีกครั้ง")
                Return False
            End If

            If intMinute > 59 Then
                W_MSG_Information("รูปแบบนาทีไม่ถูก กรุณาตรวจสอบอีกครั้ง")
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub SearchData_By_TransportManifest_No()
        Try
            Dim odtTransportManifestItem As New DataTable
            Dim objTransportManifestItem As New clsTransportSO_POD
            Dim strTransportManifest_Index As String = ""
            Dim intConSortBy As Integer = 0
            Dim intConSort As Integer = 0


            'If Me.cboConditionSort.SelectedIndex = 0 Then
            '    intConSortBy = 1
            'Else
            '    intConSortBy = 2
            'End If

            'If Me.rdoLittleToMost.Checked = True Then
            '    intConSort = 1
            'ElseIf Me.rdoMostToLittle.Checked = True Then
            '    intConSort = 2
            'End If



            'ค้นหาด้วย เลขที่ใบคุมรถ
            If rdoTransportManfest_No.Checked = True Then
                If Me.txtTransportManifest_no.Text.Trim = "" Then
                    W_MSG_Information("กรุณากรอกเลขที่ใบคุมรถ")
                    Me.txtTransportManifest_no.Focus()
                    Exit Sub
                End If

                strTransportManifest_Index = objTransportManifestItem.GetTransportManifest_Index(Me.txtTransportManifest_no.Text.Trim.Replace("'", "''"))

                If strTransportManifest_Index = "" Then
                    W_MSG_Information("ไม่พบเลขที่ใบคุมรถในระบบ กรุณาตรวจสอบอีกครั้ง")
                    Me.txtTransportManifest_no.Text = ""
                    Me.txtTransportManifest_no.Focus()
                    Exit Sub
                End If

                objTransportManifestItem.getTransportManifest_DeliveryRrsult(strTransportManifest_Index, 1, intConSortBy, intConSort, Me.chkComplete.Checked)
                odtTransportManifestItem = objTransportManifestItem.DataTable

                If odtTransportManifestItem.Rows.Count > 0 Then
                    Select Case odtTransportManifestItem.Rows(0)("Status_Id")
                        Case 13, 14, 15 '5
                            If odtTransportManifestItem.Rows.Count > 0 Then
                                Me.grdSoView_Check.DataSource = odtTransportManifestItem
                                _odtTransportManifest_OnTruck = odtTransportManifestItem
                            End If
                        Case Else
                            If Not Me.chkComplete.Checked Then
                                W_MSG_Information("เลขที่ใบคุมรถ ไม่สามารถคืนบิลได้ กรุณาตรวจสอบอีกครั้ง")
                                Me.txtTransportManifest_no.Text = ""
                                Me.txtTransportManifest_no.Focus()
                                Exit Sub
                            Else
                                Me.grdSoView_Check.DataSource = odtTransportManifestItem
                                _odtTransportManifest_OnTruck = odtTransportManifestItem
                            End If

                    End Select
                Else
                    Me.grdSoView_Check.DataSource = Nothing
                    Me.grdSoView_Check.Refresh()
                End If



            End If


            'ค้นหาด้วย Invoice No.
            If rdoInvoice_No.Checked = True Then
                If Me.txtInvoice_No.Text.Trim = "" Then
                    W_MSG_Information("กรุณากรอกเลขที่ Invoice")
                    Me.txtInvoice_No.Text = ""
                    Me.txtInvoice_No.Focus()
                    Exit Sub
                End If




                objTransportManifestItem.getTransportManifest_DeliveryRrsult(Me.txtInvoice_No.Text.Trim.Replace("'", "''"), 2, intConSortBy, intConSort, Me.chkComplete.Checked)
                odtTransportManifestItem = objTransportManifestItem.DataTable

                If odtTransportManifestItem.Rows.Count = 0 Then
                    W_MSG_Information("ไม่พบเลขที่ Invoice ในระบบ กรุณาตรวจสอบอีกครั้ง ")
                    Me.txtInvoice_No.Text = ""
                    Me.txtInvoice_No.Focus()
                    Exit Sub
                End If

                Select Case odtTransportManifestItem.Rows(0)("Status_Manifest")

                    Case 5, 7, 13
                        If odtTransportManifestItem.Rows.Count > 0 Then
                            Me.grdSoView_Check.DataSource = odtTransportManifestItem
                            _odtTransportManifest_OnTruck = odtTransportManifestItem
                        End If

                    Case Else
                        W_MSG_Information("เลขที่ Invoice ไม่สามารถคืนบิลได้ กรุณาตรวจสอบอีกครั้ง")
                        Me.txtInvoice_No.Text = ""
                        Me.txtInvoice_No.Focus()
                        Exit Sub
                End Select
            End If
            If odtTransportManifestItem.Rows.Count > 0 Then
                For i As Integer = 0 To odtTransportManifestItem.Rows.Count - 1
                    If IsDBNull(odtTransportManifestItem.Rows(i)("Time_DeliveryToDestination")) Then
                        odtTransportManifestItem.Rows(i)("Time_DeliveryToDestination") = Me.dtpTime_DeliveryToDestination.Value.ToString("dd/MM/yyyy")
                    End If
                Next
            End If

            Me.GetAllStatus_SO()

            'Set ลำดับ
            'best up date 01-09-2012
            'GetAllStatus_SO()
            'end best up date 01-09-2012
            SetNumRow()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAllStatus_SO()
        Try
            'If Me.chkUpdateAllDoc.Checked = True Then
            For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
                Me.grdSoView_Check.Rows(i).Cells("cboSalesOrder_Status").Value = Me.cboUpdateAllSOStatus.SelectedValue
            Next
            'Else
            'For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
            '    Me.grdSoView_Check.Rows(i).Cells("cboSalesOrder_Status").Value = Me._odtTransportManifest_OnTruck.Rows(i)("Status_Manifest")
            'Next


            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SetNumRow()
        Try
            If Me.grdSoView_Check.RowCount > 0 Then
                For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
                    Me.grdSoView_Check.Rows(i).Cells("col_Row_Num").Value = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    'Private Sub chkUpdateAllDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try
    '        'If Me.chkUpdateAllDoc.Checked = True Then
    '        '    Me.cboUpdateAllSOStatus.Enabled = True
    '        '    'Me.getDataUpdateAllSalesOrder_Status()
    '        '    GetAllStatus_SO()
    '        'Else
    '        '    Me.cboUpdateAllSOStatus.Enabled = False
    '        '    'Me.cboUpdateAllSOStatus.DataSource = Nothing

    '        '    For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
    '        '        Me.grdSoView_Check.Rows(i).Cells("cboSalesOrder_Status").Value = Me._odtTransportManifest_OnTruck.Rows(i)("Status_Manifest")
    '        '    Next
    '        'End If

    '        Me.cboUpdateAllSOStatus.Enabled = Me.chkUpdateAllDoc.Checked

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub grdSoView_Check_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSoView_Check.CellLeave
    '    Try
    '        If e.RowIndex <= -1 Then Exit Sub
    '        Select Case Me.grdSoView_Check.Columns(e.ColumnIndex).Name.ToUpper
    '            Case "COL_TIME_DELIVERYTODESTINATION"

    '                If IsDate(grdSoView_Check.CurrentCell.Value) = False Then


    '                    'grdSoView_Check.CurrentCell.Value = DBNull.Value
    '                    CType(Me.grdSoView_Check.DataSource, DataTable).RejectChanges()

    '                End If

    '        End Select
    '    Catch ex As Exception

    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cboUpdateAllSOStatus_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUpdateAllSOStatus.SelectionChangeCommitted
    '    Try
    '        GetAllStatus_SO()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    'Private Sub dtpTime_DeliveryToDestination_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTime_DeliveryToDestination.ValueChanged
    '    Try

    '        If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub
    '        Dim odtTime_DeliveryToDestination As New DataTable

    '        odtTime_DeliveryToDestination = Me.grdSoView_Check.DataSource



    '        odtTime_DeliveryToDestination.BeginLoadData()
    '        If odtTime_DeliveryToDestination.Rows.Count > 0 Then
    '            For i As Integer = 0 To odtTime_DeliveryToDestination.Rows.Count - 1
    '                odtTime_DeliveryToDestination.Rows(i)("Time_DeliveryToDestination") = dtpTime_DeliveryToDestination.Value
    '            Next

    '        End If
    '        odtTime_DeliveryToDestination.EndLoadData()
    '        Me.grdSoView_Check.DataSource = odtTime_DeliveryToDestination
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnOK_DateReturnDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK_DateReturnDoc.Click
        Try

            If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub

            Dim odtDateToReturnDoc As New DataTable

            odtDateToReturnDoc = Me.grdSoView_Check.DataSource

            odtDateToReturnDoc.BeginLoadData()
            If odtDateToReturnDoc.Rows.Count > 0 Then
                'For i As Integer = 0 To odtDateToReturnDoc.Rows.Count - 1
                '    odtDateToReturnDoc.Rows(i)("Time_DocReturnedToSource") = dtpTime_DocReturnedToOwner.Value
                'Next


                For Each odr As DataRow In odtDateToReturnDoc.Rows
                    odr("Time_DocReturnedToSource") = dtpTime_DocReturnedToOwner.Value
                Next
            End If


            odtDateToReturnDoc.EndLoadData()
            odtDateToReturnDoc.AcceptChanges()
            Me.grdSoView_Check.DataSource = odtDateToReturnDoc
            Me.SetNumRow()
            'For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
            '    Me.grdSoView_Check.Rows(i).Cells("col_Time_DocReturnedToSource").Value = Me.dtpTime_DocReturnedToOwner.Value
            'Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnOKTimeToDeriverly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKTimeToDeriverly.Click

        Try

            If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub
            Dim odtTime_DeliveryToDestination As New DataTable

            odtTime_DeliveryToDestination = Me.grdSoView_Check.DataSource



            odtTime_DeliveryToDestination.BeginLoadData()
            If odtTime_DeliveryToDestination.Rows.Count > 0 Then
                'For i As Integer = 0 To odtTime_DeliveryToDestination.Rows.Count - 1
                '    odtTime_DeliveryToDestination.Rows(i)("Time_DeliveryToDestination") = dtpTime_DeliveryToDestination.Value
                'Next

                For Each odr As DataRow In odtTime_DeliveryToDestination.Rows
                    odr("Time_DeliveryToDestination") = dtpTime_DeliveryToDestination.Value
                    odr("Time_DeliveryToDestination_Time") = dtpTime_DeliveryToDestination.Value
                Next

            End If
            odtTime_DeliveryToDestination.EndLoadData()
            odtTime_DeliveryToDestination.AcceptChanges()
            Me.grdSoView_Check.DataSource = odtTime_DeliveryToDestination

            SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnOKSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SearchData_By_TransportManifest_No()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnOKStatus_Transport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKStatus_Transport.Click
        Try
            GetAllStatus_SO()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub grdSoView_Check_CellMouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdSoView_Check.CellMouseUp
    '    Try
    '        'Dim intCell As Integer = 0
    '        'intCell = e.ColumnIndex

    '        'If intCell = 15 Then
    '        For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1

    '        Next
    '        'End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdSoView_Check_CellToolTipTextNeeded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles grdSoView_Check.CellToolTipTextNeeded
        Try

            If e.ColumnIndex = Me.grdSoView_Check.Columns("col_Time").Index Then
                e.ToolTipText = "Example : 00.00 - 23.59 หรือ 00:00 - 23:59"

                'Me.ToolTip1.Active = True
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

   
    Private Sub btnSeach2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeach2.Click
        Try
            LoadgrdItem_NoResult("")

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub btndtpTime_DeliveryToDestination_Time_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        For i As Integer = 0 To Me.grdSoView_Check.Rows.Count - 1
    '            Dim tDate As DateTime = CDate(grdSoView_Check.Rows(i).Cells("col_Time_DeliveryToDestination").Value).ToShortDateString
    '            tDate = tDate.AddHours(Me.dtpTime_DeliveryToDestination_Time.Value.Hour)
    '            tDate = tDate.AddMinutes(Me.dtpTime_DeliveryToDestination_Time.Value.Minute)

    '            grdSoView_Check.Rows(i).Cells("col_Time").Value = tDate.ToShortTimeString
    '        Next

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


    Private Sub btnInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoice.Click
        Try
            If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub
            Dim dtSource As New DataTable
            dtSource = Me.grdSoView_Check.DataSource
            dtSource.BeginLoadData()
            If dtSource.Rows.Count > 0 Then
                For Each odr As DataRow In dtSource.Rows
                    odr("str1") = Me.txtInvoice.Text
                Next
            End If
            dtSource.AcceptChanges()
            Me.grdSoView_Check.DataSource = dtSource
            Me.SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnMile_AtDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMile_AtDestination.Click
        Try
            If IsNumeric(Me.txtMile_AtDestination.Text) = False Then
                W_MSG_Error("กรุณาระบุตัวเลขเท่านั้น")
                Me.txtMile_AtDestination.Focus()
                Me.txtMile_AtDestination.SelectAll()
                Exit Sub
            End If
            If Me.grdSoView_Check.Rows.Count = 0 Then Exit Sub
            Dim dtSource As New DataTable
            dtSource = Me.grdSoView_Check.DataSource
            dtSource.BeginLoadData()
            If dtSource.Rows.Count > 0 Then
                For Each odr As DataRow In dtSource.Rows
                    odr("Mile_AtDestination") = Me.txtMile_AtDestination.Text
                Next
            End If
            dtSource.AcceptChanges()
            Me.grdSoView_Check.DataSource = dtSource
            Me.SetNumRow()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.grdTransport.RowCount = 0 Then Exit Sub
            Me._TransportManifestItem_Index = Me.grdTransport.CurrentRow.Cells("col_Index_JobReturn").Value
            Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.EDIT)
            frm.TransportManifest_Index = Me._TransportManifestItem_Index
            frm.IsSubManifest = _IsSubManifest
            frm.ShowDialog()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class