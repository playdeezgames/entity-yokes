Public Interface IYoke(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property YokeType As String
    ReadOnly Property FromEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property ToEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)
End Interface
