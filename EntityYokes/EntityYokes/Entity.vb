Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier
        Get
            Return CType(Nothing, TIdentifier)
        End Get
    End Property
End Class
