using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<SaleNotice> SaleNotices { get; set; }

    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SaleNotice>()
            .HasOne(u => u.User)
            .WithMany(sn => sn.SaleNotices)
            .HasForeignKey(key => key.UserId);

        modelBuilder.Entity<SaleNotice>()
            .HasOne(c => c.Car)
            .WithMany(sn => sn.SaleNotices)
            .HasForeignKey(key => key.CarId);
    }
} 
