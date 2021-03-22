using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using System.Collections.Generic;

namespace Api1.Entities
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(128);

            builder.HasData(new List<Catalog>
            {
                new Catalog(1, "Catalog 1", 1000),
                new Catalog(2, "Catalog 2", 2000),
                new Catalog(3, "Catalog 3", 3000),
                new Catalog(4, "Catalog 4", 4000),
                new Catalog(5, "Catalog 5", 5000),
                new Catalog(6, "Catalog 6", 6000),
                new Catalog(7, "Catalog 7", 7000),
                new Catalog(8, "Catalog 8", 8000),
                new Catalog(9, "Catalog 9", 9000)
            });
        }
    }
}
