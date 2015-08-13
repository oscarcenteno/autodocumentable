Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection

Public Class ReglaParaUnaExpresion
    Implements ReglaDeValidacion

    Private laFuncion As Func(Of Object, Object)
    Private laExpresion As Object
    Private elTipo As Type
    Private laDescripcion As String

    Sub New(laFuncion As Func(Of Object, Object), laExpresion As Object, elTipo As Type)
        Me.laFuncion = laFuncion
        Me.laExpresion = laExpresion
        Me.elTipo = elTipo
    End Sub

    Shared Function Cree(Of T)(laExpresion As Expression(Of Func(Of T, Boolean))) _
        As ReglaParaUnaExpresion
        Dim compiled = laExpresion.Compile()
        Dim elTipo As Type
        elTipo = GetType(T)
        Dim laRegla As New ReglaParaUnaExpresion(compiled.TransformadaANoGenerica,
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

        Dim elMetodo = New Lazy(Of Object)(Function() Me.laFuncion(laInstancia))
        Dim laExpresionSeCumple As Boolean
        laExpresionSeCumple = elMetodo.Value

        Dim elResultado As New List(Of String)
        If Not laExpresionSeCumple Then
            elResultado.Add(laDescripcion)
        End If

        Return elResultado
    End Function


    Public Sub ConLaDescripcion(laDescripcion As String)
        Me.laDescripcion = laDescripcion
    End Sub

End Class
