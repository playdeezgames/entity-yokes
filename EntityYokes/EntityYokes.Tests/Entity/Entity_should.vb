Imports Shouldly
Imports Xunit

Public MustInherit Class Entity_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IEntity)

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
        yoke.FromEntity.ShouldBe(firstEntity)
        yoke.ToEntity.ShouldBe(secondEntity)
        yoke.YokeType.ShouldBe(YokeType)
        firstEntity.YokesFrom.ShouldHaveSingleItem
        firstEntity.YokesFrom.Single.ToEntity.ShouldBe(secondEntity)
        secondEntity.YokesTo.ShouldHaveSingleItem
        secondEntity.YokesTo.Single.FromEntity.ShouldBe(firstEntity)
    End Sub
    <Fact>
    Sub yoke_to_self()
        Dim sut As IEntity = CreateSut()
        Const YokeType = "yoke-type"
        Dim yoke = sut.CreateYoke(YokeType, sut)
        yoke.FromEntity.ShouldBe(sut)
        yoke.ToEntity.ShouldBe(sut)
        yoke.YokeType.ShouldBe(YokeType)
        sut.YokesFrom.ShouldHaveSingleItem
        sut.YokesFrom.Single.ToEntity.ShouldBe(sut)
        sut.YokesTo.ShouldHaveSingleItem
        sut.YokesTo.Single.FromEntity.ShouldBe(sut)
    End Sub
    <Fact>
    Sub destroy_itself()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.ShouldNotBeNull
        repository.Entities.ShouldHaveSingleItem
        sut.Destroy()
        repository.Entities.ShouldBeEmpty
        Should.Throw(Of NullReferenceException)(Function() sut.EntityType)
    End Sub
    <Fact>
    Sub not_destroy_entities_with_flags()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.Flag("flag-type") = True
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_metadata()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.Metadata("metadata-type") = "value"
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_counter()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.Counter("counter-type") = 1
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_statistic()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        sut.Statistic("statistic-type") = 1.0
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_yokes()
        Dim repository As IEntityRepository = CreateRepository()
        Dim sut = repository.CreateEntity(EntityType)
        Dim yoke = sut.CreateYoke("yoke-type", sut)
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    Const EntityType = "entity-type"
    Protected Overrides Function CreateSut() As IEntity
        Return CreateRepository().CreateEntity(EntityType)
    End Function
    Private Function CreateRepository() As IEntityRepository
        Return New EntityRepository(Of TEntityIdentifier, TYokeIdentifier)(CreateStore())
    End Function
    Protected MustOverride Function NextEntityIdentifier() As TEntityIdentifier
    Protected MustOverride Function NextYokeIdentifier() As TYokeIdentifier
    Private Function CreateStore() As IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
        Return New FakeEntityStore(Of TEntityIdentifier, TYokeIdentifier)(AddressOf NextEntityIdentifier, AddressOf NextYokeIdentifier)
    End Function
End Class
