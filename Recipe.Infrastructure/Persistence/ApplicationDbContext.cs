using Microsoft.EntityFrameworkCore;
using Recipe.Infrastructure.Persistence.Configuration;

namespace Recipe.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Recipe> Recipe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RecipeConfig());
    }
}