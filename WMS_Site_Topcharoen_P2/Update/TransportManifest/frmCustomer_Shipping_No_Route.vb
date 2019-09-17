Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_OUTB_SO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.CurrencyTextBox

Public Class frmCustomer_Shipping_No_Route
    Private _SalesOrder_NotRoute As DataTable
    Public Property SalesOrder_NotRoute() As DataTable
        Get
            Return _SalesOrder_NotRoute
        End Get
        Set(ByVal value As DataTable)
            _SalesOrder_NotRoute = value
        End Set
    End Property
    Private _chk_NotRoute As Boolean
    Public Property chk_NotRoute() As Boolean
        Get
            Return _chk_NotRoute
        End Get
        Set(ByVal value As Boolean)
            _chk_NotRoute = value
        End Set
    End Property

    Private Sub frmCustomer_Shipping_No_Route_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdCustomer_Shipping.AutoGenerateColumns = False
            getComboRoute()
            getComboRegion()
            If Me._SalesOrder_NotRoute Is Nothing Then
                'Alert
            Else
                getComboSubRoute()
                ShowCustomer_Shipping_notRoute(_SalesOrder_NotRoute)
            End If
            'ShowCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'update best 10-09-2012
    Private Sub getComboSubRoute()
        Dim objClassDB As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("-11")
            objDT = objClassDB.DataTable


            With col_cbosubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With

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

            With Col_cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = objDT
            End With



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub getComboRegion()
        Dim objClassDB As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable


            With col_cboTransportRegion
                .DisplayMember = "Description"
                .ValueMember = "TransportRegion_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub getComboSubRoute(ByVal vRoute_Index As String, ByVal Row_index As Integer)
        Dim objClassDB As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try       
            grdCustomer_Shipping.Rows(Row_index).Cells("col_cbosubRoute").Value = ""
            grdCustomer_Shipping.Rows(Row_index).Cells("col_cboTransportRegion").Value = ""
            objClassDB.GetAllAsDataTable(vRoute_Index)
            objDT = objClassDB.DataTable
            Dim cboCell As New DataGridViewComboBoxCell
            With cboCell
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With
            grdCustomer_Shipping.Rows(Row_index).Cells("col_cbosubRoute") = cboCell

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getComboSubRoute()
            ShowCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub ShowCustomer_Shipping_notRoute(ByVal Order_HasRow_InMaster As DataTable)
        Try

            Dim strWhere As String = ""
            Dim Orderlist As List(Of String)
            Dim Orderstr As String = ""
            Orderlist = chkRow_notRoute(_SalesOrder_NotRoute)
            strWhere = "AND Customer_Shipping_Location_Index in ("
            For Each Orderstr In Orderlist
                strWhere += "'" + Orderstr + "',"
            Next
            strWhere = strWhere.Substring(0, strWhere.Length - 1)
            strWhere += ")"

            Dim objClassDB As New ms_Customer_Shipping_Update(ms_Customer_Shipping_Update.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable

            grdCustomer_Shipping.DataSource = Nothing
            'grdCustomer_Shipping.Rows.Clear()

            objClassDB.ShowDataForMain_location("", strWhere, True)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                grdCustomer_Shipping.DataSource = objDT
            Else
                grdCustomer_Shipping.Rows.Clear()
                grdCustomer_Shipping.Refresh()
            End If           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function chkRow_notRoute(ByVal Datachk As DataTable) As List(Of String)
        Try
            Dim subRoute As New List(Of String)
            For irow As Integer = 0 To Datachk.Rows.Count - 1
                subRoute.Add(Datachk.Rows(irow)("Customer_Shipping_Location_Index").ToString)
            Next
            Return subRoute
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ShowCustomer_Shipping()

        Dim strWhere As String = ""
        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchType.SelectedIndex
                Case 1
                    ' Customer Name (Thai) : LIKE Search 'A%'
                    strWhere &= " AND (Shipping_Location_Name LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                Case 0
                    ' Customer Name (English): LIKE Search 'A%'
                    strWhere &= " AND (Customer_Shipping_Location_Id LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "

            End Select
        End If
        'strWhere &= "  AND ( isnull(ms_Customer_Shipping.Route_index,'')='' or isnull(ms_Customer_Shipping.subroute_index,'')='' or  isnull(ms_Customer_Shipping.TransportRegion_Index,'')='' )  "
        strWhere &= "  AND ( isnull(Route_index,'')='' or isnull(subroute_index,'')='' or  isnull(TransportRegion_Index,'')='' )  "

        Dim objClassDB As New ms_Customer_Shipping_Update(ms_Customer_Shipping_Update.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            grdCustomer_Shipping.DataSource = Nothing
            'grdCustomer_Shipping.Rows.Clear()

            objClassDB.ShowDataForMain_location("", strWhere)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                grdCustomer_Shipping.DataSource = objDT
            Else
                grdCustomer_Shipping.Rows.Clear()
                grdCustomer_Shipping.Refresh()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    'Private Sub grdCustomer_Shipping_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomer_Shipping.CellValueChanged
    '    Try
    '        With grdCustomer_Shipping
    '            Select Case .Columns(e.ColumnIndex).Name.ToUpper
    '                Case "Col_cboRoute".ToUpper
    '                    getComboSubRoute(.Rows(e.RowIndex).Cells("Col_cboRoute").Value, e.RowIndex)
    '            End Select
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                ShowCustomer_Shipping()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim countROW As Integer = grdCustomer_Shipping.Rows.Count - 1
        Dim Err_Msg As String = ""
        Try
            Dim objCusShip As New ms_Customer_Shipping_Update(ms_Customer_Shipping_Update.enuOperation_Type.UPDATE)
            If Me._SalesOrder_NotRoute Is Nothing Then
                For irow As Integer = 0 To grdCustomer_Shipping.Rows.Count - 1
                    Dim isinsert As Boolean = True
                    With grdCustomer_Shipping
                        If .Rows(irow).Cells("Col_cboRoute").Value Is Nothing Then
                            isinsert = False
                            '_chk_NotRoute = False
                        ElseIf .Rows(irow).Cells("Col_cboRoute").Value.ToString() = "" Then
                            isinsert = False
                            '_chk_NotRoute = False
                        End If
                        If .Rows(irow).Cells("col_cbosubRoute").Value Is Nothing Then
                            isinsert = False
                            '_chk_NotRoute = False
                        ElseIf .Rows(irow).Cells("col_cbosubRoute").Value.ToString() = "" Then
                            isinsert = False
                            '_chk_NotRoute = False
                        End If
                        If .Rows(irow).Cells("col_cboTransportRegion").Value Is Nothing Then
                            isinsert = False
                            '_chk_NotRoute = False
                        ElseIf .Rows(irow).Cells("col_cboTransportRegion").Value.ToString() = "" Then
                            isinsert = False
                            ' _chk_NotRoute = False
                        End If
                        If isinsert = True Then
                            objCusShip.Update_Route_subRoute(.Rows(irow).Cells("ColIndex").Value.ToString, .Rows(irow).Cells("Col_cboRoute").Value.ToString, .Rows(irow).Cells("col_cbosubRoute").Value.ToString, .Rows(irow).Cells("col_cboTransportRegion").Value.ToString)
                            '_chk_NotRoute = True
                        End If
                    End With
                Next
            Else
                ' For irow As Integer = 0 To grdCustomer_Shipping.Rows.Count - 1
                While (countROW >= 0)
                    Dim isinsert As Boolean = True
                    If Me.grdCustomer_Shipping.Rows(countROW).Cells("Col_cboRoute").Value Is Nothing Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือก สายจัดส่ง บรรทัดรหัสที่ " + grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    ElseIf grdCustomer_Shipping.Rows(countROW).Cells("Col_cboRoute").Value.ToString() = "" Or grdCustomer_Shipping.Rows(countROW).Cells("Col_cboRoute").Value = "0010000000000" Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือก สายจัดส่ง บรรทัดรหัสที่ " + grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    End If
                    If grdCustomer_Shipping.Rows(countROW).Cells("col_cbosubRoute").Value Is Nothing Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือกสายจัดส่งย่อยบรรทัด รหัสที่ " + grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    ElseIf grdCustomer_Shipping.Rows(countROW).Cells("col_cbosubRoute").Value.ToString() = "" Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือกสายจัดส่งย่อยบรรทัด รหัสที่ " + grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    End If

                    If grdCustomer_Shipping.Rows(countROW).Cells("col_cboTransportRegion").Value Is Nothing Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือกเขตพื้นที่จัดส่งบรรทัด รหัสที่ " + grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    ElseIf grdCustomer_Shipping.Rows(countROW).Cells("col_cboTransportRegion").Value.ToString() = "" Then
                        isinsert = False
                        _chk_NotRoute = False
                        W_MSG_Information("กรุณาเลือกเขตพื้นที่จัดส่งบรรทัด รหัสที่ " & grdCustomer_Shipping.Rows(countROW).Cells("ColCustomerID").Value.ToString)
                        Exit Sub
                    End If

                    If isinsert = True Then
                        objCusShip.Update_Route_subRoute(grdCustomer_Shipping.Rows(countROW).Cells("ColIndex").Value.ToString, grdCustomer_Shipping.Rows(countROW).Cells("Col_cboRoute").Value.ToString, grdCustomer_Shipping.Rows(countROW).Cells("col_cbosubRoute").Value.ToString, grdCustomer_Shipping.Rows(countROW).Cells("col_cboTransportRegion").Value.ToString)
                        grdCustomer_Shipping.Rows.RemoveAt(countROW)
                        grdCustomer_Shipping.Refresh()
                        _chk_NotRoute = True
                    End If
                    countROW = countROW - 1
                    ' Next
                End While
                If grdCustomer_Shipping.Rows.Count = 0 Then
                    W_MSG_Information("บันทึกข้อมูลเรียบร้อย")
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomer_Shipping_No_Route_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Me._SalesOrder_NotRoute Is Nothing Then

        Else
            If grdCustomer_Shipping.Rows.Count = 0 Then
                _chk_NotRoute = True
            End If
        End If
    End Sub

    'Private Sub grdCustomer_Shipping_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdCustomer_Shipping.RowsAdded
    '    Try
    '        With grdCustomer_Shipping.Rows(e.RowIndex)
    '            'Select Case .Columns().Name.ToUpper
    '            '    Case "Col_cboRoute".ToUpper

    '            'End Select

    '            getComboSubRoute(.Cells("Col_cboRoute").Value, e.RowIndex)
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

  
End Class