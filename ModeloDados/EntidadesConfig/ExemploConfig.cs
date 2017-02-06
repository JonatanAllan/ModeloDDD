using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ModeloDominio.Entidades;

namespace ModeloDados.EntidadesConfig
{
    public class ExemploConfig : EntityTypeConfiguration<Exemplo>
    {
        public ExemploConfig()
        {
            // Tabela
            ToTable("Exemplos");

            // Chave primária
            HasKey(t => new { t.Codigo, t.CodigoAdicional });

            // Propriedades
            Property(t => t.Codigo)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.CodigoAdicional)
                .IsRequired()
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Nome)
                .IsOptional()
                .HasColumnOrder(2)
                .HasColumnName("Nome")
                .HasMaxLength(60);

            Property(t => t.Descricao)
                .IsOptional()
                .HasColumnOrder(2)
                .HasColumnName("Descricao")
                .HasColumnType("TEXT");

            Property(t => t.DataAtualizacaoRegistro);
            Property(t => t.DataCriacaoRegistro);
        }
    }
}