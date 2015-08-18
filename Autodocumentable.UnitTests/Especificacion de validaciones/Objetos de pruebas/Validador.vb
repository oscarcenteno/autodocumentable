Public Class Validador
    Inherits EspecificacionDeValidaciones(Of Solicitud)

    Private elInstrumento As InstrumentoAsociado
    Private rangosDeOtrasCondiciones As IList(Of RangoDePlazos)
    Private perfilesDelUsuario As Perfil

    Public Sub New(elInstrumento As InstrumentoAsociado,
                       rangosDeOtrasCondiciones As IList(Of RangoDePlazos),
                       perfilesDelUsuario As Perfil)

        Me.elInstrumento = elInstrumento
        Me.rangosDeOtrasCondiciones = rangosDeOtrasCondiciones

        EspecifiqueLasReglasDeValidacion()
        EspecifiqueLasReglasDePropiedades()
    End Sub

    Private Sub EspecifiqueLasReglasDePropiedades()
        LaPropiedad(Function(laSolicitud) laSolicitud.Nemotecnico).EsAlfanumerica()
        LaPropiedad(Function(laSolicitud) laSolicitud.Nemotecnico).MideEntre(4, 10)
        LaPropiedad(Function(laSolicitud) laSolicitud.TasaBruta).
            EsMayorOIgualQue(0).
            EsMenorOIgualQue(1)
        LaPropiedad(Function(laSolicitud) laSolicitud.MultiplosDelMonto) _
            .EsMayorOIgualQue(0.01)

        Dim losValoresPermitidos As String()
        losValoresPermitidos = {"1234", "4564", "7894"}

        LaPropiedad(Function(laSolicitud) laSolicitud.ValorEnTexto) _
            .ExisteEnLaColeccion(losValoresPermitidos)

    End Sub

    Private Sub EspecifiqueLasReglasDeValidacion()
        SeCumpleQue(Function(laSolicitud) LaPeriodicidadDebeSerValida(laSolicitud)) _
                .ConLaDescripcion("La periodicidad debe ser acorde al instrumento.")
    End Sub

    Public Function LaPeriodicidadDebeSerValida(laSolicitud As Solicitud) As Boolean
        Dim esValida As Boolean
        esValida = elInstrumento.Periodicidades.HasFlag(laSolicitud.Periodicidades)

        Return esValida
    End Function

End Class

