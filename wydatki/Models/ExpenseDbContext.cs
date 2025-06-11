using Microsoft.EntityFrameworkCore;

namespace wydatki.Models;

public class ExpenseDbContext : DbContext
{
    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options): base(options) {}
    
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Expense>().Property(e => e.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>().HasData(new Category("Jedzenie") {Id = 1}, new Category("Mieszkanie") {Id =2});
        modelBuilder.Entity<Expense>().HasData(new Expense("Czynsz",2311.52,2,new DateTime(2025,06,11)) {Id = 1});
    }
}