using System;
using API.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public class ProductRepository(AppDbContext Context) : IProductRespository
{
    public async Task<ServerResponse<IEnumerable<Product>>> GetProducts(string? brand,string? type)
    {
        ServerResponse<IEnumerable<Product>> response = new ServerResponse<IEnumerable<Product>>();
        var query = Context.products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
        {
          query =  query.Where(x => x.Brand == brand);
        }
        if (!string.IsNullOrWhiteSpace(type))
        {
          query =  query.Where(x => x.Type == type);
        }
        var data = await query.ToListAsync();
        if (data == null)
        {
            response.IsSuccess = true;
            response.Data = null;
            response.Message = "No products available";
        }
        else
        {
            response.IsSuccess = true;
            response.Data = data;
            response.Message = "Products Found Successfully";
        }
        return response;
    }
    public async Task<ServerResponse<Product>> GetProductById(int Id)
    {
        ServerResponse<Product> response = new ServerResponse<Product>();
        var data = await Context.products.FindAsync(Id);
        if (data == null)
        {
            response.IsSuccess = true;
            response.Data = null;
            response.Message = "No products available";
        }
        else
        {
            response.IsSuccess = true;
            response.Data = data;
            response.Message = "Product Found Successfully";
        }
        return response;
    }
    public async Task<ServerResponse<Product>> AddProduct(Product product)
    {
        ServerResponse<Product> response = new ServerResponse<Product>();
        Context.products.Add(product);
        if (await Context.SaveChangesAsync() > 0)
        {
            response.IsSuccess = true;
            response.Data = product;
            response.Message = "Product added successfully";
        }
        else
        {
            response.IsSuccess = false;
            response.Data = product;
            response.Message = "Something went wrong";
        }
        return response;
    }
    public async Task<ServerResponse<Product>> UpdateProduct(int Id, Product product)
    {
        ServerResponse<Product> response = new ServerResponse<Product>();
        var data = await Context.products.FindAsync(Id);
        if (data != null)
        {
            // data.Name = product.Name;
            // data.Price = product.Price;
            // data.Description = product.Description;
            // data.Brand = product.Brand;
            // data.ProductImage = product.ProductImage;
            // data.Type = product.Type;
            // data.ProductInStock = product.ProductInStock;
            Context.ChangeTracker.Clear();
            Context.Entry(product).State = EntityState.Modified;
            if (await Context.SaveChangesAsync() > 0)
            {
                response.IsSuccess = true;
                response.Data = null;
                response.Message = "Product Updated Successfully";
            }
            else
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Something went wrong";
            }
        }
        else
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = "Product Not Found";
        }
        return response;
    }
    public async Task<ServerResponse<Product>> DeleteProduct(int Id)
    {
        ServerResponse<Product> response = new ServerResponse<Product>();
        var data = await Context.products.FindAsync(Id);
        if (data != null)
        {
            Context.products.Remove(data);
            if (await Context.SaveChangesAsync() > 0)
            {
                response.IsSuccess = true;
                response.Data = null;
                response.Message = "Product Deleted Successfully";
            }
            else
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = "Something went wrong";
            }
        }
        else
        {
            response.IsSuccess = false;
            response.Data = null;
            response.Message = "Product Not Found";
        }
        return response;
    }

    public async Task<ServerResponse<IReadOnlyList<string>>> GetTypes()
    {
        ServerResponse<IReadOnlyList<string>> response = new ServerResponse<IReadOnlyList<string>>();
        var types = await Context.products.Select(x => x.Type).Distinct().ToListAsync();
        if (types == null)
        {
            response.IsSuccess = true;
            response.Message = "No types specified";
        }
        else
        {
            response.Data = types;
            response.IsSuccess = true;
            response.Message = "Types fetched successfully";
        }
        return response;
    }

    public async Task<ServerResponse<IReadOnlyList<string>>> GetBrands()
    {
        ServerResponse<IReadOnlyList<string>> response = new ServerResponse<IReadOnlyList<string>>();
        var brands = await Context.products.Select(x => x.Brand).Distinct().ToListAsync();
        if (brands == null)
        {
            response.Data = null;
            response.IsSuccess = true;
            response.Message = "No brands specified";
        }
        else
        {
            response.Data = brands;
            response.IsSuccess = true;
            response.Message = "Brands fetched successfully";
        }
        return response;
    }
}
