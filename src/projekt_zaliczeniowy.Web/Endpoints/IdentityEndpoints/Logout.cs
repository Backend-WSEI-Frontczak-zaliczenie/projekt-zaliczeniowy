using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class Logout : EndpointWithoutRequest
{
  private readonly SignInManager<IdentityUser> _signInManager;
  private readonly ILogger<Logout> _logger;

  public Logout( SignInManager<IdentityUser> signInManager, ILogger<Logout> logger)
  {
    _signInManager = signInManager;
    _logger = logger;
  }

  public override void Configure()
  {
    Post("/Identity/Logout");
  }
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    await _signInManager.SignOutAsync();
    await SendOkAsync();
    return;
  }
}
