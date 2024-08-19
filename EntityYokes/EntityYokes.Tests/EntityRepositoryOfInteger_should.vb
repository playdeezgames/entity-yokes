
Public Class EntityRepositoryOfInteger_should
    Inherits EntityRepository_should(Of Integer)
    Private identifier As Integer = 0

    Protected Overrides Function NextIdentifier() As Integer
        Dim result = identifier
        identifier += 1
        Return result
    End Function

    Protected Overrides Function CreateStore() As IEntityStore(Of Integer)
        Return Nothing
    End Function
End Class
