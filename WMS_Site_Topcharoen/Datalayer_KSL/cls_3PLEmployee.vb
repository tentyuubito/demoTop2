Imports WMS_STD_Formula
Imports System.Data.SqlClient
Imports System.Text

Public Class cls_3PLEmployee : Inherits DBType_SQLServer : Implements IDisposable

    Private strSQL As New StringBuilder

#Region "Employee"
    Public Function getEmployee(ByVal Document_Index As String, ByVal Process_Id As Integer) As DataTable
        Try
            strSQL.Length = 0
            strSQL.Append(" SELECT tb_3PLEmployee.Id_Running As Employee3PL_Index,ms_Employee.Employee_Index,ms_3PLEmployeeType.Type_Index,ms_Employee.Employee_Id As Employee_Id,ms_Employee.Employee_name As Employee_Name")
            strSQL.Append(" ,ms_3PLEmployeeType.Type_Name As EmployeeType")
            strSQL.Append(" FROM tb_3PLEmployee")
            strSQL.Append(" INNER JOIN ms_Employee ON tb_3PLEmployee.Employee_Index = ms_Employee.Employee_Index")
            strSQL.Append(" LEFT OUTER JOIN ms_3PLEmployeeType ON tb_3PLEmployee.EmployeeType_Index = ms_3PLEmployeeType.Type_Index AND ms_3PLEmployeeType.Type_Process = 0 AND ms_3PLEmployeeType.Status = 1")
            strSQL.Append(" WHERE tb_3PLEmployee.Status = 1 AND tb_3PLEmployee.Document_Index = @Document_Index AND tb_3PLEmployee.Process_Id = @Process_Id")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Document_Index", SqlDbType.VarChar).Value = Document_Index
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
            End With
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function insertEmployee(ByVal Employee_Index As String, ByVal EmployeeType_Index As Integer, ByVal Document_Index As String, ByVal Process_Id As Integer) As Integer
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Employee_Index", SqlDbType.VarChar).Value = Employee_Index
                .Add("@EmployeeType_Index", SqlDbType.Int).Value = EmployeeType_Index
                .Add("@Status", SqlDbType.Bit).Value = True
                .Add("@Document_Index", SqlDbType.VarChar).Value = Document_Index
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
            End With
            strSQL.Append(" INSERT INTO tb_3PLEmployee(Employee_Index,EmployeeType_Index,Status,Document_Index,Process_Id,add_by,add_date,add_branch)")
            strSQL.Append(" VALUES(@Employee_Index,@EmployeeType_Index,@Status,@Document_Index,@Process_Id {0})")
            Dim Index As Integer = Me.getIdentityNumber(String.Format(strSQL.ToString(), Me.addby()))
            If Index < 0 Then Throw New Exception("ไม่สามารถเพิ่มข้อมูลได้")
            Return Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function updateEmployee(ByVal Employee_Index As String, ByVal EmployeeType_Index As Integer, ByVal Employee3PL_Index As String) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Employee_Index", SqlDbType.VarChar).Value = Employee_Index
                .Add("@EmployeeType_Index", SqlDbType.Int).Value = EmployeeType_Index
                .Add("@Employee3PL_Index", SqlDbType.Int).Value = Employee3PL_Index
            End With
            strSQL.Append("UPDATE tb_3PLEmployee SET Employee_Index = @Employee_Index,EmployeeType_Index = @EmployeeType_Index {0} WHERE Id_Running = @Employee3PL_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function deleteEmployee(ByVal Employee3PL_Index) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Int).Value = False
                .Add("@Employee3PL_Index", SqlDbType.Int).Value = Employee3PL_Index
            End With
            strSQL.Append("UPDATE tb_3PLEmployee SET Status = @Status {0} WHERE Id_Running = @Employee3PL_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Worker"
    Public Function getWorker(ByVal Document_Index As String, ByVal Process_Id As Integer) As DataTable
        Try
            strSQL.Length = 0
            strSQL.Append(" SELECT tb_3PLWorker.Id_Running As Worker_Id,ms_3PLEmployeeType.Type_Name As Worker_Type,CONVERT(numeric(19,1),tb_3PLWorker.Qty) As Worker_Qty,ms_3PLWorkerUnit.Unit As Worker_Unit")
            strSQL.Append(" ,ms_3PLEmployeeType.Type_Index,ms_3PLWorkerUnit.WorkerUnit_Id As WorkerUnit_Index")
            strSQL.Append(" FROM tb_3PLWorker")
            strSQL.Append(" INNER JOIN ms_3PLEmployeeType ON tb_3PLWorker.WorkerType_Index = ms_3PLEmployeeType.Type_Index AND ms_3PLEmployeeType.Type_Process = 1 AND ms_3PLEmployeeType.Status = 1")
            strSQL.Append(" LEFT OUTER JOIN ms_3PLWorkerUnit ON tb_3PLWorker.WorkerUnit_Index = ms_3PLWorkerUnit.WorkerUnit_Id AND ms_3PLWorkerUnit.Status = 1")
            strSQL.Append(" WHERE tb_3PLWorker.Status = 1 AND tb_3PLWorker.Document_Index = @Document_Index AND tb_3PLWorker.Process_Id = @Process_Id")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Document_Index", SqlDbType.VarChar).Value = Document_Index
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
            End With
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function insertWorker(ByVal WorkerType_Index As String, ByVal WorkerUnit_Index As Integer, ByVal Document_Index As String, ByVal Process_Id As Integer) As Integer
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@WorkerType_Index", SqlDbType.VarChar).Value = WorkerType_Index
                .Add("@WorkerUnit_Index", SqlDbType.Int).Value = WorkerUnit_Index
                .Add("@Status", SqlDbType.Bit).Value = True
                .Add("@Document_Index", SqlDbType.VarChar).Value = Document_Index
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
            End With
            strSQL.Append(" INSERT INTO tb_3PLWorker(WorkerType_Index,WorkerUnit_Index,Status,Document_Index,Process_Id,add_by,add_date,add_branch)")
            strSQL.Append(" VALUES(@WorkerType_Index,@WorkerUnit_Index,@Status,@Document_Index,@Process_Id {0})")
            Dim Index As Integer = Me.getIdentityNumber(String.Format(strSQL.ToString(), Me.addby()))
            If Index < 0 Then Throw New Exception("ไม่สามารถเพิ่มข้อมูลได้")
            Return Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function updateWorker(ByVal WorkerType_Index As String, ByVal WorkerUnit_Index As Integer, ByVal Qty As Decimal, ByVal Worker3PL_Index As String) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@WorkerType_Index", SqlDbType.VarChar).Value = WorkerType_Index
                .Add("@WorkerUnit_Index", SqlDbType.Int).Value = WorkerUnit_Index
                .Add("@Worker3PL_Index", SqlDbType.Int).Value = Worker3PL_Index
                .Add("@Qty", SqlDbType.Decimal).Value = Qty
            End With
            strSQL.Append("UPDATE tb_3PLWorker SET WorkerType_Index = @WorkerType_Index,WorkerUnit_Index = @WorkerUnit_Index,Qty = @Qty {0} WHERE Id_Running = @Worker3PL_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function deleteWorker(ByVal Worker3PL_Index) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Int).Value = False
                .Add("@Worker3PL_Index", SqlDbType.Int).Value = Worker3PL_Index
            End With
            strSQL.Append("UPDATE tb_3PLWorker SET Status = @Status {0} WHERE Id_Running = @Worker3PL_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Worker Unit"
    Public Function getUnit3PL(ByVal Unit_Index As String, ByVal Unit_Name As String) As DataTable
        Try
            strSQL.Length = 0
            strSQL.Append(" SELECT WorkerUnit_Id As Unit_Index,Unit As Unit_Name,2 As NewFlag FROM ms_3PLWorkerUnit")
            strSQL.Append(" WHERE Status = @Status")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Bit).Value = True
                If Not String.IsNullOrEmpty(Unit_Index) Then
                    strSQL.Append(" AND WorkerUnit_Id = @Unit_Index")
                    .Add("@Unit_Index", SqlDbType.Int).Value = Unit_Index
                End If
                If Not String.IsNullOrEmpty(Unit_Name) Then
                    strSQL.Append(" AND Unit LIKE '%' + @Unit_Name + '%'")
                    .Add("@Unit_Name", SqlDbType.VarChar).Value = Unit_Name
                End If
            End With
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function insertUnit(ByVal Unit_Name As String) As Integer
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Unit_Name", SqlDbType.NVarChar).Value = Unit_Name
                .Add("@Status", SqlDbType.Bit).Value = True
            End With
            strSQL.Append(" INSERT INTO ms_3PLWorkerUnit(Unit,Status,add_by,add_date,add_branch)")
            strSQL.Append(" VALUES(@Unit_Name,@Status {0})")
            Dim Index As Integer = Me.getIdentityNumber(String.Format(strSQL.ToString(), Me.addby()))
            If Index < 1 Then Throw New Exception("ไม่สามารถเพิ่มข้อมูลได้")
            Return Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function deleteUnit(ByVal Unit_Index As Integer) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Bit).Value = False
                .Add("@Unit_Index", SqlDbType.Int).Value = Unit_Index
            End With
            strSQL.Append("UPDATE ms_3PLWorkerUnit SET Status = @Status {0} WHERE WorkerUnit_Id = @Unit_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function updateUnit(ByVal Unit_Index As Integer, ByVal Unit_Name As String) As Boolean
        Try
            strSQL.Length = 0
            strSQL.Append("UPDATE ms_3PLWorkerUnit SET Unit = @Unit_Name {0} WHERE WorkerUnit_Id = @Unit_Index")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Unit_Name", SqlDbType.NVarChar).Value = Unit_Name
                .Add("@Unit_Index", SqlDbType.Int).Value = Unit_Index
            End With
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
        End Try
    End Function
#End Region

#Region "Employee Type"
    Public Function getType3PL(ByVal Type_Index As String, ByVal Type_Name As String, ByVal Process_Id As Integer, ByVal Type_Process As Integer, Optional ByVal Connection As SqlConnection = Nothing, Optional ByVal myTrans As SqlTransaction = Nothing) As DataTable
        Try
            strSQL.Length = 0
            strSQL.Append(" SELECT Type_Index,Type_Name,2 As NewFlag FROM ms_3PLEmployeeType")
            strSQL.Append(" WHERE Status = @Status AND Type_Process = @Type_Process AND Process_Id = @Process_Id")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Bit).Value = True
                .Add("@Type_Process", SqlDbType.Int).Value = Type_Process
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
                If Not String.IsNullOrEmpty(Type_Index) Then
                    strSQL.Append(" AND Type_Index = @Type_Index")
                    SQLServerCommand.Parameters.Add("@Type_Index", SqlDbType.Int).Value = Type_Index
                End If
                If Not String.IsNullOrEmpty(Type_Name) Then
                    strSQL.Append(" AND Type_Name LIKE '%' + @Type_Name + '%'")
                    SQLServerCommand.Parameters.Add("@Type_Name", SqlDbType.VarChar).Value = Type_Name
                End If
            End With
            If Connection Is Nothing OrElse myTrans Is Nothing Then
                Return DBExeQuery(strSQL.ToString())
            Else
                Return DBExeQuery(strSQL.ToString(), Connection, myTrans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function insertType(ByVal Type_Name As String, ByVal Process_Id As Integer, ByVal Process_Type As Integer) As Integer
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Type_Name", SqlDbType.NVarChar).Value = Type_Name
                .Add("@Process_Id", SqlDbType.Int).Value = Process_Id
                .Add("@Type_Process", SqlDbType.Int).Value = Process_Type
                .Add("@Status", SqlDbType.Bit).Value = True
            End With
            strSQL.Append(" INSERT INTO ms_3PLEmployeeType(Type_Name,Process_Id,Type_Process,Status,add_by,add_date,add_branch)")
            strSQL.Append(" VALUES(@Type_Name,@Process_Id,@Type_Process,@Status {0} )")
            Dim Index As Integer = Me.getIdentityNumber(String.Format(strSQL.ToString(), Me.addby()))
            If Index < 1 Then Throw New Exception("ไม่สามารถเพิ่มข้อมูลได้")
            Return Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function deleteType(ByVal Type_Index As Integer) As Boolean
        Try
            strSQL.Length = 0
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Bit).Value = False
                .Add("@Type_Index", SqlDbType.Int).Value = Type_Index
            End With
            strSQL.Append("UPDATE ms_3PLEmployeeType SET Status = @Status {0} WHERE Type_Index = @Type_Index")
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function updateType(ByVal Type_Index As Integer, ByVal Type_Name As String) As Boolean
        Try
            strSQL.Length = 0
            strSQL.Append("UPDATE ms_3PLEmployeeType SET Type_Name = @Type_Name {0} WHERE Type_Index = @Type_Index")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Type_Name", SqlDbType.NVarChar).Value = Type_Name
                .Add("@Type_Index", SqlDbType.Int).Value = Type_Index
            End With
            Return DBExeNonQuery(String.Format(strSQL.ToString(), Me.updateby())) > 0
        Catch ex As Exception
        End Try
    End Function
#End Region

    Private Function getIdentityNumber(ByVal Sql As String) As Integer
        Try
            strSQL.Length = 0
            strSQL.Append(Sql)
            strSQL.Append(";" & Environment.NewLine & Environment.NewLine)
            strSQL.Append("SELECT @@IDENTITY")
            Using dt As DataTable = DBExeQuery(strSQL.ToString())
                If dt.Rows.Count = 0 OrElse IsDBNull(dt.Rows(0).Item(0)) Then Return -1
                Return dt.Rows(0).Item(0)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function addby() As String
        Try
            With SQLServerCommand.Parameters
                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            Return " ,@add_by,GETDATE(),@add_branch "
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function updateby() As String
        Try
            With SQLServerCommand.Parameters
                .Add("@update_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            Return " ,update_by = @update_by,update_date = GETDATE(),update_branch = @update_Branch "
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub setDataColumnReadOnly(ByVal dt As DataTable, Optional ByVal isReadOnly As Boolean = False)
        Try
            If dt Is Nothing Then Exit Sub
            For Each dc As DataColumn In dt.Columns
                dc.ReadOnly = isReadOnly
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub setDataColumnReadOnly(ByVal dt As DataTable, ByVal Column_Name As String, Optional ByVal isReadOnly As Boolean = False)
        Try
            If dt.Columns.Contains(Column_Name) Then
                dt.Columns(Column_Name).ReadOnly = isReadOnly
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub setDataColumnAllowNull(ByVal dt As DataTable, Optional ByVal isAllowNull As Boolean = False)
        Try
            If dt Is Nothing Then Exit Sub
            For Each dc As DataColumn In dt.Columns
                If Not dc.AllowDBNull Then
                    dc.AllowDBNull = isAllowNull
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub setDataColumnAllowNull(ByVal dt As DataTable, ByVal Column_Name As String, Optional ByVal isAllowNull As Boolean = False)
        Try
            If dt.Columns.Contains(Column_Name) Then
                dt.Columns(Column_Name).AllowDBNull = isAllowNull
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private disposedValue As Boolean = False
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                strSQL = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
