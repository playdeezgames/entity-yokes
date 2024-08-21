Public Interface IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Sub DestroyEntity(identifier As TEntityIdentifier)
    Sub SetEntityFlag(identifier As TEntityIdentifier, flagType As String)
    Sub ClearEntityFlag(identifier As TEntityIdentifier, flagType As String)
    Sub WriteEntityMetadata(identifier As TEntityIdentifier, metadataType As String, value As String)
    Sub WriteEntityCounter(identifier As TEntityIdentifier, counterType As String, value As Integer)
    Sub WriteEntityStatistic(identifier As TEntityIdentifier, statisticType As String, value As Double)
    Sub ClearEntityMetadata(identifier As TEntityIdentifier, metadataType As String)
    Sub ClearEntityCounter(identifier As TEntityIdentifier, counterType As String)
    Sub ClearEntityStatistic(identifier As TEntityIdentifier, statisticType As String)
    Function CreateEntity(entityType As String) As TEntityIdentifier
    Function CreateYoke(yokeType As String, fromIdentifier As TEntityIdentifier, toIdentifier As TEntityIdentifier) As TYokeIdentifier
    Function ListEntities() As IEnumerable(Of TEntityIdentifier)
    Function ReadEntityType(identifier As TEntityIdentifier) As String
    Function DoesEntityExist(identifier As TEntityIdentifier) As Boolean
    Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TEntityIdentifier)
    Function CheckEntityHasFlag(identifier As TEntityIdentifier, flagType As String) As Boolean
    Function ListEntityFlags(identifier As TEntityIdentifier) As IEnumerable(Of String)
    Function ListEntityMetadatas(identifier As TEntityIdentifier) As IEnumerable(Of String)
    Function ListEntityCounters(identifier As TEntityIdentifier) As IEnumerable(Of String)
    Function ListEntityStatistics(identifier As TEntityIdentifier) As IEnumerable(Of String)
    Function ReadEntityMetadata(identifier As TEntityIdentifier, metadataType As String) As String
    Function ReadEntityCounter(identifier As TEntityIdentifier, counterType As String) As Integer?
    Function ReadEntityStatistic(identifier As TEntityIdentifier, statisticType As String) As Double?
    Function ReadEntityYokesFrom(identifier As TEntityIdentifier, yokeType As String) As IEnumerable(Of TYokeIdentifier)
    Function ReadEntityYokesTo(identifier As TEntityIdentifier, yokeType As String) As IEnumerable(Of TYokeIdentifier)
    Function ReadYoke(identifier As TYokeIdentifier) As (YokeType As String, FromIdentifier As TEntityIdentifier, ToIdentifier As TEntityIdentifier)?
End Interface
