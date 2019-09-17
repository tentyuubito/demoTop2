Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class clsCustomer_Shipping : Inherits DBType_SQLServer

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

                ' BGN Customer_Shipping_Index
                Dim objSy As New Sy_AutoNumber
                Dim Customer_Shipping_Index = objSy.getSys_Value("Customer_Shipping_Index")
                objSy = Nothing
                ' END Customer_Shipping_Index

                strSQL = " select top 1 Customer_Shipping_Index from ms_Customer_Shipping where status_id<>-1 and Customer_Index=@Customer_Index and Str1=@Customer_Shipping_Id "
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Customer_Shipping_Id", SqlDbType.VarChar, 50).Value = Me.getValue(dr, colList, "Customer_Shipping_Id")
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = dr("__Customer_Index").ToString()
                End With
                If Not (DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text) = Nothing) Then
                    Throw New Exception(String.Format("√À— ºŸÈ√—∫ {0} ´È”", Me.getValue(dr, colList, "Customer_Shipping_Id")))
                End If

                strSQL = " insert into ms_Customer_Shipping(Customer_Shipping_Index,Customer_Index,Title,Company_Name,Address,District_Index,Province_Index,Postcode,Tel,Fax,Mobile,Email,Contact_Person,Contact_Person2,Contact_Person3,Barcode,Remark,Status,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_date,add_branch,Customer_Type_Index,Discount_Value,Route_Index,SubRoute_Index,TransportRegion_Index) values(@Customer_Shipping_Index,@Customer_Index,@Title,@Company_Name,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Contact_Person,@Contact_Person2,@Contact_Person3,@Barcode,@Remark,@Status,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,getdate(),@add_branch,@Customer_Type_Index,@Discount_Value,@Route_Index,@SubRoute_Index,@TransportRegion_Index) "
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = Customer_Shipping_Index
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = dr("__Customer_Index").ToString()
                    .Add("@Title", SqlDbType.NVarChar, 100).Value = "∫√‘…—∑"
                    .Add("@Company_Name", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Customer_Shipping_Desc")
                    .Add("@Address", SqlDbType.NVarChar, 510).Value = Me.getValue(dr, colList, "Customer_Shipping_Address")
                    .Add("@District_Index", SqlDbType.VarChar, 13).Value = dr("__District_Index").ToString()
                    .Add("@Province_Index", SqlDbType.VarChar, 13).Value = dr("__Province_Index").ToString()
                    .Add("@Postcode", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Postcode")
                    .Add("@Tel", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Tel")
                    .Add("@Fax", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Fax")
                    .Add("@Mobile", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Mobile")
                    .Add("@Email", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Email")
                    .Add("@Contact_Person", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person1")
                    .Add("@Contact_Person2", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person2")
                    .Add("@Contact_Person3", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Contact_Person3")
                    .Add("@Barcode", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Remark", SqlDbType.NVarChar, 510).Value = Me.getValue(dr, colList, "Remark")
                    .Add("@Status", SqlDbType.Int).Value = DBNull.Value
                    .Add("@Str1", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Customer_Shipping_Id")
                    .Add("@Str2", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str3", SqlDbType.NVarChar, 200).Value = "1000000203"
                    .Add("@Str4", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "Province_Desc")
                    .Add("@Str5", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "District_Desc")
                    .Add("@Str6", SqlDbType.NVarChar, 510).Value = Me.getValue(dr, colList, "Customer_Shipping_Desc")
                    .Add("@Str7", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str8", SqlDbType.NVarChar, 200).Value = Me.getValue(dr, colList, "SubDistrict_Desc")
                    .Add("@Str9", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@Str10", SqlDbType.NVarChar, 200).Value = DBNull.Value
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = "Import_WMS"
                    .Add("@add_branch", SqlDbType.Int).Value = 1
                    .Add("@Customer_Type_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                    .Add("@Discount_Value", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Route_Index", SqlDbType.VarChar, 50).Value = DBNull.Value
                    .Add("@SubRoute_Index", SqlDbType.VarChar, 50).Value = DBNull.Value
                    .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
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
