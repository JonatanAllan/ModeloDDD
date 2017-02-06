using ModeloRecursos.Interfaces.ServicosExternos;
using ModeloRecursos.Recursos;
using RestSharp;
using System.Collections.Generic;

namespace ModeloRecursos.ServicosExternos
{
    public abstract class BaseServicosExternos : IBaseServicosExternos
    {
        private readonly RestClient _cliente;

        protected BaseServicosExternos(string baseUrl)
        {
            _cliente = new RestClient(baseUrl);
        }

        public Parameter CriarParametro(string nome, object valor,
            ParameterType tipo = ParameterType.QueryString) => new Parameter
            {
                Name = nome,
                Value = valor,
                Type = tipo
            };

        public List<Parameter> CriarParametrosPaginacao(int deslocamento, int limite,
            string condicoes = null, string ordenarPor = null)
        {
            var parametros = new List<Parameter>
            {
                CriarParametro("ordenarPor", ordenarPor),
                CriarParametro("deslocamento", deslocamento),
                CriarParametro("limite", limite)
            };

            if (!string.IsNullOrEmpty(condicoes))
            {
                parametros.Add(CriarParametro("condicoes", condicoes));
            }

            return parametros;
        }

        public IRestResponse Executar(string recurso, Method metodo,
            List<Parameter> cabecalhos = null, List<Parameter> parametros = null, object corpo = null)
        {
            var requisicao = new RestRequest(recurso, metodo)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new RestSharpJsonSerializer()
            };

            if (cabecalhos != null)
            {
                foreach (var cabecalho in cabecalhos)
                {
                    requisicao.AddHeader(cabecalho.Name, cabecalho.Value.ToString());
                }
            }

            if (parametros != null)
            {
                foreach (var parametro in parametros)
                {
                    requisicao.AddParameter(parametro.Name, parametro.Value.ToString(), parametro.ContentType, parametro.Type);
                }
            }

            if (corpo != null)
            {
                requisicao.AddJsonBody(corpo);
            }

            return _cliente.Execute(requisicao);
        }
    }
}