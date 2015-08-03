Public MustInherit Class EspecificacionDePermisosBase(Of Perfil, Accion)

    Private permisos As IList(Of Permiso)

    Public ReadOnly Property LosPermisos As IEnumerable(Of Permiso)
        Get
            Return permisos
        End Get
    End Property

    Protected Sub New()
        InicialiceLaListaDePermisos()
        EspecifiqueLosPermisos()
    End Sub

    Private Sub InicialiceLaListaDePermisos()
        Me.permisos = New List(Of Permiso)
    End Sub

    Protected Function Un(actor As Perfil) As PermisoFluido(Of Accion)
        Dim nuevoPermiso As New Permiso
        Dim perfilComoObjeto As Object = actor
        nuevoPermiso.UnActorConPerfil = Integer.Parse(perfilComoObjeto)
        permisos.Add(nuevoPermiso)

        Dim elPermisoFluido As New PermisoFluido(Of Accion)(nuevoPermiso)
        Return elPermisoFluido
    End Function

    Public Function SePermiteLa(accion As Accion) As ConsultaSobreUnPermisoFluida(Of Perfil, Accion)
        Dim laConsulta As New ConsultaSobreUnPermisoFluida(Of Perfil, Accion)(Me)
        laConsulta.LaAccion = accion

        Return laConsulta
    End Function

    Protected MustOverride Sub EspecifiqueLosPermisos()


End Class