Imports System.Reflection

Class ValidadorDeEsMayorQue
    Inherits ValidadorDePropiedad

    Private elValorPorComparar As IComparable
    Dim laFuncionDelValorParaComparar As Func(Of Object, Object)
    Dim elMiembroParaComparar As MemberInfo

    Sub New(elValorPorComparar As IComparable)
        Me.elValorPorComparar = elValorPorComparar
    End Sub

    Public Sub New(laFuncionDelValorParaComparar As Func(Of Object, Object),
                   elMiembroParaComparar As MemberInfo)
        Me.laFuncionDelValorParaComparar = laFuncionDelValorParaComparar
        Me.elMiembroParaComparar = elMiembroParaComparar
    End Sub

    Public Overrides Function EsValida(elContexto As ContextoDeUnaPropiedad) As Boolean
        Dim esMayorQue As Boolean

        If elContexto.ValorDeLaPropiedad Is Nothing Then
            esMayorQue = False
        Else
            Dim elValorPorComparar As Object = ObtengaValorParaComparar(elContexto)

            Dim elResultadoDeComparar As Integer
            elResultadoDeComparar = Comparador.GetComparisonResult(elContexto.ValorDeLaPropiedad,
                                                                 elValorPorComparar)
            If elResultadoDeComparar > 0 Then
                esMayorQue = True
            Else
                esMayorQue = False
            End If
        End If

        Return esMayorQue

    End Function

    Private Function ObtengaValorParaComparar(contexto As ContextoDeUnaPropiedad) As Object
        If Me.laFuncionDelValorParaComparar IsNot Nothing Then
            Return laFuncionDelValorParaComparar(contexto.Instancia)
        End If

        Return Me.elValorPorComparar
    End Function


    Public Overloads Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe ser mayor que {1}."

            Dim laDescripcion As String

            If Me.elMiembroParaComparar Is Nothing Then
                laDescripcion = String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, Me.elValorPorComparar)
            Else
                Dim elNombre As String
                elNombre = Me.elMiembroParaComparar.Name
                Dim elNombreEnSentenceCase As String
                elNombreEnSentenceCase = elNombre.SepareComoPascalCase()
                laDescripcion = String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, elNombreEnSentenceCase)
            End If

            Return laDescripcion
        End Get
    End Property
End Class
