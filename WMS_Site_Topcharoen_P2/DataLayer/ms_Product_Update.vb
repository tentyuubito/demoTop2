'*** Create Date :  17/01/2008
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module


Public Class ms_Product_Update : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _product_Index As String
    Private _product_Id As String
    Private _product_Name_th As String
    Private _product_Name_en As String
    Private _productType_Index As String
    Private _std_Package_Index As String
    Private _std_Unit_Cost As String
    Private _default_Exp_Day As Integer
    '***********************************
    Private _file1 As String
    Private _file2 As String
    Private _file3 As String
    Private _str1 As String
    Private _str2 As String
    Private _str3 As String
    Private _str4 As String
    Private _str5 As String
    Private _flo1 As Double
    Private _flo2 As Double
    Private _flo3 As Double
    Private _flo4 As Double
    Private _flo5 As Double
    '***********************************
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer

    Public objProduct_index As String

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum
#End Region

#Region " Properties Section "
    '*** Property Readonly
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

    '*** Property Writeonly

    '*** Property Read and Write
    Public Property Product_Index() As String
        Get
            Return _product_Index
        End Get
        Set(ByVal Value As String)
            _product_Index = Value
        End Set
    End Property
    Public Property Product_Id() As String
        Get
            Return _product_Id
        End Get
        Set(ByVal Value As String)
            _product_Id = Value
        End Set
    End Property
    Public Property Product_Name_th() As String
        Get
            Return _product_Name_th
        End Get
        Set(ByVal Value As String)
            _product_Name_th = Value
        End Set
    End Property
    Public Property Product_Name_en() As String
        Get
            Return _product_Name_en
        End Get
        Set(ByVal Value As String)
            _product_Name_en = Value
        End Set
    End Property
    Public Property ProductType_Index() As String
        Get
            Return _productType_Index
        End Get
        Set(ByVal Value As String)
            _productType_Index = Value
        End Set
    End Property
    Public Property Std_Package_Index() As String
        Get
            Return _std_Package_Index
        End Get
        Set(ByVal Value As String)
            _std_Package_Index = Value
        End Set
    End Property
    Public Property Std_Unit_Cost() As String
        Get
            Return _std_Unit_Cost
        End Get
        Set(ByVal Value As String)
            _std_Unit_Cost = Value
        End Set
    End Property
    Public Property Default_Exp_Day() As Integer
        Get
            Return _default_Exp_Day
        End Get
        Set(ByVal Value As Integer)
            _default_Exp_Day = Value
        End Set
    End Property
    Public Property File1() As String
        Get
            Return _file1
        End Get
        Set(ByVal Value As String)
            _file1 = Value
        End Set
    End Property
    Public Property File2() As String
        Get
            Return _file2
        End Get
        Set(ByVal Value As String)
            _file2 = Value
        End Set
    End Property
    Public Property File3() As String
        Get
            Return _file3
        End Get
        Set(ByVal Value As String)
            _file3 = Value
        End Set
    End Property
    Public Property Str1() As String
        Get
            Return _str1
        End Get
        Set(ByVal Value As String)
            _str1 = Value
        End Set
    End Property
    Public Property Str2() As String
        Get
            Return _str2
        End Get
        Set(ByVal Value As String)
            _str2 = Value
        End Set
    End Property
    Public Property Str3() As String
        Get
            Return _str3
        End Get
        Set(ByVal Value As String)
            _str3 = Value
        End Set
    End Property
    Public Property Str4() As String
        Get
            Return _str4
        End Get
        Set(ByVal Value As String)
            _str4 = Value
        End Set
    End Property
    Public Property Str5() As String
        Get
            Return _str5
        End Get
        Set(ByVal Value As String)
            _str5 = Value
        End Set
    End Property
    Public Property Flo1() As Double
        Get
            Return _flo1
        End Get
        Set(ByVal Value As Double)
            _flo1 = Value
        End Set
    End Property
    Public Property Flo2() As Double
        Get
            Return _flo2
        End Get
        Set(ByVal Value As Double)
            _flo2 = Value
        End Set
    End Property
    Public Property Flo3() As Double
        Get
            Return _flo3
        End Get
        Set(ByVal Value As Double)
            _flo3 = Value
        End Set
    End Property
    Public Property Flo4() As Double
        Get
            Return _flo4
        End Get
        Set(ByVal Value As Double)
            _flo4 = Value
        End Set
    End Property
    Public Property Flo5() As Double
        Get
            Return _flo5
        End Get
        Set(ByVal Value As Double)
            _flo5 = Value
        End Set
    End Property
    Public Property Add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property
    Public Property Add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property
    Public Property Add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property
    Public Property Update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property
    Public Property Update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property
    Public Property Update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
    End Sub




#End Region


    '*** Normal DB Method
#Region " SELECT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " SELECT     *   " & _
                         " FROM       ms_Product where status_id != -1"

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 

            End If




            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub SearchData_ClickTop200(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " SELECT  Top 200 *   " & _
                         " FROM       ms_Product where status_id != -1"

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 

            End If




            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub Select_GetSku(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = "SELECT     *, ms_Product.*, ms_ProductType.Description AS ProductType, ms_Package.Description AS Package"
                strSQL &= " FROM         ms_Product LEFT JOIN"
                strSQL &= " ms_ProductType ON ms_Product.ProductType_Index = ms_ProductType.ProductType_Index LEFT JOIN"
                strSQL &= " ms_Package ON ms_Product.Std_Package_Index = ms_Package.Package_Index"
                strSQL &= " where ms_Product.status_id != -1"
                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 

            End If




            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub SelectData_For_Edit(ByVal pFilterValue As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Product "

            strWhere = ""

            strWhere += " WHERE  ms_Product.product_Id = '" & pFilterValue & "' and status_id != -1"


            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    '///// ja add pop up product on date--27 may 2551
    Public Sub getPopup_Search(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            ' Select No Condition 
            strSQL = " select Product_Index,Product_Id,Product_Name_th,Product_Name_en" & _
                     " from ms_product where Product_Index not in ('0') and status_id  not in (-1) "

            SetSQLString = strSQL & WhereString

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    '//////////////////
#End Region
#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function Insert_Master() As Boolean
        Dim strSQL As String

        Try


            strSQL = " insert into ms_Product (product_Index,product_Id,product_Name_th,product_Name_en,productType_Index,std_Package_Index,Std_Unit_Cost,add_by,add_date,add_branch) "
            strSQL &= " values (@product_Index,@product_Id ,@product_Name_th,@product_Name_en,@productType_Index,@std_Package_Index,@Std_Unit_Cost,@add_by,getdate(),@add_branch)"


            strSQL = strSQL
            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 30).Value = Me._product_Index
                .Parameters.Add("@product_Id", SqlDbType.VarChar, 50).Value = Me._product_Id
                .Parameters.Add("@product_Name_th", SqlDbType.VarChar, 255).Value = Me._product_Name_th

                .Parameters.Add("@product_Name_en", SqlDbType.VarChar, 255).Value = Me._product_Name_en
                .Parameters.Add("@productType_Index", SqlDbType.VarChar, 100).Value = Me._productType_Index
                .Parameters.Add("@std_Package_Index", SqlDbType.VarChar, 100).Value = Me._std_Package_Index
                .Parameters.Add("@Std_Unit_Cost", SqlDbType.VarChar, 100).Value = Me._std_Unit_Cost
                '.Parameters.Add("@ Default_Exp_Day", SqlDbType.Int, 30).Value = Me._default_Exp_Day


                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
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
            strSQL = Nothing

        End Try
    End Function
#End Region
#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function Update_Master() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Product set product_Id=@product_Id"
            strSQL &= ",product_Name_th =@product_Name_th"
            strSQL &= ",Product_Name_en = @Product_Name_en"
            strSQL &= ",ProductType_Index =@ProductType_Index"
            strSQL &= ",Std_Package_Index =@Std_Package_Index"
            strSQL &= ",Std_Unit_Cost = @Std_Unit_Cost"
            strSQL &= ",Default_Exp_Day =@Default_Exp_Day"
            strSQL &= ",Update_by =@Update_by"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= @update_branch"
            strSQL &= " WHERE product_Index=@product_Index"


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 30).Value = Me._product_Index
                .Parameters.Add("@product_Id", SqlDbType.VarChar, 50).Value = Me._product_Id
                .Parameters.Add("@product_Name_th", SqlDbType.VarChar, 255).Value = Me._product_Name_th
                .Parameters.Add("@product_Name_en", SqlDbType.VarChar, 255).Value = Me._product_Name_en
                .Parameters.Add("@productType_Index", SqlDbType.VarChar, 100).Value = Me._productType_Index
                .Parameters.Add("@std_Package_Index", SqlDbType.VarChar, 100).Value = Me._std_Package_Index
                .Parameters.Add("@Std_Unit_Cost", SqlDbType.VarChar, 100).Value = Me._std_Unit_Cost
                .Parameters.Add("@Default_Exp_Day", SqlDbType.Int, 30).Value = Me._default_Exp_Day
                .Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
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
            strSQL = Nothing
        End Try
    End Function
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Delete_Master(ByVal product_Index As String) As Boolean
        ' *** Define value from parameter
        Me._product_Index = product_Index

        Dim strSQL As String

        Try
            'strSQL = " Delete  " & _
            '         " From ms_Product " & _
            '         " WHERE product_Index='" + Me._product_Index + "' "

            strSQL = "UPDATE ms_Product SET status_id = -1"
            strSQL &= ", update_by = '" + WV_UserName + "'"
            strSQL &= ", update_date=getdate() "
            strSQL &= ", update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE product_Index='" + Me._product_Index + "' "

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
            strSQL = Nothing
        End Try
    End Function
#End Region
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    'เปลี่ยนจาก _product_Id เป็น _product_Index
    Private Function isExistID(ByVal _product_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Product where product_Id = '" & _product_Id & "' AND status_id <> -1  "
            'strSQL = " select count(*) from ms_Product where product_Index = '" & _product_Index & "' AND status_id <> -1  "
            'SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@product_Id", SqlDbType.VarChar, 20).Value = _product_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "

    Public Function SaveData(ByVal _product_Index As String, ByVal _product_Id As String, ByVal _product_Name_th As String, ByVal _product_Name_en As String, ByVal _productType_Index As String, ByVal _std_Package_Index As String, ByVal _std_Unit_Cost As String, Optional ByVal _isNewCustomer As Boolean = False) As Boolean

        ' ***  define value to field ***
        Me._product_Id = _product_Id
        Me._product_Name_th = _product_Name_th

        Me._product_Name_en = _product_Name_en
        Me._productType_Index = _productType_Index
        Me._std_Package_Index = _std_Package_Index
        Me._std_Unit_Cost = _std_Unit_Cost
        Me._default_Exp_Day = _default_Exp_Day

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._product_Index = objDBIndex.getSys_Value("product_Index")
                objDBIndex = Nothing
                objProduct_index = Me._product_Index



                ''****************************Pui
                'If Me._product_Id = "" Then
                '    Me._product_Id = Me._product_Index
                'End If
                '***********************
            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._product_Index = _product_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If Not _isNewCustomer And isExistID(_product_Index) Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        '  Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_Master = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            '   Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            '    Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        '   Exit Function
                    Else
                        ' MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        '  Exit Function
                    End If

                    'Case enuOperation_Type.DELETE
                    '    ' **** check value some table if need !! 
                    '    If Me.DeleteSize_Master() = True Then
                    '        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return True
                    '        Exit Function

                    '    Else
                    '        MessageBox.Show("ไม่สามารถลบ", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False
                    '        Exit Function
                    '    End If

            End Select

        Catch ex As Exception
            Throw ex
        End Try


    End Function
#End Region
End Class