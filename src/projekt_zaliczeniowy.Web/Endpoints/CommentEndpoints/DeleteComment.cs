using FastEndpoints;
using projekt_zaliczeniowy.Core.Interfaces;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;


public class DeleteComment: Endpoint<DeleteCommentRequest> {
  private readonly ICommentRepository _repository;

  public DeleteComment(ICommentRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(DeleteCommentRequest.Route);
    Roles("Admin");
  }


  public override async Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
  {
    var comment = _repository.Delete(req.CommentId);
    if (comment == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }

    await SendOkAsync(ct);
  }
}

