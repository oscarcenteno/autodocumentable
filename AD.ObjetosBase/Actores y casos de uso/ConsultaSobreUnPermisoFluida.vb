Public Class ConsultaSobreUnPermisoFluida(Of Perfil, Accion)
    Private laEspecificacionDePermisosBase As EspecificacionDePermisosBase(Of Perfil, Accion)

    Public Sub New(especificacionDePermisosBase As EspecificacionDePermisosBase(Of Perfil, Accion))
        Me.laEspecificacionDePermisosBase = especificacionDePermisosBase
    End Sub

    Public Property LaAccion As Accion

    Public Function AUnActorConEstos(perfiles As Perfil) As Boolean

        Dim sePermite As Boolean = False
        Dim permisoEncontrado As Permiso
        permisoEncontrado = EncuentreUnaAccionParaElPerfil(LaAccion, perfiles)

        If permisoEncontrado IsNot Nothing Then
            sePermite = True
        End If

        Return sePermite

    End Function

    Private Function EncuentreUnaAccionParaElPerfil(accionConsultada As Accion, perfilesDelActor As Perfil) As Permiso
        Dim permisoEncontrado As Permiso = Nothing

        Dim perfilesComoObjeto As Object = perfilesDelActor
        Dim accionComoObjeto As Object = accionConsultada
        Dim perfilesComoInteger = Integer.Parse(perfilesComoObjeto)
        Dim accionComoInteger = Integer.Parse(accionComoObjeto)


        Dim losPermisos As IEnumerable(Of Permiso)
        losPermisos = laEspecificacionDePermisosBase.LosPermisos


        For Each p In losPermisos
            If (p.PuedeRealizacionLaAccion = accionComoInteger) _
                And (p.UnActorConPerfil And perfilesComoInteger) Then
                permisoEncontrado = p
            End If
        Next

        Return permisoEncontrado
    End Function





End Class
