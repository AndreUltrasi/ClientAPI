using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClientContext>
    {
        public ClientContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=DATABASE;User ID=sa;Password=NovaSenhaForte123!;TrustServerCertificate=True;");

            return new ClientContext(optionsBuilder.Options);
        }
    }
}
