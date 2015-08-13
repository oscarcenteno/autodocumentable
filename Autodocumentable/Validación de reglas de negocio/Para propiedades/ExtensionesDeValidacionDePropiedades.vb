Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Reflection

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
    Public Function TieneUnTamanoDe(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String), tamanoExacto As Integer) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeTieneUnTamanoDe(tamanoExacto))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function TieneUnTamanoEntre(Of T) _
    (elConfigurador As ConfiguradorDeReglas(Of T, String),
     tamanoMinimo As Integer,
     tamanoMaximo As Integer) As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeTieneUnTamanoEntre(tamanoMinimo,
                                                                          tamanoMaximo))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function IniciaCon(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String),
         textoConElQueDebeIniciar As String) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeIniciaCon(textoConElQueDebeIniciar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsCorreoElectronico(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsCorreoElectronico())
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsHexagecimal(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsHexagecimal())
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsAlfanumerica(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsAlfanumerica())
        Return elConfigurador
    End Function

    <Extension> _
    Public Function ContieneSoloDigitos(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeContieneSoloDigitos())
        Return elConfigurador
    End Function

    <Extension> _
    Public Function CumpleConLaExpresionRegular(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String), expresionRegular As String) _
        As ConfiguradorDeReglas(Of T, String)

        Dim elValidador As ValidadorDeCumpleConLaExpresionRegular
        elValidador = New ValidadorDeCumpleConLaExpresionRegular(expresionRegular)

        elConfigurador.AgregarValidador(elValidador)

        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMayorQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty), 
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty), valorPorComparar As TProperty) _
        As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsMayorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMayorQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
         As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        Dim laFuncionNoGenerica = laFuncion.TransformadaANoGenerica
        Dim elMiembro As MemberInfo
        elMiembro = expression.GetMember()
        Dim elValidador As New ValidadorDeEsMayorQue(laFuncionNoGenerica, elMiembro)

        elConfigurador.AgregarValidador(elValidador)

        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMenorQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty), 
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As New ValidadorDeEsMenorQue(valorPorComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsMenorQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        Dim elValidador As ValidadorDeEsMenorQue
        elValidador = New ValidadorDeEsMenorQue(laFuncion.TransformadaANoGenerica(),
                                                expression.GetMember())
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty), 
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsIgualQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        Dim elValidador
        elValidador = New ValidadorDeEsIgualQue(laFuncion.TransformadaANoGenerica(),
                                                expression.GetMember())
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMayorQue(Of T,
                                         TProperty As {
                                             IComparable(Of TProperty), 
                                             IComparable
                                             }
                                         ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As ValidadorDeEsIgualOMayorQue
        elValidador = New ValidadorDeEsIgualOMayorQue(valorPorComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMayorQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        elConfigurador.AgregarValidador(
            New ValidadorDeEsIgualOMayorQue(laFuncion.TransformadaANoGenerica(),
                                            expression.GetMember()))
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMenorQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty), 
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As New ValidadorDeEsIgualOMenorQue(valorPorComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension> _
    Public Function EsIgualOMenorQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        Dim laFuncionNoGenerica = laFuncion.TransformadaANoGenerica()
        Dim elMiembro As MemberInfo
        elMiembro = expression.GetMember()

        Dim elValidador As New ValidadorDeEsIgualOMenorQue(laFuncionNoGenerica, elMiembro)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

End Module
