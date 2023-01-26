public class Product
{
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public int SupplierId { get; set; }
  public Supplier Suppliers { get; set; }
  public int OrderId { get; set; }
  public Order Orders { get; set; }
  public DateTime OrderDate { get; set; }
  public Decimal TotalAmount { get; set; }


}