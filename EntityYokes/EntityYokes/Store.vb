Public Class Store
    Implements IStore

    Public Function CreateEntity() As IEntity Implements IStore.CreateEntity
        Return New Entity()
    End Function
End Class
