Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient

Public class svar_HandlingChargeByDocType: Inherits DBType_SQLServer
#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _HandlingChargeByDocType_Index As String = ""
    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Private _Currency_Index As String = ""
    Private _ServiceFee_Unit_Index As String = ""
    Private _HandlingType_Index As String = ""
    Private _ServiceGroup_Id As Integer = 0
    Private _Rate As Double = 0
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
    Public Property HandlingChargeByDocType_Index() As String
        Get
            Return _HandlingChargeByDocType_Index
        End Get
        Set(ByVal Value As String)
            _HandlingChargeByDocType_Index = Value
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

    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal Value As String)
            _DocumentType_Index = Value
        End Set
    End Property

    Public Property Currency_Index() As String
        Get
            Return _Currency_Index
        End Get
        Set(ByVal Value As String)
            _Currency_Index = Value
        End Set
    End Property

    Public Property ServiceFee_Unit_Index() As String
        Get
            Return _ServiceFee_Unit_Index
        End Get
        Set(ByVal Value As String)
            _ServiceFee_Unit_Index = Value
        End Set
    End Property
    Public Property HandlingType_Index() As String
        Get
            Return _HandlingType_Index
        End Get
        Set(ByVal Value As String)
            _HandlingType_Index = Value
        End Set
    End Property
    Public Property ServiceGroup_Id() As Integer
        Get
            Return _ServiceGroup_Id
        End Get
        Set(ByVal Value As Integer)
            _ServiceGroup_Id = Value
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

    Public Property Minimum_Rate() As Double
        Get
            Return _Minimum_Rate
        End Get
        Set(ByVal Value As Double)
            _Minimum_Rate = Value
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


#End Region
    Public Sub New()

    End Sub

#Region " SELECT DATA "
    Public Sub SelectUnitbyGroup(ByVal ServiceGroup_Id As String)
        '  
        Dim strSQL As String = ""
        Dim sbSql As New System.Text.StringBuilder
        Try

            sbSql.Append(" select *  from svms_ServiceFee_Unit ")
            If Not ServiceGroup_Id = "" Then
                sbSql.Append("  where ServiceGroup_Id = " & ServiceGroup_Id & "")
            End If

            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub GetHandlingChargeByDocType(ByVal pstrCustomer_Index As String, ByVal pintProcess_Id As Integer)
        Dim strSQL As String = " "
        Try
            strSQL = " exec dbo.spHandlingChargeByDocType '" & pstrCustomer_Index & "'," & pintProcess_Id

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
             " FROM svar_HandlingChargeByDocType " & _
             " WHERE    status_id <> -1 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            '_dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Sub getProcessStatus(ByVal strCondition As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     * "
            strSQL &= " FROM ms_Process "
            strSQL &= " WHERE Status_Id <> -1 "
            SetSQLString = strSQL & strCondition & " Order by Process_id"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            '_dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Sub GetHandlingChargeByDocType_ServiceGroup(ByVal pstrHandlingChargeByDocType_Index As String, ByVal pintServiceGroup_Id As Integer)
        Dim strSQL As String = " "
        Try
            strSQL = "  SELECT SVHDOC.*,HT.Description as HandlingType,UNIT.Description as Unit"
            strSQL &= " FROM    svar_HandlingChargeByDocType SVHDOC INNER JOIN"
            strSQL &= "         tb_HandlingType HT ON HT.HandlingType_Index = SVHDOC.HandlingType_Index  INNER JOIN"
            strSQL &= "         svms_ServiceFee_Unit UNIT ON UNIT.ServiceFee_Unit_Index = SVHDOC.ServiceFee_Unit_Index "
            strSQL &= " WHERE   SVHDOC.status_id <> -1"
            strSQL &= "         AND SVHDOC.HandlingChargeByDocType_Index='" & pstrHandlingChargeByDocType_Index & "'"
            strSQL &= "         AND SVHDOC.ServiceGroup_Id=" & pintServiceGroup_Id

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            '_dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

#End Region

#Region " INSERT DATA "
    Public Sub Insert()
        Dim strSQL As String = " "
        Try
            strSQL = " INSERT INTO svar_HandlingChargeByDocType(HandlingChargeByDocType_Index,Customer_Index,DocumentType_Index,Currency_Index,ServiceFee_Unit_Index,HandlingType_Index,ServiceGroup_Id,Rate,Minimum_Rate,Description,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,update_by,update_date,update_branch,status_id)" & _
            "       VALUES(@HandlingChargeByDocType_Index,@Customer_Index,@DocumentType_Index,@Currency_Index,@ServiceFee_Unit_Index,@HandlingType_Index,@ServiceGroup_Id,@Rate,@Minimum_Rate,@Description,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_date,@add_branch,@update_by,@update_date,@update_branch,@status_id)"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = _HandlingChargeByDocType_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _DocumentType_Index
                .Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _Currency_Index
                .Add("@ServiceFee_Unit_Index", SqlDbType.VarChar, 13).Value = _ServiceFee_Unit_Index
                .Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _HandlingType_Index
                .Add("@ServiceGroup_Id", SqlDbType.Int, 10).Value = _ServiceGroup_Id
                .Add("@Rate", SqlDbType.Float, 15).Value = _Rate
                .Add("@Minimum_Rate", SqlDbType.Float, 15).Value = _Minimum_Rate
                .Add("@Description", SqlDbType.NVarChar, 200).Value = _Description
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

#Region " UPDATE DATA "
    Public Sub Update()
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE svar_HandlingChargeByDocType" & _
            " SET HandlingChargeByDocType_Index=@HandlingChargeByDocType_Index," & _
            "     Customer_Index=@Customer_Index," & _
            "     DocumentType_Index=@DocumentType_Index," & _
            "     Currency_Index=@Currency_Index," & _
            "     ServiceFee_Unit_Index=@ServiceFee_Unit_Index," & _
            "     HandlingType_Index=@HandlingType_Index," & _
            "     ServiceGroup_Id=@ServiceGroup_Id," & _
            "     Rate=@Rate," & _
            "     Minimum_Rate=@Minimum_Rate," & _
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
            "           WHERE          HandlingChargeByDocType_Index = @HandlingChargeByDocType_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = _HandlingChargeByDocType_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _DocumentType_Index
                .Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _Currency_Index
                .Add("@ServiceFee_Unit_Index", SqlDbType.VarChar, 13).Value = _ServiceFee_Unit_Index
                .Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _HandlingType_Index
                .Add("@ServiceGroup_Id", SqlDbType.Int, 10).Value = _ServiceGroup_Id
                .Add("@Rate", SqlDbType.Float, 15).Value = _Rate
                .Add("@Minimum_Rate", SqlDbType.Float, 15).Value = _Minimum_Rate
                .Add("@Description", SqlDbType.NVarChar, 200).Value = _Description
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
            strSQL = " UPDATE svar_HandlingChargeByDocType" & _
                      " SET Status_Id=-1" & _
                      " WHERE HandlingChargeByDocType_Index='" & _HandlingChargeByDocType_Index & "'"
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

Public Class svar_HandlingChargeByDocTypeTransaction : Inherits DBType_SQLServer

    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        SEARCH
        CANCEL
    End Enum

    Dim _objHandlingChargeByDocType As New svar_HandlingChargeByDocType
    Dim _objHandlingChargeByDocTypePerDropItemCollection As New List(Of svar_HandlingChargeByDocType)

    Private _HandlingChargeByDocType_Index As String = ""
    Public Property HandlingChargeByDocType_Index() As String
        Get
            Return _HandlingChargeByDocType_Index
        End Get
        Set(ByVal Value As String)
            _HandlingChargeByDocType_Index = Value
        End Set
    End Property

    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        objStatus = Operation_Type
    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHandlingChargeByDocTypePerDropItemCollection As List(Of svar_HandlingChargeByDocType))
        MyBase.New()
        objStatus = Operation_Type
        _objHandlingChargeByDocTypePerDropItemCollection = objHandlingChargeByDocTypePerDropItemCollection
    End Sub

    Public Function SaveData() As String
        Try
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Return Me.InsertData()
                Case enuOperation_Type.UPDATE
                    Return Me.InsertData()
                Case enuOperation_Type.CANCEL
                    Return Me.DeleteData()
            End Select

            Return ""

        Catch ex As Exception
            Throw ex
        End Try

        'Return True
    End Function

    Private Function InsertData() As String
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            ' --- STEP 2: Insert Line Item: SalesOrderItem 
            strSQL = " DELETE svar_HandlingChargeByDocType WHERE HandlingChargeByDocType_Index=@HandlingChargeByDocType_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = Me._HandlingChargeByDocType_Index
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            EXEC_Command()

            For Each _objItem As svar_HandlingChargeByDocType In _objHandlingChargeByDocTypePerDropItemCollection
                strSQL = " INSERT INTO svar_HandlingChargeByDocType(HandlingChargeByDocType_Index,Customer_Index,DocumentType_Index,Currency_Index,ServiceFee_Unit_Index,HandlingType_Index,ServiceGroup_Id,Rate,Minimum_Rate,Description,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,update_by,update_date,update_branch,status_id)" & _
                "       VALUES(@HandlingChargeByDocType_Index,@Customer_Index,@DocumentType_Index,@Currency_Index,@ServiceFee_Unit_Index,@HandlingType_Index,@ServiceGroup_Id,@Rate,@Minimum_Rate,@Description,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch,@update_by,getdate(),@update_branch,@status_id)"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingChargeByDocType_Index
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Index
                    .Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentType_Index
                    .Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _objItem.Currency_Index
                    .Add("@ServiceFee_Unit_Index", SqlDbType.VarChar, 13).Value = _objItem.ServiceFee_Unit_Index
                    .Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index
                    .Add("@ServiceGroup_Id", SqlDbType.Int, 10).Value = _objItem.ServiceGroup_Id
                    .Add("@Rate", SqlDbType.Float, 15).Value = _objItem.Rate
                    .Add("@Minimum_Rate", SqlDbType.Float, 15).Value = _objItem.Minimum_Rate
                    .Add("@Description", SqlDbType.NVarChar, 200).Value = _objItem.Description
                    .Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Add("@Str4", SqlDbType.NVarChar, 2000).Value = _objItem.Str4
                    .Add("@Str5", SqlDbType.NVarChar, 2000).Value = _objItem.Str5
                    .Add("@Flo1", SqlDbType.Float, 15).Value = _objItem.Flo1
                    .Add("@Flo2", SqlDbType.Float, 15).Value = _objItem.Flo2
                    .Add("@Flo3", SqlDbType.Float, 15).Value = _objItem.Flo3
                    .Add("@Flo4", SqlDbType.Float, 15).Value = _objItem.Flo4
                    .Add("@Flo5", SqlDbType.Float, 15).Value = _objItem.Flo5

                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    .Add("@status_id", SqlDbType.Int, 10).Value = 1

                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()

            Next

            ': Add Log to Sy_Audit_Log
            'Dim oAudit_Log As New Sy_Audit_Log
            'oAudit_Log.Document_Index = Me._newSaleOrder_Index
            'oAudit_Log.Document_No = _objSaleOrder.SalesOrder_No
            'oAudit_Log.Ref_No1 = _objSaleOrder.Str1
            'oAudit_Log.Ref_No1 = _objSaleOrder.Str2
            'oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_SO)

            ' --- Commit transaction
            myTrans.Commit()

            Return "PASS"

        Catch ex As Exception
            myTrans.Rollback()
            Return ex.Message.ToString
        Finally
            disconnectDB()
        End Try

    End Function
    Private Function DeleteData() As String
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = " DELETE svar_HandlingChargeByDocType WHERE HandlingChargeByDocType_Index=@HandlingChargeByDocType_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = Me._HandlingChargeByDocType_Index
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return "PASS"

        Catch ex As Exception
            myTrans.Rollback()
            Return ex.Message.ToString
        Finally
            disconnectDB()
        End Try

    End Function


    Private Function IsExist_HandlingChargeByDocType(ByVal pstrHandlingChargeByDocType_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM svar_HandlingChargeByDocType WHERE HandlingChargeByDocType_Index = @HandlingChargeByDocType_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@HandlingChargeByDocType_Index", SqlDbType.VarChar, 13).Value = pstrHandlingChargeByDocType_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()

            Select Case GetScalarOutput
                Case Nothing
                    Return False
                Case "", "0"
                    Return False
                Case Else
                    Return True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class