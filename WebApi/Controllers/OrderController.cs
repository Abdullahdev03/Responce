using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class OrderController: ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet("GetOrders")]
    public async Task<Response<List<OrderDto>>> GetOrders()
    {
        return await _orderService.GetOrders();
    }

    [HttpPost("AddOrder")]
    
    public async  Task<Response<OrderDto>> AddOrder(OrderDto order)
    {
        if (ModelState.IsValid)
        {
            return await _orderService.AddOrder(order);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }
    

    [HttpPut("UpdateOrder")]
    public async Task<Response<OrderDto>> UpdateOrder(OrderDto order)
    {
        if (ModelState.IsValid)
        {
            return await _orderService.UpdateOrder(order);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
        }
        
    }
    [HttpDelete("DeleteOrder")]
    public async Task<Response<string>> Delete(int id)
    {
        await _orderService.DeleteOrder(id);
        return new Response<string>("Deleted");
    }
    
}