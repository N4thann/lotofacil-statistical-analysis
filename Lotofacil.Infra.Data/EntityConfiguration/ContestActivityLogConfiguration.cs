using Lotofacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lotofacil.Infra.Data.EntityConfiguration
{
    public class ContestActivityLogConfiguration : IEntityTypeConfiguration<ContestActivityLog>
    {
        public void Configure(EntityTypeBuilder<ContestActivityLog> builder)
        {
            builder.ToTable("ContestActivityLog");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(b => b.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(b => b.Data)
                   .HasColumnName("Data")
                   .IsRequired();

            builder.Property(b => b.Numbers)
                   .HasColumnName("Numbers")
                   .HasMaxLength(45)
                   .IsRequired();

            builder.Property(c => c.BaseContestName)
                .HasColumnName("BaseContestName")
                .HasMaxLength(20) 
                .IsRequired();

            // Propriedade ContestNumbers
            builder.Property(c => c.BaseContestNumbers)
                .HasColumnName("BaseContestNumbers")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(c => c.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();
        }
    }
}
/*Essa abordagem foi utilizada para evitar que as entidades ficassem poluídas com 
 * o uso dos Data Annotations, assim fiz essa configuração para definir as propriedades
 * dos atributos das tabelas(Não confundir com as validações no frontend com Fluent Validations)*/
