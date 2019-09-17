Public Class ColArgs

    Public Enum ColArgsType
        [String]
        [Date]
        [Integer]
        [Decimal]
        [IntegerPositive]
        [IntegerPositiveNotZero]
        [DecimalPositive]
        [DecimalPositiveNotZero]
    End Enum

    Private _Column_Index As Integer
    Public Property Column_Index() As Integer
        Get
            Return Me._Column_Index
        End Get
        Set(ByVal value As Integer)
            Me._Column_Index = value
        End Set
    End Property

    Private _Column_Name As String
    Public Property Column_Name() As String
        Get
            Return Me._Column_Name
        End Get
        Set(ByVal value As String)
            Me._Column_Name = value
        End Set
    End Property

    Private _Column_Name_Alias As String
    Public Property Column_Name_Alias() As String
        Get
            Return Me._Column_Name_Alias
        End Get
        Set(ByVal value As String)
            Me._Column_Name_Alias = value
        End Set
    End Property

    Private _ColumnType As ColArgsType
    Public Property ColumnType() As ColArgsType
        Get
            Return Me._ColumnType
        End Get
        Set(ByVal value As ColArgsType)
            Me._ColumnType = value
        End Set
    End Property

    Private _IsRequireData As Boolean
    Public Property IsRequireData() As Boolean
        Get
            Return Me._IsRequireData
        End Get
        Set(ByVal value As Boolean)
            Me._IsRequireData = value
        End Set
    End Property

    Private _IsRequireColumn As Boolean
    Public Property IsRequireColumn() As Boolean
        Get
            Return Me._IsRequireColumn
        End Get
        Set(ByVal value As Boolean)
            Me._IsRequireColumn = value
        End Set
    End Property

    Public Sub New(ByVal Column_Index As Integer, ByVal Column_Name As String, ByVal Column_Name_Alias As String, ByVal ColumnType As ColArgsType, Optional ByVal IsRequireData As Boolean = True, Optional ByVal IsRequireColumn As Boolean = True)
        Me.Column_Index = Column_Index
        Me.Column_Name = Column_Name
        Me.Column_Name_Alias = Column_Name_Alias
        Me.ColumnType = ColumnType
        Me.IsRequireData = IsRequireData
        Me.IsRequireColumn = IsRequireColumn
    End Sub

End Class
