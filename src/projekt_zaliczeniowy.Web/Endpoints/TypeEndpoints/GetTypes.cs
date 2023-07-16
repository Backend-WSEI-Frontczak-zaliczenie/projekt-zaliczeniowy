using System.Threading;
using Azure;
using FastEndpoints;
using Microsoft.OpenApi.Any;
using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Infrastructure.Data;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
using projekt_zaliczeniowy.SharedKernel.Interfaces;

namespace projekt_zaliczeniowy.Web.Endpoints.TypeEndpoints;


public class GetTypes: Endpoint<GetTypesRequest, IEnumerable<TypeRecord>> {
  private readonly ITypeRepository _repository;

  public GetTypes(ITypeRepository repository)
  {
    _repository = repository;
  }
  public override void Configure()
  {
    Get(GetTypesRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetTypesRequest req, CancellationToken ct)
  {
    var types = _repository.getAllTypes();
    if (types == null)
    {
      await SendErrorsAsync(500, ct);
      return;
    }
    
    var typeRecords = types.Select(type => new TypeRecord(type.Id, type.Name));

    await SendOkAsync(typeRecords, ct);
  }
  
}

