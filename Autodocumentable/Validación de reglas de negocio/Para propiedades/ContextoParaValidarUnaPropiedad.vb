Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public Class ContextoParaValidarUnaPropiedad

    Public Property Instancia As Object
    Public Property ReglaDeLaPropiedad As ReglaParaUnaPropiedad
    Private elContenedorDelValorDeLaPropiedad As Lazy(Of Object)
    Public Sub New(instancia As Object, reglaDeLaPropiedad As ReglaParaUnaPropiedad)
        Me.Instancia = instancia
        Me.ReglaDeLaPropiedad = reglaDeLaPropiedad
        Me.elContenedorDelValorDeLaPropiedad = New Lazy(Of Object)(Function() reglaDeLaPropiedad.FunctionDeLaPropiedad(instancia))
    End Sub

    Public ReadOnly Property ValorDeLaPropiedad As Object
        Get
            Return elContenedorDelValorDeLaPropiedad.Value
        End Get
    End Property

End Class
