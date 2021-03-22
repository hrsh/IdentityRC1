using Api2.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api2.Context
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public Task<int> SaveAllChangesAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogConfiguration());
        }

        public virtual DbSet<Blog> Blogs { get; set; }
    }
}
