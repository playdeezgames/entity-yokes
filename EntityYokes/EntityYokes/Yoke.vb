Friend Class Yoke
    Implements IYoke

    Public ReadOnly Property YokeType As String Implements IYoke.YokeType
        Get
            Return "yoke-type"
        End Get
    End Property
End Class
