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

            // Configuração dos atributos Matched11 a Matched15
            builder.Property(b => b.Matched11)
                   .HasColumnName("Matched11");

            builder.Property(b => b.Matched12)
                   .HasColumnName("Matched12");

            builder.Property(b => b.Matched13)
                   .HasColumnName("Matched13");

            builder.Property(b => b.Matched14)
                   .HasColumnName("Matched14");

            builder.Property(b => b.Matched15)
                   .HasColumnName("Matched15");

            // Relacionamento com Contest (ContestsAbove11)
            builder.HasMany(b => b.ContestsAbove11)
                   .WithOne() // Sem FK de volta para BaseContest em Contest
                   .HasForeignKey("BaseContestId") // Chave estrangeira para BaseContest
                   .OnDelete(DeleteBehavior.Cascade); // Configuração de exclusão em cascata
                    //Ao Excluir um BaseContest, todos os Contests relacionados a ele serão excluídos também
        }
    }
}
