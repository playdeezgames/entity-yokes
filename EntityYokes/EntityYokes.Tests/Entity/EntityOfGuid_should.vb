Public Class EntityOfGuid_should
    Inherits Entity_should(Of Guid, Guid)

    Protected Overrides Function NextEntityIdentifier() As Guid
        Return Guid.NewGuid
    End Function

    Protected Overrides Function NextYokeIdentifier() As Guid
        Return Guid.NewGuid
    End Function
End Class
