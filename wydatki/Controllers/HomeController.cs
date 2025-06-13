using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> Index(string sorter ="Name", string filter = "")
    {
        // Fetch categories for the dropdown
        var categories = await _expenseService.GetCategoriesAsync();
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        }).ToList();

   
        // Fetch expenses based on filter and sorter
        var expenses = await _expenseService.GetExpensesByNameAsync(filter, sorter);
        return View(expenses);
    }

    public async Task<IActionResult> Details(int ExpenseId)
    {
        var expense = await _expenseService.GetExpenseAsync(ExpenseId);
        return View(expense);
    }

    public async Task<IActionResult> Delete(int ExpenseId)
    {
        await _expenseService.DeleteExpenseAsync(ExpenseId);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(int ExpenseId)
    {
        var expense = await _expenseService.GetExpenseAsync(ExpenseId);
        var categories = await _expenseService.GetCategoriesAsync();
        
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,  // Displayed text
            Value = c.Id.ToString() // Value of the option
        }).ToList();

        return View(expense);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Expense expense)
    {
        await _expenseService.EditExpenseAsync(expense);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> AddExpense()
    {
        var categories = await _expenseService.GetCategoriesAsync();

        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,  // Displayed text
            Value = c.Id.ToString() // Value of the option
        }).ToList();
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddExpense(Expense expense)
    {
        _expenseService.AddExpenseAsync(expense);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> AddCategory()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category,bool prevSite)
    {
        await _expenseService.AddCategoryAsync(category);
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