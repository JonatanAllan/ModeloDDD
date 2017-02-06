using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ModeloDados.EntidadesConfig;
using ModeloDominio.Entidades;

namespace ModeloDados.Contextos
{
    public class BaseContexto : DbContext
    {
        static BaseContexto()
        {
            Database.SetInitializer<BaseContexto>(null);
        }

        public BaseContexto() 
            : base("BaseConexao")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Exemplo> Exemplos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("VARCHAR"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(120));

            modelBuilder.Configurations.Add(new ExemploConfig());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacaoRegistro").CurrentValue = DateTime.Now;
                    entry.Property("DataAtualizacaoRegistro").CurrentValue = DateTime.Now;
                }

                if (entry.State != EntityState.Modified) continue;

                entry.Property("DataCriacaoRegistro").IsModified = false;
                entry.Property("DataAtualizacaoRegistro").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}