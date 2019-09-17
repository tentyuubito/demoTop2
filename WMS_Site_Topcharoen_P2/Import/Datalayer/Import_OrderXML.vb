Imports System.Xml.XPath
Imports System.Xml
Imports WMS_STD_INB_Receive_Datalayer

Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Public Class Import_OrderXML : Inherits DBType_SQLServer

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _USE_BRANCH_ID As String

    Public Property USE_BRANCH_ID() As String
        Get
            Return _USE_BRANCH_ID
        End Get
        Set(ByVal value As String)
            _USE_BRANCH_ID = value
        End Set
    End Property

    Public Function StartImport_OrderXML(ByVal FilePathName As String) As Integer
        Dim strField_Type As String = ""

        Try
            SetUSE_BRANCH_ID()

            '########################   ORDER HEADER    ########################
            strField_Type = "OREDERHEADER"
            ' getconfig_ImportXML(strField_Type) ' For More than One Order

            '---- For One Order 
            Dim objHeader As New tb_Order
            Dim ItemLife_Total_Day As Integer = 0
            Dim objSys_MaxID As New Sy_Temp_AutoNumber

            Dim strReceipt_ID As String = ""
            Dim objPalletType As New tb_PalletType_History
            Dim objPalletTypeCollection As New List(Of tb_PalletType_History)


            '---------------------------

            objHeader.Order_Index = objSys_MaxID.getSys_Value("Order_Index")
            objHeader.DocumentType_Index = "0010000000002"
            objHeader.Order_Date = CDate(ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Order_Date"))).ToString("yyyy/MM/dd")
            If _USE_BRANCH_ID = 1 Then
                Dim strWhere As String = " Branch_ID ='" & WV_Branch_ID & "'"
                Dim objDocumentNumber As New Sy_DocumentNumber
                objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                objDocumentNumber = Nothing
            Else
                Dim strWhere As String = ""
                Dim objDocumentNumber As New Sy_DocumentNumber
                objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                objDocumentNumber = Nothing
            End If

            objHeader.Lot_No = ""

            '#---- Customer
            Dim strCustomer_Name_Th As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Name_Th"))
            Dim strCustomer_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Id"))
            Dim strCustomer_Tax As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Tax"))

            If IsExitData("ms_Customer", "Str1", strCustomer_Tax) = False Then
                ' W_MSG_Confirm("ต้องการเพิ่มข้อมูล Customer :" & strName_Th & " หรือไม่")
                Dim objDocumentNumber As New Sy_AutoNumber
                strCustomer_Id = "X-" & objDocumentNumber.getSys_ID("Customer_Id")

                ' Auto Insert Package were Package and Update Package For Sku This
                Dim objAuto As New ms_Customer(ms_Customer.enuOperation_Type.ADDNEW)
                Dim strName_Th As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Name_Th"))
                Dim strName_Eng As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Name_Eng"))

                '--- address
                Dim strStreet As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Street"))
                Dim strCountry_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Country_Code"))
                Dim strCountry_Index As String = GetIndexByID("ms_Country", "Country_Index", "Country_Code", strCountry_Id)

                Dim strProvince_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Province"))
                Dim strProvince_Index As String = ""
                If IsExitData("ms_Province", "Province", strProvince_Id) = False Then
                    strProvince_Index = "10"
                Else
                    strProvince_Index = GetIndexByID("ms_Province", "Province_Index", "Province", strProvince_Id)
                End If

                Dim strDistrict_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_District"))
                Dim strDistrict_Index As String = ""
                If IsExitData("ms_District", "District", strDistrict_Id) = False Then
                    strDistrict_Index = "1"
                Else
                    strDistrict_Index = GetIndexByID("ms_District", "District_Index", "District", strDistrict_Id)
                End If

                If strCountry_Index = "1000000203" Then
                    strProvince_Id = ""
                    strDistrict_Id = ""
                Else
                    strProvince_Index = ""
                    strDistrict_Index = ""
                End If
                Dim strPostCode As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_PostCode"))
                Dim strTax As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Customer_Tax"))
                objAuto.SaveData("", strCustomer_Id, " ", strName_Th, "", "0010000000001", strStreet, strDistrict_Index, strProvince_Index, strPostCode, "", "", "", "", "", "", "", "", "0010000000001", 0, 0, 0, "Import data", 0, "", 1, strTax, "", strCountry_Index, strDistrict_Id, strProvince_Id, strName_Eng)
            End If
            objHeader.Customer_Index = GetIndexByID("ms_Customer", "Customer_Index", "Customer_Name", strCustomer_Name_Th)

            '#---- Supplier
            Dim strSupplier_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Id"))
            Dim strSupplier_Name_Th As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Name_Th"))

            If IsExitData("ms_Supplier", "Supplier_Name", strSupplier_Name_Th) = False Then

                Dim objDocumentNumber As New Sy_AutoNumber
                strSupplier_Id = "X-" & objDocumentNumber.getSys_ID("Supplier_ID")

                ' Auto Insert Package were Package and Update Package For Sku This
                ' Auto Insert Package were Package and Update Package For Sku This
                Dim objAuto As New ms_Supplier(ms_Supplier.enuOperation_Type.ADDNEW)
                Dim strName_Th As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Name_Th"))
                Dim strName_Eng As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Name_Eng"))

                '--- address
                Dim strStreet As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Street"))
                Dim strCountry_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Country_Code"))
                Dim strCountry_Index As String = GetIndexByID("ms_Country", "Country_Index", "Country_Code", strCountry_Id)

                Dim strProvince_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Province"))
                Dim strProvince_Index As String = ""
                If IsExitData("ms_Province", "Province", strProvince_Id) = False Then
                    strProvince_Index = "10"
                Else
                    strProvince_Index = GetIndexByID("ms_Province", "Province_Index", "Province", strProvince_Id)
                End If

                Dim strDistrict_Id As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_District"))
                Dim strDistrict_Index As String = ""
                If IsExitData("ms_District", "District", strDistrict_Id) = False Then
                    strDistrict_Index = "1"
                Else
                    strDistrict_Index = GetIndexByID("ms_District", "District_Index", "District", strDistrict_Id)
                End If

                If strCountry_Index = "1000000203" Then
                    strProvince_Id = ""
                    strDistrict_Id = ""
                Else
                    strProvince_Index = ""
                    strDistrict_Index = ""
                End If
                Dim strPostCode As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_PostCode"))
                Dim strTax As String = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Supplier_Tax"))

                objAuto.SaveData("", strSupplier_Id, " ", strName_Th, "", "0010000000001", strStreet, strDistrict_Index, strProvince_Index, strPostCode, "", "", "", "", "", "", "", "", "0010000000001", 0, 0, 0, "Import data", 0, "", 1, strTax, "", strCountry_Index, strDistrict_Id, strProvince_Id, strName_Eng, "")
            End If
            objHeader.Supplier_Index = GetIndexByID("ms_Supplier", "Supplier_Index", "Supplier_Name", strSupplier_Name_Th)

            objHeader.Department_Index = ""
            'ReferenceNo
            objHeader.Ref_No1 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Ref_No1"))

            'If objHeader.Ref_No1 <> "" Then
            '    Dim strReferenceNo As String = objHeader.Ref_No1
            '    If GetReferenceNo(strReferenceNo) = True Then
            '        Return 1
            '        Exit Try
            '    End If
            'End If

            objHeader.Invoice_No = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Invoice_No")) '"" 'Me.txtInvoice_No.Text

            objHeader.Ref_No2 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Ref_No2"))
            objHeader.Ref_No3 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Ref_No3"))
            objHeader.Ref_No4 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Ref_No4"))
            objHeader.Ref_No5 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Ref_No5"))
            objHeader.Str1 = ReadXML(FilePathName, getconfig_ElementImportXML(strField_Type, "Str1"))
            objHeader.Str2 = ""
            objHeader.Str3 = ""
            objHeader.Str4 = ""
            objHeader.Str5 = ""
            objHeader.Comment = "Import data"
            objHeader.Str8 = "" 'Me.cboContact_Name.Text
            objHeader.PO_No = "" 'Me.txtPo.Text

            objHeader.ASN_No = "" 'Me.txtASN_No.Text



            '########################   ORDER ITEM    ########################

            'getconfig_ImportXML("OREDERITEM")
            Dim objItemCollection As New List(Of tb_OrderItem)
            objItemCollection = READXMLITEM(FilePathName, "ORDERITEM", objHeader)

            If objItemCollection.Count = 0 Then
                ' Return 2
                Exit Try

            End If

            '--- Insert OrderTransaction
            objPalletTypeCollection.Add(objPalletType)
            Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
            objDB.SaveData()
            objDB = Nothing

            Return 9

        Catch ex As Exception

            Throw ex
        End Try
    End Function

#Region "   READ XML   "

    Function READXMLITEM(ByVal FilenamePath As String, ByVal Field_Type As String, ByVal pobjHeader As tb_Order) As List(Of tb_OrderItem)
        'Begin XmlDim 
        ' Dim ListID As String = ""

        Try

            Dim tmpValue As String = ""
            Dim objItemCollection As New List(Of tb_OrderItem)
            Dim objItem As New tb_OrderItem
            Dim CustXML As XmlDocument = New XmlDocument()
            CustXML.Load(FilenamePath)
            Dim nav As XPathNavigator = CustXML.CreateNavigator()
            Dim HeaderItemElement As String = getconfig_ElementImportXML("ORDERITEMHEADER", "ORDERITEMHEADER")
            Dim iCustomer As XPathNodeIterator = nav.Select(nav.Compile(HeaderItemElement))
            Dim i As Integer = 0

            While iCustomer.MoveNext()

                Dim lstNav As XPathNavigator
                Dim iterNews As XPathNodeIterator
                lstNav = iCustomer.Current
                iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)

                'Loop through the child nodes
                objItem = New tb_OrderItem
                Dim objDBIndex As New Sy_AutoNumber
                objItem.OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                objDBIndex = Nothing
                objItem.Order_Index = pobjHeader.Order_Index

                Dim strSKU_Id As String = ""
                Dim strSKU_Name_th As String = ""
                Dim strSKU_Name_Eng As String = ""
                Dim strPackage_Id As String = ""
                Dim strPackage_Index As String = ""
                Dim strSku_Index As String = ""
                Dim strCustomer_Shipping As String = ""
                Dim itmpPck As Integer = 0

                'Dim lstNav2 As XPathNavigator
                'Dim iterNews2 As XPathNodeIterator
                'lstNav2 = iCustomer.Current.Value
                'iterNews = lstNav.SelectDescendants(XPathNodeType.Element, False)

                While iterNews.MoveNext
                    '   Debug.WriteLine(iterNews.Current.Name & ": " & iterNews.Current.Value)
                    If getconfig_ElementImportXML(Field_Type, "SKU_ID") = iterNews.Current.Name Then
                        strSKU_Id = iterNews.Current.Value
                    End If

                    If getconfig_ElementImportXML(Field_Type, "SKU_Name_th") = iterNews.Current.Name Then
                        strSKU_Name_th = iterNews.Current.Value
                    End If

                    If getconfig_ElementImportXML(Field_Type, "SKU_Name_Eng") = iterNews.Current.Name Then
                        strSKU_Name_Eng = iterNews.Current.Value
                    End If

                    If getconfig_ElementImportXML(Field_Type, "Package_Id") = iterNews.Current.Name Then
                        If itmpPck = 0 Then
                            strPackage_Id = iterNews.Current.Value
                            '  NEW SKU
                            If IsExitData("ms_SKU", "str1", strSKU_Name_th) = False Then
                                ' Auto Insert SKU
                                strPackage_Index = SavePackage(strPackage_Id)
                                Dim objDocumentNumber As New Sy_AutoNumber
                                strSKU_Id = "X-" & objDocumentNumber.getSys_ID("sku_Id")
                                ' strSKU_Id = ""
                                strSku_Index = SaveSKU(strPackage_Index, strSKU_Id, strSKU_Name_th, strSKU_Name_Eng, pobjHeader.Customer_Index, pobjHeader.Supplier_Index)
                                Me.SaveSKURatio(strSku_Index, strPackage_Index, 1) 'Default Ratio=1 ?
                                objItem.Sku_Index = strSku_Index
                            Else
                                'Get Sku_Index
                                objItem.Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "str1", strSKU_Name_th)

                                If IsCheckPackage(strPackage_Id, objItem.Sku_Index) = False Then
                                    ' Auto Insert Package were Package and Update Package For Sku This
                                    strPackage_Index = SavePackage(strPackage_Id)
                                    Me.SaveSKURatio(objItem.Sku_Index, strPackage_Index, 1) 'Default Ratio=1 ?
                                End If
                            End If

                            'Get Package_Index
                            objItem.Package_Index = GetPackage_Index(objItem.Sku_Index, strPackage_Id)

                            'New Package
                            If objItem.Sku_Index <> "" Then
                                Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                                objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
                                objRatio = Nothing
                                objItem.Total_Qty = objItem.Qty * objItem.Ratio
                            End If

                            itmpPck += 1

                        End If

                    End If

                    '#---- Start Consignee_Index
                    '  Dim strValue() As String

                    Dim strCompany_Id As String = ""
                    Dim strCompany_Name As String = ""

                    If getconfig_ElementImportXML(Field_Type, "Consignee") = iterNews.Current.Name Then
                        strCompany_Name = iterNews.Current.Value

                       
                        Dim strCustomer_Index As String = pobjHeader.Customer_Index
                        If IsExitData_Customer_Shipping("ms_Customer_Shipping", "Company_Name", strCompany_Name, strCustomer_Index) = False Then
                            ' W_MSG_Confirm("ต้องการเพิ่มข้อมูล Customer :" & strName_Th & " หรือไม่")
                            Dim objAuto As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)

                            Dim objDocumentNumber As New Sy_AutoNumber
                            strCompany_Id = "X-" & objDocumentNumber.getSys_ID("customer_Shipping_Id")

                            objAuto.SaveData("", strCustomer_Index, "บริษัท", strCompany_Name, "", "", "10", "10", "", "", "", "", "", "", "", "", "", "", strCompany_Id, "", "", "", "")

                        End If
                        'objItem.Consignee_Index = GetIndexByID("ms_Customer_Shipping", "Customer_Shipping_Index", "Company_Name", strCompany_Name)
                        objItem.Consignee_Index = GetIndexByID("ms_Customer_Shipping", "Customer_Shipping_Index", "Company_Name", strCompany_Name)

                    End If
                
                    If getconfig_ElementImportXML(Field_Type, "HS_Code") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value

                        Dim strValue As String = tmpValue.Remove(0, 4)
                        objItem.HS_Code = strValue

                    End If

                    'DutyType
                    Dim lstDuty As XPathNavigator
                    Dim iterDuty As XPathNodeIterator
                    lstDuty = iterNews.Current
                    iterDuty = lstDuty.SelectDescendants(XPathNodeType.Element, False)

                    While iterDuty.MoveNext

                        If getconfig_ElementImportXML(Field_Type, "DutyType") = iterNews.Current.Name Then

                            tmpValue = iterNews.Current.GetAttribute("dutyType", "")
                            Select Case tmpValue

                                Case "0100" '0100 ภาษีศุลกากร
                                    If getconfig_ElementImportXML(Field_Type, "TAX1") = iterDuty.Current.Name Then
                                        Dim tmpTax1 As String = iterDuty.Current.Value
                                        objItem.Tax1 = tmpTax1
                                        Exit While
                                    End If

                                Case "0500" '0500 ภาษีมูลค่าเพิ่ม
                                    If getconfig_ElementImportXML(Field_Type, "TAX2") = iterDuty.Current.Name Then
                                        Dim tmpTax2 As String = iterDuty.Current.Value
                                        objItem.Tax2 = tmpTax2
                                        Exit While
                                    End If

                                Case "0300" '0300 ภาษีสรรพสามิต
                                    If getconfig_ElementImportXML(Field_Type, "TAX3") = iterDuty.Current.Name Then
                                        Dim tmpTax3 As String = iterDuty.Current.Value
                                        objItem.Tax3 = tmpTax3
                                        Exit While
                                    End If

                                Case "0400" '0400 ภาษีมหาดไทย
                                    If getconfig_ElementImportXML(Field_Type, "TAX4") = iterDuty.Current.Name Then
                                        Dim tmpTax4 As String = iterDuty.Current.Value
                                        objItem.Tax4 = tmpTax4
                                        Exit While
                                    End If

                                Case "0600" '0600 ภาษีอื่นๆ
                                    If getconfig_ElementImportXML(Field_Type, "TAX5") = iterDuty.Current.Name Then
                                        Dim tmpTax5 As String = iterDuty.Current.Value
                                        objItem.Tax5 = tmpTax5
                                        Exit While
                                    End If

                            End Select
                        End If
                    End While

                    If getconfig_ElementImportXML(Field_Type, "Qty") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value
                        objItem.Qty = CDbl(tmpValue)
                        objItem.Total_Qty = objItem.Qty * objItem.Ratio
                    End If

                    If getconfig_ElementImportXML(Field_Type, "NetWeight") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value
                        objItem.Weight = CDbl(tmpValue)


                    End If

                    objItem.Volume = 0 'odrOrderItem("Volume").ToString

                    If getconfig_ElementImportXML(Field_Type, "Price_Per_Pck") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value
                        objItem.Price_Per_Pck = CDbl(tmpValue)
                    End If

                    If getconfig_ElementImportXML(Field_Type, "OrderItem_Price") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value
                        objItem.OrderItem_Price = CDbl(tmpValue)

                    End If

                    If getconfig_ElementImportXML(Field_Type, "Str1") = iterNews.Current.Name Then
                        tmpValue = iterNews.Current.Value
                        objItem.Str1 = tmpValue
                    End If

                    If getconfig_ElementImportXML(Field_Type, "Str3") = iterNews.Current.Name Then

                        ' Dim sOuterXml As String = iterNews.Current.OuterXml
                        ' Dim strFileNew() As String = sOuterXml.Split("""")
                        ' Dim sSplitXML As String = strFileNew(1).Trim
                        '  Dim sDesPack As String = Me.GetPackage_Index(sSplitXML)

                        tmpValue = iterNews.Current.Value
                        objItem.Str3 = tmpValue '& " " & sDesPack
                    End If

                    objItem.HandlingType_Index = "1"
                    objItem.Plan_Qty = 0
                    objItem.Str7 = ""
                    objItem.PO_No = ""
                    objItem.Comment = ""
                    ' objItem.Str1 = "" 'odrOrderItem("Ref").ToString

                    objItem.Str5 = ""
                    objItem.Str2 = "Import OrderItem"
                    objItem.Str10 = ""
                    objItem.PalletType_Index = ""
                    objItem.Pallet_Qty = 0
                    objItem.Plot = "" 'odrOrderItem("Lot").ToString
                    objItem.Lot_No = ""
                    objItem.ItemStatus_Index = "0010000000001"
                    objItem.Serial_No = ""

                    objItem.Mfg_Date = Now
                    objItem.Exp_date = Now
                    objItem.IsMfg_Date = True
                    objItem.IsExp_Date = True
                    ' **********************************************************
                    ' ItemDefinition_Index
                    objItem.ItemDefinition_Index = ""
                    objItem.Declaration_No = pobjHeader.Ref_No1
                    objItem.Invoice_No = pobjHeader.Ref_No2

                    ' *** Add value ***

                End While
                i = i + 1
                objItem.Seq = i
                objItemCollection.Add(objItem)
            End While


            Return objItemCollection
        Catch ex As Exception

            Throw ex
        End Try

    End Function

    Private tmpName As String = ""
    Private strOutPut As String = ""

    Sub ReadAllXML(ByVal NodeName As String)
        Try
            Dim m_xmld As XmlDocument = New XmlDocument()
            m_xmld.Load("C:\IDEC99ABRS100100214_ABRS100100214.xml")
            Dim m_nodelist As XmlNodeList
            m_nodelist = m_xmld.SelectNodes(NodeName)

            Dim m_node As XmlNode
            For Each m_node In m_nodelist
                If m_node.ChildNodes.Count = 0 Then
                    strOutPut &= tmpName & " : " & ""
                    'strOutPut &= Environment.NewLine
                    '' INSERT ELEMENT
                    ''strOutPut &= tmpName & " : " & m_node.ChildNodes.Item(iChild).InnerText
                    ''strSQL = "INSERT INTO config_ImportXML (Element) VALUES ('" & tmpName & "')"
                    ''SetSQLString = strSQL
                    ''connectDB()
                    ''EXEC_DataAdapter()
                End If
                For iChild As Integer = 0 To m_node.ChildNodes.Count - 1
                    If m_node.ChildNodes.Item(iChild).Name <> "#text" Then
                        tmpName = m_node.ChildNodes.Item(iChild).Name
                        ReadAllXML(NodeName & "/" & m_node.ChildNodes.Item(iChild).Name)
                    Else
                        strOutPut &= tmpName & " : " & m_node.ChildNodes.Item(iChild).InnerText
                        'strOutPut &= Environment.NewLine

                        '' INSERT ELEMENT
                        ''strOutPut &= tmpName & " : " & m_node.ChildNodes.Item(iChild).InnerText
                        ''strSQL = "INSERT INTO config_ImportXML (Element) VALUES ('" & tmpName & "')"
                        ''SetSQLString = strSQL
                        ''connectDB()
                        ''EXEC_DataAdapter()

                    End If
                Next
            Next




        Catch errorVariable As Exception
            MsgBox(errorVariable.ToString())
        End Try
    End Sub

    'oXml = XmlReader.Create(path, oXmlSettings)
    'While oXml.Read()
    '    Select Case oXml.NodeType
    '        Case XmlNodeType.Element        'Read Element as-is         
    '            If taglist.contains(oXml.Name) Then
    '                stringbuilder.Append(oXml.ReadOuterXml())
    '            End If
    '        Case XmlNodeType.Text
    '            stringbuilder.Append(oXml.Value)
    '    End Select
    'End While

    Function ReadXML(ByVal FilenamePath As String, ByVal NodeElement As String) As String
        Try
            Dim strValue As String = ""
            Dim m_xmld As XmlDocument = New XmlDocument()
            m_xmld.Load(FilenamePath)
            Dim m_nodelist As XmlNodeList
            m_nodelist = m_xmld.SelectNodes(NodeElement)
            Dim m_node As XmlNode
            For Each m_node In m_nodelist
                For iChild As Integer = 0 To m_node.ChildNodes.Count - 1
                    strValue = m_node.ChildNodes.Item(iChild).InnerText
                Next
            Next
            Return strValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "   DATA LAYER   "

    Public Function GetReferenceNo(ByVal ReferenceNo As String) As Boolean
        Try
            Dim strSQL As String = ""

            strSQL = " SELECT     Order_Index, Order_No,Invoice_No"
            strSQL &= " FROM         dbo.tb_Order "
            strSQL &= " WHERE Invoice_No = '" & ReferenceNo & "' and Status <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then

                If _dataTable.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function IsExitData(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String) As Boolean
        Try
            Dim strSQL As String

            strSQL = "SELECT count(*) FROM " & pstrTableName & " WHERE " & pstrFieldName & " = @" & pstrFieldName & " "

            With SQLServerCommand
                'gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@" & pstrFieldName & "", SqlDbType.VarChar, 255).Value = pstrFieldValue

            End With

            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case "0"
                    Return False
                Case Else
                    Return True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function IsExitData_Customer_Shipping(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String, ByVal pstrCustomer_Index As String) As Boolean
        Try
            Dim strSQL As String

            strSQL = "SELECT count(*) FROM " & pstrTableName & " WHERE " & pstrFieldName & " = @" & pstrFieldName & " and Customer_Index = '" & pstrCustomer_Index & "'"

            With SQLServerCommand
                'gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@" & pstrFieldName & "", SqlDbType.VarChar, 255).Value = pstrFieldValue

            End With

            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case "0"
                    Return False
                Case Else
                    Return True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function IsCheckPackage(ByVal pstrpackage_Id As String, ByVal pstrsku_Index As String) As Boolean
        Try
            Dim strSQL As String

            strSQL = "SELECT    ms_Package.* "
            strSQL &= " FROM        ms_Package INNER JOIN"
            strSQL &= "            ms_SKURatio ON ms_Package.Package_Index = ms_SKURatio.Package_Index INNER JOIN"
            strSQL &= "    ms_SKU ON ms_SKURatio.Sku_Index = ms_SKU.Sku_Index"
            strSQL &= "   WHERE ms_SKU.status_id <> -1"
            strSQL &= "    and ms_Package.package_Id = '" & pstrpackage_Id & "' and ms_SKU.sku_Index ='" & pstrsku_Index & "' "


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim strSQL As String = ""
            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = @" & pstrField_ID & ""

            SetSQLString = strSQL
            '      strSQL = "SELECT " & pstrFieldName & " FROM " & pstrTableName & " WHERE " & pstrFieldName & " = @" & pstrFieldName & ""

            With SQLServerCommand
                'gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@" & pstrField_ID & "", SqlDbType.VarChar, 255).Value = pstrField_ID_Value

            End With
            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'connectDB()
            'EXEC_Command()

            SetSQLString = strSQL
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

    Private Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String
        Try
            Dim strSQL As String = ""

            'strSQL = " SELECT P.Package_Index "
            'strSQL &= " FROM ms_Package P INNER JOIN ms_SKU S ON P.Package_Index = S.Package_Index"
            'strSQL &= " 	INNER JOIN ms_SKURatio SR ON SR.SKU_index = S.SKU_Index AND P.Package_Index = SR.Package_Index"
            strSQL = "  SELECT     P.Package_Index"
            strSQL &= "  FROM         dbo.ms_SKURatio SR INNER JOIN"
            strSQL &= "        dbo.ms_SKU S ON SR.Sku_Index = S.Sku_Index INNER JOIN"
            strSQL &= "     dbo.ms_Package P ON SR.Package_Index = P.Package_Index"
            strSQL &= "  WHERE S.SKU_index = '" & pstrSKU_Index & "' AND  P.Description = '" & pstrPackage_ID.Replace("'", "''") & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return _dataTable.Rows(0)("Package_Index").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetPackage_Index(ByVal pstrPackage_Name As String) As String
        Try
            Dim strSQL As String = ""

            strSQL = "  SELECT     Des_Package "
            strSQL &= "  FROM       ms_Package_Des"
            strSQL &= "  WHERE Description = '" & pstrPackage_Name & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return _dataTable.Rows(0)("Des_Package").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub SetUSE_BRANCH_ID()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_BRANCH_ID", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._USE_BRANCH_ID = objDT.Rows(0).Item("Config_Value").ToString

            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Public Sub getconfig_ImportXML(ByVal Field_Type As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM config_ImportXML"
            strSQL &= " where Field_Type='" & Field_Type & "' and IsUse=1"

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

    Private Function getconfig_ElementImportXML(ByVal Field_Type As String, ByVal Field_Name As String) As String
        Dim strSQL As String

        Try

            strSQL = " SELECT ELEMENT "
            strSQL &= " FROM config_ImportXML"
            strSQL &= " where Field_Type='" & Field_Type & "' and IsUse=1 "
            strSQL &= " and Field_Name ='" & Field_Name & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return _scalarOutput
            Else
                Return _scalarOutput
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Function SavePackage(ByVal ppackage_Id As String) As String

        Try
            Dim Package_Index As String = ""
            Dim package_Id As String = ppackage_Id
            Dim description As String = ppackage_Id
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0
            Dim Weight As Double = 0.0

            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
            Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, 0, Weight, 0) ', txtUnit_id.Text

            Return Package_Index

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function SaveSKU(ByVal ppackage_Index As String, ByVal pSku_Id As String, ByVal pSku_Name_th As String, ByVal pSku_Name_Eng As String, ByVal pCustomer_Index As String, ByVal pSupplier_Index As String) As String
        Try
            Dim Sku_Index As String = ""
            Dim Product_Index As String = ""
            Dim objDB As New ms_SKU(ms_SKU.enuOperation_Type.ADDNEW)
            Dim blnSaveResult As Boolean = False

            objDB._ProductSku_Type = "1"
            objDB._Product_Type = "0010000000001"  ' Default
            objDB._ProductName = pSku_Name_th
            ' ------ Remark: 
            ' ------ Str10 stores GroupSKU value.
            ' ------ Str4 and Str5 is Customer and Supplier Reference Code respectively.
            objDB.Str10 = 1 ' Normal Sku
            objDB.Str4 = ""
            objDB.Str5 = ""

            objDB.Item_Package_Index = ""

            ' TODO: This is wrong.
            Dim objDBIndex As New Sy_AutoNumber
            Sku_Index = objDBIndex.getSys_Value("SKU_Index")
            objDBIndex = Nothing
            'killz set "","",false,false
            blnSaveResult = objDB.SaveData(Sku_Index, pSku_Id, "", -1, ppackage_Index, "", -1, -1, -1, "", "", 0, 0, 0, 0, 0, 0, pSku_Name_th, pSku_Name_Eng, pSku_Name_Eng, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", "", "", pCustomer_Index, pSupplier_Index, "", 1, "", "", "", 0, "", "", "", "")
            blnSaveResult = objDB.InsertSKU_Transation()
            pSku_Id = objDB.Sku_Id
            Product_Index = objDB.Product_Index.ToString
            If blnSaveResult Then
                Dim PackageIndexE As String = ppackage_Index
                Dim RatioE As String = 1
                Dim objDBEdit As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
                objDBEdit.SaveData("", Sku_Index, PackageIndexE, Val(RatioE))
                ' Save successfully!
            End If


            Return Sku_Index

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub SaveSKURatio(ByVal pSku_Index As String, ByVal pPackage_Index As String, ByVal ratio As Double)
        Try

            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, Val(ratio))


            '    W_MSG_Information_ByIndex(1)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

End Class

