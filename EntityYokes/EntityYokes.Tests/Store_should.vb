Imports Shouldly
Imports Xunit

Public MustInherit Class Store_should(Of TIdentifier)
    <Fact>
    Sub create_entity()
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        Dim actual = sut.CreateEntity()
        actual.ShouldNotBeNull()
        Should.NotThrow(Sub()
                            Dim identifier = actual.Identifier
                        End Sub)
    End Sub
    Private Function CreateSut() As IStore(Of TIdentifier)
        Return New Store(Of TIdentifier)()
    End Function
End Class

