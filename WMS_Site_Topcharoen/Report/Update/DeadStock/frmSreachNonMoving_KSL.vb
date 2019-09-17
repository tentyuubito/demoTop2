Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmSreachNonMoving_KSL


    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""
            If chkCustomer.Checked Then
                strWhere &= String.Format(" And Customer_index = '{0}' ", txtCustomerId.Tag)
            End If
            If chkWarehouse.Checked Then
                strWhere &= String.Format(" And Warehouse_index = '{0}' ", txtWareHouserId.Tag)
            End If
            If chkProductType.Checked Then
                strWhere &= String.Format(" And ProductType_Index = '{0}' ", txtProductType_id.Tag)
            End If
            If chkStatus.Checked Then
                strWhere &= String.Format(" And ItemStatus_Index = '{0}' ", cboStatus.SelectedValue)
            End If
            If chkSku.Checked Then
                strWhere &= String.Format(" And Sku_index = '{0}' ", txtSku_id.Tag)
            End If
            If chkdate.Checked Then
                strWhere &= String.Format("  And (Lot_No  >= '{0}' And Lot_No  <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            If chkMaxRS.Checked Then
                strWhere &= String.Format("  And ( Max_Order_Date BETWEEN '{0}' and  '{0}') ", dtRS.Value.ToString("yyyy-MM-dd"))
            End If
            If chkMaxWH.Checked Then
                strWhere &= String.Format(" And (  Max_Withdraw_Date  BETWEEN '{0}' and '{0}') ", dtWH.Value.ToString("yyyy-MM-dd"))
            End If
            If chkNonMoving.Checked Then
                strWhere &= String.Format(" And Numday_NonMove  BETWEEN '{0}' and '{1}' ", txtStart.Text, txtEND.Text)
            End If
            If chkRemark.Checked Then
                strWhere &= String.Format(" And Remark = '{0}' ", txtRemark.Text)
            End If
            Dim Report_Name = "RPT_KSL_Moving"
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, strWhere)
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oReport.LoadReport()
            cry.SetParameterValue("Bdate", dtStartDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Edate", dtEndDate.Value.ToString("dd-M-yy"))
            cry.SetParameterValue("Warehouse", txtWareHouserId.Text)

            frm.CrystalReportViewer1.ReportSource = cry

            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                txtCustomerId.Tag = tmpCustomer_Index
                txtCustomerId.Text = frm.strCustomer_Name_Id
                txtCustomerDes.Text = frm.customerName
            Else
                txtCustomerId.Tag = ""
                Me.txtCustomerId.Text = ""
                Me.txtCustomerDes.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnWareHouser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWareHouser.Click
        Try
            Dim frm As New frmWareHouse_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpWareHouse_index As String = ""
            tmpWareHouse_index = frm.WareHouse_index

            If tmpWareHouse_index = "" Then
                Exit Sub
            End If

            If Not tmpWareHouse_index = "" Then
                txtWareHouserId.Tag = tmpWareHouse_index
                txtWareHouserId.Text = frm.WareHouseName
                txtWareHouserDes.Text = frm.WareHouseDes
            Else
                Me.txtWareHouserId.Tag = ""
                Me.txtWareHouserId.Text = ""
                Me.txtWareHouserDes.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub chkRemark_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRemark.CheckedChanged
        Try
            txtRemark.Enabled = chkRemark.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        Try
            btnCustomer.Enabled = chkCustomer.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkWarehouse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWarehouse.CheckedChanged
        Try
            btnWareHouser.Enabled = chkWarehouse.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtStartDate.Enabled = chkdate.Checked
            dtEndDate.Enabled = chkdate.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getStatus()
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        objDT = objClassDB.DBExeQuery("select * from ms_ItemStatus where status_id <> -1 order by seq")
        Try
            With cboStatus
                .DisplayMember = "Description"
                .ValueMember = "ItemStatus_Index"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub frmSreachSO_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getStatus()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProduct.Click
        Try
            Dim frm As New frmProductType_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpProductType_Index As String = ""
            tmpProductType_Index = frm.ProductType_Index

            If tmpProductType_Index = "" Then
                Exit Sub
            End If

            If Not tmpProductType_Index = "" Then
                txtProductType_id.Tag = tmpProductType_Index
                txtProductType_id.Text = frm.ProductType_Id
                txtProductTypeDes.Text = frm.ProductType_Desc
            Else
                Me.txtProductType_id.Tag = ""
                Me.txtProductType_id.Text = ""
                Me.txtProductTypeDes.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkProductType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProductType.CheckedChanged
        Try
            btnProduct.Enabled = chkProductType.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        Try
            cboStatus.Enabled = chkStatus.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkMaxRS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaxRS.CheckedChanged
        Try
            dtRS.Enabled = chkMaxRS.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkMaxWH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaxWH.CheckedChanged
        Try
            dtWH.Enabled = chkMaxWH.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkNonMoving_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNonMoving.CheckedChanged
        Try
            txtStart.Enabled = chkNonMoving.Checked
            txtEND.Enabled = chkNonMoving.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtNonMonving_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStart.KeyPress, txtEND.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False

            Case Else
                e.Handled = True
        End Select
    End Sub


    Private Sub chkSku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku.CheckedChanged
        Try
            btnSku.Enabled = chkSku.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStatus.SelectedIndexChanged

    End Sub
    Private Sub btnSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.SEARCH, "")
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpSku_Index As String = ""
            tmpSku_Index = frm.Sku_Index

            If tmpSku_Index = "" Then
                Exit Sub
            End If

            If Not tmpSku_Index = "" Then
                txtSku_id.Tag = tmpSku_Index
                txtSku_id.Text = frm.Sku_ID
                txtSkuDes.Text = frm.Sku_Des_th
            Else
                Me.txtSku_id.Tag = ""
                Me.txtSku_id.Text = ""
                Me.txtSkuDes.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class