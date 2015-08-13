
Class ValidadorDeIniciaCon
    Inherits ValidadorDePropiedad

    Private textoConQueDebeIniciar As String

    Public Sub New(textoConQueDebeIniciar As Integer)
        Me.textoConQueDebeIniciar = textoConQueDebeIniciar
    End Sub

    Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim elTextoEsValido As Boolean

        If contexto.ValorDeLaPropiedad Is Nothing Then
            elTextoEsValido = False
        Else

            Dim elValorComoObjeto As Object = contexto.ValorDeLaPropiedad
            Dim valorComoString As String = elValorComoObjeto.ToString
            Dim iniciaConLoEsperado = valorComoString.StartsWith(textoConQueDebeIniciar)

            If iniciaConLoEsperado Then
                elTextoEsValido = True
            Else
                elTextoEsValido = False
            End If
        End If

        Return elTextoEsValido
    End Function

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe iniciar con este texto '{1}'."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, Me.textoConQueDebeIniciar)
        End Get
    End Property
End Class
