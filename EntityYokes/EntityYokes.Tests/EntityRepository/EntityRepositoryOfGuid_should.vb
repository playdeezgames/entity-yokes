
Public Class EntityRepositoryOfGuid_should
    Inherits EntityRepository_should(Of Guid)

    Protected Overrides Function NextEntityIdentifier() As Guid
        Return Guid.NewGuid
    End Function
End Class
