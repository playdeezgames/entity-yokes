Imports EntityYokes.SQLite
Imports Microsoft.Data.Sqlite

Module Program
    Const BoardEntityType = "board"
    Const BoardColumns = 12
    Const BoardRows = 12
    Sub Main(args As String())
        Using connection As New SqliteConnection("Data Source=:memory:")
            connection.Open()
            Dim store As IEntityStore(Of Integer, Integer) = New EntityStore(connection)
            Dim repository As IEntityRepository = New EntityRepository(Of Integer, Integer)(store)
            Dim boardEntity = repository.CreateEntity(BoardEntityType)
            Using command = connection.CreateCommand
                command.CommandText = "VACUUM INTO ""output.db"";"
                command.ExecuteNonQuery()
            End Using
            connection.Close()
        End Using
    End Sub
End Module
