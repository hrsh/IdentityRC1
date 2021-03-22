using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using System.Collections.Generic;

namespace Api2.Entities
{
    public class EntityConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(512);
            builder.Property(a => a.Body).IsRequired();

            builder.HasData(new List<Blog>
            {
                new Blog(1001, string.Format(ServiceDefaultConfig.BlogTitle, 1), ServiceDefaultConfig.BlogBody),
                new Blog(1002, string.Format(ServiceDefaultConfig.BlogTitle, 2), ServiceDefaultConfig.BlogBody),
                new Blog(1003, string.Format(ServiceDefaultConfig.BlogTitle, 3), ServiceDefaultConfig.BlogBody),
            });
        }
    }
}
