using projekt_zaliczeniowy.Core.Interfaces;

namespace projekt_zaliczeniowy.Core.Services;

public class CommentValidationService: ICommentValidationService
{
  public List<string> ValidateComment(string textContent)
  {
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(textContent))
    {
      errors.Add("Treść komentarza jest wymagana.");
    }
    else if (textContent.Length > 1000)
    {
      errors.Add("Treść komentarza nie może przekraczać 1000 znaków.");
    }

    return errors;
  }
}
