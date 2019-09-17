Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration
Imports System.Threading

Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_INB_PO_Datalayer

Public Class Cutomer_Service : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private xLOG As New Interface_Log_Service

    Dim objDocumentNumber As New Sy_AutoNumber
    '------------------------------------------------------------------------------------------------------------------
    '#Customer Shipping
    Private Customer_Index As String = ""
    Private Customer_Shipping_Index As String = ""
    Private Customer_Shipping_Id As String = "" 'Str1
    Private Title As String = "ไม่ระบุ"
    Private Company_Name As String = ""
    Private CustomerType_Index As String = "0010000000001" 'N/A
    Private Address As String = ""
    Private District_Index As String = "0010000000000" 'ไม่ระบุ
    Private Province_Index As String = "0010000000000" 'ไม่ระบุ
    Private Town_Index As String = "0010000000000" 'ไม่ระบุ
    Private Town As String = "ไม่ระบุ" 'ไม่ระบุ
    Private PostCode As String = ""
    Private Tel As String = ""
    Private Fax As String = ""
    Private Mobile As String = ""
    Private Email As String = ""
    Private Contact_Person As String = ""
    Private Contact_Person2 As String = ""
    Private Contact_Person3 As String = ""
    Private Barcode As String = ""
    Private Remark As String = ""
    Private Country_Index As String = "1000000203" 'Str3 กรณีต่งประเทศ,1000000203	TH	THAILAND
    Private Province As String = "" 'Str5 กรณีต่งประเทศ
    Private District As String = "" 'Str4 กรณีต่งประเทศ
    Private Name_Eng As String = "" 'Str6
    Private TaxNo As String = "" '
    Private Branch As String = "" '
    Private Depart As String = "" 'สาขา
    '#Customer Shipping Location
    Private Customer_Shipping_Location_Index As String = ""
    Private Customer_Shipping_Location_Id As String = ""
    Private SalesUnit As String = ""
    Private UpdateBy As String = ""
    Private Route_Index As String = "0010000000000" 'N/A, DeliveryRoute": "SAG",
    Private SubRoute_Index As String = "0010000000000" 'N/A
    Private TransportRegion_Index As String = "0010000000000" 'N/A
    Private Begin_flag As String = "1"
    Private Credit_Term As Integer = 0
    Private Str_Sup_Distr As String = ""
    Private Code_WinSpeed_1 As String = ""
    Private Code_WinSpeed_2 As String = ""
    '------------------------------------------------------------------------------------------------------------------
    Function WebService_Customer(Optional ByVal CurrentRowData As Integer = 0 _
    , Optional ByVal TotalRowData As Integer = 0 _
    , Optional ByVal CustomerCode As String = "" _
    , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
        Try

            'Dim WebServicex As New WebReference.WebServiceWMS
            '_dataTable = DBExeQuery("select ID from (select Customer_Shipping_Location_Id as ID from ms_Customer_Shipping_Location UNION select Str1 as ID from ms_Customer_Shipping ) xx ORDER BY ID")
            'For Each dr As DataRow In _dataTable.Rows
            '    'Reflag All Customer In Cusmer_Id
            '    WebServicex.RESET_IsExport(dr("ID").ToString, "001")
            'Next

            'Validate
            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
            Else
                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                If Not IsDate(xDate) Then
                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
                End If
            End If

            'Dim webService As String
            'Dim AppSettingsReader As New System.Configuration.AppSettingsReader()
            'webService = AppSettingsReader.GetValue("WebService_Customer", GetType(String))

            '' Create a request using a URL that can receive a post. 
            'Dim request As WebRequest = WebRequest.Create(webService)
            '' Set the Method property of the request to POST.
            'request.Method = "POST"
            '' Create POST data and convert it to a byte array.
            'Dim postData As String = "" '"strDocType=" & strDocType & "&strDocNo=" & strDocNo
            'postData &= "<?xml version=""1.0"" encoding=""utf-8""?>"
            'postData &= "<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">"
            'postData &= "  <soap:Body>"
            'postData &= "    <Get_DataCustomer xmlns=""http://tempuri.org/"">"
            'postData &= String.Format("     <CurrentRowData>{0}</CurrentRowData>", CurrentRowData.ToString)
            'postData &= String.Format("     <TotalRowData>{0}</TotalRowData>", TotalRowData.ToString)
            'postData &= String.Format("     <CustomerCode>{0}</CustomerCode>", CustomerCode.ToString)
            'postData &= String.Format("     <LastUpdate>{0}</LastUpdate>", LastUpdate_yyyyMMdd.ToString)
            'postData &= "   </Get_DataCustomer>"
            'postData &= " </soap:Body>"
            'postData &= "</soap:Envelope>"

            'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            '' Set the ContentType property of the WebRequest.
            'request.ContentType = "text/xml; charset=utf-8"
            '' Set the ContentLength property of the WebRequest.
            'request.ContentLength = byteArray.Length
            '' Get the request stream."
            'Dim dataStream As Stream = request.GetRequestStream()
            '' Write the data to the request stream.
            'dataStream.Write(byteArray, 0, byteArray.Length)
            '' Close the Stream object.
            'dataStream.Close()
            '' Get the response.
            'Dim response As WebResponse = request.GetResponse()
            '' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            '' Get the stream containing content returned by the server.
            'dataStream = response.GetResponseStream()
            '' Open the stream using a StreamReader for easy access.
            'Dim reader As New StreamReader(dataStream)
            '' Read the content.
            'Dim responseFromServer As String = reader.ReadToEnd()
            '' Display the content.
            '' Clean up the streams.
            'reader.Close()
            'dataStream.Close()
            'response.Close()

            ''Convert text to XML
            'Dim xmlDoc As New Xml.XmlDocument
            'xmlDoc.LoadXml(responseFromServer)

            'Dim nav As XPath.XPathNavigator
            'nav = xmlDoc.CreateNavigator()
            'nav.MoveToRoot()
            ''Move to the first child node (comment field).
            'nav.MoveToFirstChild()


            'Dim WebService As New WebReference.WebServiceWMS
            'Dim str As String = WebService.Get_DataCustomer(CurrentRowData.ToString, TotalRowData.ToString, CustomerCode.ToString, LastUpdate_yyyyMMdd.ToString)
            'Dim dsXML As New DataSet
            'Json convert to dataset           
            'dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
            'Convert datatable to dataview for grouping
            'Dim dv As New DataView(dsXML.Tables(0))

            'Do
            '    'Find the first element.
            '    If nav.NodeType = XPath.XPathNodeType.Element Then
            '        'Determine whether children exist.
            '        If nav.HasChildren = True Then
            '            'Move to the first child.
            '            nav.MoveToFirstChild()
            '            'Loop through all of the children.
            '            Do
            '                'Json convert to dataset
            '                Dim dsXML As New DataSet
            '                dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(nav.Value)
            'Step 1. Auto CUSTOMER
            'If dsXML.Tables.Contains("CUSTOMER") Then

            '    Dim dtProvice As New DataTable
            '    Dim dtDistrict As New DataTable
            '    Dim dtTown As New DataTable
            '    Dim dtRoute As New DataTable
            '    Dim dtSubRoute As New DataTable
            '    Dim drSelect() As DataRow
            '    dtProvice = DBExeQuery("select * from ms_Province where status_id != -1 ")
            '    dtDistrict = DBExeQuery("select * from ms_District where status_id != -1 ")
            '    dtTown = DBExeQuery("select * from ms_Town where status_id != -1 ")
            '    dtRoute = DBExeQuery("select * from ms_Route  where status_id != -1 ")
            '    dtSubRoute = DBExeQuery("select * from ms_SubRoute where status_id != -1 ")
            '    For Each drCus As DataRow In dsXML.Tables("CUSTOMER").Rows
            '        '#Customer Shipping 
            '        Customer_Index = ""
            '        Customer_Shipping_Index = ""
            '        Customer_Shipping_Id = drCus("Code").ToString
            '        Company_Name = drCus("Name1").ToString
            '        Address = drCus("WorkAddress").ToString
            '        Town_Index = "0010000000000"
            '        Province_Index = "0010000000000"
            '        District_Index = "0010000000000"
            '        Province = "ไม่ระบุ"
            '        District = "ไม่ระบุ"
            '        Town = "ไม่ระบุ"

            '        TaxNo = ""
            '        'billsubdist = รหัสตำบล
            '        'GroupCode = รหัสอำเภอ
            '        'routeID = รหัสจังหวัด
            '        'homesubdist = ตำบล
            '        'homedistrict = อำเภอ
            '        'homeprovince = จังหวัด

            '        drSelect = dtProvice.Select(String.Format("Province_Id = '{0}'", drCus("routeID").ToString))
            '        If drSelect.Length > 0 Then
            '            Province_Index = drSelect(0)("Province_Index").ToString
            '            Province = drSelect(0)("Province").ToString
            '        Else
            '            Dim obj_msProvice As New ms_Province(ms_Province.enuOperation_Type.ADDNEW)
            '            obj_msProvice.SaveData("", drCus("routeID").ToString, drCus("homeprovince").ToString)
            '            Province_Index = obj_msProvice.Province_Index
            '            Province = drCus("routeID").ToString
            '        End If
            '        drSelect = dtDistrict.Select(String.Format("District_Id = '{0}'", drCus("GroupCode").ToString))
            '        If drSelect.Length > 0 Then
            '            District_Index = drSelect(0)("District_Index").ToString
            '            District = drSelect(0)("District").ToString
            '        Else
            '            Dim obj_msDistrict As New ms_District(ms_District.enuOperation_Type.ADDNEW)
            '            obj_msDistrict.SaveData("", drCus("GroupCode").ToString, Province_Index, drCus("homedistrict").ToString)
            '            District_Index = obj_msDistrict.District_Index
            '            District = obj_msDistrict.District
            '        End If
            '        drSelect = dtTown.Select(String.Format("Town_Id = '{0}' ", drCus("billsubdist").ToString))
            '        If drSelect.Length > 0 Then
            '            Town_Index = drSelect(0)("Town_Index").ToString
            '            Town = drSelect(0)("Town_Name").ToString
            '        Else
            '            Dim _exc As New DBType_SQLServer
            '            Dim strSQL As String = ""
            '            Dim Town_index As String = drCus("billsubdist").ToString 'New Sy_AutoNumber().getSys_Value("Town_index")
            '            strSQL = "Insert into ms_Town (Town_index,Town_ID,Town_Name,District_Index,status_id) "
            '            strSQL &= String.Format(" Values ('{0}','{1}','{2}','{3}',0)", Town_index, drCus("billsubdist").ToString, drCus("Town_Name").ToString, District_Index)
            '            _exc.DBExeNonQuery(strSQL)
            '        End If
            '        'drSelect = dtRoute.Select(String.Format("Route_No='{0}'", drCus("DepartCode").ToString))
            '        'If drSelect.Length > 0 Then
            '        '    Route_Index = drSelect(0)("Route_Index").ToString
            '        'End If
            '        Credit_Term = IIf(IsDBNull(drCus("BillCredit")), 0, drCus("BillCredit"))
            '        Str_Sup_Distr = drCus("guid").ToString
            '        PostCode = drCus("CustZipCode").ToString
            '        Tel = drCus("Telephone").ToString
            '        Mobile = drCus("DefContactCode").ToString
            '        Fax = drCus("Fax").ToString
            '        Email = drCus("EmailAddress").ToString
            '        TaxNo = drCus("TaxNo").ToString
            '        Depart = drCus("guid").ToString
            '        Contact_Person = drCus("ContactName").ToString
            '        Contact_Person2 = drCus("ParamValues30").ToString '1กท4-05-ผ004"
            '        Contact_Person3 = drCus("Warehouse").ToString '"Warehouse": "SP725G",
            '        Name_Eng = drCus("Name2").ToString  'Str6
            '        '------------------------------------------------------------------------------------------------------------------
            '        '#Customer Shipping,#Customer Shipping Location
            '        SalesUnit = drCus("saleunit").ToString
            '        UpdateBy = drCus("UpdateBy").ToString
            '        Customer_Shipping_Location_Id = drCus("Code").ToString
            '        Code_WinSpeed_1 = drCus("ParamValues29").ToString
            '        Code_WinSpeed_2 = drCus("ParamValues30").ToString
            '        Customer_Shipping_Index = ""
            '        Call Me.SaveCustomer_Shipping()
            '        Call Me.SaveCustomer_Shipping_Location()
            '        '------------------------------------------------------------------------------------------------------------------
            '        'Step 2. Auto SHIPTO
            '        If dsXML.Tables.Contains("SHIPTO") Then
            '            If dsXML.Tables("SHIPTO").Rows.Count > 0 Then
            '                'Customer_Shipping_Id = dsXML.Tables("SHIPTO").Rows(0)("Code").ToString
            '                'drSelect = dsXML.Tables("SHIPTO").Select(String.Format("Code='{0}'", Customer_Shipping_Id))
            '                'drSelect = dsXML.Tables("SHIPTO").Select("")
            '                drSelect = dsXML.Tables("SHIPTO").Select(String.Format("Cust_ID='{0}'", drCus("Code").ToString))
            '                If drSelect.Length > 0 Then
            '                    For Each drRowupdate As DataRow In drSelect
            '                        Customer_Shipping_Id = drRowupdate("Code").ToString
            '                        'Dim drRowupdate As DataRow = drSelect(0)
            '                        '------------------------------------------------------------------------------------------------------------------
            '                        '"Province": "10","Ampher": "1030",Town": "103005" ตำบล
            '                        'Reset
            '                        District_Index = "0010000000000" 'ไม่ระบุ
            '                        Province_Index = "0010000000000" 'ไม่ระบุ
            '                        Town_Index = "0010000000000" 'ไม่ระบุ
            '                        drSelect = dtProvice.Select(String.Format("Province_Id='{0}'", drRowupdate("Province").ToString))
            '                        If drSelect.Length > 0 Then
            '                            Province_Index = drSelect(0)("Province_Index").ToString
            '                        Else
            '                            Dim obj_msProvice As New ms_Province(ms_Province.enuOperation_Type.ADDNEW)
            '                            obj_msProvice.SaveData("", drRowupdate("Province").ToString, drRowupdate("Department_Name").ToString)
            '                            Province_Index = obj_msProvice.Province_Index
            '                            Province = drRowupdate("Province").ToString
            '                        End If
            '                        drSelect = dtDistrict.Select(String.Format("District_Id='{0}'", drRowupdate("Ampher").ToString))
            '                        If drSelect.Length > 0 Then
            '                            District_Index = drSelect(0)("District_Index").ToString
            '                        Else
            '                            Dim obj_msDistrict As New ms_District(ms_District.enuOperation_Type.ADDNEW)
            '                            obj_msDistrict.SaveData("", drRowupdate("Ampher").ToString, Province_Index, drRowupdate("Section_Name").ToString)
            '                            District_Index = obj_msDistrict.District_Index
            '                            District = obj_msDistrict.District
            '                        End If
            '                        drSelect = dtTown.Select(String.Format("Town_Id='{0}'", drRowupdate("Town").ToString))
            '                        If drSelect.Length > 0 Then
            '                            Town_Index = drSelect(0)("Town_Index").ToString
            '                        Else
            '                            Dim _exc As New DBType_SQLServer
            '                            Dim strSQL As String = ""
            '                            Dim Town_index As String = drRowupdate("Town").ToString ' New Sy_AutoNumber().getSys_Value("Town_index")
            '                            strSQL = "Insert into ms_Town (Town_index,Town_ID,Town_Name,District_Index,status_id) "
            '                            strSQL &= String.Format(" Values ('{0}','{1}','{2}','{3}',0)", Town_index, drRowupdate("Town").ToString, drRowupdate("Town_Name").ToString, District_Index)
            '                            _exc.DBExeNonQuery(strSQL)
            '                        End If
            '                        'Auto insert อำเภอ,จังหวัด,ตำบล
            '                        '------------------------------------------------------------------------------------------------------------------
            '                        '#Customer Shipping

            '                        Customer_Shipping_Id = drRowupdate("Code").ToString
            '                        Company_Name = drRowupdate("Name").ToString
            '                        Address = drRowupdate("Address_").ToString
            '                        PostCode = drRowupdate("PostalCode").ToString

            '                        TaxNo = drRowupdate("TaxID").ToString
            '                        Branch = drRowupdate("Branch").ToString

            '                        Call Me.SaveCustomer_Shipping()
            '                        'TODO : Wait service return status
            '                        '#Customer Shipping Location
            '                        SalesUnit = drRowupdate("SaleUnit").ToString
            '                        'Customer_Shipping_Location_Id = drRowupdate("Code").ToString
            '                        Customer_Shipping_Location_Id = drRowupdate("Code").ToString
            '                        'Call Me.SaveCustomer_Shipping()
            '                        Call Me.SaveCustomer_Shipping_Location()


            '                        Branch = ""
            '                        'TODO : Wait service return status
            '                        '------------------------------------------------------------------------------------------------------------------
            '                    Next
            '                End If
            '            End If
            '        End If
            '        'Step 3. Auto BILLTO
            '        If dsXML.Tables.Contains("BILLTO") Then
            '            If dsXML.Tables("BILLTO").Rows.Count > 0 Then
            '                'Customer_Shipping_Id = dsXML.Tables("BILLTO").Rows(0)("Code").ToString
            '                'drSelect = dsXML.Tables("BILLTO").Select(String.Format("Code='{0}'", Customer_Shipping_Id))
            '                'drSelect = dsXML.Tables("BILLTO").Select("")
            '                drSelect = dsXML.Tables("BILLTO").Select(String.Format("Cust_ID='{0}'", drCus("Code").ToString))
            '                If drSelect.Length > 0 Then
            '                    For Each drRowupdate As DataRow In drSelect
            '                        Customer_Shipping_Id = drRowupdate("Code").ToString
            '                        '------------------------------------------------------------------------------------------------------------------
            '                        '"Province": "10","Ampher": "1030",Town": "103005" ตำบล
            '                        'Reset
            '                        District_Index = "0010000000000" 'ไม่ระบุ
            '                        Province_Index = "0010000000000" 'ไม่ระบุ
            '                        Town_Index = "0010000000000" 'ไม่ระบุ
            '                        drSelect = dtProvice.Select(String.Format("Province_Id='{0}'", drRowupdate("Province").ToString))
            '                        If drSelect.Length > 0 Then
            '                            Province_Index = drSelect(0)("Province_Index").ToString
            '                        Else
            '                            Dim obj_msProvice As New ms_Province(ms_Province.enuOperation_Type.ADDNEW)
            '                            obj_msProvice.SaveData("", drRowupdate("Province").ToString, drRowupdate("Department_Name").ToString)
            '                            Province_Index = obj_msProvice.Province_Index
            '                            Province = drRowupdate("Province").ToString
            '                        End If
            '                        drSelect = dtDistrict.Select(String.Format("District_Id='{0}'", drRowupdate("Ampher").ToString))
            '                        If drSelect.Length > 0 Then
            '                            District_Index = drSelect(0)("District_Index").ToString
            '                        Else
            '                            Dim obj_msDistrict As New ms_District(ms_District.enuOperation_Type.ADDNEW)
            '                            obj_msDistrict.SaveData("", drRowupdate("Ampher").ToString, Province_Index, drRowupdate("Section_Name").ToString)
            '                            District_Index = obj_msDistrict.District_Index
            '                            District = obj_msDistrict.District
            '                        End If
            '                        drSelect = dtTown.Select(String.Format("Town_Id='{0}'", drRowupdate("Town").ToString))
            '                        If drSelect.Length > 0 Then
            '                            Town_Index = drSelect(0)("Town_Index").ToString
            '                        Else
            '                            Dim _exc As New DBType_SQLServer
            '                            Dim strSQL As String = ""
            '                            Dim Town_index As String = drRowupdate("Town").ToString ' New Sy_AutoNumber().getSys_Value("Town_index")
            '                            strSQL = "Insert into ms_Town (Town_index,Town_ID,Town_Name,District_Index,status_id) "
            '                            strSQL &= String.Format(" Values ('{0}','{1}','{2}','{3}',0)", Town_index, drRowupdate("Town").ToString, drRowupdate("Town_Name").ToString, District_Index)
            '                            _exc.DBExeNonQuery(strSQL)
            '                        End If
            '                        'Auto insert อำเภอ,จังหวัด,ตำบล
            '                        '------------------------------------------------------------------------------------------------------------------
            '                        '#Customer Shipping

            '                        'Customer_Shipping_Id = drRowupdate("Code").ToString
            '                        Company_Name = drRowupdate("Name").ToString
            '                        Address = drRowupdate("Address_").ToString
            '                        PostCode = drRowupdate("PostalCode").ToString
            '                        'Call Me.SaveCustomer_Shipping()
            '                        '#Customer Shipping Location
            '                        Customer_Shipping_Id = drRowupdate("Code").ToString
            '                        Customer_Shipping_Index = ""
            '                        'Customer_Shipping_Location_Id = drRowupdate("Code").ToString

            '                        TaxNo = drRowupdate("TaxID").ToString
            '                        Branch = drRowupdate("Branch").ToString

            '                        Call Me.SaveCustomer_Shipping()

            '                        Branch = ""
            '                        'Call Me.SaveCustomer_Shipping_Location()
            '                        '------------------------------------------------------------------------------------------------------------------
            '                    Next
            '                End If
            '            End If
            '        End If

            '    Next

            'End If


            '            Loop While nav.MoveToNext()
            '        End If
            '    End If
            'Loop While nav.MoveToNext()
            'Pause.
            'End Save

            Return "S"
        Catch ex As Exception
            'log
            Dim xLogDescription As String = "Auto  : Customer_Shipping_Id : " & Customer_Shipping_Id & ", Error : " & ex.Message.ToString
            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping_Location", Customer_Shipping_Id, Customer_Shipping_Index, "E", xLogDescription)
            Return ex.Message.ToString
        End Try

    End Function

    Function WebService_Customer_LastUpdate(Optional ByVal CustomerCode As String = "", Optional ByVal SalseUnit As String = "") As String
        Try
            'Dim webService As String
            'Dim AppSettingsReader As New System.Configuration.AppSettingsReader()
            'webService = AppSettingsReader.GetValue("WebService_LastUpdate", GetType(String))

            '' Create a request using a URL that can receive a post. 
            'Dim request As WebRequest = WebRequest.Create(webService)
            '' Set the Method property of the request to POST.
            'request.Method = "POST"
            '' Create POST data and convert it to a byte array.
            'Dim postData As String = "" '"strDocType=" & strDocType & "&strDocNo=" & strDocNo
            'postData &= "<?xml version=""1.0"" encoding=""utf-8""?>"
            'postData &= "<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">"
            'postData &= "  <soap:Body>"
            'postData &= "    <Update_LastUpdate_Customer xmlns=""http://tempuri.org/"">"
            'postData &= String.Format("     <CustomerCode>{0}</CustomerCode>", CustomerCode.ToString)
            'postData &= "   </Update_LastUpdate_Customer>"
            'postData &= " </soap:Body>"
            'postData &= "</soap:Envelope>"

            'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            '' Set the ContentType property of the WebRequest.
            'request.ContentType = "text/xml; charset=utf-8"
            '' Set the ContentLength property of the WebRequest.
            'request.ContentLength = byteArray.Length
            '' Get the request stream."
            'Dim dataStream As Stream = request.GetRequestStream()
            '' Write the data to the request stream.
            'dataStream.Write(byteArray, 0, byteArray.Length)
            '' Close the Stream object.
            'dataStream.Close()
            '' Get the response.
            'Dim response As WebResponse = request.GetResponse()
            '' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            '' Get the stream containing content returned by the server.
            'dataStream = response.GetResponseStream()
            '' Open the stream using a StreamReader for easy access.
            'Dim reader As New StreamReader(dataStream)
            '' Read the content.
            'Dim responseFromServer As String = reader.ReadToEnd()
            '' Display the content.
            '' Clean up the streams.
            'reader.Close()
            'dataStream.Close()
            'response.Close()

            ''Convert text to XML
            'Dim xmlDoc As New Xml.XmlDocument
            'xmlDoc.LoadXml(responseFromServer)

            'Dim nav As XPath.XPathNavigator
            'nav = xmlDoc.CreateNavigator()
            'nav.MoveToRoot()
            ''Move to the first child node (comment field).
            'nav.MoveToFirstChild()
            'Do
            '    'Find the first element.
            '    If nav.NodeType = XPath.XPathNodeType.Element Then
            '        'Determine whether children exist.
            '        If nav.HasChildren = True Then
            '            'Move to the first child.
            '            nav.MoveToFirstChild()
            '            'Loop through all of the children.
            '            Do
            'Dim WebService As New WebReference.WebServiceWMS
            'Dim str As String = WebService.Update_IsExport(CustomerCode.ToString, SalseUnit)

            'Return nav.Value
            'nav.Value = "Success" 
            'Dim xStatus As String = ""
            'If str.Trim = "Success" Then
            '    xStatus = "S"
            'Else
            '    xStatus = "E"
            'End If

            'Dim xLogDescription As String = "Update LastDate CustomerCode : " & CustomerCode & " , Massage : " & str
            'xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping", Customer_Shipping_Id, Customer_Shipping_Index, xStatus, xLogDescription)

            '            Loop While nav.MoveToNext()
            '        End If
            '    End If
            'Loop While nav.MoveToNext()
            'Pause.
            Return "S"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function


    Private Function SaveCustomer_Shipping() As String
        Try
            Customer_Shipping_Index = Me.GetIndexByID("ms_Customer_Shipping", "Customer_Shipping_Index", "str1", Customer_Shipping_Id)
            Dim objDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
            If String.IsNullOrEmpty(Customer_Shipping_Index) Then
                'Insert
                objDB = New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
                objDB.SaveData(Customer_Shipping_Index, Customer_Index, Title, Company_Name, CustomerType_Index, Address _
                , District_Index, Province_Index, PostCode, Tel, Fax, Mobile, Email _
                , Contact_Person, Contact_Person2, Contact_Person3, Barcode, Remark _
                , Customer_Shipping_Id, Country_Index, District, Province, Name_Eng)

                Customer_Shipping_Index = objDB.Customer_Shipping_Index
                'DBExeNonQuery(String.Format("Update ms_Customer_Shipping set SalesTool_User = '{0}',INT_U = 1 where Customer_Shipping_Index = '{1}'", UpdateBy, Customer_Shipping_Index))
                Dim xLogDescription As String = "Auto Insert  : Customer_Shipping_Id : " & Customer_Shipping_Id
                xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping", Customer_Shipping_Id, Customer_Shipping_Index, "S", xLogDescription)

            Else
                'Update
                'objDB = New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.UPDATE)
                Dim xSQL As String = ""
                xSQL &= " UPDATE ms_Customer_Shipping SET update_date = getdate()"
                xSQL &= String.Format(" ,Company_Name='{0}',Str6='{1}', Address='{2}',Postcode='{3}'", Company_Name, Name_Eng, Address, PostCode)
                xSQL &= String.Format(" ,District_Index='{0}', Province_Index='{1}' ,str4 = '{2}',str5='{3}' ", District_Index, Province_Index, Province, District)
                xSQL &= String.Format(" ,Tel='{0}', Fax='{1}',Mobile='{2}',Email='{3}'", Tel, Fax, Mobile, Email)

                'Addess จ. อ. ต. เส้นทางหลัก
                xSQL &= String.Format(" , Town_index='{0}'", Town_Index)
                xSQL &= String.Format(" ,Contact_Person='{0}', Contact_Person2='{1}',Contact_Person3='{2}'", Contact_Person, Contact_Person2, Contact_Person3)
                xSQL &= String.Format(" WHERE Customer_Shipping_Index='{0}'", Customer_Shipping_Index)
                DBExeNonQuery(xSQL)


                'log
                Dim xLogDescription As String = "Auto Update " & xSQL.Replace("'", "")
                xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping", Customer_Shipping_Id, Customer_Shipping_Index, "S", xLogDescription)

            End If
            'Fix Hard code interface flag
            Dim xobjDB As New DBType_SQLServer
            Dim _sql As String = ""
            _sql = String.Format("update ms_Customer_Shipping set Credit_Term= {0}, SalesTool_User = '{1}', Town_index='{2}',INT_U = 1,Branch='{3}'", Credit_Term, UpdateBy, Town_Index, Branch)
            _sql &= String.Format(" ,Code_WinSpeed_1 = '{0}',Code_WinSpeed_2 = '{1}',Str_Sup_Distr = '{3}',Tax_No='{4}' where Customer_Shipping_Index = '{2}'", Code_WinSpeed_1, Code_WinSpeed_2, Customer_Shipping_Index, Str_Sup_Distr, TaxNo)
            xobjDB.DBExeNonQuery(_sql)
            'Update Last
            Me.WebService_Customer_LastUpdate(Customer_Shipping_Id, SalesUnit.ToString)

            Return Customer_Shipping_Index
        Catch ex As Exception

            'log
            Dim xLogDescription As String = "Auto  : Customer_Shipping_Id : " & Customer_Shipping_Id & ", Error : " & ex.Message.ToString
            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping", Customer_Shipping_Id, Customer_Shipping_Index, "E", xLogDescription)
            Throw ex
        End Try
    End Function

    Private Function SaveCustomer_Shipping_Location() As String
        Try
            Customer_Shipping_Location_Index = Me.GetIndexByID("ms_Customer_Shipping_Location", "Customer_Shipping_Location_Index", "Customer_Shipping_Location_Id", Customer_Shipping_Location_Id)
            Dim objCusSL As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.ADDNEW)
            If String.IsNullOrEmpty(Customer_Shipping_Location_Index) Then
                'Insert
                objCusSL = New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.ADDNEW)
                objCusSL.SaveData(Customer_Shipping_Location_Index, Customer_Shipping_Location_Id, Customer_Shipping_Index, Company_Name, Address _
                , District_Index, Province_Index, PostCode, Tel, Fax, Mobile, Email, Remark _
                , District, Province, Contact_Person, Contact_Person2, Contact_Person3, Route_Index _
                , Country_Index, Begin_flag, Customer_Shipping_Location_Id, SubRoute_Index, TransportRegion_Index)

                Customer_Shipping_Location_Index = objCusSL.Customer_Shipping_Location_Index

                DBExeNonQuery(String.Format("Update ms_Customer_Shipping_Location set Address = '{0}' where Customer_Shipping_Location_Index = '{1}'", Address, Customer_Shipping_Location_Index))

                'log
                Dim xLogDescription As String = "Auto Insert : Customer_Shipping_Location_Id : " & Customer_Shipping_Location_Id
                xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping_Location", Customer_Shipping_Location_Id, Customer_Shipping_Location_Index, "S", xLogDescription)

            Else
                'Update
                'objCusSL = New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.UPDATE)
                Dim xSQL As String = ""
                xSQL &= " UPDATE ms_Customer_Shipping_Location SET  update_date = getdate() "
                xSQL &= String.Format(" ,Shipping_Location_Name='{0}', Address='{1}',Postcode='{2}'", Company_Name, Address, PostCode)
                xSQL &= String.Format(" ,Tel='{0}', Fax='{1}',Mobile='{2}',Email='{3}'", Tel, Fax, Mobile, Email)
                'Addess จ. อ. ต. เส้นทางหลัก
                xSQL &= String.Format(" , Town_index='{0}',District_Index='{1}',Province_Index='{2}'", Town_Index, District_Index, Province_Index)
                xSQL &= String.Format(" ,Contact_Person1='{0}', Contact_Person2='{1}',Contact_Person3='{2}'", Contact_Person, Contact_Person2, Contact_Person3)
                xSQL &= String.Format(" WHERE Customer_Shipping_Location_Index='{0}'", Customer_Shipping_Location_Index)
                DBExeNonQuery(xSQL)


                'log
                Dim xLogDescription As String = "Auto update " & xSQL.Replace("'", "")
                xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping_Location", Customer_Shipping_Location_Id, Customer_Shipping_Location_Index, "S", xLogDescription)

            End If

            'Fix Hard code interface flag
            Dim xobjDB As New DBType_SQLServer
            xobjDB.DBExeNonQuery(String.Format("update ms_Customer_Shipping_Location  set INT_U = 1,SalesTool_User = '{1}',Town_index='{2}',Tax_No='{3}' where Customer_Shipping_Location_Index = '{0}'", Customer_Shipping_Location_Index, UpdateBy, Town_Index, TaxNo))


            Return Customer_Shipping_Location_Index
        Catch ex As Exception

            'log
            Dim xLogDescription As String = "Auto  : Customer_Shipping_Location_Id : " & Customer_Shipping_Location_Id & ", Error : " & ex.Message.ToString
            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Customer_Shipping_Location", Customer_Shipping_Location_Id, Customer_Shipping_Location_Index, "E", xLogDescription)

            Throw ex
        End Try
    End Function

    Public Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String, Optional ByVal strwhere As String = "") As String
        Try
            Dim strSQL As String = ""
            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = @" & pstrField_ID & ""
            SetSQLString = strSQL
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@" & pstrField_ID & "", SqlDbType.VarChar, 255).Value = pstrField_ID_Value
            End With
            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select
            SetSQLString = strSQL & strwhere
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            Select Case _scalarOutput
                Case Nothing
                    Return ""
                Case ""
                    Return ""
                Case Else
                    Return _scalarOutput.ToString
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

Public Class Product_Service : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private xLOG As New Interface_Log_Service
    Dim objDocumentNumber As New Sy_AutoNumber
    '------------------------------------------------------------------------------------------------------------------
    Function WebService_Product(Optional ByVal CurrentRowData As Integer = 0 _
    , Optional ByVal TotalRowData As Integer = 0 _
    , Optional ByVal ProductCode As String = "" _
    , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
        Dim Product_Header As String = ""
        Try
            'Validate
            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
            Else
                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                If Not IsDate(xDate) Then
                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
                End If
            End If
            'Dim webService As String
            'Dim AppSettingsReader As New System.Configuration.AppSettingsReader()

            'webService = AppSettingsReader.GetValue("WebService_Product", GetType(String))

            '' Create a request using a URL that can receive a post. 
            'Dim request As WebRequest = WebRequest.Create(webService)
            '' Set the Method property of the request to POST.
            'request.Method = "POST"
            '' Create POST data and convert it to a byte array.
            'Dim postData As String = "" '"strDocType=" & strDocType & "&strDocNo=" & strDocNo
            'postData &= "<?xml version=""1.0"" encoding=""utf-8""?>"
            'postData &= "<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">"
            'postData &= "  <soap:Body>"
            'postData &= "    <Get_DataProduct xmlns=""http://tempuri.org/"">"
            'postData &= String.Format("     <CurrentRowData>{0}</CurrentRowData>", CurrentRowData.ToString)
            'postData &= String.Format("     <TotalRowData>{0}</TotalRowData>", TotalRowData.ToString)
            'postData &= String.Format("     <ProductCode>{0}</ProductCode>", ProductCode.ToString)
            'postData &= String.Format("     <LastUpdate>{0}</LastUpdate>", LastUpdate_yyyyMMdd.ToString)
            'postData &= "   </Get_DataProduct>"
            'postData &= " </soap:Body>"
            'postData &= "</soap:Envelope>"

            'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            '' Set the ContentType property of the WebRequest.
            'request.ContentType = "text/xml; charset=utf-8"
            '' Set the ContentLength property of the WebRequest.
            'request.ContentLength = byteArray.Length
            '' Get the request stream."
            'Dim dataStream As Stream = request.GetRequestStream()
            '' Write the data to the request stream.
            'dataStream.Write(byteArray, 0, byteArray.Length)
            '' Close the Stream object.
            'dataStream.Close()
            '' Get the response.
            'Dim response As WebResponse = request.GetResponse()
            '' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            '' Get the stream containing content returned by the server.
            'dataStream = response.GetResponseStream()
            '' Open the stream using a StreamReader for easy access.
            'Dim reader As New StreamReader(dataStream)
            '' Read the content.
            'Dim responseFromServer As String = reader.ReadToEnd()
            '' Display the content.
            '' Clean up the streams.
            'reader.Close()
            'dataStream.Close()
            'response.Close()

            ''Convert text to XML
            'Dim xmlDoc As New Xml.XmlDocument
            'xmlDoc.LoadXml(responseFromServer)

            'Dim nav As XPath.XPathNavigator
            'nav = xmlDoc.CreateNavigator()
            'nav.MoveToRoot()
            ''Move to the first child node (comment field).
            'nav.MoveToFirstChild()
            'Do
            '    'Find the first element.
            '    If nav.NodeType = XPath.XPathNodeType.Element Then
            '        'Determine whether children exist.
            '        If nav.HasChildren = True Then
            '            'Move to the first child.
            '            nav.MoveToFirstChild()
            '            'Loop through all of the children.
            '            Do
            'Json convert to dataset
            'Dim dsXML As New DataSet
            'dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(nav.Value)

            'Dim WebService As New WebReference.WebServiceWMS
            'Dim str As String = WebService.Get_DataProduct(CurrentRowData.ToString, TotalRowData.ToString, ProductCode.ToString, LastUpdate_yyyyMMdd.ToString)
            'Dim dsXML As New DataSet
            'Json convert to dataset           
            'dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
            ''Step 1. Auto
            'If dsXML.Tables.Contains("PRODUCT") Then
            '    'Load all data for check data : kill แนะนำ
            '    Dim xSQL As String = ""
            '    Dim xDTALL As New DataTable
            '    Dim xNewSKU As Boolean = False
            '    xSQL = " SELECT S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
            '    xSQL &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3"
            '    xSQL &= " 		,P.Description as Package,P.*"
            '    xSQL &= " FROM ms_SKURatio R"
            '    xSQL &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
            '    xSQL &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
            '    xSQL &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
            '    xSQL &= " WHERE S.status_id <> -1"
            '    xSQL &= " ORDER BY S.Sku_Id,R.Ratio"
            '    xDTALL = DBExeQuery(xSQL)

            '    For Each drProd As DataRow In dsXML.Tables("PRODUCT").Rows
            '        Dim xArrSKU() As DataRow
            '        Dim xSku_Index As String = ""
            '        Dim xPackage_Index As String = ""
            '        Dim xBarcode1 As String = ""
            '        Product_Header = drProd("P_Code").ToString
            '        xArrSKU = xDTALL.Select(String.Format("Sku_Id = '{0}'", drProd("P_Code").ToString))
            '        If xArrSKU.Length > 0 Then
            '            xNewSKU = False
            '            xSku_Index = xArrSKU(0)("Sku_Index").ToString
            '        Else
            '            xNewSKU = True
            '            Dim objDBIndex As New Sy_AutoNumber
            '            xSku_Index = objDBIndex.getSys_Value("SKU_Index")
            '            objDBIndex = Nothing
            '        End If
            '        If Not xNewSKU Then
            '            'STEP 2 : Product Update
            '            xArrSKU = xDTALL.Select(String.Format("Sku_Id = '{0}'", drProd("P_Code").ToString))
            '            xSQL = "UPDATE ms_Sku SET "
            '            xSQL &= String.Format(" INT_U = 1,Str1 = '{0}'", drProd("PRODUCTNAME").ToString)
            '            xSQL &= String.Format(" ,Str2 = '{0}'", drProd("PRODUCTSHORTNAME").ToString)
            '            ' If Not drProd("BARCODE1").ToString = "" Then xSQL &= String.Format(" ,Barcode1 = '{0}'", drProd("BARCODE1").ToString)
            '            If Not drProd("Option10").ToString = "" Then xSQL &= String.Format(" ,Str4 = '{0}'", drProd("Option10").ToString)
            '            If Not drProd("UpdateBy").ToString = "" Then xSQL &= String.Format(" ,SalesTool_User = '{0}' , update_date = getdate()", drProd("UpdateBy").ToString)
            '            xSQL &= String.Format(" WHERE Sku_Index = '{0}'", xArrSKU(0)("Sku_Index").ToString)
            '            DBExeQuery(xSQL)

            '            'Update Retrun Service
            '            Me.WebService_Product_Update(drProd("P_Code").ToString, xSku_Index, drProd("P_Code").ToString)
            '            'log
            '            Dim xLogDescription As String = "Auto update : " & xSQL.Replace("'", "")
            '            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Sku", drProd("P_Code").ToString, xArrSKU(0)("Sku_Index").ToString, "S", xLogDescription)


            '        End If
            '        'STEP 1 : UOM Update
            '        '**************************************************************************
            '        '1. ถ้ามี 1 หน่วย UNIT2RATE = UNIT1 
            '        '2. ถ้ามี 2 หน่วย UNIT2RATE = UNIT1 และ UNIT1RATE = UNIT2
            '        '3. ถ้ามี 3 หน่วย UNIT3RATE = UNIT1 และ UNIT1RATE = UNIT3 และ UNIT2RATE = UNIT2
            '        Dim iUnit As Integer = 0
            '        Dim xArrUOM() As DataRow
            '        Dim xRatio As Double = 0
            '        Dim xUOMID As String = ""
            '        Dim xUOMNAME As String = ""
            '        Dim xBARCODE As String = ""
            '        xSQL = String.Format("P_Code = '{0}'", drProd("P_Code").ToString)
            '        xSQL &= " AND (UNIT1RATE IS NOT NULL) AND (UNIT2RATE  IS NOT NULL) AND (UNIT3RATE IS NOT NULL)"
            '        xArrUOM = dsXML.Tables("PRODUCT").Select(xSQL)
            '        If xArrUOM.Length > 0 Then
            '            'มี 3 หน่วย
            '            'STD UOM
            '            xRatio = xArrUOM(0)("UNIT1RATE")
            '            xUOMID = xArrUOM(0)("UNIT3").ToString
            '            xUOMNAME = xArrUOM(0)("UNIT3NAME").ToString
            '            xBARCODE = xArrUOM(0)("BARCODE3").ToString
            '            xBarcode1 = xBARCODE
            '            xPackage_Index = Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)
            '            'CONVET2 UOM
            '            xRatio = xArrUOM(0)("UNIT2RATE")
            '            xUOMID = xArrUOM(0)("UNIT2").ToString
            '            xUOMNAME = xArrUOM(0)("UNIT2NAME").ToString
            '            xBARCODE = xArrUOM(0)("BARCODE2").ToString
            '            Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)
            '            'CONVET3 UOM
            '            xRatio = xArrUOM(0)("UNIT3RATE")
            '            xUOMID = xArrUOM(0)("UNIT1").ToString
            '            xUOMNAME = xArrUOM(0)("UNIT1NAME").ToString
            '            xBARCODE = xArrUOM(0)("BARCODE1").ToString
            '            Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)

            '        Else
            '            xSQL = String.Format("P_Code = '{0}'", drProd("P_Code").ToString)
            '            xSQL &= " AND (UNIT1RATE  IS NOT NULL) AND (UNIT2RATE  IS NOT NULL)"
            '            xArrUOM = dsXML.Tables("PRODUCT").Select(xSQL)
            '            If xArrUOM.Length > 0 Then
            '                'มี 2 หน่วย
            '                'STD UOM
            '                xRatio = xArrUOM(0)("UNIT1RATE")
            '                xUOMID = xArrUOM(0)("UNIT2").ToString
            '                xUOMNAME = xArrUOM(0)("UNIT2NAME").ToString
            '                xBARCODE = xArrUOM(0)("BARCODE2").ToString
            '                xBarcode1 = xBARCODE
            '                xPackage_Index = Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)
            '                'CONVET2 UOM
            '                xRatio = xArrUOM(0)("UNIT2RATE")
            '                xUOMID = xArrUOM(0)("UNIT1").ToString
            '                xUOMNAME = xArrUOM(0)("UNIT1NAME").ToString
            '                xBARCODE = xArrUOM(0)("BARCODE1").ToString
            '                Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)
            '            Else
            '                xSQL = String.Format("P_Code = '{0}'", drProd("P_Code").ToString)
            '                xSQL &= " AND (UNIT2RATE  IS NOT NULL)"
            '                xArrUOM = dsXML.Tables("PRODUCT").Select(xSQL)
            '                If xArrUOM.Length > 0 Then
            '                    'มี 1 หน่วย
            '                    xRatio = xArrUOM(0)("UNIT2RATE")
            '                    xUOMID = xArrUOM(0)("UNIT1").ToString
            '                    xUOMNAME = xArrUOM(0)("UNIT1NAME").ToString
            '                    xBARCODE = xArrUOM(0)("BARCODE1").ToString
            '                    xBarcode1 = xBARCODE
            '                    xPackage_Index = Me.Insert_Package(xDTALL, xSku_Index, xUOMNAME, xUOMID, xRatio, xBARCODE)
            '                Else
            '                    'มี 0 หน่วย
            '                End If
            '            End If
            '        End If
            '        'STEP 1 : END UOM Update
            '        '**************************************************************************
            '        If xNewSKU = True Then
            '            'STEP 3 : Product Insert
            '            Dim xSku_Id As String = drProd("P_Code").ToString
            '            Dim xSize_Index As String = "0010000000001" '--- ไม่ระบุ ---
            '            'Dim xPackage_Index As String = "-1" '--- ไม่ระบุ ---
            '            Dim xUnitWeight_Index As String = drProd("grossweight")
            '            Dim xColor_Index As String = "0010000000001" '--- ไม่ระบุ ---
            '            Dim xModel_Index As String = "0010000000001" '--- ไม่ระบุ ---
            '            Dim xBrand_Index As String = "0010000000001" '--- ไม่ระบุ ---
            '            'Dim xBarcode1 As String = "" '--- ไม่ระบุ ---
            '            Dim xBarcode2 As String = "" '--- ไม่ระบุ ---
            '            Dim xPrice1 As Double = 0
            '            Dim xPrice2 As Double = 0
            '            Dim xPrice3 As Double = 0
            '            Dim xLifeTime_y As Integer = 0
            '            Dim xLifeTime_m As Integer = 0
            '            Dim xLifeTime_d As Integer = 0
            '            Dim xStr1 As String = drProd("PRODUCTNAME").ToString
            '            Dim xStr2 As String = drProd("PRODUCTSHORTNAME").ToString
            '            Dim xDescription As String = "" '--- ไม่ระบุ ---
            '            Dim xMin_Qty As Double = 0
            '            Dim xMin_Weight As Double = 0
            '            Dim xMin_Volume As Double = 0
            '            Dim xMax_Qty As Double = 0
            '            Dim xMax_Weight As Double = 0
            '            Dim xMax_Volume As Double = 0
            '            Dim xbitPlot As Boolean = True
            '            Dim xbitControlAsset As Boolean = False
            '            Dim xbitPackRework As Boolean = False
            '            Dim xbitBarcodeProcess As Boolean = False
            '            Dim xbitPackingTypes As Boolean = False
            '            Dim xQty_Per_Pallet As Double = 0
            '            Dim xVolumeX As Double = 1
            '            Dim xVolumeY As Double = 1
            '            Dim xVolumeZ As Double = drProd("Vol")
            '            Dim xVolume As Double = drProd("Vol")
            '            Dim xStr4 As String = drProd("Option10").ToString 'รหัสสินค้าเก่าจาก ERP
            '            Dim xStr5 As String = "" '--- ไม่ระบุ ---
            '            Dim xCurrency_Index As String = "0010000000039" 'Thailand,BATH
            '            Dim xCustomer_Index As String = "" '--- ไม่ระบุ ---
            '            Dim xSupplier_Index As String = "" '--- ไม่ระบุ ---
            '            Dim xFileName As String = "" '--- ไม่ระบุ ---
            '            Dim xPick_Method As Integer = 1
            '            Dim xItem_Package_Index As String = "" '--- ไม่ระบุ ---
            '            Dim xLocation_Index As String = "" '--- ไม่ระบุ ---
            '            Dim xProductClass_Index As String = "" '--- ไม่ระบุ ---
            '            Dim xProductSubClass_Index As String = "" '--- ไม่ระบุ ---
            '            Dim UpdateBy As String = drProd("UpdateBy").ToString
            '            Dim objDB As New ms_SKU(ms_SKU.enuOperation_Type.ADDNEW)
            '            'Need Assige before save sku
            '            '-----------------------------------------------------
            '            objDB._ProductSku_Type = "1"
            '            objDB._Product_Type = "0010000000001" '--- ไม่ระบุ ---
            '            objDB._ProductName = xStr1
            '            objDB.Str10 = 1 'Normal
            '            objDB.Str4 = xStr4
            '            objDB.Str5 = xStr5
            '            objDB.PalletType_Index = "0010000000001" 'Default
            '            objDB.Pick_Method = xPick_Method
            '            '-----------------------------------------------------
            '            Dim blnSaveResult As Boolean = False
            '            blnSaveResult = objDB.SaveData(xSku_Index, xSku_Id, "", xSize_Index, xPackage_Index, xUnitWeight_Index, _
            '            xColor_Index, xModel_Index, xBrand_Index, xBarcode1, xBarcode2, xPrice1, xPrice2, xPrice3, xLifeTime_y, xLifeTime_m, xLifeTime_d, _
            '            xStr1, xStr2, xDescription, xMin_Qty, xMin_Weight, xMin_Volume, xMax_Qty, xMax_Weight, xMax_Volume, _
            '            xbitPlot, xbitControlAsset, xbitPackRework, xbitBarcodeProcess, xbitPackingTypes, xQty_Per_Pallet, xVolumeX, xVolumeY, xVolumeZ, xVolume, _
            '            xStr4, xStr5, xCurrency_Index, xCurrency_Index, xCurrency_Index, xCustomer_Index, xSupplier_Index, xFileName, xPick_Method, xItem_Package_Index, _
            '            xLocation_Index, xProductClass_Index, xProductSubClass_Index, Nothing)
            '            blnSaveResult = objDB.InsertSKU_Transation()
            '            '-----------------------------------------------------

            '            'Fix Hard code interface flag
            '            Dim xobjDB As New DBType_SQLServer
            '            xobjDB.DBExeNonQuery(String.Format("update ms_SKU  set INT_U = 1 ,SalesTool_User = '{1}' where Sku_Index = '{0}'", xSku_Index, UpdateBy))


            '            'Update Retrun Service
            '            Me.WebService_Product_Update(drProd("P_Code").ToString, xSku_Index, xSku_Id)
            '            'log
            '            Dim xLogDescription As String = "Auto Insert : Sku_Id : " & drProd("P_Code").ToString
            '            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Sku", xSku_Id, xSku_Index, "S", xLogDescription)

            '            '-----------------------------------------------------
            '            'STEP 4 : Re load UOM
            '            xSQL = " SELECT S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
            '            xSQL &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3"
            '            xSQL &= " 		,P.Description as Package,P.*"
            '            xSQL &= " FROM ms_SKURatio R"
            '            xSQL &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
            '            xSQL &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
            '            xSQL &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
            '            xSQL &= String.Format(" WHERE S.status_id <> -1 AND S.Sku_Index = '{0}'", xSku_Index)
            '            xSQL &= " ORDER BY S.Sku_Id,R.Ratio"
            '            Dim xDTNew As New DataTable
            '            xDTNew = DBExeQuery(xSQL)
            '            For Each drNew As DataRow In xDTNew.Rows
            '                xDTALL.Rows.Add(drNew.ItemArray)
            '            Next
            '            xDTALL.AcceptChanges()


            '        End If
            '    Next

            'End If
            '            Loop While nav.MoveToNext()
            '        End If
            '    End If
            'Loop While nav.MoveToNext()
            'Pause.
            'End Save

            Return "S"
        Catch ex As Exception
            'log
            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
            xLOG.KSL_SY_LOG_INTERFACE_ST("System", Product_Header, "", "E", xLogDescription)

            Return ex.Message.ToString
        End Try

    End Function
    Function WebService_Product_Update(Optional ByVal ProductCode As String = "", Optional ByVal xSku_Index As String = "", Optional ByVal Sku_id As String = "") As String
        Try

            'Dim WebService As New WebReference.WebServiceWMS
            'Dim str As String = WebService.Update_FLAG_PRODUCT(ProductCode.ToString)
            ''Return nav.Value
            ''nav.Value = "Success" 
            'Dim xStatus As String = ""
            'If str.Trim = "Success" Then
            '    xStatus = "S"
            'Else
            '    xStatus = "E"
            'End If

            'Dim xLogDescription As String = "Update LastDate ProductCode : " & ProductCode & " , Massage : " & str
            'xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Sku", xSku_Index, Sku_id, xStatus, xLogDescription)

            '            Loop While nav.MoveToNext()
            '        End If
            '    End If
            'Loop While nav.MoveToNext()
            'Pause.
            Return "S"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function
    Private Function Insert_Package(ByVal dtKCPackage As DataTable, ByVal Sku_Index As String, ByVal UOMNAME As String, ByVal UOMID As String, ByVal UOMRATIO As String, ByVal UOMBARCODE As String) As String
        Try
            Dim xArrUOM() As DataRow
            xArrUOM = dtKCPackage.Select(String.Format("Package_Id = '{0}' and Sku_Index = '{1}'", UOMID, Sku_Index))
            If xArrUOM.Length = 0 Then
                'Insert UOM
                'New Package
                '0010000000000	None	ไม่ระบุ	1
                '0010000000001	Cm	เซนติเมตร	1000000
                '0010000000002	M	เมตร	1
                Dim xNewPackage_Index As String = ""
                Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
                xNewPackage_Index = objms_Package.SaveData("", UOMID, UOMNAME, 0, 0, 0, 0, 0, UOMBARCODE, "0010000000000") ', txtUnit_id.Text
                'New Ratio
                Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
                objDBSKURatio.SaveData("", Sku_Index, xNewPackage_Index, UOMRATIO)

                'Fix Hard code interface flag
                Dim xobjDB As New DBType_SQLServer
                'xobjDB.DBExeNonQuery(String.Format("update ms_Package  set Description = '{0}',Barcode = '{1}' , update_by = 'Import Interface',update_date = getdate() where Package_Index = '{2}'", UOMNAME, UOMBARCODE, xNewPackage_Index))
                'xobjDB.DBExeNonQuery(String.Format("update ms_Sku set Barcode1 = '{0}' where Sku_Index = '{1}', update_by = 'Import Interface',update_date = getdate() and  Package_Index = '{2}' ", UOMBARCODE, Sku_Index, xNewPackage_Index))

                'log
                Dim xLogDescription As String = "Auto Insert : Sku_Index : " & Sku_Index & " ,Package : " & UOMNAME
                xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Package", UOMID, xNewPackage_Index, "S", xLogDescription)

                Return xNewPackage_Index
            Else
                'Update UOM
                'Fix Hard code interface flag
                Dim xobjDB As New DBType_SQLServer
                xobjDB.DBExeNonQuery(String.Format("update ms_Package  set status_id = 0, Barcode = '{0}', update_by = 'Import Interface',update_date = getdate() where Package_Index = '{1}' and isnull(Barcode,'') = '' ", UOMBARCODE, xArrUOM(0).Item("Package_Index").ToString))
                xobjDB.DBExeNonQuery(String.Format("update ms_Sku set Barcode1 = '{0}' , update_by = 'Import Interface',update_date = getdate() where   isnull(Barcode1,'') = '' and Sku_Index = '{1}' and  Package_Index = '{2}' ", UOMBARCODE, Sku_Index, xArrUOM(0).Item("Package_Index").ToString))

                Return xArrUOM(0).Item("Package_Index").ToString
            End If

        Catch ex As Exception
            'log
            Dim xLogDescription As String = "Auto Insert : Sku_Index : " & Sku_Index & " ,Package : " & UOMNAME & ", Error : " & ex.Message.ToString
            xLOG.KSL_SY_LOG_INTERFACE_ST("ms_Package", UOMID, "", "E", xLogDescription)

            Throw ex
        End Try
    End Function

End Class

'Public Class SalseOrder_Service : Inherits DBType_SQLServer

'    Private _dataTable As DataTable = New DataTable
'    Private _scalarOutput As String
'    Private xLOG As New Interface_Log_Service

'    Dim objDocumentNumber As New Sy_AutoNumber
'    '------------------------------------------------------------------------------------------------------------------
'    Function WebService_Salesorder(Optional ByVal OrderID As String = "" _
'    , Optional ByVal SaleUnit As String = "" _
'    , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_SalesOrder(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            'Convert datatable to dataview for grouping
'            Dim dv As New DataView(dsXML.Tables(0))
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : Salesorder"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})

'            'Step 1. Auto       
'            If Not Me.InsertToSaleOrder(dsXML.Tables(0), dtGroup, "0010000000018") Then
'                Return "E"
'                Exit Function
'            End If

'            'Pause.
'            Return "S"
'        Catch ex As Exception
'            'log
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", "", "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function

'    Function WebService_TransferOut(Optional ByVal OrderID As String = "" _
'    , Optional ByVal SaleUnit As String = "" _
'    , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_GoodsTransferOut(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : TransferOut"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            'Convert datatable to dataview for grouping
'            Dim dv As New DataView(dsXML.Tables(0))

'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})
'            If Not Me.InsertToSaleOrder(dsXML.Tables(0), dtGroup, "0010000000052") Then

'                Return "E"
'                Exit Function
'            End If

'            'Pause.
'            Return "S"
'        Catch ex As Exception
'            'log
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", "", "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function
'    Function WebService_GoodsIssue(Optional ByVal OrderID As String = "" _
'        , Optional ByVal SaleUnit As String = "" _
'        , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_GoodsIssue(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : TransferOut"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            'Convert datatable to dataview for grouping
'            Dim dv As New DataView(dsXML.Tables(0))

'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})
'            If Not Me.InsertToSaleOrder(dsXML.Tables(0), dtGroup, "0020000000105") Then ' GoodISSure

'                Return "E"
'                Exit Function
'            End If

'            'Pause.
'            Return "S"
'        Catch ex As Exception
'            'log
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", "", "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function

'    Function WebService_CreditNote(Optional ByVal OrderID As String = "" _
'        , Optional ByVal SaleUnit As String = "" _
'        , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_CreditNotes(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : TransferOut"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            'Convert datatable to dataview for grouping
'            Dim dv As New DataView(dsXML.Tables(0))

'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})
'            If Not Me.InsertToSaleOrder(dsXML.Tables(0), dtGroup, "0020000000106") Then ' CreditNote
'                Return "E"
'                Exit Function
'            End If

'            'Pause.
'            Return "S"
'        Catch ex As Exception
'            'log
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", "", "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function
'    Private Function InsertToSaleOrder(ByVal dtDetail As DataTable, ByVal _dtHeader As DataTable, ByVal Document_Index As String) As Boolean
'        Dim Header_No As String = ""
'        Try
'            'Begin Save

'            'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
'            Dim objSaleOrder As New tb_SalesOrder
'            Dim objSaleOrderItem As New tb_SalesOrderItem
'            Dim objSaleOrderItemCollection As New List(Of tb_SalesOrderItem)

'            Dim objDBIndex As New Sy_AutoNumber
'            Dim objDBTempIndex As New Sy_Temp_AutoNumber
'            Dim xSalesOrder_Index As New List(Of String)
'            'Add on Query
'            Dim xQuery As String = ""
'            Dim xDt As New DataTable
'            Dim xObjDB As New DBType_SQLServer
'            Dim xDistributionCenter_Index As String = ""
'            WV_UserName = "admin"


'            _dtHeader.Columns.Add("SaleUnit")
'            For Each drHeader As DataRow In _dtHeader.Rows
'                Dim dt_cusship, dt_cus, dt_cusship_location As New DataTable

'                Dim drDetail() As DataRow = dtDetail.Select(String.Format("OrderID = '{0}'", drHeader("OrderID").ToString.Trim))
'                Dim drHeaderData As DataRow = drDetail(0)
'                drHeader("SaleUnit") = drHeaderData("SaleUnit").ToString.Trim
'                Header_No = drHeader("OrderID").ToString.Trim
'                Dim dtCheck As New DataTable
'                dtCheck = DBExeQuery("select * from tb_salesorder where SalesOrder_No = '" & drHeader("OrderID").ToString.Trim & "' and status <> -1")

'                If dtCheck.Rows.Count > 0 Then
'                    'Order Id ซ้ำ
'                    Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO")
'                    Dim xLogDescription = "OrderID Duplicate :" & drHeader("OrderID").ToString
'                    xLOG.KSL_SY_LOG_INTERFACE_ST("Auto Update FLAG OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                    'Return False
'                    Continue For
'                End If


'                '----------------------------------------------------------------------------------------------------------------
'                'Add-On 2018.05.25
'                'Check num record,countrow
'                If dtDetail.Columns.Contains("countrow") Then
'                    If drDetail.Length <> CInt(drDetail(0)("countrow")) Then
'                        'Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO")
'                        Dim xLogDescription = "OrderID :" & drHeader("OrderID").ToString
'                        xLogDescription &= ", Sales Order countrow ST is :" & drDetail(0)("countrow").ToString & " , WMS Receipt : " & drDetail.Length.ToString
'                        xLOG.KSL_SY_LOG_INTERFACE_ST("Error Record OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                        'Return False
'                        Continue For
'                    End If
'                End If
'                '----------------------------------------------------------------------------------------------------------------

'                Dim str_CusShip As String = IIf(drHeaderData("Customer_Id").ToString = Nothing, "SPBR00001", drHeaderData("Customer_Id"))
'                Dim str_Cus As String = IIf(drHeaderData("OWNER").ToString = Nothing, -1, drHeaderData("OWNER").ToString)
'                dt_cus = DBExeQuery("Select * from ms_Customer where Customer_id = '" & str_Cus & "'")


'                '  dt_cusship_Location = DBExeQuery("select * from ms_Customer_Shipping_Location where Customer_Shipping_Index = '" & dt_cusship.Rows(0).Item("Customer_Shipping_Index") & "'")


'                ''Header
'                '  drHeader("SaleUnit") = drHeaderData("SaleUnit").ToString.Trim
'                objSaleOrder = New tb_SalesOrder
'                objSaleOrder.SalesOrder_Index = objDBTempIndex.getSys_Value("SalesOrder_Index")
'                objSaleOrder.DocumentType_Index = Document_Index.Trim.ToString
'                objSaleOrder.SalesOrder_No = drHeaderData("Orderid").ToString.Trim
'                objSaleOrder.SalesOrder_Date = CDate(drHeaderData("OrderDate").ToString.Substring(0, 4) & "/" & drHeaderData("OrderDate").ToString.Substring(4, 2) & "/" & drHeaderData("OrderDate").ToString.Substring(6)).ToString("yyyy/MM/dd") 'Jen 8/09/2017

'                objSaleOrder.Customer_Index = dt_cus.Rows(0).Item("Customer_Index").ToString
'                objSaleOrder.Carrier_Index = "0010000000000" 'Fix ไม่ระบุ
'                Dim Sql As String = ""

'                If Document_Index = "0010000000018" Then '' Only SO


'                    ''-----Reset SHIPTO Allway
'                    ''Reflag
'                    'str = WebService.RESET_IsExport(drHeaderData("SHIPTO").ToString, drHeader("SaleUnit").ToString)
'                    '' WebService = New WebReference.WebServiceWMS
'                    ''Get New
'                    'str = WebService.Get_DataCustomer(0, 0, drHeaderData("SHIPTO").ToString, "")
'                    'dsXML = New DataSet
'                    'dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'                    'If dsXML.Tables.Contains("CUSTOMER") Then
'                    '    Dim c As New Cutomer_Service
'                    '    c.WebService_Customer(0, 0, drHeaderData("SHIPTO").ToString, "")
'                    'Else
'                    '    Dim xLogDescription = "Not Found SHIPTO in Table WMS:" & str_CusShip & " ms_Customer_Shipping,ms_Customer_Shipping_Location"
'                    '    xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                    '    'Return False
'                    '    Continue For
'                    'End If
'                    '-----

'                    'Sql = "Select Customer_Shippisng_Index , str1 as BILLTO ,*  from ms_Customer_Shipping where Str1 = '" + drHeaderData("BILLTO").ToString + "' and status_id <> -1"
'                    Sql = "Select * , str1 as BILLTO  from ms_Customer_Shipping where Str1 = '" + drHeaderData("BILLTO").ToString + "' and status_id <> -1"
'                    '>>>>>>> aea5d1a2c8d5d950942f6ac89edbf02ba72b75ce
'                    dt_cusship = DBExeQuery(Sql)
'                    If dt_cusship.Rows.Count > 0 Then
'                        objSaleOrder.Str3 = dt_cusship.Rows(0).Item("Tax_No").ToString 'เลขเสียภาษี
'                        objSaleOrder.Customer_Shipping_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Index").ToString
'                    Else
'                        '-----Reset BILLTO  
'                        Dim WebService As New WebReference.WebServiceWMS
'                        'Reflag All Customer In Cusmer_Id
'                        WebService.RESET_IsExport(drHeaderData("Customer_Id").ToString, drHeader("SaleUnit").ToString)
'                        ' WebService = New WebReference.WebServiceWMS
'                        'Get New
'                        Dim str As String = WebService.Get_DataCustomer(0, 0, drHeaderData("Customer_Id").ToString, "")
'                        Dim dsXML As New DataSet
'                        dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'                        If dsXML.Tables.Contains("CUSTOMER") Then
'                            Dim c As New Cutomer_Service
'                            c.WebService_Customer(0, 0, drHeaderData("Customer_Id").ToString, "")
'                            dt_cusship = DBExeQuery(Sql)
'                            If dt_cusship.Rows.Count > 0 Then
'                                objSaleOrder.Str3 = dt_cusship.Rows(0).Item("Tax_No").ToString 'เลขเสียภาษี
'                                objSaleOrder.Customer_Shipping_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Index").ToString
'                            Else
'                                Dim xLogDescription = "Not Found Customer in Table WMS:" & str_CusShip & " ms_Customer_Shipping,ms_Customer_Shipping_Location"
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                                Continue For
'                            End If

'                        Else
'                            Dim xLogDescription = "Not Found Customer in Table WMS:" & str_CusShip & " ms_Customer_Shipping,ms_Customer_Shipping_Location"
'                            xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)

'                            Continue For
'                        End If

'                        'Dim xLogDescription = "Not Found BILLTO in Table WMS:" & drHeaderData("BILLTO").ToString & " ms_Customer_Shipping"
'                        'xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                        ''Return False
'                        'Continue For
'                    End If

'                    Sql = "Select Customer_Shipping_Location_Index , Customer_Shipping_Location_Id as SHIPTO  from ms_Customer_Shipping_Location where Customer_Shipping_Location_id = '" + drHeaderData("SHIPTO").ToString + "' and status_id <> -1"
'                    dt_cusship_location = DBExeQuery(Sql)
'                    If dt_cusship_location.Rows.Count > 0 Then
'                        objSaleOrder.Customer_Shipping_Location_Index = dt_cusship_location.Rows(0).Item("Customer_Shipping_Location_Index").ToString
'                    Else
'                        Dim xLogDescription = "Not Found SHIPTO in Table WMS:" & drHeaderData("SHIPTO").ToString & " ms_Customer_Shipping_Location"
'                        xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                        'Return False
'                        Continue For
'                    End If


'                Else

'                    Sql = "select  ms_Customer_Shipping.Customer_Shipping_Index,Customer_Shipping_Location_Index from ms_Customer_Shipping "
'                    Sql &= " inner join ms_Customer_Shipping_Location on ms_Customer_Shipping.Str1 = ms_Customer_Shipping_Location.Customer_Shipping_Location_Id"
'                    Sql &= " where Customer_Shipping_Location_Id = '" & str_CusShip & "'"
'                    dt_cusship = DBExeQuery(Sql)
'                    If dt_cusship.Rows.Count = 0 Then 'ไม่เจอ CusmerShipping
'                        'CusmerShipping NotFlound DataBase
'                        Dim WebService As New WebReference.WebServiceWMS
'                        'Reflag
'                        WebService.RESET_IsExport(drHeaderData("Customer_Id").ToString, drHeader("SaleUnit").ToString)
'                        WebService = New WebReference.WebServiceWMS
'                        'Get New
'                        Dim str As String = WebService.Get_DataCustomer(0, 0, str_CusShip, "")
'                        Dim dsXML As New DataSet
'                        dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'                        If dsXML.Tables.Contains("CUSTOMER") Then
'                            Dim c As New Cutomer_Service
'                            c.WebService_Customer(0, 0, str_CusShip, "")
'                            dt_cusship = DBExeQuery(Sql)
'                            If dt_cusship.Rows.Count > 0 Then

'                                objSaleOrder.Customer_Shipping_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Index").ToString
'                                objSaleOrder.Customer_Shipping_Location_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Location_Index").ToString


'                            Else 'ถ้า Reset_Flag แล้ว ไม่เจอ
'                                Dim xLogDescription = "Not Found Customer_Id in Table WMS:" & str_CusShip & " ms_Customer_Shipping,ms_Customer_Shipping_Location"
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                                'Return False
'                                Continue For
'                            End If
'                        Else
'                            Dim xLogDescription = "Not Found Customer_Id in Interface :" & str_CusShip & " ms_Customer_Shipping,ms_Customer_Shipping_Location"
'                            xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Customer_Shipping,ms_Customer_Shipping_Location", str_CusShip, drHeader("OrderID").ToString, "E", xLogDescription)
'                            'Return False
'                            Continue For
'                        End If
'                    Else
'                        objSaleOrder.Customer_Shipping_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Index").ToString
'                        objSaleOrder.Customer_Shipping_Location_Index = dt_cusship.Rows(0).Item("Customer_Shipping_Location_Index").ToString
'                    End If

'                End If

'                objSaleOrder.Credit_Term = drHeaderData("BillCredit").ToString
'                objSaleOrder.Currency_Index = "0010000000039" 'Fix Thailand , BATH
'                objSaleOrder.PaymentMethod_Index = "" 'Fix
'                objSaleOrder.Amount = drHeaderData("Total")
'                objSaleOrder.Discount_Percent = IIf(String.IsNullOrEmpty(drHeaderData("DISCOUNT_RATE_OL").ToString().Replace("%", "")), 0, drHeaderData("DISCOUNT_RATE_OL").ToString().Replace("%", ""))
'                objSaleOrder.Discount_Amt = drHeaderData("DISCOUNT_AMOUNT")
'                objSaleOrder.Deposit_Amt = 0
'                objSaleOrder.Total_Amt = drHeaderData("Total")
'                objSaleOrder.VAT_Percent = drHeaderData("VATRATE")
'                objSaleOrder.VAT = drHeaderData("VATVALUE")
'                objSaleOrder.Net_Amt = drHeaderData("Money")


'                objSaleOrder.Str1 = drHeaderData("Orderid").ToString.Trim ' drHeaderData("SaleOrder").ToString.Trim 'Me.txtRef1.Text
'                objSaleOrder.Str2 = "" ' drHeaderData("PurchaseOrder").ToString.Trim 'Me.txtRef2.Text
'                objSaleOrder.Str8 = "" ' drHeaderData("WorkOrder ID").ToString.Trim 'Me.txtRef2.Text
'                objSaleOrder.Str3 = "" 'Me.txtTax_No.Text

'                xQuery = String.Format("select * from ms_Customer where Customer_Index = '{0}'", objSaleOrder.Customer_Index)
'                xDt = xObjDB.DBExeQuery(xQuery)
'                If xDt.Rows.Count > 0 Then
'                    objSaleOrder.Str10 = xDt.Rows(0)("Address").ToString
'                    objSaleOrder.Str6 = xDt.Rows(0)("Tel").ToString
'                    objSaleOrder.Str7 = xDt.Rows(0)("Fax").ToString
'                End If
'                'xQuery = String.Format("SELECT (isnull(Address,'') + ' ' + isnull(District,'') +' '+ isnull(Province,'') +' '+ isnull(Postcode,'')) as xAddressShipping_Location,*   from VIEW_MS_Cus_Ship_Location ")
'                ''xQuery &= String.Format(" where status_id  not in (-1) and Customer_Shipping_Index = '{0}' ", objSaleOrder.Customer_Shipping_Index)
'                'xQuery &= String.Format(" where status_id  not in (-1) and Customer_Shipping_Location_Index = '{0}' ", objSaleOrder.Customer_Shipping_Location_Index)

'                xQuery = " SELECT (isnull(Address + ' ' ,'') "
'                xQuery &= " 		 + (case when isnull(Town_index,'') = '0010000000000' then '' else  isnull(Town_Name,'')  + ' ' end)"
'                xQuery &= " 		 + (case when isnull(District_Index,'') = '0010000000000' then '' else  isnull(District,'')  + ' ' end)"
'                xQuery &= " 		 + (case when isnull(Province_Index,'') = '0010000000000' then '' else  isnull(Province,'')  + ' ' end)"
'                xQuery &= " 		+ isnull(Postcode,'')) as xAddressShipping_Location"
'                xQuery &= " 	,*   "
'                xQuery &= " from VIEW_MS_Cus_Ship_Location  "
'                xQuery &= String.Format(" where status_id  not in (-1) and Customer_Shipping_Location_Index = '{0}' ", objSaleOrder.Customer_Shipping_Location_Index)

'                xDt = xObjDB.DBExeQuery(xQuery)
'                If xDt.Rows.Count > 0 Then
'                    objSaleOrder.Str9 = xDt.Rows(0)("xAddressShipping_Location").ToString
'                    objSaleOrder.Str4 = xDt.Rows(0)("Tel").ToString
'                    objSaleOrder.Str5 = xDt.Rows(0)("Fax").ToString
'                    objSaleOrder.SubRoute_Index = xDt.Rows(0)("SubRoute_Index").ToString
'                    objSaleOrder.Route_Index = xDt.Rows(0)("Route_Index").ToString
'                    objSaleOrder.TransportRegion_Index = xDt.Rows(0)("TransportRegion_Index").ToString

'                    If Document_Index.Equals("0010000000018") Or Document_Index.Equals("0020000000106") Then 'SalesOrder OR CreditNote
'                        objSaleOrder.DistributionCenter_Index = xDt.Rows(0)("DistributionCenter_Index").ToString
'                    Else ' TranferOut or GoodIssue
'                        objSaleOrder.DistributionCenter_Index = "0010000000000" 'fix N/A

'                        '*********************** SALE WAREHOUSE *********************** 
'                        'ขาจ่าย  Transfer Out / Goods Issue    ให้สนใจ  From_WarehouseID
'                        If dtDetail.Columns.Contains("From_WarehouseID") Then
'                            If drHeaderData("From_WarehouseID").ToString.Trim().Length > 0 Then
'                                dtCheck = DBExeQuery("select * from ms_MappingWareHouseST_WMS where WAREHOUSEID = '" & drHeaderData("From_WarehouseID").ToString.Trim & "' and isnull(status_id,0) <> -1")
'                                If dtCheck.Rows.Count > 0 Then
'                                    xDistributionCenter_Index = dtCheck.Rows(0).Item("DistributionCenter_Index").ToString.Trim
'                                End If
'                            End If

'                            If xDistributionCenter_Index.Trim.Length > 0 Then
'                                objSaleOrder.DistributionCenter_Index = xDistributionCenter_Index
'                            Else
'                                Dim xLogDescription = "OrderID :" & drHeader("OrderID").ToString
'                                xLogDescription &= ", Sales Order DistributionCenter Is Not To From_WarehouseID :" & drHeaderData("From_WarehouseID").ToString
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Error DistributionCenter OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                                Continue For
'                            End If

'                        End If


'                        '*********************** SALE WAREHOUSE *********************** 
'                    End If
'                End If

'                objSaleOrder.Time_ExpectedDocPickup = Now.ToString("yyyy/MM/dd") ' dtpTime_DocPickup.Value
'                objSaleOrder.chkTime_DocPickup = False
'                objSaleOrder.Time_DocPickup = Now.ToString("yyyy/MM/dd") 'Fix dtpTime_DocPickup.Value
'                objSaleOrder.chkExpected_Delivery_Date = True
'                objSaleOrder.Expected_Delivery_Date = CDate(drHeaderData("DELIVERYDATE").Substring(0, 4) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(4, 2) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(6)).ToString("yyyy/MM/dd")

'                'objSaleOrder.Expected_Delivery_Date = CDate(drHeaderData("DELIVERYDATE").Substring(0, 4) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(4, 2) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(6)).ToString("yyyy/MM/dd")

'                objSaleOrder.Process_Id = 10
'                objSaleOrder.add_by = drHeaderData("APPROVED_BY").ToString
'                objSaleOrder.Status = 1
'                objSaleOrder.Reserve_index = ""
'                objSaleOrder.District_Index = ""
'                objSaleOrder.Province_Index = ""
'                objSaleOrder.VehicleType_Index = ""
'                objSaleOrder.PODRemark1 = ""
'                objSaleOrder.PODDoc_Copy1 = ""
'                objSaleOrder.PODDoc_Copy2 = ""
'                objSaleOrder.PODDoc_Copy3 = ""
'                objSaleOrder.PODDoc_Copy4 = ""
'                objSaleOrder.PODDoc_Copy5 = ""
'                objSaleOrder.GRRemark1 = ""
'                objSaleOrder.GRDoc_Copy1 = ""
'                objSaleOrder.GRDoc_Copy2 = ""
'                objSaleOrder.GRDoc_Copy3 = ""
'                objSaleOrder.GRDoc_Copy4 = ""
'                objSaleOrder.GRDoc_Copy5 = ""
'                objSaleOrder.Remark = drHeaderData("Remark").ToString

'                objSaleOrderItemCollection = New List(Of tb_SalesOrderItem)
'                Dim iItem As Integer = 0
'                Dim dr_Detail() As DataRow = dtDetail.Select(String.Format("OrderID = '{0}'", drHeader("OrderID").ToString.Trim))
'                For Each drRow As DataRow In dr_Detail
'                    iItem = iItem + 1
'                    objSaleOrderItem = New tb_SalesOrderItem
'                    objSaleOrderItem.SalesOrder_Index = objSaleOrder.SalesOrder_Index
'                    objSaleOrderItem.SalesOrderItem_Index = objDBIndex.getSys_Value("SalesOrderItem_Index")
'                    objSaleOrderItem.Item_Seq = drRow("no")
'                    'SKU
'                    objSaleOrderItem.Qty = CDbl(IIf(drRow("Quantity").ToString.Length = 0, 0, drRow("Quantity")))
'                    xQuery = "select S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
'                    xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
'                    xQuery &= " from ms_SKURatio R"
'                    xQuery &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index  and ISNULL(P.status_id,0) <> -1 "
'                    xQuery &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
'                    xQuery &= " 	left outer join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
'                    xQuery &= " Where S.Sku_Id = '" & drRow("ProductID").ToString.Trim & "' and  S.status_id <> -1"
'                    xQuery &= " and P.Package_Id = '" & drRow("Unit").ToString.Trim & "'" '  xQuery &= " and P.Description = '" & drRow("Unit").ToString.Trim & "'"
'                    xQuery &= " Order by S.Sku_Id,R.Ratio"
'                    xDt = xObjDB.DBExeQuery(xQuery)
'                    ' ถ้าไม่เจอให้ส่งค่าไปReset Flage ก่อน 1 ครั้งแล้ว ค่อยแมพใหม่
'                    If Not xDt.Rows.Count > 0 Then
'                        'CusmerShipping NotFlound DataBase
'                        Dim WebService As New WebReference.WebServiceWMS
'                        WebService = New WebReference.WebServiceWMS
'                        WebService.RESET_FLAG_PRODUCT(drRow("ProductID").ToString.Trim)
'                        Dim str = WebService.Get_DataProduct(0, 0, drRow("ProductID").ToString.Trim, "")
'                        Dim dsXML As New DataSet
'                        dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)

'                        If dsXML.Tables.Contains("PRODUCT") Then
'                            Dim P As New Product_Service
'                            P.WebService_Product(0, 0, drRow("ProductID").ToString.Trim, "")
'                            xDt = xObjDB.DBExeQuery(xQuery) 'ค้าหาProductIDอีกรอบ
'                            If xDt.Rows.Count > 0 Then
'                                objSaleOrderItem.Sku_Index = xDt.Rows(0)("Sku_Index").ToString
'                                objSaleOrderItem.Package_Index = xDt.Rows(0)("Package_Index").ToString
'                                objSaleOrderItem.Ratio = xDt.Rows(0)("Ratio")
'                                objSaleOrderItem.Total_Qty = objSaleOrderItem.Qty * objSaleOrderItem.Ratio
'                                objSaleOrderItem.Weight = objSaleOrderItem.Qty * xDt.Rows(0)("Weight")
'                                objSaleOrderItem.Volume = objSaleOrderItem.Qty * xDt.Rows(0)("DimensionM3")
'                            Else
'                                dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'                                'Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO")
'                                Dim xLogDescription = "Auto insert Error Not Flound Sku_id : " & drRow("ProductID").ToString.Trim
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Sku", drRow("ProductID").ToString.Trim, objSaleOrder.SalesOrder_No, "E", xLogDescription)
'                                Continue For
'                                'Return False
'                            End If

'                        End If
'                    Else
'                        objSaleOrderItem.Sku_Index = xDt.Rows(0)("Sku_Index").ToString
'                        objSaleOrderItem.Package_Index = xDt.Rows(0)("Package_Index").ToString
'                        objSaleOrderItem.Ratio = xDt.Rows(0)("Ratio")
'                        objSaleOrderItem.Total_Qty = objSaleOrderItem.Qty * objSaleOrderItem.Ratio
'                        objSaleOrderItem.Weight = objSaleOrderItem.Qty * xDt.Rows(0)("Weight")
'                        objSaleOrderItem.Volume = objSaleOrderItem.Qty * xDt.Rows(0)("DimensionM3")
'                    End If


'                    objSaleOrderItem.UnitPrice = drRow("price")
'                    objSaleOrderItem.Amount = drRow("TotalDetail")
'                    objSaleOrderItem.Discount_Amt = drRow("discount")
'                    objSaleOrderItem.Remark = "Import Interface"
'                    objSaleOrderItem.Itemstatus_index = "0010000000001" 'Fix ,GOOD สินค้าดี/พร้อมขาย
'                    objSaleOrderItem.Plot = ""
'                    objSaleOrderItem.ERP_location = ""
'                    objSaleOrderItemCollection.Add(objSaleOrderItem)
'                Next

'                '-----------------------------------------------
'                'Add-On 2018.05.25
'                'Check num record,countrow
'                If dtDetail.Columns.Contains("countrow") Then
'                    If CInt(drDetail(0)("countrow")) <> objSaleOrderItemCollection.Count Then
'                        'Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO")
'                        Dim xLogDescription = "OrderID :" & drHeader("OrderID").ToString
'                        xLogDescription &= ", Sales Order countrow ST is :" & drDetail(0)("countrow").ToString & " , WMS Record before insert : " & objSaleOrderItemCollection.Count.ToString
'                        xLOG.KSL_SY_LOG_INTERFACE_ST("Error Record OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                        'Return False
'                        Continue For
'                    End If
'                End If
'                '-----------------------------------------------


'                Dim objDBPOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.ADDNEW, objSaleOrder, objSaleOrderItemCollection)
'                If Not Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO") Then
'                    Return False
'                    Exit Function
'                End If
'                Try
'                    Dim tSalesOrder_Index As String = objDBPOTransaction.SaveData

'                    '***********************************************************************
'                    If dtDetail.Columns.Contains("PONO") Then
'                        'KSL Uddate auther column
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set Interface_No = '{0}',PO_No = '{2}' where SalesOrder_Index = '{1}'", drHeader("OrderID").ToString, tSalesOrder_Index, drHeaderData("PONO").ToString))
'                    Else
'                        'KSL Uddate auther column
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set Interface_No = '{0}' where SalesOrder_Index = '{1}'", drHeader("OrderID").ToString, tSalesOrder_Index))
'                    End If

'                    If Document_Index = "0010000000018" Then
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set SalesAreaCode = '{0}' ,Discount_Percent = '{2}'  ,SaleType = '{3}'  where SalesOrder_Index = '{1}'", drHeaderData("SalesAreaCode"), tSalesOrder_Index, objSaleOrder.Discount_Percent, drHeaderData("SaleType").ToString))
'                    Else
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set SalesAreaCode = '{0}' ,Discount_Percent = '{2}'   where SalesOrder_Index = '{1}'", drHeaderData("SalesAreaCode"), tSalesOrder_Index, objSaleOrder.Discount_Percent))
'                    End If
'                    If dtDetail.Columns.Contains("SaleCode") Then
'                        'KSL Uddate auther column
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set SaleCode = '{0}' where SalesOrder_Index = '{1}'", drHeaderData("SaleCode").ToString, tSalesOrder_Index))
'                    End If
'                    If dtDetail.Columns.Contains("SaleName") Then
'                        'KSL Uddate auther column
'                        DBExeNonQuery(String.Format("Update tb_SalesOrder set SaleName = '{0}' where SalesOrder_Index = '{1}'", drHeaderData("SaleName").ToString, tSalesOrder_Index))
'                    End If

'                    '***********************************************************************


'                    'RGB Reset color
'                    'Me.getToltalSum(tSalesOrder_Index, objSaleOrder.Customer_Index)

'                    xSalesOrder_Index.Add(tSalesOrder_Index)
'                    Dim i As Integer = 0
'                    'If dtDetail.Columns.Contains("WOI ID") Then
'                    For Each dr As DataRow In dr_Detail
'                        Dim strSQL As String = ""
'                        'strSQL = "update tb_SalesOrderItem set WOI_ID = '" & dr("WOI ID") & "' where SalesOrderItem_Index = '" & objSaleOrderItemCollection.Item(i).SalesOrderItem_Index.ToString & "'"
'                        strSQL = "update tb_SalesOrderItem set Discount_Rate = '" & dr("Discount_Rate") & "' where SalesOrderItem_Index = '" & objSaleOrderItemCollection.Item(i).SalesOrderItem_Index.ToString & "'"
'                        xObjDB.DBExeNonQuery(strSQL)
'                        i += 1
'                    Next
'                    'End If
'                    If Not insert_SalesUnit("tb_SalesOrder", "SalesOrder_Index", tSalesOrder_Index, drHeader("SaleUnit").ToString) Then
'                        Exit Function
'                    End If
'                    Dim xLogDescription = "Auto insert Salse Order : " & objSaleOrder.SalesOrder_No
'                    xLOG.KSL_SY_LOG_INTERFACE_ST("tb_SalesOrder,tb_SaleOrderItem", tSalesOrder_Index, objSaleOrder.SalesOrder_No, "S", xLogDescription)

'                Catch exx As Exception
'                    For Each iSalesOrder_Index As String In xSalesOrder_Index
'                        xQuery = String.Format("update tb_SalesOrder set Status = -1 where SalesOrder_Index = '{0}'", iSalesOrder_Index)
'                        xObjDB.DBExeNonQuery(xQuery)
'                    Next
'                    Throw exx
'                End Try
'                'End Save

'                objDBPOTransaction = Nothing
'                objSaleOrder = Nothing
'                objSaleOrderItemCollection = Nothing
'                'Next for header
'            Next

'            Return True
'        Catch ex As Exception
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", Header_No, "", "E", xLogDescription)
'            Return False

'        End Try

'    End Function


'    Public Function getToltalSum(ByVal SalesOrder_index As String, ByVal Customer_index As String) As Boolean

'        ' *** define value ***
'        Dim strSQL As String = ""
'        Try
'            Dim _dt_TotalQTY As New DataTable
'            strSQL = "  Select SUM(Total_Qty) AS Total_Qty,sku_index from tb_salesOrderItem "
'            strSQL &= "  inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index"
'            strSQL &= "  where tb_salesOrder.SalesOrder_Index = '" & SalesOrder_index & "'"
'            strSQL &= "  GROUP BY sku_index"
'            _dt_TotalQTY = DBExeQuery(strSQL)

'            Dim _dt_Qty_Bal As New DataTable
'            strSQL = "select * from VIEW_KSL_SalesOrderCheckStock  where "
'            strSQL &= " Customer_Index= '" & Customer_index & "' "
'            strSQL &= " and  sku_index in( Select sku_index from tb_salesOrderItem "
'            strSQL &= " inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index"
'            strSQL &= " where tb_salesOrder.SalesOrder_Index = '" & SalesOrder_index & "'"
'            strSQL &= " GROUP BY sku_index)"
'            _dt_Qty_Bal = DBExeQuery(strSQL)
'            Dim Result As Integer = 0
'            If _dt_Qty_Bal.Rows.Count = 0 Then
'                Result += 1
'            Else
'                If _dt_TotalQTY.Rows.Count = _dt_Qty_Bal.Rows.Count Then
'                    For i As Integer = 0 To _dt_TotalQTY.Rows.Count - 1
'                        If _dt_TotalQTY.Rows(i).Item("Total_Qty") > _dt_Qty_Bal.Rows(i).Item("Qty_Bal") Then
'                            Result += 1

'                            'แดง
'                            DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 2 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_index, _dt_Qty_Bal.Rows(i).Item("Sku_Index")))
'                        Else
'                            'ไม่สี
'                            DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 0 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_index, _dt_Qty_Bal.Rows(i).Item("Sku_Index")))

'                        End If
'                    Next
'                Else
'                    Result += 1
'                End If


'            End If

'            If Result = 0 Then
'                'เขียว
'                DBExeNonQuery("Update tb_salesorder set RGB_Check = '1' where SalesOrder_Index = '" & SalesOrder_index & "'")
'                Return True
'            Else
'                'แดง
'                DBExeNonQuery("Update tb_salesorder set RGB_Check = '2' where SalesOrder_Index = '" & SalesOrder_index & "'")
'                Return False
'            End If

'        Catch ex As Exception
'            Throw ex
'        Finally
'            disconnectDB()
'        End Try
'    End Function

'    Function insert_SalesUnit(ByVal xTables As String, ByVal xTable_Colum As String, ByVal xTable_Index As String, ByVal xSalesUnit As String) As Boolean
'        Try
'            Dim strSQL As String = ""
'            strSQL = " Update " & xTables & " set  SalesUnit = '" & xSalesUnit & "' Where " & xTable_Colum & " = '" & xTable_Index & "' "
'            DBExeNonQuery(strSQL)
'            Return DBExeNonQuery(strSQL)
'        Catch ex As Exception
'            Return ex.Message.ToString
'        End Try
'    End Function
'    Function Update_SalesOrder(ByVal OrderId As String, ByVal SalesUnit As String, ByVal CheckUpdate As String) As Boolean
'        Dim WebService As New WebReference.WebServiceWMS
'        Dim str As String
'        Dim xLogDescription As String
'        Dim xStatus As String = ""
'        Try
'            str = WebService.Update_TFLAG(OrderId.ToString, SalesUnit.ToString)
'            If str.ToUpper = "SUCCESS" Then
'                xStatus = "S"
'            Else
'                xStatus = "E"
'            End If

'            'log
'            If CheckUpdate = "SO" Then
'                xLogDescription = "Auto Update TFLAG : " & str
'                xLOG.KSL_SY_LOG_INTERFACE_ST("tb_SalesOrder", OrderId, SalesUnit, xStatus, xLogDescription)
'            Else
'                xLogDescription = "Auto Update TFLAG : " & str
'                xLOG.KSL_SY_LOG_INTERFACE_ST("tb_PurchaseOrder", OrderId, SalesUnit, xStatus, xLogDescription)
'            End If

'            Return True
'        Catch ex As Exception
'            'log 
'            If CheckUpdate = "SO" Then
'                xLogDescription = "Auto Update TFLAG : " & ex.Message
'                xLOG.KSL_SY_LOG_INTERFACE_ST("tb_SalesOrder", OrderId, SalesUnit, "E", xLogDescription)
'            Else
'                xLogDescription = "Auto Update TFLAG : " & ex.Message
'                xLOG.KSL_SY_LOG_INTERFACE_ST("tb_PurchaseOrder", OrderId, SalesUnit, "E", xLogDescription)
'            End If

'            Return False
'        End Try

'    End Function
'End Class


'Public Class Purchase_Service : Inherits DBType_SQLServer
'    Private _dataTable As DataTable = New DataTable
'    Private _scalarOutput As String
'    Private xLOG As New Interface_Log_Service
'    Private SO As New SalseOrder_Service

'    Dim objDocumentNumber As New Sy_AutoNumber

'    Function WebService_TransferIn(Optional ByVal OrderID As String = "" _
'     , Optional ByVal SaleUnit As String = "" _
'     , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_GoodsTransferIn(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : TransferIn"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            'Convert datatable to dataview for grouping
'            Dim dv As New DataView(dsXML.Tables(0))

'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})
'            If Not Me.InsertToPurchaseOrder(dsXML.Tables(0), dtGroup, "0020000000502") Then
'                Return "E"
'                Exit Function
'            End If

'            'Pause.
'            Return "S"
'        Catch ex As Exception

'            Return ex.Message.ToString
'        End Try

'    End Function

'    Function WebService_Return(Optional ByVal OrderID As String = "" _
'  , Optional ByVal SaleUnit As String = "" _
'  , Optional ByVal LastUpdate_yyyyMMdd As String = "") As String
'        Try
'            'Validate
'            If String.IsNullOrEmpty(LastUpdate_yyyyMMdd) Then
'                'LastUpdate_yyyyMMdd = Now.ToString("yyyyMMdd")
'            Else
'                Dim xDate As Date = Date.ParseExact(LastUpdate_yyyyMMdd, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
'                If Not IsDate(xDate) Then
'                    Return "Error LastUpdate :" & LastUpdate_yyyyMMdd.ToString
'                End If
'            End If

'            Dim WebService As New WebReference.WebServiceWMS
'            Dim str As String = WebService.Get_Return(OrderID.ToString, SaleUnit.ToString, LastUpdate_yyyyMMdd.ToString)
'            Dim dsXML As New DataSet
'            'Json convert to dataset           
'            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'            'Convert datatable to dataview for grouping
'            If dsXML.Tables(0).Rows.Count = 0 Then
'                'Dim xLogDescription As String = "Not Flound data : Return"
'                'xLOG.KSL_SY_LOG_INTERFACE_ST("Import not success", OrderID, "", "E", xLogDescription)
'                Return "E"
'                Exit Function
'            End If
'            Dim dv As New DataView(dsXML.Tables(0))
'            'getting distinct values for group column
'            Dim dtGroup As DataTable = dv.ToTable(True, New String() {"OrderID"})
'            If Not Me.InsertToPurchaseOrder(dsXML.Tables(0), dtGroup, "0020000000503") Then
'                Return "E"
'                Exit Function
'            End If
'            'Pause.
'            Return "S"
'        Catch ex As Exception
'            'log
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", "", "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function

'    Private Function InsertToPurchaseOrder(ByVal dataDetail As DataTable, ByVal dataHeader As DataTable, ByVal DocumentType_index As String) As Boolean

'        ' TODO: HARDCODE-MSG
'        Dim Header_NO As String = ""
'        Dim i As Integer = 0
'        Dim j As Integer = 0
'        Dim k As Integer = 0
'        Dim strTempSKUIndex = ""
'        Dim strTempPackageIndex = ""
'        Dim dblSKURatio As Double = 1
'        Dim xQuery As String = ""
'        Dim xDt As New DataTable
'        Dim xObjDB As New DBType_SQLServer

'        WV_UserName = "admin"

'        ' ------ STEP 1: Declare Data Table Objects

'        Dim objtb_PurchaseOrderItemCollection As New List(Of tb_PurchaseOrderItem)
'        Dim objtb_PurchaseOrder_Discount As New tb_PurchaseOrder_Discount
'        Dim objtb_PurchaseOrder_DiscountItem As New List(Of tb_PurchaseOrder_Discount)
'        Dim objtb_PurchaseOrderOtherCollection As New List(Of tb_PurchaseOrderOther)
'        dataHeader.Columns.Add("SaleUnit")

'        Try
'            Dim xLogDescription As String = ""
'            For Each drHeader As DataRow In dataHeader.Rows
'                objtb_PurchaseOrderItemCollection = New List(Of tb_PurchaseOrderItem)
'                Dim dt_cusship, dt_cus As New DataTable
'                Dim drDetail() As DataRow = dataDetail.Select(String.Format("OrderID = '{0}'", drHeader("OrderID").ToString.Trim))
'                Dim drHeaderData As DataRow = drDetail(0)
'                Dim dtCheck As New DataTable
'                drHeader("SaleUnit") = drHeaderData("SaleUnit").ToString.Trim
'                Header_NO = drHeader("OrderID").ToString.Trim
'                dtCheck = DBExeQuery("select * from tb_PurchaseOrder where PurchaseOrder_No = '" & drHeader("OrderID").ToString.Trim & "' and status <> -1")
'                If dtCheck.Rows.Count > 0 Then
'                    'Order Id ซ้ำ
'                    SO.Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "PO")
'                    xLogDescription = "PurchaseOrderID Duplicate: " & drHeader("OrderID").ToString
'                    xLOG.KSL_SY_LOG_INTERFACE_ST("Auto Update FLAG OrderID", "", drHeader("OrderID").ToString, "E", xLogDescription)
'                    Continue For

'                End If


'                '----------------------------------------------------------------------------------------------------------------
'                'Add-On 2018.05.25
'                'Check num record,countrow
'                If dataDetail.Columns.Contains("countrow") Then
'                    If drDetail.Length <> CInt(drDetail(0)("countrow")) Then
'                        'SO.Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "PO")
'                        xLogDescription = "OrderID :" & drHeader("OrderID").ToString
'                        xLogDescription &= ", Purchase Order countrow ST is :" & dataDetail.Rows(0)("countrow").ToString & " , WMS Receipt : " & drDetail.Length.ToString
'                        xLOG.KSL_SY_LOG_INTERFACE_ST("Error Record OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                        'Return False
'                        Continue For
'                    End If
'                End If
'                '----------------------------------------------------------------------------------------------------------------


'                'Add on
'                '*********************** SALE WAREHOUSE *********************** 
'                'ขารับ  Transfer In  / Goods Return       ให้สนใจ  To_WarehouseID
'                Dim xDistributionCenter_Index As String = ""
'                Select Case DocumentType_index
'                    Case "0020000000502", "0020000000503" 'ขารับ  Transfer In  / Goods Return 
'                        If dataDetail.Columns.Contains("WarehouseID") Then
'                            If drHeaderData("WarehouseID").ToString.Trim().Length > 0 Then
'                                dtCheck = DBExeQuery("select * from ms_MappingWareHouseST_WMS where WAREHOUSEID = '" & drHeaderData("WarehouseID").ToString.Trim & "' and isnull(status_id,0) <> -1")
'                                If dtCheck.Rows.Count > 0 Then
'                                    xDistributionCenter_Index = dtCheck.Rows(0).Item("DistributionCenter_Index").ToString.Trim
'                                End If
'                            End If


'                            If xDistributionCenter_Index.Length > 0 Then
'                                Dim xobj As New DBType_SQLServer
'                                xobj.DBExeNonQuery(String.Format("update tb_PurchaseOrder set DistributionCenter_Index = '{0}' ", dtCheck.Rows(0).Item("DistributionCenter_Index").ToString))
'                            Else
'                                xLogDescription = "DistributionCenter Is Not To From_WarehouseID :" & drHeaderData("WarehouseID").ToString.Trim
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Error DistributionCenter OrderID", "", drHeader("OrderID").ToString, "E", xLogDescription)
'                                Continue For
'                            End If
'                        End If



'                End Select

'                '*********************** SALE WAREHOUSE *********************** 


'                Dim str_CusShip As String = IIf(drHeaderData("Customer_Id").ToString = Nothing, -1, drHeaderData("Customer_Id"))
'                Dim str_Cus As String = IIf(drHeaderData("OWNER").ToString = Nothing, -1, drHeaderData("OWNER").ToString)
'                dt_cus = DBExeQuery("Select * from ms_Customer where Customer_id = '" & str_Cus & "'")
'                'Dim Sql As String = ""
'                'Sql = "select ms_Customer_Shipping.Customer_Shipping_Index,Customer_Shipping_Location_Index from ms_Customer_Shipping "
'                'Sql &= " inner join ms_Customer_Shipping_Location on ms_Customer_Shipping.Str1 = ms_Customer_Shipping_Location.Customer_Shipping_Location_Id"
'                'Sql &= " where Customer_Shipping_Location_Id = '" & str_CusShip & "'"
'                'dt_cusship = DBExeQuery(Sql)


'                ' ------ STEP 2: Prepare values for PO Header
'                Dim obj As New Sy_AutoNumber
'                Dim PurchaseOrder_Index As String = obj.getSys_Value("PurchaseOrder_Index")
'                ' obj = Nothing
'                drHeader("SaleUnit") = drHeaderData("SaleUnit").ToString.Trim
'                Dim objtb_PurchaseOrder As New tb_PurchaseOrder
'                With objtb_PurchaseOrder
'                    .PurchaseOrder_Index = PurchaseOrder_Index
'                    .DocumentType_Index = DocumentType_index '"0010000000502" 'Transfer IN
'                    .PurchaseOrder_No = drHeaderData("OrderID").ToString.Trim
'                    .PurchaseOrder_Date = CDate(drHeaderData("OrderDate").ToString.Substring(0, 4) & "/" & drHeaderData("OrderDate").ToString.Substring(4, 2) & "/" & drHeaderData("OrderDate").ToString.Substring(6)).ToString("yyyy/MM/dd")
'                    .Carrier_Index = ""
'                    .Customer_Receive_Location_Index = ""
'                    .Expected_Delivery_Date = CDate(drHeaderData("DELIVERYDATE").Substring(0, 4) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(4, 2) & "/" & drHeaderData("DELIVERYDATE").ToString.Substring(6)).ToString("yyyy/MM/dd")
'                    .Delivery_Date = Now.ToString("yyyy/MM/dd")
'                    .Customer_Index = dt_cus.Rows(0).Item("Customer_Index").ToString
'                    .Supplier_Index = "0010000000000" ' ฟิกไปก่อน บจก. ไทยเครื่องชั่ง
'                    .Department_Index = ""
'                    .Remark = "Import Interface"
'                    .Credit_Term = CDbl(IIf(drHeaderData("BillCredit").ToString.Length = 0, 0, drHeaderData("BillCredit").ToString))
'                    .Exchange_Rate = 1
'                    .PaymentMethod_Index = ""
'                    .Currency_Index = "0010000000039"

'                    'objtb_PurchaseOrder.Payment_Ref = ""
'                    'objtb_PurchaseOrder.FullPaid_Date = Date.Now
'                    xQuery = String.Format("select * from ms_Customer where Customer_Index = '{0}'", objtb_PurchaseOrder.Customer_Index)
'                    xDt = xObjDB.DBExeQuery(xQuery)
'                    If xDt.Rows.Count > 0 Then
'                        .Str9 = xDt.Rows(0)("Address").ToString 'Me.txtShip_Address.Text.ToString.Trim
'                        .Str4 = xDt.Rows(0)("Tel").ToString 'Me.txtShip_Phone.Text.ToString.Trim
'                        .Str5 = xDt.Rows(0)("Fax").ToString 'Me.txtShip_Fax.Text.ToString.Trim

'                    End If

'                    .Amount = drHeaderData("Total")
'                    .Discount_Percent = 0
'                    .Discount_Amt = 0
'                    .Deposit_Amt = 0
'                    .Total_Amt = 0
'                    .VAT_Percent = drHeaderData("VATRATE")
'                    .VAT = drHeaderData("VATVALUE")
'                    .Net_Amt = drHeaderData("Money")
'                    .Supplier_Address = ""
'                    .Supplier_Tel = ""
'                    .Supplier_Fax = ""
'                    .Str1 = ""
'                    .Str2 = ""
'                    .Str3 = "" 'เลขประจำตัวเสียภาษี
'                    ' Note we use field Str9 to store Ship Address because the field length is 2000.

'                    .add_by = WV_UserName ' Me.txtUser.Text
'                    .Status = 1
'                End With
'                'If Status = 2 Then
'                '    objtb_PurchaseOrder.Status = 2
'                'Else
'                '    objtb_PurchaseOrder.Status = 1
'                'End If
'                If Not SO.Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "PO") Then
'                    Return False
'                    Continue For
'                End If
'                Dim iItem As Integer = 0

'                ' ------ STEP 3: Prepare values for PO Items
'                For Each drRow As DataRow In drDetail
'                    iItem = iItem + 1
'                    Dim objtb_PurchaseOrderItem As New tb_PurchaseOrderItem
'                    With objtb_PurchaseOrderItem
'                        Dim obj_V2 As New Sy_AutoNumber
'                        .PurchaseOrderItem_Index = obj_V2.getSys_Value("PurchaseOrderItem_Index")
'                        .PurchaseOrder_Index = objtb_PurchaseOrder.PurchaseOrder_Index

'                        objtb_PurchaseOrderItem.Item_Seq = drRow("no")
'                        .Qty = CDbl(IIf(drRow("Quantity").ToString.Length = 0, 0, drRow("Quantity")))
'                        xQuery = "select S.Sku_Index, S.Sku_Id,R.SkuRatio_Index, R.Ratio,T.Description as DimensionType,T.Ratio as Dimension"
'                        xQuery &= " 		,(Dimension_Hi*Dimension_Wd*Dimension_Len)/T.Ratio as DimensionM3,P.*"
'                        xQuery &= " from ms_SKURatio R"
'                        xQuery &= " 	inner join ms_Package P ON P.Package_Index = R.Package_Index"
'                        xQuery &= " 	inner join ms_SKU S ON S.Sku_Index = R.Sku_Index"
'                        xQuery &= " 	inner join ms_DimensionType T ON T.DimensionType_Index = P.DimensionType_Index"
'                        xQuery &= " Where S.Sku_Id = '" & drRow("ProductID").ToString.Trim & "'"
'                        xQuery &= " and P.Package_Id = '" & drRow("Unit").ToString.Trim & "'"
'                        xQuery &= " Order by S.Sku_Id,R.Ratio"
'                        xDt = xObjDB.DBExeQuery(xQuery)
'                        If xDt.Rows.Count = 0 Then
'                            'Add here
'                            'CusmerShipping NotFlound DataBase
'                            Dim WebService As New WebReference.WebServiceWMS
'                            Dim str As String = WebService.Get_DataProduct(0, 0, drRow("ProductID").ToString.Trim, "")
'                            Dim dsXML As New DataSet
'                            dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
'                            If dsXML.Tables.Contains("PRODUCT") Then
'                                Dim P As New Product_Service
'                                P.WebService_Product(0, 0, drRow("ProductID").ToString.Trim, "")
'                                xDt = xObjDB.DBExeQuery(xQuery)
'                            Else
'                                'Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "SO")
'                                xLogDescription = "Auto insert Error Not Flound Sku_id : " & drRow("ProductID").ToString.Trim
'                                xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Sku", drRow("ProductID").ToString.Trim, objtb_PurchaseOrder.PurchaseOrder_No, "E", xLogDescription)
'                                Continue For
'                                'Return False
'                            End If
'                        End If
'                        If xDt.Rows.Count > 0 Then
'                            .Sku_Index = xDt.Rows(0)("Sku_Index").ToString
'                            .Package_Index = xDt.Rows(0)("Package_Index").ToString
'                            .Ratio = xDt.Rows(0)("Ratio")
'                            .Total_Qty = .Qty * .Ratio
'                            .Weight = .Qty * xDt.Rows(0)("Weight")
'                            .Volume = .Qty * xDt.Rows(0)("DimensionM3")
'                        Else
'                            xLogDescription = "Auto insert Error Not Flound Sku_id : " & drRow("ProductID").ToString.Trim
'                            xLOG.KSL_SY_LOG_INTERFACE_ST("Please Add To ms_Sku", drRow("ProductID").ToString.Trim, objtb_PurchaseOrder.PurchaseOrder_No, "E", xLogDescription)
'                            Continue For
'                            'Return False
'                        End If
'                        '  dblSKURatio = getSKU_PackageRatio(strTempSKUIndex, strTempPackageIndex)

'                        .UnitPrice = drRow("price")
'                        .Amount = drRow("TotalDetail")
'                        .Discount_Amt = drRow("discount")
'                        '''''''insert เปอร์เซ็นต์เกิน
'                        .Percent_Over_Allow = 0
'                        .Total_Amt = objtb_PurchaseOrderItem.Amount - objtb_PurchaseOrderItem.Discount_Amt
'                        .Currency_Index = "0010000000039" 'Fix Thailand , BATH
'                        .Remark = "Import Interface"

'                        objtb_PurchaseOrderItemCollection.Add(objtb_PurchaseOrderItem)
'                    End With
'                    objtb_PurchaseOrder_Discount = New tb_PurchaseOrder_Discount

'                    'objtb_PurchaseOrder_Discount.PurchaseOrder_Index = objtb_PurchaseOrder.PurchaseOrder_Index
'                    'objtb_PurchaseOrder_Discount.Discount_Seq = dtDiscount.Rows(j).Item("Discount_Seq").ToString
'                    'objtb_PurchaseOrder_Discount.Discount_Type = dtDiscount.Rows(j).Item("Discount_Type").ToString
'                    'objtb_PurchaseOrder_Discount.Discount_Amount1 = dtDiscount.Rows(j).Item("Discount_Amount1").ToString
'                    'objtb_PurchaseOrder_Discount.Discount_Amount2 = dtDiscount.Rows(j).Item("Discount_Amount2").ToString
'                    'objtb_PurchaseOrder_Discount.Discount_Total = dtDiscount.Rows(j).Item("Discount_Total").ToString
'                    'objtb_PurchaseOrder_DiscountItem.Add(objtb_PurchaseOrder_Discount)


'                    ' ------ STEP 5: Prepare values for tb_PurchaseOrderOther
'                    Dim objtb_PurchaseOrderOther As New tb_PurchaseOrderOther
'                    objtb_PurchaseOrderOther = New tb_PurchaseOrderOther


'                Next


'                '-----------------------------------------------
'                'Add-On 2018.05.25
'                'Check num record,countrow
'                If dataDetail.Columns.Contains("countrow") Then
'                    If CInt(dataDetail.Rows(0)("countrow")) <> objtb_PurchaseOrderItemCollection.Count Then
'                        'SO.Update_SalesOrder(drHeader("OrderID").ToString, drHeader("SaleUnit").ToString, "PO")
'                        xLogDescription = "OrderID :" & drHeader("OrderID").ToString
'                        xLogDescription &= ", Purchase Order countrow ST is :" & dataDetail.Rows(0)("countrow").ToString & " , WMS Record before insert : " & objtb_PurchaseOrderItemCollection.Count.ToString
'                        xLOG.KSL_SY_LOG_INTERFACE_ST("Error Record OrderID", drHeader("OrderID").ToString, "", "E", xLogDescription)
'                        'Return False
'                        Continue For
'                    End If
'                End If
'                '-----------------------------------------------


'                Dim objDBPOTransaction As New POTransaction(POTransaction.enuOperation_Type.ADDNEW, objtb_PurchaseOrder, objtb_PurchaseOrderItemCollection, objtb_PurchaseOrder_DiscountItem, objtb_PurchaseOrderOtherCollection)
'                Dim PurchaseOreder_index As String = objDBPOTransaction.SaveData
'                'objtb_PurchaseOrderItem = Nothing 
'                If Not SO.insert_SalesUnit("tb_PurchaseOrder", "PurchaseOrder_Index", PurchaseOreder_index, drHeader("SaleUnit").ToString) Then
'                    Exit Function
'                End If
'                xLogDescription &= "Auto insert PurchaseOrder_NO : " & objtb_PurchaseOrder.PurchaseOrder_No
'                xLOG.KSL_SY_LOG_INTERFACE_ST("tb_PurchaseOrder,tb_PurchaseOrderItem", objtb_PurchaseOrder.PurchaseOrder_Index, objtb_PurchaseOrder.PurchaseOrder_No, "S", xLogDescription)
'                objDBPOTransaction = Nothing
'                objtb_PurchaseOrder = Nothing
'                objtb_PurchaseOrderItemCollection = Nothing

'            Next
'            ' If save succeeded, _PurchaseOrder_Index will not be ""
'            'End Save

'            Return True
'        Catch ex As Exception
'            Dim xLogDescription As String = "System main error" & ", Error : " & ex.Message.ToString
'            xLOG.KSL_SY_LOG_INTERFACE_ST("System", Header_NO, "", "E", xLogDescription)

'            Return ex.Message.ToString
'        End Try

'    End Function




'End Class


Public Class Interface_Log_Service : Inherits DBType_SQLServer

    Public Sub KSL_SY_LOG_INTERFACE_ST(ByVal Type As String, ByVal Type_Id As String, ByVal Type_Index As String, ByVal Status As String, ByVal Description As String)
        Try
            Dim xSQLL As String = ""
            xSQLL = "INSERT INTO KSL_SY_LOG_INTERFACE_ST(Type,Type_Id,Type_Index,Status,Description,HostName)"
            xSQLL &= String.Format(" VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", Type, Type_Id, Type_Index, Status, Description.Replace("'", ""), WV_Host_Name) ' Jen "S" --> Status
            DBExeQuery(xSQLL)

        Catch ex As Exception
            Dim xSQLL As String = ""
            xSQLL = "INSERT INTO KSL_SY_LOG_INTERFACE_ST(Type,Type_Id,Type_Index,Status,Description,HostName)"
            xSQLL &= String.Format(" VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", Type, Type_Id, Type_Index, "E", Description.Replace("'", ""), WV_Host_Name)
            DBExeQuery(xSQLL)
            'Throw ex
        End Try
    End Sub

End Class

