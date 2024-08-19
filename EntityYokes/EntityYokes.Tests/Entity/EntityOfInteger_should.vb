Public Class EntityOfInteger_should
    Inherits Entity_should(Of Integer)
    Private identifier As Integer = 0

    Protected Overrides Function NextEntityIdentifier() As Integer
        Dim result = identifier
        identifier += 1
        Return result
    End Function
End Class
