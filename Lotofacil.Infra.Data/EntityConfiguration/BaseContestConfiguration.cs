using Lotofacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Infra.Data.EntityConfiguration
{
    public class BaseContestConfiguration : IEntityTypeConfiguration<BaseContest>//Utilizando o Fluent API
    {
        public void Configure(EntityTypeBuilder<BaseContest> builder)
        {
            // Nome da tabela
            builder.ToTable("BaseContest");

            // Chave primária
            builder.HasKey(b => b.Id);

            // Propriedade Id
            builder.Property(b => b.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            // Propriedade Name
            builder.Property(b => b.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(20)
                   .IsRequired();

            // Propriedade Data
            builder.Property(b => b.Data)
                   .HasColumnName("Data")
                   .IsRequired();

            // Propriedade Numbers
            builder.Property(b => b.Numbers)
                   .HasColumnName("Numbers")
                   .HasMaxLength(45)
                   .IsRequired();

            // Configuração dos atributos Hit11 a Hit15
            builder.Property(b => b.Hit11)
                   .HasColumnName("Hit11");

            builder.Property(b => b.Hit12)
                   .HasColumnName("Hit12");

            builder.Property(b => b.Hit13)
                   .HasColumnName("Hit13");

            builder.Property(b => b.Hit14)
                   .HasColumnName("Hit14");

            builder.Property(b => b.Hit15)
                   .HasColumnName("Hit15");

            // Propriedade CreatedAt
            builder.Property(b => b.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();

            // Relacionamento com Contest (ContestsAbove11)
            builder.HasMany(b => b.ContestsAbove11)
                   .WithOne() // Sem FK de volta para BaseContest em Contest
                   .HasForeignKey("BaseContestId") // Chave estrangeira para BaseContest
                   .OnDelete(DeleteBehavior.Cascade); // Configuração de exclusão em cascata
                    //Ao Excluir um BaseContest, todos os Contests relacionados a ele serão excluídos também
        }
    }
}
