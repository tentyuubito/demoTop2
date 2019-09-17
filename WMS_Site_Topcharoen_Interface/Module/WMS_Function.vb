Public Module WMS_Function

    Public Function DataSetHasValue(ByVal DataSet As DataSet) As Boolean
        Return DataSet IsNot Nothing AndAlso DataSet.Tables.Count > 0
    End Function

    Public Function DataTableHasValue(ByVal DataTable As DataTable) As Boolean
        Return DataTable IsNot Nothing AndAlso DataTable.Rows.Count > 0
    End Function

End Module
