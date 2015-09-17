Imports System.Runtime.CompilerServices

Module ExtensionesAObject

    <Extension>
    Sub Guarda(elObjeto As Object, elMensajeDeError As String)
        If elObjeto Is Nothing Then
            Throw New ArgumentException(elMensajeDeError)
        End If
    End Sub

End Module
