Public Interface IEntityRepository(Of TEntityIdentifier, TYokeIdentifier)
    Function CreateEntity(entityType As String) As IEntity(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TEntityIdentifier, TYokeIdentifier))
    Function RetrieveEntity(identifier As TEntityIdentifier) As IEntity(Of TEntityIdentifier, TYokeIdentifier)
    Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity(Of TEntityIdentifier, TYokeIdentifier))
    Sub DestroyEntity(entity As IEntity(Of TEntityIdentifier, TYokeIdentifier))
End Interface
