using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ModeloDominio.Entidades;

namespace ModeloDominio.Interfaces.Servicos
{
    public interface IBaseServicos<TEntidade> where TEntidade : class
    {
        void Dispose();

        void AtivarRestricoes();

        void DesativarRestricoes();

        void TruncarTabela(string tabela);

        bool Inserir(TEntidade entidade);

        bool Atualizar(TEntidade entidade);

        bool Remover(TEntidade entidade);

        bool Mesclar(TEntidade entidade);

        bool InserirEmMassa(IEnumerable<TEntidade> entidades);

        bool AtualizarEmMassa(IEnumerable<TEntidade> entidades);

        bool RemoverEmMassa(IEnumerable<TEntidade> entidades);

        bool MesclarEmMassa(IEnumerable<TEntidade> entidades);

        TEntidade ObterPorChave(object chave);

        TEntidade ObterPorChave(object[] chave);

        Contagem Contar(Expression<Func<TEntidade, bool>> condicoes = null);

        TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false);

        IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int? deslocamento = null, int? limite = null, bool noContexto = false);
    }
}