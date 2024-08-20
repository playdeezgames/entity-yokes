Imports Shouldly
Imports Xunit

Public MustInherit Class Entity_should(Of TIdentifier)
    Const FlagType = "flag-type"
    <Fact>
    Sub initially_have_no_flags()
        Dim sut As IEntity(Of TIdentifier) = CreateSut()
        sut.Flags(FlagType).ShouldBeFalse
    End Sub
    <Fact>
    Sub set_a_flag()
        Dim sut As IEntity(Of TIdentifier) = CreateSut()
        sut.Flags(FlagType) = True
        sut.Flags(FlagType).ShouldBeTrue
    End Sub
    <Fact>
    Sub retrieve_all_flags()
        Dim sut As IEntity(Of TIdentifier) = CreateSut()
        Const FirstFlagType = "first-flag"
        Const SecondFlagType = "second-flag"
        sut.Flags(FirstFlagType) = True
        sut.Flags(SecondFlagType) = True
        Dim actual = sut.AllFlags
        actual.Count.ShouldBe(2)
        actual.ShouldContain(FirstFlagType)
        actual.ShouldContain(SecondFlagType)
    End Sub
    Const EntityType = "entity-type"
    Private Function CreateSut() As IEntity(Of TIdentifier)
        Return CreateRepository().CreateEntity(EntityType)
    End Function
    Private Function CreateRepository() As IEntityRepository(Of TIdentifier)
        Return New EntityRepository(Of TIdentifier)(CreateStore())
    End Function
    Protected MustOverride Function NextEntityIdentifier() As TIdentifier
    Private Function CreateStore() As IEntityStore(Of TIdentifier)
        Return New FakeEntityStore(Of TIdentifier)(AddressOf NextEntityIdentifier)
    End Function
End Class
