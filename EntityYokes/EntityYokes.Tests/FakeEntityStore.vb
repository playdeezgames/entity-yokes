Friend Class FakeEntityStore(Of TIdentifier)
    Implements IEntityStore(Of TIdentifier)
    Private ReadOnly entityTypes As New Dictionary(Of TIdentifier, String)
    Private ReadOnly nextEntityIdentifier As Func(Of TIdentifier)

    Sub New(nextIdentifier As Func(Of TIdentifier))
        Me.nextEntityIdentifier = nextIdentifier
    End Sub

    Public Function CreateEntity(entityType As String) As TIdentifier Implements IEntityStore(Of TIdentifier).CreateEntity
        Dim identifier = nextEntityIdentifier()
        entityTypes(identifier) = entityType
        Return identifier
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

    Public Function EntityExists(identifier As TIdentifier) As Boolean Implements IEntityStore(Of TIdentifier).EntityExists
        Return entityTypes.ContainsKey(identifier)
    End Function

    Public Function EntitiesOfType(entityType As String) As IEnumerable(Of TIdentifier) Implements IEntityStore(Of TIdentifier).EntitiesOfType
        Return entityTypes.Where(Function(x) x.Value = entityType).Select(Function(x) x.Key)
    End Function
End Class
