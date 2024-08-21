Public Interface IThingie
    ReadOnly Property Flags As IEnumerable(Of String)
    Property Flag(flagType As String) As Boolean
    Property Metadata(metadataType As String) As String
    ReadOnly Property Metadatas As IEnumerable(Of String)
    Property Counter(counterType As String) As Integer?
    ReadOnly Property Counters As IEnumerable(Of String)
    Property Statistic(statisticType As String) As Double?
    ReadOnly Property Statistics As IEnumerable(Of String)
End Interface
