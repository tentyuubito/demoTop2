Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master

Public Class clsPrintPalletSlip_v2 : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Public Overloads ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public Function getOrderItemCount(ByVal Order_Index As String) As Integer
        Dim strSQL As String = ""
        Try
            strSQL &= " select max(Seq) from tb_OrderItem where Status<>-1 and Order_Index=@Order_Index "
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
            Dim dt As DataTable = DBExeQuery(strSQL)
            If (dt.Rows.Count > 0) Then
                If (IsNumeric(dt.Rows(0).Item(0))) Then
                    Return CInt(dt.Rows(0).Item(0))
                End If
            End If
            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckSKUSerial(ByVal Sku_Index As String) As Boolean
        Dim strSQL As String = " select count(Sku_Index) from ms_SKU where IsSerial=1 and Sku_Index=@Sku_Index "
        SQLServerCommand.Parameters.Clear()
        SQLServerCommand.Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = Sku_Index
        If CInt(DBExeQuery_Scalar(strSQL)) > 0 Then Return True
        Return False
    End Function

    Public Sub getView_Tag_Header_Repeat(ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = " SELECT '' AS ROW, CONVERT(BIT,0) AS ChkSelect, * " & _
                     " FROM VIEW_TAG_Header_Reprint T " & _
                     " WHERE TAG_Status <> -1 "

            SetSQLString = strSQL & pstrWhere & " ORDER BY TAG_No"


            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

End Class
