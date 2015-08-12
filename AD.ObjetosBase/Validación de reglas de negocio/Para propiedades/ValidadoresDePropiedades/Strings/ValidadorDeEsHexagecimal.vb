Imports System.Text.RegularExpressions

Class ValidadorDeEsHexagecimal
    Inherits ValidadorDePropiedad

    Overrides Function EsValida(contexto As ContextoParaValidarUnaPropiedad) As Boolean
        Dim elTextoEsValido As Boolean

        Dim laExpresion As String
        laExpresion = "^[A-Fa-f0-9]+$"
        Dim elValidador As New ValidadorDeCumpleConLaExpresionRegular(laExpresion)

        elTextoEsValido = elValidador.EsValida(contexto)

        Return elTextoEsValido
    End Function


    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe ser un valor hexagecimal."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property
End Class
