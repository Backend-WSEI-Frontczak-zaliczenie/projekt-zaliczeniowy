using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class Info : EndpointWithoutRequest
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly ILogger<Info> _logger;

  public Info(UserManager<IdentityUser> userManager, ILogger<Info> logger)
  {
    _userManager = userManager;
    _logger = logger;
  }

  public override void Configure()
  {
    Post("/Identity/Info");
  }
  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var user = await _userManager.GetUserAsync(HttpContext.User);
    if(user is null || user.Email is null)
    {
      AddError("Invalid user");
      ThrowIfAnyErrors();
      return;
    }
    var roles = HttpContext.User.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToArray();

    await SendAsync(new UserRecord(user.Email, roles));
  }
}
