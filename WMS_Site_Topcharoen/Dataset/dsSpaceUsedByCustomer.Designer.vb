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
 Global.System.Xml.Serialization.XmlRootAttribute("dsSpaceUsedByCustomer"),  _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>  _
Partial Public Class dsSpaceUsedByCustomer
    Inherits Global.System.Data.DataSet
    
    Private tableVIEW_RPT_SpaceUsedByCustomer As VIEW_RPT_SpaceUsedByCustomerDataTable
    
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
            If (Not (ds.Tables("VIEW_RPT_SpaceUsedByCustomer")) Is Nothing) Then
                MyBase.Tables.Add(New VIEW_RPT_SpaceUsedByCustomerDataTable(ds.Tables("VIEW_RPT_SpaceUsedByCustomer")))
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
    Public ReadOnly Property VIEW_RPT_SpaceUsedByCustomer() As VIEW_RPT_SpaceUsedByCustomerDataTable
        Get
            Return Me.tableVIEW_RPT_SpaceUsedByCustomer
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
        Dim cln As dsSpaceUsedByCustomer = CType(MyBase.Clone,dsSpaceUsedByCustomer)
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
            If (Not (ds.Tables("VIEW_RPT_SpaceUsedByCustomer")) Is Nothing) Then
                MyBase.Tables.Add(New VIEW_RPT_SpaceUsedByCustomerDataTable(ds.Tables("VIEW_RPT_SpaceUsedByCustomer")))
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
        Me.tableVIEW_RPT_SpaceUsedByCustomer = CType(MyBase.Tables("VIEW_RPT_SpaceUsedByCustomer"),VIEW_RPT_SpaceUsedByCustomerDataTable)
        If (initTable = true) Then
            If (Not (Me.tableVIEW_RPT_SpaceUsedByCustomer) Is Nothing) Then
                Me.tableVIEW_RPT_SpaceUsedByCustomer.InitVars
            End If
        End If
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Sub InitClass()
        Me.DataSetName = "dsSpaceUsedByCustomer"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/dsSpaceUsedByCustomer.xsd"
        Me.EnforceConstraints = true
        Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
        Me.tableVIEW_RPT_SpaceUsedByCustomer = New VIEW_RPT_SpaceUsedByCustomerDataTable
        MyBase.Tables.Add(Me.tableVIEW_RPT_SpaceUsedByCustomer)
    End Sub
    
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Private Function ShouldSerializeVIEW_RPT_SpaceUsedByCustomer() As Boolean
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
        Dim ds As dsSpaceUsedByCustomer = New dsSpaceUsedByCustomer
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
    
    Public Delegate Sub VIEW_RPT_SpaceUsedByCustomerRowChangeEventHandler(ByVal sender As Object, ByVal e As VIEW_RPT_SpaceUsedByCustomerRowChangeEvent)
    
    '''<summary>
    '''Represents the strongly named DataTable class.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"),  _
     Global.System.Serializable(),  _
     Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>  _
    Partial Public Class VIEW_RPT_SpaceUsedByCustomerDataTable
        Inherits Global.System.Data.DataTable
        Implements Global.System.Collections.IEnumerable
        
        Private columnCustomer_Index As Global.System.Data.DataColumn
        
        Private columnCustomer_Name As Global.System.Data.DataColumn
        
        Private columnBin_Count As Global.System.Data.DataColumn
        
        Private columnPercent_All_Customer As Global.System.Data.DataColumn
        
        Private columnPercent_All_Location As Global.System.Data.DataColumn
        
        Private columnPercent_Graph As Global.System.Data.DataColumn
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New()
            MyBase.New
            Me.TableName = "VIEW_RPT_SpaceUsedByCustomer"
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
        Public ReadOnly Property Customer_IndexColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCustomer_Index
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Customer_NameColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnCustomer_Name
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Bin_CountColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnBin_Count
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Percent_All_CustomerColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnPercent_All_Customer
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Percent_All_LocationColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnPercent_All_Location
            End Get
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Percent_GraphColumn() As Global.System.Data.DataColumn
            Get
                Return Me.columnPercent_Graph
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
        Public Default ReadOnly Property Item(ByVal index As Integer) As VIEW_RPT_SpaceUsedByCustomerRow
            Get
                Return CType(Me.Rows(index),VIEW_RPT_SpaceUsedByCustomerRow)
            End Get
        End Property
        
        Public Event VIEW_RPT_SpaceUsedByCustomerRowChanging As VIEW_RPT_SpaceUsedByCustomerRowChangeEventHandler
        
        Public Event VIEW_RPT_SpaceUsedByCustomerRowChanged As VIEW_RPT_SpaceUsedByCustomerRowChangeEventHandler
        
        Public Event VIEW_RPT_SpaceUsedByCustomerRowDeleting As VIEW_RPT_SpaceUsedByCustomerRowChangeEventHandler
        
        Public Event VIEW_RPT_SpaceUsedByCustomerRowDeleted As VIEW_RPT_SpaceUsedByCustomerRowChangeEventHandler
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Sub AddVIEW_RPT_SpaceUsedByCustomerRow(ByVal row As VIEW_RPT_SpaceUsedByCustomerRow)
            Me.Rows.Add(row)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overloads Function AddVIEW_RPT_SpaceUsedByCustomerRow(ByVal Customer_Index As String, ByVal Customer_Name As String, ByVal Bin_Count As Integer, ByVal Percent_All_Customer As Decimal, ByVal Percent_All_Location As Decimal, ByVal Percent_Graph As String) As VIEW_RPT_SpaceUsedByCustomerRow
            Dim rowVIEW_RPT_SpaceUsedByCustomerRow As VIEW_RPT_SpaceUsedByCustomerRow = CType(Me.NewRow,VIEW_RPT_SpaceUsedByCustomerRow)
            Dim columnValuesArray() As Object = New Object() {Customer_Index, Customer_Name, Bin_Count, Percent_All_Customer, Percent_All_Location, Percent_Graph}
            rowVIEW_RPT_SpaceUsedByCustomerRow.ItemArray = columnValuesArray
            Me.Rows.Add(rowVIEW_RPT_SpaceUsedByCustomerRow)
            Return rowVIEW_RPT_SpaceUsedByCustomerRow
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overridable Function GetEnumerator() As Global.System.Collections.IEnumerator Implements Global.System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Overrides Function Clone() As Global.System.Data.DataTable
            Dim cln As VIEW_RPT_SpaceUsedByCustomerDataTable = CType(MyBase.Clone,VIEW_RPT_SpaceUsedByCustomerDataTable)
            cln.InitVars
            Return cln
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
            Return New VIEW_RPT_SpaceUsedByCustomerDataTable
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub InitVars()
            Me.columnCustomer_Index = MyBase.Columns("Customer_Index")
            Me.columnCustomer_Name = MyBase.Columns("Customer_Name")
            Me.columnBin_Count = MyBase.Columns("Bin_Count")
            Me.columnPercent_All_Customer = MyBase.Columns("Percent_All_Customer")
            Me.columnPercent_All_Location = MyBase.Columns("Percent_All_Location")
            Me.columnPercent_Graph = MyBase.Columns("Percent_Graph")
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Private Sub InitClass()
            Me.columnCustomer_Index = New Global.System.Data.DataColumn("Customer_Index", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCustomer_Index)
            Me.columnCustomer_Name = New Global.System.Data.DataColumn("Customer_Name", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnCustomer_Name)
            Me.columnBin_Count = New Global.System.Data.DataColumn("Bin_Count", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnBin_Count)
            Me.columnPercent_All_Customer = New Global.System.Data.DataColumn("Percent_All_Customer", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnPercent_All_Customer)
            Me.columnPercent_All_Location = New Global.System.Data.DataColumn("Percent_All_Location", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnPercent_All_Location)
            Me.columnPercent_Graph = New Global.System.Data.DataColumn("Percent_Graph", GetType(String), Nothing, Global.System.Data.MappingType.Element)
            MyBase.Columns.Add(Me.columnPercent_Graph)
            Me.columnCustomer_Index.AllowDBNull = false
            Me.columnCustomer_Index.MaxLength = 13
            Me.columnCustomer_Name.MaxLength = 100
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function NewVIEW_RPT_SpaceUsedByCustomerRow() As VIEW_RPT_SpaceUsedByCustomerRow
            Return CType(Me.NewRow,VIEW_RPT_SpaceUsedByCustomerRow)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
            Return New VIEW_RPT_SpaceUsedByCustomerRow(builder)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Function GetRowType() As Global.System.Type
            Return GetType(VIEW_RPT_SpaceUsedByCustomerRow)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.VIEW_RPT_SpaceUsedByCustomerRowChangedEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_SpaceUsedByCustomerRowChanged(Me, New VIEW_RPT_SpaceUsedByCustomerRowChangeEvent(CType(e.Row,VIEW_RPT_SpaceUsedByCustomerRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.VIEW_RPT_SpaceUsedByCustomerRowChangingEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_SpaceUsedByCustomerRowChanging(Me, New VIEW_RPT_SpaceUsedByCustomerRowChangeEvent(CType(e.Row,VIEW_RPT_SpaceUsedByCustomerRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.VIEW_RPT_SpaceUsedByCustomerRowDeletedEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_SpaceUsedByCustomerRowDeleted(Me, New VIEW_RPT_SpaceUsedByCustomerRowChangeEvent(CType(e.Row,VIEW_RPT_SpaceUsedByCustomerRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.VIEW_RPT_SpaceUsedByCustomerRowDeletingEvent) Is Nothing) Then
                RaiseEvent VIEW_RPT_SpaceUsedByCustomerRowDeleting(Me, New VIEW_RPT_SpaceUsedByCustomerRowChangeEvent(CType(e.Row,VIEW_RPT_SpaceUsedByCustomerRow), e.Action))
            End If
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub RemoveVIEW_RPT_SpaceUsedByCustomerRow(ByVal row As VIEW_RPT_SpaceUsedByCustomerRow)
            Me.Rows.Remove(row)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence
            Dim ds As dsSpaceUsedByCustomer = New dsSpaceUsedByCustomer
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
            attribute2.FixedValue = "VIEW_RPT_SpaceUsedByCustomerDataTable"
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
    Partial Public Class VIEW_RPT_SpaceUsedByCustomerRow
        Inherits Global.System.Data.DataRow
        
        Private tableVIEW_RPT_SpaceUsedByCustomer As VIEW_RPT_SpaceUsedByCustomerDataTable
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
            MyBase.New(rb)
            Me.tableVIEW_RPT_SpaceUsedByCustomer = CType(Me.Table,VIEW_RPT_SpaceUsedByCustomerDataTable)
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Customer_Index() As String
            Get
                Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_IndexColumn),String)
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_IndexColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Customer_Name() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_NameColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Customer_Name' in table 'VIEW_RPT_SpaceUsedByCustomer' is D"& _ 
                            "BNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_NameColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Bin_Count() As Integer
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Bin_CountColumn),Integer)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Bin_Count' in table 'VIEW_RPT_SpaceUsedByCustomer' is DBNul"& _ 
                            "l.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Bin_CountColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Percent_All_Customer() As Decimal
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_CustomerColumn),Decimal)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Percent_All_Customer' in table 'VIEW_RPT_SpaceUsedByCustome"& _ 
                            "r' is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_CustomerColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Percent_All_Location() As Decimal
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_LocationColumn),Decimal)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Percent_All_Location' in table 'VIEW_RPT_SpaceUsedByCustome"& _ 
                            "r' is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_LocationColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property Percent_Graph() As String
            Get
                Try 
                    Return CType(Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_GraphColumn),String)
                Catch e As Global.System.InvalidCastException
                    Throw New Global.System.Data.StrongTypingException("The value for column 'Percent_Graph' in table 'VIEW_RPT_SpaceUsedByCustomer' is D"& _ 
                            "BNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_GraphColumn) = value
            End Set
        End Property
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsCustomer_NameNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_NameColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetCustomer_NameNull()
            Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Customer_NameColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsBin_CountNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_SpaceUsedByCustomer.Bin_CountColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetBin_CountNull()
            Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Bin_CountColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsPercent_All_CustomerNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_CustomerColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetPercent_All_CustomerNull()
            Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_CustomerColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsPercent_All_LocationNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_LocationColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetPercent_All_LocationNull()
            Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_All_LocationColumn) = Global.System.Convert.DBNull
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Function IsPercent_GraphNull() As Boolean
            Return Me.IsNull(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_GraphColumn)
        End Function
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub SetPercent_GraphNull()
            Me(Me.tableVIEW_RPT_SpaceUsedByCustomer.Percent_GraphColumn) = Global.System.Convert.DBNull
        End Sub
    End Class
    
    '''<summary>
    '''Row event argument class
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")>  _
    Public Class VIEW_RPT_SpaceUsedByCustomerRowChangeEvent
        Inherits Global.System.EventArgs
        
        Private eventRow As VIEW_RPT_SpaceUsedByCustomerRow
        
        Private eventAction As Global.System.Data.DataRowAction
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub New(ByVal row As VIEW_RPT_SpaceUsedByCustomerRow, ByVal action As Global.System.Data.DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public ReadOnly Property Row() As VIEW_RPT_SpaceUsedByCustomerRow
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
