Public Interface IEntity(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property Identifier As TEntityIdentifier
    ReadOnly Property EntityType As String
    Property Flag(flagType As String) As Boolean
    ReadOnly Property Flags As IEnumerable(Of String)
    Property Metadata(metadataType As String) As String
    ReadOnly Property Metadatas As IEnumerable(Of String)
    Property Counter(counterType As String) As Integer?
    ReadOnly Property Counters As IEnumerable(Of String)
    Property Statistic(statisticType As String) As Double?
    ReadOnly Property Statistics As IEnumerable(Of String)
    Function CreateYoke(yokeType As String, yokedEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)) As IYoke(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property YokesFrom As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
    ReadOnly Property YokesTo As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
End Interface
