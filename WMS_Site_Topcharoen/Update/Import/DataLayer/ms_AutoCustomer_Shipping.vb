Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Public Class ms_AutoCustomer_Shipping : Inherits ms_Customer_Shipping
#Region "VARIABLE"
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
#End Region
#Region " PROPERTY "
    '*** Property Readonly
    Public Overloads ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public Overloads ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property
#End Region
    '#Region " Operation Type "
    '    Private objStatus As enuOperation_Type

    '    Public Shadows Enum enuOperation_Type
    '        ADDNEW
    '        UPDATE
    '        DELETE
    '        SEARCH
    '        NULL
    '        CANCEL
    '    End Enum
    '#End Region
#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "
    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New(Operation_Type)
        ' objStatus = Operation_Type
    End Sub
#End Region
    Public Overloads Function AutoCustomer_Shipping(ByVal pCustomer_Shipping_ID As String, ByVal pCustomer_Shipping_Name As String) As String
        Try
            Dim Result As String = ""
            Result = GetDataCustomer_Shipping_ByCompany_Name(pCustomer_Shipping_ID)
            If Result = "" Then
                Result = InsertCustomer_Shipping(pCustomer_Shipping_ID, pCustomer_Shipping_Name, "")
            End If
            Return Result
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Insert_Supplier(ByVal Supplier_Id As String, ByVal Supplier_Name As String, ByVal Address As String) As String
        Try
            Dim objSupplier As New ms_Supplier(ms_Supplier.enuOperation_Type.ADDNEW)
            Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber
            Supplier_Index = objDBIndex.getSys_Value("Supplier_Index")
            objDBIndex = Nothing
            objSupplier.Supplier_Id = Supplier_Id
            objSupplier.Supplier_Index = Supplier_Index
            objSupplier.Supplier_Name = Supplier_Name
            objSupplier.Address = Address
            objSupplier.Insert_Master()

            Return Supplier_Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Overloads Function GetDataCustomer_Shipping_ByCompany_Name(ByVal pCustomer_Shipping_ID As String) As String
        Try
            SetSQLString = "SELECT TOP 1 isnull(Customer_Shipping_Index,'') from ms_Customer_Shipping Where Company_Name = @Company_Name "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Company_Name", SqlDbType.VarChar, 100).Value = pCustomer_Shipping_ID


            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            If _scalarOutput Is Nothing Then
                Return ""
            Else
                Return _scalarOutput
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Overloads Function InsertCustomer_Shipping(ByVal pCustomer_Shipping_ID As String, ByVal pCustomer_Shipping_Name As String, ByVal Cus_Address As String) As String
        Try

            Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber
            Customer_Shipping_Index = objDBIndex.getSys_Value("customer_Shipping_Index")
            objDBIndex = Nothing

            Customer_Index = ""
            Title = "∫√‘…—∑"
            Company_Name = pCustomer_Shipping_Name
            Customer_Type_Index = "0010000000002"
            Address = Cus_Address
            District_Index = "2" 'Default
            Province_Index = "10" 'Default
            Postcode = ""
            Tel = ""
            Fax = ""
            Mobile = ""
            Email = ""
            Contact_Person = ""
            Contact_Person2 = ""
            Contact_Person3 = ""
            Barcode = ""
            Remark = "AUTOINSERT"

            Str1 = pCustomer_Shipping_ID
            Str2 = ""
            Str3 = "1000000203"
            Str4 = ""
            Str5 = ""
            Str6 = pCustomer_Shipping_Name

            'Route_Index = ""
            'SubRoute_Index = ""
            'TransportRegion_Index = ""


            Insert_Master()

            Return Customer_Shipping_Index
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
