using System.ComponentModel.DataAnnotations;

public class Supplier
{
  [Key]
  public int SuplierId { get; set; }
  public string CompanyName { get; set; }
  public string Phone { get; set; }
  public  List<Product> Products { get; set; }
}