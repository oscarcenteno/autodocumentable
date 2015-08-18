Class ValidadorDeNoEsNulaOVacia
    Inherits ValidadorDePropiedad

    Public Overrides Function EsValida(elContexto As ContextoDeUnaPropiedad) As Boolean
        Dim noEsNulaOVacia As Boolean = False

        If elContexto.ValorDeLaPropiedad Is Nothing _
            OrElse EsUnStringInvalido(elContexto.ValorDeLaPropiedad) _
            OrElse EsUnaColeccionVacia(elContexto.ValorDeLaPropiedad) _
            OrElse Equals(elContexto.ValorDeLaPropiedad, Nothing) Then
            noEsNulaOVacia = False
        Else
            noEsNulaOVacia = True
        End If

        Return noEsNulaOVacia
    End Function

    Private Function EsUnaColeccionVacia(propertyValue As Object) As Boolean
        Dim laColeccion = TryCast(propertyValue, IEnumerable)

        Dim esUnaColeccion As Boolean
        esUnaColeccion = laColeccion IsNot Nothing

        Dim laColeccionTieneElementos As Boolean
        laColeccionTieneElementos = False

        If esUnaColeccion Then
            Dim laColeccionConObjetos As IEnumerable(Of Object)
            laColeccionConObjetos = laColeccion.Cast(Of Object)()

            laColeccionTieneElementos = laColeccionConObjetos.Any()
        End If

        Dim esUnaColeccionYEsVacia As Boolean
        esUnaColeccionYEsVacia = esUnaColeccion And laColeccionTieneElementos

        Return esUnaColeccionYEsVacia
    End Function

    Private Function EsUnStringInvalido(value As Object) As Boolean
        Dim esUnString As Boolean
        esUnString = TypeOf value Is String

        Dim esUnStringYEsInvalido As Boolean
        esUnStringYEsInvalido = True

        If esUnString Then
            esUnStringYEsInvalido = String.IsNullOrWhiteSpace(TryCast(value, String))
        Else
            esUnStringYEsInvalido = False
        End If

        Return esUnStringYEsInvalido
    End Function

    Public Overrides ReadOnly Property Descripcion(laRegla As ReglaDePropiedad) As String
        Get
            Dim laPlantilla As String
            laPlantilla = "'{0}' es requerido."
            Return String.Format(laPlantilla, laRegla.NombreDeLaPropiedad)
        End Get
    End Property
End Class
