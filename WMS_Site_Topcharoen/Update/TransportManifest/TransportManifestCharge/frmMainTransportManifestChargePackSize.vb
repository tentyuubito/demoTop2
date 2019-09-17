Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master.CurrencyTextBox
Imports WMS_STD_Formula
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master

Public Class frmMainTransportManifestChargePackSize
    Private _Customer_Index As String = ""
    Private _Carrier_Index As String = ""
    Private _TransportCharge_Index As String = ""
    Private CheckEndRate As Boolean = False
    Private Max As Double = 0

    Public Property Carrier_Index() As String
        Get
            Return _Carrier_Index
        End Get
        Set(ByVal Value As String)
            _Carrier_Index = Value
        End Set
    End Property
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmServiceRateTransport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Me.grdTransportCharge.AutoGenerateColumns = False
            Me.grdTransportPayment.AutoGenerateColumns = False
            Me.grdCarrier.AutoGenerateColumns = False

            Me.SETDEFAULT_CUSTOMER_INDEX()
            Me.getTransportJobType()
            Me.getShowCarrier()
            'Me.getShowTransportCharge()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getTransportJobType()
        Dim objClassDB As New ms_TransportJobType(ms_TransportJobType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable
            cboTransportJobType.BeginUpdate()
            With cboTransportJobType
                .DisplayMember = "Description"
                .ValueMember = "TransportJobType_Index"
                .DataSource = objDT
            End With
            cboTransportJobType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub
    Private Sub getShowCarrier()
        Try
            If (Me._Customer_Index = "") Then Exit Sub

            Dim oCustomer_Shipping_Location As New svar_TransportManifestChargePackSize ' svar_TransportPayment
            oCustomer_Shipping_Location.getTransportManifestChargePackSizeShowCarrier(Me._Customer_Index)
            Me.grdCarrier.DataSource = oCustomer_Shipping_Location.DataTable
            For i As Integer = 0 To Me.grdCarrier.RowCount - 1
                Select Case Me.grdCarrier.Rows(i).Cells("Status1").Value
                    Case "0"
                        Me.grdCarrier.Rows(i).Cells("Description1").Style.BackColor = Color.White
                    Case "1"
                        Me.grdCarrier.Rows(i).Cells("Description1").Style.BackColor = Color.LightGreen
                End Select
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub getShowTransportPayment()
        Try
            If (Me._Customer_Index = "") Or (Me._Carrier_Index = "") Then Exit Sub

            Dim oTransportCharge As New svar_TransportManifestChargePackSize
            Dim dtTransportCharge As New DataTable
            oTransportCharge.GetTransportPayment(Me._Customer_Index, Me._Carrier_Index, Me.cboTransportJobType.SelectedValue)
            dtTransportCharge = oTransportCharge.GetDataTable
            Me.grdTransportPayment.DataSource = dtTransportCharge
            'CType(Me.grdShowService.DataSource, DataTable).AcceptChanges()
            For i As Integer = 0 To Me.grdTransportPayment.RowCount - 1
                Select Case Me.grdTransportPayment.Rows(i).Cells("Status").Value
                    Case "0"
                        Me.grdTransportPayment.Rows(i).Cells("Description").Style.BackColor = Color.White
                    Case "1"
                        Me.grdTransportPayment.Rows(i).Cells("Description").Style.BackColor = Color.LightGreen
                End Select
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub getShowTransportCharge()
    '    Try
    '        If Me._Customer_Index = "" Then Exit Sub

    '        Dim oTransportCharge As New svar_TransportCharge
    '        Dim dtTransportCharge As New DataTable
    '        oTransportCharge.GetTransportCharge(Me._Customer_Index)
    '        dtTransportCharge = oTransportCharge.GetDataTable
    '        Me.grdTransportCharge.DataSource = dtTransportCharge
    '        'CType(Me.grdShowService.DataSource, DataTable).AcceptChanges()
    '        For i As Integer = 0 To Me.grdTransportCharge.RowCount - 1
    '            Select Case Me.grdTransportCharge.Rows(i).Cells("Col_Status").Value
    '                Case "0"
    '                    Me.grdTransportCharge.Rows(i).Cells("Col_Description").Style.BackColor = Color.White
    '                Case "1"
    '                    Me.grdTransportCharge.Rows(i).Cells("Col_Description").Style.BackColor = Color.LightGreen
    '            End Select
    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Sub SETDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.getCustomer()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            Me._Customer_Index = frm.Customer_Index
            If Me._Customer_Index = "" Then Exit Sub

            Me.getCustomer()
            'Me.getShowTransportCharge()
            Me.getShowCarrier()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString

            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    'Private Sub grdShowService_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    Try
    '        If e.RowIndex <= -1 Then Exit Sub
    '        If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
    '        Me.SetupTransportCharge(e.RowIndex)
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub



    Function CalculateBegin(ByVal MAX As Double) As Double
        Try
            Dim min As Double
            min = MAX + 1
            Return min
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    'Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.getShowTransportCharge()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    ' Sets the ToolTip text for cells in the Rating column.
    'Sub grdShowService_CellFormatting(ByVal sender As Object, _
    '    ByVal e As DataGridViewCellFormattingEventArgs)


    '    If (e.RowIndex <= -1) Or (e.ColumnIndex < -1) Then Exit Sub

    '    With Me.grdTransportCharge.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '        If Me.grdTransportCharge.Rows(e.RowIndex).Cells("Col_Status").Value = "1" Then
    '            .ToolTipText = "Double Click Row For : Edit " & Me.Text
    '        Else
    '            .ToolTipText = "Double Click Row For : Add " & Me.Text
    '        End If
    '    End With
    'End Sub


    'Private Sub grdShowService_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
    '        Select Case Me.grdTransportCharge.CurrentRow.Cells("Col_Status").Value
    '            Case "1"
    '                Me.btnCancel.Enabled = True
    '            Case Else
    '                Me.btnCancel.Enabled = False
    '        End Select

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If grdTransportCharge.RowCount <= 0 Then Exit Sub
    '        If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.No Then Exit Sub
    '        Dim oDelTmC As New svar_TransportChargeTransaction(svar_TransportChargeTransaction.enuOperation_Type.CANCEL)
    '        oDelTmC.TransportCharge_Index = Me.grdTransportCharge.CurrentRow.Cells("col_TransportCharge_Index").Value
    '        Dim strSave As String = oDelTmC.SaveData()
    '        If strSave = "PASS" Then
    '            'W_MSG_Confirm_ByIndex(1)
    '            Me.getShowTransportCharge()
    '        Else
    '            W_MSG_Error(strSave)
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.grdTransportCharge.RowCount = 0 Then Exit Sub

    '        Me.SetupTransportCharge(Me.grdTransportCharge.CurrentRow.Index)
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SetupTransportCharge(ByVal intRow As Integer)
    '    Try
    '        If Me._Customer_Index = "" Then Exit Sub
    '        Dim pstrTransportJobType_Index As String = Me.grdTransportCharge.Rows(intRow).Cells("col_TransportJobType_Index").Value
    '        Dim pstrTransportRegion_Index As String = Me.grdTransportCharge.Rows(intRow).Cells("col_TransportRegion_Index").Value

    '        Dim frm As New Object ' frmServiceRateTransportSetup(Me._Customer_Index, pstrTransportJobType_Index, pstrTransportRegion_Index)
    '        frm.TransportCharge_Index = Me.grdTransportCharge.Rows(intRow).Cells("col_TransportCharge_Index").Value
    '        frm.ShowDialog()
    '        Me.getShowTransportCharge()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub grdTransportPayment_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTransportPayment.SelectionChanged
    '    Try
    '        If Me.grdTransportPayment.Rows.Count = 0 Then Exit Sub
    '        Select Case Me.grdTransportPayment.CurrentRow.Cells("Status").Value
    '            Case "1"
    '                Me.btnCancelPayment.Enabled = True
    '            Case Else
    '                Me.btnCancelPayment.Enabled = False
    '        End Select

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdCustomer_Shipping_Location_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdCarrier.SelectionChanged
        Try
            If Me.grdCarrier.Rows.Count = 0 Then Exit Sub

            Me._Carrier_Index = Me.grdCarrier.CurrentRow.Cells("Carrier_Index1").Value
            Me.getShowTransportPayment()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddEditPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEditPayment.Click
        Try
            If Me._Customer_Index = "" Then Exit Sub
            If Me._Carrier_Index = "" Then Exit Sub

            Dim pstrTransportJobType_Index As String = Me.cboTransportJobType.SelectedValue

            Dim frm As New frmTransportManifestChargePackSizeSetup(Me._Customer_Index, Me._Carrier_Index, "", pstrTransportJobType_Index)
            frm.TransportManifestChargePackSize_Index = ""
            frm.ShowDialog()
            Me.getShowTransportPayment()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnEditManifest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditManifest.Click
        Try
            If Me.grdTransportPayment.RowCount = 0 Then Exit Sub
            If Me._Customer_Index = "" Then Exit Sub
            If Me._Carrier_Index = "" Then Exit Sub

            Dim intRow As Integer = Me.grdTransportPayment.CurrentRow.Index
            Dim pstrTransportJobType_Index As String = Me.grdTransportPayment.Rows(intRow).Cells("TransportJobType_Index").Value
            Dim pstrCustomer_Shipping_Location_Index As String = Me.grdTransportPayment.Rows(intRow).Cells("Customer_Shipping_Location_Index2").Value

            Dim frm As New frmTransportManifestChargePackSizeSetup(Me._Customer_Index, Me._Carrier_Index, pstrCustomer_Shipping_Location_Index, pstrTransportJobType_Index)
            frm.TransportManifestChargePackSize_Index = Me.grdTransportPayment.Rows(intRow).Cells("TransportManifestChargePackSize_Index").Value
            frm.ShowDialog()
            Me.getShowTransportPayment()

            'Me.SetupTransportPayment(Me.grdTransportPayment.CurrentRow.Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshPayment.Click
        Try
            Me.getShowCarrier()
            Me.getShowTransportPayment()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancelPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If grdTransportPayment.RowCount <= 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.No Then Exit Sub
            Dim oDelTmC As New svar_TransportPaymentTransaction(svar_TransportPaymentTransaction.enuOperation_Type.CANCEL)
            oDelTmC.TransportPayment_Index = Me.grdTransportPayment.CurrentRow.Cells("TransportPayment_Index").Value
            Dim strSave As String = oDelTmC.SaveData()
            If strSave = "PASS" Then
                'W_MSG_Confirm_ByIndex(1)
                Me.getShowTransportPayment()
            Else
                W_MSG_Error(strSave)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnExitPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitPayment.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


End Class