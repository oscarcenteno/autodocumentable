Public Class PermisoFluido(Of Accion)

    Private elPermiso As Permiso

    Sub New(elPermiso As Permiso)
        Me.elPermiso = elPermiso
    End Sub

    Public Sub PuedeRealizarLa(accion As Accion)
        Dim accionComoObjeto As Object = accion
        elPermiso.PuedeRealizacionLaAccion = Integer.Parse(accionComoObjeto)
    End Sub

End Class
