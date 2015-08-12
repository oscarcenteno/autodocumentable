﻿
Class ValidadorDeSeCumple
    Inherits ValidadorDePropiedad

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "{0}"
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property

    Public Overrides Function EsValida(contexto As ContextoParaValidarUnaPropiedad) As Boolean
        Return contexto.ValorDeLaPropiedad
    End Function
End Class
