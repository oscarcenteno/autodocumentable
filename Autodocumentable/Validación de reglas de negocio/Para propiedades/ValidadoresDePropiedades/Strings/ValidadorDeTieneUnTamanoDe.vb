
Class ValidadorDeTieneUnTamanoDe
    Inherits ValidadorDePropiedad

    Private tamanoExacto As Integer

    Public Sub New(tamanoExacto As Integer)
        Me.tamanoExacto = tamanoExacto
    End Sub

    Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim elTextoEsValido As Boolean

        If contexto.ValorDeLaPropiedad Is Nothing Then
            elTextoEsValido = False
        Else

            Dim elValorComoObjeto As Object = contexto.ValorDeLaPropiedad
            Dim valorComoString As String = elValorComoObjeto.ToString
            Dim elTamano As Integer = valorComoString.Length

            If elTamano = tamanoExacto Then
                elTextoEsValido = True
            Else
                elTextoEsValido = False
            End If
        End If

        Return elTextoEsValido
    End Function

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe tener un tamaño de exactamente {1} caracteres."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, Me.tamanoExacto)
        End Get
    End Property
End Class
