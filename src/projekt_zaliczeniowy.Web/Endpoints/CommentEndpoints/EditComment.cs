using FastEndpoints;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;


public class EditComment: Endpoint<EditCommentRequest, CommentRecord> {
  private readonly ICommentRepository _repository;
  private readonly IRestaurantRepository _restaurantRepository;

  public EditComment(ICommentRepository repository, IRestaurantRepository restaurantRepository)
  {
    _repository = repository;
    _restaurantRepository = restaurantRepository;
  }

  public override void Configure()
  {
    Post(EditCommentRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(EditCommentRequest req, CancellationToken ct)
  {
    string textContentToEdit = req.TextContent ?? "";
    
    var comment = _repository.Edit(req.CommentId, textContentToEdit);
    if (comment == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }

    var restaurant = _restaurantRepository.getRestaurantById(comment.Restaurant);
    if (restaurant == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }
    
    var commentToReturn = new CommentRecord(comment.Id, restaurant.Id, comment.Comment1);
    
    await SendAsync(commentToReturn);
  }
}

