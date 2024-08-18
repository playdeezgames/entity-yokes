Public Class StoreOfInteger_should
    Inherits Store_should(Of Integer)
    Private identifier As Integer = 0

    Protected Overrides Function NextIdentifier() As Integer
        Dim result = identifier
        identifier += 1
        Return result
    End Function
End Class
