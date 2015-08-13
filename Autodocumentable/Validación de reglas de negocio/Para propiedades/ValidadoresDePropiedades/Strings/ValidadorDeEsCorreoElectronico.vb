Imports System.Text.RegularExpressions

Class ValidadorDeEsCorreoElectronico
    Inherits ValidadorDePropiedad

    Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim elTextoEsValido As Boolean

        Dim expression As String
        expression = "^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$"
        Dim regex As New Regex(expression, RegexOptions.IgnoreCase)

        Dim valorComoString As String
        valorComoString = contexto.ValorDeLaPropiedad

        If Not regex.IsMatch(valorComoString) Then
            elTextoEsValido = False
        Else
            elTextoEsValido = True
        End If

        Return elTextoEsValido
    End Function


    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe tener el formato correcto para un correo electrónico."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property
End Class
