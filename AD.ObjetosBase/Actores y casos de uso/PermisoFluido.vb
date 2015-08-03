Public Class PermisoFluido(Of Accion)

    Private elPermiso As Permiso

    Sub New(elPermiso As Permiso)
        Me.elPermiso = elPermiso
    End Sub

    Public Sub Puede(realizarUnaAccion As Accion)
        Dim accionComoObjeto As Object = realizarUnaAccion
        elPermiso.PuedeRealizacionLaAccion = Integer.Parse(accionComoObjeto)
    End Sub

End Class
