using FastEndpoints;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;


public class DeleteRestaurant: Endpoint<DeleteRestaurantRequest> {
  private readonly IRestaurantRepository _repository;

  public DeleteRestaurant(IRestaurantRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Post(DeleteRestaurantRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteRestaurantRequest req, CancellationToken ct)
  {
    var restaurant = _repository.Delete(req.Id);
    if (restaurant == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }

    await SendOkAsync(ct);
  }
}

