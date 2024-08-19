﻿Public Class EntityRepository(Of TIdentifier)
    Implements IEntityRepository(Of TIdentifier)
    Private ReadOnly store As IEntityStore(Of TIdentifier)

    Public Sub New(store As IEntityStore(Of TIdentifier))
        Me.store = store
    End Sub

    Public ReadOnly Property AllEntities As IEnumerable(Of IEntity(Of TIdentifier)) Implements IEntityRepository(Of TIdentifier).AllEntities
        Get
            Return store.ListEntities.Select(Function(x) New Entity(Of TIdentifier)(store, x))
        End Get
    End Property

    Public Sub DestroyEntity(entity As IEntity(Of TIdentifier)) Implements IEntityRepository(Of TIdentifier).DestroyEntity
        store.DestroyEntity(entity.Identifier)
    End Sub

    Public Function CreateEntity(entityType As String) As IEntity(Of TIdentifier) Implements IEntityRepository(Of TIdentifier).CreateEntity
        Dim identifier = store.CreateEntity(entityType)
        Return New Entity(Of TIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntity(identifier As TIdentifier) As IEntity(Of TIdentifier) Implements IEntityRepository(Of TIdentifier).RetrieveEntity
        If Not store.DoesEntityExist(identifier) Then
            Return Nothing
        End If
        Return New Entity(Of TIdentifier)(store, identifier)
    End Function

    Public Function RetrieveEntitiesOfType(entityType As String) As IEnumerable(Of IEntity(Of TIdentifier)) Implements IEntityRepository(Of TIdentifier).RetrieveEntitiesOfType
        Return store.ListEntitiesOfType(entityType).Select(Function(x) New Entity(Of TIdentifier)(store, x))
    End Function
End Class
