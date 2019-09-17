Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class Insert_log : Inherits DBType_SQLServer
#Region "  Property  Import Log "

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    '''''' All import  Log ''''''''
    Private _Import_Log_Index As String = ""
    Private _Log_Date As Date
    Private _Format_Name As String = ""
    Private _File_Name As String = ""
    Private _Start_Timestamp As Date
    Private _End_Timestamp As Date
    Private _Imp_Row_Count As Integer = 0
    Private _Complete_Row_Count As Integer = 0
    '''''' Error  Log ''''''''''
    Private _Import_ErrorLog_Index As String = ""
    Private _Err_Msg As String = ""
    Private _Err_Checkpoint As String = ""
    Private _Err_Timestamp As Date
    Private _IsError As Integer = 0
    Private _Line_Item As String = ""
    Private _Remark As String = ""
#End Region
#Region "  Property  STR "


    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Str6 As String = ""
    Private _Str7 As String = ""
    Private _Str8 As String = ""
    Private _Str9 As String = ""
    Private _Str10 As String = ""
    Public Property Str1() As String
        Get
            Return _Str1
        End Get
        Set(ByVal Value As String)
            _Str1 = Value
        End Set
    End Property

    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal Value As String)
            _Str2 = Value
        End Set
    End Property

    Public Property Str3() As String
        Get
            Return _Str3
        End Get
        Set(ByVal Value As String)
            _Str3 = Value
        End Set
    End Property

    Public Property Str4() As String
        Get
            Return _Str4
        End Get
        Set(ByVal Value As String)
            _Str4 = Value
        End Set
    End Property

    Public Property Str5() As String
        Get
            Return _Str5
        End Get
        Set(ByVal Value As String)
            _Str5 = Value
        End Set
    End Property

    Public Property Str6() As String
        Get
            Return _Str6
        End Get
        Set(ByVal Value As String)
            _Str6 = Value
        End Set
    End Property

    Public Property Str7() As String
        Get
            Return _Str7
        End Get
        Set(ByVal Value As String)
            _Str7 = Value
        End Set
    End Property

    Public Property Str8() As String
        Get
            Return _Str8
        End Get
        Set(ByVal Value As String)
            _Str8 = Value
        End Set
    End Property

    Public Property Str9() As String
        Get
            Return _Str9
        End Get
        Set(ByVal Value As String)
            _Str9 = Value
        End Set
    End Property

    Public Property Str10() As String
        Get
            Return _Str10
        End Get
        Set(ByVal Value As String)
            _Str10 = Value
        End Set
    End Property

#End Region
    Sub InsertLog(ByVal pImport_Log_index As String, ByVal pFormat_Name As String, ByVal pFile_Name As String, ByVal pStart_Timestamp As Date, ByVal pImp_Row_Count As Integer, ByVal pComplete_Row_Count As Integer)
        Dim strSQL As String = " "
        Try
            strSQL = " INSERT INTO Sy_Import_Log (" & _
             " Import_Log_Index " & _
             " ,Log_Date " & _
             " ,Format_Name " & _
             " ,File_Name " & _
             " ,Start_Timestamp " & _
             " ,End_Timestamp" & _
             " ,Imp_Row_Count" & _
             " ,Complete_Row_Count" & _
             " ,Str1" & _
             " ,Str2" & _
             " ,Str3" & _
             " ,Str4" & _
             " ,Str5" & _
             " ,Str6" & _
             " ,Str7" & _
             " ,Str8" & _
             " ,Str9" & _
             " ,Str10" & _
             ")  VALUES  ( " & _
             " @Import_Log_Index " & _
             " ,getdate() " & _
             " ,@Format_Name " & _
             " ,@File_Name " & _
             " ,@Start_Timestamp " & _
             " ,getdate() " & _
             " ,@Imp_Row_Count" & _
             " ,@Complete_Row_Count" & _
             " ,@Str1" & _
             " ,@Str2" & _
             " ,@Str3" & _
             " ,@Str4" & _
             " ,@Str5" & _
             " ,@Str6" & _
             " ,@Str7" & _
             " ,@Str8" & _
             " ,@Str9" & _
             " ,@Str10" & _
             " )"

            'Dim objAutonumber As New Sy_AutoNumber
            'Me._Import_Log_Index = objAutonumber.getSys_Value("Import_Log_Index")
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Import_Log_Index", SqlDbType.VarChar, 13).Value = pImport_Log_index
                .Parameters.Add("@Format_Name", SqlDbType.VarChar, 50).Value = pFormat_Name
                .Parameters.Add("@File_Name", SqlDbType.VarChar, 255).Value = pFile_Name
                .Parameters.Add("@Start_Timestamp", SqlDbType.DateTime, 7).Value = pStart_Timestamp
                .Parameters.Add("@Imp_Row_Count", SqlDbType.Int, 4).Value = pImp_Row_Count
                .Parameters.Add("@Complete_Row_Count", SqlDbType.Int, 4).Value = pComplete_Row_Count
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 255).Value = _Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 255).Value = _Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 255).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 255).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 255).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 255).Value = _Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 255).Value = _Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 255).Value = _Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 255).Value = _Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 255).Value = _Str10
            End With


            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub


    Sub insertErrorLog(ByVal pFile_Name As String, ByVal pErr_Msg As String, ByVal pErr_Checkpoint As String, ByVal pLine_Item As String, ByVal pImport_log_index As String, ByVal pRemark As String)
        Dim strSQL As String = " "
        Try

            strSQL = " INSERT INTO sy_Import_Error_log (" & _
                       " Import_ErrorLog_Index " & _
                       " ,Import_Log_Index" & _
                       " ,Err_Timestamp " & _
                       " ,File_Name " & _
                       " ,Err_Msg " & _
                       " ,Err_Checkpoint " & _
                       " ,Line_Item" & _
                       " ,Remark" & _
                       " ,Str1" & _
                       " ,Str2" & _
                       " ,Str3" & _
                       " ,Str4" & _
                       " ,Str5" & _
                       " ,Str6" & _
                       " ,Str7" & _
                       " ,Str8" & _
                       " ,Str9" & _
                       " ,Str10" & _
                       ")  VALUES  ( " & _
                       " @Import_ErrorLog_Index " & _
                       " ,@Import_Log_Index" & _
                       " ,getdate() " & _
                       " ,@File_Name " & _
                       " ,@Err_Msg " & _
                       " ,@Err_Checkpoint " & _
                       " ,@Line_Item" & _
                       " ,@Remark" & _
                       " ,@Str1" & _
                       " ,@Str2" & _
                       " ,@Str3" & _
                       " ,@Str4" & _
                       " ,@Str5" & _
                       " ,@Str6" & _
                       " ,@Str7" & _
                       " ,@Str8" & _
                       " ,@Str9" & _
                       " ,@Str10" & _
                       " )"

            Dim objAutonumber As New Sy_AutoNumber
            Me._Import_ErrorLog_Index = objAutonumber.getSys_Value("Import_ErrorLog_Index")
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Import_ErrorLog_Index", SqlDbType.VarChar, 13).Value = Me._Import_ErrorLog_Index
                .Parameters.Add("@Import_Log_Index", SqlDbType.VarChar, 13).Value = pImport_log_index
                .Parameters.Add("@File_Name", SqlDbType.VarChar, 255).Value = pFile_Name
                .Parameters.Add("@Err_Msg", SqlDbType.NVarChar, 2000).Value = pErr_Msg
                .Parameters.Add("@Err_Checkpoint", SqlDbType.NVarChar, 2000).Value = pErr_Checkpoint
                .Parameters.Add("@Line_Item", SqlDbType.NVarChar, 2000).Value = pLine_Item
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = pRemark
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 255).Value = _Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 255).Value = _Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 255).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 255).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 255).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 255).Value = _Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 255).Value = _Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 255).Value = _Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 255).Value = _Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 255).Value = _Str10
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Function CheckNoItem() As Boolean
        Dim strSql As String = ""
        Try

            strSql = " Select PurchaseOrder_Index"
            strSql &= " from tb_PurchaseOrder"
            strSql &= " where PurchaseOrder_Index  Not in ("
            strSql &= "	         select PurchaseOrder_Index"
            strSql &= "           from tb_PurchaseOrderItem )"
            strSql &= " AND PurchaseOrder_Index <> '0000000000000'"

            SetSQLString = strSql
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then Return False

            For i As Integer = 0 To _dataTable.Rows.Count - 1

                strSql = ""
                strSql = " Delete tb_PurchaseOrder where PurchaseOrder_Index ='" & _dataTable.Rows(i).Item("PurchaseOrder_Index") & "' "
                SetSQLString = strSql
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                connectDB()
                EXEC_Command()

            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

End Class
