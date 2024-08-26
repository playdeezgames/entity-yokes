Public Interface IEntityRepository
    Function CreateEntity(entityType As String) As IEntity
    ReadOnly Property Entities As IEnumerable(Of IEntity)
    Function EntitiesOfType(entityType As String) As IEnumerable(Of IEntity)
End Interface
