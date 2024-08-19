Public Interface IEntity(Of TIdentifier)
    ReadOnly Property Identifier As TIdentifier
    ReadOnly Property EntityType As String
    ReadOnly Property Flags(flagType As String) As Boolean
End Interface
