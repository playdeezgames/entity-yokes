﻿Public Interface IEntityStore(Of TIdentifier)
    Function CreateEntity(entityType As String) As TIdentifier
    Function ListEntities() As IEnumerable(Of TIdentifier)
    Function ReadEntityType(identifier As TIdentifier) As String
    Function EntityExists(identifier As TIdentifier) As Boolean
End Interface
