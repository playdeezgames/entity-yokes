Imports Shouldly
Imports Xunit

Public MustInherit Class Yoke_should(Of TEntityIdentifier, TYokeIdentifier)
    Const FlagType = "flag-type"
    Const YokeType = "yoke-type"
    <Fact>
    Sub initially_have_no_flags()
        Dim sut As IYoke(Of TEntityIdentifier, TYokeIdentifier) = CreateSut()
        sut.Flags.ShouldBeEmpty
        sut.Flag(FlagType).ShouldBeFalse
    End Sub
    Const EntityType = "entity-type"
    Private Function CreateSut() As IYoke(Of TEntityIdentifier, TYokeIdentifier)
        Dim entity = CreateEntity()
        Return entity.CreateYoke(yokeType, entity)
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
