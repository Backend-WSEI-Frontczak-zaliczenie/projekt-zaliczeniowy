namespace projekt_zaliczeniowy.Core.Interfaces;


  public interface ICommentValidationService
  {
    List<string> ValidateComment(string textContent);
  }

