Imports System.Data
Imports System.Data.SqlClient
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module


Public Class ms_SubRoute_Update
    : Inherits DBType_SQLServer

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
#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
    End Sub




#End Region

#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _SubRoute_Index As String = ""
    Private _Route_Index As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Str6 As String = ""
    Private _Str7 As String = ""
    Private _Str8 As String = ""
    Private _Str9 As String = ""
    Private _Str10 As String = ""
    Private _Flo1 As Double = 0
    Private _Flo2 As Double = 0
    Private _Flo3 As Double = 0
    Private _Flo4 As Double = 0
    Private _Flo5 As Double = 0
    Private _Comment As String = ""
    Private _Description As String = ""
    Private _SubRoute_No As String = ""
    Private _status_id As Integer = 0
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _DistributionCenter_Index As String

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
    Public Property SubRoute_Index() As String
        Get
            Return _SubRoute_Index
        End Get
        Set(ByVal Value As String)
            _SubRoute_Index = Value
        End Set
    End Property

    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal Value As String)
            _Route_Index = Value
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

    Public Property Str6() As String
        Get
            Return _Str6
        End Get
        Set(ByVal Value As String)
            _Str6 = Value
        End Set
    End Property

    Public Property Str7() As String
        Get
            Return _Str7
        End Get
        Set(ByVal Value As String)
            _Str7 = Value
        End Set
    End Property

    Public Property Str8() As String
        Get
            Return _Str8
        End Get
        Set(ByVal Value As String)
            _Str8 = Value
        End Set
    End Property

    Public Property Str9() As String
        Get
            Return _Str9
        End Get
        Set(ByVal Value As String)
            _Str9 = Value
        End Set
    End Property

    Public Property Str10() As String
        Get
            Return _Str10
        End Get
        Set(ByVal Value As String)
            _Str10 = Value
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

    Public Property Comment() As String
        Get
            Return _Comment
        End Get
        Set(ByVal Value As String)
            _Comment = Value
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

    Public Property SubRoute_No() As String
        Get
            Return _SubRoute_No
        End Get
        Set(ByVal Value As String)
            _SubRoute_No = Value
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

    Public Property DistributionCenter_Index() As String
        Get
            Return _DistributionCenter_Index
        End Get
        Set(ByVal Value As String)
            _DistributionCenter_Index = Value
        End Set
    End Property


#End Region


#Region " SELECT DATA "

    Public Sub GetSubRoute_BystrAnd(ByVal strAnd As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     *  FROM ms_SubRoute  "
            strSQL &= " WHERE    status_id <> -1 "
            SetSQLString = strSQL & strAnd
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetAllAsDataTable(ByVal Route_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     *  FROM ms_SubRoute  WHERE    status_id <> -1 "
            If Not (Route_Index = "-11") Then
                strSQL &= " and Route_Index='" & Route_Index & "'"
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
    Public Sub SearchSubRoute(ByVal SubRoute_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "SELECT     *    FROM       ms_SubRoute where SubRoute_Index ='" & SubRoute_Index & "'"

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
    Public Sub SearchSubRoute_Postcode(ByVal SubRoute_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "SELECT     *    FROM       ms_SubRoute_Postcode where SubRoute_Index ='" & SubRoute_Index & "'"

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

#Region " INSERT DATA "
    Public Sub Insert()
        Dim strSQL As String = " "
        Try
            Dim objDBIndex As New Sy_AutoNumber
            If _SubRoute_No = "" Then
                _SubRoute_No = objDBIndex.getSys_ID("SubRoute_Id")
                objDBIndex = Nothing
            End If

            strSQL = " INSERT INTO ms_SubRoute(SubRoute_Index,Route_Index,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,SubRoute_No,Description,Comment,status_id,add_by,add_date,add_branch,DistributionCenter_Index)" & _
            "       VALUES(@SubRoute_Index,@Route_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@SubRoute_No,@Description,@Comment,@status_id,@add_by,getdate(),@add_branch,@DistributionCenter_Index)"
            With SQLServerCommand.Parameters
                .Clear()
                .Add(New SqlParameter("@" & "SubRoute_Index", SqlDbType.VarChar, 13)).Value = _SubRoute_Index
                .Add(New SqlParameter("@" & "Route_Index", SqlDbType.VarChar, 13)).Value = _Route_Index
                .Add(New SqlParameter("@" & "Str1", SqlDbType.NVarChar, 100)).Value = _Str1
                .Add(New SqlParameter("@" & "Str2", SqlDbType.NVarChar, 100)).Value = _Str2
                .Add(New SqlParameter("@" & "Str3", SqlDbType.NVarChar, 100)).Value = _Str3
                .Add(New SqlParameter("@" & "Str4", SqlDbType.NVarChar, 100)).Value = _Str4
                .Add(New SqlParameter("@" & "Str5", SqlDbType.NVarChar, 100)).Value = _Str5
                .Add(New SqlParameter("@" & "Str6", SqlDbType.NVarChar, 100)).Value = _Str6
                .Add(New SqlParameter("@" & "Str7", SqlDbType.NVarChar, 100)).Value = _Str7
                .Add(New SqlParameter("@" & "Str8", SqlDbType.NVarChar, 100)).Value = _Str8
                .Add(New SqlParameter("@" & "Str9", SqlDbType.NVarChar, 2000)).Value = _Str9
                .Add(New SqlParameter("@" & "Str10", SqlDbType.NVarChar, 2000)).Value = _Str10
                .Add(New SqlParameter("@" & "Flo1", SqlDbType.Float)).Value = _Flo1
                .Add(New SqlParameter("@" & "Flo2", SqlDbType.Float)).Value = _Flo2
                .Add(New SqlParameter("@" & "Flo3", SqlDbType.Float)).Value = _Flo3
                .Add(New SqlParameter("@" & "Flo4", SqlDbType.Float)).Value = _Flo4
                .Add(New SqlParameter("@" & "Flo5", SqlDbType.Float)).Value = _Flo5
                .Add(New SqlParameter("@" & "SubRoute_No", SqlDbType.NVarChar, 255)).Value = _SubRoute_No
                .Add(New SqlParameter("@" & "Description", SqlDbType.NVarChar, 255)).Value = _Description
                .Add(New SqlParameter("@" & "Comment", SqlDbType.NVarChar, 255)).Value = _Comment
                .Add(New SqlParameter("@" & "status_id", SqlDbType.Int)).Value = 0
                .Add(New SqlParameter("@" & "add_by", SqlDbType.VarChar, 50)).Value = WV_UserName
                '.Add(New SqlParameter("@" & "add_date", SqlDbType.smalldatetime, 16)).Value = _add_date
                .Add(New SqlParameter("@" & "add_branch", SqlDbType.Int)).Value = WV_Branch_ID
                '.Add(New SqlParameter("@" & "update_by", SqlDbType.varchar, 50)).Value = _update_by
                '.Add(New SqlParameter("@" & "update_date", SqlDbType.smalldatetime, 16)).Value = _update_date
                .Add(New SqlParameter("@" & "DistributionCenter_Index", SqlDbType.VarChar, 13)).Value = Me.DistributionCenter_Index
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


    Public Sub InsertSubRoute_Postcode(ByVal pSubRoute_Index As String, ByVal pdtPostcode As DataTable)
        Dim strSQL As String = " "
        Try
            strSQL = " DELETE ms_SubRoute_Postcode where SubRoute_Index='" & pSubRoute_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            For Each drPostcode As DataRow In pdtPostcode.Rows
                If drPostcode("Postcode").ToString = "" Then Continue For
                strSQL = " INSERT INTO ms_SubRoute_Postcode(SubRoute_Index,Postcode,status_id)" & _
                          "       VALUES(@SubRoute_Index,@Postcode,1)"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add(New SqlParameter("@" & "SubRoute_Index", SqlDbType.VarChar, 13)).Value = pSubRoute_Index
                    .Add(New SqlParameter("@" & "Postcode", SqlDbType.NVarChar, 100)).Value = drPostcode("Postcode").ToString
                    '.Add(New SqlParameter("@" & "Str1", SqlDbType.NVarChar, 100)).Value = _Str1
                    '.Add(New SqlParameter("@" & "Str2", SqlDbType.NVarChar, 100)).Value = _Str2
                    '.Add(New SqlParameter("@" & "Str3", SqlDbType.NVarChar, 100)).Value = _Str3
                    '.Add(New SqlParameter("@" & "Str4", SqlDbType.NVarChar, 100)).Value = _Str4
                    '.Add(New SqlParameter("@" & "Str5", SqlDbType.NVarChar, 100)).Value = _Str5
                    '  .Add(New SqlParameter("@" & "status_id", SqlDbType.Int, 10)).Value = 1
                    '.Add(New SqlParameter("@" & "add_by", SqlDbType.VarChar, 50)).Value = WV_UserName
                    '.Add(New SqlParameter("@" & "add_branch", SqlDbType.Int, 10)).Value = WV_Branch_ID
                    '.Add(New SqlParameter("@" & "update_by", SqlDbType.VarChar, 50)).Value = _update_by

                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                connectDB()
                EXEC_Command()

            Next


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
            strSQL = " UPDATE ms_SubRoute" & _
            " SET SubRoute_Index=@SubRoute_Index," & _
            "     Route_Index=@Route_Index," & _
            "     SubRoute_No=@SubRoute_No," & _
            "     Str1=@Str1," & _
            "     Str2=@Str2," & _
            "     Str3=@Str3," & _
            "     Str4=@Str4," & _
            "     Str5=@Str5," & _
            "     Str6=@Str6," & _
            "     Str7=@Str7," & _
            "     Str8=@Str8," & _
            "     Str9=@Str9," & _
            "     Str10=@Str10," & _
            "     Flo1=@Flo1," & _
            "     Flo2=@Flo2," & _
            "     Flo3=@Flo3," & _
            "     Flo4=@Flo4," & _
            "     Flo5=@Flo5," & _
"     Description=@Description," & _
            "     Comment=@Comment," & _
            "     status_id=@status_id," & _
   "     update_branch=@update_branch," & _
            "     update_by=@update_by," & _
            "     update_date=getdate(), " & _
            "     DistributionCenter_Index=@DistributionCenter_Index " & _
            "           WHERE          SubRoute_Index = @SubRoute_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add(New SqlParameter("@" & "SubRoute_Index", SqlDbType.varchar, 13)).Value = _SubRoute_Index
                .Add(New SqlParameter("@" & "Route_Index", SqlDbType.VarChar, 13)).Value = _Route_Index
                .Add(New SqlParameter("@" & "SubRoute_No", SqlDbType.VarChar, 50)).Value = _SubRoute_No
                .Add(New SqlParameter("@" & "Str1", SqlDbType.nvarchar, 100)).Value = _Str1
                .Add(New SqlParameter("@" & "Str2", SqlDbType.nvarchar, 100)).Value = _Str2
                .Add(New SqlParameter("@" & "Str3", SqlDbType.nvarchar, 100)).Value = _Str3
                .Add(New SqlParameter("@" & "Str4", SqlDbType.nvarchar, 100)).Value = _Str4
                .Add(New SqlParameter("@" & "Str5", SqlDbType.nvarchar, 100)).Value = _Str5
                .Add(New SqlParameter("@" & "Str6", SqlDbType.nvarchar, 100)).Value = _Str6
                .Add(New SqlParameter("@" & "Str7", SqlDbType.nvarchar, 100)).Value = _Str7
                .Add(New SqlParameter("@" & "Str8", SqlDbType.nvarchar, 100)).Value = _Str8
                .Add(New SqlParameter("@" & "Str9", SqlDbType.nvarchar, 2000)).Value = _Str9
                .Add(New SqlParameter("@" & "Str10", SqlDbType.nvarchar, 2000)).Value = _Str10
                .Add(New SqlParameter("@" & "Flo1", SqlDbType.Float)).Value = _Flo1
                .Add(New SqlParameter("@" & "Flo2", SqlDbType.Float)).Value = _Flo2
                .Add(New SqlParameter("@" & "Flo3", SqlDbType.Float)).Value = _Flo3
                .Add(New SqlParameter("@" & "Flo4", SqlDbType.Float)).Value = _Flo4
                .Add(New SqlParameter("@" & "Flo5", SqlDbType.Float)).Value = _Flo5
                .Add(New SqlParameter("@" & "Description", SqlDbType.NVarChar, 255)).Value = _Description
                .Add(New SqlParameter("@" & "Comment", SqlDbType.nvarchar, 255)).Value = _Comment
                .Add(New SqlParameter("@" & "status_id", SqlDbType.Int)).Value = 0
                '.Add(New SqlParameter("@" & "add_by", SqlDbType.varchar, 50)).Value = _add_by
                '.Add(New SqlParameter("@" & "add_date", SqlDbType.smalldatetime, 16)).Value = _add_date
                .Add(New SqlParameter("@" & "update_branch", SqlDbType.Int)).Value = WV_Branch_ID
                .Add(New SqlParameter("@" & "update_by", SqlDbType.VarChar, 50)).Value = WV_UserName
                .Add(New SqlParameter("@" & "DistributionCenter_Index", SqlDbType.VarChar, 13)).Value = Me.DistributionCenter_Index

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
            strSQL = " UPDATE ms_SubRoute" & _
                      " SET Status_Id=-1" & _
                      " WHERE SubRoute_Index='" & _SubRoute_Index & "'"
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


    Public Function isExistID(ByVal _Route_Index As String, ByVal _SubRoute_No As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_SubRoute where SubRoute_No = @SubRoute_No and status_id <> -1 "
            strSQL &= " and Route_Index = @Route_Index  "
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SubRoute_No", SqlDbType.VarChar, 100).Value = _SubRoute_No
            SQLServerCommand.Parameters.Add("@Route_Index", SqlDbType.VarChar, 13).Value = _Route_Index
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function CheckPostcode(ByVal pPostcode As String) As String
        Dim strSQL As String
        Try
            strSQL = "      SELECT      ms_SubRoute.SubRoute_Index,ms_SubRoute.Description,ms_SubRoute_Postcode.Postcode,ms_SubRoute.status_id"
            strSQL &= "     FROM        ms_SubRoute INNER JOIN"
            strSQL &= "                 ms_SubRoute_Postcode ON ms_SubRoute.SubRoute_Index = ms_SubRoute_Postcode.SubRoute_Index"
            strSQL &= "     WHERE       (ms_SubRoute.status_id <> -1)"
            strSQL &= "                 AND ms_SubRoute_Postcode.Postcode='" & pPostcode & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Select Case _dataTable.Rows.Count
                Case 0
                    Return ""
                Case Else
                    Return _dataTable.Rows(0).Item("Description").ToString
            End Select

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
End Class