using ProductExporter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductExporter.Shared.Interfaces;

public interface IProductWriter
{
    void WriteToTextFile(IEnumerable<Category> categories);
}
