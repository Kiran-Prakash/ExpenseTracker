using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.Entities;

[Table("Expense")]
public partial class Expense
{
    public int UserId { get; set; }

    public double ExpenseAmount { get; set; }

    public int ExpenseTypeId { get; set; }

    public int ExpenseCategoryId { get; set; }

    public string ExpenseDescription { get; set; }

    public int ExpenseId { get; set; }

    public int CreditCardId { get; set; }

    public DateOnly ExpenseDate { get; set; }
}
