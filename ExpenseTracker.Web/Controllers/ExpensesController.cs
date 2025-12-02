using ExpenseTracker.Core.Entities;
using ExpenseTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
        }

        [HttpGet("{expenseId:int}")]
        public async Task<IActionResult> GetExpenseById(int expenseId)
        {
            if (expenseId < 0)
            {
                return BadRequest("Expense ID must be greater than or equal to zero.");
            }

            var expense = await _expenseService.GetExpenseById(expenseId);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense cannot be null.");
            }

            var createdExpense = await _expenseService.AddExpense(expense);
            return CreatedAtAction(nameof(AddExpense), new { expenseId = createdExpense.ExpenseId }, createdExpense);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense cannot be null.");
            }

            var updatedExpense = await _expenseService.UpdateExpense(expense);
            if (updatedExpense == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{expenseId:int}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            await _expenseService.DeleteExpense(expenseId);
            return NoContent();
        }

        [HttpGet("userid/{userId:int}")]
        public async Task<IActionResult> GetExpensesByUserId(int userId)
        {
            var expenses = await _expenseService.GetExpensesByUserId(userId);
            return Ok(expenses);
        }

        [HttpGet("familyid/{familyId:int}")]
        public async Task<IActionResult> GetExpensesByFamilyId(int familyId)
        {
            var expenses = await _expenseService.GetExpensesByFamilyId(familyId);
            return Ok(expenses);
        }

        [HttpGet("categoryid/{categoryId:int}")]
        public async Task<IActionResult> GetExpensesByCategoryId(int categoryId)
        {
            var expenses = await _expenseService.GetExpensesByCategoryId(categoryId);
            return Ok(expenses);
        }

        [HttpGet("creditcardid/{creditCardId:int}")]
        public async Task<IActionResult> GetExpensesByCreditCardId(int creditCardId)
        {
            var expenses = await _expenseService.GetExpensesByCreditCardId(creditCardId);
            return Ok(expenses);
        }
    }
}
