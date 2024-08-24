Imports Shouldly
Imports Xunit

Public MustInherit Class Entity_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IEntity(Of TEntityIdentifier, TYokeIdentifier))
    Const FlagType = "flag-type"
    Const MetadataType = "metadata-type"
    Const CounterType = "counter-type"
    Const StatisticType = "statistic-type"
    <Fact>
    Sub set_metadata()
        Dim sut As IEntity(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        Const value = "value"
        sut.Metadata(MetadataType) = value
        sut.Metadata(MetadataType).ShouldBe(value)
        sut.Metadatas.ShouldHaveSingleItem
        Const otherValue = "other value"
        sut.Metadata(MetadataType) = otherValue
        sut.Metadata(MetadataType).ShouldBe(otherValue)
        sut.Metadatas.ShouldHaveSingleItem
        sut.Metadata(MetadataType) = Nothing
        sut.Metadata(MetadataType).ShouldBeNull
        sut.Metadatas.ShouldBeEmpty
    End Sub
    <Fact>
    Sub set_counter()
        Dim sut As IEntity(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        Const value = 1
        sut.Counter(CounterType) = value
        sut.Counter(CounterType).ShouldBe(value)
        sut.Counters.ShouldHaveSingleItem
        Const otherValue = 2
        sut.Counter(CounterType) = otherValue
        sut.Counter(CounterType).ShouldBe(otherValue)
        sut.Counters.ShouldHaveSingleItem
        sut.Counter(CounterType) = Nothing
        sut.Counter(CounterType).ShouldBeNull
        sut.Counters.ShouldBeEmpty
    End Sub
    <Fact>
    Sub set_statistic()
        Dim sut As IEntity(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        Const value = 1.0
        sut.Statistic(StatisticType) = value
        sut.Statistic(StatisticType).ShouldBe(value)
        sut.Statistics.ShouldHaveSingleItem
        Const otherValue = 2.0
        sut.Statistic(StatisticType) = otherValue
        sut.Statistic(StatisticType).ShouldBe(otherValue)
        sut.Statistics.ShouldHaveSingleItem
        sut.Statistic(StatisticType) = Nothing
        sut.Statistic(StatisticType).ShouldBeNull
        sut.Statistics.ShouldBeEmpty
    End Sub
    <Fact>
    Sub retrieve_all_flags()
        Dim sut As IEntity(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        Const FirstFlagType = "first-flag"
        Const SecondFlagType = "second-flag"
        sut.Flag(FirstFlagType) = True
        sut.Flag(SecondFlagType) = True
        Dim actual = sut.Flags
        actual.Count.ShouldBe(2)
        actual.ShouldContain(FirstFlagType)
        actual.ShouldContain(SecondFlagType)
    End Sub
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
