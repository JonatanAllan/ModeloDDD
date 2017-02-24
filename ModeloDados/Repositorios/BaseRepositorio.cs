using ModeloDados.Contextos;
using ModeloDados.Recursos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ModeloDominio.Entidades;
using ModeloDominio.Interfaces.Repositorios;

namespace ModeloDados.Repositorios
{
    public abstract class BaseRepositorio<TEntidade> : IDisposable, IBaseRepositorio<TEntidade> where TEntidade : class
    {
        protected readonly BaseContexto Db = new BaseContexto();

        public void Dispose()
        {
            Db.Dispose();
        }

        public void AtivarRestricoes()
        {
            Db.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL\"");
            Db.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"ALTER TABLE ? ENABLE TRIGGER ALL\"");
        }

        public void DesativarRestricoes()
        {
            Db.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"ALTER TABLE ? NOCHECK CONSTRAINT ALL\"");
            Db.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"ALTER TABLE ? DISABLE TRIGGER ALL\"");
        }

        public void TruncarTabela(string tabela)
        {
            Db.Database.ExecuteSqlCommand("EXEC TruncarTabela @p0", tabela);

            //Db.Database.ExecuteSqlCommand("TRUNCATE TABLE @p0", tabela);
            //Db.Set<TEntidade>().RemoveRange(Db.Set<TEntidade>());
            //Db.SaveChanges();
        }

        public virtual bool Inserir(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Added;
            return Salvar();
        }

        public virtual bool Atualizar(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Modified;
            return Salvar();
        }

        public virtual bool Remover(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Deleted;
            return Salvar();
        }

        public virtual bool Mesclar(TEntidade entidade)
        {
            Db.Set<TEntidade>().AddOrUpdate(entidade);
            return Salvar();
        }

        public virtual bool InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Added;
            }
            Db.Configuration.AutoDetectChangesEnabled = true;
            return Salvar();
        }

        public virtual bool AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Modified;
            }
            Db.Configuration.AutoDetectChangesEnabled = true;
            return Salvar();
        }

        public virtual bool RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Deleted;
            }
            Db.Configuration.AutoDetectChangesEnabled = true;
            return Salvar();
        }

        public virtual bool MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Set<TEntidade>().AddOrUpdate(entidade);
            }
            Db.Configuration.AutoDetectChangesEnabled = true;
            return Salvar();
        }

        public virtual TEntidade ObterPorChave(object chave)
        {
            return ObterPorChave(new[] { chave });
        }

        public virtual TEntidade ObterPorChave(object[] chave)
        {
            return Db.Set<TEntidade>().Find(chave);
        }

        public virtual Contagem Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return condicoes == null
                ? new Contagem { Total = Db.Set<TEntidade>().AsNoTracking().Count() }
                : new Contagem { Total = Db.Set<TEntidade>().AsNoTracking().Count(condicoes) };
        }

        public virtual TEntidade Buscar(Expression<Func<TEntidade, bool>> condicoes = null, bool noContexto = false)
        {
            if (condicoes != null)
            {
                return noContexto
                    ? Db.Set<TEntidade>().FirstOrDefault(condicoes)
                    : Db.Set<TEntidade>().AsNoTracking().FirstOrDefault(condicoes);
            }

            return noContexto
                ? Db.Set<TEntidade>().FirstOrDefault()
                : Db.Set<TEntidade>().AsNoTracking().FirstOrDefault();
        }

        public virtual IQueryable<TEntidade> Listar(Expression<Func<TEntidade, bool>> condicoes = null,
            string ordenarPor = null, int? deslocamento = null, int? limite = null, bool noContexto = false)
        {
            // Ordenado Paginado Condicional
            if (condicoes != null && ordenarPor != null && deslocamento != null && limite != null)
            {
                return noContexto
                    ? Db.Set<TEntidade>()
                        .Where(condicoes)
                        .OrderBy(ordenarPor)
                        .Skip(deslocamento.Value)
                        .Take(limite.Value)
                    : Db.Set<TEntidade>()
                        .AsNoTracking()
                        .Where(condicoes)
                        .OrderBy(ordenarPor)
                        .Skip(deslocamento.Value)
                        .Take(limite.Value);
            }

            // Ordenado Paginado
            if (condicoes == null && ordenarPor != null && deslocamento != null && limite != null)
            {
                return noContexto
                    ? Db.Set<TEntidade>().OrderBy(ordenarPor).Skip(deslocamento.Value).Take(limite.Value)
                    : Db.Set<TEntidade>().AsNoTracking().OrderBy(ordenarPor).Skip(deslocamento.Value).Take(limite.Value);
            }

            // Ordenado Condicional
            if (condicoes != null && ordenarPor != null && deslocamento == null && limite == null)
            {
                return noContexto
                    ? Db.Set<TEntidade>().Where(condicoes).OrderBy(ordenarPor)
                    : Db.Set<TEntidade>().AsNoTracking().Where(condicoes).OrderBy(condicoes);
            }

            // Ordenado
            if (condicoes == null && ordenarPor != null && deslocamento == null && limite == null)
            {
                return noContexto
                    ? Db.Set<TEntidade>().OrderBy(ordenarPor)
                    : Db.Set<TEntidade>().AsNoTracking().OrderBy(ordenarPor);
            }

            // Condicional
            if (condicoes != null && ordenarPor == null && deslocamento == null && limite == null)
            {
                return noContexto
                    ? Db.Set<TEntidade>().Where(condicoes)
                    : Db.Set<TEntidade>().AsNoTracking().Where(condicoes);
            }

            // Tudo
            return noContexto
                ? Db.Set<TEntidade>()
                : Db.Set<TEntidade>().AsNoTracking();
        }

        protected bool Salvar()
        {
            return Db.SaveChanges() > 0;
        }
    }
}