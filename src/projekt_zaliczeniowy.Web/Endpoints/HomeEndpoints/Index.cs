using System.Reflection;
using FastEndpoints;

namespace projekt_zaliczeniowy.Web.Endpoints.HomeEndpoints;

public class Index : Endpoint<GetIndexRequest, string>
{
  public override void Configure()
  {
    Get(GetIndexRequest.Route);
    AllowAnonymous();
  }
  public override async Task HandleAsync(GetIndexRequest request,
    CancellationToken cancellationToken)
  {
    var assemblyName = Assembly.GetAssembly(typeof(Index))!.GetName();

    await SendAsync($"Started application: {assemblyName.Name}, ver: {assemblyName.Version}", cancellation: cancellationToken);
  }
}
