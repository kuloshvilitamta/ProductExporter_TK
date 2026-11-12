using DataBaseConnector;
using ProductExporter.ProductReader;
using ProductExporter.ProductWriter;
using ProductExporter.Shared.Enumeration;

namespace ProductExporter_TK;

internal class Program
{
    static void Main()
    {
        var reader = ProductCategoryReaderFactory.Create(DataBaseTypes.SqlServer, DBConnector.GetSqlConnection());
        var categories = reader.GetData().ToList();
        var file=new FileInfo("D:\testing.txt");
        var writer = ProductCategoryWriterFactory.Create(FileType.TextFile, file);

        writer.WriteToTextFile(categories);
        Console.WriteLine($"Categories: {categories.Count}, Products: {categories.Sum(c => c.Products.Count)}");
    }
}
