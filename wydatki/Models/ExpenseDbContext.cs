using Microsoft.EntityFrameworkCore;

namespace wydatki.Models;

public class ExpenseDbContext : DbContext
{
    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options): base(options) {}
    
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
}