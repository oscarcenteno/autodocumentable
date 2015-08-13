Public MustInherit Class EspecificacionDePermisosBase(Of Perfil, Accion)

    Private permisos As New List(Of Permiso)

    Public ReadOnly Property LosPermisos As IEnumerable(Of Permiso)
        Get
            Return permisos
        End Get
    End Property

    Protected Function El(actor As Perfil) As PermisoFluido(Of Accion)
        Dim nuevoPermiso As New Permiso
        Dim perfilComoObjeto As Object = actor
        nuevoPermiso.UnActorConPerfil = Integer.Parse(perfilComoObjeto)
        permisos.Add(nuevoPermiso)

        Dim elPermisoFluido As New PermisoFluido(Of Accion)(nuevoPermiso)
        Return elPermisoFluido
    End Function

    Public Function SePermiteLa(accion As Accion) As ConsultaSobreUnPermiso(Of Perfil, Accion)
        Dim laConsulta As New ConsultaSobreUnPermiso(Of Perfil, Accion)(Me)
        laConsulta.LaAccion = accion

        Return laConsulta
    End Function


End Class