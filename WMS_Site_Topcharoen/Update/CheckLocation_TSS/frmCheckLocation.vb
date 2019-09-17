Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports Microsoft.Office.Interop
Imports WMS_STD_Master
Imports System.Globalization
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_Formula.W_Module
Imports Microsoft.Office.Interop.Excel

Public Class frmCheckLocation
    ' ================================================================= 
    ' VIEW used in this module:
    '  - VIEW_LocationBalance
    '   
    ' ================================================================= 

    Private _DEFAULT_ImagePath As String = ""
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""
#Region " FORM LOAD "

#End Region

#Region " INITIAL CONTROL "
    Private Sub getWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT = New System.Data.DataTable
        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            cbStore.DisplayMember = "Description"
            cbStore.ValueMember = "Warehouse_Index"
            cbStore.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub GetZone()
        Dim objClassDB As New ms_Zone(ms_Zone.enuOperation_Type.SEARCH)
        Dim objDT = New System.Data.DataTable
        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable
            cboZone.DisplayMember = "Description"
            cboZone.ValueMember = "Zone_Index"
            cboZone.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub GetLocationType()
        Dim objClassDB As New ms_LocationType
        Dim objDT = New System.Data.DataTable
        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable
            cboLocationType.DisplayMember = "Description"
            cboLocationType.ValueMember = "LocationType_Index"
            cboLocationType.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
#End Region

#Region " BUTTON & COMBO EVENT "
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Sub Load_Grd_Sku(ByVal SQL As String)
        Try
            grdLocation.AutoGenerateColumns = False
            grdLocation.DataSource = Query(SQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region " GENERIC FUNCTIONS AND SUBS "
    Function SetCondition_forSearch() As String
        Try
            Dim strSQL As String = " Where 1=1 "
            If Me.chkSku.Checked = True Then
                strSQL &= " AND Current_SKU Like '%" & Me.txtSKU_ID.Text.Trim & "%' "
            End If

            If Me.chkStore.Checked = True Then
                strSQL &= " And Warehouse_Index = '" & Me.cbStore.SelectedValue.ToString & "' "
            End If

            If Me.chkZone.Checked = True Then
                strSQL &= " And Zone_Index = '" & Me.cboZone.SelectedValue.ToString & "' "
            End If

            If Me.ChkLoc.Checked = True Then
                strSQL &= " And Lock Like '%" & Me.txtLoc.Text.Trim & "%' "
            End If

            If Me.ChkLocation.Checked = True Then
                strSQL &= " And Location_Alias Like '%" & Me.txtLocation.Text.Trim & "%' "
            End If

            If Me.chkLocationType.Checked = True Then
                strSQL &= " And LocationType_Index = '" & Me.cboLocationType.SelectedValue.ToString & "' "
            End If

            If Me.ChkLevel.Checked = True Then
                strSQL &= " And Level " & Me.cboConditionLevel.Text.Trim & " '" & Me.txtLevel.Text.ToString & "'"
            End If

            If Me.chkWeight.Checked = True Then
                strSQL &= " AND Current_Weight " & Me.cboConditionWeight.Text.Trim & " '" & Me.txtWeight.Text.ToString & "'"
            End If

            If Me.chkPallet.Checked = True Then
                strSQL &= " AND Current_Pallet " & Me.cboConditionPallet.Text.Trim & " '" & Me.txtPallet.Text.ToString & "'"
            End If


            If Me.chkLevel2SkuNotMat.Checked = True Then
                strSQL &= " AND (Level = 2 AND  Location_Alias Like '%.%' " & _
                          " AND SUBSTRING(Location_Alias,0,len(Location_Alias)-1)  In ( " & _
                          "                                                                     SELECT Location_Sub FROM " & _
                          "                                                                     ( " & _
                          "                                                                     SELECT SUBSTRING(ms_Location.Location_Alias,0,len(ms_Location.Location_Alias)-1) as Location_Sub,(select dbo.fnGetSKU_LocationBalance(ms_Location.Location_Index)) as Current_SKU  FROM ms_Location " & _
                          "                                                                     where (dbo.ms_Location.[Level] = 2) AND (dbo.ms_Location.Location_Alias LIKE '%.%')  " & _
                          "                                                                     ) as xxx " & _
                          "                                                                     group by Location_Sub,Current_SKU " & _
                          "                                                                     having Count(Current_SKU) = 1 " & _
                          "                                                                ) )"
            End If

            If Me.chkLocationRefill.Checked = True Then
                strSQL &= " AND LocationType_Index = '2' AND Max_Pallet = 2 AND Current_Pallet  = 1 "
            End If

            Return strSQL
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function SetData() As String
        Try
            Dim strSQL As String = ""
            strSQL = " SELECT 0 as [chk], ROW_NUMBER() OVER(ORDER BY Location_Index ASC) AS RowsNum,* FROM (" & _
             "                          select  L.Row,L.LocationType_Index,L.Zone_Index,L.Warehouse_Index,Z.Description as Zone,SUBSTRING(L.Location_alias,0,2) as Lock,L.Location_Index,L.Location_Alias,L.Level,T.Description as [Type] " & _
             "                                  ,L.Max_Pallet,L.Max_Weight " & _
             "                                  ,Isnull((select Count(Tag_No) from ( " & _
             "                                           select LB.Location_Index,Tag_No from tb_LocationBalance LB " & _
             "                                           where LB.Qty_Bal-LB.ReserveQty > 0 and LB.Location_Index = L.Location_Index " & _
             "                                           Group by Tag_No,LB.Location_Index " & _
             "                                           Union   " & _
             "                                           select TSL.To_Location_Index as Location_Index,TSL.Tag_No from tb_TransferStatus TS " & _
             "                                           Inner Join tb_TransferStatusLocation TSL on TS.TransferStatus_Index = TSL.TransferStatus_Index    " & _
             "                                            where TS.Status = '1'  " & _
             "                                           ) as xxx " & _
             "                                           Group by xxx.Location_Index" & _
             "                                           Having xxx.Location_Index = L.Location_Index ),0) as Current_Pallet" & _
             "                                  ,isnull((select sum(isnull(LB.Weight_Bal,0))-sum(isnull(LB.ReserveWeight,0)) from tb_LocationBalance LB where LB.Qty_Bal > 0 and LB.Location_Index = L.Location_Index),0) as Current_Weight" & _
             "                                  ,isnull((select sum(isnull(LB.Qty_Bal,0)) - sum(isnull(LB.ReserveQty,0)) from tb_LocationBalance LB where LB.Qty_Bal > 0 and LB.Location_Index = L.Location_Index),0) as Current_Qty" & _
             "                                  ,(select dbo.fnGetSKU_LocationBalance(L.Location_Index)) as Current_SKU" & _
             "                          from ms_Location L inner join" & _
             "                          ms_Zone Z on L.Zone_Index = Z.Zone_Index inner join" & _
             "	                        ms_LocationType T on T.LocationType_Index = L.LocationType_Index ) AS Total_L "
            Return strSQL
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Query(ByVal SQL As String) As System.Data.DataTable
        Try
            Dim objDB As New SQLCommands
            Dim DT As New System.Data.DataTable
            objDB.SQLComand(SQL)
            DT = objDB.DataTable
            Return DT
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport_NoLocation.Click
        Try
            If (Me.grdLocation.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Dim ds As New DataSet

            Dim objExport As New Export_Excel_KC

            ds.Tables.Add(objExport.DataGridViewToDataTable(Me.grdLocation))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            objExport.export(ds, Me.Text)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

        'Try
        '    Dim grdExport As New DataGridView
        '    grdExport = grdLocation
        '    ExportToExcel(grdExport)
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try
    End Sub

    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then
                txtSKU_ID.Tag = frm.Sku_Index
                txtSKU_ID.Text = frm.Sku_ID
                txtSku_Name.Text = frm.Sku_Des_eng
            Else
                txtSKU_ID.Tag = ""
                txtSKU_ID.Text = ""
                txtSku_Name.Text = ""
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub frmCheckStock_By_Condition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            Dim objLang As New W_Language
            'Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
            getWarehouse()
            GetZone()
            GetLocationType()
            Me.cboConditionLevel.SelectedItem = 0
            Me.cboConditionPallet.SelectedItem = 0
            Me.cboConditionWeight.SelectedItem = 0

            'Fix KSL
            'Me.chkLocationRefill.Visible = False
            Me.chkLevel2SkuNotMat.Visible = False
            Me.btnRefill.Visible = False
            Me.Col_Chk.Visible = False
            Me.ChkAll.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim StrOrderby As String = ""

            If Me.chkLevel2SkuNotMat.Checked = True Then
                StrOrderby = " Order by Lock,Row,Location_Alias "
            End If

            Load_Grd_Sku(SetData() & "  " & SetCondition_forSearch() & StrOrderby)
            Me.grdLocation.Columns("Col_Chk").ReadOnly = False

            Me.lblRows.Text = Me.lblRows.Tag.ToString & FormatNumber((Me.grdLocation.RowCount), 0).ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ChkLevel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLevel.CheckedChanged
        Try
            Me.cboConditionLevel.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkWeight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWeight.CheckedChanged
        Try
            Me.cboConditionWeight.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkPallet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPallet.CheckedChanged
        Try
            Me.cboConditionPallet.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkLocationRefill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLocationRefill.CheckedChanged
        Try
            If Me.chkLocationRefill.Checked = True Then
                Me.btnRefill.Enabled = True
            Else
                Me.btnRefill.Enabled = False
            End If
        Catch ex As Exception
            W_MSG_Information(ex.ToString)
        End Try
    End Sub

    Private Sub ChkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAll.CheckedChanged
        Try
            Dim Dttmp As New System.Data.DataTable
            Dttmp = Me.grdLocation.DataSource
            For Each Dr As DataRow In Dttmp.Rows
                Dr("chk") = Me.ChkAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Information(ex.ToString)
        End Try
    End Sub

    'Private Sub btnRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefill.Click
    '    Try
    '        Dim DrSelect() As DataRow
    '        Dim DtTmp As New system.Data.DataTable
    '        DtTmp = CType(Me.grdLocation.DataSource, DataTable)
    '        DtTmp.AcceptChanges()
    '        DrSelect = DtTmp.Select(" chk = 1 AND LocationType_Index = '3' AND (Current_Pallet < Max_Pallet) ")
    '        If DrSelect.Length > 0 Then
    '            Dim objRefill As New List(Of ItemRefill)
    '            Dim objitemRefill As New ItemRefill
    '            Dim iRow As Integer = 0
    '            Dim objSKU As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
    '            For Each Dr As DataRow In DrSelect
    '                If Dr("Location_Index").ToString <> "" And Dr("Current_SKU").ToString Then
    '                    objitemRefill = New ItemRefill
    '                    With objitemRefill
    '                        .Location_Index = Dr("Location_Index").ToString
    '                        .Sku_Index = objSKU.getSKU_Index(Dr("Current_SKU").ToString)
    '                    End With
    '                    objRefill.Add(objitemRefill)
    '                Else
    '                    Continue For
    '                End If
    '            Next
    '            Dim objArRefill As ItemRefill()
    '            objArRefill = objRefill.ToArray
    '            If objArRefill.Length > 0 Then
    '                Dim objSave As New Cl_Refill
    '                Dim objResPonse As New RefillRespone
    '                objResPonse = objSave.Refiil_Location(objArRefill)
    '                Select Case objResPonse.ReturnStatus
    '                    Case "S"
    '                        W_MSG_Information("ทำรายการเสร็จสิ้น")
    '                        btnSearch_Click(sender, e)
    '                        Exit Sub
    '                    Case "E"
    '                        W_MSG_Information(objResPonse.StrError)
    '                        Exit Sub
    '                End Select
    '            End If
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Information(ex.ToString)
    '    End Try
    'End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.grdLocation.RowCount = 0 Then Exit Sub
            Dim objLocation As New frmLocation
            With objLocation
                .location_index = Me.grdLocation.CurrentRow.Cells("col_Location_Index").Value.ToString
                .appStatus = 1 'edit
                .ShowDialog()
                'Me.btnSearch_Click(sender, e)
                'fn_search()
                'ShowgrdLocationType()
            End With
        Catch ex As Exception
            Throw ex
        End Try

        'Try
        '    Dim frm As New frmMainLocation
        '    frm.Icon = Me.Icon
        '    frm.ShowDialog()
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try

    End Sub
End Class