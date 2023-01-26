using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SuplierService
{
    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public SuplierService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    public async Task<Response<List<SupplierDto>>> GetSuppliers()
    {
        try
        {
            var result = await _contex.Suppliers.ToListAsync();
            var mapped = _mapper.Map<List<SupplierDto>>(result);
            return new Response<List<SupplierDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<SupplierDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    public async Task<Response<SupplierDto>> AddSupplier(SupplierDto supplier)
    {
        try
        {
            var existingStudent = _contex.Suppliers.Where(x => x.SuplierId == supplier.SuplierId).FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<SupplierDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Suplier with this Id already exists" });
            }
            var mapped = _mapper.Map<Supplier>(supplier);
            await _contex.Suppliers.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            return new Response<SupplierDto>(supplier);
        }
        catch (Exception ex)
        {
            return new Response<SupplierDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }


    public async Task<Response<SupplierDto>> UpdateSupplier(SupplierDto supplier)
    {
        try
        {
            var update = _contex.Suppliers.Where(x => x.SuplierId != supplier.SuplierId).AsNoTracking().FirstOrDefault();
            if (update == null) return new Response<SupplierDto>(HttpStatusCode.BadRequest, new List<string>() { "Errors" });

            var mapped = _mapper.Map<Supplier>(supplier);
            _contex.Suppliers.Update(mapped);
            return new Response<SupplierDto>(supplier);
        }
        catch (Exception ex)
        {
            return new Response<SupplierDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteSupplier(int id)
    {
        var delete = await _contex.Suppliers.FirstAsync(x => x.SuplierId == id);
        _contex.Suppliers.Remove(delete);
        await _contex.SaveChangesAsync();
        return new Response<string>("Deleted");
    }
    

}