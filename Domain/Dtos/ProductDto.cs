namespace Domain.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public Decimal TotalAmount { get; set; }

}