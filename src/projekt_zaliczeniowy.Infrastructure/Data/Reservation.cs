using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class Reservation
{
    public int Id { get; set; }

    public int? Restaurant { get; set; }

    public string? Guest { get; set; }

    public DateTime Date { get; set; }

    public virtual AspNetUser? GuestNavigation { get; set; }

    public virtual Restaurant? RestaurantNavigation { get; set; }
}
