Imports WMS_STD_OAW_Adjust_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports System.Math
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.Configuration.ConfigurationSettings

Public Class frmAdjust_Confirm
    Private _Keep_Qty As Object
    Private _adjust_index As String
    Private _count_amount As Integer
    Private _objHeaderAdjust As New tb_Adjust
    'Public _ForReadOnly As Boolean
    Private _Status As Integer = 0
    Public Property Adjust_Index() As String
        Get
            Return _adjust_index
        End Get
        Set(ByVal value As String)
            _adjust_index = value
        End Set
    End Property

    Public Property Count_Amount() As Integer
        Get
            Return _count_amount
        End Get
        Set(ByVal value As Integer)
            _count_amount = value
        End Set
    End Property

    Public Property ObjHeaderAdjust() As tb_Adjust
        Get
            Return _objHeaderAdjust
        End Get
        Set(ByVal value As tb_Adjust)
            _objHeaderAdjust = value
        End Set
    End Property

    Private Sub getAdjustItemDetail(ByVal Adjust_Index As String)
        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.getAdjustItemDetail(Adjust_Index)
            objDT = objClassDB.DataTable

            Me.grdAdjustItem.DataSource = objDT
            Me.CalSubtotalAmount()

            If objDT.Rows.Count = 0 Then
                W_MSG_Information("ไม่พบข้อมูล")
            Else
                Dim numRows As Integer = 0
                numRows = grdAdjustItem.Rows.Count
                If numRows > 0 Then
                    lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
                Else
                    lbCountRows.Text = "ไม่พบรายการ"
                End If
            End If

            ManageBotton()

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub ManageBotton()
        Try
            If Me.grdAdjustItem.RowCount = 0 Then Exit Sub

            Select Case _Status
                Case "-1"
                    Me.btnConfirn_Adjust.Enabled = False
                    Me.btnAdjust_Confirm.Enabled = False
                    Me.btnAdjust.Enabled = False
                    Me.btnPrint_Adjust.Enabled = True
                    Me.btnRefresh.Enabled = False

                Case "1"
                    Me.btnConfirn_Adjust.Enabled = True
                    Me.btnAdjust_Confirm.Enabled = False
                    Me.btnAdjust.Enabled = True
                    Me.btnPrint_Adjust.Enabled = True
                    Me.btnRefresh.Enabled = True
                Case "2"
                    Me.btnConfirn_Adjust.Enabled = True
                    Me.btnAdjust_Confirm.Enabled = True
                    Me.btnAdjust.Enabled = True
                    Me.btnPrint_Adjust.Enabled = True
                    Me.btnRefresh.Enabled = True
                Case "3"
                    Me.btnConfirn_Adjust.Enabled = False
                    Me.btnAdjust_Confirm.Enabled = False
                    Me.btnAdjust.Enabled = False
                    Me.btnPrint_Adjust.Enabled = True
                    Me.btnRefresh.Enabled = False
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdjust_Confirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjust_Confirm.Click
        Try

            Dim oAdjust As New AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            Dim AdjustItem_index As String = ""
            Dim Adjust_QTY As Integer
            Dim QTY_Bal As Integer

            'If chk_AdjustQTY() Then
            '    btnConfirn_Adjust.Enabled = True
            '    btnPrint_Adjust.Enabled = True
            'Else
            '    W_MSG_Information("ยังมีรายการที่ไม่ได้ตรวจนับ ไม่บันทึกข้อมูลได้")
            '    Exit Sub
            'End If

            Dim boolSave As Boolean = False
            For i As Integer = 0 To grdAdjustItem.Rows.Count - 1
                If CBool(grdAdjustItem.Rows(i).Cells("Col_Flag_Chk").Value) = True Then
                    Adjust_QTY = 0 'grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value
                    AdjustItem_index = grdAdjustItem.Rows(i).Cells("col_index").Value
                    QTY_Bal = grdAdjustItem.Rows(i).Cells("col_Count_1st").Value
                    If oAdjust.Update_Adjust_QTY(AdjustItem_index, QTY_Bal, Adjust_QTY) = False Then
                        W_MSG_Information("บันทึกข้อมูลผิดพลาดกรุณาตรวจสอบข้อมูล")
                        boolSave = False
                        Exit Sub
                    Else
                        boolSave = True
                    End If
                End If
            Next

            If boolSave Then
                W_MSG_Information_ByIndex("1")
                Me.getAdjustItemDetail(Me._adjust_index)
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub CalDiffQty(ByVal Row_Index As Integer)
        Try

            With Me.grdAdjustItem

                Dim diffQty As Double = 0
                Dim iQty_Bal As Double = Val(.Rows(Row_Index).Cells("Qty_Bal").Value)
                Dim iCount_1st_Total_Qty As Double = Val(.Rows(Row_Index).Cells("Count_1st_Total_Qty").Value)

                If iCount_1st_Total_Qty = iQty_Bal Then
                    .Rows(Row_Index).Cells("DiffQty").Value = 0
                End If

                If iCount_1st_Total_Qty > iQty_Bal Then
                    diffQty = iCount_1st_Total_Qty - iQty_Bal
                    .Rows(Row_Index).Cells("DiffQty").Value = "+ " + diffQty.ToString
                End If

                If iCount_1st_Total_Qty < iQty_Bal Then
                    diffQty = iQty_Bal - iCount_1st_Total_Qty
                    .Rows(Row_Index).Cells("DiffQty").Value = "- " + diffQty.ToString
                End If

                .Rows(Row_Index).Cells("Adjust_Qty").Value = Val(.Rows(Row_Index).Cells("Count_1st_Total_Qty").Value)
            End With
        Catch ex As Exception
            Throw ex
        End Try



    End Sub

    Private Sub frmAdjust_Confirm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            ''Insert
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdAdjustItem)

            '' ''SwitchLanguage
            oFunction.SwitchLanguage(Me)
            oFunction.SW_Language_Column(Me, Me.grdAdjustItem)
            grdAdjustItem.AutoGenerateColumns = False

            Dim oFunction1 As New config_Column
            oFunction1.AdjustColumnOrder(Me, Me.grdAdjustItem, W_Module.WV_UserName)

            Me.getAdjustItemDetail(Me._adjust_index)
            'If chk_AdjustQTY() = True Then
            '    btnConfirn_Adjust.Enabled = True
            'End If

            'Else
            '    For i As Integer = 0 To grdAdjustItem.Rows.Count - 1
            '        grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").ReadOnly = False
            '    Next
            '
            Me.btnAdjust.Visible = False
            Me.DataGridViewTextBoxColumn42.Visible = False
            Me.Order_No.Visible = False



            '0:ยกเลิก
            '0:ไม่ระบุ
            '1:รอยืนยันเอกสาร
            '2:รอยืนยันการปรับยอด
            '3:เสร็จสิ้น
            '4:กำลังตรวจนับ
            Select Case Me._Status
                Case 1, 2, 4
                    Me.btnDelete.Enabled = True
                Case Else
                    Me.btnDelete.Enabled = False
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub grdAdjustItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If e.RowIndex <= -1 Then Exit Sub
            Dim Row_Index As Integer = e.RowIndex
            Me.CalDiffQty(Row_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub CalSubtotalAmount()
        Try
            Dim dblSum As Double = 0
            Dim dblTempQty As Double = 0
            Dim i As Integer

            For i = 0 To grdAdjustItem.Rows.Count - 1
                _Status = grdAdjustItem.Rows(i).Cells("col_Status").Value

                If grdAdjustItem.Rows(i).Cells("col_Qty_Bal").Value.ToString = Nothing Then grdAdjustItem.Rows(i).Cells("col_Qty_Bal").Value = 0
                If grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value Is Nothing Then grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value = 0
                If grdAdjustItem.Rows(i).Cells("col_Count_1st").Value Is Nothing Then grdAdjustItem.Rows(i).Cells("col_Count_1st").Value = 0


                If grdAdjustItem.Rows(i).Cells("col_adjuststatus").Value = -9 Then
                    ' ตรวจนับแล้ว จะเป็นสี GreenYellow
                    'grdAdjustItem.Rows(i).Cells("Col_Flag_Chk").Value = 1
                    'grdAdjustItem.Rows(i).Cells("col_AdjustItem_Status").Value = "ตรวจนับแล้ว"
                    grdAdjustItem.Rows(i).Cells("Location_Alias").Style.BackColor = Color.GreenYellow
                    grdAdjustItem.Rows(i).Cells("col_AdjustItem_Status").Style.BackColor = Color.GreenYellow
                    grdAdjustItem.Rows(i).Cells("Sku_Id").Style.BackColor = Color.GreenYellow
                    'grdAdjustItem.Rows(i).Cells("col_Count_1st").Style.BackColor = Color.GreenYellow
                    grdAdjustItem.Rows(i).Cells("col_DiffQty").Style.BackColor = Color.GreenYellow
                    grdAdjustItem.Rows(i).Cells("col_Qty_Bal").Style.BackColor = Color.GreenYellow

                    grdAdjustItem.Rows(i).Cells("col_DiffQty").Value = CInt(grdAdjustItem.Rows(i).Cells("col_Count_1st").Value) - CInt(grdAdjustItem.Rows(i).Cells("col_Qty_Bal").Value)
                    If grdAdjustItem.Rows(i).Cells("col_Count_1st").Value = 0 Then
                        grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value = 0 ' grdAdjustItem.Rows(i).Cells("col_SUM_QTY").Value
                    Else
                        grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value = grdAdjustItem.Rows(i).Cells("col_Count_1st").Value
                    End If
                    'จำนวนผลต่าง,จำนวนที่ปรับ
                    Me.getAction(CDbl(grdAdjustItem.Rows(i).Cells("col_DiffQty").Value), CDbl(grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value), i)
                    'ถ้ามีผลต่างจะเป็นสีแดงส้ม
                    If CDbl(grdAdjustItem.Rows(i).Cells("col_DiffQty").Value) <> 0 Then
                        grdAdjustItem.Rows(i).Cells("col_DiffQty").Style.BackColor = Color.OrangeRed
                    End If
                Else
                    ' รอตรวจนับ
                    'grdAdjustItem.Rows(i).Cells("col_AdjustItem_Status").Value = "รอตรวจนับ"
                    grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
                    grdAdjustItem.Rows(i).Cells("col_Detail").Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
                    grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").ReadOnly = True
                    grdAdjustItem.Rows(i).Cells("col_Detail").ReadOnly = True

                    grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value = 0
                    grdAdjustItem.Rows(i).Cells("col_Count_1st").Value = 0
                    grdAdjustItem.Rows(i).Cells("col_DiffQty").Value = 0
                End If
            Next



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub getAction(ByVal Diff_Qty As Double, ByVal Adjust_QTY As Double, ByVal grdrow As Integer)
        Try
            'Dim absDiffQTY, diffqty As Double
            'diffqty = Adjust_QTY - QTYinWH
            'absDiffQTY = Abs(diffqty)
            If Diff_Qty < 0 Then
                grdAdjustItem.Rows(grdrow).Cells("col_Detail").Value = "Adjust OUT. " & Diff_Qty
            Else
                grdAdjustItem.Rows(grdrow).Cells("col_Detail").Value = "Adjust IN. " & Diff_Qty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirn_Adjust.Click
        Try
            'TODO : Fix massege
            Dim xMSG0 As String = "มีรายการที่ยังไม่ได้ตรวจนับ"
            Dim xMSG1 As String = "ระบบจะทำการปรับยอดจำนวนสินค้าในคลังตามที่นับได้จริง"
            Dim xMSG2 As String = "คุณต้องการยืนยันรายการใช่หรือไม่ ?"

            If chk_AdjustQTY() = False Then
                If W_MSG_Confirm(xMSG0 & Chr(13) & xMSG1 & Chr(13) & xMSG2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            Else
                If W_MSG_Confirm(xMSG1 & Chr(13) & xMSG2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            'Check reserve warning
            Dim dtAdj As New DataTable
            Dim objCon As New DBType_SQLServer
            Dim xSQL As String = ""
            xSQL = " SELECT LB.TAG_NO "
            xSQL &= " FROM tb_LocationBalance LB "
            xSQL &= " WHERE LB.ReserveQty > 0 "
            xSQL &= String.Format("  AND LB.TAG_NO in (select TAG_NO from tb_AdjustItemLocation where Adjust_Index = '{0}' )", Me.Adjust_Index)
            xSQL &= " GROUP BY LB.TAG_NO "
            dtAdj = objCon.DBExeQuery(xSQL)
            Dim strArr As String = "มีรายการ TAG โดนจองไม่สามารถยืนยันรายการได้ !!!"
            For Each drRow As DataRow In dtAdj.Rows
                strArr &= Chr(13) & drRow("TAG_NO").ToString
            Next
            If dtAdj.Rows.Count > 0 Then
                W_MSG_Error(strArr)
                Exit Sub
            End If
            '------------------------------------------------------------
            'Check reserve warning
            'Dim dtAdj As New DataTable
            'Dim objCon As New DBType_SQLServer
            'Dim xSQL As String = ""
            xSQL = " SELECT AJ.Adjust_No,AJI.TAG_NO"
            xSQL &= " FROM tb_AdjustItemLocation AJI inner join tb_Adjust AJ on AJI.Adjust_Index = AJ.Adjust_Index"
            xSQL &= " WHERE AJ.Status not in (-1,3) "
            xSQL &= String.Format(" AND (ISNULL(AJI.TAG_NO,'') <> '') AND AJI.TAG_NO in (select TAG_NO from tb_AdjustItemLocation where Adjust_Index = '{0}' )", Me.Adjust_Index)
            xSQL &= String.Format(" AND AJI.Adjust_Index != '{0}' ", Me.Adjust_Index)
            xSQL &= " GROUP BY AJ.Adjust_No,AJI.TAG_NO "
            dtAdj = objCon.DBExeQuery(xSQL)
            strArr = "มีรายการ TAG ในรายการตรวจนับอื่นไม่สามารถยืนยันรายการได้ !!!"
            For Each drRow As DataRow In dtAdj.Rows
                strArr &= Chr(13) & "TAG No : " & drRow("TAG_NO").ToString & " เอกสารเลขที่ : " & drRow("Adjust_No").ToString
            Next
            If dtAdj.Rows.Count > 0 Then
                W_MSG_Error(strArr)
                Exit Sub
            End If
            '------------------------------------------------------------
            xSQL = " SELECT AJI.TAG_NO,L.Location_Alias"
            xSQL &= " FROM tb_AdjustItemLocation AJI inner join ms_Location L on L.Location_Index = AJI.Location_Index"
            xSQL &= " WHERE AJI.Qty_1st_Count > 0 "
            xSQL &= String.Format(" AND AJI.Adjust_Index = '{0}' ", Me.Adjust_Index)
            xSQL &= " GROUP BY AJI.TAG_NO,L.Location_Alias "
            dtAdj = objCon.DBExeQuery(xSQL)
            strArr = "มีรายการ TAG ในรายการตำแหน่งมากกว่า 1 ตำแหน่ง ไม่สามารถยืนยันรายการได้ !!!"
            Dim bChk As Boolean = False
            For Each drRow As DataRow In dtAdj.Rows
                Dim drCheckL() As DataRow = dtAdj.Select(String.Format("TAG_NO='{0}'", drRow("TAG_NO")), "Location_Alias")
                If drCheckL.Length > 1 Then
                    strArr &= Chr(13) & "TAG No : " & drRow("TAG_NO").ToString & " ตำแหน่ง : " & drRow("Location_Alias").ToString
                    bChk = True
                End If

            Next
            If bChk Then
                W_MSG_Error(strArr)
                Exit Sub
            End If
            '------------------------------------------------------------

            ''Auto Move (Transfer)
            'Dim oAdjust As New Adjust_Stock 'AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            'If oAdjust.Confirm_Adjust_Status_AutoMove_KSL(Me.Adjust_Index, "3") = True Then
            '    W_MSG_Information_ByIndex("1")
            '    Me.getAdjustItemDetail(Me._adjust_index)
            'End If
            'Auto Out,In (Withdraw and Receive)
            Dim oAdjust As New Adjust_Stock 'AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            If oAdjust.Confirm_Adjust_Status_AutoIN_AutoOUT_KSL_V2(Me.Adjust_Index, "3") = True Then
                W_MSG_Information_ByIndex("1")
                Me.getAdjustItemDetail(Me._adjust_index)
            End If

            btnAdjust_Confirm.Enabled = False
            btnConfirn_Adjust.Enabled = False
            Me.btnAdjust.Enabled = False
            btnPrint_Adjust.Enabled = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function chk_AdjustQTY() As Boolean
        Try
            For i As Integer = 0 To grdAdjustItem.Rows.Count - 1
                If grdAdjustItem.Rows(i).Cells("col_adjuststatus").Value <> -9 Then
                    Return False
                End If
                'If grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").Value = 0 Then
                '    If grdAdjustItem.Rows(i).Cells("col_Qty_Bal").Value <> 0 Then
                '        Return False
                '    End If
                'End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub grdAdjustItem_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAdjustItem.Sorted
        Try
            CalSubtotalAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdAdjustItem_CellEndEdit_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdjustItem.CellEndEdit
        Try
            If grdAdjustItem.RowCount <= 0 Then
                Exit Sub
            End If
            Select Case e.ColumnIndex
                Case grdAdjustItem.Rows(e.RowIndex).Cells("col_Adjust_Qty").ColumnIndex
                    If CInt(grdAdjustItem.Rows(e.RowIndex).Cells("col_adjuststatus").Value) <> -9 Then
                        W_MSG_Information("รายการนี้ยังไม่ได้ทำการตรวจนับ")
                        grdAdjustItem.Rows(e.RowIndex).Cells("col_Adjust_Qty").Value = 0
                        grdAdjustItem.Rows(e.RowIndex).Cells("col_Detail").Value = ""
                        Exit Sub
                    End If
                    CalSubtotalAmount()
                Case grdAdjustItem.Rows(e.RowIndex).Cells("col_Count_1st").ColumnIndex
                    'If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdAdjustItem.CurrentRow.Cells("col_Count_1st").Value, GetType(Integer)) > 0 Then
                    '    grdAdjustItem.CurrentRow.Cells("Col_Flag_Chk").Value = 1
                    'End If
                    If Me.grdAdjustItem.Rows(e.RowIndex).Cells("col_Count_1st").Value <> Me._Keep_Qty Then
                        grdAdjustItem.Rows(e.RowIndex).Cells("Col_Flag_Chk").Value = 1
                    End If

            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Adjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint_Adjust.Click
        Try
            ExportToExcel(grdAdjustItem)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal grdExport As DataGridView)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 2
            Dim ExcelApp As Excel.Application
            Dim ExcelBooks As Excel.Workbook
            Dim ExcelSheets As Excel.Worksheet
            ExcelApp = New Excel.Application

            Dim CurrentThread As System.Threading.Thread
            CurrentThread = System.Threading.Thread.CurrentThread
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US") 'เพิ่ม ton
            ' CurrentThread.CurrentCulture = New CultureInfo("en-GB")

            ExcelApp.Visible = True
            ExcelBooks = ExcelApp.Workbooks.Add()
            ExcelSheets = DirectCast(ExcelBooks.Worksheets(1), Excel.Worksheet)

            i = 0
            j = 2

            With ExcelSheets
                .Columns().ColumnWidth = 22


                .Range("D" & j.ToString()).Value = "สรุปรายการตรวจนับ"
                .Range("D" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("D" & j.ToString()).Font.Bold = True
                .Range("D" & j.ToString()).Font.Size = 14
                '.Range("A1").Interior.Color = RGB(224, 224, 224)

                j += 1

                .Range("A" & j.ToString()).Value = "รายการที่ต้องรับเข้า"
                .Range("A" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("A" & j.ToString()).Font.Bold = True
                .Range("A" & j.ToString()).Font.Size = 12
                ''.Range("A2").Interior.Color = RGB(224, 224, 224)

                j += 1

                Dim Col As Integer = 0
                Dim strCol As String = "A"
                Dim iChar As Integer = 65

                For Col = 0 To grdExport.ColumnCount - 1
                    strCol = Chr(iChar)
                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(255, 255, 128)
                    iChar += 1
                Next

                j += 1

                Dim dtgrdExport As New DataTable
                dtgrdExport = grdExport.DataSource


                Dim Row As Integer = 0
                'strCol = "A"
                'iChar = 65
                'Col = 0
                For Row = 0 To grdExport.RowCount - 1
                    strCol = "A"
                    iChar = 65
                    Col = 0
                    If CDbl(grdExport.Rows(Row).Cells(10).Value) <= 0 Then
                        Continue For
                    End If
                    For Col = 0 To grdExport.ColumnCount - 1
                        strCol = Chr(iChar)
                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If
                        .Range(strCol & j.ToString()).Value = strData
                        .Range(strCol & j.ToString()).Font.Size = 9

                        iChar += 1
                    Next
                    j += 1
                Next


                j += 1

                .Range("A" & j.ToString()).Value = "รายการที่ต้องเบิกออก"
                .Range("A" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("A" & j.ToString()).Font.Bold = True
                .Range("A" & j.ToString()).Font.Size = 12
                ''.Range("A2").Interior.Color = RGB(224, 224, 224)

                j += 1

                Col = 0
                strCol = "A"
                iChar = 65

                For Col = 0 To grdExport.ColumnCount - 1
                    strCol = Chr(iChar)
                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(255, 255, 128)
                    iChar += 1
                Next

                j += 1

                For Row = 0 To grdExport.RowCount - 1
                    strCol = "A"
                    iChar = 65
                    Col = 0
                    If CDbl(grdExport.Rows(Row).Cells(10).Value) >= 0 Then
                        Continue For
                    End If
                    For Col = 0 To grdExport.ColumnCount - 1
                        strCol = Chr(iChar)
                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If
                        .Range(strCol & j.ToString()).Value = strData
                        .Range(strCol & j.ToString()).Font.Size = 9

                        iChar += 1
                    Next
                    j += 1
                Next

            End With

            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.getAdjustItemDetail(Me._adjust_index)
            'If chk_AdjustQTY() = True Then
            '    btnConfirn_Adjust.Enabled = True
            'End If
            'If _ForReadOnly = True Then
            '    grdAdjustItem.ReadOnly = True
            '    btnConfirn_Adjust.Enabled = False
            '    btnAdjust_Confirm.Enabled = False
            '    btnPrint_Adjust.Enabled = True
            'Else
            '    For i As Integer = 0 To grdAdjustItem.Rows.Count - 1
            '        grdAdjustItem.Rows(i).Cells("col_Adjust_Qty").ReadOnly = False
            '    Next
            'End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjust.Click
        Try
            Dim frm As New frmAdjust_Stock
            frm.Adjust_Index = Me.Adjust_Index
            frm.ShowDialog()
            Me.getAdjustItemDetail(Me._adjust_index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAdjust_Confirm_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

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
                            If grdAdjustItem.CurrentRow.Cells(Column_Index).EditedFormattedValue.ToString <> "" Then
                                Dim strData As String = CStr(grdAdjustItem.CurrentRow.Cells(Column_Index).EditedFormattedValue)
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
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = CStr(grdAdjustItem.CurrentCell.ColumnIndex)

            'Select Case Column_Index
            '    Case Is = CStr(grdResendMSG.Columns("ColUnitPrice").Index)
            e.Handled = Check_GrdKeyPress(0, e, CInt(Column_Index))
            '    Case Is = CStr(grdResendMSG.Columns("ColQty").Index)
            'e.Handled = Check_GrdKeyPress(0, e, CInt(Column_Index))
            '    Case Is = CStr(grdResendMSG.Columns("Col_B_For_Vet").Index)
            'e.Handled = Check_GrdKeyPress(0, e, CInt(Column_Index))


            'End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub grdAdjustItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdAdjustItem.EditingControlShowing
        Try

            Dim strName As String = grdAdjustItem.Columns(grdAdjustItem.CurrentCell.ColumnIndex).Name
            If (strName <> "Col_Flag_Chk") Then
                Dim txtEdit As TextBox = CType(e.Control, TextBox)
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BntGridMng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntGridMng.Click
        Try
            Dim oFunction As New config_Column
            oFunction.Insert_config_DataGridColumn(Me, Me.grdAdjustItem, W_Module.WV_UserName)

            Dim frm As New SetGrid_Column
            frm.FormName = Me.Name
            frm.GridName = Me.grdAdjustItem.Name
            frm.ShowDialog()

            ' *********************
            oFunction.AdjustColumnOrder(Me, Me.grdAdjustItem, W_Module.WV_UserName)
            frm.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdAdjustItem_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdAdjustItem.CellBeginEdit
        Try
            If grdAdjustItem.RowCount <= 0 Then
                Exit Sub
            End If

            Me._Keep_Qty = Me.grdAdjustItem.Rows(e.RowIndex).Cells("col_Count_1st").Value


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdAdjustItem_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdAdjustItem.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim hit As DataGridView.HitTestInfo = Me.grdAdjustItem.HitTest(e.X, e.Y)
                Me.grdAdjustItem.Rows(hit.RowIndex).Selected = True
                Me.grdAdjustItem.ContextMenu.Show(Me.grdAdjustItem, New Point(hit.RowIndex, hit.ColumnIndex))

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub mnuInventoryAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventoryAdjust.Click
        Try

            'Save With Highlight Rows ต้องไม่ใช่รายการที่ Key
            'จำนวนเท่ากับจำนวนที่อยู่ในคลัง
            Dim selectedRowCount As Integer = grdAdjustItem.Rows.GetRowCount(DataGridViewElementStates.Selected)
            If selectedRowCount > 0 Then
                'Dim sb As New System.Text.StringBuilder()
                Dim i As Integer
                For i = 0 To selectedRowCount - 1
                    'sb.Append("Row: ")
                    'sb.Append(grdAdjustItem.SelectedRows(i).Index.ToString())
                    'sb.Append(Environment.NewLine)

                    Dim iRowSelect As Integer = grdAdjustItem.SelectedRows(i).Index
                    If CBool(Me.grdAdjustItem.Rows(iRowSelect).Cells("Col_Flag_Chk").Value) = True Then
                        Me.grdAdjustItem.Rows(iRowSelect).Cells("col_Count_1st").Value = 0
                        Me.grdAdjustItem.Rows(iRowSelect).Cells("Col_Flag_Chk").Value = False
                    Else
                        Me.grdAdjustItem.Rows(iRowSelect).Cells("col_Count_1st").Value = grdAdjustItem.Rows(iRowSelect).Cells("col_Qty_Bal").Value
                        Me.grdAdjustItem.Rows(iRowSelect).Cells("Col_Flag_Chk").Value = True
                    End If


                Next i
                'sb.Append("Total: " + selectedRowCount.ToString())
                'MessageBox.Show(sb.ToString(), "Selected Rows")
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdAdjustItem.RowCount = 0 Then Exit Sub

            If W_MSG_Confirm("คุณต้องการลบรายการที่ " & Me.grdAdjustItem.CurrentRow.Cells("col_Seq").Value & " ใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                Dim AdjustItemLocation_Index As String = Me.grdAdjustItem.CurrentRow.Cells("col_index").Value
                Dim objCon As New DBType_SQLServer
                If objCon.DBExeNonQuery(String.Format("DELETE tb_AdjustItemLocation where AdjustItemLocation_Index = '{0}'", AdjustItemLocation_Index)) > 0 Then
                    Me.grdAdjustItem.Rows.RemoveAt(grdAdjustItem.CurrentRow.Index)
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
End Class