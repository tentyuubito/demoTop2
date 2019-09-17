Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module

Public Class Temp_Table : Inherits DBType_SQLServer

    Private _DtTemp As New DataTable
    Private _FileName As String

    Public Function getFromTempTable(ByVal strFileName As String, ByVal GUid As String) As DataTable
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "select * from  _Prepare_Imports "
            strSQL &= "where  [filename]='" & strFileName & "' And Status = 1 AND GUid = '" & GUid & "'"

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
    Sub RenameColumn(ByRef _DtTemp As DataTable, ByVal strProcessID As String)
        Try
            Dim objClsImportMaster As New clsImportMaster
            Dim _DT As New DataTable
            _DT = objClsImportMaster.getConfig_Import_Text(strProcessID)
            If Not _DT Is Nothing Then
                For iRow As Integer = 0 To _DT.Rows.Count - 1
                    _DtTemp.Columns(_DT.Rows(iRow)("Tab_Index")).ColumnName = _DT.Rows(iRow)("Field_Name").ToString
                Next iRow
            End If
            _DT = Nothing
            objClsImportMaster = Nothing
        Catch ex As Exception

            Throw ex
        End Try
    End Sub
    Public Sub UpdateStatus(ByVal strGUID As String, ByVal strFileName As String, ByVal strValue As String)
        Dim strSQL As String = " "
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = " Update _Prepare_Imports Set"
            strSQL &= " Status= @Status"
            strSQL &= " Where Status = 1"
            strSQL &= " AND   FileName = @FileName"
            strSQL &= " AND GUid = @GUid "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = Convert.ToInt16(strValue)
                .Parameters.Add("@FileName", SqlDbType.NVarChar, 4000).Value = strFileName
                .Parameters.Add("@GUid", SqlDbType.NVarChar, 4000).Value = strGUID
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            '*** Commit transaction 
            myTrans.Commit()

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    'Public Function InsertTemp() As Boolean
    '    Dim strSQL As String = " "
    '    connectDB()
    '    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '    SQLServerCommand.Transaction = myTrans
    '    Try
    '        Dim CountDtTemp = _DtTemp.Columns.Count
    '        For iRows As Integer = 0 To _DtTemp.Rows.Count - 1
    '            strSQL = " INSERT INTO _Prepare_Imports"
    '            strSQL &= "  ("

    '            'Add Field
    '            For iCol As Integer = 0 To CountDtTemp - 1
    '                If iCol = 0 Then
    '                    strSQL &= "[" & iCol & "]"
    '                Else
    '                    strSQL &= ",[" & iCol & "]"
    '                End If
    '            Next
    '            strSQL &= " ,Status"
    '            strSQL &= " ,FileName"
    '            strSQL &= " ,GUid"
    '            strSQL &= "  ) VALUES ( "

    '            'Add VALUES
    '            For iData As Integer = 0 To CountDtTemp - 1
    '                If iData = 0 Then
    '                    If _DtTemp.Rows(iRows).Item(iData).ToString = "" Then
    '                        strSQL &= "''"
    '                    Else
    '                        strSQL &= "'" & _DtTemp.Rows(iRows).Item(iData).ToString & "'"
    '                    End If
    '                Else
    '                    If _DtTemp.Rows(iRows).Item(iData).ToString = "" Then
    '                        strSQL &= ",''"
    '                    Else
    '                        strSQL &= " ,'" & _DtTemp.Rows(iRows).Item(iData).ToString & "'"
    '                    End If
    '                End If
    '            Next
    '            strSQL &= ",1"
    '            strSQL &= ",'" & _FileName & "'"
    '            strSQL &= ",'" & System.Guid.NewGuid.ToString() & "'"
    '            strSQL &= "  ) "

    '            SetSQLString = strSQL
    '            SetCommandType = DBType_SQLServer.enuCommandType.Text
    '            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '            EXEC_Command()
    '        Next

    '        myTrans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Function

    Public Function InsertTemp(ByVal DtDataToInsertTemp As DataTable, ByVal strFileName As String, ByVal strGUid As String) As Boolean
        Try
            Dim objDB As New SaveDataSO
            objDB.DtTemp = DtDataToInsertTemp
            objDB.FileName = strFileName
            objDB.GUid = strGUid

            If objDB.InsertTemp() Then
                Return True
                'objDB.Status = 2 'จองเพื่อ  Insert Temp To PO
                'If objDB.UpdateStatus() Then
                'If Insert_PO(DtDataToInsertTemp) Then
                '    'Insert Complete
                'Else
                '    'Insert Error
                'End If
                'Else
                '    'Update Error
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
