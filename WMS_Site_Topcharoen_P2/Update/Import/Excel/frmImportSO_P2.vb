Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports Microsoft.Office.Interop

Public Class frmImportSO_P2

#Region " + Variable + "

    Private Const CONNECTION_STRING_XLS As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
    Private Const CONNECTION_STRING_XLSX As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;Xml;HDR=YES"";"

    Private Const REMOVE_HEADER_ROW As Integer = 1

    Private _LockUI, _HaveData As Boolean
    Private _ConnectionString As String

    Private _Service As ImportExcel
    Private _Data As DataTable

#End Region

#Region " + Property + "

    'Nothing

#End Region

#Region " + Function + "

    'Nothing

#End Region

#Region " + Sub + "

    Private Sub InitProperty()
        Try
            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

            Me._Service = New ImportExcel

            Dim ServiceDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
            ServiceDocumentType.getDocumentType(10)

            With Me.cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = ServiceDocumentType.DataTable

                If ServiceDocumentType.DataTable.Rows.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

            With Me.grdData
                .AllowUserToAddRows = False
                .AllowUserToOrderColumns = False
                '  .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
            End With

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub LoadData()
        Try
            If String.IsNullOrEmpty(Me.txtFileName.Text) OrElse String.IsNullOrEmpty(Me.cboSheet.Text.Trim) Then
                Exit Sub
            End If

            Using Data As New DataTable
                Using Connection As New OleDb.OleDbConnection(String.Format(Me._ConnectionString, Me.txtFileName.Text))
                    Using DataAdapter As New OleDb.OleDbDataAdapter
                        With DataAdapter
                            .SelectCommand = New OleDb.OleDbCommand(" SELECT * FROM [" & Me.cboSheet.Text.Trim & "$] ", Connection)
                            .Fill(Data)
                        End With
                    End Using
                End Using

                Me._HaveData = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Me._HaveData Then
                    Data.Rows.RemoveAt(0)
                    'For i As Integer = 0 To REMOVE_HEADER_ROW - 1
                    '    Data.Rows.RemoveAt(0)
                    'Next
                End If

                Me._Data = Data.Copy
                Me.grdData.DataSource = Me._Data

                For Each Column As DataGridViewColumn In Me.grdData.Columns
                    Column.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
            End Using

            If Not Me._HaveData Then
                W_MSG_Error("ไม่พบข้อมูล Excel")
            End If

            SetButton()
            SetLabelSum()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub LoadWorkSheet(ByVal PathExcel As String)
        Try
            Dim Excel As Excel.Application = CreateObject("Excel.Application")
            Dim WorkBook As Excel.Workbook = Excel.Workbooks.Open(PathExcel)

            With Me.cboSheet.Items
                .Clear()
                .Add(String.Empty)
                .Clear()
            End With

            For Each Sheet As Excel.Worksheet In WorkBook.Worksheets
                Me.cboSheet.Items.Add(Sheet.Name)
            Next

            If Me.cboSheet.Items.Count > 0 Then
                Me.cboSheet.SelectedIndex = 0
            End If

            Excel.Workbooks.Close()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetButton()
        Try
            Me.btnSave.Enabled = Me._HaveData
            Me.btnClear.Enabled = Me._HaveData

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetLabelSum()
        Try
            If Me._HaveData Then
                Me.lblSum.Text = "รวม : " & FormatNumber(Me._Data.Rows.Count, 0) & " รายการ"
            Else
                Me.lblSum.Text = "ไม่พบรายการ"
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ClearUI()
        Try
            If Me._HaveData Then
                Me._Data.Rows.Clear()
                Me._Data.Columns.Clear()

                Me._HaveData = False
            End If

            Me._ConnectionString = String.Empty

            Me.cboSheet.Items.Clear()
            Me.txtFileName.Clear()

            SetButton()
            SetLabelSum()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

#End Region

#Region " + Event + "

    Private Sub frmImportSO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InitProperty()
            ClearUI()

            '     Me.cboSO_Type.SelectedIndex = 0

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me._LockUI Then Exit Sub
        Me._LockUI = True
        Try
            Me.openDialogImport.Reset()

            If Me.openDialogImport.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Me.txtFileName.Text = Me.openDialogImport.FileName.ToString.Trim
            Select Case IO.Path.GetExtension(Me.txtFileName.Text).ToString.ToLower
                Case ".xls"
                    Me._ConnectionString = CONNECTION_STRING_XLS

                Case ".xlsx"
                    Me._ConnectionString = CONNECTION_STRING_XLSX
            End Select

            LoadWorkSheet(Me.txtFileName.Text)

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        Finally
            Me._LockUI = False
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        If Me._LockUI Then Exit Sub
        Me._LockUI = True
        Try
            LoadData()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        Finally
            Me._LockUI = False
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If Me._LockUI Then Exit Sub
        Me._LockUI = True
        Try
            ClearUI()

        Catch Ex As Exception
            W_MSG_Error(Ex.ToString)
        Finally
            Me._LockUI = False
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me._LockUI Then Exit Sub
        Me._LockUI = True
        Try
            If Me.cboDocumentType.SelectedIndex < 0 Then
                W_MSG_Information("กรุณาเลือกข้อมูล ประเภทเอกสาร")
                Exit Sub
            End If

            If W_MSG_Confirm("ยืนยันการบันทึก ใช่ หรือ ไม้") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Dim dtResult As New DataTable

            dtResult = Me._Service.ImportSalesOrder(Me._Data, Me.cboDocumentType.SelectedValue, "")

            If IsNothing(dtResult) Then
                ClearUI()
                W_MSG_Information("นำเข้าข้อมูล SO เสร็จสิ้น")
            Else


                grdData.AutoGenerateColumns = True
                grdData.DataSource = Nothing
                grdData.DataSource = dtResult


                For Each grdRow As DataGridViewRow In grdData.Rows
                    If Not String.IsNullOrEmpty(grdRow.Cells("Message").Value) Then
                        grdRow.Cells(0).Style.BackColor = Color.Red
                    End If
                Next

                W_MSG_Information("Import ข้อมูลล้มเหลว")
            End If

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        Finally
            Me._LockUI = False
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged

    End Sub
End Class