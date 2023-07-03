using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class RestaurantsType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Region { get; set; }

    public virtual Region? RegionNavigation { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
