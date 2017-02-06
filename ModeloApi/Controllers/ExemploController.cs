using ModeloApi.Parametros.Buscar;
using ModeloApi.Parametros.ContarListar;
using ModeloAplicacao.Interfaces.ServicosApp;
using ModeloDominio.Entidades;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ModeloApi.Controllers
{
    public class ExemploController : ApiController
    {
        private readonly IExemploServicosApp _servicosApp;

        public ExemploController(IExemploServicosApp servicosApp)
        {
            _servicosApp = servicosApp;
        }

        [HttpGet]
        public HttpResponseMessage Contar([FromUri]ExemploParametrosContarListar condicoes)
        {
            var total = _servicosApp.Contar(condicoes.Expressao());

            return Request.CreateResponse(HttpStatusCode.OK, total);
        }

        [HttpGet]
        public HttpResponseMessage Buscar([FromUri]ExemploParametrosBuscar condicoes)
        {
            var entidade = _servicosApp.Buscar(condicoes.Expressao());

            return Request.CreateResponse(HttpStatusCode.OK, entidade);
        }

        [HttpGet]
        public HttpResponseMessage Listar([FromUri]ExemploParametrosContarListar condicoes, [FromUri]string ordenarPor = null, [FromUri]int? deslocamento = null, [FromUri]int? limite = null)
        {
            var lista = _servicosApp.Listar(condicoes.Expressao(), ordenarPor ?? "Codigo", deslocamento ?? 0, limite ?? 100);

            return Request.CreateResponse(HttpStatusCode.OK, lista);
        }

        [HttpGet]
        public HttpResponseMessage ObterPorChave(object id)
        {
            var exemplo = _servicosApp.ObterPorChave(id);

            return Request.CreateResponse(HttpStatusCode.OK, exemplo);
        }

        [HttpPost]
        public HttpResponseMessage Inserir([FromBody]Exemplo exemplo)
        {
            _servicosApp.Inserir(exemplo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Atualizar([FromBody]Exemplo exemplo)
        {
            _servicosApp.Atualizar(exemplo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Remover([FromBody]Exemplo exemplo)
        {
            _servicosApp.Remover(exemplo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Mesclar([FromBody]Exemplo exemplo)
        {
            _servicosApp.Mesclar(exemplo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage InserirEmMassa([FromBody]IEnumerable<Exemplo> exemplos)
        {
            _servicosApp.InserirEmMassa(exemplos);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage AtualizarEmMassa([FromBody]IEnumerable<Exemplo> exemplos)
        {
            _servicosApp.AtualizarEmMassa(exemplos);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage RemoverEmMassa([FromBody]IEnumerable<Exemplo> exemplos)
        {
            _servicosApp.RemoverEmMassa(exemplos);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage MesclarEmMassa([FromBody]IEnumerable<Exemplo> exemplos)
        {
            _servicosApp.MesclarEmMassa(exemplos);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}