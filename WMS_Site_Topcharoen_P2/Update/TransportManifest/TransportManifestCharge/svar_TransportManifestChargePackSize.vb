Imports System.Data
Imports System.Data.SqlClient
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Public Class svar_TransportManifestChargePackSize : Inherits DBType_SQLServer
#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _TransportManifestChargePackSize_Index As String = ""
    Private _Carrier_Index As String = ""
    Private _Customer_Index As String = ""
    Private _PackSize_Index As String = ""
    Private _TransportJobType_Index As String = ""
    Private _Customer_Shipping_Location_Index As String = ""
    Private _TransportManifestChargePackSizeGroup_Id As Integer = 0
    Private _Rate As Double = 0
    Private _Minimum_Drop As Double = 0
    Private _Minimum_Rate As Double = 0

    Private _Description As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Flo1 As Double = 0
    Private _Flo2 As Double = 0
    Private _Flo3 As Double = 0
    Private _Flo4 As Double = 0
    Private _Flo5 As Double = 0
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer = 0
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer = 0
    Private _status_id As Integer = 0

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
    Public Property TransportManifestChargePackSize_Index() As String
        Get
            Return _TransportManifestChargePackSize_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifestChargePackSize_Index = Value
        End Set
    End Property

    Public Property Carrier_Index() As String
        Get
            Return _Carrier_Index
        End Get
        Set(ByVal Value As String)
            _Carrier_Index = Value
        End Set
    End Property
    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property

    Public Property PackSize_Index() As String
        Get
            Return _PackSize_Index
        End Get
        Set(ByVal Value As String)
            _PackSize_Index = Value
        End Set
    End Property

    Public Property TransportJobType_Index() As String
        Get
            Return _TransportJobType_Index
        End Get
        Set(ByVal Value As String)
            _TransportJobType_Index = Value
        End Set
    End Property

    Public Property Customer_Shipping_Location_Index() As String
        Get
            Return _Customer_Shipping_Location_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Location_Index = Value
        End Set
    End Property

    Public Property TransportManifestChargePackSizeGroup_Id() As Integer
        Get
            Return _TransportManifestChargePackSizeGroup_Id
        End Get
        Set(ByVal Value As Integer)
            _TransportManifestChargePackSizeGroup_Id = Value
        End Set
    End Property

    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal Value As Double)
            _Rate = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal Value As String)
            _Description = Value
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

    Public Property status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
        End Set
    End Property

    Public Property Minimum_Drop() As Double
        Get
            Return _Minimum_Drop
        End Get
        Set(ByVal Value As Double)
            _Minimum_Drop = Value
        End Set
    End Property
    Public Property Minimum_Rate() As Double
        Get
            Return _Minimum_Rate
        End Get
        Set(ByVal Value As Double)
            _Minimum_Rate = Value
        End Set
    End Property
#End Region
    Public Sub New()

    End Sub

#Region " SELECT DATA "

    Public Sub GetPackSize(ByVal pstrTransportManifestChargePackSize_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     *,Size_Index as PackSize_Index,0.0 as Rate "
            strSQL &= "  FROM ms_PackSize "
            strSQL &= "   WHERE    status_id <> -1 "

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


    Public Sub GetAllAsDataTable(ByVal pstrTransportManifestChargePackSize_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     *"
            strSQL &= "  FROM svar_TransportManifestChargePackSize "
            strSQL &= "   WHERE    status_id <> -1 "
            If pstrTransportManifestChargePackSize_Index <> "" Then
                strSQL &= "   AND    TransportManifestChargePackSize_Index ='" & pstrTransportManifestChargePackSize_Index & "'"
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

    Public Sub GetAllAsDataTable()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     * " & _
             " FROM svar_TransportManifestChargePackSize " & _
             " WHERE    status_id = 0 "
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
    Public Sub GetTransportManifestChargePackSize(ByVal strCondition As String)
        Dim strSQL As String = " "
        Try
            strSQL = "  SELECT  *  "
            strSQL &= " FROM    VIEW_TSS_TransportManifestChargePackSize"
            strSQL &= " WHERE   1=1 " & strCondition & " Order By TransportJobType_Index,Customer_Index,Carrier_Index,Customer_Shipping_Location_Index,PackSize_Index"
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

    Public Sub getTransportManifestChargePackSizeShowCarrier(ByVal pstrCustomer_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " exec dbo.spTransportManifestChargePackSizeShowCarrier '" & pstrCustomer_Index & "' "


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
    Public Sub GetTransportPayment(ByVal pstrCustomer_Index As String, ByVal pstrCarrier_Index As String, ByVal strTransportJobType_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " exec dbo.spTransportManifestChargePackSize '" & pstrCustomer_Index & "', '" & pstrCarrier_Index & "','" & strTransportJobType_Index & "'"

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

#Region " INSERT DATA "
    Public Function Insert() As String
        Dim strSQL As String = " "
        Try
            SetSQLString = " DELETE svar_TransportManifestChargePackSize WHERE Customer_Index=@Customer_Index" & _
            " AND Carrier_Index=@Carrier_Index" & _
            " AND TransportJobType_Index=@TransportJobType_Index" & _
            " AND Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index" & _
            " AND PackSize_Index=@PackSize_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _Carrier_Index
                .Add("@TransportJobType_Index", SqlDbType.VarChar, 13).Value = _TransportJobType_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _Customer_Shipping_Location_Index
                .Add("@PackSize_Index", SqlDbType.VarChar, 13).Value = _PackSize_Index
            End With
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            strSQL = " INSERT INTO svar_TransportManifestChargePackSize(Customer_Index,PackSize_Index,Carrier_Index,TransportJobType_Index,Customer_Shipping_Location_Index,Rate,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,update_by,update_date,update_branch,status_id)" & _
            "       VALUES(@Customer_Index,@PackSize_Index,@Carrier_Index,@TransportJobType_Index,@Customer_Shipping_Location_Index,@Rate,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@update_by,getdate(),@update_branch,@status_id)"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Add("@PackSize_Index", SqlDbType.VarChar, 13).Value = _PackSize_Index
                .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _Carrier_Index
                .Add("@TransportJobType_Index", SqlDbType.VarChar, 13).Value = _TransportJobType_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _Customer_Shipping_Location_Index
                .Add("@Rate", SqlDbType.Float, 15).Value = _Rate
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = "" '_Str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = "" '_Str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = "" '_Str3
                .Add("@Str4", SqlDbType.NVarChar, 2000).Value = "" '_Str4
                .Add("@Str5", SqlDbType.NVarChar, 2000).Value = "" '_Str5
                .Add("@Flo1", SqlDbType.Float, 15).Value = 0 '_Flo1
                .Add("@Flo2", SqlDbType.Float, 15).Value = 0 '_Flo2
                .Add("@Flo3", SqlDbType.Float, 15).Value = 0 '_Flo3
                .Add("@Flo4", SqlDbType.Float, 15).Value = 0 '_Flo4
                .Add("@Flo5", SqlDbType.Float, 15).Value = 0 '_Flo5
                '.Add("@add_by", SqlDbType.VarChar, 50).Value = _add_by
                '.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _add_date
                '.Add("@add_branch", SqlDbType.Int, 10).Value = _add_branch
                .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserFullName
                '.Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                .Add("@status_id", SqlDbType.Int, 10).Value = 1
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return "PASS"
        Catch ex As Exception
            Return ex.Message.ToString
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function
#End Region

#Region " UPDATE DATA "
    Public Sub Update()
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE svar_TransportManifestChargePackSize" & _
            " SET TransportManifestChargePackSize_Index=@TransportManifestChargePackSize_Index," & _
            "     Carrier_Index=@Carrier_Index," & _
            "     PackSize_Index=@PackSize_Index," & _
            "     TransportJobType_Index=@TransportJobType_Index," & _
            "     Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index," & _
            "     TransportManifestChargePackSizeGroup_Id=@TransportManifestChargePackSizeGroup_Id," & _
            "     Rate=@Rate," & _
            "     Description=@Description," & _
            "     Str1=@Str1," & _
            "     Str2=@Str2," & _
            "     Str3=@Str3," & _
            "     Str4=@Str4," & _
            "     Str5=@Str5," & _
            "     Flo1=@Flo1," & _
            "     Flo2=@Flo2," & _
            "     Flo3=@Flo3," & _
            "     Flo4=@Flo4," & _
            "     Flo5=@Flo5," & _
            "     add_by=@add_by," & _
            "     add_date=@add_date," & _
            "     add_branch=@add_branch," & _
            "     update_by=@update_by," & _
            "     update_date=@update_date," & _
            "     update_branch=@update_branch," & _
            "     status_id=@status_id " & _
            "           WHERE          TransportManifestChargePackSize_Index = @TransportManifestChargePackSize_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifestChargePackSize_Index", SqlDbType.VarChar, 13).Value = _TransportManifestChargePackSize_Index
                .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _Carrier_Index
                .Add("@PackSize_Index", SqlDbType.VarChar, 13).Value = _PackSize_Index
                .Add("@TransportJobType_Index", SqlDbType.VarChar, 13).Value = _TransportJobType_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _Customer_Shipping_Location_Index
                .Add("@TransportManifestChargePackSizeGroup_Id", SqlDbType.Int, 10).Value = _TransportManifestChargePackSizeGroup_Id
                .Add("@Rate", SqlDbType.Float, 15).Value = _Rate
                .Add("@Description", SqlDbType.NVarChar, 500).Value = _Description
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = _Str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = _Str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = _Str3
                .Add("@Str4", SqlDbType.NVarChar, 2000).Value = _Str4
                .Add("@Str5", SqlDbType.NVarChar, 2000).Value = _Str5
                .Add("@Flo1", SqlDbType.Float, 15).Value = _Flo1
                .Add("@Flo2", SqlDbType.Float, 15).Value = _Flo2
                .Add("@Flo3", SqlDbType.Float, 15).Value = _Flo3
                .Add("@Flo4", SqlDbType.Float, 15).Value = _Flo4
                .Add("@Flo5", SqlDbType.Float, 15).Value = _Flo5
                .Add("@add_by", SqlDbType.VarChar, 50).Value = _add_by
                .Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _add_date
                .Add("@add_branch", SqlDbType.Int, 10).Value = _add_branch
                .Add("@update_by", SqlDbType.VarChar, 50).Value = _update_by
                .Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = _update_branch
                .Add("@status_id", SqlDbType.Int, 10).Value = _status_id
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub
#End Region

#Region " DELETE DATA "
    Public Sub Delete()
        Dim strSQL As String = ""
        Try
            strSQL = " UPDATE svar_TransportManifestChargePackSize" & _
                      " SET Status_Id=-1" & _
                      " WHERE TransportManifestChargePackSize_Index='" & _TransportManifestChargePackSize_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub
#End Region

End Class