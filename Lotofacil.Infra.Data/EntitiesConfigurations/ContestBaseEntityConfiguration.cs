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
    //Essa configuração segue o príncipio do DRY, pegando as configurações comuns
    //entre as 3 entidades e centralizando aqui. Nas outras entityconfigurations utilizamos
    //o método AppendConfig, visto que as outras configurações herdam essa
    public abstract class ContestBaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : ContestBaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
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

            AppendConfig(builder);
        }

        protected abstract void AppendConfig(EntityTypeBuilder<T> builder);
    }
}
