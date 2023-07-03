using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RestaurantsType> RestaurantsTypes { get; set; } = new List<RestaurantsType>();
}
