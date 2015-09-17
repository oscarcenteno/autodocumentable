Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Reflection

Module ExtensionesDeReflection

    <Extension>
    Function ObtengaElMiembro(laExpresion As LambdaExpression) As MemberInfo
        Dim memberExp = ObtengaLaExpresionSiEsUnaria(laExpresion.Body)

        If memberExp Is Nothing Then
            Return Nothing
        End If

        Return memberExp.Member
    End Function

    <Extension>
    Function ObtengaElMetodo(laExpresion As LambdaExpression) As MemberInfo
        Dim elMetodo As MemberInfo

        If (TypeOf laExpresion.Body Is MethodCallExpression) Then
            Dim expresionDeMetodo As MethodCallExpression
            expresionDeMetodo = laExpresion.Body
            elMetodo = expresionDeMetodo.Method
        Else
            Throw New ArgumentException("La validacion debe ser un llamado a un método de instancia.")
        End If

        Return elMetodo
    End Function

    Function ObtengaLaExpresionSiEsUnaria(toUnwrap As Expression) As MemberExpression

        If (TypeOf toUnwrap Is UnaryExpression) Then
            Return CType(toUnwrap, UnaryExpression).Operand
        End If

        Return toUnwrap
    End Function

    <Extension>
    Function TransformadaANoGenerica(Of T, TProperty)(func As Func(Of T, TProperty)) As Func(Of Object, Object)
        Return Function(x) func(DirectCast(x, T))
    End Function

End Module
