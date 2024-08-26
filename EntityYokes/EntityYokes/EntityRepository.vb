Public Class EntityRepository(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntityRepository
    Private ReadOnly store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)

    Public Sub New(store As IEntityStore(Of TEntityIdentifier, TYokeIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property Entities As IEnumerable(Of IEntity) Implements IEntityRepository.Entities
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

    Public Function EntitiesOfType(entityType As String) As IEnumerable(Of IEntity) Implements IEntityRepository.EntitiesOfType
        Return store.ListEntitiesOfType(entityType).Select(Function(x) New Entity(Of TEntityIdentifier, TYokeIdentifier)(store, x))
    End Function
End Class
