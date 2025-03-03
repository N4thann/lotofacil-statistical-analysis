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
    public class ContestConfiguration : ContestBaseEntityConfiguration<Contest>
    {
        protected override void AppendConfig(EntityTypeBuilder<Contest> builder)
        {
            builder.ToTable("Contest");

            //Propriedade LastProcessed 
            builder.Property(b => b.LastProcessed)
                .HasColumnName("LastProcessed")
                .IsRequired(false);
        }
    }
}
