Public Class TransicionFluida(Of Estado, Accion)

    Private laTransicion As Transicion(Of Estado, Accion)

    Public Sub New(laTransicion As Transicion(Of Estado, Accion))
        Me.laTransicion = laTransicion
    End Sub

    Public Function VaDesdeEl(elEstado As Estado) As TransicionFluida(Of Estado, Accion)
        laTransicion.EstadoOrigen = elEstado
        Return Me
    End Function

    Public Sub HastaEl(estadoDestino As Estado)
        laTransicion.EstadoDestino = estadoDestino
    End Sub

End Class