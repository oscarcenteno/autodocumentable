Imports System.Text.RegularExpressions

Class ValidadorDeEsAlfanumerica
    Inherits ValidadorDePropiedad

    Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim laValidacionEsValida As Boolean

        Dim expression As String
        expression = "^[a-z0-9]{3,16}$"
        Dim regex As New Regex(expression, RegexOptions.IgnoreCase)

        Dim valorComoString As String
        valorComoString = contexto.ValorDeLaPropiedad

        If Not regex.IsMatch(valorComoString) Then
            laValidacionEsValida = False
        Else
            laValidacionEsValida = True
        End If

        Return laValidacionEsValida
    End Function


    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "{0} debe contener dígitos y letras solamente."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property
End Class
