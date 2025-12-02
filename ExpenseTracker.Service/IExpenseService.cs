using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public interface IExpenseService
    {
        Task<ExpenseModel> AddExpense(Expense expenseModel);
        Task<bool> DeleteExpense(int expenseId);
        Task<ExpenseModel> UpdateExpense(Expense expense);
        Task<ExpenseModel> GetExpenseById(int expenseId);
        Task<IEnumerable<ExpenseModel>> GetAllExpenses();
        Task<IEnumerable<ExpenseModel>> GetExpensesByUserId(int userId);
        Task<IEnumerable<ExpenseModel>> GetExpensesByFamilyId(int familyId);
        Task<IEnumerable<ExpenseModel>> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<ExpenseModel>> GetExpensesByCategoryId(int categoryID);
        Task<IEnumerable<ExpenseModel>> GetExpensesByCreditCardId(int creditCardId);
        Task<IEnumerable<ExpenseCategoryModel>> GetAllExpensesCategories();
        Task<IEnumerable<ExpenseTypeModel>> GetAllExpensesTypes();
        Task<IEnumerable<CreditCardModel>> GetAllExpensesCreditCards();

    }
}
