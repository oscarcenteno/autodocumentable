Friend Class ValidadorDeExisteEnLaColeccion
    Inherits ValidadorDePropiedad

    Dim losValoresPermitidos As IEnumerable(Of Object)

    Public Sub New(losValoresPermitidos As IEnumerable(Of Object))
        Me.losValoresPermitidos = losValoresPermitidos
    End Sub

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe ser alguno de estos valores: {1}"
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad,
                                 losValoresPermitidos.ConviertaATexto())
        End Get
    End Property

    Public Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim existeEnLaLista As Boolean
        Dim elValor As Object
        elValor = contexto.ValorDeLaPropiedad
        existeEnLaLista = losValoresPermitidos.Any(Function(c) c.Equals(elValor))

        Return existeEnLaLista
    End Function
End Class
