Public Interface ReglaDeValidacion

    ReadOnly Property Descripciones As IEnumerable(Of String)
    Function Valide(laInstancia As Object) As IEnumerable(Of String)

End Interface
