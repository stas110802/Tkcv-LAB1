using System.Collections.Generic;

namespace Tkcv_LAB1.MVVM.Models;

public sealed class Product
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string VendorCode { get; set; }
    public decimal Price { get; set; }
    public List<string> Materials { get; set; }
    public string PathImg { get; set; }
}