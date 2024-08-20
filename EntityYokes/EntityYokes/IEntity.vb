Public Interface IEntity(Of TIdentifier)
    ReadOnly Property Identifier As TIdentifier
    ReadOnly Property EntityType As String
    Property Flag(flagType As String) As Boolean
    ReadOnly Property Flags As IEnumerable(Of String)
    ReadOnly Property Metadatas As IEnumerable(Of String)
    ReadOnly Property Counters As IEnumerable(Of String)
    ReadOnly Property Statistics As IEnumerable(Of String)
End Interface
