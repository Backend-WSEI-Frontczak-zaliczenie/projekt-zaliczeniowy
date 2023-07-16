using System.Threading;
using Azure;
using FastEndpoints;
using Microsoft.OpenApi.Any;
using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Infrastructure.Data;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.SharedKernel.Interfaces;
namespace projekt_zaliczeniowy.Web.Endpoints.RegionEndpoints;


public class GetRegions: Endpoint<GetRegionsRequest, IEnumerable<RegionRecord>> {
  private readonly IRegionRepository _repository;

  public GetRegions(IRegionRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Get(GetRegionsRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetRegionsRequest req, CancellationToken ct)
  {
    var regions = _repository.getAllRegions();
    if (regions == null)
    {
      await SendAsync(new RegionRecord[] { });
      return;
    }

    var regionRecords = regions.Select(region => new RegionRecord(region.Id, region.Name));
    
    await SendAsync(regionRecords);
  }
  
}
