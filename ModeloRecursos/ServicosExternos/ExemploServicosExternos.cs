using ModeloRecursos.Interfaces.ServicosExternos;
using ModeloRecursos.ViewModels;
using RestSharp;
using System.Collections.Generic;

namespace ModeloRecursos.ServicosExternos
{
    public class ExemploServicosExternos : BaseServicosExternos, IExemploServicosExternos
    {
        public ExemploServicosExternos()
            : base("http://localhost")
        {
            
        }

        public RespostaViewModel<IEnumerable<ExemploViewModel>> ListarExemplo(List<Parameter> parametros)
        {
            const string recurso = "/Exemplo/Listar";
            var resposta = Executar(recurso, Method.GET, parametros);

            return new RespostaViewModel<IEnumerable<ExemploViewModel>>(resposta);
        }
    }
}