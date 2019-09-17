Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports System.Data
Imports System.Data.SqlClient

Public Class ml_MappingBarcode : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Sku_Index As String
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property

    Public ReadOnly Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
    End Property

#Region "  Sub Cal M3 "

    Private _DimenstionRatio As Double = 1
    Private _DimenstionRatioPakcage As Double = 1
    Private _Digit As Integer = 4
    Private _DigitPackage As Integer = 4

    Function getDigitM3(ByVal pstrDimensionType_Index As String) As Integer
        Dim objDB As New ms_DimensionType
        Try
            Return objDB.GetDigitDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function getRatioM3(ByVal pstrDimensionType_Index As String) As Double
        Dim objDB As New ms_DimensionType
        Dim _Ratio As Double = 1
        Try
            Return objDB.GetRatioDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function CalculateToM3(ByVal Ratio As Double, ByVal Width As Double, ByVal Length As Double, ByVal Height As Double) As Double

        Dim Volume As Double = 0
        Volume = (CDbl(Width) * CDbl(Length) * CDbl(Height)) / Ratio
        If Volume <> 0 Then
            Volume = Math.Round(Volume, _Digit).ToString
        End If

        Return Volume
    End Function
#End Region

    Public Function AutoInsertSKU(ByVal Sku_Id As String, ByVal Sku_Name_Thai As String, ByVal Package_Name As String, Optional ByVal Ratio As Integer = 1, Optional ByVal ProductType_ID As String = "", Optional ByVal ProductType_Name As String = "", Optional ByVal Sku_Name_Eng As String = "", Optional ByVal Unit_Weight As Double = 0, Optional ByVal DimensionType_Id As String = "", Optional ByVal Width As Double = 0, Optional ByVal Length As Double = 0, Optional ByVal Height As Double = 0, Optional ByVal Barcode1 As String = "", Optional ByVal Barcode2 As String = "", Optional ByVal Flo1 As Double = 0, Optional ByVal ItemLife_m As Double = 0) As Boolean
        Dim strSQL As String = ""
        Dim strSku_Index As String = ""
        Dim strPackage_Index As String = ""
        Dim strSkuRatio_Index As String = ""
        Dim strProduct_Index As String = ""
        Dim objCustomer_Setting As New config_CustomSetting
        Dim strProductType_Index As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim objSy_AutoNumber As New Sy_AutoNumber
        Try
            If ProductType_ID <> "" Then
                strSQL = "  SELECT  * "
                strSQL &= "  FROM   ms_ProductType  "
                strSQL &= "   WHERE  ProductType_Id = '" & ProductType_ID & "'  AND Status_Id <> -1 "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "PD")

                If DS.Tables("PD").Rows.Count <= 0 Then 'AutoInsertProductType
                    strSQL = "  INSERT INTO ms_ProductType(                                                             "
                    strSQL &= "                              ProductType_Index, ProductType_ID, Description, add_by     "
                    strSQL &= "	                            ,add_date,add_branch,status_id,ItemLife_y,ItemLife_m        "
                    strSQL &= "		                        ,ItemLife_d                                                 "
                    strSQL &= "	                           )   VALUES                                                   "
                    strSQL &= "                         (   @ProductType_Index,@ProductType_Id,@Description             "
                    strSQL &= "                             ,@add_by,getdate(),@add_branch,@status_id                   "
                    strSQL &= "                             ,@ItemLife_y,@ItemLife_m,@ItemLife_d                        "
                    strSQL &= "                         )                                                               "

                    strProductType_Index = objSy_AutoNumber.getSys_Value("ProductType_Index")

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = strProductType_Index
                        .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = ProductType_ID
                        If ProductType_Name = "" Then
                            .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ProductType_ID
                        Else
                            .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ProductType_Name
                        End If
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                        .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                Else
                    strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
                End If
            Else
                strSQL = "  SELECT  top 1  * "
                strSQL &= "  FROM   ms_ProductType Where Status_Id <> -1 "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "PD")
                strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
            End If




            Dim objDiman As New ms_DimensionType
            Dim dtDimensionTypeData As New DataTable
            Dim DimensionType_Index As String = ""
            Dim Volume As Double = 0
            If DimensionType_Id <> "" Then
                dtDimensionTypeData = objDiman.GetDimensionTypeData(" AND DimensionType_Id = '" & DimensionType_Id & "'")
                DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
            Else
                dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
            End If

            _DimenstionRatio = getRatioM3(DimensionType_Index)
            _Digit = getDigitM3(DimensionType_Index)
            Volume = Me.CalculateToM3(_DimenstionRatio, Width, Length, Height)

            '----------------------------------------------------------------------------------


            strPackage_Index = objSy_AutoNumber.getSys_Value("Package_Index")
            strSQL = "   INSERT INTO ms_Package (                                                           "
            strSQL &= "                         Package_Index,Package_Id,Description,                       "
            strSQL &= "                         Dimension_Hi,Dimension_Wd,Dimension_Len,Dimension_Unit_Id , "
            strSQL &= "                         add_by,add_date,add_branch,status_id ,                      "
            strSQL &= "                         Pkg_Description,isItem_Package,Weight,DimensionType_Index   "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @Package_Index, @Package_Id,@Description ,                              "
            strSQL &= "             @Dimension_Hi,@Dimension_Wd,@Dimension_Len,@Dimension_Unit_Id ,         "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id ,                              "
            strSQL &= "             @Pkg_Description,@isItem_Package,@Weight,@DimensionType_Index           "
            strSQL &= "          )                                                                          "


            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Package_Name
                .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Package_Name

                .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = Height
                .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = Width
                .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = Length
                .Parameters.Add("@Dimension_Unit_Id", SqlDbType.VarChar).Value = "0"

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

                .Parameters.Add("@Pkg_Description", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@isItem_Package", SqlDbType.Int).Value = 0
                .Parameters.Add("@Weight", SqlDbType.Float).Value = Unit_Weight
                .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            'Insert ms_Product
            strProduct_Index = objSy_AutoNumber.getSys_Value("Product_Index")

            strSQL = "   INSERT INTO ms_Product (                                                           "

            strSQL &= "                         Product_Index,Product_Id,Product_Name_th,Product_Name_en ,  "
            strSQL &= "                         ProductType_Index,Std_Unit_Cost,Default_Exp_Day ,                             "
            strSQL &= "                         add_by,add_date,add_branch,status_id    "

            strSQL &= "                         )                                                           "


            strSQL &= "  VALUES (                                                                           "

            strSQL &= "             @Product_Index, @Product_Id,@Product_Name_th,@Product_Name_en ,         "
            strSQL &= "             @ProductType_Index,@Std_Unit_Cost,@Default_Exp_Day ,                                       "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id            "

            strSQL &= "          )                                                                          "



            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Product_Index", SqlDbType.VarChar).Value = strProduct_Index
                .Parameters.Add("@Product_Id", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@Product_Name_th", SqlDbType.NVarChar).Value = Sku_Name_Thai
                '.Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Sku_Name_Eng
                If Sku_Name_Eng = "" Then
                    .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Sku_Name_Thai
                Else
                    .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Sku_Name_Eng
                End If
                .Parameters.Add("@ProductType_Index", SqlDbType.VarChar).Value = strProductType_Index
                .Parameters.Add("@Std_Unit_Cost", SqlDbType.Float).Value = 0
                .Parameters.Add("@Default_Exp_Day", SqlDbType.Float).Value = 0

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            '----------------------------------------------------------------------------------------------------------------------------------
            'Insert ms_SKU
            strSku_Index = objSy_AutoNumber.getSys_Value("Sku_Index")
            _Sku_Index = strSku_Index
            ' strReturn = strSku_Index

            strSQL = "  INSERT INTO ms_SKU (Sku_Index,Sku_Id,Product_Index,Size_Index,Package_Index,UnitWeight_Index,Unit_Volume, "
            strSQL &= " Color_Index,Model_Index,Brand_Index,Barcode1,Barcode2,Price1,Price2,Price3,ItemLife_y,ItemLife_m,ItemLife_d, "
            strSQL &= " Str1,Str2,Str10,Min_Qty,Min_Weight,Min_Volume,Max_Qty,Max_Weight,Max_Volume,Qty_Per_Pallet, "
            strSQL &= " add_by,add_date,add_branch,isPLot,status_id,PalletType_Index,Pick_Method,Picking_Type,Unit_Width,Unit_Length,Unit_Height,Flo1   "
            strSQL &= " )    "
            strSQL &= " VALUES (                                                                            "
            strSQL &= "         @Sku_Index, @Sku_Id  ,@Product_Index                                                      "
            strSQL &= "         ,@Size_Index,@Package_Index,@UnitWeight_Index, @Unit_Volume,@Color_Index,@Model_Index ,@Brand_Index ,@Barcode1,@Barcode2 "
            strSQL &= "         ,@Price1,@Price2,@Price3                                                  "
            strSQL &= "         ,@ItemLife_y ,@ItemLife_m ,@ItemLife_d                                      "
            strSQL &= "         ,@Str1  ,@Str2  ,@Str10                                                     "
            strSQL &= "         ,@Min_Qty ,@Min_Weight ,@Min_Volume                                         "
            strSQL &= "         ,@Max_Qty ,@Max_Weight ,@Max_Volume                                         "
            strSQL &= "         ,@Qty_Per_Pallet                                                            "
            strSQL &= "         ,@add_by,getdate(),@add_branch                                              "
            strSQL &= "         ,@isPLot                                                                    "
            strSQL &= "         ,@status_id                                                                 "
            strSQL &= "         ,@PalletType_Index                                                          "
            strSQL &= "         ,@Pick_Method                                                               "
            strSQL &= "         ,@Picking_Type                                                            "
            strSQL &= "         ,@Unit_Width,@Unit_Length,@Unit_Height ,@Flo1 ) "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Sku_Id", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@Product_Index", SqlDbType.NVarChar).Value = strProduct_Index
                .Parameters.Add("@Size_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = Unit_Weight
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = Volume '(Width * Length * Height)
                .Parameters.Add("@Color_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Model_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@Brand_Index", SqlDbType.VarChar).Value = ""

                If Barcode1 = "" Then
                    .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Sku_Id
                Else
                    .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Barcode1
                End If
                .Parameters.Add("@Barcode2", SqlDbType.VarChar).Value = Barcode2

                .Parameters.Add("@Price1", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price2", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price3", SqlDbType.Float).Value = 0
                .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = ItemLife_m
                .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = Sku_Name_Thai

                If Sku_Name_Eng = "" Then
                    .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Sku_Name_Thai
                Else
                    .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Sku_Name_Eng
                End If
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = "1"
                .Parameters.Add("@Min_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = 1
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@isPLot", SqlDbType.Bit).Value = 0
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar).Value = "1"
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = 1
                .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = 0


                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Height

                .Parameters.Add("@Flo1", SqlDbType.Float).Value = Flo1

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '----------------------------------------------------------------------------------------------------------------------------------
            strSkuRatio_Index = objSy_AutoNumber.getSys_Value("SkuRatio_Index")
            objSy_AutoNumber = Nothing

            strSQL = "   INSERT INTO ms_SkuRatio (                                                          "
            strSQL &= "                         SkuRatio_Index,Sku_Index,Package_Index,Ratio,               "
            strSQL &= "                         add_by,add_date,add_branch,status_id                        "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @SkuRatio_Index, @Sku_Index,@Package_Index,@Ratio,                      "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id                                "
            strSQL &= "          )                                                                          "
            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@SkuRatio_Index", SqlDbType.VarChar).Value = strSkuRatio_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Ratio", SqlDbType.Float).Value = Ratio
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            myTrans.Commit()


            Return True
        Catch ex As Exception
            myTrans.Rollback()
            '_MessageError = ex.ToString
            Return False
        Finally

            disconnectDB()
        End Try
    End Function

    Public Function MappingBarcode_TSS(ByVal strSku_Index As String, ByVal Barcode1 As String, ByVal Barcode As String, ByVal Qty_Barcode As String, ByVal Width As Double, ByVal Length As Double, ByVal Height As Double, Optional ByVal Weight As Double = 0) As String
        Try
            Dim strSQL As String = ""

            SetSQLString = "SELECT * FROM VIEW_Barcode_Mapping WHERE Barcode = '" & Barcode & "' AND Sku_Index = '" & strSku_Index & "'"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Dim Ratio As Double = 0
            If _dataTable.Rows.Count = 0 Then
                strSQL = "INSERT tb_Barcode_Mapping(Sku_Index,Barcode_SKU,Barcode,Qty_Barcode,DimensionType_Index,Dimension_Wd,Dimension_Len,Dimension_Hi,Weight,Volume)"
                strSQL &= " Values(@Sku_Index,@Barcode_SKU,@Barcode,@Qty_Barcode,'0010000000001',@Unit_Width,@Unit_Length,@Unit_Height,@Weight,ROUND(((convert(float,@Unit_Width) * convert(float,@Unit_Length) * convert(float,@Unit_Height))/1000000),4))"
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Sku_Index", SqlDbType.NVarChar).Value = strSku_Index
                    .Parameters.Add("@Barcode_SKU", SqlDbType.NVarChar).Value = Barcode1
                    .Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Barcode
                    .Parameters.Add("@Qty_Barcode", SqlDbType.Float).Value = Qty_Barcode
                    .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Width
                    .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Length
                    .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Height
                    .Parameters.Add("@Weight", SqlDbType.Float).Value = Weight
                    '.Parameters.Add("@Volume", SqlDbType.Float).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.NVarChar).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Else

                Ratio = IIf(_dataTable.Rows(0).Item("Barcode2"), _dataTable.Rows(0).Item("Barcode2"), 0)

                strSQL = "  UPDATE tb_Barcode_Mapping"
                strSQL &= " SET     Qty_Barcode=@Qty_Barcode,Dimension_Wd=@Unit_Width,Dimension_Len=@Unit_Length,Dimension_Hi=@Unit_Height,update_date=getdate()"
                strSQL &= "         ,Volume = ROUND(((convert(float,@Unit_Width) * convert(float,@Unit_Length) * convert(float,@Unit_Height))/1000000),4)"
                strSQL &= "         ,Weight=@Weight"
                strSQL &= " WHERE   barcode=@barcode and sku_index=@sku_index"
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@sku_index", SqlDbType.NVarChar).Value = strSku_Index
                    .Parameters.Add("@barcode", SqlDbType.NVarChar).Value = Barcode
                    .Parameters.Add("@Qty_Barcode", SqlDbType.Float).Value = Qty_Barcode
                    .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Width
                    .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Length
                    .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Height
                    .Parameters.Add("@Weight", SqlDbType.Float).Value = Weight
                    '.Parameters.Add("@Volume", SqlDbType.Float).Value = 0
                    .Parameters.Add("@update_by", SqlDbType.NVarChar).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If



            If Qty_Barcode = Ratio Then
                strSQL = "  UPDATE  ms_Sku"
                strSQL &= " SET     Unit_Width=@Unit_Width,Unit_Length=@Unit_Length,Unit_Height=@Unit_Height,update_date=getdate()"
                strSQL &= "         ,Unit_Volume = ROUND(((convert(float,@Unit_Width) * convert(float,@Unit_Length) * convert(float,@Unit_Height))/1000000),4)"
                'strSQL &= "         ,UnitWeight_Index = @Weight"
                strSQL &= " WHERE   sku_index=@sku_index"
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@sku_index", SqlDbType.NVarChar).Value = strSku_Index
                    .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Width
                    .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Length
                    .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Height
                    .Parameters.Add("@Barcode2", SqlDbType.Float).Value = Qty_Barcode
                    '.Parameters.Add("@Weight", SqlDbType.Float).Value = Weight
                    .Parameters.Add("@update_by", SqlDbType.NVarChar).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If

            Dim SQLO = " UPDATE DEV.SKB_NEW SET CHANGED_FLAG = 'N' "
            SQLO &= " WHERE BARCODE = '" & Barcode & "'"
            ' OracleModule.ExecuteNonQuery(SQLO)


            Return "PASS"
        Catch ex As Exception
            Return ex.Message.ToString
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetCheckingBarcodeDupp(ByVal pSku_Barcode As String) As DataTable

        Try
            Dim strSQL As String = ""
            Dim oDT As New DataTable
            'strSQL = "  SELECT Sku_Index FROM tb_Barcode_Mapping WHERE Barcode =  '" & pSku_Barcode & "'"
            'strSQL = "  SELECT BM.Sku_Index,SKU.Sku_Id,SKU.str1,BM.Qty_Barcode FROM tb_Barcode_Mapping BM"
            'strSQL &= " inner join ms_SKU SKU On BM.Sku_Index = SKU.Sku_Index  where BM.Barcode = '" & pSku_Barcode & "' AND SKU.Status_id <> -1"
            'Config By Jeng 18/06/2014
            strSQL = "select Sku_Index ,1 as Qty_Barcode from ms_sku Where Barcode1 = '" & pSku_Barcode & "' AND Status_id <> -1"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            oDT = GetDataTable
            Return oDT
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetCheckingBarcodePackageDupp(ByVal pSku_Barcode As String, Optional ByVal SalesOrder_Index As String = "") As DataTable

        Try
            Dim strSQL As String = ""
            Dim oDT As New DataTable

            If String.IsNullOrEmpty(SalesOrder_Index) = True Then

                strSQL = " Select sk.Sku_Index,sk.Package_Index as Std_Package_Index, sk.Sku_Id, sk.Str1 AS Sku_Name,pk.Barcode,pk.Package_Index,pk.Description as Package_Des,1 as Qty_Barcode, sr.Ratio,psr.Description as Package,isnull(sk.IsScanItemAll,0) as IsScanItemAll ,isnull(sk.IsNoPack,0) as IsNoPack " & _
                         " From ms_sku sk " & _
                         " INNER JOIN ms_SKURatio sr ON sr.Sku_Index = sk.Sku_Index  " & _
                         " INNER JOIN ms_Package psr ON psr.Package_Index = sk.Package_Index  " & _
                         " INNER JOIN ms_Package pk ON sr.Package_Index = pk.Package_Index   " & _
                         " Where pk.Barcode = '" & pSku_Barcode & "' AND sk.Status_id <> -1 AND ISNULL(pk.Barcode,'') <> '' "

            Else

                strSQL = " Select sk.Sku_Index,sk.Package_Index as Std_Package_Index, sk.Sku_Id, sk.Str1 AS Sku_Name,pk.Barcode,pk.Package_Index,pk.Description as Package_Des,1 as Qty_Barcode"
                strSQL &= "  		, sr.Ratio,psr.Description as Package ,isnull(sk.IsScanItemAll,0) as IsScanItemAll  ,isnull(sk.IsNoPack,0) as IsNoPack  "
                strSQL &= "  From ms_sku sk  "
                strSQL &= "  	 INNER JOIN ms_SKURatio sr ON sr.Sku_Index = sk.Sku_Index   "
                strSQL &= "  	 INNER JOIN ms_Package psr ON psr.Package_Index = sk.Package_Index   "
                strSQL &= "  	 INNER JOIN ms_Package pk ON sr.Package_Index = pk.Package_Index    "
                strSQL &= " Where   ISNULL(pk.Barcode,'') = '" & pSku_Barcode & "'  "
                'strSQL &= "         AND sk.Sku_Index in (select R.Sku_Index "
                'strSQL &= "  						from ms_SKURatio R "
                'strSQL &= "  							inner join ms_Package P on P.Package_Index = R.Package_Index"
                'strSQL &= "  						 AND	P.Barcode = '" & pSku_Barcode & "' )"
                strSQL &= "         AND sk.Sku_Index in ( Select Sku_Index from tb_SalesOrderItem where SalesOrder_Index = '" & SalesOrder_Index & "' )"
                strSQL &= "  Order by sk.Sku_Id,psr.Description ,sr.Ratio	 "


                'strSQL = " Select sk.Sku_Index, sk.Sku_Id, sk.Str1 AS Sku_Name,pk.Package_Index,pk.Description as Package_Des,1 as Qty_Barcode, sr.Ratio,psr.Description as Package,isnull(sk.IsScanItemAll,0) as IsScanItemAll ,isnull(sk.IsNoPack,0) as IsNoPack " & _
                '                      " From ms_sku sk " & _
                '                      " INNER JOIN ms_SKURatio sr ON sr.Sku_Index = sk.Sku_Index  " & _
                '                      " INNER JOIN ms_Package psr ON psr.Package_Index = sk.Package_Index  " & _
                '                      " INNER JOIN ms_Package pk ON sr.Package_Index = pk.Package_Index   " & _
                '                      " Where pk.Barcode = '" & pSku_Barcode & "' AND sk.Status_id <> -1 " & _
                '                      " AND sk.Sku_Index in ( Select Sku_Index from tb_SalesOrderItem " & _
                '                      " where SalesOrder_Index = '" & SalesOrder_Index & "' )"

            End If
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                oDT = GetDataTable
            Return oDT

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetSKU_By_SKU_ID_Like_Search(ByVal pstrSKU_ID As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT TOP 100 SKU.*,ISNULL(SKU.UnitWeight_Index,0) As Weight, ISNULL(SKU.Unit_Volume ,0) As Volume, ISNULL(SKU.Qty_Per_Pallet,0) As Qty_Per_Pallet,Ratio.Package_Index Ratio_Package_Index, Package.Package_ID ,Package.Description Package_Des,Ratio.Ratio, ms_Location.Location_Alias "
            'krit update 04/11/2011 for LP
            'strSQL &= " , CASE WHEN SKU.Barcode1 <> '' THEN 'OK' ELSE '' END As IsBarcode"

            'best update 28/09/2013 for Tss
            strSQL &= " , CASE WHEN isnull((select top 1 Sku_Index from tb_Barcode_Mapping where Sku_Index = SKU.Sku_Index),'') "
            strSQL &= " <> '' THEN 'OK' ELSE '' END As IsBarcode "


            strSQL &= " FROM ms_SKU SKU INNER JOIN ms_Package Package ON SKU.Package_Index = Package.Package_Index"
            strSQL &= " INNER JOIN ms_SKURatio Ratio ON SKU.SKU_Index = Ratio.SKU_Index AND  SKU.Package_Index = Ratio.Package_Index "
            strSQL &= " LEFT JOIN ms_Location ON ms_Location.Location_Index = SKU.Location_Index "
            strSQL &= " WHERE SKU.SKU_ID like '%" & pstrSKU_ID.Replace("'", "''") & "%' And SKU.Status_ID <> -1 "
            strSQL &= " ORDER BY SKU.SKU_ID"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function UpdateSKU(ByVal User_name As String, ByVal pstrSKU_Index As String, ByVal pstrBarcode1 As String, ByVal pintDigit As Integer, ByVal pdblQty_Per_Pallet As Double, ByVal pdblUnit_Width As Double, ByVal pdblUnit_Length As Double, ByVal pdblUnit_Height As Double, ByVal pdblUnit_Volume As Double, ByVal pdblUnitWeight_Index As Double, ByVal pboolIsControlsAsset As Boolean, ByVal pboolIsControlPack As Boolean) As Boolean
        Dim strSQL As String
        Try
            strSQL = "UPDATE ms_SKU SET "
            strSQL &= "  Qty_Per_Pallet = @Qty_Per_Pallet"
            strSQL &= " , Unit_Width = @Unit_Width"
            strSQL &= " , Unit_Length = @Unit_Length"
            strSQL &= " , Unit_Height = @Unit_Height"
            strSQL &= " , Unit_Volume = @Unit_Volume"
            strSQL &= " , UnitWeight_Index = @UnitWeight_Index"
            strSQL &= " , update_by = @User_Update "
            strSQL &= " WHERE SKU_Index = @SKU_Index"
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SKU_Index", SqlDbType.VarChar).Value = pstrSKU_Index.Trim
                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = pdblQty_Per_Pallet
                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = pdblUnit_Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = pdblUnit_Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = pdblUnit_Height
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = pdblUnit_Volume
                .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = pdblUnitWeight_Index
                .Parameters.Add("@User_Update", SqlDbType.VarChar, 200).Value = User_name
            End With
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetSkuDetail(ByVal pstrSku_Index As String) As DataTable
        Try
            Dim strSQL As String = ""
            strSQL = "   select *  "
            strSQL &= "  from ms_Sku"
            strSQL &= "  where Sku_Index = '" & pstrSku_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function



    Public Function GetDataBarcode_Mapping(ByVal str_barcode As String, ByVal str_sku_index As String, Optional ByVal Map_ID As Integer = -1) As DataTable

        Dim strSQL As String = ""
        Try

            strSQL = " Select  tb_Barcode_Mapping.*,ms_DimensionType.Description as DimensionType "
            strSQL &= " FROM tb_Barcode_Mapping left Outer join"
            strSQL &= "     ms_DimensionType ON ms_DimensionType.DimensionType_Index = tb_Barcode_Mapping.DimensionType_Index"
            strSQL &= " WHERE 1=1"
            If str_barcode <> "" Then
                strSQL &= " and tb_Barcode_Mapping.Barcode = '" & str_barcode & "' "
            End If
            If str_sku_index <> "" Then
                strSQL &= " and tb_Barcode_Mapping.sku_index ='" & str_sku_index & "' "
            End If
            If Map_ID <> -1 Then
                strSQL &= " and tb_Barcode_Mapping.Map_ID ='" & Map_ID & "' "
            End If
            strSQL &= " ORDER BY tb_Barcode_Mapping.Map_ID DESC"

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function DeleteBarcodeMapping(ByVal pint_Map_ID As Integer) As Boolean
        Try
            Dim strSQL As String = ""

            Dim dt As New DataTable

            strSQL = " SELECT SKU_Index from tb_Barcode_Mapping "
            strSQL &= " WHERE SKU_Index IN (SELECT SKU_Index From tb_Barcode_Mapping Where Map_ID = " & pint_Map_ID & ")"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


            If _dataTable.Rows.Count = 1 Then
                strSQL = " UPDATE ms_SKU SET "
                strSQL &= " Barcode1 = ''"
                strSQL &= " WHERE SKU_Index = '" & _dataTable.Rows(0)("SKU_Index").ToString & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                connectDB()
                EXEC_Command()

            End If


            strSQL = "Delete from tb_Barcode_Mapping"
            strSQL &= " WHERE Map_ID = @Map_ID"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Map_ID", SqlDbType.Int).Value = pint_Map_ID
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()


            Return True

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetSKU_By_Barcode(ByVal pstrBarcode As String, ByVal sku_id As String) As DataTable
        Dim strSQL As String = ""
        Try

            'krit add new for LP 23/09/2011

            strSQL = "  SELECT  SKU.SKU_Index,SKU.SKU_ID,SKU.Str1,SKU.Barcode1,ISNULL(SKU.UnitWeight_Index,0) As Weight, ISNULL(SKU.Unit_Volume ,0) As Volume, ISNULL(SKU.Qty_Per_Pallet,0) As Qty_Per_Pallet"
            strSQL &= " ,SKU.Unit_Width,SKU.Unit_Length,SKU.Unit_Height "
            strSQL &= " , CASE WHEN COUNT(tb_Barcode_Mapping.Barcode) > 0 THEN 'OK' ELSE '' END As IsBarcode"
            strSQL &= " FROM ms_SKU SKU "
            strSQL &= " LEFT JOIN tb_Barcode_Mapping ON SKU.SKU_index = tb_Barcode_Mapping.SKU_Index "
            strSQL &= " WHERE  SKU.Status_id <> -1"

            If pstrBarcode.ToString.Trim <> "" Then
                strSQL &= " AND tb_Barcode_Mapping.Barcode = '" & pstrBarcode.Replace("'", "''") & "'"
            End If
            If sku_id.ToString.Trim <> "" Then
                strSQL &= " AND SKU.Sku_Id = '" & sku_id & "'"
            End If
            strSQL &= " GROUP BY SKU.SKU_Index,SKU.SKU_ID,SKU.Str1,SKU.Barcode1,SKU.UnitWeight_Index, SKU.Unit_Volume , SKU.Qty_Per_Pallet"
            strSQL &= " ,SKU.Unit_Width,SKU.Unit_Length,SKU.Unit_Height "
            strSQL &= " ORDER BY SKU.SKU_ID"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function CheckMultiBarcode_Promotion(ByVal pstrSku_Index As String, ByVal pstrBarcode As String) As Boolean
        Try
            Dim strSQL As String = ""
            strSQL = "   SELECT Sku_Index "
            strSQL &= "  FROM tb_Barcode_Mapping "
            strSQL &= "  WHERE  Barcode = '" & pstrBarcode & "'"

            'strSQL &= "  WHERE Sku_Index = '" & pstrSku_Index & "' And ' Barcode = '" & pstrBarcode & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Sub InsertBarcodeMapping(ByVal pstr_Sku_index As String, ByVal pstr_Barcode As String, _
                                    ByVal Barcode_SKU As String, _
                                    ByVal Qty_Barcode As Double, _
                                    ByVal DimensionType_Index As String, _
                                    ByVal Dimension_Len As Double, _
                                    ByVal Dimension_Wd As Double, _
                                    ByVal Dimension_Hi As Double, _
                                    ByVal Volume As Double, _
                                    ByVal Weight As Double, _
                                    ByVal Qty_PerPallet As Double)
        Try
            Dim strSQL As String = ""
            Dim strSKU_ID As String = ""
            strSQL = "INSERT INTO tb_Barcode_Mapping (sku_index, barcode,add_by,add_date,barcode_SKU"
            strSQL &= " ,Qty_Barcode,DimensionType_Index,Dimension_Len,Dimension_Wd,Dimension_Hi,Volume,Weight,Qty_PerPallet) "
            strSQL &= " VALUES(@sku_index, @barcode,@add_by,getdate(),@barcode_SKU"
            strSQL &= " ,@Qty_Barcode,@DimensionType_Index,@Dimension_Len,@Dimension_Wd,@Dimension_Hi,@Volume,@Weight,@Qty_PerPallet) "


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@sku_index", SqlDbType.NVarChar).Value = pstr_Sku_index
                .Parameters.Add("@barcode", SqlDbType.NVarChar).Value = pstr_Barcode
                .Parameters.Add("@add_by", SqlDbType.NVarChar).Value = WV_UserFullName 'pstr_add_by
                .Parameters.Add("@barcode_SKU", SqlDbType.NVarChar).Value = Barcode_SKU

                .Parameters.Add("@DimensionType_Index", SqlDbType.NVarChar).Value = DimensionType_Index
                .Parameters.Add("@Qty_Barcode", SqlDbType.Float).Value = Qty_Barcode
                .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = Dimension_Len
                .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = Dimension_Wd
                .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = Dimension_Hi
                .Parameters.Add("@Volume", SqlDbType.Float).Value = Volume
                .Parameters.Add("@Weight", SqlDbType.Float).Value = Weight
                .Parameters.Add("@Qty_PerPallet", SqlDbType.Float).Value = Qty_PerPallet
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            'strSQL = "   SELECT Barcode1 "
            'strSQL &= "  FROM ms_SKU  "
            'strSQL &= "  WHERE Sku_Index ='" & pstr_Sku_index & "'"
            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            '_dataTable = GetDataTable

            'If _dataTable.Rows.Count <= 0 Or _dataTable.Rows(0)("Barcode1").ToString = "" Or IsDBNull(_dataTable.Rows(0)("Barcode1")) = True Then
            '    strSQL = "UPDATE ms_SKU SET Barcode1 = '" & pstr_Barcode & "' "
            '    strSQL &= " WHERE SKU_Index = '" & pstr_Sku_index & "'"

            '    SetSQLString = strSQL
            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            '    connectDB()
            '    EXEC_Command()
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

End Class
