Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient

Public Class tb_TAG_Update : Inherits DBType_SQLServer
    'Private _objItemCollection As List(Of tb_TAG)
    'Private _objItem As New tb_TAG(enuOperation_Type.ADDNEW)

    Private _objItemCollection As List(Of tb_TAG_Update)

    Public Property objItemCollection() As List(Of tb_TAG_Update)
        Get
            Return _objItemCollection
        End Get
        Set(ByVal value As List(Of tb_TAG_Update))
            _objItemCollection = value
        End Set
    End Property
    'Private _objItem As New tb_TAG(enuOperation_Type.ADDNEW)

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _TAG_No As String = ""
    Private _LinkOrderFlag As Boolean
    Private _Order_No As String = ""
    Private _Order_Index As String = ""
    Private _Order_Date As Date
    Private _Order_Time As String = ""
    Private _OrderItem_Index As String = ""
    Private _OrderItemLocation_Index As String = ""
    Private _Customer_Index As String = ""
    Private _Supplier_Index As String = ""
    Private _Sku_Index As String = ""
    Private _PLot As String = ""
    Private _ItemStatus_Index As String = ""
    Private _Package_Index As String = ""
    Private _Unit_Weight As Decimal
    Private _Size_Index As String = ""
    Private _Pallet_No As String = ""
    Private _Qty As Decimal
    Private _Weight As Decimal
    Private _Volume As Decimal
    Private _Qty_per_TAG As Decimal
    Private _Weight_per_TAG As Decimal
    Private _Volume_per_TAG As Decimal
    Private _TAG_Status As Integer
    Private _Ref_No1 As String = ""
    Private _Ref_No2 As String = ""
    Private _Ref_No3 As String = ""
    Private _Ref_No4 As String = ""
    Private _Ref_No5 As String = ""
    Private _tag_id As String = ""
    Private _description As String = ""
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer
    Private _IsTemporaryTAG As Boolean
    Private _GuID As String = ""

    Private _TAG_Index As String
    Public Property TAG_Index() As String
        Get
            Return _TAG_Index
        End Get
        Set(ByVal value As String)
            _TAG_Index = value
        End Set
    End Property


    Private _ERP_Location As String = ""
    Public Property ERP_Location() As String
        Get
            Return _ERP_Location
        End Get
        Set(ByVal value As String)
            _ERP_Location = value
        End Set
    End Property



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
    Public Property TAG_No() As String
        Get
            Return _TAG_No
        End Get
        Set(ByVal Value As String)
            _TAG_No = Value
        End Set
    End Property

    Public Property LinkOrderFlag() As Boolean
        Get
            Return _LinkOrderFlag
        End Get
        Set(ByVal Value As Boolean)
            _LinkOrderFlag = Value
        End Set
    End Property

    Public Property Order_No() As String
        Get
            Return _Order_No
        End Get
        Set(ByVal Value As String)
            _Order_No = Value
        End Set
    End Property

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal Value As String)
            _Order_Index = Value
        End Set
    End Property

    Public Property Order_Date() As Date
        Get
            Return _Order_Date
        End Get
        Set(ByVal Value As Date)
            _Order_Date = Value
        End Set
    End Property

    Public Property Order_Time() As String
        Get
            Return _Order_Time
        End Get
        Set(ByVal Value As String)
            _Order_Time = Value
        End Set
    End Property

    Public Property OrderItem_Index() As String
        Get
            Return _OrderItem_Index
        End Get
        Set(ByVal Value As String)
            _OrderItem_Index = Value
        End Set
    End Property

    Public Property OrderItemLocation_Index() As String
        Get
            Return _OrderItemLocation_Index
        End Get
        Set(ByVal Value As String)
            _OrderItemLocation_Index = Value
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

    Public Property Supplier_Index() As String
        Get
            Return _Supplier_Index
        End Get
        Set(ByVal Value As String)
            _Supplier_Index = Value
        End Set
    End Property

    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal Value As String)
            _Sku_Index = Value
        End Set
    End Property

    Public Property PLot() As String
        Get
            Return _PLot
        End Get
        Set(ByVal Value As String)
            _PLot = Value
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

    Public Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal Value As String)
            _Package_Index = Value
        End Set
    End Property

    Public Property Unit_Weight() As Decimal
        Get
            Return _Unit_Weight
        End Get
        Set(ByVal Value As Decimal)
            _Unit_Weight = Value
        End Set
    End Property

    Public Property Size_Index() As String
        Get
            Return _Size_Index
        End Get
        Set(ByVal Value As String)
            _Size_Index = Value
        End Set
    End Property

    Public Property Pallet_No() As String
        Get
            Return _Pallet_No
        End Get
        Set(ByVal Value As String)
            _Pallet_No = Value
        End Set
    End Property

    Public Property Qty() As Decimal
        Get
            Return _Qty
        End Get
        Set(ByVal Value As Decimal)
            _Qty = Value
        End Set
    End Property

    Public Property Weight() As Decimal
        Get
            Return _Weight
        End Get
        Set(ByVal Value As Decimal)
            _Weight = Value
        End Set
    End Property

    Public Property Volume() As Decimal
        Get
            Return _Volume
        End Get
        Set(ByVal Value As Decimal)
            _Volume = Value
        End Set
    End Property

    Public Property Qty_per_TAG() As Decimal
        Get
            Return _Qty_per_TAG
        End Get
        Set(ByVal Value As Decimal)
            _Qty_per_TAG = Value
        End Set
    End Property

    Public Property Weight_per_TAG() As Decimal
        Get
            Return _Weight_per_TAG
        End Get
        Set(ByVal Value As Decimal)
            _Weight_per_TAG = Value
        End Set
    End Property

    Public Property Volume_per_TAG() As Decimal
        Get
            Return _Volume_per_TAG
        End Get
        Set(ByVal Value As Decimal)
            _Volume_per_TAG = Value
        End Set
    End Property

    Public Property TAG_Status() As Integer
        Get
            Return _TAG_Status
        End Get
        Set(ByVal Value As Integer)
            _TAG_Status = Value
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

    Public Property Ref_No3() As String
        Get
            Return _Ref_No3
        End Get
        Set(ByVal Value As String)
            _Ref_No3 = Value
        End Set
    End Property

    Public Property Ref_No4() As String
        Get
            Return _Ref_No4
        End Get
        Set(ByVal Value As String)
            _Ref_No4 = Value
        End Set
    End Property

    Public Property Ref_No5() As String
        Get
            Return _Ref_No5
        End Get
        Set(ByVal Value As String)
            _Ref_No5 = Value
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

    Public Property IsTemporaryTAG() As Boolean
        Get
            Return _IsTemporaryTAG
        End Get
        Set(ByVal Value As Boolean)
            _IsTemporaryTAG = Value
        End Set
    End Property

    Public Property GuID() As String
        Get
            Return _GuID
        End Get
        Set(ByVal Value As String)
            _GuID = Value
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

    '*** Transaction DB Method
#Region " TRANSACTION "
    Public Function InsertData(Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim strSQL As String = ""
        Dim _objItem As New tb_TAG_Update(enuOperation_Type.ADDNEW)
        'connectDB()
        'Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        'SQLServerCommand.Transaction = myTrans



        Dim IsNotPassTransaction As Boolean = False
        Try
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            For Each _objItem In _objItemCollection

                strSQL = " INSERT INTO tb_Tag(TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,add_by,add_branch,ERP_Location,IsTemporaryTAG,GuID)" & _
                "    VALUES(@TAG_Index,@TAG_No,@LinkOrderFlag,@Order_No,@Order_Index,@Order_Date,@Order_Time,@OrderItem_Index,@OrderItemLocation_Index,@Customer_Index,@Supplier_Index,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@Ref_No1,@Ref_No2,@Ref_No3,@add_by,@add_branch,@ERP_Location,@IsTemporaryTAG,@GuID)"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 13).Value = _objItem._TAG_Index
                    .Parameters.Add("@TAG_No", SqlDbType.VarChar, 50).Value = _objItem._TAG_No
                    .Parameters.Add("@LinkOrderFlag", SqlDbType.Bit).Value = _objItem._LinkOrderFlag
                    .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = _objItem._Order_No
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem._Order_Index
                    .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime).Value = _objItem._Order_Date
                    .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = _objItem._Order_Time
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem._OrderItem_Index 'bug
                    .Parameters.Add("@OrderItemLocation_Index", SqlDbType.VarChar, 13).Value = _objItem._OrderItemLocation_Index 'bug
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objItem._Customer_Index
                    .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objItem._Supplier_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.Char, 13).Value = _objItem._Sku_Index
                    .Parameters.Add("@PLot", SqlDbType.NVarChar, 50).Value = _objItem._PLot
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = _objItem._ItemStatus_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objItem._Package_Index
                    .Parameters.Add("@Unit_Weight", SqlDbType.Float).Value = _objItem._Unit_Weight
                    .Parameters.Add("@Size_Index", SqlDbType.VarChar, 13).Value = _objItem._Size_Index
                    .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 50).Value = _objItem._Pallet_No
                    .Parameters.Add("@Qty", SqlDbType.Float).Value = _objItem._Qty
                    .Parameters.Add("@Weight", SqlDbType.Float).Value = _objItem._Weight
                    .Parameters.Add("@Volume", SqlDbType.Float).Value = _objItem._Volume
                    .Parameters.Add("@Qty_per_TAG", SqlDbType.Float).Value = _objItem._Qty_per_TAG
                    .Parameters.Add("@Weight_per_TAG", SqlDbType.Float).Value = _objItem._Weight_per_TAG
                    .Parameters.Add("@Volume_per_TAG", SqlDbType.Float).Value = _objItem._Volume_per_TAG
                    .Parameters.Add("@TAG_Status", SqlDbType.Int).Value = 1 '_objItem._TAG_Status
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objItem._Ref_No1
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objItem._Ref_No2
                    .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objItem._Ref_No3
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = _objItem.ERP_Location
                    .Parameters.Add("@IsTemporaryTAG", SqlDbType.Bit).Value = _objItem.IsTemporaryTAG
                    .Parameters.Add("@GuID", SqlDbType.VarChar, 36).Value = _objItem.GuID
                End With

                'SetSQLString = strSQL
                'SetCommandType = DBType_SQLServer.enuCommandType.Text
                'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                'EXEC_Command()
                DBExeNonQuery(strSQL, _Connection, _myTrans)
            Next

            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If

            Return _objItem._TAG_No

        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If
            Throw ex
        Finally
            If IsNotPassTransaction Then
                SQLServerCommand.Connection.Close()
            End If
        End Try
    End Function

    'Public Sub InsertData_Only_SKU()
    '    Dim strSQL As String = ""
    '    'Dim _objItem As New tb_TAG(enuOperation_Type.ADDNEW)
    '    connectDB()
    '    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '    SQLServerCommand.Transaction = myTrans
    '    Try
    '        Dim i As Integer
    '        For i = 0 To _objItemCollection.Count - 1
    '            strSQL = " INSERT INTO tb_Tag(TAG_No,LinkOrderFlag,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,add_by,add_branch)" & _
    '                            "      VALUES(@TAG_No,@LinkOrderFlag,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@add_by,@add_branch)"

    '            With SQLServerCommand.Parameters
    '                .Clear()
    '                .Add(New SqlParameter("@TAG_Index", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._TAG_Index
    '                .Add(New SqlParameter("@TAG_No", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._TAG_No
    '                .Add(New SqlParameter("@LinkOrderFlag", SqlDbType.Bit, 1)).Value = _objItemCollection(i)._LinkOrderFlag
    '                .Add(New SqlParameter("@Sku_Index", SqlDbType.Char, 13)).Value = _objItemCollection(i)._Sku_Index
    '                .Add(New SqlParameter("@PLot", SqlDbType.NVarChar, 50)).Value = _objItemCollection(i)._PLot
    '                .Add(New SqlParameter("@ItemStatus_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._ItemStatus_Index
    '                .Add(New SqlParameter("@Package_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._Package_Index '= "001"
    '                .Add(New SqlParameter("@Unit_Weight", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Unit_Weight
    '                .Add(New SqlParameter("@Size_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._Size_Index
    '                .Add(New SqlParameter("@Pallet_No", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._Pallet_No
    '                .Add(New SqlParameter("@Qty", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Qty
    '                .Add(New SqlParameter("@Weight", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Weight
    '                .Add(New SqlParameter("@Volume", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Volume
    '                .Add(New SqlParameter("@Qty_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Qty_per_TAG
    '                .Add(New SqlParameter("@Weight_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Weight_per_TAG
    '                .Add(New SqlParameter("@Volume_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Volume_per_TAG
    '                .Add(New SqlParameter("@TAG_Status", SqlDbType.Int, 10)).Value = _objItemCollection(i)._TAG_Status
    '                .Add(New SqlParameter("@add_by", SqlDbType.VarChar, 50)).Value = WV_UserName
    '                .Add(New SqlParameter("@add_branch", SqlDbType.Int, 10)).Value = WV_Branch_ID
    '            End With
    '            SetSQLString = strSQL
    '            SetCommandType = enuCommandType.Text
    '            SetEXEC_TYPE = EXEC.NonQuery
    '            connectDB()
    '            EXEC_Command()
    '        Next
    '        myTrans.Commit()
    '    Catch ex As Exception
    '        myTrans.Rollback()
    '        Throw ex
    '    Finally

    '        disconnectDB()
    '    End Try
    'End Sub
#End Region
End Class
