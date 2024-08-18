Public Interface IStore(Of TIdentifier)
    Function CreateEntity() As IEntity(Of TIdentifier)
End Interface
