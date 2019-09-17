Imports System.Text
Imports System.IO
Imports System.Data.SqlClient
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports System.Data.OleDb

Public Class Import_ASN : Inherits DBType_SQLServer

    Private _DataSource As New DataTable
    Private _FilePath As String = ""
    Private _AdvanceShipNotice_Index As String = ""
    Private _AdvanceShipNoticeItem_Index As String = ""
    Private _AdvanceShipNotice_No As String = ""
    Private _AdvanceShipNotice_Date As Date
    Private _Expected_Delivery_Date As Date
    Private _Supplier_Index As String = ""
    Private _Customer_Index As String = ""
    Private _Remark As String = ""
    Private _Sku_Index As String = ""
    Private _Plot As String = ""
    Private _Qty As Double
    Private _Package_Index As String = ""
    Private _StrPackage As String = ""
    Private CustomerID As String = ""
    Private CustomerName As String = ""
    Private _Item_Seq As Integer = 0

    Private _DocumentType_Index As String = ""
    Private _EPSON_Location_Index As String = ""
    Private _DocumentItem_Status_Index As String = ""
    Private _dataTable As DataTable = New DataTable
    Dim _osbErr_Dup As New StringBuilder
    'Private _Use_car As Integer

#Region " Property "

    'Public Property Use_car() As Integer
    '    Get
    '        Return _Use_car
    '    End Get
    '    Set(ByVal Value As Integer)
    '        _Use_car = Value
    '    End Set
    'End Property

    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value
        End Set
    End Property
    Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal value As String)
            _Plot = value
        End Set
    End Property

    Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property
    'Update by bo 1/07/11
    Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Public Property AdvanceShipNotice_Index() As String
        Get
            Return _AdvanceShipNotice_Index
        End Get
        Set(ByVal value As String)
            _AdvanceShipNotice_Index = value
        End Set
    End Property

    Public Property AdvanceShipNoticeItem_Index() As String
        Get
            Return _AdvanceShipNoticeItem_Index
        End Get
        Set(ByVal value As String)
            _AdvanceShipNoticeItem_Index = value
        End Set
    End Property

    Public Property StrPackage() As String
        Get
            Return _StrPackage
        End Get
        Set(ByVal value As String)
            _StrPackage = value
        End Set
    End Property


    Public Property AdvanceShipNotice_No() As String
        Get
            Return _AdvanceShipNotice_No
        End Get
        Set(ByVal value As String)
            _AdvanceShipNotice_No = value
        End Set
    End Property


    Public Property AdvanceShipNotice_Date() As Date
        Get
            Return _AdvanceShipNotice_Date
        End Get
        Set(ByVal value As Date)
            _AdvanceShipNotice_Date = value
        End Set
    End Property
    Public Property Expected_Delivery_Date() As Date
        Get
            Return _Expected_Delivery_Date
        End Get
        Set(ByVal value As Date)
            _Expected_Delivery_Date = value
        End Set
    End Property
    Public Property Supplier_Index() As String
        Get
            Return _Supplier_Index
        End Get
        Set(ByVal value As String)
            _Supplier_Index = value
        End Set
    End Property

    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal value As String)
            _Package_Index = value
        End Set
    End Property
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property
    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property

    Public Property Item_Seq() As Integer
        Get
            Return _Item_Seq
        End Get
        Set(ByVal value As Integer)
            _Item_Seq = value
        End Set
    End Property

    Property EPSON_Location_Index() As String
        Get
            Return _EPSON_Location_Index
        End Get
        Set(ByVal value As String)
            _EPSON_Location_Index = value
        End Set
    End Property

    Property DocumentItem_Status_Index() As String
        Get
            Return _DocumentItem_Status_Index
        End Get
        Set(ByVal value As String)
            _DocumentItem_Status_Index = value
        End Set
    End Property


#End Region

#Region " LoadDataFromTextFile "



#End Region
    'Read data from text file and convert data to datatable format

#Region "Insert "

    Public Sub saveHeader()
        Try
            Dim strSQLHeader As String

            strSQLHeader = "INSERT INTO tb_AdvanceShipNotice(AdvanceShipNotice_Index,AdvanceShipNotice_No,AdvanceShipNotice_Date,Expected_Delivery_Date,Supplier_Index,Remark,Status,add_date,add_by,add_branch,Customer_Index,DocumentType_Index) "
            strSQLHeader &= " VALUES(@AdvanceShipNotice_Index,@AdvanceShipNotice_No,@AdvanceShipNotice_Date,@Expected_Delivery_Date,@Supplier_Index,@Remark,@Status,@add_date,@add_by,@add_branch,@Customer_Index,@DocumentType_Index)"

            Dim objSy_AutoNumber As New WMS_STD_Formula.Sy_AutoNumber
            Me._AdvanceShipNotice_Index = objSy_AutoNumber.getSys_Value("AdvanceShipNotice_Index")
            objSy_AutoNumber = Nothing


            With Me.SQLServerCommand
                .CommandText = strSQLHeader
                .Parameters.Clear()
                .Parameters.Add("@AdvanceShipNotice_Index", SqlDbType.VarChar, 13).Value = Me._AdvanceShipNotice_Index
                .Parameters.Add("@AdvanceShipNotice_No", SqlDbType.VarChar, 50).Value = _AdvanceShipNotice_No
                .Parameters.Add("@AdvanceShipNotice_Date", SqlDbType.DateTime, 23).Value = _AdvanceShipNotice_Date
                .Parameters.Add("@Expected_Delivery_Date", SqlDbType.DateTime, 23).Value = _Expected_Delivery_Date
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _Supplier_Index
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _Remark
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = Now
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = Me.DocumentType_Index
            End With

            SetSQLString = strSQLHeader
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SaveDetail()
        Try
            Dim strSQLDetail As String
            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)

            'Get Ratio
            Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim dblRatio As Double = 0
            dblRatio = objRatio.getRatio(_Sku_Index, _Package_Index)
            objRatio = Nothing

            strSQLDetail = " INSERT INTO tb_AdvanceShipNoticeItem(AdvanceShipNoticeItem_Index,Item_Seq,AdvanceShipNotice_Index,Sku_Index,Package_Index,Qty,Plot,Ratio,Total_Qty,Net_weight,Weight,Volume,Serial_No,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch)"
            strSQLDetail &= " VALUES(@AdvanceShipNoticeItem_Index,@Item_Seq,@AdvanceShipNotice_Index,@Sku_Index,@Package_Index,@Qty,@Plot,@Ratio,@Total_Qty,@Net_weight,@Weight,@Volume,@Serial_No,@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch)"

            Dim objSy_AutoNumber As New Sy_AutoNumber
            Me._AdvanceShipNoticeItem_Index = objSy_AutoNumber.getSys_Value("AdvanceShipNoticeItem_Index")
            objSy_AutoNumber = Nothing

            With Me.SQLServerCommand
                .CommandText = strSQLDetail
                .Parameters.Clear()
                .Parameters.Add("@AdvanceShipNoticeItem_Index", SqlDbType.VarChar, 13).Value = Me._AdvanceShipNoticeItem_Index
                .Parameters.Add("@Item_Seq", SqlDbType.Int, 4).Value = _Item_Seq
                .Parameters.Add("@AdvanceShipNotice_Index", SqlDbType.VarChar, 13).Value = Me._AdvanceShipNotice_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _Sku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _Package_Index
                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _Qty

                ' *** Calculate Tatal Qty *** 
                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _Qty * dblRatio
                .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = _Plot
                ''''''update by bo 5/07/2011 เพิ่มการinsert tb_tb_AdvanceShipNoticeItem
                .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = 0
                '.Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Net_weight", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Serial_No", SqlDbType.VarChar, 100).Value = ""
                .Parameters.Add("@UnitPrice", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = ""
                .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = ""
                .Parameters.Add("@Reason", SqlDbType.NVarChar, 1000).Value = ""
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = ""
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = ""
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 200).Value = ""
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 200).Value = ""
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                .Parameters.Add("@add_by  ", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID



























            End With
            SetSQLString = strSQLDetail
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


#Region "   Import Exel   "

    Public Function LoadWorkSheet(ByVal pstrFileName As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            '=============
            oConnSource.Open()

            Dim Objdt As DataTable = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            Dim Objds As New DataSet

            Me.DataSource = Objdt

            Return Objdt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
            'strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter


            With odaSource
                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'") ' "Sheet1''s$"
                    '.SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "] WHERE Doc_No <> '' ORDER BY Doc_No ", oConnSource)
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "] ", oConnSource)
                Else
                    '.SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] WHERE Doc_No <> '' ORDER BY Doc_No ", oConnSource)
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] ", oConnSource)
                End If
                '===================
                .Fill(odtTemp)

            End With

            Select Case odtTemp.Columns.Count
                Case 11
                    odtTemp.Columns.Add(New DataColumn("L_CustomerRef"))
                    odtTemp.Columns.Add(New DataColumn("L_Reference2"))
                    odtTemp.Columns.Add(New DataColumn("H_ContainerNo"))
                    odtTemp.Columns.Add(New DataColumn("H_PermitNo"))
                Case 12
                    odtTemp.Columns.Add(New DataColumn("L_Reference2"))
                    odtTemp.Columns.Add(New DataColumn("H_ContainerNo"))
                    odtTemp.Columns.Add(New DataColumn("H_PermitNo"))
                Case 13
                    odtTemp.Columns.Add(New DataColumn("H_ContainerNo"))
                    odtTemp.Columns.Add(New DataColumn("H_PermitNo"))
                Case 14
                    odtTemp.Columns.Add(New DataColumn("H_PermitNo"))
            End Select

            'If odtTemp.Columns.Contains("L_CustomerRef") = False Then
            '    odtTemp.Columns.Add(New DataColumn("L_CustomerRef"))
            'End If

            'If odtTemp.Columns.Contains("L_Reference2") = False Then
            '    odtTemp.Columns.Add(New DataColumn("L_Reference2"))
            'End If

            If odtTemp.Columns.Contains("M3") = False Then
                odtTemp.Columns.Add("M3", GetType(Double))
            End If

            If odtTemp.Columns.Contains("Lot") = False Then
                odtTemp.Columns.Add("Lot", GetType(String))
            End If

            SetGridHeaderName(odtTemp)

            Me.DataSource = odtTemp

            Return odtTemp

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub SetGridHeaderName(ByVal prtDataTable As DataTable)

        Dim ObjDT As New DataTable

        Dim column As New DataColumn

        ObjDT = prtDataTable

        ObjDT.Columns(1).ColumnName = "DocRef_No"
        ObjDT.Columns(2).ColumnName = "Source_No"
        ObjDT.Columns(3).ColumnName = "Doc_Date"
        ObjDT.Columns(4).ColumnName = "RCV_Inv_No"
        ObjDT.Columns(5).ColumnName = "Cust_PO_No"
        ObjDT.Columns(6).ColumnName = "Product_No"
        ObjDT.Columns(7).ColumnName = "Qty"
        ObjDT.Columns(8).ColumnName = "TotNetWeight"
        ObjDT.Columns(9).ColumnName = "Tot_GWeight"
        ObjDT.Columns(10).ColumnName = "Remark"
        '
        ObjDT.Columns(11).ColumnName = "L_CustomerRef"
        ObjDT.Columns(12).ColumnName = "L_Reference2"
        ObjDT.Columns(13).ColumnName = "H_ContainerNo"
        ObjDT.Columns(14).ColumnName = "H_PermitNo"
        '
        ObjDT.Columns(1).DataType.ToString()
        ObjDT.Columns(2).DataType.ToString()
        'ObjDT.Columns(3).DataType.ToString()
        ObjDT.Columns(5).DataType.ToString()
        ObjDT.Columns(6).DataType.ToString()
        'ObjDT.Columns(7).DataType.ToString()
        'ObjDT.Columns(8).DataType.ToString()
        'ObjDT.Columns(9).DataType.ToString()
        ObjDT.Columns(10).DataType.ToString()
        ObjDT.Columns(11).DataType.ToString()
        ObjDT.Columns(12).DataType.ToString()
        ObjDT.Columns(13).DataType.ToString()
        ObjDT.Columns(14).DataType.ToString()
    End Sub

    Public Function LoadDataFromFile_Old(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter


            With odaSource
                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'") ' "Sheet1''s$"
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "] WHERE Doc_No <> '' ORDER BY Doc_No ", oConnSource)
                Else
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] WHERE Doc_No <> '' ORDER BY Doc_No ", oConnSource)
                End If
                .Fill(odtTemp)
            End With

            Me.DataSource = odtTemp

            Return odtTemp

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function CheckingData() As Boolean
        Try
            Dim boolResult As Boolean = True

            For Each odr As DataRow In Me.DataSource.Rows
                For Each odc As DataColumn In Me.DataSource.Columns                    
                    Select Case odc.ColumnName.Trim.ToLower
                        Case "Doc_No".ToLower
                            If IsExitData("tb_AdvanceShipNotice", "AdvanceShipNotice_No", odr(odc.ColumnName).ToString) = True Then
                                odr("Check_Result") = "ข้อมูลใบรับสินค้านี้มีอยู่ก่อนแล้ว !"
                                'boolResult = False
                                'Exit For
                            End If

                        Case "SKU_ID".ToLower

                            If IsExitData("ms_SKU", "Sku_Id", odr(odc.ColumnName).ToString) = False Then
                                odr("Check_Result") = "ไม่พบข้อมูล SKU นี้ !"
                                'boolResult = False
                                'Exit For
                            End If
                            If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
                                odr("Check_Result") = "กรุณากรอกข้อมูล SKU!"
                                boolResult = False
                                'Exit For
                            End If

                        Case "UOM".ToLower

                            '_Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

                            '_Package_Index = GetPackage_Index(_Sku_Index, odr(odc.ColumnName).ToString.Trim)

                            If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
                                odr("Check_Result") = "ไม่พบข้อมูล UOM นี้ !"
                                boolResult = False
                                'Exit For
                            End If



                        Case "QTY".ToLower
                            If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                                odr("Check_Result") = "กรุณาป้อนจำนวน"
                                boolResult = False
                                'Exit For
                            ElseIf IsNumeric(odr(odc.ColumnName.Trim).ToString) = False Then
                                odr("Check_Result") = "กรุณาใหม่ป้อนจำนวนให้ถูกต้อง"
                                boolResult = False
                                'Exit For
                            End If
                        Case "Customer_ID".ToLower
                            If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
                                odr("Check_Result") = "กรุณาระบุ Customer_ID !"
                                boolResult = False
                                'Exit For
                            End If
                            'Case "width", "length", "height", "volume", "weight"
                            '    If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
                            '        odr(odc.ColumnName.Trim) = 0
                            '    End If
                    End Select
                    odr("Check_Result") = "OK."
                Next
            Next

            Return boolResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT P.Package_Index "
            strSQL &= " FROM ms_Package P INNER JOIN ms_SKU S ON P.Package_Index = S.Package_Index"
            strSQL &= " 	INNER JOIN ms_SKURatio SR ON SR.SKU_index = S.SKU_Index AND P.Package_Index = SR.Package_Index"
            strSQL &= " WHERE S.SKU_index = '" & pstrSKU_Index & "' AND  P.Description = '" & pstrPackage_ID.Replace("'", "''") & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)("Package_Index").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "' and Status_Id <> -1 "

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)


            ' _Package_Index = odtServer.Rows(0).Item("")
            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)(pstrField_Index).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function GetSource_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
    '    Try

    '        Dim strSQL As String = ""
    '        Dim strReturn As String


    '        strSQL &= " SELECT * "
    '        strSQL &= " FROM ms_Customer_Receive_Location "
    '        strSQL &= " WHERE Customer_Receive_Location_Id = '" & pstrModel & "' and Customer_Index = '" & pstrCustomer & "' and Status_id <> -1"

    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable

    '        If _dataTable.Rows.Count > 0 Then
    '            strReturn = _dataTable.Rows(0)("Customer_Receive_Location_Index").ToString
    '        Else
    '            strReturn = ""
    '        End If
    '        Return strReturn
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Function GetSKU_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String

            strSQL &= " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE SKU_Id = '" & pstrModel & "' and Customer_Index = '" & pstrCustomer & "' and Status_id <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("SKU_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function IsExitData(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String) As Boolean
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrFieldName & " FROM " & pstrTableName & " WHERE " & pstrFieldName & " = '" & pstrFieldValue.Trim & "'"
            Select Case pstrTableName
                Case "tb_Withdraw", "tb_Order", "tb_AdvanceShipNotice"
                    strSQL &= " And Status <> -1"
                Case Else
                    strSQL &= " And Status_Id <> -1"
            End Select
            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function SetDEFAULT_ASN_DOCUMENTTYPE_INDEX() As String
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("DEFAULT_ASN_DOCUMENTTYPE_INDEX")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Return objDT.Rows(0)("config_value").ToString
            Else
                Return ""
            End If
            Return ""

            '###################################

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Function

    'Public Function StartImportASNExcel_New() As Boolean
    '    Try
    '        Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
    '        Dim oDT As New DataTable
    '        Dim oblPO As New Import_ASN
    '        Dim strSKU_Id As String = ""
    '        Dim strSKU_Name_th As String = ""
    '        Dim strPackage_Id As String = ""
    '        Dim strPackage_Index As String = ""
    '        Dim strSku_Index As String = ""
    '        Dim strDocumentNo As String = ""
    '        Dim iCurrent_Line_Number As Integer = 0
    '        Dim Current_Text_Line As String = ""
    '        Dim iCount_Error_Header As Integer = 0

    '        oDT = Me.DataSource
    '        Dim iItem_Seq As Integer = 0
    '        '************ Start Datatable Import
    '        For i As Integer = 0 To oDT.Rows.Count - 1

    '            With oblPO
    '                '.saveHeader()

    '                .DocumentType_Index = Me.DocumentType_Index
    '                .AdvanceShipNoticeItem_Index = ""
    '                .AdvanceShipNotice_Index = ""
    '                .AdvanceShipNotice_No = "" ' ใช้ Function 
    '                .AdvanceShipNotice_Date = oDT.Rows(i).Item(3).ToString

    '                Dim strSplitDate As String = ""
    '                If strDocumentNo <> oDT.Rows(i).Item(1).ToString Then
    '                    '*********** BEGIN HEADER ********************
    '                    If oblPO.IsExitData("tb_AdvanceShipNotice", "AdvanceShipNotice_No", oDT.Rows(i).Item(1).Value.ToString) = True Then
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        iCount_Error_Header += 1

    '                        Continue For

    '                    Else 'ถ้าไม่ใช่ให้เก็บค่า RCV_No ไว้ใน  strDocumentNo
    '                        strDocumentNo = oDT.Rows(i).Item(1).ToString.Trim
    '                    End If
    '                    .Customer_Index = Me.Customer_Index

    '                    obl_Import_HDO.CustomerIndex = Me.Customer_Index
    '                    obl_Import_HDO.Ref_No2 = oDT.Rows(i).Item(1).ToString.Trim
    '                    obl_Import_HDO.DoNo = ""
    '                    obl_Import_HDO.DoDate = Now
    '                    obl_Import_HDO.CustomerShippingNo = oDT.Rows(i).Item(2).ToString.Trim
    '                    obl_Import_HDO.CustomerShippingIndex = Me.GetCareOf_Index(oDT.Rows(i).Item(2).ToString.Trim, Me.Customer_Index).Trim.ToString
    '                    obl_Import_HDO.Remark = "" 'oDT.Rows(i).Item(5).ToString.Trim

    '                    If Me._DEFAULT_CURRENCY_INDEX Is Nothing Then
    '                        obl_Import_HDO.Currency = ""
    '                    Else
    '                        obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
    '                    End If

    '                    obl_Import_HDO.DocumentTypeIndex = Me.DocumentType_Index
    '                    obl_Import_HDO.EPSON_Location_Index = Me.EPSON_Location_Index

    '                    obl_Import_HDO.SalesOrderInsert()
    '                    iCount_Comp_Header += 1

    '                    '*********** END HEADER ********************

    '                End If

    '            End With
    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Old Code
    Public Function StartImportASNExcel_Old() As Boolean
        Try
            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim oDT As New DataTable
            Dim oblPO As New Import_ASN
            Dim strSKU_Id As String = ""
            Dim strSKU_Name_th As String = ""
            Dim strPackage_Id As String = ""
            Dim strPackage_Index As String = ""
            Dim strSku_Index As String = ""

            oDT = Me.DataSource
            Dim iItem_Seq As Integer = 0

            For i As Integer = 0 To oDT.Rows.Count - 1

                With oblPO
                    .DocumentType_Index = SetDEFAULT_ASN_DOCUMENTTYPE_INDEX()
                    .AdvanceShipNoticeItem_Index = ""
                    .AdvanceShipNotice_Index = ""
                    .AdvanceShipNotice_No = oDT.Rows(i).Item("DOC_No").ToString
                    .AdvanceShipNotice_Date = oDT.Rows(i).Item("DOC_Date").ToString
                    'GetDEFAULT_CUSTOMER_INDEX()
                    '**** Insert Customer
                    .Customer_Index = "" ' Me.CustomerIndex
                    Dim strCustomer_Id As String = oDT.Rows(i).Item("Customer_Id").ToString
                    Dim strCustomer_Name_Th As String = oDT.Rows(i).Item("Customer_Name").ToString
                    If IsExitData("ms_Customer", "Customer_Id", strCustomer_Id) = False Then
                        Dim oCustomer As New ms_Customer(ms_Customer.enuOperation_Type.ADDNEW)
                        .Customer_Index = oCustomer.SaveData("", strCustomer_Id, " ", strCustomer_Name_Th, "", "0010000000001", "", "", "", "", "", "", "", "", "", "", "", "", "0010000000001", 0, 0, 0, "Import data", 0, "", 1, "", "", "", "", "", strCustomer_Name_Th)
                    End If

                    .Customer_Index = .GetIndexByID("ms_Customer", "Customer_Index", "Customer_Id", strCustomer_Id)
                    '**** End Customer
                    '**** Insert Supplier
                    .Supplier_Index = ""
                    Dim strSupplier_Id As String = oDT.Rows(i).Item("Supplier_ID").ToString
                    Dim strSupplier_Name_Th As String = oDT.Rows(i).Item("Supplier_Name").ToString
                    If strSupplier_Id.Trim <> "" Then
                        If IsExitData("ms_Supplier", "Supplier_ID", strSupplier_Id) = False Then
                            Dim oms_Supplier As New ms_Supplier(ms_Supplier.enuOperation_Type.ADDNEW)
                            oms_Supplier.SaveData("", strSupplier_Id, " ", strSupplier_Name_Th, "", "0010000000001", "", "", "", "", "", "", "", "", "", "", "", "", "0010000000001", 0, 0, 0, "Import data", 0, "", 1, "", "", "", "", "", strSupplier_Name_Th, "")
                            .Supplier_Index = .GetIndexByID("ms_Supplier", "Supplier_Index", "Supplier_ID", "")
                        End If
                        .Supplier_Index = GetIndexByID("ms_Supplier", "Supplier_Index", "Supplier_ID", strSupplier_Id)
                    End If
                    '**** End Supplier

                    .Expected_Delivery_Date = oDT.Rows(i).Item("Delivery_Date").ToString
                    .Plot = oDT.Rows(i).Item("Lot_No").ToString
                    .Qty = oDT.Rows(i).Item("QTY").ToString
                    '.total_Qty()
                    '**** Insert SKU
                    strSKU_Id = oDT.Rows(i).Item("SKU_ID").ToString
                    strSKU_Name_th = oDT.Rows(i).Item("SKU_NAME").ToString
                    strPackage_Id = oDT.Rows(i).Item("UOM").ToString

                    If IsExitData("ms_SKU", "Sku_Id", strSKU_Id) = False Then
                        strPackage_Index = SavePackage(strPackage_Id)
                        strSku_Index = SaveSKU(strPackage_Index, strSKU_Id, strSKU_Name_th, strSKU_Name_th, Me.Customer_Index, Me.Supplier_Index)
                        Me.SaveSKURatio(strSku_Index, strPackage_Index, 1) 'Default Ratio=1 ?
                        Me.Sku_Index = strSku_Index
                    Else
                        .Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "Sku_Id", strSKU_Id)

                        'If IsCheckPackage(strPackage_Id, .Sku_Index) = False Then
                        '    ' Auto Insert Package were Package and Update Package For Sku This
                        '    strPackage_Index = SavePackage(strPackage_Id)
                        '    Me.SaveSKURatio(.Sku_Index, strPackage_Index, 1) 'Default Ratio=1 ?
                        'End If
                    End If

                    .Package_Index = GetPackageSku(.Sku_Index) 'GetPackage_Index(.Sku_Index, strPackage_Id)

                    '**** End SKU
                    If IsExitData("tb_AdvanceShipNotice", "AdvanceShipNotice_No", oDT.Rows(i).Item("DOC_No").ToString) = True Then
                        .AdvanceShipNotice_Index = getAdvanceShipNoticeIndex(oDT.Rows(i).Item("DOC_No").ToString)
                        iItem_Seq += 1
                    Else
                        iItem_Seq = 1
                        .saveHeader()
                    End If

                    .Item_Seq = iItem_Seq

                    .SaveDetail()


                End With
            Next

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
            If GetDataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetPackageSku(ByVal pstrsku_Index As String) As String
        Try
            Dim strSQL As String
            Dim package_index As String = ""

            strSQL = "SELECT   package_index   "

            strSQL &= " FROM       ms_sku "

            strSQL &= "   WHERE ms_SKU.status_id <> -1"
            strSQL &= "    and ms_SKU.sku_Index ='" & pstrsku_Index & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            package_index = GetDataTable.Rows(0).Item("package_index")

            Return package_index

        Catch ex As Exception
            Throw ex
            Return Package_Index
        Finally
            disconnectDB()
        End Try
    End Function


#End Region

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
    Function GetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        'Public CustomerID As String = ""
        'Public CustomerName As String = ""

        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                CustomerID = objDT.Rows(0).Item("Customer_Id").ToString
                CustomerName = objDT.Rows(0).Item("Customer_Name").ToString

            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return CustomerID
        'Return CustomerName
        'Return CustomerIndex
    End Function

    Public Function getAdvanceShipNoticeIndex(ByVal AdvanceShipNotice_No As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT AdvanceShipNotice_Index FROM tb_AdvanceShipNotice WHERE AdvanceShipNotice_NO = '" & AdvanceShipNotice_No & "'"

            strSQL &= " And Status <> -1"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)
            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)("AdvanceShipNotice_Index").ToString
            End If
            Return ""
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSOURCE_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String


            strSQL &= " SELECT * " 'get
            strSQL &= " FROM ms_Customer_Receive_Location "
            strSQL &= " WHERE Customer_Receive_Location_Id = '" & pstrModel & "' and Customer_Index = '" & pstrCustomer & "' and Status_id <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("Customer_Receive_Location_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
    Public Function GetPackage_Index_Import(ByVal pstrSKU_Index As String, ByVal pstrCustomer_Index As String) As String
        'ค้นหา sku_index จาก model
        Try

            Dim strSQL As String = ""
            Dim strReturn As String
            strSQL = "SELECT * FROM ms_SKU WHERE SKU_Index = '" & pstrSKU_Index & "' and Customer_Index = '" & pstrCustomer_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("Package_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            Throw ex
        End Try


    End Function
    Public Sub getSKU_Detail_Update(ByVal pstrSku_Id As String, ByVal pstrCustomer_Index As String)

        'Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try
            If pstrSku_Id.Trim = "" Then Exit Sub

            pstrSku_Id = pstrSku_Id.Replace("'", "''").ToString
            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE Sku_Id Like '" & pstrSku_Id & "%' AND status_id  not in (-1) "   'FOR TIMCO
            'strSQL &= "WHERE Sku_Id = '" & pstrSku_Id & "' AND status_id  not in (-1) "
            If Not pstrCustomer_Index = "" Then
                strSQL &= " AND Customer_Index ='" & pstrCustomer_Index & "'"
            End If

            strSQL &= " Order By Sku_Id "

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
End Class

