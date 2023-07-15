namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class EditRestaurantRequest
{
  public const string Route = "/restaurants/edit/{Id}";


  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Type { get; set; }
  public string? City { get; set; }
  public bool AdultOnly { get; set; }
  public decimal Rating { get; set; }
}
