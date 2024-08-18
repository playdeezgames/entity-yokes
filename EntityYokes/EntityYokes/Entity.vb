Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier
        Get
            Return CType(Nothing, TIdentifier)
        End Get
    End Property

    Public ReadOnly Property EntityType As String Implements IEntity(Of TIdentifier).EntityType
        Get
            Return "entity-type"
        End Get
    End Property
End Class
