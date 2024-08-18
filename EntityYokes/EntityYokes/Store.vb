Imports System.Runtime.InteropServices

Public Class Store(Of TIdentifier)
    Implements IStore(Of TIdentifier)

    Public Function CreateEntity() As IEntity(Of TIdentifier) Implements IStore(Of TIdentifier).CreateEntity
        Return New Entity(Of TIdentifier)()
    End Function
End Class
