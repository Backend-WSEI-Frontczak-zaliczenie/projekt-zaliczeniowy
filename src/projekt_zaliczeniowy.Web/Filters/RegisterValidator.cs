using FastEndpoints;
using FluentValidation;
using projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

namespace projekt_zaliczeniowy.Web.Filters;

public class LoginValidator : Validator<LoginRequest>
{
  public LoginValidator()
  {
    RuleFor(x => x.Email)
      .EmailAddress();
  }
}
