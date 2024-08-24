Imports Shouldly
Imports Xunit

Public MustInherit Class Yoke_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
    Const FlagType = "flag-type"
    Const YokeType = "yoke-type"
    Const EntityType = "entity-type"
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
        Dim repository As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Flag("flag-type") = True
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_metadata()
        Dim repository As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Metadata("metadata-type") = "value"
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_counter()
        Dim repository As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Counter("counter-type") = 1
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    <Fact>
    Sub not_destroy_entities_with_statistic()
        Dim repository As IEntityRepository(Of TEntityIdentifier, TYokeIdentifier) = CreateRepository()
        Dim entity = repository.CreateEntity(EntityType)
        Dim sut = entity.CreateYoke(YokeType, entity)
        sut.Statistic("statistic-type") = 1.0
        Should.Throw(Of InvalidOperationException)(Sub() sut.Destroy())
    End Sub
    Protected Overrides Function CreateSut() As IYoke(Of TEntityIdentifier, TYokeIdentifier)
        Dim entity = CreateEntity()
        Return entity.CreateYoke(YokeType, entity)
    End Function
    Private Function CreateEntity() As IEntity(Of TEntityIdentifier, TYokeIdentifier)
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
