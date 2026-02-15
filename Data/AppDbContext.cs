using FeedsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "skye.lumen", FullName = "Skye Lumen" },
            new User { Id = 2, Username = "lisa.nova", FullName = "Lisa Nova" },
            new User { Id = 3, Username = "cypress.vale", FullName = "Cypres Vale" }
        );
    }
}
