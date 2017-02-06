using ModeloDominio.Interfaces.Repositorios;
using ModeloDominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public void Inserir(TEntidade entidade)
        {
            _repositorio.Inserir(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            _repositorio.Atualizar(entidade);
        }

        public void Remover(TEntidade entidade)
        {
            _repositorio.Remover(entidade);
        }

        public void Mesclar(TEntidade entidade)
        {
            _repositorio.Mesclar(entidade);
        }

        public void InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            _repositorio.InserirEmMassa(entidades);
        }

        public void AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            _repositorio.AtualizarEmMassa(entidades);
        }

        public void RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            _repositorio.RemoverEmMassa(entidades);
        }

        public void MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            _repositorio.MesclarEmMassa(entidades);
        }

        public TEntidade ObterPorChave(object chave)
        {
            return _repositorio.ObterPorChave(chave);
        }

        public TEntidade ObterPorChave(object[] chave)
        {
            return _repositorio.ObterPorChave(chave);
        }

        public object Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return _repositorio.Contar(condicoes);
        }

        public TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false)
        {
            return _repositorio.Buscar(condicoes, noContexto);
        }

        public IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int deslocamento = -1, int limite = -1, bool noContexto = false)
        {
            return _repositorio.Listar(condicoes, ordenarPor, deslocamento, limite, noContexto);
        }
    }
}