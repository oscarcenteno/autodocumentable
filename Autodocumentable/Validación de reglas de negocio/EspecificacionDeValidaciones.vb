Imports System.Linq.Expressions

Public MustInherit Class EspecificacionDeValidaciones(Of T)

    Private lasReglas As New List(Of ReglaDeValidacion)

    Protected Function LaPropiedad(Of TProperty)(expresion As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        expresion.Guarda("La expresión de la propiedad no puede ser nula.")

        Dim regla As ReglaParaUnaPropiedad
        regla = ReglaParaUnaPropiedad.Cree(expresion)
        lasReglas.Add(regla)

        Dim configurador As New ConfiguradorDeReglas(Of T, TProperty)(regla)

        Return configurador
    End Function

    Protected Function SeCumpleQue(expresion As Expression(Of Func(Of T, Boolean))) _
        As ReglaParaUnaExpresion

        expresion.Guarda("La expresion no puede ser nula.")

        Dim regla As ReglaParaUnaExpresion
        regla = ReglaParaUnaExpresion.Cree(expresion)

        lasReglas.Add(regla)

        Return regla
    End Function

    Public Function Valide(laInstancia As T) As ResultadoDeLaValidacion
        Dim resultado As New ResultadoDeLaValidacion

        For Each unaRegla In lasReglas
            Dim mensajesDeError As IEnumerable(Of String)
            mensajesDeError = Evalue(unaRegla, laInstancia)
            resultado.AgregarErrores(mensajesDeError)
        Next

        Return resultado
    End Function

    Private Function Evalue(unaRegla As ReglaDeValidacion, laInstancia As T) _
        As IEnumerable(Of String)

        Dim mensajesDeError As New List(Of String)

        Try
            Dim resultadosDeLaValidacion As IEnumerable(Of String)
            resultadosDeLaValidacion = unaRegla.Valide(laInstancia)

            mensajesDeError.AddRange(resultadosDeLaValidacion)
        Catch ex As Exception
            mensajesDeError.AddRange(unaRegla.Descripciones)
        End Try

        Return mensajesDeError
    End Function

    Public ReadOnly Property Especificacion As String()
        Get
            Dim lasDescripciones As New List(Of String)

            For Each regla As ReglaDeValidacion In lasReglas
                lasDescripciones.AddRange(regla.Descripciones)
            Next

            Return lasDescripciones.ToArray
        End Get
    End Property

End Class
