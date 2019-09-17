Public Class Sy_Running
    Inherits DBType_SQLServer

    Public Sub New()
        System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")
    End Sub

    Public Function GetNextRunning(ByVal RunningKey As String) As String
        If String.IsNullOrEmpty(RunningKey) Then
            Throw New Exception("ไม่พบข้อมูล Running Key")
        End If

        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" EXEC sp_GetNextRunning @Running_Key ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Running_Key", SqlDbType.VarChar).Value = RunningKey
            End With

            Dim DataRunning As DataTable = DBExeQuery(SQL.ToString)
            If DataRunning Is Nothing OrElse Not DataRunning.Rows.Count > 0 Then
                Throw New Exception("ไม่พบข้อมูล Running Key")
            End If

            Dim Result As Boolean = DataRunning.Rows.Item(0).Item("Result")
            Dim Message As String = DataRunning.Rows.Item(0).Item("Message").ToString
            Dim Value As String = DataRunning.Rows.Item(0).Item("Value").ToString
            Dim Format As String = DataRunning.Rows.Item(0).Item("Format").ToString
            Dim RunningDate As Date = DataRunning.Rows.Item(0).Item("Date").ToString

            If Not Result Then
                Throw New Exception(Message)
            End If

            Dim RunningNumber As String

            If String.IsNullOrEmpty(Format) Then
                RunningNumber = Value
            Else
                RunningNumber = Format.Trim.ToUpper
                RunningNumber = RunningNumber.Replace("YYYY", RunningDate.ToString("yyyy"))
                RunningNumber = RunningNumber.Replace("MM", RunningDate.ToString("MM"))
                RunningNumber = RunningNumber.Replace("DD", RunningDate.ToString("dd"))
                RunningNumber = RunningNumber.Replace("YY", RunningDate.ToString("yy"))
                RunningNumber = RunningNumber.Replace("Y", Strings.Right(RunningDate.ToString("yy"), 1))

                Dim CountRunning As Integer
                Dim RunningNumberFormat As String

                While Not RunningNumber.IndexOf("R") < 0
                    CountRunning = 1

                    For i As Integer = RunningNumber.IndexOf("R") + 1 To RunningNumber.Length - 1
                        If RunningNumber(i).ToString.Equals("R") Then
                            CountRunning += 1
                        Else
                            Exit For
                        End If
                    Next

                    If CountRunning > Value.Length Then
                        RunningNumberFormat = StrDup(CountRunning - Value.Length, "0") & Value
                    Else
                        RunningNumberFormat = Strings.Right(Value, CountRunning)
                    End If

                    RunningNumber = Replace(RunningNumber, StrDup(CountRunning, "R"), RunningNumberFormat)
                End While
            End If

            Return RunningNumber

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetNextRunningBAG(Optional ByVal WareHousePrefix As String = "1W") As String
        Try
            If WareHousePrefix Is Nothing Then
                WareHousePrefix = "1W"
            End If

            Return WareHousePrefix.Trim & GetNextRunning("PACKING_BAG")

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetNextRunningBOX(ByVal UrgentID As String, ByVal DropPointID As String) As String
        Try
            If UrgentID Is Nothing Then
                UrgentID = String.Empty
            End If

            If DropPointID Is Nothing Then
                DropPointID = String.Empty
            End If

            Return DropPointID.Trim & UrgentID.Trim & GetNextRunning("PACKING_BOX")

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    'Public Function GetNextRunning(ByVal RunningKey As String) As String
    '    If String.IsNullOrEmpty(RunningKey) Then
    '        Throw New Exception("ไม่พบข้อมูล Running Key")
    '    End If

    '    Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS, True)
    '    Try
    '        Dim SQL As New System.Text.StringBuilder
    '        With SQL
    '            .Append(" SELECT Running_Index, Running_Number, Running_Date, Reset_By_Day, Reset_By_Month, Reset_By_Year, Running_Format ")
    '            .Append(" FROM Sy_Running ")
    '            .Append(" WHERE Running_Key = @Running_Key ")
    '        End With

    '        With MyTransaction.SqlParameter
    '            .Clear()

    '            .Add("@Running_Key", SqlDbType.VarChar).Value = RunningKey
    '        End With

    '        Dim DataRunning As DataTable = MyTransaction.ExecuteQuery(SQL.ToString)
    '        If Not WMS_Function.DataTableHasValue(DataRunning) Then
    '            Throw New Exception(String.Format("ไม่พบข้อมูล Running Key [ {0} ] ในระบบ", RunningKey))
    '        End If

    '        Dim RunningIndex As Long = DataRunning.Rows.Item(0).Item("Running_Index")
    '        Dim RunningNumber As Long = DataRunning.Rows.Item(0).Item("Running_Number")
    '        Dim RunningDate As Date = DataRunning.Rows.Item(0).Item("Running_Date")
    '        Dim ResetByDay As Boolean = DataRunning.Rows.Item(0).Item("Reset_By_Day")
    '        Dim ResetByMonth As Boolean = DataRunning.Rows.Item(0).Item("Reset_By_Month")
    '        Dim ResetByYear As Boolean = DataRunning.Rows.Item(0).Item("Reset_By_Year")
    '        Dim RunningFormat As String = DataRunning.Rows.Item(0).Item("Running_Format").ToString

    '        Dim IsResetDate As Boolean = False
    '        Dim CurrentDate As Date = MyTransaction.ExecuteScalar("SELECT dbo.FORMATDATE(GETDATE(),'YYYY-MM-DD')")

    '        If ResetByDay Then
    '            IsResetDate = CurrentDate <> RunningDate
    '        Else
    '            Dim IsYearDiff As Boolean = CurrentDate.Year <> RunningDate.Year
    '            Dim IsMonthDiff As Boolean = IsYearDiff OrElse CurrentDate.Month <> RunningDate.Month

    '            If ResetByYear Then
    '                IsResetDate = IsYearDiff
    '            Else
    '                IsResetDate = IsMonthDiff
    '            End If
    '        End If

    '        If IsResetDate Then
    '            RunningDate = CurrentDate
    '            RunningNumber = 0
    '        End If

    '        RunningNumber += 1

    '        Dim SQL_Update As New System.Text.StringBuilder
    '        With SQL_Update
    '            .Append(" UPDATE Sy_Running , Running_Date, Reset_By_Day, Reset_By_Month, Reset_By_Year, Running_Format ")
    '            .Append(" SET Running_Number = @Running_Number ")
    '            .Append("   , Running_Date = @Running_Date ")
    '            .Append(" WHERE Running_Index = @Running_Index ")
    '        End With

    '        If Not UpdateRunning(Running, _Connection, _MyTrans) Then
    '            Throw New Exception("บันทึกข้อมูลผิดพลาด" & vbCrLf & "ไม่สามารถบันทึกข้อมูล Running ได้")
    '        End If

    '        Dim RunningNumber As String

    '        If String.IsNullOrEmpty(Running.RUN_FORMAT) Then
    '            RunningNumber = Running.RUN_NUMBER.ToString
    '        Else
    '            RunningNumber = Running.RUN_FORMAT.Trim.ToUpper
    '            RunningNumber = RunningNumber.Replace("YYYY", Running.RUN_DT.Value.ToString("yyyy"))
    '            RunningNumber = RunningNumber.Replace("MM", Running.RUN_DT.Value.ToString("MM"))
    '            RunningNumber = RunningNumber.Replace("DD", Running.RUN_DT.Value.ToString("dd"))
    '            RunningNumber = RunningNumber.Replace("YY", Running.RUN_DT.Value.ToString("yy"))

    '            Dim CountRunning As Integer
    '            Dim RunningNumberFormat As String

    '            While Not RunningNumber.IndexOf("R") < 0
    '                CountRunning = 1

    '                For i As Integer = RunningNumber.IndexOf("R") + 1 To RunningNumber.Length - 1
    '                    If RunningNumber(i).ToString.Equals("R") Then
    '                        CountRunning += 1
    '                    Else
    '                        Exit For
    '                    End If
    '                Next

    '                If CountRunning > Running.RUN_NUMBER.ToString.Length Then
    '                    RunningNumberFormat = StrDup(CountRunning - Running.RUN_NUMBER.ToString.Length, "0") & Running.RUN_NUMBER.ToString
    '                Else
    '                    RunningNumberFormat = Running.RUN_NUMBER.ToString
    '                End If

    '                RunningNumber = Replace(RunningNumber, StrDup(CountRunning, "R"), RunningNumberFormat, , 1)
    '            End While
    '        End If

    '        If IsNotPassTransaction Then
    '            _MyTrans.Commit()
    '        End If

    '        MyTransaction.Commit()

    '    Catch Ex As Exception
    '        MyTransaction.Rollback()
    '        Throw Ex
    '    End Try
    'End Function

End Class


