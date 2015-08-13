Public Class ConsultaSobreUnPermiso(Of Perfil, Accion)
    Private laEspecificacion As EspecificacionDePermisosBase(Of Perfil, Accion)

    Public Sub New(laEspecificacion As EspecificacionDePermisosBase(Of Perfil, Accion))
        Me.laEspecificacion = laEspecificacion
    End Sub

    Public Property LaAccion As Accion

    Public Function AUnActorConEstos(perfiles As Perfil) As Boolean

        Dim sePermite As Boolean = False
        Dim permisoEncontrado As Permiso
        permisoEncontrado = EncuentreUnaAccionParaLosPerfiles(LaAccion, perfiles)

        If permisoEncontrado IsNot Nothing Then
            sePermite = True
        End If

        Return sePermite

    End Function

    Private Function EncuentreUnaAccionParaLosPerfiles(laAccion As Accion,
                                                       losPerfiles As Perfil) As Permiso
        Dim permisoEncontrado As Permiso = Nothing

        Dim perfilesComoObjeto As Object = losPerfiles
        Dim accionComoObjeto As Object = laAccion
        Dim perfilesComoInteger = Integer.Parse(perfilesComoObjeto)
        Dim accionComoInteger = Integer.Parse(accionComoObjeto)

        Dim losPermisos As IEnumerable(Of Permiso)
        losPermisos = laEspecificacion.LosPermisos

        For Each p In losPermisos
            If (p.PuedeRealizacionLaAccion = accionComoInteger) _
                And (p.UnActorConPerfil And perfilesComoInteger) Then
                permisoEncontrado = p
            End If
        Next

        Return permisoEncontrado
    End Function

End Class
