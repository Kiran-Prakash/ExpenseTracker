using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Models
{
    public class UserModel
    {
        public string UserName { get; set; }

        public int FamilyId { get; set; }

        public int? AdobjId { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string DisplayName { get; set; }

        public string EmailId { get; set; }
    }
}
