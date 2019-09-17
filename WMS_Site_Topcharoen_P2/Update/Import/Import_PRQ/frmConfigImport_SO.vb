Public Class frmConfigImport_SO
    Private _config_PO_No As Integer = 0
    Private _config_PO_Date As Integer = 0
    Private _config_Supplier_Code As Integer = 0
    Private _config_Supplier_Name As Integer = 0
    Private _config_Supplier_Address As Integer = 0
    Private _config_PO_Exp As Integer = 0
    Private _config_Supplier_Tel As Integer = 0
    Private _config_Supplier_Fax As Integer = 0
    Private _config_Sku_Id As Integer = 0
    Private _config_Sku_Name As Integer = 0
    Private _config_Sku_Package As Integer = 0
    Private _config_Sku_Qty As Integer = 0
    Private _config_Sku_Price As Integer = 0
    Private _config_Remark As Integer = 0


    Private _Format_Import_Index As String = ""
    Public Property Format_Import_Index() As String
        Get
            Return _Format_Import_Index
        End Get
        Set(ByVal value As String)
            _Format_Import_Index = value
        End Set
    End Property


    Public Property Config_PO_No() As Integer
        Get
            Return _config_PO_No
        End Get
        Set(ByVal value As Integer)
            _config_PO_No = value
        End Set
    End Property
    Public Property Config_PO_Date() As Integer
        Get
            Return _config_PO_Date
        End Get
        Set(ByVal value As Integer)
            _config_PO_Date = value
        End Set
    End Property
    Public Property Config_Supplier_Code() As Integer
        Get
            Return _config_Supplier_Code
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Code = value
        End Set
    End Property
    Public Property Config_Supplier_Name() As Integer
        Get
            Return _config_Supplier_Name
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Name = value
        End Set
    End Property
    Public Property Config_Supplier_Address() As Integer
        Get
            Return _config_Supplier_Address
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Address = value
        End Set
    End Property
    Public Property Config_PO_Exp() As Integer
        Get
            Return _config_PO_Exp
        End Get
        Set(ByVal value As Integer)
            _config_PO_Exp = value
        End Set
    End Property
    Public Property Config_Supplier_Tel() As Integer
        Get
            Return _config_Supplier_Tel
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Tel = value
        End Set
    End Property
    Public Property Config_Supplier_Fax() As Integer
        Get
            Return _config_Supplier_Fax
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Fax = value
        End Set
    End Property
    Public Property Config_Sku_Id() As Integer
        Get
            Return _config_Sku_Id
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Id = value
        End Set
    End Property
    Public Property Config_Sku_Name() As Integer
        Get
            Return _config_Sku_Name
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Name = value
        End Set
    End Property
    Public Property Config_Sku_Package() As Integer
        Get
            Return _config_Sku_Package
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Package = value
        End Set
    End Property
    Public Property Config_Sku_Qty() As Integer
        Get
            Return _config_Sku_Qty
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Qty = value
        End Set
    End Property
    Public Property Config_Sku_Price() As Integer
        Get
            Return _config_Sku_Price
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Price = value
        End Set
    End Property
    Public Property Config_Remark() As Integer
        Get
            Return _config_Remark
        End Get
        Set(ByVal value As Integer)
            _config_Remark = value
        End Set
    End Property

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Dim objImportSO As New bl_Import_SO_V2
            objImportSO.Config_SO_No = Me.txt_PO_NO.Text
            objImportSO.Config_SO_Date = Me.txtPO_Date.Text
            objImportSO.Config_Supplier_Code = Me.txtSupplier_Code.Text
            objImportSO.Config_Supplier_Name = Me.txtSupplier_Name.Text
            objImportSO.Config_Supplier_Address = Me.txtSupplier_Address.Text
            objImportSO.Config_SO_Exp = Me.txtPO_Exp.Text
            objImportSO.Config_Supplier_Tel = Me.txtSupplier_Tel.Text
            objImportSO.Config_Supplier_Fax = Me.txtSupplier_Fax.Text
            objImportSO.Config_Sku_Id = Me.txtSku_Id.Text
            objImportSO.Config_Sku_Name = Me.txtSku_Name.Text
            objImportSO.Config_Sku_Package = Me.txtSku_Package.Text
            objImportSO.Config_Sku_Qty = Me.txtSku_Qty.Text
            objImportSO.Config_Sku_Price = Me.txtSku_Price.Text
            objImportSO.Config_Remark = Me.txtRemark.Text


           


            objImportSO.UpdateConfig(Me.Format_Import_Index)
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย")
            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show("บันทึกผิดพลาด")
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmConfigImport_PO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

       
            Dim objImportSO As New bl_Import_SO_V2
            objImportSO.GetConfig_Import_SO(Me._Format_Import_Index)
            Dim dt As New DataTable
            dt = objImportSO.DataTable
            If dt.Rows.Count = 0 Then Exit Sub
            Me.txt_PO_NO.Text = dt.Rows(0).Item("SOLD_TO_ID").ToString
            Me.txtPO_Date.Text = dt.Rows(0).Item("SO_NO").ToString
            Me.txtSupplier_Code.Text = dt.Rows(0).Item("SOLD_TO_NAME").ToString
            Me.txtSupplier_Name.Text = dt.Rows(0).Item("DOC_DATE").ToString
            Me.txtSupplier_Address.Text = dt.Rows(0).Item("SOLD_TO_ADD1").ToString
            Me.txtPO_Exp.Text = dt.Rows(0).Item("SALE_NAME").ToString
            Me.txtSupplier_Tel.Text = dt.Rows(0).Item("SOLD_TO_ADD2").ToString
            Me.txtSupplier_Fax.Text = dt.Rows(0).Item("SOLD_TO_TEL").ToString
            Me.txtSku_Id.Text = dt.Rows(0).Item("EXPECT_DELIVERY_DATE").ToString
            Me.txtSku_Name.Text = dt.Rows(0).Item("SEQ").ToString
            Me.txtSku_Package.Text = dt.Rows(0).Item("SKU_ID").ToString
            Me.txtSku_Qty.Text = dt.Rows(0).Item("QTY").ToString
            Me.txtSku_Price.Text = dt.Rows(0).Item("PACKAGE").ToString
            Me.txtRemark.Text = dt.Rows(0).Item("Remark").ToString

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class