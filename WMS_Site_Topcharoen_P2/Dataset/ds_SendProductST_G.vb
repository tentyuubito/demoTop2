

Partial Public Class ds_SendProductST_G
    Partial Class View_Rpt_Sub_ST_GDataTable

        Private Sub View_Rpt_Sub_ST_GDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.Str2Column.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
