Public NotInheritable Class Comparador
    Private Sub New()
    End Sub

    ' TODO: Cambiar retorno por un objeto no con ByRef
    Public Shared Function IntenteComparar(elValor As IComparable,
                                           valorParaComparar As IComparable,
                                           ByRef elResultadoDeSalida As Integer) As Boolean
        Dim sePudoComparar As Boolean
        sePudoComparar = False

        Try
            Compare(elValor, valorParaComparar, elResultadoDeSalida)
            sePudoComparar = True
        Catch
            elResultadoDeSalida = 0
            sePudoComparar = False
        End Try

        Return sePudoComparar
    End Function

    Private Shared Sub Compare(value As IComparable,
                               valueToCompare As IComparable,
                               ByRef result As Integer)
        Try
            ' try default (will work on same types)
            result = value.CompareTo(valueToCompare)
        Catch generatedExceptionName As ArgumentException
            ' attempt to to value type comparison
            If TypeOf value Is Decimal OrElse TypeOf valueToCompare Is Decimal OrElse TypeOf value Is Double OrElse TypeOf valueToCompare Is Double OrElse TypeOf value Is Single OrElse TypeOf valueToCompare Is Single Then
                ' we are comparing a decimal/double/float, then compare using doubles
                result = Convert.ToDouble(value).CompareTo(Convert.ToDouble(valueToCompare))
            Else
                ' use long integer
                result = CLng(value).CompareTo(CLng(valueToCompare))
            End If
        End Try
    End Sub

    Public Shared Function GetComparisonResult(value As IComparable, valueToCompare As IComparable) As Integer
        Dim result As Integer
        If IntenteComparar(value, valueToCompare, result) Then
            Return result
        End If

        Return value.CompareTo(valueToCompare)
    End Function

    ''' <summary>
    ''' Tries to compare the two objects, but will throw an exception if it fails.
    ''' </summary>
    ''' <returns>True on success, otherwise False.</returns>
    Public Shared Function GetEqualsResult(value As IComparable, valueToCompare As IComparable) As Boolean
        Dim result As Integer
        If IntenteComparar(value, valueToCompare, result) Then
            Return result = 0
        End If

        Return value.Equals(valueToCompare)
    End Function
End Class
