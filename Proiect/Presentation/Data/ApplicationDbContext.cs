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
        
        
        builder.Entity<Account>()
            .HasMany<Contract>()
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Publisher>()
            .HasMany<Contract>()
            .WithOne(x => x.Publisher)
            .HasForeignKey(x => x.PublisherId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Account>()
            .HasMany<Creative>()
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Account>()
            .HasMany<TargetingRule>()
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Account>()
            .HasMany<LineItem>()
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Creative>()
            .HasMany<LineItem>()
            .WithOne(x => x.Creative)
            .HasForeignKey(x => x.CreativeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Contract>()
            .HasMany<LineItem>()
            .WithOne(x => x.Contract)
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LineItem>()
            .HasMany<TargetingRule>(x => x.TargetingRules)
            .WithMany(x => x.LineItems);
        
    }
}