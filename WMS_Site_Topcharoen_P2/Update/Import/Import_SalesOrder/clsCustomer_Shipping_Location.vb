Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class clsCustomer_Shipping_Location : Inherits DBType_SQLServer

    Public Function SelectIndex(ByVal strSQL As String) As String
        Try
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function SelectData(ByVal strSQL As String) As DataTable
        Try
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function ImportData(ByVal dt As DataTable, ByVal colList As List(Of ColArgs)) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            For Each dr As DataRow In dt.Rows
                Console.WriteLine("INSERT")

                ' BGN Customer_Shipping_Location_Index
                Dim objSy As New Sy_AutoNumber
                Dim Customer_Shipping_Location_Index = objSy.getSys_Value("Customer_Shipping_Location_Index")
                objSy = Nothing
                ' END Customer_Shipping_Location_Index

                strSQL = " select top 1 Customer_Shipping_Location_Index from ms_Customer_Shipping_Location where status_id<>-1 and Customer_Shipping_Index=@Customer_Shipping_Index and Customer_Shipping_Location_Id=@Customer_Shipping_Location_Id "
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 50).Value = Me.getValue(dr, colList, "Customer_Shipping_Location_Id")
                    .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = dr("__Customer_Shipping_Index").ToString()
                End With
                If Not (DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text) = Nothing) Then
                    Throw New Exception(String.Format("รหัสที่อยู่จัดส่งสินค้า {0} ซ้ำ", Me.getValue(dr, colList, "Customer_Shipping_Location_Id")))
                End If

                strSQL = " insert into ms_Customer_Shipping_Location(Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Route_Index,Customer_Shipping_Index,Shipping_Location_Name,Address,District_Index,Province_Index,Postcode,Tel,Fax,Mobile,Email,AmtWH,Contact_Person1,Contact_Person2,Contact_Person3,Remark,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,DayIndex,add_by,add_date,add_branch,isBangkok,SubRoute_Index,TransportRegion_Index,Is_GI_PrimaryWH,Warehouse_Index,Is_GI_PrimaryWHOnly,Is_GI_RemainingAge,RemainingAge_Value,RemainingAge_Unit,Is_GI_NotOlderThanLastIssue,LastIssue_Option,Is_GI_COA_Req,Is_DL_Mon,Is_DL_Tue,Is_DL_Wed,Is_DL_Thu,Is_DL_Fri,Is_DL_Sat,Is_DL_Sun,DL_Mon_Remark,DL_Tue_Remark,DL_Wed_Remark,DL_Thu_Remark,DL_Fri_Remark,DL_Sat_Remark,DL_Sun_Remark,MinDeliveryPerOrder,Begin_Flag) values(@Customer_Shipping_Location_Index,@Customer_Shipping_Location_Id,@Route_Index,@Customer_Shipping_Index,@Shipping_Location_Name,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@AmtWH,@Contact_Person1,@Contact_Person2,@Contact_Person3,@Remark,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@DayIndex,@add_by,getdate(),@add_branch,@isBangkok,@SubRoute_Index,@TransportRegion_Index,@Is_GI_PrimaryWH,@Warehouse_Index,@Is_GI_PrimaryWHOnly,@Is_GI_RemainingAge,@RemainingAge_Value,@RemainingAge_Unit,@Is_GI_NotOlderThanLastIssue,@LastIssue_Option,@Is_GI_COA_Req,@Is_DL_Mon,@Is_DL_Tue,@Is_DL_Wed,@Is_DL_Thu,@Is_DL_Fri,@Is_DL_Sat,@Is_DL_Sun,@DL_Mon_Remark,@DL_Tue_Remark,@DL_Wed_Remark,@DL_Thu_Remark,@DL_Fri_Remark,@DL_Sat_Remark,@DL_Sun_Remark,@MinDeliveryPerOrder,@Begin_Flag) "
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Customer_Shipping_Location_Index
                    .Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 15).Value = Me.getValue(dr, colList, "Customer_Shipping_Location_Id")
                    .Add("@Route_Index", SqlDbType.VarChar, 13).Value = dr("__Route_Index").ToString()
                    .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = dr("__Customer_Shipping_Index").ToString()
                    .Add("@Shipping_Location_Name", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Customer_Shipping_Location_Desc")
                    .Add("@Address", SqlDbType.NVarChar, 510).Value = Me.getValue(dr, colList, "Customer_Shipping_Location_Address")
                    .Add("@District_Index", SqlDbType.VarChar, 13).Value = dr("__District_Index").ToString()
                    .Add("@Province_Index", SqlDbType.VarChar, 13).Value = dr("__Province_Index").ToString()
                    .Add("@Postcode", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Postcode")
                    .Add("@Tel", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Tel")
                    .Add("@Fax", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Fax")
                    .Add("@Mobile", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Mobile")
                    .Add("@Email", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Email")
                    .Add("@AmtWH", SqlDbType.Int).Value = 0
                    .Add("@Contact_Person1", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person1")
                    .Add("@Contact_Person2", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person2")
                    .Add("@Contact_Person3", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person3")
                    .Add("@Remark", SqlDbType.NVarChar, 510).Value = Me.getValue(dr, colList, "Remark")
                    .Add("@Str1", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str2", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str3", SqlDbType.NVarChar, 200).Value = "1000000203"
                    .Add("@Str4", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str5", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str6", SqlDbType.NVarChar, 510).Value = DBNull.Value
                    .Add("@Str7", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str8", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "SubDistrict_Desc")
                    .Add("@Str9", SqlDbType.NVarChar, 4000).Value = Me.getValue(dr, colList, "District_Desc")
                    .Add("@Str10", SqlDbType.NVarChar, 4000).Value = Me.getValue(dr, colList, "Province_Desc")
                    .Add("@DayIndex", SqlDbType.NVarChar, 4000).Value = DBNull.Value
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = "Import_WMS"
                    .Add("@add_branch", SqlDbType.Int).Value = 1
                    .Add("@isBangkok", SqlDbType.Int).Value = 0
                    .Add("@SubRoute_Index", SqlDbType.VarChar, 50).Value = dr("__SubRoute_Index").ToString()
                    .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                    .Add("@Is_GI_PrimaryWH", SqlDbType.Bit).Value = DBNull.Value
                    .Add("@Warehouse_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                    .Add("@Is_GI_PrimaryWHOnly", SqlDbType.Bit).Value = False
                    .Add("@Is_GI_RemainingAge", SqlDbType.Bit).Value = False
                    .Add("@RemainingAge_Value", SqlDbType.Int).Value = DBNull.Value
                    .Add("@RemainingAge_Unit", SqlDbType.Int).Value = DBNull.Value
                    .Add("@Is_GI_NotOlderThanLastIssue", SqlDbType.Bit).Value = False
                    .Add("@LastIssue_Option", SqlDbType.Char).Value = DBNull.Value
                    .Add("@Is_GI_COA_Req", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Mon", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Tue", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Wed", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Thu", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Fri", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Sat", SqlDbType.Bit).Value = False
                    .Add("@Is_DL_Sun", SqlDbType.Bit).Value = False
                    .Add("@DL_Mon_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Tue_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Wed_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Thu_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Fri_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Sat_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@DL_Sun_Remark", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@MinDeliveryPerOrder", SqlDbType.Int).Value = DBNull.Value
                    .Add("@Begin_Flag", SqlDbType.NVarChar, 20).Value = DBNull.Value
                End With
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Next
            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Private Function getColArgs(ByVal colList As List(Of ColArgs), ByVal Column_Name As String) As ColArgs
        Try
            For Each colArgs As ColArgs In colList
                If (colArgs.Column_Name = Column_Name) Then
                    Return colArgs
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Private Function getValue(ByVal dr As DataRow, ByVal colList As List(Of ColArgs), ByVal Column_Name As String) As Object
        Try
            Dim colArgs As ColArgs = Me.getColArgs(colList, Column_Name)
            If (colArgs Is Nothing) Then Return DBNull.Value
            If (Not dr.Table.Columns.Contains(colArgs.Column_Name_Alias)) Then Return DBNull.Value
            Return dr(colArgs.Column_Name_Alias)
        Catch ex As Exception
            Throw ex
        End Try
        Return ""
    End Function

End Class
