using FastEndpoints;
using FluentValidation;
using projekt_zaliczeniowy.Web.Endpoints.IdentityEndpoints;

namespace projekt_zaliczeniowy.Web.Filters;

public class RegisterValidator : Validator<RegisterRequest>
{
  public RegisterValidator()
  {
    RuleFor(x => x.Email)
      .EmailAddress();
    RuleFor(x => x.Password)
      .MinimumLength(6);
  }
}
