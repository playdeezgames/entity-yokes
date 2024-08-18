Public Class Store(Of TIdentifier)
    Implements IStore(Of TIdentifier)
    Private ReadOnly nextIdentifier As Func(Of TIdentifier)
    Public Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextIdentifier = nextIdentifier
    End Sub

    Public Function CreateEntity(entityType As String) As IEntity(Of TIdentifier) Implements IStore(Of TIdentifier).CreateEntity
        Return New Entity(Of TIdentifier)(nextIdentifier(), entityType)
    End Function
End Class
