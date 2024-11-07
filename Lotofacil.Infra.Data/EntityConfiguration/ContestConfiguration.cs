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
    public class ContestConfiguration : IEntityTypeConfiguration<Contest>
    {
        public void Configure(EntityTypeBuilder<Contest> builder)
        {
            // Nome da tabela
            builder.ToTable("Contest");

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
        }
    }
}
