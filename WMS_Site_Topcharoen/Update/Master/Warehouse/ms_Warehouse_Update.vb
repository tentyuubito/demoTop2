'*** Create Date :  17/01/2008
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class ms_Warehouse : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _warehouse_Index As String
    Private _branch_Id As Integer
    Private _warehouse_No As String
    Private _seq As Integer
    Private _description As String
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _DistributionCenter_Index As String

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum
#End Region

#Region " Properties Section "
    '*** Property Readonly
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

    '*** Property Writeonly

    '*** Property Read and Write
    Public Property Warehouse_Index() As String
        Get
            Return _warehouse_Index
        End Get
        Set(ByVal Value As String)
            _warehouse_Index = Value
        End Set
    End Property
    Public Property Branch_Id() As Integer
        Get
            Return _branch_Id
        End Get
        Set(ByVal Value As Integer)
            _branch_Id = Value
        End Set
    End Property
    Public Property Warehouse_No() As String
        Get
            Return _warehouse_No
        End Get
        Set(ByVal Value As String)
            _warehouse_No = Value
        End Set
    End Property
    Public Property Seq() As Integer
        Get
            Return _seq
        End Get
        Set(ByVal Value As Integer)
            _seq = Value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
    Public Property Add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property
    Public Property Add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property
    Public Property Add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property
    Public Property Update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property
    Public Property Update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property
    Public Property Update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property DistributionCenter_Index() As String
        Get
            Return _DistributionCenter_Index
        End Get
        Set(ByVal Value As String)
            _DistributionCenter_Index = Value
        End Set
    End Property

#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
    End Sub




#End Region

#Region "  CHECK DATA  "
    Public Function isExistID(ByVal _province_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Warehouse where Warehouse_No = @Warehouse_No and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Warehouse_No", SqlDbType.VarChar, 20).Value = _province_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function isChckID(ByVal _Province_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Province where Province_Id = @Province_Id and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Province_Id", SqlDbType.VarChar, 15).Value = _province_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region
    '*** Normal DB Method
#Region "SEARCH"
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Sub getPopup_Search(ByVal pWhere As String)

        Dim strSQL As String = ""

        Try
            strSQL = "SELECT     *    FROM     ms_Warehouse where 1=1 "
            _dataTable = DBExeQuery(strSQL + pWhere)


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub SearchWarehouse(ByVal ColumnName As String, ByVal pFilterValue As String, ByVal Warehouse_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then

                strSQL = "SELECT     *    FROM     ms_Warehouse where Warehouse_Index ='" & Warehouse_Index & "'"
                strWhere = pFilterValue
            Else

            End If

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " select ms_Warehouse.*,ms_DistributionCenter.DistributionCenter_Id,ms_DistributionCenter.Description as [DistributionCenter_Desc] from ms_Warehouse left join ms_DistributionCenter on ms_Warehouse.DistributionCenter_Index=ms_DistributionCenter.DistributionCenter_Index where ms_Warehouse.status_id not in (-1) "
                'strSQL = " SELECT     *   "
                'strSQL &= " FROM   ms_Warehouse "
                'strSQL &= " WHERE  status_id != -1 "

                strWhere = pFilterValue
            Else
                ' Sql for define ColumnName & Filter Value 

            End If

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SelectData_For_Edit(ByVal pFilterValue As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Warehouse "

            strWhere = ""

            strWhere += " WHERE  ms_Warehouse.warehouse_Index = '" & pFilterValue & "' and status_id != -1"


            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub SelectSomeData()
        Dim strSQL As String = ""

        Try


            strSQL = "SELECT WAREHOUSE_NO AS WHNO,DESCRIPTION AS DESCR FROM ms_WAREHOUSE where status_id != -1"

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
    Public Function GetWarehouseNameByWareIndex(ByVal wareIndex As String) As String
        Dim strSQL As String = ""
        Dim warehouseName As String = ""
        Dim datarow As DataRow
        Try
            strSQL = "SELECT    Description "
            strSQL &= "FROM     ms_Warehouse "
            strSQL &= "WHERE    Warehouse_Index = '" & wareIndex & "' "
            strSQL &= "And      status_id != -1 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            For Each datarow In _dataTable.Rows
                warehouseName = datarow("Description")
            Next

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()

        End Try
        Return warehouseName
    End Function
#End Region
#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Private Function Insert_Master() As Boolean
        Dim strSQL As String

        Try
            If Trim(_warehouse_No) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _warehouse_No = objDocumentNumber.getSys_ID("Warehouse_No")
                _warehouse_No = _warehouse_No
                objDocumentNumber = Nothing

            End If


            strSQL = " insert into ms_Warehouse  (warehouse_Index,warehouse_No,Description,add_by,add_date,branch_id,DistributionCenter_Index) "
            strSQL &= " values (@warehouse_Index,@warehouse_No ,@Description,@add_by,getdate(),@add_branch,@DistributionCenter_Index)"


            strSQL = strSQL
            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@warehouse_Index", SqlDbType.VarChar, 13).Value = Me._warehouse_Index
                .Parameters.Add("@warehouse_No", SqlDbType.VarChar, 50).Value = Me._warehouse_No
                .Parameters.Add("@Description", SqlDbType.VarChar, 255).Value = Me._description
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = Me.DistributionCenter_Index
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
#End Region
#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Private Function Update_Master() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Warehouse set warehouse_No ='" + Me._warehouse_No + "' ,Description ='" + Me._description + "'"
            strSQL &= ",Update_by = '" + WV_UserName + "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",DistributionCenter_Index='" & Me.DistributionCenter_Index & "' "
            strSQL &= " WHERE warehouse_Index='" + Me._warehouse_Index + "' "


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
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Public Function Delete_Master(ByVal province_Index As String) As Boolean
        ' *** Define value from parameter
        Me._warehouse_Index = province_Index

        Dim strSQL As String

        Try
            'strSQL = " Delete  " & _
            '         " From ms_Province " & _
            '         " WHERE province_Index='" + Me._province_Index + "' "

            strSQL = "update ms_Warehouse set status_id = -1"
            strSQL &= ",Update_by = '" + WV_UserName + "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE Warehouse_Index='" + Me._warehouse_Index + "' "

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
#End Region
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "
    Public Function SaveData(ByVal _province_Index As String, ByVal _province_Id As String, ByVal _province As String, Optional ByVal DistributionCenter_Index As String = "") As Boolean



        ' ***  define value to field ***
        Me._warehouse_No = _province_Id
        Me._description = _province
        Me.DistributionCenter_Index = DistributionCenter_Index

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._warehouse_Index = objDBIndex.getSys_Value("Warehouse_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._warehouse_Index = _province_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_warehouse_No) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_Master = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        Exit Function
                    End If

                    'Case enuOperation_Type.DELETE
                    '    ' **** check value some table if need !! 
                    '    If Me.DeleteSize_Master() = True Then
                    '        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return True
                    '        Exit Function

                    '    Else
                    '        MessageBox.Show("ไม่สามารถลบ", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False
                    '        Exit Function
                    '    End If

            End Select


        Catch ex As Exception

            Throw ex
        End Try


    End Function

#End Region
End Class