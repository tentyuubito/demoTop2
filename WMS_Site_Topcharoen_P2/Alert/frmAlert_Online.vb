Imports System.Windows.Forms
Imports WMS_STD_INB_ASN_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_OUTB_SO_Datalayer
Public Class frmAlert_Online


    Private Sub frmAleft_Online_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ASN
        grdASNAlert.AutoGenerateColumns = False
        getASNData()
        grdSOAlert.AutoGenerateColumns = False
        getSOData()

    End Sub

    Private Sub getASNData()

        Dim objASN As New tb_AdvanceShipNotice
        Dim odtASN As DataTable
        Try
            objASN.GetASN_View(" AND Status=1 AND AdvanceShipNotice_Date = '" & Now.Date.ToString("yyyy/MM/dd") & "'")
            odtASN = objASN.GetDataTable
            With grdASNAlert
                .DataSource = odtASN
            End With

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub getSOData()
        Dim objSalesOrder As New tb_SalesOrder
        Dim odtSalesOrder As DataTable = New DataTable

        Try
            objSalesOrder.getSOMain("" & " AND Status=1 AND SalesOrder_Date = '" & Now.Date.ToString("yyyy/MM/dd") & "'")
            odtSalesOrder = objSalesOrder.GetDataTable
            grdSOAlert.DataSource = odtSalesOrder
            With grdSOAlert
                .DataSource = odtSalesOrder
            End With

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmAleft_Online_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub


End Class