Imports Shouldly
Imports Xunit

Public MustInherit Class Yoke_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IYoke)
    Const FlagType = "flag-type"
    Const YokeType = "yoke-type"
    Const EntityType = "entity-type"
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
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Flag("flag-type") = True
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_metadata()
        Dim repository As IEntityRepository = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Metadata("metadata-type") = "value"
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_counter()
        Dim repository As IEntityRepository = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Counter("counter-type") = 1
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_statistic()
        Dim repository As IEntityRepository = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Statistic("statistic-type") = 1.0
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    Protected Overrides Function CreateSut() As IYoke
        Dim entity = CreateEntity()
        Return entity.CreateYoke(YokeType, entity)
    End Function
    Private Function CreateEntity() As IEntity
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
