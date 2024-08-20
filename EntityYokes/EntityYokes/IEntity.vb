Public Interface IEntity(Of TIdentifier)
    ReadOnly Property Identifier As TIdentifier
    ReadOnly Property EntityType As String
    Property Flag(flagType As String) As Boolean
    ReadOnly Property Flags As IEnumerable(Of String)
    ReadOnly Property Metadata(metadataType As String) As String
    ReadOnly Property Metadatas As IEnumerable(Of String)
    ReadOnly Property Counter(counterType As String) As Integer?
    ReadOnly Property Counters As IEnumerable(Of String)
    ReadOnly Property Statistic(statisticType As String) As Double?
    ReadOnly Property Statistics As IEnumerable(Of String)
End Interface
