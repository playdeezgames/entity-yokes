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
    Sub blow_up_when_trying_to_create_entity_with_null_entity_type()
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
        Should.Throw(Of ArgumentNullException)(Function() sut.CreateEntity(Nothing))
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
    <Fact>
    Sub retrieve_entities_by_entity_type()
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
        sut.RetrieveEntitiesOfType(EntityType).Count.ShouldBe(0)
        sut.CreateEntity(EntityType)
        sut.CreateEntity(EntityType)
        Dim actual = sut.RetrieveEntitiesOfType(EntityType)
        actual.Count.ShouldBe(2)
        actual.All(Function(x) x.EntityType = EntityType).ShouldBeTrue
    End Sub
    <Fact>
    Sub destroy_entity()
        Dim sut As IEntityRepository(Of TIdentifier) = CreateSut()
        Dim entity = sut.CreateEntity(EntityType)
        entity.ShouldNotBeNull
        sut.AllEntities.ShouldHaveSingleItem
        sut.DestroyEntity(entity)
        sut.AllEntities.ShouldBeEmpty
        Should.Throw(Of KeyNotFoundException)(Sub()
                                                  Dim entityType = entity.EntityType
                                              End Sub)
    End Sub
    Private Function CreateSut() As IEntityRepository(Of TIdentifier)
        Return New EntityRepository(Of TIdentifier)(CreateStore())
    End Function
    Protected MustOverride Function NextEntityIdentifier() As TIdentifier
    Private Function CreateStore() As IEntityStore(Of TIdentifier)
        Return New FakeEntityStore(Of TIdentifier)(AddressOf NextEntityIdentifier)
    End Function
End Class

