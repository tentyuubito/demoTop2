Public Class frmSelectReport
    Private _report_name As String
    Private _report_id As String
    Private _Report_Index As String
    Private _Report_View As String
    Private _CloseType As Boolean = True
    Public Property CloseType() As Boolean
        Get
            Return _CloseType
        End Get
        Set(ByVal value As Boolean)
            _CloseType = value
        End Set
    End Property
    Public Property report_name() As String
        Get
            Return _report_name
        End Get
        Set(ByVal value As String)
            _report_name = value
        End Set
    End Property

    Public Property report_id() As String
        Get
            Return _report_id
        End Get
        Set(ByVal value As String)
            _report_id = value
        End Set
    End Property
    Public Property Report_Index() As String
        Get
            Return _Report_Index
        End Get
        Set(ByVal value As String)
            _Report_Index = value
        End Set
    End Property
    Public Property Report_View() As String
        Get
            Return _Report_View
        End Get
        Set(ByVal value As String)
            _Report_View = value
        End Set
    End Property

    Private Sub frmSelectReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oBj As New cls_SP_Report
            Dim dt As New DataTable
            dt = oBj.getReport()
            grdReport.AutoGenerateColumns = False
            grdReport.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOut.Click
        CloseType = False
        Me.Close()
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            _report_id = grdReport.CurrentRow.Cells("col_report_id").Value.ToString
            _report_name = grdReport.CurrentRow.Cells("col_report_name").Value.ToString
            _Report_Index = grdReport.CurrentRow.Cells("col_report_index").Value.ToString
            _Report_View = grdReport.CurrentRow.Cells("col_report_view").Value.ToString
            CloseType = True
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdReport_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdReport.CellDoubleClick
        Try
            btnSelect_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class