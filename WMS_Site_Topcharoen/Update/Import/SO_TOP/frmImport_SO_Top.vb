Imports System.IO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master

Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
'Imports Microsoft.Office.Interop

Public Class frmImport_SO_Top
    Public Document_Group_Name As String = "" 'KSL
    Private _boolResult As String
    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Private _dtHeader As New DataTable

    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            'objms_Customer.getPopup_Customer("Customer_Index", Me.txtCustomer_Id.Tag.ToString)
            objms_Customer.SearchData_Click("", " and Customer_Index='" & Me._Customer_Index & "'")
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                'Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString

            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                'Me.txtCustomer_Name.Text = ""

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    Sub SET_DEFAULT_CUSTOMER_BYUSER()
        Try
            Dim tCustomer_Index As String
            Dim objconfig As New config_UserByCustomer(config_UserByCustomer.enuOperation_Type.SEARCH)
            objconfig.GetCustomerDefault(WV_User_Index)
            tCustomer_Index = objconfig.ScalarOutput
            If tCustomer_Index <> "" Then
                Me._Customer_Index = tCustomer_Index
                Me.getCustomer()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub AddcbProductType()
    '    Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Try
    '        objClassDB.SearchData_Click("TIMCO_ShowBarcode", " AND TIMCO_ShowBarcode > 0")
    '        objDT = objClassDB.DataTable

    '        cbProductGroup.DisplayMember = "ProductType_ID"
    '        cbProductGroup.ValueMember = "ProductType_Index"
    '        cbProductGroup.DataSource = objDT

    '        If objDT.Rows.Count > 0 Then
    '            cbProductGroup.SelectedIndex = 0
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objClassDB = Nothing
    '    End Try
    'End Sub

    Private Sub frmImport_DO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language

            ''Insert
            'oFunction.Insert(Me)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)
            '  Me.SET_DEFAULT_CUSTOMER_BYUSER()
            '===============================
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            Me.getDocumentType(10)


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Dim xSQL As String = ""
        Try

            xSQL = " SELECT  DocumentType_Index,  Description FROM     ms_DocumentType"
            xSQL &= " WHERE Process_Id= 10 and status_id not in (-1)"
            xSQL &= " AND Document_Group_Name = '" & Me.Document_Group_Name & "'"
            objDT = objClassDB.DBExeQuery(xSQL)

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

        'Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable

        'Try
        '    objClassDB.getDocumentType(Process_Id)
        '    objDT = objClassDB.DataTable

        '    With cboDocumentType
        '        .DisplayMember = "Description"
        '        .ValueMember = "DocumentType_Index"
        '        .DataSource = objDT
        '    End With

        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objClassDB = Nothing
        '    objDT = Nothing
        'End Try

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        If Me.grdPreviewData.RowCount = 0 Then
            W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
            Exit Sub
        End If
        '' ------ STEP 1: Declare Data Table Objects
        Dim objSaleOrder As New tb_SalesOrder
        Dim objSaleOrderItem As New tb_SalesOrderItem
        Dim objSaleOrderItemCollection As New List(Of tb_SalesOrderItem)

        Dim objDBIndex As New Sy_AutoNumber
        Dim objDBTempIndex As New Sy_Temp_AutoNumber
        Dim xSalesOrder_Index As New List(Of String)
        'Add on Query
        Dim xQuery As String = ""
        Dim xDt As New DataTable
        Dim xObjDB As New DBType_SQLServer
        Dim xDistributionCenter_Index As String = ""
        xQuery = String.Format("select * from se_User where User_Index = '{0}'", WV_User_Index)
        xDt = xObjDB.DBExeQuery(xQuery)
        If xDt.Rows.Count > 0 Then
            xDistributionCenter_Index = xDt.Rows(0)("DistributionCenter_Index").ToString
        End If

        Try
            If Me._dtHeader.Rows.Count = 0 Then
                W_MSG_Information("กรุณา Validate data")
                Exit Sub
            End If

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            Dim dtDetail As New DataTable
            dtDetail = Me.grdPreviewData.DataSource
            For Each drHeader As DataRow In _dtHeader.Rows
                Dim drDetail() As DataRow = dtDetail.Select(String.Format("[Order Number] = '{0}'", drHeader("SalesOrder_No").ToString.Trim))
                'Header
                Dim drHeaderData As DataRow = drDetail(0)
                objSaleOrder = New tb_SalesOrder
                objSaleOrder.SalesOrder_Index = objDBTempIndex.getSys_Value("SalesOrder_Index")
                objSaleOrder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
                objSaleOrder.SalesOrder_No = drHeaderData("Order Number").ToString.Trim
                objSaleOrder.SalesOrder_Date = CDate(drHeaderData("Created Date").ToString).ToString("yyyy/MM/dd")

                objSaleOrder.Customer_Index = Me._Customer_Index.ToString
                objSaleOrder.Carrier_Index = "0010000000000" 'Fix ไม่ระบุ


                'objSaleOrder.Customer_Shipping_Index = Me.txtConsignee_Id.Tag.ToString
                'objSaleOrder.Customer_Shipping_Location_Index = Me.txtShipping_Location_ID.Tag.ToString

                xQuery = String.Format("select * from ms_Customer_Shipping where Company_Name = '{0}' and status_id <> -1", drHeaderData("Channel").ToString.Trim)
                objSaleOrder.Customer_Shipping_Index = xObjDB.DBExeQuery_Scalar(xQuery)

                xQuery = String.Format("select * from ms_Customer_Shipping_Location where Shipping_Location_Name = '{0}'  and status_id <> -1", drHeaderData("Channel").ToString.Trim)
                objSaleOrder.Customer_Shipping_Location_Index = xObjDB.DBExeQuery_Scalar(xQuery)



                objSaleOrder.Remark = drHeaderData("Carrier").ToString
                objSaleOrder.Credit_Term = 0 'Fix
                objSaleOrder.Currency_Index = "0010000000039" 'Fix Thailand , BATH
                objSaleOrder.PaymentMethod_Index = "" 'Fix


                objSaleOrder.Amount = 0
                objSaleOrder.Discount_Percent = 0
                objSaleOrder.Discount_Amt = 0
                objSaleOrder.Deposit_Amt = 0
                objSaleOrder.Total_Amt = 0
                objSaleOrder.VAT_Percent = 7
                objSaleOrder.VAT = 0
                objSaleOrder.Net_Amt = 0

                objSaleOrder.Str1 = "" 'drHeaderData("SaleOrder").ToString.Trim 'Me.txtRef1.Text
                objSaleOrder.Str2 = "" 'drHeaderData("PurchaseOrder").ToString.Trim 'Me.txtRef2.Text
                objSaleOrder.Str8 = "" 'drHeaderData("WorkOrder ID").ToString.Trim 'Me.txtRef2.Text
                objSaleOrder.Str3 = "" 'Me.txtTax_No.Text

                xQuery = String.Format("select * from ms_Customer where Customer_Index = '{0}'", objSaleOrder.Customer_Index)
                xDt = xObjDB.DBExeQuery(xQuery)
                If xDt.Rows.Count > 0 Then
                    objSaleOrder.Str10 = xDt.Rows(0)("Address").ToString 'Me.txtCustomer_Address.Text
                    objSaleOrder.Str6 = xDt.Rows(0)("Tel").ToString 'Me.txtCustomer_Phone.Text
                    objSaleOrder.Str7 = xDt.Rows(0)("Fax").ToString 'Me.txtCustomer_Fax.Text
                End If

                xQuery = String.Format("select c.*,r.DistributionCenter_Index from ms_Customer_Shipping_Location c inner join ms_SubRoute r on r.SubRoute_Index = c.SubRoute_Index where c.Customer_Shipping_Location_Index = '{0}'", objSaleOrder.Customer_Shipping_Location_Index)
                xDt = xObjDB.DBExeQuery(xQuery)
                If xDt.Rows.Count > 0 Then
                    objSaleOrder.Str9 = xDt.Rows(0)("Address").ToString 'Me.txtShip_Address.Text.ToString.Trim
                    objSaleOrder.Str4 = xDt.Rows(0)("Tel").ToString 'Me.txtShip_Phone.Text.ToString.Trim
                    objSaleOrder.Str5 = xDt.Rows(0)("Fax").ToString 'Me.txtShip_Fax.Text.ToString.Trim

                    objSaleOrder.SubRoute_Index = xDt.Rows(0)("SubRoute_Index").ToString
                    objSaleOrder.Route_Index = xDt.Rows(0)("Route_Index").ToString
                    objSaleOrder.TransportRegion_Index = xDt.Rows(0)("TransportRegion_Index").ToString
                    If xDt.Rows(0)("DistributionCenter_Index").ToString <> "" Then
                        xDistributionCenter_Index = xDt.Rows(0)("DistributionCenter_Index").ToString
                    End If
                End If

                objSaleOrder.DistributionCenter_Index = xDistributionCenter_Index

                objSaleOrder.Time_ExpectedDocPickup = Now.ToString("yyyy/MM/dd") ' dtpTime_DocPickup.Value
                objSaleOrder.chkTime_DocPickup = False
                objSaleOrder.Time_DocPickup = Now.ToString("yyyy/MM/dd") 'Fix dtpTime_DocPickup.Value
                objSaleOrder.chkExpected_Delivery_Date = True
                objSaleOrder.Expected_Delivery_Date = CDate(drHeaderData("Created Date").ToString).ToString("yyyy/MM/dd")

                objSaleOrder.Process_Id = 10
                objSaleOrder.add_by = WV_UserName
                objSaleOrder.Status = 1
                objSaleOrder.Reserve_index = ""
                objSaleOrder.District_Index = ""
                objSaleOrder.Province_Index = ""
                objSaleOrder.VehicleType_Index = ""
                objSaleOrder.PODRemark1 = ""
                objSaleOrder.PODDoc_Copy1 = ""
                objSaleOrder.PODDoc_Copy2 = ""
                objSaleOrder.PODDoc_Copy3 = ""
                objSaleOrder.PODDoc_Copy4 = ""
                objSaleOrder.PODDoc_Copy5 = ""
                objSaleOrder.GRRemark1 = ""
                objSaleOrder.GRDoc_Copy1 = ""
                objSaleOrder.GRDoc_Copy2 = ""
                objSaleOrder.GRDoc_Copy3 = ""
                objSaleOrder.GRDoc_Copy4 = ""
                objSaleOrder.GRDoc_Copy5 = ""

                objSaleOrderItemCollection = New List(Of tb_SalesOrderItem)
                Dim iItem As Integer = 0
                For Each drRow As DataRow In drDetail
                    iItem = iItem + 1
                    objSaleOrderItem = New tb_SalesOrderItem
                    objSaleOrderItem.SalesOrder_Index = objSaleOrder.SalesOrder_Index
                    objSaleOrderItem.SalesOrderItem_Index = objDBIndex.getSys_Value("SalesOrderItem_Index")
                    objSaleOrderItem.Item_Seq = iItem
                    'SKU
                    objSaleOrderItem.Qty = CDbl(drRow("Product Quantity").ToString.Trim)

                    'xQuery = "select S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
                    'xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
                    'xQuery &= " from ms_SKURatio R"
                    'xQuery &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
                    'xQuery &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
                    'xQuery &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
                    'xQuery &= " Where S.Sku_Id = '" & drRow("RM_Code").ToString.Trim & "'"
                    'xQuery &= " and P.Package_Id = '" & drRow("RM_Unit1").ToString.Trim & "'"
                    'xQuery &= " Order by S.Sku_Id,R.Ratio"
                    'xDt = xObjDB.DBExeQuery(xQuery)


                    xQuery = "select S.Sku_Index, S.Sku_Id,T.Description as DimensionType,T.Ratio as Dimension"
                    xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
                    xQuery &= " from ms_SKU S"
                    xQuery &= " 	inner join ms_Package P ON P.Package_Index = S.Package_Index"
                    xQuery &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
                    xQuery &= " Where S.Sku_Id = '" & drRow("Product SKU").ToString.Trim & "'"
                    xDt = xObjDB.DBExeQuery(xQuery)

                    If xDt.Rows.Count > 0 Then
                        objSaleOrderItem.Sku_Index = xDt.Rows(0)("Sku_Index").ToString
                        objSaleOrderItem.Package_Index = xDt.Rows(0)("Package_Index").ToString
                        objSaleOrderItem.Ratio = 1 'xDt.Rows(0)("Ratio")
                        objSaleOrderItem.Total_Qty = objSaleOrderItem.Qty * objSaleOrderItem.Ratio
                        objSaleOrderItem.Weight = objSaleOrderItem.Qty * xDt.Rows(0)("Weight")
                        objSaleOrderItem.Volume = objSaleOrderItem.Qty * xDt.Rows(0)("DimensionM3")
                    End If
                    objSaleOrderItem.UnitPrice = CDbl(drRow("Product Unit Price").ToString.Trim)
                    objSaleOrderItem.Discount_Amt = CDbl(drRow("Product Discount").ToString.Trim)
                    objSaleOrderItem.Amount = (objSaleOrderItem.Qty * objSaleOrderItem.UnitPrice) - objSaleOrderItem.Discount_Amt
                    objSaleOrderItem.Remark = drRow("Note").ToString
                    objSaleOrderItem.Itemstatus_index = "0010000000001" 'Fix ,GOOD สินค้าดี/พร้อมขาย
                    objSaleOrderItem.Plot = ""
                    objSaleOrderItem.ERP_location = ""
                    objSaleOrderItemCollection.Add(objSaleOrderItem)
                Next

                Dim objDBPOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.ADDNEW, objSaleOrder, objSaleOrderItemCollection)
                Try
                    Dim tSalesOrder_Index As String = objDBPOTransaction.SaveData

                    'คำนวณส่วนหัวเงินใหม่
                    Dim objDB As New DBType_SQLServer
                    Dim xsql As String = ""
                    'Detail
                    xsql = String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' ", tSalesOrder_Index)
                    Dim dtDDOld As New DataTable
                    dtDDOld = objDB.DBExeQuery(xsql)
                    'Header
                    xsql = String.Format(" select * from tb_SalesOrder where SalesOrder_Index = '{0}' ", tSalesOrder_Index)
                    Dim dtHDOld As New DataTable
                    dtHDOld = objDB.DBExeQuery(xsql)

                    If dtDDOld.Rows.Count > 0 Then
                        Dim xTotal_Amt As Double = dtDDOld.Compute("SUM(Amount)", "")
                        Dim xDiscount_Amt As Double = (xTotal_Amt * dtHDOld.Rows(0)("Discount_Percent")) / 100
                        If dtHDOld.Rows(0)("Discount_Amt") = 0 Then
                            xDiscount_Amt = 0
                        End If
                        If Double.IsNaN(xDiscount_Amt) Then xDiscount_Amt = 0
                        Dim xAmount As Double = xTotal_Amt - xDiscount_Amt
                        Dim xVAT As Double = xAmount * (7 / 100)
                        Dim xNet_Amt As Double = xAmount + xVAT

                        xsql = " update tb_SalesOrder "
                        xsql &= String.Format(" set Total_Amt = '{0}' ", Format(xTotal_Amt, "0.00"))
                        xsql &= String.Format("     , Discount_Amt = '{0}' ", Format(xDiscount_Amt, "0.00"))
                        xsql &= String.Format("     , Amount = '{0}' ", Format(xAmount, "0.00"))
                        xsql &= String.Format("     , VAT = '{0}' ", Format(xVAT, "0.00"))
                        xsql &= String.Format("     , Net_Amt = '{0}' ", Format(xNet_Amt, "0.00"))
                        xsql &= String.Format(" where SalesOrder_Index = '{0}' ", tSalesOrder_Index)
                        objDB.DBExeNonQuery(xsql)

                    End If


                Catch exx As Exception
                    For Each iSalesOrder_Index As String In xSalesOrder_Index
                        xQuery &= String.Format("update tb_SalesOrder set Status = -1 where SalesOrder_Index = '{0}'", iSalesOrder_Index)
                        xObjDB.DBExeNonQuery(xQuery)
                    Next
                    Throw exx
                End Try

                objDBPOTransaction = Nothing
                objSaleOrder = Nothing
                objSaleOrderItemCollection = Nothing
            Next

            W_MSG_Information_ByIndex("1")
            Me.grdPreviewData.DataSource = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally
        End Try

    End Sub

    Private Function SearchInv(ByVal tmpCustomer_Index As String, ByVal tmpSkuID As String, ByVal tmpSkuQty As Double) As String

        Dim objClassDB As New bl_Import_SO
        Dim objDT As DataTable = New DataTable
        Dim intRatio As Integer = 0

        Try
            SearchInv = "-"

            objClassDB.SearchSkuInv(tmpCustomer_Index, tmpSkuID)
            objDT = objClassDB.GetDataTable

            If objDT.Rows.Count > 0 Then
                If Val(tmpSkuQty) > Val(objDT.Rows(0).Item("Qty_Free").ToString) Then
                    SearchInv = "จำนวนสินค้าที่เบิกได้ (Available) : " & Val(objDT.Rows(0).Item("Qty_Free").ToString)
                Else
                    SearchInv = "OK"
                End If
                Exit Function
            Else
                SearchInv = "จำนวนสินค้าคงเหลือ (Inventory) : 0 "
                Exit Function
            End If

            SearchInv = "-"

        Catch ex As Exception
            Throw ex
        Finally

            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    '#Region "   ReadTxT Import SO   "
    '    Public Function ReadAndSplit(ByVal myReader As String, ByVal strDelimited As String)
    '        Try

    '            Dim CurrLine As String = myReader
    '            Dim strSplit() As String
    '            Dim strSaleOrderNo As String = ""

    '            strSplit = CurrLine.Split(strDelimited)

    '            Return strSplit
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ReadText_DO()
    '        Try
    '            Dim DetailFileList() As String = Directory.GetFiles(Me._DEFAULT_IMPORT_SO_PATH, "*.txt")
    '            For i As Integer = 0 To DetailFileList.Length - 1
    '                ReadDetail(DetailFileList(i))
    '            Next

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Sub ReadDetail(ByVal pstrFilePath As String)
    '        Dim strSalesOrder_No As String = ""
    '        Dim strDelimited As String = vbTab
    '        Dim obl_Import_SO_Item As New bl_Import_SO_Item

    '        Try
    '            Dim myReader As StreamReader
    '            Dim Current_Text_Line As String = ""
    '            Dim strCustomerShipping_Index As String = ""
    '            Dim strInvoice_No As String = ""
    '            Dim strSplit() As String
    '            Dim strTempText As New ArrayList
    '            Dim strSku_Index As String = ""
    '            Dim strArray(2) As String

    '            Dim iTotal_Header As Integer = 0
    '            Dim iTotal_Detail As Integer = 0

    '            Dim iCount_Comp_Header As Integer = 0
    '            Dim iCount_Comp_Detail As Integer = 0

    '            Dim iCount_Error_Header As Integer = 0
    '            Dim iCount_Error_Detail As Integer = 0
    '            ' Dim osb As New StringBuilder

    '            Dim iCurrent_Line_Number As Integer = 0

    '            Dim obl_Log As New bl_Log

    '            myReader = New StreamReader(pstrFilePath, System.Text.UnicodeEncoding.Default)


    '            _osbNewSKU = New StringBuilder
    '            _osbErr_Dup = New StringBuilder
    '            _osbErr_Data_Incomplete = New StringBuilder


    '            _osbErr_Dup.AppendLine("Error : Duplicate data")
    '            _osbErr_Dup.AppendLine("")

    '            _osbErr_Data_Incomplete.AppendLine("")
    '            _osbErr_Data_Incomplete.AppendLine("Error : Data Incomplete")
    '            _osbErr_Data_Incomplete.AppendLine("")

    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("[4] New SKU")
    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("")

    '            Dim strSplitDate As String = ""

    '            While myReader.Peek <> -1
    '                iTotal_Detail += 1
    '                iCurrent_Line_Number += 1
    '                Current_Text_Line = myReader.ReadLine
    '                strSplit = ReadAndSplit(Current_Text_Line, strDelimited)

    '                If strSplit(0).ToString = "ZZZ" Then
    '                    Continue While
    '                End If

    '                If strSalesOrder_No <> strSplit(0).ToString Then
    '                    '*********** BEGIN HEADER ********************
    '                    Dim obl_Import_HDO As New bl_Import_SO
    '                    If obl_Import_HDO.CheckSalesOrder_No(strSplit(0)) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        iCount_Error_Header += 1

    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                        End If

    '                        Continue While
    '                    Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strSaleOrderNo
    '                        strSalesOrder_No = strSplit(0).Trim
    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                            Continue While
    '                        End If
    '                    End If
    '                    obl_Import_HDO.InvoiceNo = strSplit(0).Trim
    '                    obl_Import_HDO.Str2 = strSplit(3).Trim
    '                    obl_Import_HDO.DoNo = strSplit(0).Trim
    '                    strSplitDate = strSplit(4).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.DoDate = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))

    '                    strSplitDate = strSplit(13).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.Expected_Delivery_Date = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
    '                    strCustomerShipping_Index = obl_Import_HDO.GetCustomerShipping_Index(strSplit(5).Trim, strSplit(6).Trim, "", _
    '                                                                   strSplit(7).Trim, strSplit(8).Trim)
    '                    obl_Import_HDO.Str8 = strSplit(9).Trim
    '                    obl_Import_HDO.CustomerShippingNo = strSplit(5).Trim
    '                    obl_Import_HDO.CustomerShippingIndex = strCustomerShipping_Index
    '                    obl_Import_HDO.PostCode = strSplit(8).Trim
    '                    obl_Import_HDO.Remark = strSplit(14).Trim
    '                    obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
    '                    If strSplit(2).Trim = "1" Then
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_NORMAL
    '                    Else
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_RETURN
    '                    End If
    '                    obl_Import_HDO.SalesOrderInsert()
    '                    iCount_Comp_Header += 1
    '                    '*********** END HEADER ********************
    '                End If

    '                '*********** BEGIN ITEM ********************

    '                obl_Import_SO_Item.RefNo1 = strSalesOrder_No
    '                strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSalesOrder_No)
    '                obl_Import_SO_Item.SaleOrderIndex = strArray(0)
    '                obl_Import_SO_Item.Currency = strArray(1)

    '                'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
    '                If obl_Import_SO_Item.SaleOrderIndex = "" Then
    '                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    iCount_Error_Detail += 1

    '                    'อ่านบรรทัดใหม่
    '                    Continue While

    '                Else

    '                    'Check Duplicate data in Sales Order Item
    '                    If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

    '                        iCount_Error_Detail += 1

    '                        'อ่านบรรทัดใหม่
    '                        Continue While
    '                    End If

    '                    Dim objSy_AutoNumber As New Sy_AutoNumber
    '                    obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
    '                    objSy_AutoNumber = Nothing
    '                    obl_Import_SO_Item.ItemSeq = CInt(strSplit(1).Trim)
    '                    obl_Import_SO_Item.Qty = CDbl(strSplit(11).Trim)
    '                    obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
    '                    obl_Import_SO_Item.Volume = 0 '
    '                    obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
    '                    obl_Import_SO_Item.Remark = strSplit(14).Trim

    '                    '*** BEGIN SKU ***
    '                    obl_Import_SO_Item.SkuId = strSplit(10).Trim
    '                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId)
    '                    'Insert New Sku and Package
    '                    If obl_Import_SO_Item.SkuIndex = "" Then
    '                        obl_Import_SO_Item.Package_ID = strSplit(12).Trim
    '                        obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
    '                        obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
    '                        _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
    '                    End If

    '                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, strSplit(12).Trim)  'strArray(1)

    '                    If obl_Import_SO_Item.PackageIndex = "" Then
    '                        ' Auto Insert Package were Package and Update Package For Sku This
    '                        obl_Import_SO_Item.PackageIndex = SavePackage(strSplit(12).Trim)
    '                        Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
    '                    End If
    '                    'Get Package_Index with Package_Id



    '                    '*** END SKU ***

    '                    'Set property and Insert
    '                    obl_Import_SO_Item.SalesOrder_ItemInsert()

    '                    iCount_Comp_Detail += 1
    '                End If

    '                '*********** END ITEM ********************
    '            End While

    '            myReader.Close()

    '            strDestination = MoveFile(pstrFilePath)

    '            'Write Log
    '            With obl_Log
    '                'Move file
    '                .Write_To_Path = strDestination & "\" & pstrFilePath.ToUpper.Replace(Me._DEFAULT_IMPORT_SO_PATH.ToUpper, "") & ".log"
    '                .Process_Name = "Import"
    '                .Module_Name = "DO"
    '                .Start_Process = Now.ToString("dd/MM/yyyy HH:mm:ss")

    '                .Target = pstrFilePath
    '                .Destination = strDestination

    '                .Total_Header = iTotal_Header
    '                .Total_Detail = iTotal_Detail

    '                .Complete_Header = iCount_Comp_Header
    '                .Complete_Detail = iCount_Comp_Detail

    '                .Incomplete_Header = iCount_Error_Header
    '                .Incomplete_Detail = iCount_Error_Detail

    '                .Error_List = New StringBuilder

    '                .Error_List.AppendLine(_osbErr_Dup.ToString)
    '                .Error_List.AppendLine(_osbErr_Data_Incomplete.ToString)
    '                .Error_List.AppendLine(_osbNewSKU.ToString)

    '                .Write_Log()
    '            End With

    '        Catch ex As Exception
    '            obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(strSalesOrder_No)
    '            Throw ex
    '        End Try

    '    End Sub

    '    Sub SaveSKURatio(ByVal pSku_Index As String, ByVal pPackage_Index As String, ByVal ratio As Double)
    '        Try

    '            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
    '            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, CDbl(ratio))

    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End Sub

    '    Function SavePackage(ByVal ppackage_Id As String) As String

    '        Try
    '            Dim Package_Index As String = ""
    '            Dim package_Id As String = ppackage_Id
    '            Dim description As String = ppackage_Id
    '            Dim dimension_Hi As Double = 0.0
    '            Dim dimension_Wd As Double = 0.0
    '            Dim dimension_Len As Double = 0.0
    '            Dim Weight As Double = 0.0

    '            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
    '            Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, 0, Weight, 0) ', txtUnit_id.Text

    '            Return Package_Index

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Function MoveFile(ByVal pstrSourceFile As String) As String
    '        Try

    '            Dim ImportFolder As String = Me._DEFAULT_IMPORT_SO_PATH


    '            Dim strResultFileName As String
    '            Dim filemove As String

    '            If Directory.Exists(ImportFolder & SubFolder) = False Then
    '                Directory.CreateDirectory(ImportFolder & SubFolder)
    '            End If

    '            If pstrSourceFile = "" Then
    '                Return ""
    '                'Continue For
    '            End If


    '            'For i As Integer = 0 To pstrSourceFile.Length - 1
    '            strResultFileName = Path.GetFileName(pstrSourceFile)

    '            filemove = ImportFolder & SubFolder & "\" & strResultFileName
    '            File.Move(pstrSourceFile, filemove)
    '            'Next

    '            Return ImportFolder & SubFolder

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ShowError(ByVal pstrErrorList As ArrayList, ByVal pintCountSucc As Integer)

    '        Try

    '            Dim strResult As String
    '            Dim strError As String = ""
    '            For Each strResult In pstrErrorList
    '                strError &= strResult & vbNewLine
    '            Next
    '            If strError = "" Then
    '                strError = "Not found."
    '            End If

    '            MsgBox("ระบบทำการ Import ข้อมูลสำเร็จ " & vbNewLine _
    '            & "Total : " & pintCountSucc & " record." & vbNewLine & vbNewLine _
    '            & "----------------------------------------------Error list------------------------------------------" & vbNewLine _
    '            & strError & vbNewLine _
    '            & "Total : " & pstrErrorList.Count & " record.")

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Function ConvertToDate(ByVal pstrValue As String) As Date

    '        Try
    '            'strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4)
    '            pstrValue = pstrValue & "0"
    '            Dim strDate As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 1, 1), Mid(pstrValue, 1, 2))
    '            Dim strMonth As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 2, 2), Mid(pstrValue, 3, 2))
    '            Dim strYear As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 4, 4), Mid(pstrValue, 5, 4))

    '            Return CDate(strYear & "/" & strMonth & "/" & strDate)

    '        Catch ex As Exception

    '            Throw ex

    '        End Try

    '    End Function
    '#End Region

    'Old Code
    'Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
    '    Try
    '        'checking 
    '        If Me.txtCustomer_Id.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Customer")
    '            Exit Sub
    '        End If

    '        If Me.txtWorkSheet.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Work Sheet")
    '            Exit Sub
    '        End If
    '        Dim oImport_SO As New bl_Import_SO
    '        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
    '            Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

    '            'load data from excel

    '            Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
    '        Else
    '            Exit Sub
    '        End If

    '        With oImport_SO
    '            .DataSource = Me.grdPreviewData.DataSource
    '            '.Customer_Index = Me.cboCustomer.SelectedValue
    '            '.DocumentType_Index = Me.cboDocumentType.SelectedValue
    '            '.ItemStatus_Index = Me.cboItemStatus.SelectedValue


    '            'If .CheckingData Then

    '            '    _boolResult = True
    '            'Else
    '            '    _boolResult = False
    '            'End If
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try

    'End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
        Try
            Dim oImport_SO As New bl_Import_SO
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                Dim objWS As DataTable = New DataTable

                objWS = oImport_SO.LoadWorkSheet(Me.txtFilePath.Text)

                With cboWorkSheet
                    .DataSource = objWS
                    .DisplayMember = "TABLE_NAME"
                    .ValueMember = "TABLE_NAME"
                End With
                cboWorkSheet.SelectedIndex = 0
                LoadData()
                '====================
            Else
                Exit Sub
            End If

            Me.btnImport.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub frmImport_SO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value **** มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index
            If tmpCustomer_Index = "" Then
                Exit Sub
            End If
            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                'Me.txtCustomer_Name.Text = ""
            End If
            ' *********************
            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged
        Try
            Me._DocumentType_Index = cboDocumentType.SelectedValue.ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub cbProductGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me._EPSON_Location_Index = cbProductGroup.SelectedValue.ToString
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub LoadData()
        Try
            'checking
            If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
                Exit Sub
            End If

            Dim oImport_SO As New bl_Import_SO

            '===============
            Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            '===============

            oImport_SO.DataSource = grdPreviewData.DataSource

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub BntLoaddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntLoaddata.Click
        Try
            LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub txtFilePath_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePath.DoubleClick
        Try
            Dim AppPath As String
            AppPath = Application.StartupPath
            Dim ps As New ProcessStartInfo()
            With ps
                'Dim strApppath As String = ""
                'strApppath = AppPath.Replace("\bin", "")
                .FileName = AppPath + "\Import\Format\SO_IMPORT_TIMCO.xls"
                'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\SO_IMPORT_TIMCO.xls
                .UseShellExecute = True
            End With

            Dim p As New Process
            p.StartInfo = ps
            p.Start()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If

            End If

            Dim frm As New frmConsignee_Popup
            frm.Customer_Index = "" 'Me._Customer_Index
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frm.Consignee_Index

            If Not tmp_Index = "" Then
                Me.txtConsignee_Id.Tag = tmp_Index
                txtConsignee_Id.Text = frm.Consignee_ID
                txtConsignee_Name.Text = frm.Consignee_Name


                Dim objCust_Ship_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
                Dim odtCus_ShipLocation As DataTable
                objCust_Ship_Location.getCus_Ship_Locartion_SearchPopUp(" WHERE  Customer_Shipping_Location_Id Like '" & Me.txtConsignee_Id.Text & "' ")
                odtCus_ShipLocation = objCust_Ship_Location.GetDataTable

                If odtCus_ShipLocation.Rows.Count > 0 Then
                    Me.txtShipping_Location_ID.Tag = odtCus_ShipLocation.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                    Me.Get_CUSTOMERSHIPPINGLOCATION()
                End If




            Else
                Me.txtConsignee_Id.Tag = ""
                txtConsignee_Id.Text = ""
                txtConsignee_Name.Text = ""

            End If

            frm.Close()



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            'checking 
            If Me.txtCustomer_Id.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ  " & Me.lblCustomer.Text)
                Exit Sub
            End If
            ''checking 
            'If Me.txtConsignee_Id.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาระบุ " & Me.lblConsignee.Text)
            '    Exit Sub
            'End If

            ''checking 
            'If Me.txtShipping_Location_ID.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาระบุ  " & Me.lblReservedLocation.Text)
            '    Exit Sub
            'End If

            Me.btnImport.Enabled = False
            If Me.Check_Data() = True Then
                Me.btnImport.Enabled = True
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Function Check_Data() As Boolean
        Try
            Dim xQuery As String = ""
            Dim xDt As New DataTable
            Dim xObjDB As New DBType_SQLServer

            Dim i As Integer
            Dim oImport_SO As New bl_SO_TOP
            Check_Data = True

            _dtHeader = New DataTable
            _dtHeader.Columns.Add("SalesOrder_No", GetType(String))

            For i = 0 To grdPreviewData.Rows.Count - 1
                With grdPreviewData
                    'Set Red
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.Red
                    ''-------------------------------------------------------------------------------
                    'Document
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("Order Number").Value) Then
                        Dim dtSO As New DataTable
                        dtSO = oImport_SO.GetSaleOrder_No(.Rows(i).Cells("Order Number").Value.ToString)
                        If dtSO.Rows.Count > 0 Then
                            .Rows(i).Cells("Check_Result").Value = "พบ Order Number. ซ้ำที่ (" & dtSO.Rows(0)("Str1").ToString & ")"
                            .Rows(i).Cells("Order Number").Style.BackColor = Color.Red
                            Check_Data = False
                            Continue For
                        Else
                            'Add Primary key
                            Dim drNewRow As DataRow
                            drNewRow = _dtHeader.NewRow
                            Dim xSalesOrder_No As String = .Rows(i).Cells("Order Number").Value.ToString.Trim
                            drNewRow("SalesOrder_No") = xSalesOrder_No
                            Dim drCheck() As DataRow = _dtHeader.Select(String.Format("SalesOrder_No='{0}'", xSalesOrder_No))
                            If drCheck.Length = 0 Then
                                _dtHeader.Rows.Add(drNewRow)
                            End If
                            .Rows(i).Cells("Check_Result").Value = "OK"
                            .Rows(i).Cells("Order Number").Style.BackColor = Color.YellowGreen
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "Order Number. ผิดพลาด"
                        .Rows(i).Cells("Order Number").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    End If

                    If IsDate(.Rows(i).Cells("Created Date").Value) Then
                        Dim strDate As String = CDate(.Rows(i).Cells("Created Date").Value).Year
                        If CInt(strDate) > 2500 Then
                            .Rows(i).Cells("Check_Result").Value = "Created Date. ผิดพลาดต้องเป็น คศ."
                            .Rows(i).Cells("Created Date").Style.BackColor = Color.Red
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("Created Date").Style.BackColor = Color.YellowGreen
                    Else
                        .Rows(i).Cells("Check_Result").Value = "Created Date. ผิดพลาด"
                        .Rows(i).Cells("Created Date").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    End If


                    'Detail
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("Channel").Value) Then
                        xQuery = String.Format("select * from ms_Customer_Shipping where Company_Name = '{0}'  and status_id <> -1", .Rows(i).Cells("Channel").Value.ToString)
                        If xObjDB.DBExeQuery(xQuery).Rows.Count = 0 Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ Ship to นี้ในระบบ "
                            .Rows(i).Cells("Channel").Style.BackColor = Color.Red
                            Check_Data = False
                            Continue For
                        Else
                            .Rows(i).Cells("Check_Result").Value = "OK"
                            .Rows(i).Cells("Channel").Style.BackColor = Color.YellowGreen
                        End If


                        xQuery = String.Format("select * from ms_Customer_Shipping_Location where Shipping_Location_Name = '{0}'  and status_id <> -1", .Rows(i).Cells("Channel").Value.ToString)
                        If xObjDB.DBExeQuery(xQuery).Rows.Count = 0 Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ Sold to/Bill to นี้ในระบบ "
                            .Rows(i).Cells("Channel").Style.BackColor = Color.Red
                            Check_Data = False
                            Continue For
                        Else
                            .Rows(i).Cells("Check_Result").Value = "OK"
                            .Rows(i).Cells("Channel").Style.BackColor = Color.YellowGreen
                        End If

                    Else
                        .Rows(i).Cells("Check_Result").Value = "Channel. ผิดพลาด"
                        .Rows(i).Cells("Channel").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    End If



                    '-------------------------------------------------------------------------------
                    'Detail
                    Dim xRM_SKU_Index As String = ""
                    Dim dtSKU As New DataTable
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("Product SKU").Value) Then
                        xRM_SKU_Index = oImport_SO.GetSKU_ById(.Rows(i).Cells("Product SKU").Value.ToString.Trim, "")
                        dtSKU = oImport_SO.GetDataTable
                        If xRM_SKU_Index = "" Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ Product SKU "
                            .Rows(i).Cells("Product SKU").Style.BackColor = Color.Red
                            Check_Data = False
                            Continue For
                        Else
                            .Rows(i).Cells("Check_Result").Value = "OK"
                            .Rows(i).Cells("Product SKU").Style.BackColor = Color.YellowGreen
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "Product SKU. ผิดพลาด"
                        .Rows(i).Cells("Product SKU").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    End If

                    'Qty
                    If Not IsNumeric(.Rows(i).Cells("Product Quantity").Value) Then
                        .Rows(i).Cells("Check_Result").Value = "Product Quantity. ผิดพลาด"
                        .Rows(i).Cells("Product Quantity").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("Product Quantity").Style.BackColor = Color.YellowGreen
                    End If


                    If Not IsNumeric(.Rows(i).Cells("Product Unit Price").Value) Then
                        .Rows(i).Cells("Check_Result").Value = "Product Unit Price. ผิดพลาด"
                        .Rows(i).Cells("Product Unit Price").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("Product Unit Price").Style.BackColor = Color.YellowGreen
                    End If

                    If Not IsNumeric(.Rows(i).Cells("Product Discount").Value) Then
                        .Rows(i).Cells("Check_Result").Value = "Product Discount. ผิดพลาด"
                        .Rows(i).Cells("Product Discount").Style.BackColor = Color.Red
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("Product Discount").Style.BackColor = Color.YellowGreen
                    End If


                    If Not .Columns.Contains("Note") Then
                        .Rows(i).Cells("Check_Result").Value = "Note. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("Note").Style.BackColor = Color.YellowGreen
                    End If

                    If Not .Columns.Contains("No") Then
                        .Rows(i).Cells("Check_Result").Value = "No. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                        .Rows(i).Cells("No").Style.BackColor = Color.YellowGreen
                    End If

                    '-------------------------------------------------------------------------------
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.YellowGreen
                    '-------------------------------------------------------------------------------
                End With
            Next



        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub btnCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Shipping_Location.Click
        Try
            Dim strWhere As String = ""
            Dim oConfig As New config_CustomSetting
            If Me.txtConsignee_Id.Text.Trim.Length > 0 Then
                strWhere = " AND Customer_Shipping_Index  = '" & Me.txtConsignee_Id.Tag & "' "
            End If

            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = strWhere
            frm.ShowDialog()
            Dim tmpCustomer_Shipping_Location_Index As String = ""
            tmpCustomer_Shipping_Location_Index = frm.Customer_Shipping_Location_Index
            If tmpCustomer_Shipping_Location_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Shipping_Location_Index = "" Then
                Me.txtShipping_Location_ID.Tag = tmpCustomer_Shipping_Location_Index
                Me.Get_CUSTOMERSHIPPINGLOCATION()
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
                Me.txtShipping_Location_Name.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Get_CUSTOMERSHIPPINGLOCATION()
        Try
            Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
            Dim objDTms_Shipping_Location As DataTable = New DataTable
            Dim _Postcode As String = ""
            Try
                objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", Me.txtShipping_Location_ID.Tag.ToString)
                objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
                If objDTms_Shipping_Location.Rows.Count > 0 Then

                    Me.txtShipping_Location_ID.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                    Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Id").ToString
                    Me.txtShipping_Location_Name.Text = objDTms_Shipping_Location.Rows(0).Item("Company_Name").ToString

                Else
                    Me.txtShipping_Location_ID.Tag = ""
                    Me.txtShipping_Location_ID.Text = ""
                    Me.txtShipping_Location_Name.Text = ""
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objms_Shipping_Location = Nothing
                objDTms_Shipping_Location = Nothing
            End Try
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboWorkSheet_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWorkSheet.SelectionChangeCommitted
        Try
            Me.grdPreviewData.DataSource = Nothing
            Me.LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class