using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.Entities;

[Table("User")]
public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int FamilyId { get; set; }

    public int? AdobjId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string DisplayName { get; set; } = null!;

    public string EmailId { get; set; } = null!;
}
