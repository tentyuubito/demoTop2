Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_formula.W_Module
Imports WMS_STD_Master

Imports System.Data.SqlClient
''' <summary>
''' 25/07/2011
''' create by big
''' </summary>
''' <remarks></remarks>
Public Class frmAddLockRack
    Private _warehouse_Index As String = ""
    Public Property WareHouse_Index() As String
        Get
            Return _warehouse_Index
        End Get
        Set(ByVal value As String)
            _warehouse_Index = value
        End Set
    End Property
    Private _Room_Index As String = ""
    Public Property Room_Index() As String
        Get
            Return _Room_Index
        End Get
        Set(ByVal value As String)
            _Room_Index = value
        End Set
    End Property
    Private _Layout_Index As String = ""
    Public Property Layout_Index() As String
        Get
            Return _Layout_Index
        End Get
        Set(ByVal value As String)
            _Layout_Index = value
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
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbRoom()
        Dim objClassDB As New ms_Room(ms_Room.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbRoom.DataSource = objDT
            cbRoom.DisplayMember = "Description"
            cbRoom.ValueMember = "Room_Index"
        Catch ex As Exception
            Throw ex
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
            Throw ex
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
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub fn_SaveData_Location(ByVal pstr_Location_Alias As String, ByVal pstr_Warehouse_Index As String, ByVal pstr_Room As String, ByVal pstr_Zone_Index As String, ByVal pstr_Lock As String, ByVal pstr_LocationType_Index As String, ByVal pstr_Max_Qty As String, ByVal pstr_Max_Weight As String, ByVal pstr_Max_Volume As String, ByVal pstr_Row As String, ByVal pstr_Level As String, ByVal pstr_Depth As String, ByVal max_pallet As Decimal)
        Try
            'Add New
            Dim objItemCollection As New List(Of ms_Location)
            Dim objms_Location As New ms_Location(ms_Size.enuOperation_Type.ADDNEW, objItemCollection)

            objItemCollection.Add(objms_Location)

            With objms_Location
                objms_Location.Location_Alias = pstr_Location_Alias
                objms_Location.Warehouse_Index = pstr_Warehouse_Index
                objms_Location.Room = pstr_Room
                objms_Location.Zone_Index = pstr_Zone_Index
                objms_Location.Lock = pstr_Lock
                objms_Location.LocationType_Index = pstr_LocationType_Index
                objms_Location.Max_Qty = pstr_Max_Qty
                objms_Location.Max_Weight = pstr_Max_Weight
                objms_Location.Max_Volume = pstr_Max_Volume
                objms_Location.Row = pstr_Row
                objms_Location.Level = pstr_Level
                objms_Location.Depth = pstr_Depth
                objms_Location.Layout_Index = Me.Layout_Index
                objms_Location.Max_Pallet = max_pallet



            End With
            objms_Location.Insert()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmAddLockRack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AddcbWarehouse()
            AddcbLocationType()
            AddcbRoom()
            AddcbZone()
            grdLocationAlias.AutoGenerateColumns = False
            If Me.WareHouse_Index <> "" Then
                Me.cbWarehouse.SelectedValue = Me.WareHouse_Index

            End If
            If Me.Room_Index <> "" Then
                Me.cbRoom.SelectedValue = Me.Room_Index
            End If



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnGenLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenLocation.Click
        Try
            'validate 
            If txtFormatLocation.Text = "" Then
                W_MSG_Information("กรุณาระบุ Format Location")
                txtFormatLocation.Focus()
                Exit Sub
            ElseIf txtLockRack.Text = "" Then
                W_MSG_Information("กรุณาระบุล็อค")
                txtLockRack.Focus()
                Exit Sub
            ElseIf txtRow1.Text = "" Then
                W_MSG_Information("กรุณาระบุแถว")
                txtRow1.Focus()
                Exit Sub
            ElseIf txtRow2.Text = "" Then
                W_MSG_Information("กรุณาระบุแถว")
                txtRow2.Focus()
                Exit Sub
            ElseIf CInt(txtRow1.Text) > CInt(txtRow2.Text) Then
                W_MSG_Information("กรุณาระบุแถวให้ถูกต้อง")
                txtRow1.Focus()
                Exit Sub
            ElseIf txtDepth1.Text = "" Then
                W_MSG_Information("กรุณาระบุลึก")
                txtDepth1.Focus()
                Exit Sub
            ElseIf txtDepth2.Text = "" Then
                W_MSG_Information("กรุณาระบุลึก")
                txtDepth2.Focus()
                Exit Sub
            ElseIf CInt(txtDepth1.Text) > CInt(txtDepth2.Text) Then
                W_MSG_Information("กรุณาระบุความลึกให้ถูกต้อง")
                txtDepth1.Focus()
                Exit Sub
            ElseIf txtLevel1.Text = "" Then
                W_MSG_Information("กรุณาระบุชั้น")
                txtLevel1.Focus()
                Exit Sub
            ElseIf txtLevel2.Text = "" Then
                W_MSG_Information("กรุณาระบุชั้น")
                txtLevel2.Focus()
                Exit Sub
            ElseIf CInt(txtLevel1.Text) > CInt(txtLevel2.Text) Then
                W_MSG_Information("กรุณาระบุชั้นให้ถูกต้อง")
                txtLevel1.Focus()
                Exit Sub
            ElseIf txtMaxQTY.Text = "" Then
                W_MSG_Information("กรุณาระบุ Max QTY")
                txtMaxQTY.Focus()
                Exit Sub
            ElseIf txtMaxWeight.Text = "" Then
                W_MSG_Information("กรุณาระบุ Max Weight")
                txtMaxWeight.Focus()
                Exit Sub
            ElseIf txtMaxVolume.Text = "" Then
                W_MSG_Information("กรุณาระบุ Max Volume")
                txtMaxVolume.Focus()
                Exit Sub
            ElseIf txtMaxPallet.Text = "" Then
                W_MSG_Information("กรุณาระบุ Max Pallet")
                txtMaxPallet.Focus()
                Exit Sub
            End If
            'end validate

            ''''''''''''start gen location ''''''''''''''''
            'create colum datatble objDT_GenLocation by clone 
            Dim objDT_GenLocation As DataTable = New ms_Location(ms_Size.enuOperation_Type.SEARCH).getCloneLocationHeader()
            Dim objDr_GenLocation As DataRow
            'check colum datable ว่ามี colum ที่ต้องการหรือไม่
            If objDT_GenLocation.Columns.Contains("Warehouse_Index") = False Then
                objDT_GenLocation.Columns.Add("Warehouse_Index") '
            End If
            If objDT_GenLocation.Columns.Contains("Zone_Index") = False Then
                objDT_GenLocation.Columns.Add("Zone_Index") '
            End If
            'String Replace FormatLocation
            Dim str_replace_step1 As String = ""
            Dim str_replace_step2 As String = ""
            Dim str_replace_step3 As String = ""
            Dim str_replace_step4 As String = ""

            str_replace_step1 = txtFormatLocation.Text.Replace("[Lock]", txtLockRack.Text)
            'Replace FormatLocation
            For i As Integer = CInt(txtRow1.Text) To CInt(txtRow2.Text)
                str_replace_step2 = str_replace_step1.Replace("[Row]", i.ToString("D" & txtPositionRow.Text.Length & ""))
                For j As Integer = CInt(txtDepth1.Text) To CInt(txtDepth2.Text)
                    str_replace_step3 = str_replace_step2.Replace("[Depth]", j.ToString("D" & txtPositionDepth.Text.Length & ""))
                    For k As Integer = CInt(txtLevel1.Text) To CInt(txtLevel2.Text)
                        str_replace_step4 = str_replace_step3.Replace("[Level]", k.ToString("D" & txtPositionLevel.Text.Length & ""))
                        objDr_GenLocation = objDT_GenLocation.NewRow
                        objDr_GenLocation("Location_Alias") = str_replace_step4
                        objDr_GenLocation("Warehouse_Index") = Me.cbWarehouse.SelectedValue
                        objDr_GenLocation("Room") = Me.cbRoom.SelectedValue
                        objDr_GenLocation("Zone_Index") = Me.cbZone.SelectedValue
                        objDr_GenLocation("Lock") = Me.txtLockRack.Text.Trim
                        objDr_GenLocation("LocationType_Index") = Me.cbLocationType.SelectedValue
                        objDr_GenLocation("Max_Pallet") = Me.txtMaxPallet.Text.Trim
                        objDr_GenLocation("Max_Qty") = Me.txtMaxQTY.Text.Trim
                        objDr_GenLocation("Max_Weight") = Me.txtMaxWeight.Text.Trim
                        objDr_GenLocation("Max_Volume") = Me.txtMaxVolume.Text.Trim
                        objDr_GenLocation("Row") = i.ToString
                        objDr_GenLocation("Level") = k.ToString
                        objDr_GenLocation("Depth") = j.ToString
                        objDT_GenLocation.Rows.Add(objDr_GenLocation)
                    Next
                Next
            Next
            'Bind Grid
            grdLocationAlias.DataSource = objDT_GenLocation
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btn_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        Try
            txtDepth1.Text = "1"
            txtDepth2.Text = "1"
            txtLevel1.Text = "1"
            txtLevel2.Clear()
            txtLockRack.Clear()
            txtMaxPallet.Text = "1000000"
            txtMaxQTY.Text = "1000000"
            txtMaxVolume.Text = "1000000"
            txtMaxWeight.Text = "1000000"
            txtPositionDepth.Text = "XX"
            txtPositionLevel.Text = "XX"
            txtPositionRow.Text = "XX"
            txtRow1.Text = "1"
            txtRow2.Clear()
            grdLocationAlias.DataSource = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'validate
            Dim objLocation As New ms_Location(ms_Size.enuOperation_Type.SEARCH)
            Dim objDT_LocationAlias As DataTable = CType(grdLocationAlias.DataSource, DataTable)

            If grdLocationAlias.Rows.Count = 0 Then
                W_MSG_Information("กรุณาทำการ สร้าง Location")
                Exit Sub
            End If
            For Each drChkLocationAlias As DataRow In CType(grdLocationAlias.DataSource, DataTable).Rows
                Dim str_chk_alias As String = objLocation.getLocation_Index(drChkLocationAlias("Location_Alias"))
                If str_chk_alias <> "" Then
                    W_MSG_Information("มีตำแหน่งนี้ในระบบแล้ว")
                    Exit Sub
                End If
            Next

            'save location
            For Each dr As DataRow In objDT_LocationAlias.Rows
                fn_SaveData_Location(dr("Location_Alias").ToString, dr("Warehouse_Index").ToString, dr("Room").ToString, dr("Zone_Index").ToString, dr("Lock").ToString, dr("LocationType_Index").ToString, dr("Max_Qty").ToString, dr("Max_Weight").ToString, dr("Max_Volume").ToString, dr("Row").ToString, dr("Level").ToString, dr("Depth").ToString, dr("Max_Pallet"))
            Next
            W_MSG_Information("บันทึกข้อมูลสำเร็จ")
            Me.grdLocationAlias.DataSource = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMaxQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxQTY.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtMaxQTY, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMaxWeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxWeight.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtMaxWeight, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMaxVolume_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxVolume.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtMaxVolume, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMaxPallet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxPallet.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtMaxPallet, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtRow1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRow1.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtRow1, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtRow2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRow2.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtRow2, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDepth1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDepth1.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtDepth1, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDepth2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDepth2.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtDepth2, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLevel1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLevel1.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLevel1, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLevel2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLevel2.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLevel2, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


End Class