namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class AddRestaurantRequest
{
  public const string Route = "/restaurants/add";


  public required string Name { get; set; }
  public string? Type { get; set; }
  public string? City { get; set; }
  public bool AdultOnly { get; set; }
  public decimal Rating { get; set; }
}
