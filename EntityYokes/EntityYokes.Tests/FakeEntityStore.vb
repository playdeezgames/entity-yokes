Friend Class FakeEntityStore(Of TIdentifier)
    Implements IEntityStore(Of TIdentifier)
    Private ReadOnly entityTypes As New Dictionary(Of TIdentifier, String)
    Private ReadOnly entityFlags As New Dictionary(Of TIdentifier, HashSet(Of String))
    Private ReadOnly nextEntityIdentifier As Func(Of TIdentifier)

    Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextEntityIdentifier = nextIdentifier
    End Sub

    Public Sub DestroyEntity(identifier As TIdentifier) Implements IEntityStore(Of TIdentifier).DestroyEntity
        Dim value As HashSet(Of String) = Nothing
        If entityFlags.TryGetValue(identifier, value) AndAlso value.Count <> 0 Then
            Throw New InvalidOperationException($"Entity with identifier `{identifier}` has flags.")
        End If
        entityTypes.Remove(identifier)
    End Sub

    Public Sub SetEntityFlag(identifier As TIdentifier, flagType As String) Implements IEntityStore(Of TIdentifier).SetEntityFlag
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            flags = New HashSet(Of String)
            entityFlags(identifier) = flags
        End If
        flags.Add(flagType)
    End Sub

    Public Sub ClearEntityFlag(identifier As TIdentifier, flagType As String) Implements IEntityStore(Of TIdentifier).ClearEntityFlag
        Dim flags As HashSet(Of String) = Nothing
        If entityFlags.TryGetValue(identifier, flags) Then
            flags.Remove(flagType)
        End If
    End Sub

    Public Function CreateEntity(entityType As String) As TIdentifier Implements IEntityStore(Of TIdentifier).CreateEntity
        Dim identifier = nextEntityIdentifier()
        entityTypes(identifier) = entityType
        Return identifier
    End Function

    Public Function ListEntities() As IEnumerable(Of TIdentifier) Implements IEntityStore(Of TIdentifier).ListEntities
        Return entityTypes.Keys
    End Function

    Public Function ReadEntityType(identifier As TIdentifier) As String Implements IEntityStore(Of TIdentifier).ReadEntityType
        Dim entityType As String = Nothing
        If Not entityTypes.TryGetValue(identifier, entityType) Then
            Throw New KeyNotFoundException($"Did not find Entity with Identifier of `{identifier}`")
        End If
        Return entityType
    End Function

    Public Function DoesEntityExist(identifier As TIdentifier) As Boolean Implements IEntityStore(Of TIdentifier).DoesEntityExist
        Return entityTypes.ContainsKey(identifier)
    End Function

    Public Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TIdentifier) Implements IEntityStore(Of TIdentifier).ListEntitiesOfType
        Return entityTypes.Where(Function(x) x.Value = entityType).Select(Function(x) x.Key)
    End Function

    Public Function CheckEntityHasFlag(identifier As TIdentifier, flagType As String) As Boolean Implements IEntityStore(Of TIdentifier).CheckEntityHasFlag
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            Return False
        End If
        Return flags.Contains(flagType)
    End Function

    Public Function ListEntityFlags(identifier As TIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TIdentifier).ListEntityFlags
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            Return Array.Empty(Of String)
        End If
        Return flags
    End Function

    Public Function ListEntityMetadatas(identifier As TIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TIdentifier).ListEntityMetadatas
        Return Array.Empty(Of String)
    End Function

    Public Function ListEntityCounters(identifier As TIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TIdentifier).ListEntityCounters
        Return Array.Empty(Of String)
    End Function

    Public Function ListEntityStatistics(identifier As TIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TIdentifier).ListEntityStatistics
        Return Array.Empty(Of String)
    End Function

    Public Function ReadEntityMetadata(identifier As TIdentifier, metadataType As String) As String Implements IEntityStore(Of TIdentifier).ReadEntityMetadata
        Return Nothing
    End Function

    Public Function ReadEntityCounter(identifier As TIdentifier, counterType As String) As Integer? Implements IEntityStore(Of TIdentifier).ReadEntityCounter
        Return Nothing
    End Function

    Public Function ReadEntityStatistic(identfier As TIdentifier, statisticType As String) As Double? Implements IEntityStore(Of TIdentifier).ReadEntityStatistic
        Return Nothing
    End Function
End Class
