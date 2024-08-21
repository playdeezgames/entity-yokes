Public Class YokeOfGuid_should
    Inherits Yoke_should(Of Guid, Guid)

    Protected Overrides Function NextEntityIdentifier() As Guid
        Return Guid.NewGuid
    End Function

    Protected Overrides Function NextYokeIdentifier() As Guid
        Return Guid.NewGuid
    End Function
End Class
