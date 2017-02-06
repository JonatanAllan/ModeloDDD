using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace ModeloRecursos.ViewModels
{
    public class RespostaViewModel<TEntidade> where TEntidade : class
    {
        [JsonProperty("CodigoStatus")]
        public HttpStatusCode CodigoStatus { get; set; }

        [JsonProperty("DescricaoStatus")]
        public string DescricaoStatus { get; set; }

        [JsonProperty("MensagemErro", NullValueHandling = NullValueHandling.Ignore)]
        public string MensagemErro { get; set; }

        [JsonProperty("Conteudo", NullValueHandling = NullValueHandling.Ignore)]
        public TEntidade Conteudo { get; set; }

        public RespostaViewModel(IRestResponse resposta)
        {
            CodigoStatus = resposta.StatusCode;
            DescricaoStatus = resposta.StatusDescription;

            if (CodigoStatus == HttpStatusCode.OK ||
                CodigoStatus == HttpStatusCode.Created ||
                CodigoStatus == HttpStatusCode.Accepted || 
                CodigoStatus == HttpStatusCode.NonAuthoritativeInformation || 
                CodigoStatus == HttpStatusCode.NoContent || 
                CodigoStatus == HttpStatusCode.ResetContent || 
                CodigoStatus == HttpStatusCode.PartialContent)
            {
                try
                {
                    Conteudo = JsonConvert.DeserializeObject<TEntidade>(resposta.Content);
                }
                catch (Exception ex)
                {
                    MensagemErro = ex.Message;
                }
            }
            else
            {
                MensagemErro = resposta.Content;
            }
        }
    }
}