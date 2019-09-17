Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_OAW_Adjust_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports WMS_STD_OAW_Report
Imports WMS_STD_OAW_Adjust

Public Class frmAdjust
    Dim objHeaderAdjust As New tb_Adjust

    Private _Adjust_Index As String = ""
    Private _Count_Amount As Integer = 0

    Public Property Adjust_Index() As String
        Get
            Return _adjust_index
        End Get
        Set(ByVal value As String)
            _adjust_index = value
        End Set
    End Property

    Private _Status As Integer = 0

    'Public Property Status() As Integer
    '    Get
    '        Return _Status
    '    End Get
    '    Set(ByVal value As Integer)
    '        _Status = value
    '    End Set
    'End Property

    Private _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU As String = ""
    Private _Customer_Index As String = ""


#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        CANCEL
        NULL
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal Adjust_Index As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

        Select Case objStatus

            Case enuOperation_Type.UPDATE
                Me._adjust_index = Adjust_Index
                Me._Adjust_Index = Me._Adjust_Index

        End Select

    End Sub


#End Region

#Region "Variable & Property "


#End Region

#Region " Load Value to ComboBox "


    Private Sub getSKU_Description()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        'Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable

        'Try
        '    objClassDB.getSKU_MasterDetail("", "")
        '    objDT = objClassDB.DataTable

        '    cboSKU_Description.DisplayMember = "SKU_Description"

        '    cboSKU_Description.ValueMember = "Sku_Index"

        '    cboSKU_Description.DataSource = objDT


        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objClassDB = Nothing
        '    objDT = Nothing
        'End Try

    End Sub
    Private Sub getItemStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("ItemStatus_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("ItemStatus") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("ItemStatus") = "Show All"
            End Select
            objDT.Rows.Add(drNew)


            cboItemStatus.DisplayMember = "ItemStatus"
            cboItemStatus.ValueMember = "ItemStatus_Index"
            cboItemStatus.DataSource = objDT
            cboItemStatus.SelectedValue = "-11"


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report_Adjust '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cbPrint
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

    Private Sub getLock()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New ml_SCGSRC
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.Search_Lock()
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cbLock
                .DisplayMember = "lock"
                .ValueMember = "lock"
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


    Private Sub getLock2()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New ml_SCGSRC
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.Search_Lock()
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cbLock2
                .DisplayMember = "lock"
                .ValueMember = "lock"
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

    Private Function SaveData() As String
        Dim objHeader As New tb_Adjust
        Dim objItem As New tb_AdjustItemLocation
        Dim objItemCollection As New List(Of tb_AdjustItemLocation)
        Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

        Try
            ' *********** Define Value for Header ***********
            objHeader.Count_Amount = Me._Count_Amount
            objHeader.Adjust_Index = Me._Adjust_Index
            objHeader.Adjust_No = Me.txtAdjust_No.Text
            objHeader.Adjust_Date = Me.dtpAdjust_Date.Value
            objHeader.Adjust_Time = Me.txtTimes.Text

            ' xxxxxxx Using TAG Properties xxxxxxxxxxx
            objHeader.Customer_Index = _Customer_Index '"0"
            ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

            objHeader.Ref_No1 = Me.txtRef_No1.Text
            objHeader.Str1 = Me.txtStr1.Text
            objHeader.Str2 = Me.txtStr2.Text
            objHeader.Comment = Me.txtComment.Text
            objHeader.DocumentType_Index = cboDocumentType.SelectedValue.ToString

            objHeader.Status = Me._Status
            ' *** Load value to object ***
            Me.objHeaderAdjust = objHeader
            ' ****************************
            If objHeader.Adjust_No.Trim = "" Then
                Dim strWhere As String = ""
                Dim objDocumentNumber As New Sy_DocumentNumber
                objHeader.Adjust_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Adjust_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                txtAdjust_No.Text = objHeader.Adjust_No
                objDocumentNumber = Nothing
            End If



            CType(Me.grdAdjustItem.DataSource, DataTable).AcceptChanges()
            Dim dtDataSource As New DataTable
            dtDataSource = CType(Me.grdAdjustItem.DataSource, DataTable)
            Dim drArrAdjustItem() As DataRow = dtDataSource.Select("chkSelect=1")
            Dim iRowSeq As Integer = 1
            For Each drAdjustItem As DataRow In drArrAdjustItem
                ' *** New Object *********
                objItem = New tb_AdjustItemLocation
                ' ************************
                With Me.grdAdjustItem
                    objItem.Adjust_Index = objHeader.Adjust_Index
                    ' *** Check value of AdjustItemLocation_Index ***                 
                    If drAdjustItem("AdjustItemLocation_Index").ToString = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        drAdjustItem("AdjustItemLocation_Index") = objDBIndex.getSys_Value("AdjustItemLocation_Index")
                        objItem.AdjustItemLocation_Index = drAdjustItem("AdjustItemLocation_Index").ToString
                        objDBIndex = Nothing
                    Else
                        objItem.AdjustItemLocation_Index = drAdjustItem("AdjustItemLocation_Index").ToString
                    End If
                    If drAdjustItem("Tag_No").ToString <> "" Then
                        objItem.Tag_No = drAdjustItem("Tag_No").ToString
                    End If
                    If drAdjustItem("LocationBalance_Index").ToString <> "" Then
                        objItem.LocationBalance_Index = drAdjustItem("LocationBalance_Index").ToString
                    End If
                    If drAdjustItem("Location_Index").ToString <> "" Then
                        objItem.Location_Index = drAdjustItem("Location_Index").ToString
                    End If
                    If drAdjustItem("Sku_Index").ToString <> "" Then
                        objItem.Sku_Index = drAdjustItem("Sku_Index").ToString
                    Else
                        objItem.Sku_Index = ""
                    End If
                    'If grdAdjustItemdrAdjustItem("").ToString <> "" Then
                    '    objItem.ItemStatus_Index = drAdjustItem("").Tostring
                    'Else
                    '    objItem.ItemStatus_Index = ""
                    'End If
                    If drAdjustItem("ItemStatus_Index").ToString <> "" Then
                        objItem.ItemStatus_Index = drAdjustItem("ItemStatus_Index").ToString
                    Else
                        objItem.ItemStatus_Index = ""
                    End If
                    If drAdjustItem("Order_Index").ToString <> "" Then
                        objItem.Order_Index = drAdjustItem("Order_Index").ToString
                    Else
                        objItem.Order_Index = ""
                    End If
                    If drAdjustItem("Plot").ToString <> "" Then
                        objItem.Plot = drAdjustItem("Plot").ToString
                    Else
                        objItem.Plot = ""
                    End If
                    If drAdjustItem("Serial_No").ToString <> "" Then
                        objItem.Serial_No = drAdjustItem("Serial_No").ToString
                    Else
                        objItem.Serial_No = ""
                    End If
                    If IsNumeric(drAdjustItem("Qty_Bal")) Then
                        objItem.Qty_Bal = Val(drAdjustItem("Qty_Bal").ToString)
                    Else
                        objItem.Qty_Bal = 0
                    End If
                    If IsNumeric(drAdjustItem("Weight_Bal")) Then
                        objItem.Weight_Bal = Val(drAdjustItem("Weight_Bal").ToString)
                    Else
                        objItem.Weight_Bal = 0
                    End If

                    If IsNumeric(drAdjustItem("Volume_Bal")) Then
                        objItem.Volume_Bal = CDbl(drAdjustItem("Volume_Bal").ToString)
                    Else
                        objItem.Volume_Bal = 0
                    End If

                    objItem.Qty_1st_Count = 0
                    objItem.Qty_2nd_Count = 0
                    objItem.Qty_3rd_Count = 0
                    objItem.Weight_1st_Count = 0
                    objItem.Weight_2nd_Count = 0
                    objItem.Weight_3rd_Count = 0
                    objItem.Volume_1st_Count = 0
                    objItem.Volume_2nd_Count = 0
                    objItem.Volume_3rd_Count = 0

                    If drAdjustItem("ItemComment").ToString <> "" Then
                        objItem.ItemComment = drAdjustItem("ItemComment").ToString
                    Else
                        objItem.ItemComment = ""
                    End If

                    If drAdjustItem("Str1").ToString <> "" Then
                        objItem.Str1 = drAdjustItem("Str1").ToString
                    Else
                        objItem.Str1 = ""
                    End If
                    If drAdjustItem("Str2").ToString <> "" Then
                        objItem.Str2 = drAdjustItem("Str2").ToString
                    Else
                        objItem.Str2 = ""
                    End If

                    objItem.Adjust_Qty = 0
                    objItem.Adjust_Weight = 0
                    objItem.Adjust_Volume = 0
                    If IsDate(drAdjustItem("Mfg_date")) Then objItem.Mfg_date = CDate(drAdjustItem("Mfg_date"))
                    If IsDate(drAdjustItem("Exp_date")) Then objItem.Exp_date = CDate(drAdjustItem("Exp_date"))

                    'If IsNumeric(drAdjustItem("Seq")) Then
                    objItem.Seq = iRowSeq
                    'End If
                    iRowSeq += 1
                End With
                objItemCollection.Add(objItem)

            Next


            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection)
                    Me._Adjust_Index = objDB.SaveData
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE, objHeader, objItemCollection)
                    Me._Adjust_Index = objDB.SaveData
                    objDB = Nothing
            End Select

            If Not Me._Adjust_Index = "" Then
                _Adjust_Index = Me._Adjust_Index
                Me.btnSave.Enabled = False
                Me.grbSearch.Enabled = False

                'lblT2.Text = Date.Now.TimeOfDay.ToString
                ' Save Success
                MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "ผลการบันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                objStatus = enuOperation_Type.UPDATE
                Return "PASS"
            Else
                ' Save Success
                Return "FAIL"
                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "ผลการบันทีกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Return ex.Message.ToString
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            GC.Collect()

        End Try

        ' *************************************************
    End Function

    Private Sub GetInventory_For_Adjust_Check_Move(Optional ByVal mLocation_Alias As String = "")
        Try
            Dim sAction_Date As String = Me.dtTransaction_Date.Value.ToString("yyyy-MM-dd")
            Dim sAction_Time1 As String = Me.dtTransaction_Time1.Value.ToString("HH:mm")
            Dim sAction_Time2 As String = Me.dtTransaction_Time2.Value.ToString("HH:mm")

            Dim strSQL As String = ""
            If Me.chkByPick.Checked = True Then
                strSQL = " exec [dbo].[spAdjustCheckByPickking] '" & sAction_Date & "','" & sAction_Time1 & "','" & sAction_Time2 & "'"
            End If
            If Me.chkCheckMove.Checked = True Then
                '---------------------------------------------------------------------------------------------------
                Dim Location_Alias1 As String = ""
                Dim Location_Alias2 As String = ""
                Dim lock1 As String = ""
                Dim lock2 As String = ""
                Dim Zone As String = ""
                mLocation_Alias = "'" & mLocation_Alias.Replace("'", "'''") & "'"
                If Me.chkLocation.Checked = True Then
                    'strWhere &= " And (Location_Alias between '" & Me.txtLocation1.Text & "' and  '" & Me.txtLocation2.Text & "' )"
                    Location_Alias1 = Me.txtLocation1.Text
                    Location_Alias2 = Me.txtLocation2.Text
                End If

                If chbLock.Checked = True Then
                    'strWhere &= " And (lock between '" & cbLock.SelectedValue & "' and  '" & cbLock2.SelectedValue & "' )"
                    lock1 = Me.cbLock.SelectedValue
                    lock2 = Me.cbLock2.SelectedValue
                End If

                If Me.chkZone.Checked Then
                    If Me.cboZone.SelectedValue <> "-11" Then
                        'strWhere &= " And Zone_Index  ='" & Me.cboZone.SelectedValue & "' "
                        Zone = Me.cboZone.SelectedValue
                    End If
                End If

                'mLocation_Alias
                '---------------------------------------------------------------------------------------------------

                strSQL = " exec [dbo].[spAdjustCheckByMove] '" & sAction_Date & "','" & sAction_Time1 & "','" & sAction_Time2 & "','" & Location_Alias1 & "','" & Location_Alias2 & "','" & lock1 & "' ,'" & lock2 & "','" & Zone & "'"
            End If

            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSQL)
            objDT = objDb.DataTable

            Me.grdAdjustItem.DataSource = Me.GetRowSelected(objDT)

            Me.ManageSeq()

            If objDT.Rows.Count = 0 Then
                W_MSG_Information("ไม่พบข้อมูล")
            Else
                Dim numRows As Integer = 0
                numRows = grdAdjustItem.Rows.Count
                If numRows > 0 Then
                    lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
                Else
                    lbCountRows.Text = "ไม่พบรายการ"
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub GetInventory_For_Adjust(Optional ByVal mLocation_Alias As String = "")
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim strHaving As String = " HAVING 1=1 "
            Dim intDay As Integer = Me.dtpAdjust_Date.Value.Day
            Dim intMonth As Integer = Me.dtpAdjust_Date.Value.Month
            Dim intYear As Integer = Me.dtpAdjust_Date.Value.Year
            If intYear > 2500 Then
                intYear = intYear - 543
            End If

            '------------------------------------------------------------------------------------------
            'Get config Picking
            strWhere &= " WHERE 1=1 "

            Dim objPicking As New config_CustomSetting
            Dim strSelect As String = ""

            strWhere &= " And (Customer_Index ='" & Me._Customer_Index & "')"
            If Me.chkSku.Checked Then
                If txtSKU_ID.Tag <> "" Then
                    strWhere &= " And (Sku_Index ='" & Me.txtSKU_ID.Tag & "')   "
                End If
            Else
                Select Case cboDocumentType.SelectedValue
                    Case _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
                    Case Else
                End Select

            End If

            'strWhere &= " )  "

            Select Case cboDocumentType.SelectedValue
                Case _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
                    strSelect = objPicking.GetConfig_Picking("ADJUSTBYSKU")
                Case Else
                    strSelect = objPicking.GetConfig_Picking("ADJUST")
            End Select


            strSQL = strSelect

            If Me.chkProductType.Checked Then
                If Me.cbProductType.SelectedValue <> "-11" Then
                    strWhere &= " And ProductType_Index ='" & Me.cbProductType.SelectedValue & "' "
                End If
            End If

            If (Me.chkProductType2.Checked) Then
                Dim arrProductType() As String = txtProductType.Text.Split(vbNewLine)
                Dim ProductTypeList As New List(Of String)
                For Each str As String In arrProductType
                    If (Not str.Trim() = Nothing) Then
                        ProductTypeList.Add(str.Trim())
                    End If
                Next
                'If (ProductTypeList.Count = 1) Then
                '    strWhere &= String.Format(" {0} ProductType_Id like @ProductType_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                '    .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = "%" & Me.txtProductType.Text.Trim() & "%"
                If (ProductTypeList.Count > 0) Then
                    Dim str As String = String.Join("','", ProductTypeList.ToArray())
                    strWhere &= String.Format(" {0} ProductType_Id in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                End If
            End If

            If Me.chkItemStatus.Checked Then
                If Me.cboItemStatus.SelectedValue <> "-11" Then
                    strWhere &= " And ItemStatus_Index ='" & Me.cboItemStatus.SelectedValue & "' "
                End If
            End If
            If chkMfgDate.Checked = True Then
                Dim StartDate As String = Format(dtpFromDate_M.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(dtpToDate_M.Value, "yyyy/MM/dd").ToString() + "  23:59:59"
                strWhere &= " And SKU_Index IN (SELECT Sku_Index FROM tb_OrderItem "
                strWhere &= " WHERE Mfg_Date between '" & StartDate & "' and '" & EndtDate & "')"
            End If
            If chkExpDate.Checked = True Then
                Dim StartDate As String = Format(dtpFromDate_Ex.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(dtpToDate_Ex.Value, "yyyy/MM/dd").ToString() + "  23:59:59"
                strWhere &= " And SKU_Index IN (SELECT Sku_Index FROM tb_OrderItem "
                strWhere &= " WHERE convert(datetime,Exp_Date,103)  between '" & StartDate & "' and '" & EndtDate & "')"
            End If
            If chkOrderDate.Checked = True Then
                Dim StartDate As String = Format(DateM.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(DateE.Value, "yyyy/MM/dd").ToString() + "  23:59:59"
                strWhere &= " And SKU_Index IN (SELECT Sku_Index FROM tb_OrderItem inner join tb_Order On tb_OrderItem.Order_Index = tb_Order.Order_Index   "
                strWhere &= " WHERE Order_Date between '" & StartDate & "' and '" & EndtDate & "')"
            End If


            'If chkWithdrawDate.Checked = True Then
            '    Dim StartDate As String = Format(DateWithB.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
            '    Dim EndtDate As String = Format(DateWithF.Value, "yyyy/MM/dd").ToString() + "  23:59:59"
            '    strWhere &= " And SKU_Index IN (SELECT Sku_Index FROM tb_WithdrawItem LEFT OUTER JOIN tb_Withdraw ON tb_Withdraw.Withdraw_Index = tb_WithdrawItem.Withdraw_Index where  Withdraw_Date between '" & StartDate & "' and '" & EndtDate & "')"
            'End If

            'Get Condition From config Picking
            Dim strSort As String = " "
            Dim strGroup_By As String = " "
            'Dim strHaving As String = " "
            Select Case cboDocumentType.SelectedValue
                Case _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
                    strWhere &= objPicking.GetOther_Where("SORT_ADJUSTBYSKU", "") 'Other_Where :sort
                    strGroup_By = objPicking.GetOther_Where("ADJUSTBYSKU", "") 'Other_Where   :group
                    strSort &= objPicking.GetConfig_Picking("SORT_ADJUSTBYSKU") 'condition     :where
                    '---------------------------------------------------------------------------------------------------
                    If mLocation_Alias <> "" Then
                        strWhere &= "  And Location_Alias  in (" & mLocation_Alias & ") "
                    End If

                    If Me.chkLocation.Checked = True Then
                        strWhere &= " And (Location_Alias between '" & Me.txtLocation1.Text & "' and  '" & Me.txtLocation2.Text & "' )"
                    End If

                    If chbLock.Checked = True Then
                        strWhere &= " And (lock between '" & cbLock.SelectedValue & "' and  '" & cbLock2.SelectedValue & "' )"
                    End If

                    If Me.chkWarehouse.Checked Then
                        If Me.cbStore.SelectedValue <> "-11" Then
                            strWhere &= " And Warehouse_Index  ='" & Me.cbStore.SelectedValue & "' "
                        End If
                        If Me.cbRoom.SelectedValue <> "-11" Then
                            strWhere &= " AND Room = " & Me.cbRoom.SelectedValue & " " ' edit And to Or
                        End If
                    End If

                    If Me.chkZone.Checked Then
                        If Me.cboZone.SelectedValue <> "-11" Then
                            strWhere &= " And Zone_Index  ='" & Me.cboZone.SelectedValue & "' "
                        End If
                    End If

                    '---------------------------------------------------------------------------------------------------
                    'Merge String

                    strSQL = strSQL & strWhere & strGroup_By & strSort
                    '---------------------------------------------------------------------------------------------------

                Case Else

                    If Me.chkNoEmptyLoc.Checked = False Then
                        strWhere &= " or (isnull(Sku_Index,'') = '') "
                    End If


                    strWhere &= objPicking.GetOther_Where("SORT_ADJUST", "")
                    strGroup_By = objPicking.GetOther_Where("ADJUST", "")
                    strSort &= objPicking.GetConfig_Picking("SORT_ADJUST")


                    '---------------------------------------------------------------------------------------------------
                    If mLocation_Alias <> "" Then
                        strHaving &= "  And Location_Alias  in (" & mLocation_Alias & ") "
                    End If

                    If Me.chkLocation.Checked = True Then
                        strHaving &= " And (Location_Alias between '" & Me.txtLocation1.Text & "' and  '" & Me.txtLocation2.Text & "' )"
                    End If

                    If chbLock.Checked = True Then
                        strHaving &= " And (lock between '" & cbLock.SelectedValue & "' and  '" & cbLock2.SelectedValue & "' )"
                    End If

                    If Me.chkWarehouse.Checked Then
                        If Me.cbStore.SelectedValue <> "-11" Then
                            strHaving &= " And Warehouse_Index  ='" & Me.cbStore.SelectedValue & "' "
                        End If
                        If Me.cbRoom.SelectedValue <> "-11" Then
                            strHaving &= " AND Room = " & Me.cbRoom.SelectedValue & " " ' edit And to Or
                        End If
                    End If

                    If Me.chkZone.Checked Then
                        If Me.cboZone.SelectedValue <> "-11" Then
                            strHaving &= " And Zone_Index  ='" & Me.cboZone.SelectedValue & "' "
                        End If
                    End If

                    If Me.chkLocationType.Checked Then
                        If Me.cboLocationType.SelectedValue <> "-11" Then
                            strHaving &= " And LocationType_Index ='" & Me.cboLocationType.SelectedValue & "' "
                        End If
                    End If

                    '---------------------------------------------------------------------------------------------------
                    'Merge String

                    strSQL = strSQL & strWhere & strGroup_By & strHaving & strSort
                    '---------------------------------------------------------------------------------------------------

            End Select


            strWhere &= New clsUserByDC().GetDistributionCenterByUser()

            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSQL)
            objDT = objDb.DataTable

            Me.grdAdjustItem.DataSource = Me.GetRowSelected(objDT)

            Me.ManageSeq()

            If objDT.Rows.Count = 0 Then
                W_MSG_Information("ไม่พบข้อมูล")
            Else
                Dim numRows As Integer = 0
                numRows = grdAdjustItem.Rows.Count
                If numRows > 0 Then
                    lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
                Else
                    lbCountRows.Text = "ไม่พบรายการ"
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub ManageSeq()
        Try
            If Me.grdAdjustItem.RowCount = 0 Then Exit Sub
            CType(Me.grdAdjustItem.DataSource, DataTable).AcceptChanges()
            Dim iRow As Integer = 0
            For Each drSeq As DataRow In CType(Me.grdAdjustItem.DataSource, DataTable).Rows
                iRow += 1
                drSeq("Seq") = iRow
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function GetRowSelected(ByVal podtData As DataTable) As DataTable
        'Keep Selected Product
        Try
            CType(grdAdjustItem.DataSource, DataTable).AcceptChanges()

            Dim odtTemp As New DataTable
            Dim odrTempSelected() As DataRow    '--- Array Datarow
            Dim odrDuplicate() As DataRow       '--- Array Datarow
            odtTemp = grdAdjustItem.DataSource
            'STEP 1 : ถ้าไม่มีที่เลือกเลยให้ Clear Data Source
            odrTempSelected = odtTemp.Select("chkSelect=1") 'ตัวที่เลือกจาก DataSource Adjust
            If odrTempSelected.Length = 0 Then
                odtTemp = odtTemp.Clone
                podtData.Merge(odtTemp)
                Return podtData
            End If

            Dim odtTempSource As New DataTable
            odtTempSource = odtTemp.Clone
            odrTempSelected = odtTemp.Select("chkSelect=1") 'ตัวที่เลือกจาก DataSource Adjust
            If odrTempSelected.Length > 0 Then
                For Each odrdelete As DataRow In odrTempSelected
                    Dim odrData As DataRow
                    odrData = odtTemp.NewRow
                    odrData.ItemArray = odrdelete.ItemArray.Clone
                    odtTempSource.Rows.Add(odrdelete.ItemArray)
                Next
            End If

            'STEP 3 : เอาตัวที่ซ้ำออก
            Dim odtTemp2 As New DataTable
            odtTemp2 = podtData.Clone
            odtTemp2.Merge(odtTempSource) 'รวม DataSource Adjust เข้ากับ DataSource DataSelect

            'ลบข้อมูลที่ซ้ำออกจากข้อมูลที่เลือกมา
            podtData.Merge(odtTempSource)
            odrTempSelected = odtTemp2.Select("chkSelect=1") 'ตัวที่เลือกจาก DataSource Adjust
            For Each odrSelected As DataRow In odrTempSelected
                Select Case cboDocumentType.SelectedValue
                    Case _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
                        'strSelect = objPicking.GetConfig_Picking("ADJUSTBYSKU")
                        'strWhere &= " And Customer_Index ='" & Me._Customer_Index & "'"
                        odrDuplicate = podtData.Select("isnull(Sku_Index,'') = '" & odrSelected("Sku_Index").ToString & "'")
                    Case Else
                        odrDuplicate = podtData.Select("isnull(Location_Index,'') = '" & odrSelected("Location_Index").ToString & "' and isnull(Sku_Index,'') = '" & odrSelected("Sku_Index").ToString & "'")
                End Select

                If odrDuplicate.Length > 0 Then
                    For Each odrdelete As DataRow In odrDuplicate
                        podtData.Rows.Remove(odrdelete)
                    Next
                End If
            Next

            For Each odrSelected As DataRow In odrTempSelected
                Dim odrData As DataRow
                odrData = podtData.NewRow
                odrData.ItemArray = odrSelected.ItemArray.Clone
                If podtData.Rows.Count > 0 Then
                    podtData.Rows.InsertAt(odrData, 0)
                Else
                    podtData.Rows.Add(odrSelected.ItemArray)
                End If
            Next
            'End If


            Return podtData
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Function SetDateTime(ByVal DTP As DateTimePicker) As String

        DTP.Format = DateTimePickerFormat.Custom
        DTP.CustomFormat = "dd/MM/yyyy"
        Dim strDate As String = DTP.Text.Trim
        DTP.Format = DateTimePickerFormat.Long

        Return strDate
    End Function

    Private Sub Lock_Key_Input()
        Me.Count_1st_Total_Qty.ReadOnly = True
        Me.Count_2nd_Total_Qty.ReadOnly = True
        Me.Count_3rd_Total_Qty.ReadOnly = True
    End Sub
#Region "EXPORT TO EXCEL"

    Private Sub ExportToExcel(ByVal grdExport As DataGridView, ByVal Customer As String, ByVal Month As String, ByVal DocDate As String)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 2
            Dim ExcelApp As Excel.Application
            Dim ExcelBooks As Excel.Workbook
            Dim ExcelSheets As Excel.Worksheet
            ExcelApp = New Excel.Application

            Dim CurrentThread As System.Threading.Thread
            CurrentThread = System.Threading.Thread.CurrentThread
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
            'CurrentThread.CurrentCulture = New CultureInfo("en-GB")

            ExcelApp.Visible = True
            ExcelBooks = ExcelApp.Workbooks.Add()
            ExcelSheets = DirectCast(ExcelBooks.Worksheets(1), Excel.Worksheet)

            i = 0
            j = 2

            With ExcelSheets
                .Columns().ColumnWidth = 17


                .Range("C" & j.ToString()).Value = "STOCK REPORT"
                .Range("C" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("C" & j.ToString()).Font.Bold = True
                .Range("C" & j.ToString()).Font.Size = 14
                '.Range("A1").Interior.Color = RGB(224, 224, 224)

                ' ----- ROW ชื่อลูกค้า
                j += 1
                .Range("A" & j.ToString()).Value = "ชื่อลูกค้า"
                .Range("A" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("A" & j.ToString()).Font.Bold = True
                .Range("A" & j.ToString()).Font.Underline = False
                .Range("A" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)

                .Range("B" & j.ToString()).Value = Customer
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Underline = True
                .Range("B" & j.ToString()).Font.Size = 14

                ' -----ROW ประจำเดือน
                j += 1

                .Range("A" & j.ToString()).Value = "ประจำเดือน"
                .Range("A" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("A" & j.ToString()).Font.Bold = True
                .Range("A" & j.ToString()).Font.Underline = False
                .Range("A" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)

                .Range("B" & j.ToString()).Value = Month
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Underline = True
                .Range("B" & j.ToString()).Font.Size = 14

                ' -----ROW วันที่พิมพ์เอกสาร
                j += 1
                .Range("A" & j.ToString()).Value = "วันที่พิมพ์เอกสาร"
                .Range("A" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("A" & j.ToString()).Font.Bold = True
                .Range("A" & j.ToString()).Font.Underline = False
                .Range("A" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)

                .Range("B" & j.ToString()).Value = DocDate
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Underline = True
                .Range("B" & j.ToString()).Font.Size = 14


                '-----------DATA--------------
                j += 2

                Dim Col As Integer = 0
                Dim strCol As String = "A"
                Dim iChar As Integer = 65

                For Col = 0 To grdExport.ColumnCount - 1
                    strCol = Chr(iChar)
                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(192, 192, 192)
                    iChar += 1
                Next

                j += 1

                Dim dtgrdExport As New DataTable
                dtgrdExport = grdExport.DataSource


                Dim Row As Integer = 0
                'strCol = "A"
                'iChar = 65
                'Col = 0
                For Row = 0 To grdExport.RowCount - 1
                    strCol = "A"
                    iChar = 65
                    Col = 0
                    For Col = 0 To grdExport.ColumnCount - 1
                        strCol = Chr(iChar)
                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If


                        If strData = "0" Then
                            .Range(strCol & j.ToString()).Value = strData
                        ElseIf strData = "" Then
                            .Range(strCol & j.ToString()).Value = strData
                        ElseIf strData.Substring(0, 1) = "0" Then
                            .Range(strCol & j.ToString()).Value = "'" & strData 'ไม่ให้ 0 ตัวหน้าหาย
                        Else
                            .Range(strCol & j.ToString()).Value = strData
                        End If

                        .Range(strCol & j.ToString()).Font.Size = 9

                        iChar += 1
                    Next
                    j += 1
                Next

            End With

            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region
#Region "GET ADJUST HEADER & ADJUST ITEM"
    Private Sub getAdjustHeader(ByVal Adjust_Index As String)


        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getAdjustHeader(Adjust_Index)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then

                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.getCustomer(Me._Customer_Index)
                'txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                'txtCustomer_Name.Text = objDT.Rows(0).Item("Title").ToString + objDT.Rows(0).Item("Customer_Name").ToString

                txtAdjust_No.Text = objDT.Rows(0).Item("Adjust_No").ToString
                dtpAdjust_Date.Value = CDate(objDT.Rows(0).Item("Adjust_Date")) '.ToString("dd/MM/yyyy") 'objDT.Rows(0).Item("Adjust_Date").ToShortDateString
                txtTimes.Text = objDT.Rows(0).Item("Adjust_Time").ToString
                txtRef_No1.Text = objDT.Rows(0).Item("Ref_No1").ToString
                txtComment.Text = objDT.Rows(0).Item("Comment").ToString
                txtStr1.Text = objDT.Rows(0).Item("Str1").ToString
                txtStr2.Text = objDT.Rows(0).Item("Str2").ToString
                Me._Count_Amount = Val(objDT.Rows(0).Item("Count_Amount").ToString)
                cboDocumentType.SelectedValue = objDT.Rows(0).Item("DocumentType_Index").ToString

                Me._Status = objDT.Rows(0).Item("Status").ToString
                Me.ManageBotton()
            End If



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getAdjustItemDetail(ByVal Adjust_Index As String)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getAdjustItemDetail(Adjust_Index)
            objDT = objClassDB.DataTable
            Me.grdAdjustItem.DataSource = objDT

            'If objDT.Rows.Count = 0 Then
            '    W_MSG_Information("ไม่พบข้อมูล")
            'Else
            Dim numRows As Integer = 0
            numRows = grdAdjustItem.Rows.Count
            If numRows > 0 Then
                lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            Else
                lbCountRows.Text = "ไม่พบรายการ"
            End If
            'End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmAdjust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdAdjustItem.AutoGenerateColumns = False
            grdAdjExcel.AutoGenerateColumns = False
            Dim oFunction As New W_Language
            ''Insert
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdAdjustItem)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me, 4)
            oFunction.SW_Language_Column(Me, Me.grdAdjustItem, 4)

            ' *** Load data to ComboBox ***
            Dim config As New config_CustomSetting
            Me._DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU = config.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU")
            'Me._Customer_Index = config.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")
            Me.dtTransaction_Time1.Text = "00:00"
            Me.dtTransaction_Time2.Text = "23:58"

            Me.LoadDocumentType()
            Me.getSKU_Description()
            Me.getProductType()
            Me.getItemStatus()
            Me.getWarehouse()
            'Me.getRoom()
            Me.getReportName(4)
            Me.btnPrint.Enabled = False
            Me.getLock()
            Me.getLock2()
            'Me.getZone()
            Me.getLocationType()
            ' *****************************

            ' *** Lock Key Input ***
            Me.Lock_Key_Input()
            ' **********************
            pnlExcel.Visible = False

            'LoadDataSource
            Me.getAdjustHeader(_Adjust_Index)
            Me.getAdjustItemDetail(_Adjust_Index)

            ' *** set value Time in Count *** 
            Select Case Me.objStatus
                Case enuOperation_Type.ADDNEW
                    SetDEFAULT_CUSTOMER_INDEX()
                    ' *** Get Temp Adjust_Index ***
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    Me._Adjust_Index = objDBTempIndex.getSys_Value("Adjust_Index")
                    objDBTempIndex = Nothing
                    ' *****************************

                    Me.txtTimes.Text = DateTime.Now.ToString("HH:mm")
                    ' *** Lock Key Input *** 
                    With Me.grdAdjustItem
                        Me.Count_2nd_Total_Qty.ReadOnly = True
                        Me.Count_3rd_Total_Qty.ReadOnly = True
                    End With

                    btnExcel.Enabled = False
                    Me.btnCancel.Enabled = False
                    Me.btnSave.Enabled = False
                    Me.btnUpdate.Enabled = False
                    Me.btnShowItemList.Enabled = False
                    Me.btnExcel.Enabled = False
                    Me.btnPrint.Enabled = False
                    Me.btnSearch.Enabled = False
                    Me.btnConfirm.Enabled = False
                    Me.cbPrint.Enabled = False

                    Me.cboDocumentType.Enabled = True
                    Me.btnSave.Enabled = True
                    Me.btnSearch.Enabled = True
                Case enuOperation_Type.UPDATE
                    Me.txtAdjust_No.ReadOnly = True
                    Me.btnExcel.Enabled = True

                    'Me.getAdjustHeader(_Adjust_Index)
                    'Me.getAdjustItemDetail(_Adjust_Index)
                    'If _Status = 1 Then
                    '    Me.chkSelect.Visible = False
                    '    Me.chkSelectAll.Visible = False
                    '    Me.btnPrint.Enabled = True
                    '    Me.cbPrint.Enabled = True
                    '    Me.btnSearch.Enabled = False
                    '    Me.btnExcel.Enabled = True
                    'ElseIf -1 Then
                    '    Me.grbSearch.Enabled = False
                    '    Me.btnSave.Enabled = False
                    '    Me.btnPrint.Enabled = False
                    '    Me.chkSelect.Visible = False
                    '    Me.chkSelectAll.Enabled = False
                    '    Me.btnSearch.Enabled = False
                    '    Me.btnExcel.Enabled = False
                    'Else

                    '    Me.grbSearch.Enabled = False
                    '    Me.btnSave.Enabled = False
                    '    Me.btnPrint.Enabled = True
                    '    Me.chkSelect.Visible = False
                    '    Me.chkSelectAll.Enabled = False
                    '    Me.btnSearch.Enabled = False
                    '    Me.btnExcel.Enabled = True
                    'End If

            End Select

            '0:ยกเลิก
            '0:ไม่ระบุ
            '1:รอยืนยันเอกสาร
            '2:รอยืนยันการปรับยอด
            '3:เสร็จสิ้น
            '4:กำลังตรวจนับ
            Select Case Me._Status
                Case 1, 2, 4
                    Me.btnDelete.Enabled = True
                Case Else
                    Me.btnDelete.Enabled = False
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub SetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try

            '_USE_PRODUCT_CUSTOMER = objCustomSetting.getConfig_Key_USE("USE_PRODUCT_CUSTOMER")
            'Me._Customer_Index = objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            Me._Customer_Index = oUser.GetUserByCustomerDefault()
            If Me._Customer_Index <> "" Then Me.getCustomer()

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

    'Sub SetDEFAULT_CUSTOMER_INDEX()
    '    Dim objCustomSetting As New config_CustomSetting
    '    Dim objDT As DataTable = New DataTable

    '    Try
    '        objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
    '        objDT = objCustomSetting.DataTable
    '        If objDT.Rows.Count > 0 Then
    '            Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
    '            getCustomer(Me._Customer_Index)
    '        End If

    '        '###################################
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objCustomSetting = Nothing
    '    End Try

    'End Sub
    Private Sub ManageBotton()
        Try
            Me.btnCancel.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnShowItemList.Enabled = False
            Me.btnExcel.Enabled = False
            Me.btnPrint.Enabled = False
            Me.btnSearch.Enabled = False
            Me.chkSelect.Visible = False
            Me.chkSelectAll.Visible = False
            Me.btnConfirm.Enabled = False
            Me.cboDocumentType.Enabled = False
            Me.cbPrint.Enabled = False

            '-1:         ยกเลิก
            '0:          ไม่ระบุ()
            '1:          รอยืนยันเอกสาร
            '2:          รอยืนยันการปรับยอด()
            '3:          เสร็จสิ้น()
            '4:          กำลังตรวจนับ()
            Select Case Me._Status
                Case "0" ' new
                    'Me.cboDocumentType.Enabled = True
                    'Me.btnSave.Enabled = True
                    'Me.btnSearch.Enabled = True
                Case "-1"

                Case "1"
                    Me.cboDocumentType.Enabled = True
                    Me.chkSelect.Visible = True
                    Me.chkSelectAll.Visible = True
                    Me.btnCancel.Enabled = True
                    Me.btnUpdate.Enabled = True
                    Me.btnConfirm.Enabled = True
                    Me.btnPrint.Enabled = True
                    Me.btnSearch.Enabled = True
                    Me.cbPrint.Enabled = True
                Case "2"
                    Me.btnUpdate.Enabled = True
                    Me.btnCancel.Enabled = True
                    Me.btnShowItemList.Enabled = True
                    Me.btnPrint.Enabled = True
                    Me.cbPrint.Enabled = True
                Case "3"
                    Me.btnShowItemList.Enabled = True
                    Me.btnUpdate.Enabled = False
                    Me.btnPrint.Enabled = True
                    Me.cbPrint.Enabled = True
                Case "4"
                    Me.btnCancel.Enabled = True
                    Me.btnUpdate.Enabled = False
                    Me.btnShowItemList.Enabled = True
                    Me.btnPrint.Enabled = True
                    Me.cbPrint.Enabled = True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub getCustomer(ByVal pstr_Customer_Index)
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_Customer("Customer_Index", pstr_Customer_Index)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = Me._Customer_Index
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Name.Text = ""
                Me.txtCustomer_Id.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    Private Sub LoadDocumentType()

        Dim objDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim odtDocumentType As New DataTable

        Try
            objDocumentType.getDocumentType(4)
            odtDocumentType = objDocumentType.DataTable
            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = odtDocumentType
            End With
            cboDocumentType.SelectedIndex = 0
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdAdjustItem.Rows.Count = 0 Then
                ' MessageBox.Show("กรุณาค้นหารายการสินค้า", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                W_MSG_Information("กรุณาค้นหารายการสินค้า")
                Exit Sub
            End If
            CType(Me.grdAdjustItem.DataSource, DataTable).AcceptChanges()
            Dim drArrChk() As DataRow = CType(Me.grdAdjustItem.DataSource, DataTable).Select("chkSelect=1")

            If drArrChk.Length <= 0 Then
                W_MSG_Information("ไม่พบรายการที่จะทำการบันทึก กรุณาตรวจสอบข้อมูล")
                Exit Sub
            End If

            If Me.SaveData() = "PASS" Then
                Me.txtAdjust_No.ReadOnly = True
                Me.getAdjustHeader(_Adjust_Index)
                Me.getAdjustItemDetail(_Adjust_Index)
                btnPrint.Enabled = True
                cbPrint.Enabled = True
                btnExcel.Enabled = True
                Me.objStatus = enuOperation_Type.UPDATE
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If txtCustomer_Id.Text = "" Then
                MessageBox.Show("กรุณาเลือกลูกค้า", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (Me.chkByPick.Checked = True) Or (Me.chkCheckMove.Checked = True) Then
                Me.GetInventory_For_Adjust_Check_Move()
                Me.txtComment.Text = "Date : " & Me.dtTransaction_Date.Text & " Time : " & Me.dtTransaction_Time1.Text & " - " & Me.dtTransaction_Time2.Text
            Else
                Me.GetInventory_For_Adjust()
            End If

            'Me.ItemStatus_Description.Visible = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdAdjustItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdjustItem.CellContentClick

    End Sub

    Private Sub grdAdjustItem_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdAdjustItem.EditingControlShowing
        'Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
        '    Case "Count_1st_Total_Qty"           ' float
        '        Dim txtQty As TextBox = CType(e.Control, TextBox)
        '        AddHandler txtQty.KeyPress, AddressOf editingcontrol_Decimal_Keypress


        '    Case "Count_2nd_Total_Qty"           ' Decimal
        '        Dim txtWeight As TextBox = CType(e.Control, TextBox)
        '        AddHandler txtWeight.KeyPress, AddressOf editingcontrol_Decimal_Keypress

        '    Case "Count_3rd_Total_Qty"           ' Decimal
        '        Dim txtVolume As TextBox = CType(e.Control, TextBox)
        '        AddHandler txtVolume.KeyPress, AddressOf editingcontrol_Decimal_Keypress
        'End Select
    End Sub



    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oconfig_Report As New WMS_STD_OAW_Report.config_Report

        Try
            Dim frm As New WMS_STD_OAW_Report.frmReportViewerMain
            frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo("ADJ_PrintOut", "And Adjust_Index ='" & Me._Adjust_Index & "'")
            frm.ShowDialog()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value ****
            Dim tmpCustomer_Index As String = ""


            tmpCustomer_Index = frm.Customer_Index

            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
            If Me.txtCustomer_Id.Text = "" Or Me.txtCustomer_Name.Text = "" Then
                Me._Customer_Index = ""
            End If
            ' *********************
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getZone()

        If Me.cbStore.SelectedValue Is Nothing Then Exit Sub

        Dim objClassDB As New cls_KSL 'ms_Zone(ms_Zone.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getZone_Warehouse(Me.cbStore.SelectedValue)
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("Zone_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("Description") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("Description") = "Show All"
            End Select
            objDT.Rows.Add(drNew)
            With Me.cboZone
                .DisplayMember = "Description"
                .ValueMember = "Zone_Index"
                .DataSource = objDT
                .SelectedValue = -11
            End With


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub getRoom()
        Dim objClassDB As New ms_Room(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            If Me.cbStore.SelectedValue Is Nothing Then Exit Sub

            objClassDB.SelectByWareHouse(cbStore.SelectedValue.ToString)
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("Room_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("Description") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("Description") = "Show All"
            End Select
            objDT.Rows.Add(drNew)

            cbRoom.DisplayMember = "Description"
            cbRoom.ValueMember = "Room_Index"
            cbRoom.DataSource = objDT

            cbRoom.SelectedValue = -11

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("Warehouse_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("Description") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("Description") = "Show All"
            End Select
            objDT.Rows.Add(drNew)

            cbStore.DisplayMember = "Description"
            cbStore.ValueMember = "Warehouse_Index"
            cbStore.DataSource = objDT
            cbStore.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getProductType()
        Dim objClassDB As New ms_ProductType(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try

            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("ProductType_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("Description") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("Description") = "Show All"
            End Select
            objDT.Rows.Add(drNew)

            cbProductType.DisplayMember = "Description"
            cbProductType.ValueMember = "ProductType_Index"
            cbProductType.DataSource = objDT

            cbProductType.SelectedValue = "-11"
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getLocationType()
        Dim objClassDB As New ml_TNP
        Dim objDT As DataTable = New DataTable

        Try

            objClassDB.getLocationType("")
            objDT = objClassDB.DataTable

            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("LocationType_Index") = "-11"
            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    drNew("Description") = "แสดงทุกรายการ"
                Case enmLanguage.English
                    drNew("Description") = "Show All"
            End Select
            objDT.Rows.Add(drNew)

            cboLocationType.DisplayMember = "Description"
            cboLocationType.ValueMember = "LocationType_Index"
            cboLocationType.DataSource = objDT

            cboLocationType.SelectedValue = "-11"
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            Dim oconfig_Report As New config_Report_TNP
            Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString
            Try
                'SET report_PrintOut to WMS_STD_OAW_Report
                Dim frm As New WMS_STD_OAW_Report.frmReportViewerMain
                frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo2(Report_Name, "And Adjust_Index ='" & Me._Adjust_Index & "'", " order by Location_Alias asc ")
                frm.ShowDialog()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            Dim i As Integer = 0
            For i = 0 To grdAdjustItem.Rows.Count - 1
                grdAdjustItem.Rows(i).Cells("chkSelect").Value = chkSelectAll.Checked
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSeachSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSku.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") And (Not frm.Sku_Index Is Nothing) Then        'or
                txtSKU_ID.Tag = frm.Sku_Index
                txtSKU_ID.Text = frm.Sku_ID
                txtSku_Name.Text = frm.Sku_Des_th
                Me.chkSku.Checked = True
                '  getPackage_Sku()

            Else
                txtSKU_ID.Tag = ""
                txtSKU_ID.Text = ""
                txtSku_Name.Text = ""
                Me.chkSku.Checked = False
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Sub getPackage_Sku()
    '    Try
    '        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
    '        Dim objDT As DataTable = New DataTable
    '        Dim strSku_Index As String = ""
    '        Sku_Id = Me.txtSKU_ID.Text
    '        objClassDB.getSKU_Detail(Sku_Id)
    '        objDT = objClassDB.DataTable
    '        If objDT.Rows.Count > 0 Then
    '            Sku_Index = objDT.Rows(0).Item("Sku_index").ToString
    '            objClassDB.getSKU_Package(Sku_Index)
    '            objDT = objClassDB.DataTable

    '            cboPackage.DisplayMember = "Package"
    '            cboPackage.ValueMember = "Package_Index"
    '            cboPackage.DataSource = objDT
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub


    Private Sub txtFromRow_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub txtToRow_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbStore_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStore.SelectionChangeCommitted
        Try
            If cbStore.SelectedValue = -11 Then
                cbRoom.SelectedValue = "-11"
            Else
                Me.getRoom()
                Me.getZone()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAdjust_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigAdjust
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 4)
                    oFunction.SW_Language_Column(Me, Me.grdAdjustItem, 4)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            pnlExcel.Visible = True
            lblExCustomer.Text = txtCustomer_Name.Text
            cboMonth.SelectedIndex = (Date.Now.Month) - 1
            dtDocdate.Value = dtpAdjust_Date.Value

            Dim strWhere As String = " And Adjust_Index = '" & Me._Adjust_Index & "'"
            Dim objDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
            Dim objDT As New DataTable
            objDT = objDB.getAdjustItemExcel(strWhere)
            If objDT.Rows.Count > 0 Then
                grdAdjExcel.DataSource = objDT
                objDT = Nothing
            Else
            End If
            'VIEW_AdjustItem_Print_Excel
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancleExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancleExport.Click
        Try
            pnlExcel.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmExcel.Click
        Try
            Dim grdExport As New DataGridView
            grdExport = grdAdjExcel 'grdAdjustItem
            Dim oconfig_Report As New config_Report_Adjust
            'Dim frmReport As New frmReportViewerMain
            Dim ds_Operation As New DataSet

            'Dim oCrystal_report As New ReportDocument
            'oCrystal_report = oconfig_Report.GetReportInfo("ADJ_Main")
            'frmReport.CrystalReportViewer1.ReportSource = oCrystal_report

            'frmReport.ShowDialog()

            ExportToExcel(grdExport, lblExCustomer.Text, cboMonth.Text, dtDocdate.Text)
            pnlExcel.Visible = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Me.objStatus = enuOperation_Type.UPDATE
            Me.btnSave.Enabled = True

            Me.btnUpdate.Enabled = False
            Me.btnCancel.Enabled = False
            Me.btnShowItemList.Enabled = False
            Me.btnConfirm.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim objDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim NewAdjustView_Index As String = ""
        objDB.NewAdjustView_Index = Me._Adjust_Index
        Dim intStatus As Integer = Me._Status
        Select Case intStatus
            Case 2, 3
                'Dim frmpassword As New PopupEnterPassword
                'frmpassword.ShowDialog()
                'If frmpassword.passwordistrue = False Then
                '    Exit Sub
                'End If
        End Select
        Try
            If MessageBox.Show("คุณต้องการยกเลิกรายการเลขที่เอกสาร " & Me.txtAdjust_No.Text & "   ใช่หรือไม่ ", "ยืนยันการยกเลิกรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If objDB.Cancel_AdjustView(Me._Adjust_Index) = True Then
                    MessageBox.Show("ยกเลิกรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objDB = Nothing

                    'Me.getAdjustHeader(_Adjust_Index)
                    'Me.getAdjustItemDetail(_Adjust_Index)
                    'Exit Sub
                    Me.Close()
                Else
                    MessageBox.Show("ไม่สามารถยกเลิกรายการได้ ระบบทำงานผิดพลาด", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    objDB = Nothing
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShowItemList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowItemList.Click
        Try
            Dim frm As New frmAdjust_Confirm
            ' frm._ForReadOnly = False
            frm.Adjust_Index = Me._Adjust_Index
            frm.ShowDialog()
            Me.getAdjustHeader(_Adjust_Index)
            Me.getAdjustItemDetail(_Adjust_Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If W_MSG_Confirm("ท่านต้องการยืนยันเอกสารเลขที่ " & Me.txtAdjust_No.Text & " ใช่หรือไม่ ") = Windows.Forms.DialogResult.No Then Exit Sub

            Dim NewAdjustView_Index As String = Me._Adjust_Index
            Dim oAdjust As New AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            oAdjust.Update_Adjust_Status(NewAdjustView_Index, 4)
            W_MSG_Information_ByIndex("1")
            Me.getAdjustHeader(_Adjust_Index)
            Me.getAdjustItemDetail(_Adjust_Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged
        Try
            If Me.cboDocumentType.SelectedValue Is Nothing Then Exit Sub

            'Select Case cboDocumentType.SelectedValue
            '    Case _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
            '        Me.Location_Alias.Visible = False
            '        Me.ItemStatus_Description.Visible = False
            '    Case Else
            '        Me.Location_Alias.Visible = True
            '        Me.ItemStatus_Description.Visible = True
            'End Select

            If Me.grdAdjustItem.RowCount > 0 Then
                If W_MSG_Confirm("เปลี่ยน" & Me.lblDocument_Type.Text & " ต้องลบล้างรายการ " & Chr(13) & "คุณต้องการล้างรายการใช่หรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub
                CType(Me.grdAdjustItem.DataSource, DataTable).AcceptChanges()
                Dim odtTemp As New DataTable
                odtTemp = CType(grdAdjustItem.DataSource, DataTable).Clone
                Me.grdAdjustItem.DataSource = odtTemp
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cbStore_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStore.SelectedIndexChanged
        Try
            Me.getRoom()
            Me.getZone()
            If Me.cbStore.SelectedValue <> "-11" Then
                Me.chkWarehouse.Checked = True
            Else
                Me.chkWarehouse.Checked = False
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnLocationMulti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationMulti.Click
        Try
            Dim frm As New frmMultiSearch
            frm.ShowDialog()
            If frm.ArrDocument_No.Trim <> "" Then
                Me.GetInventory_For_Adjust(frm.ArrDocument_No.Trim)
                Me.chkSelectAll.Checked = False
                Me.chkSelectAll.Checked = True
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub chkCheckMove_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckMove.MouseLeave
        Try
            Me.Color_By_Move(False)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkCheckMove_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCheckMove.MouseMove
        Try
            Me.Color_By_Move(True)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Color_By_Move(ByVal Hover As Boolean)
        Try
            If Hover Then
                Me.dtTransaction_Date.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.chkCheckMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.chbLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                'Me.chkLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.chkZone.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Else
                Me.dtTransaction_Date.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.chkCheckMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.chbLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                'Me.chkLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.chkZone.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub chkByPick_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkByPick.MouseLeave
        Try
            Me.Color_By_Pick(False)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkByPick_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkByPick.MouseMove
        Try
            Me.Color_By_Pick(True)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Color_By_Pick(ByVal Hover As Boolean)
        Try
            If Hover Then
                Me.dtTransaction_Date.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                Me.chkByPick.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Else
                Me.dtTransaction_Date.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.dtTransaction_Time1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
                Me.chkByPick.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdAdjustItem.RowCount = 0 Then Exit Sub
            If W_MSG_Confirm("คุณต้องการลบรายการที่ " & Me.grdAdjustItem.CurrentRow.Cells("col_Seq").Value & " ใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                Dim AdjustItemLocation_Index As String = Me.grdAdjustItem.CurrentRow.Cells("AdjustItemLocation_Index").Value
                Dim objCon As New DBType_SQLServer
                If objCon.DBExeNonQuery(String.Format("DELETE tb_AdjustItemLocation where AdjustItemLocation_Index = '{0}'", AdjustItemLocation_Index)) > 0 Then
                    Me.grdAdjustItem.Rows.RemoveAt(grdAdjustItem.CurrentRow.Index)
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
      
    End Sub

    Private Sub btnProductType_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductType_Popup.Click
        Try
            Dim _popup As New frmProductType_Popup()
            _popup.ShowDialog()
            If (_popup.DialogResult = System.Windows.Forms.DialogResult.OK) Then
                Dim _strProductType As String = Me.txtProductType.Text.ToString().Trim()
                If (Not _strProductType = Nothing) Then
                    _strProductType = _popup.ProductType_Id + vbCrLf + _strProductType
                Else
                    _strProductType = _popup.ProductType_Id
                End If
                Me.txtProductType.Text = _strProductType
                Me.chkProductType2.Checked = True
            ElseIf (_popup.DialogResult = System.Windows.Forms.DialogResult.Cancel) Then
                Exit Sub
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class