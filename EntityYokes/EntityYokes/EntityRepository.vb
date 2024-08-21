Public Class EntityRepository(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier)
    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)

    Public Sub New(store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TEntityIdentifier, TYokeIdentifier)) Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier).AllEntities
        Get
            Return store.ListEntities.Select(Function(x) New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public Sub DestroyEntity(entity As IEntity(Of TEntityIdentifier, TYokeIdentifier)) Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier).DestroyEntity
        If store.ListEntityCounters(entity.Identifier).Any OrElse
            store.ListEntityFlags(entity.Identifier).Any OrElse
            store.ListEntityMetadatas(entity.Identifier).Any OrElse
            store.ListEntityStatistics(entity.Identifier).Any Then
            Throw New InvalidOperationException($"Entity with identifier `{entity.Identifier}` is not empty.")
        End If
        store.DestroyEntity(entity.Identifier)
    End Sub

    Public Function CreateEntity(entityType As String) As IEntity(Of TEntityIdentifier, TYokeIdentifier) Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier).CreateEntity
        If entityType Is Nothing Then
            Throw New ArgumentNullException(NameOf(entityType))
        End If
        Dim identifier = store.CreateEntity(entityType)
        Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntity(identifier As TEntityIdentifier) As IEntity(Of TEntityIdentifier, TYokeIdentifier) Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier).RetrieveEntity
        If Not store.DoesEntityExist(identifier) Then
            Return Nothing
        End If
        Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity(Of TEntityIdentifier, TYokeIdentifier)) Implements IEntityRepository(Of TEntityIdentifier, TYokeIdentifier).RetrieveEntitiesOfType
        Return store.ListEntitiesOfType(entityType).Select(Function(x) New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, x))
    End Function
End Class
