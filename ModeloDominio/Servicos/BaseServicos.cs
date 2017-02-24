using ModeloDominio.Interfaces.Repositorios;
using ModeloDominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ModeloDominio.Entidades;

namespace ModeloDominio.Servicos
{
    public abstract class BaseServicos<TEntidade> : IDisposable, IBaseServicos<TEntidade> where TEntidade : class
    {
        private readonly IBaseRepositorio<TEntidade> _repositorio;

        protected BaseServicos(IBaseRepositorio<TEntidade> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }

        public void AtivarRestricoes()
        {
            _repositorio.AtivarRestricoes();
        }

        public void DesativarRestricoes()
        {
            _repositorio.DesativarRestricoes();
        }

        public void TruncarTabela(string tabela)
        {
            _repositorio.TruncarTabela(tabela);
        }

        public bool Inserir(TEntidade entidade)
        {
            return _repositorio.Inserir(entidade);
        }

        public bool Atualizar(TEntidade entidade)
        {
            return _repositorio.Atualizar(entidade);
        }

        public bool Remover(TEntidade entidade)
        {
            return _repositorio.Remover(entidade);
        }

        public bool Mesclar(TEntidade entidade)
        {
            return _repositorio.Mesclar(entidade);
        }

        public bool InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _repositorio.InserirEmMassa(entidades);
        }

        public bool AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _repositorio.AtualizarEmMassa(entidades);
        }

        public bool RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _repositorio.RemoverEmMassa(entidades);
        }

        public bool MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            return _repositorio.MesclarEmMassa(entidades);
        }

        public TEntidade ObterPorChave(object chave)
        {
            return _repositorio.ObterPorChave(chave);
        }

        public TEntidade ObterPorChave(object[] chave)
        {
            return _repositorio.ObterPorChave(chave);
        }

        public Contagem Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return _repositorio.Contar(condicoes);
        }

        public TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false)
        {
            return _repositorio.Buscar(condicoes, noContexto);
        }

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int? deslocamento = null, int? limite = null, bool noContexto = false)
        {
            return _repositorio.Listar(condicoes, ordenarPor, deslocamento, limite, noContexto);
        }
    }
}