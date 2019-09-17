Imports WMS_STD_Formula

Public Class clsUserByDC : Inherits DBType_SQLServer

    Public Function GetDistributionCenterByUser() As String
        Dim strWhere As String = ""
        Dim strSQL As String = ""
        Try
            strSQL &= " select DistributionCenter_Index from se_User where user_index=@user_index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@user_index", SqlDbType.VarChar, 13).Value = W_Module.WV_User_Index
            End With
            Dim _dt As DataTable = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                If (Not IsDBNull(_dt.Rows(0).Item("DistributionCenter_Index"))) Then
                    If (Not _dt.Rows(0).Item("DistributionCenter_Index") = "0010000000000") Then
                        strWhere &= String.Format(" and (DistributionCenter_Index='{0}' or isnull(DistributionCenter_Index,'0010000000000')='0010000000000') ", _dt.Rows(0).Item("DistributionCenter_Index"))
                    End If
                End If
            End If
            Return strWhere
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetWarehouseByUser() As String
        Dim strWhere As String = ""
        Dim strSQL As String = ""
        Try
            strSQL &= " select ms_Warehouse.Warehouse_Index from se_User inner join ms_Warehouse on ms_Warehouse.DistributionCenter_Index=se_User.DistributionCenter_Index where user_index=@user_index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@user_index", SqlDbType.VarChar, 13).Value = W_Module.WV_User_Index
            End With
            Dim _dt As DataTable = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                If (Not IsDBNull(_dt.Rows(0).Item("Warehouse_Index"))) Then
                    Dim Warehouse_Index_IN As String = ""
                    For Each _dr As DataRow In _dt.Rows
                        Warehouse_Index_IN &= String.Format(",'{0}'", _dr.Item("Warehouse_Index"))
                    Next
                    If (Warehouse_Index_IN.Length > 0) Then
                        Warehouse_Index_IN = Warehouse_Index_IN.Substring(1)
                    End If
                    strWhere &= String.Format(" and Warehouse_Index in ({0}) ", Warehouse_Index_IN)
                End If
            End If
            Return strWhere
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDistributionCenter_IndexByUser(ByVal _Connection As SqlClient.SqlConnection, ByVal _myTrans As SqlClient.SqlTransaction) As String
        Dim DistributionCenter_Index As String = "0010000000000"
        Dim strSQL As String = ""
        Try
            strSQL &= " select DistributionCenter_Index from se_User where user_index=@user_index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@user_index", SqlDbType.VarChar, 13).Value = W_Module.WV_User_Index
            End With
            Dim _dt As DataTable = DBExeQuery(strSQL, _Connection, _myTrans)
            If (_dt.Rows.Count > 0) Then
                If (Not IsDBNull(_dt.Rows(0).Item("DistributionCenter_Index"))) Then
                    If (Not _dt.Rows(0).Item("DistributionCenter_Index") = "0010000000000") Then
                        DistributionCenter_Index = _dt.Rows(0).Item("DistributionCenter_Index")
                    End If
                End If
            End If
            Return DistributionCenter_Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
