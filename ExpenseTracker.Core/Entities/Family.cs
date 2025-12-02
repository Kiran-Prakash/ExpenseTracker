using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.Entities;

[Table("Family")]
public partial class Family
{
    public int FamilyId { get; set; }

    public string FamilyName { get; set; } = null!;
}
