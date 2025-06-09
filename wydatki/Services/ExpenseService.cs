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
        return await _db.Expenses.ToListAsync();
    }
}