using Microsoft.EntityFrameworkCore;
using Punk.Domain;
using Punk.Domain.Models;

namespace Punk.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FavouriteBeer> FavouriteBeers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FavouriteBeer>()
            .HasKey(fb => new { fb.UserId, fb.BeerId });

        builder.Entity<FavouriteBeer>()
            .HasOne(fb => fb.User)
            .WithMany(user => user.FavouriteBeers)
            .HasForeignKey(fb => fb.UserId);
    }
}