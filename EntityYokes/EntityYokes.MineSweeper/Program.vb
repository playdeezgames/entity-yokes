Imports System.IO
Imports EntityYokes.SQLite
Imports Microsoft.Data.Sqlite

Module Program
    Const BoardEntityType = "board"
    Const BoardColumnEntityType = "board-column"
    Const ContainsYokeType = "contains"
    Const ColumnsCounterType = "columns"
    Const ColumnCounterType = "column"
    Const RowsCounterType = "rows"
    Const RowCounterType = "row"
    Const BoardColumns = 11
    Const BoardRows = 13
    Sub Main(args As String())
        Using connection As New SqliteConnection("Data Source=:memory:")
            Dim repository As IEntityRepository = CreateRepository(connection)
            InitializeGameData(repository)
            ExportAndCloseDatabase(connection)
        End Using
    End Sub

    Private Sub InitializeGameData(repository As IEntityRepository)
        Dim boardEntity As IEntity = CreateBoard(repository)
        For Each column In Enumerable.Range(0, BoardColumns)
            Dim boardColumnEntity = repository.CreateEntity(BoardColumnEntityType)
            boardColumnEntity.Counter(ColumnCounterType) = column
            boardEntity.CreateYoke(ContainsYokeType, boardColumnEntity)
        Next
    End Sub

    Private Function CreateBoard(repository As IEntityRepository) As IEntity
        Dim boardEntity = repository.CreateEntity(BoardEntityType)
        boardEntity.Counter(ColumnsCounterType) = BoardColumns
        boardEntity.Counter(RowsCounterType) = BoardRows
        Return boardEntity
    End Function

    Private Function CreateRepository(connection As SqliteConnection) As IEntityRepository
        connection.Open()
        Dim store As IEntityStore(Of Integer, Integer) = New EntityStore(connection)
        Dim repository As IEntityRepository = New EntityRepository(Of Integer, Integer)(store)
        Return repository
    End Function

    Private Sub ExportAndCloseDatabase(connection As SqliteConnection)
        File.Delete("output.db")
        Using command = connection.CreateCommand
            command.CommandText = "VACUUM INTO ""output.db"";"
            command.ExecuteNonQuery()
        End Using
        connection.Close()
    End Sub
End Module
