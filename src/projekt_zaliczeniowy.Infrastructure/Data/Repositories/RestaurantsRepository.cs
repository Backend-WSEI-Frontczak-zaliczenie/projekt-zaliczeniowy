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
  
  public Restaurant? Add(string name, string city, string type, decimal rating, bool adultOnly)
  {
    var lastRestaurant = _context.Restaurants.OrderBy(a => a.Id).LastOrDefault();
    int lastRestaurantId = (lastRestaurant != null && lastRestaurant?.Id != null) ? lastRestaurant.Id : 0;
    Restaurant restaurant = new Restaurant
    {
      Id = lastRestaurantId + 1,
      Name = name,
      City = _context.Cities.Where(a => a.Name == city).FirstOrDefault()?.Id,
      Type = _context.RestaurantsTypes.Where(a => a.Name == type).FirstOrDefault()?.Id,
      AdultOnly = adultOnly,
      Rating = rating
    };
    _context.Restaurants.Add(restaurant);
    _context.SaveChanges();
    return restaurant;
  }
  
  public Restaurant? Edit(int restaurantId, string name, string city, string type, decimal rating, bool adultOnly)
  {
    var restaurant = _context.Restaurants.Find(restaurantId);
    if (restaurant == null)
    {
      return null;
    }
    restaurant.Name = name;
    restaurant.City = _context.Cities.Where(a => a.Name == city).FirstOrDefault()?.Id;
    restaurant.Type = _context.RestaurantsTypes.Where(a => a.Name == type).FirstOrDefault()?.Id;
    restaurant.AdultOnly = adultOnly;
    restaurant.Rating = rating;
    _context.SaveChanges();
    return restaurant;
  }
  
  public Restaurant? Delete(int restaurantId)
  {
    var restaurant = _context.Restaurants.Find(restaurantId);
    var comments = _context.Comments.Where(a => a.Restaurant == restaurantId);
    if (restaurant == null)
    {
      return null;
    }
    foreach (var comment in comments)
    {
      _context.Comments.Remove(comment);
    }
    _context.Restaurants.Remove(restaurant);
    _context.SaveChanges();
    return restaurant;
  }
}
