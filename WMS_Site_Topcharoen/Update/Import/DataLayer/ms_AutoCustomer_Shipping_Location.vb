Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Public Class ms_AutoCustomer_Shipping_Location : Inherits ms_Customer_Shipping_Location

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
    Public Overloads Function AutoCustomer_Shipping_location(ByVal pCustomer_Shipping_Location_ID As String, ByVal pShipping_Location_Name As String, ByVal pAddress As String, Optional ByVal pCustomer_Shipping_Index As String = "") As String
        Try
            Dim Result As String = ""
            Result = GetDataCustomer_Shipping_Location_ByCompany_Name(pCustomer_Shipping_Location_ID)
            If Result = "" Then
                Result = InsertCustomer_Shipping_Location(pCustomer_Shipping_Location_ID, pShipping_Location_Name, pAddress, pCustomer_Shipping_Index)
            End If
            Return Result
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Overloads Function GetDataCustomer_Shipping_Location_ByCompany_Name(ByVal pCustomer_Shipping_Location_ID As String) As String
        Try
            SetSQLString = "SELECT TOP 1 isnull(Customer_Shipping_Location_Index,'') from ms_Customer_Shipping_Location Where Customer_Shipping_Location_Id = @Customer_Shipping_Location_Id "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 15).Value = pCustomer_Shipping_Location_ID


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
            Return Nothing
        End Try
    End Function


    Public Overloads Function InsertCustomer_Shipping_Location(ByVal pCustomer_Shipping_Location_ID As String, ByVal pShipping_Location_Name As String, ByVal pAddress As String, Optional ByVal pCustomer_Shipping_Index As String = "") As String
        Try

            Dim objDBIndex As New WMS_STD_Formula.Sy_AutoNumber
            Customer_Shipping_Location_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_Index")
            objDBIndex = Nothing



            Customer_Shipping_Location_Id = pCustomer_Shipping_Location_ID
            Customer_Shipping_Index = pCustomer_Shipping_Index
            Shipping_Location_Name = pShipping_Location_Name
            Address = pAddress
            District_Index = "-1" '2
            Province_Index = "-1" '10
            Postcode = ""
            Tel = ""
            Fax = ""
            Mobile = ""
            Email = ""
            Remark = "AUTOINSERT"
            Contact_Person1 = ""
            Contact_Person2 = ""
            Contact_Person3 = ""

            Str3 = "1000000203"
            Str4 = ""
            Str5 = ""

            Route_Index = "0010000000000"
            Subroute_Index = "0010000000000"



            Insert_Master()



            Return Customer_Shipping_Location_Index
        Catch ex As Exception
            Return Nothing
        End Try

    End Function



End Class
