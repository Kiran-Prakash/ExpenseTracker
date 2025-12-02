using ExpenseTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public ExpenseRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> AddExpense(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> DeleteExpense(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if(expense == null)
            {
                return false;
            }
            _context.Expenses.Remove(expense);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            if(expense == null)
            {
                throw new ArgumentNullException("expense cannot be null.");
            }
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> GetExpenseById(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await Task.FromResult(_context.Expenses);
        }

        public async Task<IEnumerable<Expense>> GetExpensesByUserId(int userId)
        {
            var userExpenses = _context.Expenses.Where(e => e.UserId == userId);
            return userExpenses;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByFamilyId(int familyId)
        {
            var familyExpenses = _context.Expenses.Where(e => 
                _context.Users.Any(user => user.UserId == e.UserId && user.FamilyId == familyId));
            return familyExpenses;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var dateRangeExpenses = _context.Expenses.Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate);
            return dateRangeExpenses;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByCategoryId(int categoryId)
        {
            var categoryExpenses = _context.Expenses.Where(e => e.ExpenseCategoryId == categoryId);
            return categoryExpenses;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByCreditCardId(int creditCardId)
        {
            var creditCardExpenses = _context.Expenses.Where(e => e.CreditCardId == creditCardId);
            return creditCardExpenses;
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpensesCategories()
        {
            return await Task.FromResult(_context.ExpenseCategories);
        }

        public async Task<IEnumerable<ExpenseType>> GetAllExpensesTypes()
        {
            return await Task.FromResult(_context.ExpenseTypes);
        }

        public async Task<IEnumerable<CreditCard>> GetAllExpensesCreditCards()
        {
            return await Task.FromResult(_context.CreditCards);
        }
    }
}
