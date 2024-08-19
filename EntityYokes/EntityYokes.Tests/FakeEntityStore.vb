Friend Class FakeEntityStore(Of TIdentifier)
    Implements IEntityStore(Of TIdentifier)
    Private ReadOnly entityTypes As New Dictionary(Of TIdentifier, String)
    'Private ReadOnly entityFlags As New Dictionary(Of TIdentifier, HashSet(Of String))
    Private ReadOnly nextEntityIdentifier As Func(Of TIdentifier)

    Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextEntityIdentifier = nextIdentifier
    End Sub

    Public Sub DestroyEntity(identifier As TIdentifier) Implements IEntityStore(Of TIdentifier).DestroyEntity
        entityTypes.Remove(identifier)
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
        Return False
    End Function
End Class
