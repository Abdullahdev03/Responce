using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class ProductContoller: ControllerBase
{
    private readonly ProductService _productService;

    public ProductContoller(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("GetProducts")]
    public async Task<Response<List<ProductDto>>> GetProducts()
    {
        return await _productService.GetProducts();
    }

    [HttpPost("AddProduct")]
    public async  Task<Response<ProductDto>> AddProduct(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            return await _productService.AddProduct(product);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }
    [HttpPut("UpdateProduct")]
    public async Task<Response<ProductDto>> UpdateProduct(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            return await _productService.UpdateProduct(product);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
        }
    }


    [HttpDelete("DeleteProduct")]
    public async Task<Response<string>> Delete(int id)
    {
        await _productService.DeleteProduct(id);
        return new Response<string>("Deleted");
    }


}
