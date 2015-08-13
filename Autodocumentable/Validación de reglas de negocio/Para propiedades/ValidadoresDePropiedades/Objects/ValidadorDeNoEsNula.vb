
Class ValidadorDeNoEsNula
    Inherits ValidadorDePropiedad

    Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim laValidacionEsValida As Boolean

        If contexto.ValorDeLaPropiedad Is Nothing Then
            laValidacionEsValida = False
        Else
            laValidacionEsValida = True
        End If

        Return laValidacionEsValida
    End Function


    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' es requerido."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property
End Class
