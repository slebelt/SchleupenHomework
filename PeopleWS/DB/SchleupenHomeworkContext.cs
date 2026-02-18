using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeopleWS.DB.Models;

namespace PeopleWS.DB;

public partial class SchleupenHomeworkContext : DbContext
{
    public SchleupenHomeworkContext()
    {
    }

    public SchleupenHomeworkContext(DbContextOptions<SchleupenHomeworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;User=stefan;Password=hardToGuessPW;Database=SchleupenHomework; Connection Timeout=120");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Street).HasMaxLength(255);
            entity.Property(e => e.StreetNumber).HasMaxLength(255);
            entity.Property(e => e.Town).HasMaxLength(255);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);

            entity.HasMany(d => d.Addresses).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonAddressRelation",
                    r => r.HasOne<Address>().WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PersonAddressAddress"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PersonAddressPerson"),
                    j =>
                    {
                        j.HasKey("PersonId", "AddressId").HasName("PRIMARY");
                        j.ToTable("PersonAddressRelation");
                        j.HasIndex(new[] { "AddressId" }, "PersonAddressAddress");
                        j.IndexerProperty<int>("PersonId").HasColumnName("PersonID");
                        j.IndexerProperty<int>("AddressId").HasColumnName("AddressID");
                    });

            entity.HasMany(d => d.Phones).WithMany(p => p.People)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonPhoneRelation",
                    r => r.HasOne<Phone>().WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PersonPhonePhone"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PersonPhonePerson"),
                    j =>
                    {
                        j.HasKey("PersonId", "PhoneId").HasName("PRIMARY");
                        j.ToTable("PersonPhoneRelation");
                        j.HasIndex(new[] { "PhoneId" }, "PersonPhonePhone");
                        j.IndexerProperty<int>("PersonId").HasColumnName("PersonID");
                        j.IndexerProperty<int>("PhoneId").HasColumnName("PhoneID");
                    });
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneId).HasName("PRIMARY");

            entity.ToTable("Phone");

            entity.Property(e => e.PhoneId).HasColumnName("PhoneID");
            entity.Property(e => e.CountryPrefix).HasMaxLength(4);
            entity.Property(e => e.PhoneType).HasMaxLength(7);
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View");

            entity.Property(e => e.Addresses).HasColumnType("mediumtext");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumbers).HasColumnType("mediumtext");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
