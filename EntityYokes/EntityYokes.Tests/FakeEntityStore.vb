Friend Class FakeEntityStore(Of TIdentifier)
    Implements IEntityStore(Of TIdentifier)
    Private ReadOnly entityTypes As New Dictionary(Of TIdentifier, String)
    Private ReadOnly nextIdentifier As Func(Of TIdentifier)

    Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextIdentifier = nextIdentifier
    End Sub

    Public Function GetNextIdentifier() As TIdentifier Implements IEntityStore(Of TIdentifier).GetNextIdentifier
        Return nextIdentifier()
    End Function

    Public Function CreateEntity(entityType As String) As TIdentifier Implements IEntityStore(Of TIdentifier).CreateEntity
        Dim identifier = GetNextIdentifier()
        entityTypes(identifier) = entityType
    End Function

    Public Function ListEntities() As IEnumerable(Of TIdentifier) Implements IEntityStore(Of TIdentifier).ListEntities
        Return entityTypes.Keys
    End Function

    Public Function ReadEntityType(identifier As TIdentifier) As String Implements IEntityStore(Of TIdentifier).ReadEntityType
        Dim entityType As String = Nothing
        If Not entityTypes.TryGetValue(identifier, entityType) Then
            Return Nothing
        End If
        Return entityType
    End Function
End Class
