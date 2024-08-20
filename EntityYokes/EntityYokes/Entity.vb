Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Friend Sub New(store As IEntityStore(Of TIdentifier), identifier As TIdentifier)
        Me.store = store
        Me.Identifier = identifier
    End Sub

    Private ReadOnly store As IEntityStore(Of TIdentifier)
    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier

    Public ReadOnly Property EntityType As String Implements IEntity(Of TIdentifier).EntityType
        Get
            Return store.ReadEntityType(Identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IEntity(Of TIdentifier).Flag
        Get
            Return store.CheckEntityHasFlag(Identifier, flagType)
        End Get
        Set(value As Boolean)
            If value Then
                store.SetEntityFlag(Identifier, flagType)
            Else
                store.ClearEntityFlag(Identifier, flagType)
            End If
        End Set
    End Property

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IEntity(Of TIdentifier).Flags
        Get
            Return store.ListEntityFlags(Identifier)
        End Get
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IEntity(Of TIdentifier).Metadatas
        Get
            Return store.ListEntityMetadatas(Identifier)
        End Get
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IEntity(Of TIdentifier).Counters
        Get
            Return store.ListEntityCounters(Identifier)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IEntity(Of TIdentifier).Statistics
        Get
            Return store.ListEntityStatistics(Identifier)
        End Get
    End Property

    Public ReadOnly Property Metadata(metadataType As String) As String Implements IEntity(Of TIdentifier).Metadata
        Get
            Return store.ReadEntityMetadata(Identifier, metadataType)
        End Get
    End Property

    Public ReadOnly Property Counter(counterType As String) As Integer? Implements IEntity(Of TIdentifier).Counter
        Get
            Return store.ReadEntityCounter(Identifier, counterType)
        End Get
    End Property

    Public ReadOnly Property Statistic(statisticType As String) As Double? Implements IEntity(Of TIdentifier).Statistic
        Get
            Return store.ReadEntityStatistic(Identifier, statisticType)
        End Get
    End Property
End Class
