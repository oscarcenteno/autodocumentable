Public Class ConfiguradorDeReglas(Of T, TProperty)

    Private laRegla As ReglaParaUnaPropiedad

    Friend Sub New(laRegla As ReglaParaUnaPropiedad)
        Me.laRegla = laRegla
    End Sub

    Friend Sub AgregarValidador(elValidador As ValidadorDePropiedad)
        elValidador.Guarda("AgregarValidador debe recibir un validador válido.")
        laRegla.RegistreElValidador(elValidador)
    End Sub

End Class