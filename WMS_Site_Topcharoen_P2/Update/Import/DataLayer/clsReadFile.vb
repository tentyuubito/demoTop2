Imports System.IO
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Configuration.ConfigurationSettings
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class clsReadFile : Inherits DBType_SQLServer
#Region "Private Property"
    Private _dataTable As DataTable = New DataTable
#End Region

#Region "ReadExcel"

    Public Function LoadWorkSheet(ByVal pstrFileName As String) As DataTable
        Try
            Dim odtTemp As New DataTable
            Dim SheetList As New ArrayList
            Dim strConnString As String

            strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 12.0 Xml;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            oConnSource.Open()
            Dim Objdt As DataTable = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim Objds As New DataSet
            oConnSource.Close()

            Return Objdt

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try
            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 12.0 Xml;HDR=Yes;IMEX=1"";"
            If pstrFileName = "" Then
                Return odtTemp
            End If
            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter
            With odaSource

                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'").Trim  ' "Sheet1''s$"
                    .SelectCommand = New OleDbCommand("SELECT  * FROM [" & pstrWorkSheet & "] ", oConnSource)
                Else
                    .SelectCommand = New OleDbCommand("SELECT  * FROM [" & pstrWorkSheet & "$] ", oConnSource)
                End If
                .Fill(odtTemp)

            End With
            Return odtTemp
        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function
    Public Function LoadDataFromFile_Config(ByVal pstrFileName As String, ByVal pstrWorkSheet As String, ByVal FilePrefix As String) As DataTable
        Try
            Dim strSQL_Import_Text As String = ""
            Dim strSQL As String = "SELECT * FROM Config_Import_Excel WHERE Process_Id = '10' AND File_Prefix = '" & FilePrefix & "'"
            Dim field As String = ""
            Dim where As String = ""
            Dim Group As String = ""
            Dim strSQLSelect As String = "SELECT  "
            Dim dt As DataTable = DBExeQuery(strSQL)
            If dt.Rows.Count = 0 Then
                field = "*"
            Else
                If dt.Rows(0).Item("Is_Use").ToString <> Nothing Or dt.Rows(0).Item("Is_Use").ToString <> String.Empty Then
                    If dt.Rows(0).Item("Is_Use") = 1 Then
                        field = dt.Rows(0).Item("Excel_Condition").ToString
                        If field = Nothing Or field = String.Empty Then
                            field = "*"
                        End If
                        where = dt.Rows(0).Item("Where_Condition").ToString
                        Group = dt.Rows(0).Item("Group_Condition").ToString
                    Else
                        field = "*"
                    End If
                Else
                    field = "*"
                End If
            End If
            Dim strConnString As String
            Dim odtTemp As New DataTable

            strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 12.0 Xml;HDR=Yes;IMEX=1"";"

            'strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 12.0 Xml;HDR=Yes;IMEX=1"";"
            If pstrFileName = "" Then
                Return odtTemp
            End If
            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter
            With odaSource
                'If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                '    pstrWorkSheet = pstrWorkSheet.Replace("''", "'").Trim  ' "Sheet1''s$"
                '    .SelectCommand = New OleDbCommand("SELECT  " & field & " FROM [" & pstrWorkSheet & "] ", oConnSource)
                'Else
                '    .SelectCommand = New OleDbCommand("SELECT  " & field & " FROM [" & pstrWorkSheet & "$] ", oConnSource)
                'End If
                strSQLSelect &= field & " FROM ["
                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'").Trim  ' "Sheet1''s$"
                    strSQLSelect &= pstrWorkSheet & "]"
                Else
                    strSQLSelect &= pstrWorkSheet & "$]"
                End If
                If where <> Nothing Or where <> String.Empty Then
                    strSQLSelect &= " WHERE " & where
                End If
                If Group <> Nothing Or Group <> String.Empty Then
                    strSQLSelect &= " Group By " & Group
                End If
                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'").Trim  ' "Sheet1''s$"
                    .SelectCommand = New OleDbCommand(strSQLSelect, oConnSource)
                Else
                    .SelectCommand = New OleDbCommand(strSQLSelect, oConnSource)
                End If
                .Fill(odtTemp)

            End With
            Return odtTemp
        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

#End Region

#Region "GUID"
    Public Function GenGUID() As String
        Try
            Return System.Guid.NewGuid.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
