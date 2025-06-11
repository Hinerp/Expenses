using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using wydatki.Models;
using wydatki.Services;

namespace wydatki.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IExpenseService _expenseService;

    public HomeController(ILogger<HomeController> logger, IExpenseService expenseService)
    {
        _logger = logger;
        _expenseService = expenseService;
    }

    public async Task<IActionResult> Index(string filter="")
    {
        var Expenses = (filter!="")?await _expenseService.GetExpensesByNameAsync(filter):await _expenseService.GetExpensesAsync();
        return View(Expenses);
    }

    public async Task<IActionResult> Details(int ExpenseId)
    {
        var expense = await _expenseService.GetExpenseAsync(ExpenseId);
        return View(expense);
    }

    public async Task<IActionResult> Delete(int ExpenseId)
    {
        _expenseService.DeleteExpenseAsync(ExpenseId);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(int ExpenseId)
    {
        return View();
    }

    public async Task<IActionResult> AddExpense()
    {
        return View(await _expenseService.GetCategoriesAsync());
    }
    
    public async Task<IActionResult> AddCategory()
    {
        return View();
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}