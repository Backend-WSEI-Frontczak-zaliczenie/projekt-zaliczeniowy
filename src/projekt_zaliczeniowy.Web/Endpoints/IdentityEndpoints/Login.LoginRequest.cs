using Microsoft.AspNetCore.Mvc;
namespace projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

public class LoginRequest
{
    public const string Route = "/Identity/Login";

    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; } = false;
    [FromQuery]
    public string? ReturnUrl { get; set; }
}
