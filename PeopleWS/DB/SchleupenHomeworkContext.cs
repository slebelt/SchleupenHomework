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

    public virtual DbSet<PersonAddressRelation> PersonAddressRelations { get; set; }

    public virtual DbSet<PersonPhoneRelation> PersonPhoneRelations { get; set; }

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
        });

        modelBuilder.Entity<PersonAddressRelation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PersonAddressRelation");

            entity.HasIndex(e => e.AddressId, "PersonAddressAddress");

            entity.HasIndex(e => new { e.PersonId, e.AddressId }, "PersonAddressUnique").IsUnique();

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Address).WithMany()
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonAddressAddress");

            entity.HasOne(d => d.Person).WithMany()
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonAddressPerson");
        });

        modelBuilder.Entity<PersonPhoneRelation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PersonPhoneRelation");

            entity.HasIndex(e => e.PhoneId, "PersonPhonePhone");

            entity.HasIndex(e => new { e.PersonId, e.PhoneId }, "PersonPhoneUnique").IsUnique();

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.PhoneId).HasColumnName("PhoneID");

            entity.HasOne(d => d.Person).WithMany()
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonPhonePerson");

            entity.HasOne(d => d.Phone).WithMany()
                .HasForeignKey(d => d.PhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonPhonePhone");
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
