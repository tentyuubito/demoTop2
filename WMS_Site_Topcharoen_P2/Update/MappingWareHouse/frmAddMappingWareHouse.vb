Imports WMS_STD_Master.W_Language

Public Class frmAddMappingWareHouse
    Dim _Excute As New DBType_SQLServer

    Private _SaveType As Integer
    Public Property SaveType() As Integer
        Get
            Return _SaveType
        End Get
        Set(ByVal value As Integer)
            _SaveType = value
        End Set
    End Property

    Private _index As Integer
    Public Property index() As Integer
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property


    Private Sub getcboDistribution()
        Try
            Dim dt As New DataTable
            dt = _Excute.DBExeQuery("select * from ms_DistributionCenter where status_id <> -1")

            With cboDtisributionCenter
                .DisplayMember = "DistributionCenter_Id"
                .ValueMember = "DistributionCenter_Index"
                .DataSource = dt
            End With
            cboDtisributionCenter.SelectedValue = "0010000000000"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Public Sub ShowMappingST_WMS(Optional ByVal pwhere As String = "")
        Try
            Dim _dt As New DataTable
            Dim str_sql As String = ""
            str_sql = "Select WH.WAREHOUSEID,WH.WAREHOUSENAME,DS.DistributionCenter_Id,DS.DistributionCenter_Index  from ms_MappingWareHouseST_WMS WH "
            str_sql &= " left join ms_DistributionCenter DS "
            str_sql &= " on WH.DistributionCenter_Index = DS.DistributionCenter_Index and DS.status_id <> -1"
            str_sql &= " Where isnull(WH.Status_Id ,0)<> -1 " + pwhere
            _dt = _Excute.DBExeQuery(str_sql)

            If _dt.Rows.Count > 0 Then
                txtWareHouseID.Text = _dt.Rows(0).Item("WAREHOUSEID").ToString
                txtWareHouseName.Text = _dt.Rows(0).Item("WAREHOUSENAME").ToString
                cboDtisributionCenter.SelectedValue = _dt.Rows(0).Item("DistributionCenter_Index").ToString
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmAddMappingWareHouse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getcboDistribution()
            If SaveType = 2 Then
                ShowMappingST_WMS(String.Format(" and Map_index = '{0}' ", Me._index))
                'Update
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim str As String
            Dim _Check As String = _Excute.DBExeQuery_Scalar(String.Format("select count(*) from ms_MappingWareHouseST_WMS where WAREHOUSEID = '{0}'", Me._index))
            If txtWareHouseID.Text.Trim.Length = 0 Then
                W_MSG_Information("กรุณากรอกรหัส WareHouse")
                Exit Sub
            End If

            If SaveType = 1 Then
                If Not (_Check.Trim = "" Or _Check.Trim = "0") Then
                    W_MSG_Information("รหัส WareHouse ซ้ำ")
                    Exit Sub
                End If
                If txtWareHouseName.Text.Trim.Length = 0 Then
                    W_MSG_Information("กรุณากรอกชื่อ WareHouse")
                    Exit Sub
                End If
                str = "insert into  [dbo].[ms_MappingWareHouseST_WMS] ([WAREHOUSEID],[WAREHOUSENAME],[DistributionCenter_Index]) "
                str &= String.Format(" Values ('{0}','{1}','{2}')", txtWareHouseID.Text.Trim, txtWareHouseName.Text.Trim, cboDtisributionCenter.SelectedValue)

            Else
                str = String.Format("Update [ms_MappingWareHouseST_WMS] Set  WAREHOUSEID = '{0}',[WAREHOUSENAME]= '{1}',[DistributionCenter_Index]= '{2}' where Map_index = '{3}' " _
                , txtWareHouseID.Text.Trim, txtWareHouseName.Text.Trim, cboDtisributionCenter.SelectedValue, Me._index)


            End If
            _Excute.DBExeNonQuery(str)


            W_MSG_Information("บันทึกเรียบร้อย")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class