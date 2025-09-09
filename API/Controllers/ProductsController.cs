using API.Models;
using API.RequiredHelper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGeneralRepository<Product> repo) : ControllerBase
    {
        [HttpGet]
        // public async Task<ServerResponse<IEnumerable<Product>>> GetAllProducts([FromQuery] string? brand, [FromQuery] string? type)
        // {
        //     // return await productRespository.GetProducts(brand,type);
        //     return await repo.ListAllAsync();
        // }

        [HttpGet]

        public async Task<ServerResponse<Pagination<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var serverResponse = new ServerResponse<Pagination<Product>>();

            var spec = new ProductSpecifications(specParams);
            var count = await repo.CountAsync(spec);
            var products = await repo.ListAsync(spec); // should return IEnumerable<Product>

            var paginationResult = new Pagination<Product>(
                specParams.PageIndex,
                specParams.PageSize,
                count,
                products
            );

            if (paginationResult.Items.Any())
            {
                serverResponse.Data = paginationResult;
                serverResponse.IsSuccess = true;
                serverResponse.Message = "Products fetched successfully";
            }
            else
            {
                serverResponse.Data = paginationResult;
                serverResponse.IsSuccess = true;
                serverResponse.Message = "No products found";
            }

            return serverResponse;
        }


        [HttpGet("{Id:int}")]
        public async Task<ServerResponse<Product?>> GetProductById(int Id)
        {
            // return await productRespository.GetProductById(Id);
            return await repo.GetByIdAsync(Id);
        }
        [HttpPost]
        public async Task<ServerResponse<Product>> AddProduct(Product Product)
        {
            // return await productRespository.AddProduct(Product);
            return await repo.Add(Product);
        }

        [HttpPut("{Id:int}")]
        public async Task<ServerResponse<Product>> UpdateProduct(int Id, [FromBody] Product Product)
        {
            // return await productRespository.UpdateProduct(Id, Product);
            return await repo.Update(Id, Product);
        }

        [HttpDelete("{Id:int}")]
        public async Task<ServerResponse<Product>> DeleteProduct(int Id)
        {
            // return await productRespository.DeleteProduct(Id);
            ServerResponse<Product> response = new ServerResponse<Product>();
            ServerResponse<Product?> data = await repo.GetByIdAsync(Id);
            if (data.Data != null)
            {
                return await repo.Delete(data.Data);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Record Not Found";
                return response;
            }
        }
        [HttpGet("brands")]
        public async Task<ServerResponse<IEnumerable<string>>> GetBrands()
        {
            // return await productRespository.GetBrands();
            ServerResponse<IEnumerable<string>> ServerResponse = new ServerResponse<IEnumerable<string>>();

            var spec = new BrandSpecification();
            var data = await repo.ListAsync(spec);
            ServerResponse.IsSuccess = true;
            ServerResponse.Data = data;
            return ServerResponse;
        }
        [HttpGet("types")]
        public async Task<ServerResponse<IEnumerable<string>>> GetTypes()
        {
            // return await productRespository.GetTypes();
            ServerResponse<IEnumerable<string>> ServerResponse = new ServerResponse<IEnumerable<string>>();

            var spec = new TypeSpecification();
            var data = await repo.ListAsync(spec);
            ServerResponse.IsSuccess = true;
            ServerResponse.Data = data;
            return ServerResponse;
        }
    }
}
