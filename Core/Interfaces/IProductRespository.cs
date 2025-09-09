using System;
using API.Models;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRespository
{
    Task<ServerResponse<IEnumerable<Product>>> GetProducts(string? brand,string? type);
    Task<ServerResponse<Product>> GetProductById(int Id);

    Task<ServerResponse<Product>> AddProduct(Product product);

    Task<ServerResponse<Product>> UpdateProduct(int Id, Product product);

    Task<ServerResponse<Product>> DeleteProduct(int Id);

    Task<ServerResponse<IReadOnlyList<string>>> GetBrands();

    Task<ServerResponse<IReadOnlyList<string>>> GetTypes();

}
