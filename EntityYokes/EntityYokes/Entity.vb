Friend Class Entity(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntity(Of TEntityIdentifier, TYokeIdentifier)

    Friend Sub New(store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier), identifier As TEntityIdentifier)
        Me.store = store
        Me.Identifier = identifier
    End Sub

    Public Function CreateYoke(yokeType As String, yokedEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)) As IYoke(Of TEntityIdentifier, TYokeIdentifier) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).CreateYoke
        Return New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, store.CreateYoke(yokeType, Me.Identifier, yokedEntity.Identifier))
    End Function

    Public Sub Destroy() Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Destroy
        If store.ListEntityCounters(Identifier).Any OrElse
            store.ListEntityFlags(Identifier).Any OrElse
            store.ListEntityMetadatas(Identifier).Any OrElse
            store.ListEntityStatistics(Identifier).Any OrElse
            store.ListEntityYokesFrom(Identifier).Any OrElse
            store.ListEntityYokesTo(Identifier).Any Then
            Throw New InvalidOperationException($"Entity with identifier `{Identifier}` is not empty.")
        End If
        store.DestroyEntity(Identifier)
    End Sub

    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Public ReadOnly Property Identifier As TEntityIdentifier Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Identifier

    Public ReadOnly Property EntityType As String Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).EntityType
        Get
            If Not store.DoesEntityExist(Identifier) Then
                Throw New NullReferenceException($"Entity with identifier `{Identifier}` does not exist")
            End If
            Return store.ReadEntityType(Identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Flag
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

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Flags
        Get
            Return store.ListEntityFlags(Identifier)
        End Get
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Metadatas
        Get
            Return store.ListEntityMetadatas(Identifier)
        End Get
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Counters
        Get
            Return store.ListEntityCounters(Identifier)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Statistics
        Get
            Return store.ListEntityStatistics(Identifier)
        End Get
    End Property

    Public Property Metadata(metadataType As String) As String Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Metadata
        Get
            Return store.ReadEntityMetadata(Identifier, metadataType)
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                store.WriteEntityMetadata(Identifier, metadataType, value)
            Else
                store.ClearEntityMetadata(Identifier, metadataType)
            End If
        End Set
    End Property

    Public Property Counter(counterType As String) As Integer? Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Counter
        Get
            Return store.ReadEntityCounter(Identifier, counterType)
        End Get
        Set(value As Integer?)
            If value IsNot Nothing Then
                store.WriteEntityCounter(Identifier, counterType, value.Value)
            Else
                store.ClearEntityCounter(Identifier, counterType)
            End If
        End Set
    End Property

    Public Property Statistic(statisticType As String) As Double? Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).Statistic
        Get
            Return store.ReadEntityStatistic(Identifier, statisticType)
        End Get
        Set(value As Double?)
            If value IsNot Nothing Then
                store.WriteEntityStatistic(Identifier, statisticType, value.Value)
            Else
                store.ClearEntityStatistic(Identifier, statisticType)
            End If
        End Set
    End Property

    Public ReadOnly Property YokesFrom As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier)) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).YokesFrom
        Get
            Return store.ListEntityYokesFrom(Identifier).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public ReadOnly Property YokesTo As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier)) Implements IEntity(Of TEntityIdentifier, TYokeIdentifier).YokesTo
        Get
            Return store.ListEntityYokesTo(Identifier).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property
End Class
