Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine

Public Class clsReport : Inherits DBType_SQLServer

    Public Function BarcodeByte(ByVal TextBarcode As String, Optional ByVal ShowBarcodeText As Boolean = True, Optional ByVal BarcodeType As BarcodeNETWorkShop.BARCODE_TYPE = BarcodeNETWorkShop.BARCODE_TYPE.CODE128B, Optional ByVal RotateAngle As BarcodeNETWorkShop.ROTATE_ANGLE = BarcodeNETWorkShop.ROTATE_ANGLE.R0) As Byte()
        Try

            Dim objBarcode As New BarcodeNETWorkShop.BarcodeNETWindows
            Dim pic As System.Drawing.Image
            Dim BitmapConverter As System.ComponentModel.TypeConverter
            Dim ResultByte() As Byte

            objBarcode.BarcodeText = TextBarcode
            objBarcode.BarcodeType = BarcodeType
            objBarcode.RotateAngle = RotateAngle
            objBarcode.BarcodeColor = Color.Black
            objBarcode.TextColor = Color.Black
            objBarcode.ShowBarcodeText = ShowBarcodeText

            pic = objBarcode.GetBarcodeBitmap()
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())

            Return ResultByte
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenCodeToByte(ByVal pText As String) As Byte()
        Try
            Dim ResultByte() As Byte
            Dim pic As System.Drawing.Image = GenCode128.Code128Rendering.MakeBarcodeImage(pText, 1, False)
            Dim BitmapConverter As System.ComponentModel.TypeConverter
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetReportInfoPickingLIst(ByVal Report_Name As String, ByVal salesOrder_No As String) As ReportDocument
        Try

       
            Dim oDt As New DataTable()
            Dim oReportDocument As New ReportDocument()
            Dim oDS As New DataSet()

            Dim oConfig As New config_Report()
            If WV_Report_Path = Nothing Then
                oConfig.GetDataReport_Developer(Report_Name)
                oDt = oConfig.GetDataTable()
            Else
                oConfig.GetDataReport(Report_Name)
                oDt = oConfig.GetDataTable()
            End If


            oReportDocument.Load(WV_Report_Path & oDt.Rows(0)("Report_Path").ToString())

            '  oDS.Tables.Add(oConfig.GetReportData(oDt.Rows(0)("View_Name").ToString), "")
            oDS.DataSetName = oDt.Rows(0)("DataSet_Name").ToString()


            Dim strWhere As String = ""

            Dim objtb_SalesOrderPacking As New WMS_STD_OUTB_Transport.tb_SalesOrderPackingItem
            Dim objtb_SalesOrderPackingHead As New PackingTransaction(PackingTransaction.enuOperation_Type.SEARCH)

            Dim objDT As New DataTable

            strWhere = " AND (SalesOrder_No = '" & salesOrder_No & "') "
            '   End If


            objtb_SalesOrderPackingHead.GetAllDataSOP(strWhere)
            objDT = objtb_SalesOrderPackingHead.DataTable

            Dim salesORderPacking_Index As String = ""
            If objDT.Rows.Count > 0 Then
                For Each dtrow As DataRow In objDT.Rows
                    salesORderPacking_Index &= "'" & dtrow.Item("SalesOrderPacking_Index").ToString & "',"
                Next
            Else

                Return oReportDocument
            End If


            salesORderPacking_Index = salesORderPacking_Index.Remove(salesORderPacking_Index.Length - 1)

            strWhere = " AND SalesOrderPacking_Index in  (" & salesORderPacking_Index & ") "

            objtb_SalesOrderPacking.GetAllDataSOP_Item(strWhere)

            Dim dtTemp As DataTable = objtb_SalesOrderPacking.DataTable
            '  objDT = objtb_SalesOrderPacking.DataTable


            '   strWhere
            '  Dim salesOrder_no As String = "asdasjkpfkpgfgfg"

            Dim frem As Byte() = Me.BarcodeByte(salesOrder_No, True)

            oDS.Tables.Add(dtTemp)
            oDS.Tables(0).TableName = oDt.Rows(0)("DataTable_Name").ToString()


            ' END Fix Report

            ' oDS.Tables(i).AcceptChanges()
            oReportDocument.SetDataSource(oDS)

            Return oReportDocument
        Catch ex As Exception
            Throw ex
        End Try


    End Function


    Public Function GetReportInfo(ByVal Report_Name As String, ByVal strWhere As String) As ReportDocument
        Dim oDt As New DataTable()
        Dim oReportDocument As New ReportDocument()
        Dim oDS As New DataSet()

        Dim oConfig As New config_Report()
        If WV_Report_Path = Nothing Then
            oConfig.GetDataReport_Developer(Report_Name)
            oDt = oConfig.GetDataTable()
        Else
            oConfig.GetDataReport(Report_Name)
            oDt = oConfig.GetDataTable()
        End If

        For i As Integer = 0 To oDt.Rows.Count - 1
            If oDt.Rows(i)("IsVisible") = 1 Then
                oReportDocument.Load(WV_Report_Path & oDt.Rows(i)("Report_Path").ToString())
            End If
            oDS.Tables.Add(oConfig.GetReportData(oDt.Rows(i)("View_Name").ToString, strWhere))
            oDS.DataSetName = oDt.Rows(i)("DataSet_Name").ToString()
            oDS.Tables(i).TableName = oDt.Rows(i)("DataTable_Name").ToString()
            For Each dc As DataColumn In oDS.Tables(i).Columns
                If (dc.ColumnName.StartsWith("Barcode_")) Then
                    Dim _barcode_Column_Name As String = dc.ColumnName
                    Dim _text_Column_Name As String = dc.ColumnName.Substring(dc.ColumnName.IndexOf("Barcode_") + 8)
                    If (oDS.Tables(i).Columns.Contains(_text_Column_Name)) Then
                        For Each dr As DataRow In oDS.Tables(i).Rows
                            dr(_barcode_Column_Name) = Me.GenCodeToByte(dr(_text_Column_Name).ToString())
                        Next
                    End If
                End If
            Next

            ' BGN Fix Report
            Select Case Report_Name.ToUpper
                Case "TAGSTICKERPRINTOUT", "TAGSTICKERPRINTOUT_MINI", "TAGSTICKERCHEMICALPRINTOUT_MINI", "TAGSTICKERSPBRPRINTOUT_MINI"
                    If (Not (oDS.Tables(i).Columns.Contains("pic"))) Then
                        oDS.Tables(i).Columns.Add("pic", GetType(System.Byte()))
                    End If

                    If (Not (oDS.Tables(i).Columns.Contains("Package_Barcode"))) Then
                        oDS.Tables(i).Columns.Add("Package_Barcode", GetType(System.Byte()))
                    End If

                    If (Not (oDS.Tables(i).Columns.Contains("Tag_Barcode1"))) Then
                        oDS.Tables(i).Columns.Add("Tag_Barcode1", GetType(System.Byte()))
                    End If

                    If (Not (oDS.Tables(i).Columns.Contains("Tag_Barcode2"))) Then
                        oDS.Tables(i).Columns.Add("Tag_Barcode2", GetType(System.Byte()))
                    End If

                    For Each dr As DataRow In oDS.Tables(i).Rows
                        If (oDS.Tables(i).Columns.Contains("TAG_No")) Then

                            'Dim objBarcode As New WMS_STD_INB_Barcode.Barcode
                            'objBarcode.GenBarcode(dr("TAG_No").ToString())
                            'dr("pic") = WMS_STD_Master.W_Function.ConvertFileToByte(Application.StartupPath & "\" & dr("TAG_No").ToString() & ".bmp")
                            'Try
                            '    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\" & dr("TAG_No").ToString() & ".bmp")
                            'Catch ex As Exception

                            'End Try

                            dr("pic") = Me.BarcodeByte(dr("TAG_No").ToString(), True)

                        End If
                        If (oDS.Tables(i).Columns.Contains("Package_No")) Then dr("Package_Barcode") = Me.GenCodeToByte(dr("Package_No").ToString())
                        If (oDS.Tables(i).Columns.Contains("Tag_No1")) Then dr("Tag_Barcode1") = Me.GenCodeToByte(dr("Tag_No1").ToString())
                        If (oDS.Tables(i).Columns.Contains("Tag_No2")) Then dr("Tag_Barcode2") = Me.GenCodeToByte(dr("Tag_No2").ToString())
                    Next
                Case "WTH_PRINTOUT_V1"
                    If (Not (oDS.Tables(i).Columns.Contains("Withdraw_Barcode"))) Then
                        oDS.Tables(i).Columns.Add("Withdraw_Barcode", GetType(System.Byte()))
                    End If
                    For Each dr As DataRow In oDS.Tables(i).Rows
                        If (oDS.Tables(i).Columns.Contains("Withdraw_No")) Then dr("Withdraw_Barcode") = Me.GenCodeToByte(dr("Withdraw_No").ToString())
                    Next
                Case Else
                    ' BGN Other

                    ' END Other
            End Select
            ' END Fix Report

            oDS.Tables(i).AcceptChanges()
            oReportDocument.SetDataSource(oDS)
        Next
        Return oReportDocument
    End Function


End Class
