using AstreeWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AstreeWebApp.Models;

namespace AstreeWebApp.Areas.Identity.Data;

public class AstreeDbContext : IdentityDbContext<AstreeUser, IdentityRole<Guid>, Guid>
{
    public AstreeDbContext(DbContextOptions<AstreeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

public DbSet<AstreeWebApp.Models.Policy> Policy { get; set; } = default!;

public DbSet<AstreeWebApp.Models.Payment> Payment { get; set; } = default!;

public DbSet<AstreeWebApp.Models.Claim> Claim { get; set; } = default!;

public DbSet<AstreeWebApp.Models.Document> Document { get; set; } = default!;
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AstreeUser>
{
    public void Configure(EntityTypeBuilder<AstreeUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
    }
}
