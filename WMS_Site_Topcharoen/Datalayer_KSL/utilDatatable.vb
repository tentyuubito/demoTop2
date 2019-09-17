Public Module utilDatatable


    Public Function GroupDataTable(ByVal Data As DataTable, ByVal Fields As String()) As DataTable
        Try
            Return GroupDataTable(Data, Fields, New String() {}, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GroupDataTable(ByVal Data As DataTable, ByVal Fields As String(), ByVal SumFields As String(), ByVal strWhere As String) As DataTable
        Try
            Dim dtResult As New DataTable

            If Data.Columns.Count = 0 Then Return Nothing



            Dim DataGroup As New DataTable
            DataGroup = Data.Copy


            Dim dtWhere As New DataTable
            If strWhere.Trim.Length > 0 Then
                dtWhere = DataGroup.Clone
                Dim drrDataWhere() As DataRow
                drrDataWhere = Data.Select(strWhere)
                If drrDataWhere.Length > 0 Then


                    For Each dr As DataRow In drrDataWhere
                        dtWhere.Rows.Add(dr.ItemArray)
                    Next
                End If
            Else
                dtWhere = Data
            End If
            DataGroup.Clear()
            DataGroup = dtWhere





            For Each str As String In Fields
                If DataGroup.Columns.Contains(str.Trim) = False Then Return Nothing
                dtResult.Columns.Add(DataGroup.Columns(str.Trim).ColumnName, DataGroup.Columns(str.Trim).DataType)
            Next




            Dim strSelect As New System.Text.StringBuilder


            For Each dr As DataRow In DataGroup.Rows
                strSelect = New System.Text.StringBuilder
                For Each str As String In Fields
                    If strSelect.Length = 0 Then
                        strSelect.Append(String.Format(" [{0}] = '{1}' ", str, dr(str).ToString.Replace("'", "''")))
                    Else
                        strSelect.Append(String.Format(" AND [{0}] = '{1}' ", str, dr(str).ToString.Replace("'", "''")))
                    End If
                Next


                If dtResult.Select(strSelect.ToString).Length = 0 Then
                    Dim dradd As DataRow
                    dradd = dtResult.NewRow
                    For Each str As String In Fields
                        If dr(str).ToString = "" Then '{System.DBNull}
                            dr(str) = ""
                        End If
                        dradd(str) = dr(str)
                    Next
                    dtResult.Rows.Add(dradd)

                End If
                dtResult.AcceptChanges()

            Next



            If SumFields.Length > 0 Then




                Dim DecSum As Decimal = 0


                Dim dttmpCol As New DataTable
                dttmpCol = dtResult.Clone


                Dim fillter As New System.Text.StringBuilder

                For Each str As String In SumFields
                    If DataGroup.Columns.Contains(str) = False Then Return Nothing
                    dtResult.Columns.Add(DataGroup.Columns(str).ColumnName, DataGroup.Columns(str).DataType)


                    For Each dr As DataRow In dtResult.Rows
                        DecSum = 0
                        fillter = New System.Text.StringBuilder
                        Dim icol As Integer = 0
                        For Each col As DataColumn In dttmpCol.Columns   
                            If icol > 0 Then fillter.Append(" AND ")
                            fillter.Append(String.Format("{0}='{1}'", col.ColumnName, dr(col.ColumnName).ToString.Replace("'", "''")))
                            icol += 1
                        Next

                        'Try
                        'Dim xdr() As DataRow = DataGroup.Select(fillter.ToString)
                        DecSum = DataGroup.Compute(String.Format("SUM({0})", str), fillter.ToString)
                        dr(str) = DecSum
                        '                Catch ex As Exception
                        '    MessageBox.Show(ex.ToString)
                        'End Try

                    Next







                Next


            End If





            Return dtResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function UnGroupDataTable(ByVal dtData As DataTable, ByVal drGroup As DataRow) As DataTable
        Try
            Dim dtGroup As New DataTable
            dtGroup = drGroup.Table.Clone
            dtGroup.Rows.Add(drGroup.ItemArray)
            Return UnGroupDataTable(dtData, dtGroup, New String() {}, New String() {})
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function UnGroupDataTable(ByVal dtData As DataTable, ByVal drGroup As DataRow, ByVal NotUsedFields As String()) As DataTable
        Try
            Dim dtGroup As New DataTable
            dtGroup = drGroup.Table.Clone
            dtGroup.Rows.Add(drGroup.ItemArray)
            Return UnGroupDataTable(dtData, dtGroup, NotUsedFields, New String() {})
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UnGroupDataTable(ByVal dtData As DataTable, ByVal drGroup As DataRow, ByVal NotUsedFields As String(), ByVal NotUsedFieldWhenEmpty As String()) As DataTable
        Try
            Dim dtGroup As New DataTable
            dtGroup = drGroup.Table.Clone
            dtGroup.Rows.Add(drGroup.ItemArray)
            Return UnGroupDataTable(dtData, dtGroup, NotUsedFields, NotUsedFieldWhenEmpty)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function UnGroupDataTable(ByVal dtData As DataTable, ByVal dtGroup As DataTable, ByVal NotUsedFields As String(), ByVal NotUsedFieldWhenEmpty As String()) As DataTable
        Dim dtResult As New DataTable
        Try

            dtResult = dtData.Clone

            Dim dtNotUsedFields As New DataTable
            dtNotUsedFields.Columns.Add("NotUsedFields", GetType(String))
            For Each str As String In NotUsedFields
                dtNotUsedFields.Rows.Add(str)
            Next



            Dim dtNotUsedFieldWhenEmpty As New DataTable
            dtNotUsedFieldWhenEmpty.Columns.Add("NotUsedFieldWhenEmpty", GetType(String))
            For Each str As String In NotUsedFieldWhenEmpty
                dtNotUsedFieldWhenEmpty.Rows.Add(str)
            Next

            Dim strSelect As New System.Text.StringBuilder

            For Each dr As DataRow In dtGroup.Rows
                strSelect = New System.Text.StringBuilder
                For Each colGroup As DataColumn In dtGroup.Columns
                    Dim str As String = colGroup.ColumnName
                    If dtData.Columns.Contains(str) = False Or dtNotUsedFields.Select(String.Format("NotUsedFields='{0}'", str)).Length > 0 Then Continue For

                    If dtNotUsedFieldWhenEmpty.Select(String.Format("NotUsedFieldWhenEmpty='{0}'", str)).Length > 0 Then
                        If IsDBNull(dr(str)) Or dr(str).ToString.Trim.Length > 0 Then
                            Continue For
                        End If
                    End If

                    If strSelect.Length = 0 Then
                        strSelect.Append(String.Format(" {0} = '{1}' ", str, dr(str).ToString.Replace("'", "''")))
                    Else
                        strSelect.Append(String.Format(" AND {0} = '{1}' ", str, dr(str).ToString.Replace("'", "''")))
                    End If


                Next

                Dim drrResult() As DataRow
                drrResult = dtData.Select(strSelect.ToString)

                For Each drResult As DataRow In drrResult
                    dtResult.Rows.Add(drResult.ItemArray)
                Next

                dtResult.AcceptChanges()

            Next


            Return dtResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
