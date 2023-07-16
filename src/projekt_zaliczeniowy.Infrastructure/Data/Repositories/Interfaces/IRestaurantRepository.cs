using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data;

namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
public interface IRestaurantRepository
{
  IEnumerable<Restaurant> getAllRestaurants(string? City, string? Region, string? Type);

  Restaurant? getRestaurantById(int id);
  
  Restaurant? Add(string name, string city, string type, decimal rating, bool adultOnly);
  
  Restaurant? Edit(int restaurantId, string name, string city, string type, decimal rating, bool adultOnly);
  
  Restaurant? Delete(int restaurantId);
}
