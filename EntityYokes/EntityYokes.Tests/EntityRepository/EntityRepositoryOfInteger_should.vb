
Public Class EntityRepositoryOfInteger_should
    Inherits EntityRepository_should(Of Integer, Integer)
    Private entityIdentifier As Integer = 0
    Private yokeIdentifier As Integer = 0

    Protected Overrides Function NextEntityIdentifier() As Integer
        Dim result = entityIdentifier
        entityIdentifier += 1
        Return result
    End Function

    Protected Overrides Function NextYokeIdentifier() As Integer
        Dim result = yokeIdentifier
        yokeIdentifier += 1
        Return result
    End Function
End Class
