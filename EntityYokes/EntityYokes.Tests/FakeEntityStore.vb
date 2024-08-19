Friend Class FakeEntityStore(Of TIdentifier)
    Implements IEntityStore(Of TIdentifier)

    Private ReadOnly nextIdentifier As Func(Of TIdentifier)

    Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextIdentifier = nextIdentifier
    End Sub

    Public Function GetNextIdentifier() As TIdentifier Implements IEntityStore(Of TIdentifier).GetNextIdentifier
        Return nextIdentifier()
    End Function
End Class
