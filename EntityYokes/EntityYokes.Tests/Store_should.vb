Imports Shouldly
Imports Xunit

Public Class Store_should
    <Fact>
    Sub create_entity()
        Dim sut As IStore = CreateSut()
        Dim actual = sut.CreateEntity()
        actual.ShouldNotBeNull()
    End Sub
    Private Function CreateSut() As IStore
        Return New Store()
    End Function
End Class

