Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms

Public Class frmDocumentTypeV5

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update
    Private _validate As New WMS_STD_Master.ValidateCharacter()
    Private _DocumentType_Id_Old As String

    Public Property DocumentType_Id_Old() As String
        Get
            Return _DocumentType_Id_Old
        End Get
        Set(ByVal value As String)
            _DocumentType_Id_Old = value
        End Set
    End Property


    Private _DocumentType_Index As String = ""
    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property


    Private _Ref_DocumentType_Index As String = ""
    Public Property Ref_DocumentType_Index() As String
        Get
            Return _Ref_DocumentType_Index
        End Get
        Set(ByVal value As String)
            _Ref_DocumentType_Index = value
        End Set
    End Property
    ''' <summary>
    ''' -------------------------------------------------
    ''' Update Date : 10/06/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Fix Bug : เพิ่มสถานะสินค้า
    ''' -------------------------------------------------
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDocumentType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.grdItemStatus.AutoGenerateColumns = False

            Me.cboFormat1.SelectedIndex = 0
            Me.cboFormat2.SelectedIndex = 0
            Me.cboReset_Running_By.SelectedIndex = 0
            Me.cboFormat_Date.SelectedIndex = 0
            Me.cboFormat_Running.SelectedIndex = 3

            AddProcessID()
            AddcboItemStatus()
            AddSysKeyName()


            Select Case SaveType
                Case 0 'Save
                    Dim oItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
                    Dim dtItemStatusAll As New DataTable

                    '*** Get All Status
                    oItemStatus.getPopup_Search("")
                    dtItemStatusAll = oItemStatus.GetDataTable

                    'Define default Need Item
                    If Not dtItemStatusAll.Columns.Contains("DocumentType_ItemStatus_index") Then
                        dtItemStatusAll.Columns.Add(New DataColumn("DocumentType_ItemStatus_index", GetType(String)))
                    End If
                    If Not dtItemStatusAll.Columns.Contains("chkSelect") Then
                        dtItemStatusAll.Columns.Add(New DataColumn("chkSelect", GetType(Boolean)))
                    End If
                    For Each drItemStatusAll As DataRow In dtItemStatusAll.Rows
                        drItemStatusAll("DocumentType_ItemStatus_index") = ""
                        drItemStatusAll("chkSelect") = False
                    Next
                    If dtItemStatusAll.Columns.Contains("priority_index") = False Then
                        dtItemStatusAll.Columns.Add("priority_index")
                    End If
                    grdItemStatus.DataSource = dtItemStatusAll
                    Me.cboSysKeyName.SelectedValue = ""
                    Me.txtSysValue.Text = "00000"
                Case 1 'Update
                    Dim objms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
                    objms_DocumentType.SearchDocumentTypeV4(DocumentType_Index)
                    Dim odtms_producttype As New DataTable
                    odtms_producttype = objms_DocumentType.DataTable

                    If odtms_producttype.Rows.Count > 0 Then
                        With odtms_producttype.Rows(0)
                            Me.DocumentType_Id_Old = .Item("DocumentType_Id").ToString
                            Me.DocumentType_Index = .Item("DocumentType_Index").ToString
                            Me.txtID.Text = .Item("DocumentType_Id").ToString
                            Me.txtDes.Text = .Item("Description").ToString
                            Me.cboProcess.SelectedValue = .Item("Process_Id").ToString
                            If .Item("Sys_Key_Name").ToString <> "" Then
                                Me.cboSysKeyName.SelectedValue = odtms_producttype.Rows(0).Item("Sys_Key_Name")
                            Else
                                Me.chkUseMaxDocument.Checked = True
                                Me.cboSysKeyName.Enabled = False
                                Me.cboSysKeyName.Text = ""
                                Me.txtSysValue.Enabled = False
                                Me.txtSysValue.Text = "00000"
                            End If
                            'If .Item("Ref_DocumentType_Index").ToString = "" Then
                            '    Me.txtDocument_description.Text = ""
                            'Else
                            '    Me.txtDocument_description.Text = .Item("Ref_Description").ToString
                            'End If
                            If .Item("ItemStatus_Index").ToString <> "" Then
                                cboItemStatus.SelectedValue = .Item("ItemStatus_Index").ToString
                            End If
                            If .Item("Location_Index").ToString <> "" Then
                                Dim omsloc As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                                txtItemStatus_Location.Text = omsloc.getLocation_Alias(.Item("Location_Index").ToString)
                            End If
                            If .Item("Format_Document").ToString <> "" Then
                                Dim strFormatdate As String = .Item("Format_Document").ToString
                                strFormatdate = strFormatdate.Replace("[", "%")
                                strFormatdate = strFormatdate.Replace("]", "%")
                                Dim ArrFormatdate() As String = strFormatdate.Split("%")
                                Me.txtFormat_Document.Text = ArrFormatdate(0)
                                Me.cboFormat1.Text = ArrFormatdate(1)
                                Me.cboFormat2.Text = ArrFormatdate(3)
                            End If
                            If .Item("Reset_Running_By").ToString <> "" Then
                                Me.cboReset_Running_By.Text = .Item("Reset_Running_By").ToString
                            End If
                            If .Item("Format_Date").ToString <> "" Then
                                Me.cboFormat_Date.Text = .Item("Format_Date").ToString
                            End If
                            If .Item("Format_Running").ToString <> "" Then
                                Me.cboFormat_Running.Text = Len(.Item("Format_Running").ToString)
                            End If
                        End With
                        If odtms_producttype.Columns.Contains("Ref_No1") = True Then
                            Me.txtRef_No1.Text = odtms_producttype.Rows(0)("Ref_No1").ToString
                        End If
                        If odtms_producttype.Columns.Contains("Ref_No2") = True Then
                            Me.txtRef_No2.Text = odtms_producttype.Rows(0)("Ref_No2").ToString
                        End If
                        If odtms_producttype.Columns.Contains("Ref_No3") = True Then
                            Me.txtRef_No3.Text = odtms_producttype.Rows(0)("Ref_No3").ToString
                        End If
                    End If

                    ' ***** BEGIN Have Assigne ****
                    Dim oItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
                    Dim dtItemStatus As New DataTable

                    '*** Get Current Status
                    oItemStatus.getItemStatusByDocumentType_Index_AllStatus(DocumentType_Index)
                    dtItemStatus = oItemStatus.GetDataTable
                    If dtItemStatus.Columns.Contains("priority_index") = False Then
                        dtItemStatus.Columns.Add("priority_index")
                    End If
                    grdItemStatus.DataSource = dtItemStatus
            End Select

            Me.cboSysKeyName_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub AddSysKeyName()
        Dim objClassDB As New Sy_AutoNumber
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetDataSy_AutoNumber()
            objDT = objClassDB.DataTable
            cboSysKeyName.DisplayMember = "Sys_Key"
            cboSysKeyName.ValueMember = "Sys_Key"
            cboSysKeyName.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcboItemStatus()
        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable
            cboItemStatus.DisplayMember = "Description"
            cboItemStatus.ValueMember = "ItemStatus_Index"
            cboItemStatus.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddProcessID()
        Dim objClassDB As New ms_Process(ms_Process.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            cboProcess.DisplayMember = "process_Name"
            cboProcess.ValueMember = "Process_Id"
            cboProcess.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If _validate.validateKey(txtID.Text, lbID.Text) Then Return
            'If _validate.validateKey(txtDes.Text, lblDes.Text) Then Return
            If _validate.validateKey(txtFormat_Document.Text, lblFormat_Document.Text) Then Return
            If (Not Me.chkUseMaxDocument.Checked) Then
                If _validate.validateKey(cboSysKeyName.Text, lblSys.Text) Then Return
            End If
            'If txtID.Text.Trim = "" Then
            '    W_MSG_Information_ByIndex(74)
            '    txtID.Focus()
            '    Exit Sub
            'End If

            If txtDes.Text.Trim = "" Then
                W_MSG_Information_ByIndex(75)
                txtDes.Focus()
                Exit Sub
            End If
            If (Not Me.chkUseMaxDocument.Checked) Then
                If Me.cboSysKeyName.Text = "" Then
                    W_MSG_Information_ByIndex(75)
                    Me.cboSysKeyName.Focus()
                    Exit Sub
                End If
            Else
                Me.cboSysKeyName.Text = ""
                Me.txtSysValue.Text = ""
            End If


            saveDocumentType(DocumentType_Index, txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text, _Ref_DocumentType_Index)

            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub saveDocumentType(ByVal Index As String, ByVal ID As String, ByVal Process As Integer, ByVal Description As String, ByVal Ref_DocumentType_Index As String)
        Try
            Dim strItemStatus_Index As String = ""
            Dim strPalletStatus_Index As String = ""
            Dim strLocation_Index As String = ""

            Dim objlocation As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            strLocation_Index = objlocation.getLocation_Index(Me.txtItemStatus_Location.Text)
            If Me.txtItemStatus_Location.Text <> "" And strLocation_Index = "" Then
                W_MSG_Information("ไม่พบตำแหน่ง  ( " & Me.txtItemStatus_Location.Text & " ) นี้ในฐานข้อมูล กรุณาป้อนตำแหน่งใหม่อีกครั้ง")
                Exit Sub
            End If

            Select Case SaveType
                Case 0 'Add New
                    Dim objCheckIdms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
                    Dim BCheckId As Boolean = objCheckIdms_DocumentType.isChckID(txtID.Text)
                    If BCheckId Then
                        W_MSG_Information_ByIndex("45")
                        Me.btnSave.Enabled = True
                        Exit Sub
                    Else
                        Dim objms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.ADDNEW)
                        'Format Document
                        objms_DocumentType.Format_Document = Me.txtFormat_Document.Text & "[" & Me.cboFormat1.Text & "]" & "[" & Me.cboFormat2.Text & "]"
                        objms_DocumentType.Format_Date = Me.cboFormat_Date.Text
                        Dim strRunning As New String("x"c, CInt(Me.cboFormat_Running.Text))
                        objms_DocumentType.Format_Running = strRunning
                        objms_DocumentType.Reset_Running_By = Me.cboReset_Running_By.Text
                        If cboItemStatus.SelectedValue Is Nothing Then
                            strItemStatus_Index = ""
                        Else
                            strItemStatus_Index = cboItemStatus.SelectedValue.ToString
                        End If
                        objms_DocumentType.Process_Id = Me.cboProcess.SelectedValue
                        objms_DocumentType.DocumentType_Id = txtID.Text
                        objms_DocumentType.DocumentType_Index = ""
                        objms_DocumentType.Description = txtDes.Text
                        'objms_DocumentType.Ref_DocumentType_Index = Me._Ref_DocumentType_Index
                        objms_DocumentType.ItemStatus_Index = strItemStatus_Index
                        objms_DocumentType.Location_Index = strLocation_Index
                        objms_DocumentType.Sys_Key_Name = cboSysKeyName.Text
                        objms_DocumentType.Sys_Value = Me.txtSysValue.Text
                        objms_DocumentType.Ref_No1 = Me.txtRef_No1.Text.Trim
                        objms_DocumentType.Ref_No2 = Me.txtRef_No2.Text.Trim
                        objms_DocumentType.Ref_No3 = Me.txtRef_No3.Text.Trim

                        objms_DocumentType.SaveDataV5()
                        Me._DocumentType_Index = objms_DocumentType.DocumentType_Index
                        SaveItemStatus(Me._DocumentType_Index)
                        W_MSG_Information_ByIndex(1)
                        Me.Close()
                    End If

                Case 1 'Update
                    If Me.txtID.Text.Trim = "" Then
                        W_MSG_Information("กรุณาป้อน" & lbID.Text)
                        Exit Sub
                    End If

                    Dim objms_DocumentType As New ms_DocumentType_KSL(ms_DocumentType_KSL.enuOperation_Type.UPDATE)
                    If Me.DocumentType_Id_Old <> txtID.Text.Trim Then
                        If objms_DocumentType.isExistID(txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If
                    'Format Document
                    objms_DocumentType.Format_Document = Me.txtFormat_Document.Text & "[" & Me.cboFormat1.Text & "]" & "[" & Me.cboFormat2.Text & "]"
                    objms_DocumentType.Format_Date = Me.cboFormat_Date.Text
                    Dim strRunning As New String("x"c, CInt(Me.cboFormat_Running.Text))
                    objms_DocumentType.Format_Running = strRunning
                    objms_DocumentType.Reset_Running_By = Me.cboReset_Running_By.Text
                    If cboItemStatus.SelectedValue Is Nothing Then
                        strItemStatus_Index = ""
                    Else
                        strItemStatus_Index = cboItemStatus.SelectedValue.ToString
                    End If
                    objms_DocumentType.Process_Id = Me.cboProcess.SelectedValue
                    objms_DocumentType.DocumentType_Id = txtID.Text
                    objms_DocumentType.DocumentType_Index = Me._DocumentType_Index
                    objms_DocumentType.Description = txtDes.Text
                    'objms_DocumentType.Ref_DocumentType_Index = Me._Ref_DocumentType_Index
                    objms_DocumentType.ItemStatus_Index = strItemStatus_Index
                    objms_DocumentType.Location_Index = strLocation_Index
                    objms_DocumentType.Sys_Key_Name = cboSysKeyName.Text
                    objms_DocumentType.Sys_Value = Me.txtSysValue.Text

                    objms_DocumentType.Ref_No1 = Me.txtRef_No1.Text.Trim
                    objms_DocumentType.Ref_No2 = Me.txtRef_No2.Text.Trim
                    objms_DocumentType.Ref_No3 = Me.txtRef_No3.Text.Trim


                    objms_DocumentType.SaveDataV5()
                    Me._DocumentType_Index = objms_DocumentType.DocumentType_Index
                    SaveItemStatus(Me._DocumentType_Index)
                    W_MSG_Information_ByIndex(1)
                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub SaveItemStatus(ByVal pstrDocumentType_Index As String)
        Try


            Select Case SaveType
                Case 0 'Add New
                    Dim oSaveDocumentItemStatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.ADDNEW)
                    Dim strDocumentType_ItemStatus_index As String = ""

                    For Each drSaveItem As DataRow In CType(grdItemStatus.DataSource, DataTable).Rows
                        oSaveDocumentItemStatus.DocumentType_ItemStatus_index = strDocumentType_ItemStatus_index
                        oSaveDocumentItemStatus.DocumentType_Index = pstrDocumentType_Index
                        oSaveDocumentItemStatus.Process_Id = cboProcess.SelectedValue.ToString
                        oSaveDocumentItemStatus.ItemStatus_Index = drSaveItem("ItemStatus_Index")
                        oSaveDocumentItemStatus.Description = txtDes.Text
                        oSaveDocumentItemStatus.status_id = -1

                        If CType(drSaveItem("chkSelect"), Boolean) Then 'boxเช็คแล้วจะมีการเปลี่ยนค่า status_id 
                            oSaveDocumentItemStatus.status_id = 1
                        Else
                            Continue For
                        End If

                        If Not IsNumeric(drSaveItem("priority_index")) Then
                            oSaveDocumentItemStatus.priority_index = 0
                        Else
                            If drSaveItem("chkSelect") = True Then 'เช็คว่าboxที่จะใส่ลำดับความสำคัญถูกเช็คหรือไม่ถ้าไม่จะไม่มีการบันทึกให้
                                oSaveDocumentItemStatus.priority_index = drSaveItem("priority_index")
                            Else
                                oSaveDocumentItemStatus.priority_index = 0
                            End If
                        End If

                        oSaveDocumentItemStatus.Insert()
                    Next

                Case 1
                    Dim oSaveDocumentItemStatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.UPDATE)
                    Dim strDocumentType_ItemStatus_index As String = ""
                    oSaveDocumentItemStatus.DeleteAll(pstrDocumentType_Index)
                    For Each drSaveItem As DataRow In CType(grdItemStatus.DataSource, DataTable).Rows
                        If drSaveItem("chkSelect") = False Then
                            Continue For
                        End If
                        oSaveDocumentItemStatus.DocumentType_Index = pstrDocumentType_Index
                        oSaveDocumentItemStatus.DocumentType_ItemStatus_index = strDocumentType_ItemStatus_index
                        oSaveDocumentItemStatus.DocumentType_Index = pstrDocumentType_Index
                        oSaveDocumentItemStatus.Process_Id = cboProcess.SelectedValue.ToString
                        oSaveDocumentItemStatus.ItemStatus_Index = drSaveItem("ItemStatus_Index")
                        oSaveDocumentItemStatus.Description = txtDes.Text
                        oSaveDocumentItemStatus.status_id = 1

                        If Not IsNumeric(drSaveItem("priority_index")) Then
                            oSaveDocumentItemStatus.priority_index = 0
                        Else
                            If drSaveItem("chkSelect") = True Then
                                oSaveDocumentItemStatus.priority_index = drSaveItem("priority_index")
                            Else
                                oSaveDocumentItemStatus.priority_index = 0
                            End If
                        End If


                        oSaveDocumentItemStatus.Insert()
                    Next
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub frmDocumentType_Update_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        Try
            If MessageBox.Show("คุณต้องการบันทึกข้อมูลใช่หรือไม่ ", "ยืนยันการบันทึกข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                btnSave_Click(sender, New System.EventArgs)
            End If
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Try
            For i As Integer = 0 To Me.grdItemStatus.RowCount - 1
                Me.grdItemStatus.Rows(i).Cells("chkSelect").Value = Me.chkAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Sub grdItemStatus_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdItemStatus.EditingControlShowing
        Try
            Dim strName As String = Me.grdItemStatus.Columns(grdItemStatus.CurrentCell.ColumnIndex).Name.ToUpper
            If (strName = "COL_PRIORITY") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = Me.grdItemStatus.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = Me.grdItemStatus.Columns("COL_PRIORITY").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If Me.grdItemStatus.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = Me.grdItemStatus.CurrentRow.Cells(Column_Index).EditedFormattedValue
                                If strData.IndexOf(".") >= 0 Then Return True
                            Else
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return True
                    End If
                Case 1
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                        Return False
                    Else
                        Return True
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function

    Private Sub frmDocumentType_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSeachLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachLoc.Click
        Dim frmLoc As New WMS_STD_Master.frmLocation_Popup
        frmLoc.ShowDialog()
        '.Rows(e.RowIndex).Cells("Location_Index").Value = frmLoc.Col_Alias
        txtItemStatus_Location.Text = frmLoc.Col_Alias
        txtItemStatus_Location.Tag = frmLoc.Col_Index
        frmLoc.Close()
    End Sub

    Private Sub cboSysKeyName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSysKeyName.SelectedIndexChanged
        Try
            Dim objClassDB As New Sy_AutoNumber
            Dim objDT As DataTable = New DataTable

            Try
                If Me.cboSysKeyName.SelectedValue = "" Then
                    Me.txtSysValue.Text = "00000"
                Else
                    Me.txtSysValue.Text = objClassDB.SelectLast_Sys_Value(Me.cboSysKeyName.SelectedValue)
                    'Me.txtSysValue.Text = objClassDB.SelectMax_Sys_Value(Me.cboSysKeyName.SelectedValue)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objDT = Nothing
                objClassDB = Nothing
            End Try
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboSysKeyName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboSysKeyName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim objClassDB As New Sy_AutoNumber
                Dim objDT As DataTable = New DataTable

                Try
                    'If Me.cboSysKeyName.SelectedIndex <> 0 Then
                    If objClassDB.CheckSys_Value(Me.cboSysKeyName.Text) Then
                        Me.txtSysValue.Text = "00000"
                    Else
                        Me.txtSysValue.Text = objClassDB.SelectMax_Sys_Value(Me.cboSysKeyName.SelectedValue)
                    End If
                    'End If
                Catch ex As Exception
                    Throw ex
                Finally
                    objDT = Nothing
                    objClassDB = Nothing
                End Try
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkUseMaxDocument_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseMaxDocument.CheckedChanged
        Try
            If (chkUseMaxDocument.Checked) Then
                Me.cboSysKeyName.Enabled = False
                Me.cboSysKeyName.Text = ""
                Me.txtSysValue.Enabled = False
                Me.txtSysValue.Text = "00000"
            Else
                Me.cboSysKeyName.Enabled = True
                Me.txtSysValue.Enabled = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class