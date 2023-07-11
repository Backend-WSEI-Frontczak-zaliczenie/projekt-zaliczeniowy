using FastEndpoints;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;


public class AddComment: Endpoint<AddCommentRequest, CommentRecord> {
  private readonly ICommentRepository _repository;

  public AddComment(ICommentRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Post(AddCommentRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(AddCommentRequest req, CancellationToken ct)
  {
    string textContentToAdd = req.TextContent ?? "";
    var comment = _repository.Add(req.RestaurantId, textContentToAdd);
    if (comment == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }
    int restaurantId = comment.Restaurant ?? req.RestaurantId;
    var commentToReturn = new CommentRecord(comment.Id, restaurantId, comment.Comment1);


    await SendAsync(commentToReturn);
  }
}

