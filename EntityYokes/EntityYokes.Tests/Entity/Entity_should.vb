Imports Shouldly
Imports Xunit

Public MustInherit Class Entity_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IEntity(Of TEntityIdentifier, TYokeIdentifier))

    Const MetadataType = "metadata-type"
    Const CounterType = "counter-type"
    Const StatisticType = "statistic-type"
    <Fact>
    Sub yoke_to_another_entity()
        Dim repository = CreateRepository()
        Dim firstEntity = repository.CreateEntity(EntityType)
        Dim secondEntity = repository.CreateEntity(EntityType)
        Const YokeType = "yoke-type"
        Dim yoke = firstEntity.CreateYoke(YokeType, secondEntity)
        yoke.ShouldNotBeNull
        yoke.FromEntity.Identifier.ShouldBe(firstEntity.Identifier)
        yoke.ToEntity.Identifier.ShouldBe(secondEntity.Identifier)
        yoke.YokeType.ShouldBe(YokeType)
        firstEntity.YokesFrom.ShouldHaveSingleItem
        firstEntity.YokesFrom.Single.ToEntity.Identifier.ShouldBe(secondEntity.Identifier)
        secondEntity.YokesTo.ShouldHaveSingleItem
        secondEntity.YokesTo.Single.FromEntity.Identifier.ShouldBe(firstEntity.Identifier)
    End Sub
    <Fact>
    Sub yoke_to_self()
        Dim sut As IEntity(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        Const YokeType = "yoke-type"
        Dim yoke = sut.CreateYoke(YokeType, sut)
        yoke.FromEntity.Identifier.ShouldBe(sut.Identifier)
        yoke.ToEntity.Identifier.ShouldBe(sut.Identifier)
        yoke.YokeType.ShouldBe(YokeType)
        sut.YokesFrom.ShouldHaveSingleItem
        sut.YokesFrom.Single.ToEntity.Identifier.ShouldBe(sut.Identifier)
        sut.YokesTo.ShouldHaveSingleItem
        sut.YokesTo.Single.FromEntity.Identifier.ShouldBe(sut.Identifier)
    End Sub
    <Fact>
    Sub destroy_itself()
        Dim repository As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.ShouldNotBeNull
        repository.AllEntities.ShouldHaveSingleItem
        sut.Destroy()
        repository.AllEntities.ShouldBeEmpty
        Should.Throw(Of NullReferenceException)(Function() sut.EntityType)
    End Sub
    <Fact>
    Sub not_destroy_entities_with_flags()
        Dim sut As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = sut.CreateEntity(EntityType)
        entity.Flag("flag-type") = True
        Should.Throw(Of InvalidOperationException)(Sub() entity.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_metadata()
        Dim sut As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = sut.CreateEntity(EntityType)
        entity.Metadata("metadata-type") = "value"
        Should.Throw(Of InvalidOperationException)(Sub() entity.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_counter()
        Dim sut As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = sut.CreateEntity(EntityType)
        entity.Counter("counter-type") = 1
        Should.Throw(Of InvalidOperationException)(Sub() entity.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_statistic()
        Dim sut As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = sut.CreateEntity(EntityType)
        entity.Statistic("statistic-type") = 1.0
        Should.Throw(Of InvalidOperationException)(Sub() entity.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_yokes()
        Dim sut As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = sut.CreateEntity(EntityType)
        Dim yoke = entity.CreateYoke("yoke-type", entity)
        Should.Throw(Of InvalidOperationException)(Sub() entity.Destroy())
    End Sub
    Const EntityType = "entity-type"
    Protected Overrides Function CreateSut() As IEntity(Of TEntityIdentifier, TYokeIdentifier)
        Return CreateRepository().CreateEntity(EntityType)
    End Function
    Private Function CreateRepository() As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier)
        Return New EntityRepository(Of TEntityIdentifier, TYokeIdentifier)(CreateStore())
    End Function
    Protected MustOverride Function NextEntityIdentifier() As TEntityIdentifier
    Protected MustOverride Function NextYokeIdentifier() As TYokeIdentifier
    Private Function CreateStore() As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
        Return New FakeEntityStore(Of TEntityIdentifier, TYokeIdentifier)(AddressOf NextEntityIdentifier, AddressOf NextYokeIdentifier)
    End Function
End Class
