Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Public Class tb_TransferOwner : Inherits DBType_SQLServer

#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _TransferOwner_Index As String = ""
    Private _TransferOwner_No As String = ""
    Private _Customer_Index As String = ""
    Private _AssetLocationBalance_Index As String = ""
    Private _DocumentType_Index As String = ""
    Private _TransferOwner_Date As Date
    Private _Times As String = ""
    Private _Ref_No1 As String = ""
    Private _Ref_No2 As String = ""
    Private _Comment As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Flo1 As Double
    Private _Flo2 As Double
    Private _Flo3 As Double
    Private _Flo4 As Double
    Private _Flo5 As Double
    Private _Status As Integer
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer
    Private _New_Customer_Index As String = ""

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
    Public Property TransferOwner_Index() As String
        Get
            Return _TransferOwner_Index
        End Get
        Set(ByVal Value As String)
            _TransferOwner_Index = Value
        End Set
    End Property
    Public Property New_Customer_Index() As String
        Get
            Return _New_Customer_Index
        End Get
        Set(ByVal Value As String)
            _New_Customer_Index = Value
        End Set
    End Property
    Public Property TransferOwner_No() As String
        Get
            Return _TransferOwner_No
        End Get
        Set(ByVal Value As String)
            _TransferOwner_No = Value
        End Set
    End Property
    Public Property AssetLocationBalance_Index() As String
        Get
            Return _AssetLocationBalance_Index
        End Get
        Set(ByVal Value As String)
            _AssetLocationBalance_Index = Value
        End Set
    End Property


    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property

    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal Value As String)
            _DocumentType_Index = Value
        End Set
    End Property

    Public Property TransferOwner_Date() As Date
        Get
            Return _TransferOwner_Date
        End Get
        Set(ByVal Value As Date)
            _TransferOwner_Date = Value
        End Set
    End Property

    Public Property Times() As String
        Get
            Return _Times
        End Get
        Set(ByVal Value As String)
            _Times = Value
        End Set
    End Property

    Public Property Ref_No1() As String
        Get
            Return _Ref_No1
        End Get
        Set(ByVal Value As String)
            _Ref_No1 = Value
        End Set
    End Property

    Public Property Ref_No2() As String
        Get
            Return _Ref_No2
        End Get
        Set(ByVal Value As String)
            _Ref_No2 = Value
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return _Comment
        End Get
        Set(ByVal Value As String)
            _Comment = Value
        End Set
    End Property

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

    Public Property Flo1() As Double
        Get
            Return _Flo1
        End Get
        Set(ByVal Value As Double)
            _Flo1 = Value
        End Set
    End Property

    Public Property Flo2() As Double
        Get
            Return _Flo2
        End Get
        Set(ByVal Value As Double)
            _Flo2 = Value
        End Set
    End Property

    Public Property Flo3() As Double
        Get
            Return _Flo3
        End Get
        Set(ByVal Value As Double)
            _Flo3 = Value
        End Set
    End Property

    Public Property Flo4() As Double
        Get
            Return _Flo4
        End Get
        Set(ByVal Value As Double)
            _Flo4 = Value
        End Set
    End Property

    Public Property Flo5() As Double
        Get
            Return _Flo5
        End Get
        Set(ByVal Value As Double)
            _Flo5 = Value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal Value As Integer)
            _Status = Value
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


#End Region

#Region " SELECT DATA "

#End Region

#Region " INSERT DATA "
    Public Function InsertTempHesder() As String
        Try
            Dim strSQL As String = ""
            strSQL = " INSERT INTO tb_TransferOwner ( TransferOwner_Index,TransferOwner_No,TransferOwner_Date,DocumentType_Index,Customer_Index, " & _
                     "                                New_Customer_Index,Times,add_by,add_date,add_branch,Status,User_UseDoc ) Values (@TransferOwner_Index,@TransferOwner_No,@TransferOwner_Date,@DocumentType_Index,@Customer_Index,@New_Customer_Index,@Times,@add_by,Getdate(),@add_branch,-2,1) "
            With SQLServerCommand
                .Parameters.Clear()
                Dim objDBIndex As New Sy_AutoNumber
                Me._TransferOwner_Index = objDBIndex.getSys_Value("TransferOwner_Index")
                objDBIndex = Nothing
                .Parameters.Add("@TransferOwner_Index", SqlDbType.VarChar, 13).Value = Me._TransferOwner_Index
                .Parameters.Add("@TransferOwner_No", SqlDbType.VarChar, 50).Value = Me._TransferOwner_Index
                .Parameters.Add("@TransferOwner_Date", SqlDbType.SmallDateTime, 4).Value = Me._TransferOwner_Date
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = Me._DocumentType_Index
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Index
                .Parameters.Add("@New_Customer_Index", SqlDbType.VarChar, 13).Value = Me._New_Customer_Index
                .Parameters.Add("@Times", SqlDbType.NVarChar, 50).Value = Me._Times
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, eCommandType.Text)
            Return Me._TransferOwner_Index
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region " UPDATE DATA "

#End Region

#Region " DELETE DATA "

#End Region

#Region " CONFIG FIELD "
    Public Function getFieldConfig() As DataTable
        Dim strSQL As String
        Try

            strSQL = " SELECT *   " & _
                     " FROM   config_TransferCodeLocation " & _
                     " WHERE IsUse=0 "


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

End Class