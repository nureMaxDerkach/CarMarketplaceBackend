using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<City> Cities { get; set; }
    
    public DbSet<Region> Regions { get; set; }

    public DbSet<Country> Countries { get; set; }
    public DbSet<SaleNotice> SaleNotices { get; set; }

    public DbSet<SaleNoticeComment> SaleNoticeComments { get; set; }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarPhoto> CarPhotos { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }

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

        modelBuilder.Entity<SaleNoticeComment>()
            .HasOne(s => s.SaleNotice)
            .WithMany(c => c.SaleNoticeComments)
            .HasForeignKey(key => key.SaleNoticeId);

        modelBuilder.Entity<SaleNoticeComment>()
            .HasOne(u => u.User)
            .WithMany(c => c.SaleNoticeComments)
            .HasForeignKey(key => key.UserId);

        modelBuilder.Entity<Car>()
            .HasOne(m => m.Model)
            .WithOne(c => c.Car)
            .HasForeignKey<Car>(key => key.ModelId);

        modelBuilder.Entity<CarPhoto>()
            .HasOne(c => c.Car)
            .WithMany(p => p.CarPhotos)
            .HasForeignKey(key => key.CarId);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.CityId)
            .IsUnique(false);

        modelBuilder.Entity<Car>()
            .HasIndex(m => m.ModelId)
            .IsUnique(false);

        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Ukraine" }
        );

        modelBuilder.Entity<Region>().HasData(
            new Region { Id = 1, Name = "Kyiv Region", CountryId = 1 },
            new Region { Id = 2, Name = "Lviv Region", CountryId = 1 },
            new Region { Id = 3, Name = "Odesa Region", CountryId = 1 },
            new Region { Id = 4, Name = "Kharkiv Region", CountryId = 1 },
            new Region { Id = 5, Name = "Dnipro Region", CountryId = 1 },
            new Region { Id = 6, Name = "Vinnytsia Region", CountryId = 1 },
            new Region { Id = 7, Name = "Zaporizhzhia Region", CountryId = 1 },
            new Region { Id = 8, Name = "Ivano-Frankivsk Region", CountryId = 1 }
        );

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "Kyiv", RegionId = 1 },
            new City { Id = 2, Name = "Lviv", RegionId = 2 },
            new City { Id = 3, Name = "Odesa", RegionId = 3 },
            new City { Id = 4, Name = "Kharkiv", RegionId = 4 },
            new City { Id = 5, Name = "Dnipro", RegionId = 5 },
            new City { Id = 6, Name = "Vinnytsia", RegionId = 6 },
            new City { Id = 7, Name = "Zaporizhzhia", RegionId = 7 },
            new City { Id = 8, Name = "Ivano-Frankivsk", RegionId = 8 }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Toyota" },
            new Brand { Id = 2, Name = "BMW" },
            new Brand { Id = 3, Name = "Mercedes-Benz" },
            new Brand { Id = 4, Name = "Ford" }
        );

        modelBuilder.Entity<Model>().HasData(
            new Model { Id = 1, Name = "Corolla", BrandId = 1 },
            new Model { Id = 2, Name = "Camry", BrandId = 1 },
            new Model { Id = 3, Name = "X5", BrandId = 2 },
            new Model { Id = 4, Name = "M3", BrandId = 2 },
            new Model { Id = 5, Name = "C-Class", BrandId = 3 },
            new Model { Id = 6, Name = "E-Class", BrandId = 3 },
            new Model { Id = 7, Name = "Focus", BrandId = 4 },
            new Model { Id = 8, Name = "Mustang", BrandId = 4 }
        );
    }
} 
