Public Class EntityRepository(Of TIdentifier)
    Implements IEntityRepository(Of TIdentifier)
    'Private ReadOnly entityCreator As Func(Of String, TIdentifier)
    'Private ReadOnly entityReader As Func(Of TIdentifier, IEntity(Of TIdentifier))
    'Private ReadOnly entityLister As Func(Of IEnumerable(Of TIdentifier))
    Private ReadOnly entityTable As New Dictionary(Of TIdentifier, IEntity(Of TIdentifier))
    Private ReadOnly store As IEntityStore(Of TIdentifier)

    Public Sub New(store As IEntityStore(Of TIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TIdentifier)) Implements IEntityRepository(Of TIdentifier).AllEntities
        Get
            Return entityTable.Values
        End Get
    End Property

    Public Function CreateEntity(entityType As String) As IEntity(Of TIdentifier) Implements IEntityRepository(Of TIdentifier).CreateEntity
        Dim result = New Entity(Of TIdentifier)(store.GetNextIdentifier, entityType)
        entityTable(result.Identifier) = result
        Return result
    End Function

    Public Function RetrieveEntity(identifier As TIdentifier) As IEntity(Of TIdentifier) Implements IEntityRepository(Of TIdentifier).RetrieveEntity
        Dim result As IEntity(Of TIdentifier) = Nothing
        If Not entityTable.TryGetValue(identifier, result) Then
            Return Nothing
        End If
        Return result
    End Function
End Class
