Imports WMS_STD_Master.W_Language

Public Class frmTransportManifest_AdminConfirm
    Private _IsPacking As Boolean = False
    Private _TransportManifest_Index As String = ""
    Private _TransportManifest_No As String = ""
    Public Property TransportManifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifest_Index = Value
        End Set
    End Property

    Private Sub frmTransportManifest_AdminConfirm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.btnHandOver.Enabled = False
            Me.btnLoading.Enabled = False
            'Me.btnPacking.Enabled = False

            Dim oTm As New ml_TSS
            Dim dtTm As New DataTable
            oTm.getVIEW_TSS_Mobile_TransportmanifestItem(" AND TransportManifest_Index = '" & Me._TransportManifest_Index & "'")
            dtTm = oTm.GetDataTable
            If dtTm.Rows.Count > 0 Then
                Me._TransportManifest_No = dtTm.Rows(0).Item("TransportManifest_No")
                Me._IsPacking = dtTm.Rows(0).Item("IsPacking")

                If dtTm.Rows(0).Item("Status_Manifest") = 8 Then
                    Me.btnHandOver.Enabled = True
                End If
                If dtTm.Rows(0).Item("Status_Load") = 16 Then
                    Me.btnLoading.Enabled = True
                End If
                'If dtTm.Rows(0).Item("Status_Pack") = 9 Then
                '    Me.btnPacking.Enabled = True
                'End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnHandOver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHandOver.Click
        Try
            Dim otss As New ml_TSS
            Dim DtHandOver As New DataTable
            otss.GetDetail_HandOver_Group(Me._TransportManifest_No)
            DtHandOver = otss.GetDataTable

            Dim Dr() As DataRow = DtHandOver.Select("HandOver_Total_Qty = '0'")
            For Each drupdate As DataRow In Dr
                otss.UpdateDetail_HandOver(drupdate("Total_Qty"), drupdate("Sku_Index"), _TransportManifest_Index, drupdate("Barcode1").ToString)
            Next

            W_MSG_Information_ByIndex(1)
            Me.frmTransportManifest_AdminConfirm_Load(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoading.Click
        Try
            Dim dtDataLoading As New DataTable
            Dim otss As New ml_TSS
            otss.LoaddingPack(Me._TransportManifest_No, " Order By StatusLoad")
            dtDataLoading = otss.GetDataTable

            Dim dr() As DataRow
            dr = Nothing

            For Each drLoading As DataRow In dtDataLoading.Rows
                If Me._IsPacking Then
                    dr = dtDataLoading.Select("BarcodePacking = '" & drLoading("BarcodePacking").ToString & "' and Invoice_No ='" & drLoading("Invoice_No").ToString & "'")
                Else
                    Dim oDTSku_Dupp As New DataTable
                    otss.GetCheckingBarcodeDupp(drLoading("BarcodePacking").ToString)
                    oDTSku_Dupp = otss.GetDataTable

                    Select Case oDTSku_Dupp.Rows.Count
                        Case Is <= 0
                            W_MSG_Error("ไม่พบ Barcode : " & drLoading("BarcodePacking").ToString & Chr(13) & "ในเอกสาร")
                        Case Else

                            'Dim Dr() As DataRow = DtHandOver.Select(" Barcode = '" & Me.txtSkuID.Text.Trim & "' AND HandOver_Total_Qty = '0' ")

                            For Each drGet As DataRow In oDTSku_Dupp.Rows
                                dr = dtDataLoading.Select(" Sku_Index = '" & drGet("Sku_Index") & "' AND Loading = '0'  and Invoice_No ='" & drLoading("Invoice_No").ToString & "'")
                                If dr.Length > 0 Then
                                    Exit For
                                End If
                            Next
                    End Select
                End If


                If dr Is Nothing Then Continue For
                If dr.Length > 0 Then
                    'update จำนวน
                    For i As Integer = 0 To dr.Length - 1
                        otss.UpdateLoading(dr(i).Item("SalesOrderPacking_Index"), dr(i).Item("Qty_Pack"))
                    Next
                    'Me.txtBarcode.Text = ""
                    'Me.txtBarcode.Focus()
                Else
                    '_MsgAlert.ShowMessage("ไม่พบ" & Label3.Text & " : " & Me.txtBarcode.Text & Chr(13) & "ใน" & Label1.Text & " : " & Me.txtInvoice.Text)

                    'Me.txtBarcode.Text = ""
                    'Me.txtBarcode.Focus()
                    'Return False
                End If

                Dim dt As New DataTable
                otss.LoaddingPackDisplay(Me._TransportManifest_No, " AND Invoice_No='" & drLoading("Invoice_No").ToString & "'")
                dt = otss.GetDataTable
                Dim cntCom As Integer = 0
                If dt.Rows.Count > 0 Then
                    cntCom = dt.Compute("COUNT(BarcodePacking)", "StatusLoad <> ''")
                    If cntCom = dt.Rows.Count Then
                        otss.UpdateStatus(dt.Rows(0).Item("TransportManifest_Index"), dt.Rows(0).Item("SalesOrder_Index"), "")
                    End If
                End If


            Next

       
            W_MSG_Information_ByIndex(1)
            Me.frmTransportManifest_AdminConfirm_Load(sender, e)


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
End Class