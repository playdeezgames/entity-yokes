Imports System.IO
Imports EntityYokes.SQLite
Imports Microsoft.Data.Sqlite

Module Program
    Sub Main(args As String())
        Using connection As New SqliteConnection("Data Source=:memory:")
            Dim repository As IEntityRepository = CreateRepository(connection)
            InitializeGameData(repository)
            ExportAndCloseDatabase(connection)
        End Using
    End Sub

    Private Sub ExportAndCloseDatabase(connection As SqliteConnection)
        File.Delete("output.db")
        Using command = connection.CreateCommand
            command.CommandText = "VACUUM INTO ""output.db"";"
            command.ExecuteNonQuery()
        End Using
        connection.Close()
    End Sub

    Private Sub InitializeGameData(repository As IEntityRepository)
        Dim entityA = repository.CreateEntity("a")
        Debug.Assert(entityA.EntityType = "a")
        entityA.Statistic("s1") = 0.0
        entityA.Statistic("s2") = 1.0
        Debug.Assert(entityA.Statistics.Count = 2)
        Debug.Assert(entityA.Statistic("s2").Value = 1.0)
        entityA.Statistic("s1") = Nothing
        entityA.Statistic("s2") = 2.0
        Debug.Assert(entityA.Statistics.Count = 1)
        entityA.Flag("f1") = True
        entityA.Flag("f2") = True
        Debug.Assert(entityA.Flags.Count = 2)
        Debug.Assert(entityA.Flag("f1"))
        entityA.Flag("f1") = False
        Debug.Assert(entityA.Flags.Count = 1)
        entityA.Counter("c1") = 0
        entityA.Counter("c2") = 1
        Debug.Assert(entityA.Counters.Count = 2)
        Debug.Assert(entityA.Counter("c2").Value = 1)
        entityA.Counter("c1") = Nothing
        entityA.Counter("c2") = 2
        Debug.Assert(entityA.Counters.Count = 1)
        entityA.Metadata("m1") = "a"
        entityA.Metadata("m2") = "b"
        Debug.Assert(entityA.Metadatas.Count = 2)
        Debug.Assert(entityA.Metadata("m2") = "b")
        entityA.Metadata("m1") = Nothing
        entityA.Metadata("m2") = "c"
        Debug.Assert(entityA.Metadatas.Count = 1)
        Dim entityB = repository.CreateEntity("b")
        Debug.Assert(repository.Entities.Count = 2)
        Debug.Assert(repository.EntitiesOfType("a").Count = 1)
        Dim yokeA = entityA.CreateYoke("a", entityB)
        Debug.Assert(yokeA.YokeType = "a")
        yokeA.Statistic("s1") = 0.0
        yokeA.Statistic("s2") = 1.0
        Debug.Assert(yokeA.Statistics.Count = 2)
        Debug.Assert(yokeA.Statistic("s2").Value = 1.0)
        yokeA.Statistic("s1") = Nothing
        yokeA.Statistic("s2") = 2.0
        yokeA.Flag("f1") = True
        yokeA.Flag("f2") = True
        Debug.Assert(yokeA.Flags.Count = 2)
        Debug.Assert(yokeA.Flag("f1"))
        yokeA.Flag("f1") = False
        Debug.Assert(yokeA.Flags.Count = 1)
        yokeA.Counter("c1") = 0
        yokeA.Counter("c2") = 1
        Debug.Assert(yokeA.Counters.Count = 2)
        Debug.Assert(yokeA.Counter("c2").Value = 1)
        yokeA.Counter("c1") = Nothing
        yokeA.Counter("c2") = 2
        Debug.Assert(yokeA.Counters.Count = 1)
        yokeA.Metadata("m1") = "a"
        yokeA.Metadata("m2") = "b"
        Debug.Assert(yokeA.Metadatas.Count = 2)
        Debug.Assert(yokeA.Metadata("m2") = "b")
        yokeA.Metadata("m1") = Nothing
        yokeA.Metadata("m2") = "c"
        Debug.Assert(yokeA.Metadatas.Count = 1)
        Debug.Assert(yokeA.FromEntity IsNot Nothing)
        Debug.Assert(yokeA.ToEntity IsNot Nothing)

        Dim yokeB = entityA.CreateYoke("b", entityA)
        Debug.Assert(entityA.YokesOfTypeFrom("b").Count = 1)
        Debug.Assert(entityA.YokesFrom.Count = 2)
        Debug.Assert(entityA.YokesTo.Count = 1)
        Debug.Assert(entityB.YokesTo.Count = 1)
        Debug.Assert(entityA.YokesOfTypeTo("b").Count = 1)
        yokeB.Destroy()
        Debug.Assert(entityA.YokesFrom.Count = 1)
        Debug.Assert(entityA.YokesTo.Count = 0)
        Debug.Assert(entityB.YokesTo.Count = 1)
        Dim entityC = repository.CreateEntity("c")
        Debug.Assert(repository.Entities.Count = 3)
        entityC.Destroy()
        Debug.Assert(repository.Entities.Count = 2)
    End Sub

    Private Function CreateRepository(connection As SqliteConnection) As IEntityRepository
        connection.Open()
        Dim store As IEntityStore(Of Integer, Integer) = New EntityStore(connection)
        Dim repository As IEntityRepository = New EntityRepository(Of Integer, Integer)(store)
        Return repository
    End Function
End Module
