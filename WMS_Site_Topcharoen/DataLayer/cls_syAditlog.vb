Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class cls_syAditlog : Inherits DBType_SQLServer


    Private _Process_ID As String = ""
    Private _Description As String = ""
    Private _Document_Index As String = ""
    Private _Document_No As String = ""
    Private _Audit_log_index As String = ""
    Private _Log_Type_ID As String = ""

    Public Property Process_ID() As String
        Get
            Return _Process_ID
        End Get
        Set(ByVal value As String)
            _Process_ID = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
    Public Property Document_Index() As String
        Get
            Return _Document_Index
        End Get
        Set(ByVal value As String)
            _Document_Index = value
        End Set
    End Property
    Public Property Document_No() As String
        Get
            Return _Document_No
        End Get
        Set(ByVal value As String)
            _Document_No = value
        End Set
    End Property
    Public Property Log_Type_ID() As String
        Get
            Return _Log_Type_ID
        End Get
        Set(ByVal value As String)
            _Log_Type_ID = value
        End Set
    End Property
  

    Public Function Insert_Master() As Boolean
        Dim strSQL As String

        Try
            If Trim(_Audit_log_index) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _Audit_log_index = objDocumentNumber.getSys_ID("Audit_Log_IndexV2")

                objDocumentNumber = Nothing

            End If

            strSQL = " insert into  Sy_Audit_Log  (Audit_Log_Index,Proces_id,TypePatfrom,Branch_ID,Log_Date,Log_Type_ID,Event_Date,Description,Document_Index,Document_No,user_Index,userName,Host_Name,Host_IP)"
            strSQL &= " values(@Audit_Log_Index,@Process_ID,@TypePatfrom,@Branch_ID,getdate(),@Log_Type_ID,getdate(),@Description,@Document_Index,@Document_No,@user_Index,@userName,@Host_Name,@Host_IP)"


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Audit_Log_Index", SqlDbType.VarChar).Value = Me._Audit_log_index
                .Parameters.Add("@Process_ID", SqlDbType.Int).Value = Me._Process_ID
                .Parameters.Add("@TypePatfrom", SqlDbType.VarChar).Value = "PC"
                .Parameters.Add("@Branch_ID", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@Description", SqlDbType.VarChar).Value = Me._Description
                .Parameters.Add("@Document_Index", SqlDbType.VarChar).Value = Me._Document_Index
                .Parameters.Add("@Document_No", SqlDbType.VarChar).Value = Me._Document_No
                .Parameters.Add("@Log_Type_ID", SqlDbType.Int).Value = Me._Log_Type_ID
                .Parameters.Add("@user_Index", SqlDbType.VarChar).Value = WV_User_Index
                .Parameters.Add("@userName", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@Host_Name", SqlDbType.VarChar).Value = WV_Host_Name
                .Parameters.Add("@Host_IP", SqlDbType.VarChar).Value = WV_Host_Ip
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function

    Public Function Insert_Config_RPT(ByVal Report_Name As String, ByVal Unit_Print As Integer, ByVal Total_Print As Integer, ByVal SalesOrder_Index As String) As Boolean
        Dim strSQL As String

        Try


            strSQL = " Insert into  config_RPT_Print  (Report_Name,Unit_Print,Total_Print,SalesOrder_Index,add_by,add_date,add_branch)"
            strSQL &= " values (@Report_Name,@Unit_Print,@Total_Print,@SalesOrder_Index,@add_by,getdate(),@add_branch)"


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Report_Name", SqlDbType.VarChar).Value = Report_Name
                .Parameters.Add("@Unit_Print", SqlDbType.Int).Value = Unit_Print
                .Parameters.Add("@Total_Print", SqlDbType.Int).Value = Total_Print
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.VarChar).Value = WV_Branch_ID

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
    Public Function SetNewSO_Report(ByVal SalesOrder_Index As String) As Boolean
        Dim strSQL As String

        Try


            strSQL = " Insert into  tb_SalesOrder_PrintINV  (SalesOrder_Index,RPT_KSL_Invoice,RPT_KSL_Invoice_Copy,RPT_KSL_Invoice_Dup,RPT_KSL_Invoice_PrintFrom,RPT_KSL_Recept)"
            strSQL &= " values (@SalesOrder_Index,@RPT_KSL_Invoice,@RPT_KSL_Invoice_Copy,@RPT_KSL_Invoice_Dup,@RPT_KSL_Invoice_PrintFrom,@RPT_KSL_Recept)"


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
                .Parameters.Add("@RPT_KSL_Invoice", SqlDbType.Int).Value = 0
                .Parameters.Add("@RPT_KSL_Invoice_Copy", SqlDbType.Int).Value = 0
                .Parameters.Add("@RPT_KSL_Invoice_Dup", SqlDbType.Int).Value = 0
                .Parameters.Add("@RPT_KSL_Invoice_PrintFrom", SqlDbType.Int).Value = 0
                .Parameters.Add("@RPT_KSL_Recept", SqlDbType.Int).Value = 0

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
    Public Function CheckDupSOinINV(ByVal SalesOrder_Index As String) As DataTable
        Dim strSQL As String

        Try


            strSQL = " select * from tb_SalesOrder_PrintINV where SalesOrder_Index = @SalesOrder_Index"


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index

            End With

            Dim Result As New DataTable
            Result = DBExeQuery(strSQL)

            If Result.Rows.Count > 0 Then
                Return Result
            End If

            Return Nothing

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
    Public Function Update_Config_RPT(ByVal Report_Name As String, ByVal Unit_Print As Integer, ByVal Total_Print As Integer, ByVal SalesOrder_Index As String, ByVal Config_Rpt_Index As String) As Boolean
        Dim strSQL As String

        Try
            strSQL = " Update config_RPT_Print set Report_Name=@Report_Name,Unit_Print=@Unit_Print,Total_Print=@Total_Print,SalesOrder_Index=@SalesOrder_Index,update_by=@update_by,update_date=getdate(),update_branch=@update_branch where Config_Rpt_Index = @Config_Rpt_Index"
            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Report_Name", SqlDbType.VarChar).Value = Report_Name
                .Parameters.Add("@Unit_Print", SqlDbType.Int).Value = Unit_Print
                .Parameters.Add("@Total_Print", SqlDbType.Int).Value = Total_Print
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
                .Parameters.Add("@update_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@Config_Rpt_Index", SqlDbType.Int).Value = Config_Rpt_Index
            End With

            DBExeNonQuery(strSQL)


            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function

End Class
