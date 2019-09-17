Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language


Public Class frmImport_Order

    Private Sub frmImport_Order_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Load Document Type (Receipt = 1)
        Dim oms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        oms_DocumentType.getDocumentType(1)

        With cboDocumentType
            .DisplayMember = "Description"
            .ValueMember = "DocumentType_Index"
            .DataSource = oms_DocumentType.DataTable
        End With

        'Load Customer 
        Dim oms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        oms_Customer.SearchData_Click("", " and status <> -1")

        With cboCustomer
            .DisplayMember = "Customer_Name"
            .ValueMember = "Customer_Index"
            .DataSource = oms_Customer.GetDataTable
        End With

        'Load Item Status
        Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)

        With cboItemStatus
            .DisplayMember = "ItemStatusDes"
            .ValueMember = "ItemStatus_Index"
            .DataSource = objDocumentType_Itemstatus.DataTable
        End With

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try

            'checking
            If Me.txtFilePath.Text.Trim = "" Or Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
                Exit Sub
            End If

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            Dim oImport_Order As New Import_Order
            With oImport_Order
                .DataSource = Me.grdPreviewData.DataSource
                .Customer_Index = Me.cboCustomer.SelectedValue
                .DocumentType_Index = Me.cboDocumentType.SelectedValue
                .ItemStatus_Index = Me.cboItemStatus.SelectedValue
                If .CheckingData Then

                    .StartImportData()
                Else
                    W_MSG_Information("กรุณาตรวจสอบข้อมูลนำเข้าใหม่อีกครั้ง")
                    Exit Sub
                End If
            End With

            W_MSG_Information("นำข้อมูลเข้าเรียบร้อยแล้ว")

        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            'checking 
            If Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ Work Sheet")
                Exit Sub
            End If

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                'load data from excel
                Dim oImport_Order As New Import_Order

                Me.grdPreviewData.DataSource = oImport_Order.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub
End Class