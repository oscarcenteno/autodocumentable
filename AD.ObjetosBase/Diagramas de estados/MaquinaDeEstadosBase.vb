Public MustInherit Class MaquinaDeEstadosBase(Of Estado, Accion)

    Private transiciones As New List(Of Transicion(Of Estado, Accion))
    Private elEstadoActual As Estado

    Protected Sub New(estadoInicial As Estado)
        Me.elEstadoActual = estadoInicial
    End Sub

    Protected Function La(nombreDeLaTransicion As Accion) As TransicionFluida(Of Estado, Accion)
        Dim nuevaTransicion As New Transicion(Of Estado, Accion)
        nuevaTransicion.Nombre = nombreDeLaTransicion
        transiciones.Add(nuevaTransicion)

        Dim laTransicionFluida As New TransicionFluida(Of Estado, Accion)(nuevaTransicion)

        Return laTransicionFluida
    End Function

    Public ReadOnly Property EstadoActual As Estado
        Get
            Return Me.elEstadoActual
        End Get
    End Property

    Public Function SePermiteLa(accion As Accion) As Boolean
        Dim sePermite As Boolean
        sePermite = False

        Dim transicionEncontrada As Transicion(Of Estado, Accion)
        transicionEncontrada = EncuentreAccionSobreEstadoOrigen(accion, Me.elEstadoActual)

        Dim laTransicionExiste As Boolean
        laTransicionExiste = transicionEncontrada IsNot Nothing

        If laTransicionExiste Then
            sePermite = True
        End If

        Return sePermite
    End Function

    Public Sub ApliqueLa(accion As Accion)
        If SePermiteLa(accion) Then
            Dim laAccionPorAplicar As Transicion(Of Estado, Accion)
            laAccionPorAplicar = EncuentreAccionSobreEstadoOrigen(accion, Me.elEstadoActual)
            Me.elEstadoActual = laAccionPorAplicar.EstadoDestino
        Else
            Dim plantillaDelError As String
            plantillaDelError = "La acción {0} no se permite sobre el estado {1}"
            Dim elMensajeDeError As String
            elMensajeDeError = String.Format(plantillaDelError,
                                             accion.ToString,
                                             Me.elEstadoActual.ToString)
            Throw New Exception(elMensajeDeError)
        End If
    End Sub

    Private Function EncuentreAccionSobreEstadoOrigen(accion As Accion, estadoOrigen As Estado) As Transicion(Of Estado, Accion)
        Dim transicionEncontrada As Transicion(Of Estado, Accion) = Nothing

        For Each t In transiciones
            If t.Nombre.ToString.Equals(accion.ToString) _
                And t.EstadoOrigen.ToString.Equals(estadoOrigen.ToString) Then
                transicionEncontrada = t
            End If
        Next

        Return transicionEncontrada
    End Function

End Class