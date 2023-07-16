using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;

namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories;
internal class TypeRepository: ITypeRepository
{
  private readonly ApplicationDbContext _context;

  public TypeRepository(ApplicationDbContext context)
  {
    this._context = context;
  }
  
  public IEnumerable<RestaurantsType> getAllTypes()
  {
    return _context.RestaurantsTypes.ToList();
  }
}
