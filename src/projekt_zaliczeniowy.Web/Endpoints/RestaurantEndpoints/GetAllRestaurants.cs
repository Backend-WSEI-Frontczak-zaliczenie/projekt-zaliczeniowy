using System.Threading;
using Azure;
using FastEndpoints;
using Microsoft.OpenApi.Any;
using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Infrastructure.Data;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.SharedKernel.Interfaces;

namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

public class GetAllRestaurants: Endpoint<GetAllRestaurantsRequest, List<RestaurantRecord>>
{
  private readonly IRestaurantRepository _repository;

  public GetAllRestaurants(IRestaurantRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Get(GetAllRestaurantsRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetAllRestaurantsRequest req, CancellationToken ct)
  {
    var allRestaurants = _repository.getAllRestaurants();
    var allRestaurantsToReturn = new List<RestaurantRecord>();
    foreach (var item in allRestaurants)
    {
      var currentRestaurant = new RestaurantRecord(item.Id, item.Name, item.TypeNavigation?.Name, item.CityNavigation?.Name, item.AdultOnly, item.Rating);
      allRestaurantsToReturn.Add(currentRestaurant);
    }

    await SendAsync(allRestaurantsToReturn);
  }

}
