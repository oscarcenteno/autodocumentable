Namespace CondicionesFinancieras.Agregar

    <TestClass()> Public Class Valide_Tests
        Dim resultadoEsperado As Boolean
        Dim elInstrumento As New InstrumentoAsociado
        Dim losRangosDeOtrasCondiciones As New List(Of RangoDePlazos)
        Dim elValidador As Validador
        Dim laSolicitud As Solicitud
        Dim losPerfilesDelUsuario As Perfil
        Dim resultadoDeLaValidacion As ResultadoDeLaValidacion
        Dim resultadoObtenido As Boolean

        <TestInitialize> Public Sub InicializarPrueba()
            InicialiceElContextoDelValidador()
            InicialiceElValidador()
            InicialiceLaSolicitudBasica()
        End Sub

        Private Sub InicialiceElContextoDelValidador()
            elInstrumento.Periodicidades = Periodicidades.AlVencimiento

            Dim unRango As RangoDePlazos
            unRango = New RangoDePlazos With {.Inicial = 1, .Final = 5}
            losRangosDeOtrasCondiciones.Add(unRango)
            unRango = New RangoDePlazos With {.Inicial = 10, .Final = 13}
            losRangosDeOtrasCondiciones.Add(unRango)
            losPerfilesDelUsuario = Perfil.DigitadorDeCondicionesFinancieras
        End Sub

        Private Sub InicialiceElValidador()
            elValidador = New Validador(elInstrumento, losRangosDeOtrasCondiciones,
                                        losPerfilesDelUsuario)
        End Sub

        Private Sub InicialiceLaSolicitudBasica()
            laSolicitud = New Solicitud
            laSolicitud.Periodicidades = Periodicidades.AlVencimiento
            laSolicitud.Plazos = New RangoDePlazos With {.Inicial = 6, .Final = 9}
            laSolicitud.Nemotecnico = "DEP7000"
            laSolicitud.ValorEnTexto = "1234"
            laSolicitud.MultiplosDelMonto = 50
        End Sub

        <TestMethod()> Public Sub Valide_SolicitudEsValida_EsValida()
            resultadoEsperado = True

            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElRangoEsInvalido_NoEsValidaConErrores()
            resultadoEsperado = False

            laSolicitud.Plazos = New RangoDePlazos With {.Inicial = 1, .Final = 10}
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LaPeriodicidadEsInvalida_NoEsValidaConErrores()
            resultadoEsperado = False

            laSolicitud.Periodicidades = Periodicidades.Bimestral
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElNemotecnicoEsMasLargo_NoEsValida()
            resultadoEsperado = False

            laSolicitud.Nemotecnico = "12345678901234"
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_ElNemotecnicoNoEsAlfanumerico_NoEsValida()
            resultadoEsperado = False

            laSolicitud.Nemotecnico = "$12345678!"
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LaTasaEsInvalida_NoEsValida()
            resultadoEsperado = False

            laSolicitud.TasaBruta = 2
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

        <TestMethod()> Public Sub Valide_LosMultiplosDelMontoSonInvalidos_NoEsValida()
            resultadoEsperado = False

            laSolicitud.MultiplosDelMonto = 0
            resultadoDeLaValidacion = elValidador.Valide(laSolicitud)
            resultadoObtenido = resultadoDeLaValidacion.EsValida

            Assert.AreEqual(resultadoEsperado, resultadoObtenido)
        End Sub

    End Class

End Namespace

