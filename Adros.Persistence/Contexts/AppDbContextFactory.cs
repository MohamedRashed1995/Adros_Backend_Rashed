using Adros.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Adros.Persistence.Contexts
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // استخدم الـ connection string بتاعتك مباشرة
            optionsBuilder.UseSqlServer(
                "Server=db36362.public.databaseasp.net; Database=db36362; User Id=db36362; Password=H_b2zE4#!9yB; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
