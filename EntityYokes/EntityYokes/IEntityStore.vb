Public Interface IEntityStore(Of TIdentifier)
    Function GetNextIdentifier() As TIdentifier
    Function CreateEntity(entityType As String) As TIdentifier
    Function ListEntities() As IEnumerable(Of TIdentifier)
    Function ReadEntityType(identifier As TIdentifier) As String
End Interface
