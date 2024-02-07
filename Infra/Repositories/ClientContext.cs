using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ClientContext : DbContext
    {
        public DbSet<ClientModel> Clients { get; set; } = null!;

        public ClientContext(DbContextOptions<ClientContext> options)
        : base(options)
        { }

        //public async Task<bool> CommitAsync()
        //{
        //    return await SaveChangesAsync() > 0;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientModel>();
        }
    }
}
