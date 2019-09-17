Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports WMS_STD_Formula

Public Class frmReportViewerMain

    Private mDS As DataSet
    Dim objReportQuery As New mlLoading_Report

    Private _Report_Id As String = ""
    Private _Report_Name As String = ""
    Private _Document_Index As String = ""


    Public Property Report_Id() As String
        Get
            Return Me._Report_Id
        End Get
        Set(ByVal value As String)
            Me._Report_Id = value
        End Set
    End Property

    Public Property Report_Name() As String
        Get
            Return _Report_Name
        End Get
        Set(ByVal value As String)
            _Report_Name = value
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



    Private Sub frmReportViewerMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Select Case Me._Report_Name.ToUpper
                Case "RPTCHECKSTOCK"
                    Dim cry As New rptCheckStock
                    mDS = objReportQuery.Report_rptCheckStock(Me._Document_Index)
                    cry.SetDataSource(mDS)

                    CrystalReportViewer1.ReportSource = cry
                Case Else

            End Select

        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class mlLoading_Report
    Inherits DBType_SQLServer

    Public Function Report_rptCheckStock(ByVal StrCommand As String) As DataSet
        Dim strSql As String = ""
        connectDB()
        Try
            DS = New DataSet("dsCheckStock")
            Dim odtComparePutaway As New DataTable
            strSql = "    select * from VIEW_ACTION_For_ADJUST "

            SetSQLString = strSql & StrCommand
            EXEC_DataAdapter()
            odtComparePutaway = GetDataTable

            odtComparePutaway.TableName = "VIEW_ACTION_For_ADJUST"
            DS.Tables.Add(odtComparePutaway)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

        Return DS
    End Function

End Class