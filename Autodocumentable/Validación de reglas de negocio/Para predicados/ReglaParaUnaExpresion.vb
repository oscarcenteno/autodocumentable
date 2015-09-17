Imports System.Linq.Expressions
Imports System.Reflection

Public Class ReglaParaUnaExpresion
    Implements ReglaDeValidacion

    Private elMiembro As MemberInfo
    Private laFuncion As Func(Of Object, Object)
    Private laExpresion As Object
    Private elTipo As Type
    Private laDescripcion As String

    Sub New(elMiembro As MemberInfo, laFuncion As Func(Of Object, Object), laExpresion As Object, elTipo As Type)
        Me.elMiembro = elMiembro

        Me.laDescripcion = ObtengaElNombreDelMetodo()

        Me.laFuncion = laFuncion
        Me.laExpresion = laExpresion
        Me.elTipo = elTipo
    End Sub

    Private Function ObtengaElNombreDelMetodo() As String
        Dim elNombre As String
        elNombre = elMiembro.Name

        Dim elNombreEnPascalCase As String
        elNombreEnPascalCase = elNombre.SepareComoUnaOracion()

        Return elNombreEnPascalCase
    End Function

    Shared Function Cree(Of T)(laExpresion As Expression(Of Func(Of T, Boolean))) _
        As ReglaParaUnaExpresion

        Dim laExpresionCompilada As Func(Of T, Boolean)
        laExpresionCompilada = laExpresion.Compile()

        Dim elTipo As Type
        elTipo = GetType(T)

        Dim elMetodo As MemberInfo
        elMetodo = laExpresion.ObtengaElMetodo()

        Dim laExpresionNoGenerica As Func(Of Object, Object)
        laExpresionNoGenerica = laExpresionCompilada.TransformadaANoGenerica()

        Dim laRegla As New ReglaParaUnaExpresion(elMetodo,
                                                 laExpresionNoGenerica,
                                                 laExpresion,
                                                 elTipo)

        Return laRegla
    End Function

    Public ReadOnly Property Descripciones As IEnumerable(Of String) _
        Implements ReglaDeValidacion.Descripciones
        Get
            Dim lasDescripciones As New List(Of String)
            lasDescripciones.Add(Me.laDescripcion)

            Return lasDescripciones
        End Get
    End Property

    Public Function Valide(laInstancia As Object) As IEnumerable(Of String) _
        Implements ReglaDeValidacion.Valide
        Dim seCumple As Boolean = LaExpresionSeCumple(laInstancia)
        Dim elResultado As List(Of String) = RegistreSiNoSeCumple(seCumple)

        Return elResultado
    End Function

    Private Function RegistreSiNoSeCumple(laExpresionSeCumple As Boolean) As List(Of String)
        Dim elResultado As New List(Of String)
        If Not laExpresionSeCumple Then
            elResultado.Add(laDescripcion)
        End If

        Return elResultado
    End Function

    Private Function LaExpresionSeCumple(laInstancia As Object) As Boolean
        Dim elMetodo = New Lazy(Of Object)(Function() Me.laFuncion(laInstancia))
        Dim seCumple As Boolean = elMetodo.Value

        Return seCumple
    End Function

    Public Sub ConLaDescripcion(laDescripcion As String)
        Me.laDescripcion = laDescripcion
    End Sub

End Class
