Public Class EntityRepository(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntityRepository
    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)

    Public Sub New(store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity) Implements IEntityRepository.AllEntities
        Get
            Return store.ListEntities.Select(Function(x) New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, x))
        End Get
    End Property

    Public Function CreateEntity(entityType As String) As IEntity Implements IEntityRepository.CreateEntity
        If entityType Is Nothing Then
            Throw New ArgumentNullException(NameOf(entityType))
        End If
        Dim identifier = store.CreateEntity(entityType)
        Return New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity) Implements IEntityRepository.RetrieveEntitiesOfType
        Return store.ListEntitiesOfType(entityType).Select(Function(x) New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, x))
    End Function
End Class
