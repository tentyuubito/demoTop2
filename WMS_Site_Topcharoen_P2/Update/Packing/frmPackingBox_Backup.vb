Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Imports WMS_STD_Master_Datalayer

Public Class frmPackingBox_Backup

    Dim dtSalesOrder As New DataTable
    Dim dtSalesOrderGroupItem As New DataTable
    Private _strSalesOrder_Index As String = ""
    Private _strProductType_Index As String = ""
    Private _customer_shipping_index As String = ""
    Private _Customer_Shipping_Location_Index As String = ""

    Private Sub frmPackingBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosedPacking.Click
        Try

            'If String.IsNullOrEmpty(txtBarcodeBAG.Text) Then
            '    W_MSG_Information("กรุณากรอก Barcode Bag")
            '    Exit Sub
            'End If

            _strSalesOrder_Index = ""

            Dim BarcodeScan As String = ""
            For i As Integer = 0 To grdSOView.Rows.Count - 1
                If grdSOView.Rows(i).Cells("chkSelect").Value = 1 Then
                    _strSalesOrder_Index &= "'" & grdSOView.Rows(i).Cells("Col_SalesOrder_index").Value & "',"
                    BarcodeScan &= "'" & grdSOView.Rows(i).Cells("Barcode_BAG").Value & "',"
                End If

            Next

            If String.IsNullOrEmpty(_strSalesOrder_Index) Then
                W_MSG_Error("กรุณาเลือก SalesOrder ที่ต้องการเบิก !!")
                Exit Sub
            End If
            _strSalesOrder_Index = _strSalesOrder_Index.Trim().Substring(0, _strSalesOrder_Index.Length - 1)
            BarcodeScan = BarcodeScan.Trim().Substring(0, BarcodeScan.Length - 1)
            Dim dtSave As New DataTable
            Dim clsPacking As New Tb_Packing_TopCharoen

            '   Dim dtrowSalesOrder As DataRow() = dtSalesOrder.Select("ProductType_Index in (" & _strProductType_Index & ")")
            '   Dim dtrowSalesOrder2 As DataRow() = dtSalesOrder.Select("ProductType_Index in (" & _strProductType_Index & ") AND IsPrintBarcode = 1 ")

            Dim getBarcodeBox As String = "BOX" & Now.ToString("yyyyMMddhhss")


            If clsPacking.SavePackingBox(_strSalesOrder_Index, BarcodeScan, getBarcodeBox) Then
                W_MSG_Information("บันทึกเสร็จสิ้น")
                getSo()
            End If

            '   Dim oReport As New WMS_STD_OUTB_Report.Loading_Report("rptTransferDocRange", "")
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument


            oCrystal = GetReportInfo("rptTransferDocRange", " and barcode_bag in (" & BarcodeScan & ")")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()


            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New ReportDocument

            oCrystal2 = GetReportInfo("rptTransferPackingDocRange", " and barcode_box = '" & getBarcodeBox & "'")
            frmReport2.CrystalReportViewer1.ReportSource = oCrystal2
            frmReport2.ShowDialog()

            If True Then
                clsPacking = Nothing

            End If

            Dim frm As New frmPopupScanBarcodePacking
            '    frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub


    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet
        Dim clsPacking As New Tb_Packing_TopCharoen
        odt = clsPacking.GetDataReport(pstrReportName)

        For i As Integer = 0 To odt.Rows.Count - 1

            If odt.Rows(i)("IsVisible") = 1 Then
                oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            End If

            ods.Tables.Add(clsPacking.GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal

    End Function


    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrmClose.Click
        Me.Close()

    End Sub

    Private Sub getSo()
        Try


            _strProductType_Index = ""
            _strSalesOrder_Index = ""
            Dim clsPacking As New Tb_Packing_TopCharoen


            dtSalesOrder = clsPacking.getSalesOrderPacking(_customer_shipping_index, "")
            grdSOView.AutoGenerateColumns = False
            grdSOView.DataSource = dtSalesOrder 'dtSalesOrder.DefaultView.ToTable(True, "SalesOrder_Index", "SalesOrder_No", "Route_Name", "Company_Name", "Shipping_Location_Name")


            '    grdSoGroupItem.DataSource = dtSalesOrder.DefaultView.ToTable(True, "ProductType_Id", "Group", "Eye", "Add", "Tilted", "Color", "Degree", "BC", "VMI", "Generation", "Brand", "Total_Qty")
            'Using Data As DataTable = dtSalesOrder.DefaultView.ToTable(True, "Checked", "ProductType_Index", "Group")
            '    Data.Columns.Item("Checked").ReadOnly = False

            '    With Data.Columns.Add("Total_Qty", GetType(Decimal))
            '        .SetOrdinal(Data.Columns.Count - 1)
            '    End With

            '    For Each Row As DataRow In Data.Rows
            '        Row.Item("Total_Qty") = dtSalesOrder.Compute("SUM(Total_Qty)", String.Format(" ProductType_Index = '{0}' ", Row.Item("ProductType_Index")))
            '    Next
            '    grdSOView.DataSource = Data.Copy

            'End Using

            clsPacking = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub



    Private Sub frmPackingDrop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getSo()
    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim frm As New frmConsignee_Popup

            frm.ShowDialog()

            Dim tmp_Index As String = ""
            _customer_shipping_index = frm.Consignee_Index
            txtConsignee_Id.Text = frm.Consignee_ID
            tmp_Index = frm.Consignee_Index

            frm.Close()

            getSo()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtBarcodeBAG_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcodeBAG.KeyPress
        If e.KeyChar <> ChrW(Keys.Enter) Then
            Exit Sub
        End If

        For i As Integer = 0 To grdSOView.Rows.Count - 1
            If Me.grdSOView.Rows(i).Cells("Barcode_BAG").Value = txtBarcodeBAG.Text Then
                Me.grdSOView.Rows(i).Cells("Col_ConfirmBAG").Value = "PASS"
                Me.grdSOView.Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                Me.grdSOView.Rows(i).Cells("chkSelect").Value = 1
            End If
        Next

        txtBarcodeBAG.Text = ""


    End Sub

    Private Sub grdSOView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSOView.Sorted
        Try
            For i As Integer = 0 To grdSOView.Rows.Count - 1
                If Me.grdSOView.Rows(i).Cells("chkSelect").Value = 1 Then
                    Me.grdSOView.Rows(i).Cells("Col_ConfirmBAG").Value = "PASS"
                    Me.grdSOView.Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                    Me.grdSOView.Rows(i).Cells("chkSelect").Value = 1
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class

