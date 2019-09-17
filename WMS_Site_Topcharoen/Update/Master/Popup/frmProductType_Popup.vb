Imports System.Data.SqlClient
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module

Public Class frmProductType_Popup

    Private _ProductType_Index As String
    Public Property ProductType_Index() As String
        Get
            Return _ProductType_Index
        End Get
        Set(ByVal value As String)
            _ProductType_Index = value
        End Set
    End Property

    Private _ProductType_Id As String
    Public Property ProductType_Id() As String
        Get
            Return _ProductType_Id
        End Get
        Set(ByVal value As String)
            _ProductType_Id = value
        End Set
    End Property

    Private _ProductType_Desc As String
    Public Property ProductType_Desc() As String
        Get
            Return _ProductType_Desc
        End Get
        Set(ByVal value As String)
            _ProductType_Desc = value
        End Set
    End Property

    Dim ds As DataSet

    Private cnn As New SqlClient.SqlConnection(WV_ConnectionString)

    Private Sub frmProductType_Popup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim oFunction As New W_Language
        'oFunction.SwitchLanguage(Me, 2)
        ''oFunction.SW_Language_Column(Me, Me.dgvData, 2)
        'oFunction = Nothing

        Me.dgvData.AutoGenerateColumns = False
        Try
            Me.defaultOnLoad()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub getDgvData()
        Try
            Me.ds = Me.getDataSet()
            Me.dgvData.DataSource = Me.ds.Tables(0)
            If (Me.dgvData.Rows.Count() = 0) Then
                Me.lblData_Count.Text = "ไม่พบข้อมูล"
                W_MSG_Information("ไม่พบข้อมูล")
            Else
                Me.lblData_Count.Text = String.Format("พบข้อมูล : {0}", Me.dgvData.Rows.Count().ToString("#,##0"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function getDataSet() As DataSet
        Dim ds As New DataSet()
        Try
            Dim SheetName As String = Now.ToString("yyyyMMdd_HHmm")
            Dim dt As New System.Data.DataTable
            Dim query As String = " select ProductType_Index,ProductType_Id,Description from ms_ProductType "
            Dim strWhere As String = ""
            Dim strGroupby As String = ""
            Dim strOrderby As String = ""

            Dim sqlCommand As New SqlCommand
            With sqlCommand
                .Parameters.Clear()
                If (Not Me.txtSearch.Text.Trim() = Nothing) Then
                    Select Case Me.cboSearch.SelectedIndex
                        Case 0 ' Id
                            strWhere &= String.Format(" {0} ProductType_Id like @ProductType_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                            .Parameters.Add("@ProductType_Id", SqlDbType.VarChar).Value = "%" & Me.txtSearch.Text.Trim() & "%"
                            strOrderby = " order by ProductType_Id asc,Description asc "
                        Case 1 ' Desc
                            strWhere &= String.Format(" {0} Description like @Description ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                            .Parameters.Add("@Description", SqlDbType.VarChar).Value = "%" & Me.txtSearch.Text.Trim() & "%"
                            strOrderby = " order by Description asc,ProductType_Id asc "
                    End Select
                End If
                .CommandType = CommandType.Text
                .CommandText = query & strWhere & strGroupby & strOrderby
                .Connection = Me.cnn
                .CommandTimeout = 0
            End With
            Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
            dt.TableName = SheetName
            da.Fill(dt)
            ds.Tables.Add(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Private Sub defaultOnLoad()
        Try
            Me.resetCondition()
            Me.getDgvData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub resetCondition()
        Try
            If (Me.cboSearch.Items.Count > 0) Then Me.cboSearch.SelectedIndex = 0
            Me.txtSearch.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getDgvData()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChoose.Click
        Try
            If (Me.dgvData.Rows.Count() = 0) Then Exit Sub
            If (Me.dgvData.CurrentRow.Cells("col_ProductType_Index").Value = Nothing) Then Exit Sub
            Me.ProductType_Index = Me.dgvData.CurrentRow.Cells("col_ProductType_Index").Value
            Me.ProductType_Id = Me.dgvData.CurrentRow.Cells("col_ProductType_Id").Value
            Me.ProductType_Desc = Me.dgvData.CurrentRow.Cells("col_ProductType_Desc").Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPre_Manifest_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.getDgvData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvData_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellDoubleClick
        Try
            If (Me.dgvData.Rows.Count() = 0) Then Exit Sub
            If (Me.dgvData.CurrentRow.Cells("col_ProductType_Index").Value = Nothing) Then Exit Sub
            Me.ProductType_Index = Me.dgvData.CurrentRow.Cells("col_ProductType_Index").Value
            Me.ProductType_Id = Me.dgvData.CurrentRow.Cells("col_ProductType_Id").Value
            Me.ProductType_Desc = Me.dgvData.CurrentRow.Cells("col_ProductType_Desc").Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class