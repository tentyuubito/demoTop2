Imports WMS_STD_Formula
Public Class cls_Validate : Inherits DBType_SQLServer
    Dim NotFound As Integer = 0
    Dim Max_SQLLength As Integer = 3000
    Dim _grd As New DataGridView
    Public Function Validate(ByVal dt As DataTable, ByVal grd As DataGridView) As Integer
        Try
            Me._grd = grd
            For i As Integer = 0 To dt.Rows.Count - 1
                'If SKU_Package(dt.Rows(i).Item("Sku_Id"), dt.Rows(i).Item("Package_Id")) = False Then
                '    HighListGridView(i, dt.Columns.IndexOf("Sku_Id"))
                '    HighListGridView(i, dt.Columns.IndexOf("Package_Id"))
                '    NotFound += 1
                'End If
                If dt.Columns.Contains("ShipTo") Then
                    If SelectData("Customer_Shipping_Location_Id", "ms_Customer_Shipping_Location", "Customer_Shipping_Location_Id = '" & dt.Rows(i).Item("ShipTo") & "'") = False Then
                        HighListGridView(i, dt.Columns.IndexOf("ShipTo"))
                        NotFound += 1
                    End If
                End If
                If dt.Columns.Contains("SalesOrder_No") Then
                    If SelectData("SalesOrder_No", "tb_SalesOrder", "salesOrder_No = '" & dt.Rows(i).Item("SalesOrder_No") & "'") Then
                        HighListGridView(i, dt.Columns.IndexOf("SalesOrder_No"))
                        NotFound += 1
                    End If
                End If
                If dt.Columns.Contains("Customer_Id") Then
                    If SelectData("Customer_Id", "ms_Customer", "Customer_Id = '" & dt.Rows(i).Item("Customer_Id") & "'") = False Then
                        HighListGridView(i, dt.Columns.IndexOf("Customer_Id"))
                        NotFound += 1
                    End If
                End If
                If dt.Columns.Contains("Sku_Id") And dt.Columns.Contains("Package_Id") Then
                    If SelectData("Sku_Id", "View_Check_Sku", "Sku_Id = '" & dt.Rows(i).Item("Sku_Id") & "'  And Package_Name = '" & dt.Rows(i).Item("Package_Id") & "'") = False Then
                        HighListGridView(i, dt.Columns.IndexOf("Sku_Id"))
                        HighListGridView(i, dt.Columns.IndexOf("Package_Id"))
                        NotFound += 1
                    End If
                End If
            Next
            'If dt.Columns.Contains("Sku_Id") And dt.Columns.Contains("Package_Id") Then
            '    SKU_Package_Test(dt)
            'End If
            'SaleOrder_Customer_Test(dt)
            If NotFound <> 0 Then
                MsgBox("พบข้อผิดพลาดจำนวน " & NotFound & " ตัว")
            Else
                MsgBox("Validate Success")
            End If
            Return NotFound
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Private Function SKU_Package_Test(ByVal dt As DataTable) As Boolean
    '    Dim _dt As New DataTable
    '    Dim _dtSelect As New DataTable
    '    Dim strList As New List(Of String)
    '    Dim strSelect As String = ""
    '    Dim strSQL As String = "SELECT TOP "
    '    Dim strSQLEnd As String = " Sku_Id,Package_Id FROM ms_SKU"
    '    strSQLEnd &= " INNER JOIN ms_SKURatio ON ms_SKu.Sku_Index = ms_SKURatio.Sku_Index"
    '    strSQLEnd &= " INNER JOIN ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
    '    strSQLEnd &= " INNER JOIN ms_DimensionType ON ms_Package.DimensionType_Index = ms_DimensionType.DimensionType_Index"
    '    strSQLEnd &= " WHERE "
    '    Try
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            If strList.IndexOf("(Sku_Id= '" & dt.Rows(i).Item("Sku_Id") & "' AND Package_Id = '" & dt.Rows(i).Item("Package_Id") & "')") = -1 Then
    '                strList.Add("(Sku_Id= '" & dt.Rows(i).Item("Sku_Id") & "' AND Package_Id = '" & dt.Rows(i).Item("Package_Id") & "')")
    '            End If
    '        Next
    '        Dim counter As Integer = 0, CountTOP = 1
    '        For Each list As String In strList
    '            If strSelect.Length + (list.Length * 2 + 100) < Max_SQLLength Then
    '                If counter <> strList.Count - 1 Then
    '                    strSelect &= list & " OR "
    '                Else
    '                    strSelect = strSQL & CountTOP & strSQLEnd & strSelect & list
    '                    _dtSelect = DBExeQuery(strSelect)
    '                    If _dtSelect.Rows.Count <> 0 Then
    '                        If _dt.Rows.Count = 0 Then
    '                            _dt.Merge(_dtSelect)
    '                        Else
    '                            For i As Integer = 0 To _dtSelect.Rows.Count - 1
    '                                If _dt.Select("Sku_Id = '" & _dtSelect.Rows(i).Item("Sku_Id") & "' AND Package_Id = '" & _dtSelect.Rows(i).Item("Package_Id") & "'").Length <> 0 Then
    '                                    _dtSelect.Rows(i).Delete()
    '                                End If
    '                            Next
    '                            If _dtSelect.Rows.Count > 0 Then
    '                                _dt.Merge(_dtSelect)
    '                            End If
    '                        End If
    '                    End If
    '                    strSelect = ""
    '                    CountTOP = 0
    '                    _dtSelect.Clear()
    '                End If
    '            Else
    '                strSelect = strSQL & CountTOP & strSQLEnd & strSelect & list
    '                _dtSelect = DBExeQuery(strSelect)
    '                If _dtSelect.Rows.Count <> 0 Then
    '                    If _dt.Rows.Count = 0 Then
    '                        _dt.Merge(_dtSelect)
    '                    Else
    '                        For i As Integer = 0 To _dtSelect.Rows.Count - 1
    '                            If _dt.Select("Sku_Id = '" & _dtSelect.Rows(i).Item("Sku_Id") & "' AND Package_Id = '" & _dtSelect.Rows(i).Item("Package_Id") & "'").Length <> 0 Then
    '                                _dtSelect.Rows(i).Delete()
    '                            End If
    '                        Next
    '                        If _dtSelect.Rows.Count > 0 Then
    '                            _dt.Merge(_dtSelect)
    '                        End If
    '                    End If
    '                End If
    '                strSelect = ""
    '                CountTOP = 0
    '                _dtSelect.Clear()
    '            End If
    '            CountTOP += 1
    '            counter += 1
    '        Next
    '        If _dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                If _dt.Select("Sku_Id = '" & dt.Rows(i).Item("Sku_Id") & "' AND Package_Id = '" & dt.Rows(i).Item("Package_Id") & "'").Length = 0 Then
    '                    HighListGridView(i, dt.Columns.IndexOf("Sku_Id"))
    '                    HighListGridView(i, dt.Columns.IndexOf("Package_Id"))
    '                    NotFound += 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '        Return False
    '    Finally
    '        strList.Clear()
    '        _dt.Clear()
    '        strSelect = Nothing
    '    End Try
    '    Return True
    'End Function
    'Private Function SaleOrder_Customer_Test(ByVal dt As DataTable) As Boolean
    '    Dim _dt As New DataTable
    '    Dim _dtSelect As New DataTable
    '    Dim strList As New List(Of String)
    '    Dim strSelect As String = ""
    '    Dim strSQL As String = "SELECT TOP "
    '    Dim strSQLEnd As String = " SalesOrder_No,Customer_Id FROM tb_SalesOrder"
    '    strSQLEnd &= " INNER JOIN ms_Customer ON tb_SalesOrder.Customer_Index = ms_Customer.Customer_Index"
    '    strSQLEnd &= " WHERE "
    '    Try
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            If strList.IndexOf("(SalesOrder_No= '" & dt.Rows(i).Item("SalesOrder_No") & "' AND Customer_Id = '" & dt.Rows(i).Item("Customer_Id") & "')") = -1 Then
    '                strList.Add("(SalesOrder_No= '" & dt.Rows(i).Item("SalesOrder_No") & "' AND Customer_Id = '" & dt.Rows(i).Item("Customer_Id") & "')")
    '            End If
    '        Next
    '        Dim counter As Integer = 0, CountTOP = 1
    '        For Each list As String In strList
    '            If strSelect.Length + (list.Length * 2 + 100) < Max_SQLLength Then
    '                If counter <> strList.Count - 1 Then
    '                    strSelect &= list & " OR "
    '                Else
    '                    strSelect = strSQL & CountTOP & strSQLEnd & strSelect & list
    '                    _dtSelect = DBExeQuery(strSelect)
    '                    If _dtSelect.Rows.Count <> 0 Then
    '                        If _dt.Rows.Count = 0 Then
    '                            _dt.Merge(_dtSelect)
    '                        Else
    '                            For i As Integer = 0 To _dtSelect.Rows.Count - 1
    '                                If _dt.Select("SalesOrder_No = '" & _dtSelect.Rows(i).Item("SalesOrder_No") & "' AND Customer_Id = '" & _dtSelect.Rows(i).Item("Customer_Id") & "'").Length <> 0 Then
    '                                    _dtSelect.Rows(i).Delete()
    '                                End If
    '                            Next
    '                            If _dtSelect.Rows.Count > 0 Then
    '                                _dt.Merge(_dtSelect)
    '                            End If
    '                        End If
    '                    End If
    '                    strSelect = ""
    '                    CountTOP = 0
    '                    _dtSelect.Clear()
    '                End If
    '            Else
    '                strSelect = strSQL & CountTOP & strSQLEnd & strSelect & list
    '                _dtSelect = DBExeQuery(strSelect)
    '                If _dtSelect.Rows.Count <> 0 Then
    '                    If _dt.Rows.Count = 0 Then
    '                        _dt.Merge(_dtSelect)
    '                    Else
    '                        For i As Integer = 0 To _dtSelect.Rows.Count - 1
    '                            If _dt.Select("SalesOrder_No = '" & _dtSelect.Rows(i).Item("SalesOrder_No") & "' AND Customer_Id = '" & _dtSelect.Rows(i).Item("Customer_Id") & "'").Length <> 0 Then
    '                                _dtSelect.Rows(i).Delete()
    '                            End If
    '                        Next
    '                        If _dtSelect.Rows.Count > 0 Then
    '                            _dt.Merge(_dtSelect)
    '                        End If
    '                    End If
    '                End If
    '                strSelect = ""
    '                CountTOP = 0
    '                _dtSelect.Clear()
    '            End If
    '            CountTOP += 1
    '            counter += 1
    '        Next
    '        If _dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                If _dt.Select("SalesOrder_No = '" & dt.Rows(i).Item("SalesOrder_No") & "' AND Customer_Id = '" & dt.Rows(i).Item("Customer_Id") & "'").Length <> 0 Then
    '                    HighListGridView(i, dt.Columns.IndexOf("SalesOrder_No"))
    '                    HighListGridView(i, dt.Columns.IndexOf("Customer_Id"))
    '                    NotFound += 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '        Return False
    '    Finally
    '        strList.Clear()
    '        _dt.Clear()
    '        strSelect = Nothing
    '    End Try
    '    Return True
    'End Function
    Private Function CheckShipTo(ByVal Id As String) As Boolean
        Try
            Dim strSQL As String = "SELECT TOP 1 Customer_Shipping_Location_Id FROM ms_Customer_Shipping_Location"
            strSQL &= " WHERE Customer_Shipping_Location_Id = '" & Id & "'"
            Return DBExeQuery(strSQL).Rows.Count
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function SelectData(ByVal field As String, ByVal table As String, Optional ByVal where As String = Nothing) As Boolean
        Try
            Dim strSQL As String = "SELECT TOP 1 " & field & " FROM " & table
            If where <> String.Empty Or where <> Nothing Then
                strSQL &= " WHERE " & where
            End If
            Return DBExeQuery(strSQL).Rows.Count
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub HighListGridView(ByVal row As Integer, ByVal cell As Integer)
        Try
            _grd.Rows(row).DefaultCellStyle.BackColor = Color.Silver
            _grd.Rows(row).Cells(cell).Style.BackColor = Color.FromArgb(248, 146, 6)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
