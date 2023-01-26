public class Album
{
  public int AlbumId { get; set; }
  public int productId { get; set; }
  public Product Products { get; set; }
  public string AlbumName { get; set; }
  public string ArtistName { get; set; }
  public string Genre { get; set; }
  public DateTime RealiseDate { get; set; }
}