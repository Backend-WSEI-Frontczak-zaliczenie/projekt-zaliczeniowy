using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Core.ProjectAggregate.Specifications;
using projekt_zaliczeniowy.SharedKernel.Interfaces;
using projekt_zaliczeniowy.Web.Endpoints.ContributorEndpoints;
using FastEndpoints;
using Ardalis.Specification;
using System.Threading;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;

namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

public class GetRestaurantById: Endpoint<GetRestaurantByIdRequest, RestaurantRecord>
{
  private readonly IRestaurantRepository _repository;

  public GetRestaurantById(IRestaurantRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Get(GetRestaurantByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetRestaurantByIdRequest req, CancellationToken ct)
  {
    var restaurant = _repository.getRestaurantById(req.RestaurantId);
    if (restaurant == null)
    {
      await SendNotFoundAsync(ct);
      return;
    }
    var restaurantToReturn = new RestaurantRecord(restaurant.Id, restaurant.Name, restaurant.TypeNavigation?.Name, restaurant.CityNavigation?.Name, restaurant.AdultOnly, restaurant.Rating);


    await SendAsync(restaurantToReturn);
  }
}
