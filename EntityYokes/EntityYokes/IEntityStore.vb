Public Interface IEntityStore(Of TIdentifier)
    Sub DestroyEntity(identifier As TIdentifier)
    Sub SetEntityFlag(identifier As TIdentifier, flagType As String)
    Sub ClearEntityFlag(identifier As TIdentifier, flagType As String)
    Sub WriteEntityMetadata(identifier As TIdentifier, metadataType As String, value As String)
    Sub WriteEntityCounter(identifier As TIdentifier, counterType As String, value As Integer)
    Sub WriteEntityStatistic(identifier As TIdentifier, statisticType As String, value As Double)
    Sub ClearEntityMetadata(identifier As TIdentifier, metadataType As String)
    Sub ClearEntityCounter(identifier As TIdentifier, counterType As String)
    Sub ClearEntityStatistic(identifier As TIdentifier, statisticType As String)
    Function CreateEntity(entityType As String) As TIdentifier
    Function ListEntities() As IEnumerable(Of TIdentifier)
    Function ReadEntityType(identifier As TIdentifier) As String
    Function DoesEntityExist(identifier As TIdentifier) As Boolean
    Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TIdentifier)
    Function CheckEntityHasFlag(identifier As TIdentifier, flagType As String) As Boolean
    Function ListEntityFlags(identifier As TIdentifier) As IEnumerable(Of String)
    Function ListEntityMetadatas(identifier As TIdentifier) As IEnumerable(Of String)
    Function ListEntityCounters(identifier As TIdentifier) As IEnumerable(Of String)
    Function ListEntityStatistics(identifier As TIdentifier) As IEnumerable(Of String)
    Function ReadEntityMetadata(identifier As TIdentifier, metadataType As String) As String
    Function ReadEntityCounter(identifier As TIdentifier, counterType As String) As Integer?
    Function ReadEntityStatistic(identifier As TIdentifier, statisticType As String) As Double?
End Interface
