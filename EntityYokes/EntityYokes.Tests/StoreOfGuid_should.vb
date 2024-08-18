Public Class StoreOfGuid_should
    Inherits Store_should(Of Guid)

    Protected Overrides Function NextIdentifier() As Guid
        Return Guid.NewGuid
    End Function
End Class
