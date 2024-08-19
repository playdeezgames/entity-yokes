Public Interface IEntityStore(Of TIdentifier)
    Sub DestroyEntity(identifier As TIdentifier)
    Function CreateEntity(entityType As String) As TIdentifier
    Function ListEntities() As IEnumerable(Of TIdentifier)
    Function ReadEntityType(identifier As TIdentifier) As String
    Function DoesEntityExist(identifier As TIdentifier) As Boolean
    Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TIdentifier)
End Interface
