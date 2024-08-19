
Public Class EntityRepositoryOfGuid_should
    Inherits EntityRepository_should(Of Guid)

    Protected Overrides Function NextIdentifier() As Guid
        Return Guid.NewGuid
    End Function
End Class
