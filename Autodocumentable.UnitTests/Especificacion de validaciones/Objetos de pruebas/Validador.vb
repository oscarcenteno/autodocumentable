Public Class Validador
    Inherits EspecificacionDeValidaciones(Of Solicitud)

    Private elInstrumento As InstrumentoAsociado
    Private losRangosDeOtrasCondiciones As IList(Of RangoDePlazos)
    Private losPerfilesDelUsuario As Perfil

    Public Sub New(elInstrumento As InstrumentoAsociado,
                   losRangosDeOtrasCondiciones As IList(Of RangoDePlazos),
                   losPerfilesDelUsuario As Perfil)

        Me.elInstrumento = elInstrumento
        Me.losRangosDeOtrasCondiciones = losRangosDeOtrasCondiciones

        EspecifiqueLasReglasDeValidacion()
        EspecifiqueLasReglasDePropiedades()
    End Sub

    Private Sub EspecifiqueLasReglasDePropiedades()
        LaPropiedad(Function(laSolicitud) laSolicitud.Nemotecnico).
            EsAlfanumerica().
            MideEntre(4, 10)

        LaPropiedad(Function(laSolicitud) laSolicitud.TasaBruta).
            EsMayorOIgualQue(0).
            EsMenorOIgualQue(1)

        LaPropiedad(Function(laSolicitud) laSolicitud.MultiplosDelMonto).
            EsMayorOIgualQue(0.01)

        Dim losValoresPermitidos As String()
        losValoresPermitidos = {"1234", "4564", "7894"}
        LaPropiedad(Function(laSolicitud) laSolicitud.ValorEnTexto).
            ExisteEnLaColeccion(losValoresPermitidos)
    End Sub

    Private Sub EspecifiqueLasReglasDeValidacion()
        SeCumpleQue(Function(laSolicitud) LaPeriodicidadDebeSerAcordeAlInstrumento(laSolicitud))
        SeCumpleQue(Function(laSolicitud) LosPlazosNoSeDebenRepetirDentroDelInstrumento(laSolicitud))
    End Sub

    Public Function LaPeriodicidadDebeSerAcordeAlInstrumento(laSolicitud As Solicitud) As Boolean
        Dim esValida As Boolean
        esValida = elInstrumento.Periodicidades.HasFlag(laSolicitud.Periodicidades)

        Return esValida
    End Function

    Public Function LosPlazosNoSeDebenRepetirDentroDelInstrumento(laSolicitud As Solicitud) As Boolean

        Dim elPlazo As RangoDePlazos
        elPlazo = laSolicitud.Plazos

        Dim sonValidos As Boolean
        If AlgunoSeTraslapaCon(elPlazo) Then
            sonValidos = False
        Else
            sonValidos = True
        End If

        Return sonValidos
    End Function

    Private Function AlgunoSeTraslapaCon(elPlazo As RangoDePlazos) As Boolean
        Dim unoSeTraslapa As Boolean

        For Each otroRango In losRangosDeOtrasCondiciones
            If elPlazo.SeTraslapaCon(otroRango) Then
                unoSeTraslapa = True
            End If
        Next

        Return unoSeTraslapa
    End Function
End Class

