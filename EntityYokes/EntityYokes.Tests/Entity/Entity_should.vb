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
