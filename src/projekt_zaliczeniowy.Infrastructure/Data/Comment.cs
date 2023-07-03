using System;
using System.Collections.Generic;

namespace projekt_zaliczeniowy.Infrastructure.Data;

public partial class Comment
{
    public int Id { get; set; }

    public int? Restaurant { get; set; }

    public string Comment1 { get; set; } = null!;

    public virtual Restaurant? RestaurantNavigation { get; set; }
}
