Public Interface IYoke
    Inherits IThingie
    ReadOnly Property YokeType As String
    ReadOnly Property FromEntity As IEntity
    ReadOnly Property ToEntity As IEntity
    Sub Destroy()
End Interface
