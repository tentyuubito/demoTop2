Imports C1.Win.C1Ribbon
Imports System.Threading
Imports System.Globalization

'MASTER
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_VRWareHouse
'INBOUND
Imports WMS_STD_INB_ASN
Imports WMS_STD_INB_ASN_Datalayer
Imports WMS_STD_INB_Barcode
Imports WMS_STD_INB_PO
Imports WMS_STD_INB_PO_Datalayer
Imports WMS_STD_INB_Receive
Imports WMS_STD_INB_Receive_Datalayer
'OUTBOUND
Imports WMS_STD_OUTB
Imports WMS_STD_OUTB_Datalayer
Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_OUTB_Reserv_Datalayer
Imports WMS_STD_OUTB_SO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_OUTB_Transport
Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_OUTB_WithDraw
Imports WMS_STD_OUTB_WithDraw_Datalayer
'TRANSFER
Imports WMS_STD_TMM_BorrowReturn
Imports WMS_STD_TMM_BorrowReturn_Datalayer
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_TMM_TransferCode
Imports WMS_STD_TMM_TransferOwner
Imports WMS_STD_TMM_TransferStatus
'OTHER
Imports WMS_STD_OAW_Adjust
Imports WMS_STD_OAW_Adjust_Datalayer
Imports WMS_STD_OAW_INVENTORY
Imports WMS_STD_OAW_INVENTORY_Datalayer
Imports WMS_STD_OAW_Invoice
Imports WMS_STD_OAW_Invoice_Datalayer
Imports WMS_STD_OAW_Packing
Imports WMS_STD_OAW_Packing_Datalayer
Imports WMS_STD_OAW_ProductionOrder
Imports WMS_STD_OAW_ProductionOrder_Datalayer

'REPORT
Imports WMS_STD_Report

Public Class frmMainC1
    Public Description_Th As String = ""
    Public Description_Eng As String = ""

    Dim godtMenu_Permission As New DataTable
    'Dim godtReport_Permission As New DataTable
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'CreateAppMenu()
        'CreateHomeTab()
        Try
            Me.CloseAllChildForm()
            Dim obj As New W_Language
            obj.SwitchLanguage(Me)

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            '-----------------------------------------------------------------
            Select Case WV_Language
                Case enmLanguage.Thai
                    Me.Text = Me.Description_Th & Me.ProductVersion
                Case enmLanguage.English
                    Me.Text = Me.Description_Eng & Me.ProductVersion
            End Select
            '-----------------------------------------------------------------
            Me.C1Ribbon1.Minimized = True

            'FistTab = False

            Dim objse_menuItem_Setting As New se_menuItem_Setting(se_menuItem_Setting.enuOperation_Type.SEARCH)
            Dim strGroupUser_Index As String = WV_GroupUser_Index
            'Load Data Menu
            objse_menuItem_Setting.SelectAllMenuByGroup(strGroupUser_Index)
            godtMenu_Permission = objse_menuItem_Setting.GetDataTable

            'Loop tab Cotrol Swith ภาษา
            For Each oMenu As C1.Win.C1Ribbon.RibbonTab In Me.C1Ribbon1.Tabs
                Enable_Menu_Tab(oMenu)
            Next

            If WV_UserName = "dhong" Then
            Else
                If IsDisplayAlertEXPIRED() Or IsDisplayAlertMAX() Or IsDisplayAlertMIN() Then
                    Me.getCheckAge()
                End If
            End If


            Me.Text &= " [ Online : id " & WV_UserName & " ,name : " & WV_UserFullName & " ] "
            Me.lblDepartMentDesc.Text = WV_Department_Des

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getCheckAge()
        Try
            Dim ofrmExpire As New frmExpire
            If ofrmExpire.CheckExpiredItem Then
                ofrmExpire.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsDisplayAlertMAX() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Try
            Return objCustomSetting.getConfig_Key_USE("USE_ALERT_MAX")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function IsDisplayAlertMIN() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Try
            Return objCustomSetting.getConfig_Key_USE("USE_ALERT_MIN")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function IsDisplayAlertEXPIRED() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Try
            Return objCustomSetting.getConfig_Key_USE("USE_ALERT_EXPIRED")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "   Menu Control   "
    Private Sub Enable_Menu_Tab(ByVal poMenu As C1.Win.C1Ribbon.RibbonTab)
        Try
            'Enable Ribbon In Tab
            Me.Internal_Enable_Menu(poMenu)
            'Enable Ribbon In Group
            If poMenu IsNot Nothing Then
                For Each objRibbonGroup As C1.Win.C1Ribbon.RibbonGroup In poMenu.Groups
                    For Each objRibbonButton As C1.Win.C1Ribbon.RibbonItem In objRibbonGroup.Items
                        'Main Control In Group
                        Me.Internal_Enable_Menu(objRibbonButton)
                        Select Case objRibbonButton.GetType.FullName.ToUpper
                            Case "C1.Win.C1Ribbon.RibbonMenu".ToUpper
                                'Control Item
                                Dim objRibbonControl As C1.Win.C1Ribbon.RibbonMenu = TryCast(objRibbonButton, C1.Win.C1Ribbon.RibbonMenu)
                                If objRibbonControl IsNot Nothing Then
                                    For Each ribbonBarItem As C1.Win.C1Ribbon.RibbonButton In objRibbonControl.Items
                                        Me.Internal_Enable_Menu(ribbonBarItem)
                                    Next
                                End If
                            Case Else
                                Continue For
                        End Select
                    Next
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Internal_Enable_Menu(ByVal poMenu As Object)
        Try
            '------------------------------------------------------
            '       Me.AutoInsertMenuToDB(poMenu) ' Enable Auto Insert
            '------------------------------------------------------
            Select Case poMenu.GetType.FullName.ToUpper
                Case "C1.Win.C1Ribbon.RibbonSeparator".ToUpper
                    Exit Sub
            End Select
            '------------------------------------------------------
            Dim odrMenu() As DataRow
            odrMenu = godtMenu_Permission.Select("menuItem_Name = '" & poMenu.Name & "'")
            If odrMenu.Length > 0 Then
                If odrMenu(0)("Enable").ToString = "1" Then
                    poMenu.Visible = True
                    Select Case WV_Language
                        Case enmLanguage.Thai
                            poMenu.Text = odrMenu(0)("menuItem_Th").ToString
                        Case enmLanguage.English
                            poMenu.Text = odrMenu(0)("menuItem_Eng").ToString
                    End Select
                Else
                    poMenu.Visible = False
                End If
            Else
                poMenu.Visible = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Dim iCount As Integer = 0
    Private Sub AutoInsertMenuToDB(ByVal poMenu As Object)
        'Auto Add Menu to DB
        Try
            '------------------------------------------------------
            Select Case poMenu.GetType.FullName.ToUpper
                Case "C1.Win.C1Ribbon.RibbonSeparator".ToUpper
                    Exit Sub
            End Select
            '------------------------------------------------------
            Dim oSysAutoIndex As New WMS_STD_Formula.Sy_AutoNumber
            Dim strIndex As String
            Dim ose_menuItem As New se_menuItem(se_menuItem.enuOperation_Type.ADDNEW)
            strIndex = oSysAutoIndex.getSys_Value("menuItem_index")

            With ose_menuItem
                iCount += 1
                .SaveData(strIndex, iCount, poMenu.Name.ToString, poMenu.Text, poMenu.Text)
            End With

        Catch ex As Exception
            Throw ex
        End Try


    End Sub


#End Region

#Region "   TEST RIBBON   "

    Private Sub CreateAppMenu()
        ' **** create the Application menu buttons
        C1Ribbon1.ApplicationMenu.BottomPaneItems.Add(New RibbonButton("Options"))
        C1Ribbon1.ApplicationMenu.BottomPaneItems.Add(New RibbonButton("Exit"))

        ' *** create the controls for the left pane
        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(New RibbonButton("&New"))
        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(New RibbonButton("&Open"))
        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(New RibbonButton("&Save"))

        Dim split As RibbonSplitButton = New RibbonSplitButton("Save &As")
        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(split)
        split.Items.Add(New RibbonButton("Word Document"))
        split.Items.Add(New RibbonButton("D2H project file"))

        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(New RibbonSeparator())
        C1Ribbon1.ApplicationMenu.LeftPaneItems.Add(New RibbonButton("&Print"))

        CreateRecentDocumentList()
    End Sub

    Private Sub CreateRecentDocumentList()

        ' **** create the recently used document list (controls in the right pane)

        ' first create a header and make sure it's not selectable
        Dim listItem As RibbonListItem = New RibbonListItem(New RibbonLabel("Recent Documents"))
        listItem.AllowSelection = False
        C1Ribbon1.ApplicationMenu.RightPaneItems.Add(listItem)
        C1Ribbon1.ApplicationMenu.RightPaneItems.Add(New RibbonListItem(New RibbonSeparator()))

        ' create the recently used document list
        Dim recentDocuments() As String = {"Document 1", "Document 2", "Document 3"}
        Dim documentName As String

        For Each documentName In recentDocuments
            ' each item consists of the name of the document and a push pin
            listItem = New RibbonListItem()
            listItem.Items.Add(New RibbonLabel(documentName))

            ' allow the button to be selectable so we can toggle it
            Dim pin As RibbonToggleButton = New RibbonToggleButton()
            pin.SmallImage = My.Resources.unpinned
            pin.AllowSelection = True
            listItem.Items.Add(pin)
            AddHandler pin.Click, AddressOf pinButton_Click

            C1Ribbon1.ApplicationMenu.RightPaneItems.Add(listItem)
        Next
    End Sub

    Private Sub pinButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim pin As RibbonToggleButton = CType(sender, RibbonToggleButton)
        If pin.Pressed Then
            pin.SmallImage = My.Resources.pinned
        Else
            pin.SmallImage = My.Resources.unpinned
        End If
    End Sub

    Private Sub CreateHomeTab()
        C1Ribbon1.Tabs.Clear()

        Dim homeTab As RibbonTab = New RibbonTab("Home")
        Dim clipboardGroup As RibbonGroup = New RibbonGroup("Clipboard")
        Dim pasteButton As RibbonButton = New RibbonButton("Paste", Nothing, My.Resources.Paste)
        pasteButton.TextImageRelation = TextImageRelation.ImageAboveText

        clipboardGroup.Items.Add(pasteButton)
        homeTab.Groups.Add(clipboardGroup)
        C1Ribbon1.Tabs.Add(homeTab)
    End Sub
#End Region

#Region "   งานรับสินค้า   "
    Private Sub CloseAllChildForm()
        Try
            For Each cf As Form In Me.MdiChildren
                cf.Close()
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub mnuJob_Order_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Order.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmOrderView
            frm._ReceiveType = 0
            frm.showBtn_cancel = True
            frm.MdiParent = Me
            '    pnlImg.Visible = False
            frm.Show()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            '  pnlImg.Visible = True
        End Try
    End Sub
    Private Sub mnuJob_ASN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_ASN.Click
        Try
            CloseAllChildForm()
            'Dim frm As New frmASNview
            Dim frm As New frmImportsTextSO
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_Production_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Production.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmProductionOrderView

            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   งานเบิกสินค้า   "
    Private Sub mnuJob_SalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_SalesOrder.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmSOView_V4
            'frm.Document_Group_Name = "SALE" 'KSL
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_SalesOrder2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_SalesOrder2.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmReportQueryToolsSF 'frmSOView_V4
            frm.ShowDialog()
            'frm.Document_Group_Name = "PICK" 'KSL
            'frm.MdiParent = Me
            'frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_SalesOrder3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_SalesOrder3.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmSOView_V4
            'frm.Document_Group_Name = "BOM" 'KSL
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_Withdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Withdraw.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmWithdrawAssetView_V4
            frm._withdrawType = 0
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   งานโอนย้ายสินค้า   "

    Private Sub mnuJob_ItemStatusTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_ItemStatusTransfer.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmAssetTransferView_V2
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mmuTransferOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmuTransferOwner.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmTransferOwner_View_V2
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_ItemCodeTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_ItemCodeTransfer.Click
        Try
            'CloseAllChildForm()
            'Dim frm As New frmTransferCodeView
            'frm.MdiParent = Me
            'frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   งานซื้อขาย   "
    Private Sub mnuOrdering_PurchaseOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrdering_PurchaseOrder.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmPOView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuReports_ManagementReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReports_ManagementReport.Click
        Try
            Dim frm As New frmMainReport
            frm.Report_Name = ""
            frm.Report_Condition = ""
            frm.DisplayTreeView = True
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region "   งานคลังสินค้า   "
    Private Sub mnuWMS_VRWareHouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_VRWareHouse.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmVRWH_DashBoard
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuWMS_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_Location.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmCheckLocation
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuWMS_SpaceUtilization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_SpaceUtilization.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmSpaceUtilization_VR
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuWMS_CheckStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_CheckStock.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmAdjustView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuWMS_Reserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_Reserve.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmReserveView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuWMS_BorrowDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_BorrowDoc.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmBorrowView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuWMS_BorrowReturnDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_BorrowReturnDoc.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmBorrowReturnView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuWMS_BarcodePC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWMS_BarcodePC.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmPrintBarcode
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuDayEnd_Recalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDayEnd_Recalculate.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmReCalculate
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuDayEnd_NewRecal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDayEnd_NewRecal.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmNewLastRecal
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "    งานสินค้าคงคลัง  "
    Private Sub mnuInventory_ExpiredWarning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_ExpiredWarning.Click
        Try
            Dim frm As New frmExpire
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub
    Private Sub mnuInventory_StockCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_StockCard.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmStockCard_Update
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuInventory_Basic_StockOnHand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_Basic_StockOnHand.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmCheckStock_By_Condition_Update
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuInventory_StockByReceivedOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_StockByReceivedOrder.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmCheckStock_By_Order
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuInventory_StockAgeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_StockAgeCheck.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmCheckStock_By_Age
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuInventory_GraphStockAgeAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_GraphStockAgeAnalysis.Click
        Try
            Dim frm As New frmStockAgingAnalysis_VR
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuInventory_Tracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_Tracking.Click
        Try
            Dim frm As New WMS_STD_OAW_INVENTORY.frmTracking
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub
    Private Sub mnuInventory_StockBlockingControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory_StockBlockingControl.Click
        Try
            Dim frm As New frmMainWithdraw_Block_CustomerLot
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub
#End Region

#Region "   งานบัญชีการเงิน   "
    Private Sub mnuAccount_PaymentHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccount_PaymentHistory.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmPaymentHistory
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuAccount_Invoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccount_Invoice.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmInvoiceView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuAccount_ServiceRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccount_ServiceRate.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmServiceCharge_Config
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   ทะเบียน   "
    Private Sub mnuRegister_SKU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegister_SKU.Click
        Try
            Dim frm As New frmProduct_SKU
            frm.Text = mnuRegister_SKU.Text
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRegister_Customer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegister_Customer.Click
        Try
            Dim frm As New frmCustomer
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuCustomer_Shipping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomer_Shipping.Click
        Try
            'Dim frm As New frmCustomer_Shipping
            Dim frm As New frmCustomer_ShippingV2
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomer_Shipping_Location.Click
        Try
            Dim frm As New frmMainCustomer_Shipping_Location_V3
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRegister_Supplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegister_Supplier.Click
        Try
            Dim frm As New frmSupplier
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRegister_StaffProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegister_StaffProfile.Click
        Try
            Dim frm As New frmMainEmployee
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRegister_VehicleProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStickerBarcode.Click
        Try
            Dim frm As New frmStickerBarcode()
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   การจัดการระบบ   "
    Private Sub mnuSetting_Language_Thai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_Language_Thai.Click
        Try
            WV_Language = enmLanguage.Thai
            CloseAllChildForm()
            'Loop tab Cotrol Swith ภาษา
            Me.Form1_Load(sender, e)
            'For Each oMenu As C1.Win.C1Ribbon.RibbonTab In Me.C1Ribbon1.Tabs
            '    Enable_Menu_Tab(oMenu)
            'Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuSetting_Language_Eng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_Language_Eng.Click
        Try
            WV_Language = enmLanguage.English
            CloseAllChildForm()
            'Loop tab Cotrol Swith ภาษา
            Me.Form1_Load(sender, e)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuSetting_User_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_User.Click
        Try
            Dim frm As New frmMainUserManagement_Encode
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuSetting_UserGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_UserGroup.Click
        Try
            Dim frm As New frmMainGroupUser
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuSetting_Permission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_Permission.Click
        Try
            Dim frm As New frmMainAuthority
            frm.ShowDialog()
            Me.Form1_Load(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuSetting_ChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetting_ChangePassword.Click
        Try
            Dim frm As New PopupChangePassword
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   ข้อมูลพื้นฐาน   "
    Private Sub mnuBasicData_DocType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_DocType.Click
        Try
            Dim frm As New frmMainDocumentTypeV5
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_CustomerType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_CustomerType.Click
        Try
            Dim frm As New frmMainCustomerT
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_SupplierType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_SupplierType.Click
        Try
            Dim frm As New frmMainSupplierType
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Department_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Department.Click
        Try
            Dim frm As New frmMainDepartment
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Title_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Title.Click
        Try
            Dim frm As New frmMainTitle
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   ข้อมูลเกี่ยวกับสินค้า   "
    Private Sub mnuBasicData_Product_ProductType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_ProductType.Click
        Try
            Dim frm As New frmMainProductType
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Product_Pallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Pallet.Click
        Try
            Dim frm As New frmMainPalletType
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_ItemStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_ItemStatus.Click
        Try
            Dim frm As New frmMainItemStatus
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_ItemDefinition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_ItemDefinition.Click
        Try
            Dim frm As New frmMainItemDefinition
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_Package_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Package.Click
        Try
            Dim frm As New frmMainPackage
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_Size_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Size.Click
        Try
            Dim frm As New frmMainSize
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_Brand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Brand.Click
        Try

            Dim frm As New frmMainBrand
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_Model_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Model.Click
        Try
            Dim frm As New frmMainModel
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Product_Color_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Product_Color.Click
        Try
            Dim frm As New frmMainColour
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Plant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Plant.Click
        Try
            'Dim frm As New frmMainPlant
            Dim frm As New frmMainConfig_Picking_KSL
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuBasicData_ProductLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_ProductLine.Click
        Try
            Dim frm As New frmMainProductionLine
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   ข้อมูลเกียวกับสถานที่   "
    Private Sub mnuBasicData_Location_Zone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Zone.Click
        Try
            Dim frm As New frmMainZone
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_LocationType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_LocationType.Click
        Try
            Dim frm As New frmMainLocationType
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Location.Click

        Try
            Dim frm As New frmMainLocation
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_Country_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Country.Click
        Try
            Dim frm As New frmMainCountry
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_Province_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Province.Click

        Try
            Dim frm As New frmMainProvince_1
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_District_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_District.Click

        Try
            Dim frm As New frmMainDistrict
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub mnuBasicData_Location_Town_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Town.Click

        Try
            Dim frm As New frmMainTown
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub mnuBasicData_Location_Port_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Port.Click

        Try
            Dim frm As New frmMainPort
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Location_Terminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Terminal.Click
        Try
            Dim frm As New frmMainTerminal
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuMappingWH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMappingWH.Click
        Try
            Dim frm As New frmMappingWareHouse
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Location_Pallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Pallet.Click
        Try
            Dim frm As New frmMainPalletTypeLocation
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Location_WareHouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_WareHouse.Click
        Try
            Dim frm As New frmMainWarehouse
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuBasicData_Location_Room_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Location_Room.Click
        Try
            Dim frm As New frmMainRoom
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   ข้อมูลเกี่ยวกับคิดเงิน   "
    Private Sub mnuBasicData_Account_BillingType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Account_BillingType.Click

        Try
            Dim frm As New frmMainBillingType
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Account_Currency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Account_Currency.Click
        Try
            Dim frm As New frmMainCurrency
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   ข้อมูลเกี่ยวกับการจัดส่ง   "
    Private Sub mnuBasicData_Transport_Carrier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Transport_Carrier.Click
        Try
            Dim frm As New frmMainCarrier
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub mnuBasicData_Transport_Container_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Transport_Container.Click
        Try
            Dim frm As New frmMainContainer
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Transport_VehicleType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Transport_VehicleType.Click
        Try
            Dim frm As New frmMainVehicleType
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Destributioncenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Destributioncenter.Click
        Try
            Dim frm As New frmMainDistributionCenter
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_Route_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_Route.Click
        Try
            Dim frm As New frmMainRoute
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuBasicData_TransportRegion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBasicData_TransportRegion.Click
        Try
            Dim frm As New frmMainTransportRegion
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuMainEmployee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainEmployee.Click
        Try
            Dim frm As New WMS_STD_OUTB_Transport.frmDriver_Popup
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   เชื่อมโยงข้อมูลนอกระบบ   "
    Private Sub mnuJob_Import_Order_Doc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_Order_Doc.Click
        Try
            Dim ofrm As New Import_WMS_Order.frmMainImportOrder
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_Import_Order_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_Order.Click
        Try
            Dim ofrm As New frmImport_ASN_Excel(frmImport_ASN_Excel.enuOperation_Type.ASN)
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_SyncData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_SyncData.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmOnline_SyncData
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_Import_Withdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImport_SO.Click
        Try
            Dim ofrm As New Import_WMS_SO_EXCEL.frmImport_Excel_SO_New 'frmImport_SO(frmImport_SO.enuOperation_Type.SO)
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_Import_XML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_XML.Click
        Try
            Dim ofrm As New frmImport_OrderXML
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuJob_Import_Order_StockBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_Order_StockBalance.Click
        Try
            Dim ofrm As New frmImport_Order_StockBalance
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub mnuJob_Import_Order_Doc_SN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_Order_Doc_SN.Click
        Try
            Dim ofrm As New Import_WMS_Order.frmMainImportOrderSerial
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "  ใบประกอบสินค้า "
    Private Sub mnuJob_Service_RePackage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Service_RePackage.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmPackingView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   งานขนส่งสินค้า   "

    Private Sub mnuJob_DeliveryOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_DeliveryOrder.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmTOView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_GROrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_GROrder.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmGROView
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_TruckLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_TruckLoad.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmTransportBillLoad_Update(frmTransportBillLoad_Update.Manifest_Mode.ADD)
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_DeliveryConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_DeliveryConfirm.Click
        Try
            'CloseAllChildForm()
            'Dim frm As New frmTransportSO_POD 'frmTransportPOD
            'frm.MdiParent = Me
            'frm.Show()

            CloseAllChildForm()
            Dim frm As New frmTransportSO_POD_Update_LP 'frmTransportPOD
            'frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_TransportManifest_Doc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_TransportManifest_Doc.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmTransportManifestView_Update
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_TransportManifest_Register_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_TransportManifest_Register.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmTruckQueue_Register  '"""
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub mnuJob_TransportCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_TransportCharge.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmMainServiceRateTransport
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   Report Customize   "
    Private Sub mnuRpt_Withdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRpt_Withdraw_KSL.Click
        Try
            Dim frm As New frmSreachWithdraw_KSLV2
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuRpt_PO_KSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRpt_PO_KSL.Click
        Try
            Dim frm As New frmSreachPO_KSL
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRpt_SO_KSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRpt_SO_KSL.Click
        Try
            Dim frm As New frmSreachSO_KSL
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRPT_KSL_Picking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRPT_KSL_Picking.Click
        Try
            Dim frm As New frmSreachPicking_KSL
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRPT_Adjust_KSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRPT_Adjust_KSL.Click
        Try
            Dim frm As New frmSreachAdjust_KSL
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRPT_DeadStrok_KSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRPT_DeadStrok_KSL.Click
        Try
            Dim frm As New frmSreachNonMoving_KSL
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub mnuRPT_Audit_log_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRPT_Audit_log.Click
        Try
            Dim frm As New frmSreachAudit_log
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

    'Private Sub mnuBackupRestoreDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBackupRestoreDB.Click
    '    Try
    '        CloseAllChildForm()
    '        Dim frm As New frmBackup_Restore_DB
    '        frm.MdiParent = Me
    '        frm.Show()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


    Private Sub frmMainC1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Dim obj As New UserSession
            obj.UpdateSession()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPackingDrop.Click
        Try
            CloseAllChildForm()
            Dim frmSelect As New frmSelect_Packing
            frmSelect.ShowDialog()

            If frmSelect.DialogResult <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Select Case frmSelect.PackingType
                Case frmSelect_Packing.ePackingType.CL
                    Dim frm As New frmMainPackingDrop(frmMainPackingDrop.ePackingType.CL)
                    'frm.MdiParent = Me
                    frm.ShowDialog()

                Case frmSelect_Packing.ePackingType.SL
                    Dim frm As New frmMainPackingDrop(frmMainPackingDrop.ePackingType.SL)
                    'frm.MdiParent = Me
                    frm.ShowDialog()

                Case Else
                    Exit Sub
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuRegister_VehicleProfile_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegister_VehicleProfile.Click
        Try
            Dim frm As New frmMainVehicle
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJobReprintTAG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJobReprintTAG.Click
        Try
            'Dim frm As New frmPrintTAG_Repeat
            Dim frm As New frmMainTag_Reprint
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuPurchaseOrder_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseOrder_Request.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmPR_View()
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuQueryTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQueryTools.Click
        Try
            Dim _strUrl As String = New config_CustomSetting().getConfig_Key_DEFUALT("DEFAULT_REPORT_TOOLS")
            System.Diagnostics.Process.Start(_strUrl)
            'Dim frm As New frmReportTools()
            'frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    


   
    Private Sub mnuPrintInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintInvoice.Click
        Try
            Dim frm As New frmInvoice_Print
            frm.btnGenInvoice.Visible = False
            frm.btnCancelInvoice.Visible = False
            frm.chkInvoiceNotNull.Checked = True
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuBackupRestoreDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuCreateInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCreateInvoice.Click
        Try
            Dim frm As New frmInvoice_Print
            frm.chkInvoiceNull.Checked = True
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_InterfaceST_Log_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_InterfaceST_Log.Click
        Try
            'CloseAllChildForm()
            Dim frm As New frmInterface_log
            frm.Icon = Me.Icon
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_Order_admin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Order_admin.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmOrderView
            frm._ReceiveType = 0
            frm.showBtn_cancel = True
            frm.MdiParent = Me
            '    pnlImg.Visible = False
            frm.Show()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            '  pnlImg.Visible = True
        End Try
    End Sub

    Private Sub mnuJob_Import_PO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_Import_PO.Click
        Try
            Dim ofrm As New frmImportSO
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()

            'Dim ofrm As New frmImport_PO_TCR
            'ofrm.Icon = Me.Icon
            'ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuPackingBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPackingBox.Click
        Try
            CloseAllChildForm()
            Dim frm As New frmMainPackingBox
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class
