using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{


        public class DataContext : DbContext
        {

            public DbSet<ClientModel> ClientModel { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options
                .UseSqlServer("Server=127.0.0.1,1433;Initial Catalog=DATABASE;User ID=sa;Password=SqlServer2019!;TrustServerCertificate=True;");

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ClientModel>();
            }
        }
    
}
