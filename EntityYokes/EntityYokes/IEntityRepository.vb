Public Interface IEntityRepository(Of TEntityIdentifier)
    Function CreateEntity(entityType As String) As IEntity(Of TEntityIdentifier)
    ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TEntityIdentifier))
    Function RetrieveEntity(identifier As TEntityIdentifier) As IEntity(Of TEntityIdentifier)
    Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity(Of TEntityIdentifier))
    Sub DestroyEntity(entity As IEntity(Of TEntityIdentifier))
End Interface
