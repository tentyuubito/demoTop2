Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
'Imports WMS_INH_Receive

Public Class TransferOwnerTransaction : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    '*** Property Readonly
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property
    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
    End Enum
#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum
#End Region

#Region "variable & property "

    Private _NewTransferOwner_Index As String

    Private _NewTransferOwner_No As String
    Private _objItemCollection As New List(Of tb_TransferOwnerLocation)
    Private objTransferCodeTransaction As New tb_TransferOwnerLocation

    Public Property NewTransferOwner_Index() As String
        Get
            Return _NewTransferOwner_Index
        End Get
        Set(ByVal value As String)
            _NewTransferOwner_Index = value
        End Set
    End Property
    Public Property NewTransferOwner_No() As String
        Get
            Return _NewTransferOwner_No
        End Get
        Set(ByVal value As String)
            _NewTransferOwner_No = value
        End Set
    End Property
#End Region

#Region " SELECT DATA "
    Public Sub getTransferOwnerView(ByVal strWhere As String)
        '  
        Dim strSQL As String = ""

        Try

            ' *** Need to tb_TransferCodeLocation.status =-9 *** 
            strSQL = " select * "
            strSQL &= " from VIEW_TransferOwner_View "
            strSQL &= " WHERE 1=1 "
            strSQL &= strWhere
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    'Add By Pong 28/04/2015
    Public Sub getTransferOwnerView(ByVal WhereString As String, ByVal pintRowStart As Integer, ByVal pintRowEnd As Integer)
        '  
        Dim strSQL As String = ""

        Try

            ' *** Need to tb_TransferCodeLocation.status =-9 *** 
            strSQL &= "SELECT * FROM ("
            strSQL &= " SELECT ROW_NUMBER() OVER(ORDER BY TransferOwner_Index ASC) AS ROW_NO, Count(*) Over() AS ROW_COUNT"

            strSQL &= " , * "
            strSQL &= " from VIEW_TransferOwner_View "
            strSQL &= " WHERE 1=1 "

            If WhereString <> "" Then
                strSQL &= WhereString
            End If
            strSQL &= " ) AS TransferOwner_View "

            If (pintRowStart > 0) Then
                strSQL &= " WHERE ROW_NO BETWEEN " & pintRowStart & " AND " & pintRowEnd
            End If

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SelectTransferCodeLocation(ByVal TransferCode_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " select * "
            strSQL &= " from VIEW_TransferCodeLocation "
            strWhere = " WHERE TransferCode_Index ='" & TransferCode_Index & "'"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Sub
    Public Sub getTransferOwnerHeader(ByVal TransferOwner_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " select * "
            strSQL &= " from VIEW_TransferOwnerHeader "
            strWhere = " WHERE TransferOwner_Index ='" & TransferOwner_Index & "'"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Sub
    Public Sub GetTransferAssetLocation(ByVal TransferCode_Index As String)
        '  
        Dim strSQL As String = ""

        Try

            strSQL = " select * "
            strSQL &= " from VIEW_TransferCode_Detail "
            strSQL &= " WHERE TransferCode_Index ='" & TransferCode_Index & "'"
            'strSQL &= "  AND Total_Qty - Return_Qty > 0 "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Sub
    Public Sub GetTransferOwnerLocation(ByVal TransferOwner_Index As String)
        '  
        Dim strSQL As String = ""

        Try

            strSQL = " select * "
            strSQL &= " from VIEW_TransferOwner_Detail "
            strSQL &= " WHERE TransferOwner_Index ='" & TransferOwner_Index & "' "
            'strSQL &= "  AND Total_Qty - Return_Qty > 0 "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Sub

    ''' <summary>
    ''' Get Transfer Status View
    ''' </summary>
    ''' <param name="WhereString"></param>
    ''' <remarks></remarks>
    Public Sub GetTransferCodeView(ByVal WhereString As String)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            '' --- Need to join ms_ProcessStatus with Process_Id = 15 FOR TransferCode
            'strSQL = "   SELECT   tb_TransferCode.TransferCode_Index, tb_TransferCode.TransferCode_No, tb_TransferCode.TransferCode_Date, tb_TransferCode.Ref_No1, tb_TransferCode.add_by"
            'strSQL &= " ,ms_DocumentType.DocumentType_Index, ms_DocumentType.Description as Document_Des"
            'strSQL &= " ,ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name"
            'strSQL &= " ,ms_ProcessStatus.Status, ms_ProcessStatus.Description as Status_Des"
            'strSQL &= " ,tb_TransferCode.Str1,tb_TransferCode.Comment"
            'strSQL &= " ,tb_TransferCode.TransferCode_Name,tb_TransferCode.ExpectedReturn_Date"
            'strSQL &= " , ms_Department.Department_index , ms_Department.Description as Department_des"
            'strSQL &= "  FROM    tb_TransferCode INNER JOIN "
            'strSQL &= "   ms_customer ON tb_TransferCode.Customer_Index = ms_Customer.Customer_Index "
            'strSQL &= " INNER JOIN ms_ProcessStatus ON tb_TransferCode.Status = ms_ProcessStatus.Status "
            'strSQL &= " INNER JOIN ms_DocumentType on  tb_TransferCode.DocumentType_Index = ms_DocumentType.DocumentType_Index "
            'strSQL &= " INNER JOIN ms_Department ON tb_TransferCode.Department_index = ms_Department.Department_index"
            'strSQL &= " WHERE ms_ProcessStatus.Process_Id = 15  "

            strSQL = "   SELECT   tb_TransferCode.TransferCode_Index, tb_TransferCode.TransferCode_No, tb_TransferCode.TransferCode_Date, tb_TransferCode.Ref_No1, tb_TransferCode.Ref_No2,tb_TransferCode.add_by,tb_TransferCode.DocumentType_Index, ms_DocumentType.Description,ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name,ms_ProcessStatus.Status, ms_ProcessStatus.Description as StatusDescription"
            strSQL &= "  ,tb_TransferCode.Str1,tb_TransferCode.Comment"
            strSQL &= "  FROM    tb_TransferCode INNER JOIN "
            strSQL &= "   ms_customer ON tb_TransferCode.Customer_Index = ms_Customer.Customer_Index INNER JOIN ms_ProcessStatus ON tb_TransferCode.Status = ms_ProcessStatus.Status INNER JOIN ms_DocumentType on  tb_TransferCode.DocumentType_Index = ms_DocumentType.DocumentType_Index "
            strSQL &= "   WHERE ms_ProcessStatus.Process_Id =15  "

            SetSQLString = strSQL + WhereString

            SetSQLString = strSQL + WhereString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetProcessStatus()

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT * "
            strSQL &= " FROM     VIEW_ProcessStatus "
            strSQL &= " WHERE Process_Id= 15  AND Show=1  "
            strSQL &= " ORDER BY Seq ASC "


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
#End Region

#Region " MANAGE TRANSACTION AND BALANCE "

    'Public Function BalTran(ByVal objHeader As tb_TransferCode) As String
    Public Function BalTran(ByVal pstrTransferStatus_Index As String) As String
        Dim strSQL As String
        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0
        '  _NewTransferCode_Index = objHeader.TransferCode_Index

        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0

        Dim strNew_LocationBalance_Index As String = ""
        Dim strNew_Tag_No As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans
        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Modify tb_LocationBalance 
        '             If   Move Qty All ; Update
        '             Else Move Some Qty ; Insert into tb_LocationBalance
        ' --- STEP 2: Update ms_Location 
        ' --- STEP 3: Update tb_TransferCode & tb_TransferCodeLocation
        ' --- STEP 4: Insert tb_Transaction & tb_AssetTransaction OUT
        ' --- STEP 5: Insert tb_Transaction & tb_AssetTransaction IN
        Try

            strSQL = "  SELECT  *"
            strSQL &= "  FROM      VIEW_TransferCodeLocation_Confirm"
            strSQL &= "   WHERE  TransferCode_Index ='" & pstrTransferStatus_Index & "' "
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans


            DataAdapter.Fill(DS, "TFC")

            If DS.Tables("TFC").Rows.Count <> 0 Then
                For i As Integer = 0 To DS.Tables("TFC").Rows.Count - 1

                    Dim odrTransferCode As DataRow = DS.Tables("TFC").Rows(i)

                    ' ***********************************
                    Dim Customer_Index As String = odrTransferCode("Customer_Index").ToString
                    Dim Plot As String = odrTransferCode("PLot").ToString
                    Dim Old_ItemStatus_Index As String = odrTransferCode("Old_ItemStatus_Index").ToString
                    Dim New_ItemStatus_Index As String = odrTransferCode("New_ItemStatus_Index").ToString
                    Dim Old_Sku_Index As String = odrTransferCode("Old_Sku_Index").ToString
                    Dim New_Sku_Index As String = odrTransferCode("New_Sku_Index").ToString

                    ' ***********************************
                    Dim iTag_No As String = odrTransferCode("Tag_No").ToString
                    Dim iFrom_Location_Index As String = odrTransferCode("From_Location_Index").ToString
                    Dim iTo_Location_Index As String = odrTransferCode("To_Location_Index").ToString
                    Dim iTotal_Qty As Decimal = CDec(odrTransferCode("Total_Qty").ToString)
                    Dim iWeight As Decimal = CDec(odrTransferCode("Weight").ToString)
                    Dim iVolume As Decimal = CDec(odrTransferCode("Volume").ToString)
                    Dim iItem_Qty As Decimal = CDec(odrTransferCode("Item_Qty").ToString)
                    Dim iPrice As Decimal = CDec(odrTransferCode("Price").ToString)
                    Dim iPalletType_Index As String = odrTransferCode("PalletType_Index").ToString
                    Dim iPallet_Qty As Integer = CDec(odrTransferCode("Pallet_Qty").ToString)
                    Dim TransferCode_No As String = odrTransferCode("TransferCode_No").ToString
                    Dim New_TransferCodeLocation_Index As String = odrTransferCode("TransferCodeLocation_Index").ToString
                    Dim LocationBalance_Index As String = odrTransferCode("LocationBalance_Index").ToString

                    Dim Package_Index As String = odrTransferCode("Package_Index").ToString



                    ''New Package
                    Dim strNewPackage_Index As String = ""
                    Dim oms_SKURatio As New ms_SKURatio(ms_Package.enuOperation_Type.SEARCH)
                    Dim odt As New DataTable

                    oms_SKURatio.SelectData_ByPackage(odrTransferCode("Old_Sku_Index").ToString, odrTransferCode("Package_Index").ToString)
                    odt = oms_SKURatio.DataTable
                    If odt.Rows.Count > 0 Then
                        Dim odtPackage As New DataTable
                        oms_SKURatio.SelectData(" and sku_index = '" & New_Sku_Index & "'  and ratio = " & odt.Rows(0)("ratio"))

                        odtPackage = oms_SKURatio.DataTable

                        If odtPackage.Rows.Count > 0 Then
                            strNewPackage_Index = odtPackage.Rows(0)("Package_Index").ToString
                        Else
                            oms_SKURatio.SelectData(" and sku_index = '" & New_Sku_Index & "'  and ratio = 1")

                            odtPackage = oms_SKURatio.DataTable
                            strNewPackage_Index = odtPackage.Rows(0)("Package_Index").ToString
                        End If

                    End If


                    Dim iRatio As Decimal = 1
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    iRatio = objRatio.getRatio(odrTransferCode("Old_Sku_Index").ToString, Package_Index)
                    objRatio = Nothing

                    Dim iQty As Decimal = iTotal_Qty / iRatio

                    ' *** Check ?? : Move All Qty of Tag or Move Some Qty from TAG ***
                    strSQL = "   SELECT  * "
                    strSQL &= "   FROM  tb_LocationBalance   "
                    strSQL &= "  WHERE  Tag_No='" & iTag_No & "' AND Location_Index ='" & iFrom_Location_Index & "' "
                    strSQL &= "  AND  LocationBalance_Index ='" & LocationBalance_Index & "'"


                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    DataAdapter.Fill(DS, "LB") 'tb_LocationBalance

                    If DS.Tables("LB").Rows.Count > 0 Then
                        Dim odrLocationBalance As DataRow = DS.Tables("LB").Rows(0)


                        If iTotal_Qty = CDec(odrLocationBalance("Qty_Bal").ToString) Then
                            ' *** Move All Qty of Tag ***
                            strNew_LocationBalance_Index = LocationBalance_Index
                            strNew_Tag_No = iTag_No

                            'strSQL = " UPDATE tb_LocationBalance  SET  "
                            'strSQL &= "Qty_Bal ='" & iTotal_Qty & "'"
                            'strSQL &= " ,Location_Index='" & iTo_Location_Index & "' "
                            'strSQL &= " ,ItemStatus_Index='" & New_ItemStatus_Index & "'"
                            'strSQL &= " ,SKU_Index = '" & New_Sku_Index & "'"
                            'strSQL &= " ,ReserveQty = ReserveQty - " & iTotal_Qty
                            'strSQL &= " ,ReserveWeight = ReserveWeight - " & iWeight
                            'strSQL &= " ,ReserveVolume = ReserveVolume - " & iVolume
                            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item - " & iItem_Qty
                            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price - " & iPrice
                            'strSQL &= " WHERE  Tag_No='" & iTag_No & "' "
                            'strSQL &= " AND Location_Index ='" & iFrom_Location_Index & "' "
                            'strSQL &= "  AND  LocationBalance_Index ='" & LocationBalance_Index & "'"


                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()


                            ' ***  DONG_KK UPDATE ***

                            strSQL = "  UPDATE tb_LocationBalance    "
                            strSQL &= " SET "
                            strSQL &= " Location_Index =@ToLocation_Index"
                            strSQL &= " ,ItemStatus_Index =@New_ItemStatus_Index"
                            strSQL &= " ,SKU_Index  =@New_Sku_Index"
                            strSQL &= " ,Package_Index  =@Package_Index"

                            strSQL &= " ,Qty_Recieve_Package=@Qty"
                            strSQL &= " ,Qty_Bal=@Total_Qty"
                            strSQL &= " ,Weight_Bal=@Weight"
                            strSQL &= " ,Volume_Bal=@Volume "
                            strSQL &= " ,Qty_Item_Bal =@ItemQty"
                            strSQL &= " ,OrderItem_Price_Bal=@Price"

                            strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                            strSQL &= " ,ReserveWeight = ReserveWeight-@Weight"
                            strSQL &= " ,ReserveVolume = ReserveVolume-@Volume"
                            strSQL &= " ,ReserveQty_Item = ReserveQty_Item-@ItemQty"
                            strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price-@Price"
                            strSQL &= " WHERE  LocationBalance_Index=@LocationBalance_Index "
                            'strSQL &= " WHERE Tag_No=@Tag_No "
                            'strSQL &= " AND Location_Index =@FromLocation_Index"
                            'strSQL &= "  AND  LocationBalance_Index=@LocationBalance_Index"
                            With SQLServerCommand

                                .Parameters.Clear()
                                .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 50).Value = New_ItemStatus_Index
                                .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 50).Value = New_Sku_Index
                                .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = strNewPackage_Index
                                .Parameters.Add("@FromLocation_Index", SqlDbType.VarChar, 50).Value = iFrom_Location_Index
                                .Parameters.Add("@ToLocation_Index", SqlDbType.VarChar, 50).Value = iTo_Location_Index
                                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 50).Value = LocationBalance_Index
                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = iTag_No
                                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = iTotal_Qty
                                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = iWeight
                                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = iVolume
                                ' *** If Check Qty_Recieve_Package *** 
                                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = iQty
                                .Parameters.Add("@ItemQty", SqlDbType.Float, 8).Value = iItem_Qty
                                .Parameters.Add("@Price", SqlDbType.Float, 8).Value = iPrice

                            End With
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()


                            ' *** Manage ms_Location from New Location : iTo_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 1
                            strSQL &= " Space_Used =1,Current_Qty=Current_Qty+" & iTotal_Qty & ",Current_Weight=Current_Weight+" & iWeight & ",Current_Volume=Current_Volume+" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iTo_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Manage ms_Location from Old Location : iFrom_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 2
                            strSQL &= " Current_Qty=Current_Qty-" & iTotal_Qty & ",Current_Weight=Current_Weight-" & iWeight & ",Current_Volume=Current_Volume-" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iFrom_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** If Area of Location Emtry ***
                            strSQL = "UPDATE ms_Location " 'ครั้งที่ 3
                            strSQL &= " SET Space_Used =0 "
                            strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & iFrom_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Update tb_TransferCode ***
                            strSQL = "UPDATE tb_TransferCode "
                            strSQL &= " SET Status = 2 "
                            strSQL &= " WHERE  TransferCode_Index ='" & pstrTransferStatus_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                            '06/05/2009
                            ' *** Update tb_TransferStatusLocation ***
                            strSQL = "UPDATE tb_TransferCodeLocation SET "
                            strSQL &= " Status = 2 "
                            strSQL &= " ,To_LocationBalance_Index = '" & strNew_LocationBalance_Index & "'"
                            strSQL &= " WHERE  TransferCodeLocation_Index ='" & odrTransferCode("TransferCodeLocation_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                        Else

                            strSQL = "  UPDATE tb_LocationBalance    "
                            strSQL &= " SET "
                            'strSQL &= " Location_Index =@ToLocation_Index"
                            'strSQL &= " ,ItemStatus_Index =@New_ItemStatus_Index"
                            'strSQL &= " ,SKU_Index  =@New_Sku_Index"

                            strSQL &= " Qty_Recieve_Package=Qty_Recieve_Package-@Qty"
                            strSQL &= " ,Qty_Bal=Qty_Bal-@Total_Qty"
                            strSQL &= " ,Weight_Bal=Weight_Bal-@Weight"
                            strSQL &= " ,Volume_Bal=Volume_Bal-@Volume "
                            strSQL &= " ,Qty_Item_Bal = Qty_Item_Bal-@ItemQty"
                            strSQL &= " ,OrderItem_Price_Bal=OrderItem_Price_Bal-@Price"

                            strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                            strSQL &= " ,ReserveWeight = ReserveWeight-@Weight"
                            strSQL &= " ,ReserveVolume = ReserveVolume-@Volume"
                            strSQL &= " ,ReserveQty_Item = ReserveQty_Item-@ItemQty"
                            strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price-@Price"
                            strSQL &= " WHERE Tag_No=@Tag_No "
                            strSQL &= " AND Location_Index =@FromLocation_Index"
                            strSQL &= "  AND  LocationBalance_Index=@LocationBalance_Index"
                            With SQLServerCommand

                                .Parameters.Clear()
                                .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 50).Value = New_ItemStatus_Index
                                .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 50).Value = New_Sku_Index
                                .Parameters.Add("@FromLocation_Index", SqlDbType.VarChar, 50).Value = iFrom_Location_Index
                                .Parameters.Add("@ToLocation_Index", SqlDbType.VarChar, 50).Value = iTo_Location_Index
                                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 50).Value = LocationBalance_Index
                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = iTag_No
                                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = iTotal_Qty
                                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = iWeight
                                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = iVolume
                                ' *** If Check Qty_Recieve_Package *** 
                                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = iQty
                                .Parameters.Add("@ItemQty", SqlDbType.Float, 8).Value = iItem_Qty
                                .Parameters.Add("@Price", SqlDbType.Float, 8).Value = iPrice

                            End With
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            '*********************** Add by Dong_kk 02-04-2010  **********************
                            '*********************** Begin Add New Tag No .     **********************


                            Dim objSy_AutoNumber As New Sy_AutoNumber
                            strNew_Tag_No = objSy_AutoNumber.getSys_Value("Tag_No")
                            objSy_AutoNumber = Nothing
                            Dim objDBIndex As New Sy_AutoNumber
                            Dim strNew_Tag_Index As String = objDBIndex.getSys_Value("Tag_Index")


                            strSQL = " INSERT INTO 	tb_Tag(Process_Id,Document_Index,TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,add_by,add_date,add_branch )"
                            strSQL &= "             (SELECT top 1 15, '" & pstrTransferStatus_Index & "', '" & strNew_Tag_Index & "','" & strNew_Tag_No & "'"
                            strSQL &= "             ,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No"
                            strSQL &= "             ," & CDec(odrTransferCode("Total_Qty").ToString) & "," & CDec(odrTransferCode("Weight").ToString) & "," & CDec(odrTransferCode("Volume").ToString) & "," & CDec(odrTransferCode("Total_Qty").ToString) & "," & CDec(odrTransferCode("Weight").ToString) & "," & CDec(odrTransferCode("Volume").ToString) & ",1"
                            strSQL &= "             ,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,add_by,add_date,add_branch "
                            strSQL &= "     FROM tb_Tag "
                            strSQL &= "     WHERE  Tag_No='" & iTag_No & "' AND sku_Index ='" & odrLocationBalance("Sku_Index").ToString & "' "
                            strSQL &= "             AND  OrderItem_Index ='" & odrLocationBalance("OrderItem_Index").ToString & "')"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            '*********************** END Add New Tag No . **********************


                            strSQL = " INSERT INTO tb_LocationBalance    "
                            strSQL &= " (LocationBalance_Index,Location_Index,Tag_No,Sku_Index,Order_Index,OrderItem_Index,Lot_No,PLot,ItemStatus_Index,Serial_No,PalletType_Index,Pallet_Qty,Ratio,Qty_Bal,Weight_Bal,Volume_Bal,Qty_Item_Begin,OrderItem_Price_Begin,Qty_Item_Bal,OrderItem_Price_Bal,Location_Index_Begin,Qty_Bal_Begin,Weight_Bal_Begin,Volume_Bal_Begin,Mfg_Date,Exp_Date,IsMfg_Date,IsExp_Date,Package_Index,add_by,add_branch,MixPallet,Qty_Recieve_Package,Item_Package_Index) VALUES "
                            strSQL &= " (@LocationBalance_Index,@Location_Index,@Tag_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Lot_No,@PLot,@ItemStatus_Index,@Serial_No,@PalletType_Index,@Pallet_Qty,@Ratio,@Qty_Bal,@Weight_Bal,@Volume_Bal,@Qty_Item_Begin,@OrderItem_Price_Begin,@Qty_Item_Bal,@OrderItem_Price_Bal,@Location_Index_Begin,@Qty_Bal_Begin,@Weight_Bal_Begin,@Volume_Bal_Begin,@Mfg_Date,@Exp_Date,@IsMfg_Date,@IsExp_Date,@Package_Index,@add_by,@add_branch,@MixPallet,@Qty_Recieve_Package,@Item_Package_Index) "


                            With SQLServerCommand

                                .Parameters.Clear()

                                '  **** Generate OrderItemLocation_Index  ***

                                '    Dim objDBIndex As New Sy_AutoNumber

                                strNew_LocationBalance_Index = objDBIndex.getSys_Value("LocationBalance_Index")

                                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = strNew_LocationBalance_Index
                                objDBIndex = Nothing
                                ' *******************************************
                                .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrLocationBalance("OrderItem_Index").ToString
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrLocationBalance("Order_Index").ToString
                                .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_Sku_Index").ToString
                                .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrLocationBalance("Lot_No").ToString
                                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("To_Location_Index").ToString
                                .Parameters.Add("@Location_Index_Begin", SqlDbType.VarChar, 13).Value = odrTransferCode("From_Location_Index").ToString
                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = iTag_No
                                ' ''new TagNo 2009/04/24
                                ''Dim objSy_AutoNumber As New Sy_AutoNumber
                                ''strNew_Tag_No = objSy_AutoNumber.getSys_Value("Tag_No")
                                ''objSy_AutoNumber = Nothing

                                ''.Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                                '
                                .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrLocationBalance("PLot").ToString
                                .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_ItemStatus_Index").ToString
                                .Parameters.Add("@Qty_Bal_Begin", SqlDbType.Float, 8).Value = odrTransferCode("Total_Qty")
                                .Parameters.Add("@Weight_Bal_Begin", SqlDbType.Float, 8).Value = odrTransferCode("Weight")
                                .Parameters.Add("@Volume_Bal_Begin", SqlDbType.Float, 8).Value = odrTransferCode("Volume")
                                .Parameters.Add("@Qty_Item_Begin", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                                .Parameters.Add("@OrderItem_Price_Begin", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                                .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = odrLocationBalance("Ratio")
                                .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = odrTransferCode("Total_Qty")
                                .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = odrTransferCode("Weight")
                                .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = odrTransferCode("Volume")
                                .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                                .Parameters.Add("@OrderItem_Price_Bal", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                                .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrLocationBalance("Serial_No").ToString
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 50).Value = odrLocationBalance("PalletType_Index").ToString
                                .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = odrLocationBalance("Pallet_Qty")
                                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                                .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = odrLocationBalance("IsExp_Date")
                                .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = odrLocationBalance("IsMfg_Date")
                                .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = odrLocationBalance("Exp_Date")
                                .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = odrLocationBalance("Mfg_Date")
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Item_Package_Index").ToString
                                .Parameters.Add("@MixPallet", SqlDbType.Bit, 4).Value = odrLocationBalance("MixPallet")

                                ' *** Package_Index,Qty_Recieve_Package,Qty_Faction  ***
                                .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = strNewPackage_Index 'odrLocationBalance("Package_Index").ToString

                                .Parameters.Add("@Qty_Recieve_Package", SqlDbType.Float, 8).Value = odrTransferCode("Total_Qty")

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Manage ms_Location from New Location : iTo_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 1
                            strSQL &= " Space_Used =1,Current_Qty=Current_Qty+" & iTotal_Qty & ",Current_Weight=Current_Weight+" & iWeight & ",Current_Volume=Current_Volume+" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iTo_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Manage ms_Location from Old Location : iFrom_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 2
                            strSQL &= " Current_Qty=Current_Qty-" & iTotal_Qty & ",Current_Weight=Current_Weight-" & iWeight & ",Current_Volume=Current_Volume-" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iFrom_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** If Area of Location Emtry ***
                            strSQL = "UPDATE ms_Location " 'ครั้งที่ 3
                            strSQL &= " SET Space_Used =0 "
                            strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & iFrom_Location_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Update tb_TransferCode ***
                            strSQL = "UPDATE tb_TransferCode "
                            strSQL &= " SET Status = 2 "
                            strSQL &= " WHERE  TransferCode_Index ='" & pstrTransferStatus_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' *** Update tb_TransferStatusLocation ***
                            strSQL = "UPDATE tb_TransferCodeLocation SET "
                            strSQL &= " Status = 2 "
                            strSQL &= " ,To_LocationBalance_Index = '" & strNew_LocationBalance_Index & "'"
                            strSQL &= " WHERE  TransferCodeLocation_Index ='" & odrTransferCode("TransferCodeLocation_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            EXEC_Command()
                        End If

                        ' ***********************  2. คำนวนจำนวนสินค้า
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, odrLocationBalance("LocationBalance_Index").ToString)

                        objCalBalance = Nothing
                        ' ***********************
                    End If

                    ' *** Important : You need to insert two Record in tb_transaction >> Record IN and Record OUT  ***

                    ' *** Call Function Get Balance *** จำนวนสินค้า
                    Dim objBal As New CalculateBalance
                    ' *** Qty ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index) '- Cdec(odrTransferCode("Total_Qty").ToString)
                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot) '- Cdec(odrTransferCode("Total_Qty").ToString)
                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransferCode("Total_Qty").ToString)

                    If Qty_Sku_Bal < 0 Then
                        Qty_Sku_Bal = 0
                    End If
                    If Qty_PLot_Bal < 0 Then
                        Qty_PLot_Bal = 0
                    End If
                    If Qty_ItemStatus_Bal < 0 Then
                        Qty_ItemStatus_Bal = 0
                    End If

                    ' *** Weight ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** น้ำหนักของสินค้า
                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index) '- Cdec(odrTransferCode("Weight").ToString)
                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot) '- Cdec(odrTransferCode("Weight").ToString)
                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransferCode("Weight").ToString)

                    If Weight_Sku_Bal < 0 Then
                        Weight_Sku_Bal = 0
                    End If
                    If Weight_PLot_Bal < 0 Then
                        Weight_PLot_Bal = 0
                    End If
                    If Weight_ItemStatus_Bal < 0 Then
                        Weight_ItemStatus_Bal = 0
                    End If

                    ' *** Volume ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index) '- Cdec(odrTransferCode("Volume").ToString)
                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot) '- Cdec(odrTransferCode("Volume").ToString)
                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransferCode("Volume").ToString)

                    If Volume_Sku_Bal < 0 Then
                        Volume_Sku_Bal = 0
                    End If
                    If Volume_PLot_Bal < 0 Then
                        Volume_PLot_Bal = 0
                    End If
                    If Volume_ItemStatus_Bal < 0 Then
                        Volume_ItemStatus_Bal = 0
                    End If
                    Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, odrTransferCode("From_Location_Index").ToString)
                    Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, odrTransferCode("From_Location_Index").ToString)
                    Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, Old_ItemStatus_Index, odrTransferCode("From_Location_Index").ToString)


                    If Qty_Sku_Location_Bal < 0 Then
                        Qty_Sku_Location_Bal = 0
                    End If
                    If Qty_PLot_Location_Bal < 0 Then
                        Qty_PLot_Bal = 0
                    End If
                    If Qty_ItemStatus_Location_Bal < 0 Then
                        Qty_ItemStatus_Location_Bal = 0
                    End If
                    objBal = Nothing


                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    ' *** Insert Record OUT from Old_ItemStatus *** 3.เพิ่มข้อมูลลงตาราง
                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_out,Weight_Out,Volume_Out,Qty_Item_Out,OrderItem_Price_Out,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index ,Location_Alias_To,Location_Alias_From,Serial_No,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,Item_Package_Index) VALUES "
                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Old_Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_out,@Weight_Out,@Volume_Out,@Qty_Item_Out,@OrderItem_Price_Out,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@Item_Package_Index)"

                    ' **** Manage Balance ***

                    With SQLServerCommand
                        .Parameters.Clear()
                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = odrTransferCode("TransferCode_No").ToString
                        .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("From_Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("To_Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransferCode("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_ItemStatus_Index").ToString
                        .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_ItemStatus_Index").ToString
                        .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString) 'iTotal_Qty

                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        ' Order_Date
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("TransferCode_Date").ToString).ToString("yyyy/MM/dd")

                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 15

                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Weight").ToString)
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Volume").ToString)
                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Item_Package_Index").ToString

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "13"

                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Customer_Index").ToString
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("DocumentType_Index").ToString

                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransferCode("str4").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransferCode("str5").ToString

                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Order_Index").ToString
                        .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("Order_date")).ToString("yyyy/MM/dd")
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("OrderItem_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("ProductType_Index").ToString

                        .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransferCode("To_Location_Alias").ToString
                        .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransferCode("From_Location_Alias").ToString
                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal

                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    ' 27/04/2009 Insert tb_AssetTransaction
                    ' *********************************
                    ' *** Insert tb_AssetTransaction ***
                    ' *** Insert Record OUT from Old_ItemStatus 
                    If odrTransferCode("AssetLocationBalance_Index").ToString <> "" Then


                        strSQL = " INSERT INTO tb_AssetTransaction  "
                        strSQL &= " (AssetTransaction_Index,AssetLocationBalance_Index,AssetTransaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_out,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Asset_No,Qty_Item_Out,OrderItem_Price_Out,Item_Package_Index) VALUES "
                        strSQL &= " (@AssetTransaction_Index,@AssetLocationBalance_Index,@AssetTransaction_Id,@Old_Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_out,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Asset_No,@Qty_Item_Out,@OrderItem_Price_Out,@Item_Package_Index)"

                        ' **** Manage Balance ***
                        With SQLServerCommand
                            .Parameters.Clear()
                            '  **** Generate OrderItemLocation_Index  ***
                            Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("AssetTransaction_Index")
                            objDBIndex = Nothing
                            ' *******************************************
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("AssetLocationBalance_Index").ToString
                            .Parameters.Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = odrTransferCode("TransferCode_No").ToString
                            .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("From_Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("To_Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransferCode("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_ItemStatus_Index").ToString
                            .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_ItemStatus_Index").ToString
                            .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString) 'iTotal_Qty

                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Serial_No").ToString
                            .Parameters.Add("@Asset_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Asset_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            ' Order_Date
                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("TransferCode_Date").ToString).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 15

                            .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)
                            .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Weight").ToString)
                            .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Volume").ToString)
                            .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                            .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Item_Package_Index").ToString

                            .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                            .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                            .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                            .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                            .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                            .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                            .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                            .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                            .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                            .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "13"

                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Customer_Index").ToString
                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("DocumentType_Index").ToString

                            .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransferCode("str4").ToString
                            .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransferCode("str5").ToString


                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Order_Index").ToString
                            .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("Order_date")).ToString("yyyy/MM/dd")
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("OrderItem_Index").ToString
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("ProductType_Index").ToString

                            .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransferCode("To_Location_Alias").ToString
                            .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransferCode("From_Location_Alias").ToString


                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                    ' *** Call Function Get Balance ***
                    objBal = New CalculateBalance
                    ' *** Qty ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index)
                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot)
                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, New_ItemStatus_Index)

                    ' *** Weight ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index)
                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot)
                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, New_ItemStatus_Index)

                    ' *** Volume ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index)
                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot)
                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, New_ItemStatus_Index)

                    Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, odrTransferCode("From_Location_Index").ToString)
                    Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, odrTransferCode("From_Location_Index").ToString)
                    Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Old_Sku_Index, Plot, Old_ItemStatus_Index, odrTransferCode("From_Location_Index").ToString)



                    objBal = Nothing

                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    ' *** Insert Record IN from New_ItemStatus ***
                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_In,Weight_In,Volume_In,Qty_Item_In,OrderItem_Price_In,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,Item_Package_Index) VALUES "
                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Old_Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_In,@Weight_In,@Volume_In,@Qty_Item_In,@OrderItem_Price_In,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@Item_Package_Index)"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = odrTransferCode("TransferCode_No").ToString
                        .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Lot_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransferCode("PLot").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("From_Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("To_Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_ItemStatus_Index").ToString
                        .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_ItemStatus_Index").ToString
                        .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)

                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        ' Order_Date
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("TransferCode_Date").ToString).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 15

                        .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)
                        .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Weight").ToString)
                        .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Volume").ToString)
                        .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                        .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Item_Package_Index").ToString

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "13"

                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("Customer_Index").ToString
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("DocumentType_Index").ToString

                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransferCode("str4").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransferCode("str5").ToString


                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Order_Index").ToString
                        .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("Order_date")).ToString("yyyy/MM/dd")
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("OrderItem_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("ProductType_Index").ToString

                        .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransferCode("To_Location_Alias").ToString
                        .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransferCode("From_Location_Alias").ToString

                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal

                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    '  connectDB()
                    EXEC_Command()



                    ' *********************************
                    ' 27/04/2009 Insert tb_AssetTransaction
                    ' *** Insert Record IN from New_ItemStatus ***
                    If odrTransferCode("AssetLocationBalance_Index").ToString <> "" Then

                        strSQL = " INSERT INTO tb_AssetTransaction  "
                        strSQL &= " (AssetTransaction_Index,AssetLocationBalance_Index,AssetTransaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_In,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Asset_No,Qty_Item_In,OrderItem_Price_In,Item_Package_Index) VALUES "
                        strSQL &= " (@AssetTransaction_Index,@AssetLocationBalance_Index,@AssetTransaction_Id,@New_Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_In,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Asset_No,@Qty_Item_In,@OrderItem_Price_In,@Item_Package_Index)"

                        ' **** Manage Balance ***

                        With SQLServerCommand

                            .Parameters.Clear()
                            '  **** Generate OrderItemLocation_Index  ***
                            Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("AssetTransaction_Index")
                            objDBIndex = Nothing
                            ' *******************************************
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("AssetLocationBalance_Index").ToString
                            .Parameters.Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = odrTransferCode("TransferCode_No").ToString
                            .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("From_Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("To_Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransferCode("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Old_ItemStatus_Index").ToString
                            .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_ItemStatus_Index").ToString
                            .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)

                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Serial_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            ' Order_Date
                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("TransferCode_Date").ToString).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 15

                            .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)
                            .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Weight").ToString)
                            .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Volume").ToString)
                            .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                            .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Item_Package_Index").ToString

                            .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                            .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                            .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                            .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                            .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                            .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                            .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                            .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                            .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                            .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "13"

                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("Customer_Index").ToString
                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransferCode("DocumentType_Index").ToString

                            .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransferCode("str4").ToString
                            .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransferCode("str5").ToString

                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("Order_Index").ToString
                            .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransferCode("Order_date")).ToString("yyyy/MM/dd")
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("OrderItem_Index").ToString
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("ProductType_Index").ToString

                            .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransferCode("To_Location_Alias").ToString
                            .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransferCode("From_Location_Alias").ToString
                            .Parameters.Add("@Asset_No", SqlDbType.VarChar, 50).Value = odrTransferCode("Asset_No").ToString

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        '  connectDB()
                        EXEC_Command()

                        'Update tb_AssetLocationBalance
                        strSQL = " UPDATE tb_AssetLocationBalance SET "
                        strSQL &= " Location_Index = @Location_Index"
                        strSQL &= " ,LocationBalance_Index = @LocationBalance_Index"
                        strSQL &= " ,ItemStatus_Index = @ItemStatus_Index"
                        strSQL &= " ,Package_Index = @Package_Index"
                        strSQL &= " ,SKU_Index = @SKU_Index"
                        strSQL &= " ,Str1 = @str1"
                        strSQL &= " ,Str2 = @str2"
                        strSQL &= " ,Str3 = @str3"
                        strSQL &= " ,Str4 = @str4"
                        strSQL &= " ,Str5 = @str5"
                        strSQL &= " ,Flo1 = @Flo1"
                        strSQL &= " ,Flo2 = @Flo2"
                        strSQL &= " ,Flo3 = @Flo3"
                        strSQL &= " ,Flo4 = @Flo4"
                        strSQL &= " ,Flo5 = @Flo5"
                        strSQL &= " ,Asset_No = @Asset_No"
                        strSQL &= " ,ReserveQty = ReserveQty - @ReserveQty "
                        strSQL &= " ,ReserveWeight = ReserveWeight-@ReserveWeight"
                        strSQL &= " ,ReserveVolume = ReserveVolume-@ReserveVolume"
                        strSQL &= " ,ReserveQty_Item = ReserveQty_Item-@ReserveQty_Item"
                        strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price-@ReserveOrderItem_Price"

                        strSQL &= " ,update_by = @update_by"
                        strSQL &= " ,update_date = @update_date"
                        strSQL &= " ,update_branch = @update_branch"
                        strSQL &= " ,Tag_No = @Tag_No"
                        strSQL &= " WHERE AssetLocationBalance_Index = @AssetLocationBalance_Index"

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@Location_Index", SqlDbType.NVarChar, 100).Value = odrTransferCode("To_Location_Index").ToString
                            .Parameters.Add("@LocationBalance_Index", SqlDbType.NVarChar, 100).Value = strNew_LocationBalance_Index

                            .Parameters.Add("@ItemStatus_Index", SqlDbType.NVarChar, 100).Value = odrTransferCode("New_ItemStatus_Index").ToString
                            .Parameters.Add("@Package_Index", SqlDbType.NVarChar, 100).Value = strNewPackage_Index 'odrTransferCode("Col_Package_Index_New").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransferCode("New_Sku_Index").ToString
                            .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = odrTransferCode("Str1").ToString
                            .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = odrTransferCode("Str2").ToString
                            .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = odrTransferCode("Str3").ToString
                            .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = odrTransferCode("Str4").ToString
                            .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = odrTransferCode("Str5").ToString

                            .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = odrTransferCode("Flo1").ToString
                            .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = odrTransferCode("Flo2").ToString
                            .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = odrTransferCode("Flo3").ToString
                            .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = odrTransferCode("Flo4").ToString
                            .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = odrTransferCode("Flo5").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.NVarChar, 50).Value = strNew_Tag_No


                            .Parameters.Add("@Asset_No", SqlDbType.NVarChar, 50).Value = odrTransferCode("Asset_No").ToString
                            .Parameters.Add("@ReserveQty", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Total_Qty").ToString)
                            .Parameters.Add("@ReserveWeight", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Weight").ToString)
                            .Parameters.Add("@ReserveVolume", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Volume").ToString)
                            .Parameters.Add("@ReserveQty_Item", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Item_Qty").ToString)
                            .Parameters.Add("@ReserveOrderItem_Price", SqlDbType.Float, 8).Value = CDec(odrTransferCode("Price").ToString)


                            .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@update_date", SqlDbType.DateTime).Value = Now.ToString("yyyy/MM/dd hh:mm:ss")
                            .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.NVarChar, 13).Value = odrTransferCode("AssetLocationBalance_Index").ToString
                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                    End If

                    DS.Tables("LB").Clear()

                Next
            End If

            '    '*** Commit transaction
            myTrans.Commit()
            myTrans.Dispose()
            myTrans = Nothing

            Return Me._NewTransferOwner_Index

        Catch e As Exception
            Try
                myTrans.Rollback()
                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try

        Return ""

    End Function


    Public Function TranOwner(ByVal pstrTransferOwner_Index As String) As String
        Dim strSQL As String
        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0
        '  _NewTransferCode_Index = objHeader.TransferCode_Index

        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0

        Dim strNew_LocationBalance_Index As String = ""
        Dim strNew_Tag_No As String = ""
        Dim dsTF As New DataSet
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans
        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Modify tb_LocationBalance 
        '             If   Move Qty All ; Update
        '             Else Move Some Qty ; Insert into tb_LocationBalance
        ' --- STEP 2: Update ms_Location 
        ' --- STEP 3: Update tb_TransferCode & tb_TransferCodeLocation
        ' --- STEP 4: Insert tb_Transaction & tb_AssetTransaction OUT
        ' --- STEP 5: Insert tb_Transaction & tb_AssetTransaction IN
        Try
            Dim Order_Index As String = ""
            strSQL = "  SELECT  *"
            strSQL &= "  FROM      VIEW_TransferOwnerLocation_Confirm"
            strSQL &= "   WHERE  TransferOwner_Index ='" & pstrTransferOwner_Index & "' "
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans


            DataAdapter.Fill(dsTF, "TFOC")


            Dim objDBIndex As New Sy_AutoNumber
            Dim objconfig As New config_CustomSetting


            If dsTF.Tables("TFOC").Rows.Count > 0 Then



                '************************************ Start Insert Withdraw  ***********************************************
                '**********************************************************************************************************
                Dim strwithdraw_index As String = objDBIndex.getSys_Value("withdraw_index")
                Dim strDocumentType_index As String = objconfig.getConfig_Key_DEFUALT("DEFUALT_DocType_Owner_withdraw")
                If strDocumentType_index = "" Then
                    myTrans.Rollback()
                    Return "ระบบไม่ได้ตั้งค่ามาตรฐานของ ใบเบิกสินค้า (DEFUALT_DocType_Owner_withdraw) "
                End If

                Dim strOrderDocumentType_index As String = objconfig.getConfig_Key_DEFUALT("DEFUALT_DocType_Owner_Order")
                If strOrderDocumentType_index = "" Then
                    myTrans.Rollback()
                    Return "ระบบไม่ได้ตั้งค่ามาตรฐานของ ใบรับสินค้า (DEFUALT_DocType_Owner_Order) "
                End If

                strSQL = " INSERT INTO tb_Withdraw ( Withdraw_Index,Withdraw_No,Withdraw_Date,Customer_Index,Department_Index,DocumentType_Index, "
                strSQL &= " Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,"
                strSQL &= " Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Contact_Name,Comment, "
                strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_date,add_by,add_branch,"
                strSQL &= " Customer_Shipping_Index,Driver_Index,Round,Leave_Time,Factory_In,Factory_Out,Return_Time,SO_No,Invoice_No,ASN_No,Departure_Date,Arrival_Date,"
                strSQL &= "  Vassel_Name, Flight_No, Vehicle_No, Transport_by, Origin_Port_Id, Origin_Country_Id, Destination_Port_Id, Destination_Country_Id, Terminal_Id,HandlingType_Index,ApprovedBy_Name,Checker_Name,Shipper_Index,Withdraw_Type"
                strSQL &= " ,Status) "
                strSQL &= " Values "
                strSQL &= "  ( @Withdraw_Index,@Withdraw_No,@Withdraw_Date,@Customer_Index,@Department_Index,@DocumentType_Index,"
                strSQL &= " @Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,"
                strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10, @Contact_Name,@Comment,"
                strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,getdate(),@add_by,@add_branch,"
                strSQL &= " @Customer_Shipping_Index,@Driver_Index,@Round,@Leave_Time,@Factory_In,@Factory_Out,@Return_Time,@SO_No,@Invoice_No,@ASN_No,@Departure_Date,@Arrival_Date,"
                strSQL &= " @Vassel_Name, @Flight_No, @Vehicle_No, @Transport_by, @Origin_Port_Id, @Origin_Country_Id, @Destination_Port_Id, @Destination_Country_Id, @Terminal_Id,@HandlingType_Index,@ApprovedBy_Name,@Checker_Name,@Shipper_Index,@Withdraw_Type"
                strSQL &= " ,@Status) "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = strwithdraw_index
                    .Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_No") & "W"
                    .Parameters.Add("@Withdraw_Date", SqlDbType.SmallDateTime, 4).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_Date")
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = dsTF.Tables("TFOC").Rows(0)("Customer_Index")
                    .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = ""
                    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = strDocumentType_index
                    .Parameters.Add("@Contact_Name", SqlDbType.NVarChar, 50).Value = ""
                    .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = ""
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_No")
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID


                    'Add New 19/6/2008
                    .Parameters.Add("@Customer_Shipping_Index", SqlDbType.NVarChar, 13).Value = ""
                    .Parameters.Add("@Driver_Index", SqlDbType.NVarChar, 13).Value = ""
                    .Parameters.Add("@Round", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Leave_Time", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Factory_In", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Factory_Out", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Return_Time", SqlDbType.SmallDateTime, 4).Value = Now


                    '--- AddNew 19/05/2009

                    .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = ""

                    .Parameters.Add("@So_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = ""

                    .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = Now

                    .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 100).Value = ""
                    .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 100).Value = ""
                    .Parameters.Add("@Shipper_Index", SqlDbType.NVarChar, 13).Value = ""
                    .Parameters.Add("@Withdraw_Type", SqlDbType.Bit, 1).Value = 0
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                Dim strwithdrawitem_index As String = ""
                Dim strwithdrawitemLocation_index As String = ""

                For Each drTF As DataRow In dsTF.Tables("TFOC").Rows

                    strwithdrawitem_index = objDBIndex.getSys_Value("withdrawitem_index")
                    strwithdrawitemLocation_index = objDBIndex.getSys_Value("WithdrawItemLocation_Index")

                    strSQL = " INSERT INTO tb_WithdrawItem (WithdrawItem_Index,Withdraw_Index,Sku_Index,Package_Index,PLot,ItemStatus_Index ,Serial_No"
                    strSQL &= " ,Qty,Total_Qty,Plan_Qty,Ratio,Plan_Total_Qty,Weight,Volume,Str1,Str2,Str3,Str4,Str5"
                    strSQL &= " ,Flo1,Flo2,Flo3,Flo4,Flo5,Status,add_by,add_branch"
                    strSQL &= " ,Item_Qty,Price,Item_Package_Index,Declaration_No,Invoice_No,HandlingType_Index,ItemDefinition_Index,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,AssetLocationBalance_Index,DocumentPlan_Index"
                    strSQL &= ",Tax1,Tax2,Tax3,Tax4,Tax5 ,HS_Code,ItemDescription,Seq,Consignee_Index,Str6,Str7,Str8,Str9,Str10,OrderItem_Index,ERP_Location)"
                    strSQL &= " Values "
                    strSQL &= "( @WithdrawItem_Index,@Withdraw_Index,@Sku_Index,@Package_Index,@PLot,@ItemStatus_Index ,@Serial_No"
                    strSQL &= " ,@Qty,@Total_Qty,@Plan_Qty,@Ratio,@Plan_Total_Qty,@Weight,@Volume,@Str1,@Str2,@Str3,@Str4,@Str5"
                    strSQL &= " ,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Status,@add_by,@add_branch,@Item_Qty,@Price,@Item_Package_Index,@Declaration_No,@Invoice_No,@HandlingType_Index,@ItemDefinition_Index,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@AssetLocationBalance_Index,@DocumentPlan_Index"
                    strSQL &= ",@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@Str6,@Str7,@Str8,@Str9,@Str10,@OrderItem_Index,@ERP_Location)"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = strwithdrawitem_index
                        .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = strwithdraw_index
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drTF("Old_Sku_Index")
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = drTF("Plot")
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = drTF("Old_ItemStatus_Index")
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = drTF("Package_Index")
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = ""


                        ' *** Need to set Qty=0  and Total_Qty =0 
                        .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = drTF("Qty")
                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = drTF("Total_Qty")
                        ' ***************************************

                        .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = 0 '_objItem.Plan_Qty
                        .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = drTF("Ratio")
                        .Parameters.Add("@Plan_Total_Qty", SqlDbType.Float, 8).Value = 0 '_objItem.Plan_Total_Qty
                        .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = drTF("Weight")
                        .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = drTF("Volume")
                        .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = drTF("Item_Qty")
                        .Parameters.Add("@Price", SqlDbType.Float, 8).Value = drTF("Price")

                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = ""

                        '--- Plan WithDraw 
                        .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = drTF("Plan_Process")
                        .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = drTF("DocumentPlan_No")
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = drTF("DocumentPlan_Index")
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = drTF("DocumentPlanItem_Index")
                        .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = ""
                        'Dong_kk Tax,Seq,Consignee

                        .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@HS_Code", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 200).Value = ""
                        .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = 0
                        .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = ""

                        '11-02-2010 ja add Str6-Str10
                        .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = ""

                        '16-02-2010 ja
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = drTF("OrderItem_Index")
                        .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = drTF("ERP_Location")
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()




                    strSQL = "Insert into tb_WithdrawItemLocation "
                    strSQL &= " (WithdrawItemLocation_Index,Withdraw_Index,WithdrawItem_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Tag_No,LocationBalance_Index,Location_Index,Serial_No,Qty,Package_Index,Total_Qty,Weight,Volume,Status,add_by,add_date,add_branch,Pallet_Qty,Item_Qty,Price,tagout_no,TAG_Index,ERP_Location) "
                    strSQL &= "  values("
                    strSQL &= "'" & strwithdrawitemLocation_index & "',"
                    strSQL &= "'" & strwithdraw_index & "',"
                    strSQL &= "'" & strwithdrawitem_index & "',"
                    strSQL &= "'" & drTF("Order_Index") & "',"
                    strSQL &= "'" & drTF("Old_Sku_Index") & "',"
                    strSQL &= "'" & drTF("Lot_No") & "',"
                    strSQL &= "'" & drTF("Plot") & "',"
                    strSQL &= "'" & drTF("Old_ItemStatus_Index") & "',"
                    strSQL &= "'" & drTF("Tag_No") & "',"
                    strSQL &= "'" & drTF("From_LocationBalance_Index") & "',"
                    strSQL &= "'" & drTF("From_Location_Index") & "' ,"
                    strSQL &= "'" & "" & "',"
                    strSQL &= "'" & drTF("Qty") & "',"
                    strSQL &= "'" & drTF("Package_Index") & "',"
                    strSQL &= "'" & drTF("Total_Qty") & "',"
                    strSQL &= "'" & drTF("Weight") & "',"
                    strSQL &= "'" & drTF("Volume") & "',"
                    strSQL &= "'-9',"
                    strSQL &= "'" & WV_UserName & "',"
                    strSQL &= "getdate(),"
                    strSQL &= "'" & WV_Branch_ID & "',"
                    strSQL &= "'" & "" & "',"
                    strSQL &= "'" & drTF("Item_Qty") & "',"
                    strSQL &= "'" & drTF("Price") & "',"
                    strSQL &= "'','" & drTF("TAG_Index") & "','" & drTF("ERP_Location") & "')  "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                Next
                '************************************ END Insert Withdraw  ***********************************************
                '**********************************************************************************************************



                '************************************ start Confirm Withdraw  ***********************************************
                '**********************************************************************************************************
                'strwithdraw_index
                strSQL = " SELECT       *"
                strSQL &= " FROM        VIEW_WithDrawSave"
                strSQL &= " WHERE       Withdraw_Index ='" & strwithdraw_index & "' "

                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With

                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans

                DS = New DataSet
                DataAdapter.Fill(DS, "tbl")

                If DS.Tables("tbl").Rows.Count <> 0 Then

                    For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1

                        Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                        Dim TAG_Index As String = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                        Dim Location_Index As String = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString

                        Dim Total_Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                        Dim Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Qty")
                        Dim Weight As Decimal = DS.Tables("tbl").Rows(i).Item("Weight")
                        Dim Volume As Decimal = DS.Tables("tbl").Rows(i).Item("Volume")
                        Dim ItemQty As Decimal = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                        Dim Price As Decimal = DS.Tables("tbl").Rows(i).Item("Price")
                        Dim ERP_Location As String = DS.Tables("tbl").Rows(i).Item("ERP_Location").ToString

                        Dim objPicking As New WMS_STD_OUTB_Datalayer.PICKING(WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Type.CUSTOM)
                        objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN(Connection, myTrans, WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Action.DELBALANCE_RESERVE, 2, strwithdraw_index, "ยืนยันรายการ", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                        Total_Qty, Weight, Volume, ItemQty, Price)
                        objPicking = Nothing


                        strSQL = "UPDATE tb_WithdrawItemLocation "
                        strSQL &= " SET status =-1 "
                        strSQL &= " WHERE Withdraw_Index ='" & strwithdraw_index & "' "
                        strSQL &= "UPDATE tb_WithdrawItem "
                        strSQL &= " SET NewItemFlag=2 "
                        strSQL &= " WHERE Withdraw_Index ='" & strwithdraw_index & "' "

                        With SQLServerCommand
                            .CommandText = strSQL
                            .ExecuteNonQuery()
                        End With

                        strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,Ratio,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "' "  '& " where Tag_No ='" & Tag_No & "' "


                        With SQLServerCommand
                            .Connection = Connection
                            .Transaction = myTrans
                            .CommandText = strSQL
                            .CommandTimeout = 0
                        End With

                        DataAdapter.SelectCommand = SQLServerCommand
                        DataAdapter.SelectCommand.Transaction = myTrans

                        '   DS = New DataSet
                        DataAdapter.Fill(DS, "tbl1")

                        If DS.Tables("tbl1").Rows.Count <> 0 Then
                            ' ***********************
                            Dim objCalBalance As New CalculateBalance
                            objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("LocationBalance_Index").ToString)
                            objCalBalance = Nothing
                            ' ***********************

                            If CDec(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                                ' *** update ms_PalletType *** 

                                strSQL = "UPDATE ms_PalletType "
                                strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                                strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()



                                ' *** Insert Record in tb_PalletType_History ***

                                strSQL = " INSERT INTO tb_PalletType_History  "
                                strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                                strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                                ' Generate PalletType_History_Index 

                                Dim objDBPalletTypeIndex As New Sy_AutoNumber
                                Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index")
                                objDBPalletTypeIndex = Nothing


                                ' *** Call Function Get Balance ***

                                'Dim objPalletTypeBal As New CalculateBalance
                                Dim Qty_PalletType_Bal As Decimal = 0
                                '' *** Qty ***
                                'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                                'objPalletTypeBal = Nothing

                                ' *********************************

                                With SQLServerCommand

                                    .Parameters.Clear()

                                    .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                    .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                    ' *** Important Fix Code for Pallettype Location Default ***
                                    .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                    .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                    .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                    .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                    '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                                ' **********************************************



                                ' *** Insert Record in tb_PalletType_History ***

                                strSQL = " INSERT INTO tb_PalletType_History  "
                                strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                                strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                                ' Generate PalletType_History_Index 

                                Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                                Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
                                objDBPalletTypeIndex2 = Nothing


                                ' *** Call Function Get Balance ***

                                'Dim objPalletTypeBal2 As New CalculateBalance
                                Dim Qty_PalletType_Bal2 As Decimal = 0
                                '' *** Qty ***
                                'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                                'objPalletTypeBal2 = Nothing

                                ' *********************************

                                With SQLServerCommand

                                    .Parameters.Clear()

                                    .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                    .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                    ' *** Important Fix Code for Pallettype Location Default ***
                                    .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                    .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                    .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                    .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                    '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
                                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                                ' **********************************************
                            End If

                        End If

                        ' xxxxxxxxxxxxxxxxxxxx
                        DS.Tables("tbl1").Clear()
                        ' xxxxxxxxxxxxxxxxxxxx


                        ' *********************  End Manage PalletType  ******************************

                        ' *** Call Function Get Balance ***
                        Dim objBal As New CalculateBalance
                        ' *** Qty ***
                        Qty_Sku_Bal = 0 ' objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Qty_PLot_Bal = 0 ' objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Qty_ItemStatus_Bal = 0 ' objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                        ' *** Weight ***
                        Weight_Sku_Bal = 0 ' objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Weight_PLot_Bal = 0 ' objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Weight_ItemStatus_Bal = 0 'objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                        ' *** Volume ***
                        Volume_Sku_Bal = 0 ' objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Volume_PLot_Bal = 0 ' objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Volume_ItemStatus_Bal = 0 'objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                        Qty_Sku_Location_Bal = 0 ' objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Location_Index)
                        Qty_PLot_Location_Bal = 0 'objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, Location_Index)
                        Qty_ItemStatus_Location_Bal = 0 'objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, Location_Index)


                        objBal = Nothing

                        ' *********************************
                        ' *** Insert tb_Transaction ***
                        Dim Product_Index As String = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                        Dim ProductType_Index As String = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                        Order_Index = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                        Dim Order_Date As Date = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date").ToString)
                        Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        Dim strSQL_Qty_Item_Bal As String = 0
                        Dim strSQL_OrderItem_Price_Bal As String = 0

                        strSQL = " INSERT INTO tb_Transaction    "
                        strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_index,DocumentType_Index,ItemDefinition_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,Serial_No,HandlingType_Index"
                        strSQL &= " ,Tax1_Out,Tax2_Out,Tax3_Out,Tax4_Out,Tax5_Out "
                        strSQL &= "  ,Qty_Sku_Location_Bal,Qty_PLot_Location_Bal,Qty_ItemStatus_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index) "
                        strSQL &= "  VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_index,@DocumentType_Index,@ItemDefinition_Index,@Reference1,@Reference2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_Out," & strSQL_Qty_Item_Bal & "-0,@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "-0,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@Serial_No,@HandlingType_Index"
                        strSQL &= " ,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out "
                        strSQL &= "  ,@Qty_Sku_Location_Bal,@Qty_PLot_Location_Bal,@Qty_ItemStatus_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index ) "
                        '
                        strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                        strSQL_Qty_Item_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                        strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                        strSQL_OrderItem_Price_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                        ' **** Manage Balance ***

                        With SQLServerCommand

                            .Parameters.Clear()

                            '  **** Generate OrderItemLocation_Index  ***
                            'Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                            'objDBIndex = Nothing
                            ' *******************************************

                            '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString
                            .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                            .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                            .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                            .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

                            .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                            .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                            .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                            .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                            .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                            .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                            .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                            .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                            .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                            .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString

                            'New 6-8-2008
                            .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                            .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                            ' Dong_Edit
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                            .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = Product_Index
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = ProductType_Index
                            .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = Order_Date.ToString("yyyy/MM/dd")

                            .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                            .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString

                            .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                            .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_Out").ToString
                            .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                            .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                            .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                            .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                            .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString

                            'add new 2011-03-03
                            .Parameters.Add("@Tax1_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax1")
                            .Parameters.Add("@Tax2_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax2")
                            .Parameters.Add("@Tax3_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax3")
                            .Parameters.Add("@Tax4_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax4")
                            .Parameters.Add("@Tax5_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax5")

                            .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                            .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                            .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                            .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index")

                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index")
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index")
                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        connectDB()
                        EXEC_Command()
                        strSQL = Nothing


                        '***************************** ASSET TRANSACTION ********************************

                        Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                        Dim _AssetTransaction_Index As String = ""
                        If _AssetLocationBalance_Index <> "" Then

                            strSQL = " SELECT       * "
                            strSQL &= " FROM       tb_AssetLocationBalance"
                            strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                            SetSQLString = strSQL
                            SetCommandType = enuCommandType.Text
                            SetEXEC_TYPE = EXEC.NonQuery
                            connectDB()
                            EXEC_Command()

                            Dim DSAS As New DataSet
                            DataAdapter.Fill(DSAS, "tblas")

                            If DSAS.Tables("tblas").Rows.Count <> 0 Then

                                For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1
                                    Dim objDBAsIndex As New Sy_AutoNumber
                                    _AssetTransaction_Index = objDBAsIndex.getSys_Value("AssetTransaction_Index")
                                    objDBAsIndex = Nothing
                                    'Insert tb_AssetTransaction
                                    strSQL = "  INSERT INTO tb_AssetTransaction("
                                    strSQL &= " AssetTransaction_Index, AssetLocationBalance_Index, Asset_No, Serial_No, Sku_Index, Order_Index, OrderItem_Index, Description, add_by, add_date, add_branch"
                                    strSQL &= " ,AssetTransaction_Id, Lot_No, From_Location_Index, To_Location_Index, Tag_No, Plot, ItemStatus_Index, Transation_Date, Process_id"
                                    strSQL &= " ,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal, Qty_PLot_Bal, Qty_ItemStatus_Bal, Weight_Sku_Bal, Weight_PLot_Bal, Weight_ItemStatus_Bal, Volume_Sku_Bal, Volume_PLot_Bal, Volume_ItemStatus_Bal,Qty_Item_Out, OrderItem_Price_Out"
                                    strSQL &= " ,ItemDefinition_Index, Customer_Index, DocumentType_Index, Referent_1, Referent_2, Order_Date, ProductType_Index, Product_Index)"
                                    strSQL &= "       VALUES("
                                    strSQL &= " @AssetTransaction_Index,@AssetLocationBalance_Index,@Asset_No,@Serial_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Description,@add_by,getdate(),@add_branch"
                                    strSQL &= " ,@AssetTransaction_Id,@Lot_No,@From_Location_Index,@To_Location_Index,@Tag_No,@Plot,@ItemStatus_Index,@Transation_Date,@Process_id"
                                    strSQL &= " ,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@Qty_Item_Out,@OrderItem_Price_Out"
                                    strSQL &= " ,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent_1,@Referent_2,@Order_Date,@ProductType_Index,@Product_Index)"

                                    With SQLServerCommand.Parameters
                                        .Clear()
                                        '-- from AssetLocation
                                        .Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = _AssetTransaction_Index
                                        .Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _AssetLocationBalance_Index
                                        .Add("@Asset_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Asset_No") ' _Asset_No
                                        .Add("@Serial_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Serial_No") '  _Serial_No
                                        .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("SKU_Index") '  _SKU_Index
                                        .Add("@Order_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("Order_Index") '  _Order_Index
                                        .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("OrderItem_Index") '  _OrderItem_Index
                                        .Add("@Description", SqlDbType.VarChar, 255).Value = ""

                                        .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                        .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                        '--- From WithDrawItem


                                        .Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                                        .Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                                        .Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                        .Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                        .Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                                        .Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                                        .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                                        '.Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                        Dim _Order_date As DateTime = Format(DS.Tables("tbl").Rows(i).Item("Order_date"), "dd/MM/yyyy").ToString()

                                        .Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                                        ' Process_id 
                                        .Add("@Process_id", SqlDbType.Float, 8).Value = 2

                                        .Add("@Qty_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Qty")) ' _Qty_In
                                        .Add("@Weight_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Weight"))
                                        .Add("@Volume_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Volume"))

                                        .Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                                        .Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                                        .Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                                        .Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                                        .Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                                        .Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                                        .Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                                        .Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                                        .Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal
                                        .Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                                        .Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")

                                        .Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                                        .Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                                        .Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                                        .Add("@Referent_1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                                        .Add("@Referent_2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                                        .Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = _Order_date
                                        .Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                                        .Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString




                                    End With
                                    SetSQLString = strSQL
                                    SetCommandType = enuCommandType.Text
                                    SetEXEC_TYPE = EXEC.NonQuery
                                    connectDB()
                                    EXEC_Command()


                                    strSQL = "  UPDATE tb_AssetLocationBalance    "
                                    strSQL &= "  SET Qty_Bal=Qty_Bal-@Total_Qty "
                                    strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                                    strSQL &= " ,Status = -2 "
                                    strSQL &= "  WHERE AssetLocationBalance_Index=@AssetLocationBalance_Index "
                                    With SQLServerCommand
                                        .Parameters.Clear()
                                        .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                    End With

                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()

                                Next
                            End If

                        End If

                        ' *********************************




                        ' *** Manage ms_location ***

                        strSQL = "UPDATE ms_Location "
                        strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                        strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        ' *** If Area of Location Emtry ***
                        strSQL = "UPDATE ms_Location "
                        strSQL &= " SET Space_Used =0 "
                        strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                        ' *********************************


                        ' *** End Manage ms_location ***

                        ' *** Update tb_withdraw & tb_jobWithdraw ***
                        strSQL = "UPDATE tb_Withdraw "
                        strSQL &= " SET status =2 "
                        strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        strSQL = "UPDATE tb_JobWithdraw "
                        strSQL &= " SET status =2 "
                        strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        ' *** Update tb_SalesOrder & tb_SalesOrderItem ***

                        Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString

                        If Plan_Process <> -9 Then
                            Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                            Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                            StatusWithdraw_Document = Plan_Process
                            Select Case StatusWithdraw_Document
                                Case Withdraw_Document.SO
                                    'krit update 28/12/2009 for mm
                                    'strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                                    'strSQL &= " FROM tb_SalesOrderItem  "
                                    'strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                                    '                              

                                    strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                    strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                    strSQL &= " where Plan_Process=10 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                    strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                    strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                    SQLServerCommand.Parameters.Clear()
                                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                    With SQLServerCommand
                                        .Connection = Connection
                                        .Transaction = myTrans
                                        .CommandText = strSQL
                                        .CommandTimeout = 0
                                    End With

                                    DataAdapter.SelectCommand = SQLServerCommand
                                    DataAdapter.SelectCommand.Transaction = myTrans
                                    '   DS = New DataSet
                                    DataAdapter.Fill(DS, "SO")

                                    If DS.Tables("SO").Rows.Count > 0 Then

                                        If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                            ' --- STEP 2: Update status in tb_SaleOrder = 3
                                            strSQL = "UPDATE tb_SalesOrder "
                                            strSQL &= " SET status =3 "
                                            strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                            SetSQLString = strSQL
                                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                            EXEC_Command()
                                            ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                            strSQL = "UPDATE tb_SalesOrderItem "
                                            strSQL &= " SET status =3 "
                                            strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                            SetSQLString = strSQL
                                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                            EXEC_Command()
                                        End If

                                    End If

                                Case Withdraw_Document.Packing
                                    strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                    strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                    strSQL &= " where Plan_Process=7 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                    strSQL &= " ,SUM(Qty) as Total_Qty "
                                    strSQL &= " FROM tb_PackingItem WHERE Packing_Index =@PlanDocument_Index "



                                    SQLServerCommand.Parameters.Clear()
                                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                    With SQLServerCommand
                                        .Connection = Connection
                                        .Transaction = myTrans
                                        .CommandText = strSQL
                                        .CommandTimeout = 0
                                    End With

                                    DataAdapter.SelectCommand = SQLServerCommand
                                    DataAdapter.SelectCommand.Transaction = myTrans
                                    '   DS = New DataSet
                                    DataAdapter.Fill(DS, "SO")

                                    If DS.Tables("SO").Rows.Count > 0 Then

                                        If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                            ' --- STEP 2: Update status in tb_SaleOrder = 3
                                            '****************  Case ผลิต    ****************  
                                            strSQL = "UPDATE tb_Packing "
                                            strSQL &= " SET status =5 "
                                            strSQL &= " WHERE Packing_Index ='" & DocumentPlan_Index & "'"

                                            SetSQLString = strSQL
                                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                            EXEC_Command()
                                        End If

                                    End If


                                    '15-01-2010 ja update Status Reserve 
                                Case Withdraw_Document.Reserve
                                    strSQL = " select Reserve_Index from tb_Reserve where Reserve_Index = @Reserve_Index  "
                                    SQLServerCommand.Parameters.Clear()
                                    SQLServerCommand.Parameters.Add("@Reserve_Index", SqlDbType.NVarChar, 13).Value = DocumentPlan_Index

                                    With SQLServerCommand
                                        .Connection = Connection
                                        .Transaction = myTrans
                                        .CommandText = strSQL
                                        .CommandTimeout = 0
                                    End With
                                    DataAdapter.SelectCommand = SQLServerCommand
                                    DataAdapter.SelectCommand.Transaction = myTrans
                                    '   DS = New DataSet
                                    DataAdapter.Fill(DS, "Reserve")

                                    If DS.Tables("Reserve").Rows.Count > 0 Then
                                        ' --- STEP 2: Update status in tb_Reserve = 3
                                        strSQL = "UPDATE tb_Reserve "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE Reserve_Index ='" & DS.Tables("Reserve").Rows(0).Item("Reserve_Index").ToString & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If


                                Case Withdraw_Document.Transport 'killz update 08-08-2011

                                    strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                    strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                    strSQL &= " where Plan_Process=25 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                    strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                    strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                    SQLServerCommand.Parameters.Clear()
                                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                    With SQLServerCommand
                                        .Connection = Connection
                                        .Transaction = myTrans
                                        .CommandText = strSQL
                                        .CommandTimeout = 0
                                    End With

                                    DataAdapter.SelectCommand = SQLServerCommand
                                    DataAdapter.SelectCommand.Transaction = myTrans
                                    '   DS = New DataSet
                                    DataAdapter.Fill(DS, "SO")

                                    If DS.Tables("SO").Rows.Count > 0 Then

                                        If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                            ' --- STEP 2: Update status in tb_SaleOrder = 3
                                            strSQL = "UPDATE tb_SalesOrder "
                                            strSQL &= " SET status =3 "
                                            strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                            SetSQLString = strSQL
                                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                            EXEC_Command()
                                            ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                            strSQL = "UPDATE tb_SalesOrderItem "
                                            strSQL &= " SET status =3 "
                                            strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                            SetSQLString = strSQL
                                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                            EXEC_Command()
                                        End If

                                    End If
                            End Select
                        End If
                    Next

                Else
                    'error

                End If

                '**********************************************************************************************************
                '************************************ END Confirm Withdraw  ***********************************************
                '**********************************************************************************************************



                '**********************************************************************************************************
                '************************************   Start Insert Order  ***********************************************
                '**********************************************************************************************************

                Dim strOrder_index As String = objDBIndex.getSys_Value("Order_index")
                'Dim strOrderDocumentType_index As String = objconfig.getConfig_Key_DEFUALT("DEFUALT_DocType_Owner_Order")
                Dim strOrder_Time As String = Now.Hour.ToString & ":" & Now.Minute.ToString


                ' --- STEP 1: Insert Header : tb_order 
                strSQL = " INSERT INTO tb_Order ( Order_Index,Order_No,Order_Date,Order_Time,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5, "
                strSQL &= " Lot_No,Customer_Index,Supplier_Index,Department_Index,DocumentType_Index,Status,Comment,"
                strSQL &= " Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,  "
                strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,PO_No,Invoice_No,ASN_No,Checker_Name,ApprovedBy_Name,"
                strSQL &= " HandlingType_Index,Vassel_Name,Flight_No,Vehicle_No,Transport_by,Origin_Port_Id,Origin_Country_Id,Destination_Port_Id,Destination_Country_Id,Terminal_Id,Receive_Type,Consignee_Index"
                strSQL &= " ,Departure_Date,Arrival_Date) "
                strSQL &= " Values "
                strSQL &= "  ( @Order_Index,@Order_No,@Order_Date,@Order_Time,@Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5, "
                strSQL &= " @Lot_No,@Customer_Index,@Supplier_Index,@Department_Index,@DocumentType_Index,@Status,@Comment,"
                strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10, "
                strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@PO_No,@Invoice_No,@ASN_No,@Checker_Name,@ApprovedBy_Name,"
                strSQL &= " @HandlingType_Index,@Vassel_Name,@Flight_No,@Vehicle_No,@Transport_by,@Origin_Port_Id,@Origin_Country_Id,@Destination_Port_Id,@Destination_Country_Id,@Terminal_Id,@Receive_Type,@Consignee_Index"
                'include from master site
                strSQL &= "  ,@Departure_Date,@Arrival_Date)"
                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = strOrder_index
                    .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_No") & "R"
                    .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_Date")
                    .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = strOrder_Time
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = dsTF.Tables("TFOC").Rows(0)("TransferOwner_No")
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = dsTF.Tables("TFOC").Rows(0)("Lot_No")
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = dsTF.Tables("TFOC").Rows(0)("New_Customer_Index")
                    .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = ""
                    .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = ""
                    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = strOrderDocumentType_index
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = ""
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = ""
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = ""
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = ""

                    'include from master site
                    .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = Now
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = ""
                    .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Receive_Type", SqlDbType.Int, 4).Value = 0
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = ""

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                Dim strOrderitem_index As String = ""
                Dim strtag_index As String = ""
                Dim strOrderitemlocation_index As String = ""
                Dim strtag_No As String = ""

                For Each drOrder As DataRow In dsTF.Tables("TFOC").Rows

                    strOrderitem_index = objDBIndex.getSys_Value("Orderitem_index")

                    strSQL = " INSERT INTO tb_OrderItem ( OrderItem_Index,Order_Index,Sku_Index, "
                    strSQL &= " ItemStatus_Index,Package_Index,Ratio,Qty,Total_Qty,Plan_Qty,PalletType_Index,"
                    strSQL &= " Pallet_Qty,Weight,Volume,IsMfg_Date,Mfg_Date,IsExp_date,Exp_date, "
                    strSQL &= " Status,Comment,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,ItemDefinition_Index,"
                    strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Is_SN,"
                    strSQL &= " Lot_No,Plot,Serial_No,PO_No,Invoice_No,ASN_No,Declaration_No,"
                    'strSQL &= " Item_Qty,Qty_Per_Pck,Weight_Per_Pck,Price_Per_Pck,Volume_Per_Pck,OrderItem_Price,Item_Package_Index,HandlingType_Index"
                    'include from master site
                    strSQL &= " Plan_Process, DocumentPlan_No, DocumentPlanItem_Index, DocumentPlan_Index,"
                    strSQL &= " Item_Qty,Qty_Per_Pck,Weight_Per_Pck,Price_Per_Pck,Volume_Per_Pck,OrderItem_Price,Item_Package_Index,HandlingType_Index,Tax1,Tax2,Tax3,Tax4,Tax5"
                    strSQL &= " ,HS_Code,ItemDescription,Seq,Consignee_Index,ERP_Location"
                    strSQL &= " ) "
                    strSQL &= " Values "
                    strSQL &= "( @OrderItem_Index,@Order_Index,@Sku_Index, "
                    strSQL &= " @ItemStatus_Index,@Package_Index,@Ratio,@Qty,@Total_Qty,@Plan_Qty,@PalletType_Index,"
                    strSQL &= " @Pallet_Qty,@Weight,@Volume,@IsMfg_Date,@Mfg_Date,@IsExp_date,@Exp_date, "
                    strSQL &= " @Status,@Comment,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@ItemDefinition_Index, "
                    strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Is_SN,"
                    strSQL &= " @Lot_No,@Plot,@Serial_No,@PO_No,@Invoice_No,@ASN_No,@Declaration_No,"
                    'strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index"
                    'include from master site
                    strSQL &= " @Plan_Process, @DocumentPlan_No, @DocumentPlanItem_Index, @DocumentPlan_Index,"
                    strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index,@Tax1,@Tax2,@Tax3,@Tax4,@Tax5"
                    strSQL &= " ,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@ERP_Location"
                    strSQL &= " ) "


                    With SQLServerCommand

                        .Parameters.Clear()

                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = strOrderitem_index
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = strOrder_index
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = drOrder("New_ItemStatus_Index")
                        .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = drOrder("IsMfg_Date")
                        .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = drOrder("MfgDate")
                        .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = drOrder("IsExp_Date")
                        .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = drOrder("ExpDate")
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = ""
                        .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str2", SqlDbType.NVarChar, 4000).Value = ""
                        .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                        .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = ""
                        .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = ""
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        .Parameters.Add("@Is_SN", SqlDbType.Bit, 1).Value = 0

                        '--- PCK
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drOrder("New_Sku_Index")
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = drOrder("Package_Index_New")
                        .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = 0
                        .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = drOrder("Ratio")
                        .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = drOrder("Qty")
                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = drOrder("Total_Qty")
                        .Parameters.Add("@Item_Qty", SqlDbType.Float, 15).Value = drOrder("Item_Qty")
                        .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = drOrder("Weight")
                        .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = drOrder("Volume")
                        .Parameters.Add("@OrderItem_Price", SqlDbType.Float, 8).Value = drOrder("Price")
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = drOrder("Package_Index")
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = ""

                        '--- Document 
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = drOrder("Lot_No")
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = drOrder("new_Plot")
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 50).Value = ""
                        '--- Per_PCK 
                        .Parameters.Add("@Qty_Per_Pck", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Weight_Per_Pck", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Volume_Per_Pck", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Price_Per_Pck", SqlDbType.Float, 8).Value = 0

                        'tax include from master site
                        .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = 0
                        .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = 0

                        ' include from master site
                        .Parameters.Add("@HS_Code", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 200).Value = ""
                        .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = 0

                        'add new 11-01-2010 Consignee_Index 
                        .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = ""

                        .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = 50
                        .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = drOrder("TransferOwner_No")
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = drOrder("TransferOwner_Index")
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = drOrder("TransferOwnerLocation_Index")
                        .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = drOrder("ERP_Location")


                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    '**********************************************************************************************************
                    '*************************************   END Insert Order  ************************************************
                    '**********************************************************************************************************



                    '**********************************************************************************************************
                    '*************************************   Start Gen Tag  ***************************************************
                    '**********************************************************************************************************
                   

                    strtag_index = objDBIndex.getSys_Value("Tag_index")
                    strtag_No = strtag_index
                    'strtag_No = objDBIndex.getSys_Value("TAG_NO")
                    strOrderitemlocation_index = objDBIndex.getSys_Value("OrderItemLocation_Index")

                    strSQL = " INSERT INTO tb_Tag(TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,add_by,add_branch,ERP_Location)" & _
                    "    VALUES(@TAG_Index,@TAG_No,@LinkOrderFlag,@Order_No,@Order_Index,@Order_Date,@Order_Time,@OrderItem_Index,@OrderItemLocation_Index,@Customer_Index,@Supplier_Index,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@Ref_No1,@Ref_No2,@Ref_No3,@add_by,@add_branch,@ERP_Location)"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 13).Value = strtag_index
                        .Parameters.Add("@TAG_No", SqlDbType.VarChar, 13).Value = strtag_No
                        .Parameters.Add("@LinkOrderFlag", SqlDbType.Bit, 1).Value = 0
                        .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = drOrder("TransferOwner_No") & "R"
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = strOrder_index
                        .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 16).Value = drOrder("TransferOwner_Date")
                        .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = strOrder_Time
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = strOrderitem_index
                        .Parameters.Add("@OrderItemLocation_Index", SqlDbType.VarChar, 13).Value = strOrderitemlocation_index
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = drOrder("New_Customer_Index")
                        .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Sku_Index", SqlDbType.Char, 13).Value = drOrder("New_Sku_Index")
                        .Parameters.Add("@PLot", SqlDbType.NVarChar, 50).Value = drOrder("new_Plot")
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = drOrder("New_ItemStatus_Index")
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drOrder("Package_Index_New")
                        .Parameters.Add("@Unit_Weight", SqlDbType.Float, 15).Value = 0
                        .Parameters.Add("@Size_Index", SqlDbType.VarChar, 13).Value = "-1"
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Qty", SqlDbType.Float, 15).Value = drOrder("Qty")
                        .Parameters.Add("@Weight", SqlDbType.Float, 15).Value = drOrder("Weight")
                        .Parameters.Add("@Volume", SqlDbType.Float, 15).Value = drOrder("Volume")
                        .Parameters.Add("@Qty_per_TAG", SqlDbType.Float, 15).Value = drOrder("Total_Qty")
                        .Parameters.Add("@Weight_per_TAG", SqlDbType.Float, 15).Value = drOrder("Weight")
                        .Parameters.Add("@Volume_per_TAG", SqlDbType.Float, 15).Value = drOrder("Volume")
                        .Parameters.Add("@TAG_Status", SqlDbType.Int, 10).Value = 1 '_objItem._TAG_Status
                        .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = "1/1"
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                        .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = drOrder("ERP_Location")
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    strSQL = " INSERT INTO tb_OrderItemLocation (OrderItemLocation_Index, OrderItem_Index,Order_Index,Sku_Index,Lot_No, "
                    strSQL &= " Location_Index,Tag_No,PLot,ItemStatus_Index,Package_Index,Ratio,Qty,Total_Qty,"
                    strSQL &= " Weight,Volume,Serial_No,PalletType_Index,Pallet_Qty, JobOrder_Index,"
                    strSQL &= " MixPallet,Qty_Item,Status,OrderItem_Price,"
                    strSQL &= " add_by,add_branch,TAG_Index,ERP_Location"
                    strSQL &= " ) "
                    strSQL &= " Values "
                    strSQL &= "( @OrderItemLocation_Index,@OrderItem_Index,@Order_Index,@Sku_Index,@Lot_No, "
                    strSQL &= " @Location_Index,@Tag_No,@PLot,@ItemStatus_Index,@Package_Index,@Ratio,@Qty,@Total_Qty,"
                    strSQL &= " @Weight,@Volume,@Serial_No,@PalletType_Index,@Pallet_Qty,@JobOrder_Index, "
                    strSQL &= " @MixPallet,@Qty_Item,@Status,@OrderItem_Price,"
                    strSQL &= " @add_by,@add_branch,@TAG_Index,@ERP_Location"
                    strSQL &= " ) "

                    
                    With SQLServerCommand

                        .Parameters.Clear()
                        .Parameters.Add("@OrderItemLocation_Index", SqlDbType.VarChar, 13).Value = strOrderitemlocation_index
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = strOrderitem_index
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = strOrder_index
                        .Parameters.Add("@JobOrder_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drOrder("New_Sku_Index")
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = drOrder("Lot_No")
                        .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = drOrder("To_Location_Index")
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strtag_No
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = drOrder("new_Plot")
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = drOrder("New_ItemStatus_Index")
                        .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = ""
                        .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = 0
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drOrder("Package_Index_New")
                        .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = drOrder("Ratio")
                        .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = drOrder("Qty")
                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = drOrder("Total_Qty")
                        .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = drOrder("Weight")
                        .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = drOrder("Volume")
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = ""
                        'New 23/7/2008 11.56
                        .Parameters.Add("@MixPallet", SqlDbType.Bit, 1).Value = 0

                        .Parameters.Add("@Qty_Item", SqlDbType.Float, 8).Value = 0
                        'New 22/11/2008 11.20
                        .Parameters.Add("@OrderItem_Price", SqlDbType.Float, 8).Value = drOrder("Price")

                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 13).Value = strtag_index
                        .Parameters.Add("@ERP_location", SqlDbType.NVarChar, 100).Value = drOrder("ERP_Location")
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    '**********************************************************************************************************
                    '*************************************   End Gen Tag  ***************************************************
                    '**********************************************************************************************************
                Next





                '**********************************************************************************************************
                '*************************************   Start PutAway  ***************************************************
                '**********************************************************************************************************
                Order_Index = strOrder_index
                strSQL = "     SELECT   *"
                strSQL &= "     FROM    VIEW_OrderLocationSave"
                strSQL &= "     WHERE   Order_Index='" & strOrder_index & "'"

                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With

                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans

                DS = New DataSet
                DataAdapter.Fill(DS, "tbl")

                If DS.Tables("tbl").Rows.Count <> 0 Then
                    Dim _LocationBalance_Index As String = ""
                    For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1
                        Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        Dim Order_No As String = DS.Tables("tbl").Rows(i).Item("Order_No").ToString

                        'strSQL = "UPDATE tb_Order "
                        'strSQL &= " SET status =2 "
                        'strSQL &= " WHERE Order_Index ='" & DS.Tables("tbl").Rows(i).Item("Order_Index").ToString & "' "

                        'SetSQLString = strSQL
                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        'EXEC_Command()

                        Dim oAudit_Log As New Sy_Audit_Log
                        oAudit_Log.Document_Index = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                        oAudit_Log.Document_No = Order_No
                        oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_Receiving_Order)


                        strSQL = "UPDATE tb_OrderItemLocation "
                        strSQL &= " SET status =2 "
                        strSQL &= " WHERE Order_Index ='" & DS.Tables("tbl").Rows(i).Item("Order_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        ' PalletType_Index
                        ' Pallet_Qty
                        strSQL = " INSERT INTO tb_LocationBalance    "
                        strSQL += " (LocationBalance_Index,Location_Index,Tag_No,Sku_Index,Order_Index,OrderItem_Index,Lot_No,PLot,ItemStatus_Index,Serial_No,PalletType_Index,Pallet_Qty,Ratio,Qty_Bal,Weight_Bal,Volume_Bal,Location_Index_Begin,Qty_Bal_Begin,Weight_Bal_Begin,Volume_Bal_Begin,Mfg_Date,Exp_Date,IsMfg_Date,IsExp_Date,Package_Index,Qty_Recieve_Package,add_by,add_branch,MixPallet,Qty_Item_Begin,Qty_Item_Bal,Item_Package_Index,OrderItem_Price_Begin,OrderItem_Price_Bal,TAG_Index,ERP_Location) VALUES "
                        strSQL += " (@LocationBalance_Index,@Location_Index,@Tag_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Lot_No,@PLot,@ItemStatus_Index,@Serial_No,@PalletType_Index,@Pallet_Qty,@Ratio,@Qty_Bal,@Weight_Bal,@Volume_Bal,@Location_Index_Begin,@Qty_Bal_Begin,@Weight_Bal_Begin,@Volume_Bal_Begin,@Mfg_Date,@Exp_Date,@IsMfg_Date,@IsExp_Date,@Package_Index,@Qty_Recieve_Package,@add_by,@add_branch,@MixPallet,@Qty_Item_Begin,@Qty_Item_Bal,@Item_Package_Index,@OrderItem_Price_Begin,@OrderItem_Price_Bal,@TAG_Index,@ERP_Location) "

                        With SQLServerCommand

                            .Parameters.Clear()

                            '  **** Generate OrderItemLocation_Index  ***
                            'Dim objDBIndex As New Sy_AutoNumber

                            _LocationBalance_Index = objDBIndex.getSys_Value("LocationBalance_Index")
                            .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = _LocationBalance_Index
                            'objDBIndex = Nothing
                            ' *******************************************
                            .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                            .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@Location_Index_Begin", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                            .Parameters.Add("@Qty_Bal_Begin", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                            .Parameters.Add("@Weight_Bal_Begin", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                            .Parameters.Add("@Volume_Bal_Begin", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                            .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Ratio")
                            .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                            .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                            .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                            .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString
                            .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = DS.Tables("tbl").Rows(i).Item("Pallet_Qty")
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                            .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = DS.Tables("tbl").Rows(i).Item("IsExp_Date")
                            .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = DS.Tables("tbl").Rows(i).Item("IsMfg_Date")
                            .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Exp_Date")
                            .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Mfg_Date")
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            .Parameters.Add("@MixPallet", SqlDbType.Bit, 4).Value = DS.Tables("tbl").Rows(i).Item("MixPallet")

                            .Parameters.Add("@Qty_Item_Begin", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty_Item")
                            .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty_Item")


                            ' *** Package_Index,Qty_Recieve_Package,Qty_Faction  ***
                            .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Package_Index").ToString
                            .Parameters.Add("@Qty_Recieve_Package", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty")

                            .Parameters.Add("@OrderItem_Price_Begin", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Price")
                            .Parameters.Add("@OrderItem_Price_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Price")
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString
                            .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("ERP_Location").ToString

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        '--------------------------------------------------------------------------------


                        ''UPDATE tb_AssetLocationBalance 
                        strSQL = " UPDATE tb_AssetLocationBalance "
                        strSQL &= " SET "
                        strSQL &= "  LocationBalance_Index=@LocationBalance_Index,Location_Index=@Location_Index,Tag_No=@Tag_No,ItemStatus_Index=@ItemStatus_Index"
                        strSQL &= " ,Package_Index=@Package_Index,Qty_Bal=@Qty_Bal,Weight_Bal=@Weight_Bal,Volume_Bal=@Volume_Bal,IsExp_Date=@IsExp_Date,IsMfg_Date=@IsMfg_Date"
                        strSQL &= " ,Mfg_Date=@Mfg_Date,Exp_Date=@Exp_Date,PLot=@PLot"
                        strSQL &= " WHERE Order_Index=@Order_Index AND OrderItem_Index = @OrderItem_Index"

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString
                            .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = _LocationBalance_Index
                            .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString

                            .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Ratio")
                            .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = 1

                            Dim _Weight_Bal As Integer = 0
                            Dim _Volume_Bal As Integer = 0
                            _Weight_Bal = (DS.Tables("tbl").Rows(i).Item("Weight") / 1)
                            If _Weight_Bal > 0 Then
                                .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = _Weight_Bal
                            Else
                                .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = 0
                            End If
                            If _Volume_Bal > 0 Then
                                .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = _Volume_Bal
                            Else
                                .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = 0
                            End If

                            .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = DS.Tables("tbl").Rows(i).Item("IsExp_Date")
                            .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = DS.Tables("tbl").Rows(i).Item("IsMfg_Date")
                            .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Exp_Date")
                            .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Mfg_Date")
                            .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Package_Index").ToString

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()



                        '---------------------------------------------------------------------------

                        ' *** Return Value to ms_PalletType ***
                        strSQL = " UPDATE ms_PalletType "
                        strSQL &= "SET  Pallet_Remain=Pallet_Remain-" & DS.Tables("tbl").Rows(i).Item("Pallet_Qty") & " "
                        strSQL &= " WHERE  PalletType_Index ='" & DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString & "'"

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()


                        ' *** Insert Record in tb_PalletType_History ***
                        strSQL = " INSERT INTO tb_PalletType_History  "
                        strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out, Qty_Bal,add_by,add_branch) Values  "
                        strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out) ,@add_by,@add_branch) "


                        ' Generate PalletType_History_Index 

                        Dim objDBPalletTypeIndex As New Sy_AutoNumber
                        Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index ")
                        objDBPalletTypeIndex = Nothing

                        Dim Qty_PalletType_Bal As Decimal = 0

                        With SQLServerCommand

                            .Parameters.Clear()

                            .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                            .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString
                            .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                            ' *** Important Fix Code for Pallettype Location Default ***
                            .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                            .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                            .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                            .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Pallet_Qty")
                            '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        ' *** Return Value to ms_PalletType ***
                        strSQL = " UPDATE ms_PalletType "
                        strSQL &= "SET  Pallet_Remain=Pallet_Remain+" & DS.Tables("tbl").Rows(i).Item("Pallet_Qty") & " "
                        strSQL &= " WHERE  PalletType_Index ='" & DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString & "'"

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()


                        ' *** Insert Record in tb_PalletType_History ***
                        strSQL = " INSERT INTO tb_PalletType_History  "
                        strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                        strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                        ' Generate PalletType_History_Index 

                        Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                        Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
                        objDBPalletTypeIndex2 = Nothing


                        ' *** Call Function Get Balance ***

                        'Dim objPalletTypeBal2 As New CalculateBalance
                        'Dim Qty_PalletType_Bal2 As decimal = 0
                        '' *** Qty ***
                        'Qty_PalletType_Bal = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString)
                        'objPalletTypeBal2 = Nothing

                        ' *********************************

                        With SQLServerCommand

                            .Parameters.Clear()

                            .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                            .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("PalletType_Index").ToString
                            .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                            ' *** Important Fix Code for Pallettype Location Default ***
                            .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                            .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                            .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                            .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Pallet_Qty")
                            '   .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        'update tb_tag
                        strSQL = "UPDATE tb_Tag "
                        strSQL &= " SET TAG_Status =2 "
                        strSQL &= " , OrderItemLocation_Index = '" & DS.Tables("tbl").Rows(i).Item("OrderItemLocation_Index").ToString & "' "
                        strSQL &= " WHERE TAG_No ='" & DS.Tables("tbl").Rows(i).Item("Tag_No").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()



                        ' *** Insert tb_Transaction ***
                        Dim Product_Index As String = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                        Dim ProductType_Index As String = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                        Dim Order_Date As String = DS.Tables("tbl").Rows(i).Item("Order_Date").ToString
                        Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        'strSQL = " INSERT INTO tb_Transaction    "
                        'strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_In,Weight_In,Volume_In,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,ItemDefinition_Index,Customer_Index,DocumentType_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_In,Qty_Item_Bal,OrderItem_Price_In,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,Serial_No,HandlingType_Index) VALUES "
                        'strSQL &= " (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_In,@Weight_In,@Volume_In,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent1,@Referent2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_In,@Qty_Item_Bal,@OrderItem_Price_In,0,@OrderItem_Price_Bal,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@Serial_No,@HandlingType_Index)"

                        'Include from master site
                        strSQL = " INSERT INTO tb_Transaction    "
                        strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_In,Weight_In,Volume_In,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,ItemDefinition_Index,Customer_Index,DocumentType_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_In,Qty_Item_Bal,OrderItem_Price_In,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,Serial_No,HandlingType_Index"
                        strSQL &= " ,Tax1_In,Tax2_In,Tax3_In,Tax4_In,Tax5_In "
                        strSQL &= " ,Qty_Sku_Location_Bal,Qty_PLot_Location_Bal,Qty_ItemStatus_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index )"
                        strSQL &= " VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_In,@Weight_In,@Volume_In,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent1,@Referent2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_In,@Qty_Item_Bal,@OrderItem_Price_In,0,@OrderItem_Price_Bal,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@Serial_No,@HandlingType_Index"
                        strSQL &= " ,@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In"
                        strSQL &= " ,@Qty_Sku_Location_Bal,@Qty_PLot_Location_Bal,@Qty_ItemStatus_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index )"
                        ' **** Manage Balance ***

                        ' *** Call Function Get Balance ***
                        Dim objBal As New CalculateBalance
                        ' *** Qty ***
                        Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                        ' *** Weight ***
                        Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                        ' *** Volume ***
                        Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                        Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                        Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                        Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)
                        Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)
                        Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)

                        objBal = Nothing


                        ' *********************************

                        With SQLServerCommand

                            .Parameters.Clear()

                            '  **** Generate OrderItemLocation_Index  ***
                            'Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                            'objDBIndex = Nothing
                            ' *******************************************

                            .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_No").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                            .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                            .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                            .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            ' Order_Date
                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date")).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 1


                            .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                            .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                            .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                            .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                            .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                            .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                            .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                            .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                            .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                            .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                            '.Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "" ' DS.Tables("tbl").Rows(i).Item("OrderItemLocation_Index").ToString

                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString

                            .Parameters.Add("@Referent1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Referent1").ToString
                            .Parameters.Add("@Referent2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Referent2").ToString

                            ' Dong_kk Edit
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                            .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = Product_Index
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = ProductType_Index
                            .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = Order_Date
                            'Pui
                            .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty_Item").ToString
                            .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty_Item").ToString
                            '22/11/08  15.24
                            .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Price").ToString
                            .Parameters.Add("@OrderItem_Price_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Price").ToString
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString
                            'Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No
                            .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                            .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = ""
                            .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                            .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = ""
                            .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                            .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                            .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString
                            ',@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out,@Tax1_Bal,@Tax2_Bal,@Tax3_Bal,@Tax4_Bal,@Tax5_Bal)"
                            'include from master site
                            .Parameters.Add("@Tax1_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax1").ToString
                            .Parameters.Add("@Tax2_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax2").ToString
                            .Parameters.Add("@Tax3_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax3").ToString
                            .Parameters.Add("@Tax4_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax4").ToString
                            .Parameters.Add("@Tax5_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax5").ToString

                            .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                            .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                            .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal

                            .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        connectDB()
                        EXEC_Command()
                        strSQL = Nothing

     
                    Next

                    Dim odtSumLocation As New DataTable
                    Dim odtSumOrder As New DataTable
                    strSQL = "         SELECT  CAST(ROUND((SUM(Qty_Per_Tag)),4) AS Numeric(18,2)) as SumTag"
                    strSQL &= "         FROM    tb_TAG"
                    strSQL &= "         WHERE   Order_Index = '" & Order_Index & "' And TAG_Status =2 "
                    With SQLServerCommand
                        .CommandText = strSQL
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    DataAdapter.Fill(odtSumLocation)

                    strSQL = " SELECT  CAST(ROUND((SUM(Qty)),4) AS Numeric(18,2)) as SumOI"
                    strSQL &= " from tb_OrderItem"
                    strSQL &= " WHERE Order_Index = '" & Order_Index & "' And Status <> -1 "

                    With SQLServerCommand
                        .CommandText = strSQL
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    DataAdapter.Fill(odtSumOrder)
                    If odtSumLocation.Rows(0)("SumTag") = odtSumOrder.Rows(0)("SumOI") Then
                        strSQL = "UPDATE tb_Order SET"
                        strSQL &= " Status = 2,Update_Date=getdate(),Update_by='" & WV_UserName & "',update_branch=" & WV_Branch_ID
                        strSQL &= " WHERE Order_Index = '" & Order_Index & "'"
                    Else
                        strSQL = " UPDATE tb_Order SET"
                        strSQL &= " Status = 4,Update_Date=getdate(),Update_by='" & WV_UserName & "',update_branch=" & WV_Branch_ID
                        strSQL &= " WHERE Order_Index = '" & Order_Index & "'"
                    End If

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                    '-----------------------------------------------------------------------------------------

                End If

                '**********************************************************************************************************
                '**************************************   END PutAway  ****************************************************
                '**********************************************************************************************************

                strSQL = " UPDATE tb_TransferOwner SET"
                strSQL &= " Status = 2 "
                strSQL &= " WHERE TransferOwner_Index = '" & pstrTransferOwner_Index & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                strSQL = " UPDATE tb_TransferOwnerLocation SET"
                strSQL &= " Status = 2 "
                strSQL &= " WHERE TransferOwner_Index = '" & pstrTransferOwner_Index & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


            End If
            '    '*** Commit transaction
            myTrans.Commit()
            myTrans.Dispose()
            myTrans = Nothing

            Return "TRUE"

        Catch e As Exception
            Try
                
                myTrans.Rollback()
                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try

        Return ""

    End Function


#End Region

#Region " INSERT/UPDATE TRANSFER STATUS "

    ''' <summary>
    ''' Insert tb_TransferCode and update tb_TransferCodeLocation
    ''' </summary>
    ''' <param name="objHeader"></param>
    ''' <param name="objLocation"></param>
    ''' <returns> TransferCode_Index, if success. "", if failed. </returns>
    ''' <remarks></remarks>
    ''' 
    Public Function SaveTransferOwner(ByVal objHeader As tb_TransferOwner, ByVal objLocation() As tb_TransferOwnerLocation) As String

        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Insert data into table tb_TransferCode
        ' --- STEP 2: Insert tb_TransferCodeLocation
        Try
            ' --- STEP 1: Insert data into tb_TransferCode
            strSQL = " INSERT INTO tb_TransferOwner (TransferOwner_Index,TransferOwner_No,TransferOwner_Date,Customer_Index,  "
            strSQL &= " Times,Ref_No1,Ref_No2,Comment ,Status,"
            strSQL &= " Str1,Str2,Str3,Str4,Str5, "
            strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,DocumentType_Index,New_Customer_Index"

            strSQL &= " ) "
            strSQL &= " Values "
            strSQL &= "  (@TransferOwner_Index,@TransferOwner_No,@TransferOwner_Date,@Customer_Index,"
            strSQL &= " @Times,@Ref_No1,@Ref_No2,@Comment ,@Status,"
            strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5, "
            strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@DocumentType_Index,@New_Customer_Index "
            strSQL &= " )"

            ' ---- Generate auto document number, if user does not enter one.
            If Trim(objHeader.TransferOwner_No) = "" Then
                'Dim objDocumentNumber As New Sy_DocumentNumber
                'Me._NewTransferOwner_No = (objDocumentNumber.getAuto_DocumentNumber("TRC", "TransferCode_No", "tb_TransferCode"))
                'objDocumentNumber = Nothing
                Dim objDocumentNumber As New Sy_DocumentNumber
                _NewTransferOwner_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, "") '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                objDocumentNumber = Nothing
            Else
                Me._NewTransferOwner_No = objHeader.TransferOwner_No
            End If

            With SQLServerCommand

                .Parameters.Clear()
                ' --- Generate TransferCode_Index
                Dim objDBIndex As New Sy_AutoNumber
                Me._NewTransferOwner_Index = objDBIndex.getSys_Value("TransferOwner_Index")
                objDBIndex = Nothing

                .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = Me._NewTransferOwner_Index
                .Parameters.Add("@TransferOwner_No", SqlDbType.VarChar, 50).Value = Me._NewTransferOwner_No
                .Parameters.Add("@TransferOwner_Date", SqlDbType.SmallDateTime, 4).Value = objHeader.TransferOwner_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.Customer_Index
                .Parameters.Add("@Times", SqlDbType.NVarChar, 50).Value = objHeader.Times
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 100).Value = objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 100).Value = objHeader.Ref_No2
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 510).Value = objHeader.Comment
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = objHeader.Str5
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = objHeader.Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = objHeader.DocumentType_Index
                .Parameters.Add("@New_Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.New_Customer_Index

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Insert tb_TransferCodeLocation
            ' --- Note: Loop to update each record. Record in this section were inserted since ADD NEW process.
            ' --- str4 & str5 will be updated here. These 2 fields are used to keep reference values.


            For Each oLocation As tb_TransferOwnerLocation In objLocation

                strSQL = " insert into tb_TransferOwnerLocation(TransferOwnerLocation_Index,TransferOwner_Index,Old_Sku_Index,New_Sku_Index,Order_Index,Lot_No,Plot,Old_ItemStatus_Index,New_ItemStatus_Index,Serial_No,Asset_No,Tag_No,Total_Qty,Weight,Volume,Item_Qty,Price,Package_Index,From_Location_Index,To_Location_Index,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,PalletType_Index,Pallet_Qty,Status,add_by,add_date,add_branch,OrderItem_Index,AssetLocationBalance_Index,To_LocationBalance_Index,From_LocationBalance_Index,MfgDate,ExpDate,Pallet_No,Item_Package_Index  "
                strSQL &= "  ,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,DocumentPlan_Index,new_Plot,TAG_Index,ERP_Location,IsMfg_Date,IsExp_Date  ) "
                strSQL &= " values (@TransferOwnerLocation_Index,@TransferOwner_Index,@Old_Sku_Index,@New_Sku_Index,@Order_Index,@Lot_No,@Plot,@Old_ItemStatus_Index,@New_ItemStatus_Index,@Serial_No,@Asset_No,@Tag_No,@Total_Qty,@Weight,@Volume,@Item_Qty,@Price,@Package_Index,@From_Location_Index,@To_Location_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@PalletType_Index,@Pallet_Qty,@Status,@add_by,getdate(),@add_branch,@OrderItem_Index,@AssetLocationBalance_Index,@To_LocationBalance_Index,@From_LocationBalance_Index,@MfgDate,@ExpDate,@Pallet_No,@Item_Package_Index "
                strSQL &= " ,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@DocumentPlan_Index,@new_Plot,@TAG_Index,@ERP_Location,@IsMfg_Date,@IsExp_Date  ) "
                'strSQL = " insert into tb_TransferCodeLocation(TransferCodeLocation_Index,TransferCode_Index,Old_Sku_Index,New_Sku_Index,Order_Index,Lot_No,Plot,Old_ItemStatus_Index,New_ItemStatus_Index,Serial_No,Asset_No,Tag_No,Total_Qty,Weight,Volume,Item_Qty,Price,Package_Index,From_Location_Index,To_Location_Index,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,PalletType_Index,Pallet_Qty,Status,add_by,add_date,add_branch,OrderItem_Index,AssetLocationBalance_Index,To_LocationBalance_Index,From_LocationBalance_Index,MfgDate,ExpDate,PallNo,Item_Package_Index,New_Package_Index) "
                'strSQL &= " values (@TransferCodeLocation_Index,@TransferCode_Index,@Old_Sku_Index,@New_Sku_Index,@Order_Index,@Lot_No,@Plot,@Old_ItemStatus_Index,@New_ItemStatus_Index,@Serial_No,@Asset_No,@Tag_No,@Total_Qty,@Weight,@Volume,@Item_Qty,@Price,@Package_Index,@From_Location_Index,@To_Location_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@PalletType_Index,@Pallet_Qty,@Status,@add_by,getdate(),@add_branch,@OrderItem_Index,@AssetLocationBalance_Index,@To_LocationBalance_Index,@From_LocationBalance_Index,@MfgDate,@ExpDate,@PallNo,@Item_Package_Index,@New_Package_Index)"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TransferOwnerLocation_Index", SqlDbType.VarChar, 13).Value = oLocation.TransferOwnerLocation_Index
                    .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = Me._NewTransferOwner_Index 'oLocation.TransferCode_Index
                    .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_Sku_Index
                    .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.New_Sku_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = oLocation.Order_Index
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = oLocation.OrderItem_Index
                    .Parameters.Add("@Old_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_ItemStatus_Index
                    .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.New_ItemStatus_Index
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = oLocation.Lot_No
                    .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = oLocation.Plot
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = oLocation.Serial_No
                    .Parameters.Add("@Asset_No", SqlDbType.NVarChar, 50).Value = oLocation.Asset_No.ToString

                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = oLocation.Tag_No
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = oLocation.Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = oLocation.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = oLocation.Volume
                    .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.From_Location_Index
                    .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.To_Location_Index

                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = oLocation.Item_Qty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = oLocation.Price
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = oLocation.Package_Index
                    '   .Parameters.Add("@New_Package_Index", SqlDbType.VarChar, 13).Value = oLocation.New_Package_Index

                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = oLocation.Item_Package_Index
                    If oLocation.Str1 Is Nothing Then oLocation.Str1 = ""

                    If oLocation.Str2 Is Nothing Then oLocation.Str2 = ""

                    If oLocation.Str3 Is Nothing Then oLocation.Str3 = ""

                    If oLocation.Str4 Is Nothing Then oLocation.Str4 = ""

                    If oLocation.Str5 Is Nothing Then oLocation.Str5 = ""

                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = oLocation.Str1.ToString
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = oLocation.Str2.ToString
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = oLocation.Str3.ToString
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = oLocation.Str4.ToString
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = oLocation.Str5.ToString
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = oLocation.Status '-9 ' Temporary status.
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = oLocation.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = oLocation.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = oLocation.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = oLocation.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = oLocation.Flo5
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    ' ---  Pallet Type
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = oLocation.PalletType_Index
                    .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = oLocation.Pallet_Qty
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.AssetLocationBalance_Index
                    .Parameters.Add("@From_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.From_LocationBalance_Index
                    .Parameters.Add("@To_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.To_LocationBalance_Index

                    '25/04/2009
                    .Parameters.Add("@MfgDate", SqlDbType.SmallDateTime).Value = oLocation.MfgDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@ExpDate", SqlDbType.SmallDateTime).Value = oLocation.ExpDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 200).Value = oLocation.Pallet_No

                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = oLocation.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = oLocation.DocumentPlan_No
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlanItem_Index
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlan_Index
                    .Parameters.Add("@new_Plot", SqlDbType.VarChar, 13).Value = oLocation.new_Plot
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = oLocation.TAG_Index
                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = oLocation.ERP_LOCATION
                    .Parameters.Add("@IsMfg_Date", SqlDbType.Bit).Value = oLocation.IsMfg_Date
                    .Parameters.Add("@IsExp_Date", SqlDbType.Bit).Value = oLocation.IsExp_Date

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                If oLocation.Plan_Process <> -9 Then
                    StatusWithdraw_Document = oLocation.Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.SO
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                            strSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                            strSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                            strSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "
                            'strSQL &= " , Status =3 "

                            'If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", _objHeader.Withdraw_Date.ToString("yyyy/MM/dd")) Then
                            '    strSQL &= " ,Last_Withdraw_Date='" & _objHeader.Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                            'End If
                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_PackingItem"
                            strSQL &= " SET Qty_WithDraw =Qty_WithDraw+@Qty "
                            strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"

                        Case Withdraw_Document.Reserve
                            strSQL = " UPDATE tb_Reserve SET  "
                            strSQL &= " Status=4 "
                            strSQL &= " WHERE Reserve_Index =@DocumentPlan_Index"

                        Case Withdraw_Document.Transport 'killz update 08-08-2011

                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                            strSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                            strSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                            strSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "
                            'strSQL &= " , Status =3 "
                            'If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", _objHeader.Withdraw_Date.ToString("yyyy/MM/dd")) Then
                            '    strSQL &= " ,Last_Withdraw_Date='" & _objHeader.Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                            'End If
                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


                    End Select
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlanItem_Index
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlan_Index
                        .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = oLocation.Total_Qty
                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = oLocation.Total_Qty
                        .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = oLocation.Item_Qty
                        .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = oLocation.Weight
                        .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = oLocation.Volume

                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    Dim dblQty_WithDraw As Decimal = 0
                    Dim dblTotal_Qty As Decimal = 0

                    Select Case oLocation.Plan_Process
                        Case Withdraw_Document.SO
                            strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                            strSQL &= " FROM tb_SalesOrderItem  "
                            strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                            SQLServerCommand.Parameters.Clear()
                            SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlan_Index
                            With SQLServerCommand
                                .Connection = Connection
                                .Transaction = myTrans
                                .CommandText = strSQL
                                .CommandTimeout = 0
                            End With

                            DataAdapter.SelectCommand = SQLServerCommand
                            DataAdapter.SelectCommand.Transaction = myTrans
                            Dim DSSO As New DataSet
                            DataAdapter.Fill(DSSO, "tbso")

                            If DSSO.Tables("tbso").Rows.Count <> 0 Then
                                dblQty_WithDraw = CDec(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                                dblTotal_Qty = CDec(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                                If dblQty_WithDraw = 0 Then
                                    '--- สถานะ รอเบิก
                                    strSQL = " UPDATE  tb_SalesOrder"
                                    strSQL &= " SET Status = 2  "
                                    strSQL &= " WHERE SalesOrder_Index ='" & oLocation.DocumentPlan_Index & "'"
                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                                    '--- สถานะ ค้างเบิก
                                    strSQL = " UPDATE  tb_SalesOrder"
                                    strSQL &= " SET Status = 6  "
                                    strSQL &= " WHERE SalesOrder_Index='" & oLocation.DocumentPlan_Index & "'"
                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                End If
                            End If
                    End Select

                End If
              

            Next



            myTrans.Commit()
            Return Me._NewTransferOwner_Index

        Catch e As Exception
            Try
                myTrans.Rollback()
                Return ""
                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Update tb_TransferCode and update tb_TransferCodeLocation
    ''' </summary>
    ''' <param name="objHeader"></param>
    ''' <param name="objLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateTransferOwner_V2(ByVal objHeader As tb_TransferOwner, ByVal objLocation() As tb_TransferOwnerLocation) As String
        Dim strSQL As String
        Dim odtTransfer As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans


        Try


            '  Query data for Rollback ReserveQty in tb_AssetLocationBalance and tb_LocationBalance
            strSQL = " select * from tb_TransferOwnerLocation "
            strSQL &= " WHERE TransferOwner_Index ='" & objHeader.TransferOwner_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(odtTransfer)


            ' --- STEP 1: Update table tb_TransferCode
            strSQL = "  Update tb_TransferOwner SET "
            strSQL &= " TransferOwner_No=@TransferOwner_No,TransferOwner_Date=@TransferOwner_Date,Customer_Index=@Customer_Index,New_Customer_Index=@New_Customer_Index,  "
            strSQL &= " Times=@Times,Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Comment=@Comment ,Status=@Status,"
            strSQL &= " Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5, "
            strSQL &= " Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,Update_by=@Update_by,Update_branch=@Update_branch,Update_Date=GetDate()"
            strSQL &= " WHERE TransferOwner_Index=@TransferOwner_Index "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = objHeader.TransferOwner_Index
                .Parameters.Add("@TransferOwner_No", SqlDbType.VarChar, 50).Value = objHeader.TransferOwner_No
                .Parameters.Add("@TransferOwner_Date", SqlDbType.SmallDateTime, 4).Value = objHeader.TransferOwner_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.Customer_Index
                .Parameters.Add("@New_Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.New_Customer_Index
                .Parameters.Add("@Times", SqlDbType.NVarChar, 50).Value = objHeader.Times
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 100).Value = objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 100).Value = objHeader.Ref_No2
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 510).Value = objHeader.Comment
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = objHeader.Str5
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = objHeader.Flo5
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

            End With

            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)


            For Each oLocation As tb_TransferOwnerLocation In objLocation
                strSQL = " Update tb_TransferOwnerLocation Set " & _
                         " Old_Sku_Index = @Old_Sku_Index " & _
                         " ,New_Sku_Index = @New_Sku_Index " & _
                         " ,Old_ItemStatus_Index = @Old_ItemStatus_Index " & _
                         " ,New_ItemStatus_Index = @New_ItemStatus_Index " & _
                         " ,NewItemFlag = 0 " & _
                         " where  TransferOwnerLocation_Index = @TransferOwnerLocation_Index "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_Sku_Index
                    .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.New_Sku_Index
                    .Parameters.Add("@Old_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_ItemStatus_Index
                    .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.New_ItemStatus_Index
                    .Parameters.Add("@TransferOwnerLocation_Index", SqlDbType.VarChar, 13).Value = oLocation.TransferOwnerLocation_Index
                End With
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            Next



           


            myTrans.Commit()
            Return objHeader.TransferOwner_Index
        Catch e As Exception
            Try
                myTrans.Rollback()
                Return ""
                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
        Return ""
    End Function

    Public Function UpdateTransferOwner(ByVal objHeader As tb_TransferOwner, ByVal objLocation() As tb_TransferOwnerLocation) As String
        Dim strSQL As String
        Dim odtTransfer As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update table tb_TransferCode
        ' --- STEP 2: Delete All  tb_TransferCodeLocation in tb_TransferCode
        ' --- STEP 3: Clear ReserveQty in tb_LocationBalance
        ' --- STEP 4: Clear ReserveQty and value in tb_AssetLocationBalance
        ' --- STEP 5: Insert tb_TransferCodeLocation
        ' --- STEP 6: Update ReserveQty in tb_LocationBalance
        ' --- STEP 7: Update tb_AssetLocationBalance


        Try


            '  Query data for Rollback ReserveQty in tb_AssetLocationBalance and tb_LocationBalance
            strSQL = " select * from tb_TransferOwnerLocation "
            strSQL &= " WHERE TransferOwner_Index ='" & objHeader.TransferOwner_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(odtTransfer)


            ' --- STEP 1: Update table tb_TransferCode
            strSQL = "  Update tb_TransferOwner SET "
            strSQL &= " TransferOwner_No=@TransferOwner_No,TransferOwner_Date=@TransferOwner_Date,Customer_Index=@Customer_Index,New_Customer_Index=@New_Customer_Index,  "
            strSQL &= " Times=@Times,Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Comment=@Comment ,Status=@Status,"
            strSQL &= " Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5, "
            strSQL &= " Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,Update_by=@Update_by,Update_branch=@Update_branch,Update_Date=GetDate()"
            strSQL &= " WHERE TransferOwner_Index=@TransferOwner_Index "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = objHeader.TransferOwner_Index
                .Parameters.Add("@TransferOwner_No", SqlDbType.VarChar, 50).Value = objHeader.TransferOwner_No
                .Parameters.Add("@TransferOwner_Date", SqlDbType.SmallDateTime, 4).Value = objHeader.TransferOwner_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.Customer_Index
                .Parameters.Add("@New_Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.New_Customer_Index
                .Parameters.Add("@Times", SqlDbType.NVarChar, 50).Value = objHeader.Times
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 100).Value = objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 100).Value = objHeader.Ref_No2
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 510).Value = objHeader.Comment
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = objHeader.Str5
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = objHeader.Flo5
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            ' --- STEP 2: Delete All  tb_TransferCodeLocation in tb_TransferCode
            strSQL = "Delete from tb_TransferownerLocation Where TransferOwner_Index = '" & objHeader.TransferOwner_Index & "'"
            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With
            'For Each odr As DataRow In odtTransfer.Rows
            '    ' --- STEP 3: Clear ReserveQty in tb_LocationBalance
            '    strSQL = "UPDATE tb_AssetLocationBalance "
            '    strSQL &= " SET ReserveQty = ReserveQty - " & odr("Total_Qty").ToString
            '    strSQL &= " ,Str1 ='',Str2 =''"
            '    strSQL &= " WHERE AssetLocationBalance_Index ='" & odr("AssetLocationBalance_Index").ToString & "'"

            '    With SQLServerCommand
            '        .CommandText = strSQL
            '        .ExecuteNonQuery()
            '    End With
            'Next

            ' --- STEP 5: Insert tb_TransferCodeLocation
            For Each oLocation As tb_TransferOwnerLocation In objLocation

                strSQL = " insert into tb_TransferOwnerLocation(TransferOwnerLocation_Index,TransferOwner_Index,Old_Sku_Index,New_Sku_Index,Order_Index,Lot_No,Plot,Old_ItemStatus_Index,New_ItemStatus_Index,Serial_No,Asset_No,Tag_No,Total_Qty,Weight,Volume,Item_Qty,Price,Package_Index,From_Location_Index,To_Location_Index,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,PalletType_Index,Pallet_Qty,Status,add_by,add_date,add_branch,OrderItem_Index,AssetLocationBalance_Index,To_LocationBalance_Index,From_LocationBalance_Index,MfgDate,ExpDate,Pallet_No,Item_Package_Index  "
                strSQL &= "  ,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,DocumentPlan_Index,new_Plot,TAG_Index  ) "
                strSQL &= " values (@TransferOwnerLocation_Index,@TransferOwner_Index,@Old_Sku_Index,@New_Sku_Index,@Order_Index,@Lot_No,@Plot,@Old_ItemStatus_Index,@New_ItemStatus_Index,@Serial_No,@Asset_No,@Tag_No,@Total_Qty,@Weight,@Volume,@Item_Qty,@Price,@Package_Index,@From_Location_Index,@To_Location_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@PalletType_Index,@Pallet_Qty,@Status,@add_by,getdate(),@add_branch,@OrderItem_Index,@AssetLocationBalance_Index,@To_LocationBalance_Index,@From_LocationBalance_Index,@MfgDate,@ExpDate,@Pallet_No,@Item_Package_Index "
                strSQL &= " ,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@DocumentPlan_Index,@new_Plot,@TAG_Index  ) "

                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@TransferOwnerLocation_Index", SqlDbType.VarChar, 13).Value = oLocation.TransferOwnerLocation_Index
                    .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = objHeader.TransferOwner_Index  'oLocation.TransferCode_Index
                    .Parameters.Add("@Old_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_Sku_Index
                    .Parameters.Add("@New_Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.New_Sku_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = oLocation.Order_Index
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = oLocation.OrderItem_Index
                    .Parameters.Add("@Old_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_ItemStatus_Index
                    .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.New_ItemStatus_Index
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = oLocation.Lot_No
                    .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = oLocation.Plot
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = "" 'oLocation.Serial_No
                    .Parameters.Add("@Asset_No", SqlDbType.NVarChar, 50).Value = "" 'oLocation.Asset_No.ToString

                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = oLocation.Tag_No
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = oLocation.Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = oLocation.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = oLocation.Volume
                    .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.From_Location_Index
                    .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.To_Location_Index

                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = oLocation.Item_Qty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = oLocation.Price
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = oLocation.Package_Index
                    '.Parameters.Add("@New_Package_Index", SqlDbType.VarChar, 13).Value = oLocation.New_Package_Index
                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = oLocation.Item_Package_Index
                    If oLocation.Str1 Is Nothing Then oLocation.Str1 = ""

                    If oLocation.Str2 Is Nothing Then oLocation.Str2 = ""

                    If oLocation.Str3 Is Nothing Then oLocation.Str3 = ""

                    If oLocation.Str4 Is Nothing Then oLocation.Str4 = ""

                    If oLocation.Str5 Is Nothing Then oLocation.Str5 = ""

                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = oLocation.Str1.ToString
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = oLocation.Str2.ToString
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = oLocation.Str3.ToString
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = oLocation.Str4.ToString
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = oLocation.Str5.ToString
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = oLocation.Status '-9 ' Temporary status.
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = oLocation.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = oLocation.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = oLocation.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = oLocation.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = oLocation.Flo5
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    ' ---  Pallet Type
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = "" 'oLocation.PalletType_Index
                    .Parameters.Add("@Pallet_Qty", SqlDbType.Int, 4).Value = oLocation.Pallet_Qty
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = "" 'oLocation.AssetLocationBalance_Index
                    .Parameters.Add("@From_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.From_LocationBalance_Index
                    .Parameters.Add("@To_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.To_LocationBalance_Index

                    '25/04/2009
                    .Parameters.Add("@MfgDate", SqlDbType.SmallDateTime).Value = oLocation.MfgDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@ExpDate", SqlDbType.SmallDateTime).Value = oLocation.ExpDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 200).Value = oLocation.Pallet_No


                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = oLocation.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = oLocation.DocumentPlan_No
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlanItem_Index
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = oLocation.DocumentPlan_Index
                    .Parameters.Add("@new_Plot", SqlDbType.VarChar, 13).Value = oLocation.new_Plot
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = oLocation.TAG_Index
                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()





                '' --- STEP 7: Update tb_AssetLocationBalance
                'If oLocation.AssetLocationBalance_Index.ToString <> "" Then

                '    strSQL = " UPDATE tb_AssetLocationBalance SET "
                '    strSQL &= " Str1 = @str1"
                '    strSQL &= " ,Str2 = @str2"
                '    strSQL &= " ,Str3 = @str3"
                '    strSQL &= " ,Str4 = @str4"
                '    strSQL &= " ,Str5 = @str5"
                '    strSQL &= " ,Flo1 = @Flo1"
                '    strSQL &= " ,Flo2 = @Flo2"
                '    strSQL &= " ,Flo3 = @Flo3"
                '    strSQL &= " ,Flo4 = @Flo4"
                '    strSQL &= " ,Flo5 = @Flo5"
                '    strSQL &= " ,Asset_No = @Asset_No"
                '    strSQL &= " ,ReserveQty = ReserveQty + @ReserveQty "
                '    strSQL &= " ,update_by = @update_by"
                '    strSQL &= " ,update_date = @update_date"
                '    strSQL &= " ,update_branch = @update_branch"
                '    strSQL &= " WHERE AssetLocationBalance_Index = @AssetLocationBalance_Index"

                '    With SQLServerCommand
                '        .Parameters.Clear()
                '        .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = oLocation.Str1.ToString
                '        .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = oLocation.Str2.ToString
                '        .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = oLocation.Str3.ToString
                '        .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = oLocation.Str4.ToString
                '        .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = oLocation.Str5.ToString

                '        .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = oLocation.Flo1
                '        .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = oLocation.Flo2
                '        .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = oLocation.Flo3
                '        .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = oLocation.Flo4
                '        .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = oLocation.Flo5

                '        .Parameters.Add("@Asset_No", SqlDbType.NVarChar, 50).Value = oLocation.Asset_No.ToString
                '        .Parameters.Add("@ReserveQty", SqlDbType.Float, 8).Value = oLocation.Total_Qty

                '        .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                '        .Parameters.Add("@update_date", SqlDbType.DateTime).Value = Now.ToString("yyyy/MM/dd hh:mm:ss")
                '        .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                '        .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.NVarChar, 13).Value = oLocation.AssetLocationBalance_Index.ToString
                '    End With
                '    SetSQLString = strSQL
                '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                '    EXEC_Command()
                'End If
            Next
            myTrans.Commit()
            Return objHeader.TransferOwner_Index
        Catch e As Exception
            Try
                myTrans.Rollback()
                Return ""
                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
        Return ""
    End Function

    Public Sub UpdatePlanDocument_Reserve(ByVal DocumentPlan_Index As String, ByVal DocumentPlanItem_Index As String, ByVal Qty As Decimal, ByVal Total_Qty As Decimal, ByVal ItemQty As Decimal, ByVal Weight As Decimal, ByVal Volume As Decimal, ByVal Process_Id As Integer, ByVal pConnection As SqlClient.SqlConnection, ByVal pMytrans As SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        'connectDB()
        'Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        'SQLServerCommand.Transaction = myTrans

        Try


            Select Case Process_Id
                Case 10
                    strSQL = "  UPDATE tb_SalesOrderItem "
                    strSQL &= " SET Qty_WithDraw = (Total_Qty_Withdraw / Ratio) - (@Total_Qty / Ratio) "
                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                    strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                    strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "

                Case Else
                    Throw New Exception("Not Support !!")

            End Select
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = DocumentPlanItem_Index
                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = Qty
                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Total_Qty
                .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = ItemQty
                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = Weight
                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = Volume

            End With

            DBExeNonQuery(strSQL, pConnection, pMytrans)

            UpdatePlanDocument_Status(DocumentPlan_Index, Process_Id, pConnection, pMytrans)

            'myTrans.Commit()
        Catch ex As Exception

            Throw ex
        Finally

        End Try
    End Sub

    Private Sub UpdatePlanDocument_Status(ByVal PlanDocument_Index As String, ByVal Process_Id As Withdraw_Document, ByVal pConnect As SqlClient.SqlConnection, ByVal myTrans As System.Data.SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        Try
            Select Case Process_Id
                Case 10
                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)

                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)


                        End If
                    End If
                Case Withdraw_Document.Packing
                    strSQL = " SELECT isnull(SUM(Qty_Withdraw),0) as Qty_Withdraw ,isnull(SUM(Qty),0) as Total_Qty"
                    strSQL &= " FROM tb_PackingItem  "
                    strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSPACK As New DataSet
                    DataAdapter.Fill(DSPACK, "tbpack")
                    If DSPACK.Tables("tbpack").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSPACK.Tables("tbpack").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSPACK.Tables("tbpack").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)

                        ElseIf dblQty_WithDraw < dblTotal_Qty Then
                            '--- สถานะ เบิกบางส่วน
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
          
                        End If

                    End If

                Case Withdraw_Document.Transport 'killz update 08-08-2011

                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        End If

                    End If
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " DELETE TRANSFER STATUS "
    ''' <summary>
    ''' DELETE tb_TransferCodeLocation
    ''' </summary>
    ''' <param name="TransferOwner_Index"></param>
    ''' <remarks></remarks>
    Public Sub Delete_TransferLocation(ByVal TransferOwner_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = "DELETE FROM tb_TransferOwnerLocation  "
            strSQL &= " WHERE TransferOwner_Index ='" & TransferOwner_Index & "' "
            DBExeNonQuery(strSQL, eCommandType.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Delete_TransferLocation(ByVal TransferOwnerLocation_Index As String, _
     ByVal Connect As SqlClient.SqlConnection, _
     ByVal Mytrans As SqlClient.SqlTransaction)



        Dim strSQL As String = ""
        Dim isTran As Boolean = True
        Try


            If Mytrans Is Nothing Then
                connectDB()
                Connect = Connection
                Mytrans = Connection.BeginTransaction
                SQLServerCommand.Transaction = Mytrans

                isTran = False
            End If



            DBExeNonQuery(String.Format(" sp_Delete_TransferOwnerLocation '{0}','{1}' ", TransferOwnerLocation_Index, WV_UserName), Connect, Mytrans)




            If isTran = False Then
                Mytrans.Commit()
                disconnectDB()
            End If


        Catch ex As Exception
            If isTran = False Then
                Mytrans.Rollback()
                disconnectDB()
            End If
            Throw ex
        End Try






    End Sub

    'Public Sub Delete_TransferLocation(ByVal TransferOwnerLocation_Index As String, ByVal Connection As SqlClient.SqlConnection, ByVal Mytrans As SqlClient.SqlTransaction)
    '    Dim strSQL As String = ""
    '    Try
    '        strSQL = "DELETE FROM tb_TransferOwnerLocation  "
    '        strSQL &= " WHERE TransferOwnerLocation_Index ='" & TransferOwnerLocation_Index & "' "
    '        DBExeNonQuery(strSQL, Connection, Mytrans, eCommandType.Text)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


#End Region

#Region " CANCEL TRANSFER STATUS "

    Public Function Cancel_Tranfer(ByVal TransferOwner_Index As String) As Boolean

        Dim strSQL As String = ""
        Dim odtTransfer As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans


        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update status in tb_TransferCodeLocation = -1 (Cancelled)
        ' --- STEP 2: Update status in tb_TransferCode = -1 (Cancelled)
        Try


            '  Query data for Rollback ReserveQty in tb_AssetLocationBalance and tb_LocationBalance
            strSQL = " select * from tb_TransferOwnerLocation "
            strSQL &= " WHERE TransferOwner_Index ='" & TransferOwner_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With


            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(odtTransfer)

            ' --- STEP 1: Update status in tb_TransferStatusLocation = -1 (Cancelled)
            strSQL = "UPDATE tb_TransferOwnerLocation SET"
            strSQL &= "  status =-1 "
            strSQL &= " ,cancel_by = '" & WV_UserName.Replace("'", "''") & "'"
            strSQL &= " ,cancel_date = getdate()"
            strSQL &= " ,cancel_branch = " & WV_Branch_ID
            strSQL &= " WHERE TransferOwner_Index ='" & TransferOwner_Index & "'"

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With

            ' --- STEP 2: Update status in tb_TransferStatus = -1 (Cancelled)
            strSQL = "UPDATE tb_TransferOwner SET"
            strSQL &= "  status =-1 "
            strSQL &= " ,cancel_by = '" & WV_UserName.Replace("'", "''") & "'"
            strSQL &= " ,cancel_date = getdate()"
            strSQL &= " ,cancel_branch = " & WV_Branch_ID
            strSQL &= " ,User_UseDoc = 0 "
            strSQL &= " WHERE TransferOwner_Index ='" & TransferOwner_Index & "'"

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With


            For Each odr As DataRow In odtTransfer.Rows
              
                Dim Total_Qty As Decimal = odr("Total_Qty")
                Dim Qty As Decimal = odr("Weight")
                Dim Weight As Decimal = odr("Weight")
                Dim Volume As Decimal = odr("Volume")
                Dim ItemQty As Decimal = odr("Item_Qty")
                Dim Price As Decimal = odr("Price")
                Dim LocationBalance_Index As String = odr("From_LocationBalance_Index").ToString

                Dim objPicking As New WMS_STD_OUTB_Datalayer.PICKING(WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Type.CUSTOM)
                objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN(Connection, myTrans, WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Action.DELRESERVE, 50, TransferOwner_Index, "ยกเลิกรายการที่ไม่ได้ยืนยัน", LocationBalance_Index, 0, 0, 0, 0, 0, 0, _
                Total_Qty, Weight, Volume, ItemQty, Price)
                objPicking = Nothing



                Dim Plan_Process As String = odr("Plan_Process").ToString
                Dim DocumentPlan_Index As String = odr("DocumentPlan_Index").ToString
                Dim DocumentPlanItem_Index As String = odr("DocumentPlanItem_Index").ToString


                If Plan_Process <> -9 Then


                    strSQL = ""
                    StatusWithdraw_Document = Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.SO
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw = isnull(Qty_WithDraw,0) - ( @Total_Qty / Isnull(Ratio,1) )  "
                            strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                            strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                            strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_PackingItem"
                            strSQL &= " SET Qty_WithDraw =Qty_WithDraw - @Qty "
                            strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"
                        Case Withdraw_Document.Transport 'killz update 08-08-2011
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw =Qty_WithDraw - @Qty "
                            strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                            strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                            strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                    End Select

                    If strSQL <> "" Then
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = odr("DocumentPlanItem_Index").ToString
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = odr("Total_Qty")
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = odr("Total_Qty")
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = odr("Item_Qty")
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = odr("Weight")
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = odr("Volume")

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                    End If

                    ' Update Status
                    UpdatePlanDocument_Status(DocumentPlan_Index, StatusWithdraw_Document, myTrans)
                End If

            Next

            myTrans.Commit()

            Return True

        Catch e As Exception
            Try
                myTrans.Rollback()

                Return False

                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try

    End Function
    Private Sub UpdatePlanDocument_Status(ByVal PlanDocument_Index As String, ByVal Process_Id As Withdraw_Document, ByVal myTrans As System.Data.SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        Try
            Select Case Process_Id
                Case Withdraw_Document.SO
                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                        End If
                    End If
                Case Withdraw_Document.Packing
                    strSQL = " SELECT isnull(SUM(Qty_Withdraw),0) as Qty_Withdraw ,isnull(SUM(Qty),0) as Total_Qty"
                    strSQL &= " FROM tb_PackingItem  "
                    strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSPACK As New DataSet
                    DataAdapter.Fill(DSPACK, "tbpack")
                    If DSPACK.Tables("tbpack").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSPACK.Tables("tbpack").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSPACK.Tables("tbpack").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                        ElseIf dblQty_WithDraw < dblTotal_Qty Then
                            '--- สถานะ เบิกบางส่วน
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                            'ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '    '--- สถานะ รอรับสินค้า
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =5 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                            '    ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                            '    strSQL = "UPDATE tb_SalesOrderItem "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                        End If

                    End If

                Case Withdraw_Document.Transport 'killz update 08-08-2011

                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                        End If

                    End If
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Public Sub GetTransferCodeHeader(ByVal TransferCode_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " select * from tb_TransferCode INNER JOIN  ms_Customer ON"
            strSQL &= " ms_Customer.Customer_Index = tb_TransferCode.Customer_Index "
            strSQL &= " WHERE TransferCode_Index ='" & TransferCode_Index & "' "

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    Public Sub GetDataTransferCode(ByVal pstrWhere As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL &= "  SELECT   tb_TransferCode.TransferCode_Index, tb_TransferCode.TransferCode_No, tb_TransferCode.TransferCode_Date, tb_TransferCode.Ref_No1, tb_TransferCode.add_by"
            strSQL &= "    ,ms_DocumentType.DocumentType_Index, ms_DocumentType.Description as Document_Des"
            strSQL &= "    ,tb_TransferCode.Str1,tb_TransferCode.Comment"
            strSQL &= "    ,tb_TransferCode.TransferCode_Name,tb_TransferCode.ExpectedReturn_Date"
            strSQL &= "    , ms_Department.Department_index , ms_Department.Description as Department_des"
            strSQL &= "    ,tb_TransferCodeLocation.Total_Qty - tb_TransferCodeLocation.Return_Qty As Remain_Qty"
            strSQL &= "    ,tb_TransferCodeLocation.TransferCodeLocation_Index"
            strSQL &= " FROM    tb_TransferCode INNER JOIN ms_DocumentType on  tb_TransferCode.DocumentType_Index = ms_DocumentType.DocumentType_Index "
            strSQL &= "         INNER JOIN ms_Department ON tb_TransferCode.Department_index = ms_Department.Department_index"
            strSQL &= " 	    INNER JOIN tb_TransferCodeLocation ON 	tb_TransferCode.TransferCode_Index = tb_TransferCodeLocation.TransferCode_Index"
            strSQL &= " WHERE tb_TransferCode.Status IN (1,2) AND tb_TransferCodeLocation.Total_Qty - tb_TransferCodeLocation.Return_Qty > 0"

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub
    Public Sub CheckDuplicateBorrow(ByVal pstrWhere As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "  SELECT * FROM tb_Borrow B INNER JOIN tb_BorrowLocation BL ON B.Borrow_Index = BL.Borrow_Index "
            strSQL &= " WHERE BL.Total_Qty <> BL.Return_Qty AND B.Status IN (2,3)"
            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub
End Class


