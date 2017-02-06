using ModeloRecursos.ViewModels;
using RestSharp;
using System.Collections.Generic;

namespace ModeloRecursos.Interfaces.ServicosExternos
{
    public interface IExemploServicosExternos : IBaseServicosExternos
    {
        RespostaViewModel<IEnumerable<ExemploViewModel>> ListarExemplo(List<Parameter> parametros);
    }
}