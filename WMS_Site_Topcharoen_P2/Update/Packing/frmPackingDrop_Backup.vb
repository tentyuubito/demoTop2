Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Imports WMS_STD_Master_Datalayer

Public Class frmPackingDrop_Backup

    Dim dtSalesOrder As New DataTable
    Dim dtSalesOrderGroupItem As New DataTable
    Private _strSalesOrder_Index As String = ""
    Private _strProductType_Index As String = ""
    Private _customer_shipping_index As String = ""
    Private _Customer_Shipping_Location_Index As String = ""

    Private Sub btnClosedPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosedPacking.Click
        Try
            Dim DataSource As DataTable = CType(Me.grdSOView.DataSource, DataTable)
            Dim SelectedRow As DataRow() = DataSource.Select("CheckHeader = 1")
            If Not SelectedRow.Length > 0 Then
                W_MSG_Information("กรุณาทำรายการให้สมบูรณ์อย่างน้อย 1 รายการ")
                Exit Sub
            End If

            If W_MSG_Confirm("ต้องการปิดถุง ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Dim ListIndex As New List(Of String)
            For Each Row As DataRow In SelectedRow
                ListIndex.Add(Row.Item("SalesOrder_Index").ToString)
            Next

            Dim DataCopy As DataTable = dtSalesOrder.Clone
            For Each Row As DataRow In dtSalesOrder.Rows
                If ListIndex.Contains(Row.Item("SalesOrder_Index").ToString) Then
                    DataCopy.Rows.Add(Row.ItemArray)
                End If
            Next

            Dim clsPacking As New Tb_Packing_TopCharoen
            If clsPacking.SavePacking(DataCopy.Select, SelectedRow) Then
                W_MSG_Information("บันทึกเสร็จสิ้น")
                getSo()
            End If

            'Dim dtrowSalesOrder As DataRow() = DataCopy.Select("CheckHeader = 1 and ProductType_Index in (" & _strProductType_Index & ")")
            ''  Dim dtrowSalesOrder2 As DataRow() = dtSalesOrder.Select("ProductType_Index in (" & _strProductType_Index & ") AND IsPrintBarcode = 1 ")

            'If clsPacking.SavePacking(dtrowSalesOrder) Then
            '    W_MSG_Information("บันทึกเสร็จสิ้น")
            '    getSo()
            'End If


            '_strProductType_Index = ""

            'For i As Integer = 0 To grdSoGroupItem.Rows.Count - 1
            '    If grdSoGroupItem.Rows(i).Cells("chkSelect").Value = 1 Then
            '        _strProductType_Index &= "'" & grdSoGroupItem.Rows(i).Cells("Col_ProductType_Index").Value & "',"
            '    End If

            'Next

            'If String.IsNullOrEmpty(_strProductType_Index) Then
            '    W_MSG_Error("กรุณาเลือก ProductType ที่ต้องการแพ็ค")
            '    Exit Sub
            'End If
            '_strProductType_Index = _strProductType_Index.Trim().Substring(0, _strProductType_Index.Length - 1)
            'Dim dtSave As New DataTable
            'Dim clsPacking As New Tb_Packing_TopCharoen

            'Dim DataSource As DataTable = CType(Me.grdSOView.DataSource, DataTable)

            'Dim Selected As DataRow() = DataSource.Select("CheckHeader = 1")

            'Dim ListIndex As New List(Of String)
            'For Each Row As DataRow In Selected
            '    ListIndex.Add(Row.Item("SalesOrder_Index").ToString)
            'Next

            'Dim DataCopy As DataTable = dtSalesOrder.Clone
            'For Each Row As DataRow In dtSalesOrder.Rows
            '    If ListIndex.Contains(Row.Item("SalesOrder_Index").ToString) Then
            '        DataCopy.Rows.Add(Row.ItemArray)
            '    End If
            'Next


            'Dim dtrowSalesOrder As DataRow() = DataCopy.Select("CheckHeader = 1 and ProductType_Index in (" & _strProductType_Index & ")")
            ''  Dim dtrowSalesOrder2 As DataRow() = dtSalesOrder.Select("ProductType_Index in (" & _strProductType_Index & ") AND IsPrintBarcode = 1 ")

            'If clsPacking.SavePacking(dtrowSalesOrder) Then
            '    W_MSG_Information("บันทึกเสร็จสิ้น")
            '    getSo()
            'End If

            'If True Then
            '    clsPacking = Nothing

            'End If

            'Dim frm As New frmPopupScanBarcodePacking
            '    frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrmClose.Click
        Me.Close()

    End Sub

    Private Sub btnCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Shipping_Location.Click
        Try
            Dim strWhere As String = ""

            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = strWhere
            'frm.Customer_Shipping_Index = Consignee_Index
            frm.ShowDialog()

            Dim tmpCustomer_Shipping_Location_Index As String = ""

            _Customer_Shipping_Location_Index = frm.Customer_Shipping_Location_Index


            If Not _Customer_Shipping_Location_Index = "" Then
                Me.txtShipping_Location_ID.Tag = _Customer_Shipping_Location_Index
                getCus_Shipping_Location_Index()
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""

            End If
            If Me.txtShipping_Location_ID.Text = "" Then
                Me.txtShipping_Location_ID.Tag = ""
            End If

            frm.Close()
            getSo()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SetDataGridItem()
        If Me.grdSOView.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim DataSource As DataTable = CType(Me.grdSOView.DataSource, DataTable)
        DataSource.AcceptChanges()

        Dim Selected As DataRow() = DataSource.Select("CheckHeader = 1")
        If Selected.Length > 0 Then
            Dim ListIndex As New List(Of String)
            For Each Row As DataRow In Selected
                ListIndex.Add(Row.Item("SalesOrder_Index").ToString)
            Next

            Dim DataCopy As DataTable = dtSalesOrder.Clone
            For Each Row As DataRow In dtSalesOrder.Rows
                If ListIndex.Contains(Row.Item("SalesOrder_Index").ToString) Then
                    DataCopy.Rows.Add(Row.ItemArray)
                End If
            Next

            Using Data As DataTable = DataCopy.DefaultView.ToTable(True, "Checked", "ProductType_Index", "Group")

                Data.Columns.Item("Checked").ReadOnly = False

                With Data.Columns.Add("Total_Qty", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                With Data.Columns.Add("PickQty", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                For Each Row As DataRow In Data.Rows
                    Row.Item("Total_Qty") = DataCopy.Compute("SUM(Total_Qty)", String.Format("CheckHeader = 1  and   ProductType_Index = '{0}' ", Row.Item("ProductType_Index")))
                    Row.Item("PickQty") = DataCopy.Compute("SUM(PickQty)", String.Format(" CheckHeader = 1  and   ProductType_Index = '{0}' ", Row.Item("ProductType_Index")))
                Next

                grdSoGroupItem.DataSource = Data.Copy

            End Using
        Else
            grdSoGroupItem.DataSource = Nothing
        End If
    End Sub

    Private Sub getSo()
        Try
            _strProductType_Index = ""
            _strSalesOrder_Index = ""
            Dim clsPacking As New Tb_Packing_TopCharoen


            dtSalesOrder = clsPacking.getSalesOrder(_Customer_Shipping_Location_Index, "")
            dtSalesOrder.Columns.Item("CheckHeader").ReadOnly = False

            Using Data As DataTable = dtSalesOrder.DefaultView.ToTable(True, "CheckHeader", "SalesOrder_Index", "SalesOrder_No", "Route_Name", "Company_Name", "Shipping_Location_Name")
                Data.Columns.Item("CheckHeader").ReadOnly = False

                With Data.Columns.Add("Total_Qty", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                With Data.Columns.Add("PickQty", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                With Data.Columns.Add("Barcode_Qty", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                With Data.Columns.Add("Barcode_Qty_Confirm", GetType(Decimal))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                With Data.Columns.Add("Barcode", GetType(DataTable))
                    .SetOrdinal(Data.Columns.Count - 1)
                End With

                For Each Row As DataRow In Data.Rows
                    Row.Item("Total_Qty") = dtSalesOrder.Compute("SUM(Total_Qty)", String.Format("SalesOrder_Index = '{0}'", Row.Item("SalesOrder_Index").ToString))
                    Row.Item("PickQty") = dtSalesOrder.Compute("SUM(PickQty)", String.Format("SalesOrder_Index = '{0}'", Row.Item("SalesOrder_Index").ToString))
                    Row.Item("Barcode_Qty") = dtSalesOrder.Compute("SUM(Barcode_Qty)", String.Format("SalesOrder_Index = '{0}'", Row.Item("SalesOrder_Index").ToString))
                    Row.Item("Barcode_Qty_Confirm") = 0
                Next

                grdSOView.DataSource = Data.Copy

            End Using

            Me.txtValues.Text = "รวม SO : " & FormatNumber(Me.dtSalesOrder.Rows.Count, 0) & " รายการ"

            'SetDataGridItem()


            clsPacking = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getCus_Shipping_Location_Index()
        Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDTms_Shipping_Location As DataTable = New DataTable
        Dim _Postcode As String = ""
        Try
            objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", Me.txtShipping_Location_ID.Tag.ToString)
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then

                Me.txtShipping_Location_ID.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Id").ToString

            Else

                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Shipping_Location = Nothing
            objDTms_Shipping_Location = Nothing
        End Try
    End Sub

    Private Sub SetBackgroundColor()
        Dim Data As DataRow
        For i As Integer = 0 To Me.grdSOView.Rows.Count - 1
            Data = Me.grdSOView.Rows.Item(i).DataBoundItem.Row()

            If Data("Barcode_Qty") = Data("Barcode_Qty_Confirm") Then
                Me.grdSOView.Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGreen
                Data("CheckHeader") = 1
            Else
                Me.grdSOView.Rows.Item(i).DefaultCellStyle.BackColor = Color.White
                Data("CheckHeader") = 0
            End If
        Next
    End Sub

    Private Sub frmPackingDrop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdSOView.AutoGenerateColumns = False
        getSo()
    End Sub

    Private Sub grdSOView_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOView.CellContentClick
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then
            Exit Sub
        End If

        Try
            '' Store the top row before doing the AcceptChanges
            Dim topRow As Integer = grdSOView.FirstDisplayedScrollingRowIndex

            Select Case Me.grdSOView.Columns.Item(e.ColumnIndex).Name
                Case "Check"
                    'Me.grdSOView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    'CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
                    'SetDataGridItem()

                    '' Then after the AcceptChanges do this
                    'grdSOView.FirstDisplayedScrollingRowIndex = topRow

                Case "Col_Barcode"
                    Dim Data As DataRow = Me.grdSOView.Rows.Item(e.RowIndex).DataBoundItem.Row()
                    Dim DataBarcode As DataTable = Nothing
                    If Data("Barcode") IsNot DBNull.Value Then
                        DataBarcode = CType(Data("Barcode"), DataTable)
                    End If

                    Dim frm As New frmConfirmBarcodePacking(Data("Barcode_Qty"))
                    frm.Data = DataBarcode
                    frm.ShowDialog()

                    If frm.Data Is Nothing Then
                        Data("Barcode") = DBNull.Value
                        Data("Barcode_Qty_Confirm") = 0
                    Else
                        Data("Barcode") = frm.Data
                        Data("Barcode_Qty_Confirm") = frm.Data.Rows.Count
                    End If

                    If Data("Barcode_Qty") = Data("Barcode_Qty_Confirm") Then
                        Me.grdSOView.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
                        Data("CheckHeader") = 1
                    Else
                        Me.grdSOView.Rows.Item(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                        Data("CheckHeader") = 0
                    End If

                    grdSOView.FirstDisplayedScrollingRowIndex = topRow
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdSOView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSOView.Sorted
        SetBackgroundColor()
    End Sub
End Class

