Public Class ResultadoDeLaValidacion

    Private errores As List(Of String)

    Sub New()
        errores = New List(Of String)
    End Sub

    Friend Sub AgregarErrores(mensajesDeError As IEnumerable(Of String))
        errores.AddRange(mensajesDeError)
    End Sub

    Public ReadOnly Property EsValida
        Get
            Dim cantidadDeErrores As Integer
            cantidadDeErrores = errores.Count

            Dim esValidaSiNoHayErrores As Boolean
            esValidaSiNoHayErrores = cantidadDeErrores = 0

            Return esValidaSiNoHayErrores
        End Get
    End Property

    Public ReadOnly Property ErroresDeValidacion As IEnumerable(Of String)
        Get
            Return Me.errores
        End Get
    End Property


End Class
