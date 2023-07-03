using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
