using Ardalis.GuardClauses;
using projekt_zaliczeniowy.SharedKernel;
using projekt_zaliczeniowy.SharedKernel.Interfaces;

namespace projekt_zaliczeniowy.Core.ContributorAggregate;

public class Contributor : EntityBase, IAggregateRoot
{
  public string Name { get; private set; }

  public Contributor(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
}
