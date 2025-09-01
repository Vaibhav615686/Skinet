using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Product
{
    [Key]

    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = "";

    public required decimal Price { get; set; }

    public required string ProductImage { get; set; } = "";

    public string Type { get; set; } = "";

    public string Brand { get; set; } = "";

    public int ProductInStock { get; set; }
}
