Imports WMS_STD_Formula
Public Class se_User : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _user_index As String
    Private _user_id As String
    Private _group_index As String
    Private _userName As String
    Private _userPasswd As String
    Private _userFullName As String
    Private _userDepartMent As String
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _status_id As Integer

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
    Public Property User_index() As String
        Get
            Return _user_index
        End Get
        Set(ByVal Value As String)
            _user_index = Value
        End Set
    End Property
    Public Property User_id() As String
        Get
            Return _user_id
        End Get
        Set(ByVal Value As String)
            _user_id = Value
        End Set
    End Property
    Public Property Group_index() As String
        Get
            Return _group_index
        End Get
        Set(ByVal Value As String)
            _group_index = Value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal Value As String)
            _userName = Value
        End Set
    End Property
    Public Property UserPasswd() As String
        Get
            Return _userPasswd
        End Get
        Set(ByVal Value As String)
            _userPasswd = Value
        End Set
    End Property
    Public Property UserFullName() As String
        Get
            Return _userFullName
        End Get
        Set(ByVal Value As String)
            _userFullName = Value
        End Set
    End Property
    Public Property UserDepartMent() As String
        Get
            Return _userDepartMent
        End Get
        Set(ByVal Value As String)
            _userDepartMent = Value
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
    Public Property Status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
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


    '*** Normal DB Method
#Region " SELECT DATA "
    Public Sub SelectAll()
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM se_user"

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

    Public Sub SelectByIndex(ByVal userIndex As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM se_user"
            strSQL &= " where user_index='" & userIndex & "'"

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

    ''' <summary>
    ''' create by ohm this function for search by user name
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <remarks></remarks>
    Public Sub SearchByUserName(ByVal userName As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM se_user"
            strSQL &= " where user_id ='" & userName & "'"

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

    ''' <summary>
    ''' create by ohm this function for Search By User FullName
    ''' </summary>
    ''' <param name="userFullName"></param>
    ''' <remarks></remarks>
    Public Sub SearchByUserFullName(ByVal userFullName As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM se_user"
            strSQL &= " where userFullName='" & userFullName & "'"

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

    Public Sub CheckUser(ByVal username As String, ByVal passwd As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM se_user"
            strSQL &= " where userName = '" & username & "' and userPasswd ='" & passwd & "'"
            strSQL &= " and status_id <> -1"

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
#End Region
#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    Private Function Insert_Master() As Boolean
        Dim strSQL As String

        Try

            strSQL = " insert into se_user (user_index,user_id,group_index,userName,userPasswd,userFullName,Department_Index,add_by,add_branch) "
            strSQL &= " values (@user_index,@user_id ,@group_index,@userName,@userPasswd,@userFullName,@Department_Index,@add_by,@add_branch)"

            'strSQL = " insert into se_user (user_index,user_id,group_index,userName,userPasswd,userFullName,userDepartMent,add_by,add_branch) "
            'strSQL &= " values ('" & _user_index & "','" & _user_id & "' ,'" & _group_index & "','" & _userName & "','" & _userPasswd & "','" & _userFullName & "','" & _userDepartMent & "','" & Me.Add_by & "'," & Me.Add_branch & ")"


            strSQL = strSQL
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@user_index", SqlDbType.VarChar, 50).Value = Me._user_index
                .Parameters.Add("@user_id", SqlDbType.VarChar, 50).Value = Me._user_id
                .Parameters.Add("@group_index", SqlDbType.VarChar, 50).Value = Me._group_index
                .Parameters.Add("@userName", SqlDbType.VarChar, 50).Value = Me._userName
                .Parameters.Add("@userPasswd", SqlDbType.VarChar, 50).Value = Me._userPasswd
                .Parameters.Add("@userFullName", SqlDbType.VarChar, 50).Value = Me._userFullName
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 50).Value = Me._userDepartMent
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = Me.Add_by
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = Me.Add_branch
            End With

            'SetSQLString = strSQL
            'SetCommandType = WMS_INH_Main.DBType_SQLServer.enuCommandType.Text()
            'SetEXEC_TYPE = WMS_DataLayer.DBType_SQLServer.EXEC.NonQuery
            'connectDB()
            'EXEC_Command()
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
    Private Function Update_User() As Boolean
        Dim strSQL As String

        Try
            strSQL = " update se_user set "
            strSQL &= " group_index = @group_index"
            strSQL &= " ,user_id = @user_id"
            strSQL &= " ,userName = @userName"
            strSQL &= " ,userPasswd = @userPasswd"
            strSQL &= " ,userFullName = @userFullName"
            strSQL &= " ,Department_Index = @Department_Index"
            strSQL &= " where user_index = @user_index"


            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@user_index", SqlDbType.VarChar, 50).Value = Me._user_index
                .Parameters.Add("@user_id", SqlDbType.VarChar, 50).Value = Me._user_id
                .Parameters.Add("@userName", SqlDbType.VarChar, 50).Value = Me._userName
                .Parameters.Add("@group_index", SqlDbType.VarChar, 50).Value = Me._group_index
                .Parameters.Add("@userPasswd", SqlDbType.VarChar, 50).Value = Me._userPasswd
                .Parameters.Add("@userFullName", SqlDbType.VarChar, 50).Value = Me._userFullName
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 50).Value = Me._userDepartMent
            End With


            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            '_dataTable = GetDataTable

            'SetSQLString = strSQL
            'SetCommandType = WMS_DataLayer.DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = WMS_DataLayer.DBType_SQLServer.EXEC.NonQuery
            'connectDB()
            'EXEC_Command()
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    Public Sub DeleteByIndex(ByVal UserIndex As String)
        Dim strSQL As String

        Try

            strSQL = " delete "
            strSQL &= " FROM se_user"
            strSQL &= " where user_index = '" & UserIndex & "'"

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

    '*** Return : ??
#End Region
#Region " CHECK DATA "
    Private Function isExistID(ByVal _color_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from se_user where user_id = @user_id  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@user_id", SqlDbType.VarChar, 50).Value = _user_id

            'SetSQLString = strSQL
            'SetCommandType = WMS_DataLayer.DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = WMS_DataLayer.DBType_SQLServer.EXEC.Scalar
            'connectDB()
            'EXEC_Command()
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
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
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "
    Public Function SaveData(ByVal _user_Index As String, ByVal _user_Id As String, ByVal _group_index As String, ByVal _userName As String, ByVal _userPasswd As String, ByVal _userFullName As String, ByVal _userDepartMent As String) As Boolean



        ' ***  define value to field ***
        Me._user_id = _user_Id
        Me._group_index = _group_index
        Me._userName = _userName
        Me._userPasswd = _userPasswd
        Me._userFullName = _userFullName
        Me._userDepartMent = _userDepartMent

        ' *******************************
        Try
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber 'WMS_SM_DataLayer.Sy_AutoNumber
                    Me._user_index = objDBIndex.getSys_Value("user_index")
                    objDBIndex = Nothing

                Case enuOperation_Type.UPDATE
                    Me._user_index = _user_Index
            End Select


            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_user_Id) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    Else
                        ' Can Save data
                        If Me.Insert_Master = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_User = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        Exit Function
                    End If

                Case enuOperation_Type.DELETE
                    DeleteByIndex(_user_Index)
                    '    ' **** check value some table if need !! 
                    '    If Me.Deletecolor_Master() = True Then
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