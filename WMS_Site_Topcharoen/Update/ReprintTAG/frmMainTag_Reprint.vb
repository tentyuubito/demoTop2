Imports Microsoft.Office.Interop.Excel
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_master.W_Function
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_INB_Barcode
Imports WMS_STD_INB_Report

Public Class frmMainTag_Reprint

    Private _Printer_Barcode_Name As String = System.Configuration.ConfigurationSettings.AppSettings("Printer_Barcode_Name")
    Private DEFAULT_ViewTagBarcode As String = ""
    'Private btApp As BarTender.ApplicationClass
    'Private btFormat As BarTender.Format

    Private _DocumentPlan_Index As String = ""
    Private fLoad As Boolean = False

#Region "   Property   "

    Enum enmGenTag_Type
        NotGen = 0
        NormalGen = 1
        GenPerQty = 2
    End Enum


    Private _Process_Id As Integer = 1
    Public Property Process_Id() As Integer
        Get
            Return _Process_Id
        End Get
        Set(ByVal value As Integer)
            _Process_Id = value
        End Set
    End Property

    Public Property DocumentPlan_Index() As String
        Get
            Return _DocumentPlan_Index
        End Get
        Set(ByVal value As String)
            _DocumentPlan_Index = value
        End Set
    End Property

    Private _Order_Index As String
    Private _Tag_Index As String

    Public Property Tag_Index() As String
        Get
            Return _Tag_Index
        End Get
        Set(ByVal value As String)
            _Tag_Index = value
        End Set
    End Property

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property
    Private _DEFAULT_AUTO_TAG As enmGenTag_Type
    Public Property DEFAULT_AUTO_TAG() As enmGenTag_Type
        Get
            Return _DEFAULT_AUTO_TAG
        End Get
        Set(ByVal value As enmGenTag_Type)
            _DEFAULT_AUTO_TAG = value
        End Set
    End Property

#End Region

#Region "   Page Load   "
    Private key As New Default_Key
    Private Sub frmTag_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ------ Set Language Begin ------
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, -10)
            oFunction.SW_Language_Column(Me, Me.grdTag, -10)
            oFunction = Nothing


            grdTag.AutoGenerateColumns = False
            'dtpDate.Value = Now.AddDays(-1)
            Me.key.NumericOnly(Me.txtTop)

            getProcessStatus()
            'getOrder()
            SetAUTO_GENTAG()
            Me.getReportName(101)
            Me.lb_to.Visible = False
            Me.rdbOrder_Date.Checked = True
            fLoad = True
            getTAG()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try


    End Sub

#End Region

#Region "   Search DATA   "

    Private Sub rdbOrder_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOrder_Date.CheckedChanged
        Me.dtpDate.Visible = True
        Me.txtKeySearch.Visible = False
        Me.txtKeySearch.Text = ""
        Me.lb_to.Visible = False
        If rdbOrder_Date.Checked = True Then
            Me.lb_to.Visible = True
            Me.dateEnd.Visible = True
        Else
            Me.lb_to.Visible = False
            Me.dateEnd.Visible = False
        End If
        Me.btnPop_Search.Visible = Me.rdbCustomer.Checked
    End Sub

    Private Sub rdbTag_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTag_No.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.lb_to.Visible = False
        Me.btnPop_Search.Visible = Me.rdbCustomer.Checked
    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbProduct.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.lb_to.Visible = False
        Me.btnPop_Search.Visible = Me.rdbCustomer.Checked
    End Sub

    Private Sub rdbSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.lb_to.Visible = False
        Me.btnPop_Search.Visible = Me.rdbCustomer.Checked
    End Sub

    Private Sub rdbDepartment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged, rdb_Sku.CheckedChanged, rdbOrder_No.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.lb_to.Visible = False
        Me.btnPop_Search.Visible = Me.rdbCustomer.Checked
    End Sub



#End Region

#Region "   Event button ,Checked Box  "

    'Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim objFrmPrintTAG As New frmPrintTAG
    '    Try
    '        Dim i As Integer
    '        For i = 0 To grdTag.Rows.Count - 1
    '            If grdTag.Rows(i).Cells("chkSelect").Value = True Then
    '                If grdTag.Rows.Count > 0 Then
    '                    objFrmPrintTAG.Tag_NO = grdTag.CurrentRow.Cells("col_TAG_NO").Value.ToString
    '                    objFrmPrintTAG.SKU_ID = grdTag.CurrentRow.Cells("col_sku_ID").Value.ToString
    '                    objFrmPrintTAG.ShowDialog()
    '                End If
    '                getTAG()
    '            End If
    '        Next

    '    Catch ex As Exception
    '        W_MSG_Error(ex.ToString)
    '    End Try
    'End Sub

    'Private Sub btnNewOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim objFrmTag_add As New frmTag_Add_Old

    '    Try
    '        objFrmTag_add.objStatus = frmTag_Add_Old.enuOperation_Type.ADDNEW
    '        objFrmTag_add.Order_Index = _Order_Index
    '        objFrmTag_add.ShowDialog()
    '        getTAG()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    'Private Sub btnEditOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim objFrmTag_add As New frmTag_Add_Old
    '    Try
    '        If grdTag.Rows.Count <> 0 Then
    '            objFrmTag_add.objStatus = frmTag_Add_Old.enuOperation_Type.UPDATE
    '            objFrmTag_add.Order_Index = _Order_Index
    '            'objFrmTag_add.TagNO = grdTag.Rows(grdTag.CurrentRow.Index).Cells("Col_TAG_NO").Value.ToString
    '            objFrmTag_add.TAG_Index = grdTag.Rows(grdTag.CurrentRow.Index).Cells("Col_TAG_Index").Value.ToString
    '            objFrmTag_add.Size = New Size(526, 455)
    '            objFrmTag_add.ShowDialog()
    '        End If
    '        getTAG()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            If grdTag.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If Me.grdTag.Rows.Count <= 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(5) = System.Windows.Forms.DialogResult.Yes Then
                Dim dtDelTag As New System.Data.DataTable
                dtDelTag = CType(grdTag.DataSource, DataTable)
                dtDelTag.AcceptChanges()
                Dim drDelTagArr() As DataRow = dtDelTag.Select("chkSelect=1 and TAG_Status in(1)", "chkSelect")
                Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.DELETE)
                For Each drDelTag As DataRow In drDelTagArr

                    Me.Tag_Index = drDelTag("TAG_Index").ToString

                    'objTag.Delete(Me.Tag_Index)
                    objTag.DeleteAllTagForOrderItem(Me.Tag_Index)
                Next
            End If
            getTAG()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReportTAG.Click
    '    Try
    '        Dim objfrm As New frmReportTAG
    '        objfrm.ShowDialog()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    'Private Sub btnSaveTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If grdTag.Rows.Count = 0 Then
    '        Exit Sub
    '    End If

    '    Try
    '        Dim frm As New frmPutawayWithTAG
    '        frm.Order_Index = grdTag.Rows(grdTag.CurrentRow.Index).Cells("col_Order_Index").Value.ToString
    '        frm.ShowDialog()

    '        'Refresh Grid Tag
    '        getTAG()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getTAG()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub ChkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAll.CheckedChanged

        Try
            If grdTag.Rows.Count = 0 Then Exit Sub
            For i As Integer = 0 To grdTag.Rows.Count - 1
                If ChkAll.Checked = True Then
                    grdTag.Rows(i).Cells("chkSelect").Value = True
                Else
                    grdTag.Rows(i).Cells("chkSelect").Value = False
                End If
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnAutoGenTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            AutoGenTag()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



#End Region

    '#Region "   Event Drid   "

    '    Private Sub grdTag_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTag.SelectionChanged
    '        Try
    '            Select Case grdTag.CurrentRow.Cells("col_OrderStatus").Value
    '                Case 1, 4
    '                    btnSaveTag.Enabled = True
    '                    btnCancel.Enabled = True
    '                Case Else

    '                    btnSaveTag.Enabled = False
    '                    btnCancel.Enabled = False
    '            End Select

    '            Select Case grdTag.CurrentRow.Cells("col_TAG_Status").Value
    '                Case -1, 2
    '                    btnCancel.Enabled = False
    '                Case Else
    '                    btnCancel.Enabled = True

    '            End Select
    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message)
    '        End Try


    '    End Sub
    '#End Region

#Region "   Get DATA   "

    Private Sub getTAG(Optional ByVal mLocation_Alias As String = "")
        If fLoad = False Then Exit Sub
        Dim objTAG As New ml_TAG 'tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Dim objDT As System.Data.DataTable = New System.Data.DataTable
        Dim strWhere As String = ""
        Try
            If mLocation_Alias = "" Then
                If Len(_Order_Index) > 0 Then
                    strWhere &= " AND Order_Index = '" & _Order_Index & "'"
                End If
                If txtKeySearch.Text.Trim <> "" Then

                    If Me.rdbTag_No.Checked = True Then
                        strWhere &= " AND Tag_No = '" & Me.txtKeySearch.Text & "'"
                    ElseIf Me.rdbCustomer.Checked = True Then
                        If Not txtKeySearch.Text = "" Then
                            strWhere &= " AND Customer_Id LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                        End If
                        'ElseIf Me.rdbSupplier.Checked = True Then
                        '    strWhere &= " AND Supplier_Name Like '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                    ElseIf Me.rdbProduct.Checked = True Then
                        strWhere &= " AND str1 LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                    ElseIf Me.rdb_Sku.Checked = True Then
                        strWhere &= " AND SKU_ID LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                    ElseIf Me.rdbOrder_No.Checked = True Then
                        strWhere &= " AND Order_No LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                    End If
                End If
                If Me.dtpDate.Visible Then 'วันที่รับสินค้า()
                    strWhere &= " AND Order_Date BETWEEN '" & Me.dtpDate.Value.ToString("yyyy/MM/dd 00:00:00 ") & "' AND '" & Me.dateEnd.Value.ToString("yyyy/MM/dd 23:58:00") & "'"
                End If
                Select Case Me.cboDocumentStatus.SelectedValue
                    Case 0
                        strWhere += " "
                    Case Else
                        strWhere += " AND TAG_Status=" & Me.cboDocumentStatus.SelectedValue
                End Select
            Else
                strWhere &= " AND (Location_Alias in (" & mLocation_Alias & "))"
            End If

            If Me._Process_Id = 5 Then
                strWhere = " AND TAG_No in (select case when isnull(TAG_NoNew,'')= '' then TAG_NO"
                strWhere &= " 	                    else TAG_NoNew end"
                strWhere &= " from  tb_TransferStatusLocation   "
                strWhere &= " where TransferStatus_Index = '" & Me._DocumentPlan_Index & "' ) "
            End If

            'strWhere &= " AND Customer_Index IN (SELECT Customer_Index FROM config_UserByCustomer WHERE user_index  = '" & W_Module.WV_User_Index & "' AND IsUse = 1)"

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()
            strWhere &= New clsUserByDC().GetDistributionCenterByUser()
            'strWhere = strWhere.Replace("DistributionCenter_Index", "VIEW_TAG_Header_Reprint_V2.DistributionCenter_Index")


            If Not IsNumeric(Me.txtTop.Text) Then
                Me.txtTop.Text = "500"
            End If

            objTAG.getView_Tag_Header(strWhere, CInt(Me.txtTop.Text))
            objDT = objTAG.DataTable

            Me.grdTag.Refresh()
            grdTag.DataSource = objDT

            For i As Integer = 0 To grdTag.Rows.Count - 1

                With grdTag.Rows(i)
                    If .Cells("Col_Print_Status").ToString = "1" Then
                        .Cells("col_TAG_NO").Style.BackColor = Color.LightGreen
                    End If

                    Dim intStatus As Integer = .Cells("col_Tag_Status").Value
                    Select Case intStatus
                        Case -1
                            .Cells("col_Status").Style.BackColor = Color.Pink
                            .Cells("col_Location_Alias").Style.BackColor = Color.Pink
                        Case 2
                            .Cells("col_Status").Style.BackColor = Color.LightGreen
                            .Cells("col_Location_Alias").Style.BackColor = Color.LightGreen
                        Case 4
                            .Cells("col_Status").Style.BackColor = Color.LightYellow
                            .Cells("col_Location_Alias").Style.BackColor = Color.LightYellow
                        Case Else
                    End Select

                    .Cells("col_weight").Value = CDbl(grdTag.Rows(i).Cells("col_weight").Value).ToString("#0.00")
                    .Cells("col_volume").Value = CDbl(grdTag.Rows(i).Cells("col_volume").Value).ToString("#0.00")
                End With
            Next

            If objDT.Rows.Count > 0 Then
                Label1.Text = "พบจำนวน " & objDT.Rows.Count & " รายการ แสดง " & Me.txtTop.Text.ToString & " รายการ"
            Else
                Label1.Text = "ไม่พบรายการ"
            End If

            'txtTAGCount.Text = objDT.Rows.Count
            'Me.btnEditTAG.Enabled = True

            If grdTag.Rows.Count > 0 Then
                grdTag.Rows(0).Selected = False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objTAG = Nothing
            objDT = Nothing
        End Try
    End Sub

    'Private Sub getOrder()
    '    Dim objOrder As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
    '    Dim odtOrder As System.Data.DataTable = New System.Data.DataTable
    '    Try
    '        objOrder.getOrderHeader(Me._Order_Index)
    '        odtOrder = objOrder.DataTable
    '        txtOrderNo.Text = odtOrder.Rows(0).Item("Order_No").ToString
    '        txtOrderDate.Text = Format(odtOrder.Rows(0).Item("Order_Date"), "dd/MM/yyyy").ToString()
    '        txtCustomer.Text = odtOrder.Rows(0).Item("Customer_Name").ToString
    '        txtSupplier.Text = odtOrder.Rows(0).Item("Supplier_Name").ToString

    '        'Add 13/07/2009
    '        'If PutAway Complete
    '        'If odtOrder.Rows(0)("Status").ToString = 2 Then
    '        '    btnNewTAG.Enabled = False
    '        '    btnEditTAG.Enabled = False
    '        '    btnCancel.Enabled = False
    '        '    btnAutoGenTag.Enabled = False
    '        'End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Private Function GetOrderItemNonTag() As System.Data.DataTable

        Dim objItem As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Try
            objItem.getOrderItemNonTag(Me._Order_Index)
            Dim odtItem As System.Data.DataTable
            odtItem = objItem.GetDataTable
            Return odtItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Dim objDT As System.Data.DataTable = New System.Data.DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Sub SetAUTO_GENTAG()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As System.Data.DataTable = New System.Data.DataTable

        Try
            objCustomSetting.GetConfig_Value("AUTO_GENTAG", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._DEFAULT_AUTO_TAG = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me._DEFAULT_AUTO_TAG = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

#End Region

#Region "   Report AND  Barcode"

    'Private Sub getReportName(ByVal Process_Id As Integer)
    '    'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


    '    Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
    '    Dim objDT As System.Data.DataTable = New System.Data.DataTable

    '    Try
    '        objClassDB.GetListReportName(Process_Id)
    '        objDT = objClassDB.DataTable

    '        ' ***** Using add comboboxcolumn *****
    '        With cbPrint
    '            .DisplayMember = "Description"
    '            .ValueMember = "Report_Name"
    '            .DataSource = objDT
    '        End With

    '        ' *************************************

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objClassDB = Nothing
    '        objDT = Nothing
    '    End Try

    'End Sub

    'Private Sub btn_Print_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        If grdTag.RowCount <= 0 Then Exit Sub
    '        Dim oconfig_Report As New config_Report
    '        Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString

    '        Try
    '            Dim frm As New frmReportJobOrder
    '            frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TAG_Index ='" & grdTag.CurrentRow.Cells("col_TAG_Index").Value & "'")
    '            frm.ShowDialog()
    '            '###################################
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        Finally
    '        End Try

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
#Region "    Set Printer   "

    Private Sub SetDefaultPrinter(ByVal PrinterName As String, _
 ByVal DriverName As String, ByVal PrinterPort As String)
        Dim DeviceLine As String

        'rebuild a valid device line string 
        DeviceLine = PrinterName & "," & DriverName & "," & PrinterPort

        'Store the new printer information in the 
        '[WINDOWS] section of the WIN.INI file for 
        'the DEVICE= item 
        Call WriteProfileString("windows", "Device", DeviceLine)

        'Cause all applications to reload the INI file 
        'Call SendMessage(HWND_BROADCAST, WM_WININICHANGE, 0, "windows")

    End Sub
    Private Declare Function WriteProfileString Lib "kernel32" Alias "WriteProfileStringA" _
        (ByVal lpszSection As String, ByVal lpszKeyName As String, _
        ByVal lpszString As String) As Long
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
         (ByVal hwnd As Long, ByVal wMsg As Long, _
         ByVal wParam As Long, ByVal lparam As String) As Long
    Private Const HWND_BROADCAST As Long = &HFFFF&
    Private Const WM_WININICHANGE As Long = &H1A



#End Region
    Private Function GetItemBarcode_TemplateFile()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As System.Data.DataTable = New System.Data.DataTable
        Dim _FileName As String = ""
        Try

            objCustomSetting.GetConfig_Value("ITEM_BARCODE_TEMPLATE")

            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                _FileName = objDT.Rows(0).Item("Config_Value").ToString

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return _FileName
    End Function
    Private WithEvents docToPrint As New Printing.PrintDocument
    Private Sub document_PrintPage(ByVal sender As Object, _
       ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
           Handles docToPrint.PrintPage
        Dim text As String = "In document_PrintPage method."
        Dim printFont As New System.Drawing.Font _
            ("Arial", 35, System.Drawing.FontStyle.Regular)

        e.Graphics.DrawString(text, printFont, _
            System.Drawing.Brushes.Black, 10, 10)
    End Sub


    'Private Sub setTagBarcode(ByVal PTag_No As String)

    '    Try
    '        Dim objconfig_TagBarcode As New config_TagBarcode
    '        Dim odtconfig_TagBarcode As System.Data.DataTable

    '        Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
    '        Dim odtTag As System.Data.DataTable

    '        Dim Lable_Name As String = ""
    '        Dim Field_Name1 As String = ""



    '        Dim Field_Name2 As String = ""
    '        ' Get  config_TagBarcode
    '        objconfig_TagBarcode.GetAllAsDataTable()
    '        odtconfig_TagBarcode = objconfig_TagBarcode.GetDataTable
    '        If odtconfig_TagBarcode.Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '        ' Get Tag Data
    '        SetDEFAULT_ViewTagBarcode()
    '        objTag.getConfig_View(DEFAULT_ViewTagBarcode, PTag_No)
    '        odtTag = objTag.GetDataTable

    '        If odtTag.Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '        With btFormat

    '            For i As Integer = 0 To odtconfig_TagBarcode.Rows.Count - 1
    '                Lable_Name = odtconfig_TagBarcode.Rows(i).Item("Lable_Name").ToString
    '                Field_Name1 = odtconfig_TagBarcode.Rows(i).Item("Field_Name1").ToString
    '                Field_Name2 = odtconfig_TagBarcode.Rows(i).Item("Field_Name2").ToString

    '                Select Case Field_Name1
    '                    Case "Weight"
    '                        If IsNumeric(odtTag.Rows(0).Item("Field_Name1").ToString) Then
    '                            If CDbl(odtTag.Rows(0).Item("Field_Name1").ToString) > 0 Then
    '                                .SetNamedSubStringValue(Field_Name1, CDbl(odtTag.Rows(0).Item(Field_Name1)).ToString("##,##0.00") & " KGS.")
    '                            Else
    '                                .SetNamedSubStringValue(Lable_Name, " - KGS.")
    '                            End If
    '                        Else
    '                            .SetNamedSubStringValue(Lable_Name, " - KGS.")
    '                        End If
    '                    Case Else
    '                        If Field_Name2 <> "" Then
    '                            .SetNamedSubStringValue(Lable_Name, odtTag.Rows(0).Item(Field_Name1).ToString & " " & odtTag.Rows(0).Item(Field_Name2).ToString)
    '                        Else
    '                            .SetNamedSubStringValue(Lable_Name, odtTag.Rows(0).Item(Field_Name1).ToString)
    '                        End If
    '                End Select

    '            Next
    '            .PrintOut(False, False)
    '        End With
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
    Sub SetDEFAULT_ViewTagBarcode()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As System.Data.DataTable = New System.Data.DataTable

        Try
            objCustomSetting.GetConfig_Value("DEFAULT_VIEW_TAGBARCODE", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_ViewTagBarcode = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_ViewTagBarcode = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    'Private Sub btnPrintTagIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintTagIn.Click
    '    Try
    '        Dim oconfig_Report As New config_Report
    '        Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.UPDATE)
    '        Dim strTag_No As String
    '        Dim strOrder_No As String
    '        Dim strTag_Index As String = ""
    '        Dim odtImage As New System.Data.DataTable
    '        Dim frm As New frmReportJobOrder

    '        odtImage.Columns.Add("pic", GetType(System.Byte()))
    '        odtImage.Columns.Add("picOrder", GetType(System.Byte()))

    '        For intRow As Integer = 0 To grdTag.Rows.Count - 1
    '            If grdTag.Rows(intRow).Cells("chkSelect").Value = True Then
    '                strTag_No = Me.grdTag.Rows(intRow).Cells("Col_Tag_No").Value
    '                strOrder_No = Me.grdTag.Rows(intRow).Cells("Col_Order_No").Value

    '                If strTag_Index <> "" Then
    '                    strTag_Index &= ","
    '                End If
    '                strTag_Index &= "'" & Me.grdTag.Rows(intRow).Cells("col_TAG_Index").Value.ToString & "'"

    '                Dim objBarcode As New Barcode
    '                objBarcode.GenBarcode(strTag_No)
    '                objBarcode.GenBarcode(strOrder_No)

    '                Dim odr As DataRow
    '                odr = odtImage.NewRow

    '                odr("pic") = ConvertFileToByte(Application.StartupPath & "\" & strTag_No & ".bmp")
    '                odr("picOrder") = ConvertFileToByte(Application.StartupPath & "\" & strOrder_No & ".bmp")
    '                odtImage.Rows.Add(odr)
    '                objTag.UpdatePrintStatus(Me.grdTag.Rows(intRow).Cells("col_TAG_Index").Value.ToString)
    '                Me.grdTag.Rows(intRow).Cells("col_TAG_NO").Style.BackColor = Color.LightGreen

    '            End If
    '        Next
    '        If odtImage.Rows.Count = 0 Then
    '            W_MSG_Information("กรุณาเลือกรายการที่จะพิมพ์ TAG")
    '            Exit Sub
    '        End If

    '        frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo("TagPrintOut", "AND TAG_Index in (" & strTag_Index & ")", odtImage)
    '        '   frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo("TagPrintOut", "AND TAG_Index = '" & Me.grdTag.CurrentRow.Cells("col_TAG_Index").Value.ToString & "'")
    '        frm.ShowDialog()


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


#End Region

#Region "    AutoGenTag  "

    Private Sub AutoGenTag()
        Dim objItemCollection As New List(Of tb_TAG)

        Dim objDBTempIndex As New Sy_AutoNumber
        Dim objAutoNumber As New Sy_AutoNumber

        Try
            Dim odtOrderItemNonTag As System.Data.DataTable = GetOrderItemNonTag()

            If odtOrderItemNonTag.Rows.Count = 0 Then
                W_MSG_Information(" ไม่พบรายการ สร้าง TAG")
                Exit Sub
            End If

            For Each odrOrderItem As DataRow In odtOrderItemNonTag.Rows

                Select Case Me._DEFAULT_AUTO_TAG
                    Case enmGenTag_Type.NormalGen
                        Dim otb_TAG As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                        otb_TAG = SetTagItem(odrOrderItem, otb_TAG)

                        otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                        otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")

                        objItemCollection.Add(otb_TAG)

                    Case enmGenTag_Type.GenPerQty

                        For iRowQty As Integer = 1 To odrOrderItem("Qty").ToString
                            'Set Value 
                            Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                            objItemPerQty = SetTagItem(odrOrderItem, objItemPerQty)

                            'Set Value Per Tag
                            With objItemPerQty
                                .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                .Qty_per_TAG = 1
                                .Weight_per_TAG = .Weight / .Qty
                                .Volume_per_TAG = .Volume / .Qty
                                .Ref_No3 = iRowQty & "/" & .Qty
                            End With

                            objItemCollection.Add(objItemPerQty)
                        Next
                End Select
            Next

            Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

            objItemA.objItemCollection = objItemCollection
            objItemA.InsertData()

            objDBTempIndex = Nothing
            objAutoNumber = Nothing
            getTAG()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function SetTagItem(ByVal podrOrderItem As DataRow, ByVal poTagItem As tb_TAG) As tb_TAG

        Try

            With podrOrderItem
                poTagItem.Order_No = .Item("Order_No").ToString
                poTagItem.Order_Index = Me._Order_Index
                poTagItem.Order_Date = .Item("Order_Date").ToString
                poTagItem.Order_Time = .Item("Order_Time").ToString
                poTagItem.Customer_Index = .Item("Customer_Index").ToString

                If .Item("Supplier_Index").ToString IsNot Nothing Then
                    poTagItem.Supplier_Index = .Item("Supplier_Index").ToString
                Else
                    poTagItem.Supplier_Index = ""
                End If

                If .Item("OrderItem_Index").ToString IsNot Nothing Then
                    poTagItem.OrderItem_Index = .Item("OrderItem_Index").ToString
                Else
                    poTagItem.OrderItem_Index = ""
                End If

                poTagItem.OrderItemLocation_Index = ""
                If .Item("Sku_Index").ToString IsNot Nothing Then
                    poTagItem.Sku_Index = .Item("Sku_Index").ToString
                Else
                    poTagItem.Sku_Index = ""
                End If

                If .Item("PLot").ToString IsNot Nothing Then
                    poTagItem.PLot = .Item("PLot").ToString
                Else
                    poTagItem.PLot = ""
                End If

                If .Item("ItemStatus_Index").ToString IsNot Nothing Then
                    poTagItem.ItemStatus_Index = .Item("ItemStatus_Index").ToString
                Else
                    poTagItem.ItemStatus_Index = ""
                End If

                If .Item("Package_Index").ToString IsNot Nothing Then
                    poTagItem.Package_Index = .Item("Package_Index").ToString
                Else
                    poTagItem.Package_Index = ""
                End If

                poTagItem.Unit_Weight = 0
                poTagItem.Size_Index = -1


                If .Item("PalletType_Index").ToString IsNot Nothing Then
                    poTagItem.Pallet_No = .Item("PalletType_Index").ToString
                Else
                    poTagItem.Pallet_No = ""
                End If


                poTagItem.TAG_Status = 0

                If .Item("str1").ToString IsNot Nothing Then
                    poTagItem.Ref_No1 = .Item("str1").ToString
                Else
                    poTagItem.Ref_No1 = ""
                End If

                If .Item("str2").ToString IsNot Nothing Then
                    poTagItem.Ref_No2 = .Item("str2").ToString
                Else
                    poTagItem.Ref_No2 = ""
                End If


                If .Item("Qty").ToString IsNot Nothing Then
                    poTagItem.Qty = .Item("Qty").ToString
                Else
                    poTagItem.Qty = 0
                End If


                If .Item("Weight").ToString IsNot Nothing Then
                    poTagItem.Weight = .Item("Weight").ToString
                Else
                    poTagItem.Weight = 0
                End If


                If .Item("Volume").ToString IsNot Nothing Then
                    poTagItem.Volume = .Item("Volume").ToString
                Else
                    poTagItem.Volume = 0
                End If

            End With

            poTagItem.Qty_per_TAG = poTagItem.Qty
            poTagItem.Weight_per_TAG = poTagItem.Weight
            poTagItem.Volume_per_TAG = poTagItem.Volume

            poTagItem.Ref_No3 = "1/1"
            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


    'Private Sub grdTag_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTag.CellDoubleClick
    '    btnEditOrder_Click(sender, e)
    'End Sub

#Region " GetReportName "

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As System.Data.DataTable = New System.Data.DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
#End Region



    Private Sub btn_Print_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click
        Try

            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

            Try
                Me.PrintBarcode()
                'Me.getTAG()
                '###################################
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub PrintBarcode()
        Try

            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Dim dtTAG As New System.Data.DataTable

            Try
                'Select Case Report_Name.ToUpper
                '    Case "TAGSTICKERPRINTOUT", "TAGSTICKERPRINTOUT_MINI", "TAGSTICKERCHEMICALPRINTOUT_MINI", "TAGSTICKERSPBRPRINTOUT_MINI"
                Dim oReport As New clsReport()
                Dim TAG_Index_IN As String = ""
                Dim drArr() As DataRow = DirectCast(Me.grdTag.DataSource, System.Data.DataTable).Select(" chkSelect=1 and TAG_Status <> -1 ")
                If drArr.Length > 0 Then
                    For Each dr As DataRow In drArr
                        TAG_Index_IN &= "'" & dr("TAG_Index").ToString() & "',"

                        ' BGN Update PrintCount
                        Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.NULL)
                        objTag.UpdatePrintStatus(dr("TAG_Index").ToString())
                        ' END Update PrintCount
                    Next
                Else
                    W_MSG_Information_ByIndex("300032")
                    Exit Sub
                End If
                TAG_Index_IN = TAG_Index_IN.Substring(0, TAG_Index_IN.Trim.Length - 1)
                Dim strWhere As String = " and TAG_Index in(" & TAG_Index_IN & ")"

                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                rpt = oReport.GetReportInfo(Report_Name, strWhere)

                frm.CrystalReportViewer1.ReportSource = rpt
                frm.ShowDialog()
                'End Select


                '###################################
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally

            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    'Sub PrintBarcode()
    '    Try

    '        'Dim oconfig_Report As New config_Report
    '        'Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
    '        'Dim dtTAG As New System.Data.DataTable

    '        'Try
    '        '    CType(grdTag.DataSource, DataTable).AcceptChanges()
    '        '    dtTAG = grdTag.DataSource
    '        '    Dim drTagArr() As DataRow = dtTAG.Select("chkSelect=1 and TAG_Status <> -1")

    '        '    Dim strBarcode1 As String = ""
    '        '    Dim odtImage As New System.Data.DataTable
    '        '    odtImage.Columns.Add("pic", GetType(System.Byte()))

    '        '    Dim strWherePlus As String = " AND "
    '        '    Dim tag_index As String = ""
    '        '    Dim TAG_No As String = ""
    '        '    Dim wh As String
    '        '    Dim chk_valid As Boolean = False
    '        '    For Each drTag As DataRow In drTagArr
    '        '        tag_index = drTag("TAG_Index").ToString
    '        '        TAG_No = drTag("TAG_No").ToString
    '        '        'objTag.DeleteAllTagForOrderItem(Me.Tag_Index)
    '        '        strWherePlus &= "  TAG_Index = '" & tag_index & "' OR"
    '        '        chk_valid = True

    '        '        'Generate Barcode
    '        '        strBarcode1 = TAG_No
    '        '        'Dim objBarcode As New Barcode
    '        '        'objBarcode.GenBarcode(strBarcode1)
    '        '        'Dim odr As DataRow
    '        '        'odr = odtImage.NewRow
    '        '        'odr("pic") = ConvertFileToByte(Application.StartupPath & "\" & strBarcode1 & ".bmp")
    '        '        'If System.IO.File.Exists(Application.StartupPath & "\" & strBarcode1 & ".bmp") Then
    '        '        '    System.IO.File.Delete(Application.StartupPath & "\" & strBarcode1 & ".bmp")
    '        '        'End If
    '        '        'odtImage.Rows.Add(odr)

    '        '        Dim objBarcode As New ml_TNP
    '        '        objBarcode.GenBarcode(strBarcode1)
    '        '        Dim odr As DataRow
    '        '        odr = odtImage.NewRow
    '        '        odr("pic") = ConvertFileToByte(Application.StartupPath & "\" & strBarcode1 & ".bmp")
    '        '        If System.IO.File.Exists(Application.StartupPath & "\" & strBarcode1 & ".bmp") Then
    '        '            System.IO.File.Delete(Application.StartupPath & "\" & strBarcode1 & ".bmp")
    '        '        End If
    '        '        odtImage.Rows.Add(odr)


    '        '    Next
    '        '    If chk_valid = False Then
    '        '        W_MSG_Information_ByIndex("300032") 'กรุณาเลือกรายการ
    '        '        Exit Sub
    '        '    End If

    '        '    wh = strWherePlus.Substring(1, strWherePlus.Length - 3)


    '        '    Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
    '        '    Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, wh, odtImage)
    '        '    frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
    '        '    frm.ShowDialog()

    '        '    '###################################

    '        Dim oconfig_Report As New config_Report
    '        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
    '        Dim dtTAG As New System.Data.DataTable

    '        Try
    '            Select Case Report_Name
    '                Case "TagStickerPrintOut"
    '                    Dim SQL As New System.Text.StringBuilder
    '                    Dim strWhere As New System.Text.StringBuilder
    '                    For Each dr As DataRow In CType(grdTag.DataSource, DataTable).Copy().Select("chkSelect = 1 and TAG_Status <> -1")
    '                        strWhere.Append(String.Format("'{0}',", dr("TAG_No")))
    '                    Next
    '                    SQL.Append(" SELECT TAG_No,Order_No,Order_Date,Ref_No1 FROM VIEW_TAG")
    '                    SQL.Append(" WHERE TAG_No IN ({0})")
    '                    SQL.Append(" GROUP BY TAG_No,Order_No,Order_Date,Ref_No1")
    '                    Dim objCon As New WMS_STD_Formula.DBType_SQLServer
    '                    Dim dt As System.Data.DataTable = objCon.DBExeQuery(String.Format(SQL.ToString(), strWhere.Append("''").ToString()))
    '                    If dt.Rows.Count = 0 Then Throw New Exception("ไม่สามารถพิมพ์พาเลทได้")
    '                    dt.Columns.Add("Pic", GetType(System.Byte()))
    '                    For Each dr As DataRow In dt.Rows
    '                        dr("Pic") = cls_GenQRCode.StaticGenQRCodeToByte(dr("TAG_No"))
    '                    Next
    '                    Dim ds As New DataSet("dsTag")
    '                    dt.TableName = "VIEW_TAG"
    '                    ds.Tables.Add(dt)

    '                    Dim rpt As New rptTAG2
    '                    rpt.SetDataSource(ds)

    '                    'Set Default Printer
    '                    Dim strSQL As String = "SELECT TOP 1 Config_Value FROM config_CustomSetting WHERE Config_Key = 'DEFAULT_PRINTER_2x1.5'"
    '                    Dim default_printer As New cls_Default_Printer()
    '                    default_printer.SetPrinter(New DBType_SQLServer().DBExeQuery_Scalar(strSQL), "", "")

    '                    Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
    '                    frm.CrystalReportViewer1.ReportSource = rpt
    '                    frm.ShowDialog()

    '                    default_printer.SetDefaultPrinter()
    '                    default_printer.Dispose()

    '                    dt.Dispose()
    '                    ds.Dispose()
    '                    Exit Sub
    '                Case "Tag_Hanwa", "Tag_Hanwa_4x6"
    '                    If grdTag.DataSource Is Nothing OrElse CType(grdTag.DataSource, DataTable).Rows.Count = 0 Then Exit Sub
    '                    dtTAG = grdTag.DataSource
    '                    Dim drTag() As DataRow = dtTAG.Select("chkSelect=1 and TAG_Status <> -1")
    '                    If drTag.Length = 0 Then Exit Sub
    '                    Dim ds As New DataSet("dsTag_Hanwa")
    '                    Dim strSQL As String = "SELECT * FROM VIEW_RPT_Tag_Hanwa WHERE Tag_Index IN ({0})"
    '                    Dim strWhere As String = ""
    '                    For Each dr As DataRow In drTag
    '                        strWhere &= "'" & dr("TAG_Index").ToString() & "',"
    '                    Next
    '                    Dim objCon As New DBType_SQLServer
    '                    Dim dt As System.Data.DataTable = objCon.DBExeQuery(String.Format(strSQL, strWhere.Substring(0, strWhere.Length - 1)))
    '                    If dt Is Nothing OrElse dt.Rows.Count = 0 Then
    '                        W_MSG_Information("ไม่พบเลข Tag นี้")
    '                        Exit Sub
    '                    End If
    '                    dt.TableName = "VIEW_RPT_Tag_Hanwa"
    '                    dt.Columns.Add("Tag_Img", GetType(System.Byte()))
    '                    Dim genTag As New cls_GenQRCode()
    '                    For Each dr As DataRow In dt.Rows
    '                        If Not String.IsNullOrEmpty(dr("TAG_Index").ToString()) Then
    '                            dr("Tag_Img") = genTag.GenQRCodeToByte(dr("TAG_No").ToString())
    '                        End If
    '                    Next
    '                    ds.Tables.Add(dt)
    '                    Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
    '                    Dim cry As New Object
    '                    Select Case Report_Name
    '                        Case "Tag_Hanwa"
    '                            strSQL = "SELECT TOP 1 Config_Value FROM config_CustomSetting WHERE Config_Key = 'DEFAULT_PRINTER_A4'"
    '                            cry = New rptTag_Hanwa
    '                        Case Else 'Tag_Hanwa_4x6
    '                            cry = New rptTag_Hanwa_4x6
    '                            strSQL = "SELECT TOP 1 Config_Value FROM config_CustomSetting WHERE Config_Key = 'DEFAULT_PRINTER_4x6'"
    '                    End Select

    '                    'Set Default Printer
    '                    Dim default_printer As New cls_Default_Printer()
    '                    default_printer.SetPrinter(objCon.DBExeQuery_Scalar(strSQL), "", "")

    '                    cry.SetDataSource(ds)
    '                    frm.CrystalReportViewer1.ReportSource = cry
    '                    frm.ShowDialog()

    '                    default_printer.SetDefaultPrinter()

    '                    default_printer.Dispose()
    '                    strSQL = Nothing
    '                    strWhere = Nothing
    '                    objCon = Nothing
    '                    genTag.Dispose()
    '                    cry.Dispose()
    '                    frm.Dispose()
    '                    dt.Dispose()
    '                    ds.Dispose()
    '                    Exit Sub
    '            End Select
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        Finally
    '        End Try

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Function GenQRCodeToByte(ByVal pText As String) As Byte()
    '    Try
    '        Dim appSet As New Configuration.AppSettingsReader()
    '        Dim ResultByte() As Byte
    '        '  Dim QRGen As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    '        Dim QRGen As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
    '        Dim pic As System.Drawing.Image
    '        Dim BitmapConverter As System.ComponentModel.TypeConverter
    '        QRGen.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
    '        QRGen.QRCodeScale = CInt(appSet.GetValue("QRCodeScale", GetType(String)))
    '        QRGen.QRCodeVersion = CInt(appSet.GetValue("QRCodeVersion", GetType(String)))
    '        pic = QRGen.Encode(pText)
    '        BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
    '        ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
    '        Return ResultByte

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '        Return Nothing
    '    End Try

    'End Function

    Private Sub btnMultiSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMultiSearch.Click
        Try
            Dim frm As New frmMultiSearch
            frm.ShowDialog()
            If frm.ArrDocument_No.Trim <> "" Then

                Me.getTAG(frm.ArrDocument_No.Trim)

                Me.ChkAll.Checked = False
                Me.ChkAll.Checked = True
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainTag_Reprint_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigLanguage
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, -10)
                    oFunction.SW_Language_Column(Me, Me.grdTag, -10)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop_Search.Click
        Try
            If Me.rdbCustomer.Checked Then
                Dim frm As New frmCustmer_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomer_Index As String = ""
                tmpCustomer_Index = frm.Customer_Index 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                'If Not tmpCustomer_Index = "" Then
                Me.txtKeySearch.Text = frm.strCustomer_Name_Id
                Me.txtKeySearch.Tag = frm.Customer_Index
                'Else
                'Me.txtKeySearch.Text = ""
                'Me.txtKeySearch.Tag = ""
                'End If
                ' *********************
                frm.Close()
                If Not String.IsNullOrEmpty(Me.txtKeySearch.Text) Then
                    Me.btnSearch_Click(New Object, New EventArgs)
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If (Me.grdTag.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Dim ds As New DataSet

            ds.Tables.Add(DataGridViewToDataTable(Me.grdTag))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            Me.export(ds)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub export(ByVal ds As DataSet)
        Try
            Dim saveFileDialog As New SaveFileDialog
            saveFileDialog.Filter = "Excel File|*.xls"
            saveFileDialog.Title = "Save an Excel File"
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            saveFileDialog.FileName = Me.Text
            If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Me.exportExcel(saveFileDialog.FileName, ds)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    Private Sub exportExcel(ByVal strFileName As String, ByVal dsExport As DataSet)
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        'start the application
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim xlApp As Object = CreateObject("Excel.Application")

        'get the window handle
        Dim xlHWND As Integer = xlApp.Hwnd

        'this will have the process ID after call to GetWindowThreadProcessId
        Dim xlProcessId As Integer = 0

        'get the process ID
        GetWindowThreadProcessId(xlHWND, xlProcessId)

        'get the process
        Dim xlProcess As Process = Process.GetProcessById(xlProcessId)

        Dim isFileOpen As Boolean = False

        'Dim xlWorkBook As Workbook
        'Dim xlWorkSheet As Worksheet
        Dim xlWorkBook As New Object
        Dim xlWorkSheet As New Object
        Try
            If xlApp Is Nothing Then
                W_MSG_Error("Excel is not properly installed")
                Return
            End If
            If IsNumeric(xlApp.Version) AndAlso CDec(xlApp.Version) < 11 Then
                W_MSG_Error("Excel is lower than version 11 (2003)")
                Return
            End If

            Dim misValue As Object = System.Reflection.Missing.Value
            'Dim chartRange As Range
            Dim sheetIndex As Integer = 0

            xlWorkBook = xlApp.Workbooks.Add(misValue)

            ' Copy each DataTable as a new Sheet
            For Each dt As System.Data.DataTable In dsExport.Tables
                Dim Title_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)
                For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                    With dt.Columns(iCol)
                        If .ColumnName.Contains("title_") Then
                            Title_Dict.Add(.ColumnName, dt.Rows(0).Item(.ColumnName).ToString())
                            dt.Columns.Remove(dt.Columns(iCol))
                        End If
                    End With
                Next
                sheetIndex += 1

                ' Copy the DataTable to an object array
                Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

                ' Copy the column names to the first row of the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    rawData(0, col) = dt.Columns(col).ColumnName
                Next

                ' Copy the values to the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    For row As Integer = 0 To dt.Rows.Count - 1
                        rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
                    Next
                Next

                Dim finalColLetter As String = ColumnIndexToColumnLetter(dt.Columns.Count)

                ' Create a new Sheet
                xlWorkSheet = CType(xlWorkBook.Sheets.Add(xlWorkBook.Sheets(sheetIndex), Type.Missing, 1, XlSheetType.xlWorksheet), Worksheet)

                'xlWorkSheet = xlWorkBook.Sheets(dt.TableName)
                xlWorkSheet.Name = dt.TableName

                ' Format
                'xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).NumberFormat = "@"
                For iCol As Integer = 0 To dt.Columns.Count - 1
                    Select Case dt.Columns(iCol).DataType.Name
                        Case "Boolean", "String"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                        Case "DateTime", "TimeSpan"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "dd/MM/yyyy"
                        Case "Decimal", "Double"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0.000000"
                        Case "Int16", "Int32", "Int64"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0"
                        Case Else
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                    End Select
                Next
                'xlWorkSheet.Range("B:B").NumberFormat = "dd/MM/yyyy"
                'xlWorkSheet.Range("C:D").NumberFormat = "@"
                'xlWorkSheet.Range("E:F").NumberFormat = "#,##0"
                'xlWorkSheet.Range("G:H").NumberFormat = "@"
                'xlWorkSheet.Range("I:K").NumberFormat = "#,##0.00"
                'xlWorkSheet.Range("L:L").NumberFormat = "@"

                ' data export to Excel
                Dim iStartRows As Integer = 1
                Dim excelRange As String = String.Format("A{0}:{1}{2}", iStartRows, finalColLetter, iStartRows + dt.Rows.Count)
                xlWorkSheet.Range(excelRange, Type.Missing).Value2 = rawData

                'Dim headerList As New List(Of String)
                '

                'For iHeader As Integer = 1 To headerList.Count
                '    xlWorkSheet.Cells(1, iHeader) = headerList.Item(iHeader - 1)
                'Next

                'For iCols As Integer = 1 To 18
                '    ' Merge Cell
                '    xlWorkSheet.Range(String.Format("{0}1:{0}3", ColumnIndexToColumnLetter(iCols))).Merge()
                'Next
                'xlWorkSheet.Range(String.Format("{0}1:{1}1", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(24))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(22))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(23), ColumnIndexToColumnLetter(24))).Merge()

                ' Font Bold
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Bold = True

                ' Align
                xlWorkSheet.Range("A:AU").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("B:B").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("C:D").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("E:F").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("G:H").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("I:K").HorizontalAlignment = XlHAlign.xlHAlignRight
                'xlWorkSheet.Range("L:L").HorizontalAlignment = XlHAlign.xlHAlignLeft

                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).VerticalAlignment = XlVAlign.xlVAlignCenter
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).HorizontalAlignment = XlHAlign.xlHAlignCenter

                ' Fill Color
                'xlWorkSheet.Range(String.Format("A1:{0}3", finalColLetter)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray)
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Interior.ColorIndex = 56

                ' Font Color
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

                ' Border Cell
                xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)
                With xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).Borders(XlBordersIndex.xlInsideVertical)
                    .LineStyle = XlLineStyle.xlContinuous
                    .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = XlBorderWeight.xlThin
                End With
                With xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Borders(XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = XlLineStyle.xlContinuous
                    .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = XlBorderWeight.xlThin
                End With
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)

                '' Read Data
                'Dim strCell As String = ""
                'Dim iCellTemp As Integer = 0
                'Dim strCellTemp As String = ""
                'Dim flag As Boolean = False
                'For iRows As Integer = iStartRows + 1 To iStartRows + dt.Rows.Count + 1
                '    strCell = xlWorkSheet.Cells(iRows, 1).value
                '    If iCellTemp = 0 Then
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                '    If strCell <> strCellTemp Then
                '        If flag Then
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 15
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        Else
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 2
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        End If
                '        flag = Not flag
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                'Next

                ' Set Font
                xlWorkSheet.Cells.Font.Name = "Tahoma"
                xlWorkSheet.Cells.Font.Size = 10
                'xlWorkSheet.Rows.RowHeight = 18
                xlWorkSheet.Cells.WrapText = False

                ' Set Column Autosize
                xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).EntireColumn.AutoFit()
                'xlWorkSheet.Range("D:D").EntireColumn.ColumnWidth = 20
                'xlWorkSheet.Range("E:F").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("G:G").EntireColumn.ColumnWidth = 50
                'xlWorkSheet.Range("H:H").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("I:K").EntireColumn.ColumnWidth = 12
                'xlWorkSheet.Range("L:L").EntireColumn.ColumnWidth = 20

                xlWorkSheet = Nothing
            Next

            '--------------------------------------------------------

            'Dim strFileName As String = fileName
            'Dim isFileOpen As Boolean = False
            Try
                Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                fileTemp.Close()
            Catch ex As Exception
                isFileOpen = True
                MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            If Not isFileOpen Then
                If System.IO.File.Exists(strFileName) Then
                    System.IO.File.Delete(strFileName)
                End If
                xlWorkBook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            End If
            isFileOpen = True
            xlApp.Visible = True

        Catch ex As Exception
            If Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
            If Not isFileOpen AndAlso Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            ReleaseObject(xlApp)
            ReleaseObject(xlWorkBook)
            ReleaseObject(xlWorkSheet)
        End Try
    End Sub

    Private Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = String.Empty
        Dim modnum As Integer = 0

        While div > 0
            modnum = (div - 1) Mod 26
            colLetter = Chr(65 + modnum) & colLetter
            div = CInt((div - modnum) \ 26)
        End While

        Return colLetter
    End Function

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Function IfNullObj(ByVal o As Object, Optional ByVal DefaultValue As String = "") As String
        Dim ret As String = ""
        Try
            If o Is DBNull.Value Then
                ret = DefaultValue
            Else
                ret = o.ToString
            End If
            Return ret
        Catch ex As Exception
            Return ret
        End Try
    End Function

    Private Function DataGridViewToDataTable(ByVal dtg As DataGridView, Optional ByVal DataTableName As String = "myDataTable") As System.Data.DataTable
        Try
            Dim dt As New System.Data.DataTable(DataTableName)
            Dim row As DataRow
            Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
            Dim visibleList As New List(Of String)
            'Add Datacolumn
            For Each c As DataGridViewColumn In dtg.Columns
                'If c.Visible = False Then Continue For
                If c.Visible Then
                    visibleList.Add(c.HeaderText)
                End If
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.ColumnName = c.HeaderText
                If (Not c.ValueType Is Nothing) Then
                    idColumn.DataType = c.ValueType
                End If
                dt.Columns.Add(idColumn)
            Next
            'Now Iterate thru Datagrid and create the data row
            For Each dr As DataGridViewRow In dtg.Rows
                'Iterate thru datagrid
                row = dt.NewRow 'Create new row
                'Iterate thru Column 1 up to the total number of columns
                Try
                    For cn As Integer = 0 To TotalDatagridviewColumns
                        If dr.Cells(cn).Value IsNot Nothing Then
                            row.Item(cn) = dr.Cells(cn).Value
                            'If IsDate(row.Item(cn).GetType) Then
                            'row.Item(cn) = IfNullObj(dr.Cells(cn).Value) ' This Will handle error datagridviewcell on NULL Values
                            'Else
                            '    row.Item(cn) = DBNull.Value
                            'End If

                        End If

                    Next
                Catch ex1 As Exception
                    W_Language.W_MSG_Error(ex1.Message.ToString)
                End Try

                'Now add the row to Datarow Collection
                dt.Rows.Add(row)
            Next
            For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                If Not visibleList.Contains(dt.Columns(iCol).ColumnName) Then
                    dt.Columns.Remove(dt.Columns(iCol))
                End If
            Next
            'Now return the data table
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class