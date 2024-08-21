Imports Shouldly
Imports Xunit

Public MustInherit Class Yoke_should(Of TEntityIdentifier, TYokeIdentifier)
    Inherits Thingie_should(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
    Const FlagType = "flag-type"
    Const YokeType = "yoke-type"
    Const EntityType = "entity-type"
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
