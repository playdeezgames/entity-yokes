Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Friend Sub New(store As IEntityStore(Of TIdentifier), identifier As TIdentifier, entityType As String)
        Me.store = store
        Me.Identifier = identifier
        Me.EntityType = entityType
    End Sub

    Private ReadOnly store As IEntityStore(Of TIdentifier)
    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier

    Public ReadOnly Property EntityType As String Implements IEntity(Of TIdentifier).EntityType
End Class
