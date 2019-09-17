Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class Import_Withdraw

    Private _DataSource As New DataTable
    Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value

            _GroupWithdraw_ID = New DataTable
            Dim strOrder_ID As String = ""
            _GroupWithdraw_ID.Columns.Add(New DataColumn("Withdraw_ID"))

            For Each odr As DataRow In _DataSource.Rows
                If strOrder_ID <> odr("Withdraw_ID").ToString.Trim Then
                    strOrder_ID = odr("Withdraw_ID").ToString.Trim
                    _GroupWithdraw_ID.Rows.Add(odr("Withdraw_ID").ToString.Trim)
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

    Private _GroupWithdraw_ID As New DataTable

    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Dim odtTemp As New DataTable

        Try

            Dim strConnString As String
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then Return odtTemp

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            With odaSource
                .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] ORDER BY Withdraw_ID,SKU_ID", oConnSource)
                .Fill(odtTemp)
            End With

            Me.DataSource = odtTemp

        Catch ex As Exception
            Throw ex
        End Try
        Return odtTemp
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

    Function StartImportData() As Boolean
        Dim odtFIFO As New DataTable
        Try

            If CheckingData() Then

                odtFIFO = Calculate_FIFO(Me.DataSource)
                Dim objSys_MaxID As New Sy_Temp_AutoNumber
                Dim oWithdraw As New tb_Withdraw

                Dim oWithdrawItem As New tb_WithdrawItem
                Dim oWithdrawItemCollection As New List(Of tb_WithdrawItem)

                Dim oWithdrawItemLocation As New tb_WithdrawItemLocation
                Dim oWithdrawItemLocationCollection As New List(Of tb_WithdrawItemLocation)

                Dim oPalletType As New tb_PalletType_History
                Dim oPalletTypeCollection As New List(Of tb_PalletType_History)

                Dim strSQL As String

                Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                Dim objDBIndex As New Sy_AutoNumber

                For Each odrWithdraw As DataRow In _GroupWithdraw_ID.Rows
                    With oWithdraw

                        ' *********** Define Value for Header ***********
                        'objSys_MaxID = New Sy_Temp_AutoNumber
                        .Withdraw_Index = objSys_MaxID.getSys_Value("Withdraw_Index")
                        .Withdraw_No = odrWithdraw("Withdraw_ID").ToString

                        .Contact_Name = ""
                        .Customer_Index = Me.Customer_Index
                        .Department_Index = ""

                        .Ref_No2 = ""
                        .Ref_No3 = ""
                        .Ref_No4 = ""
                        .Ref_No5 = ""
                        .Str1 = ""
                        .Str2 = ""
                        .Str3 = ""
                        .Str4 = ""
                        .Str5 = ""
                        .Str6 = ""
                        .Str7 = ""
                        .Str8 = ""
                        .Str9 = ""
                        .SO_No = ""
                        .Invoice_No = ""

                        .DocumentType_Index = Me.DocumentType_Index
                        .Comment = ""
                        .Customer_Shipping_Index = ""

                        .Driver_Index = ""
                        .Round = Now
                        .Str10 = ""
                        .Leave_Time = Now
                        .Factory_In = Now
                        .Factory_Out = Now
                        .Return_Time = Now

                    End With

                    'Clear Item
                    oWithdrawItemCollection.Clear()
                    oWithdrawItemLocationCollection.Clear()

                    strSQL = "Withdraw_ID = '" & odrWithdraw("Withdraw_ID").ToString & "'"
                    For Each odrFIFO As DataRow In odtFIFO.Select(strSQL)

                        'Withdraw
                        With oWithdraw
                            .Withdraw_Date = odrFIFO("Withdraw_Date").ToString.Trim
                            .Ref_No1 = odrFIFO("Ref").ToString.Trim
                        End With



                        ' *** WithdrawItem *********
                        oWithdrawItem = New tb_WithdrawItem
                        With oWithdrawItem
                            .Withdraw_Index = oWithdraw.Withdraw_Index

                            'WithdrawItem_Index ***  
                            .WithdrawItem_Index = objDBIndex.getSys_Value("WithdrawItem_Index")

                            'Sku_Index
                            .Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odrFIFO("SKU_ID").ToString.Trim)

                            'Qty
                            ' *** set Plan_Qty =Input Qty from Screen ***
                            .Plan_Qty = Val(odrFIFO("Qty").ToString)
                            ' *** set Qty 
                            .Qty = Val(odrFIFO("Qty").ToString)

                            'Package_Index
                            .Package_Index = GetPackage_Index(oWithdrawItem.Sku_Index, odrFIFO("UOM").ToString.Trim)

                            ' *** Get Retio ***

                            .Ratio = objRatio.getRatio(.Sku_Index, .Package_Index)

                            ' *****************

                            ' *** Calculate Tatal Qty *** 
                            ' *** Important using Plan_Qty for Calculate Plan_Total_Qty ***
                            .Plan_Total_Qty = .Plan_Qty * .Ratio

                            ' *** set Total_Qty  ***
                            .Total_Qty = .Qty * .Ratio

                            'PLot
                            .PLot = odrFIFO("PLot").ToString.Trim

                            'ItemStatus_Index
                            .ItemStatus_Index = Me.ItemStatus_Index

                            'clPallet_No_item
                            .Str4 = ""
                        End With
                        ' *** Add value ***
                        oWithdrawItemCollection.Add(oWithdrawItem)

                        oWithdrawItemLocation = New tb_WithdrawItemLocation
                        With oWithdrawItemLocation
                            'WithdrawItemLocation_Index ***  
                            .WithdrawItemLocation_Index = objDBIndex.getSys_Value("WithdrawItemLocation_Index")

                            .Withdraw_Index = oWithdraw.Withdraw_Index
                            .WithdrawItem_Index = oWithdrawItem.WithdrawItem_Index


                            .Item_Qty = oWithdrawItem.ItemQty
                            .ItemStatus_Index = oWithdrawItem.ItemStatus_Index
                            .Lot_No = ""
                            .Plot = oWithdrawItem.PLot
                            .Price = oWithdrawItem.Price
                            .Qty = oWithdrawItem.Qty
                            .Ratio = oWithdrawItem.Ratio
                            .Package_Index = oWithdrawItem.Package_Index
                            .Pallet_Qty = 0
                            .Total_Qty = oWithdrawItem.Total_Qty
                            .Volume = oWithdrawItem.Volume
                            .Weight = oWithdrawItem.Weight
                            .Sku_Index = oWithdrawItem.Sku_Index

                            .Tag_No = odrFIFO("Tag_No").ToString
                            .Order_Index = odrFIFO("Order_Index").ToString
                            .LocationBalance_Index = odrFIFO("LocationBalance_Index").ToString
                            .Location_Index = odrFIFO("Location_Index").ToString

                        End With
                        oWithdrawItemLocationCollection.Add(oWithdrawItemLocation)
                    Next


                    'Save Withdraw Transaction
                    Dim objWithdrawTransaction As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.ADDNEW, oWithdraw, oWithdrawItemCollection, oWithdrawItemLocationCollection, oPalletTypeCollection)
                    Dim strRealWithDraw_Index As String
                    strRealWithDraw_Index = objWithdrawTransaction.SaveData()


                    'Save Location Balance
                    save_whitdrawLcation(strRealWithDraw_Index, oWithdrawItemCollection, odtFIFO.Select(strSQL))

                Next
            Else
                Return False
            End If
            Return True

        Catch ex As Exception
            Throw ex
        End Try

        odtFIFO = Nothing
    End Function
    Function CheckingData() As Boolean

        Try
            Dim boolResult As Boolean = True

            For Each odr As DataRow In Me.DataSource.Rows
                For Each odc As DataColumn In Me.DataSource.Columns
                    Select Case odc.ColumnName.Trim.ToLower
                        Case "Withdraw_ID".ToLower
                            If IsExitData("tb_Withdraw", "Withdraw_No", odr(odc.ColumnName).ToString) = True Then
                                odr("Check_Result") = "ข้อมูลใบเบิกสินค้านี้มีอยู่ก่อนแล้ว !"
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
                            Else
                                Dim odtLocationBalance As DataTable = GetLocationBalance(odr("SKU_ID").ToString.Trim)

                                If odtLocationBalance.Rows.Count <= 0 Then
                                    odr("Check_Result") = "ไม่พบสินค้ารายการนี้"
                                    boolResult = False
                                    Exit For
                                End If

                                Dim intQTY As Integer
                                Dim intQTY_Sum As Integer
                                Dim strPackage_index As String
                                Dim strSKU_Index As String
                                Dim intRatio As Integer = 1

                                strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

                                ' strPackage_index = GetIndexByID("ms_Package", "Package_Index", "Package_Id", odr("UOM"))


                                strPackage_index = GetPackage_Index(strSKU_Index, odr("UOM").ToString.Trim)


                                intRatio = Get_sku_ratio(strSKU_Index, strPackage_index)
                                'Check Amount Withdraw
                                intQTY = odr("QTY").ToString * intRatio
                                intQTY_Sum = odtLocationBalance.Compute("SUM(QTY_Bal)", "1=1")

                                'SUM(Qty_Bal) Less than QTY Exit
                                If intQTY_Sum < intQTY Then
                                    odr("Check_Result") = "จำนวนสินค้าไม่เพียงพอต่อการเบิกครั้งนี้ !"
                                    boolResult = False
                                    Exit For
                                End If

                            End If

                        Case "width", "length", "height", "volume", "weight"
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr(odc.ColumnName.Trim) = 0
                            End If

                            ''Case "Customer_ID".ToLower
                            ''    If IsExitData("ms_Customer", "Customer_Id", odr(odc.ColumnName).ToString) = False Then
                            ''        odr("Checking_Result") = "ไม่พบข้อมูล ลูกค้ารหัสนี้"
                            ''        Return False
                            ''    End If
                        Case Else

                    End Select

                    odr("Check_Result") = "OK."
                Next
            Next

            Return boolResult
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function CheckingDataExcel() As Boolean

        Try
            Dim boolResult As Boolean = True

            For Each odr As DataRow In Me.DataSource.Rows
                For Each odc As DataColumn In Me.DataSource.Columns
                    Select Case odc.ColumnName.Trim.ToLower
                        Case "Withdraw_ID".ToLower
                            If IsExitData("tb_Withdraw", "Withdraw_No", odr(odc.ColumnName).ToString) = True Then
                                odr("Check_Result") = "ข้อมูลใบเบิกสินค้านี้มีอยู่ก่อนแล้ว !"
                                'boolResult = False
                                'Exit For
                            End If

                        Case "SKU_ID".ToLower
                            If IsExitData("ms_SKU", "Sku_Id", odr(odc.ColumnName).ToString) = False Then
                                odr("Check_Result") = "ไม่พบข้อมูล SKU นี้ !"
                                boolResult = False
                                'Exit For
                            End If
                            If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
                                odr("Check_Result") = "กรุณากรอกข้อมูล SKU!"
                                boolResult = False
                                'Exit For
                            End If

                        Case "UOM".ToLower
                            'Dim strSKU_Index As String
                            'strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

                            'Dim strPackage_Index As String
                            'strPackage_Index = GetPackage_Index(strSKU_Index, odr(odc.ColumnName).ToString.Trim)
                            If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
                                odr("Check_Result") = "ไม่พบข้อมูล UOM นี้ !"
                                boolResult = False
                                'Exit For
                            End If


                        Case "QTY".ToLower
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr("Check_Result") = "กรุณาป้อนจำนวน"
                                boolResult = False
                                Exit For

                            ElseIf IsNumeric(odr(odc.ColumnName.Trim).ToString) = False Then
                                odr("Check_Result") = "กรุณาป้อนจำนวนใหม่ให้ถูกต้อง"
                                boolResult = False
                                Exit For
                            Else
                                Dim odtLocationBalance As DataTable = GetLocationBalance(odr("SKU_ID").ToString.Trim)

                                If odtLocationBalance.Rows.Count <= 0 Then
                                    odr("Check_Result") = "ไม่พบสินค้ารายการนี้"
                                    boolResult = False
                                    Exit For
                                End If

                                Dim intQTY As Integer
                                Dim intQTY_Sum As Integer
                                Dim strPackage_index As String
                                Dim strSKU_Index As String
                                Dim intRatio As Integer = 1

                                strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

                                ' strPackage_index = GetIndexByID("ms_Package", "Package_Index", "Package_Id", odr("UOM"))


                                strPackage_index = GetPackage_Index(strSKU_Index, odr("UOM").ToString.Trim)


                                intRatio = Get_sku_ratio(strSKU_Index, strPackage_index)
                                'Check Amount Withdraw
                                intQTY = odr("QTY").ToString * intRatio
                                intQTY_Sum = odtLocationBalance.Compute("SUM(QTY_Bal)", "1=1")

                                'SUM(Qty_Bal) Less than QTY Exit
                                If intQTY_Sum < intQTY Then
                                    odr("Check_Result") = "จำนวนสินค้าไม่เพียงพอต่อการเบิกครั้งนี้ !"
                                    boolResult = False
                                    Exit For
                                End If

                            End If

                        Case "width", "length", "height", "volume", "weight"
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr(odc.ColumnName.Trim) = 0
                            End If

                            ''Case "Customer_ID".ToLower
                            ''    If IsExitData("ms_Customer", "Customer_Id", odr(odc.ColumnName).ToString) = False Then
                            ''        odr("Checking_Result") = "ไม่พบข้อมูล ลูกค้ารหัสนี้"
                            ''        Return False
                            ''    End If
                        Case Else

                    End Select

                    odr("Check_Result") = "OK."
                Next
            Next

            Return boolResult
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function StartImportDataExcel() As Boolean
        Dim odtFIFO As New DataTable
        'Dim oDT As New DataTable
        'oDT = Me.DataSource
        'For i As Integer = 0 To oDT.Rows.Count - 1
        'Next
        Try

            odtFIFO = Calculate_FIFO(Me.DataSource)
            Dim objSys_MaxID As New Sy_Temp_AutoNumber
            Dim oWithdraw As New tb_Withdraw

            Dim oWithdrawItem As New tb_WithdrawItem
            Dim oWithdrawItemCollection As New List(Of tb_WithdrawItem)

            Dim oWithdrawItemLocation As New tb_WithdrawItemLocation
            Dim oWithdrawItemLocationCollection As New List(Of tb_WithdrawItemLocation)

            Dim oPalletType As New tb_PalletType_History
            Dim oPalletTypeCollection As New List(Of tb_PalletType_History)

            Dim strSQL As String

            Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDBIndex As New Sy_AutoNumber

            For Each odrWithdraw As DataRow In _GroupWithdraw_ID.Rows
                With oWithdraw

                    ' *********** Define Value for Header ***********
                    'objSys_MaxID = New Sy_Temp_AutoNumber
                    .Withdraw_Index = objSys_MaxID.getSys_Value("Withdraw_Index")
                    .Withdraw_No = odrWithdraw("Withdraw_ID").ToString

                    .Contact_Name = ""
                    .Customer_Index = Me.Customer_Index
                    .Department_Index = ""

                    .Ref_No2 = ""
                    .Ref_No3 = ""
                    .Ref_No4 = ""
                    .Ref_No5 = ""
                    .Str1 = ""
                    .Str2 = ""
                    .Str3 = ""
                    .Str4 = ""
                    .Str5 = ""
                    .Str6 = ""
                    .Str7 = ""
                    .Str8 = ""
                    .Str9 = ""
                    .SO_No = ""
                    .Invoice_No = ""

                    .DocumentType_Index = Me.DocumentType_Index
                    .Comment = ""
                    .Customer_Shipping_Index = ""

                    .Driver_Index = ""
                    .Round = Now
                    .Str10 = ""
                    .Leave_Time = Now
                    .Factory_In = Now
                    .Factory_Out = Now
                    .Return_Time = Now

                End With

                'Clear Item
                oWithdrawItemCollection.Clear()
                oWithdrawItemLocationCollection.Clear()

                strSQL = "Withdraw_ID = '" & odrWithdraw("Withdraw_ID").ToString & "'"
                For Each odrFIFO As DataRow In odtFIFO.Select(strSQL)

                    'Withdraw
                    With oWithdraw
                        .Withdraw_Date = odrFIFO("Withdraw_Date").ToString.Trim
                        .Ref_No1 = odrFIFO("Ref").ToString.Trim
                    End With



                    ' *** WithdrawItem *********
                    oWithdrawItem = New tb_WithdrawItem
                    With oWithdrawItem
                        .Withdraw_Index = oWithdraw.Withdraw_Index

                        'WithdrawItem_Index ***  
                        .WithdrawItem_Index = objDBIndex.getSys_Value("WithdrawItem_Index")

                        'Sku_Index
                        .Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odrFIFO("SKU_ID").ToString.Trim)

                        'Qty
                        ' *** set Plan_Qty =Input Qty from Screen ***
                        .Plan_Qty = Val(odrFIFO("Qty").ToString)
                        ' *** set Qty 
                        .Qty = Val(odrFIFO("Qty").ToString)

                        'Package_Index
                        .Package_Index = GetPackage_Index(oWithdrawItem.Sku_Index, odrFIFO("UOM").ToString.Trim)

                        ' *** Get Retio ***

                        .Ratio = objRatio.getRatio(.Sku_Index, .Package_Index)

                        ' *****************

                        ' *** Calculate Tatal Qty *** 
                        ' *** Important using Plan_Qty for Calculate Plan_Total_Qty ***
                        .Plan_Total_Qty = .Plan_Qty * .Ratio

                        ' *** set Total_Qty  ***
                        .Total_Qty = .Qty * .Ratio

                        'PLot
                        .PLot = odrFIFO("PLot").ToString.Trim

                        'ItemStatus_Index
                        .ItemStatus_Index = Me.ItemStatus_Index

                        'clPallet_No_item
                        .Str4 = ""
                    End With
                    ' *** Add value ***
                    oWithdrawItemCollection.Add(oWithdrawItem)

                    oWithdrawItemLocation = New tb_WithdrawItemLocation
                    With oWithdrawItemLocation
                        'WithdrawItemLocation_Index ***  
                        .WithdrawItemLocation_Index = objDBIndex.getSys_Value("WithdrawItemLocation_Index")

                        .Withdraw_Index = oWithdraw.Withdraw_Index
                        .WithdrawItem_Index = oWithdrawItem.WithdrawItem_Index


                        .Item_Qty = oWithdrawItem.ItemQty
                        .ItemStatus_Index = oWithdrawItem.ItemStatus_Index
                        .Lot_No = ""
                        .Plot = oWithdrawItem.PLot
                        .Price = oWithdrawItem.Price
                        .Qty = oWithdrawItem.Qty
                        .Ratio = oWithdrawItem.Ratio
                        .Package_Index = oWithdrawItem.Package_Index
                        .Pallet_Qty = 0
                        .Total_Qty = oWithdrawItem.Total_Qty
                        .Volume = oWithdrawItem.Volume
                        .Weight = oWithdrawItem.Weight
                        .Sku_Index = oWithdrawItem.Sku_Index

                        .Tag_No = odrFIFO("Tag_No").ToString
                        .Order_Index = odrFIFO("Order_Index").ToString
                        .LocationBalance_Index = odrFIFO("LocationBalance_Index").ToString
                        .Location_Index = odrFIFO("Location_Index").ToString

                    End With
                    oWithdrawItemLocationCollection.Add(oWithdrawItemLocation)
                Next


                'Save Withdraw Transaction
                Dim objWithdrawTransaction As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.ADDNEW, oWithdraw, oWithdrawItemCollection, oWithdrawItemLocationCollection, oPalletTypeCollection)
                Dim strRealWithDraw_Index As String
                strRealWithDraw_Index = objWithdrawTransaction.SaveData()


                'Save Location Balance
                save_whitdrawLcation(strRealWithDraw_Index, oWithdrawItemCollection, odtFIFO.Select(strSQL))

            Next



        Catch ex As Exception
            Throw ex
        End Try

        odtFIFO = Nothing
    End Function

    Sub save_whitdrawLcation(ByVal pstrWithDraw_Index As String, ByVal pobjWithdraw_Item As List(Of tb_WithdrawItem), ByVal podrWithdraw() As DataRow)
        Try
            For i As Integer = 0 To podrWithdraw.Length - 1
                Dim otb_WithdrawItem_Location As New tb_WithdrawItemLocation

                Dim LocationBal_Index As String = podrWithdraw(i)("LocationBalance_Index").ToString.Trim
                '-----------------------------------------------------
                Dim Qty_RecivePackage As String = pobjWithdraw_Item(i).Qty
                Dim Package_ReciveIndex As String = pobjWithdraw_Item(i).Package_Index
                Dim Qty_bal As Double = pobjWithdraw_Item(i).Total_Qty
                Dim Weight As Double = 0
                Dim Volume As Double = 0
                Dim Order_Index As String = podrWithdraw(i)("Order_Index").ToString.Trim
                Dim sku_index As String = pobjWithdraw_Item(i).Sku_Index
                'Dim Lot_No As String = DT.Rows(0).Item("Lot_No").ToString
                Dim Lot_No As String = ""
                Dim Plot As String = pobjWithdraw_Item(i).PLot
                Dim Item_Status_Index As String = pobjWithdraw_Item(i).ItemStatus_Index
                Dim Tag_no As String = podrWithdraw(i)("TAG_No").ToString.Trim
                Dim Location_index As String = podrWithdraw(i)("Location_Index").ToString
                Dim Serial_No As String = pobjWithdraw_Item(i).Serial_No
                '------------------------------------------------------
                Dim WhitDraw_Index As String = pstrWithDraw_Index
                Dim WhitDrawItem_Index As String = pobjWithdraw_Item(i).WithdrawItem_Index
                '------------------------------------------------------
                Dim ItemQty As Double = pobjWithdraw_Item(i).ItemQty
                Dim Price As Double = pobjWithdraw_Item(i).Price

                Dim Pallet_Qty As Integer = 0

                otb_WithdrawItem_Location.SAVE_TB_WITHDRAWITEMLOCATION(WhitDraw_Index, WhitDrawItem_Index, LocationBal_Index, Qty_RecivePackage, Package_ReciveIndex, Qty_bal, Weight, Volume, Order_Index, sku_index, Lot_No, Plot, Item_Status_Index, Tag_no, Location_index, Serial_No, Pallet_Qty, ItemQty, Price, 1)
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "'"

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

    Function Get_sku_ratio(ByVal strSku_index As String, ByVal strPackage_index As String) As Integer
        Dim intRation As Integer = 0
        Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.SEARCH)
        Dim objDT As New DataTable
        objDB.SelectData_ByPackage(strSku_index, strPackage_index)
        objDT = objDB.DataTable

        If objDT.Rows.Count > 0 Then
            intRation = objDT.Rows(0).Item("Ratio")
        End If

        Return intRation
    End Function

#Region "   function picking (FIFO)   "
    Private Function Calculate_FIFO(ByVal podtWithdraw As DataTable) As DataTable
        Dim odtResult As New DataTable
        Try
            odtResult = podtWithdraw.Clone

            odtResult.Columns.Add(New DataColumn("Total_QTY"))
            odtResult.Columns.Add(New DataColumn("LocationBalance_Index"))
            odtResult.Columns.Add(New DataColumn("PLot"))
            odtResult.Columns.Add(New DataColumn("Order_Index"))
            odtResult.Columns.Add(New DataColumn("Location_Index"))
            odtResult.Columns.Add(New DataColumn("TAG_No"))

            odtResult.Columns.Remove("Check_Result")

            For Each odr As DataRow In podtWithdraw.Rows
                AutoWithdrawByItem(odtResult, odr)
            Next
        Catch ex As Exception
            Throw ex
        End Try

        Return odtResult
    End Function


    Function GetLocationBalance(ByVal pstrSKU_ID As String) As DataTable
        Dim odtLocationBalance As New DataTable
        Try

            Dim strWhere As String
            Dim strSKU_Index As String

            'Get Location By SKU_ID
            strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", pstrSKU_ID)

            strWhere = "  WHERE sku_index='" & strSKU_Index & "'"
            strWhere &= " AND ItemStatus_Index = '" & Me.ItemStatus_Index & "'"
            strWhere &= " AND Customer_Index = '" & Me.Customer_Index & "'"
            strWhere &= " AND QTY_Bal > 0"
            strWhere &= " ORDER BY CONVERT(int,ref1,20),Order_date ASC"

            Dim objDB As New WMS_STD_OUTB_Datalayer.View_LocationBalance
            objDB.SearchData_Click(strWhere)

            odtLocationBalance = objDB.GetDataTable

        Catch ex As Exception
            Throw ex
        End Try

        Return odtLocationBalance
    End Function

    Function AutoWithdrawByItem(ByVal podtResult As DataTable, ByVal podrItem As DataRow) As DataTable
        Try

            Dim intRatio As Integer = 1
            Dim odtLocationBalance As DataTable = GetLocationBalance(podrItem("SKU_ID").ToString.Trim)
            Dim intQTY_Bal As Integer
            Dim intQTY As Integer
            Dim intQTY_Sum As Integer
            Dim strPackage_index As String
            Dim strSKU_Index As String


            If odtLocationBalance.Rows.Count <= 0 Then Return podtResult

            strSKU_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", podrItem("SKU_ID").ToString.Trim)
            strPackage_index = GetIndexByID("ms_Package", "Package_Index", "Package_Id", podrItem("UOM"))

            intRatio = Get_sku_ratio(strSKU_Index, strPackage_index)
            'Check Amount Withdraw
            intQTY = podrItem("QTY").ToString * intRatio
            intQTY_Sum = odtLocationBalance.Compute("SUM(QTY_Bal)", "1=1")

            'SUM(Qty_Bal) Less than QTY Exit
            If intQTY_Sum < intQTY Then Return podtResult

            For Each odrLocation As DataRow In odtLocationBalance.Rows
                intQTY_Bal = odrLocation("Qty_Bal").ToString.Trim()

                'Decrease intQTY_Bal
                If intQTY_Bal >= intQTY Then
                    intQTY_Bal = intQTY
                End If

                With podtResult
                    .Rows.Add(podrItem("Withdraw_ID").ToString.Trim _
                            , podrItem("Withdraw_Date").ToString.Trim _
                            , podrItem("SKU_ID").ToString.Trim _
                            , podrItem("SKU_Name").ToString.Trim _
                            , podrItem("QTY").ToString.Trim _
                            , podrItem("UOM").ToString.Trim _
                            , podrItem("Ref").ToString.Trim _
                            , intQTY_Bal _
                            , odrLocation("LocationBalance_Index").ToString.Trim _
                            , odrLocation("PLot").ToString.Trim _
                            , odrLocation("Order_Index").ToString.Trim _
                            , odrLocation("Location_Index").ToString.Trim _
                            , odrLocation("TAG_No").ToString.Trim _
                             )
                End With

                'Decrease QTY Withdraw 
                intQTY -= intQTY_Bal
                If intQTY <= 0 Then Exit For
            Next

        Catch ex As Exception
            Throw ex
        End Try
        Return podtResult
    End Function

#End Region


End Class
