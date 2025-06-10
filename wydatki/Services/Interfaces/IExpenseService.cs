﻿using wydatki.Models;

namespace wydatki.Services;

public interface IExpenseService
{
    public Task<List<Expense>> GetExpensesAsync();
    public  Task<Expense> GetExpenseAsync(int ExpenseId);
}