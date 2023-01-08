using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<Contract> Contracts { get; set; }
    
    public DbSet<TargetingRule> TargetingRules { get; set; }

    public DbSet<Creative> Creatives { get; set; }
    
    public DbSet<LineItem> LineItems { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<LineItem>()
            .HasMany<TargetingRule>(x => x.TargetingRules)
            .WithMany(x => x.LineItems);
        
    }
}