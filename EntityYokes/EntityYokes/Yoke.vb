Friend Class Yoke(Of TEntityIdentifier, TYokeIdentifier)
    Implements IYoke(Of TEntityIdentifier, TYokeIdentifier)

    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Private ReadOnly identifier As TYokeIdentifier

    Sub New(
           store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier),
           identifier As TYokeIdentifier)
        Me.store = store
        Me.identifier = identifier
    End Sub

    Public ReadOnly Property YokeType As String Implements IYoke(Of TEntityIdentifier, TYokeIdentifier).YokeType
        Get
            Return store.ReadYokeType(identifier)
        End Get
    End Property

    Public ReadOnly Property FromEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier) Implements IYoke(Of TEntityIdentifier, TYokeIdentifier).FromEntity
        Get
            Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, store.ReadYokeFromIdentifier(identifier))
        End Get
    End Property

    Public ReadOnly Property ToEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier) Implements IYoke(Of TEntityIdentifier, TYokeIdentifier).ToEntity
        Get
            Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, store.ReadYokeToIdentifier(identifier))
        End Get
    End Property

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IYoke(Of TEntityIdentifier, TYokeIdentifier).Flags
        Get
            Return store.ListYokeFlags(identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IYoke(Of TEntityIdentifier, TYokeIdentifier).Flag
        Get
            Return store.CheckYokeHasFlag(identifier, flagType)
        End Get
        Set(value As Boolean)
            If value Then
                store.SetYokeFlag(identifier, flagType)
            Else
                store.ClearYokeFlag(identifier, flagType)
            End If
        End Set
    End Property

    Public Property Metadata(metadataType As String) As String Implements IThingie.Metadata
        Get
            Return Nothing
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IThingie.Metadatas
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public Property Counter(counterType As String) As Integer? Implements IThingie.Counter
        Get
            Return Nothing
        End Get
        Set(value As Integer?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IThingie.Counters
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public Property Statistic(statisticType As String) As Double? Implements IThingie.Statistic
        Get
            Return Nothing
        End Get
        Set(value As Double?)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IThingie.Statistics
        Get
            Return Array.Empty(Of String)
        End Get
    End Property
End Class
