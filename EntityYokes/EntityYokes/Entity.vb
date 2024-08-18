Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Friend Sub New(entityType As String)
        Me.EntityType = entityType
    End Sub

    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property EntityType As String Implements IEntity(Of TIdentifier).EntityType
End Class
