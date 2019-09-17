Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_OUTB_Datalayer

Public Class cls_KSL_SINO : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

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

    Public Sub getTransferStatusView(ByVal WhereString As String, ByVal pintRowStart As Integer, ByVal pintRowEnd As Integer)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            ' --- Need to join ms_ProcessStatus with Process_Id =5 FOR TransferStatus

            strSQL &= "SELECT * FROM ("
            strSQL &= " SELECT ROW_NUMBER() OVER(ORDER BY tb_TransferStatus.TransferStatus_Index ASC) AS ROW_NO, Count(*) Over() AS ROW_COUNT"
            strSQL &= " ,tb_TransferStatus.TransferStatus_Index, tb_TransferStatus.TransferStatus_No, tb_TransferStatus.TransferStatus_Date, tb_TransferStatus.Ref_No1, tb_TransferStatus.Ref_No2,tb_TransferStatus.add_by,tb_TransferStatus.DocumentType_Index, ms_DocumentType.Description,ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name,ms_ProcessStatus.Status, ms_ProcessStatus.Description as StatusDescription"
            strSQL &= "  ,tb_TransferStatus.Str1,tb_TransferStatus.Comment"
            strSQL &= "  ,tb_TransferStatus.Status_Fullfill ,  (case when isnull(tb_TransferStatus.Activity_id, 0) = 0 then 'รอแสกน' when isnull(tb_TransferStatus.Activity_id, 0) = 1 then 'กำลังแสกน' when isnull(tb_TransferStatus.Activity_id, 0) = 7 then 'แสกนเรียบร้อย'  else '' end  ) as Activity  "
            strSQL &= "  ,US.DistributionCenter_Index, dbo.ms_DistributionCenter.Description AS DistributionCenter"
            strSQL &= "  FROM    tb_TransferStatus INNER JOIN "
            strSQL &= "   ms_customer ON tb_TransferStatus.Customer_Index = ms_Customer.Customer_Index INNER JOIN ms_ProcessStatus ON tb_TransferStatus.Status = ms_ProcessStatus.Status INNER JOIN ms_DocumentType on  tb_TransferStatus.DocumentType_Index = ms_DocumentType.DocumentType_Index "

            strSQL &= "   LEFT OUTER JOIN"
            strSQL &= "   dbo.se_User AS US ON (US.userName = dbo.tb_TransferStatus.add_by OR US.user_id = dbo.tb_TransferStatus.add_by) LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_DistributionCenter ON US.DistributionCenter_Index = dbo.ms_DistributionCenter.DistributionCenter_Index"

            strSQL &= "   WHERE  ms_ProcessStatus.Process_Id =5  "

            If WhereString <> "" Then
                strSQL &= WhereString
            End If
            strSQL &= " ) AS TransferStatus_View "

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

    Public Function getReferenceDocSO(ByVal Process_Id As Integer, ByVal _Ref_SO_DocumentType_Index As String) As String

        Dim strSQL As String = ""

        Try
            strSQL = "   select Ref_DocumentType_Index from ms_DocumentType  "
            strSQL &= "  where Process_Id = " & Process_Id & " AND DocumentType_Index = '" & _Ref_SO_DocumentType_Index & "'"
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getSKU_Package(ByVal sku_Index As String) As DataTable
        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT      ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package,ms_Package.Weight as Unit_Weight,cast(ms_Package.Dimension_Wd*ms_Package.Dimension_Len*ms_Package.Dimension_Hi as decimal)/ms_DimensionType.Ratio as Unit_Volume "
            strSQL &= " FROM        ms_SKURatio INNER JOIN  "
            strSQL &= "             ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "             ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= " left join ms_DimensionType on ms_DimensionType.DimensionType_Index=ms_Package.DimensionType_Index "
            strSQL &= " WHERE       ms_SKURatio.Sku_Index ='" & sku_Index & "'"
            strSQL &= "             AND ms_SKURatio.status_id <> -1 AND ms_SKU.Status_Id <> -1 AND  isNull(isItem_Package,0) = 0"

            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class

Public Class TF_Logic : Inherits DBType_SQLServer

    Private builder As New System.Text.StringBuilder


    Public Function InsertTF(ByVal objHeader As tb_TransferStatus, _
      Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String


        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            Dim strSQL As String = ""

            strSQL = " INSERT INTO tb_TransferStatus (TransferStatus_Index,TransferStatus_No,TransferStatus_Date,Customer_Index,  "
            strSQL &= " Times,Ref_No1,Ref_No2,Comment ,Status,"
            strSQL &= " Str1,Str2,Str3,Str4,Str5, "
            strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,DocumentType_Index"
            strSQL &= " ) "
            strSQL &= " Values "
            strSQL &= "  (@TransferStatus_Index,@TransferStatus_No,@TransferStatus_Date,@Customer_Index,"
            strSQL &= " @Times,@Ref_No1,@Ref_No2,@Comment ,@Status,"
            strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5, "
            strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@DocumentType_Index"
            strSQL &= " )"

            If Trim(objHeader.TransferStatus_No) = "" Then
                objHeader.TransferStatus_No = New Sy_DocumentNumber_New().Auto_DocumentType_Number(objHeader.DocumentType_Index, "", objHeader.TransferStatus_Date.ToString("yyyy/MM/dd"), _Connection, _myTrans)
            End If

            If Trim(objHeader.TransferStatus_Index) = "" Then
                objHeader.TransferStatus_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "TransferStatus_Index")
            End If


            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@TransferStatus_Index", SqlDbType.VarChar, 13).Value = objHeader.TransferStatus_Index
                .Parameters.Add("@TransferStatus_No", SqlDbType.VarChar, 50).Value = objHeader.TransferStatus_No
                .Parameters.Add("@TransferStatus_Date", SqlDbType.SmallDateTime, 4).Value = objHeader.TransferStatus_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objHeader.Customer_Index
                .Parameters.Add("@Times", SqlDbType.NVarChar, 50).Value = objHeader.Times
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 400).Value = objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 400).Value = objHeader.Ref_No2
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = objHeader.Comment
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
            End With

            DBExeNonQuery(strSQL, _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()

            Return objHeader.TransferStatus_Index
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function InsertTFL(ByVal objLocation() As tb_TransferStatusLocation, _
        Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String


        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If
            Dim strSQL As String = ""
            strSQL = " insert into tb_TransferStatusLocation(TransferStatusLocation_Index,TransferStatus_Index,Sku_Index,Package_Index,Order_Index,Lot_No,Plot,Old_ItemStatus_Index,New_ItemStatus_Index,Serial_No,Tag_No,Total_Qty,Weight,Volume,Item_Qty,Price,From_Location_Index,To_Location_Index,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,PalletType_Index,Pallet_Qty,Status,add_by,add_date,add_branch,OrderItem_Index,AssetLocationBalance_Index,From_LocationBalance_Index,To_LocationBalance_Index,MfgDate,ExpDate,PallNo,Asset_No,Item_Package_Index,Qty,TAG_Index,ERP_Location_TO,ERP_Location_From,NewItemFlag) "
            strSQL &= " values (@TransferStatusLocation_Index,@TransferStatus_Index,@Sku_Index,@Package_Index,@Order_Index,@Lot_No,@Plot,@Old_ItemStatus_Index,@New_ItemStatus_Index,@Serial_No,@Tag_No,@Total_Qty,@Weight,@Volume,@Item_Qty,@Price,@From_Location_Index,@To_Location_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@PalletType_Index,@Pallet_Qty,@Status,@add_by,getdate(),@add_branch,@OrderItem_Index,@AssetLocationBalance_Index,@From_LocationBalance_Index,@To_LocationBalance_Index,@MfgDate,@ExpDate,@PallNo,@Asset_No,@Item_Package_Index,@Qty,@TAG_Index,@ERP_Location_TO,@ERP_Location_From,2)"



            For Each oLocation As tb_TransferStatusLocation In objLocation
                With SQLServerCommand
                    .Parameters.Clear()
                    If oLocation.TransferStatusLocation_Index = "" Then
                        oLocation.TransferStatusLocation_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "TransferStatusLocation_Index")
                    End If
                    .Parameters.Add("@TransferStatusLocation_Index", SqlDbType.VarChar, 13).Value = oLocation.TransferStatusLocation_Index
                    .Parameters.Add("@TransferStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.TransferStatus_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = oLocation.Sku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = oLocation.Package_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = oLocation.Order_Index
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = oLocation.OrderItem_Index
                    .Parameters.Add("@Old_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.Old_ItemStatus_Index
                    .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = oLocation.New_ItemStatus_Index
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = oLocation.Lot_No
                    .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = oLocation.Plot
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = oLocation.Serial_No
                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = oLocation.Tag_No
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = oLocation.Qty
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = oLocation.Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = oLocation.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = oLocation.Volume
                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = oLocation.Item_Qty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = oLocation.Price
                    .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.From_Location_Index
                    .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = oLocation.To_Location_Index
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

                    '25/04/2009
                    .Parameters.Add("@MfgDate", SqlDbType.SmallDateTime).Value = oLocation.MfgDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@ExpDate", SqlDbType.SmallDateTime).Value = oLocation.ExpDate.ToString("yyyy/MM/dd")
                    .Parameters.Add("@PallNo", SqlDbType.VarChar, 100).Value = oLocation.PallNo


                    '06/05/2009
                    .Parameters.Add("@From_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.From_LocationBalance_Index
                    .Parameters.Add("@To_LocationBalance_Index", SqlDbType.VarChar, 13).Value = oLocation.To_LocationBalance_Index


                    '09/05/2009
                    .Parameters.Add("@Asset_No", SqlDbType.VarChar, 100).Value = oLocation.Asset_No

                    '14/08/2013
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = oLocation.TAG_Index

                    .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 200).Value = oLocation.ERP_Location_TO
                    .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 200).Value = oLocation.ERP_Location_From

                End With

                DBExeNonQuery(strSQL, _Connection, _myTrans)
            Next




            If IsNotPassTransaction Then _myTrans.Commit()
            Return ""
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function ConfirmTF(ByVal TransferStatus_Index As String, _
          Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String


        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

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
            Dim WeightPerQty As Decimal = 0
            '06/05/2009
            Dim Qty_Sku_Location_Bal As Decimal = 0
            Dim Qty_PLot_Location_Bal As Decimal = 0
            Dim Qty_ItemStatus_Location_Bal As Decimal = 0

            Dim strNew_LocationBalance_Index As String = ""
            Dim strNew_Tag_No As String = ""



            ' --- TRANSACTION SUMMARY ---
            ' --- STEP 1: Modify tb_LocationBalance 
            '             If   Move Qty All ; Update
            '             Else Move Some Qty ; Insert into tb_LocationBalance
            ' --- STEP 2: Update ms_Location 
            ' --- STEP 3: Update tb_TransferStatus & tb_TransferStatusLocation
            ' --- STEP 4: Insert tb_Transaction & tb_AssetTransaction OUT
            ' --- STEP 5: Insert tb_Transaction & tb_AssetTransaction IN



            ' STEP 1:
            strSQL = "  SELECT *"
            strSQL &= "  FROM    VIEW_TransferStatus_Confirm"
            strSQL &= "   WHERE  (Status = 1) AND (TransferStatus_Index ='" & TransferStatus_Index & "') "

            With SQLServerCommand
                .Connection = _Connection
                .Transaction = _myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = _myTrans

            If DS.Tables.Contains("TFS") Then
                DS.Tables("TFS").Clear()
            End If
            DataAdapter.Fill(DS, "TFS")

            If DS.Tables("TFS").Rows.Count <> 0 Then
                For i As Integer = 0 To DS.Tables("TFS").Rows.Count - 1

                    Dim odrTransfer As DataRow = DS.Tables("TFS").Rows(i)

                    ' ***********************************
                    Dim Customer_Index As String = odrTransfer("Customer_Index").ToString
                    Dim Plot As String = odrTransfer("PLot").ToString
                    Dim Old_ItemStatus_Index As String = odrTransfer("Old_ItemStatus_Index").ToString
                    Dim New_ItemStatus_Index As String = odrTransfer("New_ItemStatus_Index").ToString
                    Dim Sku_Index As String = odrTransfer("Sku_Index").ToString
                    Dim strNew_Tag_Index As String = odrTransfer("TAG_Index").ToString
                    ' ***********************************
                    Dim iTag_No As String = odrTransfer("Tag_No").ToString
                    Dim iFrom_Location_Index As String = odrTransfer("From_Location_Index").ToString
                    Dim iTo_Location_Index As String = odrTransfer("To_Location_Index").ToString
                    Dim iTotal_Qty As Decimal = CDec(odrTransfer("Total_Qty").ToString)
                    Dim iWeight As Decimal = CDec(odrTransfer("Weight").ToString)
                    Dim iVolume As Decimal = CDec(odrTransfer("Volume").ToString)
                    Dim iItem_Qty As Decimal = CDec(odrTransfer("Item_Qty").ToString)
                    Dim iPrice As Decimal = CDec(odrTransfer("Price").ToString)
                    Dim iPalletType_Index As String = odrTransfer("PalletType_Index").ToString
                    Dim iPallet_Qty As Integer = CDec(odrTransfer("Pallet_Qty").ToString)
                    Dim TransferStatus_No As String = odrTransfer("TransferStatus_No").ToString
                    Dim New_TransferStatusLocation_Index As String = odrTransfer("TransferStatusLocation_Index").ToString
                    Dim From_LocationBalance_Index As String = odrTransfer("From_LocationBalance_Index").ToString
                    Dim iTag_Index As String = odrTransfer("Tag_Index").ToString
                    Dim iRatio As Decimal = 1
                    Dim ERP_Location_TO As String = odrTransfer("ERP_Location_TO").ToString
                    Dim ERP_Location_From As String = odrTransfer("ERP_Location_From").ToString
                    'Dim objRatio As New ms_SKU_Update(ms_SKU_Update.enuOperation_Type.SEARCH)
                    iRatio = getRatio(odrTransfer("Sku_Index").ToString, odrTransfer("Package_Index").ToString, _Connection, _myTrans)
                    ' objRatio = Nothing

                    Dim iQty As Decimal = iTotal_Qty / iRatio

                    ' *** Check ?? : Move All Qty of Tag or Move Some Qty from TAG ***
                    strSQL = "   SELECT  * "
                    strSQL &= "   FROM  tb_LocationBalance   "
                    '   strSQL &= "  WHERE  Tag_No='" & iTag_No & "' AND Location_Index ='" & iFrom_Location_Index & "' "
                    '   strSQL &= "  AND  LocationBalance_Index ='" & From_LocationBalance_Index & "'"
                    strSQL &= "  WHERE  LocationBalance_Index ='" & From_LocationBalance_Index & "'"


                    With SQLServerCommand
                        .Connection = _Connection
                        .Transaction = _myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = _myTrans

                    If DS.Tables.Contains("LB") Then
                        DS.Tables("LB").Clear()
                    End If
                    DataAdapter.Fill(DS, "LB") 'tb_LocationBalance

                    If DS.Tables("LB").Rows.Count > 0 Then
                        Dim odrLocationBalance As DataRow = DS.Tables("LB").Rows(0)
                        WeightPerQty = DS.Tables("LB").Rows(0)("Weight_Bal_Begin") / DS.Tables("LB").Rows(0)("Qty_Bal_Begin")
                        If iTotal_Qty = CDec(odrLocationBalance("Qty_Bal").ToString) Then
                            ' *** Move All Qty of Tag ***
                            strNew_LocationBalance_Index = odrTransfer("From_LocationBalance_Index").ToString
                            strNew_Tag_No = iTag_No

                            'strSQL = " UPDATE tb_LocationBalance  SET  "
                            'strSQL &= "Qty_Bal ='" & iTotal_Qty & "',"
                            'strSQL &= " Qty_Recieve_Package = " & odrLocationBalance("Ratio").ToString & " * " & iTotal_Qty & " ,"
                            'strSQL &= " Location_Index='" & iTo_Location_Index & "', ItemStatus_Index='" & New_ItemStatus_Index & "'  "
                            '' --- 29/04/2009 Krit
                            'strSQL &= " ,ReserveQty = ReserveQty - " & iTotal_Qty
                            'strSQL &= " ,ReserveWeight = ReserveWeight - " & iWeight
                            'strSQL &= " ,ReserveVolume = ReserveVolume - " & iVolume
                            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item - " & iItem_Qty
                            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price - " & iPrice
                            '' ---------------------------
                            'strSQL &= " WHERE  Tag_No='" & iTag_No & "' AND Location_Index ='" & iFrom_Location_Index & "' "
                            'strSQL &= "  AND  LocationBalance_Index ='" & From_LocationBalance_Index & "'"


                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()


                            strSQL = "  UPDATE tb_LocationBalance    "
                            strSQL &= " SET "
                            strSQL &= " Location_Index =@ToLocation_Index"
                            strSQL &= " ,ItemStatus_Index =@New_ItemStatus_Index"
                            '  strSQL &= " ,SKU_Index  =@SKU_Index"

                            strSQL &= " ,Qty_Recieve_Package=@Qty"
                            strSQL &= " ,Qty_Bal=@Total_Qty"
                            strSQL &= " ,Weight_Bal=@Weight"
                            strSQL &= " ,Volume_Bal=@Volume "
                            strSQL &= " ,Qty_Item_Bal =@ItemQty"
                            strSQL &= " ,OrderItem_Price_Bal=@Price"
                            strSQL &= " ,ERP_Location = @ERP_Location "
                            'strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                            'strSQL &= " ,ReserveWeight = ReserveWeight-@Weight"
                            'strSQL &= " ,ReserveVolume = ReserveVolume-@Volume"
                            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item-@ItemQty"
                            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price-@Price"
                            'strSQL &= " WHERE Tag_No=@Tag_No "
                            'strSQL &= " AND Location_Index =@FromLocation_Index"
                            strSQL &= "  WHERE  LocationBalance_Index=@From_LocationBalance_Index"
                            With SQLServerCommand
                                .Parameters.Clear()
                                .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 50).Value = New_ItemStatus_Index
                                .Parameters.Add("@From_LocationBalance_Index", SqlDbType.VarChar, 50).Value = From_LocationBalance_Index
                                .Parameters.Add("@FromLocation_Index", SqlDbType.VarChar, 50).Value = iFrom_Location_Index
                                .Parameters.Add("@ToLocation_Index", SqlDbType.VarChar, 50).Value = iTo_Location_Index
                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = iTag_No
                                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = iTotal_Qty
                                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = WeightPerQty * iTotal_Qty 'iWeight
                                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = iVolume
                                ' *** If Check Qty_Recieve_Package *** 
                                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = iQty
                                .Parameters.Add("@ItemQty", SqlDbType.Float, 8).Value = iItem_Qty
                                .Parameters.Add("@Price", SqlDbType.Float, 8).Value = iPrice
                                .Parameters.Add("@ERP_Location", SqlDbType.VarChar, 200).Value = ERP_Location_TO
                            End With

                            DBExeNonQuery(strSQL, _Connection, _myTrans)


                            Dim objPicking As New WMS_STD_OUTB_Datalayer.PICKING(WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Type.CUSTOM)
                            'objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN(Connection, myTrans, PICKING.enmPicking_Action.DELBALANCE, 5, "", "ยืนยันการโอน,คืนค่าจอง", From_LocationBalance_Index, 0, 0, 0, 0, 0, 0, _
                            'iTotal_Qty, (WeightPerQty * iTotal_Qty), iVolume, iItem_Qty, iPrice)

                            objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN(_Connection, _myTrans, WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Action.DELRESERVE, 5, "", "ยืนยันการโอน,คืนค่าจอง", From_LocationBalance_Index, iQty, iTotal_Qty, (WeightPerQty * iTotal_Qty), iVolume, iItem_Qty, iPrice, _
                   iTotal_Qty, (WeightPerQty * iTotal_Qty), iVolume, iItem_Qty, iPrice)


                            ' *** Manage ms_Location from New Location : iTo_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 1
                            strSQL &= " Space_Used =1,Current_Qty=Current_Qty+" & iTotal_Qty & ",Current_Weight=Current_Weight+" & iWeight & ",Current_Volume=Current_Volume+" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iTo_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Manage ms_Location from Old Location : iFrom_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 2
                            strSQL &= " Current_Qty=Current_Qty-" & iTotal_Qty & ",Current_Weight=Current_Weight-" & iWeight & ",Current_Volume=Current_Volume-" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iFrom_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** If Area of Location Emtry ***
                            strSQL = "UPDATE ms_Location " 'ครั้งที่ 3
                            strSQL &= " SET Space_Used =0 "
                            strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & iFrom_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Update tb_TransferStatus ***
                            strSQL = "UPDATE tb_TransferStatus "
                            strSQL &= " SET Status = 2 "
                            strSQL &= " WHERE  TransferStatus_Index ='" & TransferStatus_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            '06/05/2009
                            ' *** Update tb_TransferStatusLocation ***
                            strSQL = "UPDATE tb_TransferStatusLocation SET "
                            strSQL &= " Status = 2 "
                            strSQL &= " ,Str4 = '" & iTag_No & "'"
                            strSQL &= " ,To_LocationBalance_Index = '" & strNew_LocationBalance_Index & "'"
                            strSQL &= " WHERE  TransferStatusLocation_Index ='" & odrTransfer("TransferStatusLocation_Index").ToString & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                        Else


                            Dim objpicking As New WMS_STD_OUTB_Datalayer.PICKING(WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Type.CUSTOM)
                            objpicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN(_Connection, _myTrans, WMS_STD_OUTB_Datalayer.PICKING.enmPicking_Action.DELBALANCE_RESERVE, 5, "", "ยืนยันการโอน", From_LocationBalance_Index, iQty, iTotal_Qty, (WeightPerQty * iTotal_Qty), iVolume, iItem_Qty, iPrice, _
                            iTotal_Qty, (WeightPerQty * iTotal_Qty), iVolume, iItem_Qty, iPrice)

                            '*********************** Add by Dong_kk 02-04-2010  **********************
                            '*********************** Begin Add New Tag No .     **********************

                            Dim objSy_AutoNumber As New Sy_AutoNumber
                            strNew_Tag_No = objSy_AutoNumber.getSys_Value(_Connection, _myTrans, "Tag_No")
                            strNew_Tag_Index = objSy_AutoNumber.getSys_Value(_Connection, _myTrans, "Tag_Index")



                            Dim RatioOrderItem As Decimal = CType(DBExeQuery_Scalar(String.Format(" select Top 1 Ratio from tb_OrderItem where OrderItem_Index In (select OrderItem_Index from tb_Tag where TAG_Index = '{0}') ", iTag_Index), _Connection, _myTrans, eCommandType.Text), Decimal)


                            strSQL = " INSERT INTO 	tb_Tag(Process_Id,Document_Index,TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,add_by,add_date,add_branch,ERP_Location )"
                            strSQL &= "             (SELECT top 1 5, '" & TransferStatus_Index & "', '" & strNew_Tag_Index & "','" & strNew_Tag_No & "'"
                            strSQL &= "             ,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No"
                            strSQL &= "             ,Qty," & CDec(odrTransfer("Weight").ToString) & "," & CDec(odrTransfer("Volume").ToString) & "," & CDec(odrTransfer("Total_Qty").ToString) / RatioOrderItem & "," & CDec(odrTransfer("Weight").ToString) & "," & CDec(odrTransfer("Volume").ToString) & ",2"
                            strSQL &= "             ,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,add_by,add_date,add_branch,'" & odrTransfer("ERP_Location_TO").ToString & "' "
                            strSQL &= "     FROM tb_Tag "
                            strSQL &= String.Format("     WHERE  TAG_Index = '{0}')", iTag_Index)


                            DBExeNonQuery(strSQL, _Connection, _myTrans)


                            strSQL = "UPDATE tb_TransferStatusLocation SET "
                            strSQL &= " Tag_NoNew = '" & strNew_Tag_No & "'"
                            strSQL &= " ,Str4 = '" & iTag_No & "'"
                            strSQL &= " WHERE  TransferStatusLocation_Index ='" & odrTransfer("TransferStatusLocation_Index").ToString & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            '*********************** END Add New Tag No . **********************

                            strSQL = " INSERT INTO tb_LocationBalance    "
                            strSQL &= " (LocationBalance_Index,Location_Index,Tag_No,Sku_Index,Order_Index,OrderItem_Index,Lot_No,PLot,ItemStatus_Index,Serial_No,PalletType_Index,Pallet_Qty,Ratio,Qty_Bal,Weight_Bal,Volume_Bal,Qty_Item_Begin,OrderItem_Price_Begin,Qty_Item_Bal,OrderItem_Price_Bal,Location_Index_Begin,Qty_Bal_Begin,Weight_Bal_Begin,Volume_Bal_Begin,Mfg_Date,Exp_Date,IsMfg_Date,IsExp_Date,Package_Index,add_by,add_branch,MixPallet,Qty_Recieve_Package,Item_Package_Index,Tag_Index,ERP_Location) VALUES "
                            strSQL &= " (@LocationBalance_Index,@Location_Index,@Tag_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Lot_No,@PLot,@ItemStatus_Index,@Serial_No,@PalletType_Index,@Pallet_Qty,@Ratio,@Qty_Bal,@Weight_Bal,@Volume_Bal,@Qty_Item_Begin,@OrderItem_Price_Begin,@Qty_Item_Bal,@OrderItem_Price_Bal,@Location_Index_Begin,@Qty_Bal_Begin,@Weight_Bal_Begin,@Volume_Bal_Begin,@Mfg_Date,@Exp_Date,@IsMfg_Date,@IsExp_Date,@Package_Index,@add_by,@add_branch,@MixPallet,@Qty_Recieve_Package,@Item_Package_Index,@Tag_Index,@ERP_Location) "


                            With SQLServerCommand

                                .Parameters.Clear()

                                '  **** Generate OrderItemLocation_Index  ***

                                '    Dim objDBIndex As New Sy_AutoNumber

                                strNew_LocationBalance_Index = objSy_AutoNumber.getSys_Value(_Connection, _myTrans, "LocationBalance_Index")
                                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = strNew_LocationBalance_Index
                                ' *******************************************
                                .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrLocationBalance("OrderItem_Index").ToString
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrLocationBalance("Order_Index").ToString
                                .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrLocationBalance("Sku_Index").ToString
                                .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrLocationBalance("Lot_No").ToString
                                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("To_Location_Index").ToString
                                .Parameters.Add("@Location_Index_Begin", SqlDbType.VarChar, 13).Value = odrTransfer("From_Location_Index").ToString
                                ' .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = odrLocationBalance("Tag_No").ToString + "-I"
                                'new TagNo 2009/04/24

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                                '
                                .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrLocationBalance("PLot").ToString
                                .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("New_ItemStatus_Index").ToString
                                .Parameters.Add("@Qty_Bal_Begin", SqlDbType.Float, 8).Value = odrTransfer("Total_Qty")
                                .Parameters.Add("@Weight_Bal_Begin", SqlDbType.Float, 8).Value = WeightPerQty * odrTransfer("Total_Qty")  'odrTransfer("Weight")
                                .Parameters.Add("@Volume_Bal_Begin", SqlDbType.Float, 8).Value = odrTransfer("Volume")
                                .Parameters.Add("@Qty_Item_Begin", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                                .Parameters.Add("@OrderItem_Price_Begin", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
                                .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = odrLocationBalance("Ratio")
                                .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = odrTransfer("Total_Qty")
                                .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = WeightPerQty * odrTransfer("Total_Qty") 'odrTransfer("Weight")
                                .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = odrTransfer("Volume")
                                .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                                .Parameters.Add("@OrderItem_Price_Bal", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
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

                                .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Item_Package_Index").ToString
                                .Parameters.Add("@MixPallet", SqlDbType.Bit, 4).Value = odrLocationBalance("MixPallet")

                                ' *** Package_Index,Qty_Recieve_Package,Qty_Faction  ***
                                .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = odrLocationBalance("Package_Index").ToString
                                .Parameters.Add("@Qty_Recieve_Package", SqlDbType.Float, 8).Value = odrTransfer("Total_Qty") / odrLocationBalance("Ratio")
                                .Parameters.Add("@Tag_Index", SqlDbType.VarChar, 50).Value = strNew_Tag_Index
                                .Parameters.Add("@ERP_Location", SqlDbType.VarChar, 50).Value = odrTransfer("ERP_Location_TO").ToString
                            End With

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Manage ms_Location from New Location : iTo_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 1
                            strSQL &= " Space_Used =1,Current_Qty=Current_Qty+" & iTotal_Qty & ",Current_Weight=Current_Weight+" & iWeight & ",Current_Volume=Current_Volume+" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iTo_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Manage ms_Location from Old Location : iFrom_Location_Index ***
                            strSQL = " UPDATE ms_Location  SET  " 'ครั้งที่ 2
                            strSQL &= " Current_Qty=Current_Qty-" & iTotal_Qty & ",Current_Weight=Current_Weight-" & iWeight & ",Current_Volume=Current_Volume-" & iVolume & "  "
                            strSQL &= " WHERE  Location_Index ='" & iFrom_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** If Area of Location Emtry ***
                            strSQL = "UPDATE ms_Location " 'ครั้งที่ 3
                            strSQL &= " SET Space_Used =0 "
                            strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & iFrom_Location_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Update tb_TransferStatus ***
                            strSQL = "UPDATE tb_TransferStatus "
                            strSQL &= " SET Status = 2 "
                            strSQL &= " WHERE  TransferStatus_Index ='" & TransferStatus_Index & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' *** Update tb_TransferStatusLocation ***
                            strSQL = "UPDATE tb_TransferStatusLocation SET "
                            strSQL &= " Status = 2 "
                            strSQL &= " ,To_LocationBalance_Index = '" & strNew_LocationBalance_Index & "'"
                            strSQL &= " WHERE  TransferStatusLocation_Index ='" & odrTransfer("TransferStatusLocation_Index").ToString & "' "

                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                        End If

                        ' ***********************  2. คำนวนจำนวนสินค้า
                        Dim objCalBalance As New WMS_STD_Master_Datalayer.CalculateBalance
                        objCalBalance.setQty_Recieve_Package(_Connection, _myTrans, odrLocationBalance("LocationBalance_Index").ToString)

                        objCalBalance = Nothing
                        ' ***********************
                    End If

                    ' *** Important : You need to insert two Record in tb_transaction >> Record IN and Record OUT  ***

                    ' *** Call Function Get Balance *** จำนวนสินค้า
                    Dim objBal As New WMS_STD_Master_Datalayer.CalculateBalance
                    ' *** Qty ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index) '- Cdec(odrTransfer("Total_Qty").ToString)
                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot) '- Cdec(odrTransfer("Total_Qty").ToString)
                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransfer("Total_Qty").ToString)

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
                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index) '- Cdec(odrTransfer("Weight").ToString)
                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot) '- Cdec(odrTransfer("Weight").ToString)
                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransfer("Weight").ToString)

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
                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index) '- Cdec(odrTransfer("Volume").ToString)
                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot) '- Cdec(odrTransfer("Volume").ToString)
                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, Old_ItemStatus_Index) '- Cdec(odrTransfer("Volume").ToString)

                    If Volume_Sku_Bal < 0 Then
                        Volume_Sku_Bal = 0
                    End If
                    If Volume_PLot_Bal < 0 Then
                        Volume_PLot_Bal = 0
                    End If
                    If Volume_ItemStatus_Bal < 0 Then
                        Volume_ItemStatus_Bal = 0
                    End If

                    '06/05/2009
                    Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, odrTransfer("From_Location_Index").ToString)
                    Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, odrTransfer("From_Location_Index").ToString)
                    Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, Old_ItemStatus_Index, odrTransfer("From_Location_Index").ToString)

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
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_out,Weight_Out,Volume_Out,Qty_Item_Out,OrderItem_Price_Out,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index ,Location_Alias_To,Location_Alias_From,Serial_No,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,Item_Package_Index,DocumentPlan_Index,DocumentPlanItem_Index,TAG_Index,ERP_Location_From,ERP_Location_TO,TAG_IndexNew,Tag_NoNew) VALUES "
                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_out,@Weight_Out,@Volume_Out,@Qty_Item_Out,@OrderItem_Price_Out,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@Item_Package_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@TAG_Index,@ERP_Location_From,@ERP_Location_TO,@TAG_IndexNew,@Tag_NoNew)"

                    ' **** Manage Balance ***

                    With SQLServerCommand
                        .Parameters.Clear()
                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value(_Connection, _myTrans, "Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = odrTransfer("TransferStatus_No").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransfer("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("From_Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("To_Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = odrTransfer("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransfer("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Old_ItemStatus_Index").ToString
                        .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("New_ItemStatus_Index").ToString
                        .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString) 'iTotal_Qty

                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransfer("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        ' Order_Date
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("TransferStatus_Date").ToString).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 5

                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Weight").ToString)
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Volume").ToString)
                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Item_Package_Index").ToString
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

                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Customer_Index").ToString
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransfer("DocumentType_Index").ToString


                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransfer("str1").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransfer("str2").ToString


                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Order_Index").ToString
                        .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("Order_date")).ToString("yyyy/MM/dd")
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransfer("OrderItem_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransfer("ProductType_Index").ToString

                        .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransfer("To_Location_Alias").ToString
                        .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransfer("From_Location_Alias").ToString

                        '06/05/2009
                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal

                        '15/08/2013
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("TransferStatus_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("TransferStatusLocation_Index").ToString
                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = odrTransfer("TAG_Index").ToString


                        .Parameters.Add("@ERP_Location_From", SqlDbType.VarChar, 200).Value = odrTransfer("ERP_Location_TO").ToString
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.VarChar, 200).Value = odrTransfer("ERP_Location_From").ToString

                        .Parameters.Add("@TAG_IndexNew", SqlDbType.VarChar, 200).Value = strNew_Tag_Index
                        .Parameters.Add("@Tag_NoNew", SqlDbType.VarChar, 200).Value = strNew_Tag_No

                    End With

                    DBExeNonQuery(strSQL, _Connection, _myTrans)


                    ' 27/04/2009 Insert tb_AssetTransaction
                    ' *********************************
                    ' *** Insert tb_AssetTransaction ***
                    ' *** Insert Record OUT from Old_ItemStatus 
                    If odrTransfer("AssetLocationBalance_Index").ToString <> "" Then

                        strSQL = " INSERT INTO tb_AssetTransaction  "
                        strSQL &= " (AssetTransaction_Index,AssetLocationBalance_Index,AssetTransaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_out,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Asset_No,Qty_Item_Out,OrderItem_Price_Out,Item_Package_Index) VALUES "
                        strSQL &= " (@AssetTransaction_Index,@AssetLocationBalance_Index,@AssetTransaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_out,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Asset_No,@Qty_Item_Out,@OrderItem_Price_Out,@Item_Package_Index)"

                        ' **** Manage Balance ***
                        With SQLServerCommand
                            .Parameters.Clear()
                            '  **** Generate OrderItemLocation_Index  ***
                            Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value(_Connection, _myTrans, "AssetTransaction_Index")
                            objDBIndex = Nothing
                            ' *******************************************
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = odrTransfer("AssetLocationBalance_Index").ToString
                            .Parameters.Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = odrTransfer("TransferStatus_No").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransfer("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("From_Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("To_Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = odrTransfer("Tag_No").ToString
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransfer("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Old_ItemStatus_Index").ToString
                            .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("New_ItemStatus_Index").ToString
                            .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString) 'iTotal_Qty

                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransfer("Serial_No").ToString
                            .Parameters.Add("@Asset_No", SqlDbType.VarChar, 50).Value = odrTransfer("Asset_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            ' Order_Date
                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("TransferStatus_Date").ToString).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 5

                            .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                            .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Weight").ToString)
                            .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Volume").ToString)
                            .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                            .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Item_Package_Index").ToString

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

                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Customer_Index").ToString
                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransfer("DocumentType_Index").ToString

                            .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransfer("str4").ToString
                            .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransfer("str5").ToString


                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Order_Index").ToString
                            .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("Order_date")).ToString("yyyy/MM/dd")
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransfer("OrderItem_Index").ToString
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransfer("ProductType_Index").ToString

                            .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransfer("To_Location_Alias").ToString
                            .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransfer("From_Location_Alias").ToString

                        End With

                        DBExeNonQuery(strSQL, _Connection, _myTrans)
                    End If

                    ' *** Call Function Get Balance ***
                    objBal = New WMS_STD_Master_Datalayer.CalculateBalance
                    ' *** Qty ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index)
                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot)
                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, New_ItemStatus_Index)

                    ' *** Weight ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index)
                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot)
                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, New_ItemStatus_Index)

                    ' *** Volume ***
                    ' *** Need To using New_ItemStatus_Index for Parameter *** 
                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(_Connection, _myTrans, Customer_Index, Sku_Index)
                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot)
                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, New_ItemStatus_Index)

                    '06/05/2009
                    Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, odrTransfer("From_Location_Index").ToString)
                    Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, odrTransfer("From_Location_Index").ToString)
                    Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(_Connection, _myTrans, Customer_Index, Sku_Index, Plot, New_ItemStatus_Index, odrTransfer("From_Location_Index").ToString)

                    objBal = Nothing

                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    ' *** Insert Record IN from New_ItemStatus ***
                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_In,Weight_In,Volume_In,Qty_Item_In,OrderItem_Price_In,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,Item_Package_Index,DocumentPlan_Index,DocumentPlanItem_Index,TAG_Index,ERP_Location_From,ERP_Location_TO) VALUES "
                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_In,@Weight_In,@Volume_In,@Qty_Item_In,@OrderItem_Price_In,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@Item_Package_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@TAG_Index,@ERP_Location_From,@ERP_Location_TO)"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value(_Connection, _myTrans, "Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = odrTransfer("TransferStatus_No").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransfer("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("From_Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("To_Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransfer("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Old_ItemStatus_Index").ToString
                        .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("New_ItemStatus_Index").ToString
                        .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)

                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransfer("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        ' Order_Date
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("TransferStatus_Date").ToString).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 5

                        .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                        .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Weight").ToString)
                        .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Volume").ToString)
                        .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                        .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Item_Package_Index").ToString
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

                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = odrTransfer("Customer_Index").ToString
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransfer("DocumentType_Index").ToString

                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransfer("str4").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransfer("str5").ToString


                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Order_Index").ToString
                        .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("Order_date")).ToString("yyyy/MM/dd")
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransfer("OrderItem_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransfer("ProductType_Index").ToString


                        .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransfer("To_Location_Alias").ToString
                        .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransfer("From_Location_Alias").ToString

                        '06/05/2009
                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal

                        '15/08/2013
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("TransferStatus_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("TransferStatusLocation_Index").ToString
                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = strNew_Tag_Index 'odrTransfer("TAG_Index").ToString




                        .Parameters.Add("@ERP_Location_From", SqlDbType.VarChar, 200).Value = odrTransfer("ERP_Location_TO").ToString
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.VarChar, 200).Value = odrTransfer("ERP_Location_From").ToString


                    End With

                    DBExeNonQuery(strSQL, _Connection, _myTrans)



                    ' *********************************
                    ' 27/04/2009 Insert tb_AssetTransaction
                    ' *** Insert Record IN from New_ItemStatus ***
                    If odrTransfer("AssetLocationBalance_Index").ToString <> "" Then

                        strSQL = " INSERT INTO tb_AssetTransaction  "
                        strSQL &= " (AssetTransaction_Index,AssetLocationBalance_Index,AssetTransaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_Index,ItemDefinition_Index,DocumentType_Index,Referent_1,Referent_2,Qty_In,Order_Index,Order_Date,OrderItem_Index,ProductType_Index,New_ItemStatus_Index,Location_Alias_To,Location_Alias_From,Serial_No,Asset_No) VALUES "
                        strSQL &= " (@AssetTransaction_Index,@AssetLocationBalance_Index,@AssetTransaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_Index,@ItemDefinition_Index,@DocumentType_Index,@Reference1,@Reference2,@Qty_In,@Order_Index,@Order_Date,@OrderItem_Index,@ProductType_Index,@New_ItemStatus_Index,@Location_Alias_To,@Location_Alias_From,@Serial_No,@Asset_No)"

                        ' **** Manage Balance ***

                        With SQLServerCommand

                            .Parameters.Clear()
                            '  **** Generate OrderItemLocation_Index  ***
                            Dim objDBIndex As New Sy_AutoNumber
                            .Parameters.Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value(_Connection, _myTrans, "AssetTransaction_Index")
                            objDBIndex = Nothing
                            ' *******************************************
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = odrTransfer("AssetLocationBalance_Index").ToString
                            .Parameters.Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = odrTransfer("TransferStatus_No").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Sku_Index").ToString
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = odrTransfer("Lot_No").ToString
                            .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("From_Location_Index").ToString
                            .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = odrTransfer("To_Location_Index").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = strNew_Tag_No
                            .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = odrTransfer("PLot").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Old_ItemStatus_Index").ToString
                            .Parameters.Add("@New_ItemStatus_Index", SqlDbType.VarChar, 13).Value = odrTransfer("New_ItemStatus_Index").ToString
                            .Parameters.Add("@Move_Qty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)

                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = odrTransfer("Serial_No").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            ' Order_Date
                            .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("TransferStatus_Date").ToString).ToString("yyyy/MM/dd")
                            ' Process_id 
                            .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 5

                            .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                            .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Weight").ToString)
                            .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Volume").ToString)
                            .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                            .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)
                            .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Item_Package_Index").ToString

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

                            .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 50).Value = odrTransfer("Customer_Index").ToString
                            .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = odrTransfer("DocumentType_Index").ToString

                            .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = odrTransfer("str4").ToString
                            .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = odrTransfer("str5").ToString

                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = odrTransfer("Order_Index").ToString
                            .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = CDate(odrTransfer("Order_date")).ToString("yyyy/MM/dd")
                            .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = odrTransfer("OrderItem_Index").ToString
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = odrTransfer("ProductType_Index").ToString

                            .Parameters.Add("@Location_Alias_To", SqlDbType.VarChar, 50).Value = odrTransfer("To_Location_Alias").ToString
                            .Parameters.Add("@Location_Alias_From", SqlDbType.VarChar, 50).Value = odrTransfer("From_Location_Alias").ToString
                            .Parameters.Add("@Asset_No", SqlDbType.VarChar, 50).Value = odrTransfer("Asset_No").ToString

                        End With

                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                        'Update tb_AssetLocationBalance
                        strSQL = " UPDATE tb_AssetLocationBalance SET "
                        strSQL &= " Location_Index = @Location_Index"
                        strSQL &= " ,LocationBalance_Index = @LocationBalance_Index"
                        strSQL &= " ,ItemStatus_Index = @ItemStatus_Index"
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
                            .Parameters.Add("@Location_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("To_Location_Index").ToString
                            .Parameters.Add("@LocationBalance_Index", SqlDbType.NVarChar, 100).Value = strNew_LocationBalance_Index

                            .Parameters.Add("@ItemStatus_Index", SqlDbType.NVarChar, 100).Value = odrTransfer("New_ItemStatus_Index").ToString

                            .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = odrTransfer("Str1").ToString
                            .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = odrTransfer("Str2").ToString
                            .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = odrTransfer("Str3").ToString
                            .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = odrTransfer("Str4").ToString
                            .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = odrTransfer("Str5").ToString

                            .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = odrTransfer("Flo1").ToString
                            .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = odrTransfer("Flo2").ToString
                            .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = odrTransfer("Flo3").ToString
                            .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = odrTransfer("Flo4").ToString
                            .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = odrTransfer("Flo5").ToString
                            .Parameters.Add("@Tag_No", SqlDbType.NVarChar, 50).Value = strNew_Tag_No

                            .Parameters.Add("@Asset_No", SqlDbType.NVarChar, 50).Value = odrTransfer("Asset_No").ToString
                            .Parameters.Add("@ReserveQty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                            .Parameters.Add("@ReserveWeight", SqlDbType.Float, 8).Value = CDec(odrTransfer("Weight").ToString)
                            .Parameters.Add("@ReserveVolume", SqlDbType.Float, 8).Value = CDec(odrTransfer("Volume").ToString)
                            .Parameters.Add("@ReserveQty_Item", SqlDbType.Float, 8).Value = CDec(odrTransfer("Item_Qty").ToString)
                            .Parameters.Add("@ReserveOrderItem_Price", SqlDbType.Float, 8).Value = CDec(odrTransfer("Price").ToString)

                            .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@update_date", SqlDbType.DateTime).Value = Now.ToString("yyyy/MM/dd hh:mm:ss")
                            .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.NVarChar, 13).Value = odrTransfer("AssetLocationBalance_Index").ToString
                        End With
                        DBExeNonQuery(strSQL, _Connection, _myTrans)


                        '--- 18/05/2009 (Auto Return Borrow Case Asset Only) 
                        strSQL = " UPDATE tb_BorrowLocation SET "
                        strSQL &= " Return_Qty = Return_Qty + @Return_Qty"
                        strSQL &= " WHERE "
                        strSQL &= " AssetLocationBalance_Index = @AssetLocationBalance_Index"
                        strSQL &= " AND Return_Qty + @Return_Qty <= @Return_Qty "
                        strSQL &= " AND Status = 2 "

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@Return_Qty", SqlDbType.Float, 8).Value = CDec(odrTransfer("Total_Qty").ToString)
                            .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.NVarChar, 13).Value = odrTransfer("AssetLocationBalance_Index").ToString
                        End With

                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                    End If


                    DS.Tables("LB").Clear()

                Next
            End If


            ' *** Update tb_TransferStatus ***
            strSQL = "UPDATE tb_TransferStatus "
            strSQL &= " SET Status = 2 "
            strSQL &= " WHERE  TransferStatus_Index ='" & TransferStatus_Index & "' "

            DBExeNonQuery(strSQL, _Connection, _myTrans)

            '--- 18/05/2009 (Auto Return Borrow Case Asset Only) 
            ' --- Update Status tb_Borrow
            ' --- Return Someone
            strSQL = "  UPDATE tb_Borrow SET"
            strSQL &= "  	Status = 3"
            strSQL &= "  WHERE Borrow_index IN "
            strSQL &= "  	("
            strSQL &= "  		Select Borrow_index "
            strSQL &= "  		From tb_BorrowLocation"
            strSQL &= "  		Group by Borrow_index "
            strSQL &= "  		Having (Sum(Total_Qty) - Sum(Return_Qty)) >= 1 And Sum(Return_Qty) > 0"
            strSQL &= "  	)"

            DBExeNonQuery(strSQL, _Connection, _myTrans)

            ' --- Return All
            strSQL = "  UPDATE tb_Borrow SET"
            strSQL &= "  	Status = 4"
            strSQL &= "  WHERE Borrow_index IN "
            strSQL &= "  	("
            strSQL &= "  		Select Borrow_index "
            strSQL &= "  		From tb_BorrowLocation"
            strSQL &= "  		Group by Borrow_index "
            strSQL &= "  		Having (Sum(Total_Qty) - Sum(Return_Qty)) = 0 And Sum(Return_Qty) > 0"
            strSQL &= "  	)"

            DBExeNonQuery(strSQL, _Connection, _myTrans)





            If IsNotPassTransaction Then _myTrans.Commit()
            Return ""
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getRatio(ByVal Sku_Index As String, ByVal Package_Index As String, _
       Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Decimal


        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Decimal = 0
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            Dim strSQL As String = ""
            strSQL = "  SELECT  ms_SKURatio.Ratio "
            strSQL &= " FROM     ms_SKURatio INNER JOIN "
            strSQL &= " ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "     ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= " WHERE ms_SKURatio.Sku_Index ='" & Sku_Index & "' AND ms_SKURatio.Package_Index='" & Package_Index & "' and ms_SKU.status_id != -1"


            Result = DBExeQuery_Scalar(strSQL, _Connection, _myTrans)

            If IsNumeric(Result) = False Then
                Result = 0
            End If


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

End Class

Public Class GI_Logic : Inherits DBType_SQLServer

    Private _Packing_index As String = ""
    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
        Reservation = 30
    End Enum

    Public Function InsertWTHWTHI(ByVal _objHeader As tb_Withdraw, ByVal _objItemCollection As List(Of tb_WithdrawItem), ByVal _objItemCollectionWITL As List(Of tb_WithdrawItemLocation), ByVal objPalletTypeCollection As List(Of tb_PalletType_History), _
    Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String


        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            Dim Withdraw_Index As String = ""


            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update ms_Location set Location_Index = '0010000000001' where Location_Index = '0010000000001' ", _Connection, _myTrans)



            Withdraw_Index = InsertWTH(_objHeader, _Connection, _myTrans)
            InsertWTHI(_objHeader.Withdraw_Date, _objItemCollection, _objItemCollectionWITL, objPalletTypeCollection, _Connection, _myTrans)




            If IsNotPassTransaction Then _myTrans.Commit()
            Return Withdraw_Index
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function InsertWTH(ByVal _objHeader As tb_Withdraw, _
    Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String


        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            ' ***** Step 1 Insert Header : tb_withdraw ***
            ' ******************************************************************
            strSQL = " INSERT INTO tb_Withdraw ( Withdraw_Index,Withdraw_No,Withdraw_Date,Customer_Index,Department_Index,DocumentType_Index, "
            strSQL &= " Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,"
            strSQL &= " Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Contact_Name,Comment, "
            strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_date,add_by,add_branch,"
            strSQL &= " Customer_Shipping_Index,Driver_Index,Round,Leave_Time,Factory_In,Factory_Out,Return_Time,SO_No,Invoice_No,ASN_No,Departure_Date,Arrival_Date,"
            strSQL &= "  Vassel_Name, Flight_No, Vehicle_No, Transport_by, Origin_Port_Id, Origin_Country_Id, Destination_Port_Id, Destination_Country_Id, Terminal_Id,HandlingType_Index,ApprovedBy_Name,Checker_Name,Shipper_Index,Withdraw_Type,DistributionCenter_Index"
            strSQL &= " ) "
            strSQL &= " Values "
            strSQL &= "  ( @Withdraw_Index,@Withdraw_No,@Withdraw_Date,@Customer_Index,@Department_Index,@DocumentType_Index,"
            strSQL &= " @Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,"
            strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10, @Contact_Name,@Comment,"
            strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,getdate(),@add_by,@add_branch,"
            strSQL &= " @Customer_Shipping_Index,@Driver_Index,@Round,@Leave_Time,@Factory_In,@Factory_Out,@Return_Time,@SO_No,@Invoice_No,@ASN_No,@Departure_Date,@Arrival_Date,"
            strSQL &= " @Vassel_Name, @Flight_No, @Vehicle_No, @Transport_by, @Origin_Port_Id, @Origin_Country_Id, @Destination_Port_Id, @Destination_Country_Id, @Terminal_Id,@HandlingType_Index,@ApprovedBy_Name,@Checker_Name,@Shipper_Index,@Withdraw_Type,@DistributionCenter_Index"
            strSQL &= " ) "

            _objHeader.Withdraw_Date = CDate(_objHeader.Withdraw_Date.ToString("yyyy/MM/dd"))

            If Trim(_objHeader.Withdraw_No) = "" Then
                'Dim objDocumentNumber As New Sy_DocumentNumber
                '_objHeader.Withdraw_No = (objDocumentNumber.getAuto_DocumentNumber(_Connection, _myTrans, "WTH", "Withdraw_No", "tb_Withdraw"))
                _objHeader.Withdraw_No = New Sy_DocumentNumber_New().Auto_DocumentType_Number(_objHeader.DocumentType_Index, "", _objHeader.Withdraw_Date, _Connection, _myTrans)

                'objDocumentNumber = Nothing
            End If


            If _objHeader.Withdraw_Index.Trim.Length = 0 Then
                _objHeader.Withdraw_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "Withdraw_Index")
            End If

            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objHeader.Withdraw_Index
                .Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = _objHeader.Withdraw_No

                .Parameters.Add("@Withdraw_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Withdraw_Date
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Parameters.Add("@Contact_Name", SqlDbType.NVarChar, 50).Value = _objHeader.Contact_Name
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objHeader.Comment
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No2
                .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No3
                .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No4
                .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No5
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objHeader.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objHeader.Str10
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objHeader.Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID


                'Add New 19/6/2008
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Customer_Shipping_Index
                .Parameters.Add("@Driver_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Driver_Index
                .Parameters.Add("@Round", SqlDbType.SmallDateTime, 4).Value = _objHeader.Round
                .Parameters.Add("@Leave_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Leave_Time
                .Parameters.Add("@Factory_In", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_In
                .Parameters.Add("@Factory_Out", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_Out
                .Parameters.Add("@Return_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Return_Time


                '--- AddNew 19/05/2009

                .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = _objHeader.Vassel_Name
                .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = _objHeader.Flight_No
                .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = _objHeader.Vehicle_No
                .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = _objHeader.Transport_by
                .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Port_Id
                .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Country_Id
                .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Port_Id
                .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Country_Id
                .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = _objHeader.Terminal_Id
                .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index

                .Parameters.Add("@So_No", SqlDbType.VarChar, 50).Value = _objHeader.SO_No
                .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objHeader.Invoice_No
                .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objHeader.ASN_No

                .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Departure_Date
                .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Arrival_Date

                .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = _objHeader.ApprovedBy_Name
                .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = _objHeader.Checker_Name
                .Parameters.Add("@Shipper_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Shipper_Index
                .Parameters.Add("@Withdraw_Type", SqlDbType.Bit, 1).Value = _objHeader.Withdraw_Type
                .Parameters.Add("@DistributionCenter_Index", SqlDbType.NVarChar, 13).Value = New clsUserByDC().GetDistributionCenter_IndexByUser(_Connection, _myTrans)
            End With

            ' *****************************************************************

            DBExeNonQuery(strSQL, _Connection, _myTrans)






            ' *** Step 4 update New Withdraw_Index with tb_Withdraw ***
            strSQL = " UPDATE tb_Withdraw SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "' ,Status=1 "
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"


            DBExeNonQuery(strSQL, _Connection, _myTrans)

            strSQL = " UPDATE tb_PalletType_History SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "' "
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"


            DBExeNonQuery(strSQL, _Connection, _myTrans)

            ' *** Step 5 update New Order_Index with tb_WithdrawItem ***
            strSQL = " UPDATE tb_WithdrawItem SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "' ,Status=1 "
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"


            DBExeNonQuery(strSQL, _Connection, _myTrans)

            If _Packing_index <> "" Then

                strSQL = " insert into tb_PackingWithdraw(Packing_Index,Withdraw_Index)"
                strSQL &= " values ('" & Me._Packing_index & "','" & _objHeader.Withdraw_Index & "')"
            End If

            DBExeNonQuery(strSQL, _Connection, _myTrans)

            ' *** Step 5 update New Order_Index with tb_WithdrawItem ***
            strSQL = " UPDATE tb_WithdrawItemLocation SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "'"
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"


            DBExeNonQuery(strSQL, _Connection, _myTrans)

            If _Packing_index <> "" Then
                strSQL = " insert into tb_PackingWithdraw(Packing_Index,Withdraw_Index)"
                strSQL &= " values ('" & Me._Packing_index & "','" & _objHeader.Withdraw_Index & "')"
            End If


            DBExeNonQuery(strSQL, _Connection, _myTrans)

            strSQL = " UPDATE tb_WithDrawTruckOut SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"

            DBExeNonQuery(strSQL, _Connection, _myTrans)

            strSQL = " UPDATE tb_WithDrawSN SET  "
            strSQL &= " Withdraw_Index='" & _objHeader.Withdraw_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Withdraw_Index='" & _objHeader.Withdraw_Index & "'"


            DBExeNonQuery(strSQL, _Connection, _myTrans)








            Dim oAssign As New tb_AssignJob_New
            With oAssign
                .User_Index = "0010000000001"
                .Assign_Date = Now
                .DocumentPlan_No = _objHeader.Withdraw_No
                .DocumentPlan_Index = _objHeader.Withdraw_Index
                .Plan_Process = 2
                .Priority = 3
                .AssignJob_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "AssignJob_Index")
                .InsertData(_Connection, _myTrans)
            End With




            If IsNotPassTransaction Then _myTrans.Commit()
            Return _objHeader.Withdraw_Index
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function InsertWTHI(ByVal Withdraw_Date As Date, ByVal _objItemCollection As List(Of tb_WithdrawItem), ByVal _objItemCollectionWITL As List(Of tb_WithdrawItemLocation), ByVal objPalletTypeCollection As List(Of tb_PalletType_History), _
    Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""

            'DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", _Connection, _myTrans)
            'DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", _Connection, _myTrans)
            'DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", _Connection, _myTrans)
            'DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", _Connection, _myTrans)
            'DBExeNonQuery(" update ms_Location set Location_Index = '0010000000001' where Location_Index = '0010000000001' ", _Connection, _myTrans)



            ' **** Step 2 Insert ProductItem  : tb_WithdrawItem 
            For Each _objItem As tb_WithdrawItem In _objItemCollection

                strSQL = " INSERT INTO tb_WithdrawItem (WithdrawItem_Index,Withdraw_Index,Sku_Index,Package_Index,PLot,ItemStatus_Index ,Serial_No"
                strSQL &= " ,Qty,Total_Qty,Plan_Qty,Ratio,Plan_Total_Qty,Weight,Volume,Str1,Str2,Str3,Str4,Str5"
                strSQL &= " ,Flo1,Flo2,Flo3,Flo4,Flo5,Status,add_by,add_branch"
                strSQL &= " ,Item_Qty,Price,Item_Package_Index,Declaration_No,Invoice_No,HandlingType_Index,ItemDefinition_Index,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,AssetLocationBalance_Index,DocumentPlan_Index"
                strSQL &= ",Tax1,Tax2,Tax3,Tax4,Tax5 ,HS_Code,ItemDescription,Seq,Consignee_Index,Str6,Str7,Str8,Str9,Str10,OrderItem_Index,NewItemFlag,ERP_Location)"
                strSQL &= " Values "
                strSQL &= "( @WithdrawItem_Index,@Withdraw_Index,@Sku_Index,@Package_Index,@PLot,@ItemStatus_Index ,@Serial_No"
                strSQL &= " ,@Qty,@Total_Qty,@Plan_Qty,@Ratio,@Plan_Total_Qty,@Weight,@Volume,@Str1,@Str2,@Str3,@Str4,@Str5"
                strSQL &= " ,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Status,@add_by,@add_branch,@Item_Qty,@Price,@Item_Package_Index,@Declaration_No,@Invoice_No,@HandlingType_Index,@ItemDefinition_Index,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@AssetLocationBalance_Index,@DocumentPlan_Index"
                strSQL &= ",@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@Str6,@Str7,@Str8,@Str9,@Str10,@OrderItem_Index,0,@ERP_Location)"


                If _objItem.WithdrawItem_Index.Trim.Length = 0 Then
                    _objItem.WithdrawItem_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "WithdrawItem_Index")
                End If


                With SQLServerCommand
                    .Parameters.Clear()

                    .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = _objItem.WithdrawItem_Index
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objItem.Withdraw_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objItem.Sku_Index
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = _objItem.PLot
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemStatus_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Package_Index
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemDefinition_Index


                    ' *** Need to set Qty=0  and Total_Qty =0 
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                    ' ***************************************

                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Qty
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objItem.Ratio
                    .Parameters.Add("@Plan_Total_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objItem.Flo5
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItem.Serial_No
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = _objItem.Price

                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Item_Package_Index
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 100).Value = _objItem.Invoice_No
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = _objItem.Declaration_No
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index

                    '--- Plan WithDraw 
                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = _objItem.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = _objItem.DocumentPlan_No
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _objItem.AssetLocationBalance_Index
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index

                    'Dong_kk Tax,Seq,Consignee

                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = _objItem.Tax1
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = _objItem.Tax2
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = _objItem.Tax3
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = _objItem.Tax4
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = _objItem.Tax5
                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 100).Value = _objItem.HS_Code
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 400).Value = _objItem.ItemDescription
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = _objItem.Seq
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objItem.Consignee_Index

                    '11-02-2010 ja add Str6-Str10
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objItem.Str10

                    '16-02-2010 ja
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index
                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = _objItem.ERP_Location

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()

                    DBExeNonQuery(strSQL, _Connection, _myTrans)

                    ' ****************************************************************
                    '--- PlanWithDraw

                    If _objItem.Plan_Process <> -9 Then
                        StatusWithdraw_Document = _objItem.Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                strSQL = "  UPDATE tb_SalesOrderItem "
                                strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                                strSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                                strSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                                strSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "
                                'strSQL &= " , Status =3 "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", Withdraw_Date.ToString("yyyy/MM/dd"), _Connection, _myTrans, SQLServerCommand) Then
                                    strSQL &= " ,Last_Withdraw_Date='" & Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                                End If
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
                                'strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                                strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+(@Total_Qty / Ratio) "
                                strSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                                strSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                                strSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "
                                'strSQL &= " , Status =3 "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", Withdraw_Date.ToString("yyyy/MM/dd"), _Connection, _myTrans, SQLServerCommand) Then
                                    strSQL &= " ,Last_Withdraw_Date='" & Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                                End If
                                strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


                        End Select
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume

                        End With


                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                        'Update Status All Document  WithDraw
                        UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, _objItem.Plan_Process, _Connection, _myTrans)

                    End If


                End With

            Next


            ' --- ADD NEW By Dong_KK Need InSert To Transaction
            If _objItemCollectionWITL IsNot Nothing Then
                For Each _objItemWITL As tb_WithdrawItemLocation In _objItemCollectionWITL


                    strSQL = "Insert into tb_WithdrawItemLocation "
                    strSQL &= " (WithdrawItemLocation_Index,Withdraw_Index,WithdrawItem_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Tag_No,LocationBalance_Index,Location_Index,Serial_No,Qty,Package_Index,Total_Qty,Weight,Volume,Status,add_by,add_date,add_branch,Pallet_Qty,Item_Qty,Price,tagout_no,TAG_Index,ERP_Location,FEFO_AUTO) "
                    strSQL &= "  values("
                    strSQL &= "'" & _objItemWITL.WithdrawItemLocation_Index & "',"
                    strSQL &= "'" & _objItemWITL.Withdraw_Index & "',"
                    strSQL &= "'" & _objItemWITL.WithdrawItem_Index & "',"
                    strSQL &= "'" & _objItemWITL.Order_Index & "',"
                    strSQL &= "'" & _objItemWITL.Sku_Index & "',"
                    strSQL &= "'" & _objItemWITL.Lot_No & "',"
                    strSQL &= "'" & _objItemWITL.Plot & "',"
                    strSQL &= "'" & _objItemWITL.ItemStatus_Index & "',"
                    strSQL &= "'" & _objItemWITL.Tag_No & "',"
                    strSQL &= "'" & _objItemWITL.LocationBalance_Index & "',"
                    strSQL &= "'" & _objItemWITL.Location_Index & "' ,"
                    strSQL &= "'" & _objItemWITL.Serial_No & "',"
                    strSQL &= "'" & _objItemWITL.Qty & "',"
                    strSQL &= "'" & _objItemWITL.Package_Index & "',"
                    strSQL &= "'" & _objItemWITL.Total_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Weight & "',"
                    strSQL &= "'" & _objItemWITL.Volume & "',"
                    strSQL &= "'-9',"
                    strSQL &= "'" & WV_UserName & "',"
                    strSQL &= "getdate(),"
                    strSQL &= "'" & WV_Branch_ID & "',"
                    strSQL &= "'" & _objItemWITL.Pallet_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Item_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Price & "',"
                    strSQL &= "'" & _objItemWITL.TagOut_No & "',"
                    strSQL &= "'" & _objItemWITL.TAG_Index & "',"
                    strSQL &= "'" & _objItemWITL.ERP_Location & "',"
                    strSQL &= "'1')"

                    DBExeNonQuery(strSQL, _Connection, _myTrans)

                Next
            End If



            If IsNotPassTransaction Then _myTrans.Commit()
            Return ""
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function



    Public Sub ConfirmWTH(ByVal Withdraw_Index As String, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing)

        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            Dim strSQL As String = ""

            Dim Qty_Sku_Bal As Decimal = 0
            Dim Weight_Sku_Bal As Decimal = 0
            Dim Volume_Sku_Bal As Decimal = 0
            Dim Qty_PLot_Bal As Decimal = 0
            Dim Weight_PLot_Bal As Decimal = 0
            Dim Volume_PLot_Bal As Decimal = 0
            Dim Qty_ItemStatus_Bal As Decimal = 0
            Dim Weight_ItemStatus_Bal As Decimal = 0
            Dim Volume_ItemStatus_Bal As Decimal = 0

            Dim Qty_Sku_Location_Bal As Decimal = 0
            Dim Qty_ItemStatus_Location_Bal As Decimal = 0
            Dim Qty_PLot_Location_Bal As Decimal = 0


            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", _Connection, _myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", _Connection, _myTrans)


            ' *** 0. insert tb_jobWithdraw ***
            ' *** 1.  update status of Withdraw ***
            ' *** 2.  update tb_LocationBalance  ***
            ' *** 3.  insert tb_transaction --- bal 
            ' *** 4.  update tb_withdraw  ***


            ' *** Get data of Product ***

            'strSQL = "   SELECT  WIL.WithdrawItemLocation_Index,WIL.Withdraw_Index,WIL.Sku_Index,WIL.Lot_No,WIL.Location_Index,WIL.Tag_No,WIL.PLot,WIL.ItemStatus_Index,WIL.Total_Qty,WIL.Weight,WIL.Volume,WIL.Serial_No ,"
            'strSQL &= "   W.Withdraw_Date, W.DocumentType_Index, W.Customer_Index,WIL.Qty,WIL.Package_Index,W.Withdraw_No,WI.ItemDefinition_Index  "
            'strSQL &= " ,WI.str1 as Reference1,WI.str2 as Reference2" 'New 6-8-2008
            'strSQL &= "   FROM  tb_WithdrawItemLocation WIL INNER JOIN  "
            'strSQL &= "   tb_Withdraw W ON WIL.Withdraw_Index=W.Withdraw_Index INNER JOIN "
            'strSQL &= "   tb_WithdrawItem WI ON WIL.WithdrawItem_Index=WI.WithdrawItem_Index  "
            'strSQL &= "   WHERE WIL.Withdraw_Index ='" & Withdraw_Index & "' "

            ' Dong_Edit
            strSQL = " SELECT       * "
            strSQL &= " FROM        VIEW_WithDrawSave "
            strSQL &= " WHERE       Withdraw_Index ='" & Withdraw_Index & "' "

            With SQLServerCommand
                .Connection = _Connection
                .Transaction = _myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = _myTrans

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

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(_Connection, _myTrans, PICKING.enmPicking_Action.DELBALANCE_RESERVE, 2, Withdraw_Index, "ยืนยันรายการ", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                    Total_Qty, Weight, Volume, ItemQty, Price)
                    objPicking = Nothing


                    strSQL = "UPDATE tb_WithdrawItemLocation "
                    strSQL &= " SET status =-1 "
                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .Connection = _Connection
                        .Transaction = _myTrans
                        .ExecuteNonQuery()
                    End With



                    ' ********************* Manage PalletType  ******************************


                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,Ratio,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "' "  '& " where Tag_No ='" & Tag_No & "' "


                    With SQLServerCommand
                        .Connection = _Connection
                        .Transaction = _myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = _myTrans

                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl1")

                    If DS.Tables("tbl1").Rows.Count <> 0 Then
                        ' ***********************
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(_Connection, _myTrans, DS.Tables("tbl1").Rows(0).Item("LocationBalance_Index").ToString)
                        objCalBalance = Nothing
                        ' ***********************

                        If CDec(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                            ' *** update ms_PalletType *** 

                            strSQL = "UPDATE ms_PalletType "
                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                            DBExeNonQuery(strSQL, _Connection, _myTrans)


                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value(_Connection, _myTrans, "PalletType_History_Index")
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

                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                            DBExeNonQuery(strSQL, _Connection, _myTrans)

                            ' **********************************************



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value(_Connection, _myTrans, "PalletType_History_Index")
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

                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                            DBExeNonQuery(strSQL, _Connection, _myTrans)

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
                    Dim Order_Index As String = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                    Dim Order_Date As Date = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date").ToString)
                    Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                    Dim strSQL_Qty_Item_Bal As String = 0
                    Dim strSQL_OrderItem_Price_Bal As String = 0

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_index,DocumentType_Index,ItemDefinition_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,Serial_No,HandlingType_Index"
                    strSQL &= " ,Tax1_Out,Tax2_Out,Tax3_Out,Tax4_Out,Tax5_Out "
                    strSQL &= " ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index,ERP_Location_From,ERP_Location_TO )"
                    strSQL &= "  VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_index,@DocumentType_Index,@ItemDefinition_Index,@Reference1,@Reference2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_Out," & strSQL_Qty_Item_Bal & "-0,@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "-0,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@Serial_No,@HandlingType_Index"
                    strSQL &= " ,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out "
                    strSQL &= " ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@ERP_Location_From,@ERP_Location_TO )"
                    '
                    strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                    strSQL_Qty_Item_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                    strSQL_OrderItem_Price_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value(_Connection, _myTrans, "Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index").ToString

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
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
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
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

                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString

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
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal

                        .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 100).Value = ERP_Location
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 100).Value = ERP_Location

                    End With

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'connectDB()
                    'EXEC_Command()

                    DBExeNonQuery(strSQL, _Connection, _myTrans)

                    strSQL = Nothing


                    '***************************** ASSET TRANSACTION ********************************

                    Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                    Dim _AssetTransaction_Index As String = ""
                    If _AssetLocationBalance_Index <> "" Then

                        strSQL = " SELECT       * "
                        strSQL &= " FROM       tb_AssetLocationBalance"
                        strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                        'SetSQLString = strSQL
                        'SetCommandType = enuCommandType.Text
                        'SetEXEC_TYPE = EXEC.NonQuery
                        'connectDB()
                        'EXEC_Command()
                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                        Dim DSAS As New DataSet
                        DataAdapter.Fill(DSAS, "tblas")

                        If DSAS.Tables("tblas").Rows.Count <> 0 Then

                            For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1
                                Dim objDBAsIndex As New Sy_AutoNumber
                                _AssetTransaction_Index = objDBAsIndex.getSys_Value(_Connection, _myTrans, "AssetTransaction_Index")
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
                                    .Add("@Description", SqlDbType.VarChar, 200).Value = ""

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
                                'SetSQLString = strSQL
                                'SetCommandType = enuCommandType.Text
                                'SetEXEC_TYPE = EXEC.NonQuery
                                'connectDB()
                                'EXEC_Command()
                                DBExeNonQuery(strSQL, _Connection, _myTrans)


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

                                'SetSQLString = strSQL
                                'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                'EXEC_Command()
                                DBExeNonQuery(strSQL, _Connection, _myTrans)

                            Next
                        End If

                    End If

                    ' *********************************




                    ' *** Manage ms_location ***

                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()
                    DBExeNonQuery(strSQL, _Connection, _myTrans)

                    ' *** If Area of Location Emtry ***
                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Space_Used =0 "
                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()
                    DBExeNonQuery(strSQL, _Connection, _myTrans)
                    ' *********************************


                    ' *** End Manage ms_location ***

                    ' *** Update tb_withdraw & tb_jobWithdraw ***
                    strSQL = "UPDATE tb_Withdraw "
                    strSQL &= " SET status =2  "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()
                    DBExeNonQuery(strSQL, _Connection, _myTrans)


                    strSQL = "UPDATE tb_JobWithdraw "
                    strSQL &= " SET status =2 "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()
                    DBExeNonQuery(strSQL, _Connection, _myTrans)

                    ' *** Update tb_SalesOrder & tb_SalesOrderItem ***

                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString

                    If Plan_Process <> -9 Then
                        Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                        Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                        StatusWithdraw_Document = Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                ' '' '' ''krit update 28/12/2009 for mm
                                '' '' ''strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                                '' '' ''strSQL &= " FROM tb_SalesOrderItem  "
                                '' '' ''strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index and Status <> -1"
                                ' '' '' ''  case นี้ กรณี สองใบเบิก 1 SO ไม่รอง รับ   และ ไม่รองรับ Ratio ด้วย         <------ เบส 29 /10 /2013               

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=10 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = _Connection
                                    .Transaction = _myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = _myTrans
                                '   DS = New DataSet
                                If DS.Tables.Contains("SO") Then
                                    DS.Tables("SO").Clear()
                                End If
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        'SetSQLString = strSQL
                                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        'EXEC_Command()

                                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        'SetSQLString = strSQL
                                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        'EXEC_Command()
                                        DBExeNonQuery(strSQL, _Connection, _myTrans)
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
                                    .Connection = _Connection
                                    .Transaction = _myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = _myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        '****************  Case ผลิต    ****************  
                                        strSQL = "UPDATE tb_Packing "
                                        strSQL &= " SET status =5 "
                                        strSQL &= " WHERE Packing_Index ='" & DocumentPlan_Index & "'"

                                        'SetSQLString = strSQL
                                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        'EXEC_Command()
                                        DBExeNonQuery(strSQL, _Connection, _myTrans)
                                    End If

                                End If


                                '15-01-2010 ja update Status Reserve 
                            Case Withdraw_Document.Reserve
                                strSQL = " select Reserve_Index from tb_Reserve where Reserve_Index = @Reserve_Index  "
                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@Reserve_Index", SqlDbType.NVarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = _Connection
                                    .Transaction = _myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With
                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = _myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "Reserve")

                                If DS.Tables("Reserve").Rows.Count > 0 Then
                                    ' --- STEP 2: Update status in tb_Reserve = 3
                                    strSQL = "UPDATE tb_Reserve "
                                    strSQL &= " SET status =3 "
                                    strSQL &= " WHERE Reserve_Index ='" & DS.Tables("Reserve").Rows(0).Item("Reserve_Index").ToString & "'"

                                    'SetSQLString = strSQL
                                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    'EXEC_Command()
                                    DBExeNonQuery(strSQL, _Connection, _myTrans)
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
                                    .Connection = _Connection
                                    .Transaction = _myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = _myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        'SetSQLString = strSQL
                                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        'EXEC_Command()
                                        DBExeNonQuery(strSQL, _Connection, _myTrans)


                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        'SetSQLString = strSQL
                                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        'EXEC_Command()
                                        DBExeNonQuery(strSQL, _Connection, _myTrans)
                                    End If

                                End If
                        End Select


                        'Update Status All Document  WithDraw
                        'UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, StatusWithdraw_Document, myTrans)

                    End If



                Next
                DBExeNonQuery(String.Format("UPDATE tb_WithdrawItemSerial SET Status = 2 where Withdraw_Index = '{0}'", Withdraw_Index), _Connection, _myTrans, eCommandType.Text)
            Else
                Throw New Exception("Confirm Error!!!")
            End If





            If IsNotPassTransaction Then _myTrans.Commit()
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Sub


    Private Function Check_LastWithdraw_Date(ByVal pTableName As String, ByVal pColumn_Index As String, ByVal pStr_Index As String, ByVal pColumn_Date As String, ByVal pCheck_Date As String, ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As Boolean
        Dim strSQL As String
        Try

            '--- Check Before
            Dim tmpScalar As String

            strSQL = " SELECT  " & pColumn_Date
            strSQL &= " FROM " & pTableName
            strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"
            Try
                tmpScalar = DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text)
            Catch ex As Exception
                tmpScalar = ""
            End Try

            Select Case tmpScalar
                Case Nothing
                    Return True
                Case ""
                    Return True
                Case Else

                    strSQL = " SELECT  count(*) as gg"
                    strSQL &= " FROM " & pTableName
                    strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"
                    strSQL &= " AND " & pColumn_Date & "<'" & pCheck_Date & "'"

                    tmpScalar = DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text)


                    Select Case tmpScalar
                        Case Nothing
                            Return False
                        Case ""
                            Return False
                        Case Else
                            Return True
                    End Select
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub UpdatePlanDocument_Status(ByVal PlanDocument_Index As String, ByVal Process_Id As Withdraw_Document, ByVal pConnect As SqlClient.SqlConnection, ByVal myTrans As System.Data.SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        Try
            Select Case Process_Id
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
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

                            'สเสร็จสิ้น Fix ตอนยืนยันเอา
                            'ElseIf dblQty_WithDraw >= dblTotal_Qty Then
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =3 "
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

                            'No Action รอส่งเมื่อ Confrim เท่านั้น

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
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                        ElseIf dblQty_WithDraw < dblTotal_Qty Then
                            '--- สถานะ เบิกบางส่วน
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
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

    Public Function getRatio(ByVal Sku_Index As String, ByVal Package_Index As String, _
       Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Double


        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Decimal = 0
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            strSQL = "  SELECT  ms_SKURatio.Ratio "
            strSQL &= " FROM     ms_SKURatio INNER JOIN "
            strSQL &= " ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "     ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= " WHERE ms_SKURatio.Sku_Index ='" & Sku_Index & "' AND ms_SKURatio.Package_Index='" & Package_Index & "' and ms_SKU.status_id != -1"


            Dim _scalarOutput As String = ""
            _scalarOutput = DBExeQuery_Scalar(strSQL, _Connection, _myTrans)

            If _scalarOutput Is Nothing Then _scalarOutput = ""
            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Result = 0
            Else
                Result = CDec(_scalarOutput)
            End If

            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try


    End Function

    Public Function getRouteSubRoute(ByVal Customer_Shipping_Location_Index As String, _
       Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable


        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            strSQL = " select Route_Index,SubRoute_Index from ms_Customer_Shipping_Location WHERE  Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index  "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = Customer_Shipping_Location_Index
            End With

            Result = DBExeQuery(strSQL, _Connection, _myTrans)


            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try


    End Function

    Public Function getPrice1(ByVal SKU_Index As String, _
           Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Double


        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Double = 0
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            strSQL = " select isnull(Price1,0) as Price1 from ms_sku Where SKU_Index=@SKU_Index  "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SKU_Index", SqlDbType.VarChar).Value = SKU_Index
            End With

            Result = DBExeQuery_Scalar(strSQL, _Connection, _myTrans)


            If IsNumeric(Result) = False Then Result = 0


            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try


    End Function

    Public Function getVolWeight(ByVal SKU_Index As String, _
           Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable


        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""


            strSQL = "  SELECT TOP 1 PAC.Dimension_Hi,PAC.Dimension_Wd,PAC.Dimension_Len,D.Ratio,SKU.Sku_Index "
            strSQL &= " ,CASE WHEN ISNULL(((PAC.Dimension_Hi*PAC.Dimension_Wd*PAC.Dimension_Len) / D.Ratio ),0) = 0 THEN 0 ELSE CONVERT(DECIMAL(18,6),(((PAC.Dimension_Hi*PAC.Dimension_Wd*PAC.Dimension_Len) / D.Ratio ) / SKUR.Ratio)) END  AS Volum  "
            strSQL &= " ,CASE WHEN PAC.weight = 0 THEN 0 ELSE CONVERT(DECIMAL(18,6),(PAC.weight / SKUR.Ratio)) END AS Weight "
            strSQL &= " FROM MS_SKU SKU "
            strSQL &= " INNER JOIN MS_SKURATIO SKUR "
            strSQL &= " ON SKU.Sku_Index = SKUR.Sku_Index "
            strSQL &= " INNER JOIN MS_PACKAGE PAC"
            strSQL &= " ON SKUR.Package_Index = PAC.Package_Index "
            strSQL &= " INNER join ms_DimensionType D "
            strSQL &= " ON PAC.DimensionType_Index = D.DimensionType_Index "
            strSQL &= " WHERE PAC.isCASE = 1 AND PAC.status_Id <> -1 "
            strSQL &= " AND SKU.Sku_Index = @SKU_Index "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SKU_Index", SqlDbType.VarChar).Value = SKU_Index
            End With

            Result = DBExeQuery(strSQL, _Connection, _myTrans)

            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try


    End Function
End Class

Public Class Sy_DocumentNumber_New : Inherits DBType_SQLServer


    Public Function Auto_DocumentType_Number(ByVal strDocumentType_index As String, Optional ByVal strWhere As String = "", Optional ByVal pdateDocument_Date As Object = Nothing, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing)
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim strSQL As String = ""
            Dim strSQLAutoNumber As String = ""
            ' Dim objDr As SqlClient.SqlDataReader = Nothing
            ' Dim objDrAutoNumber As SqlClient.SqlDataReader = Nothing

            Dim iDocumentValue As Int64
            Dim StrFormat_Date As String = ""
            Dim strFormat_Running As String = ""
            Dim strFormat_Document As String = ""
            Dim strSys_Key_Name As String = ""
            Dim strDate As String = ""
            Dim NewDocumentValue As String = ""
            Dim NewDocumentID As String = ""

            Dim dtOdr As New DataTable
            Dim dtTemp As New DataTable

            'connectDB()

            strSQL = " select * "
            strSQL &= " From ms_DocumentType "
            strSQL &= "where DocumentType_Index ='" & strDocumentType_index & "'"

            'SetSQLString = strSQL
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'EXEC_Command()


            'objDr = GetDataReader
            ''''''''''Get DataReader To Datatable'''''''
            'dtTemp.Load(objDr)
            'dtOdr = dtTemp
            ''''''''''''''''''''''''''''''''''''''''''''

            dtOdr = DBExeQuery(strSQL, _Connection, _myTrans)


            If dtOdr.Rows.Count > 0 Then
                If IsDate(pdateDocument_Date) And dtOdr.Rows(0).Item("Sys_Key_Name").ToString = "" Then
                    Dim objDocumentNumber As New Sy_AutoyyyyMM
                    'Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
                    Try
                        'SQLServerCommand.Transaction = myTrans
                        NewDocumentID = objDocumentNumber.Auto_DocumentType_Number(_Connection, _myTrans, strDocumentType_index, pdateDocument_Date, "")

                        If IsNotPassTransaction Then
                            _myTrans.Commit()
                        End If

                        If NewDocumentID <> "" Then Return NewDocumentID

                    Catch ex As Exception
                        If IsNotPassTransaction Then
                            _myTrans.Rollback()
                        End If
                        Throw ex
                    Finally
                        If IsNotPassTransaction Then
                            SQLServerCommand.Connection.Close()
                        End If
                    End Try

                End If
            End If

            ' If objDr.Read Then
            If dtOdr.Rows.Count > 0 Then

                StrFormat_Date = dtOdr.Rows(0).Item("Format_Date").ToString 'objDr("Format_Date").ToString


                ''''''''''chk Format Date is single y '''''''''''''''
                Dim str2 As String
                Dim str1 As String
                Dim format_date As String = StrFormat_Date
                str1 = StrFormat_Date
                str2 = StrFormat_Date

                str1 = str1.Replace("Y", "")
                str1 = str1.Replace("y", "")

                str2 = str2.Replace("M", "")
                str2 = str2.Replace("m", "")
                str2 = str2.Replace("D", "")
                str2 = str2.Replace("d", "")

                If Not IsDate(pdateDocument_Date) Then
                    pdateDocument_Date = Now.ToString("yyyy/MM/dd")
                End If

                If pdateDocument_Date.ToString.Trim = "" Then
                    If str2 = "y" Or str2 = "Y" Then
                        Dim strY As String = Now.ToString("yyyy")
                        Dim strDM As String = Now.ToString(str1)
                        strY = strY.Substring(strY.Length - 1, 1)
                        format_date = Replace(format_date, str1, strDM)
                        format_date = Replace(format_date, str2, strY)
                        strDate = format_date
                    Else
                        strDate = Now.ToString(StrFormat_Date)
                    End If
                Else
                    If str2 = "y" Or str2 = "Y" Then
                        Dim strY As String = CDate(pdateDocument_Date).ToString("yyyy") 'Now.ToString("yyyy")
                        Dim strDM As String = CDate(pdateDocument_Date).ToString(str1) 'Now.ToString(str1)
                        strY = strY.Substring(strY.Length - 1, 1)
                        format_date = Replace(format_date, str1, strDM)
                        format_date = Replace(format_date, str2, strY)
                        strDate = format_date
                    Else
                        strDate = CDate(pdateDocument_Date).ToString(StrFormat_Date)
                    End If

                End If

                ''''''''''''''''''''''''''''''''''''''''''''''''

                ' strDate = Now.ToString(StrFormat_Date)
                strFormat_Running = dtOdr.Rows(0).Item("Format_Running").ToString 'objDr("Format_Running").ToString
                strFormat_Document = dtOdr.Rows(0).Item("Format_Document").ToString 'objDr("Format_Document").ToString
                strSys_Key_Name = dtOdr.Rows(0).Item("Sys_Key_Name").ToString 'objDr("Sys_Key_Name").ToString


                'objDr.Close()
                'objDr = Nothing
            End If

            If strSys_Key_Name <> "" Then
                If strWhere <> "" Then
                    strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & " '  And " & strWhere & ""
                Else
                    strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & " ' "
                End If

                'SetSQLString = strSQLAutoNumber
                'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
                'SetCommandType = DBType_SQLServer.enuCommandType.Text
                'EXEC_Command()
                'objDrAutoNumber = GetDataReader

                Dim dtAutoNumber As New DataTable
                dtAutoNumber = DBExeQuery(strSQLAutoNumber, _Connection, _myTrans)

                If dtAutoNumber.Rows.Count > 0 Then
                    iDocumentValue = CDbl(dtAutoNumber.Rows(0)("Sys_Value")) + 1
                    NewDocumentValue = Right(iDocumentValue.ToString(StrDup(strFormat_Running.Length, "0")), strFormat_Running.Length) 'iDocumentValue.ToString(StrDup(strFormat_Running.Length, "0"))

                    '--- Chk if Current Running = MaxRunning = Run NewRuning(00001)  --- '
                    Dim iMaxRunning As Integer = 9
                    Dim strMaxRunning As String
                    strMaxRunning = iMaxRunning.ToString(StrDup(strFormat_Running.Length, "9"))
                    If NewDocumentValue = strMaxRunning Then
                        iDocumentValue = 1
                        NewDocumentValue = iDocumentValue.ToString(StrDup(strFormat_Running.Length, "0"))
                    End If
                    '--- ---------------------------------------------------------  ---- '
                    ' Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber
                    'objDBIndex.UpdateSys_Value(strSys_Key_Name, iDocumentValue)
                    UpdateSys_Value(strSys_Key_Name, iDocumentValue, _Connection, _myTrans)


                End If




            End If


            If (strFormat_Document <> "") Then
                Dim strNewDocument As String
                strNewDocument = strFormat_Document
                strNewDocument = Replace(strNewDocument, "[Format_Date]", strDate)
                strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                NewDocumentID = strNewDocument


                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''' Chk Running No. By Month '''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim chk As Boolean = False
                If dtOdr IsNot Nothing Then
                    If Not dtOdr.Columns.Contains("RunningByMonth") Then
                        chk = False
                    Else
                        chk = dtOdr.Rows(0).Item("RunningByMonth")
                    End If
                Else
                    chk = False
                End If


                If chk = True Then
                    Dim strRunningName As String = strNewDocument.Substring(0, strNewDocument.Length - NewDocumentValue.Length)

                    strSQL = "select *  "
                    strSQL &= " from sy_TempRunning "
                    strSQL &= " where Running_Name ='" & strRunningName & "'"

                    ' Dim objms_ As New WMS_STD_Formula.SQLCommands
                    Dim objDT As New DataTable
                    Dim RunningNo As Integer = 1

                    'objms_.SQLComand(strSQL)
                    'objDT = objms_.GetDataTable

                    objDT = DBExeQuery(strSQL, _Connection, _myTrans)

                    If objDT.Rows.Count = 0 Then
                        'Insert & New Running 

                        strSQL = " INSERT INTO sy_temprunning  ("
                        strSQL &= "		Running_Name ,"
                        strSQL &= "     Running_no"
                        strSQL &= "	) VALUES ("
                        strSQL &= "	    '" & strRunningName & "',"
                        strSQL &= "     " & RunningNo & ""
                        strSQL &= " )"

                        'connectDB()
                        'SetSQLString = strSQL
                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                        ''SQLServerCommand.Transaction = myTrans
                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        'EXEC_Command()

                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                        '''''''''''' GEN DocumentNumber ''''''''''
                        NewDocumentValue = RunningNo.ToString(StrDup(strFormat_Running.Length, "0"))
                        strNewDocument = strFormat_Document
                        strNewDocument = Replace(strNewDocument, "[Format_Date]", strDate)
                        strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                        NewDocumentID = strNewDocument

                    Else
                        ' Update Running
                        RunningNo = objDT.Rows(0).Item("Running_no")
                        RunningNo += 1
                        strSQL = "  UPDATE  sy_temprunning SET "
                        strSQL &= " 	    Running_no ='" & RunningNo & "'"
                        strSQL &= " WHERE  Running_Name ='" & strRunningName & "'"

                        DBExeNonQuery(strSQL, _Connection, _myTrans)

                        'connectDB()
                        'SetSQLString = strSQL
                        'SetCommandType = DBType_SQLServer.enuCommandType.Text
                        '' SQLServerCommand.Transaction = myTrans
                        'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        'EXEC_Command()


                        '''''''''''' GEN DocumentNumber ''''''''''
                        NewDocumentValue = RunningNo.ToString(StrDup(strFormat_Running.Length, "0"))
                        strNewDocument = strFormat_Document
                        strNewDocument = Replace(strNewDocument, "[Format_Date]", strDate)
                        strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                        NewDocumentID = strNewDocument

                    End If
                End If
            End If

            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return NewDocumentID



        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try

    End Function

    Public Sub UpdateSys_Value(ByVal Sys_Key As String, ByVal Sys_Value As String, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing)

        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            Dim strSQL As String

            strSQL = " update Sy_AutoNumber set Sys_Value=@Sys_Value " & _
                     " WHERE Sys_Key =@Sys_Key "

            With SQLServerCommand.Parameters
                .Clear()
                .AddWithValue("@Sys_Value", Sys_Value)
                .AddWithValue("@Sys_Key", Sys_Key)
            End With


            DBExeNonQuery(strSQL, _Connection, _myTrans)

        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try


    End Sub

End Class


Public Class tb_AssignJob_New : Inherits DBType_SQLServer
    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _AssignJob_Index As String = ""

    Private _User_Index As String = ""
    Private _Assign_Date As Date = Date.Now
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date = Date.Now
    Private _update_branch As Integer
    Private _DocumentPlan_No As String = ""
    Private _DocumentPlan_Index As String = ""
    Private _Plan_Process As Integer = -9
    Private _Priority As Integer = -9
    Private _Comment As String = ""
    Private _status As Integer = -9
#Region " Properties Section "
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

    '*** Property Writeonly

    '*** Property Read and Write
    Public Property AssignJob_Index() As String
        Get
            Return _AssignJob_Index
        End Get
        Set(ByVal Value As String)
            _AssignJob_Index = Value
        End Set
    End Property


    Public Property User_Index() As String
        Get
            Return _User_Index
        End Get
        Set(ByVal Value As String)
            _User_Index = Value
        End Set
    End Property
    Public Property Assign_Date() As Date
        Get
            Return _Assign_Date
        End Get
        Set(ByVal Value As Date)
            _Assign_Date = Value
        End Set
    End Property
    Public Property Plan_Process() As Integer
        Get
            Return (Me._Plan_Process)
        End Get
        Set(ByVal value As Integer)
            Me._Plan_Process = value
        End Set
    End Property


    Public Property DocumentPlan_No() As String
        Get
            Return _DocumentPlan_No
        End Get
        Set(ByVal value As String)
            _DocumentPlan_No = value
        End Set
    End Property
    Public Property DocumentPlan_Index() As String
        Get
            Return (_DocumentPlan_Index)
        End Get
        Set(ByVal value As String)
            _DocumentPlan_Index = value
        End Set
    End Property

    Public Property Priority() As Integer
        Get
            Return _Priority
        End Get
        Set(ByVal Value As Integer)
            _Priority = Value
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return (_Comment)
        End Get
        Set(ByVal value As String)
            _Comment = value
        End Set
    End Property

    Public Property status() As Integer
        Get
            Return (Me._status)
        End Get
        Set(ByVal value As Integer)
            Me._status = value
        End Set
    End Property


#End Region

#Region " SELECT DATA "

    Public Sub getAssignJob_ByDocNo(ByVal strDocumentPlan_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "   select * "
            strSQL &= "  from  tb_AssignJob "
            strSQL &= "  where DocumentPlan_Index = '" & strDocumentPlan_Index & "'"

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    Public Function CHK_SkuExpDate(ByVal pSku_Index As String, ByVal pExpDate As String, ByVal pOrder_Date As String) As Boolean
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim Dt As DataTable
            strSQL &= " SELECT CAST(Odi.Exp_Date AS DATE) AS Exp_Date ,Odi.Mfg_Date ,Odi.Sku_Index  "
            strSQL &= " FROM tb_Order Od INNER JOIN tb_OrderItem Odi ON Od.Order_Index = Odi.Order_Index"
            strSQL &= " WHERE Odi.Sku_Index ='" & pSku_Index & "' "
            strSQL &= " AND CAST(Odi.Exp_Date AS DATE) ='" & pExpDate & "' "
            strSQL &= " AND CAST(Od.Order_Date AS DATE) ='" & pOrder_Date & "' "
            strSQL &= " GROUP BY Odi.Exp_Date ,Odi.Mfg_Date ,Odi.Sku_Index  "

            Dt = DBExeQuery(strSQL)
            Return Dt.Rows.Count
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function CHK_SkuMinQty(ByVal pSku_Index As String, ByVal pDay As Integer) As Boolean
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""


            strSQL &= " SELECT  CASE WHEN " & pDay & " < ISNULL(Min_Qty,0)  THEN 1 ELSE 0 END AS Bol   FROM ms_sku "
            strSQL &= " WHERE Sku_Index = '" & pSku_Index & "' AND status_id <> -1 "
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function CHK_SkuInSO(ByVal pSku_Index As String, ByVal pSalesOrder_Index As String) As Boolean
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim Dt As DataTable
            strSQL &= " SELECT SI.Sku_Index FROM tb_salesorder S "
            strSQL &= " INNER JOIN tb_salesOrderItem SI "
            strSQL &= " ON S.SalesOrder_Index = SI.SalesOrder_Index "
            strSQL &= " WHERE SI.Sku_Index ='" & pSku_Index & "' AND S.SalesOrder_Index ='" & pSalesOrder_Index & "'"
            strSQL &= " AND SI.Status <> -1  "

            Dt = DBExeQuery(strSQL)
            Return Dt.Rows.Count
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GET_RejectRef(ByVal SalesOrder_Index As String) As String
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim result As String = ""
            strSQL &= " select isnull(max(tooi.str2),'') as RejectCode from tb_Order too  "
            strSQL &= " inner join tb_OrderItem tooi on tooi.Order_Index = too.Order_Index "
            strSQL &= " where too.Ref_No1 = isnull((select top 1 SalesOrder_No from tb_SalesOrder where SalesOrder_Index = @SalesOrder_Index and Status <> -1),'_x_') "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With


            result = DBExeQuery_Scalar(strSQL)

            If result Is Nothing Then result = ""

            Return result
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GET_Reject(Optional ByVal isReturn As Boolean = False) As DataTable
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim Dt As DataTable
            strSQL &= " SELECT Reject_Code +'/'+Reject_Desc AS Reject_Desc,* FROM ms_Reject_SINO  "
            strSQL &= " WHERE Status <> -1 "
            If isReturn Then
                strSQL &= " AND Remark='Return' "
            Else
                strSQL &= " AND isnull(Remark,'')<>'Return' "
            End If
            strSQL &= " ORDER BY [Index] "


            Dt = DBExeQuery(strSQL)
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function CHK_ProductType(ByVal pProductType_Id As String) As String
        Try
            Dim strSQL As String = ""
            Dim strReturn As String = ""

            strSQL &= " SELECT ProductType_Index,ProductType_Id FROM ms_ProductType  "
            strSQL &= " WHERE Status_id <> -1 AND ProductType_Id = '" & pProductType_Id & "'"

            strReturn = DBExeQuery_Scalar(strSQL)
            Return strReturn
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa
    '''     - Select User  (ไม่เอา User ที่เป็น Customer,Supplier )
    '''     - Sum จำนวนเอกสารของแต่ละ User โดยเอาเฉพาะเอกสารใบเบิกที่มีสถานะเท่ากับ 'รอยืนยัน')
    ''' </remarks>
    Public Sub getUser_AssignJobWithDraw()


        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "  SELECT se_user.user_Index,se_user.UserName,se_user.userFullName"
            strSQL &= "     ,Sum_DocAssign =  CASE WHEN Sum_DocAssign IS NULL  THEN 0 "
            strSQL &= "         ELSE Sum_DocAssign  "
            strSQL &= "     End"
            strSQL &= " FROM se_user Left Join "
            strSQL &= " ("
            strSQL &= "     SELECT  count(*) as Sum_DocAssign,user_Index"
            strSQL &= "     FROM tb_AssignJob AssignJob LEFT JOIN tb_Withdraw ON "
            strSQL &= "             AssignJob.DocumentPlan_Index = WithDraw_Index"
            strSQL &= "     WHERE(Plan_Process = 2) AND tb_Withdraw.Status = 1 "
            strSQL &= "     GROUP BY AssignJob.user_Index) AssignJob_Withdraw On "
            strSQL &= " AssignJob_Withdraw.user_Index = se_user.user_Index"
            strSQL &= " WHERE se_user.status_id <> -1 AND( se_user.Customer_Index is null and se_user.Supplier_Index  is null) OR"
            strSQL &= " ( se_user.Customer_Index = '' and se_user.Supplier_Index  ='')"

            'update 31/08/2010 by krit
            'เพิ่มเรื่องมอบหมายงานให้ทุกคนได้โดยไม่ระบุ แต่ต้อง config DB ใน se_User.User_Index = '0010000000001' เท่านั้น
            strSQL &= " OR se_user.user_Index = '0010000000001'"


            SetSQLString = strSQL & strWhere


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
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa 
    '''      - Innert Data To tb_AssignJob
    ''' </remarks>

#Region " INSERT DATA "
    Public Function InsertData(Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Boolean
        Dim strSQL As String = ""

        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            strSQL = " INSERT INTO tb_AssignJob (AssignJob_Index,Assign_Date,User_Index,Priority,Comment,Plan_Process,DocumentPlan_Index,DocumentPlan_No "
            strSQL &= " ,add_by,add_date,add_branch "
            strSQL &= " ) "

            strSQL &= " Values "
            strSQL &= "  (@AssignJob_Index,@Assign_Date,@User_Index,@Priority,@Comment,@Plan_Process,@DocumentPlan_Index,@DocumentPlan_No "
            strSQL &= " ,@add_by,@add_date,@add_branch "
            strSQL &= " ) "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@AssignJob_Index", SqlDbType.VarChar, 13).Value = _AssignJob_Index
                .Parameters.Add("@Assign_Date", SqlDbType.SmallDateTime, 4).Value = CDate(_Assign_Date).ToString("yyyy/MM/dd")
                .Parameters.Add("@User_Index", SqlDbType.VarChar, 50).Value = _User_Index
                .Parameters.Add("@Plan_Process", SqlDbType.VarChar, 13).Value = _Plan_Process
                .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _DocumentPlan_Index
                .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 13).Value = _DocumentPlan_No
                .Parameters.Add("@Priority", SqlDbType.Int, 4).Value = Priority
                '   .Parameters.Add("@status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Comment", SqlDbType.VarChar, 13).Value = _Comment
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@add_date", SqlDbType.SmallDateTime, 4).Value = CDate(Now).ToString("yyyy/MM/dd")


            End With

            DBExeNonQuery(strSQL, _Connection, _myTrans)

            If IsNotPassTransaction Then _myTrans.Commit()

            Return True
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try


    End Function

#End Region

#Region " UPDATE DATA "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa 
    '''      - Update Data To tb_AssignJob By AssignJob_Index
    ''' </remarks>

    Public Function UpdateData() As Boolean
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try

            strSQL = " Update tb_AssignJob "
            strSQL &= " SET "
            strSQL &= " Assign_Date = @Assign_Date "
            strSQL &= ",User_Index = @User_Index "
            strSQL &= " ,Plan_Process = @Plan_Process "
            strSQL &= " ,DocumentPlan_Index = @DocumentPlan_Index "
            strSQL &= " ,DocumentPlan_No = @DocumentPlan_No "
            strSQL &= " ,Update_by = @Update_by "
            strSQL &= " ,Update_date = @Update_date "
            strSQL &= " ,Update_branch = @Update_branch "
            strSQL &= " WHERE AssignJob_Index = @AssignJob_Index "




            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@AssignJob_Index", SqlDbType.VarChar, 13).Value = _AssignJob_Index
                .Parameters.Add("@Assign_Date", SqlDbType.SmallDateTime, 4).Value = CDate(_Assign_Date).ToString("yyyy/MM/dd")
                .Parameters.Add("@User_Index", SqlDbType.VarChar, 50).Value = _User_Index
                .Parameters.Add("@Plan_Process", SqlDbType.VarChar, 13).Value = _Plan_Process
                .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _DocumentPlan_Index
                .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 13).Value = _DocumentPlan_No
                .Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@Update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@Update_date", SqlDbType.SmallDateTime, 4).Value = CDate(Now).ToString("yyyy/MM/dd")


            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '*** Commit transaction
            myTrans.Commit()
            Return True

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex

        Finally
            disconnectDB()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa 
    '''      - Set Priority To tb_AssignJob By AssignJob_Index
    ''' </remarks>
    Public Sub SetPriority(ByVal pPriority As Integer, ByVal pstrAssignJob_Index As String)
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try

            strSQL = " Update tb_AssignJob "
            strSQL &= " SET "
            strSQL &= " Priority = '" & pPriority & "' "

            strSQL &= " ,Update_by = '" & WV_UserName & "' "
            strSQL &= " ,Update_date = '" & CDate(Now).ToString("yyyy/MM/dd") & "' "
            strSQL &= " ,Update_branch =  '" & WV_Branch_ID & "' "
            strSQL &= " WHERE AssignJob_Index =  '" & pstrAssignJob_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
                .ExecuteNonQuery()
            End With
            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

#End Region



End Class

Public Class utilCultureInfo


    Public Sub DefaultCulture(Optional ByVal cul As String = "en-GB")
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(cul)
        System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture
    End Sub


End Class


Public Class Barcode_New





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



End Class
