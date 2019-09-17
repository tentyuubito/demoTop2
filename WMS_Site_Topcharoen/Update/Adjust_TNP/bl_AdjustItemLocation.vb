'*** Create Date :  04/03/2008
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports System.Data

Public Class bl_AdjustItemLocation : Inherits DBType_SQLServer

    '*** Fileds

#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _AdjustItemLocation_Index As String = ""
    Private _Adjust_Index As String = ""
    Private _Tag_No As String = ""
    Private _Location_Index As String = ""
    Private _Order_Index As String = ""
    Private _Sku_Index As String = ""
    Private _Lot_No As String = ""
    Private _Plot As String = ""
    Private _ItemStatus_Index As String = ""
    Private _Serial_No As String = ""
    Private _ItemComment As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Status As Integer = 0
    Private _Qty_Bal As Double = 0
    Private _Weight_Bal As Double = 0
    Private _Volume_Bal As Double = 0
    Private _Qty_1st_Count As Double = 0
    Private _Qty_2nd_Count As Double = 0
    Private _Qty_3rd_Count As Double = 0
    Private _Weight_1st_Count As Double = 0
    Private _Weight_2nd_Count As Double = 0
    Private _Weight_3rd_Count As Double = 0
    Private _Volume_1st_Count As Double = 0
    Private _Volume_2nd_Count As Double = 0
    Private _Volume_3rd_Count As Double = 0
    Private _Adjust_Qty As Double = 0
    Private _Adjust_Weight As Double = 0
    Private _Adjust_Volume As Double = 0
    Private _Flo1 As Double = 0
    Private _Flo2 As Double = 0
    Private _Flo3 As Double = 0
    Private _Flo4 As Double = 0
    Private _Flo5 As Double = 0
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer
    Private _LocationBalance_Index As String = ""
    Private _NewItem As Integer

    Private _Seq As Integer = 0

#End Region

#Region " Properties "
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
    Public Property AdjustItemLocation_Index() As String
        Get
            Return _AdjustItemLocation_Index
        End Get
        Set(ByVal Value As String)
            _AdjustItemLocation_Index = Value
        End Set
    End Property

    Public Property Adjust_Index() As String
        Get
            Return _Adjust_Index
        End Get
        Set(ByVal Value As String)
            _Adjust_Index = Value
        End Set
    End Property

    Public Property Tag_No() As String
        Get
            Return _Tag_No
        End Get
        Set(ByVal Value As String)
            _Tag_No = Value
        End Set
    End Property

    Public Property Location_Index() As String
        Get
            Return _Location_Index
        End Get
        Set(ByVal Value As String)
            _Location_Index = Value
        End Set
    End Property

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal Value As String)
            _Order_Index = Value
        End Set
    End Property

    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal Value As String)
            _Sku_Index = Value
        End Set
    End Property

    Public Property Lot_No() As String
        Get
            Return _Lot_No
        End Get
        Set(ByVal Value As String)
            _Lot_No = Value
        End Set
    End Property

    Public Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal Value As String)
            _Plot = Value
        End Set
    End Property

    Public Property ItemStatus_Index() As String
        Get
            Return _ItemStatus_Index
        End Get
        Set(ByVal Value As String)
            _ItemStatus_Index = Value
        End Set
    End Property

    Public Property Serial_No() As String
        Get
            Return _Serial_No
        End Get
        Set(ByVal Value As String)
            _Serial_No = Value
        End Set
    End Property

    Public Property ItemComment() As String
        Get
            Return _ItemComment
        End Get
        Set(ByVal Value As String)
            _ItemComment = Value
        End Set
    End Property

    Public Property Str1() As String
        Get
            Return _Str1
        End Get
        Set(ByVal Value As String)
            _Str1 = Value
        End Set
    End Property

    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal Value As String)
            _Str2 = Value
        End Set
    End Property

    Public Property Str3() As String
        Get
            Return _Str3
        End Get
        Set(ByVal Value As String)
            _Str3 = Value
        End Set
    End Property

    Public Property Str4() As String
        Get
            Return _Str4
        End Get
        Set(ByVal Value As String)
            _Str4 = Value
        End Set
    End Property

    Public Property Str5() As String
        Get
            Return _Str5
        End Get
        Set(ByVal Value As String)
            _Str5 = Value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal Value As Integer)
            _Status = Value
        End Set
    End Property

    Public Property Qty_Bal() As Double
        Get
            Return _Qty_Bal
        End Get
        Set(ByVal Value As Double)
            _Qty_Bal = Value
        End Set
    End Property

    Public Property Weight_Bal() As Double
        Get
            Return _Weight_Bal
        End Get
        Set(ByVal Value As Double)
            _Weight_Bal = Value
        End Set
    End Property

    Public Property Volume_Bal() As Double
        Get
            Return _Volume_Bal
        End Get
        Set(ByVal Value As Double)
            _Volume_Bal = Value
        End Set
    End Property

    Public Property Qty_1st_Count() As Double
        Get
            Return _Qty_1st_Count
        End Get
        Set(ByVal Value As Double)
            _Qty_1st_Count = Value
        End Set
    End Property

    Public Property Qty_2nd_Count() As Double
        Get
            Return _Qty_2nd_Count
        End Get
        Set(ByVal Value As Double)
            _Qty_2nd_Count = Value
        End Set
    End Property

    Public Property Qty_3rd_Count() As Double
        Get
            Return _Qty_3rd_Count
        End Get
        Set(ByVal Value As Double)
            _Qty_3rd_Count = Value
        End Set
    End Property

    Public Property Weight_1st_Count() As Double
        Get
            Return _Weight_1st_Count
        End Get
        Set(ByVal Value As Double)
            _Weight_1st_Count = Value
        End Set
    End Property

    Public Property Weight_2nd_Count() As Double
        Get
            Return _Weight_2nd_Count
        End Get
        Set(ByVal Value As Double)
            _Weight_2nd_Count = Value
        End Set
    End Property

    Public Property Weight_3rd_Count() As Double
        Get
            Return _Weight_3rd_Count
        End Get
        Set(ByVal Value As Double)
            _Weight_3rd_Count = Value
        End Set
    End Property

    Public Property Volume_1st_Count() As Double
        Get
            Return _Volume_1st_Count
        End Get
        Set(ByVal Value As Double)
            _Volume_1st_Count = Value
        End Set
    End Property

    Public Property Volume_2nd_Count() As Double
        Get
            Return _Volume_2nd_Count
        End Get
        Set(ByVal Value As Double)
            _Volume_2nd_Count = Value
        End Set
    End Property

    Public Property Volume_3rd_Count() As Double
        Get
            Return _Volume_3rd_Count
        End Get
        Set(ByVal Value As Double)
            _Volume_3rd_Count = Value
        End Set
    End Property

    Public Property Adjust_Qty() As Double
        Get
            Return _Adjust_Qty
        End Get
        Set(ByVal Value As Double)
            _Adjust_Qty = Value
        End Set
    End Property

    Public Property Adjust_Weight() As Double
        Get
            Return _Adjust_Weight
        End Get
        Set(ByVal Value As Double)
            _Adjust_Weight = Value
        End Set
    End Property

    Public Property Adjust_Volume() As Double
        Get
            Return _Adjust_Volume
        End Get
        Set(ByVal Value As Double)
            _Adjust_Volume = Value
        End Set
    End Property

    Public Property Flo1() As Double
        Get
            Return _Flo1
        End Get
        Set(ByVal Value As Double)
            _Flo1 = Value
        End Set
    End Property

    Public Property Flo2() As Double
        Get
            Return _Flo2
        End Get
        Set(ByVal Value As Double)
            _Flo2 = Value
        End Set
    End Property

    Public Property Flo3() As Double
        Get
            Return _Flo3
        End Get
        Set(ByVal Value As Double)
            _Flo3 = Value
        End Set
    End Property

    Public Property Flo4() As Double
        Get
            Return _Flo4
        End Get
        Set(ByVal Value As Double)
            _Flo4 = Value
        End Set
    End Property

    Public Property Flo5() As Double
        Get
            Return _Flo5
        End Get
        Set(ByVal Value As Double)
            _Flo5 = Value
        End Set
    End Property

    Public Property add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property

    Public Property add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property

    Public Property add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property

    Public Property update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property

    Public Property update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property

    Public Property update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property LocationBalance_Index() As String
        Get
            Return _LocationBalance_Index
        End Get
        Set(ByVal Value As String)
            _LocationBalance_Index = Value
        End Set
    End Property

    Public Property NewItem() As Integer
        Get
            Return _NewItem
        End Get
        Set(ByVal Value As Integer)
            _NewItem = Value
        End Set
    End Property
    Public Property Seq() As Integer
        Get
            Return _Seq
        End Get
        Set(ByVal Value As Integer)
            _Seq = Value
        End Set
    End Property

#End Region


#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Protected Overrides Sub Finalize()
        Dispose()
        MyBase.Finalize()
    End Sub
    Public Sub Dispose()
        'Class variable
        _dataTable = Nothing
        _scalarOutput = Nothing

        'Fields variable
    End Sub
    Public Sub Clear()
        _dataTable.Clear()
        _scalarOutput = ""

        'Fields variable 
    End Sub
#End Region

    '*** Normal DB Method
#Region " SELECT DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
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

    Public Sub getItemStatus_Adjust(ByVal pSku_Index As String, ByVal pLocation_Alias As String)
        Try
            Dim strSQL As String = ""
            strSQL = " select *  "
            strSQL &= " from ms_ItemStatus where isnull(Status_Id,1) <> -1 "
            strSQL &= "  AND  ItemStatus_Index IN (SELECT  tb_AdjustItemLocation.ItemStatus_Index From tb_AdjustItemLocation inner join "
            strSQL &= "                                     Ms_Location ON Ms_Location.Location_Index = tb_AdjustItemLocation.Location_Index "
            strSQL &= "                             WHERE   tb_AdjustItemLocation.Sku_Index='" & pSku_Index & "'"
            strSQL &= "                             AND     Ms_Location.Location_Alias='" & pLocation_Alias & "')"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function getSkuBarcode_index(ByVal Sku_BarCode As String) As String
        Dim strSQL, Sku_index As String
        connectDB()
        Try
            'Dim strSku_Index As String = ""
            'strSQL = "  SELECT Sku_Index FROM tb_Barcode_Mapping WHERE Barcode = '" & Sku_BarCode & "'"
            'SetSQLString = strSQL
            'EXEC_DataAdapter()
            '_dataTable = GetDataTable

            'If _dataTable Is Nothing Then
            '    Return ""
            'End If

            'If _dataTable.Rows.Count > 0 Then
            '    strSku_Index = _dataTable.Rows(0)("Sku_Index").ToString
            'Else
            '    Return ""
            'End If

            strSQL = " select Sku_Index "
            strSQL &= " from ms_sku where Barcode1 ='" & Sku_BarCode & "'"

            SetSQLString = strSQL
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Sku_index = _dataTable.Rows(0).Item("Sku_Index")
            Else
                Sku_index = ""
            End If
            Return Sku_index
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function getSku_index(ByVal Sku_BarCode As String) As String
        Dim strSQL, Sku_index As String
        connectDB()
        Try
            Dim strSku_Index As String = ""
            strSQL = "  SELECT Sku_Index FROM tb_Barcode_Mapping WHERE Barcode = '" & Sku_BarCode & "'"
            SetSQLString = strSQL
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable Is Nothing Then
                Return ""
            End If

            If _dataTable.Rows.Count > 0 Then
                strSku_Index = _dataTable.Rows(0)("Sku_Index").ToString
            Else
                Return ""
            End If

            strSQL = " select Sku_Index "
            strSQL &= " from ms_sku where Sku_Index ='" & strSku_Index & "'"

            SetSQLString = strSQL
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Sku_index = _dataTable.Rows(0).Item("Sku_Index")
            Else
                Sku_index = ""
            End If
            Return Sku_index
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
    Public Function insert_Adjust_Quantity() As Boolean
        Try
            Dim strsql As String

            strsql = " DELETE tb_AdjustItemLocation WHERE AdjustItemLocation_Index ='" & AdjustItemLocation_Index & "' "
            If Qty_1st_Count = 0 Then
                strsql &= " DELETE tb_AdjustItemLocation WHERE Adjust_Index = '" & Adjust_Index & "' and Location_Index ='" & Location_Index & "'  and Status = -9 and NewItem = 1  "
            End If
            SetSQLString = strsql
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            strsql = " insert into tb_AdjustItemLocation (AdjustItemLocation_Index,Adjust_Index,Tag_No,Location_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Serial_No,ItemComment,Str1,Str2,Str3,Str4,Str5,Status,Qty_Bal,Weight_Bal,Volume_Bal,Qty_1st_Count,Qty_2nd_Count,Qty_3rd_Count,Weight_1st_Count,Weight_2nd_Count,Weight_3rd_Count,Volume_1st_Count,Volume_2nd_Count,Volume_3rd_Count,Adjust_Qty,Adjust_Weight,Adjust_Volume,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,LocationBalance_Index,NewItem)"
            strsql &= " values (@AdjustItemLocation_Index,@Adjust_Index,@Tag_No,@Location_Index,@Order_Index,@Sku_Index,@Lot_No,@Plot,@ItemStatus_Index,@Serial_No,@ItemComment,@Str1,@Str2,@Str3,@Str4,@Str5,@Status,@Qty_Bal,@Weight_Bal,@Volume_Bal,@Qty_1st_Count,@Qty_2nd_Count,@Qty_3rd_Count,@Weight_1st_Count,@Weight_2nd_Count,@Weight_3rd_Count,@Volume_1st_Count,@Volume_2nd_Count,@Volume_3rd_Count,@Adjust_Qty,@Adjust_Weight,@Adjust_Volume,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@LocationBalance_Index,@NewItem)"
            '  **** Generate MovementItemLocation_Index  ***
            Dim objDBItemIndex As New Sy_AutoNumber
            AdjustItemLocation_Index = objDBItemIndex.getSys_Value("AdjustItemLocation_Index")
            objDBItemIndex = Nothing
            ' *******************************************

            strsql = strsql
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@AdjustItemLocation_Index", SqlDbType.VarChar, 13).Value = AdjustItemLocation_Index
                .Parameters.Add("@Adjust_Index", SqlDbType.VarChar, 13).Value = Adjust_Index
                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = Location_Index
                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = Sku_Index
                .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = Lot_No
                .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = Plot
                .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = ItemStatus_Index
                .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = Serial_No
                .Parameters.Add("@ItemComment", SqlDbType.NVarChar, 100).Value = ItemComment
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = Str5
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = _Status
                .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_Bal
                .Parameters.Add("@Weight_Bal", SqlDbType.Float, 8).Value = Weight_Bal
                .Parameters.Add("@Volume_Bal", SqlDbType.Float, 8).Value = Volume_Bal

                .Parameters.Add("@Qty_1st_Count", SqlDbType.Float, 8).Value = Qty_1st_Count
                .Parameters.Add("@Qty_2nd_Count", SqlDbType.Float, 8).Value = Qty_2nd_Count
                .Parameters.Add("@Qty_3rd_Count", SqlDbType.Float, 8).Value = Qty_3rd_Count

                .Parameters.Add("@Weight_1st_Count", SqlDbType.Float, 8).Value = Weight_1st_Count
                .Parameters.Add("@Weight_2nd_Count", SqlDbType.Float, 8).Value = Weight_2nd_Count
                .Parameters.Add("@Weight_3rd_Count", SqlDbType.Float, 8).Value = Weight_3rd_Count

                .Parameters.Add("@Volume_1st_Count", SqlDbType.Float, 8).Value = Volume_1st_Count
                .Parameters.Add("@Volume_2nd_Count", SqlDbType.Float, 8).Value = Volume_2nd_Count
                .Parameters.Add("@Volume_3rd_Count", SqlDbType.Float, 8).Value = Volume_3rd_Count

                .Parameters.Add("@Adjust_Qty", SqlDbType.Float, 8).Value = Adjust_Qty
                .Parameters.Add("@Adjust_Weight", SqlDbType.Float, 8).Value = Adjust_Weight
                .Parameters.Add("@Adjust_Volume", SqlDbType.Float, 8).Value = Adjust_Volume

                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = LocationBalance_Index
                .Parameters.Add("@NewItem", SqlDbType.VarChar, 13).Value = NewItem


            End With

            SetSQLString = strsql
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()
            strsql = Nothing
            Return True
        Catch ex As Exception
            Return False
            Throw ex
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

            strSQL &= " WHERE AdjustItemLocation_Index=@AdjustItemLocation_Index "

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@Adjust_Index", SqlDbType.VarChar, 13).Value = Adjust_Index
                .Parameters.Add("@AdjustItemLocation_Index", SqlDbType.VarChar, 13).Value = AdjustItemLocation_Index
                .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = iSeq
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
#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
#End Region
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
#End Region
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [ 04/03/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "
#End Region
End Class