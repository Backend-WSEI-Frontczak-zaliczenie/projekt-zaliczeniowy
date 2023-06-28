using Ardalis.Specification;

namespace projekt_zaliczeniowy.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
