Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports WMS_STD_Master
Public Class frmLocation
    Private _appStatus As Integer
    Private _location_index As String

    Private _validate As New ValidateCharacter
    Public Property location_index() As String
        Get
            Return _location_index
        End Get
        Set(ByVal value As String)
            _location_index = value
        End Set
    End Property
    Public Property appStatus() As Integer
        Get
            Return _appStatus
        End Get
        Set(ByVal value As Integer)
            _appStatus = value
        End Set
    End Property

    Sub AddcbWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbWarehouse.DataSource = objDT
            cbWarehouse.DisplayMember = "Description"
            cbWarehouse.ValueMember = "Warehouse_Index"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbRoom()
        Dim objClassDB As New ms_Room(ms_Room.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", String.Format(" AND Warehouse_Index = '{0}' ", Me.cbWarehouse.SelectedValue.ToString))
            objDT = objClassDB.DataTable

            cbRoom.DataSource = objDT
            cbRoom.DisplayMember = "Description"
            cbRoom.ValueMember = "Room_Index"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbLocationType()
        Dim objClassDB As New ms_LocationType()
        Dim objDT As DataTable = New DataTable
        objClassDB.GetAllAsDataTable()

        Try
            'objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbLocationType.DataSource = objDT
            cbLocationType.DisplayMember = "Description"
            cbLocationType.ValueMember = "LocationType_Index"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbZone()

        Dim objClassDB As New ms_Zone(ms_Zone.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbZone.DataSource = objDT
            cbZone.DisplayMember = "Description"
            cbZone.ValueMember = "Zone_Index"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub AddcbAction()
        Dim objClassDB As New ms_LocationAction
        Dim objDT As DataTable = objClassDB.GetData
        Try
            With cboAction
                .DisplayMember = "Status_Th"
                .ValueMember = "Action_Id"
                .DataSource = objDT
            End With
            cboAction.SelectedValue = "1"

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Private Sub frmLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2028)

            AddcbWarehouse()
            AddcbLocationType()

            AddcbZone()
            AddcbAction()

            Select Case appStatus
                Case 0 'app status == add'

                Case 1 'app status == edit'
                    txtID.Text = location_index
                    Dim objLocation As New ms_Location(ms_Size.enuOperation_Type.SEARCH)
                    Dim objDT As DataTable = New DataTable

                    objLocation.SelectShowDataNotJoin(location_index, "3")
                    objDT = objLocation.DataTable
                    If objDT.Rows.Count = 0 Then

                        Exit Sub
                    End If
                    txtID.Text = objDT.Rows(0)("Location_Index").ToString()
                    txtLocation.Text = objDT.Rows(0)("Location_Alias").ToString()
                    cbWarehouse.SelectedValue = objDT.Rows(0)("Warehouse_Index").ToString()
                    cbRoom.SelectedValue = objDT.Rows(0)("Room").ToString()
                    txtLock.Text = objDT.Rows(0)("Lock").ToString()
                    cbLocationType.SelectedValue = objDT.Rows(0)("LocationType_Index").ToString()
                    txtMaxQty.Text = objDT.Rows(0)("Max_Qty").ToString()
                    txtMax_Weight.Text = objDT.Rows(0)("Max_Weight").ToString()
                    txtMax_Volume.Text = objDT.Rows(0)("Max_Volume").ToString()
                    cbZone.SelectedValue = objDT.Rows(0)("Zone_Index").ToString()
                    cboAction.SelectedValue = objDT.Rows(0)("Action_Id").ToString()
                    txtRow.Text = objDT.Rows(0)("Row").ToString()
                    txtLevel.Text = objDT.Rows(0)("Level").ToString()
                    txtDepth.Text = objDT.Rows(0)("Depth").ToString()
                    txtScore.Text = objDT.Rows(0)("Score").ToString()
                    txtMaxPallet.Text = objDT.Rows(0)("Max_Pallet").ToString()


                    'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                    If objDT.Rows(0)("Allow_Sugguest_Putaway").ToString = "1" Then
                        Me.chkAllow_Sugguest_Putaway.Checked = True
                    Else
                        Me.chkAllow_Sugguest_Putaway.Checked = False
                    End If

                    If objDT.Rows(0)("Allow_Sugguest_Pick").ToString = "1" Then
                        Me.chkAllow_Sugguest_Pick.Checked = True
                    Else
                        Me.chkAllow_Sugguest_Pick.Checked = False
                    End If

            End Select
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
    Sub saveData_Location()
        Try
            Select Case _appStatus
                Case 0 'Add New
                    Dim objLocation As New ms_Location(ms_Size.enuOperation_Type.SEARCH)
                    Dim objDT As DataTable = New DataTable

                    objLocation.SelectShowDataNotJoin(location_index, "3")
                    objDT = objLocation.DataTable
                    If objDT.Rows.Count > 0 Then
                        W_MSG_Information("มีชื่อตำแหน่งในระบบแล้ว")
                        Exit Sub
                    Else
                        Dim objItemCollection As New List(Of ms_Location)
                        Dim objms_Location As New ms_Location(ms_Size.enuOperation_Type.ADDNEW, objItemCollection)

                        objItemCollection.Add(objms_Location)

                        With objms_Location
                            objms_Location.Location_Alias = Me.txtLocation.Text.Trim
                            objms_Location.Warehouse_Index = Me.cbWarehouse.SelectedValue
                            objms_Location.Room = Me.cbRoom.SelectedValue
                            objms_Location.Zone_Index = Me.cbZone.SelectedValue
                            objms_Location.Lock = Me.txtLock.Text.Trim
                            objms_Location.LocationType_Index = Me.cbLocationType.SelectedValue
                            objms_Location.Max_Qty = Me.txtMaxQty.Text.Trim
                            objms_Location.Max_Weight = Me.txtMax_Weight.Text.Trim
                            objms_Location.Max_Volume = Me.txtMax_Volume.Text.Trim

                            objms_Location.Zone_Index = Me.cbZone.SelectedValue

                            objms_Location.Row = Me.txtRow.Text.Trim
                            objms_Location.Level = Me.txtLevel.Text.Trim
                            objms_Location.Depth = Me.txtDepth.Text.Trim
                            objms_Location.score = Me.txtScore.Text.Trim

                            objms_Location.Max_Pallet = Me.txtMaxPallet.Text.Trim
                            objms_Location.Action_Id = Me.cboAction.SelectedValue

                            'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                            If Me.chkAllow_Sugguest_Putaway.Checked Then
                                objms_Location.Allow_Sugguest_Putaway = 1
                            Else
                                objms_Location.Allow_Sugguest_Putaway = 0
                            End If

                            If Me.chkAllow_Sugguest_Pick.Checked Then
                                objms_Location.Allow_Sugguest_Pick = 1
                            Else
                                objms_Location.Allow_Sugguest_Pick = 0
                            End If


                        End With

                        objms_Location.Insert()

                        '(SaveData(txtIndex.Text, txtID.Text, Holiday_date, txtDes.Text, txtremark.Text))
                        'W_MSG_Information_ByIndex(1)
                        'W_MSG_Information("บันทึกข้อมูลสำเร็จ")
                    End If

                Case 1 'Update

                    Dim objItemCollection As New List(Of ms_Location)
                    Dim objms_Location As New ms_Location(ms_Size.enuOperation_Type.UPDATE, objItemCollection)

                    objItemCollection.Add(objms_Location)

                    With objms_Location
                        objms_Location.Location_Index = Me.txtID.Text.Trim
                        objms_Location.Location_Alias = Me.txtLocation.Text.Trim
                        objms_Location.Warehouse_Index = Me.cbWarehouse.SelectedValue
                        objms_Location.Room = Me.cbRoom.SelectedValue
                        objms_Location.Zone_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.cbZone.SelectedValue, GetType(String))
                        objms_Location.Lock = Me.txtLock.Text.Trim
                        objms_Location.LocationType_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.cbLocationType.SelectedValue, GetType(String))
                        objms_Location.Max_Qty = Me.txtMaxQty.Text.Trim
                        objms_Location.Max_Weight = Me.txtMax_Weight.Text.Trim
                        objms_Location.Max_Volume = Me.txtMax_Volume.Text.Trim
                        objms_Location.Zone_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.cbZone.SelectedValue, GetType(String))

                        objms_Location.Row = Me.txtRow.Text.Trim
                        objms_Location.Level = Me.txtLevel.Text.Trim
                        objms_Location.Depth = Me.txtDepth.Text.Trim
                        objms_Location.score = Me.txtScore.Text.Trim

                        objms_Location.Max_Pallet = Me.txtMaxPallet.Text.Trim
                        objms_Location.Action_Id = Me.cboAction.SelectedValue

                        'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                        If Me.chkAllow_Sugguest_Putaway.Checked Then
                            objms_Location.Allow_Sugguest_Putaway = 1
                        Else
                            objms_Location.Allow_Sugguest_Putaway = 0
                        End If

                        If Me.chkAllow_Sugguest_Pick.Checked Then
                            objms_Location.Allow_Sugguest_Pick = 1
                        Else
                            objms_Location.Allow_Sugguest_Pick = 0
                        End If

                    End With

                    objms_Location.Update()
                    'W_MSG_Information("แก้ไขข้อมูลสำเร็จ")
                    'Dim objms_Holiday As New ms_Holiday(ms_ProductType.enuOperation_Type.UPDATE)
                    'If ID_Old <> txtID.Text.Trim Then
                    '    If objms_Holiday.isChckID(txtID.Text) = True Then
                    '        W_MSG_Information("มีชื่อย่อนี้ในระบบแล้ว")
                    '        Exit Sub
                    '    End If
                    'End If
                    'objms_Holiday.SaveData(txtIndex.Text, txtID.Text, Holiday_date, txtDes.Text, txtremark.Text)
                    'W_MSG_Information_ByIndex(1)


            End Select

            Dim objCon As New DBType_SQLServer
            objCon.DBExeNonQuery(String.Format("update ms_Location set update_by = '{0}' , update_date = getdate() where Location_Alias = '{1}'", W_Module.WV_UserName, Me.txtLocation.Text))
            W_MSG_Information("บันทึกข้อมูลเรียบร้อย")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If _validate.validateKey(txtID.Text, lbID.Text) Then Return
            'If _validate.validateKey(txtLocation.Text, lblDes.Text) Then Return
            'If _validate.validateKey(cbWarehouse.Text, lblWareHouse.Text) Then Return
            'If _validate.validateKey(cbRoom.Text, lblRoom.Text) Then Return
            'If _validate.validateKey(cbZone.Text, lblZone.Text) Then Return
            'If _validate.validateKey(txtLock.Text, lblLock.Text) Then Return

            If txtLocation.Text = "" Then
                W_MSG_Information("กรุณาระบุตำแหน่ง")
                txtLocation.Focus()
                Exit Sub
            ElseIf cbZone.SelectedValue = "" Then
                W_MSG_Information("กรุณาระบุโซน")
                txtLock.Focus()
                Exit Sub
            ElseIf txtLock.Text = "" Then
                W_MSG_Information("กรุณาระบุล็อค")
                txtLock.Focus()
                Exit Sub
            ElseIf txtRow.Text = "" Then
                W_MSG_Information("กรุณาระบุแถว")
                txtRow.Focus()
                Exit Sub
            ElseIf txtLevel.Text = "" Then
                W_MSG_Information("กรุณาระบุชั้น")
                txtLevel.Focus()
                Exit Sub
            ElseIf txtDepth.Text = "" Then
                W_MSG_Information("กรุณาระบุลึก")
                txtDepth.Focus()
                Exit Sub
            End If

            If Not IsNumeric(Me.txtMaxPallet.Text.Trim) Then
                W_MSG_Information(String.Format("กรุณาระบุ {0} ให้ถูกต้อง !!", Me.lblMaxPallet))
                txtMaxPallet.Focus()
                Exit Sub
            End If

            If Me.cbRoom.SelectedValue Is Nothing Then
                W_MSG_Information(String.Format("กรุณาระบุ {0} ให้ถูกต้อง !!", Me.lblRoom))
                Exit Sub
            End If

            If Me.location_index = "" Then
                Dim oLocation As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                Dim ochkLocation_Index As String = oLocation.getLocation_Index(txtLocation.Text)
                If ochkLocation_Index <> "" Then
                    W_MSG_Information("ตำแหน่งนี้มีในระบบแล้วกรุณาเปลี่ยนชื่อตำแหน่ง")
                    txtLocation.Focus()
                    Exit Sub
                End If
                saveData_Location()
            Else

                saveData_Location()
            End If
            'refresh grid
            Dim objMainLocation As New frmMainLocation
            Dim objLocation As New ms_Location(ms_Size.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable
            If Not objMainLocation.cboSearchType.Text = "" Then
                Select Case objMainLocation.cboSearchType.SelectedIndex
                    Case 0 'all
                        objLocation.SelectShowData("", "")
                    Case 1
                        objLocation.SelectShowData(objMainLocation.txtSearchKey.Text.Trim, "1")
                    Case 2
                        objLocation.SelectShowData(objMainLocation.txtSearchKey.Text.Trim, "2")
                End Select
                objMainLocation.grdLocationType.DataSource = Nothing 'Rows.Clear()
                objDT = objLocation.DataTable
                objMainLocation.grdLocationType.DataSource = objDT
            Else
                objLocation.SelectShowData("", "")
            End If
            Me.Close()
            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLevel_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLevel.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLevel, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtRow_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRow.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtRow, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDepth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDepth.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtDepth, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtScore_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtScore.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtScore, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMax_Weight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMax_Weight.KeyPress
        Try
            e.Handled = CheckNumText(txtMax_Weight, e) 'e.Handled = CurrencyTextBox.NumbericOnly(txtLevel, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    
    Private Sub txtMax_Volume_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMax_Volume.KeyPress
        Try
            e.Handled = CheckNumText(txtMax_Volume, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function CheckNumText(ByVal pText As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Try
            If Char.IsNumber(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = ChrW(System.Windows.Forms.Keys.Back) Then
                If e.KeyChar = "." Then
                    If txtMax_Volume.Text.IndexOf(".") >= 0 Then Return True
                    If txtMaxQty.Text.IndexOf(".") >= 0 Then Return True
                    If txtMax_Weight.Text.IndexOf(".") >= 0 Then Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub txtMaxQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxQty.KeyPress
        Try
            e.Handled = CheckNumText(txtMaxQty, e)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub frmLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigLocation
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2028)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cbWarehouse_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbWarehouse.SelectedValueChanged
        Try
            Me.cbRoom.DataSource = Nothing
            AddcbRoom()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class