Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Public Class Import_SO : Inherits DBType_SQLServer
    'Public Function Inserted(ByVal _DtTemp As DataTable, ByVal _FileName As String) As Boolean
    '    Try
    '        Dim StatusReturn As Boolean = False
    '        Dim objTempTable As New Temp_Table

    '        Dim objclsImport As New clsImportMaster
    '        Dim _dt As New DataTable
    '        If objTempTable.InsertTemp(_DtTemp, _FileName) = True Then
    '            _dt = objTempTable.getFromTempTable(_FileName)
    '            objTempTable.RenameColumn(_dt, 101)
    '            Dim objClsimportSo As New clsImportSO
    '            objClsimportSo.Process_ID = 10
    '            objClsimportSo.File_Prefix = "SO"
    '            GetConfig_Default_Import("SO", 10)
    '            objClsimportSo.DtTemp = _dt
    '            If objClsimportSo.ReadData() = True Then

    '                For iRow As Integer = 0 To objClsimportSo.DtHeader.Rows.Count - 1
    '                    objClsimportSo.SalesOrder_No = objClsimportSo.DtHeader.Rows(iRow)(1).ToString.Trim
    '                    objClsimportSo.Insert_SO()
    '                Next
    '                For iRow As Integer = 0 To _dt.Rows.Count - 1
    '                    Dim iDatarow As DataRow = _dt.Rows(iRow)
    '                    objTempTable.UpdateStatus(iDatarow("GuID").ToString, _FileName, "2")
    '                    iDatarow = Nothing
    '                Next
    '                StatusReturn = True
    '            End If
    '            objClsimportSo = Nothing
    '        End If
    '        _dt = Nothing
    '        objTempTable = Nothing
    '        objclsImport = Nothing
    '        Return StatusReturn
    '    Catch ex As Exception
    '        Return False
    '        Throw ex
    '    End Try
    'End Function
End Class
