Imports WMS_STD_Master.W_Language


Public Class frmPO_Popup

    Private _IsProcess As Boolean = False
    Public ReadOnly Property IsProcess() As Boolean
        Get
            Return _IsProcess
        End Get
    End Property

    Private _PurchaseOrder_Index As String = ""
    Public Property PurchaseOrder_Index() As String
        Get
            Return _PurchaseOrder_Index
        End Get
        Set(ByVal value As String)
            _PurchaseOrder_Index = value
        End Set
    End Property

    Private _PurchaseOrderItem_Index As String = ""
    Public Property PurchaseOrderItem_Index() As String
        Get
            Return _PurchaseOrderItem_Index
        End Get
        Set(ByVal value As String)
            _PurchaseOrderItem_Index = value
        End Set
    End Property

    Private _Sku_Index As String = ""
    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property

    Private _Package_Index As String = ""
    Public Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal value As String)
            _Package_Index = value
        End Set
    End Property

    Private _Customer_Index As String = ""
    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Private _PurchaseOrder_No As String = ""
    Public Property PurchaseOrder_No() As String
        Get
            Return _PurchaseOrder_No
        End Get
        Set(ByVal value As String)
            _PurchaseOrder_No = value
        End Set
    End Property

    Private _Sku_Id As String = ""
    Public Property Sku_Id() As String
        Get
            Return _Sku_Id
        End Get
        Set(ByVal value As String)
            _Sku_Id = value
        End Set
    End Property

    Private _Sku_Name As String = ""
    Public Property Sku_Name() As String
        Get
            Return _Sku_Name
        End Get
        Set(ByVal value As String)
            _Sku_Name = value
        End Set
    End Property

    Private _Qty As Decimal = 0
    Public Property Qty() As Decimal
        Get
            Return _Qty
        End Get
        Set(ByVal value As Decimal)
            _Qty = value
        End Set
    End Property

    Private _Total_Received_Qty As Decimal = 0
    Public Property Total_Received_Qty() As Decimal
        Get
            Return _Total_Received_Qty
        End Get
        Set(ByVal value As Decimal)
            _Total_Received_Qty = value
        End Set
    End Property

    Private _Total_Padding_Qty As Decimal = 0
    Public Property Total_Padding_Qty() As Decimal
        Get
            Return _Total_Padding_Qty
        End Get
        Set(ByVal value As Decimal)
            _Total_Padding_Qty = value
        End Set
    End Property

    Private Sub frmVehicle_Popup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dgvPopupList.AutoGenerateColumns = False
            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")
            If cboCondition.Items.Count > 0 Then
                cboCondition.SelectedIndex = 0
            End If
            getDgvPopupList()
            Me.ActiveControl = txtCondition
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub dgvPopupList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPopupList.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            SelectPopup(e.RowIndex)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCondition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCondition.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then getDgvPopupList()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getDgvPopupList()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If dgvPopupList.Rows.Count = 0 Then Exit Sub
            If dgvPopupList.CurrentRow.Index < 0 Then Exit Sub
            SelectPopup(dgvPopupList.CurrentRow.Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.PurchaseOrder_Index = ""
            Me.PurchaseOrderItem_Index = ""
            Me.Sku_Index = ""
            Me.Package_Index = ""
            Me.Customer_Index = ""
            Me.PurchaseOrder_No = ""
            Me.Sku_Id = ""
            Me.Sku_Name = ""
            Me.Qty = 0
            Me.Total_Received_Qty = 0
            Me.Total_Padding_Qty = 0
            Me._IsProcess = True
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SelectPopup(ByVal RowIndex As String)
        Try
            Me.PurchaseOrder_Index = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_PurchaseOrder_Index").Value
            Me.PurchaseOrderItem_Index = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_PurchaseOrderItem_Index").Value
            Me.Sku_Index = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Sku_Index").Value
            Me.Package_Index = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Package_Index").Value
            Me.Customer_Index = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Customer_Index").Value
            Me.PurchaseOrder_No = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_PurchaseOrder_No").Value
            Me.Sku_Id = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Sku_Id").Value
            Me.Sku_Name = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Sku_Name").Value
            Me.Qty = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Qty").Value
            Me.Total_Received_Qty = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Total_Received_Qty").Value
            Me.Total_Padding_Qty = dgvPopupList.Rows(dgvPopupList.CurrentRow.Index).Cells("col_Total_Padding_Qty").Value
            _IsProcess = True
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getDgvPopupList()
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""

            strWhere &= String.Format(" where Customer_Index='{0}' ", Me.Customer_Index)
            If Me.txtCondition.Text.Trim <> "" Then
                Select Case cboCondition.SelectedIndex
                    Case 0
                        strWhere &= String.Format(" and PurchaseOrder_No like '%{0}%' ", Me.txtCondition.Text.Trim.Replace("'", ""))
                    Case 1
                        strWhere &= String.Format(" and Sku_Id like '%{0}%' ", Me.txtCondition.Text.Trim.Replace("'", ""))
                End Select
            End If

            If (Me.chkExpected_Delivery_Date.Checked) Then
                strWhere &= String.Format(" and cast(Expected_Delivery_Date as Date)>='{0}' and cast(Expected_Delivery_Date as Date)<='{1}' ", Me.dtpExpected_Delivery_Date_S.Value.ToString("yyyy-MM-dd"), Me.dtpExpected_Delivery_Date_E.Value.ToString("yyyy-MM-dd"))
            End If

            If (Me.chkPurchaseOrder_Date.Checked) Then
                strWhere &= String.Format(" and cast(PurchaseOrder_Date as Date)>='{0}' and cast(PurchaseOrder_Date as Date)<='{1}' ", Me.dtpPurchaseOrder_Date_S.Value.ToString("yyyy-MM-dd"), Me.dtpPurchaseOrder_Date_E.Value.ToString("yyyy-MM-dd"))
            End If


            If Me.chkShowAll.Checked Then
                strWhere &= String.Format(" and (Total_Padding_Qty >= 0)")
            Else
                strWhere &= String.Format(" and (Total_Padding_Qty > 0)")
            End If
            Dim clsObj As New clsPO_Popup()
            dgvPopupList.DataSource = clsObj.getPO_PopupList(strWhere)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class