Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language

Public Class frmWithdrawAsset_TAG
    Private xCONDB As New DBType_SQLServer
    Private xDT As New DataTable
    Private xSQL As String = ""


    Private _Withdraw_Index As String
    Public Property Withdraw_Index() As String
        Get
            Return _Withdraw_Index
        End Get
        Set(ByVal value As String)
            _Withdraw_Index = value
        End Set
    End Property

    Private Sub frmWithdrawAsset_TAG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            'STEP 1 : Load All
            xSQL = String.Format(" SELECT chkSelect = 0,* FROM VIEW_KSL_Withdraw_TAG WHERE Withdraw_Index = '{0}' Order by PRQ,Sku_Id,LOT", Me._Withdraw_Index)
            xDT = xCONDB.DBExeQuery(xSQL)
            If xDT.Rows.Count > 0 Then
                Me.txtWithdraw_No.Text = xDT.Rows(0)("Withdraw_No").ToString
                Me.txtCustomer_Id.Text = xDT.Rows(0)("Customer_Id").ToString
                Me.txtCustomer_Name.Text = xDT.Rows(0)("Customer_Name").ToString
            End If

            'STEP 2 : Load non tag
            Dim arrData() As DataRow = xDT.Select("isnull(TagOut_No,'') = ''")
            Dim dtData As New DataTable
            dtData = xDT.Clone()
            If arrData.Length > 0 Then
                For Each drRow As DataRow In arrData
                    dtData.Rows.Add(drRow.ItemArray)
                Next
                dtData.AcceptChanges()
            End If
            Me.grdNonTAG.AutoGenerateColumns = False
            Me.grdNonTAG.DataSource = dtData

            'STEP 3 : Load tag
            arrData = xDT.Select("isnull(TagOut_No,'') <> ''")
            dtData = New DataTable
            dtData = xDT.Clone()
            If arrData.Length > 0 Then
                For Each drRow As DataRow In arrData
                    dtData.Rows.Add(drRow.ItemArray)
                Next
                dtData.AcceptChanges()
            End If
            dtData.Columns("chkSelect").ReadOnly = False
            Me.grdTAG.AutoGenerateColumns = False
            Me.grdTAG.DataSource = dtData
            'Reset
            Me.chkSelect.ReadOnly = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            If Me.grdNonTAG.RowCount = 0 Then Exit Sub
            Me.AutoTag("ADD", Me.grdNonTAG.CurrentRow.Index)
            Me.LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuto.Click
        Try
            If Me.grdNonTAG.RowCount = 0 Then Exit Sub
            For iRow As Integer = 0 To Me.grdNonTAG.RowCount - 1
                Me.AutoTag("ADD", iRow)
            Next
            Me.LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If Me.grdTAG.RowCount = 0 Then Exit Sub
            CType(Me.grdTAG.DataSource, DataTable).AcceptChanges()
            Dim drArr() As DataRow = CType(Me.grdTAG.DataSource, DataTable).Select("chkSelect=1")
            If drArr.Length = 0 Then Exit Sub
            For iRow As Integer = 0 To Me.grdTAG.RowCount - 1
                If CType(Me.grdTAG.Rows(iRow).Cells("chkSelect").Value, Boolean) Then
                    Me.AutoTag("DEL", iRow)
                End If
            Next
            Me.LoadData()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub AutoTag(ByVal Key As String, ByVal iRow As Integer)
        Try
            Dim dtData As New DataTable
            ''Dim iRow As Integer = 0
            Select Case Key
                Case "ADD"
                    With Me.grdNonTAG
                        Dim objAutoNumber As New WMS_STD_Formula.Sy_AutoNumber
                        Dim TagOut_No As String = objAutoNumber.getSys_Value("TagOut_No")

                        dtData = CType(.DataSource, DataTable)
                        'iRow = .CurrentRow.Index
                        xSQL = String.Format(" UPDATE WIL SET WIL.TagOut_No ='{0}' ", TagOut_No)
                        xSQL &= " FROM tb_WithdrawItemLocation WIL inner join tb_WithdrawItem WI on WI.WithdrawItem_Index = WIL.WithdrawItem_Index "
                        xSQL &= String.Format(" WHERE WIL.Withdraw_Index = '{0}'", Me._Withdraw_Index)
                        xSQL &= String.Format(" AND WI.DocumentPlan_Index = '{0}'", dtData.Rows(iRow).Item("DocumentPlan_Index").ToString)
                        xSQL &= String.Format(" AND WIL.Plot = '{0}'", dtData.Rows(iRow).Item("LOT").ToString)
                        xSQL &= String.Format(" AND WIL.Sku_Index = '{0}'", dtData.Rows(iRow).Item("Sku_Index").ToString)
                        xCONDB.DBExeQuery(xSQL)

                    End With

                Case "DEL"
                    With Me.grdTAG
                        dtData = CType(.DataSource, DataTable)
                        'iRow = .CurrentRow.Index
                        xSQL = String.Format(" UPDATE tb_WithdrawItemLocation set TagOut_No ='{0}' ", "")
                        xSQL &= String.Format(" WHERE Withdraw_Index = '{0}'", Me._Withdraw_Index)
                        xSQL &= String.Format(" AND TagOut_No = '{0}'", dtData.Rows(iRow).Item("TagOut_No").ToString)
                        xCONDB.DBExeQuery(xSQL)
                    End With
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrint_TAG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint_TAG.Click
        Try
            'TODO : Report TAG
            'W_MSG_Error("°¥∑” Èπµ’πÕ–‰√¬—ß‰¡Ë‰¥È∑”")
            If Me.grdTAG.RowCount = 0 Then Exit Sub
            CType(Me.grdTAG.DataSource, DataTable).AcceptChanges()
            Dim drArr() As DataRow = CType(Me.grdTAG.DataSource, DataTable).Select("chkSelect=1")
            If drArr.Length = 0 Then Exit Sub
            Dim TagOut_No As String = ""
            For Each drRow As DataRow In drArr
                TagOut_No &= "'" & drRow("TagOut_No").ToString() & "',"
            Next

            TagOut_No = TagOut_No.Substring(0, TagOut_No.Trim.Length - 1)
            Dim strWhere As String = " and TagOut_No in (" & TagOut_No & ")"


            Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_OUTB_Report.Loading_Report("TAG_WITHDRAW", strWhere)
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub


    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If Me.grdTAG.RowCount = 0 Then Exit Sub
            Dim i As Integer
            For i = 0 To Me.grdTAG.Rows.Count - 1
                Me.grdTAG.Rows(i).Cells("chkSelect").Value = chkSelectAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
      
    End Sub
End Class