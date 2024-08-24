Friend Class Yoke(Of TEntityIdentifier, TYokeIdentifier)
    Implements IYoke

    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Private ReadOnly identifier As TYokeIdentifier

    Sub New(
           store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier),
           identifier As TYokeIdentifier)
        Me.store = store
        Me.identifier = identifier
    End Sub

    Public ReadOnly Property YokeType As String Implements IYoke.YokeType
        Get
            Return store.ReadYokeType(identifier)
        End Get
    End Property

    Public ReadOnly Property FromEntity As IEntity Implements IYoke.FromEntity
        Get
            Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, store.ReadYokeFromIdentifier(identifier))
        End Get
    End Property

    Public ReadOnly Property ToEntity As IEntity Implements IYoke.ToEntity
        Get
            Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, store.ReadYokeToIdentifier(identifier))
        End Get
    End Property

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IYoke.Flags
        Get
            Return store.ListYokeFlags(identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IYoke.Flag
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
            Return store.ReadYokeMetadata(identifier, metadataType)
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                store.WriteYokeMetadata(identifier, metadataType, value)
            Else
                store.ClearYokeMetadata(identifier, metadataType)
            End If
        End Set
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IThingie.Metadatas
        Get
            Return store.ListYokeMetadatas(identifier)
        End Get
    End Property

    Public Property Counter(counterType As String) As Integer? Implements IThingie.Counter
        Get
            Return store.ReadYokeCounter(identifier, counterType)
        End Get
        Set(value As Integer?)
            If value IsNot Nothing Then
                store.WriteYokeCounter(identifier, counterType, value.Value)
            Else
                store.ClearYokeCounter(identifier, counterType)
            End If
        End Set
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IThingie.Counters
        Get
            Return store.ListYokeCounters(identifier)
        End Get
    End Property

    Public Property Statistic(statisticType As String) As Double? Implements IThingie.Statistic
        Get
            Return store.ReadYokeStatistic(identifier, statisticType)
        End Get
        Set(value As Double?)
            If value IsNot Nothing Then
                store.WriteYokeStatistic(identifier, statisticType, value.Value)
            Else
                store.ClearYokeStatistic(identifier, statisticType)
            End If
        End Set
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IThingie.Statistics
        Get
            Return store.ListYokeStatistics(identifier)
        End Get
    End Property

    Public Sub Destroy() Implements IYoke.Destroy
        If store.ListYokeCounters(identifier).Any OrElse
            store.ListYokeFlags(identifier).Any OrElse
            store.ListYokeMetadatas(identifier).Any OrElse
            store.ListYokeStatistics(identifier).Any Then
            Throw New InvalidOperationException($"Yoke with identifier `{identifier}` is not empty.")
        End If
        store.DestroyYoke(identifier)
    End Sub
End Class
