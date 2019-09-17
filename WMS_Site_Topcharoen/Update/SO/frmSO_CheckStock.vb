Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language

Public Class frmSO_CheckStock
    Public SalesOrder_No As String = ""
    Public SalesOrder_Index As String = ""
    Public Status As Integer = 0
    Public isChange As Boolean = False
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If (Me.DataGridView1.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Dim ds As New DataSet

            Dim objExport As New Export_Excel_KC
            ds.Tables.Add(objExport.DataGridViewToDataTable(Me.DataGridView1))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm") & "_" & SalesOrder_No
            objExport.export(ds, Me.Text)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub frmSO_CheckStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Status = 1 Then
                Me.btnConfirm.Enabled = True
            End If
            For i As Integer = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Rows(i).Cells("col_Total_Qty_Request").Value <= 0 Then
                    Me.DataGridView1.Rows(i).Cells("col_Total_Qty_Request").Style.BackColor = Color.Red
                End If
                If Me.DataGridView1.Rows(i).Cells("col_Total_Qty").Value > Me.DataGridView1.Rows(i).Cells("col_Qty_Bal").Value Then
                    Me.DataGridView1.Rows(i).Cells("col_Total_Qty").Style.BackColor = Color.Red
                End If

            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim xdb As New DBType_SQLServer
            Dim xdt As New DataTable
            xdt = xdb.DBExeQuery(" SELECT tb_SalesOrder.*,ms_DocumentType.Document_Group_Name FROM tb_SalesOrder INNER JOIN ms_DocumentType ON ms_DocumentType.DocumentType_Index = tb_SalesOrder.DocumentType_Index WHERE SalesOrder_Index = '" & SalesOrder_Index & "'")
            'ตรวจสอบเส้นทาง
            Dim xsql As String = ""
            xsql &= " (ISNULL(Route_Index,'') IN ('0010000000000',''))"
            xsql &= " AND (ISNULL(SubRoute_Index,'') IN ('0010000000000',''))"
            xsql &= " AND (ISNULL(DistributionCenter_Index,'') IN ('0010000000000',''))"
            Dim drArrCheckRoute() As DataRow = xdt.Select(xsql)
            If drArrCheckRoute.Length > 0 Then
                W_MSG_Information("กรุณาตรวจสอบ เส้นทางหลัก เส้นทางย่อยและศูนย์กระจาย")
                Exit Sub
            End If

            drArrCheckRoute = CType(Me.DataGridView1.DataSource, DataTable).Select("Qty_Bal < Total_Qty")
            If drArrCheckRoute.Length > 0 Then
                If W_MSG_Confirm("จำนวนที่สั่งมากกว่าสินค้าคงเหลือ คุณต้องการยืนยันใช่หรือไม่") = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
                'W_MSG_Information("มีรายการจำนวนที่สั่ง มากกว่า สินค้าคงเหลือ")
                'Exit Sub
            End If

            drArrCheckRoute = CType(Me.DataGridView1.DataSource, DataTable).Select()
            Dim Result As Integer = 0
            For Each drColor As DataRow In drArrCheckRoute
                If drColor("Total_Qty") > drColor("Qty_Bal") And drColor("Total_Qty") > 0 Then
                    Result += 1
                    'แดง
                    xdb.DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 2 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_Index, drColor("Sku_Index").ToString))
                ElseIf drColor("Total_Qty") <= drColor("Qty_Bal") And drColor("Total_Qty") > 0 Then
                    'ไม่สี
                    xdb.DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 0 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_Index, drColor("Sku_Index").ToString))
                End If
            Next



            Dim objSOTransaction As New cls_KSL
            If (New clsSO().canConfirmSO(xdt.Rows(0)("SalesOrder_Index").ToString)) Then
                If Result = 0 Then
                    'เขียว
                    xdb.DBExeNonQuery("Update tb_salesorder set RGB_Check = '1' where SalesOrder_Index = '" & SalesOrder_Index & "'")
                Else
                    'แดง
                    xdb.DBExeNonQuery("Update tb_salesorder set RGB_Check = '2' where SalesOrder_Index = '" & SalesOrder_Index & "'")
                End If
                'ยืนยัน
                objSOTransaction.Confirm_SO(xdt.Rows(0)("SalesOrder_Index").ToString, xdt.Rows(0)("SalesOrder_No").ToString)
                Me.Status = 2
            End If


            W_MSG_Information_ByIndex(300036)
            objSOTransaction = Nothing
            Me.isChange = True
            Me.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            'CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
            'Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status not in (2,6)")
            'For Each drso As DataRow In drArr
            '    W_MSG_Information(GetMessage_Data("400030") & drso("SalesOrder_No") & GetMessage_Data("400059"))
            '    Exit Sub
            'Next
            Select Case Me.Status
                Case "2", "6" 'รอเบิก
                    Dim objDB As New DBType_SQLServer
                    Dim dtDB As New DataTable
                    dtDB = objDB.DBExeQuery(String.Format("select * from tb_SalesOrderItem where isnull(Total_Qty_Withdraw,0) > 0 and SalesOrder_Index = '{0}' ", Me.SalesOrder_Index))
                    If dtDB.Rows.Count > 0 Then
                        W_MSG_Confirm("มีรายการเบิกสินค้าไม่สามารถคืนสถานะได้")
                        Exit Sub
                    End If
                    If W_MSG_Confirm("คุณต้องการปรับแก้ไขเอกสารใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then Exit Sub
                    Dim tSaleOrder_Index As String = Me.SalesOrder_Index
                    Dim tSaleOrder_No As String = Me.SalesOrder_No
                    Dim oupdate As New ml_TSS
                    oupdate.UPDATE_STATUS_SO(tSaleOrder_Index, 1)
                    ''เขียว
                    objDB.DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 0 where SalesOrder_Index = '{0}'", tSaleOrder_Index))
                    objDB.DBExeNonQuery(String.Format("Update tb_salesorder set RGB_Check = 0 where SalesOrder_Index = '{0}'", tSaleOrder_Index))



                    ' --- STEP 3: Update status in Sy_Audit_Log
                    'insert log
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 10
                    obj_cls.Description = "คืนสถานะเป็นรอยืนยัน"
                    obj_cls.Document_Index = tSaleOrder_Index
                    obj_cls.Document_No = tSaleOrder_No
                    obj_cls.Log_Type_ID = 154
                    obj_cls.Insert_Master()

                    W_MSG_Information_ByIndex(1)
                    Me.Status = 1
                    Me.isChange = True
                    Me.btnConfirm.Enabled = True
                    'Me.Close()

                Case Else
                    W_MSG_Information("สถานะต้องเป็น 'รอเบิก','ค้างจ่าย' เท่านั้น")
            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class