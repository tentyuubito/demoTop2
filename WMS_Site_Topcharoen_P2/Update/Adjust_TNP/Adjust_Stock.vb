Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.DBType_SQLServer
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_OAW_Adjust_Datalayer
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer
Public Class Adjust_Stock : Inherits DBType_SQLServer

#Region "   Property   "

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

#End Region

#Region "   SELECT   "
    Public Sub Adjust_HeaderDetail(ByVal Adjust_Index As String)
        Try
            Dim strSQL As String = ""
            strSQL &= "SELECT *"
            strSQL &= "FROM ("
            strSQL &= " SELECT ms_Location.Location_Alias ,"
            strSQL &= " STATE = case (select count(*) "
            strSQL &= " 			  from tb_AdjustItemLocation ASJI "
            strSQL &= "     where(ASJI.Location_Index = ms_Location.Location_Index)"
            strSQL &= " 					and ASJI.Adjust_Index =tb_Adjust.Adjust_Index and ASJI.Status =-9 )"
            strSQL &= " 		when 0 then ''"
            strSQL &= " 		else 'OK' end"
            strSQL &= " 	,tb_Adjust.Adjust_No,ms_DocumentType.Description as DocumentType_Desc,tb_Adjust.Adjust_Date,ms_DocumentType.DocumentType_Index	"
            strSQL &= " FROM  tb_AdjustItemLocation Left outer join "
            strSQL &= " 	ms_Location on tb_AdjustItemLocation.Location_Index=ms_Location.Location_Index inner join"
            strSQL &= " 	tb_Adjust on tb_Adjust.Adjust_Index = tb_AdjustItemLocation.Adjust_Index inner join "
            strSQL &= " 	ms_DocumentType on ms_DocumentType.DocumentType_Index = tb_Adjust.DocumentType_Index "
            strSQL &= " Where    tb_Adjust.Adjust_Index = '" & Adjust_Index & "'"
            strSQL &= " Group By ms_Location.Location_Alias, ms_Location.Location_Index,tb_Adjust.Adjust_Index,tb_Adjust.Adjust_Date,tb_Adjust.Adjust_No,ms_DocumentType.Description,ms_DocumentType.DocumentType_Index"
            strSQL &= " ) ADJ"
            strSQL &= " ORDER BY STATE,Location_Alias"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function getlocation_index(ByVal Location_Alias As String) As String
        Try

            Dim strSQL, location_index As String
            strSQL = " select location_index "
            strSQL &= " from ms_location where Location_Alias='" & Location_Alias & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                location_index = _dataTable.Rows(0).Item("location_index")
            Else
                location_index = ""
            End If
            Return location_index
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getSku_index(ByVal pstrSku_Id As String) As String
        Try
            Dim strSQL, Sku_index As String
            strSQL = " select Sku_index "
            strSQL &= " from ms_sku where Sku_Id ='" & pstrSku_Id & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Sku_index = _dataTable.Rows(0).Item("Sku_index")
            Else
                Sku_index = ""
            End If
            Return Sku_index
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getSkuBarcode1_index(ByVal pstrBarcode1 As String) As String
        Try
            Dim strSQL, Sku_index As String
            strSQL = " select Sku_index "
            strSQL &= " from ms_sku where Barcode1 ='" & pstrBarcode1 & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Sku_index = _dataTable.Rows(0).Item("Sku_index")
            Else
                Sku_index = ""
            End If
            Return Sku_index
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getAdjustItemLocationIndex_NoITem(ByVal pAdjust_Index As String, ByVal pLocation_Alias As String, ByVal pstrSku_Id As String) As Boolean
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT Top 1 tb_AdjustItemLocation.* "
            strSQL &= " FROM    tb_AdjustItemLocation Left JOIN"
            strSQL &= "           ms_SKU ON tb_AdjustItemLocation.Sku_Index = ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "            ms_Location ON tb_AdjustItemLocation.Location_Index = ms_Location.Location_Index"
            strSQL &= " WHERE     tb_AdjustItemLocation.Adjust_Index = '" & pAdjust_Index & "'"
            strSQL &= " and      ms_Location.Location_Alias = '" & pLocation_Alias & "'"
            strSQL &= " And isnull(ms_SKU.Sku_Id,'') = '' and tb_AdjustItemLocation.Status = 1 "


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Sub GetAdjustItemLocation_SKU_ItemStatus(ByVal pAdjust_Index As String, ByVal pLocation_Alias As String, ByVal pSku_Barcode As String, ByVal pItemStatus_Index As String, ByVal IsAdjustSku As Boolean)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM   VIEW_AdjustItemLocation "
            strSQL &= " where Adjust_Index='" & pAdjust_Index & "' "
            If pSku_Barcode <> "" Then
                strSQL &= " and isnull(Barcode1,'')='" & pSku_Barcode & "'"
            End If
            If Not IsAdjustSku Then
                If pSku_Barcode <> "" Then
                    strSQL &= " AND  ItemStatus_Index='" & pItemStatus_Index & "'  and  Location_Alias='" & pLocation_Alias & "'"
                End If
                strSQL &= " AND Location_Alias='" & pLocation_Alias & "'"
            End If

            SetSQLString = strSQL & " Order by Qty_1st_Count desc"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub Adjust_Detail(ByVal Adjust_Index As String, ByVal pandWhere As String)
        Try
            Dim strSQL As String = ""
            strSQL = " SELECT * , CASE Status WHEN - 9 THEN 'OK.' ELSE '' END AS STATE "
            strSQL &= " FROM  tb_AdjustItemLocation inner join ms_Location on tb_AdjustItemLocation.Location_Index=ms_Location.Location_Index"
            strSQL &= " LEFT OUTER JOIN ms_SKU ON tb_AdjustItemLocation.SKU_Index = ms_SKU.SKU_Index "
            strSQL &= " LEFT OUTER JOIN ms_ItemStatus ON tb_AdjustItemLocation.ItemStatus_Index = ms_ItemStatus.ItemStatus_Index "
            strSQL &= " WHERE 1 = 1" & pandWhere

            If Adjust_Index <> "" Then
                strSQL &= " And tb_AdjustItemLocation.Adjust_Index ='" & Adjust_Index & "'"
            End If


            strSQL &= " ORDER BY ms_Location.Location_Alias,ms_SKU.SKU_ID,STATE "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub getAdjust_ItemDetail(ByVal pAdjust_Index As String, Optional ByVal pLocation_Alias As String = "", Optional ByVal pSku_Barcode As String = "")
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            ',ADJ.Adjust_Date,ADJ.Adjust_No,
            strSQL = "SELECT * "
            strSQL &= "  FROM VIEW_AdjustItemLocation"
            strSQL &= "  WHERE Adjust_Index ='" & pAdjust_Index & "' "

            If (Not pLocation_Alias = "") Then strSQL &= " AND     Location_Alias = '" & pLocation_Alias & "'"
            If (Not pSku_Barcode = "") Then strSQL &= " And     Sku_Id = '" & pSku_Barcode & "'"




            strSQL = strSQL & strWhere & " ORDER BY STATE,Location_Alias"

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
    Public Sub GetSKU_DetailByBarcode(ByVal pstrSku_Barcode1 As String)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " from ms_sku inner join ms_package on  ms_sku.Package_Index=ms_package.Package_Index inner join ms_Product on ms_Product.Product_Index=ms_sku.Product_Index  "
            strSQL &= " where Barcode1='" & pstrSku_Barcode1 & "' "

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

    Public Sub GetAdjustItemLocation_SKU(ByVal pAdjust_Index As String, ByVal pLocation_Alias As String, ByVal pSku_Barcode As String, ByVal IsAdjustSku As Boolean)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM   VIEW_AdjustItemLocation "
            strSQL &= " where Adjust_Index='" & pAdjust_Index & "' and isnull(Barcode1,'')='" & pSku_Barcode & "' "
            If Not IsAdjustSku Then
                strSQL &= " and  Location_Alias='" & pLocation_Alias & "'"
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


    Public Function ChkAdjustByLocationBarcode(ByVal pAdjust_Index As String, ByVal pLocation_Alias As String, ByVal pSku_Barcode As String, ByVal IsAdjustSku As Boolean) As Boolean
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT count(*) "
            strSQL &= " FROM    tb_AdjustItemLocation Left JOIN"
            strSQL &= "           ms_SKU ON tb_AdjustItemLocation.Sku_Index = ms_SKU.Sku_Index Left outer JOIN "
            strSQL &= "            ms_Location ON tb_AdjustItemLocation.Location_Index = ms_Location.Location_Index"
            strSQL &= " WHERE     tb_AdjustItemLocation.Adjust_Index = '" & pAdjust_Index & "'"
            If Not IsAdjustSku Then
                strSQL &= " and      ms_Location.Location_Alias = '" & pLocation_Alias & "'"
            End If

            If pSku_Barcode <> "" Then
                strSQL &= " And isnull(ms_SKU.Barcode1,'') = '" & pSku_Barcode & "'"
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case "0"
                    Return False
                Case Else
                    Return True
            End Select


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function ChkAdjustItem_Update(ByVal pAdjust_Index As String) As Boolean
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT count(*) "
            strSQL &= " FROM    tb_AdjustItemLocation "
            strSQL &= " WHERE     Adjust_Index = '" & pAdjust_Index & "' and Status = 1 "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case "0"
                    Return True
                Case Else
                    Return False
            End Select


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub getItemStatus_Adjust(ByVal pSku_Index As String, ByVal pLocation_Alias As String, ByVal pstrAdjust_Index As String)
        Try
            Dim strSQL As String = ""
            strSQL = " select *  "
            strSQL &= " from ms_ItemStatus where isnull(Status_Id,1) <> -1 "
            strSQL &= "  AND  ItemStatus_Index IN (SELECT  tb_AdjustItemLocation.ItemStatus_Index From tb_AdjustItemLocation inner join "
            strSQL &= "                                     Ms_Location ON Ms_Location.Location_Index = tb_AdjustItemLocation.Location_Index "
            strSQL &= "                             WHERE   tb_AdjustItemLocation.Sku_Index='" & pSku_Index & "'"
            strSQL &= "                             AND     Ms_Location.Location_Alias='" & pLocation_Alias & "'"
            strSQL &= "                             AND     tb_AdjustItemLocation.Adjust_Index = '" & pstrAdjust_Index & "')"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetItemStatus() As DataSet
        Try
            Dim oms_ItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
            Dim ods As New DataSet

            oms_ItemStatus.getItemStatus()
            ods.Tables.Add(oms_ItemStatus.GetDataTable)
            Return ods
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetDataBarcode_Sharp(ByVal pstrAnd As String)
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT * "
            strSQL &= " FROM ms_Barcode_Sharp "

            If pstrAnd <> "" Then
                strSQL &= " WHERE 1=1 " & pstrAnd
            End If

            strSQL &= " ORDER BY Description"

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

    Public Function GetMaxBarcode_Sharp() As Integer
        Dim strSQL As String = ""
        Dim strValue As String = ""
        Try

            strSQL = " SELECT Max(Barcode_Index) As Max_Barcode_Index"
            strSQL &= " FROM ms_Barcode_Sharp "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            strValue = GetScalarOutput

            If strValue Is Nothing Then
                Return 0
            Else
                Return strValue
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Sub GetLocation_By_SN(ByVal pstrBarcode As String, ByVal pstrSerial_No As String, ByVal pstrSKU_ID As String)
        Dim strSQL As String = ""
        Try

            'strSQL = " SELECT * "
            'strSQL &= " FROM tb_LocationBalance LB INNER JOIN ms_SKU SKU ON LB.SKU_Index = SKU.SKU_Index"
            'strSQL &= " INNER JOIN tb_AssetLocationBalance ALB ON ALB.LocationBalance_Index = LB.LocationBalance_Index"
            'strSQL &= " INNER JOIN ms_Location L ON LB.Location_Index = L.Location_Index"
            'strSQL &= " WHERE  ALB.Serial_No = '" & pstrSerial_No & "'"
            'strSQL &= " AND ALB.Status = 1"
            ''  strSQL &= " AND LB.Qty_Bal > 0 "


            strSQL = " SELECT * "
            strSQL &= " FROM  "
            strSQL &= "  tb_AssetLocationBalance ALB INNER JOIN ms_SKU SKU ON ALB.SKU_Index = SKU.SKU_Index "
            strSQL &= " INNER JOIN ms_Location L ON ALB.Location_Index = L.Location_Index"
            strSQL &= " WHERE  ALB.Serial_No = '" & pstrSerial_No & "'"
            strSQL &= " AND ALB.Status = 1"
            '  strSQL &= " AND LB.Qty_Bal > 0 "


            If pstrBarcode = "" Then
                strSQL &= " AND SKU.SKU_ID = '" & pstrSKU_ID & "'"
            Else
                strSQL &= " AND SKU.Sku_Id = '" & pstrBarcode & "'"
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


    Public Function ChkAdjustItemStatus(ByVal AdjustItemLocation_Index As String, ByVal Tstatus As Integer) As Boolean
        Dim strSQL As String = ""
        Dim strSQLSatus As String = ""

        Try
            strSQL = " select * "
            strSQL &= " from tb_AdjustItemLocation "
            strSQL &= " where AdjustItemLocation_Index  = '" & AdjustItemLocation_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count < 1 Then Return False
            If _dataTable.Rows(0).Item("Status") = Tstatus Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            disconnectDB()
        End Try

    End Function


#End Region

#Region "   INSERT   "


    Public Function insert_Adjust_Quantity(ByVal AdjustItemLocation_index As String, ByVal adjust_index As String, ByVal Count_1st_Total_Qty As Double, ByVal Location_Alias As String, ByVal sku_Barcode As String, ByVal piNewItem As Integer, ByVal ItemStatus_Index As String, ByVal isAdjustSku As Boolean, Optional ByVal Status As Integer = -9, Optional ByVal iSeq As Integer = 0) As Boolean
        Try
            Dim obl_adjustItem As New bl_AdjustItemLocation
            Dim odt As New DataTable
            Dim Obj_Dataset As New DataSet
            Dim pBool As Boolean = False
            obl_adjustItem.Adjust_Index = adjust_index
            obl_adjustItem.Qty_1st_Count = Count_1st_Total_Qty
            obl_adjustItem.ItemStatus_Index = ItemStatus_Index ' "0010000000001"
            obl_adjustItem.Location_Index = getlocation_index(Location_Alias)
            If isAdjustSku Then
                obl_adjustItem.ItemStatus_Index = ""
                obl_adjustItem.Location_Index = ""
            End If

            obl_adjustItem.AdjustItemLocation_Index = AdjustItemLocation_index
            obl_adjustItem.Status = Status
            obl_adjustItem.Sku_Index = getSkuBarcode1_index(sku_Barcode)
            obl_adjustItem.NewItem = piNewItem
            obl_adjustItem.Seq = iSeq
            pBool = obl_adjustItem.insert_Adjust_Quantity()

            Me.Update_Seq(obl_adjustItem.Adjust_Index, obl_adjustItem.AdjustItemLocation_Index, obl_adjustItem.Seq)

            Return pBool
        Catch ex As Exception
            Throw ex
        End Try


    End Function




#End Region

#Region "   Update   "
    Public Function Update_Adjust_Quantity(ByVal AdjustItemLocation_Index As String, ByVal adjust_index As String, ByVal Count_1st_Total_Qty As Double, ByVal pNoItemLocation_Index As String) As String
        'ByVal Adjust_Qty As Double, ByVal Adjust_Weight As Double, ByVal Adjust_Volume As Double,

        Dim strSQL As String = ""
        Dim strSQLSatus As String = ""

        'If ChkAdjustItemStatus(AdjustItemLocation_Index, 1) = False Then
        '    Return "DUP"
        '    Exit Function
        'End If
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            If Count_1st_Total_Qty = 0 Then
                strSQL = " DELETE tb_AdjustItemLocation WHERE Adjust_Index = '" & adjust_index & "' and Location_Index ='" & pNoItemLocation_Index & "'  and Status = -9 and NewItem = 1  "
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If


            'If pNoItemLocation_Index = "" Then
            strSQL = " UPDATE tb_AdjustItemLocation SET "
            strSQL &= " Adjust_Qty=@Adjust_Qty"
            ' strSQL &= " ,Adjust_Weight=@Adjust_Weight,Adjust_Volume =@Adjust_Volume"
            strSQL &= " , update_by=@update_by,update_date=getdate(),update_branch=@update_branch,Status=@Status"
            strSQL &= ",Qty_1st_Count=@Qty_1st_Count"
            strSQL &= " WHERE AdjustItemLocation_Index=@AdjustItemLocation_Index "

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@AdjustItemLocation_Index", SqlDbType.VarChar, 13).Value = AdjustItemLocation_Index
                .Parameters.Add("@Adjust_Qty", SqlDbType.Float, 8).Value = 0 'Adjust_Qty
                '.Parameters.Add("@Adjust_Weight", SqlDbType.Float, 8).Value = Adjust_Weight
                '.Parameters.Add("@Adjust_Volume", SqlDbType.Float, 8).Value = Adjust_Volume
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = -9
                .Parameters.Add("@Qty_1st_Count", SqlDbType.Float, 8).Value = Count_1st_Total_Qty
            End With
            'Else

            '    strSQL = " UPDATE tb_AdjustItemLocation SET "
            '    strSQL &= " Adjust_Qty=@Adjust_Qty"
            '    ' strSQL &= " ,Adjust_Weight=@Adjust_Weight,Adjust_Volume =@Adjust_Volume"
            '    strSQL &= " , update_by=@update_by,update_date=getdate(),update_branch=@update_branch,Status=@Status"
            '    strSQL &= ",Qty_1st_Count=@Qty_1st_Count"
            '    strSQL &= " WHERE adjust_index=@adjust_index And Location_Index=@Location_Index AND (isnull(Status,1) =1 or NewItem=1) "

            '    With SQLServerCommand

            '        .Parameters.Clear()
            '        .Parameters.Add("@adjust_index", SqlDbType.VarChar, 13).Value = adjust_index
            '        .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = pNoItemLocation_Index

            '        .Parameters.Add("@Adjust_Qty", SqlDbType.Float, 8).Value = 0 'Adjust_Qty
            '        '.Parameters.Add("@Adjust_Weight", SqlDbType.Float, 8).Value = Adjust_Weight
            '        '.Parameters.Add("@Adjust_Volume", SqlDbType.Float, 8).Value = Adjust_Volume
            '        .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
            '        .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
            '        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = -9
            '        .Parameters.Add("@Qty_1st_Count", SqlDbType.Float, 8).Value = Count_1st_Total_Qty
            '    End With
            'End If


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            ' connectDB()
            EXEC_Command()

            '*** Commit transaction
            myTrans.Commit()
            Return "TRUE"

        Catch ex As Exception
            myTrans.Rollback()
            Return "FALSE"
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function


    Public Function Update_Seq(ByVal Adjust_Index As String, ByVal AdjustItemLocation_Index As String, ByVal iSeq As Integer) As String
        Dim strSQL As String = ""
        Dim strSQLSatus As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            strSQL = " UPDATE tb_AdjustItemLocation SET "
            If iSeq = 0 Then
                strSQL &= " Seq= (select count(*) from tb_AdjustItemLocation where Adjust_Index = @Adjust_Index ) + 1"
            Else
                strSQL &= " Seq=@Seq"
            End If
            strSQL &= " ,update_by=@update_by,update_branch=@update_branch"
            strSQL &= " WHERE AdjustItemLocation_Index=@AdjustItemLocation_Index "

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@Adjust_Index", SqlDbType.VarChar, 13).Value = Adjust_Index
                .Parameters.Add("@AdjustItemLocation_Index", SqlDbType.VarChar, 13).Value = AdjustItemLocation_Index
                .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = iSeq
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            myTrans.Commit()
            Return "TRUE"

        Catch ex As Exception
            myTrans.Rollback()
            Return "FALSE"
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function


#End Region

#Region "   DELETE   "
    Public Function Delete(ByVal pintBarcode_Index As Integer) As Boolean
        Try
            Dim strSQL As String = ""

            strSQL = "Delete from ms_Barcode_Sharp"
            strSQL &= " WHERE Barcode_Index = @Barcode_Index"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Barcode_Index", SqlDbType.Int).Value = pintBarcode_Index
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region


    Public Function Confirm_Adjust_Status_AutoIN_AutoOUT_KSL_V2(ByVal pstrAdjust_index As String, ByVal pstrStatus As String) As Boolean
        'Init. connection
        'Dim objConfig As New config_CustomSetting
        'Dim DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST As String = objConfig.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST")
        'Dim DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST As String = objConfig.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST")

        'Dim _Connection As SqlClient.SqlConnection = Nothing
        'Dim _myTrans As SqlClient.SqlTransaction = Nothing
        '_Connection = Connection
        'With SQLServerCommand
        '    .Connection = _Connection
        '    If .Connection.State = ConnectionState.Open Then .Connection.Close()
        '    .Connection.Open()
        '    .Transaction = _myTrans
        'End With
        '_myTrans = Connection.BeginTransaction()
        'Try
        '    'KSL : Adjust auto move. picking and transferstatus
        '    '------------------------------------------------------------------------------------------------------------------------
        '    Dim TypePicking As New WMS_Site_Topcharoen_P2.PICKING.enmPicking_Type
        '    Dim objPicking As New WMS_Site_Topcharoen_P2.PICKING(WMS_Site_Topcharoen_P2.PICKING.enmPicking_Type.FIFO)
        '    Dim Result As New DataTable
        '    Dim dtLocationBalance As New DataTable
        '    Dim strWhere As String = ""
        '    Dim xSQL As String = ""
        '    Dim xOrder_Index As String = ""
        '    Dim xWithdraw_Index As String = ""
        '    xSQL = " SELECT AJ.DocumentType_Index,AJ.Customer_Index,S.Package_Index,AJ.Adjust_No,AJI.*"
        '    xSQL &= " FROM tb_AdjustItemLocation AJI "
        '    xSQL &= "   Inner join tb_Adjust AJ ON AJ.Adjust_Index = AJI.Adjust_Index"
        '    xSQL &= "   Inner join ms_SKU S ON S.Sku_Index = AJI.Sku_Index"
        '    xSQL &= String.Format("  WHERE AJ.Adjust_Index = '{0}'", pstrAdjust_index)
        '    Dim dtAdj As New DataTable
        '    dtAdj = DBExeQuery(xSQL, _Connection, _myTrans)
        '    Dim drArrWD() As DataRow
        '    '------------------------------------------------------------------------------------------------------------------------
        '    'STEP 1 : สร้างใบเบิกสินค้า
        '    drArrWD = dtAdj.Select("(Qty_1st_Count <> Qty_Bal)", "AdjustItemLocation_Index")
        '    If drArrWD.Length > 0 Then
        '        'Auto Picking by Tag
        '        xSQL = " SELECT AJ.DocumentType_Index,AJ.Customer_Index,AJ.Adjust_No"
        '        'Key
        '        xSQL &= " ,AJI.TAG_No"
        '        xSQL &= " ,AJI.Mfg_Date,AJI.Exp_Date,AJI.Plot"
        '        xSQL &= " FROM tb_AdjustItemLocation AJI "
        '        xSQL &= "   Inner join tb_Adjust AJ ON AJ.Adjust_Index = AJI.Adjust_Index"
        '        xSQL &= "   Inner join ms_SKU S ON S.Sku_Index = AJI.Sku_Index"
        '        xSQL &= String.Format("  WHERE AJ.Adjust_Index = '{0}'  AND Qty_1st_Count <> Qty_Bal ", pstrAdjust_index)
        '        xSQL &= " GROUP BY AJ.DocumentType_Index,AJ.Customer_Index,AJ.Adjust_No"
        '        'Key
        '        xSQL &= " ,AJI.TAG_No"
        '        xSQL &= " ,AJI.Mfg_Date,AJI.Exp_Date,AJI.Plot"

        '        Dim dtAutoPick As New DataTable
        '        dtAutoPick = DBExeQuery(xSQL, _Connection, _myTrans)

        '        Dim objWD As New GrouppingConditionPicking()
        '        Dim _Customer_Index As String = drArrWD(0).Item("Customer_Index").ToString()
        '        xWithdraw_Index = objWD.CreateWTH(DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST, _Customer_Index) 'Fix เบิกปรับยอด

        '        For Each drfifo As DataRow In dtAutoPick.Rows
        '            'STEP 1.1 : Get Balance
        '            xSQL = " SELECT *"
        '            xSQL &= " FROM tb_LocationBalance "
        '            strWhere = " WHERE 1=1 and Qty_Bal > 0"
        '            'Key
        '            strWhere &= String.Format(" AND  TAG_No = '{0}'", drfifo("TAG_No").ToString)
        '            strWhere &= String.Format(" AND  Mfg_Date = '{0}'", CDate(drfifo("Mfg_Date")).ToString("yyyy/MM/dd"))
        '            strWhere &= String.Format(" AND  Exp_Date = '{0}'", CDate(drfifo("Exp_Date")).ToString("yyyy/MM/dd"))
        '            strWhere &= String.Format(" AND  Plot = '{0}'", drfifo("Plot").ToString)

        '            xSQL &= strWhere
        '            Dim dtLB As New DataTable
        '            dtLB = DBExeQuery(xSQL, _Connection, _myTrans)
        '            If dtLB.Rows.Count = 0 Then Continue For

        '            For Each drPicking As DataRow In dtLB.Rows
        '                If drPicking("Qty_Bal") = 0 Then Continue For
        '                'STEP 1.2 : Picking reserve
        '                Result = New DataTable
        '                strWhere = ""
        '                strWhere &= String.Format(" AND  LocationBalance_Index = '{0}'", drPicking("LocationBalance_Index").ToString)
        '                TypePicking = PICKING.enmPicking_Type.FIFO
        '                objPicking = New WMS_Site_Topcharoen_P2.PICKING(TypePicking, drPicking("Sku_Index").ToString, drPicking("Package_Index").ToString, drPicking("Qty_Bal"), strWhere, "")
        '                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 4, pstrAdjust_index, "")
        '                If Result IsNot Nothing Then
        '                    If Result.Rows.Count > 0 Then
        '                        dtLocationBalance.Merge(Result)
        '                    End If
        '                End If
        '            Next

        '        Next
        '        'STEP 1.3 : สร้างรายการเบิกสินค้า
        '        objWD.CreateWTHIL(xWithdraw_Index, "", "", "", -9, dtLocationBalance, _Connection, _myTrans)
        '        objWD.UpdateWithdrawNo(xWithdraw_Index, "System auto from adjust", _Connection, _myTrans)
        '        xSQL = String.Format("UPDATE tb_WithdrawItem set DocumentPlan_No = '{0}' , DocumentPlan_Index = '{1}' WHERE Withdraw_Index = '{2}'", dtAdj.Rows(0)("Adjust_No").ToString, dtAdj.Rows(0)("Adjust_Index").ToString, xWithdraw_Index)
        '        DBExeNonQuery(xSQL, _Connection, _myTrans)
        '    End If
        '    'Catch ex As Exception
        '    '    MessageBox.Show(ex.Message.ToString & Chr(13) & iiii.ToString)
        '    '    _myTrans.Rollback()
        '    '    Exit Function
        '    'End Try

        '    '------------------------------------------------------------------------------------------------------------------------
        '    'STEP 2 : สร้างใบรับวินค้า
        '    drArrWD = dtAdj.Select("(Qty_1st_Count <> Qty_Bal) and (Qty_1st_Count > 0)", "AdjustItemLocation_Index")
        '    'drArrWD = dtAdj.Select(" (Qty_1st_Count > 0)", "AdjustItemLocation_Index")
        '    If drArrWD.Length > 0 Then
        '        Dim objDBIndex As New Sy_AutoNumber
        '        Dim objDocumentNumber As New clsDocumentNumber()
        '        Dim xAutoField As String = ""
        '        Dim xAuto1 As String = ""
        '        Dim xAuto2 As String = ""
        '        Dim xOrderItem_Index As String = ""
        '        Dim xOrder_No As String = ""
        '        Dim xdtAutoField As New DataTable
        '        '---------------------------------------------------------------------------
        '        'Auto Insert Order Header
        '        xAutoField = ""
        '        xAuto1 = ""
        '        xAuto2 = ""

        '        xdtAutoField = New DataTable
        '        xdtAutoField = DBExeQuery("SELECT sys.columns.name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tb_Order') ", _Connection, _myTrans)
        '        For Each xdrField As DataRow In xdtAutoField.Rows
        '            xAutoField &= xdrField("name").ToString & ","
        '        Next
        '        xAutoField = xAutoField.Remove(xAutoField.Length - 1, 1)
        '        xAuto1 = xAutoField 'Insert
        '        xAuto2 = xAutoField 'Select
        '        'Update Value Field
        '        xOrder_Index = objDBIndex.getSys_Value("Order_Index")
        '        xOrder_No = objDocumentNumber.Auto_DocumentType_Number(DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST, " Branch_ID ='" & WV_Branch_ID & "'", Now.ToString("yyyy/MM/dd"))
        '        xAuto2 = xAuto2.Replace("Order_Index", "'" & xOrder_Index & "'")
        '        xAuto2 = xAuto2.Replace("Order_No", "'" & xOrder_No & "'")
        '        xAuto2 = xAuto2.Replace("Order_Date", "'" & Now.ToString("yyyy/MM/dd") & "'")
        '        xAuto2 = xAuto2.Replace("DocumentType_Index", "'" & DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST & "'")
        '        xAuto2 = xAuto2.Replace("Ref_No1", "'" & drArrWD(0)("Adjust_No").ToString & "'")
        '        xAuto2 = xAuto2.Replace("Customer_Index", "'" & drArrWD(0)("Customer_Index").ToString & "'")
        '        xAuto2 = xAuto2.Replace("add_by", "'" & WV_UserName & "'")
        '        xAuto2 = xAuto2.Replace("add_date", "getdate()")
        '        xAuto2 = xAuto2.Replace("Status", "1")
        '        xSQL = " INSERT INTO tb_Order (" & xAuto1 & ")"
        '        xSQL &= " SELECT TOP 1 " & xAuto2 & " FROM tb_Order WHERE Customer_Index = '" & drArrWD(0)("Customer_Index").ToString & "'"
        '        DBExeNonQuery(xSQL, _Connection, _myTrans)
        '        '---------------------------------------------------------------------------
        '        For Each drAj As DataRow In drArrWD
        '            'STEP 1.1 : Get Balance,objDBIndex.getSys_Value("OrderItem_Index")
        '            xSQL = " SELECT TOP 1 LB.*"
        '            xSQL &= " ,S.UnitWeight_Index,S.Unit_Volume "
        '            xSQL &= " FROM tb_LocationBalance LB inner join ms_Sku S on S.Sku_Index = LB.Sku_Index"
        '            'xSQL &= String.Format("  WHERE LB.Sku_Index = '{0}' AND LB.TAG_No = '{1}'", drAj("Sku_Index").ToString, drAj("TAG_No").ToString)
        '            strWhere = " WHERE 1=1 "
        '            'Key
        '            strWhere &= String.Format(" AND  LB.Sku_Index = '{0}'", drAj("Sku_Index").ToString)
        '            strWhere &= String.Format(" AND  LB.TAG_No = '{0}'", drAj("TAG_No").ToString)
        '            strWhere &= String.Format(" AND  LB.Mfg_Date = '{0}'", CDate(drAj("Mfg_Date")).ToString("yyyy/MM/dd"))
        '            strWhere &= String.Format(" AND  LB.Exp_Date = '{0}'", CDate(drAj("Exp_Date")).ToString("yyyy/MM/dd"))
        '            strWhere &= String.Format(" AND  LB.Plot = '{0}'", drAj("Plot").ToString)
        '            'strWhere &= String.Format(" AND  LB.ItemStatus_Index = '{0}'", drAj("ItemStatus_Index").ToString)
        '            'strWhere &= String.Format(" AND  LB.Location_Index = '{0}'", drAj("Location_Index").ToString)
        '            xSQL &= strWhere

        '            xSQL &= " Order by LB.Qty_Bal desc "
        '            Dim dtLB As New DataTable
        '            dtLB = DBExeQuery(xSQL, _Connection, _myTrans)
        '            If dtLB.Rows.Count = 0 Then Continue For
        '            Dim drLB As DataRow = dtLB.Rows(0)
        '            '---------------------------------------------------------------------------
        '            'Auto Insert OrderItem
        '            xAutoField = ""
        '            xAuto1 = ""
        '            xAuto2 = ""
        '            xdtAutoField = New DataTable
        '            xdtAutoField = DBExeQuery("SELECT sys.columns.name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tb_OrderItem') ", _Connection, _myTrans)
        '            For Each xdrField As DataRow In xdtAutoField.Rows
        '                xAutoField &= xdrField("name").ToString & ","
        '            Next
        '            xAutoField = xAutoField.Remove(xAutoField.Length - 1, 1)
        '            xAuto1 = xAutoField 'Insert
        '            xAuto2 = xAutoField 'Select
        '            'Update Value Field
        '            xOrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
        '            Dim xTotal_Qty As Decimal = drAj("Qty_1st_Count") '- drAj("Qty_Bal")
        '            xAuto2 = xAuto2.Replace("Order_Index", "'" & xOrder_Index & "'")
        '            xAuto2 = xAuto2.Replace("OrderItem_Index", "'" & xOrderItem_Index & "'")
        '            xAuto2 = xAuto2.Replace(",Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
        '            xAuto2 = xAuto2.Replace(",Total_Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
        '            xAuto2 = xAuto2.Replace("Ratio", 1)
        '            xAuto2 = xAuto2.Replace(",Sku_Index", ",'" & drAj("Sku_Index").ToString & "'")
        '            xAuto2 = xAuto2.Replace(",Package_Index", ",'" & drAj("Package_Index").ToString & "'")
        '            xAuto2 = xAuto2.Replace(",ItemStatus_Index", ",'" & drAj("ItemStatus_Index").ToString & "'")
        '            xAuto2 = xAuto2.Replace(",PLot,", ",'" & drLB("PLot").ToString & "',")
        '            xAuto2 = xAuto2.Replace(",Mfg_Date,", ",'" & CDate(drLB("Mfg_Date")).ToString("yyyy/MM/dd") & "',")
        '            xAuto2 = xAuto2.Replace(",Exp_Date,", ",'" & CDate(drLB("Exp_Date")).ToString("yyyy/MM/dd") & "',")
        '            xAuto2 = xAuto2.Replace(",IsMfg_Date,", ",1,")
        '            xAuto2 = xAuto2.Replace(",IsExp_Date,", ",1,")
        '            If drLB("Qty_Bal_Begin") > 0 Then
        '                xAuto2 = xAuto2.Replace(",Item_Qty,", ",'" & CDbl(FormatNumber((drLB("Qty_Item_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6)) & "',")
        '                Dim Weight As Decimal = Val(FormatNumber((drLB("Weight_Bal_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6))
        '                Dim Volume As Decimal = Val(FormatNumber((drLB("Volume_Bal_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6))
        '                Dim OrderItem_Price As Decimal = Val(FormatNumber((drLB("OrderItem_Price_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6))

        '                xAuto2 = xAuto2.Replace(",Weight,", ",'" & Weight & "',")
        '                xAuto2 = xAuto2.Replace(",Volume,", ",'" & Volume & "',")
        '                xAuto2 = xAuto2.Replace(",OrderItem_Price,", ",'" & OrderItem_Price & "',")
        '            Else
        '                xAuto2 = xAuto2.Replace(",Item_Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
        '                xAuto2 = xAuto2.Replace(",Weight,", ",'" & CDbl(FormatNumber(drLB("UnitWeight_Index") * xTotal_Qty, 6)) & "',")
        '                xAuto2 = xAuto2.Replace(",Volume,", ",'" & CDbl(FormatNumber(drLB("Unit_Volume") * xTotal_Qty, 6)) & "',")
        '                xAuto2 = xAuto2.Replace(",OrderItem_Price,", ",'" & CDbl(FormatNumber(0, 6)) & "',")
        '            End If
        '            xAuto2 = xAuto2.Replace("Pallet_Qty", CDbl(FormatNumber(drLB("Pallet_Qty"), 6)))
        '            xAuto2 = xAuto2.Replace("ERP_Location", "'" & drLB("ERP_Location").ToString & "'")
        '            'Get Location Alias for putaway
        '            xAuto2 = xAuto2.Replace(",Str5,", ",'" & drAj("TAG_No").ToString & "',")
        '            Dim xLocation_Index As String = DBExeQuery_Scalar("select Location_Alias from ms_Location where Location_Index = '" & drAj("Location_Index").ToString & "'", _Connection, _myTrans)
        '            xAuto2 = xAuto2.Replace(",Str4,", ",'" & xLocation_Index & "',")

        '            xAuto2 = xAuto2.Replace("Plan_Process", "-9")
        '            xAuto2 = xAuto2.Replace("DocumentPlan_No", "'" & drAj("Adjust_No").ToString & "'")
        '            xAuto2 = xAuto2.Replace("DocumentPlan_Index", "'" & drAj("Adjust_Index").ToString & "'")
        '            xAuto2 = xAuto2.Replace("add_by", "'" & WV_UserName & "'")
        '            xAuto2 = xAuto2.Replace("add_date", "getdate()")
        '            xAuto2 = xAuto2.Replace(",Status", ",1")
        '            xSQL = " INSERT INTO tb_OrderItem (" & xAuto1 & ")"
        '            xSQL &= " SELECT " & xAuto2 & " FROM tb_OrderItem WHERE OrderItem_Index = '" & drLB("OrderItem_Index").ToString & "'"
        '            DBExeNonQuery(xSQL, _Connection, _myTrans)

        '        Next
        '    End If
        '    '------------------------------------------------------------------------------------------------------------------------
        '    _myTrans.Commit()
        '    '------------------------------------------------------------------------------------------------------------------------
        '    'ยืนยันใบเบิก
        '    Dim xPASS As String = "PASS"
        '    If dtLocationBalance.Rows.Count > 0 Then
        '        If xWithdraw_Index.Trim.Length > 0 Then
        '            If dtLocationBalance.Rows.Count > 0 Then
        '                Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
        '                xPASS = objClassDB.Withdraw_Confirm(xWithdraw_Index)
        '            End If
        '        End If
        '    End If

        '    'ยืนยันใบรับ
        '    If xOrder_Index.Trim.Length > 0 Then
        '        If xPASS = "PASS" Then 'ยืนยันเบิกผ่าน
        '            Dim fAuto As New frmImport_Order_StockBalance
        '            fAuto.AutoGenTag(xOrder_Index)
        '            xPASS = fAuto.PutAwayWith_Suggest_Location(xOrder_Index)
        '        End If
        '    End If

        '    If xPASS <> "PASS" Then
        '        'ยกเลิกรับ
        '        If xOrder_Index.Trim.Length > 0 Then
        '            Dim oblst As New bl_ImportOrder_Stock_TNP
        '            oblst.Delete_OrderTransactionAuto(xOrder_Index)
        '        End If
        '        'ยกเลิกจ่าย
        '        If xWithdraw_Index.Trim.Length > 0 Then
        '            Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
        '            objClassDB.Cancel_Withdraw(xWithdraw_Index)
        '        End If
        '    End If

        '------------------------------------------------------------------------------------------------------------------------
        'ยืนยันใบสั่งตรจนับ
        Try
            Dim xsql As String = ""
            xsql = "UPDATE tb_Adjust  "
            xsql &= " SET status =" & pstrStatus & ","
            xsql &= "update_by='" & WV_UserName & "',"
            xsql &= "update_date=getdate(),"
            xsql &= "update_branch =" & WV_Branch_ID
            xsql &= " WHERE  Adjust_Index='" & pstrAdjust_index & "' "
            xsql &= " UPDATE tb_AdjustItemLocation SET Status =-9,Adjust_Qty = Qty_1st_Count WHERE Adjust_Index='" & pstrAdjust_index & "' "
            DBExeNonQuery(xsql)
            '------------------------------------------------------------------------------------------------------------------------

            Return True

        Catch ex As Exception
            '  _myTrans.Rollback()
            Throw ex
        Finally
            SQLServerCommand.Connection.Close()
        End Try

    End Function


    Public Function Confirm_Adjust_Status_AutoIN_AutoOUT_KSL(ByVal pstrAdjust_index As String, ByVal pstrStatus As String) As Boolean
        'Init. connection
        Dim objConfig As New config_CustomSetting
        Dim DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST As String = objConfig.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST")
        Dim DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST As String = objConfig.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST")

        Dim _Connection As SqlClient.SqlConnection = Nothing
        Dim _myTrans As SqlClient.SqlTransaction = Nothing
        _Connection = Connection
        With SQLServerCommand
            .Connection = _Connection
            If .Connection.State = ConnectionState.Open Then .Connection.Close()
            .Connection.Open()
            .Transaction = _myTrans
        End With
        _myTrans = Connection.BeginTransaction()
        Try
            'KSL : Adjust auto move. picking and transferstatus
            '------------------------------------------------------------------------------------------------------------------------
            Dim TypePicking As New WMS_Site_Topcharoen_P2.PICKING.enmPicking_Type
            Dim objPicking As New WMS_Site_Topcharoen_P2.PICKING(WMS_Site_Topcharoen_P2.PICKING.enmPicking_Type.FIFO)
            Dim Result As New DataTable
            Dim dtLocationBalance As New DataTable
            Dim strWhere As String = ""
            Dim xSQL As String = ""
            Dim xOrder_Index As String = ""
            Dim xWithdraw_Index As String = ""
            xSQL = " SELECT AJ.DocumentType_Index,AJ.Customer_Index,S.Package_Index,AJ.Adjust_No,AJI.*"
            xSQL &= " FROM tb_AdjustItemLocation AJI "
            xSQL &= "   Inner join tb_Adjust AJ ON AJ.Adjust_Index = AJI.Adjust_Index"
            xSQL &= "   Inner join ms_SKU S ON S.Sku_Index = AJI.Sku_Index"
            xSQL &= String.Format("  WHERE AJ.Adjust_Index = '{0}'", pstrAdjust_index)
            Dim dtAdj As New DataTable
            dtAdj = DBExeQuery(xSQL, _Connection, _myTrans)
            Dim drArrWD() As DataRow
            '------------------------------------------------------------------------------------------------------------------------
            'STEP 1 : สร้างใบเบิกสินค้า
            'Dim iiii As Integer = 0
            'Try
            drArrWD = dtAdj.Select("(Qty_1st_Count <> Qty_Bal) and (Qty_Bal > 0)", "AdjustItemLocation_Index")
            If drArrWD.Length > 0 Then
                Dim objWD As New GrouppingConditionPicking()
                Dim _Customer_Index As String = drArrWD(0).Item("Customer_Index").ToString()
                xWithdraw_Index = objWD.CreateWTH(DEFAULT_DOCUMENT_TYPE_WITHDRAW_ADJUST, _Customer_Index) 'Fix เบิกปรับยอด
                For Each drAj As DataRow In drArrWD
                    'iiii += 1
                    'If iiii.ToString = "72" Then
                    '    'MessageBox.Show(iiii.ToString)
                    '    iiii = iiii
                    'End If
                    'STEP 1.1 : Get Balance
                    xSQL = " SELECT SUM(ISNULL(Qty_Bal,0)) Qty_Bal"
                    xSQL &= " FROM tb_LocationBalance "
                    strWhere = " WHERE 1=1 "
                    strWhere &= String.Format(" AND  Sku_Index = '{0}'", drAj("Sku_Index").ToString)
                    strWhere &= String.Format(" AND  TAG_No = '{0}'", drAj("TAG_No").ToString)
                    strWhere &= String.Format(" AND  Mfg_Date = '{0}'", CDate(drAj("Mfg_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  Exp_Date = '{0}'", CDate(drAj("Exp_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  Plot = '{0}'", drAj("Plot").ToString)
                    'strWhere &= String.Format(" AND  ItemStatus_Index = '{0}'", drAj("ItemStatus_Index").ToString)
                    'strWhere &= String.Format(" AND  Location_Index = '{0}'", drAj("Location_Index").ToString)

                    xSQL &= strWhere
                    Dim dtLB As New DataTable
                    dtLB = DBExeQuery(xSQL, _Connection, _myTrans)
                    If dtLB.Rows.Count = 0 Then Continue For
                    Dim xQty_Bal As Decimal = 0
                    If drAj("Qty_1st_Count") > drAj("Qty_Bal") Then
                        xQty_Bal = dtLB.Rows(0)("Qty_Bal")
                    Else
                        xQty_Bal = drAj("Qty_Bal") - drAj("Qty_1st_Count")
                    End If

                    If xQty_Bal = 0 Then Continue For
                    'STEP 1.2 : Picking reserve
                    Result = New DataTable
                    'strWhere = String.Format(" AND TAG_No = '{0}'", drAj("TAG_No").ToString)
                    strWhere = ""
                    strWhere &= String.Format(" AND  Sku_Index = '{0}'", drAj("Sku_Index").ToString)
                    strWhere &= String.Format(" AND  TAG_No = '{0}'", drAj("TAG_No").ToString)
                    strWhere &= String.Format(" AND  Mfg_Date = '{0}'", CDate(drAj("Mfg_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  Exp_Date = '{0}'", CDate(drAj("Exp_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  Plot = '{0}'", drAj("Plot").ToString)
                    'strWhere &= String.Format(" AND  ItemStatus_Index = '{0}'", drAj("ItemStatus_Index").ToString)
                    'strWhere &= String.Format(" AND  Location_Index = '{0}'", drAj("Location_Index").ToString)
                    TypePicking = PICKING.enmPicking_Type.FIFO
                    objPicking = New WMS_Site_Topcharoen_P2.PICKING(TypePicking, drAj("Sku_Index").ToString, drAj("Package_Index").ToString, xQty_Bal, strWhere, "")
                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 4, pstrAdjust_index, "")
                    If Result IsNot Nothing Then
                        If Result.Rows.Count > 0 Then
                            dtLocationBalance.Merge(Result)
                        End If
                    End If
                Next
                'STEP 1.3 : สร้างรายการเบิกสินค้า
                objWD.CreateWTHIL(xWithdraw_Index, "", "", "", -9, dtLocationBalance, _Connection, _myTrans)
                objWD.UpdateWithdrawNo(xWithdraw_Index, "System auto from adjust", _Connection, _myTrans)
                xSQL = String.Format("UPDATE tb_WithdrawItem set DocumentPlan_No = '{0}' , DocumentPlan_Index = '{1}' WHERE Withdraw_Index = '{2}'", dtAdj.Rows(0)("Adjust_No").ToString, dtAdj.Rows(0)("Adjust_Index").ToString, xWithdraw_Index)
                DBExeNonQuery(xSQL, _Connection, _myTrans)
            End If
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message.ToString & Chr(13) & iiii.ToString)
            '    _myTrans.Rollback()
            '    Exit Function
            'End Try

            '------------------------------------------------------------------------------------------------------------------------
            'STEP 2 : สร้างใบรับวินค้า
            drArrWD = dtAdj.Select("(Qty_1st_Count - Qty_Bal) > 0", "AdjustItemLocation_Index")
            If drArrWD.Length > 0 Then
                Dim objDBIndex As New Sy_AutoNumber
                Dim objDocumentNumber As New clsDocumentNumber()
                Dim xAutoField As String = ""
                Dim xAuto1 As String = ""
                Dim xAuto2 As String = ""
                Dim xOrderItem_Index As String = ""
                Dim xOrder_No As String = ""
                Dim xdtAutoField As New DataTable
                '---------------------------------------------------------------------------
                'Auto Insert Order Header
                xAutoField = ""
                xAuto1 = ""
                xAuto2 = ""

                xdtAutoField = New DataTable
                xdtAutoField = DBExeQuery("SELECT sys.columns.name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tb_Order') ", _Connection, _myTrans)
                For Each xdrField As DataRow In xdtAutoField.Rows
                    xAutoField &= xdrField("name").ToString & ","
                Next
                xAutoField = xAutoField.Remove(xAutoField.Length - 1, 1)
                xAuto1 = xAutoField 'Insert
                xAuto2 = xAutoField 'Select
                'Update Value Field
                xOrder_Index = objDBIndex.getSys_Value("Order_Index")
                xOrder_No = objDocumentNumber.Auto_DocumentType_Number(DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST, " Branch_ID ='" & WV_Branch_ID & "'", Now.ToString("yyyy/MM/dd"))
                xAuto2 = xAuto2.Replace("Order_Index", "'" & xOrder_Index & "'")
                xAuto2 = xAuto2.Replace("Order_No", "'" & xOrder_No & "'")
                xAuto2 = xAuto2.Replace("Order_Date", "'" & Now.ToString("yyyy/MM/dd") & "'")
                xAuto2 = xAuto2.Replace("DocumentType_Index", "'" & DEFAULT_DOCUMENT_TYPE_RECEIVE_ADJUST & "'")
                xAuto2 = xAuto2.Replace("Ref_No1", "'" & drArrWD(0)("Adjust_No").ToString & "'")
                xAuto2 = xAuto2.Replace("add_by", "'" & WV_UserName & "'")
                xAuto2 = xAuto2.Replace("add_date", "getdate()")
                xAuto2 = xAuto2.Replace("Status", "1")
                xSQL = " INSERT INTO tb_Order (" & xAuto1 & ")"
                xSQL &= " SELECT TOP 1 " & xAuto2 & " FROM tb_Order WHERE Customer_Index = '" & drArrWD(0)("Customer_Index").ToString & "'"
                DBExeNonQuery(xSQL, _Connection, _myTrans)
                '---------------------------------------------------------------------------
                For Each drAj As DataRow In drArrWD
                    'STEP 1.1 : Get Balance,objDBIndex.getSys_Value("OrderItem_Index")
                    xSQL = " SELECT TOP 1 LB.*"
                    xSQL &= " ,S.UnitWeight_Index,S.Unit_Volume "
                    xSQL &= " FROM tb_LocationBalance LB inner join ms_Sku S on S.Sku_Index = LB.Sku_Index"
                    'xSQL &= String.Format("  WHERE LB.Sku_Index = '{0}' AND LB.TAG_No = '{1}'", drAj("Sku_Index").ToString, drAj("TAG_No").ToString)
                    strWhere = " WHERE 1=1 "
                    strWhere &= String.Format(" AND  LB.Sku_Index = '{0}'", drAj("Sku_Index").ToString)
                    strWhere &= String.Format(" AND  LB.TAG_No = '{0}'", drAj("TAG_No").ToString)
                    strWhere &= String.Format(" AND  LB.Mfg_Date = '{0}'", CDate(drAj("Mfg_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  LB.Exp_Date = '{0}'", CDate(drAj("Exp_Date")).ToString("yyyy/MM/dd"))
                    strWhere &= String.Format(" AND  LB.Plot = '{0}'", drAj("Plot").ToString)
                    'strWhere &= String.Format(" AND  LB.ItemStatus_Index = '{0}'", drAj("ItemStatus_Index").ToString)
                    'strWhere &= String.Format(" AND  LB.Location_Index = '{0}'", drAj("Location_Index").ToString)
                    xSQL &= strWhere

                    xSQL &= " Order by LB.Qty_Bal desc "
                    Dim dtLB As New DataTable
                    dtLB = DBExeQuery(xSQL, _Connection, _myTrans)
                    If dtLB.Rows.Count = 0 Then Continue For
                    Dim drLB As DataRow = dtLB.Rows(0)
                    '---------------------------------------------------------------------------
                    'Auto Insert OrderItem
                    xAutoField = ""
                    xAuto1 = ""
                    xAuto2 = ""
                    xdtAutoField = New DataTable
                    xdtAutoField = DBExeQuery("SELECT sys.columns.name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tb_OrderItem') ", _Connection, _myTrans)
                    For Each xdrField As DataRow In xdtAutoField.Rows
                        xAutoField &= xdrField("name").ToString & ","
                    Next
                    xAutoField = xAutoField.Remove(xAutoField.Length - 1, 1)
                    xAuto1 = xAutoField 'Insert
                    xAuto2 = xAutoField 'Select
                    'Update Value Field
                    xOrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                    Dim xTotal_Qty As Decimal = drAj("Qty_1st_Count") '- drAj("Qty_Bal")
                    xAuto2 = xAuto2.Replace("Order_Index", "'" & xOrder_Index & "'")
                    xAuto2 = xAuto2.Replace("OrderItem_Index", "'" & xOrderItem_Index & "'")
                    xAuto2 = xAuto2.Replace(",Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
                    xAuto2 = xAuto2.Replace(",Total_Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
                    xAuto2 = xAuto2.Replace("Ratio", 1)
                    xAuto2 = xAuto2.Replace(",Sku_Index", ",'" & drAj("Sku_Index").ToString & "'")
                    xAuto2 = xAuto2.Replace(",Package_Index", ",'" & drAj("Package_Index").ToString & "'")
                    'xAuto2 = xAuto2.Replace(",ItemStatus_Index", ",'" & drAj("ItemStatus_Index").ToString & "'")
                    xAuto2 = xAuto2.Replace(",PLot,", ",'" & drLB("PLot").ToString & "',")
                    xAuto2 = xAuto2.Replace(",Mfg_Date,", ",'" & CDate(drLB("Mfg_Date")).ToString("yyyy/MM/dd") & "',")
                    xAuto2 = xAuto2.Replace(",Exp_Date,", ",'" & CDate(drLB("Exp_Date")).ToString("yyyy/MM/dd") & "',")
                    xAuto2 = xAuto2.Replace(",IsMfg_Date,", ",1,")
                    xAuto2 = xAuto2.Replace(",IsExp_Date,", ",1,")
                    If drLB("Qty_Bal_Begin") > 0 Then
                        xAuto2 = xAuto2.Replace(",Item_Qty,", ",'" & CDbl(FormatNumber((drLB("Qty_Item_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",Weight,", ",'" & CDbl(FormatNumber((drLB("Weight_Bal_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",Volume,", ",'" & CDbl(FormatNumber((drLB("Volume_Bal_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",OrderItem_Price,", ",'" & CDbl(FormatNumber((drLB("OrderItem_Price_Begin") / drLB("Qty_Bal_Begin")) * xTotal_Qty, 6)) & "',")
                    Else
                        xAuto2 = xAuto2.Replace(",Item_Qty,", ",'" & CDbl(FormatNumber(xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",Weight,", ",'" & CDbl(FormatNumber(drLB("UnitWeight_Index") * xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",Volume,", ",'" & CDbl(FormatNumber(drLB("Unit_Volume") * xTotal_Qty, 6)) & "',")
                        xAuto2 = xAuto2.Replace(",OrderItem_Price,", ",'" & CDbl(FormatNumber(0, 6)) & "',")
                    End If
                    xAuto2 = xAuto2.Replace("Pallet_Qty", CDbl(FormatNumber(drLB("Pallet_Qty"), 6)))
                    xAuto2 = xAuto2.Replace("ERP_Location", "'" & drLB("ERP_Location").ToString & "'")
                    'Get Location Alias for putaway
                    xAuto2 = xAuto2.Replace(",Str5,", ",'" & drAj("TAG_No").ToString & "',")
                    Dim xLocation_Index As String = DBExeQuery_Scalar("select Location_Alias from ms_Location where Location_Index = '" & drAj("Location_Index").ToString & "'", _Connection, _myTrans)
                    xAuto2 = xAuto2.Replace(",Str4,", ",'" & xLocation_Index & "',")

                    xAuto2 = xAuto2.Replace("Plan_Process", "-9")
                    xAuto2 = xAuto2.Replace("DocumentPlan_No", "'" & drAj("Adjust_No").ToString & "'")
                    xAuto2 = xAuto2.Replace("DocumentPlan_Index", "'" & drAj("Adjust_Index").ToString & "'")
                    xAuto2 = xAuto2.Replace("add_by", "'" & WV_UserName & "'")
                    xAuto2 = xAuto2.Replace("add_date", "getdate()")
                    xAuto2 = xAuto2.Replace(",Status", ",1")
                    xSQL = " INSERT INTO tb_OrderItem (" & xAuto1 & ")"
                    xSQL &= " SELECT " & xAuto2 & " FROM tb_OrderItem WHERE OrderItem_Index = '" & drLB("OrderItem_Index").ToString & "'"
                    DBExeNonQuery(xSQL, _Connection, _myTrans)

                Next
            End If
            '------------------------------------------------------------------------------------------------------------------------
            _myTrans.Commit()
            '------------------------------------------------------------------------------------------------------------------------
            'ยืนยันใบเบิก
            Dim xPASS As String = "PASS"
            If dtLocationBalance.Rows.Count > 0 Then
                If xWithdraw_Index.Trim.Length > 0 Then
                    If dtLocationBalance.Rows.Count > 0 Then
                        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
                        xPASS = objClassDB.Withdraw_Confirm(xWithdraw_Index)
                    End If
                End If
            End If

            'ยืนยันใบรับ
            If xOrder_Index.Trim.Length > 0 Then
                If xPASS = "PASS" Then 'ยืนยันเบิกผ่าน
                    Dim fAuto As New frmImport_Order_StockBalance
                    fAuto.AutoGenTag(xOrder_Index)
                    xPASS = fAuto.PutAwayWith_Suggest_Location(xOrder_Index)
                End If
            End If

            If xPASS <> "PASS" Then
                'ยกเลิกรับ
                If xOrder_Index.Trim.Length > 0 Then
                    Dim oblst As New bl_ImportOrder_Stock_TNP
                    oblst.Delete_OrderTransactionAuto(xOrder_Index)
                End If
                'ยกเลิกจ่าย
                If xWithdraw_Index.Trim.Length > 0 Then
                    Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
                    objClassDB.Cancel_Withdraw(xWithdraw_Index)
                End If
            End If

            '------------------------------------------------------------------------------------------------------------------------
            'ยืนยันใบสั่งตรจนับ
            xSQL = "UPDATE tb_Adjust  "
            xSQL &= " SET status =" & pstrStatus & ","
            xSQL &= "update_by='" & WV_UserName & "',"
            xSQL &= "update_date=getdate(),"
            xSQL &= "update_branch =" & WV_Branch_ID
            xSQL &= " WHERE  Adjust_Index='" & pstrAdjust_index & "' "
            xSQL &= " UPDATE tb_AdjustItemLocation SET Status =-9,Adjust_Qty = Qty_1st_Count WHERE Adjust_Index='" & pstrAdjust_index & "' "
            DBExeNonQuery(xSQL)
            '------------------------------------------------------------------------------------------------------------------------

            Return True

        Catch ex As Exception
            _myTrans.Rollback()
            Throw ex
        Finally
            SQLServerCommand.Connection.Close()
        End Try

    End Function


    'Public Function Confirm_Adjust_Status_AutoMove_KSL(ByVal pstrAdjust_index As String, ByVal pstrStatus As String) As Boolean
    '    'Init. connection
    '    Dim _Connection As SqlClient.SqlConnection = Nothing
    '    Dim _myTrans As SqlClient.SqlTransaction = Nothing
    '    _Connection = Connection
    '    With SQLServerCommand
    '        .Connection = _Connection
    '        If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '        .Connection.Open()
    '        .Transaction = _myTrans
    '    End With
    '    _myTrans = Connection.BeginTransaction()
    '    Try
    '        'KSL : Adjust auto move. picking and transferstatus
    '        '------------------------------------------------------------------------------------------------------------------------
    '        Dim TypePicking As New WMS_Site_KingStella.PICKING.enmPicking_Type
    '        Dim objPicking As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '        Dim Result As New DataTable
    '        Dim dtLocationBalance As New DataTable
    '        Dim strWhere As String = ""
    '        Dim xSQL As String = ""
    '        xSQL &= " SELECT AJ.DocumentType_Index,AJ.Customer_Index,S.Package_Index,AJ.Adjust_No,AJI.*"
    '        xSQL &= " FROM tb_AdjustItemLocation AJI "
    '        xSQL &= "   Inner join tb_Adjust AJ ON AJ.Adjust_Index = AJI.Adjust_Index"
    '        xSQL &= "   Inner join ms_SKU S ON S.Sku_Index = AJI.Sku_Index"
    '        xSQL &= String.Format("  WHERE AJ.Adjust_Index = '{0}'", pstrAdjust_index)
    '        Dim dt As New DataTable
    '        dt = DBExeQuery(xSQL, _Connection, _myTrans)
    '        For Each drAj As DataRow In dt.Rows
    '            Result = New DataTable
    '            strWhere = String.Format(" AND TAG_No = '{0}'", drAj("TAG_No").ToString)
    '            TypePicking = PICKING.enmPicking_Type.FIFO
    '            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, drAj("Sku_Index").ToString, drAj("Package_Index").ToString, drAj("Qty_1st_Count"), strWhere, "")
    '            'objPicking.useAutoAdjust = True
    '            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, "")
    '            'Reset New Status, Location
    '            For Each drBal As DataRow In Result.Rows
    '                drBal("To_Location_Index") = drAj("Location_Index").ToString
    '                drBal("New_ItemStatus_Index") = drAj("ItemStatus_Index").ToString 'Keep for old
    '            Next
    '            If Result IsNot Nothing Then
    '                If Result.Rows.Count > 0 Then
    '                    dtLocationBalance.Merge(Result)
    '                End If
    '            End If

    '        Next
    '        'สร้างใบโอนย้ายสินค้า Auto
    '        Dim objAutoTranfer As New GrouppingConditionPicking
    '        Dim TransferStatus_Index As String = objAutoTranfer.AutoTransfer_KSL(dtLocationBalance, dt.Rows(0)("Customer_Index").ToString, "", "", "", "", _Connection, _myTrans)
    '        xSQL = String.Format(" UPDATE tb_TransferStatus set Status_Fullfill = 0, Ref_No1 = '{0}' ,Comment = '{2}'  WHERE TransferStatus_Index  = '{1}' ", dt.Rows(0)("Adjust_No").ToString, TransferStatus_Index, "โอนย้ายจากการตรวจนับ")
    '        xSQL &= String.Format(" UPDATE tb_TransferStatusLocation set Status_Fullfill = 0,Tag_NoNew = Tag_No  WHERE TransferStatus_Index  = '{0}' ", TransferStatus_Index)
    '        xSQL &= String.Format(" UPDATE tb_LocationBalance set ReserveQty = 0,ReserveWeight = 0,ReserveVolume = 0,ReserveQty_Item = 0,ReserveOrderItem_Price = 0 WHERE LocationBalance_Index in (select From_LocationBalance_Index from tb_TransferStatusLocation where TransferStatus_Index  = '{0}') ", TransferStatus_Index)
    '        xSQL &= " UPDATE tb_LocationBalance set Qty_Bal=0,Weight_Bal=0,Volume_Bal=0,Qty_Item_Bal=0,OrderItem_Price_Bal = 0  WHERE Qty_Bal < 0 "
    '        xSQL &= " UPDATE tb_LocationBalance set ReserveQty = 0,ReserveWeight = 0,ReserveVolume = 0,ReserveQty_Item = 0,ReserveOrderItem_Price = 0  WHERE Qty_Bal <= 0 "
    '        DBExeNonQuery(xSQL, _Connection, _myTrans)
    '        '------------------------------------------------------------------------------------------------------------------------
    '        Dim strSQL As String = ""
    '        strSQL = "UPDATE tb_Adjust  "
    '        strSQL &= " SET status =" & pstrStatus & ","
    '        strSQL &= "update_by='" & WV_UserName & "',"
    '        strSQL &= "update_date=getdate(),"
    '        strSQL &= "update_branch =" & WV_Branch_ID
    '        strSQL &= " WHERE  Adjust_Index='" & pstrAdjust_index & "' "
    '        strSQL &= " UPDATE tb_AdjustItemLocation SET Status =-9,Adjust_Qty = Qty_1st_Count WHERE Adjust_Index='" & pstrAdjust_index & "' "
    '        DBExeNonQuery(strSQL, _Connection, _myTrans)
    '        '------------------------------------------------------------------------------------------------------------------------
    '        _myTrans.Commit()

    '        Return True

    '    Catch ex As Exception
    '        _myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        SQLServerCommand.Connection.Close()
    '    End Try

    'End Function



    'Private Sub AutoGenTag(ByVal pstrOrder_Index As String)
    '    Dim objItemCollection As New List(Of tb_TAG_TNP)

    '    Dim objDBTempIndex As New Sy_AutoNumber
    '    Dim objAutoNumber As New Sy_AutoNumber

    '    Try
    '        Dim odtOrderItemNonTag As DataTable = Me.GetOrderItemNonTag(pstrOrder_Index)
    '        If odtOrderItemNonTag.Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '        For Each odrOrderItem As DataRow In odtOrderItemNonTag.Rows
    '            'Normal Gen
    '            Dim otb_TAG As New tb_TAG_TNP(tb_TAG_TNP.enuOperation_Type.ADDNEW)
    '            otb_TAG = Me.SetTagItem(odrOrderItem, otb_TAG)
    '            otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
    '            otb_TAG.TAG_No = otb_TAG.Pallet_No
    '            'otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
    '            If otb_TAG.TAG_No = "" Then
    '                otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
    '            End If
    '            otb_TAG.Suggest_Location_Index = ""
    '            objItemCollection.Add(otb_TAG)
    '        Next
    '        Dim objItemA As New tb_TAG_TNP(tb_TAG_TNP.enuOperation_Type.ADDNEW)

    '        objItemA.objItemCollection = objItemCollection
    '        objItemA.InsertData()


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDBTempIndex = Nothing
    '        objAutoNumber = Nothing
    '    End Try

    'End Sub
    'Private Function GetOrderItemNonTag(ByVal pstrOrder_Index As String) As DataTable

    '    Dim objItem As New tb_TAG_TNP(tb_TAG_TNP.enuOperation_Type.SEARCH)
    '    Try
    '        objItem.getOrderItemNonTag(pstrOrder_Index)
    '        Dim odtItem As DataTable
    '        odtItem = objItem.GetDataTable
    '        Return odtItem
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Public Function SetTagItem(ByVal podrOrderItem As DataRow, ByVal poTagItem As tb_TAG_TNP) As tb_TAG_TNP
    '    Try

    '        With podrOrderItem
    '            'poTagItem.Suggest_Location_Index = getLocation_Index(.Item("Str4").ToString) 'Location_Alias From OrderItem in Str4
    '            poTagItem.Order_No = .Item("Order_No").ToString
    '            poTagItem.Order_Index = .Item("Order_Index").ToString
    '            poTagItem.Order_Date = .Item("Order_Date").ToString
    '            poTagItem.Order_Time = .Item("Order_Time").ToString
    '            poTagItem.Customer_Index = .Item("Customer_Index").ToString

    '            If .Item("Supplier_Index").ToString IsNot Nothing Then
    '                poTagItem.Supplier_Index = .Item("Supplier_Index").ToString
    '            Else
    '                poTagItem.Supplier_Index = ""
    '            End If

    '            If .Item("OrderItem_Index").ToString IsNot Nothing Then
    '                poTagItem.OrderItem_Index = .Item("OrderItem_Index").ToString
    '            Else
    '                poTagItem.OrderItem_Index = ""
    '            End If

    '            poTagItem.OrderItemLocation_Index = ""
    '            If .Item("Sku_Index").ToString IsNot Nothing Then
    '                poTagItem.Sku_Index = .Item("Sku_Index").ToString
    '            Else
    '                poTagItem.Sku_Index = ""
    '            End If

    '            If .Item("PLot").ToString IsNot Nothing Then
    '                poTagItem.PLot = .Item("PLot").ToString
    '            Else
    '                poTagItem.PLot = ""
    '            End If

    '            If .Item("ItemStatus_Index").ToString IsNot Nothing Then
    '                poTagItem.ItemStatus_Index = .Item("ItemStatus_Index").ToString
    '            Else
    '                poTagItem.ItemStatus_Index = ""
    '            End If

    '            If .Item("Package_Index").ToString IsNot Nothing Then
    '                poTagItem.Package_Index = .Item("Package_Index").ToString
    '            Else
    '                poTagItem.Package_Index = ""
    '            End If

    '            poTagItem.Unit_Weight = 0
    '            poTagItem.Size_Index = -1


    '            'If .Item("PalletType_Index").ToString IsNot Nothing Then
    '            '    poTagItem.Pallet_No = .Item("PalletType_Index").ToString
    '            'Else
    '            '    poTagItem.Pallet_No = ""
    '            'End If


    '            poTagItem.TAG_Status = 0

    '            If .Item("str1").ToString IsNot Nothing Then
    '                poTagItem.Ref_No1 = .Item("str1").ToString
    '            Else
    '                poTagItem.Ref_No1 = ""
    '            End If

    '            If .Item("str2").ToString IsNot Nothing Then
    '                poTagItem.Ref_No2 = .Item("str2").ToString
    '            Else
    '                poTagItem.Ref_No2 = ""
    '            End If


    '            If .Item("Qty").ToString IsNot Nothing Then
    '                poTagItem.Qty = .Item("Qty").ToString
    '            Else
    '                poTagItem.Qty = 0
    '            End If
    '            If .Item("Weight").ToString IsNot Nothing Then
    '                poTagItem.Weight = .Item("Weight").ToString
    '            Else
    '                poTagItem.Weight = 0
    '            End If
    '            If .Item("Volume").ToString IsNot Nothing Then
    '                poTagItem.Volume = .Item("Volume").ToString
    '            Else
    '                poTagItem.Volume = 0
    '            End If

    '            'dong_Edit cal weigth,Volume
    '            Dim objOrderItem As New tb_OrderItem
    '            Dim dt As New DataTable
    '            objOrderItem.searchByOrderItem_Index(poTagItem.OrderItem_Index)
    '            dt = objOrderItem.GetDataTable()
    '            poTagItem.Qty_per_TAG = poTagItem.Qty
    '            If dt.Rows.Count > 0 Then
    '                poTagItem.Weight_per_TAG = dt.Rows(0).Item("Weight_Bal") 'poTagItem.Weight
    '                poTagItem.Volume_per_TAG = dt.Rows(0).Item("Volume_Bal") ' poTagItem.Volume
    '                poTagItem.Pallet_No = dt.Rows(0).Item("Str5").ToString
    '            Else
    '                poTagItem.Weight_per_TAG = poTagItem.Weight
    '                poTagItem.Volume_per_TAG = poTagItem.Volume
    '            End If
    '            poTagItem.Ref_No3 = "1/1"

    '        End With

    '        Return poTagItem
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Private Function PutAwayWith_Suggest_Location(ByVal pstrOrder_Index As String) As String
    '    Dim dtTemDataSource As DataTable = New DataTable
    '    Dim objOrderItemLocation As New tb_OrderItemLocation
    '    Dim objCollection As New List(Of tb_OrderItemLocation)
    '    Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '    Dim intjRow As Integer = 0

    '    Try
    '        Dim dbmMaxPalletFix As Double = 99

    '        Dim oTag As New tb_TAG_TNP(tb_TAG_TNP.enuOperation_Type.SEARCH)
    '        oTag.getPutawayWithTAG(pstrOrder_Index)
    '        dtTemDataSource = oTag.DataTable

    '        If dtTemDataSource.Rows.Count = 0 Then
    '            'If Me.BwgImport.CancellationPending = False Then
    '            '    Me.BwgImport.CancelAsync()
    '            Return "System Error " & pstrOrder_Index.ToString
    '            'End If
    '        End If

    '        'Checking Location

    '        '*** Begin : Generate Pallet Seq
    '        'Dim oml_Timgo As New ml_Timco(ml_Timco.enuOperation_Type.SEARCH)
    '        Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '        Dim strTo_Location_Index As String = ""
    '        'Dim odtTempPallet_Location As New DataTable


    '        Dim strTempPallet_No As String = ""
    '        Dim dblMax_Pallet As Double = 0

    '        'Dim drArrCheckData() As DataRow

    '        'Dim oGentPallet As New ml_Timco(ml_Timco.enuOperation_Type.SEARCH)
    '        'Dim oObject() As Object

    '        ''For Each drSaveOIL As DataRow In dtTemDataSource.Rows
    '        ''    oblst.UpdateSys_Suggest_Location(drSaveOIL("Suggest_Location_Alias_TMP").ToString, drSaveOIL("TAG_Index").ToString)
    '        ''Next

    '        'dtTemDataSource.AcceptChanges()
    '        'drArrCheckData = dtTemDataSource.Select("isnull(Suggest_Location_Alias_TMP,'') = '' or isnull(Pallet_No,'')=''")
    '        'If drArrCheckData.Length > 0 Then
    '        '    Return ("ตำแหน่ง : " & drArrCheckData(0)("Suggest_Location_Alias_TMP").ToString & Chr(13) & "Pallet No เกิน กรุณาเลือก Location ใหม่")
    '        '    'Exit Function
    '        'End If

    '        '*** END : Generate Pallet Seq

    '        '*********************************** begin : Save PutAway and Generate Pallet  ***********************************
    '        'Save Transaction
    '        'Before New Gen
    '        'Update เพราะว่าถ้ามีการ save หลายครั้งอาจทำ
    '        Dim oblst As New bl_ImportOrder_Stock_TNP
    '        oblst.UpdateSys_Value_MaxIndexTable("tb_OrderItemLocation", "OrderItemLocation_Index") 'objOrderItemLocation.OrderItemLocation_Index = objDBIndex.getSys_Value("OrderItemLocation_Index")

    '        Dim objCollection_CollOrderItemLocation As New List(Of List(Of tb_OrderItemLocation))
    '        Dim objCollection_JobOrder As New List(Of tb_JobOrder)

    '        'Dim oml_Timco As New ml_Timco(ml_Timco.enuOperation_Type.UPDATE)
    '        For Each drSaveOIL As DataRow In dtTemDataSource.Rows
    '            'update Suggest Location
    '            drSaveOIL("Suggest_Location_Index") = getlocation_index(drSaveOIL("Str4").ToString) 'From tb_Orderitem
    '            oblst.UpdateSys_Suggest_Location(drSaveOIL("Suggest_Location_Index"), drSaveOIL("TAG_Index").ToString)
    '            ' If drSaveOIL("Suggest_Location_Index").ToString <> "" Then
    '            Dim objReturn() As Object
    '            objReturn = Me.SetLocation_fromitemAll(drSaveOIL, pstrOrder_Index) 'Dong_kk Modify Date. 2012/02/29 Move in Transaction
    '            If objReturn Is Nothing Then
    '                Return "System Error"
    '            Else
    '                objCollection_CollOrderItemLocation.Add(objReturn(0))
    '                objCollection_JobOrder.Add(objReturn(1))
    '            End If

    '            'Update For Timco 'Dong_kk Modify Date. 2012/02/29 Move in Transaction
    '            'oml_Timco.Update_PalletNo(drSaveOIL("Tag_No"), drSaveOIL("OrderItem_Index"), drSaveOIL("Pallet_No").ToString.ToUpper, False)
    '            'Else
    '            'Return ("Location . " & drSaveOIL("Suggest_Location_Alias_TMP").ToString & Chr(13) & "Pallet No เกิน กรุณาเลือก Location ใหม่")
    '            ' End If
    '        Next

    '        oblst = Nothing
    '        '*********************************** begin : PutAway  ***********************************

    '        'PutAway
    '        Dim objJobOrder As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
    '        objJobOrder.SaveData_PutAway(pstrOrder_Index, objCollection_CollOrderItemLocation, objCollection_JobOrder)


    '        '20150109
    '        'Update ERP LOCATION BY OrderItem col str1
    '        Dim objIMP As New bl_ImportOrder_Stock_TNP
    '        objIMP.fncUpdateERPLocation(pstrOrder_Index)
    '        Return "PASS"

    '    Catch ex As Exception
    '        Return ex.Message.ToString & ": Row " & intjRow & " "
    '    Finally

    '    End Try

    'End Function

    'Function SetLocation_fromitemAll(ByVal drSaveOIL As DataRow, ByVal pstrOrder_Index As String) As Object()
    '    Try
    '        Dim objOrderItemLocation As New tb_OrderItemLocation
    '        Dim objOrderItem As New tb_OrderItem
    '        Dim objCollection As New List(Of tb_OrderItemLocation)
    '        Dim clsJobOrder As New tb_JobOrder

    '        clsJobOrder = setObjJobOrder(drSaveOIL)
    '        '  objOrderItem = getProductSelection(Me.OrderItem_Id) '(Me.grdOrderItem.Rows(RowIndex).Cells("OrderItem_Index").Value)
    '        objCollection.Clear()

    '        Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '        Dim sumQty As Double = 0
    '        Dim sumTotal_Qty As Double = 0
    '        Dim Recieve_Qty As Double = CDbl(drSaveOIL("Qty_per_TAG").ToString)
    '        Dim Recieve_Total_Qty As Double = CDbl(drSaveOIL("Total_Qty").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Total_Qty").Value)

    '        objOrderItemLocation = New tb_OrderItemLocation

    '        With objOrderItemLocation
    '            .Order_Index = pstrOrder_Index
    '            .OrderItem_Index = drSaveOIL("OrderItem_Index").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Col_OrderItem_Index").Value.ToString
    '            .Tag_No = drSaveOIL("Tag_No").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
    '            .Package_Index = drSaveOIL("Package_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_Package_Index").Value.ToString
    '            .PLot = drSaveOIL("Plot").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("clPlot").Value.ToString
    '            .ItemStatus_Index = drSaveOIL("ItemStatus_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_ItemStatus_Index").Value.ToString
    '            .Serial_No = ""
    '            .Location_Index = drSaveOIL("Suggest_Location_Index").ToString 'objClassDB.getLocation_Index(drSaveOIL("Suggest_Location_Alias").ToString)
    '            .PalletType_Index = ""
    '            .Pallet_Qty = 0
    '            .Sku_Index = drSaveOIL("Sku_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("Col_Sku_Index").Value.ToString
    '            .Qty = Recieve_Qty
    '            .Total_Qty = Recieve_Total_Qty
    '            .Ratio = CDbl(drSaveOIL("Ratio").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Ratio").Value)
    '            .Weight = CDbl(drSaveOIL("Weight_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_weight").Value.ToString)
    '            .Volume = CDbl(drSaveOIL("Volume_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_Volume").Value.ToString)
    '            .MixPallet = 0

    '            .Add_by = WV_UserFullName 'drSaveOIL("Pallet_No").ToString

    '            If IsNumeric(drSaveOIL("Qty_Per_Pck").ToString) Then .Qty_Item = CDbl(drSaveOIL("Qty_Per_Pck").ToString) * .Qty
    '            If IsNumeric(drSaveOIL("Price_Per_Pck").ToString) Then .OrderItem_Price = CDbl(drSaveOIL("Price_Per_Pck").ToString) * .Qty


    '            ' *** Sum Balance ***
    '            sumQty = sumQty + .Qty
    '            sumTotal_Qty = sumTotal_Qty + .Total_Qty
    '            .TAG_Index = drSaveOIL("TAG_Index").ToString
    '        End With

    '        objCollection.Add(objOrderItemLocation)


    '        Dim objReturn(1) As Object
    '        objReturn(0) = objCollection
    '        objReturn(1) = clsJobOrder

    '        Return objReturn
    '        ' *** Check Balance ***
    '        'If Not Recieve_Qty = sumQty Then
    '        '    MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        '    Exit Sub
    '        'End If



    '        ' *** Save Data ***
    '        'Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
    '        'objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
    '        'objJobLocation = Nothing

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Function setObjJobOrder(ByVal drSaveOIL As DataRow) As Object

    '    Dim objJobOrder As New tb_JobOrder
    '    objJobOrder.JobOrder_Index = drSaveOIL("Order_Index").ToString
    '    objJobOrder.JobOrder_Date = Now
    '    objJobOrder.JobOrder_No = drSaveOIL("Order_No").ToString

    '    ' *************************************
    '    ' Current System Use tb_Order with tb_JobOrder by 1:1  
    '    '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
    '    objJobOrder.Order_Index = drSaveOIL("Order_Index").ToString
    '    ' *************************************

    '    objJobOrder.Str1 = ""
    '    objJobOrder.Str2 = ""
    '    objJobOrder.Str3 = ""
    '    objJobOrder.Str4 = ""
    '    objJobOrder.Str5 = ""

    '    Return objJobOrder
    'End Function

End Class
