Imports WMS_STD_Formula
Imports System.Data.SqlClient
Public Class PutawaySuggestLocation : Inherits DBType_SQLServer




    Public Function Suggest_Location(ByVal Tag_Index As String, ByVal Sku_Index As String, _
    ByVal ProductType_Index As String, ByVal ItemStatus_Index As String, _
    ByVal DocumentType_Index As String, ByVal Customer_Index As String, _
    ByVal Total_Qty As Double, ByVal Total_Weight As Double, ByVal Total_Volume As String, _
    Optional ByVal _Connection As SqlConnection = Nothing, Optional ByVal _myTrans As SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Try


            If _Connection Is Nothing Then
                connectDB()
                _Connection = Connection
                _myTrans = Connection.BeginTransaction()
                SQLServerCommand.Transaction = _myTrans
                IsNotPassTransaction = True
            End If



            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Tag_Index", SqlDbType.VarChar, 13).Value = Tag_Index
                .Add("@SKU_INDEX", SqlDbType.VarChar, 13).Value = Sku_Index
                .Add("@PRODUCTTYPE_INDEX", SqlDbType.VarChar, 13).Value = ProductType_Index
                .Add("@ITEMSTATUS_INDEX", SqlDbType.VarChar, 13).Value = ItemStatus_Index
                .Add("@DOCUMENTTYPE_INDEX", SqlDbType.VarChar, 13).Value = DocumentType_Index
                .Add("@CUSTOMER_INDEX", SqlDbType.VarChar, 13).Value = Customer_Index
                .Add("@TOTAL_QTY", SqlDbType.Decimal).Value = Total_Qty
                .Add("@TOTAL_WEIGHT", SqlDbType.Decimal).Value = Total_Weight
                .Add("@TOTAL_VOLUME", SqlDbType.Decimal).Value = Total_Volume
            End With


            Dim Location_Alias As String = ""
            Dim dtLocation As New DataTable
            dtLocation = DBExeQuery(" spSuggest_Location  @Tag_Index , @SKU_INDEX,@PRODUCTTYPE_INDEX,@ITEMSTATUS_INDEX,@DOCUMENTTYPE_INDEX,@CUSTOMER_INDEX" & _
                                    ",@TOTAL_QTY,@TOTAL_WEIGHT,@TOTAL_VOLUME", _Connection, _myTrans)


            If dtLocation.Rows.Count > 0 Then

                If Tag_Index <> "" Then
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@Tag_Index", SqlDbType.VarChar, 13).Value = Tag_Index
                        .Add("@Location_Index", SqlDbType.VarChar, 13).Value = dtLocation.Rows(0)("Location_Index").ToString
                    End With
                    DBExeNonQuery(" UPDATE tb_Tag SET tb_Tag.Suggest_Location_Index	= @Location_Index WHERE Tag_Index=@Tag_Index ", _Connection, _myTrans)
                End If

                Location_Alias = dtLocation.Rows(0)("Location_Alias").ToString
            End If
            If IsNotPassTransaction Then
                _myTrans.Commit()
            End If
            Return Location_Alias
        Catch ex As Exception
            If IsNotPassTransaction Then
                _myTrans.Rollback()
            End If

            Throw ex
        Finally

        End Try
    End Function







End Class
