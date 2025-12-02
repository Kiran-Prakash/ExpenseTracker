using ExpenseTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public interface IExpenseRepository
    {
        Task<Expense> AddExpense(Expense expense);
        Task<bool> DeleteExpense(int expenseId);
        Task<Expense> UpdateExpense(Expense expense);
        Task<Expense> GetExpenseById(int expenseId);
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<IEnumerable<Expense>> GetExpensesByUserId(int userId);
        Task<IEnumerable<Expense>> GetExpensesByFamilyId(int familyId);
        Task<IEnumerable<Expense>> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Expense>> GetExpensesByCategoryId(int categoryID);
        Task<IEnumerable<Expense>> GetExpensesByCreditCardId(int creditCardId);
        
        Task<IEnumerable<ExpenseCategory>> GetAllExpensesCategories();
        Task<IEnumerable<ExpenseType>> GetAllExpensesTypes();
        Task<IEnumerable<CreditCard>> GetAllExpensesCreditCards();

    }
}
