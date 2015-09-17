Imports Autodocumentable.UnitTests

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
        Dim sonValidos As Boolean
        sonValidos = True

        For Each otroRango In losRangosDeOtrasCondiciones
            If LosPlazosSeTraslapan(laSolicitud.Plazos, otroRango) Then
                sonValidos = False
            End If
        Next

        Return sonValidos
    End Function

    Private Function LosPlazosSeTraslapan(unRango As RangoDePlazos, otroRango As RangoDePlazos) As Boolean
        Dim seTraslapan As Boolean
        seTraslapan = Not (unRango.Final < otroRango.Inicial Or
            otroRango.Final < unRango.Inicial)

        Return seTraslapan
    End Function
End Class

