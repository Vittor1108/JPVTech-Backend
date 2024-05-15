using JPVTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Data.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(180)
                .IsRequired(true);

            builder.Property(x => x.IncomeValue)
                .HasColumnType("decimal(10, 2)")
                .IsRequired(true);

            builder.Property(x => x.DateBirth)
                .IsRequired(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.CPF)
                .IsRequired(true)
                .HasMaxLength(11);

            builder.HasIndex(x => x.CPF)
                .IsUnique();
        }
    }
}
