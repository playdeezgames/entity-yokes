Public Interface IEntity(Of TIdentifier)
    ReadOnly Property Identifier As TIdentifier
    ReadOnly Property EntityType As String
    Property Flags(flagType As String) As Boolean
    ReadOnly Property AllFlags As IEnumerable(Of String)
End Interface
