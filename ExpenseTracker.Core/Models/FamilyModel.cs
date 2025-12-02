using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Models
{
    public class FamilyModel
    {
        public string FamilyName { get; set; }
        public List<UserModel> FamilyMembers { get; set; }
    }
}
