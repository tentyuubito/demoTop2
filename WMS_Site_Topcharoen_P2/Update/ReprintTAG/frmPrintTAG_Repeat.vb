#Region "Import NameSpace"
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_master.W_Function
Imports WMS_STD_INB_Receive
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_INB_Barcode
Imports WMS_STD_INB_Report
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
#End Region
#Region "Class Form"
Public Class frmPrintTAG_Repeat
#Region "Private Sub & Function"
    Private Sub PrintBarcode()
        Try

            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Dim dtTAG As New DataTable

            Try
                Select Case Report_Name.ToUpper
                    Case "TAGSTICKERPRINTOUT", "TAGSTICKERPRINTOUT_MINI"
                        Dim oReport As New clsReport()
                        Dim TAG_Index_IN As String = ""
                        Dim drArr() As DataRow = DirectCast(Me.grdTag.DataSource, DataTable).Select(" chkSelect=1 and TAG_Status <> -1 ")
                        If drArr.Length > 0 Then
                            For Each dr As DataRow In drArr
                                TAG_Index_IN &= "'" & dr("TAG_Index").ToString() & "',"

                                ' BGN Update PrintCount
                                Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.NULL)
                                objTag.UpdatePrintStatus(dr("TAG_Index").ToString())
                                ' END Update PrintCount
                            Next
                        Else
                            W_MSG_Information_ByIndex("300032")
                            Exit Sub
                        End If
                        TAG_Index_IN = TAG_Index_IN.Substring(0, TAG_Index_IN.Trim.Length - 1)
                        Dim strWhere As String = " and TAG_Index in(" & TAG_Index_IN & ")"

                        Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                        rpt = oReport.GetReportInfo(Report_Name, strWhere)

                        frm.CrystalReportViewer1.ReportSource = rpt
                        frm.ShowDialog()
                End Select
                   

                '###################################
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally

            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub LoadData()
        Try
            Dim objTAG As New clsPrintPalletSlip 'tb_TAG_Update(tb_TAG_Update.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable
            Dim strWhere As String = ""

            If Me.txtTAG_No.Text.Trim.Length > 0 Then
                strWhere &= " And TAG_No like '" & txtTAG_No.Text.Trim & "%'"
            End If

            If txtLocation.Text <> "" Then
                strWhere &= " And Location_Alias like '" & txtLocation.Text & "%'"
            End If


            If txtSku_Id.Text.Trim <> "" Then
                strWhere &= " And Sku_Id like '%" & txtSku_Id.Text & "%'"
            End If

            If txtPlot.Text.Trim <> "" Then
                strWhere &= " And PLot like '%" & txtPlot.Text & "%'"
            End If


            If txtDocTrans_Process.Text.Trim <> "" Then
                strWhere &= " And DocTrans_Process like '%" & txtDocTrans_Process.Text & "%'"
            End If


            If Me.cmbTag_Process.SelectedValue.ToString <> "-99" Then
                strWhere &= " And Tag_Process_Id = " & cmbTag_Process.SelectedValue.ToString
            Else
                strWhere &= " AND Tag_Process_Id  in ( 1,5) "
            End If

            objTAG.getView_Tag_Header_Repeat(strWhere)
            objDT = objTAG.DataTable

            Me.grdTag.Refresh()
            grdTag.DataSource = objDT
            Dim Irow As Integer = 1
            For Each dr As DataRow In objDT.Rows
                dr("Row") = Irow
                Irow = Irow + 1
            Next

            'For i As Integer = 0 To Me.grdTag.Rows.Count - 1
            '    Select Case Me.grdTag.Rows(i).Cells("Col_TAG_Status_Index").Value
            '        Case "2"
            '            Me.grdTag.Rows(i).Cells("col_Status").Style.BackColor = Color.LightGreen
            '            Me.grdTag.Rows(i).Cells("col_TAG_NO").Style.BackColor = Color.LightGreen
            '        Case Else
            '            Me.grdTag.Rows(i).Cells("col_Status").Style.BackColor = Color.White
            '            Me.grdTag.Rows(i).Cells("col_TAG_NO").Style.BackColor = Color.White

            '    End Select
            'Next
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub
    Private Function CreateTag_Process() As DataTable
        Try
            Dim objTempDT As New DataTable


            objTempDT.Columns.Add("index", GetType(Integer))
            objTempDT.Columns.Add("Caption", GetType(String))

            Dim objDr As DataRow
            objDr = objTempDT.NewRow
            objDr.Item("index") = "5"
            objDr.Item("Caption") = "โอน"
            objTempDT.Rows.Add(objDr)

            Dim objDr2 As DataRow
            objDr2 = objTempDT.NewRow
            objDr2.Item("index") = "1"
            objDr2.Item("Caption") = "รับ"
            objTempDT.Rows.Add(objDr2)


            Return objTempDT
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub AddDatatoCombobox(ByVal objTempDT As DataTable, ByVal TempCombo As ComboBox, ByVal strValueMember As String, ByVal strDisplayMember As String)
        Dim objDr As DataRow
        Try
            objDr = objTempDT.NewRow

            objDr.Item(strValueMember) = "-99"
            objDr.Item(strDisplayMember) = "ไม่ระบุ"
            objTempDT.Rows.Add(objDr)

            With TempCombo
                .ValueMember = strValueMember
                .DisplayMember = strDisplayMember
                .DataSource = objTempDT
            End With

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub
    Private Sub PepairData()
        ' Dim objUtility As New clsUtility
        Dim objDT As New DataTable
        Dim strCondition As String = ""
        Try

            objDT = CreateTag_Process()
            AddDatatoCombobox(objDT, Me.cmbTag_Process, "Index", "Caption")

            'Default Value
            '  Me.CmbLocation.SelectedValue = -99
            Me.cmbTag_Process.SelectedValue = -99
        Catch ex As Exception
            Throw ex
        Finally
            '    objUtility = Nothing
        End Try
    End Sub
#End Region
#Region "Event Handles"
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            PrintBarcode()
            LoadData()
            Me.ChkAllTag.Checked = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmPrintTAG_Repeat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdTag.AutoGenerateColumns = False
            Me.getReportName(101)
            PepairData()
            '  LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub ChkAllTag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAllTag.CheckedChanged
        Try
            If Me.grdTag.Rows.Count > 0 Then
                Dim Dttmp As New DataTable
                Dttmp = CType(Me.grdTag.DataSource, DataTable)
                For Each Dr As DataRow In Dttmp.Rows
                    Dr("chkSelect") = Me.ChkAllTag.Checked
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub cmbTag_Process_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTag_Process.SelectedIndexChanged
        Try
            txtDocTrans_Process.Text = ""

            'If cmbTag_Process.SelectedValue = "5" Then
            '    txtDocTrans_Process.Visible = True
            'Else
            '    txtDocTrans_Process.Visible = False
            'End If




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try




    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        Dim objClassDB As New WMS_STD_Master.config_Report()
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

End Class
#End Region