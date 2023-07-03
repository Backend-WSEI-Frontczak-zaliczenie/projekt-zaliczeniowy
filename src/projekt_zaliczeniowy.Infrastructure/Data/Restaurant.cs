using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Type { get; set; }

    public int? City { get; set; }

    public bool AdultOnly { get; set; }

    public decimal Rating { get; set; }

    public virtual City? CityNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual RestaurantsType? TypeNavigation { get; set; }
}
