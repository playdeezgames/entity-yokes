Public Interface IStore(Of TIdentifier)
    Function CreateEntity(entityType As String) As IEntity(Of TIdentifier)
    ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TIdentifier))
    Function RetrieveEntity(identifier As TIdentifier) As IEntity(Of TIdentifier)
End Interface
