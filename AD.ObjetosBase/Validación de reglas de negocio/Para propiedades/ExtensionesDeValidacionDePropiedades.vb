Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions

Public Module ExtensionesDeValidacionDePropiedades

    <Extension> _
    Public Sub NoEsNula(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty))

        elConfigurador.AgregarValidador(New ValidadorDeNoEsNula())
    End Sub

    <Extension> _
    Public Sub NoEsNulaOVacia(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty))

        elConfigurador.AgregarValidador(New ValidadorDeNoEsNulaOVacia())
    End Sub

    <Extension> _
    Public Function TieneUnTamanoDe(Of T)(elConfigurador As ConfiguradorDeReglas(Of T, String),
                                          tamanoExacto As Integer) _
    As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeTieneUnTamanoDe(tamanoExacto))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function TieneUnTamanoEntre(Of T)(elConfigurador As ConfiguradorDeReglas(Of T, String),
                                             tamanoMinimo As Integer, tamanoMaximo As Integer) _
    As ConfiguradorDeReglas(Of T, String)
        elConfigurador.AgregarValidador(New ValidadorDeTieneUnTamanoEntre(tamanoMinimo,
                                                                          tamanoMaximo))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsCorreoElectronico(Of T)(elConfigurador As ConfiguradorDeReglas(Of T, String)) _
    As ConfiguradorDeReglas(Of T, String)
        elConfigurador.AgregarValidador(New ValidadorDeEsCorreoElectronico())
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMayorQue(Of T, TProperty As {IComparable(Of TProperty), IComparable}) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsMayorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMayorQue(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     expression As Expression(Of Func(Of T, TProperty))) As ConfiguradorDeReglas(Of T, TProperty)

        Dim func = expression.Compile()
        elConfigurador.AgregarValidador(New ValidadorDeEsMayorQue(func.TransformadaANoGenerica(),
                                                                        expression.GetMember()))

        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMenorQue(Of T, TProperty As {IComparable(Of TProperty), IComparable}) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsMenorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMenorQue(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     expression As Expression(Of Func(Of T, TProperty))) As ConfiguradorDeReglas(Of T, TProperty)

        Dim func = expression.Compile()
        elConfigurador.AgregarValidador(New ValidadorDeEsMenorQue(func.TransformadaANoGenerica(),
                                                                        expression.GetMember()))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualQue(Of T, TProperty As {IComparable(Of TProperty), IComparable}) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsIgualQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualQue(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     expression As Expression(Of Func(Of T, TProperty))) As ConfiguradorDeReglas(Of T, TProperty)

        Dim func = expression.Compile()
        elConfigurador.AgregarValidador(New ValidadorDeEsIgualQue(func.TransformadaANoGenerica(),
                                                                        expression.GetMember()))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMayorQue(Of T, TProperty As {IComparable(Of TProperty), IComparable}) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsIgualOMayorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMayorQue(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     expression As Expression(Of Func(Of T, TProperty))) As ConfiguradorDeReglas(Of T, TProperty)

        Dim func = expression.Compile()
        elConfigurador.AgregarValidador(
            New ValidadorDeEsIgualOMayorQue(func.TransformadaANoGenerica(),
                                            expression.GetMember()))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMenorQue(Of T, TProperty As {IComparable(Of TProperty), IComparable}) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty), valorPorComparar As TProperty) _
    As ConfiguradorDeReglas(Of T, TProperty)
        elConfigurador.AgregarValidador(New ValidadorDeEsIgualOMenorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMenorQue(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     expression As Expression(Of Func(Of T, TProperty))) _
    As ConfiguradorDeReglas(Of T, TProperty)
        Dim func = expression.Compile()
        elConfigurador.AgregarValidador(
            New ValidadorDeEsIgualOMenorQue(func.TransformadaANoGenerica(),
                                            expression.GetMember()))
        Return elConfigurador
    End Function

End Module
