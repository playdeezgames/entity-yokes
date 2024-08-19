Public Class Store(Of TIdentifier)
    Implements IStore(Of TIdentifier)
    Private ReadOnly nextIdentifier As Func(Of TIdentifier)
    Private ReadOnly entityTable As New Dictionary(Of TIdentifier, IEntity(Of TIdentifier))
    Public Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextIdentifier = nextIdentifier
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TIdentifier)) Implements IStore(Of TIdentifier).AllEntities
        Get
            Return entityTable.Values
        End Get
    End Property

    Public Function CreateEntity(entityType As String) As IEntity(Of TIdentifier) Implements IStore(Of TIdentifier).CreateEntity
        Dim result = New Entity(Of TIdentifier)(nextIdentifier(), entityType)
        entityTable(result.Identifier) = result
        Return result
    End Function
End Class
