Imports WMS_STD_Master.W_Language

Public Class frmPopup_for_ExportExcel

    Private _Data As DataTable
    Private _Data_ReadOnly As Boolean

    Public Sub New(ByVal Data As DataTable, Optional ByVal DataReadOnly As Boolean = False)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            Throw New Exception("Data not found")
        End If

        Me._Data = Data
        Me._Data_ReadOnly = DataReadOnly
    End Sub

    Private Sub InitialProperty()
        Try
            With Me.grdData
                .AutoGenerateColumns = True
                .AllowUserToResizeRows = False
                .ReadOnly = Me._Data_ReadOnly

                .DataSource = Me._Data
            End With

            SetLabelSum()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetLabelSum()
        Try
            Me.lblSum.Text = "รวม : " & FormatNumber(Me._Data.Rows.Count, 0) & " รายการ"

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub frmPopup_for_ExportExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InitialProperty()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            KascoExcel.ExportToExcel(Me.grdData, "Barcode B1")

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class

