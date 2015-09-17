Namespace CondicionesFinancieras.Agregar

    <TestClass()> Public Class Valide_Tests
        Dim elResultadoEsperado As Boolean
        Dim elValidador As Validador
        Dim laSolicitud As Solicitud
        Dim elResultadoDeLaValidacion As ResultadoDeLaValidacion
        Dim elResultadoObtenido As Boolean

        <TestInitialize> Public Sub InicializarPrueba()
            InicialiceElContextoDelValidador()
            InicialiceLaSolicitudBasica()
        End Sub

        Private Sub InicialiceElContextoDelValidador()
            Dim elInstrumento As New InstrumentoAsociado
            elInstrumento.Periodicidades = Periodicidades.AlVencimiento

            Dim losRangosDeOtrasCondiciones As IEnumerable(Of RangoDePlazos)
            losRangosDeOtrasCondiciones = InicialiceRangos()

            Dim losPerfilesDelUsuario As Perfil
            losPerfilesDelUsuario = Perfil.DigitadorDeCondicionesFinancieras

            elValidador = New Validador(elInstrumento, losRangosDeOtrasCondiciones,
                                        losPerfilesDelUsuario)
        End Sub

        Private Function InicialiceRangos() As IEnumerable(Of RangoDePlazos)
            Dim losRangosDeOtrasCondiciones As New List(Of RangoDePlazos)

            Dim unRango As New RangoDePlazos(1, 5)
            losRangosDeOtrasCondiciones.Add(unRango)

            unRango = New RangoDePlazos(10, 13)
            losRangosDeOtrasCondiciones.Add(unRango)

            Return losRangosDeOtrasCondiciones
        End Function

        Private Sub InicialiceLaSolicitudBasica()
            laSolicitud = New Solicitud
            laSolicitud.Periodicidades = Periodicidades.AlVencimiento
            laSolicitud.Plazos = New RangoDePlazos(6, 9)
            laSolicitud.Nemotecnico = "DEP7000"
            laSolicitud.ValorEnTexto = "1234"
            laSolicitud.MultiplosDelMonto = 50
        End Sub

        <TestMethod()> Public Sub Valide_SolicitudEsValida_EsValida()
            elResultadoEsperado = True

            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElRangoEsInvalido_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.Plazos = New RangoDePlazos(1, 10)
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LaPeriodicidadEsInvalida_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.Periodicidades = Periodicidades.Bimestral
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElNemotecnicoEsMasLargo_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.Nemotecnico = "12345678901234"
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElNemotecnicoNoEsAlfanumerico_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.Nemotecnico = "$12345678!"
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LaTasaEsInvalida_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.TasaBruta = 2
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LosMultiplosDelMontoSonInvalidos_NoEsValida()
            elResultadoEsperado = False

            laSolicitud.MultiplosDelMonto = 0
            elResultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            elResultadoObtenido = elResultadoDeLaValidacion.EsValida

            Assert.AreEqual(elResultadoEsperado, elResultadoObtenido)
        End Sub

    End Class

End Namespace

