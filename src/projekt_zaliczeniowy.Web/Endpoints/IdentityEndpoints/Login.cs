using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using projekt_zaliczeniowy.Infrastructure.Data;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class Login : Endpoint<LoginRequest>
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;
  private readonly ILogger<Login> _logger;

  public Login(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<Login> logger)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _logger = logger;
  }

  public override void Configure()
  {
    Post(LoginRequest.Route);
    AllowAnonymous();
    //AllowFormData();
  }
  public override async Task HandleAsync(LoginRequest request,
  CancellationToken cancellationToken)
  {
    var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
    if (result.Succeeded)
    {
      await SendOkAsync();
      return;
    }

    AddError("Invalid Login Attempt");
    ThrowIfAnyErrors();
  }
}
