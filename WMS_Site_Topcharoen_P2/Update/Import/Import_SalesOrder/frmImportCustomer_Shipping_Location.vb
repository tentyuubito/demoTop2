'Imports System.Data.SqlClient
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
'Imports WMS_STD_Formula.W_Module

Public Class frmImportCustomer_Shipping_Location

    Private _IsProcess As Boolean
    Public Property IsProcess() As Boolean
        Get
            Return _IsProcess
        End Get
        Set(ByVal value As Boolean)
            _IsProcess = value
        End Set
    End Property

    Private _Dt As DataTable
    Public Property DT() As DataTable
        Get
            Return _Dt
        End Get
        Set(ByVal value As DataTable)
            _Dt = value
        End Set
    End Property

    Private Sub frmImportCustomer_Shipping_Location_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oFunction As New W_Language
        oFunction.SwitchLanguage(Me, 2)
        'oFunction.SW_Language_Column(Me, Me.dgvData, 2)
        oFunction = Nothing

        Me.dgvData.AutoGenerateColumns = False
        Me.defaultOnLoad()
        Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub defaultOnLoad()
        Try
            Me.colList = Me.getColumnList()
            Me.ViewData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            Me.IsProcess = True
            Me.ImportData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ViewData()
        Try
            Dim _dt As New DataTable
            _dt = Me.DT
            Me.dgvData.AutoGenerateColumns = True
            Me.dgvData.Columns.Clear()
            Me.btnValidate.Enabled = False
            Me.btnImport.Enabled = False
            If (_dt.Rows.Count > 0) Then
                If (Not _dt.Columns.Contains("Validate")) Then
                    _dt.Columns.Add("Validate", GetType(String))
                End If
                If (Not _dt.Columns.Contains("__Validate")) Then
                    _dt.Columns.Add("__Validate", GetType(Boolean))
                End If
                Me.dgvData.DataSource = _dt
                If (Me.dgvData.Columns.Contains("Validate")) Then
                    Me.dgvData.Columns("Validate").Visible = False
                    Me.dgvData.Columns("Validate").Frozen = False
                End If
                If (Me.dgvData.Columns.Contains("__Validate")) Then
                    Me.dgvData.Columns("__Validate").Visible = False
                End If
                For Each dgvc As DataGridViewColumn In Me.dgvData.Columns
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                Me.btnValidate.Enabled = True
                Me.btnImport.Enabled = False
                'Me.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            Else
                Me.dgvData.DataSource = Nothing
                Me.btnValidate.Enabled = False
                Me.btnImport.Enabled = False
            End If
            _dt.Dispose()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region " Validate "

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            If (Me.dgvData.Rows.Count = 0) Then Exit Sub
            If (Not ValidateColumnIndex(Me.dgvData, Me.colList)) Then Exit Sub
            If (dgvData.Columns.Contains("Validate")) Then
                Me.dgvData.Columns("Validate").DisplayIndex = 0
                Me.dgvData.Columns("Validate").Visible = True
                Me.dgvData.Columns("Validate").Frozen = True
            End If
            Me.ValidateColumnData(Me.dgvData, DirectCast(Me.dgvData.DataSource, DataTable))
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function ValidateColumnIndex(ByVal dgv As DataGridView, ByVal colList As List(Of ColArgs)) As Boolean
        For Each colArgs As ColArgs In colList
            If (colArgs.IsRequireColumn AndAlso Not Me.CheckColumnIndex(colArgs.Column_Index, colArgs.Column_Name_Alias)) Then Return False
        Next
        Return True
    End Function

    Private Function CheckColumnIndex(ByVal Col_Index As Integer, ByVal Col_Name As String) As Boolean
        If dgvData.Columns(Col_Index).Name.ToUpper.Trim() <> Col_Name.ToUpper.Trim() Then
            W_MSG_Information("Column(" & Col_Index + 1 & ") ไม่เท่ากับ " & Col_Name.Trim())
            Return False
        End If
        Return True
    End Function

    Private Sub AddColumnHide(ByVal dgv As DataGridView, ByVal dt As DataTable, ByVal Column_Name As String, ByVal Type As Type)
        If (Not dt.Columns.Contains(Column_Name)) Then
            dt.Columns.Add(Column_Name, Type)
        End If
        If (dgv.Columns.Contains(Column_Name)) Then
            dgv.Columns(Column_Name).Visible = False
        End If
    End Sub

    Private Function ValidateColumnData(ByVal dgv As DataGridView, ByVal dt As DataTable) As Boolean
        Try
            'dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            dgv.DefaultCellStyle.BackColor = Color.White
            dgv.DefaultCellStyle.ForeColor = Color.Black

            Me.AddColumnHide(dgv, dt, "__Customer_Index", GetType(String))
            Me.AddColumnHide(dgv, dt, "__Customer_Shipping_Index", GetType(String))
            Me.AddColumnHide(dgv, dt, "__Province_Index", GetType(String))
            Me.AddColumnHide(dgv, dt, "__District_Index", GetType(String))
            Me.AddColumnHide(dgv, dt, "__Route_Index", GetType(String))
            Me.AddColumnHide(dgv, dt, "__SubRoute_Index", GetType(String))

            ' Values Index
            Dim Default_Customer_Index As String = ""
            Dim Default_Customer_Shipping_Index As String = ""
            Dim Default_Province_Index As String = "0010000000000"
            Dim Default_District_Index As String = "0010000000000"
            Dim Default_Route_Index As String = "0010000000000"
            Dim Default_SubRoute_Index As String = "0010000000000"

            Dim All_Count As Integer = dt.Rows.Count
            Dim Pass_Count As Integer = 0

            Dim Col_Alias As String = ""

            For iRow As Integer = 0 To dt.Rows.Count - 1
                Dim clsSelect As New clsCustomer_Shipping_Location()

                With dt.Rows(iRow)
                    If dt.Columns.Contains("Validate") Then
                        .Item("Validate") = ""
                    End If
                    Dim flag As Boolean = True

                    ' BGN ตรวจช่องที่เป็นค่าว่าง IsRequireData
                    For Each colArgs As ColArgs In Me.colList
                        If (colArgs.IsRequireData) Then
                            If (dt.Columns.Contains(colArgs.Column_Name_Alias) And Not colArgs.Column_Name_Alias = Nothing) Then
                                Dim IsHasData As Boolean = False
                                Select Case colArgs.ColumnType
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.String
                                        IsHasData = (Not .Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "") = Nothing)
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.Date
                                        IsHasData = IsDate(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.Integer
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso Math.Round(CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))) / 1 = CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.Decimal
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.IntegerPositive
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso Math.Round(CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))) / 1 = CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) >= 0
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.DecimalPositive
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) >= 0
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.IntegerPositiveNotZero
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso Math.Round(CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", ""))) / 1 = CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) > 0
                                    Case WMS_Site_Topcharoen_P2.ColArgs.ColArgsType.DecimalPositiveNotZero
                                        IsHasData = IsNumeric(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) AndAlso CDec(.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "")) > 0
                                    Case Else
                                        IsHasData = (.Item(colArgs.Column_Name_Alias).ToString.Trim.Replace("'", "") = "")
                                End Select
                                If (Not IsHasData) Then
                                    flag = False
                                    Me.HighListGridView(dgv, iRow, colArgs.Column_Name_Alias)
                                End If
                            End If
                        End If
                    Next

                    ' BGN กรณีอื่นๆ
                    Col_Alias = Me.getColumn_Name_Alias("")
                    If (dt.Columns.Contains(Col_Alias) And Not Col_Alias = Nothing) Then
                        If (.Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                            flag = False
                            Me.HighListGridView(dgv, iRow, Col_Alias)
                        End If
                    End If
                    ' END กรณีอื่นๆ

                    If Not flag Then
                        If (dt.Columns.Contains("Validate")) Then
                            .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & "ข้อมูลไม่ถูกต้อง"
                        End If
                        Continue For
                    End If
                    ' END ตรวจช่องที่เป็นค่าว่าง IsRequireData

                    ' ใช้ Customer_Id ค้นหา Customer_Index
                    Dim Customer_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("Customer_Id")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 Customer_Index from ms_Customer where status_id<>-1 and Customer_Id='{0}' ", .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            Customer_Index = Temp_Index
                        End If
                        If (Customer_Index = Nothing) Then
                            Customer_Index = ""
                            If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                                If (dt.Columns.Contains("Validate")) Then
                                    .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                                End If
                                Me.HighListGridView(dgv, iRow, Col_Alias)
                                Continue For
                            Else
                                Customer_Index = Default_Customer_Index
                            End If
                        End If
                    Else
                        Customer_Index = Default_Customer_Index
                    End If

                    ' ใช้ Customer_Shipping_Id ค้นหา Customer_Shipping_Index
                    Dim Customer_Shipping_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("Customer_Shipping_Id")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        'Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 Customer_Shipping_Index from ms_Customer_Shipping where status_id<>-1 and Customer_Index='{0}' and Str1='{1}' ", Customer_Index, .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 Customer_Shipping_Index from ms_Customer_Shipping where status_id<>-1 and Str1='{1}' ", Customer_Index, .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            Customer_Shipping_Index = Temp_Index
                        End If
                        If (Customer_Shipping_Index = Nothing) Then
                            Customer_Shipping_Index = ""
                            If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                                If (dt.Columns.Contains("Validate")) Then
                                    .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                                End If
                                Me.HighListGridView(dgv, iRow, Col_Alias)
                                Continue For
                            Else
                                Customer_Shipping_Index = Default_Customer_Shipping_Index
                            End If
                        End If
                    Else
                        Customer_Shipping_Index = Default_Customer_Shipping_Index
                    End If

                    ' ตรวจสอบ Customer_Shipping_Location_Id ซ้ำ
                    Dim Customer_Shipping_Location_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("Customer_Shipping_Location_Id")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Customer_Shipping_Location_Index = clsSelect.SelectIndex(String.Format(" select top 1 Customer_Shipping_Location_Index from ms_Customer_Shipping_Location where status_id<>-1 and Customer_Shipping_Index='{0}' and Customer_Shipping_Location_Id='{1}' ", Customer_Shipping_Index, .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Customer_Shipping_Location_Index = Nothing) Then
                            If (dt.Columns.Contains("Validate")) Then
                                .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("มี{0}นี้แล้ว", Col_Alias)
                            End If
                            Me.HighListGridView(dgv, iRow, Col_Alias)
                            Continue For
                        End If
                    End If

                    ' ใช้ Province_Desc ค้นหา Province_Index
                    Dim Province_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("Province_Desc")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 Province_Index from ms_Province where status_id<>-1 and (ltrim(rtrim(Province))='{0}' or 'จ.'+ltrim(rtrim(Province))='{0}') ", .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            Province_Index = Temp_Index
                        End If
                        If (Province_Index = Nothing) Then
                            'Province_Index = ""
                            'If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                            '    If (dt.Columns.Contains("Validate")) Then
                            '        .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                            '    End If
                            '    Me.HighListGridView(dgv, iRow, Col_Alias)
                            '    Continue For
                            'Else
                            Province_Index = Default_Province_Index
                            'End If
                        End If
                    Else
                        Province_Index = Default_Province_Index
                    End If

                    ' ใช้ District_Desc ค้นหา District_Index
                    Dim District_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("District_Desc")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 District_Index from ms_District where status_id<>-1 and Province_Index='{0}' and (ltrim(rtrim(District))='{1}' or 'อ.'+ltrim(rtrim(District))='{1}' or 'เขต'+ltrim(rtrim(District))='{1}') ", Province_Index, .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            District_Index = Temp_Index
                        End If
                        If (District_Index = Nothing) Then
                            'District_Index = ""
                            'If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                            '    If (dt.Columns.Contains("Validate")) Then
                            '        .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                            '    End If
                            '    Me.HighListGridView(dgv, iRow, Col_Alias)
                            '    Continue For
                            'Else
                            District_Index = Default_District_Index
                            'End If
                        End If
                    Else
                        District_Index = Default_District_Index
                    End If

                    ' ใช้ Route_Id ค้นหา Route_Index
                    Dim Route_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("Route_Id")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 Route_Index from ms_Route where status_id<>-1 and ltrim(rtrim(Route_No))='{0}' ", .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            Route_Index = Temp_Index
                        End If
                        If (Route_Index = Nothing) Then
                            Route_Index = ""
                            If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                                If (dt.Columns.Contains("Validate")) Then
                                    .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                                End If
                                Me.HighListGridView(dgv, iRow, Col_Alias)
                                Continue For
                            Else
                                Route_Index = Default_Route_Index
                            End If
                        End If
                    Else
                        Route_Index = Default_Route_Index
                    End If

                    ' ใช้ SubRoute_Id ค้นหา SubRoute_Index
                    Dim SubRoute_Index As String = Nothing
                    Col_Alias = Me.getColumn_Name_Alias("SubRoute_Id")
                    If (dt.Columns.Contains(Col_Alias)) Then
                        Dim Temp_Index As String = clsSelect.SelectIndex(String.Format(" select top 1 SubRoute_Index from ms_SubRoute where status_id<>-1 and Route_Index='{0}' and ltrim(rtrim(SubRoute_No))='{1}' ", Route_Index, .Item(Col_Alias).ToString.Trim.Replace("'", "")))
                        If (Not Temp_Index = Nothing) Then
                            SubRoute_Index = Temp_Index
                        End If
                        If (SubRoute_Index = Nothing) Then
                            SubRoute_Index = ""
                            If (Not .Item(Col_Alias).ToString.Trim.Replace("'", "") = "") Then
                                If (dt.Columns.Contains("Validate")) Then
                                    .Item("Validate") = IIf(.Item("Validate") = Nothing, "", .Item("Validate") & ", ") & String.Format("ไม่พบ{0}", Col_Alias)
                                End If
                                Me.HighListGridView(dgv, iRow, Col_Alias)
                                Continue For
                            Else
                                SubRoute_Index = Default_SubRoute_Index
                            End If
                        End If
                    Else
                        SubRoute_Index = Default_SubRoute_Index
                    End If

                    .Item("__Customer_Index") = Customer_Index
                    .Item("__Customer_Shipping_Index") = Customer_Shipping_Index
                    .Item("__Province_Index") = Province_Index
                    .Item("__District_Index") = District_Index
                    .Item("__Route_Index") = Route_Index
                    .Item("__SubRoute_Index") = SubRoute_Index

                    ' Validate Passed
                    If (dt.Columns.Contains("__Validate")) Then
                        .Item("__Validate") = True
                    End If
                    Me.HighlightValidate(dgv, iRow)
                    Pass_Count += 1
                End With
            Next
            'dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            If Pass_Count = All_Count Then
                Me.btnImport.Enabled = True
            Else
                Me.btnImport.Enabled = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub HighListGridView(ByVal dgv As DataGridView, ByVal row As Integer, ByVal cell As String)
        Try
            dgv.Rows(row).DefaultCellStyle.BackColor = Color.Silver
            dgv.Rows(row).Cells(cell).Style.BackColor = Color.Pink
            If dgv.Columns.Contains("Validate") Then
                dgv.Rows(row).Cells("Validate").Style.BackColor = Color.WhiteSmoke
                dgv.Rows(row).Cells("Validate").Style.ForeColor = Color.Red
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HighlightValidate(ByVal dgv As DataGridView, ByVal row As Integer)
        Try
            If dgv.Columns.Contains("Validate") Then
                dgv.Rows(row).Cells("Validate").Value = "Passed"
                dgv.Rows(row).Cells("Validate").Style.BackColor = Color.GreenYellow
                dgv.Rows(row).Cells("Validate").Style.ForeColor = Color.Black
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " Import "

    Private Sub ImportData()
        Try
            Me.btnValidate_Click(Nothing, Nothing)
            If Not Me.btnImport.Enabled Then
                Exit Sub
            End If
            If dgvData.Rows.Count = 0 Then Exit Sub

            If W_MSG_Confirm("ต้องการนำเข้าข้อมูลหรือไม่?") = Windows.Forms.DialogResult.Yes Then
                Me.btnValidate.Enabled = False
                Me.btnImport.Enabled = False
                Application.DoEvents()
                If Me.Import_Data(DirectCast(dgvData.DataSource, DataTable)) Then
                    W_MSG_Information("นำเข้าข้อมูลเสร็จสิ้น")
                Else
                    W_MSG_Error("เกิดข้อผิดพลาด กรุณาลองใหม่")
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function Import_Data(ByVal dt As DataTable) As Boolean
        Try
            dt.AcceptChanges()
            If dt.Select(" __Validate is null ").Length > 0 Then Return False

            Dim dtTemp As DataTable = dt.Copy
            If Not New clsCustomer_Shipping_Location().ImportData(dtTemp, Me.colList) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Private colList As List(Of ColArgs)

    Private Function getColumnList() As List(Of ColArgs)
        Dim colList As New List(Of ColArgs)
        Try
            colList.Clear()
            colList.Add(New ColArgs(0, "Customer_Id", "รหัสเจ้าของสินค้า", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(1, "Customer_Shipping_Id", "Sold To Id", ColArgs.ColArgsType.String, True, True))
            colList.Add(New ColArgs(2, "Customer_Shipping_Location_Id", "Ship To Id", ColArgs.ColArgsType.String, True, True))
            colList.Add(New ColArgs(3, "Customer_Shipping_Location_Desc", "Ship To Name", ColArgs.ColArgsType.String, True, True))
            colList.Add(New ColArgs(4, "Customer_Shipping_Location_Address", "Address", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(5, "SubDistrict_Desc", "ตำบล", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(6, "District_Desc", "อำเภอ", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(7, "Province_Desc", "จังหวัด", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(8, "Postcode", "Postcode", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(9, "Tel", "Tel", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(10, "Fax", "Fax", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(11, "Mobile", "Mobile", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(12, "Email", "Email", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(13, "Remark", "หมายเหตุ", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(14, "Contact_Person1", "ชื่อผู้ติดต่อ 1", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(15, "Contact_Person2", "ชื่อผู้ติดต่อ 2", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(16, "Contact_Person3", "ชื่อผู้ติดต่อ 3", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(17, "Route_Id", "Route_Id", ColArgs.ColArgsType.String, False, True))
            colList.Add(New ColArgs(18, "SubRoute_Id", "SubRoute_Id", ColArgs.ColArgsType.String, False, True))
        Catch ex As Exception
            Throw ex
        End Try
        Return colList
    End Function

    Private Function getColumn_Name_Alias(ByVal Column_Name As String) As String
        Try
            For Each colArgs As ColArgs In Me.colList
                If (colArgs.Column_Name = Column_Name) Then
                    Return colArgs.Column_Name_Alias
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return ""
    End Function

End Class