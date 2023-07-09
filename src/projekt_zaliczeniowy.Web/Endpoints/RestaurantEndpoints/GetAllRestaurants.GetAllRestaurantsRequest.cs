namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

public class GetAllRestaurantsRequest
{
  public const string Route = "/restaurants/getAll";
  public string? City { get; set; }
  public string? Region { get; set; }
  public string? Type { get; set; }
}
