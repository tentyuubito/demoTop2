Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine



Public Class Loading_Report_Invoice
    Private _scalarOutput As String

    Private _Report_Name As String
    Public Property Report_Name() As String
        Get
            Return _Report_Name
        End Get
        Set(ByVal value As String)
            _Report_Name = value
        End Set
    End Property


    Private _Report_Condition As String
    Public Property Report_Condition() As String
        Get
            Return _Report_Condition
        End Get
        Set(ByVal value As String)
            _Report_Condition = value
        End Set
    End Property



    Private _ImageReport As DataTable
    Public Property ImageReport() As DataTable
        Get
            Return _ImageReport
        End Get
        Set(ByVal value As DataTable)
            _ImageReport = value
        End Set
    End Property



    Public Sub New(ByVal Report_Name As String, ByVal Report_Condition As String, Optional ByVal pImageReport As DataTable = Nothing)
        MyBase.New()
        Me._ImageReport = pImageReport
        Me._Report_Name = Report_Name
        Me._Report_Condition = Report_Condition
    End Sub


    Public Function LoadReport() As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Try
            Dim oCrystal As New ReportDocument
            Dim oconfig_Report As New config_Report_Invoice

            Select Case Me._Report_Name.ToUpper
                Case "TAGSTICKERPRINTOUT" 'with imager
                    oCrystal = oconfig_Report.GetReportInfo(Me._Report_Name, Me._Report_Condition, Me._ImageReport)
                Case Else
                    oCrystal = oconfig_Report.GetReportInfo(Me._Report_Name, Me._Report_Condition)
            End Select


            Return oCrystal

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class

Public Class Loading_Report_Customize
    Inherits DBType_SQLServer

    Function Report_Recive_Carco(ByVal Document_Index As String, ByVal Condition_Index As String, ByVal con_order As String) As DataSet
        Dim strSql As String = ""
        connectDB()
        Try

            strSql = " SELECT  * "
            strSql += " FROM VIEW_ASN_Heder "
            strSql += " WHERE  AdvanceShipNotice_Index ='" & Document_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .CommandText = strSql
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DS = New DataSet("dsRPT_ASN")
            DataAdapter.Fill(DS, "VIEW_RPT_ASN_Heder")


            strSql = "SELECT *  "
            strSql += " FROM  VIEW_ASN_Detail "
            strSql += " WHERE (AdvanceShipNotice_Index ='" & Document_Index & "') AND  (" & Condition_Index & "') "
            'strSql += " WHERE  (" & Condition_Index & "') "
            strSql += " ORDER BY " & con_order '" ORDER BY Sku_Id "

            With SQLServerCommand
                .Connection = Connection
                .CommandText = strSql
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.Fill(DS, "VIEW_RPT_ASN_Detail")

            Return DS
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

End Class

