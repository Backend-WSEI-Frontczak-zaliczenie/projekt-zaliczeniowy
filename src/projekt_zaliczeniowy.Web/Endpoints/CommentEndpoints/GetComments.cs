using FastEndpoints;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;


public class GetComments: Endpoint<GetCommentsRequest, List<CommentRecord>> {
  private readonly ICommentRepository _repository;

  public GetComments(ICommentRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Get(GetCommentsRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetCommentsRequest req, CancellationToken ct)
  {
    var comments = _repository.Get(req.RestaurantId);
    if (comments == null)
    {
      await SendAsync(new List<CommentRecord>());
      return;
    }
    
    var commentsToReturn = comments.Select(comment => new CommentRecord(comment.Id, comment.Restaurant, comment.Comment1)).ToList();
    await SendAsync(commentsToReturn);
  }
}

