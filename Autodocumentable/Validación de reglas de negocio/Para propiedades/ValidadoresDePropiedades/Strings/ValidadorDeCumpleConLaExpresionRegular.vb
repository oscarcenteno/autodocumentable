Imports System.Text.RegularExpressions

Class ValidadorDeCumpleConLaExpresionRegular
    Inherits ValidadorDePropiedad

    Private laExpresion As String

    Sub New(laExpresion As String)
        Me.laExpresion = laExpresion
    End Sub

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe tener el formato esperado."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property

    Public Overrides Function EsValida(contexto As ContextoParaValidarUnaPropiedad) As Boolean
        Dim elTextoEsValido As Boolean

        Dim valorComoString As String
        valorComoString = contexto.ValorDeLaPropiedad

        Dim regex As New Regex(laExpresion, RegexOptions.IgnoreCase)

        If Not regex.IsMatch(valorComoString) Then
            elTextoEsValido = False
        Else
            elTextoEsValido = True
        End If

        Return elTextoEsValido
    End Function


End Class
