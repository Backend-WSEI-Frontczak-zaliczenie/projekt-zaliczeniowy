using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_zaliczeniowy.Infrastructure.Data.Repositories.Interfaces;

namespace projekt_zaliczeniowy.Infrastructure.Data.Repositories;

internal class RegionRepository: IRegionRepository
{
  private readonly ApplicationDbContext _context;

  public RegionRepository(ApplicationDbContext context)
  {
    this._context = context;
  }
  
  public IEnumerable<Region> getAllRegions()
  {
    return _context.Regions.ToList();
  }
}
