namespace wydatki.Models;

public class Category
{
    public int  Id { get; set; }
    public string Name { get; set; }

    public Category()
    {
    }

    public Category(string Name)
    {
        this.Name = Name;
    }
}