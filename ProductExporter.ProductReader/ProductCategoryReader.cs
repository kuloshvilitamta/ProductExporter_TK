using Microsoft.Data.SqlClient;
using ProductExporter.Shared.Interfaces;
using ProductExporter.Shared.Models;
using System.Data;

namespace ProductExporter.ProductReader
{
    internal sealed class ProductCategoryReader : IProductReader, IDisposable
    {
        private bool _disposed;
        private readonly SqlConnection _inputConnection;
        private readonly bool _closeSqlConnection;

        public ProductCategoryReader(SqlConnection inputConnection)
        {
            ArgumentNullException.ThrowIfNull(inputConnection, nameof(inputConnection));

            _inputConnection = inputConnection;

            _closeSqlConnection = false;
        }

        public ProductCategoryReader(string inputConnectionString)
        {
            ArgumentNullException.ThrowIfNull(inputConnectionString, nameof(inputConnectionString));

            _inputConnection = new SqlConnection(inputConnectionString);

            _closeSqlConnection = true;
        }

        public IEnumerable<Category> GetData()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_inputConnection.State != ConnectionState.Open)
                _inputConnection.Open();
            try
            {
                var categories = new Dictionary<string, Category>();
                using var command = new SqlCommand("Select * From ProductView", _inputConnection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var product = reader.ProductBuilder();

                    if (!categories.TryGetValue(product.CategoryName, out var category))
                    {
                        category = new Category(product.CategoryName, !reader.GetBoolean("CategoryIsDeleted"));
                        categories.Add(product.CategoryName, category);
                    }

                    category.AddProducts(product);
                }

                return categories.Values;

            }
            finally
            {
                if (_closeSqlConnection)
                    _inputConnection.Close();
            }
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_closeSqlConnection)
                {
                    _inputConnection.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~ProductCategoryReader()
        {
            Dispose(false);
        }
    }
}
