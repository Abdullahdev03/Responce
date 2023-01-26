using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class SuplierController: ControllerBase
{
    private readonly SuplierService _suplierService;

    public SuplierController(SuplierService suplierService)
    {
        _suplierService = suplierService;
    }


    [HttpGet("GetSuppliers")]
    public async Task<Response<List<SupplierDto>>> GetSuppliers()
    {
        return await _suplierService.GetSuppliers();
    }

    [HttpPost("AddSupplier")]
    public async  Task<Response<SupplierDto>> AddSupplier(SupplierDto supplier)
    {
        if (ModelState.IsValid)
        {
            return await _suplierService.AddSupplier(supplier);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<SupplierDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("UpdateSupplier")]
    public async Task<Response<SupplierDto>> UpdateSupplier(SupplierDto supplier)
    {
        if (ModelState.IsValid)
        {
            return await _suplierService.UpdateSupplier(supplier);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<SupplierDto>(HttpStatusCode.BadRequest, errors);
        }
    }


    [HttpDelete("DeleteSupplier")]
    public async Task<Response<string>> DeleteSupplier(int id)
    {
        await _suplierService.DeleteSupplier(id);
        return new Response<string>("Deleted");
    }


}
