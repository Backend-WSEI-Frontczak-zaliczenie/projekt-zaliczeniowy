using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data;

namespace projekt_zaliczeniowy.Infrastructure.Repositories;
public interface IRestaurantRepository
{
  IEnumerable<Restaurant> getAllRestaurants();

  Restaurant? getRestaurantById(int id);
}
