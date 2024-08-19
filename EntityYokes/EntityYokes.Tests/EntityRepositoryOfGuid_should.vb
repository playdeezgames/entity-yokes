
Public Class EntityRepositoryOfGuid_should
    Inherits EntityRepository_should(Of Guid)

    Protected Overrides Function NextIdentifier() As Guid
        Return Guid.NewGuid
    End Function

    Protected Overrides Function CreateStore() As IEntityStore(Of Guid)
        Return Nothing
    End Function
End Class
