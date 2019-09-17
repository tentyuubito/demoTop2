Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Public Class frmCustomer_ShippingV2
    Private ClMsg As New Message
    Private _Customer_Shipping_Index As String

    Public Property Customer_Shipping_Index() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Index = Value
        End Set
    End Property

    'Dim _Customer_Index As String

    'Public Property Customer_Index() As String
    '    Get
    '        Return _Customer_Index
    '    End Get
    '    Set(ByVal Value As String)
    '        _Customer_Index = Value
    '    End Set
    'End Property

    'Dim _Customer_ID As String = ""
    'Property Customer_ID() As String
    '    Get
    '        Return _Customer_ID
    '    End Get
    '    Set(ByVal value As String)
    '        _Customer_ID = value
    '    End Set
    'End Property

    'Dim _Customer_Name As String = ""
    'Property Customer_Name() As String
    '    Get
    '        Return _Customer_Name
    '    End Get
    '    Set(ByVal value As String)
    '        _Customer_Name = value
    '    End Set
    'End Property
    ''' <summary>
    ''' Date 17/02/2010
    ''' By TaTa
    '''     ทะเบียน ลูกค้าบริษัท/ผู้รับสินค้าปลายทาง
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2003)
            oFunction.SW_Language_Column(Me, Me.grdCustomer_Shipping, 2003)

            cboSearchType.SelectedIndex = 0

            ' Me.Icon = frmMain.Icon
            ShowCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


#Region "Function"
    Sub ShowGrid()
        Dim objClassDB As New ms_Customer_Shipping(ms_Size.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            grdCustomer_Shipping.Rows.Clear()
            objClassDB.ShowDataForMain("", "")
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    grdCustomer_Shipping.Rows.Add()
                    grdCustomer_Shipping.Rows(i).Cells("ColCustomerID").Value = objDr("Str1").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColCustomerID").Tag = objDr("Customer_Shipping_Index").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColName_Thai").Value = objDr("Title").ToString + " " + objDr("Company_Name").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColName_Eng").Value = objDr("Title").ToString + " " + objDr("str6").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColAddress").Value = objDr("Address").ToString

                    grdCustomer_Shipping.Rows(i).Cells("ColTel").Value = objDr("Tel").ToString
                    If Not objDr("str4").ToString <> " " Then
                        grdCustomer_Shipping.Rows(i).Cells("ColCity").Value = objDr("str4").ToString
                        grdCustomer_Shipping.Rows(i).Cells("ColThaidefinition").Value = objDr("str5").ToString
                    Else
                        grdCustomer_Shipping.Rows(i).Cells("ColThaidefinition").Value = objDr("District").ToString
                        grdCustomer_Shipping.Rows(i).Cells("ColCity").Value = objDr("Province").ToString
                    End If
                    grdCustomer_Shipping.Rows(i).Cells("ColMobileTel").Value = objDr("Mobile").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColIndex").Value = objDr("Customer_Shipping_Index").ToString
                    i = i + 1
                Next

            Else
                grdCustomer_Shipping.Rows.Clear()
                grdCustomer_Shipping.Refresh()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub

#End Region

#Region "Event Control"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            Dim frm As New frmCustomer_Shipping_Des

            'SetDEFAULT_CUSTOMER_INDEX()
            'frm.Customer_Index = _Customer_Index
            'frm.CustomerIndex = _Customer_Index

            'frm.CustomerID = _Customer_ID
            'frm.CustomerName = _Customer_Name

            frm.SaveType = 0
            frm.ShowDialog()
            ShowCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Sub SetDEFAULT_CUSTOMER_INDEX()
    '    Dim objCustomSetting As New config_CustomSetting
    '    Dim objDT As DataTable = New DataTable

    '    Try
    '        objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
    '        objDT = objCustomSetting.DataTable
    '        If objDT.Rows.Count > 0 Then
    '            _Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
    '            '_Customer_ID = objDT.Rows(0).Item("Customer_Id").ToString
    '            '_Customer_Name = objDT.Rows(0).Item("Customer_Name").ToString
    '        End If

    '        '###################################
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objCustomSetting = Nothing
    '    End Try

    'End Sub
    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Try
            EditCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdCustomer_Shipping.Rows.Count <= 0 Then
                Exit Sub
            End If
            Me._Customer_Shipping_Index = grdCustomer_Shipping.Rows(grdCustomer_Shipping.CurrentRow.Index).Cells("ColIndex").Value

            If W_MSG_Confirm_ByIndex(100001) = Windows.Forms.DialogResult.Yes = True Then
                Dim objDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.DELETE)
                objDB.Delete_Master(Me._Customer_Shipping_Index)

                Dim xCon As New DBType_SQLServer
                xCon.DBExeNonQuery(String.Format("UPDATE ms_Customer_Shipping set Str1 = Str1 + '-X'  where Customer_Shipping_Index = '{0}'", Me._Customer_Shipping_Index))


                ShowGrid()
            End If
            grdCustomer_Shipping.ClearSelection()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


    Private Sub grdCustomer_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomer_Shipping.CellDoubleClick
        If grdCustomer_Shipping.CurrentRow Is Nothing Then Exit Sub
        EditCustomer_Shipping()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
#End Region

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.ShowCustomer_Shipping()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub ShowCustomer_Shipping()

        Dim strWhere As String = ""
        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchType.SelectedIndex
                Case 0
                    ' Customer Name (Thai) : LIKE Search 'A%'
                    strWhere &= " AND (Company_Name LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                Case 1
                    ' Customer Name (English): LIKE Search 'A%'
                    strWhere &= " AND (Str6 LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                Case 2
                    ' Phone: LIKE Search 'A%'
                    strWhere &= " AND (Str1 LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                Case 3
                    ' Phone: LIKE Search 'A%'
                    strWhere &= " AND (Tel LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                Case 4
                    ' Mobile: LIKE Search 'A%'
                    strWhere &= " AND (Mobile LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
            End Select
        End If
        If Me.chkINT_U.Checked Then
            strWhere &= " AND (INT_U = 1 )"
        End If
        Dim objClassDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            grdCustomer_Shipping.Rows.Clear()
            'ใน Class Top 500
            objClassDB.ShowDataForMain("", strWhere)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    grdCustomer_Shipping.Rows.Add()
                    grdCustomer_Shipping.Rows(i).Cells("ColCustomerID").Value = objDr("Str1").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColCustomerID").Tag = objDr("Customer_Shipping_Index").ToString
                    grdCustomer_Shipping.Rows(i).Cells("colTitle").Value = objDr("Title").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColName_Thai").Value = objDr("Company_Name").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColName_Eng").Value = objDr("str6").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColAddress").Value = objDr("Address").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColCountry").Value = objDr("Country_Name").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColTel").Value = objDr("Tel").ToString
                    grdCustomer_Shipping.Rows(i).Cells("Col_Route").Value = objDr("Route").ToString
                    grdCustomer_Shipping.Rows(i).Cells("Col_SubRoute").Value = objDr("SubRoute").ToString
                    If Not objDr("str4").ToString <> " " Then
                        grdCustomer_Shipping.Rows(i).Cells("ColCity").Value = objDr("str4").ToString
                        grdCustomer_Shipping.Rows(i).Cells("ColThaidefinition").Value = objDr("str5").ToString
                    Else
                        grdCustomer_Shipping.Rows(i).Cells("ColThaidefinition").Value = objDr("District").ToString
                        grdCustomer_Shipping.Rows(i).Cells("ColCity").Value = objDr("Province").ToString
                    End If


                    grdCustomer_Shipping.Rows(i).Cells("ColMobileTel").Value = objDr("Mobile").ToString
                    grdCustomer_Shipping.Rows(i).Cells("ColIndex").Value = objDr("Customer_Shipping_Index").ToString
                    grdCustomer_Shipping.Rows(i).Cells("col_TransportRegion").Value = objDr("TransportRegion").ToString
                    grdCustomer_Shipping.Rows(i).Cells("col_UserSalesTool").Value = objDr("SalesTool_User").ToString
                    i = i + 1
                Next

            Else
                grdCustomer_Shipping.Rows.Clear()
                grdCustomer_Shipping.Refresh()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try

    End Sub

    Private Sub EditCustomer_Shipping()
        Try
            If grdCustomer_Shipping.Rows.Count <= 0 Then
                Exit Sub
            End If
            Dim frm As New frmCustomer_Shipping_Des
            frm.SaveType = 1
            frm._Customer_Shipping_Index = grdCustomer_Shipping.Rows(grdCustomer_Shipping.CurrentRow.Index).Cells("ColIndex").Value
            frm.ShowDialog()
            ShowCustomer_Shipping()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnCusRefId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCusRefId.Click
        Try
            If grdCustomer_Shipping.Rows.Count <= 0 Then
                Exit Sub
            End If
            Dim frm As New frmCustomer_Shipping_CusRefId
            frm.Customer_Shipping_Index = grdCustomer_Shipping.CurrentRow.Cells("ColIndex").Value
            frm.Customer_Shipping_Id = grdCustomer_Shipping.CurrentRow.Cells("ColCustomerID").Value
            frm.Customer_Shipping_Name = grdCustomer_Shipping.CurrentRow.Cells("ColName_Thai").Value
            frm.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Enter Then
                Me.ShowCustomer_Shipping()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomer_Shipping_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigCustomer_Shipping
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2003)
                    oFunction.SW_Language_Column(Me, Me.grdCustomer_Shipping, 2003)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim frm As New frmConfigPrintReportBy_Customer_Shipping
            frm.Customer_Shipping_Index = grdCustomer_Shipping.CurrentRow.Cells("ColIndex").Value.ToString
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class