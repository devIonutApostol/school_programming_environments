using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Presentation.Models;

namespace Presentation.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<Contract> Contracts { get; set; }
    
    public DbSet<TargetingRule> TargetingRules { get; set; }

    public DbSet<Creative> Creatives { get; set; }
    
    public DbSet<LineItem> LineItems { get; set; }

    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
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