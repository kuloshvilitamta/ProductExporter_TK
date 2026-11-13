namespace ProductExporter.Shared.Models;

public sealed class Product
{
    public string Name { get; }
    public string Code {  get; }
    public decimal Price {  get; }
    public int Quantity {  get; }
    public bool IsDeleted { get; }
    public string CategoryName { get; }

    public Product(string name, string code, decimal price, int quantity, bool isDeleted, string categoryName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryName, nameof(categoryName));

        Name = name;
        Code = code;
        Price = price;
        Quantity = quantity;
        IsDeleted = isDeleted;
        CategoryName = categoryName;
    }

    public override string ToString()
    {
        return $"{CategoryName}\t{Code}\t{Name}\t{Price:F2}\t{Quantity}\t{Convert.ToByte(IsDeleted)}";
    }
}
