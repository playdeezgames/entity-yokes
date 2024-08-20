Public Interface IEntityStore(Of TIdentifier)
    Sub DestroyEntity(identifier As TIdentifier)
    Sub SetEntityFlag(identifier As TIdentifier, flagType As String)
    Sub ClearEntityFlag(identifier As TIdentifier, flagType As String)
    Function CreateEntity(entityType As String) As TIdentifier
    Function ListEntities() As IEnumerable(Of TIdentifier)
    Function ReadEntityType(identifier As TIdentifier) As String
    Function DoesEntityExist(identifier As TIdentifier) As Boolean
    Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TIdentifier)
    Function CheckEntityHasFlag(identifier As TIdentifier, flagType As String) As Boolean
    Function ListEntityFlags(identifier As TIdentifier) As IEnumerable(Of String)
    Function ListEntityMetadatas(identifier As TIdentifier) As IEnumerable(Of String)
End Interface
