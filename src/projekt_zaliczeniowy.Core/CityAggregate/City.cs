using Ardalis.GuardClauses;
using projekt_zaliczeniowy.SharedKernel;
using projekt_zaliczeniowy.SharedKernel.Interfaces;

namespace projekt_zaliczeniowy.Core.CityAggregate;
public class City: EntityBase, IAggregateRoot
{
    public string Name { get; set; } = null!;

    public City(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public void UpdateName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    }
}
