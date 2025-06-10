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

    public async Task<IActionResult> Index()
    {
        var Expenses = await _expenseService.GetExpensesAsync();
        return View(Expenses);
    }

    public async Task<IActionResult> Details(int ExpenseId)
    {
        var expense = await _expenseService.GetExpenseAsync(ExpenseId);
        return View(expense);
    }

    public async Task<IActionResult> Delete(int ExpenseId)
    {
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(int ExpenseId)
    {
        return RedirectToAction("Index");
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