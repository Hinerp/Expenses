using Microsoft.EntityFrameworkCore;
using wydatki.Models;

namespace wydatki.Services;

public class ExpenseService : IExpenseService
{
    private readonly ExpenseDbContext _db;

    public ExpenseService(ExpenseDbContext db)
    {
        _db = db;
    } 

    public async Task<List<Expense>> GetExpensesAsync()
    {
        return await _db.Expenses
                // to jest zeby ustawialo kategorie w obiekcie bo jakos tego nie robi
            .Include(e => e.Category)
            .ToListAsync();
    }

    public async Task<Expense> GetExpenseAsync(int ExpenseId)
    {
        return await _db.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == ExpenseId);
    }

    public async Task<List<Expense>> GetExpensesByNameAsync(string expenseName)
    {
        return await _db.Expenses.Where(e=> e.Name == expenseName)
        // to jest zeby ustawialo kategorie w obiekcie bo jakos tego nie robi
        .Include(e => e.Category)
        .ToListAsync();
    }

    public async Task DeleteExpenseAsync(int expenseId)
    {
        var expenseToDelete = await _db.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);

        if (expenseToDelete == null)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not found.");
        }

        _db.Expenses.Remove(expenseToDelete);
        await _db.SaveChangesAsync();
    }


    public async Task EditExpenseAsync(Expense exp)
    {
        var expenseToUpdate = await _db.Expenses.FirstOrDefaultAsync(e => e.Id == exp.Id);

        if (expenseToUpdate == null)
        {
            throw new ArgumentException($"Expense with ID {exp.Id} not found.");
        }

        expenseToUpdate.Name = exp.Name;
        expenseToUpdate.Amount = exp.Amount;
        expenseToUpdate.Description = exp.Description;
        expenseToUpdate.CategoryId = exp.CategoryId;
        expenseToUpdate.Date = exp.Date;

         await _db.SaveChangesAsync();
    }

    public async Task AddCategoryAsync(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        _db.Expenses.Add(expense);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }
}