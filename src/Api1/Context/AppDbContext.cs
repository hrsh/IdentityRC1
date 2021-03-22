using Api1.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Api1.Context
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CatalogConfiguration());
        }

        public async Task<int> SaveAllChangesAsync(CancellationToken ct = default) =>
            await SaveChangesAsync(true, ct);

        public virtual DbSet<Catalog> Catalogs { get; set; }
    }
}
