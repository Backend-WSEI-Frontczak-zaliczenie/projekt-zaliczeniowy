using Ardalis.Specification;
using projekt_zaliczeniowy.Core.ContributorAggregate;

namespace projekt_zaliczeniowy.Core.ProjectAggregate.Specifications;

public class ContributorByIdSpec : Specification<Contributor>, ISingleResultSpecification
{
  public ContributorByIdSpec(int contributorId)
  {
    Query
        .Where(contributor => contributor.Id == contributorId);
  }
}
