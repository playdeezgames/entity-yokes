Imports Shouldly
Imports Xunit

Public MustInherit Class Store_should(Of TIdentifier)
    <Theory>
    <InlineData("entity-type")>
    <InlineData("entity-type2")>
    Sub create_entity_with_identifier_and_entity_type(entityType As String)
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        Dim actual = sut.CreateEntity(entityType)
        actual.ShouldNotBeNull()
        Should.NotThrow(Sub()
                            Dim identifier = actual.Identifier
                        End Sub)
        actual.EntityType.ShouldBe(entityType)
    End Sub
    Private Function CreateSut() As IStore(Of TIdentifier)
        Return New Store(Of TIdentifier)()
    End Function
End Class

