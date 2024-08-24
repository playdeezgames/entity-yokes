﻿Public Interface IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Sub DestroyEntity(identifier As TEntityIdentifier)
    Sub SetEntityFlag(identifier As TEntityIdentifier, flagType As String)
    Sub ClearEntityFlag(identifier As TEntityIdentifier, flagType As String)
    Sub WriteEntityMetadata(identifier As TEntityIdentifier, metadataType As String, value As String)
    Sub WriteEntityCounter(identifier As TEntityIdentifier, counterType As String, value As Integer)
    Sub WriteEntityStatistic(identifier As TEntityIdentifier, statisticType As String, value As Double)
    Sub ClearEntityMetadata(identifier As TEntityIdentifier, metadataType As String)
    Sub ClearEntityCounter(identifier As TEntityIdentifier, counterType As String)
    Sub ClearEntityStatistic(identifier As TEntityIdentifier, statisticType As String)
    Sub SetYokeFlag(identifier As TYokeIdentifier, flagType As String)
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
    Function ListEntityYokesFrom(identifier As TEntityIdentifier) As IEnumerable(Of TYokeIdentifier)
    Function ListEntityYokesTo(identifier As TEntityIdentifier) As IEnumerable(Of TYokeIdentifier)
    Function ReadYokeType(identifier As TYokeIdentifier) As String
    Function ReadYokeFromIdentifier(identifier As TYokeIdentifier) As TEntityIdentifier
    Function ReadYokeToIdentifier(identifier As TYokeIdentifier) As TEntityIdentifier
    Function ListYokeFlags(identifier As TYokeIdentifier) As IEnumerable(Of String)
    Function CheckYokeHasFlag(identifier As TYokeIdentifier, flagType As String) As Boolean
End Interface
