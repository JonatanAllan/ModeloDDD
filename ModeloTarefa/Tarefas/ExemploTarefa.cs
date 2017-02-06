using System.Collections.Generic;
using ModeloRecursos.Interfaces.ServicosExternos;
using ModeloTarefa.Interfaces.Tarefas;
using RestSharp;

namespace ModeloTarefa.Tarefas
{
    public class ExemploTarefa : IExemploTarefa
    {
        private readonly IExemploServicosExternos _exemplosSvc;

        public ExemploTarefa(IExemploServicosExternos exemploSvc)
        {
            _exemplosSvc = exemploSvc;
        }

        public void Executar()
        {
            var parametros = new List<Parameter>();
            var exemplos = _exemplosSvc.ListarExemplo(parametros);
        }
    }
}