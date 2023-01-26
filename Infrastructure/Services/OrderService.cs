using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class OrderService
{
    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public OrderService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderDto>>> GetOrders()
    {
        try
        {
            var result = await _contex.Orders.ToListAsync();
            var mapped = _mapper.Map<List<OrderDto>>(result);
            return new Response<List<OrderDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<OrderDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<OrderDto>> AddOrder(OrderDto order)
    {
        try
        {
            var existingStudent = _contex.Orders.Where(x => x.OrderId != order.OrderId && x.CustomerId != order.CustomerId).FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<OrderDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Order with this Id already exists" });
            }
            var mapped = _mapper.Map<Address>(order);
            await _contex.Addresses.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            return new Response<OrderDto>(order);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    

    public async Task<Response<OrderDto>> UpdateOrder(OrderDto order)
    {
        try
        {
            var update = _contex.Orders.Where(x => x.OrderId == order.OrderId).AsNoTracking().FirstOrDefault();
            if (update == null)
                return new Response<OrderDto>(HttpStatusCode.BadRequest, new List<string>{"Not Found"});

            var mapped = _mapper.Map<Order>(order);
            _contex.Orders.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<OrderDto>(order);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteOrder(int id)
    {
        var delete = await _contex.Orders.FirstAsync(x => x.OrderId == id);
        _contex.Orders.Remove(delete);
        await _contex.SaveChangesAsync();
        return new Response<string>("Deleted");

    }

}
