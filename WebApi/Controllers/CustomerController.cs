using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class CustomerController: ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet("GetCustomers")]
    public async Task<Response<List<CustomerDto>>> GetCustomers()
    {
        return await _customerService.GetCustomers();
    }

    [HttpPost("AddCustomer")]
    public async  Task<Response<CustomerDto>> AddCustomer(CustomerDto customer)
    {
        if (ModelState.IsValid)
        {
            return await _customerService.AddCustomer(customer);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }
    

    [HttpPut("UpdateCustomer")]
    public async Task<Response<CustomerDto>> UpdateCustomer(CustomerDto customer)
    {
        if (ModelState.IsValid)
        {
            return await _customerService.UpdateCustomer(customer);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    

    [HttpDelete("DeleteCustomer")]
    public async Task<Response<string>> Delete(int id)
    {
        await _customerService.DeleteCustomer(id);
        return new Response<string>("deleted");
    }


}