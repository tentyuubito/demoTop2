Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports WMS_STD_Formula.W_Module
'Imports WMS_STD_MASTER_Formula.W_Function
Imports System.Collections.Generic
Imports WMS_STD_Formula
Imports WMS_STD_Master
Public Class config_Report_TNP : Inherits DBType_SQLServer

    Private Report_Path As String = ""


#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Report_Name As String
    Private _Seq As Integer
    Private _View_Name As String
    Private _Report_Part As String
    Private _Description As String
    Private _Str1 As String
    Private _Str2 As String
    Private _Str3 As String
    Private _Str4 As String
    Private _Str5 As String

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

    Public Property Seq() As Integer
        Get
            Return _Seq
        End Get
        Set(ByVal Value As Integer)
            _Seq = Value
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

    Public Property Report_Part() As String
        Get
            Return _Report_Part
        End Get
        Set(ByVal Value As String)
            _Report_Part = Value
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


#End Region

#Region " SELECT DATA "

    Public Sub GetAllAsDataTable()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     * " & _
             " FROM config_Report " & _
             " WHERE    status_id = 0 "
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

    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String) As ReportDocument
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

            Dim dt As New DataTable
            Dim dr As DataRow()

            dt = ods.Tables(0)
            Dim intCountr As Integer = 0
            Dim dra As DataRow

            'Dim ODT_Temp As New DataTable
            'Dim ODR_Temp As DataRow
            For z As Integer = 0 To dt.Rows.Count - 1
                dr = dt.Select("TAG_No = '" & dt.Rows(z)("TAG_No") & "'")

                'ODT_Temp = dt.Clone

                intCountr = dr.Length
                If intCountr < 4 Then
                    For ir As Integer = intCountr To 3
                        dra = dt.NewRow
                        dra("TAG_No") = dt.Rows(z)("TAG_No")
                        dt.Rows.Add(dra)
                    Next
                Else
                    'Do
                    '    ODR_Temp = dr(0)
                    '    ODT_Temp.Rows.Add(dr(0))
                    '    If ODT_Temp.Rows.Count <> 4 Then
                    '        ODT_Temp.Rows.RemoveAt(odt.Rows.Count)
                    '    End If
                    'Loop While ODT_Temp.Rows.Count = 4

                End If


            Next

        Next
        oCrystal.SetDataSource(ods)
        Return oCrystal
    End Function

    'Public Sub RemoveAt(Of T)(ByRef arr As T(), ByVal index As Integer)
    '    Dim uBound = arr.GetUpperBound(0)
    '    Dim lBound = arr.GetLowerBound(0)
    '    Dim arrLen = uBound - lBound

    '    If index < lBound OrElse index > uBound Then
    '        Throw New ArgumentOutOfRangeException( _
    '        String.Format("Index must be from {0} to {1}.", lBound, uBound))

    '    Else
    '        'create an array 1 element less than the input array
    '        Dim outArr(arrLen - 1) As T
    '        'copy the first part of the input array
    '        Array.Copy(arr, 0, outArr, 0, index)
    '        'then copy the second part of the input array
    '        Array.Copy(arr, index + 1, outArr, index, uBound - index)

    '        arr = outArr
    '    End If
    'End Sub

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
            If GetDataTable.Rows.Count <> 0 Then
                _dataTable = GetDataTable
            Else
                GetDataReport_Developer(pstrReportName)
            End If

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


            If GetDataTable.Rows.Count <> 0 Then
                _dataTable = GetDataTable
            Else
                GetDataReport_Master(pstrReportName)
            End If

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


    Public Function getView_Report(ByVal ViewName As String, ByVal pstrWhere As String) As DataTable
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select * from " & ViewName & " where 1=1  "

            SetSQLString = strSQL & pstrWhere

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



    Public Function GetReportInfo2(ByVal pstrReportName As String, ByVal pstrWhere As String, Optional ByVal pstrOrderby As String = "") As ReportDocument
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

            ods.Tables.Add(GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere)) 'pstrOrderby

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            If odt.Rows(i)("str1").ToString.ToLower = "image" Then
                ods.Tables(i).Columns.Add(New DataColumn("Image_Data", GetType(System.Byte())))

                For Each odrData As DataRow In _dataTable.Rows
                    odrData("Image_Data") = (odrData("Image_Path")) 'ConvertFileToByte
                Next
                ods.Tables(i).AcceptChanges()
            End If

            oCrystal.SetDataSource(ods)
            _dataTable = ods.Tables(0)
        Next

        Return oCrystal

    End Function
#End Region

#Region " INSERT DATA "

#End Region

#Region " UPDATE DATA "

#End Region

#Region " DELETE DATA "

#End Region

#Region "   Report Store Parameter   "

    Public Function _ExecQuery_StoredProc_Parameter_Object(ByVal str_storeName As [String], ByVal oparam As List(Of Store_Proc_Parameter)) As DataTable
        Try
            Dim dt_detail As New DataTable("dt_result")
            DataAdapter = New SqlClient.SqlDataAdapter()

            'Step 1 : Connecttion
            If Connection.State = ConnectionState.Open Then Connection.Close()
            connectDB()

            With SQLServerCommand
                .CommandText = str_storeName
                .CommandType = CommandType.StoredProcedure
                .Connection = Connection
                .CommandTimeout = 0

                'Step 2 : Add Parameter
                'Dim j As Integer = 0
                'While j < str_param.Length
                '    SQLServerCommand.Parameters.AddWithValue(DirectCast(str_param(j), [String]), str_param(j + 1))
                '    j += 2
                'End While
                For Each ioparameter As Store_Proc_Parameter In oparam
                    SQLServerCommand.Parameters.AddWithValue(DirectCast(ioparameter.Parameter, [String]), ioparameter.Parameter_Value)
                Next
            End With
            'Step 3 : Add Command and Datable
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.Fill(dt_detail)

            Return dt_detail
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
#End Region



End Class
Public Class Store_Proc_Parameter

    Private _Parameter As String
    Public Property Parameter() As String
        Get
            Return _Parameter
        End Get
        Set(ByVal value As String)
            _Parameter = value
        End Set
    End Property


    Private _Parameter_Value As Object
    Public Property Parameter_Value() As Object
        Get
            Return _Parameter_Value
        End Get
        Set(ByVal value As Object)
            _Parameter_Value = value
        End Set
    End Property


End Class

Public Class ImageReport1

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