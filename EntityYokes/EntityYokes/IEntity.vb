Public Interface IEntity
    Inherits IThingie
    ReadOnly Property EntityType As String
    Function CreateYoke(yokeType As String, yokedEntity As IEntity) As IYoke
    Sub Destroy()
    ReadOnly Property YokesFrom As IEnumerable(Of IYoke)
    ReadOnly Property YokesTo As IEnumerable(Of IYoke)
End Interface
