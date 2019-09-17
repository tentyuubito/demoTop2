Imports System.Threading
Imports System.Globalization
Imports DevComponents.DotNetBar
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_INB_ASN
Imports WMS_STD_INB_ASN_Datalayer
Imports WMS_STD_INB_Receive
Imports WMS_STD_INB_Receive_Datalayer



Public Class frmMainR1
    Friend BasicDataType As String = ""  '#Set ข้อมูลพื้นฐาน = frm xxx
    Friend Shared frm_Icon As Icon
    Friend Shared Agesum As Double = 0

    Dim godtMenu_Permission As New DataTable
    Dim godtReport_Permission As New DataTable
    Dim FistTab As New Boolean

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            frm_Icon = Me.Icon
            Me.Text = "โปรแกรมบริหารคลังสินค้า - Kasco WMS Version : " + Me.ProductVersion
            FistTab = False
            Dim objse_menuItem_Setting As New se_menuItem_Setting(se_menuItem_Setting.enuOperation_Type.SEARCH)
            Dim objse_reportItem_Setting As New se_reportItem_Setting(se_reportItem_Setting.enuOperation_Type.SEARCH)
            Dim strGroupUser_Index As String = WV_GroupUser_Index
            'Load Data Menu
            objse_menuItem_Setting.SelectAllMenuByGroup(strGroupUser_Index)
            godtMenu_Permission = objse_menuItem_Setting.GetDataTable
            For Each oMenu As RibbonTabItem In Me.RibbonControl1.Items
                Enable_Menu(oMenu)
            Next


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "ENABLE"
    Private Sub Enable_Menu(ByVal poMenu As RibbonTabItem)
        Try
            Internal_Enable_MenuTAB(poMenu)
            If poMenu IsNot Nothing Then

                Dim panel As RibbonPanel = poMenu.Panel

                For Each panelControl As Control In panel.Controls

                    Dim ribbonBar As RibbonBar = TryCast(panelControl, RibbonBar)
                    If ribbonBar IsNot Nothing Then
                        For Each ribbonBarItem As BaseItem In ribbonBar.Items

                            Internal_Enable_Menuinbar(ribbonBarItem)
                        Next
                    End If
                Next
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Internal_Enable_MenuTAB(ByVal poMenu As RibbonTabItem)
        Try
            Dim odrMenu() As DataRow
            odrMenu = godtMenu_Permission.Select("menuItem_Name = '" & poMenu.Name & "'")
            If odrMenu.Length > 0 Then
                If odrMenu(0)("Enable").ToString = 1 Then
                    Internal_Enable_ControlInpanel(poMenu, True)
                    poMenu.Visible = True
                Else
                    Internal_Enable_ControlInpanel(poMenu, False)
                    poMenu.Visible = False
                End If
            Else
                Internal_Enable_ControlInpanel(poMenu, False)
                poMenu.Visible = False
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Internal_Enable_ControlInpanel(ByVal poMenu As RibbonTabItem, ByVal Visi As Boolean)
        Try
            If poMenu IsNot Nothing Then
                If FistTab = False And Visi = True Then
                    poMenu.Checked = True
                    FistTab = True
                End If
                Dim panel As RibbonPanel = poMenu.Panel
                For Each panelControl As Control In panel.Controls
                    panelControl.Visible = Visi
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Internal_Enable_Menuinbar(ByVal poMenu As BaseItem)
        Try
            Dim odrMenu() As DataRow
            odrMenu = godtMenu_Permission.Select("menuItem_Name = '" & poMenu.Name & "'")

            If odrMenu.Length > 0 Then
                If odrMenu(0)("Enable").ToString = 1 Then
                    'poMenu.Enabled = True
                    poMenu.Visible = True
                Else
                    'poMenu.Enabled = False
                    poMenu.Visible = False
                End If
            Else
                'poMenu.Enabled = False
                poMenu.Visible = False
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region


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
            frm.MdiParent = Me
            frm.Show()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub mnuJob_ASN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJob_ASN.Click
        CloseAllChildForm()
        Dim frm As New frmASNview
        frm.MdiParent = Me
        frm.Show()
    End Sub
End Class