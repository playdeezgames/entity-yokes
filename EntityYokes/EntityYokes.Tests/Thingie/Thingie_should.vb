Imports Shouldly
Imports Xunit

Public MustInherit Class Thingie_should(Of TThingie As IThingie)
    Const FlagType = "flag-type"
    Const MetadataType = "metadata-type"
    Const CounterType = "counter-type"
    Const StatisticType = "statistic-type"
    <Fact>
    Sub initially_have_no_flags()
        Dim sut As TThingie = CreateSut()
        sut.Flags.ShouldBeEmpty
        sut.Flag(FlagType).ShouldBeFalse
    End Sub
    <Fact>
    Sub initially_have_no_metadatas()
        Dim sut As TThingie = CreateSut()
        sut.Metadatas.ShouldBeEmpty
        sut.Metadata(MetadataType).ShouldBeNull
    End Sub
    <Fact>
    Sub initially_have_no_counters()
        Dim sut As TThingie = CreateSut()
        sut.Counters.ShouldBeEmpty
        sut.Counter(CounterType).ShouldBeNull
    End Sub
    <Fact>
    Sub initially_have_no_statistics()
        Dim sut As TThingie = CreateSut()
        sut.Statistics.ShouldBeEmpty
        sut.Statistic(StatisticType).ShouldBeNull
    End Sub
    Protected MustOverride Function CreateSut() As TThingie
End Class
