Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Public Class frmMainCustomer_Shipping_Location_V3
    Dim ClMsg As New Message
    Dim _Customer_Index As String = ""

    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property

    Dim _Customer_ID As String = ""
    Property Customer_ID() As String
        Get
            Return _Customer_ID
        End Get
        Set(ByVal value As String)
            _Customer_ID = value
        End Set
    End Property

    Dim _Customer_Name As String = ""
    Property Customer_Name() As String
        Get
            Return _Customer_Name
        End Get
        Set(ByVal value As String)
            _Customer_Name = value
        End Set
    End Property
    ''' <summary>
    ''' Date 17/02/2010
    ''' By TaTa
    '''     ทะเบียน ลูกค้าบริษัท/ผู้รับสินค้าปลายทาง
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2004)
            oFunction.SW_Language_Column(Me, Me.grdCustomer, 2004)

            ShowGrid()

            Me.cboSearchType.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim objfrm As New frmCustomer_Shipping_Location_V3

            objfrm._Customer_Shipping_Index = "" ' _Customer_Shipping_Index
            'objfrm.Customer_Index = _Customer_Index
            'objfrm.CustomerID = "" '_CustomerID
            'objfrm.CustomerName = "" ' _CustomerName

            objfrm.SaveType = 0
            objfrm.ShowDialog()
            ShowGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub ShowGrid()
        Dim objClassDB As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            grdCustomer.Rows.Clear()
            Dim strWhere As String = ""
            If Me.txtSearchKey.Text.Trim <> "" Then
                Select Case cboSearchType.SelectedIndex
                    Case 0
                        ' Customer Name (Thai) : LIKE Search 'A%'
                        strWhere &= " AND (ms_Customer_Shipping_Location.Customer_Shipping_Location_Id LIKE '" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                    Case 1
                        ' Customer Name (English): LIKE Search 'A%'
                        strWhere &= " AND (ms_Customer_Shipping_Location.Shipping_Location_Name LIKE '%" & Me.txtSearchKey.Text.Trim.Replace("'", "''").ToString & "%') "
                End Select
            End If

            If Me.chkINT_U.Checked Then
                strWhere &= " AND ( isnull(ms_Customer_Shipping_Location.INT_U,0) = 1 )"
            End If

            objClassDB.SelectDataEditToCustomerShipping_Location("", strWhere)

            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    grdCustomer.Rows.Add()

                    grdCustomer.Rows(i).Cells("ColumnCustomerID").Value = objDr("Customer_Shipping_Location_Id").ToString
                    grdCustomer.Rows(i).Cells("ColumnCustomerID").Tag = objDr("Customer_Shipping_Location_Index").ToString
                    grdCustomer.Rows(i).Cells("ColumnName").Value = objDr("Shipping_Location_Name").ToString
                    grdCustomer.Rows(i).Cells("ColumnAddress").Value = objDr("Address").ToString
                    grdCustomer.Rows(i).Cells("ColumnTel").Value = objDr("Tel").ToString
                    grdCustomer.Rows(i).Cells("ColumnCountryName").Value = objDr("Country_Name").ToString

                    If objDr("Country_Code").ToString = "TH" Then
                        grdCustomer.Rows(i).Cells("ColumnThaidefinition").Value = objDr("District").ToString
                        grdCustomer.Rows(i).Cells("ColumnCity").Value = objDr("Province").ToString
                    Else
                        grdCustomer.Rows(i).Cells("ColumnThaidefinition").Value = objDr("str4").ToString
                        grdCustomer.Rows(i).Cells("ColumnCity").Value = objDr("str5").ToString
                    End If
                    grdCustomer.Rows(i).Cells("ColumnMobileTel").Value = objDr("Mobile").ToString
                    grdCustomer.Rows(i).Cells("ColumnIndex").Value = objDr("Customer_Shipping_Index").ToString

                    grdCustomer.Rows(i).Cells("col_TransportRegion").Value = objDr("TransportRegion").ToString
                    grdCustomer.Rows(i).Cells("col_Route").Value = objDr("Route").ToString
                    grdCustomer.Rows(i).Cells("col_SubRoute").Value = objDr("SubRoute").ToString
                    grdCustomer.Rows(i).Cells("col_DistributionCenter").Value = objDr("DistributionCenter").ToString
                    i = i + 1
                Next

            Else
                grdCustomer.Rows.Clear()
                grdCustomer.Refresh()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try

        '  Cursor.Current = Cursors.Default
    End Sub

    Sub SetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                _Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                _Customer_ID = objDT.Rows(0).Item("Customer_Id").ToString
                _Customer_Name = objDT.Rows(0).Item("Customer_Name").ToString
            End If

            '###################################
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub
    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Try
            EditCustomer()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub EditCustomer()
        Try
            If grdCustomer.RowCount <= 0 Then
                Exit Sub
            End If
            Dim objfrm As New frmCustomer_Shipping_Location_V3
            objfrm.SaveType = 1
            '   Dim Customer_Shipping_Index As String = _Customer_Shipping_Index
            objfrm.Customer_Shipping_Index = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnIndex").Value.ToString()
            objfrm.Customer_Shipping_Location_Index = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnCustomerID").Tag.ToString()
            ' objfrm._Customer_Shipping_Index = Customer_Shipping_Index
            ' objfrm.Customer_Shipping_Location_Index = Customer_Shipping_Index
            'objfrm.CustomerID = "" ' Me.txtCustomer_Id.Text
            'objfrm.CustomerName = "" 'Me.txtCustomer_Name.Text

            objfrm.ShowDialog()

            ShowGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdCustomer.RowCount <= 0 Then
                Exit Sub
            End If

            Dim objfrm As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.DELETE)
            Dim Index As String = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnCustomerID").Tag.ToString()

            If W_MSG_Confirm_ByIndex(100002) = Windows.Forms.DialogResult.Yes = True Then
                objfrm.Delete_Master(Index)

                Dim xCon As New DBType_SQLServer
                xCon.DBExeNonQuery(String.Format("UPDATE ms_Customer_Shipping_Location set Customer_Shipping_Location_Id = Customer_Shipping_Location_Id + '-X'  where Customer_Shipping_Location_Index = '{0}'", Index))

                ShowGrid()
                GC.Collect()
            End If
            'Clear All Obj is not use
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ShowGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdCustomer_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomer.CellDoubleClick
        Try
            BtnUpdate_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainCustomer_Shipping_Location_V3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigCustomer_Shipping_Location_V3
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2004)
                    oFunction.SW_Language_Column(Me, Me.grdCustomer, 2004)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class