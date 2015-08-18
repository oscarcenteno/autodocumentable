Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Reflection

Public Module ExtensionesDeValidacionDePropiedades

    <Extension>
    Public Sub NoEsNula(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty))

        elConfigurador.AgregarValidador(New ValidadorDeNoEsNula())
    End Sub

    <Extension>
    Public Sub NoEsNulaOVacia(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty))

        elConfigurador.AgregarValidador(New ValidadorDeNoEsNulaOVacia())
    End Sub

    <Extension>
    Public Function TieneUnTamanoDe(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String), tamanoExacto As Integer) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeTieneUnTamanoDe(tamanoExacto))
        Return elConfigurador
    End Function

    <Extension>
    Public Function MideEntre(Of T) _
    (elConfigurador As ConfiguradorDeReglas(Of T, String),
     elMinimo As Integer,
     elMaximo As Integer) As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeMideEntre(elMinimo, elMaximo))
        Return elConfigurador
    End Function

    <Extension>
    Public Function IniciaCon(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String),
         textoConQueDebeIniciar As String) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeIniciaCon(textoConQueDebeIniciar))
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsCorreoElectronico(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsCorreoElectronico())
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsHexagecimal(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsHexagecimal())
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsAlfanumerica(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeEsAlfanumerica())
        Return elConfigurador
    End Function

    <Extension>
    Public Function ContieneSoloDigitos(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String)) _
        As ConfiguradorDeReglas(Of T, String)

        elConfigurador.AgregarValidador(New ValidadorDeContieneSoloDigitos())
        Return elConfigurador
    End Function

    <Extension>
    Public Function CumpleConLaExpresionRegular(Of T) _
        (elConfigurador As ConfiguradorDeReglas(Of T, String), expresionRegular As String) _
        As ConfiguradorDeReglas(Of T, String)

        Dim elValidador As ValidadorDeCumpleConLaExpresionRegular
        elValidador = New ValidadorDeCumpleConLaExpresionRegular(expresionRegular)

        elConfigurador.AgregarValidador(elValidador)

        Return elConfigurador
    End Function

    <Extension>
    Public Function EsMayorQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty),
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) _
        As ConfiguradorDeReglas(Of T, TProperty)

        elConfigurador.AgregarValidador(New ValidadorDeEsMayorQue(valorPorComparar))
        Return elConfigurador
    End Function

    <Extension>
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

    <Extension>
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

    <Extension>
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

    <Extension>
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

    <Extension>
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

    <Extension>
    Public Function EsMayorOIgualQue(Of T,
                                         TProperty As {
                                             IComparable(Of TProperty),
                                             IComparable
                                             }
                                         ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As ValidadorDeEsMayorOIgualQue
        elValidador = New ValidadorDeEsMayorOIgualQue(valorPorComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsMayorOIgualQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        elConfigurador.AgregarValidador(
            New ValidadorDeEsMayorOIgualQue(laFuncion.TransformadaANoGenerica(),
                                            expression.GetMember()))
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsMenorOIgualQue(Of T,
                                   TProperty As {
                                       IComparable(Of TProperty),
                                       IComparable
                                       }
                                   ) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         valorPorComparar As TProperty) As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As New ValidadorDeEsMenorOIgualQue(valorPorComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension>
    Public Function EsMenorOIgualQue(Of T, TProperty) _
        (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
         expression As Expression(Of Func(Of T, TProperty))) _
        As ConfiguradorDeReglas(Of T, TProperty)

        Dim laFuncion = expression.Compile()
        Dim laFuncionNoGenerica = laFuncion.TransformadaANoGenerica()
        Dim elMiembro As MemberInfo
        elMiembro = expression.GetMember()

        Dim elValidador As New ValidadorDeEsMenorOIgualQue(laFuncionNoGenerica, elMiembro)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension>
    Public Function ExisteEnLaColeccion(Of T, TProperty) _
    (elConfigurador As ConfiguradorDeReglas(Of T, TProperty),
     coleccionParaComparar As IEnumerable(Of TProperty)) _
     As ConfiguradorDeReglas(Of T, TProperty)

        Dim elValidador As New ValidadorDeExisteEnLaColeccion(coleccionParaComparar)
        elConfigurador.AgregarValidador(elValidador)
        Return elConfigurador
    End Function

    <Extension>
    Public Function ConviertaATexto(laLista As IEnumerable(Of Object)) As String
        Dim elTexto As String
        elTexto = String.Empty

        Dim esElPrimerElemento As Boolean
        esElPrimerElemento = True

        For Each elemento In laLista
            If esElPrimerElemento Then
                elTexto = String.Format("'{0}'", elemento)
            Else
                elTexto = String.Format("{0}, '{1}'", elTexto, elemento.ToString)
            End If
            esElPrimerElemento = False
        Next

        Return elTexto
    End Function

End Module
