Imports WMS_STD_Formula
Public Class ms_Branch : Inherits DBType_SQLServer

#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Branch_Id As Integer
    Private _Branch_Name As String
    Private _Seq As Integer
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _status_id As Integer
    Private _Branch_DB_Conncet As String

#End Region
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

#Region " Properties "
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
    Public Property Branch_Id() As Integer
        Get
            Return _Branch_Id
        End Get
        Set(ByVal Value As Integer)
            _Branch_Id = Value
        End Set
    End Property

    Public Property Branch_Name() As String
        Get
            Return _Branch_Name
        End Get
        Set(ByVal Value As String)
            _Branch_Name = Value
        End Set
    End Property

    Public Property Seq() As Integer
        Get
            Return _Seq
        End Get
        Set(ByVal Value As Integer)
            _Seq = Value
        End Set
    End Property

    Public Property add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property

    Public Property add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property

    Public Property add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property

    Public Property update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property

    Public Property update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property

    Public Property update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
        End Set
    End Property

    Public Property Branch_DB_Conncet() As String
        Get
            Return _Branch_DB_Conncet
        End Get
        Set(ByVal Value As String)
            _Branch_DB_Conncet = Value
        End Set
    End Property


#End Region


#Region " SELECT DATA "

    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " SELECT     *   " & _
                         " FROM       ms_Branch where status_id != -1 order by Seq asc"

                strWhere = ""
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
    Public Sub SearchBranch(ByVal ColumnName As String, ByVal pFilterValue As String, ByVal Branch_Id As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then

                strSQL = "SELECT * FROM  ms_Branch where Branch_Id ='" & Branch_Id & "'"
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
    Public Function GetServerKey() As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = "SELECT DB_Name() AS DB_Name ,@@SERVERNAME AS ServerName "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region " INSERT DATA "

#End Region

#Region " UPDATE DATA "

#End Region

#Region " DELETE DATA "

#End Region

End Class