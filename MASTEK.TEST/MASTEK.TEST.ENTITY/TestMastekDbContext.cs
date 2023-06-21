using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MASTEK.TEST.ENTITY;

public partial class TestMastekDbContext : DbContext
{
    public TestMastekDbContext()
    {
    }

    public TestMastekDbContext(DbContextOptions<TestMastekDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bar> Bars { get; set; }

    public virtual DbSet<BarBeersMapping> BarBeersMappings { get; set; }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brewery> Breweries { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=104.198.178.39;port=3306;user=ef;password=Ef@2023;database=test-mastek-db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Bar");

            entity.Property(e => e.Id)
                .HasComment("Primary Key")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BarBeersMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Bar_Beers_mapping");

            entity.Property(e => e.BarId).HasColumnType("mediumtext");
            entity.Property(e => e.BeerId).HasColumnType("mediumtext");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
        });

        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Beer");

            entity.HasIndex(e => e.BreweryId, "BreweryId");

            entity.Property(e => e.Id)
                .HasComment("Primary Key")
                .HasColumnName("id");

            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.PercentageAlcoholByVolume).HasColumnType("mediumtext");

            entity.HasOne(d => d.Brewery).WithMany(p => p.Beers)
                .HasForeignKey(d => d.BreweryId)
                .HasConstraintName("Beer_ibfk_1");

        });

        modelBuilder.Entity<Brewery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Brewery");

            entity.Property(e => e.Id)
                .HasComment("Primary Key")
                .HasColumnName("id");

            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
