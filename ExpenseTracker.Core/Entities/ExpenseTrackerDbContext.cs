using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core.Entities;

public partial class ExpenseTrackerDbContext : DbContext
{
    public ExpenseTrackerDbContext()
    {
    }

    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.Property(e => e.CreditCardId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.ExpenseId).ValueGeneratedNever();

        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.Property(e => e.ExpenseCategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ExpenseType>(entity =>
        {
            entity.Property(e => e.ExpenseTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.Property(e => e.FamilyId).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.DisplayName).HasDefaultValue("Guest");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
