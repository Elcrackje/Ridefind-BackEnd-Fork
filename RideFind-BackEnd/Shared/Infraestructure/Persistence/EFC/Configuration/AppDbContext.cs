using RideFind_BackEnd.IAM;
using RideFind_BackEnd.Profiles;

using RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        //Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Publishing Context

        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(30);

        builder.Entity<Vehicle>().HasKey(t => t.Id);
        builder.Entity<Vehicle>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(t => t.VehicleName).IsRequired().HasMaxLength(50);
        builder.Entity<Vehicle>().Property(t => t.Description).HasMaxLength(150);

        builder.Entity<Asset>().HasDiscriminator(a => a.Type);
        builder.Entity<Asset>().HasKey(a => a.Id);
        builder.Entity<Asset>().HasDiscriminator<string>("asset_type")
            .HasValue<Asset>("asset_base")
            .HasValue<ImageAsset>("asset_image");

        builder.Entity<Asset>().OwnsOne(i => i.AssetIdentifier,
            ai =>
            {
                ai.WithOwner().HasForeignKey("Id");
                ai.Property(p => p.Identifier).HasColumnName("AssetIdentifier");
            });
        builder.Entity<ImageAsset>().Property(p => p.ImageUri).IsRequired();
        builder.Entity<Vehicle>().HasMany(t => t.Assets);

        //Category RelationShips
        builder.Entity<Category>().HasMany(c => c.Tutorials)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId)
            .HasPrincipalKey(t => t.Id);


        // Profile Context
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(p => p.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName).HasColumnName("FirstName");
                n.Property(p => p.LastName).HasColumnName("LastName");
            });
        builder.Entity<Profile>().OwnsOne(p => p.Email,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.Address).HasColumnName("EmailAddress");
            });
        builder.Entity<Profile>().OwnsOne(p => p.Address,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.Street).HasColumnName("AddressStreet");
                n.Property(p => p.Number).HasColumnName("AddressNumber");
                n.Property(p => p.city).HasColumnName("AddressCity");
                n.Property(p => p.PostalCode).HasColumnName("AddressPostalCode");
                n.Property(p => p.country).HasColumnName("AddressCountry");
            });
        builder.Entity<Profile>().Property(p => p.phoneNumber).HasColumnName("PhoneNumber").IsRequired();
        builder.Entity<Profile>().Property(p => p.vehicleName).HasColumnName("VehicleName").IsRequired();

        //IAM Context

        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();


        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}