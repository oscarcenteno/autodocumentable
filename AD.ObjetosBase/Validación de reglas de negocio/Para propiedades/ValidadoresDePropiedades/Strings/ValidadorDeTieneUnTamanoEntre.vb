
Class ValidadorDeTieneUnTamanoEntre
    Inherits ValidadorDePropiedad

    Private tamanoMinimo As Integer
    Private tamanoMaximo As Integer

    Public Sub New(tamanoMinimo As Integer, tamanoMaximo As Integer)
        Me.tamanoMinimo = tamanoMinimo
        Me.tamanoMaximo = tamanoMaximo
    End Sub

    Overrides Function EsValida(contexto As ContextoParaValidarUnaPropiedad) As Boolean
        Dim laValidacionEsValida As Boolean

        If contexto.ValorDeLaPropiedad Is Nothing Then
            laValidacionEsValida = False
        Else

            Dim valorComoString As String
            valorComoString = contexto.ValorDeLaPropiedad.ToString()
            Dim elTamano As Integer
            elTamano = valorComoString.Length

            If elTamano >= tamanoMinimo And elTamano <= tamanoMaximo Then
                laValidacionEsValida = True
            Else
                laValidacionEsValida = False
            End If
        End If

        Return laValidacionEsValida
    End Function

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe tener un tamaño entre {1} y {2} caracteres, inclusive."
            Return String.Format(laPlantilla,
                                 laRegla.NombreDeLaPropiedad,
                                 Me.tamanoMinimo,
                                 Me.tamanoMaximo)
        End Get
    End Property
End Class
