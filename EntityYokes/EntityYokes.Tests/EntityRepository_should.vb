Imports Shouldly
Imports Xunit

Public MustInherit Class EntityRepository_should(Of TIdentifier)
    Const EntityType = "entity-type"
    <Theory>
    <InlineData("entity-type")>
    <InlineData("entity-type2")>
    Sub create_entity_with_identifier_and_entity_type(entityType As String)
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
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
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
        Dim firstEntity = sut.CreateEntity(EntityType)
        Dim secondEntity = sut.CreateEntity(EntityType)
        firstEntity.Identifier.ShouldNotBe(secondEntity.Identifier)
        sut.AllEntities.Count.ShouldBe(2)
    End Sub
    <Fact>
    Sub retrieve_entity_by_identifier()
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
        Dim entityIdentifier = sut.CreateEntity(EntityType).Identifier
        Dim actual = sut.RetrieveEntity(entityIdentifier)
        actual.ShouldNotBeNull
        actual.Identifier.ShouldBe(entityIdentifier)
    End Sub
    Private Function CreateSut() As IEntityRepository(Of TIdentifier)
        Return New EntityRepository(Of TIdentifier)(CreateStore(), AddressOf NextIdentifier)
    End Function
    Protected MustOverride Function NextIdentifier() As TIdentifier
    Protected MustOverride Function CreateStore() As IEntityStore(Of TIdentifier)
End Class

