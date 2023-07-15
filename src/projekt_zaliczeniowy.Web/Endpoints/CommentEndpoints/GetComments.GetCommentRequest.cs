namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class GetCommentsRequest
{
  public const string Route = "/comments/get/{RestaurantId}";


  public int RestaurantId { get; set; }
}
