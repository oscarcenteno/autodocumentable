Imports System.Runtime.CompilerServices

Public Module ExtensionesDeValidacionDePredicados

    <Extension> _
    Public Function SeCumple(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, Boolean)) _
        As ConfiguradorDeReglas(Of T, Boolean)

        Dim elValidador As New ValidadorDeSeCumple
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

End Module
