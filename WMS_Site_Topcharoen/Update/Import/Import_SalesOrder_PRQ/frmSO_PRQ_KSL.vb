Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_OUTB_SO_Datalayer
Imports System.Windows.Forms

Public Class frmSO_PRQ_KSL
    Private xQuery As String = ""
    Private xdt As New DataTable
    Private xDBCon As New DBType_SQLServer

    Private _SalesOrder_Index As String = ""
    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal value As String)
            _SalesOrder_Index = value
        End Set
    End Property


    Private Sub frmSO_PRQ_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            xQuery = " select SKU.Sku_Id,SKU.Str1 AS Sku_Name,PK.Description as UOM,SOPD.*"
            xQuery &= " from tb_SalesOrder_Production_KSL SOPD "
            xQuery &= " 	inner join tb_SalesOrder SO on SO.SalesOrder_Index = SOPD.SalesOrder_Index"
            xQuery &= " 	inner join ms_Sku SKU on SKU.Sku_Index = SOPD.Sku_Index"
            xQuery &= " 	inner join ms_Package PK on PK.Package_Index = SOPD.Package_Index"
            xQuery &= String.Format(" where SO.SalesOrder_Index = '{0}'", Me.SalesOrder_Index)
            xdt = xDBCon.DBExeQuery(xQuery)
            If xdt.Rows.Count > 0 Then
                With xdt.Rows(0)
                    Me.Sku_Id.Text = .Item("Sku_Id").ToString
                    Me.Sku_Name.Text = .Item("Sku_Name").ToString
                    Me.UOM.Text = .Item("UOM").ToString
                    Me.Qty.Text = .Item("Qty").ToString
                    Me.PRQ_ID.Text = .Item("PRQ_ID").ToString
                    Me.PRQStatus.Text = .Item("PRQStatus").ToString
                    Me.WorkOrder_ID.Text = .Item("WorkOrder_ID").ToString
                    Me.WorkOrerStatus.Text = .Item("WorkOrerStatus").ToString
                    Me.SaleOrder.Text = .Item("SaleOrder").ToString
                    Me.PurchaseOrder.Text = .Item("PurchaseOrder").ToString
                    Me.Document_Type.Text = .Item("Document_Type").ToString
                    Me.PRQ_REF_ID.Text = .Item("PRQ_REF_ID").ToString
                    Me.WOI_ID.Text = .Item("WOI_ID").ToString
                    Me.Production_Line.Text = .Item("Production_Line").ToString
                End With
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class