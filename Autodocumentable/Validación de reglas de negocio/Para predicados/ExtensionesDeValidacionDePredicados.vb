Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions

Public Module ExtensionesDeValidacionDePredicados

    <Extension> _
    Public Function SeCumple(Of T, TProperty)(elConfigurador As ConfiguradorDeReglas(Of T, Boolean)) _
    As ConfiguradorDeReglas(Of T, Boolean)

        elConfigurador.AgregarValidador(New ValidadorDeSeCumple())
        Return elConfigurador

    End Function

End Module
