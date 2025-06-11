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

    public async void DeleteExpenseAsync(int ExpenseId)
    {
        _db.Expenses.Remove(await _db.Expenses.FindAsync(ExpenseId));
        _db.SaveChangesAsync();
    }


    public async void EditExpenseAsync(Expense Expense, int ExpenseId)
    {
        Expense expenseToUpdate = await _db.Expenses.FindAsync(ExpenseId);
        
        expenseToUpdate.Amount = Expense.Amount;
        expenseToUpdate.Description = Expense.Description;
        expenseToUpdate.CategoryId = Expense.CategoryId;
        expenseToUpdate.Date = Expense.Date;
    }

    public async void AddCategoryAsync(string Name)
    {
        _db.Categories.Add(new Category(Name));
        _db.SaveChangesAsync();
    }

    public async void AddExpenseAsync(Expense Expense)
    {
        _db.Expenses.Add(Expense);
        _db.SaveChangesAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }
}