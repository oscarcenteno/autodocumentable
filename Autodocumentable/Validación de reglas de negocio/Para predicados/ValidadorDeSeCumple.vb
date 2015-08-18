
Class ValidadorDeSeCumple
    Inherits ValidadorDePropiedad

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "{0}"
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property

    Public Overrides Function EsValida(elContexto As ContextoDeUnaPropiedad) As Boolean
        Return elContexto.ValorDeLaPropiedad
    End Function
End Class
