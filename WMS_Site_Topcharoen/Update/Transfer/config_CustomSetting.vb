Imports System.Data
'Imports System.Data.SqlClient.SqlCommand

Public Class config_CustomSetting : Inherits DBType_SQLServer

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property

    Public Sub GetConfig_Value(ByVal pstrConfig_Key As String, Optional ByVal pstrWhere As String = "")
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT * "
            strSQL &= " FROM dbo.config_CustomSetting "
            strSQL &= " WHERE     Config_Key = '" & pstrConfig_Key & "'"

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function GetConfig_Picking(ByVal Column_PickingType As String) As String
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT condition "
            strSQL &= " FROM config_Picking "
            strSQL &= " WHERE     Picking_Type = '" & Column_PickingType & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput Is Nothing Then
                _scalarOutput = ""
            End If

            Return _scalarOutput
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetOther_Where(ByVal Column_PickingType As String, ByVal strCondition As String) As String
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try

            strSQL = " SELECT Other_Where "
            strSQL &= " FROM config_Picking  "
            strSQL &= "where Picking_Type =@PickingType "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@PickingType", SqlDbType.NVarChar, 1000).Value = Column_PickingType

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
                EXEC_Command()
            End With


            _scalarOutput = GetScalarOutput

            If _scalarOutput Is Nothing Then
                _scalarOutput = ""
            Else
                _scalarOutput = _scalarOutput.Replace("???", strCondition)
            End If

            myTrans.Commit()
            Return _scalarOutput
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetValue_Config(ByVal pstrConfig_Key As String, Optional ByVal pstrWhere As String = "") As String
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT * "
            strSQL &= " FROM dbo.config_CustomSetting "
            strSQL &= " WHERE     Config_Key = '" & pstrConfig_Key & "'"

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If Me.GetDataTable.Rows.Count > 0 Then
                Return Me.GetDataTable.Rows(0)("Config_Value").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getConfig_Key_USE(ByVal vConfig_Key As String) As Boolean
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT Config_Value"
            strSQL &= " FROM config_CustomSetting"
            strSQL &= "           WHERE          Config_Key = '" & vConfig_Key & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case ""
                    Return False
                Case Else
                    Return CBool(_scalarOutput)
            End Select

            'If CBool(_scalarOutput) Then
            '    Return True
            'Else
            '    Return False
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getConfig_Key_DEFUALT(ByVal vConfig_Key As String) As String
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT Config_Value"
            strSQL &= " FROM config_CustomSetting"
            strSQL &= "           WHERE          Config_Key = '" & vConfig_Key & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            'If _scalarOutput.Trim = "0" Then
            '    Return 0
            'ElseIf _scalarOutput.Trim = "" Then
            '    Return ""
            'Else
            '    Return _scalarOutput
            'End If

            Select Case _scalarOutput
                Case Nothing
                    Return ""
                Case Else
                    Return _scalarOutput
            End Select

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Sub GetDEFAULT_CUSTOMER_INDEX()
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     dbo.config_CustomSetting.Config_Value, dbo.ms_Customer.Customer_Name, dbo.ms_Customer.Customer_Id, "
            strSQL &= " dbo.ms_Customer.Customer_Index"
            strSQL &= " FROM         dbo.config_CustomSetting INNER JOIN "
            strSQL &= "             dbo.ms_Customer ON dbo.config_CustomSetting.Config_Value = dbo.ms_Customer.Customer_Index"
            strSQL &= " WHERE     (dbo.config_CustomSetting.Config_Key = 'DEFAULT_CUSTOMER_INDEX')"

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetDEFAULT_Customer_Shipping_Index()
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'strSQL = " SELECT     dbo.config_CustomSetting.Config_Value, dbo.ms_Customer.Customer_Name, dbo.ms_Customer.Customer_Id, "
            'strSQL &= " dbo.ms_Customer.Customer_Index"
            'strSQL &= " FROM         dbo.config_CustomSetting INNER JOIN "
            'strSQL &= "             dbo.ms_Customer ON dbo.config_CustomSetting.Config_Value = dbo.ms_Customer.Customer_Index"
            'strSQL &= " WHERE     (dbo.config_CustomSetting.Config_Key = 'DEFAULT_Customer_Shipping_Index')"

            strSQL = " SELECT     config_CustomSetting.Config_Value, ms_Customer_Shipping.Customer_Shipping_Index, ms_Customer_Shipping.Company_Name, "
            strSQL &= "   ms_Customer_Shipping.Str1 "
            strSQL &= " FROM         config_CustomSetting INNER JOIN"
            strSQL &= "          ms_Customer_Shipping ON config_CustomSetting.Config_Value = ms_Customer_Shipping.Customer_Shipping_Index"
            strSQL &= "  WHERE     (config_CustomSetting.Config_Key = 'DEFAULT_Customer_Shipping_Index')"

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetDEFAULT_Supplier_Index()
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     dbo.config_CustomSetting.Config_Value, dbo.ms_Supplier.Supplier_Name, dbo.ms_Supplier.Supplier_Id, "
            strSQL &= "   dbo.ms_Supplier.Supplier_Index"
            strSQL &= "   FROM         dbo.config_CustomSetting INNER JOIN "
            strSQL &= "             dbo.ms_Supplier ON dbo.config_CustomSetting.Config_Value = dbo.ms_Supplier.Supplier_Index"
            strSQL &= "   WHERE     (dbo.config_CustomSetting.Config_Key = 'DEFAULT_Supplier_Index')"

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    'big function
    Public Sub SetConfig_Value(ByVal Name As String, ByVal Data As String)
        Dim strSQL As String = ""
        Try
            'UPDATE config_CustomSetting SET Config_Value = 123 
            'WHERE Config_Key=
            'DEFAULT_LAYOUT_BG_COLOR'
            strSQL &= " UPDATE config_CustomSetting SET Config_Value=@Config_Value"
            strSQL &= " WHERE Config_Key = '" & Name & "'"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Config_Value", SqlDbType.NVarChar, 1000).Value = Data
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



End Class
