using ModeloDados.Contextos;
using ModeloDados.Recursos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
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

        public virtual void Inserir(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Added;
            Db.SaveChanges();
        }

        public virtual void Atualizar(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public virtual void Remover(TEntidade entidade)
        {
            Db.Entry(entidade).State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public virtual void Mesclar(TEntidade entidade)
        {
            Db.Set<TEntidade>().AddOrUpdate(entidade);
            Db.SaveChanges();
        }

        public virtual void InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Added;
            }
            Db.SaveChanges();
            Db.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual void AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Modified;
            }
            Db.SaveChanges();
            Db.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual void RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Entry(entidade).State = EntityState.Deleted;
            }
            Db.SaveChanges();
            Db.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual void MesclarEmMassa(IEnumerable<TEntidade> entidades)
        {
            Db.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entidade in entidades)
            {
                Db.Set<TEntidade>().AddOrUpdate(entidade);
            }
            Db.SaveChanges();
            Db.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual TEntidade ObterPorChave(object chave)
        {
            return ObterPorChave(new[] { chave });
        }

        public virtual TEntidade ObterPorChave(object[] chave)
        {
            return Db.Set<TEntidade>().Find(chave);
        }

        public virtual object Contar(Expression<Func<TEntidade, bool>> condicoes = null)
        {
            return condicoes == null
                ? new { Total = Db.Set<TEntidade>().AsNoTracking().Count() }
                : new { Total = Db.Set<TEntidade>().AsNoTracking().Count(condicoes) };
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
            string ordenarPor = null, int deslocamento = -1, int limite = -1, bool noContexto = false)
        {
            // Ordenado Paginado Condicional
            if (condicoes != null && ordenarPor != null && deslocamento > -1 && limite > 0)
            {
                return noContexto
                    ? Db.Set<TEntidade>().Where(condicoes).OrderBy(ordenarPor).Skip(deslocamento).Take(limite)
                    : Db.Set<TEntidade>().AsNoTracking().Where(condicoes).OrderBy(ordenarPor).Skip(deslocamento).Take(limite);
            }

            // Ordenado Paginado
            if (condicoes == null && ordenarPor != null && deslocamento > -1 && limite > 0)
            {
                return noContexto
                    ? Db.Set<TEntidade>().OrderBy(ordenarPor).Skip(deslocamento).Take(limite)
                    : Db.Set<TEntidade>().AsNoTracking().OrderBy(ordenarPor).Skip(deslocamento).Take(limite);
            }

            // Ordenado Condicional
            if (condicoes != null && ordenarPor != null && deslocamento < 0 && limite < 1)
            {
                return noContexto
                    ? Db.Set<TEntidade>().Where(condicoes).OrderBy(ordenarPor)
                    : Db.Set<TEntidade>().AsNoTracking().Where(condicoes).OrderBy(condicoes);
            }

            // Ordenado
            if (condicoes == null && ordenarPor != null && deslocamento < 0 && limite < 1)
            {
                return noContexto
                    ? Db.Set<TEntidade>().OrderBy(ordenarPor)
                    : Db.Set<TEntidade>().AsNoTracking().OrderBy(ordenarPor);
            }

            // Condicional
            if (condicoes != null && ordenarPor == null && deslocamento < 0 && limite < 1)
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
    }
}