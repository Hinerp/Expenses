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

    

    public async Task<Expense> GetExpenseAsync(int ExpenseId)
    {
        return await _db.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == ExpenseId);
    }

    public async Task<List<Expense>> GetExpensesAsync(string expenseName, int? categoryId ,string sorter)
    {
        IQueryable<Expense> query = _db.Expenses.Include(e => e.Category);

        if (!string.IsNullOrEmpty(expenseName))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"%{expenseName}%"));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(e => e.CategoryId == categoryId.Value);
        }

        switch (sorter)
        {
            case "Name":
                query = query.OrderBy(e => e.Name);
                break;
            case "Lowest Cost":
                query = query.OrderBy(e => e.Amount);
                break;
            case "Highest Cost":
                query = query.OrderByDescending(e => e.Amount);
                break;
            case "Newest":
                query = query.OrderByDescending(e => e.Date);
                break;
            case "Oldest":
                query = query.OrderBy(e => e.Date);
                break;
            default:
                query = query.OrderBy(e => e.Name);
                break;
        }

        return await query.ToListAsync();
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
        if (string.IsNullOrEmpty(category.Name)) return;
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }

    public async Task AddExpenseAsync(Expense expense)
    {   if(expense.Date==new DateTime(0001,01,01)) expense.Date = DateTime.Today;
        _db.Expenses.Add(expense);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }
    
}