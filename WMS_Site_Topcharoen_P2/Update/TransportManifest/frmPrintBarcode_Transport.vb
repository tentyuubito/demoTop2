
Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Function

Public Class frmPrintBarcode_Transport


    Private _TransportManifest_Index As String
    Public Property Transport_Manifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal value As String)
            _TransportManifest_Index = value
        End Set
    End Property

#Region "  Form Load  "
    Private Sub frmPrintBarcode_Transport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdDataDoc_no.AutoGenerateColumns = False
            LoadDatabarcode()
        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
    End Sub

#End Region

#Region "  Sub  "
    Private Sub LoadDatabarcode()
        Try

            Dim oDT As New DataTable
            Dim objTran As New tb_TransportManifest_Update
            oDT = objTran.GetDocNo_TransportManifestItem(Me._TransportManifest_Index)

            If oDT.rows.count > 0 Then
                Me.grdDataDoc_no.DataSource = oDT
                Me.Label1.Text = "‡≈¢∑’Ë„∫§ÿ¡ : " & oDT.Rows(0)("TransportManifest_No").ToString
                Me.Label2.Text = "®”π«π∫‘≈ :  " & oDT.Rows.Count & " „∫"

            End If
            setRowsNumber()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub setRowsNumber()
        Try
            If Me.grdDataDoc_no.RowCount <= 0 Then Exit Sub

            For i As Integer = 0 To Me.grdDataDoc_no.RowCount - 1
                Me.grdDataDoc_no.Rows(i).Cells("col_Seq").Value = i + 1
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try


            Dim ODT As New DataTable("Print_Barcode_Transport")
            Dim oDR() As DataRow

            'For Report
            ' DT.Columns.Add("Pic", GetType(System.Byte()))

            Dim tmpDs As New DataSet()
            Dim tmpDt As New DataTable
            Dim tmpDr As DataRow
            tmpDt.Columns.Add("Doc_No", GetType(String))
            tmpDt.Columns.Add("Tran_NO", GetType(String))
            tmpDt.Columns.Add("Barcode", GetType(System.Byte()))
            tmpDt.Columns.Add("Barcode_TM_NO", GetType(System.Byte()))

            'ODT.TableName = "Print_Barcode_Transport"
            ODT = CType(Me.grdDataDoc_no.DataSource, DataTable)
            ODT.AcceptChanges()
            oDR = ODT.Select("chkSelect = 1")

            Dim objBarcode As New WMS_STD_INB_Barcode.Barcode

            If oDR.Length <= 0 Then
                W_MSG_Information("‡≈◊Õ°·∂«∑’ËµÈÕßæ‘¡æÏ∫“√Ï‚§È¥")
                Exit Sub
            Else
                Dim Inv_no As String = ""
                For Each dr As DataRow In oDR
                    tmpDr = tmpDt.NewRow
                    tmpDr("Doc_No") = dr("transportManifest_No").ToString
                    If dr("Str1").ToString = "" Then
                        Inv_no = dr("SalesOrder_No").ToString
                    Else
                        Inv_no = dr("Str1").ToString
                    End If
                    tmpDr("Tran_NO") = Inv_no 'dr("SalesOrder_No").ToString

                    'Create Barcode
                    objBarcode.GenBarcode(Inv_no)
                    tmpDr("Barcode") = ConvertFileToByte(Application.StartupPath & "\" & Inv_no & ".bmp")

                    objBarcode.GenBarcode(dr("transportManifest_No").ToString)
                    tmpDr("Barcode_TM_NO") = ConvertFileToByte(Application.StartupPath & "\" & dr("transportManifest_No").ToString & ".bmp")

                    tmpDt.Rows.Add(tmpDr)
                Next

                'For i As Integer = 0 To oDR.Length - 1
                '    tmpDr = tmpDt.NewRow
                '    With oDR(i)
                '        tmpDr("Doc_No") = .Item("transportManifest_No").ToString
                '        If .Item("str1").ToString <> "" Then
                '            tmpDr("Barcode") = .Item("str1").ToString
                '        Else
                '            tmpDr("Barcode") = .Item("SalesOrder_No").ToString
                '        End If
                '    End With
                '    tmpDt.Rows.Add(tmpDr)
                'Next

                tmpDt.TableName = "Print_Barcode_Transport"
                tmpDs.Tables.Add(tmpDt)


                Dim frm As New frmReportMainTransportManifest_Update
                frm.DS_TEMP = tmpDs
                frm.Report_Name = "Print_Barcode_Transport"
                frm.ShowDialog()

                setRowsNumber()
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class