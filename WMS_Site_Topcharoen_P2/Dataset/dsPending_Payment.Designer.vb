﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.8937
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On



'''<summary>
'''Represents a strongly typed in-memory cache of data.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
 Global.System.Serializable(),  _
 Global.System.ComponentModel.DesignerCategoryAttribute("code"),  _
 Global.System.ComponentModel.ToolboxItem(true),  _
 Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema"),  _
 Global.System.Xml.Serialization.XmlRootAttribute("dsPending_Payment"),  _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class dsPending_Payment
    Inherits Global.System.Data.DataSet
    
    Private tableVIEW_RPT_Pending_Payment As VIEW_RPT_Pending_PaymentDataTable
    
    Private _schemaSerializationMode As Global.System.Data.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub New()
        MyBase.New
        Me.BeginInit
        Me.InitClass
        Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler MyBase.Relations.CollectionChanged, schemaChangedHandler
        Me.EndInit
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context, false)
        If (Me.IsBinarySerialized(info, context) = true) Then
            Me.InitVars(false)
            Dim schemaChangedHandler1 As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
            AddHandler Me.Tables.CollectionChanged, schemaChangedHandler1
            AddHandler Me.Relations.CollectionChanged, schemaChangedHandler1
            Return
        End If
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(String)),String)
        If (Me.DetermineSchemaSerializationMode(info, context) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
            Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet
            ds.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("VIEW_RPT_Pending_Payment")) Is Nothing) Then
                MyBase.Tables.Add(New VIEW_RPT_Pending_PaymentDataTable(ds.Tables("VIEW_RPT_Pending_Payment")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, Global.System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.Browsable(false),  _
     Global.System.ComponentModel.DesignerSerializationVisibility(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property VIEW_RPT_Pending_Payment() As VIEW_RPT_Pending_PaymentDataTable
        Get
            Return Me.tableVIEW_RPT_Pending_Payment
        End Get
    End Property
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.BrowsableAttribute(true),  _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Visible)>  _
    Public Overrides Property SchemaSerializationMode() As Global.System.Data.SchemaSerializationMode
        Get
            Return Me._schemaSerializationMode
        End Get
        Set
            Me._schemaSerializationMode = value
        End Set
    End Property
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Shadows ReadOnly Property Tables() As Global.System.Data.DataTableCollection
        Get
            Return MyBase.Tables
        End Get
    End Property
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Shadows ReadOnly Property Relations() As Global.System.Data.DataRelationCollection
        Get
            Return MyBase.Relations
        End Get
    End Property
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Sub InitializeDerivedDataSet()
        Me.BeginInit
        Me.InitClass
        Me.EndInit
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Overrides Function Clone() As Global.System.Data.DataSet
        Dim cln As dsPending_Payment = CType(MyBase.Clone,dsPending_Payment)
        cln.InitVars
        cln.SchemaSerializationMode = Me.SchemaSerializationMode
        Return cln
    End Function
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return false
    End Function
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return false
    End Function
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As Global.System.Xml.XmlReader)
        If (Me.DetermineSchemaSerializationMode(reader) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
            Me.Reset
            Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet
            ds.ReadXml(reader)
            If (Not (ds.Tables("VIEW_RPT_Pending_Payment")) Is Nothing) Then
                MyBase.Tables.Add(New VIEW_RPT_Pending_PaymentDataTable(ds.Tables("VIEW_RPT_Pending_Payment")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, Global.System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.ReadXml(reader)
            Me.InitVars
        End If
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Protected Overrides Function GetSchemaSerializable() As Global.System.Xml.Schema.XmlSchema
        Dim stream As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
        Me.WriteXmlSchema(New Global.System.Xml.XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return Global.System.Xml.Schema.XmlSchema.Read(New Global.System.Xml.XmlTextReader(stream), Nothing)
    End Function
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Friend Overloads Sub InitVars()
        Me.InitVars(true)
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Friend Overloads Sub InitVars(ByVal initTable As Boolean)
        Me.tableVIEW_RPT_Pending_Payment = CType(MyBase.Tables("VIEW_RPT_Pending_Payment"),VIEW_RPT_Pending_PaymentDataTable)
        If (initTable = true) Then
            If (Not (Me.tableVIEW_RPT_Pending_Payment) Is Nothing) Then
                Me.tableVIEW_RPT_Pending_Payment.InitVars
            End If
        End If
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "dsPending_Payment"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/dsPending_Payment.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableVIEW_RPT_Pending_Payment = New VIEW_RPT_Pending_PaymentDataTable
        MyBase.Tables.Add(Me.tableVIEW_RPT_Pending_Payment)
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeVIEW_RPT_Pending_Payment() As Boolean
        Return false
    End Function
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As Global.System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = Global.System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Shared Function GetTypedDataSetSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
        Dim ds As dsPending_Payment = New dsPending_Payment
        Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
        Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
        Dim any As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
        any.Namespace = ds.Namespace
        sequence.Items.Add(any)
        type.Particle = sequence
        Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable
        If xs.Contains(dsSchema.TargetNamespace) Then
            Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
            Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
            Try 
                Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                dsSchema.Write(s1)
                Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator
                Do While schemas.MoveNext
                    schema = CType(schemas.Current,Global.System.Xml.Schema.XmlSchema)
                    s2.SetLength(0)
                    schema.Write(s2)
                    If (s1.Length = s2.Length) Then
                        s1.Position = 0
                        s2.Position = 0
                        
                        Do While ((s1.Position <> s1.Length)  _
                                    AndAlso (s1.ReadByte = s2.ReadByte))
                            
                            
                        Loop
                        If (s1.Position = s1.Length) Then
                            Return type
                        End If
                    End If
                    
                Loop
            Finally
                If (Not (s1) Is Nothing) Then
                    s1.Close
                End If
                If (Not (s2) Is Nothing) Then
                    s2.Close
                End If
            End Try
        End If
        xs.Add(dsSchema)
        Return type
    End Function
    
    Public Delegate Sub VIEW_RPT_Pending_PaymentRowChangeEventHandler(ByVal sender As Object, ByVal e As VIEW_RPT_Pending_PaymentRowChangeEvent)
    
    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     Global.System.Serializable(),  _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class VIEW_RPT_Pending_PaymentDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable
        
        Private columnCustomer_Id As Global.System.Data.DataColumn
        
        Private columnCustomer_Name As Global.System.Data.DataColumn
        
        Private columnTotal_Amount As Global.System.Data.DataColumn
        
        Private columnToTal_Payment_Amount As Global.System.Data.DataColumn
        
        Private columnPending_Payment As Global.System.Data.DataColumn
        
        Private columnaddress As Global.System.Data.DataColumn
        
        Private columntel As Global.System.Data.DataColumn
        
        Private columnfax As Global.System.Data.DataColumn
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "VIEW_RPT_Pending_Payment"
            Me.BeginInit
            Me.InitClass
            Me.EndInit
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal table As Global.System.Data.DataTable)
            MyBase.New
            Me.TableName = table.TableName
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
            Me.InitVars
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Customer_IdColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCustomer_Id
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Customer_NameColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCustomer_Name
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Total_AmountColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnTotal_Amount
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property ToTal_Payment_AmountColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnToTal_Payment_Amount
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Pending_PaymentColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnPending_Payment
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property addressColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnaddress
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property telColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columntel
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property faxColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnfax
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count() As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Default ReadOnly Property Item(ByVal index As Integer) As VIEW_RPT_Pending_PaymentRow
            Get
                Return CType(Me.Rows(index),VIEW_RPT_Pending_PaymentRow)
            End Get
        End Property
        
        Public Event VIEW_RPT_Pending_PaymentRowChanging As VIEW_RPT_Pending_PaymentRowChangeEventHandler
        
        Public Event VIEW_RPT_Pending_PaymentRowChanged As VIEW_RPT_Pending_PaymentRowChangeEventHandler
        
        Public Event VIEW_RPT_Pending_PaymentRowDeleting As VIEW_RPT_Pending_PaymentRowChangeEventHandler
        
        Public Event VIEW_RPT_Pending_PaymentRowDeleted As VIEW_RPT_Pending_PaymentRowChangeEventHandler
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddVIEW_RPT_Pending_PaymentRow(ByVal row As VIEW_RPT_Pending_PaymentRow)
            Me.Rows.Add(row)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddVIEW_RPT_Pending_PaymentRow(ByVal Customer_Id As String, ByVal Customer_Name As String, ByVal Total_Amount As Double, ByVal ToTal_Payment_Amount As Double, ByVal Pending_Payment As Double, ByVal address As String, ByVal tel As String, ByVal fax As String) As VIEW_RPT_Pending_PaymentRow
            Dim rowVIEW_RPT_Pending_PaymentRow As VIEW_RPT_Pending_PaymentRow = CType(Me.NewRow,VIEW_RPT_Pending_PaymentRow)
            Dim columnValuesArray() As Object = New Object() {Customer_Id, Customer_Name, Total_Amount, ToTal_Payment_Amount, Pending_Payment, address, tel, fax}
            rowVIEW_RPT_Pending_PaymentRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowVIEW_RPT_Pending_PaymentRow)
            Return rowVIEW_RPT_Pending_PaymentRow
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As VIEW_RPT_Pending_PaymentDataTable = CType(MyBase.Clone,VIEW_RPT_Pending_PaymentDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New VIEW_RPT_Pending_PaymentDataTable
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columnCustomer_Id = MyBase.Columns("Customer_Id")
            Me.columnCustomer_Name = MyBase.Columns("Customer_Name")
            Me.columnTotal_Amount = MyBase.Columns("Total_Amount")
            Me.columnToTal_Payment_Amount = MyBase.Columns("ToTal_Payment_Amount")
            Me.columnPending_Payment = MyBase.Columns("Pending_Payment")
            Me.columnaddress = MyBase.Columns("address")
            Me.columntel = MyBase.Columns("tel")
            Me.columnfax = MyBase.Columns("fax")
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columnCustomer_Id = New Global.System.Data.DataColumn("Customer_Id", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCustomer_Id)
            Me.columnCustomer_Name = New Global.System.Data.DataColumn("Customer_Name", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCustomer_Name)
            Me.columnTotal_Amount = New Global.System.Data.DataColumn("Total_Amount", GetType(Double), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnTotal_Amount)
            Me.columnToTal_Payment_Amount = New Global.System.Data.DataColumn("ToTal_Payment_Amount", GetType(Double), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnToTal_Payment_Amount)
            Me.columnPending_Payment = New Global.System.Data.DataColumn("Pending_Payment", GetType(Double), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnPending_Payment)
            Me.columnaddress = New Global.System.Data.DataColumn("address", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnaddress)
            Me.columntel = New Global.System.Data.DataColumn("tel", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columntel)
            Me.columnfax = New Global.System.Data.DataColumn("fax", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnfax)
            Me.columnCustomer_Id.AllowDBNull = false
            Me.columnCustomer_Id.MaxLength = 13
            Me.columnCustomer_Name.MaxLength = 100
            Me.columnaddress.MaxLength = 255
            Me.columntel.MaxLength = 50
            Me.columnfax.MaxLength = 50
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewVIEW_RPT_Pending_PaymentRow() As VIEW_RPT_Pending_PaymentRow
            Return CType(Me.NewRow,VIEW_RPT_Pending_PaymentRow)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New VIEW_RPT_Pending_PaymentRow(builder)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(VIEW_RPT_Pending_PaymentRow)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.VIEW_RPT_Pending_PaymentRowChangedEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_Pending_PaymentRowChanged(Me, New VIEW_RPT_Pending_PaymentRowChangeEvent(CType(e.Row,VIEW_RPT_Pending_PaymentRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.VIEW_RPT_Pending_PaymentRowChangingEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_Pending_PaymentRowChanging(Me, New VIEW_RPT_Pending_PaymentRowChangeEvent(CType(e.Row,VIEW_RPT_Pending_PaymentRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.VIEW_RPT_Pending_PaymentRowDeletedEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_Pending_PaymentRowDeleted(Me, New VIEW_RPT_Pending_PaymentRowChangeEvent(CType(e.Row,VIEW_RPT_Pending_PaymentRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.VIEW_RPT_Pending_PaymentRowDeletingEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_Pending_PaymentRowDeleting(Me, New VIEW_RPT_Pending_PaymentRowChangeEvent(CType(e.Row,VIEW_RPT_Pending_PaymentRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveVIEW_RPT_Pending_PaymentRow(ByVal row As VIEW_RPT_Pending_PaymentRow)
            Me.Rows.Remove(row)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As dsPending_Payment = New dsPending_Payment
            Dim any1 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
            any1.Namespace = "http://www.w3.org/2001/XMLSchema"
            any1.MinOccurs = New Decimal(0)
            any1.MaxOccurs = Decimal.MaxValue
            any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any1)
            Dim any2 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny
            any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
            any2.MinOccurs = New Decimal(1)
            any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
            sequence.Items.Add(any2)
            Dim attribute1 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute
            attribute1.Name = "namespace"
            attribute1.FixedValue = ds.Namespace
            type.Attributes.Add(attribute1)
            Dim attribute2 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute
            attribute2.Name = "tableTypeName"
            attribute2.FixedValue = "VIEW_RPT_Pending_PaymentDataTable"
            type.Attributes.Add(attribute2)
            type.Particle = sequence
            Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable
            If xs.Contains(dsSchema.TargetNamespace) Then
                Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
                Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream
                Try 
                    Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                    dsSchema.Write(s1)
                    Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator
                    Do While schemas.MoveNext
                        schema = CType(schemas.Current,Global.System.Xml.Schema.XmlSchema)
                        s2.SetLength(0)
                        schema.Write(s2)
                        If (s1.Length = s2.Length) Then
                            s1.Position = 0
                            s2.Position = 0
                            
                            Do While ((s1.Position <> s1.Length)  _
                                        AndAlso (s1.ReadByte = s2.ReadByte))
                                
                                
                            Loop
                            If (s1.Position = s1.Length) Then
                                Return type
                            End If
                        End If
                        
                    Loop
                Finally
                    If (Not (s1) Is Nothing) Then
                        s1.Close
                    End If
                    If (Not (s2) Is Nothing) Then
                        s2.Close
                    End If
                End Try
            End If
            xs.Add(dsSchema)
            Return type
        End Function
    End Class
    
    '''<summary>
    '''Represents strongly named DataRow class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Partial Public Class VIEW_RPT_Pending_PaymentRow
        Inherits Global.System.Data.DataRow
        
        Private tableVIEW_RPT_Pending_Payment As VIEW_RPT_Pending_PaymentDataTable
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableVIEW_RPT_Pending_Payment = CType(Me.Table,VIEW_RPT_Pending_PaymentDataTable)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Customer_Id() As String
            Get
                Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.Customer_IdColumn),String)
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.Customer_IdColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Customer_Name() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.Customer_NameColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Customer_Name' in table 'VIEW_RPT_Pending_Payment' is DBNul"& _ 
                            "l.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.Customer_NameColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Total_Amount() As Double
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.Total_AmountColumn),Double)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Total_Amount' in table 'VIEW_RPT_Pending_Payment' is DBNull"& _ 
                            ".", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.Total_AmountColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property ToTal_Payment_Amount() As Double
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.ToTal_Payment_AmountColumn),Double)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'ToTal_Payment_Amount' in table 'VIEW_RPT_Pending_Payment' i"& _ 
                            "s DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.ToTal_Payment_AmountColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Pending_Payment() As Double
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.Pending_PaymentColumn),Double)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Pending_Payment' in table 'VIEW_RPT_Pending_Payment' is DBN"& _ 
                            "ull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.Pending_PaymentColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property address() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.addressColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'address' in table 'VIEW_RPT_Pending_Payment' is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.addressColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property tel() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.telColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'tel' in table 'VIEW_RPT_Pending_Payment' is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.telColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property fax() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_Pending_Payment.faxColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'fax' in table 'VIEW_RPT_Pending_Payment' is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_Pending_Payment.faxColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsCustomer_NameNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.Customer_NameColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetCustomer_NameNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.Customer_NameColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsTotal_AmountNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.Total_AmountColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetTotal_AmountNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.Total_AmountColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsToTal_Payment_AmountNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.ToTal_Payment_AmountColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetToTal_Payment_AmountNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.ToTal_Payment_AmountColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsPending_PaymentNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.Pending_PaymentColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetPending_PaymentNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.Pending_PaymentColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsaddressNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.addressColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetaddressNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.addressColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IstelNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.telColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SettelNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.telColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsfaxNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_Pending_Payment.faxColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetfaxNull()
            Me(Me.tableVIEW_RPT_Pending_Payment.faxColumn) = Global.System.Convert.DBNull
        End Sub
    End Class
    
    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class VIEW_RPT_Pending_PaymentRowChangeEvent
        Inherits Global.System.EventArgs
        
        Private eventRow As VIEW_RPT_Pending_PaymentRow
        
        Private eventAction As Global.System.Data.DataRowAction
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As VIEW_RPT_Pending_PaymentRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As VIEW_RPT_Pending_PaymentRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Action() As Global.System.Data.DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class