Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.CurrencyTextBox

Public Class frmMainHandlingChargeByDocType
    Private _Customer_Index As String = ""
    Private _Carrier_Index As String = ""
    Private _TransportCharge_Index As String = ""
    Private CheckEndRate As Boolean = False
    Private Max As Double = 0


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmServiceRateTransport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdTransportCharge.AutoGenerateColumns = False

            Me.SETDEFAULT_CUSTOMER_INDEX()
            Me.getProcess()
            Me.getShowTransportCharge()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getProcess()
        Try
            Dim oProcess As New svar_HandlingChargeByDocType
            Dim dtProcess As New DataTable
            oProcess.getProcessStatus(" AND Process_Id In (9,10,25) ")
            'oProcess.getProcessStatus(" AND Process_Id In (1,2,25) ")
            dtProcess = oProcess.GetDataTable

            'Dim drNewRow As DataRow
            'drNewRow = dtProcess.NewRow
            'drNewRow("Process_Name") = "--- ALL ---"
            'drNewRow("Process_Id") = "-11"
            'dtProcess.Rows.Add(drNewRow)
            With Me.cboProcess
                .DisplayMember = "Process_Name"
                .ValueMember = "Process_Id"
                .DataSource = dtProcess
            End With
            'Me.cboProcess.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getHandling()
        Try
            Dim dtProcess As New DataTable
            dtProcess.Columns.Add("Description", GetType(String))
            dtProcess.Columns.Add("Handling_Type", GetType(String))

            Dim drNewRow As DataRow
            drNewRow = dtProcess.NewRow
            drNewRow("Description") = "--- ALL ---"
            drNewRow("Handling_Type") = "-11"
            dtProcess.Rows.Add(drNewRow)

            drNewRow = dtProcess.NewRow
            drNewRow("Description") = "Handling In"
            drNewRow("Handling_Type") = "1"
            dtProcess.Rows.Add(drNewRow)

            drNewRow = dtProcess.NewRow
            drNewRow("Description") = "Handling Out"
            drNewRow("Handling_Type") = "2"
            dtProcess.Rows.Add(drNewRow)

            With Me.cboProcess
                .DisplayMember = "Description"
                .ValueMember = "Handling_Type"
                .DataSource = dtProcess
            End With

            Me.cboProcess.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub

    Private Sub getShowTransportCharge()
        Try
            If Me._Customer_Index = "" Then Exit Sub
            If Me.cboProcess.SelectedValue Is Nothing Then Exit Sub

            Dim oTransportCharge As New svar_HandlingChargeByDocType
            Dim dtTransportCharge As New DataTable
            oTransportCharge.GetHandlingChargeByDocType(Me._Customer_Index, Me.cboProcess.SelectedValue)
            dtTransportCharge = oTransportCharge.GetDataTable
            Me.grdTransportCharge.DataSource = dtTransportCharge
            'CType(Me.grdShowService.DataSource, DataTable).AcceptChanges()
            For i As Integer = 0 To Me.grdTransportCharge.RowCount - 1
                Select Case Me.grdTransportCharge.Rows(i).Cells("Col_Status").Value
                    Case "0"
                        Me.grdTransportCharge.Rows(i).Cells("Col_Description").Style.BackColor = Color.White
                    Case "1"
                        Me.grdTransportCharge.Rows(i).Cells("Col_Description").Style.BackColor = Color.LightGreen
                End Select
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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
            Me.getShowTransportCharge()

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

    Private Sub grdShowService_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTransportCharge.CellDoubleClick
        Try
            If e.RowIndex <= -1 Then Exit Sub
            If Me.grdTransportCharge.RowCount = 0 Then Exit Sub

            If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
            Dim strDocumentType_Index As String = Me.grdTransportCharge.CurrentRow.Cells("col_DocumentType_Index").Value
            Dim strDHandlingChargeByDocType_Index As String = Me.grdTransportCharge.CurrentRow.Cells("col_HandlingChargeByDocType_Index").Value
            Dim frm As New frmHandlingChargeByDocTypeSetup(Me._Customer_Index, Me.cboProcess.SelectedValue, strDocumentType_Index, strDHandlingChargeByDocType_Index)
            frm.ShowDialog()
            Me.getShowTransportCharge()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Function CalculateBegin(ByVal MAX As Double) As Double
        Try
            Dim min As Double
            min = MAX + 1
            Return min
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.getShowTransportCharge()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ' Sets the ToolTip text for cells in the Rating column.
    Sub grdShowService_CellFormatting(ByVal sender As Object, _
        ByVal e As DataGridViewCellFormattingEventArgs) _
        Handles grdTransportCharge.CellFormatting

        If (e.RowIndex <= -1) Or (e.ColumnIndex < -1) Then Exit Sub

        With Me.grdTransportCharge.Rows(e.RowIndex).Cells(e.ColumnIndex)
            If Me.grdTransportCharge.Rows(e.RowIndex).Cells("Col_Status").Value = "1" Then
                .ToolTipText = "Double Click Row For : Edit " & Me.Text
            Else
                .ToolTipText = "Double Click Row For : Add " & Me.Text
            End If
        End With
    End Sub


    Private Sub grdShowService_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTransportCharge.SelectionChanged
        Try
            If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
            Select Case Me.grdTransportCharge.CurrentRow.Cells("Col_Status").Value
                Case "1"
                    Me.btnCancel.Enabled = True
                Case Else
                    Me.btnCancel.Enabled = False
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If grdTransportCharge.RowCount <= 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.No Then Exit Sub
            Dim oDelTmC As New svar_HandlingChargeByDocTypeTransaction(svar_HandlingChargeByDocTypeTransaction.enuOperation_Type.CANCEL)
            oDelTmC.HandlingChargeByDocType_Index = Me.grdTransportCharge.CurrentRow.Cells("col_HandlingChargeByDocType_Index").Value
            Dim strSave As String = oDelTmC.SaveData()
            If strSave = "PASS" Then
                'W_MSG_Confirm_ByIndex(1)
                Me.getShowTransportCharge()
            Else
                W_MSG_Error(strSave)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetup.Click
        Try
            If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
            Dim strDocumentType_Index As String = Me.grdTransportCharge.CurrentRow.Cells("col_DocumentType_Index").Value
            Dim strDHandlingChargeByDocType_Index As String = Me.grdTransportCharge.CurrentRow.Cells("col_HandlingChargeByDocType_Index").Value
            Dim frm As New frmHandlingChargeByDocTypeSetup(Me._Customer_Index, Me.cboProcess.SelectedValue, strDocumentType_Index, strDHandlingChargeByDocType_Index)
            frm.ShowDialog()
            Me.getShowTransportCharge()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub




    Private Sub cboProcess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProcess.SelectedIndexChanged
        Try
            Me.getShowTransportCharge()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class