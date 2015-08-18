Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection

Public Class ReglaDePropiedad
    Implements ReglaDeValidacion

    Private laFuncionDeLaPropiedad As Func(Of Object, Object)
    Private laExpresion As LambdaExpression
    Private elMiembro As MemberInfo
    Private elTipoDeLaPropiedad As Type
    Private elTipoDeLaClase As Type
    Private losValidadores As IList(Of ValidadorDePropiedad)

    Public Sub New(elMiembro As MemberInfo,
                   laFuncion As Func(Of Object, Object),
                   laExpresion As LambdaExpression,
                   elTipoDeLaPropiedad As Type,
                   elTipoDeLaClase As Type)
        Me.elMiembro = elMiembro
        Me.laFuncionDeLaPropiedad = laFuncion
        Me.laExpresion = laExpresion
        Me.elTipoDeLaPropiedad = elTipoDeLaPropiedad
        Me.elTipoDeLaClase = elTipoDeLaClase
        Me.losValidadores = New List(Of ValidadorDePropiedad)
    End Sub


    Public ReadOnly Property FunctionDeLaPropiedad As Func(Of Object, Object)
        Get
            Return Me.laFuncionDeLaPropiedad
        End Get
    End Property

    Public ReadOnly Property NombreDeLaPropiedad As String
        Get
            Dim elNombre As String
            elNombre = elMiembro.Name

            Dim elNombreEnPascalCase As String
            elNombreEnPascalCase = elNombre.SplitPascalCase()

            Return elNombreEnPascalCase
        End Get
    End Property

    ReadOnly Property Descripciones As IEnumerable(Of String) _
        Implements ReglaDeValidacion.Descripciones
        Get
            Dim lasDescripciones As New List(Of String)
            For Each validador In losValidadores
                lasDescripciones.Add(validador.Descripcion(Me))
            Next

            Return lasDescripciones
        End Get
    End Property

    Public Shared Function Cree(Of T, TPropiedad) _
        (laExpresion As Expression(Of Func(Of T, TPropiedad))) _
        As ReglaDePropiedad

        Dim elMiembroDeLaPropiedad = laExpresion.GetMember()
        Dim laExpresionCompilada = laExpresion.Compile()

        Return New ReglaDePropiedad(elMiembroDeLaPropiedad,
                                         laExpresionCompilada.TransformadaANoGenerica,
                                         laExpresion,
                                         GetType(TPropiedad),
                                         GetType(T))
    End Function

    Function Valide(laInstancia As Object) As IEnumerable(Of String) _
        Implements ReglaDeValidacion.Valide
        Dim elContexto As New ContextoDeUnaPropiedad(laInstancia, Me)

        Dim mensajesDeError As New List(Of String)
        For Each unValidador In losValidadores
            Dim mensajeDeError As String
            mensajeDeError = unValidador.Valide(elContexto)
            If Not String.IsNullOrEmpty(mensajeDeError) Then
                mensajesDeError.Add(mensajeDeError)
            End If
        Next

        Return mensajesDeError
    End Function

    Sub RegistreElValidador(elValidador As ValidadorDePropiedad)
        losValidadores.Add(elValidador)
    End Sub

End Class