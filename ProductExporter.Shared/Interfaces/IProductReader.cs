
using ProductExporter.Shared.Models;

namespace ProductExporter.Shared.Interfaces;

public interface IProductReader
{
    public IEnumerable<Category> GetData();
}
