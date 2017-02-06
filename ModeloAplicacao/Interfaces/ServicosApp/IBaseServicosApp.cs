using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ModeloAplicacao.Interfaces.ServicosApp
{
    public interface IBaseServicosApp<TEntidade> where TEntidade : class
    {
        void Dispose();

        void AtivarRestricoes();

        void DesativarRestricoes();

        void TruncarTabela(string tabela);

        void Inserir(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        void Remover(TEntidade entidade);

        void Mesclar(TEntidade entidade);

        void InserirEmMassa(IEnumerable<TEntidade> entidades);

        void AtualizarEmMassa(IEnumerable<TEntidade> entidades);

        void RemoverEmMassa(IEnumerable<TEntidade> entidades);

        void MesclarEmMassa(IEnumerable<TEntidade> entidades);

        TEntidade ObterPorChave(object chave);

        TEntidade ObterPorChave(object[] chave);

        object Contar(Expression<Func<TEntidade, bool>> condicoes = null);

        TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false);

        IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int deslocamento = -1, int limite = -1, bool noContexto = false);
    }
}