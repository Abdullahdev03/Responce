namespace Domain.Dtos;

public class AddressDto
{
    public int AddressId { get; set; } 
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public int CustomerId { get; set; }

}