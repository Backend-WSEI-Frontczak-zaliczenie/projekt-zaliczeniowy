namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class AddCommentRequest
{
  public const string Route = "/comments/add";


  public int RestaurantId { get; set; }

  public string? TextContent { get; set; }
}
