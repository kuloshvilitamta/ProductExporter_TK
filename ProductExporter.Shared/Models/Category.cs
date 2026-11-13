namespace ProductExporter.Shared.Models;

public sealed class Category
{
    public ICollection<Product> Products { get; } = new List<Product>();
    public string Name { get; }
    public bool IsDeleted { get; }

    public Category(string name, bool isDeleted)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
        IsDeleted = isDeleted;
    }

    public bool AddProducts(Product product)
    {
        if (product.CategoryName != Name)
                return false;

        Products.Add(product);
        
        return true;
    }

    public override string ToString()
    {
        return $"{Name}\t{Convert.ToByte(IsDeleted)}";
    }
}
