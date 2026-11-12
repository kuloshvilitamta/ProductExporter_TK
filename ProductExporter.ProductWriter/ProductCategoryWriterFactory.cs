using ProductExporter.Shared.Enumeration;
using ProductExporter.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductExporter.ProductWriter;

public static class ProductCategoryWriterFactory
{
    public static IProductWriter Create(FileType fileType, Stream inputStream)
    {
        return fileType switch
        {
            FileType.TextFile => new ProductCategoryWriter(inputStream),
            _ => throw new ArgumentException($"Unsupported input type: {fileType}", nameof(fileType)),
        };
    }

    public static IProductWriter Create(FileType fileType, FileInfo inputFile)
    {
        return fileType switch
        {
            FileType.TextFile => new ProductCategoryWriter(inputFile),
            _ => throw new ArgumentException($"Unsupported input type: {fileType}", nameof(fileType)),
        };
    }
}
