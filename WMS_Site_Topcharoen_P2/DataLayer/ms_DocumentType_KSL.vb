'*** Create Date :  17/01/2008
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class ms_DocumentType_KSL : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _documentType_Index As String
    Private _documentType_Id As String
    Private _process_Id As Integer
    Private _description As String
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _sys_Key_Name As String
    Private _sys_Value As String

    Private _Format_Date As String = ""
    Private _Format_Running As String = ""
    Private _Format_Document As String = ""
    Private _Reset_Running_By As String = ""

    Private _ItemStatus_Index As String = ""
    Private _Location_Index As String = ""
    Private _Ref_No1 As String
    Private _Ref_No2 As String
    Private _Ref_No3 As String












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
    Public Property Ref_No3() As String
        Get
            Return _Ref_No3
        End Get
        Set(ByVal value As String)
            _Ref_No3 = value
        End Set
    End Property
    Public Property Ref_No2() As String
        Get
            Return _Ref_No2
        End Get
        Set(ByVal value As String)
            _Ref_No2 = value
        End Set
    End Property
    Public Property Ref_No1() As String
        Get
            Return _Ref_No1
        End Get
        Set(ByVal value As String)
            _Ref_No1 = value
        End Set
    End Property

    Public Property DocumentType_Index() As String
        Get
            Return _documentType_Index
        End Get
        Set(ByVal Value As String)
            _documentType_Index = Value
        End Set
    End Property
    Public Property DocumentType_Id() As String
        Get
            Return _documentType_Id
        End Get
        Set(ByVal Value As String)
            _documentType_Id = Value
        End Set
    End Property
    Public Property Process_Id() As Integer
        Get
            Return _process_Id
        End Get
        Set(ByVal Value As Integer)
            _process_Id = Value
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
    Public Property Format_Date() As String
        Get
            Return _Format_Date
        End Get
        Set(ByVal Value As String)
            _Format_Date = Value
        End Set
    End Property
    Public Property Format_Running() As String
        Get
            Return _Format_Running
        End Get
        Set(ByVal Value As String)
            _Format_Running = Value
        End Set
    End Property
    Public Property Format_Document() As String
        Get
            Return _Format_Document
        End Get
        Set(ByVal Value As String)
            _Format_Document = Value
        End Set
    End Property
    Public Property Reset_Running_By() As String
        Get
            Return _Reset_Running_By
        End Get
        Set(ByVal Value As String)
            _Reset_Running_By = Value
        End Set
    End Property
    Public Property Location_Index() As String
        Get
            Return _Location_Index
        End Get
        Set(ByVal Value As String)
            _Location_Index = Value
        End Set
    End Property
    Public Property ItemStatus_Index() As String
        Get
            Return _ItemStatus_Index
        End Get
        Set(ByVal Value As String)
            _ItemStatus_Index = Value
        End Set
    End Property
    Public Property Sys_Key_Name() As String
        Get
            Return _sys_Key_Name
        End Get
        Set(ByVal Value As String)
            _sys_Key_Name = Value
        End Set
    End Property
    Public Property Sys_Value() As String
        Get
            Return _sys_Value
        End Get
        Set(ByVal Value As String)
            _sys_Value = Value
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
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Sub SearchData_Click(ByVal pColumnName As String, ByVal pstrFillter As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If pColumnName = "" Then
                ' Select No Condition 
                strSQL = "SELECT     *, dbo.ms_Process.Process_Name AS Process_Name "
                strSQL &= "  FROM         dbo.ms_DocumentType LEFT OUTER JOIN "
                strSQL &= "               dbo.ms_Process ON dbo.ms_DocumentType.Process_Id = dbo.ms_Process.Process_Id "
                strSQL &= "  WHERE     (dbo.ms_DocumentType.status_id <> - 1)"

            Else
                ' Sql for define ColumnName & Filter Value 
                strSQL = "SELECT   " & pColumnName
                strSQL &= "  FROM         dbo.ms_DocumentType LEFT OUTER JOIN "
                strSQL &= "               dbo.ms_Process ON dbo.ms_DocumentType.Process_Id = dbo.ms_Process.Process_Id "
                strSQL &= "  WHERE     (dbo.ms_DocumentType.status_id <> - 1)"

            End If

            strWhere = pstrFillter


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
    Sub getReference_Order(ByVal Process_Id As Integer)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "   select *,ValueMember = CASE  isnull(Table_Name,'')"
            strSQL &= "                         WHEN '' Then Field_Name"
            strSQL &= "                         ELSE Table_Name+'.'+Field_Name End"
            strSQL &= "  FROM   config_CustomSearch "
            strSQL &= " WHERE   Process_Id =" & Process_Id
            strSQL &= " Order by seq "
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

    Sub getReference_Field_Name_Only(ByVal Process_Id As Integer)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "   select Field_Name as ValueMember,*"
            strSQL &= "  FROM   config_CustomSearch "
            strSQL &= " WHERE   Process_Id =" & Process_Id
            strSQL &= " Order by seq "
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

    Public Sub SelectData_For_Edit(ByVal pstrDocumentType_ID As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_DocumentType "

            strWhere = " WHERE  ms_DocumentType.documentType_Id = '" & pstrDocumentType_ID & "' and status_id != -1"

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

    Public Sub getDocumentType(ByVal value_of_process_id As Integer)

        ' *** define value  ***
        Me._process_Id = value_of_process_id

        Dim strSQL As String = ""
        Try


            'strSQL = "  SELECT  DocumentType_Index,  Description "
            strSQL = "  SELECT  DocumentType_Index,  Description,* "
            strSQL &= " FROM     ms_DocumentType "
            strSQL &= " WHERE Process_Id= " & Me._process_Id & " and status_id not in ( -1 )"


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


    Public Sub getDocumentTypeByIndex(ByVal value_of_DocumentType_Index As String)

        ' *** define value  ***
        'Me._process_Id = value_of_DocumentType_id

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT  DocumentType_Index,  Description "
            strSQL &= " FROM     ms_DocumentType "
            strSQL &= " where DocumentType_Index ='" & value_of_DocumentType_Index & "'"
            'strSQL &= " WHERE Process_Id= " & Me._process_Id & " and status_id not in ( -1 )"


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

    Public Sub getDocumentType_Location(ByVal psrtDocumentType_Index As String)

        ' *** define value  ***
        'Me._process_Id = value_of_DocumentType_id

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT  Location_Index "
            strSQL &= " FROM     ms_DocumentType "
            strSQL &= " where DocumentType_Index ='" & psrtDocumentType_Index & "'"
            'strSQL &= " WHERE Process_Id= " & Me._process_Id & " and status_id not in ( -1 )"


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

    Public Function isChckID(ByVal _DocumentType_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_DocumentType where DocumentType_Id = @DocumentType_Id and status_id <> -1   "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@DocumentType_Id", SqlDbType.VarChar, 15).Value = _DocumentType_Id

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

    Public Sub SearchDocumentType(ByVal DocumentType_Index As String)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try



            strSQL = "SELECT     *    "
            strSQL &= " FROM       ms_DocumentType "
            strSQL &= " WHERE DocumentType_Index ='" & DocumentType_Index & "'"



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
    Public Sub SearchDocumentTypeV4(ByVal DocumentType_Index As String)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'strSQL = "SELECT     *    "
            'strSQL &= " FROM       ms_DocumentType "

            strSQL = " select D.*, DL.Ref_DocumentType_Index,DL.Description as Ref_Description "
            strSQL &= " from ms_DocumentType D Left join ms_DocumentType DL on D.Ref_DocumentType_Index = DL.DocumentType_Index "
            strSQL &= " WHERE D.DocumentType_Index ='" & DocumentType_Index & "'"

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

    Public Sub GetDocTypeZoneConfig(ByVal ZoneIndex As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT DocumentType_Index,DocumentType_Id,Description "
            strSQL &= " FROM ms_DocumentType "
            strSQL &= " WHERE status_id != -1 "
            strSQL &= " AND Process_Id = '1' "

            strSQL &= " AND DocumentType_Index not in (select DocumentType_Index from tb_Zone_DocumentType WHERE Zone_Index = '" & ZoneIndex & "')"

            SetSQLString = strSQL & " Order by DocumentType_Id"

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub getProcess_Reference(ByVal Main_Process_Id As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM config_Process_Reference "
            strSQL &= " WHERE Main_Process_Id ='" & Main_Process_Id & "' "
            strSQL &= " AND IsUse = '1' "

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


    Public Sub getStatusAndLocationByDocumentType(ByVal pstrProcessID As String, ByVal pstrDocumentType_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  ms_ItemStatus.Description AS ItemStatusDes"
            strSQL &= "		, ms_DocumentType.DocumentType_Index"
            strSQL &= "		, ms_DocumentType.ItemStatus_Index"
            strSQL &= "		, ms_DocumentType.Process_Id "
            strSQL &= "		, ms_DocumentType.Location_Index "
            strSQL &= "		,ms_Location.Location_Alias"
            strSQL &= "  FROM  ms_DocumentType   LEFT JOIN  ms_Location  ON"
            strSQL &= "	       ms_DocumentType.Location_Index = ms_Location.Location_Index   LEFT JOIN ms_ItemStatus  ON"
            strSQL &= "	  ms_DocumentType.ItemStatus_Index = ms_ItemStatus.ItemStatus_Index"
            strSQL &= " WHERE  ms_DocumentType.Process_Id = '" & pstrProcessID & "'"
            strSQL &= " AND ms_DocumentType.DocumentType_Index ='" & pstrDocumentType_Index & "'"

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
    '*** Return : ??
    Private Function Insert_Master() As Boolean
        Dim strSQL As String

        Try
            If Trim(_documentType_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _documentType_Id = objDocumentNumber.getSys_ID("documentType_Id")

                objDocumentNumber = Nothing

            End If

            strSQL = " insert into ms_DocumentType(documentType_Index,documentType_Id,process_Id,Description,add_by,add_date,add_branch)"
            strSQL &= " values(@documentType_Index,@documentType_Id,@process_Id,@description,@add_by,getdate(),@add_branch)"


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@documentType_Index", SqlDbType.VarChar, 13).Value = Me._documentType_Index
                .Parameters.Add("@documentType_Id", SqlDbType.VarChar, 15).Value = Me._documentType_Id
                .Parameters.Add("@process_Id", SqlDbType.Int).Value = Me._process_Id
                .Parameters.Add("@description", SqlDbType.VarChar, 100).Value = Me._description
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
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

            strSQL = "update ms_DocumentType set documentType_Id='" + Me._documentType_Id + "' ,Description=@description"
            strSQL &= ",process_Id = " & _process_Id & ""
            strSQL &= ",Update_by = '" & WV_UserName & "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE documentType_Index='" + Me._documentType_Index + "' "


            strSQL = strSQL
            With SQLServerCommand
                .Parameters.Clear()

                .Parameters.Add("@description", SqlDbType.VarChar, 100).Value = Me._description

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
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Delete_Master(ByVal documentType_Index As String) As Boolean
        ' *** Define value from parameter
        Me._documentType_Index = documentType_Index

        Dim strSQL As String

        Try
            strSQL = "update ms_DocumentType set status_id = -1"
            strSQL &= " WHERE documentType_Index='" + Me._documentType_Index + "' "

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
    Public Function isExistID(ByVal _documentType_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_DocumentType where documentType_Id = @documentType_Id and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@documentType_Id", SqlDbType.VarChar, 20).Value = _documentType_Id

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
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

#Region " INSERT DATA V4"
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function Insert_MasterV4() As Boolean
        Dim strSQL As String

        Try
            If Trim(_documentType_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _documentType_Id = objDocumentNumber.getSys_ID("documentType_Id")

                objDocumentNumber = Nothing

            End If

            strSQL = " insert into ms_DocumentType(documentType_Index,documentType_Id,process_Id,Description,add_by,add_date,add_branch"
            strSQL &= " ,Format_Date,Format_Running,Format_Document,Reset_Running_By,Sys_Key_Name)"
            strSQL &= " values(@documentType_Index,@documentType_Id,@process_Id,@description,@add_by,getdate(),@add_branch"
            strSQL &= " ,@Format_Date,@Format_Running,@Format_Document,@Reset_Running_By,@Sys_Key_Name)"

            strSQL = strSQL
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@documentType_Index", SqlDbType.VarChar, 13).Value = Me._documentType_Index
                .Add("@documentType_Id", SqlDbType.VarChar, 15).Value = Me._documentType_Id
                .Add("@process_Id", SqlDbType.Int).Value = Me._process_Id
                .Add("@description", SqlDbType.VarChar, 100).Value = Me._description
                .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                '.Add("@Ref_DocumentType_Index", SqlDbType.VarChar, 13).Value = Me._Ref_DocumentType_Index
                .Add("@Format_Date", SqlDbType.NVarChar, 50).Value = _Format_Date
                .Add("@Format_Running", SqlDbType.NVarChar, 50).Value = _Format_Running
                .Add("@Format_Document", SqlDbType.NVarChar, 50).Value = _Format_Document
                .Add("@Reset_Running_By", SqlDbType.NVarChar, 50).Value = _Reset_Running_By
                .Add("@ItemStatus_Index", SqlDbType.NVarChar, 13).Value = _ItemStatus_Index
                .Add("@Location_Index", SqlDbType.NVarChar, 13).Value = _Location_Index
                .Add("@Sys_Key_Name", SqlDbType.NVarChar, 50).Value = _sys_Key_Name

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Dim objClassDB As New Sy_AutoNumber
            Dim objDT As DataTable = New DataTable


            Try
                If objClassDB.CheckSys_Value(_sys_Key_Name) Then
                    strSQL = ""
                    strSQL = " INSERT INTO Sy_AutoNumber(Sys_Key,Sys_Value,Branch_ID)"
                    strSQL &= " VALUES(@Sys_Key,@Sys_Value,0)"
                    strSQL = strSQL
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@Sys_Key", SqlDbType.VarChar, 50).Value = _sys_Key_Name
                        .Add("@Sys_Value", SqlDbType.VarChar, 13).Value = _sys_Value
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                End If
            Catch ex As Exception
                Throw ex
            End Try


            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
    Private Function Insert_MasterV5() As Boolean
        Dim strSQL As String

        Try
            If Trim(_documentType_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _documentType_Id = objDocumentNumber.getSys_ID("documentType_Id")

                objDocumentNumber = Nothing

            End If

            strSQL = " insert into ms_DocumentType(documentType_Index,documentType_Id,process_Id,Description,add_by,add_date,add_branch"
            strSQL &= " ,Format_Date,Format_Running,Format_Document,Reset_Running_By,Sys_Key_Name,Ref_No1,Ref_No2,Ref_No3)"
            strSQL &= " values(@documentType_Index,@documentType_Id,@process_Id,@description,@add_by,getdate(),@add_branch"
            strSQL &= " ,@Format_Date,@Format_Running,@Format_Document,@Reset_Running_By,@Sys_Key_Name,@Ref_No1,@Ref_No2,@Ref_No3)"

            strSQL = strSQL
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@documentType_Index", SqlDbType.VarChar, 13).Value = Me._documentType_Index
                .Add("@documentType_Id", SqlDbType.VarChar, 15).Value = Me._documentType_Id
                .Add("@process_Id", SqlDbType.Int).Value = Me._process_Id
                .Add("@description", SqlDbType.VarChar, 100).Value = Me._description
                .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Add("@Format_Date", SqlDbType.NVarChar, 50).Value = _Format_Date
                .Add("@Format_Running", SqlDbType.NVarChar, 50).Value = _Format_Running
                .Add("@Format_Document", SqlDbType.NVarChar, 50).Value = _Format_Document
                .Add("@Reset_Running_By", SqlDbType.NVarChar, 50).Value = _Reset_Running_By
                .Add("@ItemStatus_Index", SqlDbType.NVarChar, 13).Value = _ItemStatus_Index
                .Add("@Location_Index", SqlDbType.NVarChar, 13).Value = _Location_Index
                .Add("@Sys_Key_Name", SqlDbType.NVarChar, 50).Value = _sys_Key_Name
                .Add("@Ref_No1", SqlDbType.NVarChar, 250).Value = _Ref_No1
                .Add("@Ref_No2", SqlDbType.NVarChar, 250).Value = _Ref_No2
                .Add("@Ref_No3", SqlDbType.NVarChar, 250).Value = _Ref_No3

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Dim objClassDB As New Sy_AutoNumber
            Dim objDT As DataTable = New DataTable


            Try
                If objClassDB.CheckSys_Value(_sys_Key_Name) Then
                    strSQL = ""
                    strSQL = " INSERT INTO Sy_AutoNumber(Sys_Key,Sys_Value,Branch_ID)"
                    strSQL &= " VALUES(@Sys_Key,@Sys_Value,0)"
                    strSQL = strSQL
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@Sys_Key", SqlDbType.VarChar, 50).Value = _sys_Key_Name
                        .Add("@Sys_Value", SqlDbType.VarChar, 13).Value = _sys_Value
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                End If
            Catch ex As Exception
                Throw ex
            End Try


            Return True

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
#End Region
#Region " UPDATE DATA V4"
    Private Function Update_MasterV4() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_DocumentType set documentType_Id=@documentType_Id ,Description=@description"
            strSQL &= ",process_Id =@process_Id"
            strSQL &= ",Update_by = @Update_by"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= @update_branch"
            strSQL &= ",Format_Date=@Format_Date,Format_Running=@Format_Running,Format_Document=@Format_Document,Reset_Running_By=@Reset_Running_By "
            strSQL &= ",ItemStatus_Index=@ItemStatus_Index,Location_Index=@Location_Index"
            strSQL &= " WHERE documentType_Index=@documentType_Index"
            strSQL = strSQL
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@documentType_Index", SqlDbType.VarChar, 100).Value = Me._documentType_Index
                .Add("@description", SqlDbType.VarChar, 100).Value = Me._description
                .Add("@documentType_Id", SqlDbType.VarChar, 100).Value = Me._documentType_Id
                '.Add("@Ref_DocumentType_Index", SqlDbType.VarChar, 100).Value = Me._Ref_DocumentType_Index
                .Add("@process_Id", SqlDbType.Int, 4).Value = Me._process_Id
                .Add("@Update_by", SqlDbType.VarChar, 100).Value = WV_UserName
                .Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Add("@Format_Date", SqlDbType.NVarChar, 100).Value = _Format_Date
                .Add("@Format_Running", SqlDbType.NVarChar, 100).Value = _Format_Running
                .Add("@Format_Document", SqlDbType.NVarChar, 100).Value = _Format_Document
                .Add("@Reset_Running_By", SqlDbType.NVarChar, 100).Value = _Reset_Running_By
                .Add("@ItemStatus_Index", SqlDbType.NVarChar, 100).Value = _ItemStatus_Index
                .Add("@Location_Index", SqlDbType.NVarChar, 100).Value = _Location_Index

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
    Private Function Update_MasterV5() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_DocumentType set documentType_Id=@documentType_Id ,Description=@description"
            strSQL &= ",process_Id =@process_Id"
            strSQL &= ",Update_by = @Update_by"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= @update_branch"
            strSQL &= ",Format_Date=@Format_Date,Format_Running=@Format_Running,Format_Document=@Format_Document,Reset_Running_By=@Reset_Running_By "
            strSQL &= ",ItemStatus_Index=@ItemStatus_Index,Location_Index=@Location_Index"
            strSQL &= ",Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Ref_No3=@Ref_No3,Sys_Key_Name=@Sys_Key_Name"
            strSQL &= " WHERE documentType_Index=@documentType_Index"
            strSQL = strSQL
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@documentType_Index", SqlDbType.VarChar, 100).Value = Me._documentType_Index
                .Add("@description", SqlDbType.VarChar, 100).Value = Me._description
                .Add("@documentType_Id", SqlDbType.VarChar, 100).Value = Me._documentType_Id
                '.Add("@Ref_DocumentType_Index", SqlDbType.VarChar, 100).Value = Me._Ref_DocumentType_Index
                .Add("@process_Id", SqlDbType.Int, 4).Value = Me._process_Id
                .Add("@Update_by", SqlDbType.VarChar, 100).Value = WV_UserName
                .Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Add("@Format_Date", SqlDbType.NVarChar, 100).Value = _Format_Date
                .Add("@Format_Running", SqlDbType.NVarChar, 100).Value = _Format_Running
                .Add("@Format_Document", SqlDbType.NVarChar, 100).Value = _Format_Document
                .Add("@Reset_Running_By", SqlDbType.NVarChar, 100).Value = _Reset_Running_By
                .Add("@ItemStatus_Index", SqlDbType.NVarChar, 100).Value = _ItemStatus_Index
                .Add("@Location_Index", SqlDbType.NVarChar, 100).Value = _Location_Index
                .Add("@Ref_No1", SqlDbType.NVarChar, 250).Value = _Ref_No1
                .Add("@Ref_No2", SqlDbType.NVarChar, 250).Value = _Ref_No2
                .Add("@Ref_No3", SqlDbType.NVarChar, 250).Value = _Ref_No3
                .Add("@Sys_Key_Name", SqlDbType.NVarChar, 50).Value = _sys_Key_Name

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


    '*** Transaction DB Method
#Region " TRANSACTION "
    Public Function SaveData(ByVal _documentType_Index As String, ByVal _documentType_Id As String, ByVal _process_Id As Integer, ByVal _description As String) As Boolean



        ' ***  define value to field ***
        Me._documentType_Id = _documentType_Id
        Me._process_Id = _process_Id
        Me._description = _description

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._documentType_Index = objDBIndex.getSys_Value("DocumentType_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._documentType_Index = _documentType_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_documentType_Id) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        '  Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_Master = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            '  Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            '   Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        '  Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        '  Exit Function
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

    Public Function SaveDataV4() As Boolean



        '' ***  define value to field ***
        'Me._documentType_Id = _documentType_Id
        'Me._process_Id = _process_Id
        'Me._description = _description
        'Me._Ref_DocumentType_Index = _Ref_DocumentType_Index

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._documentType_Index = objDBIndex.getSys_Value("DocumentType_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._documentType_Index = _documentType_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_documentType_Id) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        '  Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_MasterV4 = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            '  Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            '   Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_MasterV4 = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        '  Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        '  Exit Function
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

    Public Function SaveDataV5() As Boolean



        '' ***  define value to field ***
        'Me._documentType_Id = _documentType_Id
        'Me._process_Id = _process_Id
        'Me._description = _description
        'Me._Ref_DocumentType_Index = _Ref_DocumentType_Index

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._documentType_Index = objDBIndex.getSys_Value("DocumentType_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._documentType_Index = _documentType_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_documentType_Id) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        '  Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_MasterV5 = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            '  Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            '   Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_MasterV5 = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        '  Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        '  Exit Function
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