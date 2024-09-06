
Imports Microsoft.Data.Sqlite

Public Class EntityStore
    Implements IEntityStore(Of Integer, Integer)
    Const EntitiesTableName = "entities"
    Const EntityIdColumnName = "entity_id"
    Const EntityTypeColumnName = "entity_type"

    Const EntityFlagsTableName = "entity_flags"
    Const FlagTypeColumnName = "flag_type"

    Const EntityCountersTableName = "entity_counters"
    Const CounterTypeColumnName = "counter_type"
    Const ValueColumnName = "value"

    Const EntityMetadatasTableName = "entity_metadatas"
    Const MetadataTypeColumnName = "metadata_type"

    Const EntityStatisticsTableName = "entity_statistics"
    Const StatisticTypeColumnName = "statistic_type"

    Const YokesTableName = "yokes"
    Const YokeIdColumnName = "yoke_id"
    Const YokeTypeColumnName = "yoke_type"
    Const FromEntityIdColumnName = "from_entity_id"
    Const ToEntityIdColumnName = "to_entity_id"

    Const YokeFlagsTableName = "yoke_flags"
    Const YokeMetadatasTableName = "yoke_metadatas"
    Const YokeCountersTableName = "yoke_counters"
    Const YokeStatisticsTableName = "yoke_statistics"

    Private ReadOnly connection As SqliteConnection
    Sub New(connection As SqliteConnection)
        Me.connection = connection
    End Sub

    Public Sub DestroyEntity(identifier As Integer) Implements IEntityStore(Of Integer, Integer).DestroyEntity
        CreateEntitiesTable()
        Using command = connection.CreateCommand()
            command.CommandText = $"DELETE FROM `{EntitiesTableName}` WHERE `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateEntitiesTable()
        Using command = connection.CreateCommand()
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{EntitiesTableName}`
(
    `{EntityIdColumnName}` INTEGER PRIMARY KEY AUTOINCREMENT,
    `{EntityTypeColumnName}` TEXT NOT NULL
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SetEntityFlag(identifier As Integer, flagType As String) Implements IEntityStore(Of Integer, Integer).SetEntityFlag
        CreateEntityFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR IGNORE INTO `{EntityFlagsTableName}`
(
    `{EntityIdColumnName}`,
    `{FlagTypeColumnName}`
) 
VALUES
(
    @{EntityIdColumnName},
    @{FlagTypeColumnName}
);"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateEntityFlagsTable()
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{EntityFlagsTableName}`
(
    `{EntityIdColumnName}` INTEGER NOT NULL,
    `{FlagTypeColumnName}` TEXT NOT NULL,
    FOREIGN KEY(`{EntityIdColumnName}`) REFERENCES `{EntitiesTableName}`(`{EntityIdColumnName}`),
    PRIMARY KEY(`{EntityIdColumnName}`,`{FlagTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearEntityFlag(identifier As Integer, flagType As String) Implements IEntityStore(Of Integer, Integer).ClearEntityFlag
        CreateEntityFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{EntityFlagsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{FlagTypeColumnName}`=@{FlagTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteEntityMetadata(identifier As Integer, metadataType As String, value As String) Implements IEntityStore(Of Integer, Integer).WriteEntityMetadata
        CreateEntityMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{EntityMetadatasTableName}`
(
    `{EntityIdColumnName}`,
    `{MetadataTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{EntityIdColumnName},
    @{MetadataTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteEntityCounter(identifier As Integer, counterType As String, value As Integer) Implements IEntityStore(Of Integer, Integer).WriteEntityCounter
        CreateEntityCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{EntityCountersTableName}`
(
    `{EntityIdColumnName}`,
    `{CounterTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{EntityIdColumnName},
    @{CounterTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateEntityCountersTable()
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{EntityCountersTableName}`
(
    `{EntityIdColumnName}` INTEGER NOT NULL,
    `{CounterTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` INTEGER NOT NULL,
    UNIQUE(`{EntityIdColumnName}`,`{CounterTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateYokeCountersTable()
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{YokeCountersTableName}`
(
    `{YokeIdColumnName}` INTEGER NOT NULL,
    `{CounterTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` INTEGER NOT NULL,
    UNIQUE(`{YokeIdColumnName}`,`{CounterTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateEntityMetadatasTable()
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{EntityMetadatasTableName}`
(
    `{EntityIdColumnName}` INTEGER NOT NULL,
    `{MetadataTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` TEXT NOT NULL,
    UNIQUE(`{EntityIdColumnName}`,`{MetadataTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateYokeMetadatasTable()
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{YokeMetadatasTableName}`
(
    `{YokeIdColumnName}` INTEGER NOT NULL,
    `{MetadataTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` TEXT NOT NULL,
    UNIQUE(`{YokeIdColumnName}`,`{MetadataTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateEntityStatisticsTable()
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{EntityStatisticsTableName}`
(
    `{EntityIdColumnName}` INTEGER NOT NULL,
    `{StatisticTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` REAL NOT NULL,
    UNIQUE(`{EntityIdColumnName}`,`{StatisticTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateYokeStatisticsTable()
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{YokeStatisticsTableName}`
(
    `{YokeIdColumnName}` INTEGER NOT NULL,
    `{StatisticTypeColumnName}` TEXT NOT NULL,
    `{ValueColumnName}` REAL NOT NULL,
    UNIQUE(`{YokeIdColumnName}`,`{StatisticTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteEntityStatistic(identifier As Integer, statisticType As String, value As Double) Implements IEntityStore(Of Integer, Integer).WriteEntityStatistic
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{EntityStatisticsTableName}`
(
    `{EntityIdColumnName}`,
    `{StatisticTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{EntityIdColumnName},
    @{StatisticTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearEntityMetadata(identifier As Integer, metadataType As String) Implements IEntityStore(Of Integer, Integer).ClearEntityMetadata
        CreateEntityMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{EntityMetadatasTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{MetadataTypeColumnName}`=@{MetadataTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearEntityCounter(identifier As Integer, counterType As String) Implements IEntityStore(Of Integer, Integer).ClearEntityCounter
        CreateEntityCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{EntityCountersTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{CounterTypeColumnName}`=@{CounterTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearEntityStatistic(identifier As Integer, statisticType As String) Implements IEntityStore(Of Integer, Integer).ClearEntityStatistic
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{EntityStatisticsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{StatisticTypeColumnName}`=@{StatisticTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SetYokeFlag(identifier As Integer, flagType As String) Implements IEntityStore(Of Integer, Integer).SetYokeFlag
        CreateYokeFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR IGNORE INTO `{YokeFlagsTableName}`
(
    `{YokeIdColumnName}`,
    `{FlagTypeColumnName}`
) 
VALUES
(
    @{YokeIdColumnName},
    @{FlagTypeColumnName}
);"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CreateYokeFlagsTable()
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{YokeFlagsTableName}`
(
    `{YokeIdColumnName}` INTEGER NOT NULL,
    `{FlagTypeColumnName}` TEXT NOT NULL,
    FOREIGN KEY(`{YokeIdColumnName}`) REFERENCES `{YokesTableName}`(`{YokeIdColumnName}`),
    PRIMARY KEY(`{YokeIdColumnName}`,`{FlagTypeColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearYokeFlag(identifier As Integer, flagType As String) Implements IEntityStore(Of Integer, Integer).ClearYokeFlag
        CreateYokeFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{YokeFlagsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{FlagTypeColumnName}`=@{FlagTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteYokeMetadata(identifier As Integer, metadataType As String, value As String) Implements IEntityStore(Of Integer, Integer).WriteYokeMetadata
        CreateYokeMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{YokeMetadatasTableName}`
(
    `{YokeIdColumnName}`,
    `{MetadataTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{YokeIdColumnName},
    @{MetadataTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearYokeMetadata(identifier As Integer, metadataType As String) Implements IEntityStore(Of Integer, Integer).ClearYokeMetadata
        CreateYokeMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{YokeMetadatasTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{MetadataTypeColumnName}`=@{MetadataTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteYokeCounter(identifier As Integer, counterType As String, value As Integer) Implements IEntityStore(Of Integer, Integer).WriteYokeCounter
        CreateYokeCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{YokeCountersTableName}`
(
    `{YokeIdColumnName}`,
    `{CounterTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{YokeIdColumnName},
    @{CounterTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearYokeCounter(identifier As Integer, counterType As String) Implements IEntityStore(Of Integer, Integer).ClearYokeCounter
        CreateYokeCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{YokeCountersTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{CounterTypeColumnName}`=@{CounterTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub WriteYokeStatistic(identifier As Integer, statisticType As String, value As Double) Implements IEntityStore(Of Integer, Integer).WriteYokeStatistic
        CreateYokeStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT OR REPLACE INTO `{YokeStatisticsTableName}`
(
    `{YokeIdColumnName}`,
    `{StatisticTypeColumnName}`,
    `{ValueColumnName}`
) 
VALUES
(
    @{YokeIdColumnName},
    @{StatisticTypeColumnName},
    @{ValueColumnName}
);"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            command.Parameters.AddWithValue($"@{ValueColumnName}", value)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ClearYokeStatistic(identifier As Integer, statisticType As String) Implements IEntityStore(Of Integer, Integer).ClearYokeStatistic
        CreateYokeStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
DELETE FROM 
    `{YokeStatisticsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{StatisticTypeColumnName}`=@{StatisticTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DestroyYoke(identifier As Integer) Implements IEntityStore(Of Integer, Integer).DestroyYoke
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"DELETE FROM `{YokesTableName}` WHERE `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function CreateEntity(entityType As String) As Integer Implements IEntityStore(Of Integer, Integer).CreateEntity
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"INSERT INTO `{EntitiesTableName}`(`{EntityTypeColumnName}`) VALUES (@{EntityTypeColumnName}) RETURNING `{EntityIdColumnName}`;"
            command.Parameters.AddWithValue($"@{EntityTypeColumnName}", entityType)
            Return CInt(command.ExecuteScalar)
        End Using
    End Function

    Public Function CreateYoke(yokeType As String, fromIdentifier As Integer, toIdentifier As Integer) As Integer Implements IEntityStore(Of Integer, Integer).CreateYoke
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT INTO `{YokesTableName}`
(
    `{YokeTypeColumnName}`,
    `{FromEntityIdColumnName}`,
    `{ToEntityIdColumnName}`
) 
VALUES
(
    @{YokeTypeColumnName},
    @{FromEntityIdColumnName},
    @{ToEntityIdColumnName}) 
RETURNING 
    `{YokeIdColumnName}`;"
            command.Parameters.AddWithValue($"@{YokeTypeColumnName}", yokeType)
            command.Parameters.AddWithValue($"@{FromEntityIdColumnName}", fromIdentifier)
            command.Parameters.AddWithValue($"@{ToEntityIdColumnName}", toIdentifier)
            Return CInt(command.ExecuteScalar())
        End Using
    End Function

    Private Sub CreateYokesTable()
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
CREATE TABLE IF NOT EXISTS `{YokesTableName}`
(
    `{YokeIdColumnName}` INTEGER PRIMARY KEY AUTOINCREMENT,
    `{YokeTypeColumnName}` TEXT NOT NULL,
    `{FromEntityIdColumnName}` INTEGER NOT NULL,
    `{ToEntityIdColumnName}` INTEGER NOT NULL,
    FOREIGN KEY (`{FromEntityIdColumnName}`) REFERENCES `{EntitiesTableName}`(`{EntityIdColumnName}`),
    FOREIGN KEY (`{ToEntityIdColumnName}`) REFERENCES `{EntitiesTableName}`(`{EntityIdColumnName}`)
);"
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function ListEntities() As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntities
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT `{EntityIdColumnName}` FROM `{EntitiesTableName}`;"
            Using reader = command.ExecuteReader
                Dim result As New List(Of Integer)
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ReadEntityType(identifier As Integer) As String Implements IEntityStore(Of Integer, Integer).ReadEntityType
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{EntityTypeColumnName}` 
FROM 
    `{EntitiesTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Return CStr(command.ExecuteScalar)
        End Using
    End Function

    Public Function DoesEntityExist(identifier As Integer) As Boolean Implements IEntityStore(Of Integer, Integer).DoesEntityExist
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{EntitiesTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function

    Public Function ListEntitiesOfType(entityType As String) As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntitiesOfType
        CreateEntitiesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{EntityIdColumnName}` 
FROM 
    `{EntitiesTableName}` 
WHERE 
    `{EntityTypeColumnName}` = @{EntityTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityTypeColumnName}", entityType)
            Dim result As New List(Of Integer)
            Using reader = command.ExecuteReader
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
            End Using
            Return result
        End Using
    End Function

    Public Function CheckEntityHasFlag(identifier As Integer, flagType As String) As Boolean Implements IEntityStore(Of Integer, Integer).CheckEntityHasFlag
        CreateEntityFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{EntityFlagsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{FlagTypeColumnName}`=@{FlagTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function

    Public Function ListEntityFlags(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListEntityFlags
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{FlagTypeColumnName}` 
FROM 
    `{EntityFlagsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityMetadatas(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListEntityMetadatas
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{MetadataTypeColumnName}` 
FROM 
    `{EntityMetadatasTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityCounters(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListEntityCounters
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{CounterTypeColumnName}` 
FROM 
    `{EntityCountersTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityStatistics(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListEntityStatistics
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{StatisticTypeColumnName}` 
FROM 
    `{EntityStatisticsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ReadEntityMetadata(identifier As Integer, metadataType As String) As String Implements IEntityStore(Of Integer, Integer).ReadEntityMetadata
        CreateEntityMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{EntityMetadatasTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{MetadataTypeColumnName}`=@{MetadataTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CStr(result)
        End Using
    End Function

    Public Function ReadEntityCounter(identifier As Integer, counterType As String) As Integer? Implements IEntityStore(Of Integer, Integer).ReadEntityCounter
        CreateEntityCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{EntityCountersTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{CounterTypeColumnName}`=@{CounterTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function

    Public Function ReadEntityStatistic(identifier As Integer, statisticType As String) As Double? Implements IEntityStore(Of Integer, Integer).ReadEntityStatistic
        CreateEntityStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{EntityStatisticsTableName}` 
WHERE 
    `{EntityIdColumnName}`=@{EntityIdColumnName} AND 
    `{StatisticTypeColumnName}`=@{StatisticTypeColumnName};"
            command.Parameters.AddWithValue($"@{EntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CDbl(result)
        End Using
    End Function

    Public Function ListEntityYokesFrom(identifier As Integer) As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntityYokesFrom
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{YokeIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{FromEntityIdColumnName}`=@{FromEntityIdColumnName};"
            command.Parameters.AddWithValue($"@{FromEntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of Integer)
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityYokesTo(identifier As Integer) As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntityYokesTo
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{YokeIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{ToEntityIdColumnName}`=@{ToEntityIdColumnName};"
            command.Parameters.AddWithValue($"@{ToEntityIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of Integer)
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ReadYokeType(identifier As Integer) As String Implements IEntityStore(Of Integer, Integer).ReadYokeType
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{YokeTypeColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Return CStr(command.ExecuteScalar)
        End Using
    End Function

    Public Function ReadYokeFromIdentifier(identifier As Integer) As Integer Implements IEntityStore(Of Integer, Integer).ReadYokeFromIdentifier
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{FromEntityIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Return CInt(command.ExecuteScalar)
        End Using
    End Function

    Public Function ReadYokeToIdentifier(identifier As Integer) As Integer Implements IEntityStore(Of Integer, Integer).ReadYokeToIdentifier
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ToEntityIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Return CInt(command.ExecuteScalar)
        End Using
    End Function

    Public Function ListYokeFlags(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListYokeFlags
        CreateYokeFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{FlagTypeColumnName}` 
FROM 
    `{YokeFlagsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function CheckYokeHasFlag(identifier As Integer, flagType As String) As Boolean Implements IEntityStore(Of Integer, Integer).CheckYokeHasFlag
        CreateYokeFlagsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{YokeFlagsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{FlagTypeColumnName}`=@{FlagTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{FlagTypeColumnName}", flagType)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function

    Public Function ReadYokeMetadata(identifier As Integer, metadataType As String) As String Implements IEntityStore(Of Integer, Integer).ReadYokeMetadata
        CreateYokeMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{YokeMetadatasTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{MetadataTypeColumnName}`=@{MetadataTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{MetadataTypeColumnName}", metadataType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CStr(result)
        End Using
    End Function

    Public Function ListYokeMetadatas(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListYokeMetadatas
        CreateYokeMetadatasTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{MetadataTypeColumnName}` 
FROM 
    `{YokeMetadatasTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ReadYokeCounter(identifier As Integer, counterType As String) As Integer? Implements IEntityStore(Of Integer, Integer).ReadYokeCounter
        CreateYokeCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{YokeCountersTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{CounterTypeColumnName}`=@{CounterTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{CounterTypeColumnName}", counterType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CInt(result)
        End Using
    End Function

    Public Function ListYokeCounters(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListYokeCounters
        CreateYokeCountersTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{CounterTypeColumnName}` 
FROM 
    `{YokeCountersTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ReadYokeStatistic(identifier As Integer, statisticType As String) As Double? Implements IEntityStore(Of Integer, Integer).ReadYokeStatistic
        CreateYokeStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{ValueColumnName}` 
FROM 
    `{YokeStatisticsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName} AND 
    `{StatisticTypeColumnName}`=@{StatisticTypeColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{StatisticTypeColumnName}", statisticType)
            Dim result = command.ExecuteScalar
            If result Is Nothing Then
                Return Nothing
            End If
            Return CDbl(result)
        End Using
    End Function

    Public Function ListYokeStatistics(identifier As Integer) As IEnumerable(Of String) Implements IEntityStore(Of Integer, Integer).ListYokeStatistics
        CreateYokeStatisticsTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{StatisticTypeColumnName}` 
FROM 
    `{YokeStatisticsTableName}` 
WHERE 
    `{YokeIdColumnName}`=@{YokeIdColumnName};"
            command.Parameters.AddWithValue($"@{YokeIdColumnName}", identifier)
            Using reader = command.ExecuteReader
                Dim result As New List(Of String)
                While reader.Read
                    result.Add(reader.GetString(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityYokesOfTypeFrom(identifier As Integer, yokeType As String) As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntityYokesOfTypeFrom
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{YokeIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{FromEntityIdColumnName}`=@{FromEntityIdColumnName} AND
    `{YokeTypeColumnName}`=@{YokeTypeColumnName};"
            command.Parameters.AddWithValue($"@{FromEntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{YokeTypeColumnName}", yokeType)
            Using reader = command.ExecuteReader
                Dim result As New List(Of Integer)
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
                Return result
            End Using
        End Using
    End Function

    Public Function ListEntityYokesOfTypeTo(identifier As Integer, yokeType As String) As IEnumerable(Of Integer) Implements IEntityStore(Of Integer, Integer).ListEntityYokesOfTypeTo
        CreateYokesTable()
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    `{YokeIdColumnName}` 
FROM 
    `{YokesTableName}` 
WHERE 
    `{ToEntityIdColumnName}`=@{ToEntityIdColumnName} AND
    `{YokeTypeColumnName}`=@{YokeTypeColumnName};"
            command.Parameters.AddWithValue($"@{ToEntityIdColumnName}", identifier)
            command.Parameters.AddWithValue($"@{YokeTypeColumnName}", yokeType)
            Using reader = command.ExecuteReader
                Dim result As New List(Of Integer)
                While reader.Read
                    result.Add(reader.GetInt32(0))
                End While
                Return result
            End Using
        End Using
    End Function
End Class
