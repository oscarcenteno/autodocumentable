Public Class RangoDePlazos
    Private elInicial As Integer
    Private elFinal As Integer

    Public Sub New(elInicial As Integer, elFinal As Integer)
        Me.elInicial = elInicial
        Me.elFinal = elFinal
    End Sub

    Public ReadOnly Property Inicial As Integer
        Get
            Return elInicial
        End Get
    End Property

    Public ReadOnly Property Final As Integer
        Get
            Return elFinal
        End Get
    End Property

    Public Function SeTraslapaCon(otroRango As RangoDePlazos)
        Dim seTraslapan As Boolean

        If elFinal < otroRango.Inicial Or otroRango.Final < elInicial Then
            seTraslapan = False
        Else
            seTraslapan = True
        End If

        Return seTraslapan
    End Function
End Class

