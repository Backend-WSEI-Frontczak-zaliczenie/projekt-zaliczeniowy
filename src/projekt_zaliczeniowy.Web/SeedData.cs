using projekt_zaliczeniowy.Core.ContributorAggregate;
using projekt_zaliczeniowy.Core.ProjectAggregate;
using projekt_zaliczeniowy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using projekt_zaliczeniowy.SharedKernel.Interfaces;

namespace projekt_zaliczeniowy.Web;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new ApplicationDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(), serviceProvider.GetRequiredService<IDomainEventDispatcher>()))
    {
        if (dbContext.Restaurants.Any())
          return;   // DB has been seeded
        

        PopulateTestData(dbContext);
    }
  }
  public static void PopulateTestData(ApplicationDbContext dbContext)
  {
    dbContext.Database.ExecuteSql($"-- Insert data into cities\r\nINSERT INTO cities (id, name) VALUES\r\n(1, 'Warszawa'),\r\n(2, 'Kraków'),\r\n(3, 'Gdańsk'),\r\n(4, 'Wrocław'),\r\n(5, 'Poznań');\r\n\r\n-- Insert data into regions\r\nINSERT INTO regions (id, name) VALUES\r\n(1, 'European'),\r\n(2, 'Asian'),\r\n(3, 'American');\r\n\r\n-- Insert data into restaurants_types\r\nINSERT INTO restaurants_types (id, name, region) VALUES\r\n(1, 'Italian', 1),\r\n(2, 'Spanish', 1),\r\n(3, 'French', 1),\r\n(4, 'German', 1),\r\n(5, 'British', 1),\r\n(6, 'Chinese', 2),\r\n(7, 'Japanese', 2),\r\n(8, 'Korean', 2),\r\n(9, 'Indian', 2),\r\n(10, 'American', 3);\r\n\r\n-- Insert data into restaurants\r\nINSERT INTO restaurants (id, name, type, city, adult_only, rating) VALUES\r\n(1, 'Restauracja 1', 1, 1, 0, 4.5),\r\n(2, 'Restauracja 2', 2, 1, 0, 4.3),\r\n(3, 'Restauracja 3', 3, 2, 1, 4.7),\r\n(4, 'Restauracja 4', 4, 2, 0, 4.0),\r\n(5, 'Restauracja 5', 5, 3, 1, 4.6),\r\n(6, 'Restauracja 6', 6, 3, 0, 4.2),\r\n(7, 'Restauracja 7', 7, 4, 1, 4.8),\r\n(8, 'Restauracja 8', 8, 4, 0, 4.1),\r\n(9, 'Restauracja 9', 9, 5, 1, 4.9),\r\n(10, 'Restauracja 10', 10, 5, 0, 4.4);\r\n\r\n-- Insert data into comments\r\nINSERT INTO comments (id, restaurant, comment) VALUES\r\n(1, 1, 'Bardzo dobre jedzenie!'),\r\n(2, 2, 'Przyjemna atmosfera i smaczne dania.'),\r\n(3, 3, 'Obsługa mogłaby być lepsza...'),\r\n(4, 4, 'Cudowne miejsce! Na pewno wrócimy.'),\r\n(5, 5, 'Zdecydowanie polecam to miejsce.'),\r\n(6, 6, 'Danania mogłyby być bardziej doprawione.'),\r\n(7, 7, 'Fantastyczny wybór win!'),\r\n(8, 8, 'Szczerze mówiąc, spodziewałem się czegoś więcej.'),\r\n(9, 9, 'Nie mogę się doczekać naszej kolejnej wizyty!'),\r\n(10, 10, 'Wspaniała restauracja z pięknym widokiem.');\r\n");
  }
}
