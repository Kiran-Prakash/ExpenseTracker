using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Models;
using ExpenseTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository; // Change to use IExpenseRepository

        public ExpenseService(IExpenseRepository repository) // Constructor injection
        {
            _repository = repository;
        }

        public async Task<ExpenseModel> AddExpense(Expense expense)
        {
            await _repository.AddExpense(expense); // Use repository to add
            return GetExpenseModelFromExpense(expense);
        }

        public async Task<bool> DeleteExpense(int expenseId)
        {
            return await _repository.DeleteExpense(expenseId);
        }

        public async Task<ExpenseModel> UpdateExpense(Expense expense)
        {
            await _repository.UpdateExpense(expense); // Use repository to update
            return GetExpenseModelFromExpense(expense);
        }

        public async Task<ExpenseModel> GetExpenseById(int expenseId)
        {
            var expense = await _repository.GetExpenseById(expenseId); // Use repository to find
            return GetExpenseModelFromExpense(expense);
        }

        public async Task<IEnumerable<ExpenseModel>> GetAllExpenses()
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach(var expense in await _repository.GetAllExpenses())
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels); // Use repository to get all
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpensesByUserId(int userId) // Added userId parameter
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach (var expense in await _repository.GetExpensesByUserId(userId))
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels);
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpensesByFamilyId(int familyId) // Added familyId parameter
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach (var expense in await _repository.GetExpensesByFamilyId(familyId))
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels);
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach (var expense in await _repository.GetExpensesByDateRange(startDate, endDate))
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels);
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpensesByCategoryId(int categoryID)
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach (var expense in await _repository.GetExpensesByCategoryId(categoryID))
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels);
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpensesByCreditCardId(int creditCardId)
        {
            IList<ExpenseModel> expenseModels = new List<ExpenseModel>();
            foreach (var expense in await _repository.GetExpensesByCreditCardId(creditCardId))
            {
                expenseModels.Add(GetExpenseModelFromExpense(expense));
            }
            return await Task.FromResult(expenseModels);
        }

        public async Task<IEnumerable<ExpenseCategoryModel>> GetAllExpensesCategories()
        {
            var categories = await _repository.GetAllExpensesCategories(); // Use repository for categories
            return categories.Select(GetExpenseCategoryModelFromExpenseCategory);
        }

        public async Task<IEnumerable<ExpenseTypeModel>> GetAllExpensesTypes()
        {
            var types = await _repository.GetAllExpensesTypes(); // Use repository for types
            return types.Select(GetExpenseTypeModelFromExpenseType);
        }

        public async Task<IEnumerable<CreditCardModel>> GetAllExpensesCreditCards()
        {
            var creditCards = await _repository.GetAllExpensesCreditCards(); // Use repository for credit cards
            return creditCards.Select(GetCreditCardModelFromCreditCard);
        }

        private ExpenseModel GetExpenseModelFromExpense(Expense expense)
        {
            if(expense == null)
            {
                return null;
            }
            return new ExpenseModel
            {
                ExpenseId = expense.ExpenseId,
                ExpenseAmount = expense.ExpenseAmount,
                ExpenseCategoryId = expense.ExpenseCategoryId,
                ExpenseCreditCardId = expense.CreditCardId,
                ExpenseDate = expense.ExpenseDate,
                ExpenseUserId = expense.UserId,
                ExpenseDescription = expense.ExpenseDescription
            };
        }

        private  ExpenseCategoryModel GetExpenseCategoryModelFromExpenseCategory(ExpenseCategory expenseCategory) =>
            new ExpenseCategoryModel
            {
                ExpenseCategoryId = expenseCategory.ExpenseCategoryId,
                ExpenseCategoryName = expenseCategory.ExpenseCategoryName
            };

        private ExpenseTypeModel GetExpenseTypeModelFromExpenseType(ExpenseType expenseType) =>
            new ExpenseTypeModel
            {
                ExpenseTypeId = expenseType.ExpenseTypeId,
                ExpenseTypeName = expenseType.ExpenseTypeName
            };

        private CreditCardModel GetCreditCardModelFromCreditCard(CreditCard creditCard) =>
            new CreditCardModel
            {
                CreditCardId = creditCard.CreditCardId,
                CreditCardNameOnCard = creditCard.NameOnCard,
                CreditCardNumberLast4Digits = creditCard.LastFourDigits
            };
    }
}
