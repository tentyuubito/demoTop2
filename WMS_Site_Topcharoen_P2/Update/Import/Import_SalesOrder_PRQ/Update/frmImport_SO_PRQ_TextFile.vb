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

Public Class frmImport_SO_PRQ_TextFile
    Dim CheckPRQ As Integer = 0
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

    Private _DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL As String = ""
    Private _DEFAULT_DOCUMENT_TYPE_SALEORDER_PACKAGING As String = ""

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

    Private Sub frmImport_SO_PRQ_TextFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language

            ''Insert
            'oFunction.Insert(Me)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)
            Me.SET_DEFAULT_CUSTOMER_BYUSER()
            '===============================
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            Me.getDocumentType(10)


            'Defualt doc
            Dim objClassDB As New DBType_SQLServer
            Me._DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL = objClassDB.DBExeQuery_Scalar(" SELECT TOP 1 ISNULL(Config_Value,'') FROM  config_CustomSetting WHERE Config_Key = 'DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL'")
            Me._DEFAULT_DOCUMENT_TYPE_SALEORDER_PACKAGING = objClassDB.DBExeQuery_Scalar(" SELECT TOP 1 ISNULL(Config_Value,'') FROM  config_CustomSetting WHERE Config_Key = 'DEFAULT_DOCUMENT_TYPE_SALEORDER_PACKAGING'")



        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Dim xSQL As String = ""
        Try

            xSQL = " SELECT  * FROM     ms_DocumentType"
            xSQL &= " WHERE Process_Id = " & Process_Id
            xSQL &= " and status_id not in (-1)"
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
        If CheckPRQ > 0 AndAlso W_MSG_Confirm("พบ รหัส PRQ ซ้ำ ท่านต้องการดำเนินการต่อ ใช่หรือไม่") = Windows.Forms.DialogResult.No Then
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
                Dim drDetail() As DataRow = dtDetail.Select(String.Format("[PRQ ID] = '{0}'", drHeader("SalesOrder_No").ToString.Trim))
                'Header
                Dim drHeaderData As DataRow = drDetail(0)
                objSaleOrder = New tb_SalesOrder
                objSaleOrder.SalesOrder_Index = objDBTempIndex.getSys_Value("SalesOrder_Index")
                'objSaleOrder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
                objSaleOrder.DocumentType_Index = drHeaderData("DocumentType_Index").ToString.Trim
                objSaleOrder.SalesOrder_No = drHeaderData("PRQ ID").ToString.Trim
                objSaleOrder.SalesOrder_Date = CDate(drHeaderData("PRD Date").ToString).ToString("yyyy/MM/dd")

                objSaleOrder.Customer_Index = Me._Customer_Index.ToString
                objSaleOrder.Carrier_Index = "0010000000000" 'Fix ไม่ระบุ
                objSaleOrder.Customer_Shipping_Index = Me.txtConsignee_Id.Tag.ToString
                objSaleOrder.Customer_Shipping_Location_Index = Me.txtShipping_Location_ID.Tag.ToString

                objSaleOrder.Remark = ""
                objSaleOrder.Credit_Term = 0 'Fix
                objSaleOrder.Currency_Index = "0010000000039" 'Fix Thailand , BATH
                objSaleOrder.PaymentMethod_Index = "" 'Fix

                objSaleOrder.Amount = 0
                objSaleOrder.Discount_Percent = 0
                objSaleOrder.Discount_Amt = 0
                objSaleOrder.Deposit_Amt = 0
                objSaleOrder.Total_Amt = 0
                objSaleOrder.VAT_Percent = 0
                objSaleOrder.VAT = 0
                objSaleOrder.Net_Amt = 0

                objSaleOrder.Str1 = drHeaderData("SaleOrder").ToString.Trim 'Me.txtRef1.Text
                objSaleOrder.Str2 = drHeaderData("PurchaseOrder").ToString.Trim 'Me.txtRef2.Text
                objSaleOrder.Str8 = drHeaderData("WorkOrder ID").ToString.Trim 'Me.txtRef2.Text
                objSaleOrder.Str3 = "" 'Me.txtTax_No.Text

                xQuery = String.Format("select * from ms_Customer where Customer_Index = '{0}'", objSaleOrder.Customer_Index)
                xDt = xObjDB.DBExeQuery(xQuery)
                If xDt.Rows.Count > 0 Then
                    objSaleOrder.Str10 = xDt.Rows(0)("Address").ToString 'Me.txtCustomer_Address.Text
                    objSaleOrder.Str6 = xDt.Rows(0)("Tel").ToString 'Me.txtCustomer_Phone.Text
                    objSaleOrder.Str7 = xDt.Rows(0)("Fax").ToString 'Me.txtCustomer_Fax.Text
                End If

                xQuery = String.Format("select * from ms_Customer_Shipping_Location where Customer_Shipping_Location_Index = '{0}'", objSaleOrder.Customer_Shipping_Location_Index)
                xDt = xObjDB.DBExeQuery(xQuery)
                If xDt.Rows.Count > 0 Then
                    objSaleOrder.Str9 = xDt.Rows(0)("Address").ToString 'Me.txtShip_Address.Text.ToString.Trim
                    objSaleOrder.Str4 = xDt.Rows(0)("Tel").ToString 'Me.txtShip_Phone.Text.ToString.Trim
                    objSaleOrder.Str5 = xDt.Rows(0)("Fax").ToString 'Me.txtShip_Fax.Text.ToString.Trim

                    objSaleOrder.SubRoute_Index = xDt.Rows(0)("SubRoute_Index").ToString
                    objSaleOrder.Route_Index = xDt.Rows(0)("Route_Index").ToString
                    objSaleOrder.TransportRegion_Index = xDt.Rows(0)("TransportRegion_Index").ToString
                End If
                objSaleOrder.DistributionCenter_Index = xDistributionCenter_Index

                objSaleOrder.Time_ExpectedDocPickup = Now.ToString("yyyy/MM/dd") ' dtpTime_DocPickup.Value
                objSaleOrder.chkTime_DocPickup = False
                objSaleOrder.Time_DocPickup = Now.ToString("yyyy/MM/dd") 'Fix dtpTime_DocPickup.Value
                objSaleOrder.chkExpected_Delivery_Date = True
                objSaleOrder.Expected_Delivery_Date = CDate(drHeaderData("PRD Deadline").ToString).ToString("yyyy/MM/dd")

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
                    objSaleOrderItem.Qty = CDbl(drRow("QTY_RM").ToString.Trim)

                    xQuery = "select S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
                    xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
                    xQuery &= " from ms_SKURatio R"
                    xQuery &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
                    xQuery &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
                    xQuery &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
                    xQuery &= " Where S.STR4 = '" & drRow("RM_Code").ToString.Trim & "'"
                    xQuery &= " and P.Package_Id = '" & drRow("RM_Unit1").ToString.Trim & "'"
                    xQuery &= " Order by S.Sku_Id,R.Ratio"
                    xDt = xObjDB.DBExeQuery(xQuery)
                    If xDt.Rows.Count > 0 Then
                        objSaleOrderItem.Sku_Index = xDt.Rows(0)("Sku_Index").ToString
                        objSaleOrderItem.Package_Index = xDt.Rows(0)("Package_Index").ToString
                        objSaleOrderItem.Ratio = xDt.Rows(0)("Ratio")
                        objSaleOrderItem.Total_Qty = objSaleOrderItem.Qty * objSaleOrderItem.Ratio
                        objSaleOrderItem.Weight = objSaleOrderItem.Qty * xDt.Rows(0)("Weight")
                        objSaleOrderItem.Volume = objSaleOrderItem.Qty * xDt.Rows(0)("DimensionM3")
                    End If
                    objSaleOrderItem.UnitPrice = 0
                    objSaleOrderItem.Amount = 0
                    objSaleOrderItem.Discount_Amt = 0
                    objSaleOrderItem.Remark = ""
                    objSaleOrderItem.Itemstatus_index = "0010000000001" 'Fix ,GOOD สินค้าดี/พร้อมขาย
                    objSaleOrderItem.Plot = ""
                    objSaleOrderItem.ERP_location = ""
                    'objSaleOrderItem.WOI_ID = drRow("WOI ID")
                    objSaleOrderItemCollection.Add(objSaleOrderItem)

                Next

                Dim objDBPOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.ADDNEW, objSaleOrder, objSaleOrderItemCollection)
                Try
                    Dim tSalesOrder_Index As String = objDBPOTransaction.SaveData
                    xSalesOrder_Index.Add(tSalesOrder_Index)
                    Dim i As Integer = 0

                    For Each dr As DataRow In dtDetail.Select(String.Format("[PRQ ID] = '{0}'", drHeader("SalesOrder_No").ToString.Trim))

                        Dim strSQL As String = ""
                        strSQL = "update tb_SalesOrderItem set WOI_ID = '" & dr("WOI ID") & "' where SalesOrderItem_Index = '" & objSaleOrderItemCollection.Item(i).SalesOrderItem_Index & "'"
                        xObjDB.DBExeNonQuery(strSQL)
                        i += 1

                    Next


                    'KSL Add on production
                    xQuery = "select S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
                    xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
                    xQuery &= " from ms_SKURatio R"
                    xQuery &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
                    xQuery &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
                    xQuery &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
                    xQuery &= " Where S.str4 = '" & drHeaderData("FG_Code").ToString.Trim & "'"
                    xQuery &= " and P.Package_Id = '" & drHeaderData("FG_Unit").ToString.Trim & "'"
                    xQuery &= " Order by S.Sku_Id,R.Ratio"
                    xDt = xObjDB.DBExeQuery(xQuery)
                    If xDt.Rows.Count > 0 Then
                        xQuery = " INSERT INTO [dbo].[tb_SalesOrder_Production_KSL]"
                        xQuery &= " ([SalesOrder_Index],[Sku_Index],[Package_Index],[PRQ_ID],[PRQStatus]"
                        xQuery &= " ,[WorkOrder_ID],[WorkOrerStatus],[SaleOrder],[PurchaseOrder],[Document_Type]"
                        xQuery &= " ,[PRQ_REF_ID],[WOI_ID],[Production_Line],[QTY])"
                        xQuery &= " VALUES("
                        xQuery &= String.Format("'{0}'", tSalesOrder_Index)
                        xQuery &= String.Format(",'{0}'", xDt.Rows(0)("Sku_Index").ToString)
                        xQuery &= String.Format(",'{0}'", xDt.Rows(0)("Package_Index").ToString)
                        xQuery &= String.Format(",'{0}'", drHeaderData("PRQ ID").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("PRQStatus").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("WorkOrder ID").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("WorkOrerStatus").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("SaleOrder").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("PurchaseOrder").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("Document").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("PRQ REF ID").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("WOI ID").ToString.Trim)
                        xQuery &= String.Format(",'{0}'", drHeaderData("Production Line").ToString.Trim)
                        xQuery &= String.Format(",'{0}')", drHeaderData("QTY").ToString.Trim)
                        xObjDB.DBExeNonQuery(xQuery)
                    End If
                Catch exx As Exception
                    For Each iSalesOrder_Index As String In xSalesOrder_Index
                        xQuery = String.Format("update tb_SalesOrder set Status = -1 where SalesOrder_Index = '{0}'", iSalesOrder_Index)
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
    Private Function ConvertToDataTable(ByVal filePath As String) As DataTable

        Dim obj_Head As New bl_Import_SO
        Dim dt As New DataTable
        dt = obj_Head.SetHeader_TextFile("55")
        Dim tbl As New DataTable()

        Dim strText As String = System.IO.File.ReadAllText(filePath, UnicodeEncoding.GetEncoding(0))
        Dim lines() As String = strText.Replace(vbLf, "").Split(vbCr)
        tbl.Columns.Add(New DataColumn("Check_Result"))
        tbl.Columns.Add(New DataColumn("DocumentType"))
        For i As Integer = 0 To dt.Rows.Count - 1
            tbl.Columns.Add(New DataColumn(dt.Rows(i).Item(0).ToString()))
        Next
        tbl.Columns.Add(New DataColumn("DocumentType_Index")) 'เพิ่มเอง
        'tbl.Columns.Add(New DataColumn("SaleOrder")) 'เพิ่มเอง
        For Each line As String In lines
            Dim cols = line.Split("|")
            Dim dr As DataRow = tbl.NewRow()
            If line.Length <> 0 Then
                dr(0) = ""
                Dim j As Integer = 0
                For cIndex As Integer = 2 To dt.Rows.Count + 1
                    Try

                        If IsDate(cols(j)) Then
                            dr(cIndex) = convertToTH(cols(j))
                        Else
                            dr(cIndex) = cols(j)
                        End If
                        j += 1
                    Catch ex As Exception
                        W_MSG_Error(ex.Message)
                    End Try
                Next

                tbl.Rows.Add(dr)

            End If

        Next

        Return tbl

    End Function

    Private Function convertToTH(ByVal curCulture As String) As String
        If CInt(CDate(curCulture).Year.ToString) > 2500 Then
            Dim strDate() = curCulture.Split("/")
            Return strDate(0) & "/" & strDate(1) & "/" & CInt(strDate(2)) - 543
            Exit Function
        End If
        Return curCulture
    End Function

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
        Try

            Dim objWS As New DataTable
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

                strFileName = OpenFileDialog1.FileName
                Me.txtFilePath.Text = strFileName
                objWS = ConvertToDataTable(strFileName)
                Dim obj_ms As New bl_SO_PRQ_KSL
                For Each dr As DataRow In objWS.Rows
                    Dim strSKU_Name As New DataTable
                    strSKU_Name = obj_ms.GetSKU_Name(dr("FG_Code").ToString.Trim)
                    If strSKU_Name.Rows.Count > 0 Then
                        dr("FG_SKU_Name") = strSKU_Name.Rows(0).Item("Str1")
                    End If
                    strSKU_Name = obj_ms.GetSKU_Name(dr("RM_Code").ToString.Trim)
                    If strSKU_Name.Rows.Count > 0 Then
                        dr("SKU_Name") = strSKU_Name.Rows(0).Item("Str1")
                    End If
                Next

                grdPreviewData.DataSource = objWS
            Else
                Me.grdPreviewData.DataSource = objWS
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

    Private Sub cboWorkSheet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        grdPreviewData.DataSource = Nothing
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
        ''checking
        'If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
        '    W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
        '    Exit Sub
        'End If

        Dim oImport_SO As New bl_Import_SO

        '===============
        'Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

        If Me.grdPreviewData.RowCount = 0 Then
            W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
            Exit Sub
        Else
            Me.grdPreviewData.Columns("DocumentType_Index").Visible = False
        End If

        '===============

        oImport_SO.DataSource = grdPreviewData.DataSource

    End Sub

    Private Sub BntLoaddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
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
            frm.Customer_Index = Me._Customer_Index
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
            'checking 
            If Me.txtConsignee_Id.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ " & Me.lblConsignee.Text)
                Exit Sub
            End If

            'checking 
            If Me.txtShipping_Location_ID.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ  " & Me.lblReservedLocation.Text)
                Exit Sub
            End If

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
            Dim oImport_SO As New bl_SO_PRQ_KSL
            Check_Data = True

            _dtHeader = New DataTable
            _dtHeader.Columns.Add("SalesOrder_No", GetType(String))

            For i = 0 To grdPreviewData.Rows.Count - 1
                With grdPreviewData
                    'Set Red
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.Red

                    ''-------------------------------------------------------------------------------
                    'Document
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("PRQ ID").Value) Then
                        Dim dtSO As New DataTable
                        dtSO = oImport_SO.GetSaleOrder_No(.Rows(i).Cells("PRQ ID").Value.ToString)
                        'Add Primary key
                        Dim drNewRow As DataRow
                        drNewRow = _dtHeader.NewRow
                        Dim xSalesOrder_No As String = .Rows(i).Cells("PRQ ID").Value.ToString.Trim
                        drNewRow("SalesOrder_No") = xSalesOrder_No
                        Dim drCheck() As DataRow = _dtHeader.Select(String.Format("SalesOrder_No='{0}'", xSalesOrder_No))
                        If drCheck.Length = 0 Then
                            _dtHeader.Rows.Add(drNewRow)
                        End If

                        If dtSO.Rows.Count > 0 Then
                            .Rows(i).Cells("PRQ ID").Style.BackColor = Color.DimGray
                            .Rows(i).Cells("Check_Result").Value = "พบ PRQ ID. ซ้ำที่ (" & dtSO.Rows(0)("Str1").ToString & ")"
                            CheckPRQ += 1
                            'Check_Data = False
                            'Continue For
                        Else
                            .Rows(i).Cells("Check_Result").Value = "OK"
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "PRQ ID. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If

                    If Not String.IsNullOrEmpty(.Rows(i).Cells("WOI ID").Value) Then
                        Dim dtSO As New DataTable
                        dtSO = oImport_SO.GetWorkWOI_ID(.Rows(i).Cells("WOI ID").Value.ToString)
                        If dtSO.Rows.Count > 0 Then
                            .Rows(i).Cells("Check_Result").Value = "พบ WOI ID. ซ้ำที่ (" & dtSO.Rows(0)("WOI_ID").ToString & ")"
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    Else
                        .Rows(i).Cells("Check_Result").Value = "PRD Date. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If

                    If IsDate(.Rows(i).Cells("PRD Date").Value) Then
                        Dim strDate As String = CDate(.Rows(i).Cells("PRD Date").Value).Year
                        If CInt(strDate) > 2500 Then
                            .Rows(i).Cells("Check_Result").Value = "PRD Date. ผิดพลาดต้องเป็น คศ."
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    Else
                        .Rows(i).Cells("Check_Result").Value = "PRD Date. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If

                    If IsDate(.Rows(i).Cells("PRD Deadline").Value) Then
                        Dim strDate As String = CDate(.Rows(i).Cells("PRD Deadline").Value).Year
                        If CInt(strDate) > 2500 Then
                            .Rows(i).Cells("Check_Result").Value = "PRD Deadline. ผิดพลาดต้องเป็น คศ."
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    Else
                        .Rows(i).Cells("Check_Result").Value = "PRD Deadline. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If



                    '-------------------------------------------------------------------------------
                    'FG
                    Dim xRM_SKU_Index As String = ""
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("FG_Code").Value) Then
                        xRM_SKU_Index = oImport_SO.GetSKU_Index(.Rows(i).Cells("FG_Code").Value.ToString.Trim, "")
                        If xRM_SKU_Index = "" Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ FG_Code "
                            Check_Data = False
                            Continue For
                        Else
                            If Not String.IsNullOrEmpty(.Rows(i).Cells("FG_Unit").Value) Then
                                If oImport_SO.checkPackage(.Rows(i).Cells("FG_Unit").Value.ToString.Trim, xRM_SKU_Index) = False Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ FG_Unit"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            Else
                                .Rows(i).Cells("Check_Result").Value = "FG_Unit. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            End If
                            If Not String.IsNullOrEmpty(.Rows(i).Cells("FG_Unit_2").Value) Then
                                If oImport_SO.checkPackage(.Rows(i).Cells("FG_Unit_2").Value.ToString.Trim, xRM_SKU_Index) = False Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ FG_Unit_2"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            Else
                                .Rows(i).Cells("Check_Result").Value = "FG_Unit_2. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            End If
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "FG_Code. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If

                    'Qty
                    If Not IsNumeric(.Rows(i).Cells("QTY").Value) Then
                        .Rows(i).Cells("Check_Result").Value = "Qty. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    End If
                    '-------------------------------------------------------------------------------
                    'FG
                    Dim xFG_SKU_Index As String = ""
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("RM_Code").Value) Then
                        xFG_SKU_Index = oImport_SO.GetSKU_Index(.Rows(i).Cells("RM_Code").Value.ToString.Trim, "")
                        If xFG_SKU_Index = "" Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ RM_Code "
                            Check_Data = False
                            Continue For
                        Else
                            If Not String.IsNullOrEmpty(.Rows(i).Cells("RM_Unit1").Value) Then
                                If oImport_SO.checkPackage(.Rows(i).Cells("RM_Unit1").Value.ToString.Trim, xFG_SKU_Index) = False Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ RM_Unit1"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            Else
                                .Rows(i).Cells("Check_Result").Value = "RM_Unit1. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            End If

                            If Not String.IsNullOrEmpty(.Rows(i).Cells("RM_Unit2").Value) Then
                                If oImport_SO.checkPackage(.Rows(i).Cells("RM_Unit2").Value.ToString.Trim, xFG_SKU_Index) = False Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ RM_Unit2"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            Else
                                .Rows(i).Cells("Check_Result").Value = "RM_Unit2. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            End If
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "RM_Code. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If
                    'Qty
                    If Not IsNumeric(.Rows(i).Cells("QTY_RM").Value) Then
                        .Rows(i).Cells("Check_Result").Value = "QTY_RM. ผิดพลาด"
                        Check_Data = False
                        Continue For
                    Else
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    End If


                    'Doc type
                    If Not String.IsNullOrEmpty(.Rows(i).Cells("Document").Value) Then
                        'KSL : Fix Code
                        Select Case .Rows(i).Cells("Document").Value.ToString
                            Case "WIP", "FG"
                                Dim ArrDoc() As DataRow = CType(Me.cboDocumentType.DataSource, DataTable).Select("")
                                If .Rows(i).Cells("Document").Value.ToString = "WIP" Then
                                    ArrDoc = CType(Me.cboDocumentType.DataSource, DataTable).Select("DocumentType_Index = '" & Me._DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL & "'")
                                ElseIf .Rows(i).Cells("Document").Value.ToString = "FG" Then
                                    ArrDoc = CType(Me.cboDocumentType.DataSource, DataTable).Select("DocumentType_Index = '" & Me._DEFAULT_DOCUMENT_TYPE_SALEORDER_PACKAGING & "'")
                                End If
                                If ArrDoc.Length = 0 Then
                                    .Rows(i).Cells("Check_Result").Value = "ประเภทเอกสาร " & .Rows(i).Cells("Document").Value.ToString & " ผิดพลาด"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("DocumentType_Index").Value = ArrDoc(0)("DocumentType_Index").ToString
                                    .Rows(i).Cells("DocumentType").Value = ArrDoc(0)("Description").ToString
                                End If
                            Case Else
                                .Rows(i).Cells("Check_Result").Value = "ประเภทเอกสาร " & .Rows(i).Cells("Document").Value.ToString & " ผิดพลาด"
                                Check_Data = False
                                Continue For
                        End Select
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ประเภทเอกสาร  FG,WIP ผิดพลาด"
                        Check_Data = False
                        Continue For
                    End If


                    ''Stock_Qty
                    'If Me.chkStock.Checked Then
                    '    xQuery = " select isnull(R.Ratio,0) as Ratio from ms_SkuRatio R inner join ms_Package P on P.Package_Index = R.Package_Index"
                    '    xQuery &= String.Format(" where R.Sku_Index = '{0}'", xRM_SKU_Index)
                    '    xQuery &= String.Format(" and P.Description = '{0}'", .Rows(i).Cells("RM_Unit1").Value.ToString.Trim)
                    '    Dim xRatio As Double = xObjDB.DBExeQuery_Scalar(xQuery)

                    '    xQuery = " select isnull(Qty_Bal,0) as Qty_Bal "
                    '    xQuery &= " from view_WithdrawReserveLocation (nolock)"
                    '    xQuery &= String.Format(" where Sku_Index = '{0}'", xRM_SKU_Index)
                    '    Dim xTotal_Qty As Double = xObjDB.DBExeQuery_Scalar(xQuery)
                    '    Dim xStock_Qty As Double = FormatNumber((xTotal_Qty / xRatio), 6)
                    '    .Rows(i).Cells("Stock_Qty").Value = xStock_Qty
                    '    If (.Rows(i).Cells("Stock_Qty").Value) < (.Rows(i).Cells("QTY_RM").Value) Then
                    '        .Rows(i).Cells("Check_Result").Value = "จำนวนสินค้าไม่พอผลิต"
                    '    End If
                    'End If



                    '-------------------------------------------------------------------------------
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.Yellow
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

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub
End Class