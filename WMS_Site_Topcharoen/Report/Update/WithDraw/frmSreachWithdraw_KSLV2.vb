Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmSreachWithdraw_KSLV2


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""
            If chkCustomer.Checked Then
                strWhere &= String.Format(" And Customer_index = '{0}' ", txtCustomerId.Tag)
            End If
            If chkWarehouse.Checked Then
                strWhere &= String.Format(" And Warehouse_index = '{0}' ", txtWareHouserId.Tag)
            End If
            If chkdate.Checked Then
                strWhere &= String.Format("  And (Withdraw_Date >= '{0}' And Withdraw_Date <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd"), dtEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            If chkDocType.Checked Then
                strWhere &= String.Format(" And DocumentType_index = '{0}' ", cboDocType.SelectedValue)
            End If
            If chkProductType.Checked Then
                strWhere &= String.Format(" And ProductType_Index = '{0}' ", txtProductType.Tag)
            End If
            If chkSku.Checked Then
                strWhere &= String.Format(" And Sku_index = '{0}' ", txtSku_id.Tag)
            End If

            If chkRemark.Checked Then
                strWhere &= String.Format(" And Remark = '{0}' ", txtRemark.Text)
            End If
            Dim Report_Name = "RPT_KSL_WithDraw"
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

    Private Sub chkDocType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDocType.CheckedChanged
        Try
            cboDocType.Enabled = chkDocType.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub getDocType()
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(2)
            objDT = objClassDB.DataTable

            Dim cbItem(1) As String
            cbItem(0) = "0"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            With cboDocType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            cboDocType.SelectedIndex = cboDocType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub frmSreachSO_KSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getDocType()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkStocke_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProductType.CheckedChanged
        Try
            btnProductType_Popup.Enabled = chkProductType.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku.CheckedChanged
        Try
            btnSku.Enabled = chkSku.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanCel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
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

    Private Sub btnProductType_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductType_Popup.Click
        Try
            Dim _popup As New frmProductType_Popup()
            _popup.ShowDialog()
            If (_popup.DialogResult = Windows.Forms.DialogResult.OK) Then
                Me.txtProductType.Tag = _popup.ProductType_Index
                Me.txtProductType.Text = _popup.ProductType_Desc
            ElseIf (_popup.DialogResult = Windows.Forms.DialogResult.Cancel) Then
                Me.txtProductType.Tag = ""
                Me.txtProductType.Text = ""
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class