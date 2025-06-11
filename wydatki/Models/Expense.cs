﻿namespace wydatki.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; } // na co wydales piniadze
    public double Amount { get; set; } // w zlotych
    
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Expense()
    {
    }

    public Expense(string Desc,double Am, int CatId, DateTime Dat)
    {
        Description = Desc;
        Amount = Am;
        CategoryId = CatId;
        Date = Dat;
    }
}