Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports WMS_STD_Formula.W_Module

'Imports WMS_STD_MASTER_Formula.W_Function
Imports System.Collections.Generic
Imports WMS_STD_Formula
Imports WMS_STD_MASTER
Public Class config_Report_Invoice : Inherits DBType_SQLServer




#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Report_Name As String
    Private _View_Name As String
    Private _Report_Path As String
    Private _Description As String
    Private _Str1 As String
    Private _Str2 As String
    Private _Str3 As String
    Private _Str4 As String
    Private _Str5 As String


    Private _report_Group As String
    Private _seq As Integer
    Private _dataSet_Name As String
    Private _dataTable_Name As String

    Private _process_Id As Integer
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _status_id As Integer = 0
    Private _isVisible As Integer
    Private _status As Integer = -9
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
    Public Property Report_Name() As String
        Get
            Return _Report_Name
        End Get
        Set(ByVal Value As String)
            _Report_Name = Value
        End Set
    End Property
    Public Property Report_Group() As String
        Get
            Return _report_Group
        End Get
        Set(ByVal Value As String)
            _report_Group = Value
        End Set
    End Property
    Public Property Seq() As Integer
        Get
            Return _seq
        End Get
        Set(ByVal Value As Integer)
            _seq = Value
        End Set
    End Property
    Public Property DataSet_Name() As String
        Get
            Return _dataSet_Name
        End Get
        Set(ByVal Value As String)
            _dataSet_Name = Value
        End Set
    End Property
    Public Property DataTable_Name() As String
        Get
            Return _dataTable_Name
        End Get
        Set(ByVal Value As String)
            _dataTable_Name = Value
        End Set
    End Property
    Public Property View_Name() As String
        Get
            Return _View_Name
        End Get
        Set(ByVal Value As String)
            _View_Name = Value
        End Set
    End Property

    Public Property Report_Path() As String
        Get
            Return _Report_Path
        End Get
        Set(ByVal Value As String)
            _Report_Path = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal Value As String)
            _Description = Value
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
    Public Property status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
        End Set
    End Property
    Public Property IsVisible() As Integer
        Get
            Return _isVisible
        End Get
        Set(ByVal Value As Integer)
            _isVisible = Value
        End Set
    End Property

#End Region

#Region " SELECT DATA "

    Public Sub GetAllAsDataTable()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT * " & _
             " FROM config_Report " & _
             " WHERE status_id = 0 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub
    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal dsData As DataSet) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then

            Me.GetDataReport_Developer(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport(pstrReportName)
            odt = Me.GetDataTable
        End If

        For i As Integer = 0 To odt.Rows.Count - 1

            If odt.Rows(i)("IsVisible") = 1 Then
                oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            End If

            ods = dsData

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            If odt.Rows(i)("str1").ToString.ToLower = "image" Then
                ods.Tables(i).Columns.Add(New DataColumn("Image_Data", GetType(System.Byte())))

                For Each odrData As DataRow In _dataTable.Rows
                    odrData("Image_Data") = W_Function.ConvertFileToByte(odrData("Image_Path"))
                Next
                ods.Tables(i).AcceptChanges()
            End If

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal

    End Function

    Public Function GenCodeToByte(ByVal pText As String) As Byte()
        Try
            Dim ResultByte() As Byte
            Dim pic As System.Drawing.Image = GenCode128.Code128Rendering.MakeBarcodeImage(pText, 1, False)
            Dim BitmapConverter As System.ComponentModel.TypeConverter
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Function GetPathIMG(ByVal User_Index As String) As DataTable
        Try
            Dim _exc As New DBType_SQLServer
            Dim strSQL As String = ""
            strSQL = String.Format("select * from se_User where user_index = '{0}'", User_Index)
            Return _exc.DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String, ByVal _Customer_index As String) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then

            Me.GetDataReport_Developer(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport(pstrReportName)
            odt = Me.GetDataTable
        End If
        'Dim Total_DiscountSalse, Total_Real As Double
        'Dim Vat, Vat_Nomal, Discount As Integer
        For i As Integer = 0 To odt.Rows.Count - 1

            If odt.Rows(i)("IsVisible") = 1 Then
                oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            End If

            ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))
            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString


            If Not ods.Tables(i).Columns.Contains("intMaster") Then
                ods.Tables(i).Columns.Add("intMaster", GetType(Integer))
            End If

            'Dim syAutoGen As New Sy_AutoyyyyMM
            'Dim DocNO As String = ""
            'connectDB()
            'Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
            'Try
            '    If _Customer_index = "0010000000010" Then 'BR
            '        DocNO = syAutoGen.Auto_DocumentType_Number(Connection, myTrans, "0010000000502", ods.Tables(0).Rows(0).Item("Document_Date").ToString, "")
            '    ElseIf _Customer_index = "0010000000012" Then 'SP
            '        DocNO = syAutoGen.Auto_DocumentType_Number(Connection, myTrans, "0010000000501", ods.Tables(0).Rows(0).Item("Document_Date").ToString, "")
            '    End If
            '    syAutoGen = Nothing
            '    myTrans.Commit()
            'Catch ex As Exception
            '    myTrans.Rollback()
            '    Throw ex
            'End Try
            If Not ods.Tables(i).Columns.Contains("Barcode_No") Then
                If i > 0 Then
                    ods.Tables(i).Columns.Add(New DataColumn("ImgUser", GetType(System.Byte())))
                    ods.Tables(i).Columns.Add(New DataColumn("ImgDriver", GetType(System.Byte())))
                    ods.Tables(i).Columns.Add(New DataColumn("Report_Name"))
                    ods.Tables(i).Columns.Add(New DataColumn("Head_Rpt"))
                    For Each odrData As DataRow In _dataTable.Rows
                        'Header
                        odrData("Report_Name") = odt.Rows(i).Item("Report_Name").ToString
                        odrData("Head_Rpt") = odt.Rows(i).Item("Str1").ToString
                        odrData("ORDER_NO") = odt.Rows(i).Item("Number_Footer").ToString

                        Select Case pstrReportName.ToUpper
                            Case "RPT_KSL_INVOICE_PRINTFROM"
                                odrData("Head_Rpt") = "(สำเนา)ใบกำกับภาษี / ใบส่งของ"
                            Case Else
                        End Select

                        'Dim dtPath As New DataTable
                        'dtPath = GetPathIMG(odt.Rows(i).Item("User_Index").ToString)
                        'odrData("ImgUser") = GenPicToByte(dtPath.Rows(0).Item("Image_Path").ToString)
                        ' If dtPath.Rows.Count > 0 Then
                        If Not IsDBNull(odrData("Image_Path")) Then
                            odrData("ImgUser") = GenPicToByte(odrData("Image_Path"))
                        End If
                        ' End If

                        If Not IsDBNull(odrData("ImgPath_Driver")) Then
                            odrData("ImgDriver") = GenPicToByte(odrData("ImgPath_Driver"))
                        End If

                        odrData("intMaster") = 0
                        Select Case odt.Rows(i)("Report_Path").ToString.ToUpper
                            Case "\Report\Update\TAX\rptInvoice_Final.rpt".ToUpper ', "\Report\Update\TAX\rptRecept_Final.rpt".ToUpper
                                If pstrReportName = "RPT_KSL_Invoice" Then
                                    odrData("intMaster") = 1
                                End If


                        End Select
                    Next
                Else
                    'Deatail
                    ods.Tables(i).Columns.Add(New DataColumn("Barcode_No", GetType(System.Byte())))
                    ods.Tables(i).Columns.Add(New DataColumn("QRCode", GetType(System.Byte())))
                    ods.Tables(i).Columns.Add(New DataColumn("Line", GetType(System.Int32)))
                    ods.Tables(i).Columns.Add(New DataColumn("Page_Number"))
                    For Each odrData As DataRow In _dataTable.Rows
                        odrData("Barcode_No") = GenCodeToByte(odrData("Invoice_No"))
                        odrData("QRCode") = GenQRCodeToByte(odrData("Document_No"))
                        odrData("Line") = Math.Ceiling(odrData("Sku_Name").ToString.Length / 44)

                        odrData("intMaster") = 0
                        Select Case odt.Rows(i)("Report_Path").ToString.ToUpper
                            Case "\Report\Update\TAX\rptInvoice_Final.rpt".ToUpper ', "\Report\Update\TAX\rptRecept_Final.rpt".ToUpper
                                If pstrReportName = "RPT_KSL_Invoice" Then
                                    odrData("intMaster") = 1
                                End If
                        End Select

                    Next
                End If

            End If
            ods.Tables(i).AcceptChanges()

        Next
        ' View Seq
        Dim displayView = New DataView(ods.Tables(1))
        Dim subset As DataTable = displayView.ToTable(False, "Seq", "Total_Amount", _
        "Discount", "Total_Discount", "Sum_Total", "Vat", "Total_Vat", "Vat_Nomal", _
        "Total_Vat_Nomal", "ORDER_NO", "Report_Name", "Head_Rpt", "ImgUser", _
        "ImgDriver", "WinSpeed", "Customer_Name", "User_Name_INV", "Invoice_Date", "intMaster")

        ods.Tables.Remove("View_Seq")
        ods.Tables.Add(subset)

        'If Not ods.Tables(0).Columns.Contains("intMaster") Then
        '    ods.Tables(0).Columns.Add("intMaster", GetType(Integer))
        'End If

        Dim TotalRow As Integer = 18
        Dim NumRowLast As Integer = 0
        Dim NumPage As Integer = 1
        For Each dtrows As DataRow In ods.Tables(0).Rows

            If TotalRow < dtrows("Line") Then
                NumPage += 1
                TotalRow = 18
                dtrows("Page_Number") = NumPage
            Else
                dtrows("Page_Number") = NumPage
            End If
            TotalRow -= dtrows("Line")

            'dtrows("intMaster") = 0
            ''Select Case odt.Rows(i)("Report_Path").ToString.ToUpper
            ''    Case "\Report\Update\TAX\rptInvoice_Final.rpt".ToUpper, "\Report\Update\TAX\rptRecept_Final.rpt".ToUpper
            ''        dtrows("intMaster") = 1
            ''End Select

        Next
        Dim _dt As New DataTable
        _dt = subset.Copy
        _dt.Rows.Clear()
        _dt.Rows.Add(subset.Rows(0).ItemArray)

        Dim NumRow = ods.Tables(0).Compute("sum(Line)", "Page_Number = " & NumPage)
        ' NumRow = Math.Floor(NumRow)
        'While NumRow Mod 18 <> 0
        'While ods.Tables(1).Rows.Count Mod 18 <> 0
        '    ods.Tables(1).Merge(_dt)
        '    ' NumRow += 1
        'End While
        'RunSeq
        Dim K As Integer = 0

        For Each dr As DataRow In ods.Tables(1).Rows()
            If K < ods.Tables(0).Rows.Count Then
                ods.Tables(0).Rows(K).Item("Seq") = K + 1
            End If
            'If K < 18 Then
            K += 1
            'Else
            '    K = 1
            'End If
            dr("Seq") = K
        Next


        oCrystal.SetDataSource(ods)

        Select Case pstrReportName.ToUpper
            Case "RPT_KSL_INVOICE", "RPT_KSL_RECEPT"
                oCrystal.SetParameterValue("Head", "(ลูกค้า)")
            Case Else
                oCrystal.SetParameterValue("Head", "(บัญชี)")
        End Select

        Return oCrystal

    End Function

    Private Function GenPicToByte(ByVal ImgPath As String) As Byte()
        Try

            Dim ResultByte() As Byte
            Dim pic As System.Drawing.Image = System.Drawing.Image.FromFile(ImgPath)
            Dim BitmapConverter As System.ComponentModel.TypeConverter
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte

        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function
    Private Function GenQRCodeToByte(ByVal pText As String) As Byte()
        Try

            Dim ResultByte() As Byte
            Dim QRGen As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
            Dim pic As System.Drawing.Image
            Dim BitmapConverter As System.ComponentModel.TypeConverter
            QRGen.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
            pic = QRGen.Encode(pText)
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte

        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try

    End Function
    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String, ByVal poImageReport As List(Of ImageReport)) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then

            Me.GetDataReport_Developer(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport(pstrReportName)
            odt = Me.GetDataTable
        End If

        For i As Integer = 0 To odt.Rows.Count - 1
            oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)

            ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            'Insert Image to Frist dataset
            If poImageReport.Count > 0 Then
                If i = 0 Then 'Frist DataSet (Header)
                    With ods.Tables(i)
                        For Each oImgReport As ImageReport In poImageReport
                            If .Columns.Contains(oImgReport.ImgFieldName) = False Then
                                .Columns.Add(oImgReport.ImgFieldName, GetType(System.Byte()))
                            End If
                        Next

                        If .Rows.Count > 0 Then
                            For iRow As Integer = 0 To .Rows.Count - 1
                                If iRow <= poImageReport.Count - 1 Then
                                    .Rows(iRow)(poImageReport(iRow).ImgFieldName) = W_Function.ConvertFileToByte(poImageReport(iRow).ImgPath)
                                End If
                            Next
                        End If

                    End With

                End If
            End If

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal
    End Function
    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String, ByVal podtMerge As DataTable) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then

            Me.GetDataReport_Developer(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport(pstrReportName)
            odt = Me.GetDataTable
        End If

        For i As Integer = 0 To odt.Rows.Count - 1
            oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)

            ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            'Insert data to Frist dataset
            If podtMerge.Rows.Count > 0 Then
                If i = 0 Then
                    With ods.Tables(i)

                        'Add Column
                        For Each oCol As DataColumn In podtMerge.Columns
                            If .Columns.Contains(oCol.ColumnName) = False Then
                                .Columns.Add(New DataColumn(oCol.ColumnName, oCol.DataType))
                            End If
                        Next

                        'Add Data Cell
                        For iRow As Integer = 0 To podtMerge.Rows.Count - 1
                            For Each oCol As DataColumn In podtMerge.Columns
                                .Rows(iRow)(oCol.ColumnName) = podtMerge.Rows(iRow)(oCol.ColumnName)
                            Next
                        Next

                    End With

                End If
            End If

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal
    End Function
    Public Sub GetDataReport_Developer(ByVal pstrReportName As String)
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM  config_Report_Developer "
            strSQL &= " WHERE     Report_Name = '" & pstrReportName & "'"
            strSQL &= " Order By Seq ASC"


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
    Public Sub GetDataReport_Master(ByVal pstrReportName As String)
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM  config_Report_Master "
            strSQL &= " WHERE     Report_Name = '" & pstrReportName & "'"
            strSQL &= " Order By Seq ASC"


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
    Public Function GetReportInfo_ReportMaster(ByVal pstrReportName As String, ByVal pstrWhere As String) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then

            Me.GetDataReport_Master(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport_Master(pstrReportName)
            odt = Me.GetDataTable
        End If

        For i As Integer = 0 To odt.Rows.Count - 1

            If odt.Rows(i)("IsVisible") = 1 Then
                oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            End If

            ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            If odt.Rows(i)("str1").ToString.ToLower = "image" Then
                ods.Tables(i).Columns.Add(New DataColumn("Image_Data", GetType(System.Byte())))

                For Each odrData As DataRow In _dataTable.Rows
                    odrData("Image_Data") = W_Function.ConvertFileToByte(odrData("Image_Path"))
                Next
                ods.Tables(i).AcceptChanges()
            End If

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal

    End Function
    Public Function GetReportInfo_ReportMaster(ByVal pstrReportName As String, ByVal podtDataReport As DataTable) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument
        Dim ods As New DataSet

        If WV_Report_Path = "" Then
            WV_Report_Path = AppDomain.CurrentDomain.BaseDirectory
            Me.GetDataReport_Master(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport_Master(pstrReportName)
            odt = Me.GetDataTable
        End If

        For i As Integer = 0 To odt.Rows.Count - 1
            oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            'ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))
            ods.Tables.Add(podtDataReport)
            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            ''Insert data to Frist dataset
            'If podtMerge.Rows.Count > 0 Then
            '    If i = 0 Then
            '        With ods.Tables(i)

            '            'Add Column
            '            For Each oCol As DataColumn In podtMerge.Columns
            '                If .Columns.Contains(oCol.ColumnName) = False Then
            '                    .Columns.Add(New DataColumn(oCol.ColumnName, oCol.DataType))
            '                End If
            '            Next

            '            'Add Data Cell
            '            For iRow As Integer = 0 To podtMerge.Rows.Count - 1
            '                For Each oCol As DataColumn In podtMerge.Columns
            '                    .Rows(iRow)(oCol.ColumnName) = podtMerge.Rows(iRow)(oCol.ColumnName)
            '                Next
            '            Next

            '        End With

            '    End If
            'End If

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal
    End Function
    Public Sub GetDataReport(ByVal pstrReportName As String)
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM  config_Report "
            strSQL &= " WHERE     Report_Name = '" & pstrReportName & "'"
            strSQL &= " Order By Seq ASC"


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
    Public Function GetReportData(ByVal pstrSQL As String, ByVal pstrWhere As String) As DataTable
        Dim strSQL As String = ""


        Try
            strSQL = " SELECT * "
            strSQL &= " FROM  " & pstrSQL
            strSQL &= " WHERE    1=1 " & pstrWhere

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

    'Public Sub GetReportName(ByVal pstrprocessID As String)
    '    Dim strSQL As String = ""

    '    Try

    '        strSQL = " SELECT * "
    '        strSQL &= " FROM  config_Report "
    '        strSQL &= " WHERE     process_ID = '" & pstrprocessID & "'"
    '        strSQL &= " Order By Seq ASC"


    '        SetSQLString = strSQL

    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub

    Public Sub GetListReportName(ByVal pstrprocessID As String)
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM  config_Report "
            strSQL &= " WHERE  IsVisible = 1 and    process_ID = '" & pstrprocessID & "'"
            strSQL &= " Order By Seq ASC"


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
    Public Function getTableFromReader(ByVal strSQL As String) As DataTable
        Try
            Dim dt As New DataTable

            connectDB()
            SetSQLString = strSQL
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            EXEC_Command()
            Dim objDr As SqlClient.SqlDataReader = Nothing
            objDr = GetDataReader
            dt.Load(objDr)
            objDr.Close()
            objDr = Nothing
            Return dt

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetReportInfo(ByVal pstrReportName As String) As ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New ReportDocument

        If WV_Report_Path = "" Then

            Me.GetDataReport_Developer(pstrReportName)
            odt = Me.GetDataTable
        Else

            Me.GetDataReport(pstrReportName)
            odt = Me.GetDataTable
        End If

        'For i As Integer = 0 To odt.Rows.Count - 1

        If odt.Rows.Count > 0 Then
            oCrystal.Load(WV_Report_Path & odt.Rows(0)("Report_Path").ToString)
        End If
        'Next

        Return oCrystal

    End Function

    Public Sub GetDataConfigReport(ByVal strWHERE As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT * " & _
             " FROM config_Report c Left join ms_Process m on c.Process_Id = m.Process_Id" & _
             " WHERE c.status_id = 0 "
            SetSQLString = strSQL & strWHERE
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub GetDatamsProcess()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT * " & _
             " FROM ms_Process " & _
             " WHERE status_id = 0 "
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
    Public Sub GetDataConfigReport(ByVal strName As String, ByVal strWhere As String)
        Dim strSQL As String = ""
        Try

            strSQL = " SELECT * FROM config_Report "
            strSQL &= " WHERE Report_Name = '" & strName & "' "
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

#End Region

#Region " INSERT DATA "
    Public Sub Insert()

        Dim strSQL As String = ""
        Try
            strSQL = " INSERT INTO config_Report "
            strSQL &= "(Report_Name,Report_Group,Seq,DataSet_Name,DataTable_Name,View_name,Report_Path,Description,Process_Id,add_by,add_branch,add_Date,status_id,IsVisible)"
            strSQL &= "VALUES"
            strSQL &= "(@pReport_Name,@pReport_Group,@pSeq,@pDataSet_Name,@pDataTable_Name,@pView_name,@pReport_Path,@pDescription,@pProcess_Id,@padd_by,@padd_branch,getdate(),@pstatus_id,@pIsVisible)"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@pReport_Name", SqlDbType.VarChar, 200).Value = _Report_Name
                .Add("@pReport_Group", SqlDbType.VarChar, 10).Value = _report_Group
                .Add("@pSeq", SqlDbType.Int).Value = _seq
                .Add("@pDataSet_Name", SqlDbType.VarChar, 200).Value = _dataSet_Name
                .Add("@pDataTable_Name", SqlDbType.VarChar, 200).Value = _dataTable_Name
                .Add("@pView_name", SqlDbType.VarChar, 200).Value = _View_Name
                .Add("@pReport_Path", SqlDbType.VarChar, 500).Value = _Report_Path
                .Add("@pDescription", SqlDbType.VarChar, 100).Value = _Description
                .Add("@pProcess_Id", SqlDbType.Int).Value = _process_Id
                .Add("@padd_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Add("@padd_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Add("@pstatus_id", SqlDbType.Int).Value = 0
                .Add("@pIsVisible", SqlDbType.Int).Value = _isVisible
            End With
            'SetSQLString = strSQL
            'SetCommandType = enuCommandType.Text
            'SetEXEC_TYPE = EXEC.NonQuery
            'connectDB()
            'EXEC_Command()
            DBExeNonQuery(strSQL, eCommandType.Text)


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
#End Region

#Region " UPDATE DATA "
    Public Function Update() As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " Update config_Report "
            strSQL &= " SET status_id=@status_id "
            strSQL &= ",Report_Group=@Report_Group "
            strSQL &= ",Seq=@Seq "
            strSQL &= ",DataSet_Name=@DataSet_Name "
            strSQL &= ",DataTable_Name=@DataTable_Name "
            strSQL &= ",View_Name=@View_Name "
            strSQL &= ",Report_Path=@Report_Path "
            strSQL &= ",Description=@Description "
            strSQL &= ",Process_Id=@Process_Id "
            strSQL &= ",update_by=@update_by "
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch=@update_branch "
            strSQL &= ",IsVisible=@IsVisible "
            strSQL &= "  WHERE Report_Name = @Report_Name  "

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Report_Name", SqlDbType.VarChar, 200).Value = _report_Name
                .Add("@Report_Group", SqlDbType.VarChar, 10).Value = _report_Group
                .Add("@Seq", SqlDbType.Int).Value = _seq
                .Add("@DataSet_Name", SqlDbType.VarChar, 200).Value = _dataSet_Name
                .Add("@DataTable_Name", SqlDbType.VarChar, 200).Value = _dataTable_Name
                .Add("@View_name", SqlDbType.VarChar, 200).Value = _view_Name
                .Add("@Report_Path", SqlDbType.VarChar, 500).Value = _report_Path
                .Add("@Description", SqlDbType.VarChar, 100).Value = _description
                .Add("@Process_Id", SqlDbType.Int).Value = _process_Id
                .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Add("@status_id", SqlDbType.Int).Value = 0
                .Add("@IsVisible", SqlDbType.Int).Value = _isVisible
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region " DELETE DATA "
    Public Function Delete() As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " Update config_Report "
            strSQL &= " SET status_id=@status_id "
            strSQL &= ",update_by=@update_by "
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch=@update_branch "
            strSQL &= "  WHERE Report_Name = @Report_Name  "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Report_Name", SqlDbType.VarChar, 13).Value = _report_Name
                .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Add("@status_id", SqlDbType.Int).Value = -1
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            disconnectDB()
        End Try
    End Function
#End Region




End Class

Public Class ImageReport

    Private _ImgPath As String = ""
    Property ImgPath() As String
        Get
            Return _ImgPath
        End Get
        Set(ByVal value As String)
            _ImgPath = value
        End Set
    End Property


    Private _ImgFieldName As String = ""
    Property ImgFieldName() As String
        Get
            Return _ImgFieldName
        End Get
        Set(ByVal value As String)
            _ImgFieldName = value
        End Set
    End Property


End Class