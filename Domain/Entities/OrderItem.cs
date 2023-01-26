public class OrderItem
{
  public int OrderId { get; set; } 
  public Order Orders { get; set; } 
  public int ProductId { get; set; }
  public Product Products { get; set; }
  public decimal UnitPrice { get; set; }
}