Public Interface IYoke(Of TEntityIdentifier, TYokeIdentifier)
    Inherits IThingie
    ReadOnly Property YokeType As String
    ReadOnly Property FromEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)
    ReadOnly Property ToEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)
    Sub Destroy()
End Interface
