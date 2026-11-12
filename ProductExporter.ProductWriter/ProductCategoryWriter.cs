using ProductExporter.Shared.Interfaces;
using ProductExporter.Shared.Models;

namespace ProductExporter.ProductWriter;

internal sealed class ProductCategoryWriter : IProductWriter, IDisposable
{

    private readonly Stream _outputStream;
    private bool _disposed;
    private readonly bool _diposeOutputStream;

    public ProductCategoryWriter(Stream outputStream)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(outputStream));

        _outputStream = outputStream;

        if (!outputStream.CanWrite)
            throw new AccessViolationException(nameof(outputStream));

        _diposeOutputStream = false;
    }

    public ProductCategoryWriter(FileInfo outputFile)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(outputFile));

        _outputStream = outputFile.OpenWrite();

        _diposeOutputStream = true;
    }

    public bool WriteToTextFile(IEnumerable<Category> categories)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        using var writer = new StreamWriter(_outputStream);
        foreach (var category in categories)
        {
            foreach (var products in category.Products)
            {
                writer.Write(category.ToString(), products.ToString());
            }
        }

        return true;
    }

    public void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            if (_diposeOutputStream)
            {
                _outputStream.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~ProductCategoryWriter()
    {
        Dispose(false);
    }
}
