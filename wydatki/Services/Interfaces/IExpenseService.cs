using wydatki.Models;

namespace wydatki.Services;

public interface IExpenseService
{
    public Task<List<Expense>> GetExpensesAsync();
    public  Task<Expense> GetExpenseAsync(int ExpenseId);
    public void DeleteExpenseAsync(int ExpenseId);
    public void EditExpenseAsync(Expense Expense, int ExpenseId);
    public void AddCategoryAsync(string Name);
    public void AddExpenseAsync(Expense Expense);
    public Task<List<Category>> GetCategoriesAsync();

    public Task<List<Expense>> GetExpensesByNameAsync(string expenseName);

}