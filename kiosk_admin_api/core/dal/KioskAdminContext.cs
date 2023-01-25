using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using core.models.DbEntities;

namespace core.dal;

public partial class KioskAdminContext : DbContext
{
    private readonly IConfigurationRoot _appConfig;
    public KioskAdminContext()
    {
        _appConfig = new ConfigurationBuilder().AddUserSecrets<KioskAdminContext>().Build();
    }

    public KioskAdminContext(DbContextOptions<KioskAdminContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Direction> Directions { get; set; }

    public virtual DbSet<TrainStation> TrainStations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_appConfig["ConnectionStrings:KioskDb"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.DirectionId).HasName("PK__directio__FAAC47DB48316779");

            entity.ToTable("direction");

            entity.Property(e => e.DirectionId)
                .ValueGeneratedNever()
                .HasColumnName("direction_id");
            entity.Property(e => e.DirectionName)
                .HasMaxLength(200)
                .HasColumnName("direction_name");
            entity.Property(e => e.TrainstationId).HasColumnName("trainstation_id");

            entity.HasOne(d => d.Trainstation).WithMany(p => p.Directions)
                .HasForeignKey(d => d.TrainstationId)
                .HasConstraintName("FK_Direction_Train_Station");
        });

        modelBuilder.Entity<TrainStation>(entity =>
        {
            entity.HasKey(e => e.TrainstationId).HasName("PK__Train_St__AA3A51EE76EF8A69");

            entity.ToTable("Train_Station");

            entity.Property(e => e.TrainstationId)
                .ValueGeneratedNever()
                .HasColumnName("trainstation_id");
            entity.Property(e => e.TrainstationName)
                .HasMaxLength(50)
                .HasColumnName("trainstation_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CDF2F4CE4C1");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC57238D26D87").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(32)
                .HasColumnName("userID");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
