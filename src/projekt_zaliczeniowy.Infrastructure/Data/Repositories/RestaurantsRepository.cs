using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt_zaliczeniowy.Infrastructure.Data;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;

namespace projekt_zaliczeniowy.Infrastructure.Repositories;
public class RestaurantsRepository: IRestaurantRepository
{
  private readonly ApplicationDbContext _context;

  public RestaurantsRepository(ApplicationDbContext context)
  {
    this._context = context;
  }

  public IEnumerable<Restaurant> getAllRestaurants(string? City, string? Region, string? Type)
  {

    return _context.Restaurants
     .Include(a => a.CityNavigation)
     .Include(a => a.TypeNavigation)
     .ThenInclude(t => t != null ? t.RegionNavigation : null)
     .Where(a => City == null || (a.CityNavigation != null && a.CityNavigation.Name == City))
     .Where(a => Type == null || (a.TypeNavigation != null && a.TypeNavigation.Name == Type))
     .Where(a => Region == null || (a.TypeNavigation != null && a.TypeNavigation.RegionNavigation != null && a.TypeNavigation.RegionNavigation.Name == Region))
     .ToList();
  }

  public Restaurant? getRestaurantById(int id)
  {
    return _context.Restaurants.Include(a => a.CityNavigation).Include(a => a.TypeNavigation).Where(a=> a.Id == id).FirstOrDefault();
  }
}
