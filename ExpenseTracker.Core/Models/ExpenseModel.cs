using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Models
{
    public class ExpenseModel
    {
        public int ExpenseId { get; set; }
        public int ExpenseUserId { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public int ExpenseCategoryId { get; set; }
        public double ExpenseAmount { get; set; }
        public int ExpenseCreditCardId { get; set; }

        public string ExpenseDescription { get; set; }
    }

    public class ExpenseCategoryModel
    {
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
    }

    public class ExpenseTypeModel
    {
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
    }

    public class CreditCardModel
    {
        public int CreditCardId { get; set; }
        public string CreditCardNameOnCard { get; set; }
        public int CreditCardNumberLast4Digits { get; set; }
    }
}
