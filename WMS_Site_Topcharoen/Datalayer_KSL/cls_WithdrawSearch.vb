Imports System.Text
Imports WMS_STD_Formula
Imports System.Data.SqlClient

Public Class cls_WithdrawSearch : Inherits DBType_SQLServer
    Public Function getWithdraw(ByVal Withdraw_No As String, ByVal Withdraw_Date_Start As String, ByVal Withdraw_Date_End As String, ByVal Customer_Index As String, ByVal Booking_No As String, ByVal Invoice_No As String, ByVal Tag_No As String) As DataTable
        Dim strSQL As New StringBuilder
        Try
            strSQL.Append(" SELECT Withdraw_Index,CONVERT(BIT,0) As chk,Withdraw_No,Withdraw_Date FROM tb_Withdraw")
            strSQL.Append(" WHERE Status = @Status")
            'strSQL.Append(" AND (SELECT COUNT(*) FROM tb_WithdrawItem WHERE Withdraw_Index = tb_Withdraw.Withdraw_Index AND Status = @Status) > 0")
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Status", SqlDbType.Int).Value = 2
                If Not String.IsNullOrEmpty(Withdraw_Date_Start) Then
                    strSQL.Append(" AND Withdraw_Date BETWEEN @Withdraw_Date_Start AND @Withdraw_Date_End")
                    .Add("@Withdraw_Date_Start", SqlDbType.VarChar).Value = Withdraw_Date_Start
                    .Add("@Withdraw_Date_End", SqlDbType.VarChar).Value = Withdraw_Date_End
                End If
                'If Not String.IsNullOrEmpty(Customer_Index) Then
                '    strSQL.Append(" AND Customer_Index = @Customer_Index")
                '    .Add("@Customer_Index", SqlDbType.VarChar).Value = Customer_Index
                'End If
                If Not String.IsNullOrEmpty(Withdraw_No) Then
                    strSQL.Append(" AND Withdraw_No = @Withdraw_No")
                    .Add("@Withdraw_No", SqlDbType.VarChar).Value = Withdraw_No
                End If
                If Not String.IsNullOrEmpty(Tag_No) Then
                    strSQL.Append(" AND Withdraw_Index IN (SELECT Withdraw_Index FROM tb_WithdrawItemLocation WHERE tb_WithdrawItemLocation.Tag_No = @Tag_No AND Status <> -1)")
                    .Add("@Tag_No", SqlDbType.VarChar).Value = Tag_No
                End If
            End With
            strSQL.Append(" ORDER BY Withdraw_Date DESC")
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function
    Public Function getWithdrawItem(ByVal Withdraw_No As String) As DataTable
        Dim strSQL As New StringBuilder
        Try
            If String.IsNullOrEmpty(Withdraw_No) Then Return Nothing
            strSQL.Append(" SELECT tb_Withdraw.Withdraw_Index,tb_WithdrawItem.WithdrawItem_Index,CONVERT(BIT,1) As chk,tb_Withdraw.Withdraw_No,ms_SKU.Sku_Id,ms_Package.Description As Package,tb_WithdrawItem.Qty")
            strSQL.Append(" ,tb_WithdrawItem.Sku_Index,tb_WithdrawItem.Package_Index,tb_WithdrawItem.Plot,tb_WithdrawItem.ItemStatus_Index,tb_WithdrawItem.Ratio,tb_WithdrawItem.Weight,tb_WithdrawItem.Volume,tb_WithdrawItem.Price,tb_WithdrawItemLocation.Tag_No")
            strSQL.Append(" ,IsMfg_Date,Mfg_Date,IsExp_Date,Exp_Date")
            strSQL.Append(" FROM tb_Withdraw")
            strSQL.Append(" INNER JOIN tb_WithdrawItem ON tb_Withdraw.Withdraw_Index = tb_WithdrawItem.Withdraw_Index")
            strSQL.Append(" INNER JOIN tb_WithdrawItemLocation ON tb_Withdraw.Withdraw_Index = tb_WithdrawItemLocation.Withdraw_Index")
            strSQL.Append(" INNER JOIN tb_LocationBalance ON tb_WithdrawItemLocation.LocationBalance_Index = tb_LocationBalance.LocationBalance_Index")
            strSQL.Append(" AND tb_WithdrawItem.WithdrawItem_Index = tb_WithdrawItemLocation.WithdrawItem_Index")
            strSQL.Append(" INNER JOIN ms_SKU ON tb_WithdrawItem.Sku_Index = ms_SKU.Sku_Index AND ms_SKU.status_id <> -1")
            strSQL.Append(" INNER JOIN ms_Package ON tb_WithdrawItem.Package_Index = ms_Package.Package_Index")
            strSQL.Append(" WHERE tb_Withdraw.Status = 2 /*AND tb_WithdrawItem.Status = 2*/ AND tb_Withdraw.Withdraw_No = @Withdraw_No")
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_No", SqlDbType.VarChar).Value = Withdraw_No
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function
    Public Function getSUMWithdrawItemLocation(ByVal Withdraw_Index As String) As DataTable
        Dim strSQL As New StringBuilder
        Try
            With strSQL
                .Append(" SELECT Plot,Tag_No,Serial_No,comment,Reference,Mfg_Date,Exp_Date,Sku_Id,Sku_Description")
                .Append(" ,Description,Sku_PackageDescription,ItemStatus_Description,Location_Alias,Mobile_Pick")
                .Append(" ,Order_Date,ERP_Location")
                .Append(" ,ROW_NUMBER() OVER(ORDER BY (SELECT 1)) Seq")
                .Append(" ,SUM(Qty) As Qty,SUM(WeightOut) As WeightOut,SUM(VolumeOut) As VolumeOut")
                .Append(" FROM VIEW_WithDrawAssetItemLocation")
                .Append(" WHERE Withdraw_Index = @Withdraw_Index")
                .Append(" GROUP BY Plot,Tag_No,Serial_No,comment,Reference,Mfg_Date,Exp_Date,Sku_Id,Sku_Description")
                .Append(" ,Description,Sku_PackageDescription,ItemStatus_Description,Location_Alias,Mobile_Pick")
                .Append(" ,Order_Date,ERP_Location")
                '.Append(" SELECT Lot_No,Plot,Tag_No,Serial_No,Invoice_No,Pallet_No,Invoice_Out,Declaration_No_Out")
                '.Append(" ,SO_No,PACKING_No,comment,Reference,Reference2,DocumentPlan_No,Mfg_Date,Exp_Date")
                '.Append(" ,Sku_Id,Sku_Description,Product_Name_th,Product_Name_eng,Item_Package,Description")
                '.Append(" ,Sku_PackageDescription,Pallet_Name,ItemDefinition_Name,ItemDefinition,ProductType")
                '.Append(" ,ItemStatus_Description,Location_Alias,Model,Brand,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10")
                '.Append(" ,Expr1,Expr2,Expr3,Expr4,Expr5,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5")
                '.Append(" ,Order_No,Order_Date,Withdraw_No,Withdraw_Date,HS_Code,ItemDescription,ROW_NUMBER() OVER(ORDER BY (SELECT 1)) Seq")
                '.Append(" ,Company_Name,ERP_Location,Mobile_Pick")
                '.Append(" ,SUM(Qty) As Qty,SUM(WeightOut) As WeightOut,SUM(VolumeOut) As VolumeOut,SUM(Pallet_Qty) As Pallet_Qty,SUM(QtyItemOut) As QtyItemOut,SUM(intQty) As intQty")
                '.Append(" ,SUM(Price_Out) As Price_Out,SUM(OrderItem_Price) As OrderItem_Price")
                '.Append(" ,SUM(Flo1) As Flo1,SUM(Flo2) As Flo2,SUM(Flo3) As Flo3,SUM(Flo4) As Flo4,SUM(Flo5) As Flo5,SUM(Tax1) As Tax1,SUM(Tax2) As Tax2,SUM(Tax3) As Tax3,SUM(Tax4) As Tax4,SUM(Tax5) As Tax5")
                '.Append(" FROM VIEW_WithDrawAssetItemLocation")
                '.Append(" WHERE Withdraw_Index = @Withdraw_Index")
                '.Append(" GROUP BY Lot_No,Plot,Tag_No,Serial_No,Invoice_No,Pallet_No,Invoice_Out,Declaration_No_Out")
                '.Append(" ,SO_No,PACKING_No,comment,Reference,Reference2,DocumentPlan_No,Mfg_Date,Exp_Date")
                '.Append(" ,Sku_Id,Sku_Description,Product_Name_th,Product_Name_eng,Item_Package,Description")
                '.Append(" ,Sku_PackageDescription,Pallet_Name,ItemDefinition_Name,ItemDefinition,ProductType")
                '.Append(" ,ItemStatus_Description,Location_Alias,Model,Brand,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10")
                '.Append(" ,Expr1,Expr2,Expr3,Expr4,Expr5,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5")
                '.Append(" ,Order_No,Order_Date,Withdraw_No,Withdraw_Date,HS_Code,ItemDescription")
                '.Append(" ,Company_Name,ERP_Location,Mobile_Pick")
            End With
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = Withdraw_Index
            End With
            Return DBExeQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function
End Class
