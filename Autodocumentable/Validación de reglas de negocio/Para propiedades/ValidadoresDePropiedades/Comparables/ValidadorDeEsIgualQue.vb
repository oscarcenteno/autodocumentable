Imports System.Reflection

Class ValidadorDeEsIgualQue
    Inherits ValidadorDePropiedad

    Private valorPorComparar As IComparable
    Dim laFuncionDelValorParaComparar As Func(Of Object, Object)
    Dim elMiembroParaComparar As MemberInfo

    Sub New(valorPorComparar As IComparable)
        Me.valorPorComparar = valorPorComparar
    End Sub

    Public Sub New(laFuncionDelValorParaComparar As Func(Of Object, Object),
                   elMiembroParaComparar As MemberInfo)
        Me.laFuncionDelValorParaComparar = laFuncionDelValorParaComparar
        Me.elMiembroParaComparar = elMiembroParaComparar
    End Sub

    Public Overrides Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean
        Dim esMayorQue As Boolean
        If contexto.ValorDeLaPropiedad Is Nothing Then
            esMayorQue = False
        Else
            Dim valorPorComparar As Object
            valorPorComparar = ObtengaValorParaComparar(contexto)

            Dim resultadoDeComparar As Integer
            resultadoDeComparar = Comparador.GetComparisonResult(contexto.ValorDeLaPropiedad, valorPorComparar)
            esMayorQue = resultadoDeComparar = 0
        End If

        Return esMayorQue

    End Function

    Private Function ObtengaValorParaComparar(contexto As ContextoDeUnaPropiedad) As Object
        If Me.laFuncionDelValorParaComparar IsNot Nothing Then
            Return laFuncionDelValorParaComparar(contexto.Instancia)
        End If

        Return Me.valorPorComparar
    End Function


    Public Overloads Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' debe ser igual que {1}."

            Dim laDescripcion As String

            If Me.elMiembroParaComparar Is Nothing Then
                laDescripcion = String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, Me.valorPorComparar)
            Else
                Dim elNombre As String
                elNombre = Me.elMiembroParaComparar.Name
                Dim elNombreEnSentenceCase As String
                elNombreEnSentenceCase = elNombre.SplitPascalCase()
                laDescripcion = String.Format(laPlantilla, laRegla.NombreDeLaPropiedad, elNombreEnSentenceCase)
            End If

            Return laDescripcion
        End Get
    End Property
End Class
