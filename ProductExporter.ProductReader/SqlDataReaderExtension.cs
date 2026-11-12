using Microsoft.Data.SqlClient;
using ProductExporter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductExporter.ProductReader;

public static class SqlDataReaderExtension
{
    public static Product ProductBuilder(this SqlDataReader reader)
    {
          return new Product(
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetDecimal(reader.GetOrdinal("Price")),
                            reader.GetInt32(reader.GetOrdinal("Quantity")),
                            !reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                            reader.GetString(reader.GetOrdinal("CategoryName")));
    }
}
