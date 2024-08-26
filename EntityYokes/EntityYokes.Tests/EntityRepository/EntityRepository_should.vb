Imports Shouldly
Imports Xunit

Public MustInherit Class EntityRepository_should(Of TEntityIdentifier, TYokeIdentifier)
    Const EntityType = "entity-type"
    <Theory>
    <InlineData("entity-type")>
    <InlineData("entity-type2")>
    Sub create_entity_with_identifier_and_entity_type(entityType As String)
        Dim sut As IEntityRepository = CreateSut()
        sut.Entities.Count.ShouldBe(0)
        Dim actual = sut.CreateEntity(entityType)
        actual.ShouldNotBeNull()
        actual.EntityType.ShouldBe(entityType)
        sut.Entities.Count.ShouldBe(1)
    End Sub
    <Fact>
    Sub blow_up_when_trying_to_create_entity_with_null_entity_type()
        Dim sut As IEntityRepository = CreateSut()
        Should.Throw(Of ArgumentNullException)(Function() sut.CreateEntity(Nothing))
    End Sub
    <Fact>
    Sub create_entities_with_different_identifiers()
        Dim sut As IEntityRepository = CreateSut()
        Dim firstEntity = sut.CreateEntity(EntityType)
        Dim secondEntity = sut.CreateEntity(EntityType)
        firstEntity.ShouldNotBe(secondEntity)
        sut.Entities.Count.ShouldBe(2)
    End Sub
    <Fact>
    Sub retrieve_entities_by_entity_type()
        Dim sut As IEntityRepository = CreateSut()
        sut.EntitiesOfType(EntityType).Count.ShouldBe(0)
        sut.CreateEntity(EntityType)
        sut.CreateEntity(EntityType)
        Dim actual = sut.EntitiesOfType(EntityType)
        actual.Count.ShouldBe(2)
        actual.All(Function(x) x.EntityType = EntityType).ShouldBeTrue
    End Sub
    Private Function CreateSut() As IEntityRepository
        Return New EntityRepository(Of TEntityIdentifier, TYokeIdentifier)(CreateStore())
    End Function
    Protected MustOverride Function NextEntityIdentifier() As TEntityIdentifier
    Protected MustOverride Function NextYokeIdentifier() As TYokeIdentifier
    Private Function CreateStore() As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
        Return New FakeEntityStore(Of TEntityIdentifier, TYokeIdentifier)(AddressOf NextEntityIdentifier, AddressOf NextYokeIdentifier)
    End Function
End Class

