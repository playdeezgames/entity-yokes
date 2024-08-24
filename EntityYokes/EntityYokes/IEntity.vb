Public Interface IEntity(Of TEntityIdentifier, TYokeIdentifier)
    Inherits IThingie
    ReadOnly Property EntityType As String
    Function CreateYoke(yokeType As String, yokedEntity As IEntity(Of TEntityIdentifier, TYokeIdentifier)) As IYoke(Of TEntityIdentifier, TYokeIdentifier)
    Sub Destroy()
    ReadOnly Property YokesFrom As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
    ReadOnly Property YokesTo As IEnumerable(Of IYoke(Of TEntityIdentifier, TYokeIdentifier))
End Interface
