using FastEndpoints;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.CommentEndpoints;
using projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;


public class EditRestaurant: Endpoint<EditRestaurantRequest, RestaurantRecord> {
  private readonly IRestaurantRepository _repository;

  public EditRestaurant(IRestaurantRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Post(EditRestaurantRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(EditRestaurantRequest req, CancellationToken ct)
  {
    if (req.City == null || req.Type == null || req.Name == null)
    {
      await SendErrorsAsync(400, ct);
      return;
    }
    var restaurant = _repository.Edit(req.Id, req.Name, req.City, req.Type, req.Rating, req.AdultOnly);
    if (restaurant == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }
    
    RestaurantRecord restaurantToReturn = new RestaurantRecord(restaurant.Id, restaurant.Name, restaurant.TypeNavigation?.Name, restaurant.CityNavigation?.Name, restaurant.AdultOnly, restaurant.Rating);
    
    await SendAsync(restaurantToReturn);
  }
}

