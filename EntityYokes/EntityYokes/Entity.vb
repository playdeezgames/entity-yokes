Friend Class Entity(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntity

    Friend Sub New(store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier), identifier As TEntityIdentifier)
        Me.store = store
        Me.Identifier = identifier
    End Sub

    Public Function CreateYoke(yokeType As String, yokedEntity As IEntity) As IYoke Implements IEntity.CreateYoke
        Return New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, store.CreateYoke(yokeType, Me.Identifier, CType(yokedEntity, Entity(Of TEntityIdentifier, TYokeIdentifier)).Identifier))
    End Function

    Public Sub Destroy() Implements IEntity.Destroy
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
    Private ReadOnly Identifier As TEntityIdentifier

    Public ReadOnly Property EntityType As String Implements IEntity.EntityType
        Get
            If Not store.DoesEntityExist(Identifier) Then
                Throw New NullReferenceException($"Entity with identifier `{Identifier}` does not exist")
            End If
            Return store.ReadEntityType(Identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IEntity.Flag
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

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IEntity.Flags
        Get
            Return store.ListEntityFlags(Identifier)
        End Get
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IEntity.Metadatas
        Get
            Return store.ListEntityMetadatas(Identifier)
        End Get
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IEntity.Counters
        Get
            Return store.ListEntityCounters(Identifier)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IEntity.Statistics
        Get
            Return store.ListEntityStatistics(Identifier)
        End Get
    End Property

    Public Property Metadata(metadataType As String) As String Implements IEntity.Metadata
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

    Public Property Counter(counterType As String) As Integer? Implements IEntity.Counter
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

    Public Property Statistic(statisticType As String) As Double? Implements IEntity.Statistic
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

    Public ReadOnly Property YokesFrom As IEnumerable(Of IYoke) Implements IEntity.YokesFrom
        Get
            Return store.ListEntityYokesFrom(Identifier).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public ReadOnly Property YokesTo As IEnumerable(Of IYoke) Implements IEntity.YokesTo
        Get
            Return store.ListEntityYokesTo(Identifier).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public ReadOnly Property YokesOfTypeFrom(yokeType As String) As IEnumerable(Of IYoke) Implements IEntity.YokesOfTypeFrom
        Get
            Return store.ListEntityYokesOfTypeFrom(Identifier, yokeType).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public ReadOnly Property YokesOfTypeTo(yokeType As String) As IEnumerable(Of IYoke) Implements IEntity.YokesOfTypeTo
        Get
            Return store.ListEntityYokesOfTypeTo(Identifier, yokeType).Select(Function(x) New Yoke(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim other As IEntity = CType(obj, IEntity)
        If other Is Nothing Then
            Return False
        End If
        Return CType(other, Entity(Of TEntityIdentifier, TYokeIdentifier)).Identifier.Equals(Identifier)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Identifier.GetHashCode
    End Function
End Class
