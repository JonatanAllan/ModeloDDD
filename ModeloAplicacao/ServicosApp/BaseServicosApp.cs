using ModeloAplicacao.Interfaces.ServicosApp;
using ModeloDominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ModeloDominio.Entidades;

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

        public bool Inserir(TEntidade entidade)
        {
            return _servicos.Inserir(entidade);
        }

        public bool Atualizar(TEntidade entidade)
        {
            return _servicos.Atualizar(entidade);
        }

        public bool Remover(TEntidade entidade)
        {
            return _servicos.Remover(entidade);
        }

        public bool Mesclar(TEntidade entidade)
        {
            return _servicos.Mesclar(entidade);
        }

        public bool InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _servicos.InserirEmMassa(entidades);
        }

        public bool AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _servicos.AtualizarEmMassa(entidades);
        }

        public bool RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _servicos.RemoverEmMassa(entidades);
        }

        public bool MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _servicos.MesclarEmMassa(entidades);
        }

        public TEntidade ObterPorChave(object chave)
        {
            return _servicos.ObterPorChave(chave);
        }

        public TEntidade ObterPorChave(object[] chave)
        {
            return _servicos.ObterPorChave(chave);
        }

        public Contagem Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return _servicos.Contar(condicoes);
        }

        public TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false)
        {
            return _servicos.Buscar(condicoes, noContexto);
        }

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int? deslocamento = null, int? limite = null, bool noContexto = false)
        {
            return _servicos.Listar(condicoes, ordenarPor, deslocamento, limite, noContexto);
        }
    }
}