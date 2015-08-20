Public Class ResultadoDeLaValidacion
    Implements IEquatable(Of ResultadoDeLaValidacion)

    Private errores As New List(Of String)

    Friend Sub AgregarErrores(mensajesDeError As IEnumerable(Of String))
        errores.AddRange(mensajesDeError)
    End Sub

    Public Overloads Function Equals(other As ResultadoDeLaValidacion) _
            As Boolean Implements IEquatable(Of ResultadoDeLaValidacion).Equals

        Dim sonIguales As Boolean = False

        If other IsNot Nothing Then

            Dim erroresSonIguales As Boolean
            erroresSonIguales = errores.SequenceEqual(other.ErroresDeValidacion)

            sonIguales = erroresSonIguales
        End If

        Return sonIguales
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.Equals(CType(obj, ResultadoDeLaValidacion))
    End Function

    Public ReadOnly Property EsValida As Boolean
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
