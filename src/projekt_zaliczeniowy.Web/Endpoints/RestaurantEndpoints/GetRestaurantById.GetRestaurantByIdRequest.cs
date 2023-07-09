namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

public class GetRestaurantByIdRequest
{
  public const string Route = "/restaurants/{RestaurantId:int}";
  public static string BuildRoute(int restaurantId) => Route.Replace("{RestaurantId:int}", restaurantId.ToString());

  public int RestaurantId { get; set; }
}
