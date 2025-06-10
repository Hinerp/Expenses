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
}