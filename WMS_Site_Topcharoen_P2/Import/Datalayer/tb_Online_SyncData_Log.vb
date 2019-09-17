Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports System.Data

Public Class tb_Online_SyncData_Log : Inherits DBType_SQLServer
#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _Online_SyncData_Log_Index As String = ""
    Private _Online_SyncData_Type As String = ""
    Private _Table_Source As String = ""
    Private _Table_Destination As String = ""
    Private _Qty_Row As String = ""
    Private _Total_Record As Integer
    Private _Description As String = ""
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _Update_by As String = ""
    Private _Update_date As Date
    Private _Update_branch As Integer
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
    Public Property Online_SyncData_Log_Index() As String
        Get
            Return _Online_SyncData_Log_Index
        End Get
        Set(ByVal Value As String)
            _Online_SyncData_Log_Index = Value
        End Set
    End Property

    Public Property Online_SyncData_Type() As String
        Get
            Return _Online_SyncData_Type
        End Get
        Set(ByVal Value As String)
            _Online_SyncData_Type = Value
        End Set
    End Property

    Public Property Table_Source() As String
        Get
            Return _Table_Source
        End Get
        Set(ByVal Value As String)
            _Table_Source = Value
        End Set
    End Property

    Public Property Table_Destination() As String
        Get
            Return _Table_Destination
        End Get
        Set(ByVal Value As String)
            _Table_Destination = Value
        End Set
    End Property

    Public Property Qty_Row() As String
        Get
            Return _Qty_Row
        End Get
        Set(ByVal Value As String)
            _Qty_Row = Value
        End Set
    End Property

    Public Property Total_Record() As Integer
        Get
            Return _Total_Record
        End Get
        Set(ByVal Value As Integer)
            _Total_Record = Value
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

    Public Property Update_by() As String
        Get
            Return _Update_by
        End Get
        Set(ByVal Value As String)
            _Update_by = Value
        End Set
    End Property

    Public Property Update_date() As Date
        Get
            Return _Update_date
        End Get
        Set(ByVal Value As Date)
            _Update_date = Value
        End Set
    End Property

    Public Property Update_branch() As Integer
        Get
            Return _Update_branch
        End Get
        Set(ByVal Value As Integer)
            _Update_branch = Value
        End Set
    End Property


#End Region
#End Region

    Public Function GetOnline_SyncData(ByVal pstrTableName As String) As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * FROM tb_Online_SyncData_Log "
            strSQL &= " Where Table_Source = '" & pstrTableName & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Sub Insert_Online_SyncData_Log()

        Try

            Dim strSQL As String = ""
            Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber
            _Online_SyncData_Log_Index = objDBIndex.getSys_Value("Online_SyncData_Log_Index")
            objDBIndex = Nothing

            strSQL = " INSERT INTO tb_Online_SyncData_Log(Online_SyncData_Log_Index,Online_SyncData_Type,Table_Source,Table_Destination,Total_Record,Description,add_by,add_branch)"
            strSQL &= " Values "
            strSQL &= "(@Online_SyncData_Log_Index,@Online_SyncData_Type,@Table_Source,@Table_Destination,@Total_Record,@Description,@add_by,@add_branch)"



            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Online_SyncData_Log_Index", SqlDbType.NVarChar, 13).Value = _Online_SyncData_Log_Index
                .Parameters.Add("@Online_SyncData_Type", SqlDbType.NVarChar, 50).Value = _Online_SyncData_Type
                .Parameters.Add("@Table_Source", SqlDbType.NVarChar, 200).Value = _Table_Source
                .Parameters.Add("@Table_Destination", SqlDbType.NVarChar, 200).Value = _Table_Destination
                .Parameters.Add("@Total_Record", SqlDbType.Int, 10).Value = _Total_Record
                .Parameters.Add("@Description", SqlDbType.NVarChar, 2000).Value = _Description
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                ' .Parameters.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = Now
                .Parameters.Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
            End With

           
            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub



    Public Sub Update_Online_SyncData_Log()

        Try
            Dim strSQL As String
            strSQL = " UPDATE tb_Online_SyncData_Log" & _
                    " SET Online_SyncData_Log_Index=@Online_SyncData_Log_Index," & _
                    "     Online_SyncData_Type=@Online_SyncData_Type," & _
                    "     Table_Source=@Table_Source," & _
                    "     Table_Destination=@Table_Destination," & _
                    "     Total_Record=@Total_Record," & _
                    "     Description=@Description," & _
                    "     Update_by=@Update_by," & _
                    "     Update_date=@Update_date," & _
                    "     Update_branch=@Update_branch " & _
                    "           WHERE          Online_SyncData_Log_Index = @Online_SyncData_Log_Index"
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Online_SyncData_Log_Index", SqlDbType.NVarChar, 13).Value = _Online_SyncData_Log_Index
                .Parameters.Add("@Online_SyncData_Type", SqlDbType.NVarChar, 50).Value = _Online_SyncData_Type
                .Parameters.Add("@Table_Source", SqlDbType.NVarChar, 200).Value = _Table_Source
                .Parameters.Add("@Table_Destination", SqlDbType.NVarChar, 200).Value = _Table_Destination
                .Parameters.Add("@Total_Record", SqlDbType.Int, 10).Value = _Total_Record
                .Parameters.Add("@Description", SqlDbType.NVarChar, 2000).Value = _Description
                .Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@Update_date", SqlDbType.SmallDateTime, 16).Value = Now
                .Parameters.Add("@Update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
            End With
            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub


    Public Sub Update_add_branch(ByVal Table_Name As String)

        Try
            If Me.DoesFieldExist(Table_Name, "add_branch") Then
                Select Case Table_Name.ToUpper
                    Case "VIEW_ONLINE_3PL_STOCKONHAND", "VIEW_ONLINE_ORDERSUMMARY", "VIEW_ONLINE_RPT_RECGROUPBYPRODUCT", "VIEW_ONLINE_3PL_RECEIVEDSUMMARY", "VIEW_ONLINE_STOCKONHAND", "VIEW_STOCKAGE_ANALYSISBYORDERDATE", "VIEW_STOCKAGESUMMARY_BYTAG_ONLINE", "VIEW_STOCKAGE_ANALYSISBYMFGDATE"
                        Dim strSQL As String
                        strSQL = " UPDATE tb_Order SET add_branch= @add_branch "
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                        End With
                        SetSQLString = strSQL
                        connectDB()
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Case "VIEW_ONLINE_RPT_WITHDRAWGROUPBYPRODUCT", "VIEW_ONLINE_WITHDRAWSUMMARY", "VIEW_ONLINE_3PL_PICKUPSUMMARY"
                        Dim strSQL As String
                        strSQL = " UPDATE tb_withdraw SET add_branch= @add_branch "
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                        End With
                        SetSQLString = strSQL
                        connectDB()
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Case Else
                        Dim strSQL As String
                        strSQL = " UPDATE " & Table_Name & " SET add_branch= @add_branch "
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                        End With
                        SetSQLString = strSQL
                        connectDB()
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                End Select
            End If
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Function DoesFieldExist(ByVal tblName As String, _
                               ByVal fldName As String) As Boolean
        ' For Access Connection String,
        ' use "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" &
        ' accessFilePathAndName

        ' Open connection to the database
        Dim dbConn As New SqlClient.SqlConnection(WV_ConnectionString)
        dbConn.Open()
        Dim dbTbl As New DataTable

        ' Get the table definition loaded in a table adapter
        Dim strSql As String = "Select TOP 1 * from " & tblName
        Dim dbAdapater As New SqlClient.SqlDataAdapter(strSql, dbConn)
        dbAdapater.Fill(dbTbl)

        ' Get the index of the field name
        Dim i As Integer = dbTbl.Columns.IndexOf(fldName)

        If i = -1 Then
            'Field is missing
            DoesFieldExist = False
        Else
            'Field is there
            DoesFieldExist = True
        End If

        dbTbl.Dispose()
        dbConn.Close()
        dbConn.Dispose()
    End Function

End Class
