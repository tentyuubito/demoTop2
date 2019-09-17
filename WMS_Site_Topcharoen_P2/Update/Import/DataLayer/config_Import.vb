Imports System.IO
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Configuration.ConfigurationSettings
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class config_Import : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public Function fnGetConfigImport(ByVal pFile_Prefix As String, ByVal pProcess_id As Integer, ByVal pIsUse As Integer) As DataTable

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM config_import_text "
            strSQL &= " WHERE File_Prefix = '" & pFile_Prefix & "'"
            strSQL &= " And Process_id =" & pProcess_id
            strSQL &= " And IsUse =" & pIsUse
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function fnConvertAscii(ByVal Delimited As String) As String
        Try
            Dim i As Integer = Int(Delimited) 'AscW(strSubstring)
            Dim tempsplit As String = Chr(i)

            Return tempsplit
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fnGetConfigDefaultImport(ByVal pFile_Prefix As String, ByVal pProcess_id As Integer, ByVal pDefault_Name As String) As String
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT Default_Value "
            strSQL &= " FROM config_Default_text "
            strSQL &= " WHERE File_Prefix = '" & pFile_Prefix & "'"
            strSQL &= " And Process_id =" & pProcess_id
            strSQL &= " And Default_Name ='" & pDefault_Name & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable.Rows(0).Item("Default_Value").ToString
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function chkIsUse(ByVal _odt As DataTable, ByVal pField_Type As String, ByVal pField_Name As String, ByVal pProcess_id As Integer, ByVal pFile_Prefix As String) As Integer
        Dim strwhere As String = ""
        strwhere = "Field_Type = '" & pField_Type & "'" & _
                   " and Field_Name = '" & pField_Name & "'"

        Dim drArr() As DataRow = _odt.Select(strwhere)
        If drArr.Length > 0 Then
            If drArr(0).Item("IsUse") = 1 Then
                Return 1
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Function fnGetDataImport(ByVal pFile_Name As String, ByVal pStatus As Integer) As DataTable


        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM _Prepare_Imports "
            strSQL &= " WHERE [FileName] = '" & pFile_Name & "'"
            strSQL &= " And [Status] =" & pStatus

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function fnGetConfig_Default_Import_Single(ByVal pFile_Prefix As String, ByVal pProcess_id As Integer, ByVal Default_Name As String) As String

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT Default_Value "
            strSQL &= " FROM Config_Default_Import "
            strSQL &= " WHERE File_Prefix = '" & pFile_Prefix & "'"
            strSQL &= " And Process =" & pProcess_id
            strSQL &= " And Default_Name = '" & Default_Name & "' "

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.Scalar
            connectDB()
            EXEC_Command()

            Return GetScalarOutput()
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#Region "Config_Default_Import"
    Public Function fnGetConfig_Default_Import_All(ByVal pFile_Prefix As String, ByVal pProcess_id As Integer) As DataTable

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM Config_Default_Import "
            strSQL &= " WHERE File_Prefix = '" & pFile_Prefix & "'"
            strSQL &= " And Process =" & pProcess_id

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()

            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetDefault_Value(ByVal _DtConfig_Default_Import As DataTable, ByVal Default_Name As String) As String
        Try
            Dim iRows() As DataRow
            iRows = _DtConfig_Default_Import.Select("Default_Name= '" & Default_Name & "' ")
            Return iRows(0)("Default_Value")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetDateServer"
    Public Function GetDateServer() As String
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  getdate()"

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.Scalar
            connectDB()
            EXEC_Command()

            Return GetScalarOutput()
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

End Class
