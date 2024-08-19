Public Interface IEntityStore(Of TIdentifier)
    Function GetNextIdentifier() As TIdentifier
End Interface
