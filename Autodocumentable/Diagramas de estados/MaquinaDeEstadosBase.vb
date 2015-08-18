Public MustInherit Class MaquinaDeEstadosBase(Of Estado, Accion)

    Private transiciones As New List(Of Transicion(Of Estado, Accion))
    Private elEstadoActual As Estado

    Protected Sub New(estadoInicial As Estado)
        Me.elEstadoActual = estadoInicial
    End Sub

    Protected Function La(laTransicion As Accion) As TransicionFluida(Of Estado, Accion)
        Dim laNuevaTransicion As New Transicion(Of Estado, Accion)
        laNuevaTransicion.Nombre = laTransicion
        transiciones.Add(laNuevaTransicion)

        Dim laTransicionFluida As New TransicionFluida(Of Estado, Accion)(laNuevaTransicion)

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
        transicionEncontrada = EncuentreLaAccionSobreUnEstado(accion, elEstadoActual)

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
            laAccionPorAplicar = EncuentreLaAccionSobreUnEstado(accion, elEstadoActual)
            elEstadoActual = laAccionPorAplicar.EstadoDestino
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

    Private Function EncuentreLaAccionSobreUnEstado(laAccion As Accion,
                                                    elEstado As Estado) _
                                                  As Transicion(Of Estado, Accion)
        Dim latransicionEncontrada As Transicion(Of Estado, Accion) = Nothing

        For Each unaTransicion In transiciones
            Dim laAccionEsIgual As Boolean
            laAccionEsIgual = unaTransicion.Nombre.ToString.Equals(laAccion.ToString)
            Dim elEstadoEsIgual As Boolean
            elEstadoEsIgual = unaTransicion.EstadoOrigen.ToString.Equals(elEstado.ToString)

            If laAccionEsIgual And elEstadoEsIgual Then
                latransicionEncontrada = unaTransicion
            End If
        Next

        Return latransicionEncontrada
    End Function

End Class