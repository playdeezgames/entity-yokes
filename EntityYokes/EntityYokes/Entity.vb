Friend Class Entity(Of TIdentifier)
    Implements IEntity(Of TIdentifier)

    Friend Sub New(store As IEntityStore(Of TIdentifier), identifier As TIdentifier)
        Me.store = store
        Me.Identifier = identifier
    End Sub

    Private ReadOnly store As IEntityStore(Of TIdentifier)
    Public ReadOnly Property Identifier As TIdentifier Implements IEntity(Of TIdentifier).Identifier

    Public ReadOnly Property EntityType As String Implements IEntity(Of TIdentifier).EntityType
        Get
            Return store.ReadEntityType(Identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IEntity(Of TIdentifier).Flag
        Get
            Return store.CheckEntityHasFlag(Identifier, flagType)
        End Get
        Set(value As Boolean)
            If value Then
                store.SetEntityFlag(Identifier, flagType)
            Else
                Throw New NotImplementedException
            End If
        End Set
    End Property

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IEntity(Of TIdentifier).Flags
        Get
            Return store.ListEntityFlags(Identifier)
        End Get
    End Property
End Class
