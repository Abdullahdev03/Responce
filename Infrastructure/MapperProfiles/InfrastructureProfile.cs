using AutoMapper;
using Domain.Dtos;

namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierDto, Supplier>();
    }
}