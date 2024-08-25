Imports EntityYokes.SQLite
Imports Microsoft.Data.Sqlite

Module Program
    Const BoardEntityType = "board"
    Const ColumnsCounterType = "columns"
    Const RowsCounterType = "rows"
    Const BoardColumns = 11
    Const BoardRows = 13
    Sub Main(args As String())
        Using connection As New SqliteConnection("Data Source=:memory:")
            connection.Open()
            Dim store As IEntityStore(Of Integer, Integer) = New EntityStore(connection)
            Dim repository As IEntityRepository = New EntityRepository(Of Integer, Integer)(store)
            Dim boardEntity = repository.CreateEntity(BoardEntityType)
            boardEntity.Counter(ColumnsCounterType) = BoardColumns
            boardEntity.Counter(RowsCounterType) = BoardRows
            System.IO.File.Delete("output.db")
            Using command = connection.CreateCommand
                command.CommandText = "VACUUM INTO ""output.db"";"
                command.ExecuteNonQuery()
            End Using
            connection.Close()
        End Using
    End Sub
End Module
