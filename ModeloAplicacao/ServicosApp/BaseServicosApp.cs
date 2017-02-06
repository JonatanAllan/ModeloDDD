using ModeloAplicacao.Interfaces.ServicosApp;
using ModeloDominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ModeloAplicacao.ServicosApp
{
    public abstract class BaseServicosApp<TEntidade> : IDisposable, IBaseServicosApp<TEntidade> where TEntidade : class
    {
        private readonly IBaseServicos<TEntidade> _servicos;

        protected BaseServicosApp(IBaseServicos<TEntidade> servicos)
        {
            _servicos = servicos;
        }

        public void Dispose()
        {
            _servicos.Dispose();
        }

        public void AtivarRestricoes()
        {
            _servicos.AtivarRestricoes();
        }

        public void DesativarRestricoes()
        {
            _servicos.DesativarRestricoes();
        }

        public void TruncarTabela(string tabela)
        {
            _servicos.TruncarTabela(tabela);
        }

        public void Inserir(TEntidade entidade)
        {
            _servicos.Inserir(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            _servicos.Atualizar(entidade);
        }

        public void Remover(TEntidade entidade)
        {
            _servicos.Remover(entidade);
        }

        public void Mesclar(TEntidade entidade)
        {
            _servicos.Mesclar(entidade);
        }

        public void InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            _servicos.InserirEmMassa(entidades);
        }

        public void AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            _servicos.AtualizarEmMassa(entidades);
        }

        public void RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            _servicos.RemoverEmMassa(entidades);
        }

        public void MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            _servicos.MesclarEmMassa(entidades);
        }

        public TEntidade ObterPorChave(object chave)
        {
            return _servicos.ObterPorChave(chave);
        }

        public TEntidade ObterPorChave(object[] chave)
        {
            return _servicos.ObterPorChave(chave);
        }

        public object Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return _servicos.Contar(condicoes);
        }

        public TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false)
        {
            return _servicos.Buscar(condicoes, noContexto);
        }

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int deslocamento = -1, int limite = -1, bool noContexto = false)
        {
            return _servicos.Listar(condicoes, ordenarPor, deslocamento, limite, noContexto);
        }
    }
}