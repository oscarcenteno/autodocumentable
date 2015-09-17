Public Class ConsultaSobreUnPermiso(Of Perfil, Accion)
    Private laEspecificacion As EspecificacionDePermisosBase(Of Perfil, Accion)

    Public Sub New(laEspecificacion As EspecificacionDePermisosBase(Of Perfil, Accion))
        Me.laEspecificacion = laEspecificacion
    End Sub

    Public Property LaAccion As Accion

    Public Function A(losPerfiles As Perfil) As Boolean

        Dim laAccionSePermite As Boolean = False
        Dim elPermisoEncontrado As Permiso
        elPermisoEncontrado = EncuentreUnaAccionParaLosPerfiles(LaAccion, losPerfiles)

        If elPermisoEncontrado IsNot Nothing Then
            laAccionSePermite = True
        End If

        Return laAccionSePermite
    End Function

    Private Function EncuentreUnaAccionParaLosPerfiles(laAccion As Accion,
                                                       losPerfiles As Perfil) As Permiso
        Dim elPermisoEncontrado As Permiso = Nothing

        Dim losPerfilesComoObjeto As Object = losPerfiles
        Dim laAccionComoObjeto As Object = laAccion
        Dim losPerfilesComoInteger = Integer.Parse(losPerfilesComoObjeto)
        Dim laAccionComoInteger = Integer.Parse(laAccionComoObjeto)

        Dim losPermisos As IEnumerable(Of Permiso)
        losPermisos = laEspecificacion.LosPermisos

        For Each unPermiso In losPermisos
            If (unPermiso.PuedeRealizacionLaAccion = laAccionComoInteger) _
                And (unPermiso.UnActorConPerfil And losPerfilesComoInteger) Then
                elPermisoEncontrado = unPermiso
            End If
        Next

        Return elPermisoEncontrado
    End Function

End Class
