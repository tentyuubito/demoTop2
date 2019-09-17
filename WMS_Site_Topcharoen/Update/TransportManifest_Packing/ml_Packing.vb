Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Public Class ml_Packing
    Private printDialog1 As New PrintDialog
    Private printDocument1 As New System.Drawing.Printing.PrintDocument

    Private appSet As New Configuration.AppSettingsReader()


    Private _Seq As Integer = 0
    Public Property Seq() As Integer
        Get
            Return _Seq
        End Get
        Set(ByVal value As Integer)
            _Seq = value
        End Set
    End Property

    Public _isPrint As Boolean = False
    Public Sub PrintBarcodePack_SNP(ByVal SalesOrderPacking_Index As String, ByVal strParamValue As String)
        Try
            Dim ds As New WMS_STD_OUTB_Report.dsBarcodeBox
            Dim dr As DataRow
            Dim objtb_SOP As New tb_SalesOrderPacking
            Dim dtObj As New DataTable
            Dim strWhere As String
            Dim objBarcode As New ml_Barcode
            strWhere = " And  SalesOrderPacking_Index='" & SalesOrderPacking_Index & "'"
            dtObj = objtb_SOP.GetBarcodePackingPrint(strWhere)

            If dtObj.Rows.Count > 0 Then
                With dtObj.Rows(0)
                    dr = ds.Tables(0).NewRow
                    dr("InvoiceNo") = .Item("SalesOrder_No").ToString
                    dr("ShiptoName") = .Item("Company_Name").ToString
                    dr("ShiptoAddress") = .Item("Address").ToString
                    dr("Packsize") = .Item("Description").ToString
                    dr("BarcodeName") = .Item("BarcodePacking").ToString
                    dr("Seq") = .Item("Seq").ToString
                    _Seq = dr("Seq")
                    dr("Barcode") = objBarcode.GenCodeToByte(.Item("BarcodePacking").ToString)
                    ds.Tables(0).Rows.Add(dr)
                End With

            End If

            ds.Tables(0).TableName = "BarcodeBox"

            Dim objtb_SOP1 As New tb_SalesOrderPackingItem
            Dim dtObj1 As New DataTable
            strWhere = " And  SalesOrderPacking_Index='" & SalesOrderPacking_Index & "'"
            dtObj1 = objtb_SOP1.GetAllDataSOPDetail(strWhere)

            If dtObj1.Rows.Count > 0 Then
                For i As Integer = 0 To dtObj1.Rows.Count - 1
                    With dtObj1.Rows(i)
                        If Not ds.Tables(1).Columns.Contains("Seq") Then ds.Tables(1).Columns.Add("Seq", GetType(Double))
                        dr = ds.Tables(1).NewRow
                        dr("Invoice_No") = .Item("Invoice_No").ToString
                        dr("strBox") = .Item("strBox").ToString
                        dr("Str1") = .Item("Str1").ToString
                        dr("Sku_Id") = .Item("Sku_Id").ToString
                        dr("Qty_Pack") = .Item("Qty_Pack").ToString
                        dr("BarcodePacking") = .Item("BarcodePacking").ToString
                        dr("Seq") = i + 1
                        ds.Tables(1).Rows.Add(dr)
                    End With
                Next
            End If


            ds.Tables(1).TableName = "BarcodeBoxDetail"


            Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim cry As New ReportDocument

            cry = New WMS_STD_OUTB_Report.rptBarcodeBox_SNP

            Dim strPirnterName As String = appSet.GetValue("Printer_Name_Box", GetType(String))
            If strPirnterName <> "" Then
                cry.PrintOptions.PrinterName = strPirnterName
                '--------------- Set Parameter --------------------------
                cry.SetParameterValue(0, strParamValue)
                '--------------------------------------------------------
                cry.SetDataSource(ds)
                cry.PrintToPrinter(1, False, 0, 0)
            Else
                'Set DataSource
                cry.SetDataSource(ds)
                '--------------- Set Parameter --------------------------
                cry.SetParameterValue(0, strParamValue)
                '--------------------------------------------------------
                frmReportPreview.CrystalReportViewer1.ReportSource = cry
                frmReportPreview.ShowDialog()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub




    Public Sub PrintBarcodePack(ByVal SalesOrderPacking_Index As String, Optional ByVal boolMulti As Boolean = False)
        Try
            Dim ds As New dsBarcodeBox
            Dim dr As DataRow
            Dim objtb_SOP As New tb_SalesOrderPacking
            Dim dtObj As New DataTable
            Dim strWhere As String
            Dim dblPackQty As Double = 0


            Dim objtb_SOP1 As New tb_SalesOrderPackingItem
            Dim dtObj1 As New DataTable
            strWhere = " And  SalesOrderPacking_Index='" & SalesOrderPacking_Index & "'"
            If boolMulti = False Then
                dtObj1 = objtb_SOP1.GetAllDataSOPDetail(strWhere)
            Else
                strWhere = " And  SalesOrderPacking_Index in (" & SalesOrderPacking_Index & ")"
                dtObj1 = objtb_SOP1.GetAllDataSOPDetail(strWhere)
            End If

            Dim Customer_Index As String = dtObj1.Rows(0)("Customer_Index").ToString

            If dtObj1.Rows.Count > 0 Then
                For i As Integer = 0 To dtObj1.Rows.Count - 1
                    With dtObj1.Rows(i)
                        If Not ds.Tables(0).Columns.Contains("Seq") Then ds.Tables(0).Columns.Add("Seq", GetType(Double))
                        dr = ds.Tables(0).NewRow
                        dr("Invoice_No") = .Item("Invoice_No").ToString
                        dr("InvoiceNo") = .Item("InvoiceNo").ToString
                        dr("strBox") = .Item("strBox").ToString
                        dr("Str1") = .Item("Str1").ToString
                        dr("Sku_Id") = .Item("Sku_Id").ToString
                        dr("Qty_Pack") = .Item("Qty_Pack").ToString
                        dr("BarcodePacking") = .Item("BarcodePacking").ToString
                        dr("Pack_Desc") = .Item("Pack_Desc").ToString
                        dr("Weight") = .Item("Weight")
                        dr("Seq") = .Item("SeqNo")

                        dr("ShiptoName") = .Item("Company_Name").ToString
                        dr("ShiptoAddress") = .Item("Address").ToString
                        dr("Packsize") = .Item("Description").ToString
                        dr("Group") = .Item("Group").ToString
                        dr("Line") = .Item("Line").ToString
                        dr("Packing_Date") = .Item("Packing_Date").ToString
                        dr("TransportManifestItem_Index") = .Item("TransportManifestItem_Index").ToString
                        dr("MaxRow") = .Item("MaxRow")
                        dr("Expected_Delivery_Date") = .Item("Expected_Delivery_Date")
                        dr("Expected_Delivery_Date") = .Item("Expected_Delivery_Date")
                        dr("Tel") = .Item("Tel")
                        dblPackQty += dr("Qty_Pack")
                        ds.Tables(0).Rows.Add(dr)
                    End With
                Next
            End If


            ds.Tables(0).TableName = "BarcodeBoxDetail"

            





            'Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain

            'Dim cry As New rptBarcodeBox_KSL
            Using cry As New rptBarcodeBox_KSL
                Dim strPirnterName As String = ""
                If strPirnterName <> "" Then
                    cry.PrintOptions.PrinterName = strPirnterName
                    cry.SetDataSource(ds)
                    cry.SetParameterValue("PackQty", dblPackQty)
                    cry.SetParameterValue("Customer_Index", Customer_Index)
                    cry.PrintToPrinter(1, False, 0, 0)
                Else
                    'Set DataSource
                    cry.SetDataSource(ds)
                    cry.SetParameterValue("PackQty", dblPackQty)
                    cry.SetParameterValue("Customer_Index", Customer_Index)
                    If _isPrint = True Then
                        Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain
                        frmReportPreview.CrystalReportViewer1.ReportSource = cry
                        frmReportPreview.ShowDialog()

                        'Clear
                        cry.Close()
                        cry.Dispose()
                        frmReportPreview.CrystalReportViewer1.Dispose()

                        Exit Sub
                    End If


                    'print dailog
                    Me.printDialog1.Document = Me.printDocument1
                    Dim drl As DialogResult = Me.printDialog1.ShowDialog()
                    If drl = Windows.Forms.DialogResult.OK Then
                        '---------------------------------------------------------------------------------------------
                        'Get the Copy times
                        Dim nCopy As Integer = Me.printDocument1.PrinterSettings.Copies
                        'Get the number of Start Page
                        Dim sPage As Integer = Me.printDocument1.PrinterSettings.FromPage
                        'Get the number of End Page
                        Dim ePage As Integer = Me.printDocument1.PrinterSettings.ToPage
                        'Get the printer name
                        Dim PrinterName As String = Me.printDocument1.PrinterSettings.PrinterName
                        'Dim crReportDocument As New ReportDocument
                        ''Create an instance of a report
                        'crReportDocument = New Chart()
                        Try
                            'Set the printer name to print the report to. By default the sample
                            'report does not have a defult printer specified. This will tell the
                            'engine to use the specified printer to print the report. Print out 
                            'a test page (from Printer properties) to get the correct value.
                            cry.PrintOptions.PrinterName = PrinterName
                            'Start the printing process. Provide details of the print job
                            'using the arguments.
                            cry.PrintToPrinter(nCopy, False, sPage, ePage)
                            'Let the user know that the print job is completed
                            'MessageBox.Show("Report finished printing!")


                        Catch err As Exception
                            MessageBox.Show(err.ToString())
                        Finally
                            'Clear
                            cry.Close()
                            cry.Dispose()
                        End Try
                    End If
                End If
            End Using

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Public Sub PrintBarcodePackBySalesOrderNo(ByVal SalesOrder_NO As String)
        Try
            Dim ds As New WMS_STD_OUTB_Report.dsBarcodeBox
            Dim dr As DataRow
            Dim objtb_SOP As New tb_SalesOrderPacking
            Dim dtObj As New DataTable
            Dim strWhere As String
            Dim objBarcode As New ml_Barcode
            strWhere = " And  SalesOrder_No='" & SalesOrder_NO & "'"
            dtObj = objtb_SOP.GetBarcodePackingPrint(strWhere)

            If dtObj.Rows.Count > 0 Then
                With dtObj.Rows(0)
                    dr = ds.Tables(0).NewRow
                    dr("InvoiceNo") = .Item("SalesOrder_No").ToString
                    dr("ShiptoName") = .Item("Company_Name").ToString
                    dr("ShiptoAddress") = .Item("Address").ToString
                    dr("Packsize") = .Item("Description").ToString
                    dr("BarcodeName") = .Item("BarcodePacking").ToString
                    dr("Seq") = .Item("Seq").ToString
                    _Seq = dr("Seq")
                    dr("Barcode") = objBarcode.GenCodeToByte(.Item("BarcodePacking").ToString)
                    ds.Tables(0).Rows.Add(dr)
                End With

            End If

            ds.Tables(0).TableName = "BarcodeBox"

            Dim objtb_SOP1 As New tb_SalesOrderPackingItem
            Dim dtObj1 As New DataTable
            strWhere = " And  SalesOrder_No='" & SalesOrder_NO & "'"
            dtObj1 = objtb_SOP1.GetAllDataSOPDetail(strWhere)

            If dtObj1.Rows.Count > 0 Then
                For i As Integer = 0 To dtObj1.Rows.Count - 1
                    With dtObj1.Rows(i)
                        If Not ds.Tables(1).Columns.Contains("Seq") Then ds.Tables(1).Columns.Add("Seq", GetType(Double))
                        dr = ds.Tables(1).NewRow
                        dr("Invoice_No") = .Item("Invoice_No").ToString
                        dr("strBox") = .Item("strBox").ToString
                        dr("Str1") = .Item("Str1").ToString
                        dr("Sku_Id") = .Item("Sku_Id").ToString
                        dr("Qty_Pack") = .Item("Qty_Pack").ToString
                        dr("BarcodePacking") = .Item("BarcodePacking").ToString
                        dr("Seq") = i + 1
                        ds.Tables(1).Rows.Add(dr)
                    End With
                Next
            End If


            ds.Tables(1).TableName = "BarcodeBoxDetail"


            Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim cry As New ReportDocument

            cry = New WMS_STD_OUTB_Report.rptBarcodeBox2
            Dim strPirnterName As String = appSet.GetValue("Printer_Name_Box", GetType(String))
            If strPirnterName <> "" Then
                cry.PrintOptions.PrinterName = strPirnterName
                cry.SetDataSource(ds)
                cry.PrintToPrinter(1, False, 0, 0)
            Else
                'Set DataSource
                cry.SetDataSource(ds)
                frmReportPreview.CrystalReportViewer1.ReportSource = cry
                frmReportPreview.ShowDialog()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Public Sub PrintBarcodePackDetail(ByVal SalesOrderPacking_Index As String)
        Try
            Dim ds As New WMS_STD_OUTB_Report.dsBarcodeBoxDetail
            Dim dr As DataRow
            Dim objtb_SOP As New tb_SalesOrderPackingItem
            Dim dtObj As New DataTable
            Dim strWhere As String
            strWhere = " And  SalesOrderPacking_Index='" & SalesOrderPacking_Index & "'"
            dtObj = objtb_SOP.GetAllDataSOPDetail(strWhere)

            If dtObj.Rows.Count > 0 Then
   

                For i As Integer = 0 To dtObj.Rows.Count - 1
                    With dtObj.Rows(i)
                        dr = ds.Tables(0).NewRow
                        dr("Invoice_No") = .Item("Invoice_No").ToString
                        dr("strBox") = .Item("strBox").ToString
                        dr("Str1") = .Item("Str1").ToString
                        dr("Sku_Id") = .Item("Sku_Id").ToString
                        dr("Qty_Pack") = .Item("Qty_Pack").ToString
                        dr("BarcodePacking") = .Item("BarcodePacking").ToString
                        dr("Company_Name") = .Item("Company_Name").ToString
                        dr("Address") = .Item("Address").ToString
                        ds.Tables(0).Rows.Add(dr)
                    End With
                Next
            End If

            ''-----------------------------------------------------
            'ไม่ถามก่อนพิมพ์
            'Set Printer
            Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim cry As New ReportDocument

            cry = New WMS_STD_OUTB_Report.rptBarcodeBoxDetail

            Dim oPS As New System.Drawing.Printing.PrinterSettings
            Dim strPirnterName As String = "" 'oPS.PrinterName ' appSet.GetValue("Printer_Name_Box", GetType(String))
            If strPirnterName <> "" Then
                cry.PrintOptions.PrinterName = strPirnterName
                cry.SetDataSource(ds)
                cry.PrintToPrinter(1, False, 0, 0)
            Else
                'ถามก่อนพิมพ์
                cry.SetDataSource(ds)
                frmReportPreview.CrystalReportViewer1.ReportSource = cry
                frmReportPreview.ShowDialog()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Public Sub PrintBarcodePackDetail_SNP(ByVal SalesOrderPacking_Index As String)
        Try
            Dim ds As New WMS_STD_OUTB_Report.dsBarcodeBoxDetail
            Dim dr As DataRow
            Dim objtb_SOP As New tb_SalesOrderPackingItem
            Dim dtObj As New DataTable
            Dim strWhere As String
            strWhere = " And  SalesOrderPacking_Index='" & SalesOrderPacking_Index & "'"
            dtObj = objtb_SOP.GetAllDataSOPDetail(strWhere)

            If dtObj.Rows.Count > 0 Then
                For i As Integer = 0 To dtObj.Rows.Count - 1
                    With dtObj.Rows(i)
                        dr = ds.Tables(0).NewRow
                        dr("Invoice_No") = .Item("Invoice_No").ToString
                        dr("strBox") = .Item("strBox").ToString
                        dr("Str1") = .Item("Str1").ToString
                        dr("Sku_Id") = .Item("Sku_Id").ToString
                        dr("Qty_Pack") = .Item("Qty_Pack").ToString
                        dr("BarcodePacking") = .Item("BarcodePacking").ToString
                        dr("Company_Name") = .Item("Company_Name").ToString
                        dr("Address") = .Item("Address").ToString
                        dr("Invoice_Date") = .Item("Invoice_Date").ToString
                        dr("Package_Name") = .Item("Package_Name").ToString
                        ds.Tables(0).Rows.Add(dr)

                    End With
                Next
            End If

            ''-----------------------------------------------------
            'ไม่ถามก่อนพิมพ์
            'Set Printer
            Dim frmReportPreview As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim cry As New ReportDocument

            cry = New WMS_STD_OUTB_Report.rptBarcodeBoxDetail_SNP

            Dim oPS As New System.Drawing.Printing.PrinterSettings
            Dim strPirnterName As String = ""
            If strPirnterName <> "" Then
                cry.PrintOptions.PrinterName = strPirnterName
                cry.SetDataSource(ds)
                cry.PrintToPrinter(1, False, 0, 0)
            Else
                'ถามก่อนพิมพ์
                cry.SetDataSource(ds)
                frmReportPreview.CrystalReportViewer1.ReportSource = cry
                frmReportPreview.ShowDialog()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Public Sub UpdateStatusPrint(ByVal SalesOrderPacking_Index As String)
        ' ====== TODO: HARDCODE-MSG   
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.
        'If dgvDataBarcode.Rows.Count <= 0 Then Exit Sub
        Dim _BarcodePacking As String = ""
        Dim objPOTransaction As New PackingTransaction(PackingTransaction.enuOperation_Type.UPDATE)
        Dim oPo As New tb_SalesOrderPacking
        'Dim dr() As DataRow
        Dim dt As New DataTable
        dt = oPo.GetStatusPrint(" AND SalesOrderPacking_Index ='" & SalesOrderPacking_Index & "'")
        'dr = dt.Select("SalesOrderPacking_Index ='" & _SalesOrderPacking_Index & "'", "")
        If dt.Rows.Count > 0 Then
            _BarcodePacking = dt.Rows(0).Item("BarcodePacking")
            If dt.Rows(0).Item("Count_Print") > 0 Then
                oPo.Status_Print = 1
                oPo.Count_Print = dt.Rows(0).Item("Count_Print") + 1
            Else
                oPo.Status_Print = 1
                oPo.Count_Print = 1
            End If
            oPo.SalesOrder_Index = dt.Rows(0).Item("SalesOrder_Index").ToString
        Else
            oPo.SalesOrder_Index = ""
            oPo.Status_Print = 1
            oPo.Count_Print = 1
        End If

        oPo.BarcodePacking = _BarcodePacking
        Try

            If objPOTransaction.UpdatePrint(oPo) Then
                '                MessageBox.Show("ยืนยันรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Me.getSalesOrderPacking()
            Else
                MessageBox.Show("ไม่อัพเดทรายการพิมพ์ได้ ระบบทำงานผิดพลาด", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            objPOTransaction = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objPOTransaction = Nothing
        End Try
    End Sub


End Class
