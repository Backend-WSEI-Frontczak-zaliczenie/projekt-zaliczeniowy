using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;

    public virtual DbSet<City> Cities { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<Region> Regions { get; set; } = null!;

    public virtual DbSet<Reservation> Reservations { get; set; } = null!;

    public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;

    public virtual DbSet<RestaurantsType> RestaurantsTypes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cities__3213E83F62CBBC7D");

            entity.ToTable("cities");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F9686E331");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment1).HasColumnName("comment");
            entity.Property(e => e.Restaurant).HasColumnName("restaurant");

            entity.HasOne(d => d.RestaurantNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Restaurant)
                .HasConstraintName("FK__comments__restau__4316F928");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__regions__3213E83F5995FCC2");

            entity.ToTable("regions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reservat__3213E83F42A20B71");

            entity.ToTable("reservations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Guest)
                .HasMaxLength(450)
                .HasColumnName("guest");
            entity.Property(e => e.Restaurant).HasColumnName("restaurant");

            entity.HasOne(d => d.GuestNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Guest)
                .HasConstraintName("FK__reservati__guest__46E78A0C");

            entity.HasOne(d => d.RestaurantNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Restaurant)
                .HasConstraintName("FK__reservati__resta__45F365D3");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__restaura__3213E83FCEB872CE");

            entity.ToTable("restaurants");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AdultOnly).HasColumnName("adult_only");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.City)
                .HasConstraintName("FK__restaurant__city__403A8C7D");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK__restaurant__type__3F466844");
        });

        modelBuilder.Entity<RestaurantsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__restaura__3213E83F87BAC261");

            entity.ToTable("restaurants_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Region).HasColumnName("region");

            entity.HasOne(d => d.RegionNavigation).WithMany(p => p.RestaurantsTypes)
                .HasForeignKey(d => d.Region)
                .HasConstraintName("FK__restauran__regio__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
