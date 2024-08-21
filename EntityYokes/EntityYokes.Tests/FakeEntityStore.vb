Friend Class FakeEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier)
    Private ReadOnly entityTypes As New Dictionary(Of TEntityIdentifier, String)
    Private ReadOnly entityFlags As New Dictionary(Of TEntityIdentifier, HashSet(Of String))
    Private ReadOnly entityMetadatas As New Dictionary(Of TEntityIdentifier, Dictionary(Of String, String))
    Private ReadOnly entityCounters As New Dictionary(Of TEntityIdentifier, Dictionary(Of String, Integer))
    Private ReadOnly entityStatistics As New Dictionary(Of TEntityIdentifier, Dictionary(Of String, Double))
    Private ReadOnly nextEntityIdentifier As Func(Of TEntityIdentifier)
    Private ReadOnly nextYokeIdentifier As Func(Of TYokeIdentifier)
    Private ReadOnly yokeData As New Dictionary(Of TYokeIdentifier, (YokeType As String, FromIdentifier As TEntityIdentifier, ToIdentifier As TEntityIdentifier))
    Private ReadOnly fromYokes As New Dictionary(Of TEntityIdentifier, HashSet(Of TYokeIdentifier))
    Private ReadOnly toYokes As New Dictionary(Of TEntityIdentifier, HashSet(Of TYokeIdentifier))

    Sub New(nextEntityIdentifier As Func(Of TEntityIdentifier), nextYokeIdentifier As Func(Of TYokeIdentifier))
        Me.nextEntityIdentifier = nextEntityIdentifier
        Me.nextYokeIdentifier = nextYokeIdentifier
    End Sub

    Public Sub DestroyEntity(identifier As TEntityIdentifier) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).DestroyEntity
        entityTypes.Remove(identifier)
    End Sub

    Public Sub SetEntityFlag(identifier As TEntityIdentifier, flagType As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).SetEntityFlag
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            flags = New HashSet(Of String)
            entityFlags(identifier) = flags
        End If
        flags.Add(flagType)
    End Sub

    Public Sub ClearEntityFlag(identifier As TEntityIdentifier, flagType As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ClearEntityFlag
        Dim flags As HashSet(Of String) = Nothing
        If entityFlags.TryGetValue(identifier, flags) Then
            flags.Remove(flagType)
        End If
    End Sub

    Public Sub WriteEntityMetadata(identifier As TEntityIdentifier, metadataType As String, value As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).WriteEntityMetadata
        Dim metadatas As Dictionary(Of String, String) = Nothing
        If Not entityMetadatas.TryGetValue(identifier, metadatas) Then
            metadatas = New Dictionary(Of String, String)
            entityMetadatas(identifier) = metadatas
        End If
        metadatas(metadataType) = value
    End Sub

    Public Sub WriteEntityCounter(identifier As TEntityIdentifier, counterType As String, value As Integer) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).WriteEntityCounter
        Dim counters As Dictionary(Of String, Integer) = Nothing
        If Not entityCounters.TryGetValue(identifier, counters) Then
            counters = New Dictionary(Of String, Integer)
            entityCounters(identifier) = counters
        End If
        counters(counterType) = value
    End Sub

    Public Sub WriteEntityStatistic(identifier As TEntityIdentifier, statisticType As String, value As Double) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).WriteEntityStatistic
        Dim statistics As Dictionary(Of String, Double) = Nothing
        If Not entityStatistics.TryGetValue(identifier, statistics) Then
            statistics = New Dictionary(Of String, Double)
            entityStatistics(identifier) = statistics
        End If
        statistics(statisticType) = value
    End Sub

    Public Sub ClearEntityMetadata(identifier As TEntityIdentifier, metadataType As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ClearEntityMetadata
        Dim metadatas As Dictionary(Of String, String) = Nothing
        If entityMetadatas.TryGetValue(identifier, metadatas) Then
            metadatas.Remove(metadataType)
        End If
    End Sub

    Public Sub ClearEntityCounter(identifier As TEntityIdentifier, counterType As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ClearEntityCounter
        Dim counters As Dictionary(Of String, Integer) = Nothing
        If entityCounters.TryGetValue(identifier, counters) Then
            counters.Remove(counterType)
        End If
    End Sub

    Public Sub ClearEntityStatistic(identifier As TEntityIdentifier, statisticType As String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ClearEntityStatistic
        Dim statistics As Dictionary(Of String, Double) = Nothing
        If entityStatistics.TryGetValue(identifier, statistics) Then
            statistics.Remove(statisticType)
        End If
    End Sub

    Public Function CreateEntity(entityType As String) As TEntityIdentifier Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).CreateEntity
        Dim identifier = nextEntityIdentifier()
        entityTypes(identifier) = entityType
        Return identifier
    End Function

    Public Function ListEntities() As IEnumerable(Of TEntityIdentifier) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntities
        Return entityTypes.Keys
    End Function

    Public Function ReadEntityType(identifier As TEntityIdentifier) As String Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadEntityType
        Return entityTypes(identifier)
    End Function

    Public Function DoesEntityExist(identifier As TEntityIdentifier) As Boolean Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).DoesEntityExist
        Return entityTypes.ContainsKey(identifier)
    End Function

    Public Function ListEntitiesOfType(entityType As String) As IEnumerable(Of TEntityIdentifier) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntitiesOfType
        Return entityTypes.Where(Function(x) x.Value = entityType).Select(Function(x) x.Key)
    End Function

    Public Function CheckEntityHasFlag(identifier As TEntityIdentifier, flagType As String) As Boolean Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).CheckEntityHasFlag
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            Return False
        End If
        Return flags.Contains(flagType)
    End Function

    Public Function ListEntityFlags(identifier As TEntityIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityFlags
        Dim flags As HashSet(Of String) = Nothing
        If Not entityFlags.TryGetValue(identifier, flags) Then
            Return Array.Empty(Of String)
        End If
        Return flags
    End Function

    Public Function ListEntityMetadatas(identifier As TEntityIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityMetadatas
        Dim metadatas As Dictionary(Of String, String) = Nothing
        If entityMetadatas.TryGetValue(identifier, metadatas) Then
            Return metadatas.Keys
        End If
        Return Array.Empty(Of String)
    End Function

    Public Function ListEntityCounters(identifier As TEntityIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityCounters
        Dim counters As Dictionary(Of String, Integer) = Nothing
        If entityCounters.TryGetValue(identifier, counters) Then
            Return counters.Keys
        End If
        Return Array.Empty(Of String)
    End Function

    Public Function ListEntityStatistics(identifier As TEntityIdentifier) As IEnumerable(Of String) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityStatistics
        Dim statistics As Dictionary(Of String, Double) = Nothing
        If entityStatistics.TryGetValue(identifier, statistics) Then
            Return statistics.Keys
        End If
        Return Array.Empty(Of String)
    End Function

    Public Function ReadEntityMetadata(identifier As TEntityIdentifier, metadataType As String) As String Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadEntityMetadata
        Dim metadatas As Dictionary(Of String, String) = Nothing
        Dim value As String = Nothing
        If entityMetadatas.TryGetValue(identifier, metadatas) AndAlso
            metadatas.TryGetValue(metadataType, value) Then
            Return value
        End If
        Return Nothing
    End Function

    Public Function ReadEntityCounter(identifier As TEntityIdentifier, counterType As String) As Integer? Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadEntityCounter
        Dim counters As Dictionary(Of String, Integer) = Nothing
        Dim value As Integer
        If entityCounters.TryGetValue(identifier, counters) AndAlso
            counters.TryGetValue(counterType, value) Then
            Return value
        End If
        Return Nothing
    End Function

    Public Function ReadEntityStatistic(identifier As TEntityIdentifier, statisticType As String) As Double? Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadEntityStatistic
        Dim statistics As Dictionary(Of String, Double) = Nothing
        Dim value As Double
        If entityStatistics.TryGetValue(identifier, statistics) AndAlso
            statistics.TryGetValue(statisticType, value) Then
            Return value
        End If
        Return Nothing
    End Function

    Public Function ListEntityYokesFrom(identifier As TEntityIdentifier) As IEnumerable(Of TYokeIdentifier) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityYokesFrom
        Dim yokes As HashSet(Of TYokeIdentifier) = Nothing
        If fromYokes.TryGetValue(identifier, yokes) Then
            Return yokes
        End If
        Return Array.Empty(Of TYokeIdentifier)
    End Function

    Public Function ListEntityYokesTo(identifier As TEntityIdentifier) As IEnumerable(Of TYokeIdentifier) Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ListEntityYokesTo
        Dim yokes As HashSet(Of TYokeIdentifier) = Nothing
        If toYokes.TryGetValue(identifier, yokes) Then
            Return yokes
        End If
        Return Array.Empty(Of TYokeIdentifier)
    End Function

    Public Function CreateYoke(yokeType As String, fromIdentifier As TEntityIdentifier, toIdentifier As TEntityIdentifier) As TYokeIdentifier Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).CreateYoke
        Dim identifier = nextYokeIdentifier()
        yokeData(identifier) = (yokeType, fromIdentifier, toIdentifier)
        AddYokeFrom(fromIdentifier, identifier)
        AddYokeTo(toIdentifier, identifier)
        Return identifier
    End Function

    Private Sub AddYokeTo(toIdentifier As TEntityIdentifier, identifier As TYokeIdentifier)
        Dim yokes As HashSet(Of TYokeIdentifier) = Nothing
        If Not toYokes.TryGetValue(toIdentifier, yokes) Then
            yokes = New HashSet(Of TYokeIdentifier)
            toYokes(toIdentifier) = yokes
        End If
        yokes.Add(identifier)
    End Sub

    Private Sub AddYokeFrom(fromIdentifier As TEntityIdentifier, identifier As TYokeIdentifier)
        Dim yokes As HashSet(Of TYokeIdentifier) = Nothing
        If Not fromYokes.TryGetValue(fromIdentifier, yokes) Then
            yokes = New HashSet(Of TYokeIdentifier)
            fromYokes(fromIdentifier) = yokes
        End If
        yokes.Add(identifier)
    End Sub

    Public Function ReadYokeType(identifier As TYokeIdentifier) As String Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadYokeType
        Return yokeData(identifier).YokeType
    End Function

    Public Function ReadYokeFromIdentifier(identifier As TYokeIdentifier) As TEntityIdentifier Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadYokeFromIdentifier
        Return yokeData(identifier).FromIdentifier
    End Function

    Public Function ReadYokeToIdentifier(identifier As TYokeIdentifier) As TEntityIdentifier Implements IEntityStore(Of TEntityIdentifier, TYokeIdentifier).ReadYokeToIdentifier
        Return yokeData(identifier).ToIdentifier
    End Function
End Class
