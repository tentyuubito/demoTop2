Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Imports WMS_STD_Master_Datalayer

Public Class frmMainPackingBox

    Dim dtSalesOrder As New DataTable
    Dim dtSalesOrderGroupItem As New DataTable
    Private _strSalesOrder_Index As String = ""
    Private _strProductType_Index As String = ""
    Private _customer_shipping_index As String = ""
    Private _Customer_Shipping_Location_Index As String = ""

    Private Sub btnClosedPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmPackingBox
            frm.ShowDialog()

            getSo()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrmClose.Click
        Me.Close()

    End Sub



    Private Sub SetDataGridItem()
        If Me.grdPackingSoGroupView.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim DataSource As DataTable = CType(Me.grdPackingSoGroupView.DataSource, DataTable)
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

                grdPackingSoGroupItem.DataSource = Data.Copy

            End Using
        Else
            grdPackingSoGroupItem.DataSource = Nothing
        End If
    End Sub

    Private Sub getSo()
        Try
            _strProductType_Index = ""
            _strSalesOrder_Index = ""
            Dim clsPacking As New Tb_Packing_TopCharoen

            Dim conditionSearch As String = ""

            If chkDate.Checked Then
                conditionSearch &= " AND CONVERT(DATE, tb_SalesOrderPackingItem.Update_Date) BETWEEN  '" & dateStart.Value.ToString("yyyy-MM-dd") & "' AND '" & dateEnd.Value.ToString("yyyy-MM-dd") & "'"
            End If

            If Not String.IsNullOrEmpty(Me.txtBarcode.Text.Trim) Then
                conditionSearch &= " AND tb_salesOrderpacking.SalesOrderPacking_Index IN ( "
                conditionSearch &= "    SELECT SalesOrderPacking_Index "
                conditionSearch &= "    FROM tb_SalesOrderPackingItem "
                conditionSearch &= "    WHERE Barcode_BAG = '" & Me.txtBarcode.Text.Trim & "' "
                conditionSearch &= "    OR Barcode_GROUP = '" & Me.txtBarcode.Text.Trim & "' "
                conditionSearch &= " ) "
            End If

            If Not String.IsNullOrEmpty(Me.txtSalesOrderNo.Text.Trim) Then
                conditionSearch &= " AND tb_salesOrderpacking.SalesOrderPacking_Index IN ( "
                conditionSearch &= "    SELECT SalesOrderPacking_Index "
                conditionSearch &= "    FROM tb_SalesOrderPackingItem "
                conditionSearch &= "    WHERE SalesOrder_Index IN ( "
                conditionSearch &= " 	    SELECT SalesOrder_Index "
                conditionSearch &= " 	    FROM tb_SalesOrder "
                conditionSearch &= " 	    WHERE SalesOrder_No = '" & Me.txtSalesOrderNo.Text.Trim & "' "
                conditionSearch &= "    ) "
                conditionSearch &= " ) "
            End If

            If Not String.IsNullOrEmpty(Me.txtTransportManifest.Text.Trim) Then
                conditionSearch &= " AND tb_TransportManifest.TransportManifest_No = '" & Me.txtTransportManifest.Text.Trim & "' "
                'conditionSearch &= " AND tb_salesOrderpacking.SalesOrderPacking_Index IN ( "
                'conditionSearch &= "    SELECT SalesOrderPacking_Index "
                'conditionSearch &= "    FROM tb_SalesOrderPackingItem "
                'conditionSearch &= "    WHERE SalesOrder_Index IN ( "
                'conditionSearch &= "        SELECT tmi.SalesOrder_Index "
                'conditionSearch &= "        FROM tb_TransportManifest tm "
                'conditionSearch &= "        INNER JOIN tb_TransportManifestItem tmi "
                'conditionSearch &= "        ON tm.TransportManifest_Index = tmi.TransportManifest_Index "
                'conditionSearch &= "        WHERE tmi.Status <> -1 "
                'conditionSearch &= " 	    AND tm.TransportManifest_No = '" & Me.txtTransportManifest.Text.Trim & "' "
                'conditionSearch &= "        GROUP BY tmi.SalesOrder_Index "
                'conditionSearch &= "    ) "
                'conditionSearch &= " ) "
            End If

            dtSalesOrder = clsPacking.getSalesOrderMainPackingBox(conditionSearch, "")
            Me.grdPackingSoGroupView.DataSource = Nothing
            Me.grdPackingSoGroupItem.DataSource = dtSalesOrder

            Me.txtValues.Text = "รวม : " & FormatNumber(Me.dtSalesOrder.Rows.Count, 0) & " รายการ"
            If dtSalesOrder.Rows.Count <= 0 Then
                Me.grdPackingSoGroupView.DataSource = Nothing
            End If


            clsPacking = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub



    Private Sub SetBackgroundColor()
        'Dim Data As DataRow
        'For i As Integer = 0 To Me.grdPackingSoGroupView.Rows.Count - 1
        '    Data = Me.grdPackingSoGroupView.Rows.Item(i).DataBoundItem.Row()

        '    If Data("Barcode_Qty") = Data("Barcode_Qty_Confirm") Then
        '        Me.grdPackingSoGroupView.Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGreen
        '        Data("CheckHeader") = 1
        '    Else
        '        Me.grdPackingSoGroupView.Rows.Item(i).DefaultCellStyle.BackColor = Color.White
        '        Data("CheckHeader") = 0
        '    End If
        'Next
    End Sub

    Private Sub frmMainPackingBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.chkDate.Checked = True
        grdPackingSoGroupView.AutoGenerateColumns = False
        getSo()
    End Sub



    Private Sub grdSOView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPackingSoGroupView.Sorted
        SetBackgroundColor()
    End Sub

    Private Sub grdPackingSoGroupItem_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPackingSoGroupItem.SelectionChanged
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim Barcode As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Barcode").Value
            Dim dtSalesOrderItem As New DataTable
            dtSalesOrderItem = clsPacking.getSalesOrderMainPackingItemBox(Barcode, "")

            Me.grdPackingSoGroupView.DataSource = dtSalesOrderItem
            Label1.Text = dtSalesOrderItem.Rows.Count & " รายการ"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getSo()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Barcode").Value
            Dim dtSalesOrderItem As New DataTable


            If W_MSG_Confirm("คุณต้องการลบรายการนี้หรือไม่ ?") = Windows.Forms.DialogResult.Yes Then
                If clsPacking.CancelSalesOrderPackingBox(index) Then
                    W_MSG_Information("ลบเสร็จสิ้น")
                    getSo()
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            '  Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
            Dim index As String = ""
            For i As Integer = 0 To grdPackingSoGroupView.Rows.Count - 1
                index &= "'" & Me.grdPackingSoGroupView.Rows(i).Cells("Col_SalesOrderPacking_index2").Value & "',"
            Next


            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New ReportDocument

            oCrystal = clsPacking.GetReportInfo("rptTransferDocRange", " and SalesOrderPacking_index in (" & index.Trim().Substring(0, index.Length - 1) & ") ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnPrint2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint2.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Barcode").Value

            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New ReportDocument

            oCrystal = clsPacking.GetReportInfo("rptTransferPackingDocRange", " and Barcode_Box = '" & index & "' ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub


    'Private Sub btnPrintWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintWarranty.Click
    '    Try
    '        Dim clsPacking As New Tb_Packing_TopCharoen
    '        Dim clsConfig_Report As New config_Report
    '        Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
    '        Dim oCrystal As New ReportDocument

    '        Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Barcode").Value

    '        Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
    '        Dim oCrystal2 As New ReportDocument

    '        oCrystal = clsPacking.GetReportInfo("rptWarranty_TOR", "and Barcode_Box = '" & index & "' ")
    '        frmReport.CrystalReportViewer1.ReportSource = oCrystal
    '        frmReport.ShowDialog()

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try
    'End Sub

    Private Sub grdPackingSoGroupView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPackingSoGroupView.CellContentClick

    End Sub
End Class

