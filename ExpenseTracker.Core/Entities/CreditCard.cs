using System;
using System.Collections.Generic;

namespace ExpenseTracker.Core.Entities;

public partial class CreditCard
{
    public int CreditCardId { get; set; }

    public int LastFourDigits { get; set; }

    public string NameOnCard { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
