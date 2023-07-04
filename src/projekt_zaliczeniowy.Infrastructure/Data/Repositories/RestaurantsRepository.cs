using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt_zaliczeniowy.Infrastructure.Data;

namespace projekt_zaliczeniowy.Infrastructure.Repositories;
public class RestaurantsRepository: IRestaurantRepository
{
  private readonly ApplicationDbContext _context;

  public RestaurantsRepository(ApplicationDbContext context)
  {
    this._context = context;
  }

  public IEnumerable<Restaurant> getAllRestaurants()
  {
    return _context.Restaurants.Include(a => a.CityNavigation).Include(a => a.TypeNavigation).ToList();
  }

  public Restaurant? getRestaurantById(int id)
  {
    return _context.Restaurants.Include(a => a.CityNavigation).Include(a => a.TypeNavigation).Where(a=> a.Id == id).FirstOrDefault();
  }
}
