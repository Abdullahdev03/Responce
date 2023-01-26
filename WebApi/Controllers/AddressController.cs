using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("controller")]

public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("GetAddresses")]
    public async Task<Response<List<AddressDto>>> Get()
    {
        return await _addressService.GetAdresses();
    }

    [HttpPost("AddAddress")]
    public async  Task<Response<AddressDto>> Add(AddressDto address)
    {
        if (ModelState.IsValid)
        {
            return await _addressService.AddAddress(address);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("Update")]
    public async Task<Response<AddressDto>> UpdateAddress(AddressDto address)
    {
        if (ModelState.IsValid)
        {
            return await _addressService.UpdateAddress(address);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    
    [HttpDelete("Delete")]
    public async Task<Response<string>> Delete(int id)
    {
        await _addressService.DeleteAddress(id);
        return new Response<string>("Deleeeted");
    }

}