namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class DeleteRestaurantRequest
{
  public const string Route = "/restaurants/delete";


  public int Id { get; set; }
}
