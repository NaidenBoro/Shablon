using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Data.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<AppFile> Files { get; set; }
    public DbSet<AppEvent> Events { get; set; }

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
