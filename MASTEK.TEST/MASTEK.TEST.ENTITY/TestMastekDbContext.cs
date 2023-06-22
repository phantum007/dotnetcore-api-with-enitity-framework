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

    public virtual DbSet<BreweryBeersMapping> BreweryBeersMappings { get; set; }


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

            entity.HasIndex(e => e.BarId, "BarId");

            entity.HasIndex(e => e.BeerId, "BeerId");

            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

            entity.HasOne(d => d.Bar).WithMany()
                .HasForeignKey(d => d.BarId)
                .HasConstraintName("Bar_Beers_mapping_ibfk_1");

            entity.HasOne(d => d.Beer).WithMany()
                .HasForeignKey(d => d.BeerId)
                .HasConstraintName("Bar_Beers_mapping_ibfk_2");
        });

        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Beer");

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

        modelBuilder.Entity<BreweryBeersMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Brewery_Beers_mapping");

            entity.HasIndex(e => e.BreweryId, "BarId");

            entity.HasIndex(e => e.BeerId, "BeerId");

            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

            entity.HasOne(d => d.Beer).WithMany()
                .HasForeignKey(d => d.BeerId)
                .HasConstraintName("Brewery_Beers_mapping_ibfk_2");

            entity.HasOne(d => d.Brewery).WithMany()
                .HasForeignKey(d => d.BreweryId)
                .HasConstraintName("Brewery_Beers_mapping_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
