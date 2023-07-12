namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class EditCommentRequest
{
  public const string Route = "/comments/edit/{CommentId}";


  public int CommentId { get; set; }

  public string? TextContent { get; set; }
}
