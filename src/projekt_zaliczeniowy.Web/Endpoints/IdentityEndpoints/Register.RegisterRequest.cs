using Microsoft.AspNetCore.Mvc;

namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class RegisterRequest
{
  public const string Route = "/Identity/Register";

  public required string Email { get; set; }
  public required string Password { get; set; }
  [FromQuery]
  public string? ReturnUrl { get; set; }
}
