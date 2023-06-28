using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Core.ProjectAggregate;
using projekt_zaliczeniowy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace projekt_zaliczeniowy.Web;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      // Look for any TODO items.
      //if (dbContext. .Any())
      //{
      //  return;   // DB has been seeded
      //}

      PopulateTestData(dbContext);
    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {

  }
}
