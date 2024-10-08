﻿Imports Shouldly
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
    Sub set_a_flag()
        Dim sut As TThingie = CreateSut()
        sut.Flag(FlagType) = True
        sut.Flag(FlagType).ShouldBeTrue
        sut.Flags.ShouldHaveSingleItem
    End Sub
    <Fact>
    Sub clear_a_flag()
        Dim sut As TThingie = CreateSut()
        sut.Flag(FlagType) = True
        sut.Flag(FlagType).ShouldBeTrue
        sut.Flag(FlagType) = False
        sut.Flag(FlagType).ShouldBeFalse
        sut.Flags.ShouldBeEmpty
    End Sub
    <Fact>
    Sub retrieve_all_flags()
        Dim sut As TThingie = CreateSut()
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
    Sub initially_have_no_metadatas()
        Dim sut As TThingie = CreateSut()
        sut.Metadatas.ShouldBeEmpty
        sut.Metadata(MetadataType).ShouldBeNull
    End Sub
    <Fact>
    Sub set_metadata()
        Dim sut As TThingie = CreateSut()
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
    Sub initially_have_no_counters()
        Dim sut As TThingie = CreateSut()
        sut.Counters.ShouldBeEmpty
        sut.Counter(CounterType).ShouldBeNull
    End Sub
    <Fact>
    Sub set_counter()
        Dim sut As TThingie = CreateSut()
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
    Sub initially_have_no_statistics()
        Dim sut As TThingie = CreateSut()
        sut.Statistics.ShouldBeEmpty
        sut.Statistic(StatisticType).ShouldBeNull
    End Sub
    <Fact>
    Sub set_statistic()
        Dim sut As TThingie = CreateSut()
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
    Protected MustOverride Function CreateSut() As TThingie
End Class
