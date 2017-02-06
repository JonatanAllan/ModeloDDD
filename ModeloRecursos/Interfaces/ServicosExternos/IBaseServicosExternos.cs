using RestSharp;
using System.Collections.Generic;

namespace ModeloRecursos.Interfaces.ServicosExternos
{
    public interface IBaseServicosExternos
    {
        Parameter CriarParametro(string nome, object valor, ParameterType tipoParametro = ParameterType.QueryString);

        List<Parameter> CriarParametrosPaginacao(int deslocamento, int limite, string condicoes = null, string ordenarPor = null);

        IRestResponse Executar(string recurso, Method metodo, List<Parameter> cabecalhos = null, List<Parameter> parametros = null, object corpo = null);
    }
}