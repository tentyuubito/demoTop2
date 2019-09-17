Imports System.IO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master

Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Data.OleDb

Public Class frmExcelDroppiont

    Dim _Operation_Type As Type_Import
    Public Enum Type_Import
        DROPPIONT = 0
        CONFIRM_Y = 1
        CONFIRM_Z = 2
    End Enum


    Public Sub New(ByVal Operation_Type As Type_Import)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _Operation_Type = Operation_Type

    End Sub


    Private Sub frmImport_DO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Dim appSet As New Configuration.AppSettingsReader()
            'WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))

            Dim oFunction As New WMS_STD_Master.W_Language

            'SwitchLanguage
            'oFunction.SwitchLanguage(Me)

            '===============================
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")


            Me.btnImport.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnDeleteFailAll.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub



    Private Sub frmExcelDroppiont_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboWorkSheet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWorkSheet.SelectedIndexChanged
        grdPreviewData.DataSource = Nothing
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try

            Dim oImport_SO As New bl_Import_SO
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                Dim objWS As DataTable = New DataTable
                Dim ExcelConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Me.txtFilePath.Text & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
                Dim oConnSource As New OleDbConnection(ExcelConnString)
                Dim odaSource As New OleDbDataAdapter
                '=============
                oConnSource.Open()
                objWS = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                oConnSource.Close()
                With cboWorkSheet
                    .DataSource = objWS
                    .DisplayMember = "TABLE_NAME"
                    .ValueMember = "TABLE_NAME"
                End With
                cboWorkSheet.SelectedIndex = 0

                Me.LoadData_Excel()
                '====================
            Else
                Exit Sub
            End If

            Me.SetnumRows()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub
    Private Sub BntLoaddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntLoaddata.Click
        Try

            Me.LoadData_Excel()
            Me.SetnumRows()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub LoadData_Excel()
        Try
            If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
                Exit Sub
            End If
            '===============
            'Me.grdPreviewData.DataSource = Me.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

            Dim strWorkSheet As String = Me.cboWorkSheet.Text

            Dim odtTemp As New DataTable
            Dim ExcelConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Me.txtFilePath.Text & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
            Dim oConnSource As New OleDbConnection(ExcelConnString)
            Dim odaSource As New OleDbDataAdapter
            With odaSource
                strWorkSheet = Me.cboWorkSheet.Text.ToString.Replace("$", "")

                Dim sQuery As String = ""
                Select Case _Operation_Type
                    Case Type_Import.DROPPIONT
                        sQuery = " SELECT 0 as Line_No,'' AS Check_Result"
                        sQuery &= " ,'' as trandp_droppoint"
                        sQuery &= " ,refer_code		 "
                        sQuery &= " ,document_type	 "
                        sQuery &= " ,transport_group"
                        sQuery &= " ,transport_type	 "
                        sQuery &= " ,source_depart	 "
                        sQuery &= " ,branch_code	 "
                        sQuery &= " ,shipto_depart	 "
                        sQuery &= " ,shipto_code	 "
                        sQuery &= " ,tranf_remark	 "
                        sQuery &= " ,employee_code	 "
                        sQuery &= " ,note	 "

                        sQuery &= " FROM [" & strWorkSheet & "$] "
                        sQuery &= " GROUP BY "
                        sQuery &= " document_type	 "
                        sQuery &= " ,transport_group"
                        sQuery &= " ,transport_type	 "
                        sQuery &= " ,source_depart	 "
                        sQuery &= " ,branch_code	 "
                        sQuery &= " ,shipto_depart	 "
                        sQuery &= " ,shipto_code	 "
                        sQuery &= " ,tranf_remark	 "
                        sQuery &= " ,employee_code	 "
                        sQuery &= " ,note	 "
                        sQuery &= " ,refer_code		 "

                        .SelectCommand = New OleDbCommand(sQuery, oConnSource)
                        '===================
                        .Fill(odtTemp)

                        If Not odtTemp.Columns.Contains("trandp_vehicle_type") Then odtTemp.Columns.Add("trandp_vehicle_type", GetType(String))
                        If Not odtTemp.Columns.Contains("trandp_droppoint") Then odtTemp.Columns.Add("trandp_droppoint", GetType(String))
                        If Not odtTemp.Columns.Contains("trandp_route") Then odtTemp.Columns.Add("trandp_route", GetType(String))
                        If Not odtTemp.Columns.Contains("Droppoint_Index") Then odtTemp.Columns.Add("Droppoint_Index", GetType(String))

                        If Not odtTemp.Columns.Contains("trandp_route") Then odtTemp.Columns.Add("trandp_route", GetType(String))
                        If Not odtTemp.Columns.Contains("trandp_text") Then odtTemp.Columns.Add("trandp_text", GetType(String))
                        If Not odtTemp.Columns.Contains("trandp_person") Then odtTemp.Columns.Add("trandp_person", GetType(String))

                    Case Type_Import.CONFIRM_Y
                        sQuery = " SELECT 0 as Line_No,'' AS Check_Result,'' AS OMS,'' AS Remark"
                        sQuery &= " ,[SalesOrder_No],[Type of R/L],[เลขที่ใบคุมของ],[Invoice],[Item Bar],[Job_No]"
                        sQuery &= " FROM [" & strWorkSheet & "$] "
                        sQuery &= " WHERE [SalesOrder_No] <> '' and [Item Bar] <> ''"
                        sQuery &= " GROUP BY [SalesOrder_No],[Type of R/L],[เลขที่ใบคุมของ],[Invoice],[Item Bar],[Job_No]"
                        .SelectCommand = New OleDbCommand(sQuery, oConnSource)
                        '===================
                        .Fill(odtTemp)



                    Case Type_Import.CONFIRM_Z
                        sQuery = " SELECT 0 as Line_No,'' AS Check_Result,'' AS OMS,'' AS Remark"
                        sQuery &= " ,[SalesOrder_No],[เลขที่ใบคุมของ],[Invoice],[Job_No]"
                        sQuery &= " FROM [" & strWorkSheet & "$] "
                        sQuery &= " WHERE [SalesOrder_No] <> ''"
                        sQuery &= " GROUP BY [SalesOrder_No],[เลขที่ใบคุมของ],[Invoice],[Job_No]"
                        .SelectCommand = New OleDbCommand(sQuery, oConnSource)
                        '===================
                        .Fill(odtTemp)


                End Select

 

            End With

            If Not odtTemp.Columns.Contains("SalesOrder_Index") Then odtTemp.Columns.Add("SalesOrder_Index", GetType(String))

            Me.grdPreviewData.DataSource = odtTemp

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            Else
                Me.grdPreviewData.Columns("Line_No").Visible = False
            End If

            '===============

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdPreviewData.Rows.Count = 0 Then Exit Sub
            Me.grdPreviewData.Rows.RemoveAt(Me.grdPreviewData.CurrentRow.Index)
            W_MSG_Information("ลบรายการเรียบร้อยแล้ว")
            Me.SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnDeleteFailAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFailAll.Click
        Try
            If Me.grdPreviewData.Rows.Count = 0 Then Exit Sub
            Dim drArrFail() As DataRow = CType(Me.grdPreviewData.DataSource, DataTable).Select("Check_Result <> 'OK'")
            If drArrFail.Length = 0 Then
                W_MSG_Information("ไม่พบรายการผิดพลาด")
                Exit Sub
            Else
                For Each drDelete As DataRow In drArrFail
                    CType(Me.grdPreviewData.DataSource, DataTable).Rows.Remove(drDelete)
                Next
            End If

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            W_MSG_Information("ลบรายการเรียบร้อยแล้ว")
            Me.btnImport.Enabled = True
            Me.SetnumRows()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = Me.grdPreviewData.Rows.Count
        If numRows > 0 Then
            Dim drArrFail() As DataRow = CType(Me.grdPreviewData.DataSource, DataTable).Select("Check_Result <> 'OK'")
            If drArrFail.Length > 0 Then
                lblCountRows.Text = numRows & " รายการ " & " ผิดพลาด " & drArrFail.Length & " รายการ "
                Me.btnValidate.Enabled = True
                Me.btnImport.Enabled = False
                Me.btnDelete.Enabled = True
                Me.btnDeleteFailAll.Enabled = True
            Else
                lblCountRows.Text = numRows & " รายการ " & " ผิดพลาด 0 รายการ "

                Me.btnValidate.Enabled = True
                Me.btnImport.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnDeleteFailAll.Enabled = True
            End If

        Else
            lblCountRows.Text = "ไม่พบรายการ"


            Me.btnValidate.Enabled = False
            Me.btnImport.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnDeleteFailAll.Enabled = False

        End If



        'Set color
        For i As Integer = 0 To Me.grdPreviewData.RowCount - 1
            If Me.grdPreviewData.Rows(i).Cells("Check_Result").Value.ToString <> "OK" Then
                Me.grdPreviewData.Rows(i).Cells("Check_Result").Style.BackColor = Color.Red
                Select Case _Operation_Type
                    Case Type_Import.DROPPIONT
                        Me.grdPreviewData.Rows(i).Cells("trandp_droppoint").Style.BackColor = Color.Red
                        case Type_Import.CONFIRM_Y,Type_Import.CONFIRM_Z
                End Select
            Else
                Me.grdPreviewData.Rows(i).Cells("Check_Result").Style.BackColor = Color.YellowGreen
                Select Case _Operation_Type
                    Case Type_Import.DROPPIONT
                        Me.grdPreviewData.Rows(i).Cells("trandp_droppoint").Style.BackColor = Color.GreenYellow
                End Select
            End If

            Select Case _Operation_Type
                Case Type_Import.CONFIRM_Y, Type_Import.CONFIRM_Z
                    If Me.grdPreviewData.Rows(i).Cells("OMS").Value.ToString <> "Y" Then
                        Me.grdPreviewData.Rows(i).Cells("OMS").Style.BackColor = Color.Red
                    Else
                        Me.grdPreviewData.Rows(i).Cells("OMS").Style.BackColor = Color.GreenYellow
                    End If
            End Select
          
        Next

    End Sub


    Private Function Validate_droppoint() As Boolean
        Try

            Dim i As Integer
            Dim oImport_SO As New bl_Import_SO
            Dim strResult As String = "OK"
            Dim dtQuery As New DataTable
            Dim xDb As New DBType_SQLServer
            Dim xQuery As String = ""
            Dim Errors As String = ""

            xQuery = "select tranf_drop_point.*"
            xQuery &= " ,c.Customer_Shipping_Location_Index as Droppoint_Index,c.Customer_Shipping_Location_Id as Droppoint_Id,c.Shipping_Location_Name as Droppoint_Name"
            xQuery &= " from tranf_drop_point"
            xQuery &= " left outer join ms_Customer_Shipping_Location c on c.Customer_Shipping_Location_Id = tranf_drop_point.trandp_droppoint"
            xQuery &= " where 1=1 "

            Dim dtDroppoint As New DataTable
            Dim drDroppoint() As DataRow
            dtDroppoint = xDb.DBExeQuery(xQuery)

            For i = 0 To grdPreviewData.Rows.Count - 1
                With grdPreviewData.Rows(i)
                    .Cells("Line_No").Value = i + 1
                    If String.IsNullOrEmpty(.Cells("refer_code").Value) Then
                        Errors = "refer_code ยังไม่มีในระบบ"
                        .Cells("Check_Result").Value = Errors
                        Continue For
                    Else
                        xQuery = String.Format("select * from tb_SalesOrder where SalesOrder_No = '{0}' and status <> -1", .Cells("refer_code").Value.ToString)
                        dtQuery = xDb.DBExeQuery(xQuery)
                        If dtQuery.Rows.Count = 0 Then
                            Errors = .Cells("refer_code").Value.ToString & " refer_code ยังไม่มีในระบบ"
                            .Cells("Check_Result").Value = Errors
                            Continue For
                        End If

                        .Cells("SalesOrder_Index").Value = dtQuery.Rows(0).Item("SalesOrder_Index").ToString

                        'xQuery = "select trandp_vehicle_type,trandp_droppoint,trandp_route,trandp_text,trandp_person"
                        'xQuery &= " ,c.Customer_Shipping_Location_Index as Droppoint_Index,c.Customer_Shipping_Location_Id as Droppoint_Id,c.Shipping_Location_Name as Droppoint_Name"
                        'xQuery &= " from tranf_drop_point"
                        'xQuery &= " left outer join ms_Customer_Shipping_Location c on c.Customer_Shipping_Location_Id = tranf_drop_point.trandp_droppoint"
                        'xQuery &= " where 1=1 "
                        'xQuery &= String.Format(" AND trandp_branch = '{0}'", .Cells("shipto_code").Value.ToString)
                        'xQuery &= String.Format(" AND trandp_group = '{0}'", .Cells("transport_group").Value.ToString)
                        'xQuery &= String.Format(" AND trandp_type = '{0}'", .Cells("transport_type").Value.ToString)

                        'dtQuery = xDb.DBExeQuery(xQuery)
                        'If dtQuery.Rows.Count > 0 Then

                        Dim strCheckdrop As String = ""
                        strCheckdrop = String.Format("trandp_branch = '{0}' ", .Cells("shipto_code").Value.ToString.Trim)
                        drDroppoint = dtDroppoint.Select(strCheckdrop)
                        If drDroppoint.Length = 0 Then
                            Errors = .Cells("shipto_code").Value.ToString & " ไม่พบ shipto_code ในสูตร"
                            .Cells("Check_Result").Value = Errors
                            Continue For
                        End If
                        strCheckdrop = String.Format("trandp_group = '{0}' ", .Cells("transport_group").Value.ToString.Trim)
                        drDroppoint = dtDroppoint.Select(strCheckdrop)
                        If drDroppoint.Length = 0 Then
                            Errors = .Cells("transport_group").Value.ToString & " ไม่พบ transport_group ในสูตร"
                            .Cells("Check_Result").Value = Errors
                            Continue For
                        End If
                        strCheckdrop = String.Format("trandp_type = '{0}' ", .Cells("transport_type").Value.ToString.Trim)
                        drDroppoint = dtDroppoint.Select(strCheckdrop)
                        If drDroppoint.Length = 0 Then
                            Errors = .Cells("transport_type").Value.ToString & " ไม่พบ transport_type ในสูตร"
                            .Cells("Check_Result").Value = Errors
                            Continue For
                        End If

                        strCheckdrop = String.Format("trandp_branch = '{0}' ", .Cells("shipto_code").Value.ToString.Trim)
                        strCheckdrop &= String.Format(" and trandp_group = '{0}' ", .Cells("transport_group").Value.ToString.Trim)
                        strCheckdrop &= String.Format(" and trandp_type = '{0}' ", .Cells("transport_type").Value.ToString.Trim)
                        drDroppoint = dtDroppoint.Select(strCheckdrop)
                        If drDroppoint.Length > 0 Then
                            .Cells("Droppoint_Index").Value = drDroppoint(0)("Droppoint_Index").ToString
                            .Cells("trandp_droppoint").Value = drDroppoint(0)("trandp_droppoint").ToString

                            .Cells("trandp_vehicle_type").Value = drDroppoint(0)("trandp_vehicle_type").ToString
                            .Cells("trandp_route").Value = drDroppoint(0)("trandp_route").ToString
                            .Cells("trandp_text").Value = drDroppoint(0)("trandp_text").ToString
                            .Cells("trandp_person").Value = drDroppoint(0)("trandp_person").ToString


                            If drDroppoint(0)("trandp_vehicle_type").ToString = "" Then .Cells("trandp_vehicle_type").Style.BackColor = Color.Red
                            If drDroppoint(0)("Droppoint_Index").ToString = "" Then .Cells("trandp_droppoint").Style.BackColor = Color.Red
                            If drDroppoint(0)("trandp_route").ToString = "" Then .Cells("trandp_route").Style.BackColor = Color.Red
                        Else
                            Errors = .Cells("refer_code").Value.ToString & " refer_code ไม่มี Drop piont"
                            .Cells("Check_Result").Value = Errors
                            Continue For
                        End If

                    End If
                    .Cells("Check_Result").Value = "OK"
                End With
            Next

            If Errors <> "" Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function Validate_YZ() As Boolean
        Try

            Dim i As Integer
            Dim oImport_SO As New bl_Import_SO
            Dim strResult As String = "OK"
            Dim dtQuery As New DataTable
            Dim xDb As New DBType_SQLServer
            Dim xQuery As String = ""
            Dim Errors As String = ""

            For i = 0 To grdPreviewData.Rows.Count - 1
                With grdPreviewData.Rows(i)
                    .Cells("Line_No").Value = i + 1
                    If String.IsNullOrEmpty(.Cells("SalesOrder_No").Value.ToString) Then
                        Errors = "SalesOrder_No ยังไม่มีในระบบ"
                        .Cells("Check_Result").Value = Errors
                        .Cells("Check_Result").Style.BackColor = Color.Red
                        Continue For
                    Else
                        xQuery = String.Format("select ISNULL(ProveYZ,0) as Prove,* from tb_SalesOrder where status <> -1 and SalesOrder_No = '{0}'", .Cells("SalesOrder_No").Value.ToString)
                        dtQuery = xDb.DBExeQuery(xQuery)
                        If dtQuery.Rows.Count = 0 Then
                            Errors = .Cells("SalesOrder_No").Value.ToString & " SalesOrder_No ยังไม่มีในระบบ"
                            .Cells("Check_Result").Value = Errors
                            .Cells("Check_Result").Style.BackColor = Color.Red
                            Continue For
                        End If

                        .Cells("SalesOrder_Index").Value = dtQuery.Rows(0).Item("SalesOrder_Index").ToString
                        .Cells("OMS").Value = "N"
                        If dtQuery.Rows(0).Item("Prove").ToString = "1" Then
                            .Cells("OMS").Value = "Y"
                        End If


                        xQuery = String.Format(" select top 1 (case when isnull(Barcode_GROUP,'') <> '' then Barcode_GROUP else Barcode_BAG END) BarcodePacking   from tb_SalesOrderPackingItem where SalesOrder_Index = '{0}' ", dtQuery.Rows(0)("SalesOrder_Index").ToString)
                        dtQuery = xDb.DBExeQuery(xQuery)
                        If dtQuery.Rows.Count > 0 Then
                            'Errors = .Cells("SalesOrder_No").Value.ToString & " SalesOrder_No มีการออกใบคุมของแล้ว เลขที่ : " & dtQuery.Rows(0)("BarcodePacking").ToString
                            .Cells("Remark").Value = .Cells("SalesOrder_No").Value.ToString & " มีการออกใบคุมของแล้ว เลขที่ : " & dtQuery.Rows(0)("BarcodePacking").ToString
                            .Cells("Check_Result").Style.BackColor = Color.YellowGreen
                            'Continue For
                        End If

                    End If

                    Select Case _Operation_Type
                        Case Type_Import.CONFIRM_Y

                            If String.IsNullOrEmpty(.Cells("Item Bar").Value.ToString) Then
                                Errors = "Item Bar เป็นค่าว่าง"
                                .Cells("Check_Result").Value = Errors
                                .Cells("Check_Result").Style.BackColor = Color.Red
                                Continue For
                            End If

                            If String.IsNullOrEmpty(.Cells("Type of R/L").Value.ToString) Then
                                Errors = "Type of R/L เป็นค่าว่าง"
                                .Cells("Check_Result").Value = Errors
                                .Cells("Check_Result").Style.BackColor = Color.Red
                                Continue For
                            End If

                        Case Type_Import.CONFIRM_Z
                            If String.IsNullOrEmpty(.Cells("Job_No").Value.ToString) Then
                                Errors = "Job_No เป็นค่าว่าง"
                                .Cells("Check_Result").Value = Errors
                                .Cells("Check_Result").Style.BackColor = Color.Red
                                Continue For
                            End If

                    End Select

                    .Cells("Check_Result").Value = "OK"
                End With
            Next

            If Errors <> "" Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            Dim _Validate As Boolean = False
            Me.ProgressBar1.Enabled = True
            Me.pnlProgressBar.Show()
            Me.groupimport.Enabled = False
            Me.PanelOrderItemButton.Enabled = False


            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()

            Me.grdPreviewData.Columns("Line_No").Visible = True
            Select Case _Operation_Type
                Case Type_Import.DROPPIONT
                    _Validate = Me.Validate_droppoint()
                Case Type_Import.CONFIRM_Y, Type_Import.CONFIRM_Z
                    _Validate = Me.Validate_YZ()
                    If _Validate Then
                        btnProvess.Enabled = True
                    Else
                        btnProvess.Enabled = False
                    End If
            End Select

            If _Validate Then
                Me.groupimport.Enabled = True
                Me.pnlProgressBar.Hide()
                Me.ProgressBar1.Enabled = False
                Me.PanelOrderItemButton.Enabled = True
                Me.btnImport.Enabled = True
            Else
                Me.groupimport.Enabled = True
                Me.pnlProgressBar.Hide()
                Me.ProgressBar1.Enabled = False
                Me.PanelOrderItemButton.Enabled = True
            End If

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()



            W_MSG_Information("ตรวจสอบข้อมูลแล้ว")
        Catch ex As Exception
            Me.groupimport.Enabled = True
            Me.pnlProgressBar.Hide()
            Me.ProgressBar1.Enabled = False
            Me.PanelOrderItemButton.Enabled = True

            W_MSG_Error(ex.Message)
        Finally
            Me.SetnumRows()
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            Me.ProgressBar1.Enabled = True
            Me.pnlProgressBar.Show()

            Me.groupimport.Enabled = False
            Me.PanelOrderItemButton.Enabled = False
            Dim _Validate As Boolean = False
            'Update wms.
            Select Case _Operation_Type
                Case Type_Import.DROPPIONT
                    Dim xDb As New DBType_SQLServer
                    Dim xQuery As String = ""
                    For i As Integer = 0 To Me.grdPreviewData.RowCount - 1
                        Try
                            With Me.grdPreviewData.Rows(i)
                                xQuery = " update tb_SalesOrder SET "
                                xQuery &= String.Format(" OMS_document_type = '{0}'", .Cells("document_type").Value.ToString)
                                xQuery &= String.Format(" ,OMS_transport_group = '{0}'", .Cells("transport_group").Value.ToString)
                                xQuery &= String.Format(" ,OMS_transport_type = '{0}'", .Cells("transport_type").Value.ToString)
                                xQuery &= String.Format(" ,OMS_source_depart = '{0}'", .Cells("source_depart").Value.ToString)
                                xQuery &= String.Format(" ,OMS_Droppoint_Index = '{0}'", .Cells("Droppoint_Index").Value.ToString)

                                xQuery &= String.Format(" ,OMS_trandp_vehicle_type = '{0}'", .Cells("trandp_vehicle_type").Value.ToString)
                                xQuery &= String.Format(" ,OMS_trandp_route = '{0}'", .Cells("trandp_route").Value.ToString)
                                xQuery &= String.Format(" ,OMS_trandp_text = '{0}'", .Cells("trandp_text").Value.ToString)
                                xQuery &= String.Format(" ,OMS_trandp_person = '{0}'", .Cells("trandp_person").Value.ToString)
                                xQuery &= String.Format(" ,OMS_tranf_remark = '{0}'", .Cells("tranf_remark").Value.ToString)
                                xQuery &= String.Format(" ,OMS_employee_code = '{0}'", .Cells("employee_code").Value.ToString)
                                xQuery &= String.Format(" ,OMS_note = '{0}'", .Cells("note").Value.ToString)
                                xQuery &= String.Format(" ,OMS_last_update_by = '{0}'", WV_User_Index)
                                xQuery &= String.Format(" ,OMS_last_update_date = getdate()", "")

                                xQuery &= String.Format(" ,Urgent_Id = '{0}'", .Cells("transport_type").Value.ToString)

                                'xQuery &= String.Format(" ,OMS_update_count = (select isnull(SO.OMS_update_count,0)+1 from tb_SalesOrder SO where SO.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index)", "")
                                'xQuery &= String.Format(" where SalesOrder_No = '{0}' and Status <> -1", .Cells("refer_code").Value.ToString)

                                xQuery &= String.Format(" ,OMS_update_count = (select isnull(SO.OMS_update_count,0)+1 from tb_SalesOrder SO where SO.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index)", "")
                                xQuery &= String.Format(" where SalesOrder_Index = '{0}'", .Cells("SalesOrder_Index").Value.ToString)

                                xDb.DBExeNonQuery(xQuery)

                                ' --- STEP : Sy_Audit_Log
                                Dim obj_cls As New cls_syAditlog
                                obj_cls.Process_ID = 10
                                obj_cls.Description = String.Format("user name : {0} ,import excel update drop point : {1}", WMS_STD_Formula.W_Module.WV_UserName, .Cells("trandp_droppoint").Value.ToString)
                                obj_cls.Document_Index = .Cells("SalesOrder_Index").Value.ToString
                                obj_cls.Document_No = .Cells("refer_code").Value.ToString
                                obj_cls.Log_Type_ID = 1101 'แก้ไข drop point
                                obj_cls.Insert_Master()
                                obj_cls = Nothing

                            End With
                        Catch ex As Exception
                            W_MSG_Error("กรุณาตรวจสอบข้อมูล : " & i.ToString)
                            Exit Sub
                        End Try

                    Next

                    _Validate = True

                Case Type_Import.CONFIRM_Y, Type_Import.CONFIRM_Z
                    _Validate = True
                    Dim xDb As New DBType_SQLServer
                    Dim xQuery As String = ""
                    Dim dtQuery As New DataTable

                    Dim _Service As New Tb_Packing_TopCharoen
                    Dim Customer_Shipping_Location_Index As String = ""
                    Dim PackingBagIndex As String = ""

                    Dim _DataB1 As New DataTable


                    Dim _dtInterfaceY As New DataTable
                    _dtInterfaceY.Columns.Add("SalesOrder_Index", GetType(String))
                    _dtInterfaceY.Columns.Add("SalesOrder_No", GetType(String))
                    _dtInterfaceY.Columns.Add("BarcodePacking", GetType(String))
                    _dtInterfaceY.Columns.Add("ItemCode_1", GetType(String))
                    _dtInterfaceY.Columns.Add("ItemCode_2", GetType(String))
                    _dtInterfaceY.Columns.Add("Invoice_No", GetType(String))
                    _dtInterfaceY.Columns.Add("PO_No", GetType(String))
                    _dtInterfaceY.PrimaryKey = New DataColumn() {_dtInterfaceY.Columns("SalesOrder_Index")}

                    Dim Errors As String = ""
                    For i As Integer = 0 To Me.grdPreviewData.RowCount - 1
                        With Me.grdPreviewData.Rows(i)
                            Dim dtQuerySO As New DataTable
                            xQuery = String.Format("select * from tb_SalesOrder where SalesOrder_Index = '{0}'", .Cells("SalesOrder_Index").Value.ToString)
                            dtQuerySO = xDb.DBExeQuery(xQuery)
                            If dtQuerySO.Rows.Count = 0 Then
                                Errors = .Cells("SalesOrder_No").Value.ToString & " SalesOrder_No ยังไม่มีในระบบ"
                                .Cells("Check_Result").Value = Errors
                                .Cells("Check_Result").Style.BackColor = Color.Red
                                Continue For
                            End If
                            'Group RL
                            Dim drArr() As DataRow = _dtInterfaceY.Select(String.Format("SalesOrder_Index = '{0}'", .Cells("SalesOrder_Index").Value.ToString))
                            If drArr.Length = 0 Then
                                Dim drNewrow As DataRow
                                drNewrow = _dtInterfaceY.NewRow
                                drNewrow("SalesOrder_No") = .Cells("SalesOrder_No").Value.ToString
                                drNewrow("SalesOrder_Index") = .Cells("SalesOrder_Index").Value.ToString
                                drNewrow("Invoice_No") = .Cells("Invoice").Value.ToString
                                drNewrow("PO_No") = .Cells("Job_No").Value.ToString
                                drNewrow("BarcodePacking") = ""
                                If _Operation_Type = Type_Import.CONFIRM_Y Then
                                    If .Cells("Type of R/L").Value.ToString = "L" Then
                                        drNewrow("ItemCode_1") = .Cells("Item Bar").Value.ToString
                                    Else
                                        drNewrow("ItemCode_2") = .Cells("Item Bar").Value.ToString
                                    End If
                                End If
                                _dtInterfaceY.Rows.Add(drNewrow)
                            Else
                                Dim drNewrow As DataRow
                                drNewrow = _dtInterfaceY.NewRow
                                drNewrow = _dtInterfaceY.Rows.Find(.Cells("SalesOrder_Index").Value.ToString)
                                drNewrow.BeginEdit()
                                If _Operation_Type = Type_Import.CONFIRM_Y Then
                                    If .Cells("Type of R/L").Value.ToString = "L" Then
                                        drNewrow("ItemCode_1") = .Cells("Item Bar").Value.ToString
                                    Else
                                        drNewrow("ItemCode_2") = .Cells("Item Bar").Value.ToString
                                    End If
                                End If
                                drNewrow.EndEdit()
                            End If
                        End With
                    Next

                    If Errors <> "" Then
                        W_MSG_Error("กรุณาตรวจสอบข้อมูล")
                        Me.btnImport.Enabled = False
                        Me.pnlProgressBar.Hide()
                        Me.ProgressBar1.Enabled = False
                        Me.groupimport.Enabled = True
                        Me.PanelOrderItemButton.Enabled = True
                        Exit Sub
                    End If

                    'Create Temp Data
                    Dim _dtPacking As New DataTable
                    _dtPacking = _Service.ListTray("0")

                    For Each drPackBar As DataRow In _dtInterfaceY.Rows
                        'Auto Pack
                        Dim _DataSave As New DataTable
                        _DataSave = _dtPacking.Clone
                        Dim IsSL As Boolean = False

                        xQuery = " select S.SalesOrder_No,S.SalesOrder_Index,S.Customer_Shipping_Location_Index "
                        xQuery &= "     ,SI.SalesOrderItem_Index,SI.Str10,S.SAO_Type,SI.Total_Qty"
                        xQuery &= "     ,U.Sku_Id,U.Sku_Index,U.Str1 as Sku_Name,U.ItemCode_1,U.ItemCode_2,S.OMS_Droppoint_Index"
                        xQuery &= " from tb_SalesOrder S "
                        xQuery &= " inner join tb_SalesOrderItem SI ON SI.SalesOrder_Index = S.SalesOrder_Index"
                        xQuery &= " inner join ms_SKU U ON U.Sku_Index = SI.Sku_Index"
                        xQuery &= String.Format(" where S.SalesOrder_Index = '{0}'", drPackBar("SalesOrder_Index").ToString)
                        dtQuery = xDb.DBExeQuery(xQuery)
                        Customer_Shipping_Location_Index = dtQuery.Rows(0)("Customer_Shipping_Location_Index").ToString

                        'xQuery = String.Format(" delete tb_SalesOrderPacking where SalesOrder_Index = '{0}' ", dtQuery.Rows(0)("SalesOrder_Index").ToString)
                        'xQuery &= String.Format(" delete tb_SalesOrderPackingItem where SalesOrder_Index  = '{0}' ", dtQuery.Rows(0)("SalesOrder_Index").ToString)
                        'Dim xDb2 As New DBType_SQLServer
                        'xDb2.DBExeNonQuery(xQuery)


                        For Each drRow As DataRow In dtQuery.Rows
                            Dim drPack As DataRow
                            drPack = _DataSave.NewRow
                            drPack("Checked") = True
                            drPack("SalesOrder_No") = drRow("SalesOrder_No").ToString
                            drPack("SalesOrder_Index") = drRow("SalesOrder_Index").ToString
                            drPack("SalesOrderItem_Index") = drRow("SalesOrderItem_Index").ToString
                            drPack("Barcode_CL") = drRow("SalesOrder_No").ToString 'Fix
                            drPack("Barcode_Tray") = drRow("SalesOrder_No").ToString 'Fix
                            drPack("Sku_Index") = drRow("Sku_Index").ToString
                            drPack("Sku_Id") = drRow("Sku_Id").ToString
                            drPack("Sku_Name") = drRow("Sku_Name").ToString
                            drPack("Total_Qty") = drRow("Total_Qty").ToString
                            drPack("IsScanBarcode") = 1
                            drPack("ItemCode_1") = drRow("ItemCode_1").ToString
                            drPack("ItemCode_2") = drRow("ItemCode_2").ToString
                            drPack("Str10") = drRow("Str10").ToString
                            drPack("OMS_Droppoint_Index") = drRow("OMS_Droppoint_Index").ToString

                            drPack("OMS_Droppoint_Desc") = "" 'drRow("OMS_Droppoint_Desc").ToString
                            drPack("Urgent_Id") = "" 'drRow("Urgent_Id").ToString

                            If drRow("SAO_Type").ToString = "SL" Then
                                IsSL = True
                            End If
                            _DataSave.Rows.Add(drPack)
                        Next



                        Thread.Sleep(5000)

                        'Create Pack
                        'interface OMS.
                        xQuery = " select top 1 (case when isnull(Barcode_GROUP,'') <> '' then Barcode_GROUP else Barcode_BAG END) BarcodePacking,SalesOrderPacking_Index "
                        xQuery &= " from tb_SalesOrderPackingItem P "
                        xQuery &= String.Format(" where P.SalesOrder_Index = '{0}' ", dtQuery.Rows(0)("SalesOrder_Index").ToString)
                        Dim dtPack As New DataTable
                        dtPack = xDb.DBExeQuery(xQuery)

                        If dtPack.Rows.Count = 0 Then
                            PackingBagIndex = _Service.SavePackingBag(Customer_Shipping_Location_Index, _DataSave, _DataB1, True, IsSL)
                            'interface OMS.
                            xQuery = " select top 1 (case when isnull(Barcode_GROUP,'') <> '' then Barcode_GROUP else Barcode_BAG END) BarcodePacking,SalesOrderPacking_Index "
                            xQuery &= " from tb_SalesOrderPackingItem P "
                            xQuery &= String.Format(" where P.SalesOrder_Index = '{0}' ", dtQuery.Rows(0)("SalesOrder_Index").ToString)
                            dtPack = xDb.DBExeQuery(xQuery)

                            drPackBar("BarcodePacking") = dtPack.Rows(0)("BarcodePacking").ToString
                        Else

                            drPackBar("BarcodePacking") = dtPack.Rows(0)("BarcodePacking").ToString
                            PackingBagIndex = dtPack.Rows(0)("SalesOrderPacking_Index").ToString
                        End If




                        If PackingBagIndex = "" Then
                            W_MSG_Error("Error : สร้างใบคุมของ ")
                            _Validate = False
                            Exit Sub
                        End If



                        Try
                            Dim Response As String = ""
                            Dim InterfaceService As New WMS_Site_Topcharoen_Interface.WMS_Interface
                            Dim TimeOut As Integer = 60000
                            'Interface
                            Select Case _Operation_Type
                                Case Type_Import.CONFIRM_Y
                                    Response = InterfaceService.ResponseOrderList(drPackBar("SalesOrder_No").ToString, "Y", drPackBar("BarcodePacking").ToString, drPackBar("ItemCode_1").ToString, drPackBar("ItemCode_2").ToString, "", TimeOut)
                                Case Type_Import.CONFIRM_Z
                                    Response = InterfaceService.ResponseOrderList(drPackBar("SalesOrder_No").ToString, "Z", drPackBar("BarcodePacking").ToString, "", "", drPackBar("PO_No").ToString, TimeOut)
                            End Select


                            If Not (Response.ToString.Trim = "success") Then
                                'xQuery = String.Format(" delete tb_SalesOrderPacking where SalesOrderPacking_Index = '{0}' ", PackingBagIndex)
                                'xQuery &= String.Format(" delete tb_SalesOrderPackingItem where SalesOrderPacking_Index  = '{0}' ", PackingBagIndex)
                                'Dim xDb3 As New DBType_SQLServer
                                'xDb3.DBExeNonQuery(xQuery)
                                If W_MSG_Confirm("Plase contract OMS admin. Message : " & Response.ToString & Chr(13) & "Error : Interface OMS Order = " & drPackBar("SalesOrder_No").ToString & Chr(13) & "คุณต้องการทำต่อหรือไม่ ?") = Windows.Forms.DialogResult.No Then
                                    Exit Sub
                                End If
                                _Validate = False
                                'Exit Sub
                            Else
                                xQuery = " update tb_SalesOrder SET ProveYZ = 1"
                                xQuery &= String.Format(" where SalesOrder_Index = '{0}'", drPackBar("SalesOrder_Index").ToString)
                                xDb.DBExeNonQuery(xQuery)
                            End If
                        Catch exp As Exception
                            _Validate = False
                            Throw New System.Exception("Interface OMS. Error : Please contract OMS. Admin")
                        End Try


                        'Update value after create pack
                        xQuery = " update tb_SalesOrder SET "
                        xQuery &= String.Format(" Invoice_No = '{0}'", drPackBar("Invoice_No").ToString)
                        xQuery &= String.Format(" ,PO_No = '{0}'", drPackBar("PO_No").ToString)
                        xQuery &= String.Format(" ,Status = '{0}'", 5)
                        'update ตามที่ตอบกลับจริง
                        Select Case _Operation_Type
                            Case Type_Import.CONFIRM_Y
                                xQuery &= String.Format(" ,SO_Type = '{0}'", "Y")
                                xQuery &= String.Format(" ,ItemCode_1 = '{0}'", drPackBar("ItemCode_1").ToString)
                                xQuery &= String.Format(" ,ItemCode_2 = '{0}'", drPackBar("ItemCode_2").ToString)
                            Case Type_Import.CONFIRM_Z
                                xQuery &= String.Format(" ,SO_Type = '{0}'", "Z")
                        End Select

                        xQuery &= String.Format(" where SalesOrder_Index = '{0}'", drPackBar("SalesOrder_Index").ToString)
                        xDb.DBExeNonQuery(xQuery)

                    Next
            End Select

            Me.pnlProgressBar.Hide()
            Me.ProgressBar1.Enabled = False
            Me.groupimport.Enabled = True
            Me.PanelOrderItemButton.Enabled = True

         
            If _Validate Then
                Select Case _Operation_Type
                    Case Type_Import.CONFIRM_Y, Type_Import.CONFIRM_Z
                        Dim xDb As New DBType_SQLServer
                        Dim xQuery As String = ""
                        Dim dtQuery As New DataTable
                        For i As Integer = 0 To grdPreviewData.Rows.Count - 1
                            With grdPreviewData.Rows(i)
                                xQuery = String.Format("select ISNULL(ProveYZ,0) as Prove,* from tb_SalesOrder where status <> -1 and SalesOrder_No = '{0}'", .Cells("SalesOrder_No").Value.ToString)
                                dtQuery = xDb.DBExeQuery(xQuery)

                                .Cells("SalesOrder_Index").Value = dtQuery.Rows(0).Item("SalesOrder_Index").ToString
                                .Cells("OMS").Value = "N"
                                If dtQuery.Rows(0).Item("Prove").ToString = "1" Then
                                    .Cells("OMS").Value = "Y"
                                End If
                            End With
                        Next
                        Me.SetnumRows()

                End Select


                Me.btnImport.Enabled = False
                W_MSG_Information("นำเข้าข้อมูลสำเร็จ")
            Else
                W_MSG_Information("นำเข้าข้อมูลผิดพลาด")
            End If

        Catch ex As Exception
            Me.groupimport.Enabled = True
            Me.pnlProgressBar.Hide()
            Me.ProgressBar1.Enabled = False
            Me.PanelOrderItemButton.Enabled = True

            W_MSG_Error(ex.Message.ToString)
        Finally

        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvess.Click
        Try
            If Me.grdPreviewData.Rows.Count = 0 Then Exit Sub
            Dim drArrFail() As DataRow = CType(Me.grdPreviewData.DataSource, DataTable).Select("OMS = 'Y'")
            If drArrFail.Length = 0 Then
                W_MSG_Information("ไม่พบรายการผิดพลาด")
                Exit Sub
            Else
                For Each drDelete As DataRow In drArrFail
                    CType(Me.grdPreviewData.DataSource, DataTable).Rows.Remove(drDelete)
                Next
            End If

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            W_MSG_Information("ลบรายการเรียบร้อยแล้ว")
            Me.btnImport.Enabled = True
            Me.SetnumRows()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class

'Me.btnImport.Enabled = False
'Me.btnValidate.Enabled = False
'Me.btnDelete.Enabled = False
'Me.btnDeleteFailAll.Enabled = False
'Me.btnExit.Enabled = False
'Me.btnBrowse.Enabled = False
'Me.BntLoaddata.Enabled = False
