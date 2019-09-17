Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_OUTB_WithDraw_Datalayer

Public Class frmWithdraw_ScanSerial

    Public checkSave As Boolean = False
    Dim _dt_Serial As New DataTable
    Public Property dt_Serial() As DataTable
        Get
            Return _dt_Serial
        End Get
        Set(ByVal value As DataTable)
            _dt_Serial = value
        End Set
    End Property

    Dim _TempdtWithdraw As New DataTable
    Public Property TempdtWithdraw() As DataTable
        Get
            Return _TempdtWithdraw
        End Get
        Set(ByVal value As DataTable)
            _TempdtWithdraw = value
        End Set
    End Property


    Private _Total_Qty As Decimal
    Public Property Total_Qty() As Decimal
        Get
            Return _Total_Qty
        End Get
        Set(ByVal value As Decimal)
            _Total_Qty = value
        End Set
    End Property


    Private Sub getSerial()
        Try
            Dim obj_withdrawTrans As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
            Dim Serial As String = Me.txtSerial.Text '  obj_withdrawTrans.GetWithdraw_Serial(Me.txtSerial.Text)
            Me._dt_Serial.Rows.Add("", Serial)

            grdData.DataSource = Me._dt_Serial

            lblSumQty.Text = _dt_Serial.Rows.Count & "/" & CInt(Me.Total_Qty.ToString)


            txtSerial.Clear()
            txtSerial.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmWithdraw_ScanSerial_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try

            txtSerial.Focus()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

 


    Private Sub frmWithdraw_ScanSerial_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.txtSerial.Clear()

            Me.txtSerial.Focus()
            lblSumQty.Text = _dt_Serial.Rows.Count & "/" & CInt(Me.Total_Qty.ToString)

            If _dt_Serial.Rows.Count <= 0 Then
                Me._dt_Serial.Clear()
                Me._dt_Serial.Columns.Add(New DataColumn("Barcode_Bag", GetType(String)))
                Me._dt_Serial.Columns.Add(New DataColumn("Serial", GetType(String)))
            Else
                Me.grdData.DataSource = _dt_Serial
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub txtSerial_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerial.KeyPress
        Try
            If e.KeyChar <> ChrW(13) Then
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtSerial.Text) Then
                W_MSG_Information("กรุณากรอก Serial สินค้า")
                Me.txtSerial.Focus()
                Exit Sub
            End If
            If _dt_Serial.Rows.Count = CInt(Me.Total_Qty.ToString) Then
                W_MSG_Error("เกินจำนวนไม่ได้")
                Exit Sub
            End If
            getSerial()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            '   txtSerial.Clear()
            '  txtSerial.Focus()
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim AutoRunning As New Sy_Running

            Dim BarcodeBag As String = AutoRunning.GetNextRunningBAG()
            If Not Me.dt_Serial.Columns.Contains("checkPrint") Then
                Me.dt_Serial.Columns.Add("checkPrint", GetType(Boolean))
            End If

            Dim checkSuccess As Integer = 0
            For i As Integer = 0 To Me._dt_Serial.Rows.Count - 1
                If String.IsNullOrEmpty(Me._dt_Serial.Rows(i).Item("Barcode_Bag").ToString) Then
                    Me._dt_Serial.Rows(i).Item("Barcode_Bag") = BarcodeBag
                    Me._dt_Serial.Rows(i).Item("checkPrint") = True
                End If

            Next

            Print()

            If Me._dt_Serial.Select("isnull(Barcode_Bag,'') <> ''").Length = CInt(Me.Total_Qty.ToString) Then
                checkSave = True
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Sub Print()
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            '  Dim index As String = "'0010000023137" & "',"

            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            oCrystal = clsPacking.GetReportInfoWithdraw("rptTransferDocRange", "", TempdtWithdraw, Me._dt_Serial)
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub


End Class