Imports Shouldly
Imports Xunit

Public MustInherit Class Store_should(Of TIdentifier)
    <Fact>
    Sub create_entity_with_identifier_and_entity_type()
        Const EntityType = "entity-type"
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        Dim actual = sut.CreateEntity(EntityType)
        actual.ShouldNotBeNull()
        Should.NotThrow(Sub()
                            Dim identifier = actual.Identifier
                        End Sub)
        actual.EntityType.ShouldBe(EntityType)
    End Sub
    Private Function CreateSut() As IStore(Of TIdentifier)
        Return New Store(Of TIdentifier)()
    End Function
End Class

