using ModeloAplicacao.Interfaces.ServicosApp;
using ModeloDominio.Entidades;
using ModeloDominio.Interfaces.Servicos;

namespace ModeloAplicacao.ServicosApp
{
    public class ExemploServicosApp : BaseServicosApp<Exemplo>, IExemploServicosApp
    {
        private readonly IExemploServicos _servicos;

        public ExemploServicosApp(IExemploServicos servicos)
            : base (servicos)
        {
            _servicos = servicos;
        }
    }
}