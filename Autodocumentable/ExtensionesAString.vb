Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions

Module ExtensionesAString

    <Extension>
    Function SepareComoPascalCase(input As String) As String

        If (String.IsNullOrEmpty(input)) Then
            Return String.Empty
        End If

        Dim separadoEnPalabras = Regex.Replace(input, "([A-Z])", " $1").Trim()

        Return separadoEnPalabras
    End Function

    <Extension>
    Function SepareComoUnaOracion(elTexto As String) As String

        If (String.IsNullOrEmpty(elTexto)) Then
            Return String.Empty
        End If

        Dim lasPalabrasEnMinuscula As String = SepareEnPalabrasMinusculas(elTexto)
        Dim laOracion As String = ConviertaAOracion(lasPalabrasEnMinuscula)

        Return laOracion
    End Function

    Private Function SepareEnPalabrasMinusculas(elTexto As String) As String
        Dim lasPalabras = Regex.Replace(elTexto, "([A-Z])", " $1").Trim()
        Dim lasPalabrasEnMinuscula = lasPalabras.ToLower()

        Return lasPalabrasEnMinuscula
    End Function

    Private Function ConviertaAOracion(separadoEnMinuscula As String) As String
        Dim elConvertidor As New Regex("(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture)
        Dim elEvaluador As MatchEvaluator = Function(c) c.Value.ToUpper()
        Dim laOracion = elConvertidor.Replace(separadoEnMinuscula, elEvaluador)

        Return laOracion
    End Function

End Module
