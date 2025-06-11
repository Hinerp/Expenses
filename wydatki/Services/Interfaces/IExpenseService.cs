using wydatki.Models;

namespace wydatki.Services;

public interface IExpenseService
{
    public Task<List<Expense>> GetExpensesAsync();
    public  Task<Expense> GetExpenseAsync(int ExpenseId);
    public Task DeleteExpenseAsync(int ExpenseId);
    public Task EditExpenseAsync(Expense Expense);
    public Task AddCategoryAsync(string Name);
    public Task AddExpenseAsync(Expense Expense);
    public Task<List<Category>> GetCategoriesAsync();

    public Task<List<Expense>> GetExpensesByNameAsync(string expenseName);

}