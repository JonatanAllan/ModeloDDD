using ModeloDominio.Entidades;
using ModeloDominio.Interfaces.Repositorios;
using ModeloDominio.Interfaces.Servicos;

namespace ModeloDominio.Servicos
{
    public class ExemploServicos : BaseServicos<Exemplo>, IExemploServicos
    {
        private readonly IExemploRepositorio _repositorio;

        public ExemploServicos(IExemploRepositorio repositorio)
            : base (repositorio)
        {
            _repositorio = repositorio;
        }
    }
}