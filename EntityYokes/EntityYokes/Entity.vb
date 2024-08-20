﻿Friend Class Entity(Of TEntityIdentifier)
    Implements IEntity(Of TEntityIdentifier)

    Friend Sub New(store As IEntityStore(Of TEntityIdentifier), identifier As TEntityIdentifier)
        Me.store = store
        Me.Identifier = identifier
    End Sub

    Private ReadOnly store As IEntityStore(Of TEntityIdentifier)
    Public ReadOnly Property Identifier As TEntityIdentifier Implements IEntity(Of TEntityIdentifier).Identifier

    Public ReadOnly Property EntityType As String Implements IEntity(Of TEntityIdentifier).EntityType
        Get
            If Not store.DoesEntityExist(Identifier) Then
                Throw New NullReferenceException($"Entity with identifier `{Identifier}` does not exist")
            End If
            Return store.ReadEntityType(Identifier)
        End Get
    End Property

    Public Property Flag(flagType As String) As Boolean Implements IEntity(Of TEntityIdentifier).Flag
        Get
            Return store.CheckEntityHasFlag(Identifier, flagType)
        End Get
        Set(value As Boolean)
            If value Then
                store.SetEntityFlag(Identifier, flagType)
            Else
                store.ClearEntityFlag(Identifier, flagType)
            End If
        End Set
    End Property

    Public ReadOnly Property Flags As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier).Flags
        Get
            Return store.ListEntityFlags(Identifier)
        End Get
    End Property

    Public ReadOnly Property Metadatas As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier).Metadatas
        Get
            Return store.ListEntityMetadatas(Identifier)
        End Get
    End Property

    Public ReadOnly Property Counters As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier).Counters
        Get
            Return store.ListEntityCounters(Identifier)
        End Get
    End Property

    Public ReadOnly Property Statistics As IEnumerable(Of String) Implements IEntity(Of TEntityIdentifier).Statistics
        Get
            Return store.ListEntityStatistics(Identifier)
        End Get
    End Property

    Public Property Metadata(metadataType As String) As String Implements IEntity(Of TEntityIdentifier).Metadata
        Get
            Return store.ReadEntityMetadata(Identifier, metadataType)
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                store.WriteEntityMetadata(Identifier, metadataType, value)
            Else
                store.ClearEntityMetadata(Identifier, metadataType)
            End If
        End Set
    End Property

    Public Property Counter(counterType As String) As Integer? Implements IEntity(Of TEntityIdentifier).Counter
        Get
            Return store.ReadEntityCounter(Identifier, counterType)
        End Get
        Set(value As Integer?)
            If value IsNot Nothing Then
                store.WriteEntityCounter(Identifier, counterType, value.Value)
            Else
                store.ClearEntityCounter(Identifier, counterType)
            End If
        End Set
    End Property

    Public Property Statistic(statisticType As String) As Double? Implements IEntity(Of TEntityIdentifier).Statistic
        Get
            Return store.ReadEntityStatistic(Identifier, statisticType)
        End Get
        Set(value As Double?)
            If value IsNot Nothing Then
                store.WriteEntityStatistic(Identifier, statisticType, value.Value)
            Else
                store.ClearEntityStatistic(Identifier, statisticType)
            End If
        End Set
    End Property
End Class
