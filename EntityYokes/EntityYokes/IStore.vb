Public Interface IStore(Of TIdentifier)
    Function CreateEntity(entityType As String) As IEntity(Of TIdentifier)
End Interface
