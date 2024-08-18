Public Class Store
    Implements IStore

    Public Function CreateEntity() As IEntity(Of Integer) Implements IStore.CreateEntity
        Return New Entity(Of Integer)()
    End Function
End Class
