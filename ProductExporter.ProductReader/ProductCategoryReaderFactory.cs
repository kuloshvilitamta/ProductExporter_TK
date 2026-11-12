using Microsoft.Data.SqlClient;
using ProductExporter.Shared.Enumeration;
using ProductExporter.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductExporter.ProductReader;

public static class ProductCategoryReaderFactory
{
    public static IProductReader Create(DataBaseTypes dataBaseType, SqlConnection connection)
    {
        return dataBaseType switch
        {
            DataBaseTypes.SqlServer => new ProductCategoryReader(connection),
            _ => throw new ArgumentException($"Unsupported input type: {dataBaseType}", nameof(dataBaseType)),
        };
    }

    public static IProductReader Create(DataBaseTypes dataBaseType, string connectionString)
    {
        return dataBaseType switch
        {
            DataBaseTypes.SqlServer => new ProductCategoryReader(connectionString),
            _ => throw new ArgumentException($"Unsupported input type: {dataBaseType}", nameof(dataBaseType)),
        };
    }
}