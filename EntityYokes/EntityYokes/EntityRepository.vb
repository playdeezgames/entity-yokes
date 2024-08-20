Public Class EntityRepository(Of TEntityIdentifier)
    Implements IEntityRepository(Of TEntityIdentifier)
    Private ReadOnly store As IEntityStore(Of TEntityIdentifier)

    Public Sub New(store As IEntityStore(Of TEntityIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TEntityIdentifier)) Implements IEntityRepository(Of TEntityIdentifier).AllEntities
        Get
            Return store.ListEntities.Select(Function(x) New Entity(Of TEntityIdentifier)(store, x))
        End Get
    End Property

    Public Sub DestroyEntity(entity As IEntity(Of TEntityIdentifier)) Implements IEntityRepository(Of TEntityIdentifier).DestroyEntity
        If store.ListEntityCounters(entity.Identifier).Any OrElse
            store.ListEntityFlags(entity.Identifier).Any OrElse
            store.ListEntityMetadatas(entity.Identifier).Any OrElse
            store.ListEntityStatistics(entity.Identifier).Any Then
            Throw New InvalidOperationException($"Entity with identifier `{entity.Identifier}` is not empty.")
        End If
        store.DestroyEntity(entity.Identifier)
    End Sub

    Public Function CreateEntity(entityType As String) As IEntity(Of TEntityIdentifier) Implements IEntityRepository(Of TEntityIdentifier).CreateEntity
        If entityType Is Nothing Then
            Throw New ArgumentNullException(NameOf(entityType))
        End If
        Dim identifier = store.CreateEntity(entityType)
        Return New Entity(Of TEntityIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntity(identifier As TEntityIdentifier) As IEntity(Of TEntityIdentifier) Implements IEntityRepository(Of TEntityIdentifier).RetrieveEntity
        If Not store.DoesEntityExist(identifier) Then
            Return Nothing
        End If
        Return New Entity(Of TEntityIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity(Of TEntityIdentifier)) Implements IEntityRepository(Of TEntityIdentifier).RetrieveEntitiesOfType
        Return store.ListEntitiesOfType(entityType).Select(Function(x) New Entity(Of TEntityIdentifier)(store, x))
    End Function
End Class
