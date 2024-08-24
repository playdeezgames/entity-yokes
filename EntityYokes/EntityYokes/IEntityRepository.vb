Public Interface IEntityRepository
    Function CreateEntity(entityType As String) As IEntity
    ReadOnly Property AllEntities As IEnumerable(Of IEntity)
    Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity)
End Interface
