using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data;


namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;
public interface ITypeRepository
{
  IEnumerable<RestaurantsType>? getAllTypes();
}
