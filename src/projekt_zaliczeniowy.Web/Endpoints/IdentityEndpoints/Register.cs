using Azure.Core;
using FastEndpoints;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text;
using projekt_zaliczeniowy.Infrastructure.Data;

namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class Register : Endpoint<RegisterRequest>
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;
  private readonly ILogger<Register> _logger;

  public Register(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<Register> logger)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _logger = logger;
  }

  public override void Configure()
  {
    Post(RegisterRequest.Route);
    AllowAnonymous();
    //AllowFormData();
  }
  public override async Task HandleAsync(RegisterRequest request,
    CancellationToken cancellationToken)
  {
    var returnUrl = request.ReturnUrl ?? "/";

    var user = new IdentityUser { UserName = request.Email, Email = request.Email };
    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded)
    {
      _logger.LogInformation("User created a new account with password.");
        
      if (_userManager.Options.SignIn.RequireConfirmedAccount)
      {
        await SendRedirectAsync(returnUrl);
        return;
      }
      else
      {
        await _signInManager.SignInAsync(user, isPersistent: false);
        await SendRedirectAsync(returnUrl);
        return;
      }
    }
    foreach (var error in result.Errors)
    {
      AddError(error.Description);
    }
    ThrowIfAnyErrors();
  }
}
