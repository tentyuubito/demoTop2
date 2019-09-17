

Partial Public Class DataSetRptInsertTransport
    Partial Class View_rptInsertTransportDataTable

        Private Sub View_rptInsertTransportDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.Str3Column.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
