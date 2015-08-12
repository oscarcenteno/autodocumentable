﻿Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Text.RegularExpressions

Public Module Extensiones

    <Extension>
    Sub Guarda(elObjeto As Object, elMensajeDeError As String)
        If elObjeto Is Nothing Then
            Throw New ArgumentException(elMensajeDeError)
        End If
    End Sub

    <Extension>
    Sub Guarda(elString As String, elMensajeDeError As String)
        If String.IsNullOrEmpty(elString) Then
            Throw New ArgumentException(elMensajeDeError)
        End If
    End Sub

    <Extension>
    Function GetMember(laExpresion As LambdaExpression) As MemberInfo
        Dim memberExp = RemoveUnary(laExpresion.Body)

        If memberExp Is Nothing Then
            Return Nothing
        End If

        Return memberExp.Member
    End Function

    Function RemoveUnary(toUnwrap As Expression) As MemberExpression

        If (TypeOf toUnwrap Is UnaryExpression) Then
            Return CType(toUnwrap, UnaryExpression).Operand
        End If

        Return toUnwrap
    End Function

    <Extension>
    Public Function TransformadaANoGenerica(Of T, TProperty)(func As Func(Of T, TProperty)) As Func(Of Object, Object)
        Return Function(x) func(DirectCast(x, T))
    End Function


    <Extension>
    Public Function SplitPascalCase(input As String) As String

        If (String.IsNullOrEmpty(input)) Then
            Return String.Empty
        End If

        Dim separadoEnPalabras = Regex.Replace(input, "([A-Z])", " $1").Trim()

        Return separadoEnPalabras
    End Function


    <Extension>
    Public Function SplitSentenceCase(input As String) As String

        If (String.IsNullOrEmpty(input)) Then
            Return String.Empty
        End If

        Dim separadoEnPalabras = Regex.Replace(input, "([A-Z])", " $1").Trim()
        Dim separadoEnMinuscula = separadoEnPalabras.ToLower()

        Dim r As New Regex("(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture)
        Dim iniciaConMayuscula = r.Replace(separadoEnMinuscula, Function(c) c.Value.ToUpper())

        Return iniciaConMayuscula
    End Function

End Module
