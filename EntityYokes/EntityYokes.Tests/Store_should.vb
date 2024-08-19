Imports Shouldly
Imports Xunit

Public MustInherit Class Store_should(Of TIdentifier)
    <Theory>
    <InlineData("entity-type")>
    <InlineData("entity-type2")>
    Sub create_entity_with_identifier_and_entity_type(entityType As String)
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        sut.AllEntities.Count.ShouldBe(0)
        Dim actual = sut.CreateEntity(entityType)
        actual.ShouldNotBeNull()
        Should.NotThrow(Sub()
                            Dim identifier = actual.Identifier
                        End Sub)
        actual.EntityType.ShouldBe(entityType)
        sut.AllEntities.Count.ShouldBe(1)
    End Sub
    <Fact>
    Sub create_entities_with_different_identifiers()
        Const EntityType = "entity-type"
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        Dim firstEntity = sut.CreateEntity(EntityType)
        Dim secondEntity = sut.CreateEntity(EntityType)
        firstEntity.Identifier.ShouldNotBe(secondEntity.Identifier)
        sut.AllEntities.Count.ShouldBe(2)
    End Sub
    <Fact>
    Sub retrieve_entity_by_identifier()
        Const EntityType = "entity-type"
        Dim sut As IStore(Of TIdentifier) = CreateSut()
        Dim entityIdentifier = sut.CreateEntity(EntityType).Identifier
        Dim actual = sut.RetrieveEntity(entityIdentifier)
        actual.ShouldNotBeNull
        actual.Identifier.shouldBe(entityIdentifier)
    End Sub
    Private Function CreateSut() As IStore(Of TIdentifier)
        Return New Store(Of TIdentifier)(AddressOf NextIdentifier)
    End Function
    Protected MustOverride Function NextIdentifier() As TIdentifier
End Class

