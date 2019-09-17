Imports WMS_STD_Master.W_Language
Public Class frmEditSOItem


#Region " + Variable + "



#End Region

#Region " + Property + "


    Private _SalesOrderItem_Index As String
    Public Property SalesOrderItem_Index() As String
        Get
            Return _SalesOrderItem_Index
        End Get
        Set(ByVal value As String)
            _SalesOrderItem_Index = value
        End Set
    End Property



#End Region

#Region " + Function + "

#End Region

#Region " + Sub + "

    Private Sub GetData(ByVal SalesOrderItem_Index As String, Optional ByVal IsRefresh As Boolean = False)
        Try
            Dim obj As New GrouppingConditionPicking
            Dim dt As DataTable = obj.getEditSOItem(SalesOrderItem_Index)

            If dt.Rows.Count > 0 Then

                Me.txtTotal_Qty_Plan.Text = dt.Rows(0)("Total_Qty").ToString
                Me.txtTotal_Qty_Withdraw.Text = dt.Rows(0)("Total_Qty_Withdraw").ToString
                Me.lblOrg_Total_Qty.Text = dt.Rows(0)("Org_Total_Qty").ToString

                If IsRefresh = False Then
                    Me.txtTotal_Qty.Text = dt.Rows(0)("Total_Qty").ToString
                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateSOItem(ByVal SalesOrderItem_Index As String, ByVal Total_Qty As Decimal)
        Try



            Dim obj As New GrouppingConditionPicking
            If obj.UpdateEditSOItem(SalesOrderItem_Index, Total_Qty) > 0 Then
                W_MSG_Information("บันทึกเสร็จสิ้น")
            Else
                W_MSG_Error("ไม่สามารถบันทึกได้!!!")
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

#Region " + Event + "

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If IsNumeric(Me.txtTotal_Qty.Text) = False Then
                W_MSG_Error("จำนวนแก้ไข ไม่ถูกต้อง!!!")
                Exit Sub
            End If
            Dim Total_Qty As Decimal = 0
            Total_Qty = Me.txtTotal_Qty.Text
            If Total_Qty = 0 Then
                W_MSG_Error("จำนวนแก้ไข ไม่ถูกต้อง!!!")
                Exit Sub
            End If

            If Total_Qty < CDec(Me.txtTotal_Qty_Withdraw.Text) Then
                W_MSG_Error("จำนวนแก้ไข น้อยกว่าจำนวนเบิกแล้ว!!!")
                Exit Sub
            End If


            If Total_Qty > CDec(Me.lblOrg_Total_Qty.Text) And CDec(Me.lblOrg_Total_Qty.Text) > 0 Then
                W_MSG_Error("จำนวนแก้ไข มากกว่า Orginal Qty!!!")
                Exit Sub
            End If



            UpdateSOItem(_SalesOrderItem_Index, Me.txtTotal_Qty.Text)



        Catch ex As Exception
            W_MSG_Error(ex.Message)
            GetData(_SalesOrderItem_Index, True)
        End Try
    End Sub
    Private Sub frmEditSOItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

     
            If _SalesOrderItem_Index = "" Then
                W_MSG_Error("System Error!!!")
                Me.Close()
            End If

            GetData(_SalesOrderItem_Index)



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region


  

End Class