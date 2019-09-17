Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class Import_Order

    Private _DataSource As New DataTable
    Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value

            _GroupReceipt_ID = New DataTable
            Dim strOrder_ID As String = ""
            _GroupReceipt_ID.Columns.Add(New DataColumn("Receipt_ID"))

            For Each odr As DataRow In _DataSource.Rows
                If strOrder_ID <> odr("Receipt_ID").ToString.Trim Then
                    strOrder_ID = odr("Receipt_ID").ToString.Trim
                    _GroupReceipt_ID.Rows.Add(odr("Receipt_ID").ToString.Trim)
                End If
            Next
        End Set
    End Property

    Private _Customer_Index As String
    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Private _DocumentType_Index As String
    Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Private _ItemStatus_Index As String
    Property ItemStatus_Index() As String
        Get
            Return _ItemStatus_Index
        End Get
        Set(ByVal value As String)
            _ItemStatus_Index = value
        End Set
    End Property

    Private _GroupReceipt_ID As New DataTable

    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            With odaSource
                '.SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] ORDER BY Receipt_ID", oConnSource)
                .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] ", oConnSource)
                .Fill(odtTemp)
            End With



            Me.DataSource = odtTemp


            'With odaSource
            '    .SelectCommand = New OleDbCommand("SELECT Receipt_ID FROM [" & pstrWorkSheet & "$] GROUP BY Receipt_ID ORDER BY Receipt_ID", oConnSource)
            '    .Fill(_GroupReceipt_ID)
            'End With

            Return odtTemp

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function CheckingData() As Boolean
        Try
            Dim boolResult As Boolean = True

            For Each odr As DataRow In Me.DataSource.Rows
                For Each odc As DataColumn In Me.DataSource.Columns
                    Select Case odc.ColumnName.Trim.ToLower
                        Case "Receipt_ID".ToLower
                            If IsExitData("tb_Order", "Order_No", odr(odc.ColumnName).ToString) = True Then
                                odr("Check_Result") = "ข้อมูลใบรับสินค้านี้มีอยู่ก่อนแล้ว !"
                                boolResult = False
                                Exit For
                            End If

                        Case "SKU_ID".ToLower
                            If IsExitData("ms_SKU", "Sku_Id", odr(odc.ColumnName).ToString) = False Then
                                odr("Check_Result") = "ไม่พบข้อมูล SKU นี้ !"
                                boolResult = False
                                Exit For
                            End If

                        Case "UOM".ToLower

                            Dim strSKU_Index As String
                            strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

                            Dim strPackage_Index As String
                            strPackage_Index = GetPackage_Index(strSKU_Index, odr(odc.ColumnName).ToString.Trim)

                            If strPackage_Index = "" Then
                                odr("Check_Result") = "ไม่พบข้อมูล UOM นี้ !"
                                boolResult = False
                                Exit For
                            End If

                        Case "QTY".ToLower
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr("Check_Result") = "กรุณาป้อนจำนวน"
                                boolResult = False
                                Exit For
                            ElseIf IsNumeric(odr(odc.ColumnName.Trim).ToString) = False Then
                                odr("Check_Result") = "กรุณาใหม่ป้อนจำนวนให้ถูกต้อง"
                                boolResult = False
                                Exit For
                            End If

                        Case "width", "length", "height", "volume", "weight"
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr(odc.ColumnName.Trim) = 0
                            End If

                            'Case "Customer_ID".ToLower
                            '    If IsExitData("ms_Customer", "Customer_Id", odr(odc.ColumnName).ToString) = False Then
                            '        odr("Checking_Result") = "ไม่พบข้อมูล ลูกค้ารหัสนี้"
                            '        Return False
                            '    End If

                    End Select

                    odr("Check_Result") = "OK."

                Next
            Next

            Return boolResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT P.Package_Index "
            strSQL &= " FROM ms_Package P INNER JOIN ms_SKU S ON P.Package_Index = S.Package_Index"
            strSQL &= " 	INNER JOIN ms_SKURatio SR ON SR.SKU_index = S.SKU_Index AND P.Package_Index = SR.Package_Index"
            strSQL &= " WHERE S.SKU_index = '" & pstrSKU_Index & "' AND  P.Description = '" & pstrPackage_ID.Replace("'", "''") & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)("Package_Index").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "' and Status_Id <> -1 "

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)(pstrField_Index).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function IsExitData(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String) As Boolean
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrFieldName & " FROM " & pstrTableName & " WHERE " & pstrFieldName & " = '" & pstrFieldValue.Trim & "'"
            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select
            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function StartImportData() As Boolean
        Try
            If CheckingData() Then 'Checking Pass

                Dim objHeader As New tb_Order
                Dim objItem As New tb_OrderItem
                Dim objItemCollection As New List(Of tb_OrderItem)
                Dim ItemLife_Total_Day As Integer = 0
                Dim objSys_MaxID As New Sy_Temp_AutoNumber

                Dim strReceipt_ID As String = ""
                Dim objPalletType As New tb_PalletType_History
                Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

                Dim strSQL As String

                For Each odrOrder As DataRow In Me._GroupReceipt_ID.Rows

                    strSQL = "Receipt_ID = '" & odrOrder("Receipt_ID").ToString & "'"

                    ' *********** Define Value for Header ***********
                    objHeader.Order_Index = objSys_MaxID.getSys_Value("Order_Index")

                    objHeader.Order_No = odrOrder("Receipt_ID").ToString
                    objHeader.Lot_No = ""

                    ' xxxxxxx Using TAG Properties xxxxxxxxxxx
                    objHeader.Customer_Index = Me.Customer_Index
                    objHeader.Supplier_Index = ""
                    objHeader.Department_Index = ""

                    ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


                    objHeader.Ref_No2 = ""
                    objHeader.Ref_No3 = ""
                    objHeader.Ref_No4 = ""
                    objHeader.Ref_No5 = ""
                    objHeader.Str1 = ""
                    objHeader.Str2 = ""
                    objHeader.Str3 = ""
                    objHeader.Str4 = ""
                    objHeader.Str5 = ""
                    objHeader.DocumentType_Index = Me.DocumentType_Index
                    objHeader.Comment = "Import data"
                    objHeader.Str8 = "" 'Me.cboContact_Name.Text
                    objHeader.PO_No = "" 'Me.txtPo.Text
                    objHeader.Invoice_No = "" 'Me.txtInvoice_No.Text
                    objHeader.ASN_No = "" 'Me.txtASN_No.Text

                    'Clear OrderItem
                    objItemCollection.Clear()

                    For Each odrOrderItem As DataRow In Me.DataSource.Select(strSQL)

                        objHeader.Order_Date = CDate(odrOrderItem("Receipt_Date").ToString).ToString("dd/MM/yyyy")
                        objHeader.Order_Time = CDate(odrOrderItem("Receipt_Date").ToString).ToString("HH:mm")
                        objHeader.Ref_No1 = odrOrderItem("Ref").ToString


                        'Detail
                        ' *** New Object *********
                        objItem = New tb_OrderItem
                        ' ************************

                        ' *** Check PO ***
                        '   If CheckPO_use() = False Then Exit Sub

                        ' *** Check value of OrderItem_Index ***                 

                        Dim objDBIndex As New Sy_AutoNumber
                        objItem.OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                        objDBIndex = Nothing

                        ' *********************
                        ' xxx value from header ***
                        objItem.Order_Index = objHeader.Order_Index

                        'Sku_Index
                        objItem.Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odrOrderItem("SKU_ID").ToString.Trim)

                        'Plan_Qty
                        objItem.Plan_Qty = 0

                        'PO
                        objItem.Str7 = ""
                        objItem.PO_No = ""

                        'comment 
                        objItem.Comment = ""

                        'Ref.
                        objItem.Str1 = odrOrderItem("Ref").ToString
                        objItem.Str3 = ""

                        'INVOICE
                        objItem.Invoice_No = ""

                        'Pallet No.
                        objItem.Str5 = ""

                        objItem.Str2 = ""
                        'Referent 2   ' chang to Systemlot
                        'If Me.DocumentType_Index <> "0010000000022" Then
                        '    objItem.Str2 = Now.ToString("yyyyMMdd")
                        'Else
                        '    objItem.Str2 = ""
                        'End If

                        'POINDEX
                        objItem.Str10 = ""

                        'Qty
                        objItem.Qty = odrOrderItem("Qty").ToString

                        'Package_Index
                        Dim strPackage_Index As String
                        strPackage_Index = GetPackage_Index(objItem.Sku_Index, odrOrderItem("UOM").ToString.Trim)

                        objItem.Package_Index = strPackage_Index

                        ' *** Get Retio ***
                        Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
                        objRatio = Nothing

                        ' *****************
                        ' *** Calculate Tatal Qty *** 
                        objItem.Total_Qty = objItem.Qty * objItem.Ratio
                        ' ***************************

                        'Weight
                        objItem.Weight = odrOrderItem("Weight").ToString

                        'col_Net_Weight
                        objItem.Flo1 = odrOrderItem("Weight").ToString

                        'Volume
                        objItem.Volume = odrOrderItem("Volume").ToString

                        'PalletType_Index 
                        objItem.PalletType_Index = ""

                        'Pallet_Qty
                        objItem.Pallet_Qty = 0

                        'PLot
                        objItem.Plot = odrOrderItem("Lot").ToString
                        objItem.Lot_No = ""

                        'Item Status Fix (Good)
                        objItem.ItemStatus_Index = Me.ItemStatus_Index

                        'Serial_No
                        objItem.Serial_No = ""

                        objItem.Mfg_Date = Now
                        objItem.Exp_date = Now
                        'If IsDate(CDate(odrOrderItem("Mfg_Date"))) Then
                        '    objItem.Mfg_Date = CDate(odrOrderItem("Mfg_Date"))
                        'End If

                        'If IsDate(CDate(odrOrderItem("Exp_Date"))) Then
                        '    objItem.Exp_date = CDate(odrOrderItem("Exp_Date"))
                        'End If

                        ' *** set default of IsMfg_Date and IsExp_Date For EFFEM *** 
                        objItem.IsMfg_Date = True
                        objItem.IsExp_Date = True
                        ' **********************************************************

                        ' ItemDefinition_Index
                        objItem.ItemDefinition_Index = ""

                        ' *** Add value ***
                        objItemCollection.Add(objItem)

                    Next

                    'Save data per Order

                    'Not Controls Pallet
                    objPalletTypeCollection.Add(objPalletType)
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                    objDB.SaveData()
                    objDB = Nothing
                Next
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function
   
End Class

