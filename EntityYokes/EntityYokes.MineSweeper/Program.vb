Imports System.IO
Imports EntityYokes.SQLite
Imports Microsoft.Data.Sqlite
Imports System.Linq

Module Program
    Const BoardEntityType = "board"
    Const BoardColumnEntityType = "board-column"
    Const BoardCellEntityType = "board-cell"
    Const ContainsYokeType = "contains"
    Const NeighborsYokeType = "neighbors"
    Const ColumnsCounterType = "columns"
    Const ColumnCounterType = "column"
    Const RowsCounterType = "rows"
    Const RowCounterType = "row"
    Const BoardColumns = 12
    Const BoardRows = 12
    Sub Main(args As String())
        Using connection As New SqliteConnection("Data Source=:memory:")
            Dim repository As IEntityRepository = CreateRepository(connection)
            InitializeGameData(repository)
            ExportAndCloseDatabase(connection)
        End Using
    End Sub

    Private Sub InitializeGameData(repository As IEntityRepository)
        Dim boardEntity As IEntity = CreateBoard(repository)
        CreateBoardColumns(repository, boardEntity)
        YokeNeighbors(repository)
    End Sub

    Const DirectionCount = 8
    Private ReadOnly Directions As IReadOnlyList(Of Integer) = Enumerable.Range(0, DirectionCount).ToList
    Private ReadOnly DeltaX As IReadOnlyDictionary(Of Integer, Integer) =
        New Dictionary(Of Integer, Integer) From
        {
            {0, 0},
            {1, 1},
            {2, 1},
            {3, 1},
            {4, 0},
            {5, -1},
            {6, -1},
            {7, -1}
        }
    Private ReadOnly DeltaY As IReadOnlyDictionary(Of Integer, Integer) =
        New Dictionary(Of Integer, Integer) From
        {
            {0, -1},
            {1, -1},
            {2, 0},
            {3, 1},
            {4, 1},
            {5, 1},
            {6, 0},
            {7, -1}
        }

    Private Sub YokeNeighbors(repository As IEntityRepository)
        Dim boardCells = repository.RetrieveEntitiesOfType(BoardCellEntityType)
        For Each boardCell In boardCells
            Dim column = boardCell.Counter(ColumnCounterType).Value
            Dim row = boardCell.Counter(RowCounterType).Value
            For Each direction In Directions
                Dim nextColumn = column + DeltaX(direction)
                Dim nextRow = row + DeltaY(direction)
                Dim neighbor = boardCells.SingleOrDefault(Function(x) x.Counter(ColumnCounterType).Value = nextColumn AndAlso x.Counter(RowCounterType).Value = nextRow)
                If neighbor IsNot Nothing Then
                    boardCell.CreateYoke(NeighborsYokeType, neighbor)
                End If
            Next
        Next
    End Sub

    Private Sub CreateBoardColumns(repository As IEntityRepository, boardEntity As IEntity)
        For Each column In Enumerable.Range(0, BoardColumns)
            Dim boardColumnEntity = repository.CreateEntity(BoardColumnEntityType)
            boardColumnEntity.Counter(ColumnCounterType) = column
            boardEntity.CreateYoke(ContainsYokeType, boardColumnEntity)
            CreateBoardCells(repository, boardColumnEntity)
        Next
    End Sub

    Private Sub CreateBoardCells(repository As IEntityRepository, boardColumnEntity As IEntity)
        For Each row In Enumerable.Range(0, BoardRows)
            Dim boardCellEntity = repository.CreateEntity(BoardCellEntityType)
            boardCellEntity.Counter(RowCounterType) = row
            boardCellEntity.Counter(ColumnCounterType) = boardColumnEntity.Counter(ColumnCounterType)
            boardColumnEntity.CreateYoke(ContainsYokeType, boardCellEntity)
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
