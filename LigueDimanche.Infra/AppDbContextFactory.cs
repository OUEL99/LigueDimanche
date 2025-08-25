using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LigueDimanche.Infra
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Utilise une connection string par défaut pour les migrations
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LigueDimancheDB;Trusted_Connection=true;MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}