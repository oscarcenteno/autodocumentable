Public MustInherit Class ValidadorDePropiedad

    Public MustOverride ReadOnly Property Descripcion(laRegla As ReglaParaUnaPropiedad) As String

    Public MustOverride Function EsValida(contexto As ContextoDeUnaPropiedad) As Boolean

    Public Function Valide(contexto As ContextoDeUnaPropiedad) As String
        If EsValida(contexto) Then
            Return String.Empty
        Else
            Dim laDescripcion As String
            laDescripcion = Descripcion(contexto.ReglaDeLaPropiedad)
            Return laDescripcion
        End If
    End Function

End Class
