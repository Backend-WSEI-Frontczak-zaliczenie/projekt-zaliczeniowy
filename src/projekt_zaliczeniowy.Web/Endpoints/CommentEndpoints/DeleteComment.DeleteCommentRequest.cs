namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;

public class DeleteCommentRequest
{
  public const string Route = "/comments/delete";


  public int CommentId { get; set; }
}
