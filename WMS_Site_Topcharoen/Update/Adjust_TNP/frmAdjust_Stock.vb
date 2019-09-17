Imports WMS_STD_Master
Imports WMS_STD_OAW_Adjust_Datalayer
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.IO.FileStream
Imports System.Reflection
Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula

Public Class frmAdjust_Stock
    'Private _MsgAlert As New frmMsgAlert
    'Private _MsgConfirm As New frmMsgConfirm
    Private isNewadjustLocation As Boolean = False

    Private _Current_Barcode_Sharp As New DataTable
    Private _IsAdjustSku As Boolean = False
    Private _DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU As String = ""
    Private _DocumentType_Index As String = ""

#Region "    Property       "
    Enum Mode
        Online
        Offline
    End Enum

    Private _AppMode As Mode = Mode.Online
    Public Property AppMode() As Mode
        Get
            Return _AppMode
        End Get
        Set(ByVal value As Mode)
            _AppMode = value
        End Set
    End Property

    Private _Adjust_Index As String = ""
    Private _AdjustItemLocation_Index As String = ""

    Public Property Adjust_Index() As String
        Get
            Return _Adjust_Index
        End Get
        Set(ByVal Value As String)
            _Adjust_Index = Value
        End Set
    End Property

    Private chkClearLocation As Boolean = False
#End Region

    Private Sub frmAdjustDetail_Sharp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            ''Insert
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.dgAdjust_Detail)
            'oFunction.Insert_config_DataGridColumn(Me, Me.dgAdjust_DetailLocation)

            '' ''SwitchLanguage
            oFunction.SwitchLanguage(Me)
            oFunction.SW_Language_Column(Me, Me.dgAdjust_Detail)
            oFunction.SW_Language_Column(Me, Me.dgAdjust_DetailLocation)

            Me.dgAdjust_Detail.AutoGenerateColumns = False
            Me.dgAdjust_DetailLocation.AutoGenerateColumns = False

            Me.txtLocation.Focus()
            Me.LoadNewItemStatus()

            Me.LoadAdjust_Header()
            Me.LoadAdjust_Detail()

            Dim config As New config_CustomSetting
            Me._DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU = config.getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU")

            Select Case _DocumentType_Index
                Case Me._DEFAULT_DOCUMENT_TYPE_ADJUSTBYSKU
                    Me.txtLocation.Visible = False
                    Me.lblLocation.Visible = False
                    Me._IsAdjustSku = True
            End Select

            Me.txtLocation.Focus()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub LoadAdjust_Header()
        Try
            Dim objDataTable As New DataTable()
            Dim objDataTable_Detail As New DataTable()
            Dim objBusiness_Layer As New Adjust_Stock

            objBusiness_Layer.Adjust_HeaderDetail(Me._Adjust_Index)
            objDataTable = objBusiness_Layer.GetDataTable
            Me.dgAdjust_DetailLocation.DataSource = objDataTable

            If objDataTable.Rows.Count > 0 Then
                Me.txtAdjust_No.Text = objDataTable.Rows(0).Item("Adjust_No").ToString
                Me.txtAdjust_Date.Text = CDate(objDataTable.Rows(0).Item("Adjust_Date")).ToString("dd/MM/yyyy")
                Me.txtDocumentType.Text = objDataTable.Rows(0).Item("DocumentType_Desc").ToString
                Me._DocumentType_Index = objDataTable.Rows(0).Item("DocumentType_Index").ToString
            Else
                Me.txtAdjust_No.Text = ""
                Me.txtAdjust_Date.Text = ""
                Me.txtDocumentType.Text = ""
                Me._DocumentType_Index = ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub LoadAdjust_Detail()
        Try
            Dim objDataTable As New DataTable()
            Dim objDataTable_Detail As New DataTable()
            Dim objDataSet As New DataSet()
            Dim objBusiness_Layer As New Adjust_Stock

            'objBusiness_Layer.Adjust_Detail(Me._Adjust_Index, "")
            objBusiness_Layer.getAdjust_ItemDetail(Me._Adjust_Index, "")
            objDataTable = objBusiness_Layer.GetDataTable
            Me.dgAdjust_Detail.DataSource = objDataTable

            If objDataTable.Rows.Count > 0 Then
                ClearText()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub ClearText()
        Try
            Me.txtModel.Text = ""
            Me.txtSku_Name.Text = ""
            'Me.txtAdjust_Date.Text = ""
            'Me.txtAdjust_No.Text = ""
            'Me.txtDocumentType.Text = ""
            Me.txtAdjust_Qty.Text = ""
            Me.txtAdjust_Unit.Text = ""

            If chkClearLocation Then
                txtLocation.Text = ""
            End If

            txtBarcode.Text = ""
            _AdjustItemLocation_Index = ""
            txtLocation.Focus()
            isNewadjustLocation = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub LoadNewItemStatus()
        Try
            Dim ods As New DataSet
            Dim oBL As New Adjust_Stock
            ods = oBL.GetItemStatus()

            With Me.cboItemStatus
                .DisplayMember = "Description"
                .ValueMember = "ItemStatus_Index"
                .DataSource = ods.Tables(0)
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadItemStatus_Adjust(ByVal pSku_Index As String, ByVal pLocation_Alias As String)
        Try
            Dim odt As New DataTable
            Dim oBL As New Adjust_Stock
            oBL.getItemStatus_Adjust(pSku_Index, pLocation_Alias, Me._Adjust_Index)
            odt = oBL.GetDataTable

            With Me.cboItemStatus
                .DisplayMember = "Description"
                .ValueMember = "ItemStatus_Index"
                .DataSource = odt
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub txtDoc_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLocation.KeyDown

        Try
            Dim objBusiness_Layer As New Adjust_Stock

            If e.KeyCode = Keys.Enter Then
                Dim pNoItemLocation_Index As String = ""
                pNoItemLocation_Index = objBusiness_Layer.getlocation_index(Me.txtLocation.Text.Trim.Replace("'", "''"))

                If pNoItemLocation_Index = "" Then
                    Windows.Forms.Cursor.Current = Cursors.Default
                    MessageBox.Show("ไม่พบตำแหน่งจัดเก็บนี้ในฐานข้อมูล")
                    Me.txtLocation.Text = ""
                    Me.txtLocation.Focus()
                    Exit Sub
                End If

                Dim objDataTable As New DataTable()
                Dim objDataSet As New DataSet()
                Select Case AppMode
                    Case Mode.Offline
                        'Dim objbranch As New ms_branch()
                        'objbranch.SearchData_Click("", "")
                        'objDataTable = objbranch.GetDataTable()
                    Case Mode.Online
                        'get P/O to cbo.
                        If objBusiness_Layer.ChkAdjustByLocationBarcode(Me._Adjust_Index, Me.txtLocation.Text.Trim, "", False) = False Then
                            'If W_MSG_Confirm("ไม่มีตำแหน่ง : " & Me.txtLocation.Text & " ในรายการตรวจนับ" & Chr(13) & "คุณต้องการเพิ่มหรือไม่") = Windows.Forms.DialogResult.No Then
                            '    chkClearLocation = True
                            '    ClearText()
                            '    txtLocation.Focus()
                            'Else
                            '    Me._AdjustItemLocation_Index = ""
                            '    If objBusiness_Layer.insert_Adjust_Quantity(Me._AdjustItemLocation_Index, Me._Adjust_Index, 0, txtLocation.Text, txtBarcode.Text, 1, Me.cboItemStatus.SelectedValue, _IsAdjustSku, 1) = True Then
                            '        txtBarcode.Focus()
                            '    End If

                            'End If


                            'Exit Sub

                            MessageBox.Show("ไม่พบตำแหน่งในเอกสารตรวจนับ")
                            Me.txtLocation.Text = ""
                            Me.txtLocation.Focus()
                            Exit Sub

                        Else
                            ' LoadAdjust_DataSourceOnly(Me.Adjust_Index)
                            txtBarcode.Focus()
                        End If
                End Select




            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtBarcode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarcode.KeyDown
        Try
            Dim oBL As New Adjust_Stock
            Dim ods As New DataSet
            Dim odt As New DataTable


            If e.KeyCode = Keys.Enter Then
                'Dong_kk 2011/01/10 ตรวจสอบ barcode
                If Checking_Barcode() Then

                    Dim objDataTable As New DataTable()
                    Dim objDataSet As New DataSet()
                    Dim objBusiness_Layer As New Adjust_Stock
                    Select Case AppMode
                        Case Mode.Offline
                            'Dim objbranch As New ms_branch()
                            'objbranch.SearchData_Click("", "")
                            'objDataTable = objbranch.GetDataTable()
                        Case Mode.Online
                            '  Dim objBusiness_Layer As New WebReference.Business_Layer
                            'get P/O to cbo.
                            If objBusiness_Layer.ChkAdjustByLocationBarcode(Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text, _IsAdjustSku) = False Then
                                'MessageBox.Show("ไม่มี Model : " & Me.txtBarcode.Text & "ในระบบ")
                                ''MsgBox("Dont Have Barcode [" & Me.txtBarcode.Text & "] in This Work")

                                '_MsgConfirm.SetMessage("ต้องการเพิ่ม Model : " & Me.txtBarcode.Text & "ในรายการตรวจนับใช่หริอไม่")
                                '_MsgConfirm.ShowDialog()
                                If W_MSG_Confirm("ต้องการเพิ่มสินค้า : " & Me.txtBarcode.Text & "ในรายการตรวจนับใช่หริอไม่") = Windows.Forms.DialogResult.Yes Then
                                    If txtBarcode.Text = "" Then
                                        MessageBox.Show("กรุณากรอก สินค้า")
                                        Me.txtBarcode.Focus()
                                        Exit Sub
                                    End If

                                    Dim objds As New DataSet
                                    Dim objdt As New DataTable
                                    objBusiness_Layer.GetSKU_DetailByBarcode(Me.txtBarcode.Text)
                                    objdt = objBusiness_Layer.GetDataTable
                                    If objdt.Rows.Count > 0 Then
                                        txtModel.Text = objdt.Rows(0).Item("sku_id")
                                        txtSku_Name.Text = objdt.Rows(0).Item("Product_Name_th")
                                        txtAdjust_Unit.Text = objdt.Rows(0).Item("Package_Id")
                                        isNewadjustLocation = True
                                        Me.txtAdjust_Qty.Focus()
                                    Else
                                        MessageBox.Show("ไม่มี Model : " & Me.txtBarcode.Text & "ในระบบ")
                                        chkClearLocation = False
                                        ClearText()
                                        txtBarcode.Text = ""
                                        txtSku_Name.Text = ""
                                        Me.txtBarcode.Focus()

                                        Exit Sub
                                    End If
                                Else
                                    Me.txtBarcode.Text = ""
                                    Me.txtAdjust_Qty.Focus()
                                End If


                            Else

                                'objDataSet = objBusiness_Layer.GetTagNoByAdjustByLocationBarcode(_objBusiness_User, Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text)
                                objBusiness_Layer.GetAdjustItemLocation_SKU(Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text, _IsAdjustSku)
                                objDataTable = objBusiness_Layer.GetDataTable

                                If objDataTable.Rows.Count > 0 Then
                                    'Me.cboTag.ValueMember = "Tag_No"
                                    'Me.cboTag.DisplayMember = "Tag_No"
                                    'Me.cboTag.DataSource = objDataTable
                                    If txtBarcode.Text <> "" Then
                                        With objDataTable
                                            txtModel.Text = .Rows(0).Item("sku_id")
                                            txtSku_Name.Text = .Rows(0).Item("Product_Name_th")
                                            LoadItemStatus_Adjust(.Rows(0).Item("sku_Index"), Me.txtLocation.Text.Trim)
                                            txtAdjust_Unit.Text = .Rows(0).Item("Package_sku")
                                            'ย้ายไปตอน Save
                                            '_AdjustItemLocation_Index = .Rows(0).Item("AdjustItemLocation_Index")
                                        End With
                                    Else
                                        With objDataTable
                                            txtModel.Text = "NULL"
                                            txtSku_Name.Text = "NULL"
                                            txtAdjust_Unit.Text = "NULL"
                                            txtAdjust_Qty.Text = 0
                                            _AdjustItemLocation_Index = .Rows(0).Item("AdjustItemLocation_Index")
                                        End With
                                    End If

                                End If
                                Me.txtAdjust_Qty.Focus()
                            End If
                    End Select
                End If

            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function Checking_Barcode() As Boolean
        Try
            '===== Checking List ======
            '0. Check Null
            '==========================
            Dim strBarcode As String = Me.txtBarcode.Text.Trim

            '0. Check Null
            If strBarcode = "" Then
                MessageBox.Show("กรุณาป้อน Barcode")
                'MsgBox("Please enter your Barcode.")
                Me.txtBarcode.Text = ""
                Me.txtBarcode.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objDataTable As New DataTable()
            Dim objDataSet As New DataSet()
            Dim objBusiness_Layer As New Adjust_Stock
            If Not IsNumeric(Me.txtAdjust_Qty.Text) Then
                MessageBox.Show("กรุณากรอกจำนวน")
                Exit Sub
            End If

            If isNewadjustLocation = False Then
                'get P/O to cbo.
                'Get _AdjustItemLocation_Index

                'STEP 1 : ใน Location_Alias นั้นไม่ตำแหน่งให้ Update เป็น 0 ให้หมด
                Dim pNoItemLocation_Index As String = ""

                If Me.txtBarcode.Text.Trim = "" Then
                    '1.ถ้าไปที่ Loc นั้นแล้วไม่มีการระบุรายการสินค้า
                    pNoItemLocation_Index = ""
                    pNoItemLocation_Index = objBusiness_Layer.getlocation_index(Me.txtLocation.Text.Trim.Replace("'", "''"))

                    If pNoItemLocation_Index = "" Then
                        Windows.Forms.Cursor.Current = Cursors.Default
                        MessageBox.Show("ไม่พบตำแหน่งจัดเก็บนี้ในฐานข้อมูล")
                        Me.txtLocation.Text = ""
                        Me.txtLocation.Focus()
                        Exit Sub
                    End If
                    If CDbl(Me.txtAdjust_Qty.Text) > 0 Then
                        MessageBox.Show("กรุณาระบุ" & lblAdjust_Qty.Text & "เป็น 0 ถ้าตำแหน่งไม่มีสินค้า")
                        Me.txtAdjust_Qty.Focus()
                        Exit Sub
                    End If


                    ' ''Else
                    '2.ถ้าไปที่ Loc นั้นมีสินค้านั้่นจริง Update จำนวน
                    objBusiness_Layer.GetAdjustItemLocation_SKU_ItemStatus(Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text, cboItemStatus.SelectedValue, _IsAdjustSku)
                    objDataTable = objBusiness_Layer.GetDataTable
                    Me._AdjustItemLocation_Index = objDataTable.Rows(0).Item("AdjustItemLocation_Index")
                Else
                    '2.ถ้าไปที่ Loc นั้นมีสินค้านั้่นจริง Update จำนวน
                    objBusiness_Layer.GetAdjustItemLocation_SKU_ItemStatus(Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text, cboItemStatus.SelectedValue, _IsAdjustSku)
                    objDataTable = objBusiness_Layer.GetDataTable
                    Me._AdjustItemLocation_Index = objDataTable.Rows(0).Item("AdjustItemLocation_Index")
                End If

                If objBusiness_Layer.Update_Adjust_Quantity(Me._AdjustItemLocation_Index, Me._Adjust_Index, CDbl(Me.txtAdjust_Qty.Text), pNoItemLocation_Index) = True Then  ' CDbl(Me.txtSys_Qty.Text) - CDbl(Me.txtAdjust_Qty.Text), 0, 0, 
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย")
                    Me.LoadAdjust_Header()
                    Me.LoadAdjust_Detail()
                    chkClearLocation = True
                    ClearText()
                Else
                    MessageBox.Show("การบันทึกข้อมูลผิดพลาด")
                    'MsgBox("Save Data Fail.")
                End If
            Else
                'เพิ่มสินค้าที่เจอแต่ไม่มีรายการ
                Dim iNewItem As Integer = 0
                objBusiness_Layer.getAdjustItemLocationIndex_NoITem(Me._Adjust_Index, Me.txtLocation.Text, Me.txtBarcode.Text)
                objDataTable = objBusiness_Layer.GetDataTable
                Dim iSeq As Integer = 0
                If objDataTable.Rows.Count > 0 Then
                    Me._AdjustItemLocation_Index = objDataTable.Rows(0).Item("AdjustItemLocation_Index")
                    iSeq = objDataTable.Rows(0).Item("Seq")
                    iNewItem = 0
                Else
                    Me._AdjustItemLocation_Index = ""
                    iNewItem = 1
                End If

                If objBusiness_Layer.insert_Adjust_Quantity(Me._AdjustItemLocation_Index, Me._Adjust_Index, CDbl(Me.txtAdjust_Qty.Text), txtLocation.Text, txtBarcode.Text, iNewItem, Me.cboItemStatus.SelectedValue, _IsAdjustSku, , iSeq) = True Then  ' CDbl(Me.txtSys_Qty.Text) - CDbl(Me.txtAdjust_Qty.Text), 0, 0, 
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย")
                    Me.LoadAdjust_Header()
                    Me.LoadAdjust_Detail()
                    chkClearLocation = True
                    ClearText()
                Else
                    MessageBox.Show("การบันทึกข้อมูลผิดพลาด")
                    'MsgBox("Save Data Fail.")
                End If

            End If

  
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtAdjust_Qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdjust_Qty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.btnSave_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub frmAdjust_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class