Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports System.Text
Imports System.IO
Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_INB_Barcode

Public Class frmReportMainTransportManifest_Update
    Dim objReportQuery As New Loading_Report_TransportManifest
    Dim mDS As DataSet
    Public ParameterRPT_1 As String = ""
    Public ParameterRPT_2 As String = ""
    Public ParameterRPT_3 As String = ""
    Public ParameterSku As String = ""
    Public ParameterSkuDes As String = ""
    Public ParametercbCustomer As String = ""
    Public ParameterNumLocation As String = ""
    Public parameterWarehouse As String = ""
    Public paremeterRoom As String = ""
    Public parameterAge As String = ""
    Public ParameterCustomer As String = ""
    Public ParameterStore As String = ""
    Public ParameterRRoom As String = ""

    Public Company_Name As String = ""
    Public Address As String = ""
    Public Postcode As String = ""
    Public Tel As String = ""
    Public Fax As String = ""
    Public Mobile As String = ""
    Public Email As String = ""
    Public District As String = ""
    Public Province As String = ""
    Public Image_Path As String = ""


    Private _Report_Id As String
    Private _Document_Index As String
    Private _Document_No As String


    Private _DT_TEMP As New DataTable

    Private _DS_TEMP As New DataSet
    Public Property DT_TEMP() As DataTable
        Get
            Return _DT_TEMP
        End Get
        Set(ByVal value As DataTable)
            _DT_TEMP = value
        End Set
    End Property

    Public Property DS_TEMP() As DataSet
        Get
            Return _DS_TEMP
        End Get
        Set(ByVal value As DataSet)
            _DS_TEMP = value
        End Set
    End Property

    Public Property Report_Id() As String
        Get
            Return Me._Report_Id
        End Get
        Set(ByVal value As String)
            Me._Report_Id = value
        End Set
    End Property

    Public Property Document_No() As String
        Get
            Return Me._Document_No
        End Get
        Set(ByVal value As String)
            Me._Document_No = value
        End Set
    End Property

    Public Property Document_Index() As String
        Get
            Return Me._Document_Index
        End Get
        Set(ByVal value As String)
            Me._Document_Index = value
        End Set
    End Property


    Private _TAG_NO As String
    Public Property TAG_NO() As String
        Get
            Return _TAG_NO
        End Get
        Set(ByVal value As String)
            _TAG_NO = value
        End Set
    End Property

    Private _Report_Name As String
    Public Property Report_Name() As String
        Get
            Return _Report_Name
        End Get
        Set(ByVal value As String)
            _Report_Name = value
        End Set
    End Property
    Private _chkDateDocType As Integer
    Public Property CheckDateDocType() As Integer
        Get
            Return _chkDateDocType
        End Get
        Set(ByVal value As Integer)
            _chkDateDocType = value
        End Set
    End Property

    Private _TransportChargeGroup_Id As Integer
    Public Property TransportChargeGroup_Id() As Integer
        Get
            Return _TransportChargeGroup_Id
        End Get
        Set(ByVal value As Integer)
            _TransportChargeGroup_Id = value
        End Set
    End Property


    Private _startDate As Date
    Public Property Start_Date() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal value As Date)
            _startDate = value
        End Set
    End Property

    Private _endDate As Date
    Public Property End_Date() As Date
        Get
            Return _endDate
        End Get
        Set(ByVal value As Date)
            _endDate = value
        End Set
    End Property


    Private _FuelPrice As Double
    Public Property FuelPrice() As Double
        Get
            Return _FuelPrice
        End Get
        Set(ByVal value As Double)
            _FuelPrice = value
        End Set
    End Property

    Private _StrAnd As String
    Public Property StrAnd() As String
        Get
            Return _StrAnd
        End Get
        Set(ByVal value As String)
            _StrAnd = value
        End Set
    End Property




    Private Sub frmReportPPM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Select Case Me._Report_Name.ToUpper
                Case "TRANSPORTMANIFEST_PRINTOUT"
                    Report_Name = "TRANSPORTMANIFEST_PRINTOUT"
                    Dim objBarcode As New Barcode

                    Dim oconfig_Report As New config_Report

                    'objBarcode.GenBarcode(Me._Document_No)
                    mDS = objReportQuery.GetTransportManifest_PrintOut(Me._Document_Index)
                    Dim DT As DataTable
                    DT = mDS.Tables(0)
                    DT.Columns.Add("Pic_TransportManifest_Barcode", GetType(System.Byte()))
                    DT.Columns.Add("Pic_SO_Barcode", GetType(System.Byte()))


                    'If DT.Rows.Count > 0 Then
                    '    DT.Rows(0)("Pic_TransportManifest_Barcode") = GenCodeToByte(Me._Document_No) ' ConvertFilePathToByte(Application.StartupPath & "\" & Me._Document_No & ".bmp")
                    'End If
                    ' Item
                    If DT.Rows.Count > 0 Then
                        For i As Integer = 0 To DT.Rows.Count - 1
                            DT.Rows(i)("Pic_TransportManifest_Barcode") = GenCodeToByte(Me._Document_No)
                            DT.Rows(i)("Pic_SO_Barcode") = GenCodeToByte(DT.Rows(i).Item("Invoice_No").ToString.Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace("<", "").Replace("|", "")) ' ConvertFilePathToByte(Application.StartupPath & "\" & DT.Rows(i).Item("SalesOrder_No").ToString.Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace("<", "").Replace("|", "") & ".bmp")
                            DT.Rows(i)("SalesOrder_No") = DT.Rows(i)("Invoice_No").ToString

                            'System.IO.File.Delete()
                        Next

                    End If



                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument


                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, mDS)

                    Dim SubDriver_Name1 As String = ""
                    Dim SubDriver_Name2 As String = ""
                    Dim SubDriver_Name3 As String = ""

                    Dim objManifest_Driver As New x_TransportManifest_Driver
                    Dim odtManifest_Driver As DataTable

                    ' Dim _TransportManifest_Index As String = Report_Id

                    objManifest_Driver.getTransportManifest_Driver(_Document_Index, 1)
                    odtManifest_Driver = objManifest_Driver.GetDataTable

                    If odtManifest_Driver.Rows.Count > 0 Then
                        Select Case odtManifest_Driver.Rows.Count
                            Case 1
                                SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
                            Case 2
                                SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
                                SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")

                            Case 3
                                SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
                                SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")
                                '  SubDriver_Name3 = odtManifest_Driver.Rows(2).Item("Driver_name")

                            Case Else
                                SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
                                SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")
                                ' SubDriver_Name3 = odtManifest_Driver.Rows(2).Item("Driver_name")

                        End Select
                    End If

                    oCrystal.SetParameterValue("SubDriver_Name1", SubDriver_Name1.ToString)
                    oCrystal.SetParameterValue("SubDriver_Name2", SubDriver_Name2.ToString)
                    ' cry.SetParameterValue("SubDriver_Name3", SubDriver_Name3.ToString)

                    CrystalReportViewer1.ReportSource = oCrystal

                Case "TRANSPORT_PRINTOUT"
                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Report_Name = "TRANSPORT_PRINTOUT"
                    ' save to c:\Barcode.bmp
                    Dim objBarcode As New Barcode
                    mDS = objReportQuery.Report_TransportManifest_PRINTOUT(Me._Document_Index)
                    Dim DT As DataTable
                    DT = mDS.Tables(0)
                    DT.Columns.Add("Pic", GetType(System.Byte()))
                    DT.Columns.Add("Pic1", GetType(System.Byte()))
                    DT.Columns.Add("sumVolume", GetType(System.Double))
                    If DT.Rows.Count > 0 Then
                        objBarcode.GenBarcode(Me._Document_No)
                        DT.Rows(0)("Pic") = GenCodeToByte(Me._Document_No) 'ConvertFilePathToByte(Application.StartupPath & "\" & Me._Document_No & ".bmp")
                        DT.Rows(0)("sumVolume") = DT.Compute("sum(Volume)", "")
                    End If
                    Dim Inv_No As String = ""
                    For Each drCry As DataRow In DT.Rows
                        If drCry("Invoice_No").ToString = "" Then
                            Inv_No = drCry("SalesOrder_No").ToString
                        Else
                            Inv_No = drCry("Invoice_No").ToString
                        End If
                        objBarcode.GenBarcode(Inv_No)
                        drCry("Pic1") = GenCodeToByte(Inv_No) 'ConvertFilePathToByte(Application.StartupPath & "\" & Inv_No & ".bmp")
                    Next
                    Dim oconfig_Report As New config_Report
                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, mDS)
                    CrystalReportViewer1.ReportSource = oCrystal

                Case "TRANSPORT_FUELPRICE"
                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim oReport_Transport_Fuel_Price As New Report_FuelPrice
                    Report_Name = "TRANSPORT_FUELPRICE"
                    oReport_Transport_Fuel_Price.pTransportChargeGroup_Id = Me._TransportChargeGroup_Id
                    oReport_Transport_Fuel_Price.CheckDateDocType = Me._chkDateDocType
                    oReport_Transport_Fuel_Price.Start_Date = Me._startDate
                    oReport_Transport_Fuel_Price.End_Date = Me._endDate
                    oReport_Transport_Fuel_Price.FuelPrice = Me._FuelPrice
                    oReport_Transport_Fuel_Price.StrAnd = Me._StrAnd
                    mDS = oReport_Transport_Fuel_Price.GenReport_FuelPrice_PrintOut()

                    mDS.Tables(0).Columns.Add.ColumnName = "FuelPrice"
                    mDS.Tables(0).Columns.Add.ColumnName = "DateCalBetween"

                    mDS.Tables(0).Columns.Add.ColumnName = "TransportCalType"
                    mDS.Tables(0).Columns.Add.ColumnName = "ShowDisplay"

                    For Each drFuel_Price As DataRow In mDS.Tables(0).Rows
                        drFuel_Price("FuelPrice") = Me._FuelPrice
                        drFuel_Price("DateCalBetween") = Me._startDate.ToString("dd/MM/yyyy") + "-" + Me._endDate.ToString("dd/MM/yyyy")
                        drFuel_Price("TransportCalType") = ParameterRPT_2
                        drFuel_Price("ShowDisplay") = ParameterRPT_1
                    Next


                    Dim oconfig_Report As New config_Report
                    'Dim oconfig_Report As New rptCalTransportFuelPrice
                    'oconfig_Report.SetDataSource(mDS)
                    'CrystalReportViewer1.ReportSource = oconfig_Report



                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, mDS)
                    CrystalReportViewer1.ReportSource = oCrystal

                Case "Print_Barcode_Transport".ToUpper
                    Report_Name = "Print_Barcode_Transport"
                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim oconfig_Report As New config_Report
                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, DS_TEMP)
                    CrystalReportViewer1.ReportSource = oCrystal

                Case "TRANSPORTMANIFEST_CHECKLIST".ToUpper, "TRANSPORTMANIFEST_CHECKLIST2".ToUpper
                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim oconfig_Report As New config_Report
                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, " AND TransportManifest_Index = '" & Document_Index & "'")
                    CrystalReportViewer1.ReportSource = oCrystal
                Case "TRANSPORTMANIFEST_PACKING".ToUpper
                    Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim oconfig_Report As New config_Report
                    oCrystal = oconfig_Report.GetReportInfo(Report_Name, " AND TransportManifest_Index = '" & Document_Index & "'")
                    CrystalReportViewer1.ReportSource = oCrystal
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function ConvertFilePathToByte(ByVal pstrPath As String) As Byte()
        Dim FilStr As New FileStream(pstrPath, FileMode.Open) ' เปิด file แบบ Stream เพื่ออ่านเป็น Binary
        Dim streamLength As Integer = Convert.ToInt32(FilStr.Length)
        Dim br As New BinaryReader(FilStr)
        Dim pic As Byte() = New Byte(streamLength) {}
        Try
            pic = br.ReadBytes(br.BaseStream.Length)
            FilStr.Close()
            br.Close()
            Return pic
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class Loading_Report_TransportManifest
    Inherits DBType_SQLServer

    Public Function Report_TransportManifest_PRINTOUT(ByVal TransportManifest_Index As String) As DataSet
        Dim strSql As String = ""
        connectDB()
        Try

            strSql = " SELECT * "
            strSql += " FROM    VIEW_RPT_TRANSPORTMANIFEST "
            strSql += " WHERE     TransportManifest_Index ='" & TransportManifest_Index & "'"

            With SQLServerCommand
                .Connection = Connection
                .CommandText = strSql
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DS = New DataSet("dsTransportManifest")
            DataAdapter.Fill(DS, "VIEW_RPT_TRANSPORTMANIFEST")

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

        Return DS
    End Function
    Public Function GetTransportManifest_PrintOut(ByVal pstrTransportManifest_index As String) As DataSet
        Dim strSql As String = ""
        connectDB()
        Try

            strSql &= "  SELECT *  "
            strSql &= "  FROM VIEW_SHARP_CDC_RPT_TRANSPORTMANIFEST "
            strSql &= "  WHERE 1=1 and "
            strSql &= "  TransportManifest_index ='" & pstrTransportManifest_index & "'"

            With SQLServerCommand
                .Connection = Connection
                .CommandText = strSql
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DS = New DataSet("dsTransportManifest_Sharp_CDC")
            DataAdapter.Fill(DS, "VIEW_SHARP_CDC_RPT_TRANSPORTMANIFEST")

        Catch ex As Exception
            Throw ex
        End Try


        Return DS

    End Function
End Class


