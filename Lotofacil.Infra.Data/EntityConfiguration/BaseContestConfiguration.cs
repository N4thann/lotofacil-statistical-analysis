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
    public class BaseContestConfiguration : IEntityTypeConfiguration<BaseContest>
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

            // Propriedade BaseNumbers
            builder.Property(b => b.Numbers)
                   .HasColumnName("Numbers")
                   .HasMaxLength(45)
                   .IsRequired();

            // Relacionamento com Contest (ContestsAbove11)
            builder.HasMany(b => b.ContestsAbove11)
                   .WithOne() // Supondo que o Contest não tenha uma FK direta de volta
                   .HasForeignKey("BaseContestId"); // Adicione a FK para vincular ao BaseContest
        }
    }
}
