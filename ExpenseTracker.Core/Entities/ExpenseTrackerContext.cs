using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core.Entities;

public partial class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext()
    {
    }

    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExpenseTracker;Trusted_Connection=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.ToTable("CreditCard");

            entity.Property(e => e.CreditCardId)
                .ValueGeneratedNever()
                .HasColumnName("CreditCardID");
            entity.Property(e => e.NameOnCard).HasMaxLength(100);
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expense");

            entity.Property(e => e.ExpenseId)
                .ValueGeneratedNever()
                .HasColumnName("ExpenseID");
            entity.Property(e => e.CreditCardId).HasColumnName("CreditCardID");
            entity.Property(e => e.ExpenseCategoryId).HasColumnName("ExpenseCategoryID");
            entity.Property(e => e.ExpenseTypeId).HasColumnName("ExpenseTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.ToTable("ExpenseCategory");

            entity.Property(e => e.ExpenseCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("ExpenseCategoryID");
            entity.Property(e => e.ExpenseCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ExpenseType>(entity =>
        {
            entity.ToTable("ExpenseType");

            entity.Property(e => e.ExpenseTypeId)
                .ValueGeneratedNever()
                .HasColumnName("ExpenseTypeID");
            entity.Property(e => e.ExpenseTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.ToTable("Family");

            entity.Property(e => e.FamilyId)
                .ValueGeneratedNever()
                .HasColumnName("FamilyID");
            entity.Property(e => e.FamilyName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.EmailId, "UK_User").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.AdobjId).HasColumnName("ADObjID");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasDefaultValue("Guest");
            entity.Property(e => e.EmailId).HasMaxLength(50);
            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
